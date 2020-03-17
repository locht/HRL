Imports System.Linq.Expressions
Imports LinqKit.Extensions
Imports System.Data.Common
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic
Imports System.Data.Entity
Imports System.Text.RegularExpressions
Imports System.Data.Objects
Imports System.Reflection

Partial Public Class PayrollRepository

#Region "SalaryLevelGroup list"

    Public Function GetSalaryLevelGroup(ByVal _filter As SalaryLevelGroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of SalaryLevelGroupDTO)

        Try
            Dim query = From p In Context.PA_SALARY_LEVEL_GROUP

            Dim lst = query.Select(Function(p) New SalaryLevelGroupDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME_VN = p.NAME_VN,
                                       .REMARK = p.REMARK,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .ACTFLG_DB = p.ACTFLG,
                                       .ORDERS = p.ORDERS,
                                       .CREATED_DATE = p.CREATED_DATE})

            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            If _filter.ACTFLG_DB <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG_DB.ToUpper.Contains(_filter.ACTFLG_DB.ToUpper))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function

    Public Function InsertSalaryLevelGroup(ByVal objSalaryLevel As SalaryLevelGroupDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objSalaryLevelData As New PA_SALARY_LEVEL_GROUP
        Try
            objSalaryLevelData.ID = Utilities.GetNextSequence(Context, Context.PA_SALARY_LEVEL_GROUP.EntitySet.Name)
            objSalaryLevelData.CODE = objSalaryLevel.CODE.Trim
            objSalaryLevelData.NAME_VN = objSalaryLevel.NAME_VN.Trim
            objSalaryLevelData.REMARK = objSalaryLevel.REMARK
            objSalaryLevelData.ACTFLG = objSalaryLevel.ACTFLG
            objSalaryLevelData.ORDERS = objSalaryLevel.ORDERS
            objSalaryLevelData.OTHER_USE = "ALL"
            Context.PA_SALARY_LEVEL_GROUP.AddObject(objSalaryLevelData)
            Context.SaveChanges(log)
            gID = objSalaryLevelData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateSalaryLevelGroup(ByVal _validate As SalaryLevelGroupDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_SALARY_LEVEL_GROUP
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PA_SALARY_LEVEL_GROUP
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.PA_SALARY_LEVEL_GROUP
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.PA_SALARY_LEVEL_GROUP
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ValidateCheckExistSalaryLevelGroup(ByVal lstSalaryLevels As List(Of Decimal)) As Boolean

        Try
            Dim q As Integer = (From s In Context.PA_SALARY_LEVEL
                                Join r In Context.PA_SALARY_GROUP On s.SAL_GROUP_ID Equals r.ID
                                Join l In Context.PA_SALARY_LEVEL_GROUP On s.GRADE_GROUP Equals l.ID
                                Where lstSalaryLevels.Contains(l.ID) Or (lstSalaryLevels.Contains(l.ID) And l.ACTFLG = "A")).Count

            If q > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifySalaryLevelGroup(ByVal objSalaryLevel As SalaryLevelGroupDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objSalaryLevelData As New PA_SALARY_LEVEL_GROUP With {.ID = objSalaryLevel.ID}
        Try
            Context.PA_SALARY_LEVEL_GROUP.Attach(objSalaryLevelData)
            objSalaryLevelData.CODE = objSalaryLevel.CODE.Trim
            objSalaryLevelData.NAME_VN = objSalaryLevel.NAME_VN.Trim
            objSalaryLevelData.REMARK = objSalaryLevel.REMARK
            objSalaryLevelData.ORDERS = objSalaryLevel.ORDERS
            objSalaryLevelData.OTHER_USE = "ALL"
            Context.SaveChanges(log)
            gID = objSalaryLevelData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function


    Public Function ActiveSalaryLevelGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_SALARY_LEVEL_GROUP)
        Dim lstData1 As List(Of PA_SALARY_LEVEL)
        Try
            lstData = (From p In Context.PA_SALARY_LEVEL_GROUP Where lstID.Contains(p.ID)).ToList
            lstData1 = (From p In Context.PA_SALARY_LEVEL Where lstID.Contains(p.GRADE_GROUP)).ToList
            For index = 0 To lstData.Count - 1

                For index1 = 0 To lstData1.Count - 1
                    If lstData(index).ID.Equals(lstData1(index1).GRADE_GROUP) Then
                        lstData1(index1).ACTFLG = bActive
                    End If
                Next
                lstData(index).ACTFLG = bActive
            Next

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteSalaryLevelGroup(ByVal lstSalaryLevel As List(Of Decimal)) As Boolean
        Dim lstSalaryLevelData As List(Of PA_SALARY_LEVEL_GROUP)
        Try
            lstSalaryLevelData = (From p In Context.PA_SALARY_LEVEL_GROUP Where lstSalaryLevel.Contains(p.ID)).ToList
            For idx = 0 To lstSalaryLevelData.Count - 1
                Context.PA_SALARY_LEVEL_GROUP.DeleteObject(lstSalaryLevelData(idx))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region

End Class


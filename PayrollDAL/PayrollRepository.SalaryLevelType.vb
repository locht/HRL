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

#Region "SalaryLevelType list"

    Public Function GetSalaryLevelTypeList() As List(Of SalaryLevelGroupDTO)

        Try
            Dim query = (From p In Context.PA_SALARY_LEVEL_GROUP
                         Where p.ACTFLG = "A" Order By p.CREATED_DATE Descending
                         Select New SalaryLevelGroupDTO With {
                             .ID = p.ID,
                             .NAME_VN = p.NAME_VN
                         }).ToList

            Return query

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function

    Public Function InsertSalaryLevelType(ByVal objSalaryLevel As SalaryLevelTypeDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objSalaryLevelData As New PA_SALARY_LEVEL
        Try
            objSalaryLevelData.ID = Utilities.GetNextSequence(Context, Context.PA_SALARY_LEVEL.EntitySet.Name)
            Context.PA_SALARY_LEVEL.AddObject(objSalaryLevelData)
            Context.SaveChanges(log)
            gID = objSalaryLevelData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateSalaryLevelType(ByVal _validate As SalaryLevelTypeDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_SALARY_LEVEL
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PA_SALARY_LEVEL
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.PA_SALARY_LEVEL
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.PA_SALARY_LEVEL
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

    Public Function ModifySalaryLevelType(ByVal objSalaryLevel As SalaryLevelTypeDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objSalaryLevelData As New PA_SALARY_LEVEL With {.ID = objSalaryLevel.ID}
        Try
            Context.PA_SALARY_LEVEL.Attach(objSalaryLevelData)
            Context.SaveChanges(log)
            gID = objSalaryLevelData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveSalaryLevelType(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_SALARY_LEVEL)
        Try
            lstData = (From p In Context.PA_SALARY_LEVEL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                For Each item In lstData(index).PA_SALARY_RANK
                    item.ACTFLG = bActive
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

    Public Function DeleteSalaryLevelType(ByVal lstSalaryLevelType As List(Of Decimal)) As Boolean
        Dim lstSalaryLevelData As List(Of PA_SALARY_LEVEL_TYPE)
        Try
            lstSalaryLevelData = (From p In Context.PA_SALARY_LEVEL_TYPE Where lstSalaryLevelType.Contains(p.ID)).ToList
            For idx = 0 To lstSalaryLevelData.Count - 1

                Context.PA_SALARY_LEVEL_TYPE.DeleteObject(lstSalaryLevelData(idx))
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


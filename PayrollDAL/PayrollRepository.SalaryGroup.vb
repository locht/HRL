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

#Region "SalaryGroup list"

    Public Function GetSalaryGroup(ByVal _filter As SalaryGroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryGroupDTO)

        Try
            Dim query = From p In Context.PA_SALARY_GROUP
                        Where p.IS_DELETED = 0

            If _filter.CODE <> "" Then
                query = query.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If

            If _filter.NAME <> "" Then
                query = query.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If

            If _filter.EFFECT_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
            End If

            If _filter.IS_COEFFICIENT IsNot Nothing Then
                query = query.Where(Function(p) p.IS_COEFFICIENT = _filter.IS_COEFFICIENT)
            End If

            Dim lst = query.Select(Function(p) New SalaryGroupDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .EFFECT_DATE = p.EFFECT_DATE,
                                       .IS_INCENTIVE = p.IS_INCENTIVE,
                                       .IS_COEFFICIENT = p.IS_COEFFICIENT,
                                       .ORDERS = p.ORDERS,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")})


            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function

    Public Function GetEffectSalaryGroup() As SalaryGroupDTO
        Try
            Dim query = From p In Context.PA_SALARY_GROUP
            Where p.EFFECT_DATE <= Date.Now And p.ACTFLG.ToUpper = "A" And p.IS_DELETED = 0
                 Order By p.EFFECT_DATE Descending, p.ORDERS, p.CREATED_DATE Descending

            Dim EffectSalaryGroup = query.Select(Function(p) New SalaryGroupDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .EFFECT_DATE = p.EFFECT_DATE,
                                       .IS_INCENTIVE = p.IS_INCENTIVE,
                                       .IS_COEFFICIENT = p.IS_COEFFICIENT,
                                       .ORDERS = p.ORDERS,
                                       .CREATED_DATE = p.CREATED_DATE}).FirstOrDefault

            Return EffectSalaryGroup
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function

    ''' <summary>
    ''' Lay data vao combo cho bang luong
    ''' </summary>
    ''' <param name="dateValue">Ma bang luong</param>
    ''' <param name="isBlank">0: Khong lay dong empty; 1: Co lay dong empty</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSalaryGroupCombo(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_PA_SAL_GROUP",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_DATE = dateValue,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Insert data cho Salary Group
    ''' </summary>
    ''' <param name="objSalaryGroup"></param>
    ''' <param name="log"></param>
    ''' <param name="gID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertSalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objSalaryGroupData As New PA_SALARY_GROUP
        Try
            objSalaryGroupData.ID = Utilities.GetNextSequence(Context, Context.PA_SALARY_GROUP.EntitySet.Name)
            objSalaryGroupData.CODE = objSalaryGroup.CODE.Trim
            objSalaryGroupData.NAME = objSalaryGroup.NAME.Trim
            objSalaryGroupData.EFFECT_DATE = objSalaryGroup.EFFECT_DATE
            objSalaryGroupData.REMARK = objSalaryGroup.REMARK
            objSalaryGroupData.IS_INCENTIVE = objSalaryGroup.IS_INCENTIVE
            objSalaryGroupData.IS_COEFFICIENT = objSalaryGroup.IS_COEFFICIENT
            objSalaryGroupData.ORDERS = objSalaryGroup.ORDERS
            objSalaryGroupData.ACTFLG = objSalaryGroup.ACTFLG
            objSalaryGroupData.IS_DELETED = objSalaryGroup.IS_DELETED
            Context.PA_SALARY_GROUP.AddObject(objSalaryGroupData)
            Context.SaveChanges(log)


            Context.SaveChanges(log)
            gID = objSalaryGroupData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateSalaryGroup(ByVal _validate As SalaryGroupDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_SALARY_GROUP
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.IS_DELETED = 0 _
                             And p.ID <> _validate.ID).SingleOrDefault
                Else
                    query = (From p In Context.PA_SALARY_GROUP
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.IS_DELETED = 0).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_SALARY_GROUP
                             Where p.ACTFLG.ToUpper = "A" _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If

            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifySalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objSalaryGroupData As New PA_SALARY_GROUP With {.ID = objSalaryGroup.ID}
        Try
            Context.PA_SALARY_GROUP.Attach(objSalaryGroupData)
            objSalaryGroupData.CODE = objSalaryGroup.CODE.Trim
            objSalaryGroupData.NAME = objSalaryGroup.NAME.Trim
            objSalaryGroupData.EFFECT_DATE = objSalaryGroup.EFFECT_DATE
            objSalaryGroupData.REMARK = objSalaryGroup.REMARK
            objSalaryGroupData.IS_INCENTIVE = objSalaryGroup.IS_INCENTIVE
            objSalaryGroupData.IS_COEFFICIENT = objSalaryGroup.IS_COEFFICIENT
            objSalaryGroupData.ORDERS = objSalaryGroup.ORDERS

            Context.SaveChanges(log)
            gID = objSalaryGroupData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveSalaryGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstSalaryGroupData As List(Of PA_SALARY_GROUP)
        Try
            lstSalaryGroupData = (From p In Context.PA_SALARY_GROUP Where lstID.Contains(p.ID)).ToList

            For idx = 0 To lstSalaryGroupData.Count - 1
                For Each item In lstSalaryGroupData(idx).PA_SALARY_LEVEL
                    For Each item1 In item.PA_SALARY_RANK
                        item1.ACTFLG = bActive
                    Next
                    item.ACTFLG = bActive
                Next
                lstSalaryGroupData(idx).ACTFLG = bActive
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteSalaryGroup(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstSalaryGroupData As List(Of PA_SALARY_GROUP)
        Try
            lstSalaryGroupData = (From p In Context.PA_SALARY_GROUP Where lstID.Contains(p.ID)).ToList

            For idx = 0 To lstSalaryGroupData.Count - 1
                For Each item In lstSalaryGroupData(idx).PA_SALARY_LEVEL
                    For Each item1 In item.PA_SALARY_RANK
                        Context.PA_SALARY_RANK.DeleteObject(item1)
                    Next
                    Context.PA_SALARY_LEVEL.DeleteObject(item)
                Next

                Context.PA_SALARY_GROUP.DeleteObject(lstSalaryGroupData(idx))
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


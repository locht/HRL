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

#Region "SalaryExRate list"

    Public Function GetSalaryExRate(ByVal _filter As SalaryExRateDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryExRateDTO)

        Try
            Dim query = From p In Context.PA_SALARY_EXCHANGE_RATE
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

            Dim lst = query.Select(Function(p) New SalaryExRateDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .FOREIGN_CURRENCY_TYPE = p.FOREIGN_CURRENCY_TYPE,
                                       .EFFECT_DATE = p.EFFECT_DATE,
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

    Public Function GetEffectSalaryExRate() As SalaryExRateDTO
        Try
            Dim query = From p In Context.PA_SALARY_EXCHANGE_RATE
                        Where p.EFFECT_DATE <= Date.Now And p.ACTFLG.ToUpper = "A" And p.IS_DELETED = 0
                        Order By p.EFFECT_DATE Descending, p.ORDERS, p.CREATED_DATE Descending

            Dim EffectSalaryExRate = query.Select(Function(p) New SalaryExRateDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .EFFECT_DATE = p.EFFECT_DATE,
                                       .FOREIGN_CURRENCY_TYPE = p.FOREIGN_CURRENCY_TYPE,
                                       .ORDERS = p.ORDERS,
                                       .CREATED_DATE = p.CREATED_DATE}).FirstOrDefault

            Return EffectSalaryExRate
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
    Public Function GetSalaryExRateCombo(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_PA_SAL_EX_RATE",
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
    ''' Insert data cho Salary ExRate
    ''' </summary>
    ''' <param name="objSalaryExRate"></param>
    ''' <param name="log"></param>
    ''' <param name="gID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertSalaryExRate(ByVal objSalaryExRate As SalaryExRateDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objSalaryExRateData As New PA_SALARY_EXCHANGE_RATE
        Try
            objSalaryExRateData.ID = Utilities.GetNextSequence(Context, Context.PA_SALARY_EXCHANGE_RATE.EntitySet.Name)
            objSalaryExRateData.CODE = objSalaryExRate.CODE.Trim
            objSalaryExRateData.NAME = objSalaryExRate.NAME.Trim
            objSalaryExRateData.EFFECT_DATE = objSalaryExRate.EFFECT_DATE
            objSalaryExRateData.REMARK = objSalaryExRate.REMARK
            objSalaryExRateData.ORDERS = objSalaryExRate.ORDERS
            objSalaryExRateData.ACTFLG = objSalaryExRate.ACTFLG
            objSalaryExRateData.IS_DELETED = objSalaryExRate.IS_DELETED
            Context.PA_SALARY_EXCHANGE_RATE.AddObject(objSalaryExRateData)
            Context.SaveChanges(log)


            Context.SaveChanges(log)
            gID = objSalaryExRateData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateSalaryExRate(ByVal _validate As SalaryExRateDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_SALARY_EXCHANGE_RATE
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.IS_DELETED = 0 _
                             And p.ID <> _validate.ID).SingleOrDefault
                Else
                    query = (From p In Context.PA_SALARY_EXCHANGE_RATE
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.IS_DELETED = 0).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_SALARY_EXCHANGE_RATE
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

    Public Function ModifySalaryExRate(ByVal objSalaryExRate As SalaryExRateDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objSalaryExRateData As New PA_SALARY_EXCHANGE_RATE With {.ID = objSalaryExRate.ID}
        Try
            Context.PA_SALARY_EXCHANGE_RATE.Attach(objSalaryExRateData)
            objSalaryExRateData.CODE = objSalaryExRate.CODE.Trim
            objSalaryExRateData.NAME = objSalaryExRate.NAME.Trim
            objSalaryExRateData.EFFECT_DATE = objSalaryExRate.EFFECT_DATE
            objSalaryExRateData.REMARK = objSalaryExRate.REMARK
            objSalaryExRateData.ORDERS = objSalaryExRate.ORDERS
            objSalaryExRateData.FOREIGN_CURRENCY_TYPE = objSalaryExRate.FOREIGN_CURRENCY_TYPE

            Context.SaveChanges(log)
            gID = objSalaryExRateData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveSalaryExRate(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstSalaryExRateData As List(Of PA_SALARY_EXCHANGE_RATE)
        Try
            lstSalaryExRateData = (From p In Context.PA_SALARY_EXCHANGE_RATE Where lstID.Contains(p.ID)).ToList

            For idx = 0 To lstSalaryExRateData.Count - 1
                lstSalaryExRateData(idx).ACTFLG = bActive
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteSalaryExRate(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstSalaryExRateData As List(Of PA_SALARY_EXCHANGE_RATE)
        Try
            lstSalaryExRateData = (From p In Context.PA_SALARY_EXCHANGE_RATE Where lstID.Contains(p.ID)).ToList

            For idx = 0 To lstSalaryExRateData.Count - 1
                Context.PA_SALARY_EXCHANGE_RATE.DeleteObject(lstSalaryExRateData(idx))
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


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

#Region "Allowance_list "
    Public Function GetAllowanceList(ByVal _filter As AllowanceListDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AllowanceListDTO)

        Try
            Dim query = From p In Context.HU_ALLOWANCE_LIST
                        From s In Context.OT_OTHER_LIST.Where(Function(e) e.ID = p.ALLOWANCE_TYPE).DefaultIfEmpty()
            'Join s In Context.OT_OTHER_LIST On p.ALLOWANCE_TYPE Equals (s.ID)

            Dim lst = query.Select(Function(e) New AllowanceListDTO With {
                                       .ID = e.p.ID,
                                       .CODE = e.p.CODE,
                                       .NAME = e.p.NAME,
                                       .REMARK = e.p.REMARK,
                                       .ACTFLG = e.p.ACTFLG,
                                       .CREATED_DATE = e.p.CREATED_DATE,
                                       .ALLOWANCE_TYPE = e.p.ALLOWANCE_TYPE,
                                       .ORDERS = e.p.ORDERS,
                                       .IS_CONTRACT = e.p.IS_CONTRACT,
                                       .IS_INSURANCE = e.p.IS_INSURANCE,
                                       .IS_PAY = e.p.IS_PAY,
                                       .ALLOWANCE_TYPE_NAME = e.s.NAME_VN
                                   })

            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            If _filter.ALLOWANCE_TYPE <> 0 Then
                lst = lst.Where(Function(p) p.ALLOWANCE_TYPE.Equals(_filter.ALLOWANCE_TYPE))
            End If

            lst = lst.OrderBy(Sorts)


            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function
    Public Function GetAllowance(ByVal _filter As AllowanceDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " CREATED_DATE desc") As List(Of AllowanceDTO)
        Try
            Dim lst = (From p In Context.HU_ALLOWANCE
                       From e In Context.HU_EMPLOYEE.Where(Function(e) e.ID = p.EMPLOYEE_ID).DefaultIfEmpty()
                        From o In Context.HU_ALLOWANCE_LIST.Where(Function(o) o.ID = p.ALLOWANCE_TYPE).DefaultIfEmpty()
           Select New AllowanceDTO With {
                                        .ID = p.ID,
                                        .ALLOWANCE_TYPE = p.ALLOWANCE_TYPE,
                                        .ALLOWANCE_TYPE_NAME = o.NAME,
                                        .AMOUNT = p.AMOUNT,
                                        .EFFECT_DATE = p.EFFECT_DATE,
                                        .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                        .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                        .FULLNAME_VN = e.FULLNAME_VN,
                                        .EXP_DATE = p.EXP_DATE,
                                        .CREATED_DATE = p.CREATED_DATE,
                                        .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                        .REMARK = p.REMARK
                                    })

            If _filter.EMPLOYEE_ID <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_ID.ToUpper.Contains(_filter.EMPLOYEE_ID.ToUpper))
            End If
            If _filter.ALLOWANCE_TYPE_NAME <> "" Then
                lst = lst.Where(Function(p) p.ALLOWANCE_TYPE_NAME.ToUpper.Contains(_filter.ALLOWANCE_TYPE_NAME.ToUpper))
            End If
            If _filter.ALLOWANCE_TYPE <> 0 Then
                lst = lst.Where(Function(p) p.ALLOWANCE_TYPE = _filter.ALLOWANCE_TYPE)
            End If

            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.AMOUNT <> 0 Then
                lst = lst.Where(Function(p) p.AMOUNT = _filter.AMOUNT)
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

    Public Function InsertAllowance(ByVal objTitle As AllowanceDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New HU_ALLOWANCE
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.HU_ALLOWANCE.EntitySet.Name)
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.ALLOWANCE_TYPE = objTitle.ALLOWANCE_TYPE
            objTitleData.AMOUNT = objTitle.AMOUNT
            objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.EXP_DATE = objTitle.EXP_DATE
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.REMARK = objTitle.REMARK

            Context.HU_ALLOWANCE.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifyAllowance(ByVal objTitle As AllowanceDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New HU_ALLOWANCE With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.HU_ALLOWANCE Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.ALLOWANCE_TYPE = objTitle.ALLOWANCE_TYPE
            objTitleData.AMOUNT = objTitle.AMOUNT
            objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.EXP_DATE = objTitle.EXP_DATE
            objTitleData.ACTFLG = "A"
            objTitleData.REMARK = objTitle.REMARK
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveAllowance(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of HU_ALLOWANCE)
        Try
            lstData = (From p In Context.HU_ALLOWANCE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteAllowance(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstAllowanceData As List(Of HU_ALLOWANCE)
        Try
            lstAllowanceData = (From p In Context.HU_ALLOWANCE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstAllowanceData.Count - 1
                Context.HU_ALLOWANCE.DeleteObject(lstAllowanceData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTaxation")
            Throw ex
        End Try
    End Function


#End Region

#Region "Taxation "
    Public Function GetTaxation(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS ASC") As List(Of PATaxationDTO)
        Try
            Dim query = From p In Context.PA_TAXATION
                        From o In Context.OT_OTHER_LIST.Where(Function(e) e.ID = p.RESIDENT_ID).DefaultIfEmpty()
            Dim lst = query.Select(Function(s) New PATaxationDTO With {
                                        .ID = s.p.ID,
                                        .RESIDENT_ID = s.p.RESIDENT_ID,
                                        .RESIDENT_NAMEVN = s.o.NAME_VN,
                                        .RESIDENT_NAMEEN = s.o.NAME_EN,
                                        .VALUE_FROM = s.p.VALUE_FROM,
                                        .VALUE_TO = s.p.VALUE_TO,
                                        .RATE = s.p.RATE,
                                        .EXCEPT_FAST = s.p.EXCEPT_FAST,
                                        .FROM_DATE = s.p.FROM_DATE,
                                        .TO_DATE = s.p.TO_DATE,
                                        .SDESC = s.p.SDESC,
                                        .ACTFLG = If(s.p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                        .CREATED_DATE = s.p.CREATED_DATE,
                                        .ORDERS = s.p.ORDERS
                                    })
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertTaxation(ByVal objTitle As PATaxationDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_TAXATION
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_TAXATION.EntitySet.Name)
            objTitleData.RESIDENT_ID = objTitle.RESIDENT_ID
            objTitleData.VALUE_FROM = objTitle.VALUE_FROM
            objTitleData.VALUE_TO = objTitle.VALUE_TO
            objTitleData.RATE = objTitle.RATE
            objTitleData.EXCEPT_FAST = objTitle.EXCEPT_FAST
            objTitleData.FROM_DATE = objTitle.FROM_DATE
            objTitleData.TO_DATE = objTitle.TO_DATE
            objTitleData.ACTFLG = "A"
            objTitleData.SDESC = objTitle.SDESC
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            objTitleData.ORDERS = objTitle.ORDERS
            Context.PA_TAXATION.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifyTaxation(ByVal objTitle As PATaxationDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_TAXATION With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.PA_TAXATION Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.RESIDENT_ID = objTitle.RESIDENT_ID
            objTitleData.VALUE_FROM = objTitle.VALUE_FROM
            objTitleData.VALUE_TO = objTitle.VALUE_TO
            objTitleData.RATE = objTitle.RATE
            objTitleData.EXCEPT_FAST = objTitle.EXCEPT_FAST
            objTitleData.FROM_DATE = objTitle.FROM_DATE
            objTitleData.TO_DATE = objTitle.TO_DATE
            objTitleData.SDESC = objTitle.SDESC
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.ORDERS = objTitle.ORDERS
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveTaxation(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_TAXATION)
        Try
            lstData = (From p In Context.PA_TAXATION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteTaxation(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstTaxationData As List(Of PA_TAXATION)
        Try
            lstTaxationData = (From p In Context.PA_TAXATION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTaxationData.Count - 1
                Context.PA_TAXATION.DeleteObject(lstTaxationData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTaxation")
            Throw ex
        End Try
    End Function

#End Region

#Region "Payment list"
    Public Function GetPaymentList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO)
        Try
            Dim query = From p In Context.PA_PAYMENT_LIST
                        From o In Context.PA_OBJECT_SALARY.Where(Function(e) e.ID = p.OBJ_PAYMENT_ID).DefaultIfEmpty()
            Dim lst = query.Select(Function(s) New PAPaymentListDTO With {
                                        .ID = s.p.ID,
                                        .CODE = s.p.CODE,
                                        .NAME = s.p.NAME,
                                        .OBJ_PAYMENT_ID = s.p.OBJ_PAYMENT_ID,
                                        .OBJ_PAYMENT_NAME_VN = s.o.NAME_EN,
                                        .OBJ_PAYMENT_NAME_EN = s.o.NAME_VN,
                                        .EFFECTIVE_DATE = s.p.EFFECTIVE_DATE,
                                        .VALUE = s.p.VALUE,
                                        .SDESC = s.p.SDESC,
                                        .ACTFLG = If(s.p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                        .CREATED_DATE = s.p.CREATED_DATE,
                                        .ORDERS = s.p.ORDERS
                                     })
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GetPaymentListAll(Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO)
        Try

            Dim query = From p In Context.PA_PAYMENT_LIST
            Dim lst = query.Select(Function(p) New PAPaymentListDTO With {
                                        .ID = p.ID,
                                        .OBJ_PAYMENT_ID = p.OBJ_PAYMENT_ID,
                                        .CODE = p.CODE,
                                        .NAME = p.NAME,
                                        .EFFECTIVE_DATE = p.EFFECTIVE_DATE,
                                        .VALUE = p.VALUE,
                                        .SDESC = p.SDESC,
                                        .ACTFLG = p.ACTFLG,
                                        .ORDERS = p.ORDERS
                                     })
            lst = lst.OrderBy(Sorts)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function InsertPaymentList(ByVal objTitle As PAPaymentListDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_PAYMENT_LIST
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_PAYMENT_LIST.EntitySet.Name)
            objTitleData.OBJ_PAYMENT_ID = objTitle.OBJ_PAYMENT_ID
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME = objTitle.NAME
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE
            objTitleData.VALUE = objTitle.VALUE
            objTitleData.SDESC = objTitle.SDESC
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            objTitleData.ORDERS = objTitle.ORDERS
            Context.PA_PAYMENT_LIST.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifyPaymentList(ByVal objTitle As PAPaymentListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_PAYMENT_LIST With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.PA_PAYMENT_LIST Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.NAME = objTitle.NAME
            objTitleData.OBJ_PAYMENT_ID = objTitle.OBJ_PAYMENT_ID
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE
            objTitleData.VALUE = objTitle.VALUE
            objTitleData.SDESC = objTitle.SDESC
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            objTitleData.ORDERS = objTitle.ORDERS
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActivePaymentList(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_PAYMENT_LIST)
        Try
            lstData = (From p In Context.PA_PAYMENT_LIST Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeletePaymentList(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstPaymentListData As List(Of PA_PAYMENT_LIST)
        Try
            lstPaymentListData = (From p In Context.PA_PAYMENT_LIST Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstPaymentListData.Count - 1
                Context.PA_PAYMENT_LIST.DeleteObject(lstPaymentListData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

#End Region

#Region "Object Salary"
    Public Function GetObjectSalary(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAObjectSalaryDTO)
        Try

            Dim query = From p In Context.PA_OBJECT_SALARY
            Dim lst = query.Select(Function(p) New PAObjectSalaryDTO With {
                                        .ID = p.ID,
                                        .CODE = p.CODE,
                                        .NAME_VN = p.NAME_VN,
                                        .NAME_EN = p.NAME_EN,
                                        .EFFECTIVE_DATE = p.EFFECTIVE_DATE,
                                        .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                        .SDESC = p.SDESC,
                                        .CREATED_BY = p.CREATED_BY,
                                        .CREATED_DATE = p.CREATED_DATE,
                                        .CREATED_LOG = p.CREATED_LOG,
                                        .MODIFIED_BY = p.MODIFIED_BY,
                                        .MODIFIED_DATE = p.MODIFIED_DATE,
            .MODIFIED_LOG = p.MODIFIED_LOG
                                        })
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GetObjectSalaryColumn(ByVal gID As Decimal) As DataTable
        Try
            Dim query = From p In Context.PA_OBJECT_SALARY
                        Where p.ACTFLG = "A" Order By p.NAME_VN Ascending
            Dim obj = query.Select(Function(f) New PAObjectSalaryDTO With
                        {.ID = f.ID,
                         .NAME_VN = f.NAME_VN,
                         .NAME_EN = f.NAME_EN
                        }).ToList
            Return obj.ToTable()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GetObjectSalaryAll(Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAObjectSalaryDTO)
        Try

            Dim query = From p In Context.PA_OBJECT_SALARY
            Dim lst = query.Select(Function(p) New PAObjectSalaryDTO With {
                                        .ID = p.ID,
                                        .CODE = p.CODE,
                                        .NAME_VN = p.NAME_VN,
                                        .NAME_EN = p.NAME_EN,
                                        .EFFECTIVE_DATE = p.EFFECTIVE_DATE,
                                        .SDESC = p.SDESC,
                                        .CREATED_BY = p.CREATED_BY,
                                        .CREATED_DATE = p.CREATED_DATE,
                                        .CREATED_LOG = p.CREATED_LOG,
                                        .MODIFIED_BY = p.MODIFIED_BY,
                                        .MODIFIED_DATE = p.MODIFIED_DATE,
                                        .MODIFIED_LOG = p.MODIFIED_LOG
                                        })
            lst = lst.OrderBy(Sorts)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function InsertObjectSalary(ByVal objTitle As PAObjectSalaryDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_OBJECT_SALARY
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_OBJECT_SALARY.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE
            objTitleData.SDESC = objTitle.SDESC
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.PA_OBJECT_SALARY.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateObjectSalary(ByVal _validate As PAObjectSalaryDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_OBJECT_SALARY
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PA_OBJECT_SALARY
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifyObjectSalary(ByVal objTitle As PAObjectSalaryDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_OBJECT_SALARY With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.PA_OBJECT_SALARY Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE
            objTitleData.SDESC = objTitle.SDESC
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveObjectSalary(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_OBJECT_SALARY)
        Try
            lstData = (From p In Context.PA_OBJECT_SALARY Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteObjectSalary(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstObjectSalaryData As List(Of PA_OBJECT_SALARY)
        Try
            lstObjectSalaryData = (From p In Context.PA_OBJECT_SALARY Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstObjectSalaryData.Count - 1
                Context.PA_OBJECT_SALARY.DeleteObject(lstObjectSalaryData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

#End Region

#Region "Period list"

    Public Function GetPeriodList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "START_DATE desc") As List(Of ATPeriodDTO)

        Try
            Dim query = From p In Context.AT_PERIOD

            Dim lst = query.Select(Function(p) New ATPeriodDTO With {
                                       .ID = p.ID,
                                       .YEAR = p.YEAR,
                                       .MONTH = p.MONTH,
                                       .PERIOD_NAME = p.PERIOD_NAME,
                                       .PERIOD_STANDARD = p.PERIOD_STANDARD,
                                       .PERIOD_STANDARD1 = p.PERIOD_STANDARD,
                                       .START_DATE = p.START_DATE,
                                       .END_DATE = p.END_DATE,
                                       .BONUS_DATE = p.BONUS_DATE,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY,
                                       .REMARK = p.REMARK,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")
                                       })


            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function

    Public Function GetPeriodbyYear(ByVal year As Decimal) As List(Of ATPeriodDTO)
        Try
            Dim query = From p In Context.AT_PERIOD Where p.YEAR = year Order By p.START_DATE Ascending
            Dim Period = query.Select(Function(p) New ATPeriodDTO With {
                                       .ID = p.ID,
                                       .YEAR = p.YEAR,
                                       .MONTH = p.MONTH,
                                       .PERIOD_NAME = p.PERIOD_NAME,
                                       .START_DATE = p.START_DATE,
                                       .END_DATE = p.END_DATE,
                                       .BONUS_DATE = p.BONUS_DATE,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY,
                                       .REMARK = p.REMARK,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")})


            Return Period.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertPeriod(ByVal objPeriod As ATPeriodDTO, ByVal objOrgPeriod As List(Of AT_ORG_PERIOD), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objPeriodData As New AT_PERIOD

        Try
            objPeriodData.ID = Utilities.GetNextSequence(Context, Context.AT_PERIOD.EntitySet.Name)
            objPeriodData.YEAR = objPeriod.YEAR
            objPeriodData.MONTH = objPeriod.MONTH
            objPeriodData.PERIOD_NAME = objPeriod.PERIOD_NAME
            objPeriodData.PERIOD_STANDARD = objPeriod.PERIOD_STANDARD
            objPeriodData.START_DATE = objPeriod.START_DATE
            objPeriodData.END_DATE = objPeriod.END_DATE
            objPeriodData.BONUS_DATE = objPeriod.BONUS_DATE
            objPeriodData.REMARK = objPeriod.REMARK
            objPeriodData.ACTFLG = objPeriod.ACTFLG
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_PA_SETTING.ADD_ORG_PERIOD",
                                           New With {.P_PERIOD_ID = objPeriodData.ID,
                                                     .P_STATUSCOLEX = 1,
                                                     .P_STATUSPAROX = 1,
                                                     .P_CREATED_BY = log.Username,
                                                     .P_CREATED_DATE = Date.Now,
                                                     .P_CREATED_LOG = log.Ip & "-" & log.ComputerName
                                               })
            End Using
            Context.AT_PERIOD.AddObject(objPeriodData)
            Context.SaveChanges(log)
            'If objPeriodData.ID > 0 Then
            '    For Each obj As AT_ORG_PERIOD In objOrgPeriod
            '        objOrgPeriodData = New AT_ORG_PERIOD
            '        objOrgPeriodData.ID = Utilities.GetNextSequence(Context, Context.AT_ORG_PERIOD.EntitySet.Name)
            '        objOrgPeriodData.ORG_ID = obj.ORG_ID
            '        objOrgPeriodData.PERIOD_ID = obj.ID
            '        objOrgPeriodData.STATUSCOLEX = 1
            '        objOrgPeriodData.STATUSPAROX = 1
            '        Context.AT_ORG_PERIOD.AddObject(objOrgPeriodData)
            '        Context.SaveChanges(log)
            '    Next
            'End If
            gID = objPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateATPeriod(ByVal _validate As ATPeriodDTO) As Boolean
        Try
            If _validate.ID <> 0 Then

                Dim query = (From p In Context.AT_ORG_PERIOD Where p.PERIOD_ID = _validate.ID).ToList
                If query.Count > 0 Then
                    Return False
                Else
                    Return True
                End If

            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ValidateATPeriodDay(ByVal _validate As ATPeriodDTO)
        Dim query
        Try
            If _validate.START_DATE IsNot Nothing And _validate.END_DATE IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.AT_PERIOD
                             Where (_validate.START_DATE <= p.END_DATE And _validate.END_DATE >= p.START_DATE) _
                             And p.YEAR = _validate.YEAR _
                             And p.ID <> _validate.ID And p.ID = 1).FirstOrDefault
                Else
                    query = (From p In Context.AT_PERIOD
                             Where (_validate.START_DATE <= p.END_DATE And _validate.END_DATE >= p.START_DATE) _
                             And p.YEAR = _validate.YEAR).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifyPeriod(ByVal objPeriod As ATPeriodDTO, ByVal objOrgPeriod As List(Of AT_ORG_PERIOD), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPeriodData As New AT_PERIOD With {.ID = objPeriod.ID}
        'Dim objOrgPeriodData As AT_ORG_PERIOD
        Try
            Context.AT_PERIOD.Attach(objPeriodData)
            objPeriodData.YEAR = objPeriod.YEAR
            objPeriodData.MONTH = objPeriod.MONTH
            objPeriodData.PERIOD_STANDARD = objPeriod.PERIOD_STANDARD
            objPeriodData.PERIOD_NAME = objPeriod.PERIOD_NAME
            objPeriodData.START_DATE = objPeriod.START_DATE
            objPeriodData.END_DATE = objPeriod.END_DATE
            objPeriodData.BONUS_DATE = objPeriod.BONUS_DATE
            objPeriodData.REMARK = objPeriod.REMARK
            'objPeriodData.ACTFLG = objPeriod.ACTFLG
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_PA_SETTING.ADD_ORG_PERIOD",
                                           New With {.P_PERIOD_ID = objPeriodData.ID,
                                                     .P_STATUSCOLEX = 1,
                                                     .P_STATUSPAROX = 1,
                                                     .P_CREATED_BY = log.Username,
                                                     .P_CREATED_DATE = Date.Now,
                                                     .P_CREATED_LOG = log.Ip & "-" & log.ComputerName
                                               })
            End Using
            'If objPeriodData.ID > 0 Then
            '    Dim objDelete As List(Of AT_ORG_PERIOD) = (From p In Context.AT_ORG_PERIOD Where p.PERIOD_ID = objPeriodData.ID).ToList
            '    For Each obj As AT_ORG_PERIOD In objDelete
            '        Context.AT_ORG_PERIOD.DeleteObject(obj)
            '    Next
            '    Context.SaveChanges(log)
            '    Dim i = 1
            '    For Each ObjIns As AT_ORG_PERIOD In objOrgPeriod
            '        objOrgPeriodData = New AT_ORG_PERIOD
            '        objOrgPeriodData.ID = Utilities.GetNextSequence(Context, Context.AT_ORG_PERIOD.EntitySet.Name)
            '        objOrgPeriodData.ORG_ID = ObjIns.ORG_ID
            '        objOrgPeriodData.STATUSCOLEX = 1
            '        objOrgPeriodData.STATUSPAROX = 1
            '        objOrgPeriodData.PERIOD_ID = objPeriodData.ID
            '        Context.AT_ORG_PERIOD.AddObject(objOrgPeriodData)
            '        If i = objOrgPeriod.Count OrElse i Mod 40 = 0 Then
            '            Context.SaveChanges(log)
            '        End If
            '        i += 1
            '    Next
            'End If
            Context.SaveChanges(log)
            gID = objPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DeletePeriod(ByVal lstPeriod As ATPeriodDTO) As Boolean
        Dim objOrgPeriod As List(Of AT_ORG_PERIOD) = (From p In Context.AT_ORG_PERIOD Where p.PERIOD_ID = lstPeriod.ID).ToList
        Dim objPeriod As List(Of AT_PERIOD) = (From p In Context.AT_PERIOD Where p.ID = lstPeriod.ID).ToList
        Try
            For Each item In objOrgPeriod
                Context.AT_ORG_PERIOD.DeleteObject(item)
            Next
            For Each item In objPeriod
                Context.AT_PERIOD.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function ActivePeriod(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_PERIOD)
        Try
            lstData = (From p In Context.AT_PERIOD Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region

#Region "WORK STANDARD"
    Public Function IsCompanyLevel(ByVal org_id As Decimal) As Boolean
        Try
            Dim orgId2 As Decimal? = Context.HUV_ORGANIZATION.Where(Function(f) f.ID = org_id).Select(Function(f) f.ORG_ID2).FirstOrDefault()
            Return org_id = orgId2
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetWorkStandard(ByVal _filter As Work_StandardDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = " YEAR, PERIOD_ID desc") As List(Of Work_StandardDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _filter.param.ORG_ID,
                                           .P_ISDISSOLVE = _filter.param.IS_DISSOLVE})
            End Using

            Dim lst = (From p In Context.PA_WORK_STANDARD
            From OT In Context.OT_OTHER_LIST.Where(Function(OT) OT.ID = p.OBJECT_ID).DefaultIfEmpty()
            From OTL In Context.OT_OTHER_LIST_TYPE.Where(Function(OTL) OTL.ID = OT.TYPE_ID And OTL.CODE = "OBJECT_LABOR").DefaultIfEmpty()
            From AT In Context.AT_PERIOD.Where(Function(AT) p.PERIOD_ID = AT.ID).DefaultIfEmpty()
            From ORG In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty()
            From s In Context.SE_CHOSEN_ORG.Where(Function(se) se.ORG_ID = ORG.ID And
                                                                    se.USERNAME = log.Username.ToUpper)
            Where p.ORG_ID = _filter.param.ORG_ID
            Select New Work_StandardDTO With {
                                       .ID = p.ID,
                                       .YEAR = p.YEAR,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_NAME = ORG.NAME_VN,
                                       .PERIOD_ID = p.PERIOD_ID,
                                       .PERIOD_NAME = AT.PERIOD_NAME,
                                       .OBJECT_ID = p.OBJECT_ID,
                                       .OBJECT_NAME = OT.NAME_VN,
                                       .Period_standard = p.PERIOD_STANDARD,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .REMARK = p.REMARK,
            .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")
                                       })
            If _filter.YEAR IsNot Nothing Then
                lst = lst.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If _filter.PERIOD_NAME <> "" Then
                lst = lst.Where(Function(f) f.PERIOD_NAME.ToUpper.Contains(_filter.PERIOD_NAME.ToUpper))
            End If
            If _filter.OBJECT_NAME <> "" Then
                lst = lst.Where(Function(f) f.OBJECT_NAME.ToUpper.Contains(_filter.OBJECT_NAME.ToUpper))
            End If
            If _filter.ORG_ID <> 0 Then
                lst = lst.Where(Function(p) p.ORG_ID = _filter.ORG_ID)
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
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

    Public Function GetWorkStandardbyYear(ByVal year As Decimal) As List(Of Work_StandardDTO)
        Try
            Dim lst = (From p In Context.PA_WORK_STANDARD
            From OT In Context.OT_OTHER_LIST.Where(Function(OT) OT.ID = p.OBJECT_ID And OT.TYPE_ID = 2071).DefaultIfEmpty()
            From AT In Context.AT_PERIOD.Where(Function(AT) p.PERIOD_ID = AT.ID).DefaultIfEmpty()
            From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty() Where p.YEAR = year
            Select New Work_StandardDTO With {
                                       .ID = p.ID,
                                       .YEAR = p.YEAR,
                                       .ORG_ID = p.ORG_ID,
                                       .ORG_NAME = org.NAME_VN,
                                       .PERIOD_ID = p.PERIOD_ID,
                                       .PERIOD_NAME = AT.PERIOD_NAME,
                                       .OBJECT_ID = p.OBJECT_ID,
                                       .OBJECT_NAME = OT.NAME_VN,
                                       .Period_standard = p.PERIOD_STANDARD,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .REMARK = p.REMARK,
                                        .ACTFLG = p.ACTFLG
                                       })
            'Dim query = From p In Context.PA_WORK_STANDARD Where p.YEAR = year Order By p.ID Ascending
            'Dim Period = query.Select(Function(p) New Work_StandardDTO With {
            '                           .ID = p.ID,
            '                           .YEAR = p.YEAR,
            '                           .PERIOD_ID = p.PERIOD_ID,
            '                           .OBJECT_ID = p.OBJECT_ID,
            '                           .Period_standard = p.PERIOD_STANDARD,
            '                           .CREATED_DATE = p.CREATED_DATE,
            '                           .REMARK = p.REMARK,
            '                           .ACTFLG = p.ACTFLG})


            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertWorkStandard(ByVal objPeriod As Work_StandardDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objPeriodData As New PA_WORK_STANDARD
        Try
            objPeriodData.ID = Utilities.GetNextSequence(Context, Context.PA_WORK_STANDARD.EntitySet.Name)
            objPeriodData.YEAR = objPeriod.YEAR
            objPeriodData.PERIOD_ID = objPeriod.PERIOD_ID
            objPeriodData.OBJECT_ID = objPeriod.OBJECT_ID
            objPeriodData.ORG_ID = objPeriod.ORG_ID
            objPeriodData.PERIOD_STANDARD = objPeriod.Period_standard
            objPeriodData.REMARK = objPeriod.REMARK
            objPeriodData.ACTFLG = objPeriod.ACTFLG
            Context.PA_WORK_STANDARD.AddObject(objPeriodData)
            Context.SaveChanges(log)

            gID = objPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateWorkStandard(ByVal _validate As Work_StandardDTO) As Boolean
        Try
            Dim query = (From p In Context.PA_WORK_STANDARD Where p.ORG_ID = _validate.ORG_ID And p.PERIOD_ID = _validate.PERIOD_ID And p.OBJECT_ID = _validate.OBJECT_ID And p.ID <> _validate.ID).ToList
            If query.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifyWORKSTANDARD(ByVal objPeriod As Work_StandardDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPeriodData As New PA_WORK_STANDARD With {.ID = objPeriod.ID}
        Try
            Context.PA_WORK_STANDARD.Attach(objPeriodData)
            objPeriodData.YEAR = objPeriod.YEAR
            objPeriodData.ORG_ID = objPeriod.ORG_ID
            objPeriodData.PERIOD_ID = objPeriod.PERIOD_ID
            objPeriodData.OBJECT_ID = objPeriod.OBJECT_ID
            objPeriodData.PERIOD_STANDARD = objPeriod.Period_standard
            objPeriodData.REMARK = objPeriod.REMARK
            Context.SaveChanges(log)
            gID = objPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DeleteWorkStandard(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstWorkFactorData As List(Of PA_WORK_STANDARD)
        Try
            lstWorkFactorData = (From p In Context.PA_WORK_STANDARD Where lstID.Contains(p.ID)).ToList

            For index = 0 To lstWorkFactorData.Count - 1
                Context.PA_WORK_STANDARD.DeleteObject(lstWorkFactorData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

    Public Function ActiveWorkStandard(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_WORK_STANDARD)
        Try
            lstData = (From p In Context.PA_WORK_STANDARD Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region

#Region "List Salary Fomuler"
    Public Function GetAllFomulerGroup(ByVal _filter As PAFomulerGroup, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "IDX ASC") As List(Of PAFomulerGroup)

        Try
            Dim lst = (From q In Context.PA_FORMULER_GROUP
                         From s In Context.PA_OBJECT_SALARY.Where(Function(s) s.ID = q.OBJ_SAL_ID)
           Select New PAFomulerGroup With {.ID = q.ID,
                                               .NAME_VN = q.NAME_VN,
                                               .NAME_EN = q.NAME_EN,
                                               .OBJ_SAL_ID = q.OBJ_SAL_ID,
                                               .OBJ_SAL_NAME = s.NAME_VN,
                                               .START_DATE = q.START_DATE,
                                               .END_DATE = q.END_DATE,
                                               .SDESC = q.SDESC,
                                               .STATUS = q.STATUS,
                                               .IDX = q.IDX
                                               })
            If _filter.OBJ_SAL_NAME <> "" Then
                lst = lst.Where(Function(p) p.OBJ_SAL_NAME.ToUpper.Contains(_filter.OBJ_SAL_NAME.ToUpper))
            End If
            If _filter.NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
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

    Public Function InsertFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_FORMULER_GROUP
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.AT_PERIOD.EntitySet.Name)
            objData.TYPE_PAYMENT = objPeriod.TYPE_PAYMENT
            objData.OBJ_SAL_ID = objPeriod.OBJ_SAL_ID
            objData.NAME_VN = objPeriod.NAME_VN
            objData.NAME_EN = objPeriod.NAME_EN
            objData.START_DATE = objPeriod.START_DATE
            objData.END_DATE = objPeriod.END_DATE
            objData.STATUS = objPeriod.STATUS
            objData.SDESC = objPeriod.SDESC
            objData.IDX = objPeriod.IDX
            Context.PA_FORMULER_GROUP.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function ModifyFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_FORMULER_GROUP With {.ID = objPeriod.ID}
        Try
            Context.PA_FORMULER_GROUP.Attach(objData)
            objData.OBJ_SAL_ID = objPeriod.OBJ_SAL_ID
            objData.NAME_VN = objPeriod.NAME_VN
            objData.NAME_EN = objPeriod.NAME_EN
            objData.START_DATE = objPeriod.START_DATE
            objData.END_DATE = objPeriod.END_DATE
            objData.STATUS = objPeriod.STATUS
            objData.SDESC = objPeriod.SDESC
            objData.IDX = objPeriod.IDX
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteFomulerGroup(ByVal lstDelete As PAFomulerGroup) As Boolean
        Dim objData As List(Of PA_FORMULER_GROUP) = (From p In Context.PA_FORMULER_GROUP Where p.ID = lstDelete.ID).ToList
        Try
            For Each item In objData
                Context.PA_FORMULER_GROUP.DeleteObject(item)
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetListAllSalary(ByVal gID As Decimal) As List(Of PAFomuler)
        Try
            Dim query = From p In Context.PA_LISTSALARIES
                        From g In Context.PA_FORMULER_GROUP.Where(Function(g) g.OBJ_SAL_ID = p.OBJ_SAL_ID)
                         From f In Context.PA_FORMULER.Where(Function(f) f.GROUP_FML = g.ID And f.COL_NAME = p.COL_NAME).DefaultIfEmpty
                         Where g.ID = gID And p.IS_DELETED = 0 And p.IS_SUMARISING = -1 And p.IS_IMPORT = 0 Order By f.INDEX_FML Ascending, f.COL_NAME Ascending
            Dim obj = query.Select(Function(o) New PAFomuler With
                        {.ID = o.p.ID,
                         .COL_NAME = o.p.COL_NAME,
                         .NAME_VN = o.p.NAME_VN,
                         .NAME_EN = o.p.NAME_EN,
                         .COL_INDEX = o.f.INDEX_FML,
                         .FORMULER = o.f.FORMULER
                        }).ToList
            Return obj
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GetListInputColumn(ByVal gID As Decimal) As DataTable
        Try
            Dim query = From p In Context.PA_LISTSALARIES
                        From g In Context.PA_FORMULER_GROUP.Where(Function(g) g.OBJ_SAL_ID = p.OBJ_SAL_ID)
                        Where p.STATUS = "A" And g.ID = gID Order By p.NAME_VN Ascending, p.COL_INDEX Ascending
            Dim obj = query.Select(Function(f) New PAListSalariesDTO With
                        {.ID = f.p.ID,
                         .COL_INDEX = f.p.COL_INDEX,
                         .COL_NAME = f.p.COL_NAME,
                         .NAME_VN = f.p.NAME_VN & " - (" & f.p.COL_NAME & ")",
                         .NAME_EN = f.p.NAME_EN
                        }).ToList
            Return obj.ToTable()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GetListSalColunm(ByVal gID As Decimal) As DataTable
        Try
            Dim query = From p In Context.PA_LISTSAL
                        Where p.STATUS = "A" And p.GROUP_TYPE = gID Order By p.NAME_VN, p.COL_INDEX Ascending
            Dim obj = query.Select(Function(f) New PAListSalDTO With
                        {.ID = f.ID,
                         .COL_INDEX = f.COL_INDEX,
                         .COL_NAME = f.COL_NAME,
                         .NAME_VN = f.NAME_VN,
                         .NAME_EN = f.NAME_EN
                        }).ToList
            Return obj.ToTable()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GetListCalculation() As List(Of OT_OTHERLIST_DTO)
        Try
            Dim query = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                             Where p.ACTFLG = "A" And t.CODE = "CALCULATION" Order By p.CREATED_DATE Descending
                         Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                .TYPE_ID = p.TYPE_ID
                         }).ToList
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function CopyFomuler(ByRef F_ID As Decimal,
                                    ByVal log As UserLog, ByRef T_ID As Decimal) As Boolean


        Try
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_PA_SETTING.COPY_FORMULER_SALARY",
                                           New With {.OBJ_SAL_FROM = F_ID,
                                                     .OBJ_SAL_TO = T_ID})
            End Using

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function SaveFomuler(ByVal objData As PAFomuler, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objInsert As PA_FORMULER
        Dim iCount As Integer = 0
        Try
            objInsert = (From p In Context.PA_FORMULER Where p.COL_NAME = objData.COL_NAME And p.GROUP_FML = objData.GROUP_FML).SingleOrDefault
            If objInsert Is Nothing Then
                objInsert = New PA_FORMULER
                objInsert.ID = Utilities.GetNextSequence(Context, Context.PA_FORMULER.EntitySet.Name)
                objInsert.COL_NAME = objData.COL_NAME
                objInsert.INDEX_FML = objData.INDEX_FML
                objInsert.GROUP_FML = objData.GROUP_FML
                objInsert.FORMULER = objData.FORMULER
                objInsert.CREATED_BY = objData.CREATED_BY
                objInsert.CREATED_DATE = objData.CREATED_DATE
                objInsert.CREATED_LOG = objData.CREATED_LOG
                objInsert.MODIFIED_BY = objData.MODIFIED_BY
                objInsert.MODIFIED_DATE = objData.MODIFIED_DATE
                objInsert.MODIFIED_LOG = objData.MODIFIED_LOG
                Context.PA_FORMULER.AddObject(objInsert)
            Else
                objInsert.COL_NAME = objData.COL_NAME
                objInsert.INDEX_FML = objData.INDEX_FML
                objInsert.GROUP_FML = objData.GROUP_FML
                objInsert.FORMULER = objData.FORMULER
                objInsert.MODIFIED_BY = objData.MODIFIED_BY
                objInsert.MODIFIED_DATE = objData.MODIFIED_DATE
                objInsert.MODIFIED_LOG = objData.MODIFIED_LOG
            End If
            Context.SaveChanges(log)
            gID = objInsert.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function CheckFomuler(ByVal sCol As String, ByVal sFormuler As String, ByVal objID As Decimal) As Boolean
        Try
            Using cls As New DataAccess.NonQueryData
                Dim sql As String = ""
                Dim sql1 As String = ""
                Dim sql2 As String = ""
                sql = "UPDATE TEMP_CALCULATE T SET T." & sCol & " = NVL(" & sFormuler & ",0)"
                sql1 = "UPDATE TEMP_CALCULATE_SUM T SET T." & sCol & " = NVL(" & sFormuler & ",0)"
                sql2 = "UPDATE PA_INCOME_TAX_SUM T SET T." & sCol & " = NVL(" & sFormuler & ",0)"
                sql &= " WHERE 1=0 "
                sql1 &= " WHERE 1=0 "
                sql2 &= " WHERE 1=0 "
                If objID = 11 Then
                    cls.ExecuteSQL(sql2)

                Else
                    cls.ExecuteSQL(sql)
                    cls.ExecuteSQL(sql1)
                End If

            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ActiveFolmulerGroup(ByVal lstID As Decimal, ByVal log As UserLog, ByVal bActive As Decimal) As Boolean
        Dim lstData As PA_FORMULER_GROUP
        Try
            lstData = (From p In Context.PA_FORMULER_GROUP Where p.ID = lstID).SingleOrDefault
            lstData.STATUS = bActive
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region

#Region "Salaries List"
    Public Function GetListSalaries(ByVal _filter As PAListSalariesDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "COL_INDEX ASC") As List(Of PAListSalariesDTO)
        Try
            Dim lst = (From p In Context.PA_LISTSALARIES
                        From o In Context.OT_OTHER_LIST.Where(Function(o) o.ID = p.TYPE_PAYMENT).DefaultIfEmpty()
                        From ot In Context.OT_OTHER_LIST_TYPE.Where(Function(ot) ot.ID = o.TYPE_ID).DefaultIfEmpty()
                        From import In Context.OT_OTHER_LIST.Where(Function(e) e.ID = p.IMPORT_TYPE_ID).DefaultIfEmpty()
                        From ot_import In Context.OT_OTHER_LIST_TYPE.Where(Function(e) e.ID = import.TYPE_ID).DefaultIfEmpty()
                        From sal_type In Context.OT_OTHER_LIST.Where(Function(sty) sty.ID = p.GROUP_TYPE).DefaultIfEmpty()
                        From sal_obj In Context.PA_OBJECT_SALARY.Where(Function(sal) sal.ID = p.OBJ_SAL_ID).DefaultIfEmpty()
                        Where p.IS_DELETED = _filter.IS_DELETED
           Select New PAListSalariesDTO With {
                                        .ID = p.ID,
                                        .TYPE_PAYMENT = p.TYPE_PAYMENT,
                                        .COL_NAME = p.COL_NAME,
                                        .NAME_VN = p.NAME_VN,
                                        .NAME_EN = p.NAME_EN,
                                        .DATA_TYPE = p.DATA_TYPE,
                                        .COL_INDEX = p.COL_INDEX,
                                        .STATUS = If(p.STATUS = "A", "Áp dụng", "Ngừng áp dụng"),
                                        .IS_VISIBLE = p.IS_VISIBLE,
                                        .IS_INPUT = p.IS_INPUT,
                                        .IS_CALCULATE = p.IS_CALCULATE,
                                        .IS_IMPORT = p.IS_IMPORT,
                                        .INPUT_FORMULER = p.INPUT_FORMULER,
                                        .CREATED_DATE = p.CREATED_DATE,
                                        .IMPORT_TYPE_ID = p.IMPORT_TYPE_ID,
                                        .IS_WORKDAY = p.IS_WORKDAY,
                                        .IS_SUMDAY = p.IS_SUMDAY,
                                        .IS_WORKARISING = p.IS_WORKARISING,
                                        .IS_SUMARISING = p.IS_SUMARISING,
                                        .IS_PAYBACK = p.IS_PAYBACK,
                                        .IS_DELETED = p.IS_DELETED,
                                        .EFFECTIVE_DATE = p.EFFECTIVE_DATE,
                                        .EXPIRE_DATE = p.EXPIRE_DATE,
                                        .REMARK = p.REMARK,
                                        .IMPORT_TYPE_NAME = import.NAME_VN,
                                        .OBJ_SAL_ID = p.OBJ_SAL_ID,
                                        .OBJ_SAL_NAME = sal_obj.NAME_VN,
                                        .GROUP_TYPE_ID = p.GROUP_TYPE,
                                        .GROUP_TYPE_NAME = sal_type.NAME_VN
                                    })

            If _filter.COL_NAME <> "" Then
                lst = lst.Where(Function(p) p.COL_NAME.ToUpper.Contains(_filter.COL_NAME.ToUpper))
            End If
            If _filter.NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If

            If _filter.DATA_TYPE <> 0 Then
                lst = lst.Where(Function(p) p.DATA_TYPE = _filter.DATA_TYPE)
            End If
            If _filter.OBJ_SAL_ID <> 0 Then
                lst = lst.Where(Function(p) p.OBJ_SAL_ID = _filter.OBJ_SAL_ID)
            End If
            If _filter.GROUP_TYPE_ID <> 0 Then
                lst = lst.Where(Function(p) p.GROUP_TYPE_ID = _filter.GROUP_TYPE_ID)
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.OBJ_SAL_NAME <> "" Then
                lst = lst.Where(Function(p) p.OBJ_SAL_NAME.ToUpper.Contains(_filter.OBJ_SAL_NAME.ToUpper))
            End If
            If _filter.GROUP_TYPE_NAME <> "" Then
                lst = lst.Where(Function(p) p.GROUP_TYPE_NAME.ToUpper.Contains(_filter.GROUP_TYPE_NAME.ToUpper))
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
    Public Function InsertListSalaries(ByVal objTitle As PAListSalariesDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_LISTSALARIES
        Dim iCount As Integer = 0

        Try

            objTitleData.TYPE_PAYMENT = objTitle.TYPE_PAYMENT
            objTitleData.COL_NAME = objTitle.COL_NAME
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.DATA_TYPE = objTitle.DATA_TYPE
            objTitleData.COL_INDEX = objTitle.COL_INDEX
            objTitleData.STATUS = objTitle.STATUS
            objTitleData.IS_VISIBLE = objTitle.IS_VISIBLE
            objTitleData.IS_INPUT = objTitle.IS_INPUT
            objTitleData.IS_CALCULATE = objTitle.IS_CALCULATE
            objTitleData.IS_IMPORT = objTitle.IS_IMPORT
            objTitleData.INPUT_FORMULER = objTitle.INPUT_FORMULER
            objTitleData.IMPORT_TYPE_ID = objTitle.IMPORT_TYPE_ID
            objTitleData.COL_CODE = objTitle.COL_CODE
            objTitleData.IS_WORKDAY = objTitle.IS_WORKDAY
            objTitleData.IS_SUMDAY = objTitle.IS_SUMDAY
            objTitleData.IS_WORKARISING = objTitle.IS_WORKARISING
            objTitleData.IS_SUMARISING = objTitle.IS_SUMARISING
            objTitleData.IS_PAYBACK = objTitle.IS_PAYBACK
            objTitleData.REMARK = objTitle.REMARK
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE
            objTitleData.EXPIRE_DATE = objTitle.EXPIRE_DATE
            objTitleData.IS_DELETED = objTitle.IS_DELETED
            objTitleData.GROUP_TYPE = objTitle.GROUP_TYPE_ID
            objTitleData.OBJ_SAL_ID = objTitle.OBJ_SAL_ID
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_LISTSALARIES.EntitySet.Name)
            Context.PA_LISTSALARIES.AddObject(objTitleData)
            If objTitle.IS_IMPORT = -1 Then
                Using cls As New DataAccess.NonQueryData
                    cls.ExecuteStore("PKG_PA_SETTING.ADD_COL_SALARY_IMPORT_NEW",
                                               New With {.COL_NAME = objTitle.COL_NAME,
                                                         .COL_TYPE = objTitle.DATA_TYPE})
                End Using
            End If
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function InsertPA_SAL_MAPPING(ByVal objTitle As PA_SALARY_FUND_MAPPINGDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        Try

            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_PA_SETTING.INSERT_PA_SAL_MAPPING",
                                           New With {.P_ID = objTitle.ID,
                                                     .P_YEAR = objTitle.YEAR,
                                                     .P_PERIOD_ID = objTitle.PERIOD_ID,
                                                     .P_SALARY_GROUP = objTitle.SALARY_GROUP,
                                                     .P_SALARY_FUND = objTitle.SALARY_FUND,
                                                     .P_SALARY_NAME = objTitle.SALARY_NAME,
                                                     .P_CREATED_BY = log.Username,
                                                     .P_CREATED_DATE = Date.Now,
                                                     .P_CREATED_LOG = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function ModifyListSalaries(ByVal objTitle As PAListSalariesDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_LISTSALARIES With {.ID = objTitle.ID}
        Dim old_Name As String
        Try
            objTitleData = (From p In Context.PA_LISTSALARIES Where p.ID = objTitleData.ID).SingleOrDefault
            old_Name = objTitleData.COL_NAME
            objTitleData.TYPE_PAYMENT = objTitle.TYPE_PAYMENT
            objTitleData.COL_NAME = objTitle.COL_NAME
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.DATA_TYPE = objTitle.DATA_TYPE
            objTitleData.COL_INDEX = objTitle.COL_INDEX
            objTitleData.IS_VISIBLE = objTitle.IS_VISIBLE
            objTitleData.IS_INPUT = objTitle.IS_INPUT
            objTitleData.IS_CALCULATE = objTitle.IS_CALCULATE
            objTitleData.IS_IMPORT = objTitle.IS_IMPORT
            objTitleData.INPUT_FORMULER = objTitle.INPUT_FORMULER
            objTitleData.IMPORT_TYPE_ID = objTitle.IMPORT_TYPE_ID
            objTitleData.COL_CODE = objTitle.COL_CODE
            objTitleData.IS_WORKDAY = objTitle.IS_WORKDAY
            objTitleData.IS_SUMDAY = objTitle.IS_SUMDAY
            objTitleData.IS_WORKARISING = objTitle.IS_WORKARISING
            objTitleData.IS_SUMARISING = objTitle.IS_SUMARISING
            objTitleData.IS_PAYBACK = objTitle.IS_PAYBACK
            objTitleData.REMARK = objTitle.REMARK
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE
            objTitleData.EXPIRE_DATE = objTitle.EXPIRE_DATE
            objTitleData.GROUP_TYPE = objTitle.GROUP_TYPE_ID
            objTitleData.OBJ_SAL_ID = objTitle.OBJ_SAL_ID
            If objTitle.IS_IMPORT = -1 Then
                Using cls As New DataAccess.NonQueryData
                    cls.ExecuteStore("PKG_PA_SETTING.ADD_COL_SALARY_IMPORT_NEW",
                                               New With {.COL_NAME = objTitle.COL_NAME,
                                                         .COL_TYPE = objTitle.DATA_TYPE})
                End Using
            End If
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveListSalaries(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_LISTSALARIES)
        Try
            lstData = (From p In Context.PA_LISTSALARIES Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).STATUS = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Update status deleted, khong xoa khoi he thong
    ''' </summary>
    ''' <param name="lstID"></param>
    ''' <param name="log"></param>
    ''' <param name="bActive"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteListSalariesStatus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean
        Dim lstData As List(Of PA_LISTSALARIES)
        Try
            lstData = (From p In Context.PA_LISTSALARIES Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).IS_DELETED = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteListSalaries(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstListSalariesData As List(Of PA_LISTSALARIES)
        Try
            lstListSalariesData = (From p In Context.PA_LISTSALARIES Where lstID.Contains(p.ID)).ToList

            For index = 0 To lstListSalariesData.Count - 1
                Context.PA_LISTSALARIES.DeleteObject(lstListSalariesData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function
    Public Function GetListSal(ByVal _filter As PAListSalDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "COL_INDEX ASC, CREATED_DATE desc") As List(Of PAListSalDTO)
        Try
            Dim lst = (From p In Context.PA_LISTSAL
                        From o In Context.OT_OTHER_LIST.Where(Function(o) o.ID = p.DATA_TYPE).DefaultIfEmpty()
                        From sal_type In Context.OT_OTHER_LIST.Where(Function(sty) sty.ID = p.GROUP_TYPE).DefaultIfEmpty()
           Select New PAListSalDTO With {
                                        .ID = p.ID,
                                        .COL_NAME = p.COL_NAME,
                                        .NAME_VN = p.NAME_VN,
                                        .NAME_EN = p.NAME_EN,
                                        .DATA_TYPE = p.DATA_TYPE,
                                        .COL_INDEX = p.COL_INDEX,
                                        .STATUS = If(p.STATUS = "A", "Áp dụng", "Ngừng áp dụng"),
                                        .CREATED_DATE = p.CREATED_DATE,
                                        .REMARK = p.REMARK,
                                        .DATA_TYPE_NAME = If(p.DATA_TYPE = "1", "Kiểu số", If(p.DATA_TYPE = "2", "Kiểu ngày", "Kiểu chữ")),
                                        .GROUP_TYPE = p.GROUP_TYPE,
                                        .GROUP_TYPE_NAME = sal_type.NAME_VN
                                    })

            If _filter.COL_NAME <> "" Then
                lst = lst.Where(Function(p) p.COL_NAME.ToUpper.Contains(_filter.COL_NAME.ToUpper))
            End If
            If _filter.NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If _filter.DATA_TYPE <> 0 Then
                lst = lst.Where(Function(p) p.DATA_TYPE = _filter.DATA_TYPE)
            End If
            If _filter.DATA_TYPE_NAME <> 0 Then
                lst = lst.Where(Function(p) p.DATA_TYPE_NAME = _filter.DATA_TYPE_NAME)
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.GROUP_TYPE <> 0 Then
                lst = lst.Where(Function(p) p.GROUP_TYPE = _filter.GROUP_TYPE)
            End If
            If _filter.GROUP_TYPE_NAME <> 0 Then
                lst = lst.Where(Function(p) p.GROUP_TYPE_NAME = _filter.GROUP_TYPE_NAME)
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
    Public Function InsertListSal(ByVal objTitle As PAListSalDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_LISTSAL
        Dim iCount As Integer = 0

        Try

            objTitleData.COL_NAME = objTitle.COL_NAME
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.DATA_TYPE = objTitle.DATA_TYPE
            objTitleData.COL_INDEX = objTitle.COL_INDEX
            objTitleData.STATUS = objTitle.STATUS
            objTitleData.COL_CODE = objTitle.COL_CODE
            objTitleData.REMARK = objTitle.REMARK
            objTitleData.DATA_TYPE = objTitle.DATA_TYPE
            objTitleData.GROUP_TYPE = objTitle.GROUP_TYPE
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_PA_SETTING.ADD_COL_SALARY_NEW",
                                           New With {.COL_NAME = objTitle.COL_NAME,
                                                     .COL_TYPE = objTitle.DATA_TYPE})
            End Using
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_LISTSAL.EntitySet.Name)
            Context.PA_LISTSAL.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifyListSal(ByVal objTitle As PAListSalDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_LISTSAL With {.ID = objTitle.ID}
        Dim old_Name As String
        Dim old_Type As Decimal?
        Try
            objTitleData = (From p In Context.PA_LISTSAL Where p.ID = objTitleData.ID).SingleOrDefault
            old_Name = objTitleData.COL_NAME
            old_Type = objTitleData.DATA_TYPE
            objTitleData.COL_NAME = objTitle.COL_NAME
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.DATA_TYPE = objTitle.DATA_TYPE
            objTitleData.COL_INDEX = objTitle.COL_INDEX
            objTitleData.COL_CODE = objTitle.COL_CODE
            objTitleData.REMARK = objTitle.REMARK
            objTitleData.GROUP_TYPE = objTitle.GROUP_TYPE
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_PA_SETTING.EDIT_COL_SALARY_NEW",
                                           New With {.COL_NAME = old_Name,
                                                     .COL_NAME_NEW = objTitleData.COL_NAME,
                                                     .DATA_TYPE = old_Type,
                                                     .DATA_TYPE_NEW = objTitleData.DATA_TYPE})
            End Using
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveListSal(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_LISTSAL)
        Try
            lstData = (From p In Context.PA_LISTSAL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).STATUS = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function


    Public Function DeleteListSal(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstListSalData As List(Of PA_LISTSAL)
        Try
            lstListSalData = (From p In Context.PA_LISTSAL Where lstID.Contains(p.ID)).ToList
            Using cls As New DataAccess.NonQueryData
                For index = 0 To lstListSalData.Count - 1
                    cls.ExecuteStore("PKG_PA_SETTING.DELETE_COL_SALARY",
                                            New With {.COL_NAME = lstListSalData(index).COL_NAME})
                Next
            End Using
            For index = 0 To lstListSalData.Count - 1
                Context.PA_LISTSAL.DeleteObject(lstListSalData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

#End Region

#Region "lunch list : Đơn giá tiền ăn trưa"

    Public Function GetPriceLunchList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EFFECT_DATE desc") As List(Of ATPriceLunchDTO)

        Try
            Dim query = From p In Context.PA_PRICE_LUNCH

            Dim lst = query.Select(Function(p) New ATPriceLunchDTO With {
                                       .ID = p.ID,
                                       .PRICE = p.PRICE,
                                       .EFFECT_DATE = p.EFFECT_DATE,
                                       .EXPIRE_DATE = p.EXPIRE_DATE,
                                       .REMARK = p.REMARK,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY})


            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function

    Public Function GetPriceLunch(ByVal year As Decimal) As List(Of ATPriceLunchDTO)
        Try
            Dim query = From p In Context.PA_PRICE_LUNCH Where p.ID = year Order By p.ID Ascending, p.EFFECT_DATE Ascending
            Dim Period = query.Select(Function(p) New ATPriceLunchDTO With {
                                       .ID = p.ID,
                                       .PRICE = p.PRICE,
                                       .EFFECT_DATE = p.EFFECT_DATE,
                                       .EXPIRE_DATE = p.EXPIRE_DATE,
                                       .REMARK = p.REMARK,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY})


            Return Period.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertPriceLunch(ByVal objPeriod As ATPriceLunchDTO, ByVal objOrgPeriod As List(Of PA_ORG_LUNCH), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objPeriodData As New PA_PRICE_LUNCH
        Dim objOrgPeriodData As PA_ORG_LUNCH
        Try
            objPeriodData.ID = Utilities.GetNextSequence(Context, Context.PA_PRICE_LUNCH.EntitySet.Name)
            objPeriodData.PRICE = objPeriod.PRICE
            objPeriodData.EFFECT_DATE = objPeriod.EFFECT_DATE
            objPeriodData.EXPIRE_DATE = objPeriod.EXPIRE_DATE
            objPeriodData.REMARK = objPeriod.REMARK
            Context.PA_PRICE_LUNCH.AddObject(objPeriodData)
            Context.SaveChanges(log)
            If objPeriodData.ID > 0 Then
                For Each obj As PA_ORG_LUNCH In objOrgPeriod
                    objOrgPeriodData = New PA_ORG_LUNCH
                    objOrgPeriodData.ID = Utilities.GetNextSequence(Context, Context.PA_ORG_LUNCH.EntitySet.Name)
                    objOrgPeriodData.ORG_ID = obj.ORG_ID
                    objOrgPeriodData.LUNCH_ID = objPeriodData.ID
                    Context.PA_ORG_LUNCH.AddObject(objOrgPeriodData)
                    Context.SaveChanges(log)
                Next
            End If
            gID = objPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateATPriceLunch(ByVal _validate As ATPriceLunchDTO) As Boolean
        Try
            If _validate.ID <> 0 Then
                If _validate.ID <> 0 Then
                    Dim query = (From p In Context.PA_ORG_LUNCH Where p.LUNCH_ID = _validate.ID).ToList
                    If query.Count > 0 Then
                        Return False
                    Else
                        Return True
                    End If
                End If

            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ValidateATPriceLunchOrg(ByVal _validate As ATPriceLunchDTO)
        Dim query
        Try
            If _validate.EFFECT_DATE IsNot Nothing And _validate.EXPIRE_DATE IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_PRICE_LUNCH
                             From o In Context.PA_ORG_LUNCH.Where(Function(F) F.LUNCH_ID = p.ID).DefaultIfEmpty
                             Where (_validate.EFFECT_DATE <= p.EXPIRE_DATE And _validate.EXPIRE_DATE >= p.EFFECT_DATE) _
                             And o.ORG_ID = _validate.ORG_ID _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PA_PRICE_LUNCH
                             From o In Context.PA_ORG_LUNCH.Where(Function(F) F.LUNCH_ID = p.ID).DefaultIfEmpty
                             Where (_validate.EFFECT_DATE <= p.EXPIRE_DATE And _validate.EXPIRE_DATE >= p.EFFECT_DATE) _
                             And o.ORG_ID = _validate.ORG_ID).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifyPriceLunch(ByVal objPeriod As ATPriceLunchDTO, ByVal objOrgPeriod As List(Of PA_ORG_LUNCH), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPeriodData As New PA_PRICE_LUNCH With {.ID = objPeriod.ID}
        Dim objOrgPeriodData As PA_ORG_LUNCH
        Try
            Context.PA_PRICE_LUNCH.Attach(objPeriodData)
            objPeriodData.PRICE = objPeriod.PRICE
            objPeriodData.EFFECT_DATE = objPeriod.EFFECT_DATE
            objPeriodData.EXPIRE_DATE = objPeriod.EXPIRE_DATE
            objPeriodData.REMARK = objPeriod.REMARK
            If objPeriodData.ID > 0 Then

                If objOrgPeriod IsNot Nothing Then
                    Dim objDelete As List(Of PA_ORG_LUNCH) = (From p In Context.PA_ORG_LUNCH Where p.LUNCH_ID = objPeriodData.ID).ToList
                    For Each obj As PA_ORG_LUNCH In objDelete
                        Context.PA_ORG_LUNCH.DeleteObject(obj)
                    Next
                End If

                For Each ObjIns As PA_ORG_LUNCH In objOrgPeriod
                    objOrgPeriodData = New PA_ORG_LUNCH
                    objOrgPeriodData.ID = Utilities.GetNextSequence(Context, Context.PA_ORG_LUNCH.EntitySet.Name)
                    objOrgPeriodData.ORG_ID = ObjIns.ORG_ID
                    objOrgPeriodData.LUNCH_ID = objPeriodData.ID
                    Context.PA_ORG_LUNCH.AddObject(objOrgPeriodData)
                Next
            End If
            Context.SaveChanges(log)
            gID = objPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DeletePriceLunch(ByVal lstPeriod As ATPriceLunchDTO) As Boolean
        Dim objOrgPeriod As List(Of PA_ORG_LUNCH) = (From p In Context.PA_ORG_LUNCH Where p.LUNCH_ID = lstPeriod.ID).ToList
        Dim objPeriod As List(Of PA_PRICE_LUNCH) = (From p In Context.PA_PRICE_LUNCH Where p.ID = lstPeriod.ID).ToList
        Try
            For Each item In objOrgPeriod
                Context.PA_ORG_LUNCH.DeleteObject(item)
            Next
            For Each item In objPeriod
                Context.PA_PRICE_LUNCH.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region

#Region "CostCenter list"

    Public Function GetCostCenter(ByVal _filter As CostCenterDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS ASC, CREATED_DATE desc") As List(Of CostCenterDTO)

        Try
            Dim query = From p In Context.PA_COST_CENTER
                        Where p.IS_DELETED = 0

            If _filter.CODE <> "" Then
                query = query.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If

            If _filter.NAME <> "" Then
                query = query.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If

            If _filter.ACTFLG IsNot Nothing Then
                query = query.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            Dim lst = query.Select(Function(p) New CostCenterDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
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

    ''' <summary>
    ''' Insert data cho Cost Center
    ''' </summary>
    ''' <param name="objCostCenter"></param>
    ''' <param name="log"></param>
    ''' <param name="gID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Public Function InsertCostCenter(ByVal objCostCenter As CostCenterDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objCostCenterData As New PA_COST_CENTER
        Try
            objCostCenterData.ID = Utilities.GetNextSequence(Context, Context.PA_COST_CENTER.EntitySet.Name)
            objCostCenterData.CODE = objCostCenter.CODE.Trim
            objCostCenterData.NAME = objCostCenter.NAME.Trim
            objCostCenterData.REMARK = objCostCenter.REMARK
            objCostCenterData.ORDERS = objCostCenter.ORDERS
            objCostCenterData.ACTFLG = objCostCenter.ACTFLG
            objCostCenterData.IS_DELETED = objCostCenter.IS_DELETED
            Context.PA_COST_CENTER.AddObject(objCostCenterData)
            Context.SaveChanges(log)


            Context.SaveChanges(log)
            gID = objCostCenterData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifyCostCenter(ByVal objCostCenter As CostCenterDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCostCenterData As New PA_COST_CENTER With {.ID = objCostCenter.ID}
        Try
            Context.PA_COST_CENTER.Attach(objCostCenterData)
            objCostCenterData.CODE = objCostCenter.CODE.Trim
            objCostCenterData.NAME = objCostCenter.NAME.Trim
            objCostCenterData.REMARK = objCostCenter.REMARK
            objCostCenterData.ORDERS = objCostCenter.ORDERS

            Context.SaveChanges(log)
            gID = objCostCenterData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateCostCenter(ByVal _validate As CostCenterDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_COST_CENTER
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.IS_DELETED = 0 _
                             And p.ID <> _validate.ID).SingleOrDefault
                Else
                    query = (From p In Context.PA_COST_CENTER
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.IS_DELETED = 0).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ActiveCostCenter(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstCostCenterData As List(Of PA_COST_CENTER)
        Try
            lstCostCenterData = (From p In Context.PA_COST_CENTER Where lstID.Contains(p.ID)).ToList

            For idx = 0 To lstCostCenterData.Count - 1
                lstCostCenterData(idx).ACTFLG = bActive
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteCostCenterStatus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean
        Dim lstData As List(Of PA_COST_CENTER)
        Try
            lstData = (From p In Context.PA_COST_CENTER Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).IS_DELETED = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteCostCenter(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstCostCenterData As List(Of PA_COST_CENTER)
        Try
            lstCostCenterData = (From p In Context.PA_COST_CENTER Where lstID.Contains(p.ID)).ToList

            For idx = 0 To lstCostCenterData.Count - 1
                Context.PA_COST_CENTER.DeleteObject(lstCostCenterData(idx))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

#End Region

#Region "Org Bonus"
    Public Function GetOrgBonus(ByVal _filter As OrgBonusDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " ORDERS desc") As List(Of OrgBonusDTO)
        Try
            Dim lst = (From p In Context.PA_ORGBONUS
           Select New OrgBonusDTO With {
                                        .ID = p.ID,
                                        .CODE = p.CODE,
                                        .NAME = p.NAME,
                                        .ORDERS = p.ORDERS,
                                        .REMARK = p.REMARK,
                                        .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                        .CREATED_DATE = p.CREATED_DATE
                                    })

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function InsertOrgBonus(ByVal objTitle As OrgBonusDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_ORGBONUS
        Dim iCount As Integer = 0

        Try


            objTitleData.NAME = objTitle.NAME
            objTitleData.CODE = objTitle.CODE
            objTitleData.ORDERS = objTitle.ORDERS
            objTitleData.REMARK = objTitle.REMARK
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_ORGBONUS.EntitySet.Name)
            Context.PA_ORGBONUS.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function ModifyOrgBonus(ByVal objTitle As OrgBonusDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_ORGBONUS With {.ID = objTitle.ID}
        Dim old_Name As String
        Try
            objTitleData = (From p In Context.PA_ORGBONUS Where p.ID = objTitleData.ID).SingleOrDefault
            old_Name = objTitleData.NAME
            objTitleData.NAME = objTitle.NAME
            objTitleData.ORDERS = objTitle.ORDERS
            objTitleData.REMARK = objTitle.REMARK
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.CODE = objTitle.CODE
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveOrgBonus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_ORGBONUS)
        Try
            lstData = (From p In Context.PA_ORGBONUS Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function


    Public Function DeleteOrgBonus(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstOrgBonusData As List(Of PA_ORGBONUS)
        Try
            lstOrgBonusData = (From p In Context.PA_ORGBONUS Where lstID.Contains(p.ID)).ToList

            For index = 0 To lstOrgBonusData.Count - 1
                Context.PA_ORGBONUS.DeleteObject(lstOrgBonusData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function
    Public Function ValidateOrgBonus(ByVal _validate As OrgBonusDTO) As Boolean
        Try
            If _validate.ID <> 0 Then
                If _validate.ID <> 0 Then
                    Dim query = (From p In Context.PA_ORGBONUS Where p.ID <> _validate.ID And p.CODE = _validate.CODE).ToList
                    If query.Count > 0 Then
                        Return False
                    Else
                        Return True
                    End If
                End If

            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region

#Region "Payment Sources"
    Public Function GetPaymentSources(ByVal _filter As PaymentSourcesDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " effect_date desc") As List(Of PaymentSourcesDTO)
        Try
            Dim lst = (From p In Context.PA_PAYMENTSOURCES
                       From OT In Context.OT_OTHER_LIST Where OT.ID = p.PAY_TYPE
                       From OT_TYPE In Context.OT_OTHER_LIST_TYPE Where OT_TYPE.ID = OT.TYPE_ID And OT_TYPE.CODE = "PAYMENTSOURCES"
           Select New PaymentSourcesDTO With {
                                        .ID = p.ID,
                                        .YEAR = p.YEAR,
                                        .NAME = p.NAME,
                                        .ORDERS = p.ORDERS,
                                        .PAY_TYPE = p.PAY_TYPE,
                                        .PAY_TYPE_NAME = OT.NAME_VN,
                                        .REMARK = p.REMARK,
                                        .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
            .CREATED_DATE = p.CREATED_DATE
                                    })

            If _filter.YEAR.HasValue Then
                lst = lst.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If Not String.IsNullOrEmpty(_filter.NAME) Then
                lst = lst.Where(Function(f) f.NAME.ToLower().Contains(_filter.NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PAY_TYPE_NAME) Then
                lst = lst.Where(Function(f) f.PAY_TYPE_NAME.ToLower().Contains(_filter.PAY_TYPE_NAME.ToLower()))
            End If
            If _filter.ORDERS.HasValue Then
                lst = lst.Where(Function(f) f.ORDERS = _filter.ORDERS)
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(f) f.REMARK.ToLower().Contains(_filter.REMARK.ToLower()))
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
    Public Function InsertPaymentSources(ByVal objTitle As PaymentSourcesDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_PAYMENTSOURCES
        Dim iCount As Integer = 0

        Try


            objTitleData.NAME = objTitle.NAME
            objTitleData.YEAR = objTitle.YEAR
            objTitleData.PAY_TYPE = objTitle.PAY_TYPE
            objTitleData.ORDERS = objTitle.ORDERS
            objTitleData.REMARK = objTitle.REMARK
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_PAYMENTSOURCES.EntitySet.Name)
            Context.PA_PAYMENTSOURCES.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function ModifyPaymentSources(ByVal objTitle As PaymentSourcesDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_PAYMENTSOURCES With {.ID = objTitle.ID}
        Dim old_Name As String
        Try
            objTitleData = (From p In Context.PA_PAYMENTSOURCES Where p.ID = objTitleData.ID).SingleOrDefault
            old_Name = objTitleData.NAME
            objTitleData.NAME = objTitle.NAME
            objTitleData.ORDERS = objTitle.ORDERS
            objTitleData.REMARK = objTitle.REMARK
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.YEAR = objTitle.YEAR
            objTitleData.PAY_TYPE = objTitle.PAY_TYPE
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActivePaymentSources(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_PAYMENTSOURCES)
        Try
            lstData = (From p In Context.PA_PAYMENTSOURCES Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function


    Public Function DeletePaymentSources(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstPaymentSourcesData As List(Of PA_PAYMENTSOURCES)
        Try
            lstPaymentSourcesData = (From p In Context.PA_PAYMENTSOURCES Where lstID.Contains(p.ID)).ToList

            For index = 0 To lstPaymentSourcesData.Count - 1
                Context.PA_PAYMENTSOURCES.DeleteObject(lstPaymentSourcesData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

#End Region

#Region "Work Factor"
    Public Function GetWorkFactor(ByVal _filter As WorkFactorDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " EFFECT_DATE desc") As List(Of WorkFactorDTO)
        Try
            Dim lst = (From p In Context.PA_WORKFACTOR
           Select New WorkFactorDTO With {
                                        .ID = p.ID,
                                        .CODE = p.CODE,
                                        .XEPLOAI = p.XEPLOAI,
                                        .FACTOR = p.FACTOR,
                                        .BONUSE = p.BONUSE,
                                        .effect_date = p.EFFECT_DATE,
                                        .REMARK = p.REMARK,
                                        .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                        .CREATED_DATE = p.CREATED_DATE
                                    })

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function InsertWorkFactor(ByVal objTitle As WorkFactorDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_WORKFACTOR
        Dim iCount As Integer = 0

        Try


            objTitleData.XEPLOAI = objTitle.XEPLOAI
            objTitleData.CODE = objTitle.CODE
            objTitleData.FACTOR = objTitle.FACTOR
            objTitleData.BONUSE = objTitle.BONUSE
            objTitleData.EFFECT_DATE = objTitle.effect_date
            objTitleData.REMARK = objTitle.REMARK
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_WORKFACTOR.EntitySet.Name)
            Context.PA_WORKFACTOR.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function ModifyWorkFactor(ByVal objTitle As WorkFactorDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_WORKFACTOR With {.ID = objTitle.ID}
        Dim old_Name As String
        Try
            objTitleData = (From p In Context.PA_WORKFACTOR Where p.ID = objTitleData.ID).SingleOrDefault
            old_Name = objTitleData.XEPLOAI
            objTitleData.XEPLOAI = objTitle.XEPLOAI
            objTitleData.FACTOR = objTitle.FACTOR
            objTitleData.BONUSE = objTitle.BONUSE
            objTitleData.EFFECT_DATE = objTitle.effect_date
            objTitleData.REMARK = objTitle.REMARK
            'objTitleData.ACTFLG = "A"
            objTitleData.CODE = objTitle.CODE
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveWorkFactor(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_WORKFACTOR)
        Try
            lstData = (From p In Context.PA_WORKFACTOR Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function


    Public Function DeleteWorkFactor(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstWorkFactorData As List(Of PA_WORKFACTOR)
        Try
            lstWorkFactorData = (From p In Context.PA_WORKFACTOR Where lstID.Contains(p.ID)).ToList

            For index = 0 To lstWorkFactorData.Count - 1
                Context.PA_WORKFACTOR.DeleteObject(lstWorkFactorData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function
    Public Function ValidateWorkFactor(ByVal _validate As WorkFactorDTO) As Boolean
        Try
            If _validate.ID <> 0 Then
                If _validate.ID <> 0 Then
                    Dim query = (From p In Context.PA_WORKFACTOR Where p.ID <> _validate.ID And p.XEPLOAI = _validate.XEPLOAI).ToList
                    If query.Count > 0 Then
                        Return False
                    Else
                        Return True
                    End If
                End If

            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region

#Region "SalaryFund "

    Public Function GetSalaryFundByID(ByVal _filter As PASalaryFundDTO) As PASalaryFundDTO
        Try
            Dim query = From p In Context.PA_SALARY_FUND
                        Select New PASalaryFundDTO With {
                            .ID = p.ID,
                            .ORG_ID = p.ORG_ID,
                            .YEAR = p.YEAR,
                            .MONTH = p.MONTH,
                            .SAL_ALLOWANCE = p.SAL_ALLOWANCE,
                            .SAL_HARD = p.SAL_HARD,
                            .SAL_OTHER = p.SAL_OTHER,
                            .SAL_SOFT = p.SAL_SOFT,
                            .SAL_TOTAL = p.SAL_TOTAL,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If _filter.YEAR.HasValue Then
                lst = lst.Where(Function(f) f.YEAR = _filter.YEAR)
            End If

            If _filter.MONTH.HasValue Then
                lst = lst.Where(Function(f) f.MONTH = _filter.MONTH)
            End If

            If _filter.ORG_ID.HasValue Then
                lst = lst.Where(Function(f) f.ORG_ID = _filter.ORG_ID)

            End If

            Return lst.FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function UpdateSalaryFund(ByVal objSalaryFund As PASalaryFundDTO,
                                    ByVal log As UserLog) As Boolean
        Dim objSalaryFundData As New PA_SALARY_FUND
        Try
            objSalaryFundData = (From p In Context.PA_SALARY_FUND
                                 Where p.ORG_ID = objSalaryFund.ORG_ID And
                                 p.YEAR = objSalaryFund.YEAR And
                                 p.MONTH = objSalaryFund.MONTH).FirstOrDefault

            If objSalaryFundData Is Nothing Then
                objSalaryFundData = New PA_SALARY_FUND
                objSalaryFundData.ID = Utilities.GetNextSequence(Context, Context.PA_SALARY_FUND.EntitySet.Name)
                objSalaryFundData.YEAR = objSalaryFund.YEAR
                objSalaryFundData.MONTH = objSalaryFund.MONTH
                objSalaryFundData.ORG_ID = objSalaryFund.ORG_ID
                objSalaryFundData.SAL_TOTAL = objSalaryFund.SAL_TOTAL
                objSalaryFundData.SAL_SOFT = objSalaryFund.SAL_SOFT
                objSalaryFundData.SAL_OTHER = objSalaryFund.SAL_OTHER
                objSalaryFundData.SAL_HARD = objSalaryFund.SAL_HARD
                objSalaryFundData.SAL_ALLOWANCE = objSalaryFund.SAL_ALLOWANCE
                Context.PA_SALARY_FUND.AddObject(objSalaryFundData)
            Else
                objSalaryFundData.SAL_TOTAL = objSalaryFund.SAL_TOTAL
                objSalaryFundData.SAL_SOFT = objSalaryFund.SAL_SOFT
                objSalaryFundData.SAL_OTHER = objSalaryFund.SAL_OTHER
                objSalaryFundData.SAL_HARD = objSalaryFund.SAL_HARD
                objSalaryFundData.SAL_ALLOWANCE = objSalaryFund.SAL_ALLOWANCE
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

#End Region

#Region "TitleCost "

    Public Function GetTitleCost(ByVal _filter As PATitleCostDTO,
                                   ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PATitleCostDTO)
        Try
            Dim query = From p In Context.PA_TITLE_COST
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        Select New PATitleCostDTO With {
                                        .ID = p.ID,
                                        .TITLE_ID = p.TITLE_ID,
                                        .TITLE_NAME = t.NAME_VN,
                                        .SAL_BASIC = p.SAL_BASIC,
                                        .SAL_INS = p.SAL_INS,
                                        .SAL_MOBILE = p.SAL_MOBILE,
                                        .SAL_OTHER = p.SAL_OTHER,
                                        .SAL_RICE = p.SAL_RICE,
                                        .SAL_SOFT = p.SAL_SOFT,
                                        .SAL_TOTAL = p.SAL_TOTAL,
                                        .EFFECT_DATE = p.EFFECT_DATE,
                                        .EXPIRE_DATE = p.EXPIRE_DATE,
                                        .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If _filter.TITLE_NAME <> "" Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If

            If _filter.EFFECT_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE)
            End If

            If _filter.EXPIRE_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EXPIRE_DATE = _filter.EXPIRE_DATE)
            End If

            If _filter.SAL_BASIC.HasValue Then
                lst = lst.Where(Function(f) f.SAL_BASIC = _filter.SAL_BASIC)
            End If

            If _filter.SAL_INS.HasValue Then
                lst = lst.Where(Function(f) f.SAL_INS = _filter.SAL_INS)
            End If

            If _filter.SAL_MOBILE.HasValue Then
                lst = lst.Where(Function(f) f.SAL_MOBILE = _filter.SAL_MOBILE)
            End If

            If _filter.SAL_RICE.HasValue Then
                lst = lst.Where(Function(f) f.SAL_RICE = _filter.SAL_RICE)
            End If

            If _filter.SAL_OTHER.HasValue Then
                lst = lst.Where(Function(f) f.SAL_OTHER = _filter.SAL_OTHER)
            End If

            If _filter.SAL_SOFT.HasValue Then
                lst = lst.Where(Function(f) f.SAL_SOFT = _filter.SAL_SOFT)
            End If

            If _filter.SAL_TOTAL.HasValue Then
                lst = lst.Where(Function(f) f.SAL_TOTAL = _filter.SAL_TOTAL)
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

    Public Function InsertTitleCost(ByVal objTitle As PATitleCostDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_TITLE_COST
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_TITLE_COST.EntitySet.Name)
            objTitleData.TITLE_ID = objTitle.TITLE_ID
            objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.EXPIRE_DATE = objTitle.EXPIRE_DATE
            objTitleData.SAL_TOTAL = objTitle.SAL_TOTAL
            objTitleData.SAL_SOFT = objTitle.SAL_SOFT
            objTitleData.SAL_OTHER = objTitle.SAL_OTHER
            objTitleData.SAL_BASIC = objTitle.SAL_BASIC
            objTitleData.SAL_INS = objTitle.SAL_INS
            objTitleData.SAL_MOBILE = objTitle.SAL_MOBILE
            objTitleData.SAL_RICE = objTitle.SAL_RICE
            Context.PA_TITLE_COST.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifyTitleCost(ByVal objTitle As PATitleCostDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_TITLE_COST With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.PA_TITLE_COST Where p.ID = objTitleData.ID).FirstOrDefault
            objTitleData.TITLE_ID = objTitle.TITLE_ID
            objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.EXPIRE_DATE = objTitle.EXPIRE_DATE
            objTitleData.SAL_TOTAL = objTitle.SAL_TOTAL
            objTitleData.SAL_SOFT = objTitle.SAL_SOFT
            objTitleData.SAL_OTHER = objTitle.SAL_OTHER
            objTitleData.SAL_BASIC = objTitle.SAL_BASIC
            objTitleData.SAL_INS = objTitle.SAL_INS
            objTitleData.SAL_MOBILE = objTitle.SAL_MOBILE
            objTitleData.SAL_RICE = objTitle.SAL_RICE
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DeleteTitleCost(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstTitleCostData As List(Of PA_TITLE_COST)
        Try
            lstTitleCostData = (From p In Context.PA_TITLE_COST Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTitleCostData.Count - 1
                Context.PA_TITLE_COST.DeleteObject(lstTitleCostData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region

#Region "Get List Manning"
    Public Function LoadComboboxListMannName(ByVal org_id As Integer, ByVal year As Integer) As DataTable
        Try
            'Dim query = From p In Context.PA_SALARY_FUND
            '            Select New PASalaryFundDTO With {
            '                .ID = p.ID,
            '                .ORG_ID = p.ORG_ID,
            '                .YEAR = p.YEAR,
            '                .MONTH = p.MONTH,
            '                .SAL_ALLOWANCE = p.SAL_ALLOWANCE,
            '                .SAL_HARD = p.SAL_HARD,
            '                .SAL_OTHER = p.SAL_OTHER,
            '                .SAL_SOFT = p.SAL_SOFT,
            '                .SAL_TOTAL = p.SAL_TOTAL,
            '                .CREATED_DATE = p.CREATED_DATE}

            'Dim lst = query
            'Return lst
            Dim listMannName As DataTable
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_RECRUITMENT.LOAD_COMBOBOX_LISTMANNINGNAME",
                                                     New With {.P_ORG_ID = org_id,
                                                               .P_YEAR = year,
                                                                .P_CUR = cls.OUT_CURSOR})
                'If Not dtData Is Nothing Or Not dtData.Tables(0) Is Nothing Then
                '    listMannName = dtData.Tables(0)
                'End If
                listMannName = dtData
            End Using
            Return listMannName
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "LoadComboboxListMannName")
            Throw ex
        End Try
    End Function

#End Region

#Region "Validate Combobox"
    Public Function ValidateCombobox(ByVal cbxData As ComboBoxDataDTO) As Boolean
        Try
            'Danh sách nhóm ký hiệu lương
            If cbxData.GET_GROUP_TYPE Then
                Dim ID As Decimal = cbxData.LIST_GROUP_TYPE(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                             Where p.ACTFLG = "A" And t.CODE = "SAL_TYPE" And p.ID = ID
                                             Order By p.CREATED_DATE Descending
                         Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                             .TYPE_ID = p.TYPE_ID
                         }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            'Danh loại bảng lương
            If cbxData.GET_TYPE_PAYMENT Then
                Dim ID As Decimal = cbxData.LIST_TYPE_PAYMENT(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                             Where p.ACTFLG = "A" And t.CODE = "TYPE_PAYMENT" And p.ID = ID
                                             Order By p.CREATED_DATE Descending
                         Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                             .TYPE_ID = p.TYPE_ID
                         }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            'Danh sách các đối tượng cư trú
            If cbxData.GET_LIST_RESIDENT Then
                Dim ID As Decimal = cbxData.LIST_LIST_RESIDENT(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                             Where p.ACTFLG = "A" And t.CODE = "PA_RESIDENT" And p.ID = ID
                                             Order By p.CREATED_DATE Descending
                         Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                             .TYPE_ID = p.TYPE_ID
                         }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            ' Danh sách các khoản tiền 
            If cbxData.GET_LIST_PAYMENT Then
                Dim ID As Decimal = cbxData.LIST_LIST_PAYMENT(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                             Where p.ACTFLG = "A" And t.CODE = "PA_LISTPAYMENT" And p.ID = ID
                                             Order By p.CREATED_DATE Descending
                         Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                             .TYPE_ID = p.TYPE_ID
                         }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            ' Danh sách các khoản tiền trong bảng lương
            'TYPE_PAYMENT = 4123 là danh mục bảng lương tổng hợp
            If cbxData.GET_LIST_SALARY Then
                Dim ID As Decimal = cbxData.LIST_LIST_SALARY(0).ID
                Dim list As List(Of PAListSalariesDTO) = (From p In Context.PA_LISTSALARIES
                                             Where p.STATUS = "A" And p.TYPE_PAYMENT = 4123 And p.ID = ID
                                             Order By p.CREATED_DATE Descending
                         Select New PAListSalariesDTO With {
                             .ID = p.ID,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN
                         }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            'Danh sách các đối tượng lương(bảng lương)
            If cbxData.GET_OBJECT_PAYMENT Then
                Dim ID As Decimal = cbxData.LIST_OBJECT_PAYMENT(0).ID
                Dim list As List(Of PAObjectSalaryDTO) = (From p In Context.PA_OBJECT_SALARY
                                             Where p.ACTFLG = "A" And p.ID = ID
                                             Order By p.CREATED_DATE Descending
                         Select New PAObjectSalaryDTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_VN = p.NAME_VN,
                             .NAME_EN = p.NAME_EN
                         }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            If cbxData.GET_SALARY_LEVEL Then
                Dim ID As Decimal = cbxData.LIST_SALARY_LEVEL(0).ID
                Dim list As List(Of SalaryLevelDTO) = (From p In Context.PA_SALARY_LEVEL
                                             Join o In Context.PA_SALARY_GROUP On p.SAL_GROUP_ID Equals o.ID
                                             Where p.ACTFLG = "A" And p.ID = ID
                                             Order By p.NAME.ToUpper
                                             Select New SalaryLevelDTO With {
                                                 .ID = p.ID,
                                                 .NAME = p.NAME,
                                                 .SAL_GROUP_ID = p.SAL_GROUP_ID
                                             }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            If cbxData.GET_SALARY_GROUP Then
                Dim ID As Decimal = cbxData.LIST_SALARY_GROUP(0).ID
                Dim list As List(Of SalaryGroupDTO) = (From p In Context.PA_SALARY_GROUP
                                                       Where p.ID = ID
                                             Order By p.NAME.ToUpper
                                             Select New SalaryGroupDTO With {
                                                 .ID = p.ID,
                                                 .NAME = p.NAME,
                                                 .EFFECT_DATE = p.EFFECT_DATE
                                             }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            'Danh muc cap chuc danh
            If cbxData.GET_LIST_TITLE_LEVEL Then
                Dim ID As Decimal = cbxData.LIST_LIST_TITLE_LEVEL(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                             Where p.ACTFLG = "A" And t.CODE = "HU_TITLE_LEVEL" And p.ID = ID
                                             Order By p.CREATED_DATE Descending
                         Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                             .TYPE_ID = p.TYPE_ID
                         }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            'Danh loại luong thuong
            If cbxData.GET_INCENTIVE_TYPE Then
                Dim ID As Decimal = cbxData.LIST_INCENTIVE_TYPE(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                             Where p.ACTFLG = "A" And t.CODE = "PA_INCENTIVE_TYPE" And p.ID - ID
                                             Order By p.CREATED_DATE Descending
                         Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                             .TYPE_ID = p.TYPE_ID
                         }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            'Danh loại luong thuong
            If cbxData.GET_IMPORT_TYPE Then
                Dim ID As Decimal = cbxData.LIST_IMPORT_TYPE(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                             Where p.ACTFLG = "A" And t.CODE = "IMPORT_TYPE" And p.ID = ID
                                             Order By p.CREATED_DATE Descending
                         Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                             .TYPE_ID = p.TYPE_ID
                         }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            If cbxData.GET_SALARY_TYPE Then
                Dim ID As Decimal = cbxData.LIST_SALARY_TYPE(0).ID
                Dim list As List(Of SalaryTypeDTO) = (From p In Context.PA_SALARY_TYPE
                                                      Where p.ID = ID
                                                      Order By p.NAME.ToUpper
                                             Select New SalaryTypeDTO With {
                                                 .ID = p.ID,
                                                 .NAME = p.NAME
                                             }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            If cbxData.GET_PAY_TYPE Then
                Dim ID As Decimal = cbxData.LIST_PAY_TYPE(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                            Where p.ACTFLG = "A" And t.CODE = "PAYMENTSOURCES" And p.ID = ID
                                    Select New OT_OTHERLIST_DTO With {
                                        .ID = p.ID,
                                        .CODE = p.CODE,
                                        .NAME_EN = p.NAME_EN,
                                        .NAME_VN = p.NAME_VN,
                                        .TYPE_ID = p.TYPE_ID
                                    }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Return False
        End Try
    End Function

#End Region

#Region "SaleCommision"
    Public Function GetSaleCommision(ByVal _filter As SaleCommisionDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SaleCommisionDTO)

        Try
            Dim query = From p In Context.PA_SALE_COMMISION

            If _filter.CODE <> "" Then
                query = query.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If

            If _filter.NAME <> "" Then
                query = query.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                Dim searchStr As String
                If _filter.ACTFLG.ToUpper = "ÁP DỤNG" Then
                    searchStr = "A"
                ElseIf _filter.ACTFLG.ToUpper = "NGỪNG ÁP DỤNG" Then
                    searchStr = "I"
                Else
                    searchStr = _filter.ACTFLG.ToUpper
                End If
                query = query.Where(Function(p) p.ACTFLG.ToUpper.Contains(searchStr))
            End If
            Dim lst = query.Select(Function(p) New SaleCommisionDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .ORDERS = p.ORDERS,
                                       .SALE_RATE = p.SALE_RATE,
                                       .SALE_RATE1 = p.SALE_RATE,
                                       .CREATED_DATE = p.CREATED_DATE,
            .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")
                                       })
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function

    Public Function InsertSaleCommision(ByVal objSaleCommision As SaleCommisionDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objSaleCommisionData As New PA_SALE_COMMISION
        Try
            objSaleCommisionData.ID = Utilities.GetNextSequence(Context, Context.PA_SALARY_TYPE.EntitySet.Name)
            objSaleCommisionData.CODE = objSaleCommision.CODE.Trim
            objSaleCommisionData.NAME = objSaleCommision.NAME.Trim
            objSaleCommisionData.REMARK = objSaleCommision.REMARK
            objSaleCommisionData.ORDERS = objSaleCommision.ORDERS
            objSaleCommisionData.ACTFLG = objSaleCommision.ACTFLG
            objSaleCommisionData.SALE_RATE = objSaleCommision.SALE_RATE
            Context.PA_SALE_COMMISION.AddObject(objSaleCommisionData)
            Context.SaveChanges(log)
            gID = objSaleCommision.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifySaleCommision(ByVal objSaleCommision As SaleCommisionDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objSaleCommisionData As New PA_SALE_COMMISION With {.ID = objSaleCommision.ID}
        Try
            Context.PA_SALE_COMMISION.Attach(objSaleCommisionData)
            objSaleCommisionData.CODE = objSaleCommision.CODE.Trim
            objSaleCommisionData.NAME = objSaleCommision.NAME.Trim
            objSaleCommisionData.REMARK = objSaleCommision.REMARK
            objSaleCommisionData.ORDERS = objSaleCommision.ORDERS
            objSaleCommisionData.SALE_RATE = objSaleCommision.SALE_RATE
            Context.SaveChanges(log)
            gID = objSaleCommisionData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DeleteSaleCommision(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstSaleCommsionData As List(Of PA_SALE_COMMISION)
        Try
            lstSaleCommsionData = (From p In Context.PA_SALE_COMMISION Where lstID.Contains(p.ID)).ToList

            For idx = 0 To lstSaleCommsionData.Count - 1
                Context.PA_SALE_COMMISION.DeleteObject(lstSaleCommsionData(idx))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveSaleCommision(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_SALE_COMMISION)
        Try
            lstData = (From p In Context.PA_SALE_COMMISION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region


End Class


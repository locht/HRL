Imports Framework.UI
Imports Payroll.PayrollBusiness

Partial Public Class PayrollRepository

#Region "Calculate Salary"
    Public Function Load_Calculate_Load(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.Load_Calculate_Load(OrgId, PeriodId, IsDissolve, IsLoad, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function Calculate_data_sum(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.Calculate_data_sum(OrgId, PeriodId, IsDissolve, IsLoad, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function Load_data_sum(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.Load_data_sum(OrgId, PeriodId, IsDissolve, IsLoad, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function Calculate_data_temp(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.Calculate_data_temp(OrgId, PeriodId, IsDissolve, IsLoad, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function Load_data_calculate(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.Load_data_calculate(OrgId, PeriodId, IsDissolve, IsLoad, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetLitsCalculate(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataSet
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetLitsCalculate(OrgId, PeriodId, IsDissolve, IsLoad, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetListSalaryVisibleCol() As List(Of PAListSalariesDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetListSalaryVisibleCol()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "IMPORTBONUS"
    Public Function GetlistYear() As DataTable
        Try
            Dim rep As New PayrollBusinessClient
            Return rep.GetlistYear()
        Catch ex As Exception

        End Try
    End Function
    Public Function GetListGrBonus(ByVal year As Decimal) As DataTable
        Try
            Dim rep As New PayrollBusinessClient
            Return rep.GetListGrBonus(year)
        Catch ex As Exception

        End Try
    End Function
    Public Function GetGrBonus() As DataTable
        Try
            Dim rep As New PayrollBusinessClient
            Return rep.GetGrBonus()
        Catch ex As Exception

        End Try
    End Function

    Public Function GetListImportBonus(ByVal obj_sal_id As Integer, ByVal periodBonus As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String,
                                       Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable
        Try
            Dim rep As New PayrollBusinessClient
            Return rep.GetListImportBonus(obj_sal_id, periodBonus, OrgId, IsDissolve, EmployeeId, Me.Log, Sorts)
        Catch ex As Exception

        End Try
    End Function
    Public Function SaveLstImportBONUS(ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal taxId As Decimal, ByVal dtData As DataTable, ByVal lstColVal As List(Of String), ByRef RecordSussces As Integer) As Boolean
        Try
            Dim rep As New PayrollBusinessClient
            Return rep.SaveLstImportBONUS(SalaryGroup, Period, taxId, dtData, lstColVal, Me.Log, RecordSussces)
        Catch ex As Exception

        End Try
    End Function
#End Region
#Region "MANAGEMENTBONUS"
    Public Function GetListManagementBonus(ByVal obj_sal_id As Integer, ByVal periodBonus As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String,
                                       Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable
        Try
            Dim rep As New PayrollBusinessClient
            Return rep.GetListManagementBonus(obj_sal_id, periodBonus, OrgId, IsDissolve, EmployeeId, Me.Log, Sorts)
        Catch ex As Exception

        End Try
    End Function
#End Region
#Region "Import Salary"

    Public Function GetImportSalaryBonus(ByVal Year As Integer, ByVal Obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetImportBonus(Year, Obj_sal_id, PeriodId, OrgId, IsDissolve, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Import Salary"

    Public Function GetImportSalary(ByVal Obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String, Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetImportSalary(Obj_sal_id, PeriodId, OrgId, IsDissolve, EmployeeId, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_DATA_SEND_MAIL(ByVal Obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String, Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GET_DATA_SEND_MAIL(Obj_sal_id, PeriodId, OrgId, IsDissolve, EmployeeId, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetMappingSalary(ByVal Obj_sal_id As Integer, ByVal PeriodId As Integer) As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetMappingSalary(Obj_sal_id, PeriodId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetMappingSalaryImport(ByVal Obj_sal_id As Integer, ByVal PeriodId As Integer) As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetMappingSalaryImport(Obj_sal_id, PeriodId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetSalaryList() As List(Of PAListSalariesDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryList()
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function GetSalaryList_TYPE(ByVal OBJ_SAL_ID As Decimal) As List(Of PAListSalariesDTO)
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetSalaryList_TYPE(OBJ_SAL_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function SaveImport(ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByVal lstColVal As List(Of String), ByRef RecordSussces As Integer) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.SaveImport(SalaryGroup, Period, dtData, lstColVal, Me.Log, RecordSussces)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function SaveImportBonus(ByVal Org_Id As Decimal, ByVal Year As Decimal, ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByVal lstColVal As List(Of String), ByRef RecordSussces As Integer) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.SaveImportBonus(Org_Id, Year, SalaryGroup, Period, dtData, lstColVal, Me.Log, RecordSussces)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function SaveImportSalary_Fund_Mapping(ByVal Year As Decimal, ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByRef RecordSussces As Integer) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.SaveImportSalary_Fund_Mapping(Year, SalaryGroup, Period, dtData, Me.Log, RecordSussces)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "IPORTAL - View phiếu lương"
    Public Function GetPayrollSheetSum(ByVal PeriodId As Integer, ByVal EmployeeId As String,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPayrollSheetSum(PeriodId, EmployeeId, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetPayrollSheetSumSheet(ByVal PeriodId As Integer, ByVal EmployeeId As String) As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetPayrollSheetSumSheet(PeriodId, EmployeeId, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CHECK_OPEN_CLOSE(ByVal PeriodId As Integer, ByVal EmployeeId As String) As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.CHECK_OPEN_CLOSE(PeriodId, EmployeeId, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function


    Public Function CheckPeriod(ByVal PeriodId As Integer, ByVal EmployeeId As Decimal) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.CheckPeriod(PeriodId, EmployeeId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

    Public Function GetImportSalaryTNCN(ByVal Obj_sal_id As Integer, ByVal taxId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String, Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable
        Using rep As New PayrollBusinessClient
            Try
                Return rep.GetImportSalaryTNCN(Obj_sal_id, taxId, OrgId, IsDissolve, EmployeeId, Me.Log, Sorts)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function SaveImportTNCN(ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal taxId As Decimal, ByVal dtData As DataTable, ByVal lstColVal As List(Of String), ByRef RecordSussces As Integer) As Boolean
        Using rep As New PayrollBusinessClient
            Try
                Return rep.SaveImportTNCN(SalaryGroup, Period, taxId, dtData, lstColVal, Me.Log, RecordSussces)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

End Class

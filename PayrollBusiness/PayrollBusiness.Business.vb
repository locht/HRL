Imports PayrollBusiness.ServiceContracts
Imports PayrollDAL
Imports Framework.Data

Namespace PayrollBusiness.ServiceImplementations
    Partial Public Class PayrollBusiness
        Implements ServiceContracts.IPayrollBusiness

#Region "Calculate Salary"
        Public Function Load_Calculate_Load(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.Load_Calculate_Load
            Dim rep As New PayrollRepository
            Return rep.Load_Calculate_Load(OrgId, PeriodId, IsDissolve, IsLoad, log)
        End Function
        Public Function Calculate_data_sum(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.Calculate_data_sum
            Dim rep As New PayrollRepository
            Return rep.Calculate_data_sum(OrgId, PeriodId, IsDissolve, IsLoad, log)
        End Function
        Public Function Load_data_sum(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.Load_data_sum
            Dim rep As New PayrollRepository
            Return rep.Load_data_sum(OrgId, PeriodId, IsDissolve, IsLoad, log)
        End Function
        Public Function Calculate_data_temp(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.Calculate_data_temp
            Dim rep As New PayrollRepository
            Return rep.Calculate_data_temp(OrgId, PeriodId, IsDissolve, IsLoad, log)
        End Function
        Public Function Load_data_calculate(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.Load_data_calculate
            Dim rep As New PayrollRepository
            Return rep.Load_data_calculate(OrgId, PeriodId, IsDissolve, IsLoad, log)
        End Function
        Public Function GetLitsCalculate(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer,
                                     ByVal log As UserLog, Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataSet Implements ServiceContracts.IPayrollBusiness.GetLitsCalculate
            Dim rep As New PayrollRepository
            Return rep.GetLitsCalculate(OrgId, PeriodId, IsDissolve, IsLoad, log)
        End Function
        Public Function GetListSalaryVisibleCol() As List(Of PAListSalariesDTO) Implements ServiceContracts.IPayrollBusiness.GetListSalaryVisibleCol
            Dim rep As New PayrollRepository
            Return rep.GetListSalaryVisibleCol()
        End Function


#End Region
#Region "Import Bonus"

        Public Function GetImportBonus(ByVal Year As Integer, ByVal obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable Implements ServiceContracts.IPayrollBusiness.GetImportBonus
            Try
                Dim rep As New PayrollRepository
                Return rep.GetImportBonus(Year, obj_sal_id, PeriodId, OrgId, IsDissolve, log, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Import Salary"

        Public Function GetImportSalary(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable Implements ServiceContracts.IPayrollBusiness.GetImportSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.GetImportSalary(obj_sal_id, PeriodId, OrgId, IsDissolve, EmployeeId, log, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GET_DATA_SEND_MAIL(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String, ByVal log As UserLog,
                                       Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable Implements ServiceContracts.IPayrollBusiness.GET_DATA_SEND_MAIL
            Try
                Dim rep As New PayrollRepository
                Return rep.GET_DATA_SEND_MAIL(obj_sal_id, PeriodId, OrgId, IsDissolve, EmployeeId, log, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetMappingSalary(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer) As DataTable Implements ServiceContracts.IPayrollBusiness.GetMappingSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.GetMappingSalary(obj_sal_id, PeriodId)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetMappingSalaryImport(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer) As DataTable Implements ServiceContracts.IPayrollBusiness.GetMappingSalaryImport
            Try
                Dim rep As New PayrollRepository
                Return rep.GetMappingSalaryImport(obj_sal_id, PeriodId)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetSalaryList() As List(Of PAListSalariesDTO) Implements ServiceContracts.IPayrollBusiness.GetSalaryList
            Try
                Dim rep As New PayrollRepository
                Return rep.GetSalaryList()
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetSalaryList_TYPE(ByVal OBJ_SAL_ID As Decimal) As List(Of PAListSalariesDTO) Implements ServiceContracts.IPayrollBusiness.GetSalaryList_TYPE
            Try
                Dim rep As New PayrollRepository
                Return rep.GetSalaryList_TYPE(OBJ_SAL_ID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function SaveImport(ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByVal lstColVal As List(Of String), ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean Implements ServiceContracts.IPayrollBusiness.SaveImport
            Try
                Dim rep As New PayrollRepository
                Return rep.SaveImport(SalaryGroup, Period, dtData, lstColVal, log, RecordSussces)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function SaveImportBonus(ByVal Org_Id As Decimal, ByVal Year As Decimal, ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByVal lstColVal As List(Of String), ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean Implements ServiceContracts.IPayrollBusiness.SaveImportBonus
            Try
                Dim rep As New PayrollRepository
                Return rep.SaveImportBonus(Org_Id, Year, SalaryGroup, Period, dtData, lstColVal, log, RecordSussces)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function SaveImportSalary_Fund_Mapping(ByVal Year As Decimal, ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean Implements ServiceContracts.IPayrollBusiness.SaveImportSalary_Fund_Mapping
            Try
                Dim rep As New PayrollRepository
                Return rep.SaveImportSalary_Fund_Mapping(Year, SalaryGroup, Period, dtData, log, RecordSussces)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "IPORTAL - View phiếu lương"
        Public Function GetPayrollSheetSum(ByVal PeriodId As Integer, ByVal EmployeeId As String, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable Implements ServiceContracts.IPayrollBusiness.GetPayrollSheetSum
            Try
                Dim rep As New PayrollRepository
                Return rep.GetPayrollSheetSum(PeriodId, EmployeeId, log, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPayrollSheetSumSheet(ByVal PeriodId As Integer, ByVal EmployeeId As String, ByVal log As UserLog) As DataTable Implements ServiceContracts.IPayrollBusiness.GetPayrollSheetSumSheet
            Try
                Dim rep As New PayrollRepository
                Return rep.GetPayrollSheetSumSheet(PeriodId, EmployeeId, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function CHECK_OPEN_CLOSE(ByVal PeriodId As Integer, ByVal EmployeeId As String, ByVal log As UserLog) As DataTable Implements ServiceContracts.IPayrollBusiness.CHECK_OPEN_CLOSE
            Try
                Dim rep As New PayrollRepository
                Return rep.CHECK_OPEN_CLOSE(PeriodId, EmployeeId, log)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function CheckPeriod(ByVal PeriodId As Integer, ByVal EmployeeId As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.CheckPeriod
            Try
                Dim rep As New PayrollRepository
                Return rep.CheckPeriod(PeriodId, EmployeeId)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ActionSendPayslip(ByVal lstEmployee As List(Of Decimal),
                                   ByVal orgID As Decimal?,
                                   ByVal isDissolve As Decimal?,
                                   ByVal periodID As Decimal,
                                   ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ActionSendPayslip
            Using rep As New PayrollRepository
                Try
                    Return rep.ActionSendPayslip(lstEmployee, orgID, isDissolve, periodID, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ActionSendBonusslip(ByVal lstEmployee As List(Of Decimal),
                                   ByVal orgID As Decimal?,
                                   ByVal isDissolve As Decimal?,
                                   ByVal periodID As Decimal,
                                   ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.ActionSendBonusslip
            Using rep As New PayrollRepository
                Try
                    Return rep.ActionSendBonusslip(lstEmployee, orgID, isDissolve, periodID, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region


    End Class
End Namespace


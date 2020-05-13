Imports PayrollBusiness.ServiceContracts
Imports PayrollDAL
Imports Framework.Data

Namespace PayrollBusiness.ServiceImplementations
    Partial Public Class PayrollBusiness
        Implements ServiceContracts.IPayrollBusiness
#Region "Allowance List"

        Public Function GetAllowanceList(ByVal _filter As AllowanceListDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AllowanceListDTO) Implements ServiceContracts.IPayrollBusiness.GetAllowanceList
            Try
                Dim rep As New PayrollRepository
                Return rep.GetAllowanceList(_filter, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function CheckAllowance(ByVal empId As Decimal, ByVal day As Date, ByVal typeAllowance As Decimal, ByVal id As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.CheckAllowance
            Try
                Dim rep As New PayrollRepository
                Return rep.CheckAllowance(empId, day, typeAllowance, id)
            Catch ex As Exception

            End Try
        End Function


#End Region

#Region "Allowance"
        Public Function GetAllowance(ByVal _filter As AllowanceDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of AllowanceDTO) Implements ServiceContracts.IPayrollBusiness.GetAllowance
            Try
                Dim rep As New PayrollRepository
                Return rep.GetAllowance(_filter, PageIndex, PageSize, Total, _param, Sorts, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function InsertAllowance(ByVal objAllowance As AllowanceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertAllowance
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertAllowance(objAllowance, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyAllowance(ByVal objAllowance As AllowanceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyAllowance
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyAllowance(objAllowance, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveAllowance(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveAllowance
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveAllowance(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteAllowance(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteAllowance
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteAllowance(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Taxation List"

        Public Function GetTaxation(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PATaxationDTO) Implements ServiceContracts.IPayrollBusiness.GetTaxation
            Try
                Dim rep As New PayrollRepository
                Return rep.GetTaxation(PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertTaxation(ByVal objTaxation As PATaxationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertTaxation
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertTaxation(objTaxation, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyTaxation(ByVal objTaxation As PATaxationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyTaxation
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyTaxation(objTaxation, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveTaxation(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveTaxation
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveTaxation(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteTaxation(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteTaxation
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteTaxation(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Payment list"

        Public Function GetPaymentList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO) Implements ServiceContracts.IPayrollBusiness.GetPaymentList
            Try
                Dim rep As New PayrollRepository
                Return rep.GetPaymentList(PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GetPaymentListAll(Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO) Implements ServiceContracts.IPayrollBusiness.GetPaymentListAll
            Try
                Dim rep As New PayrollRepository
                Return rep.GetPaymentListAll(Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function InsertPaymentList(ByVal objPaymentList As PAPaymentListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertPaymentList
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertPaymentList(objPaymentList, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyPaymentList(ByVal objPaymentList As PAPaymentListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyPaymentList
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyPaymentList(objPaymentList, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActivePaymentList(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActivePaymentList
            Try
                Dim rep As New PayrollRepository
                Return rep.ActivePaymentList(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeletePaymentList(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeletePaymentList
            Try
                Dim rep As New PayrollRepository
                Return rep.DeletePaymentList(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "ObjectSalary"

        Public Function GetObjectSalary(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAObjectSalaryDTO) Implements ServiceContracts.IPayrollBusiness.GetObjectSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.GetObjectSalary(PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GetObjectSalaryAll(Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAObjectSalaryDTO) Implements ServiceContracts.IPayrollBusiness.GetObjectSalaryAll
            Try
                Dim rep As New PayrollRepository
                Return rep.GetObjectSalaryAll(Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function InsertObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertObjectSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertObjectSalary(objObjectSalary, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ValidateObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateObjectSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.ValidateObjectSalary(objObjectSalary)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyObjectSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyObjectSalary(objObjectSalary, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveObjectSalary(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveObjectSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveObjectSalary(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteObjectSalary(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteObjectSalary
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteObjectSalary(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Period List"
        Public Function GetPeriodList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "START_DATE desc") As List(Of ATPeriodDTO) Implements ServiceContracts.IPayrollBusiness.GetPeriodList
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetPeriodList(PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetPeriodbyYear(ByVal year As Decimal) As List(Of ATPeriodDTO) Implements ServiceContracts.IPayrollBusiness.GetPeriodbyYear
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetPeriodbyYear(year)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertPeriod(ByVal objPeriod As ATPeriodDTO, ByVal objOrgPeriod As List(Of AT_ORG_PERIOD), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertPeriod
            Try
                Return PayrollRepositoryStatic.Instance.InsertPeriod(objPeriod, objOrgPeriod, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateATPeriod(ByVal objPeriod As ATPeriodDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateATPeriod
            Try
                Return PayrollRepositoryStatic.Instance.ValidateATPeriod(objPeriod)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateATPeriodDay(ByVal objPeriod As ATPeriodDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateATPeriodDay
            Try
                Return PayrollRepositoryStatic.Instance.ValidateATPeriodDay(objPeriod)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyPeriod(ByVal objPeriod As ATPeriodDTO, ByVal objOrgPeriod As List(Of AT_ORG_PERIOD), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyPeriod
            Try
                Return PayrollRepositoryStatic.Instance.ModifyPeriod(objPeriod, objOrgPeriod, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeletePeriod(ByVal lstPeriod As ATPeriodDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.DeletePeriod
            Try
                Return PayrollRepositoryStatic.Instance.DeletePeriod(lstPeriod)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ActivePeriod(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActivePeriod
            Try
                Dim rep As New PayrollRepository
                Return rep.ActivePeriod(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function CountHoliday(ByVal date1 As Date, ByVal date2 As Date) As Integer Implements ServiceContracts.IPayrollBusiness.CountHoliday
            Try
                Dim rep As New PayrollRepository
                Return rep.CountHoliday(date1, date2)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "setupbonus"
        Public Function GetSetUpBonus(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "FROM_DATE desc") As List(Of ATSetUpBonusDTO) Implements ServiceContracts.IPayrollBusiness.GetSetUpBonus
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSetUpBonus(PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function InsertSetUpBonus(ByVal objPeriod As ATSetUpBonusDTO, ByVal objOrgPeriod As List(Of AT_ORG_SETUPBONUS), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSetUpBonus
            Try
                Return PayrollRepositoryStatic.Instance.InsertSetUpBonus(objPeriod, objOrgPeriod, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ModifySetUpBonus(ByVal objPeriod As ATSetUpBonusDTO, ByVal objOrgPeriod As List(Of AT_ORG_SETUPBONUS), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySetUpBonus
            Try
                Return PayrollRepositoryStatic.Instance.ModifySetUpBonus(objPeriod, objOrgPeriod, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function GetListOrg(ByVal id As Decimal) As List(Of AT_ORG_SETUPBONUS) Implements ServiceContracts.IPayrollBusiness.GetListOrg
            Try
                Return PayrollRepositoryStatic.Instance.GetListOrg(id)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function DeleteSetUpBonus(ByVal lstPeriod As ATSetUpBonusDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSetUpBonus
            Try
                Return PayrollRepositoryStatic.Instance.DeleteSetUpBonus(lstPeriod)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ActiveSetUpBonus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveSetUpBonus
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveSetUpBonus(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "work standard"
        Public Function GetWorkStandard(ByVal _filter As Work_StandardDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = " YEAR, PERIOD_ID desc") As List(Of Work_StandardDTO) Implements ServiceContracts.IPayrollBusiness.GetWorkStandard
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetWorkStandard(_filter, PageIndex, PageSize, Total, log, Sorts)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetWorkStandardbyYear(ByVal year As Decimal) As List(Of Work_StandardDTO) Implements ServiceContracts.IPayrollBusiness.GetWorkStandardbyYear
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetWorkStandardbyYear(year)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertWorkStandard(ByVal objPeriod As Work_StandardDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertWorkStandard
            Try
                Return PayrollRepositoryStatic.Instance.InsertWorkStandard(objPeriod, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateWorkStandard(ByVal objPeriod As Work_StandardDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateWorkStandard
            Try
                Return PayrollRepositoryStatic.Instance.ValidateWorkStandard(objPeriod)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function IsCompanyLevel(ByVal org_id As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.IsCompanyLevel
            Try
                Return PayrollRepositoryStatic.Instance.IsCompanyLevel(org_id)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyWorkStandard(ByVal objPeriod As Work_StandardDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyWorkStandard
            Try
                Return PayrollRepositoryStatic.Instance.ModifyWORKSTANDARD(objPeriod, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteWorkStandard(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteWorkStandard
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteWorkStandard(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ActiveWorkStandard(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveWorkStandard
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveWorkStandard(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "List Salary"
        Public Function GetAllFomulerGroup(ByVal _filter As PAFomulerGroup, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "IDX ASC, CREATED_DATE desc") As List(Of PAFomulerGroup) Implements ServiceContracts.IPayrollBusiness.GetAllFomulerGroup
            Try
                Return PayrollRepositoryStatic.Instance.GetAllFomulerGroup(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function InsertFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertFomulerGroup
            Try
                Return PayrollRepositoryStatic.Instance.InsertFomulerGroup(objPeriod, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ModifyFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyFomulerGroup
            Try
                Return PayrollRepositoryStatic.Instance.ModifyFomulerGroup(objPeriod, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeleteFomulerGroup(ByVal lstDelete As PAFomulerGroup) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteFomulerGroup
            Try
                Return PayrollRepositoryStatic.Instance.DeleteFomulerGroup(lstDelete)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetListAllSalary(ByVal gID As Decimal) As List(Of PAFomuler) Implements ServiceContracts.IPayrollBusiness.GetListAllSalary
            Try
                Return PayrollRepositoryStatic.Instance.GetListAllSalary(gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetObjectSalaryColumn(ByVal gID As Decimal) As DataTable Implements ServiceContracts.IPayrollBusiness.GetObjectSalaryColumn
            Try
                Return PayrollRepositoryStatic.Instance.GetObjectSalaryColumn(gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetListSalColunm(ByVal gID As Decimal) As DataTable Implements ServiceContracts.IPayrollBusiness.GetListSalColunm
            Try
                Return PayrollRepositoryStatic.Instance.GetListSalColunm(gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetListInputColumn(ByVal gID As Decimal) As DataTable Implements ServiceContracts.IPayrollBusiness.GetListInputColumn
            Try
                Return PayrollRepositoryStatic.Instance.GetListInputColumn(gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetListCalculation() As List(Of OT_OTHERLIST_DTO) Implements ServiceContracts.IPayrollBusiness.GetListCalculation
            Try
                Return PayrollRepositoryStatic.Instance.GetListCalculation()
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function CheckFomuler(ByVal sCol As String, ByVal sFormuler As String, ByVal objID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.CheckFomuler
            Try
                Return PayrollRepositoryStatic.Instance.CheckFomuler(sCol, sFormuler, objID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function CopyFomuler(ByRef F_ID As Decimal, ByVal log As UserLog, ByRef T_ID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.CopyFomuler
            Try
                Return PayrollRepositoryStatic.Instance.CopyFomuler(F_ID, log, T_ID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function SaveFomuler(ByVal objData As PAFomuler, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.SaveFomuler
            Try
                Return PayrollRepositoryStatic.Instance.SaveFomuler(objData, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ActiveFolmulerGroup(ByVal lstID As Decimal, ByVal log As UserLog, ByVal bActive As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveFolmulerGroup
            Try
                Return PayrollRepositoryStatic.Instance.ActiveFolmulerGroup(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Salary list"

        Public Function GetListSalaries(ByVal _filter As PAListSalariesDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "COL_INDEX ASC, CREATED_DATE desc") As List(Of PAListSalariesDTO) Implements ServiceContracts.IPayrollBusiness.GetListSalaries
            Try
                Dim rep As New PayrollRepository
                Return rep.GetListSalaries(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertListSalaries(ByVal objListSalaries As PAListSalariesDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertListSalaries
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertListSalaries(objListSalaries, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function InsertPA_SAL_MAPPING(ByVal objListSal As PA_SALARY_FUND_MAPPINGDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertPA_SAL_MAPPING
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertPA_SAL_MAPPING(objListSal, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyListSalaries(ByVal objListSalaries As PAListSalariesDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyListSalaries
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyListSalaries(objListSalaries, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveListSalaries(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveListSalaries
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveListSalaries(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteListSalariesStatus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteListSalariesStatus
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteListSalariesStatus(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteListSalaries(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteListSalaries
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteListSalaries(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetListSal(ByVal _filter As PAListSalDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "COL_INDEX ASC, CREATED_DATE desc") As List(Of PAListSalDTO) Implements ServiceContracts.IPayrollBusiness.GetListSal
            Try
                Dim rep As New PayrollRepository
                Return rep.GetListSal(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertListSal(ByVal objListSal As PAListSalDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertListSal
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertListSal(objListSal, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyListSal(ByVal objListSal As PAListSalDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyListSal
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyListSal(objListSal, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveListSal(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveListSal
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveListSal(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function



        Public Function DeleteListSal(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteListSal
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteListSal(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "lunch list : Đơn giá tiền ăn trưa"
        Public Function GetPriceLunchList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EFFECT_DATE desc") As List(Of ATPriceLunchDTO) Implements ServiceContracts.IPayrollBusiness.GetPriceLunchList
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetPriceLunchList(PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPriceLunch(ByVal year As Decimal) As List(Of ATPriceLunchDTO) Implements ServiceContracts.IPayrollBusiness.GetPriceLunch
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetPriceLunch(year)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertPriceLunch(ByVal objPeriod As ATPriceLunchDTO, ByVal objOrgPeriod As List(Of PA_ORG_LUNCH), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertPriceLunch
            Try
                Return PayrollRepositoryStatic.Instance.InsertPriceLunch(objPeriod, objOrgPeriod, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateATPriceLunch(ByVal _validate As ATPriceLunchDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateATPriceLunch
            Try
                Return PayrollRepositoryStatic.Instance.ValidateATPriceLunch(_validate)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ValidateATPriceLunchOrg(ByVal _validate As ATPriceLunchDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateATPriceLunchOrg
            Try
                Return PayrollRepositoryStatic.Instance.ValidateATPriceLunchOrg(_validate)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function ModifyPriceLunch(ByVal objPeriod As ATPriceLunchDTO, ByVal objOrgPeriod As List(Of PA_ORG_LUNCH), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyPriceLunch
            Try
                Return PayrollRepositoryStatic.Instance.ModifyPriceLunch(objPeriod, objOrgPeriod, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeletePriceLunch(ByVal lstPeriod As ATPriceLunchDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.DeletePriceLunch
            Try
                Return PayrollRepositoryStatic.Instance.DeletePriceLunch(lstPeriod)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region


#Region "SalaryGroup"
        Public Function GetCostCenter(ByVal _filter As CostCenterDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS ASC, CREATED_DATE desc") As List(Of CostCenterDTO) Implements ServiceContracts.IPayrollBusiness.GetCostCenter
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetCostCenter(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertCostCenter(ByVal objCostCenter As CostCenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertCostCenter
            Try
                Return PayrollRepositoryStatic.Instance.InsertCostCenter(objCostCenter, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyCostCenter(ByVal obj As CostCenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyCostCenter
            Try
                Return PayrollRepositoryStatic.Instance.ModifyCostCenter(obj, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
        Public Function ValidateCostCenter(ByVal obj As CostCenterDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateCostCenter
            Try
                Return PayrollRepositoryStatic.Instance.ValidateCostCenter(obj)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Function ActiveCostCenter(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveCostCenter
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveCostCenter(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteCostCenterStatus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteCostCenterStatus
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteCostCenterStatus(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteCostCenter(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteCostCenter
            Try
                Return PayrollRepositoryStatic.Instance.DeleteCostCenter(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Org Bonus"
        Public Function GetOrgBonus(ByVal _filter As OrgBonusDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " ORDERS desc") As List(Of OrgBonusDTO) Implements ServiceContracts.IPayrollBusiness.GetOrgBonus
            Try
                Dim rep As New PayrollRepository
                Return rep.GetOrgBonus(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function InsertOrgBonus(ByVal objTitle As OrgBonusDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertOrgBonus
            Dim rep As New PayrollRepository
            Return rep.InsertOrgBonus(objTitle, log, gID)

        End Function
        Public Function ModifyOrgBonus(ByVal objTitle As OrgBonusDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyOrgBonus
            Dim rep As New PayrollRepository
            Return rep.ModifyOrgBonus(objTitle, log, gID)

        End Function
        Public Function ActiveOrgBonus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveOrgBonus
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveOrgBonus(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeleteOrgBonus(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteOrgBonus
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteOrgBonus(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ValidateOrgBonus(ByVal _validate As OrgBonusDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateOrgBonus
            Try
                Dim rep As New PayrollRepository
                Return rep.ValidateOrgBonus(_validate)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Payment Sources"
        Public Function GetPaymentSources(ByVal _filter As PaymentSourcesDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " ORDERS desc") As List(Of PaymentSourcesDTO) Implements ServiceContracts.IPayrollBusiness.GetPaymentSources
            Try
                Dim rep As New PayrollRepository
                Return rep.GetPaymentSources(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function InsertPaymentSources(ByVal objTitle As PaymentSourcesDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertPaymentSources
            Dim rep As New PayrollRepository
            Return rep.InsertPaymentSources(objTitle, log, gID)

        End Function
        Public Function ModifyPaymentSources(ByVal objTitle As PaymentSourcesDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyPaymentSources
            Dim rep As New PayrollRepository
            Return rep.ModifyPaymentSources(objTitle, log, gID)

        End Function
        Public Function ActivePaymentSources(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActivePaymentSources
            Try
                Dim rep As New PayrollRepository
                Return rep.ActivePaymentSources(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeletePaymentSources(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeletePaymentSources
            Try
                Dim rep As New PayrollRepository
                Return rep.DeletePaymentSources(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
#Region "Work Factor"
        Public Function GetWorkFactor(ByVal _filter As WorkFactorDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " ORDERS desc") As List(Of WorkFactorDTO) Implements ServiceContracts.IPayrollBusiness.GetWorkFactor
            Try
                Dim rep As New PayrollRepository
                Return rep.GetWorkFactor(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function InsertWorkFactor(ByVal objTitle As WorkFactorDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertWorkFactor
            Dim rep As New PayrollRepository
            Return rep.InsertWorkFactor(objTitle, log, gID)

        End Function
        Public Function ModifyWorkFactor(ByVal objTitle As WorkFactorDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyWorkFactor
            Dim rep As New PayrollRepository
            Return rep.ModifyWorkFactor(objTitle, log, gID)

        End Function
        Public Function ActiveWorkFactor(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveWorkFactor
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveWorkFactor(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function DeleteWorkFactor(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteWorkFactor
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteWorkFactor(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ValidateWorkFactor(ByVal _validate As WorkFactorDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.ValidateWorkFactor
            Try
                Dim rep As New PayrollRepository
                Return rep.ValidateWorkFactor(_validate)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "SalaryFund List"

        Public Function GetSalaryFundByID(ByVal _filter As PASalaryFundDTO) As PASalaryFundDTO _
            Implements ServiceContracts.IPayrollBusiness.GetSalaryFundByID
            Try
                Dim rep As New PayrollRepository
                Return rep.GetSalaryFundByID(_filter)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function UpdateSalaryFund(ByVal objSalaryFund As PASalaryFundDTO,
                                    ByVal log As UserLog) As Boolean Implements ServiceContracts.IPayrollBusiness.UpdateSalaryFund
            Try
                Dim rep As New PayrollRepository
                Return rep.UpdateSalaryFund(objSalaryFund, log)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

#End Region

#Region "TitleCost List"

        Public Function GetTitleCost(ByVal _filter As PATitleCostDTO,
                                   ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PATitleCostDTO) Implements ServiceContracts.IPayrollBusiness.GetTitleCost
            Try
                Dim rep As New PayrollRepository
                Return rep.GetTitleCost(_filter, PageIndex, PageSize, Total, Sorts)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertTitleCost(ByVal objTitleCost As PATitleCostDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertTitleCost
            Try
                Dim rep As New PayrollRepository
                Return rep.InsertTitleCost(objTitleCost, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function ModifyTitleCost(ByVal objTitleCost As PATitleCostDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyTitleCost
            Try
                Dim rep As New PayrollRepository
                Return rep.ModifyTitleCost(objTitleCost, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteTitleCost(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteTitleCost
            Try
                Dim rep As New PayrollRepository
                Return rep.DeleteTitleCost(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Get List Manning"
        Public Function LoadComboboxListMannName(ByVal org_id As Integer, ByVal year As Integer) As DataTable _
            Implements ServiceContracts.IPayrollBusiness.LoadComboboxListMannName
            Try
                Dim rep As New PayrollRepository
                Return rep.LoadComboboxListMannName(org_id, year)
            Catch ex As Exception

                Throw ex
            End Try
        End Function
#End Region

#Region "Validate Combobox"
        Public Function ValidateCombobox(ByVal cbxData As ComboBoxDataDTO) As Boolean _
            Implements ServiceContracts.IPayrollBusiness.ValidateCombobox
            Try
                Dim rep As New PayrollRepository
                Return rep.ValidateCombobox(cbxData)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "SALE COMMISION"
        Public Function GetSaleCommision(ByVal _filter As SaleCommisionDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SaleCommisionDTO) Implements ServiceContracts.IPayrollBusiness.GetSaleCommision
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetSaleCommision(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function InsertSaleCommision(ByVal objSaleCommision As SaleCommisionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertSaleCommision
            Try
                Return PayrollRepositoryStatic.Instance.InsertSaleCommision(objSaleCommision, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifySaleCommision(ByVal objSaleCommision As SaleCommisionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifySaleCommision
            Try
                Return PayrollRepositoryStatic.Instance.ModifySaleCommision(objSaleCommision, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteSaleCommision(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteSaleCommision
            Try
                Return PayrollRepositoryStatic.Instance.DeleteSaleCommision(lstID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveSaleCommision(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean _
           Implements ServiceContracts.IPayrollBusiness.ActiveSaleCommision
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveSaleCommision(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region


#Region "Quyet Toan Thue"
        Public Function GetTaxFinalizationList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "START_DATE desc") As List(Of PATaxFinalizationDTO) Implements ServiceContracts.IPayrollBusiness.GetTaxFinalizationList
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetTaxFinalizationList(PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertTaxFinalization(ByVal objPeriod As PATaxFinalizationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.InsertTaxFinalization
            Try
                Return PayrollRepositoryStatic.Instance.InsertTaxFinalization(objPeriod, log, gID)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ModifyTaxFinalization(ByVal objPeriod As PATaxFinalizationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IPayrollBusiness.ModifyTaxFinalization
            Try
                Return PayrollRepositoryStatic.Instance.ModifyTaxFinalization(objPeriod, log, gID)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

        Public Function DeleteTaxFinalization(ByVal lstPeriod As PATaxFinalizationDTO) As Boolean Implements ServiceContracts.IPayrollBusiness.DeleteTaxFinalization
            Try
                Return PayrollRepositoryStatic.Instance.DeleteTaxFinalization(lstPeriod)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActiveTaxFinalization(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IPayrollBusiness.ActiveTaxFinalization
            Try
                Dim rep As New PayrollRepository
                Return rep.ActiveTaxFinalization(lstID, log, bActive)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetTaxFinalizationbyYear(ByVal year As Decimal) As List(Of PATaxFinalizationDTO) Implements ServiceContracts.IPayrollBusiness.GetTaxFinalizationbyYear
            Try
                Dim lst = PayrollRepositoryStatic.Instance.GetTaxFinalizationbyYear(year)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

        Public Function GetListOrgPeriod(ByVal id As Decimal) As List(Of AT_ORG_PERIOD) Implements ServiceContracts.IPayrollBusiness.GetListOrgPeriod
            Try
                Return PayrollRepositoryStatic.Instance.GetListOrgPeriod(id)
            Catch ex As Exception

                Throw ex
            End Try
        End Function

    End Class
End Namespace


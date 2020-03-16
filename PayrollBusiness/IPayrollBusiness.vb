Imports PayrollDAL
Imports Framework.Data
' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
Namespace PayrollBusiness.ServiceContracts
    <ServiceContract()>
    Public Interface IPayrollBusiness
#Region "Allowance lisst"

        <OperationContract()>
        Function GetAllowanceList(ByVal _filter As AllowanceListDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AllowanceListDTO)
        

#End Region

#Region "Allowance"
        <OperationContract()>
        Function GetAllowance(ByVal _filter As AllowanceDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AllowanceDTO)

        <OperationContract()>
        Function InsertAllowance(ByVal objAllowance As AllowanceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyAllowance(ByVal objAllowance As AllowanceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAllowance(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteAllowance(ByVal lstID As List(Of Decimal)) As Boolean

#End Region

#Region "Common"
        <OperationContract()>
        Function ActionSendPayslip(ByVal lstEmployee As List(Of Decimal), ByVal orgID As Decimal?, ByVal isDissolve As Decimal?, ByVal periodID As Decimal, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ActionSendBonusslip(ByVal lstEmployee As List(Of Decimal), ByVal orgID As Decimal?, ByVal isDissolve As Decimal?, ByVal periodID As Decimal, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As TABLE_NAME) As Boolean
        <OperationContract()>
        Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetTitleList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
#End Region
#Region "Calculate Salary"
        <OperationContract()>
        Function Load_Calculate_Load(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function Load_data_sum(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function Calculate_data_sum(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function Load_data_calculate(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function Calculate_data_temp(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetListSalaryVisibleCol() As List(Of PAListSalariesDTO)
        <OperationContract()>
        Function GetLitsCalculate(ByVal OrgId As Integer, ByVal PeriodId As Integer, ByVal IsDissolve As Integer, ByVal IsLoad As Integer,
                                    ByVal log As UserLog, Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataSet
#End Region
#Region "Import Bonus"
        <OperationContract()>
        Function GetImportBonus(ByVal Year As Integer, ByVal obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable
#End Region
#Region "Import Salary"
        <OperationContract()>
        Function GetImportSalary(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable

        <OperationContract()>
        Function GET_DATA_SEND_MAIL(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal EmployeeId As String, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable
        <OperationContract()>
        Function GetMappingSalary(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer) As DataTable
        <OperationContract()>
        Function GetMappingSalaryImport(ByVal obj_sal_id As Integer, ByVal PeriodId As Integer) As DataTable
        <OperationContract()>
        Function GetSalaryList() As List(Of PAListSalariesDTO)
        <OperationContract()>
        Function GetSalaryList_TYPE(ByVal OBJ_SAL_ID As Decimal) As List(Of PAListSalariesDTO)
        <OperationContract()>
        Function SaveImport(ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByVal lstColVal As List(Of String), ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean
        <OperationContract()>
        Function SaveImportBonus(ByVal Org_Id As Decimal, ByVal Year As Decimal, ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByVal lstColVal As List(Of String), ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean
        <OperationContract()>
        Function SaveImportSalary_Fund_Mapping(ByVal Year As Decimal, ByVal SalaryGroup As Decimal, ByVal Period As Decimal, ByVal dtData As DataTable, ByVal log As UserLog, ByRef RecordSussces As Integer) As Boolean
#End Region

#Region "Hold Salary"

        <OperationContract()>
        Function GetHoldSalaryList(ByVal PeriodId As Integer, ByVal OrgId As Integer, ByVal IsDissolve As Integer, ByVal log As UserLog,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAHoldSalaryDTO)

        <OperationContract()>
        Function InsertHoldSalary(ByVal objPeriod As List(Of PAHoldSalaryDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteHoldSalary(ByVal lstDelete As List(Of Decimal)) As Boolean
#End Region

#Region "Taxation List"

        <OperationContract()>
        Function GetTaxation(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PATaxationDTO)

        <OperationContract()>
        Function InsertTaxation(ByVal objTaxation As PATaxationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyTaxation(ByVal objTaxation As PATaxationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveTaxation(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteTaxation(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Payment List"

        <OperationContract()>
        Function GetPaymentList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO)
        <OperationContract()>
        Function GetPaymentListAll(Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO)
        <OperationContract()>
        Function InsertPaymentList(ByVal objPaymentList As PAPaymentListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyPaymentList(ByVal objPaymentList As PAPaymentListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActivePaymentList(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeletePaymentList(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Object Salary"
        <OperationContract()>
        Function GetObjectSalary(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAObjectSalaryDTO)
        <OperationContract()>
        Function GetObjectSalaryAll(Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAObjectSalaryDTO)
        <OperationContract()>
        Function InsertObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO) As Boolean

        <OperationContract()>
        Function ModifyObjectSalary(ByVal objObjectSalary As PAObjectSalaryDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveObjectSalary(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteObjectSalary(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Period List"
        <OperationContract()>
        Function GetPeriodList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "START_DATE desc") As List(Of ATPeriodDTO)
        <OperationContract()>
        Function GetPeriodbyYear(ByVal year As Decimal) As List(Of ATPeriodDTO)

        <OperationContract()>
        Function InsertPeriod(ByVal objPeriod As ATPeriodDTO, ByVal objOrgPeriod As List(Of AT_ORG_PERIOD), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyPeriod(ByVal objPeriod As ATPeriodDTO, ByVal objOrgPeriod As List(Of AT_ORG_PERIOD), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeletePeriod(ByVal lstPeriod As ATPeriodDTO) As Boolean
        <OperationContract()>
        Function ValidateATPeriod(ByVal objPeriod As ATPeriodDTO) As Boolean
        <OperationContract()>
        Function ValidateATPeriodDay(ByVal objPeriod As ATPeriodDTO) As Boolean
        <OperationContract()>
        Function ActivePeriod(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
#End Region
#Region "Work Standard"
        <OperationContract()>
        Function GetWorkStandard(ByVal _filter As Work_StandardDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = " YEAR, PERIOD_ID desc") As List(Of Work_StandardDTO)
        <OperationContract()>
        Function GetWorkStandardbyYear(ByVal year As Decimal) As List(Of Work_StandardDTO)
        <OperationContract()>
        Function InsertWorkStandard(ByVal objPeriod As Work_StandardDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyWorkStandard(ByVal objPeriod As Work_StandardDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeleteWorkStandard(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function ValidateWorkStandard(ByVal objPeriod As Work_StandardDTO) As Boolean
        <OperationContract()>
        Function ActiveWorkStandard(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function IsCompanyLevel(ByVal org_id As Decimal) As Boolean
#End Region

#Region "List Salary"
        <OperationContract()>
        Function DeleteFomulerGroup(ByVal lstDelete As PAFomulerGroup) As Boolean
        <OperationContract()>
        Function ModifyFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function InsertFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        '<OperationContract()>
        'Function GetAllFomulerGroup() As List(Of PAFomulerGroup)
        <OperationContract()>
        Function GetAllFomulerGroup(ByVal _filter As PAFomulerGroup, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "IDX ASC, CREATED_DATE desc") As List(Of PAFomulerGroup)
        <OperationContract()>
        Function GetListAllSalary(ByVal gID As Decimal) As List(Of PAFomuler)
        <OperationContract()>
        Function GetObjectSalaryColumn(ByVal gID As Decimal) As DataTable
        <OperationContract()>
        Function GetListSalColunm(ByVal gID As Decimal) As DataTable
        <OperationContract()>
        Function GetListInputColumn(ByVal gID As Decimal) As DataTable
        <OperationContract()>
        Function GetListCalculation() As List(Of OT_OTHERLIST_DTO)
        <OperationContract()>
        Function CopyFomuler(ByRef F_ID As Decimal, ByVal log As UserLog, ByRef T_ID As Decimal) As Boolean
        <OperationContract()>
        Function SaveFomuler(ByVal objData As PAFomuler, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function CheckFomuler(ByVal sCol As String, ByVal sFormuler As String, ByVal objID As Decimal) As Boolean
        <OperationContract()>
        Function ActiveFolmulerGroup(ByVal lstID As Decimal, ByVal log As UserLog, ByVal bActive As Decimal) As Boolean
#End Region

#Region "Salary Group"
        <OperationContract()>
        Function GetSalaryGroup(ByVal _filter As SalaryGroupDTO, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryGroupDTO)
        <OperationContract()>
        Function GetSalaryGroupCombo(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function InsertSalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateSalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO) As Boolean
        <OperationContract()>
        Function ModifySalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ActiveSalaryGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeleteSalaryGroup(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function GetEffectSalaryGroup() As SalaryGroupDTO

#End Region

#Region "Salary ExRate"
        <OperationContract()>
        Function GetSalaryExRate(ByVal _filter As SalaryExRateDTO, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryExRateDTO)
        <OperationContract()>
        Function GetSalaryExRateCombo(ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function InsertSalaryExRate(ByVal objSalaryExRate As SalaryExRateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateSalaryExRate(ByVal objSalaryExRate As SalaryExRateDTO) As Boolean
        <OperationContract()>
        Function ModifySalaryExRate(ByVal objSalaryExRate As SalaryExRateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ActiveSalaryExRate(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeleteSalaryExRate(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function GetEffectSalaryExRate() As SalaryExRateDTO

#End Region

#Region "Salary Level"

        <OperationContract()>
        Function GetSalaryLevel(ByVal _filter As SalaryLevelDTO, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryLevelDTO)
        <OperationContract()>
        Function InsertSalaryLevel(ByVal objSalaryLevel As SalaryLevelDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateSalaryLevel(ByVal objSalaryLevel As SalaryLevelDTO) As Boolean
        <OperationContract()>
        Function ModifySalaryLevel(ByVal objSalaryLevel As SalaryLevelDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveSalaryLevel(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteSalaryLevel(ByVal lstSalaryLevel As List(Of Decimal)) As Boolean

#End Region

#Region "Salary Level Group"

        <OperationContract()>
        Function GetSalaryLevelGroup(ByVal _filter As SalaryLevelGroupDTO, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryLevelGroupDTO)
        <OperationContract()>
        Function InsertSalaryLevelGroup(ByVal objSalaryLevelGroup As SalaryLevelGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateSalaryLevelGroup(ByVal objSalaryLevelGroup As SalaryLevelGroupDTO) As Boolean
        <OperationContract()>
        Function ModifySalaryLevelGroup(ByVal objSalaryLevelGroup As SalaryLevelGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveSalaryLevelGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteSalaryLevelGroup(ByVal lstSalaryLevelGroup As List(Of Decimal)) As Boolean

#End Region

#Region "Salary Level Type"

        <OperationContract()>
        Function GetSalaryLevelTypeList() As List(Of SalaryLevelGroupDTO)
        <OperationContract()>
        Function InsertSalaryLevelType(ByVal objSalaryLevel As SalaryLevelTypeDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateSalaryLevelType(ByVal objSalaryLevel As SalaryLevelTypeDTO) As Boolean
        <OperationContract()>
        Function ModifySalaryLevelType(ByVal objSalaryLevel As SalaryLevelTypeDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveSalaryLevelType(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteSalaryLevelType(ByVal lstSalaryLevel As List(Of Decimal)) As Boolean

#End Region

#Region "Salary Level Unilever"

        <OperationContract()>
        Function GetSalaryLevel_Unilever(ByVal _filter As SalaryLevelDTO, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryLevelDTO)
        <OperationContract()>
        Function GetSalaryLevelCombo(ByVal salGroupID As Decimal, ByVal isBlank As Boolean, Optional ByVal other_Use As String = "ALL") As DataTable
        <OperationContract()>
        Function InsertSalaryLevel_Unilever(ByVal objSalaryLevel As SalaryLevelDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateSalaryLevel_Unilever(ByVal objSalaryLevel As SalaryLevelDTO) As Boolean
        <OperationContract()>
        Function ModifySalaryLevel_Unilever(ByVal objSalaryLevel As SalaryLevelDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveSalaryLevel_Unilever(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteSalaryLevel_Unilever(ByVal lstSalaryLevel() As SalaryLevelDTO) As Boolean

#End Region

#Region "Salary Rank"

        <OperationContract()>
        Function GetSalaryRank(ByVal _filter As SalaryRankDTO, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryRankDTO)
        <OperationContract()>
        Function GetSalaryRank_Unilever(ByVal _filter As SalaryRankDTO, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryRankDTO)
        <OperationContract()>
        Function GetSalaryRankCombo(ByVal salLevelID As Decimal, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function InsertSalaryRank(ByVal objSalaryRank As SalaryRankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateSalaryRank(ByVal objSalaryRank As SalaryRankDTO) As Boolean
        <OperationContract()>
        Function ModifySalaryRank(ByVal objSalaryRank As SalaryRankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveSalaryRank(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteSalaryRank(ByVal lstSalaryRank() As SalaryRankDTO) As Boolean

#End Region

#Region "Incentive Rank"

        <OperationContract()>
        Function GetIncentiveRank(ByVal _filter As IncentiveRankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of IncentiveRankDTO)
        <OperationContract()>
        Function GetIncentiveRankDetail(ByVal _filter As IncentiveRankDetailDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TO_TARGET, CREATED_DATE desc") As List(Of IncentiveRankDetailDTO)

        <OperationContract()>
        Function GetIncentiveRankIncludeDetail(ByVal _filter As IncentiveRankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of IncentiveRankDTO)
        <OperationContract()>
        Function GetIncentiveRankIdIncludeDetail(ByVal _filter As IncentiveRankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As IncentiveRankDTO
        <OperationContract()>
        Function InsertIncentiveRankIncludeDetail(ByVal objIncentiveRank As IncentiveRankDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function InsertIncentiveRank(ByVal objIncentiveRank As IncentiveRankDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function InsertIncentiveRankDetail(ByVal objIncentiveRankDetail As IncentiveRankDetailDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function InsertIncentiveListRankDetail(ByVal objIncentiveRankDetail As List(Of IncentiveRankDetailDTO),
                                   ByVal log As UserLog, ByRef gID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function ModifyIncentiveRank(ByVal objIncentiveRank As IncentiveRankDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyIncentiveRankDetail(ByVal objIncentiveRankDetail As IncentiveRankDetailDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyIncentiveRankIncludeDetail(ByVal objIncentive As IncentiveRankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateIncentiveRank(ByVal _validate As IncentiveRankDTO)
        <OperationContract()>
        Function ActiveIncentiveRank(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeleteIncentiveRank(ByVal lstIncentiveRank As List(Of IncentiveRankDTO)) As Boolean
        <OperationContract()>
        Function ActiveIncentiveRankDetail(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeleteIncentiveRankDetail(ByVal lstIncentiveRank() As IncentiveRankDetailDTO) As Boolean

#End Region

#Region "SALARYTYPE_GROUP"

        <OperationContract()>
        Function GetSalaryType_Group(ByVal _filter As SalaryType_GroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of SalaryType_GroupDTO)
        <OperationContract()>
        Function InsertSalaryType_Group(ByVal objSalaryType_Group As SalaryType_GroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifySalaryType_Group(ByVal objSalaryType_Group As SalaryType_GroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateSalaryType_Group(ByVal _validate As SalaryType_GroupDTO)

        <OperationContract()>
        Function ActiveSalaryType_Group(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeleteSalaryType_GroupStatus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean
        <OperationContract()>
        Function DeleteSalaryType_Group(ByVal lstSalaryType_Group() As SalaryType_GroupDTO) As Boolean

#End Region

#Region "Salary Type"
        <OperationContract()>
        Function DeleteSalaryType(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ModifySalaryType(ByVal objSalaryType As SalaryTypeDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateSalaryType(ByVal objSalaryType As SalaryTypeDTO) As Boolean

        <OperationContract()>
        Function InsertSalaryType(ByVal objSalaryType As SalaryTypeDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function GetSalaryType(ByVal _filter As SalaryTypeDTO, ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Total As Integer, Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryTypeDTO)

        <OperationContract()>
        Function ActiveSalaryType(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function GetSalaryTypebyIncentive(ByVal incentive As Integer) As List(Of SalaryTypeDTO)
        <OperationContract()>
        Function GetPaymentSourcesbyYear(ByVal year As Integer) As List(Of PaymentSourcesDTO)
        <OperationContract()>
        Function GetListOrgBonus() As List(Of OrgBonusDTO)
#End Region

#Region "Salary List"

        <OperationContract()>
        Function GetListSalaries(ByVal _filter As PAListSalariesDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "COL_INDEX ASC, CREATED_DATE desc") As List(Of PAListSalariesDTO)
        <OperationContract()>
        Function InsertPA_SAL_MAPPING(ByVal objListSal As PA_SALARY_FUND_MAPPINGDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function InsertListSalaries(ByVal objListSalaries As PAListSalariesDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyListSalaries(ByVal objListSalaries As PAListSalariesDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveListSalaries(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeleteListSalariesStatus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean
        <OperationContract()>
        Function DeleteListSalaries(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GetListSal(ByVal _filter As PAListSalDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "COL_INDEX ASC, CREATED_DATE desc") As List(Of PAListSalDTO)
        <OperationContract()>
        Function InsertListSal(ByVal objListSal As PAListSalDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyListSal(ByVal objListSalaries As PAListSalDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveListSal(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteListSal(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "lunch list : Đơn giá tiền ăn trưa"
        <OperationContract()>
        Function GetPriceLunchList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EFFECT_DATE desc") As List(Of ATPriceLunchDTO)
        <OperationContract()>
        Function GetPriceLunch(ByVal year As Decimal) As List(Of ATPriceLunchDTO)

        <OperationContract()>
        Function InsertPriceLunch(ByVal objPeriod As ATPriceLunchDTO, ByVal objOrgPeriod As List(Of PA_ORG_LUNCH), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyPriceLunch(ByVal objPeriod As ATPriceLunchDTO, ByVal objOrgPeriod As List(Of PA_ORG_LUNCH), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeletePriceLunch(ByVal lstPeriod As ATPriceLunchDTO) As Boolean
        <OperationContract()>
        Function ValidateATPriceLunch(ByVal _validate As ATPriceLunchDTO) As Boolean
        <OperationContract()>
        Function ValidateATPriceLunchOrg(ByVal _validate As ATPriceLunchDTO) As Boolean

#End Region


#Region "Cos Center"
        <OperationContract()>
        Function GetCostCenter(ByVal _filter As CostCenterDTO, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "ORDERS ASC, CREATED_DATE desc") As List(Of CostCenterDTO)
        <OperationContract()>
        Function InsertCostCenter(ByVal objCostCenter As CostCenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyCostCenter(ByVal objCostCenter As CostCenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateCostCenter(ByVal objCostCenter As CostCenterDTO) As Boolean
        <OperationContract()>
        Function ActiveCostCenter(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeleteCostCenterStatus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean
        <OperationContract()>
        Function DeleteCostCenter(ByVal lstID As List(Of Decimal)) As Boolean

#End Region

        <OperationContract()>
        Function TestService(ByVal str As String) As String

        <OperationContract()>
        Function GetComboboxData(ByRef cbxData As ComboBoxDataDTO) As Boolean

#Region "IPORTAL - View phiếu lương"
        <OperationContract()>
        Function GetPayrollSheetSum(ByVal PeriodId As Integer, ByVal EmployeeId As String, ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE DESC") As DataTable
        <OperationContract()>
        Function GetPayrollSheetSumSheet(ByVal PeriodId As Integer, ByVal EmployeeId As String, ByVal log As UserLog) As DataTable

        <OperationContract()>
        Function CHECK_OPEN_CLOSE(ByVal PeriodId As Integer, ByVal EmployeeId As String, ByVal log As UserLog) As DataTable

        <OperationContract()>
        Function CheckPeriod(ByVal PeriodId As Integer, ByVal EmployeeId As Decimal) As Boolean
#End Region

#Region " Org Bonus"
        <OperationContract()>
        Function GetOrgBonus(ByVal _filter As OrgBonusDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " ORDERS desc") As List(Of OrgBonusDTO)
        <OperationContract()>
        Function InsertOrgBonus(ByVal objTitle As OrgBonusDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyOrgBonus(ByVal objTitle As OrgBonusDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ActiveOrgBonus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeleteOrgBonus(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function ValidateOrgBonus(ByVal objTitle As OrgBonusDTO) As Boolean
#End Region
#Region " Payment Sources"
        <OperationContract()>
        Function GetPaymentSources(ByVal _filter As PaymentSourcesDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " ORDERS desc") As List(Of PaymentSourcesDTO)
        <OperationContract()>
        Function InsertPaymentSources(ByVal objTitle As PaymentSourcesDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyPaymentSources(ByVal objTitle As PaymentSourcesDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ActivePaymentSources(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeletePaymentSources(ByVal lstID As List(Of Decimal)) As Boolean

#End Region
#Region " Work Factor"
        <OperationContract()>
        Function GetWorkFactor(ByVal _filter As WorkFactorDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " ORDERS desc") As List(Of WorkFactorDTO)
        <OperationContract()>
        Function InsertWorkFactor(ByVal objTitle As WorkFactorDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyWorkFactor(ByVal objTitle As WorkFactorDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ActiveWorkFactor(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeleteWorkFactor(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function ValidateWorkFactor(ByVal objTitle As WorkFactorDTO) As Boolean
#End Region
#Region "SalaryFund List"

        <OperationContract()>
        Function GetSalaryFundByID(ByVal _filter As PASalaryFundDTO) As PASalaryFundDTO

        <OperationContract()>
        Function UpdateSalaryFund(ByVal objSalaryFund As PASalaryFundDTO,
                                    ByVal log As UserLog) As Boolean

#End Region

#Region "TitleCost List"

        <OperationContract()>
        Function GetTitleCost(ByVal _filter As PATitleCostDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PATitleCostDTO)

        <OperationContract()>
        Function InsertTitleCost(ByVal objTitleCost As PATitleCostDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyTitleCost(ByVal objTitleCost As PATitleCostDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteTitleCost(ByVal lstID As List(Of Decimal)) As Boolean
#End Region


#Region "SalaryPlanning"
        <OperationContract()>
        Function GetSalaryPlanning(ByVal _filter As PASalaryPlanningDTO,
                                   ByVal _param As ParamDTO,
                                   Optional ByVal log As UserLog = Nothing) As DataTable

        <OperationContract()>
        Function GetSalaryPlanningByID(ByVal _filter As PASalaryPlanningDTO) As PASalaryPlanningDTO

        <OperationContract()>
        Function InsertSalaryPlanning(ByVal objSalaryPlanning As PASalaryPlanningDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ImportSalaryPlanning(ByVal dtData As DataTable, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifySalaryPlanning(ByVal objSalaryPlanning As PASalaryPlanningDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteSalaryPlanning(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GetTitleByOrgList(ByVal orgID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetSalaryPlanningImport(ByVal org_id As Decimal, ByVal log As UserLog) As DataSet

#End Region

#Region "SalaryTracker"

        <OperationContract()>
        Function GetSalaryTracker(ByVal _filter As PASalaryPlanningDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByVal log As UserLog = Nothing) As DataSet

        <OperationContract()>
        Function GetSalaryEmpTracker(ByVal _filter As PASalaryPlanningDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByVal log As UserLog = Nothing) As DataSet

#End Region
#Region "Get List Manning"
        <OperationContract()>
        Function LoadComboboxListMannName(ByVal org_id As Integer, ByVal year As Integer) As DataTable
#End Region

#Region "Dashboard Payroll"
        <OperationContract()>
        Function GetStatisticSalary(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticSalaryRange(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticSalaryIncome(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticSalaryAverage(ByVal _year As Integer, ByVal _month As Integer, ByVal log As UserLog) As List(Of StatisticDTO)


#End Region

#Region "Dashboard Planning"
        <OperationContract()>
        Function GetStatisticSalaryTracker(ByVal _year As Integer, Optional ByVal log As UserLog = Nothing) As DataSet
        <OperationContract()>
        Function GetStatisticEmployeeTracker(ByVal _year As Integer, Optional ByVal log As UserLog = Nothing) As DataSet
#End Region

#Region "Validate Combobox"
        <OperationContract()>
        Function ValidateCombobox(ByVal cbxData As ComboBoxDataDTO) As Boolean
#End Region
#Region "SALE COMMISION"
        <OperationContract()>
        Function GetSaleCommision(ByVal _filter As SaleCommisionDTO, ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Total As Integer, Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SaleCommisionDTO)

        <OperationContract()>
        Function InsertSaleCommision(ByVal objSaleCommision As SaleCommisionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifySaleCommision(ByVal objSalaryType As SaleCommisionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteSaleCommision(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ActiveSaleCommision(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

#End Region

    End Interface
End Namespace



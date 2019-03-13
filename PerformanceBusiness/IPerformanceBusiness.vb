Imports PerformanceDAL
Imports Framework.Data
Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
Namespace PerformanceBusiness.ServiceContracts
    <ServiceContract()>
    Public Interface IPerformanceBusiness
        <OperationContract()>
        Function TestService(ByVal str As String) As String

        <OperationContract()>
        Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As PerformanceCommon.TABLE_NAME) As Boolean

#Region "Common"

        <OperationContract()>
        Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetPeriodList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetObjectGroupList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetObjectGroupByPeriodList(ByVal periodID As Decimal, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetCriteriaByObjectList(ByVal objectID As Decimal, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetListEmployeePortal(ByVal _filter As EmployeeDTO) As List(Of EmployeeDTO)
#End Region

#Region "List"

#Region "Criteria"
        <OperationContract()>
        Function GetCriteria(ByVal _filter As CriteriaDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaDTO)

        <OperationContract()>
        Function InsertCriteria(ByVal objCriteria As CriteriaDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateCriteria(ByVal objCriteria As CriteriaDTO) As Boolean

        <OperationContract()>
        Function ModifyCriteria(ByVal objCriteria As CriteriaDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCriteria(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteCriteria(ByVal lstCriteria() As Decimal) As Boolean
#End Region
#Region "Classification"
        <OperationContract()>
        Function GetClassification(ByVal _filter As ClassificationDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ClassificationDTO)

        <OperationContract()>
        Function InsertClassification(ByVal objClassification As ClassificationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateClassification(ByVal objClassification As ClassificationDTO) As Boolean

        <OperationContract()>
        Function ModifyClassification(ByVal objClassification As ClassificationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveClassification(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteClassification(ByVal lstClassification() As Decimal) As Boolean
#End Region
#Region "ObjectGroup"
        <OperationContract()>
        Function GetObjectGroup(ByVal _filter As ObjectGroupDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ObjectGroupDTO)

        <OperationContract()>
        Function InsertObjectGroup(ByVal objObjectGroup As ObjectGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateObjectGroup(ByVal objObjectGroup As ObjectGroupDTO) As Boolean

        <OperationContract()>
        Function ModifyObjectGroup(ByVal objObjectGroup As ObjectGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveObjectGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeleteObjectGroup(ByVal lstObjectGroup() As Decimal) As Boolean
#End Region

#Region "Period"
        <OperationContract()>
        Function GetPeriod(ByVal _filter As PeriodDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PeriodDTO)
        <OperationContract()>
        Function GetPeriodById(ByVal _filter As PeriodDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PeriodDTO)

        <OperationContract()>
        Function InsertPeriod(ByVal objPeriod As PeriodDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidatePeriod(ByVal objPeriod As PeriodDTO) As Boolean

        <OperationContract()>
        Function ModifyPeriod(ByVal objPeriod As PeriodDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActivePeriod(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean

        <OperationContract()>
        Function DeletePeriod(ByVal lstPeriod() As Decimal) As Boolean
#End Region

#End Region

#Region "Setting"

#Region "ObjectGroupPeriod"
        <OperationContract()>
        Function GetObjectGroupNotByPeriodID(ByVal _filter As ObjectGroupPeriodDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "OBJECT_GROUP_CODE") As List(Of ObjectGroupPeriodDTO)

        <OperationContract()>
        Function GetObjectGroupByPeriodID(ByVal _filter As ObjectGroupPeriodDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "OBJECT_GROUP_CODE") As  _
                                         List(Of ObjectGroupPeriodDTO)

        <OperationContract()>
        Function InsertObjectGroupByPeriod(ByVal lst As List(Of ObjectGroupPeriodDTO),
                                              ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteObjectGroupByPeriod(ByVal lst As List(Of Decimal),
                                   ByVal log As UserLog) As Boolean

#End Region

#Region "CriteriaObjectGroup"
        <OperationContract()>
        Function GetCriteriaNotByObjectGroupID(ByVal _filter As CriteriaObjectGroupDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "CRITERIA_CODE") As List(Of CriteriaObjectGroupDTO)

        <OperationContract()>
        Function GetCriteriaByObjectGroupID(ByVal _filter As CriteriaObjectGroupDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "CRITERIA_CODE") As  _
                                         List(Of CriteriaObjectGroupDTO)

        <OperationContract()>
        Function InsertCriteriaByObjectGroup(ByVal lst As List(Of CriteriaObjectGroupDTO),
                                              ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteCriteriaByObjectGroup(ByVal lst As List(Of Decimal),
                                   ByVal log As UserLog) As Boolean

#End Region

#Region "EmployeeAssessment"
        <OperationContract()>
        Function GetEmployeeNotByObjectGroupID(ByVal _filter As EmployeeAssessmentDTO,
                                                ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer,
                                                Optional ByVal Sorts As String = "Employee_CODE",
                                                Optional ByVal log As UserLog = Nothing) As List(Of EmployeeAssessmentDTO)

        <OperationContract()>
        Function GetEmployeeByObjectGroupID(ByVal _filter As EmployeeAssessmentDTO,
                                             ByVal PageIndex As Integer,
                                             ByVal PageSize As Integer,
                                             ByRef Total As Integer,
                                             Optional ByVal Sorts As String = "Employee_CODE") As  _
                                         List(Of EmployeeAssessmentDTO)

        <OperationContract()>
        Function InsertEmployeeByObjectGroup(ByVal lst As List(Of EmployeeAssessmentDTO),
                                              ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteEmployeeByObjectGroup(ByVal lst As List(Of Decimal),
                                   ByVal log As UserLog) As Boolean

#End Region

#End Region

#Region "Business"

#Region "PE_PERSONAL Quản lý dữ liệu đánh giá cá nhân"
        <OperationContract()>
        Function GetEmployeeImport(ByVal group_obj_id As Decimal, ByVal period_id As Decimal, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function GetCriteriaImport(ByVal group_obj_id As Decimal) As DataSet

        <OperationContract()>
        Function ImportEmployeeAssessment(ByVal dtData As DataTable,
                                          ByVal periodID As Decimal,
                                          ByVal group_obj_ID As Decimal,
                                          ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetEmployeeImportAssessment(ByVal _param As ParamDTO,
                                                ByVal obj As PE_EMPLOYEE_ASSESSMENT_DTLDTO,
                                                ByVal P_PAGE_INDEX As Decimal,
                                                ByVal P_PAGE_SIZE As Decimal,
                                                ByVal log As UserLog) As DataSet
#End Region

#Region "PE_ORGANIZATION Đánh giá đơn vị"
        <OperationContract()>
        Function GetPe_Organization(ByVal _filter As PE_ORGANIZATIONDTO,
                            ByVal _param As ParamDTO,
                            Optional ByVal PageIndex As Integer = 0,
                            Optional ByVal PageSize As Integer = Integer.MaxValue,
                            Optional ByRef Total As Integer = 0,
                            Optional ByVal Sorts As String = "CREATED_DATE desc",
                            Optional ByVal log As UserLog = Nothing) As List(Of PE_ORGANIZATIONDTO)
        <OperationContract()>
        Function GetPe_OrganizationByID(ByVal _filter As PE_ORGANIZATIONDTO) As PE_ORGANIZATIONDTO

        <OperationContract()>
        Function InsertPe_Organization(ByVal objData As PE_ORGANIZATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyPe_Organization(ByVal objData As PE_ORGANIZATIONDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeletePe_Organization(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Đánh giá cho từng nhóm đối tượng đánh giá"
        <OperationContract()>
        Function UpdateCriteriaObjectGroup(ByVal lst As List(Of CriteriaObjectGroupDTO),
                                   ByVal log As UserLog) As Boolean

#End Region

#Region "Assessment"
        <OperationContract()>
        Function GetAssessment(ByVal _filter As AssessmentDTO, ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer,
                                         Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentDTO)
        <OperationContract()>
        Function ModifyAssessment(ByVal objAssessment As AssessmentDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function UpdateStatusEmployeeAssessment(ByVal obj As AssessmentDTO,
                                                  ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetTotalPointRating(ByVal _filter As AssessmentDTO) As DataTable
#End Region
#End Region

#Region "Portal"
        <OperationContract()>
        Function GetAssessmentDirect(ByVal _empId As Decimal, ByVal _year As Decimal?, ByVal _status As Decimal?) As List(Of AssessmentDirectDTO)
        <OperationContract()>
        Function GET_DM_STATUS() As DataTable
        <OperationContract()>
        Function UpdateStatusAssessmentDirect(ByVal obj As Decimal,
                                                  ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetKPIAssessEmp(ByVal _empId As Decimal) As List(Of AssessmentDirectDTO)

        <OperationContract()>
        Function CHECK_APP(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable

        <OperationContract()>
        Function GET_PE_ASSESSMENT_HISTORY(ByVal _Id As Decimal) As DataTable

        <OperationContract()>
        Function CHECK_APP_1(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable

        <OperationContract()>
        Function CHECK_APP_2(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As DataTable


        <OperationContract()>
        Function INSERT_PE_ASSESSMENT_HISTORY(ByVal P_PE_PE_ASSESSMENT_ID As Decimal,
                                                 ByVal P_RESULT_DIRECT As String,
                                                 ByVal P_ASS_DATE As Date?,
                                                 ByVal P_REMARK_DIRECT As String,
                                                 ByVal P_CREATED_BY As String,
                                                 ByVal P_CREATED_LOG As String,
                                                 ByVal P_EMPLOYEE_ID As Decimal,
                                                 ByVal P_SIGN_ID As Decimal) As Boolean

        <OperationContract()>
        Function PRI_PERFORMACE_APP(ByVal _empId As Decimal, ByVal __PE_PERIOD_ID As Decimal) As Boolean

        <OperationContract()>
        Function PRI_PERFORMACE_PROCESS(ByVal P_EMPLOYEE_APP_ID As Decimal, ByVal P_EMPLOYEE_ID As Decimal,
                                           ByVal P_PE_PERIOD_ID As Decimal, ByVal P_STATUS_ID As Decimal,
                                           ByVal P_NOTES As String) As Boolean
#End Region

#Region "CBNS xem KPI cua nhan vien"
        <OperationContract()>
        Function GetDirectKPIEmployee(ByVal filter As AssessmentDirectDTO, ByVal PageIndex As Integer,
                                   ByVal PageSize As Integer,
                                   ByRef Total As Integer, ByVal _param As ParamDTO,
                                   Optional ByVal Sorts As String = "EMPLOYEE_ID desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AssessmentDirectDTO)

#End Region

#Region "CBNS xem KPI cua nhan vien"
        <OperationContract()>
        Function GetAssessRatingEmployee(ByVal filter As AssessRatingDTO) As List(Of AssessRatingDTO)
        <OperationContract()>
        Function GetAssessRatingEmployeeOrg(ByVal filter As AssessRatingDTO, Optional ByVal log As UserLog = Nothing) As List(Of AssessRatingDTO)
#End Region

#Region "Dashboard"

        <OperationContract()>
        Function GetStatisticKPIByClassification(ByVal _periodId As Integer, Optional ByVal log As UserLog = Nothing) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticKPIByClassifiOrg(ByVal _periodId As Integer, Optional ByVal log As UserLog = Nothing) As DataTable
        <OperationContract()>
        Function GetStatisticClassification(Optional ByVal Sorts As String = "CODE ASC") As List(Of ClassificationDTO)
        <OperationContract()>
        Function GetStatisticKPISeniority(ByVal _periodId As Integer, Optional ByVal log As UserLog = Nothing) As DataTable



#End Region

        <OperationContract()>
        Function PRINT_PE_ASSESS(ByVal empID As Decimal, ByVal period As Decimal, ByVal obj As Decimal) As DataSet

    End Interface
End Namespace

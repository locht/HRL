Imports TrainingDAL
Imports Framework.Data
Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
Namespace TrainingBusiness.ServiceContracts
    <ServiceContract()>
    Public Interface ITrainingBusiness
        <OperationContract()>
        Function TestService(ByVal str As String) As String

#Region "Hoadm - Common"

#Region "OtherList"
        <OperationContract()>
        Function GetCodeCourse(ByVal id As Decimal) As String
        <OperationContract()>
        Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetTrCertificateList(ByVal dGroupID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetTrCenterList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetFiedlTrainList() As List(Of LectureDTO)

        <OperationContract()>
        Function GetTrPlanByYearOrg(ByVal isBlank As Boolean, ByVal dYear As Decimal, ByVal dOrg As Decimal, ByVal log As UserLog) As DataTable

        <OperationContract()>
        Function GetTrPlanByYearOrg2(ByVal isBlank As Boolean, ByVal dYear As Decimal, ByVal dOrg As Decimal, ByVal log As UserLog, Optional ByVal isIrregularly As Boolean = False) As DataTable

        <OperationContract()>
        Function GetTrLectureList(ByVal isLocal As Boolean, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetHuProvinceList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetHuDistrictList(ByVal provinceID As Decimal, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetHuContractTypeList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetTrProgramByYear(ByVal isBlank As Boolean, ByVal dYear As Decimal) As DataTable

        <OperationContract()>
        Function GetTrCriteriaGroupList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetTrAssFormList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetTrChooseProgramFormByYear(ByVal isBlank As Boolean, ByVal dYear As Decimal) As DataTable

#End Region

#Region "CostCenter"
        <OperationContract()>
        Function GetCostCenter(ByVal _filter As CostCenterDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CostCenterDTO)

        <OperationContract()>
        Function InsertCostCenter(ByVal objCostCenter As CostCenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateCostCenter(ByVal objCostCenter As CostCenterDTO) As Boolean

        <OperationContract()>
        Function ModifyCostCenter(ByVal objCostCenter As CostCenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCostCenter(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean

        <OperationContract()>
        Function DeleteCostCenter(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#End Region

#Region "Hoadm - List"

#Region "Certificate"
        <OperationContract()>
        Function GetCertificate(ByVal _filter As CertificateDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CertificateDTO)

        <OperationContract()>
        Function InsertCertificate(ByVal objCertificate As CertificateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateCertificate(ByVal objCertificate As CertificateDTO) As Boolean

        <OperationContract()>
        Function ModifyCertificate(ByVal objCertificate As CertificateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCertificate(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean

        <OperationContract()>
        Function DeleteCertificate(ByVal lstCertificate() As CertificateDTO) As Boolean
#End Region

#Region "Course"
        <OperationContract()>
        Function GetCourse(ByVal _filter As CourseDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CourseDTO)

        <OperationContract()>
        Function InsertCourse(ByVal objCourse As CourseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateCourse(ByVal objCourse As CourseDTO) As Boolean

        <OperationContract()>
        Function ModifyCourse(ByVal objCourse As CourseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCourse(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean

        <OperationContract()>
        Function DeleteCourse(ByVal lstCourse() As CourseDTO) As Boolean
#End Region

#Region "Center"
        <OperationContract()>
        Function GetCenter(ByVal _filter As CenterDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CenterDTO)

        <OperationContract()>
        Function InsertCenter(ByVal objCenter As CenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateCenter(ByVal objCenter As CenterDTO) As Boolean

        <OperationContract()>
        Function ModifyCenter(ByVal objCenter As CenterDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCenter(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean

        <OperationContract()>
        Function DeleteCenter(ByVal lstCenter() As CenterDTO) As Boolean
#End Region

#Region "Lecture"
        <OperationContract()>
        Function GetLecture(ByVal _filter As LectureDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of LectureDTO)

        <OperationContract()>
        Function InsertLecture(ByVal objLecture As LectureDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateLecture(ByVal objLecture As LectureDTO) As Boolean

        <OperationContract()>
        Function ModifyLecture(ByVal objLecture As LectureDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveLecture(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean

        <OperationContract()>
        Function DeleteLecture(ByVal lstLecture() As LectureDTO) As Boolean
#End Region

#Region "CommitAfterTrain"
        <OperationContract()>
        Function GetCommitAfterTrain(ByVal _filter As CommitAfterTrainDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommitAfterTrainDTO)

        <OperationContract()>
        Function InsertCommitAfterTrain(ByVal objCommitAfterTrain As CommitAfterTrainDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyCommitAfterTrain(ByVal objCommitAfterTrain As CommitAfterTrainDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCommitAfterTrain(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean

        <OperationContract()>
        Function DeleteCommitAfterTrain(ByVal lstCommitAfterTrain() As CommitAfterTrainDTO) As Boolean
#End Region

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
        Function ActiveCriteria(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean

        <OperationContract()>
        Function DeleteCriteria(ByVal lstCriteria() As CriteriaDTO) As Boolean
#End Region

#Region "CriteriaGroup"
        <OperationContract()>
        Function GetCriteriaGroup(ByVal _filter As CriteriaGroupDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CriteriaGroupDTO)

        <OperationContract()>
        Function InsertCriteriaGroup(ByVal objCriteriaGroup As CriteriaGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateCriteriaGroup(ByVal objCriteriaGroup As CriteriaGroupDTO) As Boolean

        <OperationContract()>
        Function ModifyCriteriaGroup(ByVal objCriteriaGroup As CriteriaGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCriteriaGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean

        <OperationContract()>
        Function DeleteCriteriaGroup(ByVal lstCriteriaGroup() As CriteriaGroupDTO) As Boolean
#End Region

#Region "AssessmentRate"
        <OperationContract()>
        Function GetAssessmentRate(ByVal _filter As AssessmentRateDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentRateDTO)

        <OperationContract()>
        Function InsertAssessmentRate(ByVal objAssessmentRate As AssessmentRateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyAssessmentRate(ByVal objAssessmentRate As AssessmentRateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAssessmentRate(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Boolean) As Boolean

        <OperationContract()>
        Function DeleteAssessmentRate(ByVal lstAssessmentRate() As AssessmentRateDTO) As Boolean
#End Region

#Region "AssessmentForm"
        <OperationContract()>
        Function GetAssessmentForm(ByVal _filter As AssessmentFormDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentFormDTO)

        <OperationContract()>
        Function InsertAssessmentForm(ByVal objAssessmentForm As AssessmentFormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyAssessmentForm(ByVal objAssessmentForm As AssessmentFormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteAssessmentForm(ByVal lstAssessmentForm() As AssessmentFormDTO) As Boolean
        <OperationContract()>
        Function GetTrRateCombo(ByVal isBlank As Boolean) As DataTable

#End Region

#End Region

#Region "Hoadm - Setting"

#Region "SettingAssFormDTO"

        <OperationContract()>
        Function GetCriteriaGroupNotByFormID(ByVal _filter As SettingAssFormDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_GROUP_CODE desc") As List(Of SettingAssFormDTO)

        <OperationContract()>
        Function GetCriteriaGroupByFormID(ByVal _filter As SettingAssFormDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_GROUP_CODE desc") As List(Of SettingAssFormDTO)

        <OperationContract()>
        Function InsertSettingAssForm(ByVal lst As List(Of SettingAssFormDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteSettingAssForm(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean


#End Region

#Region "SettingCriteriaGroupDTO"

        <OperationContract()>
        Function GetCriteriaNotByGroupID(ByVal _filter As SettingCriteriaGroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_CODE desc") As List(Of SettingCriteriaGroupDTO)

        <OperationContract()>
        Function GetCriteriaByGroupID(ByVal _filter As SettingCriteriaGroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "TR_CRITERIA_CODE desc") As List(Of SettingCriteriaGroupDTO)

        <OperationContract()>
        Function InsertSettingCriteriaGroup(ByVal lst As List(Of SettingCriteriaGroupDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteSettingCriteriaGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean


#End Region

#End Region

#Region "Hoadm - Business"

#Region "Otherlist"

        <OperationContract()>
        Function GetCourseList() As List(Of CourseDTO)

        <OperationContract()>
        Function GetIDCourseList(ByVal idSelected As String) As List(Of CourseDTO)

        <OperationContract()>
        Function GetTitlesByOrgs(ByVal orgIds As List(Of Decimal), ByVal langCode As String) As List(Of PlanTitleDTO)

        <OperationContract()>
        Function GetWIByTitle(ByVal orgIds As List(Of Decimal), ByVal langCode As String) As List(Of PlanTitleDTO)
        <OperationContract()>
        Function GetEntryAndFormByCourseID(ByVal CourseId As Decimal, ByVal langCode As String) As CourseDTO

        <OperationContract()>
        Function GetCenters() As List(Of CenterDTO)

#End Region

#Region "Plan"
        <OperationContract()>
        Function GetPlans(ByVal filter As PlanDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of PlanDTO)

        <OperationContract()>
        Function GetPlanById(ByVal Id As Decimal) As PlanDTO

        <OperationContract()>
        Function InsertPlan(ByVal plan As PlanDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyPlan(ByVal plan As PlanDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeletePlans(ByVal lstId As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function ApproveListPlan(ByVal listID As List(Of Decimal), ByVal flag As Decimal, ByVal log As UserLog) As Boolean
#End Region

#Region "Request"
        <OperationContract()>
        Function GetTrainingRequests(ByVal filter As RequestDTO,
                                         ByVal PageIndex As Integer, ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of RequestDTO)

        <OperationContract()>
        Function GetTrainingRequestsByID(ByVal filter As RequestDTO) As RequestDTO


        <OperationContract()>
        Function GetEmployeeByImportRequest(ByRef lstEmpCode As List(Of RequestEmpDTO)) As String


        <OperationContract()>
        Function GetEmployeeByPlanID(ByVal filter As RequestDTO) As List(Of RequestEmpDTO)


        <OperationContract()>
        Function InsertRequest(ByVal Request As RequestDTO,
                                  ByVal lstEmp As List(Of RequestEmpDTO),
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyRequest(ByVal Request As RequestDTO,
                                  ByVal lstEmp As List(Of RequestEmpDTO),
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean


        <OperationContract()>
        Function UpdateStatusTrainingRequests(ByVal lstID As List(Of Decimal), ByVal status As Decimal) As Boolean

        <OperationContract()>
        Function DeleteTrainingRequests(ByVal lstRequestID As List(Of Decimal)) As Boolean


        <OperationContract()>
        Function GetPlanRequestByID(ByVal Id As Decimal) As PlanDTO

#End Region

#Region "Program"
        <OperationContract()>
        Function GetRequestsForProgram(ByVal ReqID As Decimal) As RequestDTO
        <OperationContract()>
        Function GetPrograms(ByVal filter As ProgramDTO, ByVal PageIndex As Integer,
                             ByVal PageSize As Integer, ByRef Total As Integer, ByVal _param As ParamDTO,
                             Optional ByVal Sorts As String = "CREATED_DATE desc",
                             Optional ByVal log As UserLog = Nothing) As List(Of ProgramDTO)

        <OperationContract()>
        Function GetPlan_Cost_Detail(ByVal Id As Decimal) As List(Of CostDetailDTO)
        <OperationContract()>
        Function GetProgramById(ByVal Id As Decimal) As ProgramDTO
        <OperationContract()>
        Function GetProgramByChooseFormId(ByVal Id As Decimal) As ProgramDTO

        <OperationContract()>
        Function InsertProgram(ByVal Program As ProgramDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyProgram(ByVal Program As ProgramDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeletePrograms(ByVal lstId As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ValidateClassProgram(ByVal lstId As List(Of Decimal)) As Boolean

#End Region

#Region "Prepare"

        <OperationContract()>
        Function GetPrepare(ByVal _filter As ProgramPrepareDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramPrepareDTO)

        <OperationContract()>
        Function InsertPrepare(ByVal objPrepare As ProgramPrepareDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyPrepare(ByVal objPrepare As ProgramPrepareDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeletePrepare(ByVal lstPrepare() As ProgramPrepareDTO) As Boolean

#End Region

#Region "Class"

        <OperationContract()>
        Function GetClass(ByVal _filter As ProgramClassDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramClassDTO)

        <OperationContract()>
        Function GetClassByID(ByVal _filter As ProgramClassDTO) As ProgramClassDTO

        <OperationContract()>
        Function InsertClass(ByVal objClass As ProgramClassDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyClass(ByVal objClass As ProgramClassDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteClass(ByVal lstClass() As ProgramClassDTO) As Boolean

#End Region

#Region "Class Student"

        <OperationContract()>
        Function GetEmployeeNotByClassID(ByVal _filter As ProgramClassStudentDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramClassStudentDTO)

        <OperationContract()>
        Function GetEmployeeByClassID(ByVal _filter As ProgramClassStudentDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramClassStudentDTO)

        <OperationContract()>
        Function InsertClassStudent(ByVal lst As List(Of ProgramClassStudentDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteClassStudent(ByVal lst As List(Of ProgramClassStudentDTO), ByVal log As UserLog) As Boolean


#End Region

#Region "ClassSchedule"

        <OperationContract()>
        Function GetClassSchedule(ByVal _filter As ProgramClassScheduleDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramClassScheduleDTO)

        <OperationContract()>
        Function GetClassScheduleByID(ByVal _filter As ProgramClassScheduleDTO) As ProgramClassScheduleDTO

        <OperationContract()>
        Function InsertClassSchedule(ByVal objClassSchedule As ProgramClassScheduleDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyClassSchedule(ByVal objClassSchedule As ProgramClassScheduleDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteClassSchedule(ByVal lstClassSchedule() As ProgramClassScheduleDTO) As Boolean

#End Region

#Region "ProgramResult"
        <OperationContract()>
        Function GetProgramResult(ByVal _filter As ProgramResultDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramResultDTO)


        <OperationContract()>
        Function UpdateProgramResult(ByVal lst As List(Of ProgramResultDTO),
                                   ByVal log As UserLog) As Boolean


#End Region

#Region "ProgramCommit"
        <OperationContract()>
        Function GetProgramCommit(ByVal _filter As ProgramCommitDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of ProgramCommitDTO)


        <OperationContract()>
        Function UpdateProgramCommit(ByVal lst As List(Of ProgramCommitDTO),
                                   ByVal log As UserLog) As Boolean


#End Region

#Region "ProgramCost"
        <OperationContract()>
        Function GetProgramCost(ByVal _filter As ProgramCostDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramCostDTO)

        <OperationContract()>
        Function InsertProgramCost(ByVal objProgramCost As ProgramCostDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateProgramCost(ByVal objProgramCost As ProgramCostDTO) As Boolean

        <OperationContract()>
        Function ModifyProgramCost(ByVal objProgramCost As ProgramCostDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteProgramCost(ByVal lstProgramCost() As ProgramCostDTO) As Boolean
#End Region

#Region "Reimbursement"
        <OperationContract()>
        Function GetReimbursement(ByVal _filter As ReimbursementDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ReimbursementDTO)

        <OperationContract()>
        Function InsertReimbursement(ByVal objReimbursement As ReimbursementDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateReimbursement(ByVal objReimbursement As ReimbursementDTO) As Boolean

        <OperationContract()>
        Function ModifyReimbursement(ByVal objReimbursement As ReimbursementDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteReimbursement(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region

#Region "ChooseForm"
        <OperationContract()>
        Function GetChooseForm(ByVal _filter As ChooseFormDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ChooseFormDTO)

        <OperationContract()>
        Function InsertChooseForm(ByVal objChooseForm As ChooseFormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateChooseForm(ByVal objChooseForm As ChooseFormDTO) As Boolean

        <OperationContract()>
        Function ModifyChooseForm(ByVal objChooseForm As ChooseFormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteChooseForm(ByVal lst() As ChooseFormDTO) As Boolean

#End Region


#Region "AssessmentResult"
        <OperationContract()>
        Function GetEmployeeAssessmentResult(ByVal _filter As AssessmentResultDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc") As List(Of AssessmentResultDTO)

        <OperationContract()>
        Function GetAssessmentResultByID(ByVal _filter As AssessmentResultDtlDTO) As List(Of AssessmentResultDtlDTO)

        <OperationContract()>
        Function UpdateAssessmentResult(ByVal obj As AssessmentResultDTO,
                                   ByVal log As UserLog) As Boolean

#End Region

#Region "TranningRecord"
        <OperationContract()>
        Function GetListEmployeePaging(ByVal _filter As RecordEmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of RecordEmployeeDTO)

        <OperationContract()>
        Function GetEmployeeRecord(ByVal _filter As RecordEmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of RecordEmployeeDTO)
#End Region


#End Region

        <OperationContract()>
        Function test(ByVal a As CostDetailDTO) As CostDetailDTO
#Region "Setting Title Course"
        <OperationContract()>
        Function GetTitleCourse(ByVal _filter As TitleCourseDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE") As List(Of TitleCourseDTO)

        <OperationContract()>
        Function UpdateTitleCourse(ByVal objExams As TitleCourseDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteTitleCourse(ByVal obj As TitleCourseDTO) As Boolean

#End Region

#Region "Get List"
        <OperationContract()>
        Function GetTitleByList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetCourseByList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
#End Region

#Region "Employee Title Course"
        <OperationContract()>
        Function GetEmployeeTitleCourse(ByVal filter As EmployeeTitleCourseDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "HU_EMPLOYEE_ID desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of EmployeeTitleCourseDTO)
#End Region

#Region "BAO CAO"
        <OperationContract()>
        Function ExportReport(ByVal sPkgName As String,
                              ByVal sStartDate As Date?,
                              ByVal sEndDate As Date?,
                              ByVal sOrg As String,
                              ByVal IsDissolve As Integer,
                              ByVal sUserName As String, ByVal sLang As String) As DataSet

        <OperationContract()>
        Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO)

#End Region

#Region "Dashboard"
        <OperationContract()>
        Function GetStatisticCourse(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticFormCost(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticDiligence(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticRank(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)


#End Region

        <OperationContract()>
        Function GetAssessmentCourse(ByVal _filter As AssessmentCourseDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssessmentCourseDTO)

        <OperationContract()>
        Function InsertAssessmentCourse(ByVal objAssessmentForm As AssessmentCourseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyAssessmentCourse(ByVal objAssessmentForm As AssessmentCourseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteAssessmentCourse(ByVal lstAssessmentForm() As AssessmentCourseDTO) As Boolean

        <OperationContract()>
        Function GET_TR_COURSE() As DataTable

        <OperationContract()>
        Function GET_TR_ASSESSMENT_FORM() As DataTable
    End Interface
End Namespace

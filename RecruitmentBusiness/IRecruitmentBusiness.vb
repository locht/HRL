Imports RecruitmentDAL
Imports Framework.Data
Imports Framework.Data.SystemConfig
Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
Namespace RecruitmentBusiness.ServiceContracts
    <ServiceContract()>
    Public Interface IRecruitmentBusiness

        <OperationContract()>
        Function TestService(ByVal str As String) As String
        <OperationContract()>
        Function ImportRC(ByVal Data As DataTable, ByVal ProGramID As Decimal, Optional ByVal log As UserLog = Nothing) As Boolean
#Region "danh muc phuong xa"
        <OperationContract()>
        Function GetWardList(ByVal districtID As Decimal, ByVal isBlank As Boolean) As DataTable
#End Region
#Region "Hoadm - List"

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

#Region "ExamsDtl"
        <OperationContract()>
        Function GetExamsDtl(ByVal _filter As ExamsDtlDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EXAMS_ORDER") As List(Of ExamsDtlDTO)

        <OperationContract()>
        Function UpdateExamsDtl(ByVal objExams As ExamsDtlDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteExamsDtl(ByVal obj As ExamsDtlDTO) As Boolean

#End Region

#End Region

#Region "Hoadm - Business"

#Region "PlanReg"
        <OperationContract()>
        Function GetPlanReg(ByVal _filter As PlanRegDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal isSearch As Boolean = False,
                                        Optional ByVal log As UserLog = Nothing) As List(Of PlanRegDTO)

        <OperationContract()>
        Function GetPlanRegByID(ByVal _filter As PlanRegDTO) As PlanRegDTO

        <OperationContract()>
        Function InsertPlanReg(ByVal objPlanReg As PlanRegDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyPlanReg(ByVal objPlanReg As PlanRegDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeletePlanReg(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function UpdateStatusPlanReg(ByVal lstID As List(Of Decimal), ByVal status As Decimal) As Boolean

#End Region

#Region "PlanYear"
        <OperationContract()>
        Function GetPlanYear(ByVal _filter As PlanYearDTO,
                                        ByVal pageIndex As Integer,
                                        ByVal pageSize As Integer,
                                        ByRef total As Integer,
                                        ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal isSearch As Boolean = False,
                                        Optional ByVal log As UserLog = Nothing) As List(Of PlanYearDTO)
#End Region


#Region "PlanSummary"

        <OperationContract()>
        Function GetPlanSummary(ByVal _year As Decimal, ByVal _param As ParamDTO, ByVal log As UserLog) As DataTable


#End Region

#Region "Request"
        <OperationContract()>
        Function GetRequest(ByVal _filter As RequestDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of RequestDTO)

        <OperationContract()>
        Function GetRequestByID(ByVal _filter As RequestDTO) As RequestDTO

        <OperationContract()>
        Function InsertRequest(ByVal objRequest As RequestDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyRequest(ByVal objRequest As RequestDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteRequest(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function UpdateStatusRequest(ByVal lstID As List(Of Decimal), ByVal status As Decimal, ByVal log As UserLog) As Boolean

#End Region

#Region "Program"
        <OperationContract()>
        Function GetProgram(ByVal _filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ProgramDTO)

        <OperationContract()>
        Function GetProgramSearch(ByVal _filter As ProgramDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of ProgramDTO)

        <OperationContract()>
        Function GetProgramByID(ByVal _filter As ProgramDTO) As ProgramDTO

        <OperationContract()>
        Function ModifyProgram(ByVal objProgram As ProgramDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function XuatToTrinh(ByVal sID As Decimal) As DataTable

#Region "ProgramExams"
        <OperationContract()>
        Function GetProgramExams(ByVal _filter As ProgramExamsDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EXAMS_ORDER") As List(Of ProgramExamsDTO)
        <OperationContract()>
        Function GetProgramExamsByID(ByVal _filter As ProgramExamsDTO) As ProgramExamsDTO

        <OperationContract()>
        Function UpdateProgramExams(ByVal objExams As ProgramExamsDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteProgramExams(ByVal obj As ProgramExamsDTO) As Boolean

#End Region

#End Region

#Region "Candidate"

        ''' <summary>
        ''' Kiểm tra ứng viên đó có trong hệ thống ko(trừ ứng viên nghỉ việc)
        ''' </summary>
        ''' <param name="strEmpCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function CheckExistCandidate(ByVal strEmpCode As String) As Boolean

        <OperationContract()>
        Function ValidateInsertCandidate(ByVal sEmpId As String, ByVal sID_No As String, ByVal sFullName As String, ByVal dBirthDate As Date, ByVal sType As String) As Boolean

        <OperationContract()>
        Function GetCandidateFamily_ByID(ByVal sCandidateID As Decimal) As CandidateFamilyDTO

        ''' <summary>
        ''' Trả về binary của ảnh hồ sơ ứng viên
        ''' </summary>
        ''' <param name="gEmpID"></param>
        ''' <param name="sError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetCandidateImage(ByVal gEmpID As Decimal, ByRef sError As String) As Byte()

        ''' <summary>
        ''' Thêm mới ứng viên
        ''' </summary>
        ''' <param name="objEmp"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <param name="_strEmpCode"></param>
        ''' <param name="_imageBinary"></param>
        ''' <param name="objEmpCV"></param>
        ''' <param name="objEmpEdu"></param>
        ''' <param name="objEmpOther"></param>
        ''' <param name="objEmpHis"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertCandidate(ByVal objEmp As CandidateDTO, ByVal log As UserLog, ByRef gID As Decimal, _
                                  ByRef _strEmpCode As String, _
                                  ByVal _imageBinary As Byte(),
                                  ByVal objEmpCV As CandidateCVDTO, _
                                  ByVal objEmpEdu As CandidateEduDTO, _
                                   ByVal objEmpOther As CandidateOtherInfoDTO, _
                                         ByVal objEmpHealth As CandidateHealthDTO, _
                                         ByVal objEmpExpect As CandidateExpectDTO, _
                                         ByVal objEmpFamily As CandidateFamilyDTO) As Boolean

        <OperationContract()>
        Function CreateNewCandidateCode() As CandidateDTO


        ''' <summary>
        ''' Sửa thông tin ứng viên
        ''' </summary>
        ''' <param name="objEmp"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <param name="_imageBinary"></param>
        ''' <param name="objEmpCV"></param>
        ''' <param name="objEmpEdu"></param>
        ''' <param name="objEmpOther"></param>
        ''' <param name="objEmpHis"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyCandidate(ByVal objEmp As CandidateDTO, ByVal log As UserLog, ByRef gID As Decimal, _
                                  ByVal _imageBinary As Byte(), _
                                   ByVal objEmpCV As CandidateCVDTO,
                                         ByVal objEmpEdu As CandidateEduDTO, _
                                   ByVal objEmpOther As CandidateOtherInfoDTO, _
                                         ByVal objEmpHealth As CandidateHealthDTO, _
                                         ByVal objEmpExpect As CandidateExpectDTO, _
                                         ByVal objEmpFamily As CandidateFamilyDTO) As Boolean


        ''' <summary>
        ''' Lấy danh sách ứng viên có phân trang
        ''' </summary>
        ''' <param name="PageIndex"></param>
        ''' <param name="PageSize"></param>
        ''' <param name="Total"></param>
        ''' <param name="_filter"></param>
        ''' <param name="Sorts"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetListCandidatePaging(ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       ByVal _filter As CandidateDTO,
                                       Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO)

        <OperationContract()>
        Function GetFindCandidatePaging(ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       ByVal _filter As CandidateDTO,
                                       Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO)

        <OperationContract()>
        Function GetListCandidateTransferPaging(ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       ByVal _filter As CandidateDTO,
                                       Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO)



        <OperationContract()>
        Function GetListCandidate(ByVal _filter As CandidateDTO,
                                       Optional ByVal Sorts As String = "Candidate_CODE desc") As List(Of CandidateDTO)

        ''' <summary>
        ''' Lay thong tin nhan vien tu CandidateCode
        ''' </summary>
        ''' <param name="sCandidateCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetCandidateInfo(ByVal sCandidateCode As String) As CandidateDTO


        ''' <summary>
        ''' Lấy thông tin CandidateCV 
        ''' </summary>
        ''' <param name="sCandidateID">ID(Decimal) của ứng viên</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetCandidateCV(ByVal sCandidateID As Decimal) As CandidateCVDTO

        <OperationContract()>
        Function GetCandidateEdu(ByVal sCandidateID As Decimal) As CandidateEduDTO
        ''' <summary>
        ''' Lấy thông tin khác
        ''' </summary>
        ''' <param name="sCandidateID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetCandidateOtherInfo(ByVal sCandidateID As Decimal) As CandidateOtherInfoDTO

        ''' <summary>
        ''' Xóa ứng viên
        ''' </summary>
        ''' <param name="lstEmpID"></param>
        ''' <param name="log"></param>
        ''' <param name="sError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetCandidateHealthInfo(ByVal sCandidateID As Decimal) As CandidateHealthDTO

        ''' <summary>
        ''' Xóa ứng viên
        ''' </summary>
        ''' <param name="lstEmpID"></param>
        ''' <param name="log"></param>
        ''' <param name="sError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetCandidateExpectInfo(ByVal sCandidateID As Decimal) As CandidateExpectDTO

        <OperationContract()>
        Function GetCandidateHistory(ByVal sCandidateID As Decimal, ByVal sCandidateIDNO As Decimal) As List(Of CandidateHistoryDTO)

        ''' <summary>
        ''' Xóa ứng viên
        ''' </summary>
        ''' <param name="lstEmpID"></param>
        ''' <param name="log"></param>
        ''' <param name="sError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteCandidate(ByVal lstEmpID As List(Of Decimal), ByVal log As UserLog, ByRef sError As String) As Boolean

        <OperationContract()>
        Function UpdateProgramCandidate(ByVal lstCanID As List(Of Decimal), ByVal programID As Decimal, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function UpdateStatusCandidate(ByVal lstCanID As List(Of Decimal), ByVal statusID As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function UpdatePontentialCandidate(ByVal lstCanID As List(Of Decimal), ByVal bCheck As Boolean, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function UpdateBlackListCandidate(ByVal lstCanID As List(Of Decimal), ByVal bCheck As Boolean, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function UpdateReHireCandidate(ByVal lstCanID As List(Of Decimal), ByVal bCheck As Boolean, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetCandidateImport() As DataSet


        <OperationContract()>
        Function ImportCandidate(ByVal lst As List(Of CandidateImportDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function TransferHSNVToCandidate(ByVal empID As Decimal,
                                            ByVal orgID As Decimal,
                                            ByVal titleID As Decimal,
                                            ByVal programID As Decimal,
                                            ByVal log As UserLog) As Boolean

#Region "CandidateFamily"
        ''' <summary>
        ''' Lay danh sach than nhan
        ''' </summary>
        ''' <param name="_filter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetCandidateFamily(ByVal _filter As CandidateFamilyDTO) As List(Of CandidateFamilyDTO)

        ''' <summary>
        ''' Them moi nhan than
        ''' </summary>
        ''' <param name="objFamily"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertCandidateFamily(ByVal objFamily As CandidateFamilyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Chinh sua thong tin nhan than
        ''' </summary>
        ''' <param name="objFamily"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyCandidateFamily(ByVal objFamily As CandidateFamilyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Xóa nhân thân
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteCandidateFamily(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean


        ''' <summary>
        ''' Check trùng CMND của nhân thân.
        ''' </summary>
        ''' <param name="_validate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ValidateFamily(ByVal _validate As CandidateFamilyDTO) As Boolean
#End Region

#Region "Cá nhân tự đào tạo"
        ''' <summary>
        ''' Lay danh sach ngan hang
        ''' </summary>
        ''' <param name="_filter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetCandidateTrainSinger(ByVal _filter As TrainSingerDTO) As List(Of TrainSingerDTO)

        ''' <summary>
        ''' Them moi ngan hang
        ''' </summary>
        ''' <param name="objTrainSinger"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertCandidateTrainSinger(ByVal objTrainSinger As TrainSingerDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Chinh sua thong tin ngan hang
        ''' </summary>
        ''' <param name="objTrainSinger"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyCandidateTrainSinger(ByVal objTrainSinger As TrainSingerDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Xóa ngan hang
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteCandidateTrainSinger(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean


#End Region

#Region "Người tham chiếu"
        ''' <summary>
        ''' Lay danh sach ngan hang
        ''' </summary>
        ''' <param name="_filter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetCandidateReference(ByVal _filter As CandidateReferenceDTO) As List(Of CandidateReferenceDTO)

        ''' <summary>
        ''' Them moi ngan hang
        ''' </summary>
        ''' <param name="objTrainSinger"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertCandidateReference(ByVal objTrainSinger As CandidateReferenceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Chinh sua thong tin ngan hang
        ''' </summary>
        ''' <param name="objTrainSinger"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyCandidateReference(ByVal objTrainSinger As CandidateReferenceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Xóa ngan hang
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteCandidateReference(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean


#End Region
#Region "Trước khi vào MLG"
        ''' <summary>
        ''' Lay danh sach ngan hang
        ''' </summary>
        ''' <param name="_filter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetCandidateBeforeWT(ByVal _filter As CandidateBeforeWTDTO) As List(Of CandidateBeforeWTDTO)

        ''' <summary>
        ''' Them moi ngan hang
        ''' </summary>
        ''' <param name="objCandidateBeforeWT"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertCandidateBeforeWT(ByVal objCandidateBeforeWT As CandidateBeforeWTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Chinh sua thong tin ngan hang
        ''' </summary>
        ''' <param name="objCandidateBeforeWT"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyCandidateBeforeWT(ByVal objCandidateBeforeWT As CandidateBeforeWTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Xóa ngan hang
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteCandidateBeforeWT(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean


#End Region

#End Region

#Region "ProgramSchedule"
        <OperationContract()>
        Function GetProgramSchedule(ByVal _filter As ProgramScheduleDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProgramScheduleDTO)

        <OperationContract()>
        Function GetProgramScheduleByID(ByVal _filter As ProgramScheduleDTO) As ProgramScheduleDTO

        <OperationContract()>
        Function GetCandidateNotScheduleByScheduleID(ByVal _filter As ProgramScheduleCanDTO) As List(Of ProgramScheduleCanDTO)

        <OperationContract()>
        Function GetCandidateScheduleByScheduleID(ByVal _filter As ProgramScheduleCanDTO) As List(Of ProgramScheduleCanDTO)

        <OperationContract()>
        Function UpdateProgramSchedule(ByVal objExams As ProgramScheduleDTO, ByVal log As UserLog) As Boolean


#End Region

#Region "CandidateResult"
        <OperationContract()>
        Function GetCandidateResult(ByVal _filter As ProgramScheduleCanDTO) As List(Of ProgramScheduleCanDTO)


        <OperationContract()>
        Function UpdateCandidateResult(ByVal lst As List(Of ProgramScheduleCanDTO), ByVal log As UserLog) As Boolean

#End Region

#Region "Cost"
        <OperationContract()>
        Function GetCost(ByVal _filter As CostDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CostDTO)

        <OperationContract()>
        Function UpdateCost(ByVal objExams As CostDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ValidateCost(ByVal objCostCenter As CostDTO) As Boolean

        <OperationContract()>
        Function DeleteCost(ByVal obj As CostDTO) As Boolean

#End Region

#End Region

#Region "Hoadm - Common"
        <OperationContract()>
        Function GetComboList(ByRef _combolistDTO As ComboBoxDataDTO) As Boolean

        <OperationContract()>
        Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetProvinceList(ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetContractTypeList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetTitleByOrgList(ByVal orgID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetTitleByOrgListInPlan(ByVal orgID As Decimal, ByVal _year As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetProgramExamsList(ByVal programID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetProgramList(ByVal orgID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

#End Region

#Region "CV Pool"
        <OperationContract()>
        Function GetCVPoolEmpRecord(ByVal _filter As CVPoolEmpDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of CVPoolEmpDTO)
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

#Region "DashBoard"
        <OperationContract()>
        Function GetStatisticGender(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticEduacation(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticCanToEmp(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticEstimateReality(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
#End Region

    End Interface
End Namespace

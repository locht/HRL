' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
Imports ProfileDAL
Imports Framework.Data

Namespace ProfileBusiness.ServiceContracts

    <ServiceContract()>
    Public Interface IProfileBusiness

#Region " hu_certificateedit"
        <OperationContract()>
        Function GetCertificateEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)
        <OperationContract()>
        Function InsertCertificateEdit(ByVal objCertificateEdit As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                            ByVal log As UserLog,
                                            ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyCertificateEdit(ByVal objCertificateEdit As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                            ByVal log As UserLog,
                                            ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function CheckExistCertificateEdit(ByVal pk_key As Decimal) As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT
        <OperationContract()>
        Function SendCertificateEdit(ByVal lstID As List(Of Decimal),
                                          ByVal log As UserLog) As Boolean
#End Region
#Region "hu_certificate"
        <OperationContract()>
        Function GetCertificate(ByVal _filter As CETIFICATEDTO) As List(Of CETIFICATEDTO)
#End Region
#Region "Contract appendix"
        <OperationContract()>
        Function GET_NEXT_APPENDIX_ORDER(ByVal id As Decimal, ByVal contract_id As Decimal, ByVal emp_id As Decimal) As Integer
#End Region
        <OperationContract()>
        Function GetMaxId() As Decimal
        <OperationContract()>
        Function GetNameOrg(ByVal org_id As Decimal) As String
        <OperationContract()>
        Function GetOrgsTree() As List(Of OrganizationDTO)

#Region "ngach, bac, thang luong"
        <OperationContract()>
        Function GetSalaryGroupCombo(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetSalaryRankCombo(ByVal SalaryLevel As Decimal, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetSalaryLevelCombo(ByVal SalaryGroup As Decimal, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetSalaryLevelComboNotByGroup(ByVal isBlank As Boolean) As DataTable
#End Region
        <OperationContract()>
        Function Calculator_Salary(ByVal data_in As String) As DataTable
        <OperationContract()>
        Function GetHoSoLuongImport() As DataSet
#Region "Location"
        <OperationContract()>
        Function GetLocationID(ByVal ID As Decimal) As LocationDTO

        <OperationContract()>
        Function GetLocation(ByVal sACT As String, ByVal lstOrgID As List(Of Decimal)) As List(Of LocationDTO)

        <OperationContract()>
        Function InsertLocation(ByVal objLocation As LocationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyLocation(ByVal objLocation As LocationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveLocation(ByVal lstLocation As List(Of LocationDTO), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ActiveLocationID(ByVal lstLocation As LocationDTO, ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteLocationID(ByVal lstlocation As Decimal, ByVal log As UserLog) As Boolean
        'xóa nv trong black list
        <OperationContract()>
        Function DeleteNVBlackList(ByVal id_no As String, ByVal log As UserLog) As Boolean
#End Region

#Region "Hoadm - Common"

#Region "OtherList"

        <OperationContract()>
        Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetOtherListAll(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function HU_PAPER_LIST(ByVal P_EMP_ID As Decimal, ByVal sLang As String) As DataTable

        <OperationContract()>
        Function GetBankList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetBankBranchList(ByVal bankID As Decimal, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetTitleByOrgID(ByVal orgID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetJobDescByTitleID(ByVal titleID As Decimal, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetTitleList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetWardList(ByVal districtID As Decimal, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetDistrictList(ByVal provinceID As Decimal, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetProvinceList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetProvinceList1(ByVal P_NATIVE As Decimal, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetNationList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetStaffRankList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetSalaryGroupList(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetSaleCommisionList(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        '<OperationContract()>
        'Function GetSalaryTypes(ByVal query As PA_SALARY_TYPEQuery) As PA_SALARY_TYPEResult

        <OperationContract()>
        Function GetSalaryTypeList(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        <OperationContract()>
        Function GetSalaryLevelList(ByVal salGroupID As Decimal, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetSalaryRankList(ByVal salLevelID As Decimal, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetHU_AllowanceList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetPA_ObjectSalary(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetOT_WageTypeList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetOT_MissionTypeList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetOT_TripartiteTypeList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetHU_TemplateType(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetHU_MergeFieldList(ByVal isBlank As Boolean,
                                          ByVal isTemplateType As Decimal) As DataTable

        <OperationContract()>
        Function GetHU_TemplateList(ByVal isBlank As Boolean,
                                    ByVal isTemplateType As Decimal) As DataTable

        <OperationContract()>
        Function GetHU_DataDynamic(ByVal dID As Decimal,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String) As DataTable

        <OperationContract()>
        Function GetHU_MultyDataDynamic(ByVal strID As String,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String) As DataTable

        <OperationContract()>
        Function GetHU_DataDynamicContractAppendix(ByVal dID As String,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String) As DataTable

        <OperationContract()>
        Function GetHU_DataDynamicContract(ByVal dID As String,
                                       ByVal tempID As Decimal,
                                       ByRef folderName As String) As DataTable

        <OperationContract()>
        Function GetInsRegionList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetHU_CompetencyGroupList(ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetHU_CompetencyList(ByVal groupID As Decimal, ByVal isBlank As Boolean) As DataTable

        <OperationContract()>
        Function GetHU_CompetencyPeriodList(ByVal year As Decimal, ByVal isBlank As Boolean) As DataTable

#End Region

        <OperationContract()>
        Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As ProfileCommon.TABLE_NAME) As Boolean

        <OperationContract()>
        Function AutoGenCode(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String

        <OperationContract()>
        Function UpdateMergeField(ByVal lstData As List(Of MergeFieldDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetDataPrintBBBR(ByVal id As Decimal) As DataSet

        <OperationContract()>
        Function GetDataPrintBBBR3B(ByVal id As Decimal) As DataSet

#End Region
#Region "VALIDATE BUSINESS"
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Table_Name"></param>
        ''' <param name="Column_Name"></param>
        ''' <param name="Value"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ValidateBusiness(ByVal Table_Name As String, ByVal Column_Name As String, ByVal ListID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function CheckExistID(ByVal lstID As List(Of Decimal), ByVal table As String, ByVal column As String) As Boolean
#End Region
#Region "List"
        <OperationContract()>
        Function GetEmpInfomations(ByVal orgIDs As List(Of Decimal),
                                      ByVal _filter As EmployeeDTO,
                                      ByVal PageIndex As Integer,
                                      ByVal PageSize As Integer,
                                      ByRef Total As Integer,
                                      Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                      Optional ByVal log As UserLog = Nothing) As List(Of EmployeeDTO)
#Region "Title"
        <OperationContract()>
        Function GET_HU_TITLE_IMPORT() As DataSet
        <OperationContract()>
        Function GetTitle(ByVal _filter As TitleDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleDTO)

        <OperationContract()>
        Function InsertTitle(ByVal objTitle As TitleDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateTitle(ByVal objTitle As TitleDTO) As Boolean

        <OperationContract()>
        Function ModifyTitle(ByVal objTitle As TitleDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveTitle(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteTitle(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

        ''' <summary>
        ''' Lay danh sach TitleByID
        ''' </summary>
        ''' <returns>danh sach Title</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetTitleByID(ByVal sID As Decimal) As List(Of TitleDTO)
        <OperationContract()>
        Function GetTitleID(ByVal ID As Decimal) As TitleDTO

#End Region

#Region "TitleConcurrent"
        <OperationContract()>
        Function GetTitleConcurrent1(ByVal _filter As TitleConcurrentDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of TitleConcurrentDTO)
        <OperationContract()>
        Function GetTitleConcurrent(ByVal _filter As TitleConcurrentDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TitleConcurrentDTO)

        <OperationContract()>
        Function InsertTitleConcurrent(ByVal objTitleConcurrent As TitleConcurrentDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyTitleConcurrent(ByVal objTitleConcurrent As TitleConcurrentDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteTitleConcurrent(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region

#Region "ContractType"

        <OperationContract()>
        Function GetContractType(ByVal _filter As ContractTypeDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ContractTypeDTO)

        <OperationContract()>
        Function ValidContract(ByVal empid As Decimal, ByVal rd_date As Date, ByVal id As Decimal) As Boolean
        <OperationContract()>
        Function ValidContract1(ByVal empcode As String, ByVal effectdate As Date) As Boolean

        <OperationContract()>
        Function InsertContractType(ByVal objContractType As ContractTypeDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateContractType(ByVal objContractType As ContractTypeDTO) As Boolean

        <OperationContract()>
        Function ModifyContractType(ByVal objContractType As ContractTypeDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveContractType(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteContractType(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
#End Region


#Region "WelfareList"
        <OperationContract()>
        Function GET_INFO_EMPLOYEE(ByVal P_EMP_CODE As String) As DataTable
        <OperationContract()>
        Function GetlistWelfareEMP(ByVal Id As Integer) As List(Of Welfatemng_empDTO)
        <OperationContract()>
        Function GET_DETAILS_EMP(ByVal P_ID As Decimal, ByVal P_WELFARE_ID As Decimal, ByVal P_DATE As Date, ByVal log As UserLog) As DataTable
        <OperationContract()>
        Function GET_EXPORT_EMP(ByVal P_WELFARE_ID As Decimal, ByVal P_DATE As Date, ByVal log As UserLog) As DataSet
        <OperationContract()>
        Function GetWelfareList(ByVal _filter As WelfareListDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WelfareListDTO)

        <OperationContract()>
        Function InsertWelfareList(ByVal objWelfareList As WelfareListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateWelfareList(ByVal objWelfareList As WelfareListDTO) As Boolean

        <OperationContract()>
        Function ModifyWelfareList(ByVal objWelfareList As WelfareListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveWelfareList(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteWelfareList(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
#End Region
#Region "quan ly an toan lao dong"
        <OperationContract()>
        Function GetSafeLaborMng(ByVal _filter As SAFELABOR_MNGDTO, ByVal IsDissolve As Integer, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       ByVal UserLog As UserLog,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SAFELABOR_MNGDTO)
        <OperationContract()>
        Function InsertSafeLaborMng(ByVal lstSafeLaborMng As SAFELABOR_MNGDTO,
                               ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ModifySafeLaborMng(ByVal lstSafeLaborMng As SAFELABOR_MNGDTO,
                                 ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetSafeLaborMngById(ByVal Id As Integer
                                       ) As SAFELABOR_MNGDTO
        <OperationContract()>
        Function CheckCodeSafe(ByVal code As String, ByVal id As Decimal) As Boolean
        <OperationContract()>
        Function DeleteSafeLaborMng(ByVal lstWelfareMng() As SAFELABOR_MNGDTO,
                                   ByVal log As UserLog) As Boolean
#End Region

#Region "AllowanceList"

        <OperationContract()>
        Function GetAllowanceList(ByVal _filter As AllowanceListDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AllowanceListDTO)

        <OperationContract()>
        Function InsertAllowanceList(ByVal objAllowanceList As AllowanceListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAllowanceList(ByVal objAllowanceList As AllowanceListDTO) As Boolean

        <OperationContract()>
        Function ModifyAllowanceList(ByVal objAllowanceList As AllowanceListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveAllowanceList(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteAllowanceList(ByVal lstAllowanceList() As AllowanceListDTO, ByVal log As UserLog) As Boolean

#End Region

#Region "RelationShipList"
        <OperationContract()>
        Function GetRelationshipGroupList() As DataTable

        <OperationContract()>
        Function GetRelationshipList(ByVal _filter As RelationshipListDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of RelationshipListDTO)

        <OperationContract()>
        Function InsertRelationshipList(ByVal objRelationshipList As RelationshipListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateRelationshipList(ByVal objRelationshipList As RelationshipListDTO) As Boolean

        <OperationContract()>
        Function ModifyRelationshipList(ByVal objRelationshipList As RelationshipListDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveRelationshipList(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteRelationshipList(ByVal lstRelationshipList() As RelationshipListDTO, ByVal log As UserLog) As Boolean

#End Region

#Region "Organization"
        <OperationContract()>
        Function GetTreeOrgByID(ByVal ID As Decimal) As OrganizationTreeDTO
        <OperationContract()>
        Function GetOrganization(ByVal sACT As String) As List(Of OrganizationDTO)

        <OperationContract()>
        Function GetOrganizationByID(ByVal ID As Decimal) As OrganizationDTO

        <OperationContract()>
        Function InsertOrganization(ByVal objOrganization As OrganizationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateOrganization(ByVal objOrganization As OrganizationDTO) As Boolean

        <OperationContract()>
        Function ValidateCostCenterCode(ByVal objOrganization As OrganizationDTO) As Boolean

        <OperationContract()>
        Function CheckEmployeeInOrganization(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function ModifyOrganization(ByVal objOrganization As OrganizationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyOrganizationPath(ByVal lstPath As List(Of OrganizationPathDTO)) As Boolean

        <OperationContract()>
        Function ActiveOrganization(ByVal objOrganization() As OrganizationDTO, ByVal sActive As String, ByVal log As UserLog) As Boolean
#End Region

#Region "OrgTitle"

        <OperationContract()>
        Function GetOrgTitle(ByVal filter As OrgTitleDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of OrgTitleDTO)

        <OperationContract()>
        Function InsertOrgTitle(ByVal objOrgTitle As List(Of OrgTitleDTO), ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function CheckTitleInEmployee(ByVal lstID As List(Of Decimal), ByVal orgID As Decimal) As Boolean
        <OperationContract()>
        Function CheckHasInWorking(ByVal lstID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function DeleteOrgTitle(ByVal objOrgTitle As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ActiveOrgTitle(ByVal objOrgTitle As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

#End Region

#Region "Danh muc tham so he thong"
        ''' <summary>
        ''' Validate Tham so he thong
        ''' </summary>
        ''' <param name="objOtherList"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ValidateOtherList(ByVal objOtherList As OtherListDTO) As Boolean
#End Region

#Region "Nation - Danh mục quốc gia"
        ''' <summary>
        ''' Lay danh sach Quoc gia
        ''' </summary>
        ''' <returns>danh sach Quoc gia</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetNation(ByVal _filter As NationDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of NationDTO)

        '------------------------------------------------------------------------

        ''' <summary>
        ''' Them moi Quoc gia
        ''' </summary>
        ''' <param name="objNation"> Doi tuong quoc gia can Insert</param>
        ''' <param name="log"> Doi tuong chua thong tin log</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertNation(ByVal objNation As NationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Sua thong tin Quoc gia
        ''' </summary>
        ''' <param name="objNation">doi tuong chua cac thong tin can sua</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyNation(ByVal objNation As NationDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateNation(ByVal objNation As NationDTO) As Boolean

        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Áp dụng/ ngưng áp dụng Quoc gia
        ''' </summary>
        ''' <param name="objNation">doi tuong chua thong tin can xoa</param>
        ''' <param name="sActive">trang thai</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ActiveNation(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        ''' <summary>
        ''' Xóa danh mục quốc gia
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="strError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteNation(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
#End Region

#Region "Province - Danh mục Tinh Thanh"
        ''' <summary>
        ''' Lay danh sach tinh thanh theo quoc gia
        ''' </summary>
        ''' <param name="sNationID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetProvinceByNationID(ByVal sNationID As Decimal, ByVal sACTFLG As String) As List(Of ProvinceDTO)


        <OperationContract()>
        Function GetProvinceByNationCode(ByVal sNationCode As String, ByVal sACTFLG As String) As List(Of ProvinceDTO)


        ''' <summary>
        ''' Lay danh sach Tinh thanh
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetProvince(ByVal _filter As ProvinceDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ProvinceDTO)

        '------------------------------------------------------------------------

        ''' <summary>
        ''' Them moi Tinh thanh
        ''' </summary>
        ''' <param name="objProvince"> Doi tuong Tinh thanh can Insert</param>
        ''' <param name="log"> Doi tuong chua thong tin log</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertProvince(ByVal objProvince As ProvinceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Sua thong tin Tinh thanh
        ''' </summary>
        ''' <param name="objProvince">doi tuong chua cac thong tin can sua</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyProvince(ByVal objProvince As ProvinceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateProvince(ByVal objProvince As ProvinceDTO) As Boolean

        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Áp dụng/ ngưng áp dụng Tinh thanh
        ''' </summary>
        ''' <param name="objProvince">doi tuong chua thong tin can xoa</param>
        ''' <param name="sActive">trang thai</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ActiveProvince(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        ''' <summary>
        ''' Xóa danh mục tỉnh thành
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <param name="strError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteProvince(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
#End Region

#Region "District - Danh mục Quan Huyen"
        ''' <summary>
        ''' Lay danh sach Quan Huyen
        ''' </summary>
        ''' <returns>danh sach Quan Huyen</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetDistrict(ByVal _filter As DistrictDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DistrictDTO)

        '------------------------------------------------------------------------

        ''' <summary>
        ''' Lay danh sach quan huyen theo ProvinceID
        ''' </summary>
        ''' <param name="sProvinceID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetDistrictByProvinceID(ByVal sProvinceID As Decimal, ByVal sACTFLG As String) As List(Of DistrictDTO)

        '------------------------------------------------------------------------

        ''' <summary>
        ''' Them moi Quan Huyen
        ''' </summary>
        ''' <param name="objDistrict"> Doi tuong Quan Huyen can Insert</param>
        ''' <param name="log"> Doi tuong chua thong tin log</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertDistrict(ByVal objDistrict As DistrictDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Sua thong tin Quan Huyen
        ''' </summary>
        ''' <param name="objDistrict">doi tuong chua cac thong tin can sua</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyDistrict(ByVal objDistrict As DistrictDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        '-----------------------------------------------------------------------

        <OperationContract()>
        Function ValidateDistrict(ByVal _validate As DistrictDTO) As Boolean

        ''' <summary>
        ''' Áp dụng/ ngưng áp dụng Quan Huyen
        ''' </summary>
        ''' <param name="objDistrict">doi tuong chua thong tin can xoa</param>
        ''' <param name="sActive">trang thai</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ActiveDistrict(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        ''' <summary>
        ''' Xóa danh mục quận huyện
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <param name="strError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteDistrict(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean

#End Region

#Region "Ward - Danh mục xã phường"
        ''' <summary>
        ''' Lay danh sach Quan Huyen
        ''' </summary>
        ''' <returns>danh sach Quan Huyen</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetWard(ByVal _filter As Ward_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of Ward_DTO)

        '------------------------------------------------------------------------

        ''' <summary>
        ''' Lay danh sach xã phường theo districtID
        ''' </summary>
        ''' <param name="sProvinceID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetWardByDistrictID(ByVal sDistrictID As Decimal, ByVal sACTFLG As String) As List(Of Ward_DTO)

        '------------------------------------------------------------------------

        ''' <summary>
        ''' Them moi xã phường
        ''' </summary>
        ''' <param name="objDistrict"> Doi tuong Quan Huyen can Insert</param>
        ''' <param name="log"> Doi tuong chua thong tin log</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertWard(ByVal objWard As Ward_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Sua thong tin xã phường
        ''' </summary>
        ''' <param name="objDistrict">doi tuong chua cac thong tin can sua</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyWard(ByVal objWard As Ward_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        '-----------------------------------------------------------------------

        <OperationContract()>
        Function ValidateWard(ByVal _validate As Ward_DTO) As Boolean
        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Áp dụng/ ngưng áp dụng xã phường
        ''' </summary>
        ''' <param name="objDistrict">doi tuong chua thong tin can xoa</param>
        ''' <param name="sActive">trang thai</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ActiveWard(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Xóa danh mục xã phường
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <param name="strError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteWard(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
#End Region

#Region "Bank - Danh mục Ngan hang"
        ''' <summary>
        ''' Lay danh sach Ngan hang
        ''' </summary>
        ''' <returns>danh sach Ngan hang</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetBank(ByVal _filter As BankDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of BankDTO)

        '------------------------------------------------------------------------
        ''' <summary>
        ''' Them moi Ngan hang
        ''' </summary>
        ''' <param name="objBank"> Doi tuong Ngan hang can Insert</param>
        ''' <param name="log"> Doi tuong chua thong tin log</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertBank(ByVal objBank As BankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Sua thong tin Ngan hang
        ''' </summary>
        ''' <param name="objBank">doi tuong chua cac thong tin can sua</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyBank(ByVal objBank As BankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateBank(ByVal objBank As BankDTO) As Boolean

        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Áp dụng/ ngưng áp dụng Ngan hang
        ''' </summary>
        ''' <param name="objBank">doi tuong chua thong tin can xoa</param>
        ''' <param name="sActive">trang thai</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ActiveBank(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        ''' <summary>
        ''' Xóa danh mục ngân hàng
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <param name="strError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteBank(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
#End Region

#Region "BankBranch - Danh mục Chi nhanh Ngan hang"
        ''' <summary>
        ''' Lay danh sach Chi nhanh Ngan hang
        ''' </summary>
        ''' <returns>danh sach Chi nhanh Ngan hang</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetBankBranch(ByVal _filter As BankBranchDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of BankBranchDTO)

        ''' <summary>
        ''' Lay danh sach Chi nhanh Ngan hang theo Bank_ID
        ''' </summary>
        ''' <returns>danh sach Chi nhanh Ngan hang</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetBankBranchByBankID(ByVal sBank_Id As Decimal) As List(Of BankBranchDTO)


        '------------------------------------------------------------------------
        ''' <summary>
        ''' Them moi Chi nhanh Ngan hang
        ''' </summary>
        ''' <param name="objBankBranch"> Doi tuong Chi nhanh Ngan hang can Insert</param>
        ''' <param name="log"> Doi tuong chua thong tin log</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertBankBranch(ByVal objBankBranch As BankBranchDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Sua thong tin Chi nhanh Ngan hang
        ''' </summary>
        ''' <param name="objBankBranch">doi tuong chua cac thong tin can sua</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyBankBranch(ByVal objBankBranch As BankBranchDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateBankBranch(ByVal objBankBranch As BankBranchDTO) As Boolean
        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Áp dụng/ ngưng áp dụng Chi nhanh Ngan hang
        ''' </summary>
        ''' <param name="objBankBranch">doi tuong chua thong tin can xoa</param>
        ''' <param name="sActive">trang thai</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ActiveBankBranch(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        ''' <summary>
        ''' Xóa danh mcuj chi nhánh ngân hàng
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <param name="strError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteBankBranch(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
#End Region

#Region "Asset - Danh muc tai san cap phat"
        ''' <summary>
        ''' Lay danh sach Tai san cap phat
        ''' </summary>
        ''' <returns>danh sach Tai san cap phat</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetAsset(ByVal _filter As AssetDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AssetDTO)
        '------------------------------------------------------------------------
        ''' <summary>
        ''' Them moi Tai san cap phat
        ''' </summary>
        ''' <param name="objAsset"> Doi tuong Tai san cap phat can Insert</param>
        ''' <param name="log"> Doi tuong chua thong tin log</param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertAsset(ByVal objAsset As AssetDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Sua thong tin Tai san cap phat
        ''' </summary>
        ''' <param name="objAsset">doi tuong chua cac thong tin can sua</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyAsset(ByVal objAsset As AssetDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateAsset(ByVal objAsset As AssetDTO) As Boolean
        '-----------------------------------------------------------------------

        ''' <summary>
        ''' Áp dụng/ ngưng áp dụng Tai san cap phat
        ''' </summary>
        ''' <param name="objAsset">doi tuong chua thong tin can xoa</param>
        ''' <param name="sActive">trang thai</param>
        ''' <param name="log"></param>
        ''' <returns>true?false</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ActiveAsset(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        ''' <summary>
        ''' Xóa tài sản cấp phát
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <param name="strError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteAsset(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
#End Region

#Region "StaffRank"
        <OperationContract()>
        Function GetStaffRank(ByVal _filter As StaffRankDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of StaffRankDTO)

        <OperationContract()>
        Function InsertStaffRank(ByVal objStaffRank As StaffRankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateStaffRank(ByVal objStaffRank As StaffRankDTO) As Boolean

        <OperationContract()>
        Function ModifyStaffRank(ByVal objStaffRank As StaffRankDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveStaffRank(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal sActive As String) As Boolean

        <OperationContract()>
        Function DeleteStaffRank(ByVal lstStaffRank() As StaffRankDTO, ByVal log As UserLog) As Boolean

#End Region

#Region "Năng lực"

#Region "CompetencyGroup"

        <OperationContract()>
        Function GetCompetencyGroup(ByVal _filter As CompetencyGroupDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyGroupDTO)

        <OperationContract()>
        Function InsertCompetencyGroup(ByVal objCompetencyGroup As CompetencyGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateCompetencyGroup(ByVal objCompetencyGroup As CompetencyGroupDTO) As Boolean

        <OperationContract()>
        Function ModifyCompetencyGroup(ByVal objCompetencyGroup As CompetencyGroupDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCompetencyGroup(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteCompetencyGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region

#Region "Competency"

        <OperationContract()>
        Function GetCompetency(ByVal _filter As CompetencyDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyDTO)

        <OperationContract()>
        Function InsertCompetency(ByVal objCompetency As CompetencyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateCompetency(ByVal objCompetency As CompetencyDTO) As Boolean

        <OperationContract()>
        Function ModifyCompetency(ByVal objCompetency As CompetencyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCompetency(ByVal lstID As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteCompetency(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region

#Region "CompetencyBuild"

        <OperationContract()>
        Function GetCompetencyBuild(ByVal _filter As CompetencyBuildDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyBuildDTO)

        <OperationContract()>
        Function InsertCompetencyBuild(ByVal objCompetencyBuild As CompetencyBuildDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyCompetencyBuild(ByVal objCompetencyBuild As CompetencyBuildDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteCompetencyBuild(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region

#Region "CompetencyStandard"

        <OperationContract()>
        Function GetCompetencyStandard(ByVal _filter As CompetencyStandardDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyStandardDTO)

        <OperationContract()>
        Function InsertCompetencyStandard(ByVal objCompetencyStandard As CompetencyStandardDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyCompetencyStandard(ByVal objCompetencyStandard As CompetencyStandardDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteCompetencyStandard(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region

#Region "CompetencyAppendix"

        <OperationContract()>
        Function GetCompetencyAppendix(ByVal _filter As CompetencyAppendixDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyAppendixDTO)

        <OperationContract()>
        Function InsertCompetencyAppendix(ByVal objCompetencyAppendix As CompetencyAppendixDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyCompetencyAppendix(ByVal objCompetencyAppendix As CompetencyAppendixDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteCompetencyAppendix(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region

#Region "CompetencyEmp"

        <OperationContract()>
        Function GetCompetencyEmp(ByVal _filter As CompetencyEmpDTO) As List(Of CompetencyEmpDTO)

        <OperationContract()>
        Function UpdateCompetencyEmp(ByVal lstCom As List(Of CompetencyEmpDTO), ByVal log As UserLog) As Boolean

#End Region

#Region "CompetencyPeriod"

        <OperationContract()>
        Function GetCompetencyPeriod(ByVal _filter As CompetencyPeriodDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyPeriodDTO)

        <OperationContract()>
        Function InsertCompetencyPeriod(ByVal objCompetencyPeriod As CompetencyPeriodDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyCompetencyPeriod(ByVal objCompetencyPeriod As CompetencyPeriodDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteCompetencyPeriod(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region

#Region "CompetencyAssDtl"

        <OperationContract()>
        Function GetCompetencyAss(ByVal _filter As CompetencyAssDTO) As List(Of CompetencyAssDTO)

        <OperationContract()>
        Function GetCompetencyAssDtl(ByVal _filter As CompetencyAssDtlDTO) As List(Of CompetencyAssDtlDTO)

        <OperationContract()>
        Function UpdateCompetencyAssDtl(ByVal objAss As CompetencyAssDTO, ByVal lstCom As List(Of CompetencyAssDtlDTO), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function DeleteCompetencyAss(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region

#End Region

#End Region
#Region "Job Description"
        <OperationContract()>
        Function GetJobDescription(ByVal _filter As JobDescriptionDTO,
                                        ByVal _param As ParamDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of JobDescriptionDTO)
        <OperationContract()>
        Function InserJobDescription(ByVal objJobDes As JobDescriptionDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ModifyJobDescription(ByVal objJobDes As JobDescriptionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteJobDescretion(ByVal objJobDes As JobDescriptionDTO) As Boolean

        <OperationContract()>
        Function GetJobDesByID(ByVal ID As Decimal) As JobDescriptionDTO
#End Region
#Region "Bussiness"

#Region "EmployeeBussiness - Nghiệp vụ hồ sơ"


        ''' <summary>
        ''' Lay thong tin nhan vien tu EmployeeCode
        ''' </summary>
        ''' <param name="sEmployeeCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetEmployeeByEmployeeID(ByVal empID As Decimal) As EmployeeDTO


        ''' <summary>
        ''' Trả về binary của ảnh hồ sơ nhân viên
        ''' </summary>
        ''' <param name="gEmpID"></param>
        ''' <param name="sError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetEmployeeImage(ByVal gEmpID As Decimal, ByRef sError As String) As Byte()

        ''' <summary>
        ''' Hàm lấy đường dẫn ảnh HSNV để in CV trên portal
        ''' <creater>TUNGLD</creater>
        ''' </summary>
        ''' <param name="gEmpID"></param>
        ''' <param name="isOneEmployee"></param>
        ''' <param name="img_link"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetEmployeeImage_PrintCV(ByVal gEmpID As Decimal) As String
        ''' <summary>
        ''' Thêm mới nhân viên
        ''' </summary>
        ''' <param name="objEmp"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <param name="objEmpCV"></param>
        ''' <param name="objEmpEdu"></param>
        ''' <param name="objEmpOther"></param>
        ''' <param name="objBankAccLog"></param>
        ''' <param name="objEmpHealth"></param>
        ''' <param name="objEmpSalary"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertEmployee(ByVal objEmp As EmployeeDTO, ByVal log As UserLog, ByRef gID As Decimal, _
                                  ByRef _strEmpCode As String, _
                                  ByVal _imageBinary As Byte(),
                                  Optional ByVal objEmpCV As EmployeeCVDTO = Nothing, _
                                  Optional ByVal objEmpEdu As EmployeeEduDTO = Nothing, _
                                  Optional ByVal objEmpHealth As EmployeeHealthDTO = Nothing, _
                                  Optional ByVal objEmpUniform As UniformSizeDTO = Nothing) As Boolean
        <OperationContract()>
        Function CreateNewEMPLOYEECode() As EmployeeDTO

        ''' <summary>
        ''' Sửa thông tin nhân viên
        ''' </summary>
        ''' <param name="objEmp"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <param name="objEmpCV"></param>
        ''' <param name="objEmpEdu"></param>
        ''' <param name="objEmpOther"></param>
        ''' <param name="objBankAccLog"></param>
        ''' <param name="objEmpHealth"></param>
        ''' <param name="objEmpSalary"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyEmployee(ByVal objEmp As EmployeeDTO, ByVal log As UserLog, ByRef gID As Decimal, _
                                  ByVal _imageBinary As Byte(), _
                                  Optional ByVal objEmpCV As EmployeeCVDTO = Nothing,
                                  Optional ByVal objEmpEdu As EmployeeEduDTO = Nothing,
                                  Optional ByVal objEmpHealth As EmployeeHealthDTO = Nothing, _
                                  Optional ByVal objEmpUniform As UniformSizeDTO = Nothing) As Boolean

        ''' <summary>
        ''' Lấy danh sách nhân viên theo điều kiện
        ''' </summary>
        ''' <param name="_orgIds"></param>s
        ''' <param name="_filter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetEmployeeOrgChart(ByVal lstOrg As List(Of Decimal), Optional ByVal log As UserLog = Nothing) As List(Of OrgChartDTO)

        ''' <summary>
        ''' Lấy danh sách nhân viên bao gồm ảnh để hiển thị trên org chart
        ''' </summary>
        ''' <param name="_orgIds"></param>
        ''' <param name="_filter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetListEmployee(ByVal _orgIds As List(Of Decimal), ByVal _filter As EmployeeDTO) As List(Of EmployeeDTO)


        ''' <summary>
        ''' Lấy danh sách nhân viên có phân trang
        ''' </summary>
        ''' <param name="PageIndex"></param>
        ''' <param name="PageSize"></param>
        ''' <param name="Total"></param>
        ''' <param name="_orgIds"></param>
        ''' <param name="_filter"></param>
        ''' <param name="Sorts"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetListEmployeePaging(ByVal _filter As EmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of EmployeeDTO)
        <OperationContract()>
        Function GetChartEmployee(ByVal _filter As EmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of EmployeeDTO)
        <OperationContract()>
        Function GetListEmployeePortal(ByVal _filter As EmployeeDTO) As List(Of EmployeeDTO)

        ''' <summary>
        ''' Lấy thông tin EmployeeCV 
        ''' </summary>
        ''' <param name="sEmployeeID">ID(Decimal) của nhân viên</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetEmployeeAllByID(ByVal sEmployeeID As Decimal,
                                  ByRef empCV As EmployeeCVDTO,
                                  ByRef empEdu As EmployeeEduDTO,
                                  ByRef empHealth As EmployeeHealthDTO,
                                  ByRef empUniform As UniformSizeDTO) As Boolean

        <OperationContract()>
        Function GetEmployeeBG(ByVal sEmployeeID As Decimal, ByRef empBG As EmployeeBackgroundDTO) As Boolean

        ''' <summary>
        ''' Xóa nhân viên
        ''' </summary>
        ''' <param name="lstEmpID"></param>
        ''' <param name="log"></param>
        ''' <param name="sError"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteEmployee(ByVal lstEmpID As List(Of Decimal), ByVal log As UserLog, ByRef sError As String) As Boolean

        <OperationContract()>
        Function ValidateEmployee(ByVal sEmpCode As String, ByVal value As String, ByVal sType As String) As Boolean

        ''' <summary>
        ''' Hàm kiểm tra nhân viên đã có hợp đồng chưa
        ''' </summary>
        ''' <param name="strEmpCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function CheckEmpHasContract(ByVal strEmpCode As String) As Boolean
#End Region

#Region "EmployeeEdit"
        <OperationContract()>
        Function GetChangedCVList(ByVal lstEmpEdit As List(Of EmployeeEditDTO)) As Dictionary(Of String, String)

        <OperationContract()>
        Function InsertEmployeeEdit(ByVal objEmployeeEdit As EmployeeEditDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyEmployeeEdit(ByVal objEmployeeEdit As EmployeeEditDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteEmployeeEdit(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetEmployeeEditByID(ByVal _filter As EmployeeEditDTO) As EmployeeEditDTO

        <OperationContract()>
        Function SendEmployeeEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function UpdateStatusEmployeeEdit(ByVal lstID As List(Of Decimal),
                                                   ByVal status As String,
                                                   ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetApproveEmployeeEdit(ByVal _filter As EmployeeEditDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of EmployeeEditDTO)

#End Region

#Region "EmployeeFamily"
        ''' <summary>
        ''' Lay danh sach than nhan
        ''' </summary>
        ''' <param name="_filter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetEmployeeFamily(ByVal _filter As FamilyDTO) As List(Of FamilyDTO)

        ''' <summary>
        ''' Them moi nhan than
        ''' </summary>
        ''' <param name="objFamily"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertEmployeeFamily(ByVal objFamily As FamilyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Chinh sua thong tin nhan than
        ''' </summary>
        ''' <param name="objFamily"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyEmployeeFamily(ByVal objFamily As FamilyDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Xóa nhân thân
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteEmployeeFamily(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean


        ''' <summary>
        ''' Check trùng CMND của nhân thân.
        ''' </summary>
        ''' <param name="_validate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ValidateFamily(ByVal _validate As FamilyDTO) As Boolean
#End Region

#Region "EmployeeFamilyEdit"
        ''' <summary>
        ''' Lay danh sach than nhan thay doi de to mau
        ''' </summary>
        ''' <param name="lstFamilyEdit"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetChangedFamilyList(ByVal lstFamilyEdit As List(Of FamilyEditDTO)) As Dictionary(Of String, String)
        ''' <summary>
        ''' Lay danh sach than nhan
        ''' </summary>
        ''' <param name="_filter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetEmployeeFamilyEdit(ByVal _filter As FamilyEditDTO) As List(Of FamilyEditDTO)

        ''' <summary>
        ''' Them moi nhan than
        ''' </summary>
        ''' <param name="objFamilyEdit"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function InsertEmployeeFamilyEdit(ByVal objFamilyEdit As FamilyEditDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Chinh sua thong tin nhan than
        ''' </summary>
        ''' <param name="objFamilyEdit"></param>
        ''' <param name="log"></param>
        ''' <param name="gID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function ModifyEmployeeFamilyEdit(ByVal objFamilyEdit As FamilyEditDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        ''' <summary>
        ''' Xóa nhân thân
        ''' </summary>
        ''' <param name="lstDecimals"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function DeleteEmployeeFamilyEdit(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function CheckExistFamilyEdit(ByVal pk_key As Decimal) As FamilyEditDTO

        <OperationContract()>
        Function SendEmployeeFamilyEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function UpdateStatusEmployeeFamilyEdit(ByVal lstID As List(Of Decimal),
                                                   ByVal status As String,
                                                   ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function UpdateStatusEmployeeCetificateEdit(ByVal lstID As List(Of Decimal),
                                                  status As String,
                                                  ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetApproveFamilyEdit(ByVal _filter As FamilyEditDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of FamilyEditDTO)
        <OperationContract()>
        Function GetApproveEmployeeCertificateEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer, ByVal _param As ParamDTO,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)

#End Region

#Region "EmployeeTrain"
        <OperationContract()>
        Function GetEmployeeTrain(ByVal _filter As EmployeeTrainDTO) As List(Of EmployeeTrainDTO)
        <OperationContract()>
        Function GetEmployeeTrainForCompany(ByVal _filter As EmployeeTrainForCompanyDTO) As List(Of EmployeeTrainForCompanyDTO)
        <OperationContract()>
        Function GetEmployeeTrainByID(ByVal EmployeeID As Decimal) As EmployeeTrainDTO
        <OperationContract()>
        Function InsertEmployeeTrain(ByVal objEmployeeTrain As EmployeeTrainDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyEmployeeTrain(ByVal objEmployeeTrain As EmployeeTrainDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeleteEmployeeTrain(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ValidateEmployeeTrain(ByVal objValidate As EmployeeTrainDTO) As Boolean
#End Region

#Region "WorkingBefore"

        <OperationContract()>
        Function GetEmpWorkingBefore(ByVal _filter As WorkingBeforeDTO) As List(Of WorkingBeforeDTO)

        <OperationContract()>
        Function InsertWorkingBefore(ByVal objWorkingBefore As WorkingBeforeDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyWorkingBefore(ByVal objWorkingBefore As WorkingBeforeDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteWorkingBefore(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
#End Region

#Region "thong tin li lich ca nhan"

        <OperationContract()>
        Function GetEmpBackGround(ByVal _filter As EmployeeBackgroundDTO,
                                  ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EFFECTIVE_DATE desc") As List(Of EmployeeBackgroundDTO)

        <OperationContract()>
        Function InsertBackGround(ByVal objBackGround As EmployeeBackgroundDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyBackGround(ByVal objBackGround As EmployeeBackgroundDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteBackGround(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
#End Region

#Region "AssetMng"

        <OperationContract()>
        Function GetAssetMng(ByVal _filter As AssetMngDTO,
                             ByVal _param As ParamDTO,
                             ByVal PageIndex As Integer,
                             ByVal PageSize As Integer,
                             ByRef Total As Integer,
                             Optional ByVal Sorts As String = "CREATED_DATE desc",
                             Optional ByVal log As UserLog = Nothing) As List(Of AssetMngDTO)

        <OperationContract()>
        Function GetAssetMngById(ByVal Id As Integer
                                        ) As AssetMngDTO

        <OperationContract()>
        Function InsertAssetMng(ByVal objAssetMng As AssetMngDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyAssetMng(ByVal objAssetMng As AssetMngDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteAssetMng(ByVal objAssetMng() As AssetMngDTO, ByVal log As UserLog) As Boolean
#End Region
#Region "evaluate"
        <OperationContract()>
        Function GetTrainingEvaluateEmp(ByVal _empId As Decimal) As List(Of TrainningEvaluateDTO)
        <OperationContract()>
        Function GetTrainingEvaluate(ByVal _filter As TrainningEvaluateDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of TrainningEvaluateDTO)
        <OperationContract()>
        Function InsertTrainingEvaluate(ByVal objContract As TrainningEvaluateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function GetTrainingEvaluateByID(ByVal _filter As TrainningEvaluateDTO) As TrainningEvaluateDTO
        <OperationContract()>
        Function ModifyTrainingEvaluate(ByVal objContract As TrainningEvaluateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeleteTrainingEvaluate(ByVal objAssetMng As TrainningEvaluateDTO) As Boolean

#End Region
#Region "training manage"
        <OperationContract()>
        Function GetListTrainingManageByEmpID(ByVal _filter As TrainningManageDTO, ByVal _param As ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningManageDTO)

        <OperationContract()>
        Function GetTrainingManage(ByVal _filter As TrainningManageDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of TrainningManageDTO)

        <OperationContract()>
        Function InsertTrainingManage(ByVal objContract As TrainningManageDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteTrainingManage(ByVal objAssetMng As TrainningManageDTO) As Boolean
        <OperationContract()>
        Function GetTrainingManageByID(ByVal _filter As TrainningManageDTO) As TrainningManageDTO
        <OperationContract()>
        Function ModifyTrainingManage(ByVal objContract As TrainningManageDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

#End Region

#Region "trainingforeign"
        <OperationContract()>
        Function GetTrainingForeign(ByVal _filter As TrainningForeignDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of TrainningForeignDTO)
        <OperationContract()>
        Function InsertTrainingForeign(ByVal objContract As TrainningForeignDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function GetTrainingForeignByID(ByVal _filter As TrainningForeignDTO) As TrainningForeignDTO
        <OperationContract()>
        Function ModifyTrainingForeign(ByVal objContract As TrainningForeignDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function DeleteTrainingForeign(ByVal objContract() As TrainningForeignDTO) As Boolean

#End Region
#Region "Contract"
        <OperationContract()>
        Function GetContract(ByVal _filter As ContractDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of ContractDTO)

        <OperationContract()>
        Function GetContractByID(ByVal _filter As ContractDTO) As ContractDTO

        <OperationContract()>
        Function ValidateContract(ByVal sType As String, ByVal obj As ContractDTO) As Boolean
        <OperationContract()>
        Function UpdateDateToContract(ByVal id As Decimal, ByVal day As Date, ByVal remark As String) As Boolean
        <OperationContract()>
        Function InsertContract(ByVal objContract As ContractDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function CheckNotAllow(ByVal empid As Decimal, ByVal id As Decimal) As Boolean
        <OperationContract()>
        Function CheckHasFileContract(ByVal id As List(Of Decimal)) As Decimal
        <OperationContract()>
        Function CheckHasFileFileContract(ByVal id As List(Of Decimal)) As Decimal
        <OperationContract()>
        Function ModifyContract(ByVal objContract As ContractDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteContract(ByVal objAssetMng As ContractDTO) As Boolean

        <OperationContract()>
        Function CreateContractNo(ByVal objAssetMng As ContractDTO) As String

        <OperationContract()>
        Function CheckContractNo(ByVal objAssetMng As ContractDTO) As Boolean

        <OperationContract()>
        Function GetContractEmployeeByID(ByVal gEmployeeID As Decimal) As EmployeeDTO

        <OperationContract()>
        Function UnApproveContract(ByVal objContract As ContractDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function GetCheckContractTypeID(ByVal listID As String) As DataTable

#End Region

#Region "WelfareMng"
        <OperationContract()>
        Function GetWelfareListAuto(ByVal _filter As WelfareMngDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       ByVal log As UserLog) As DataTable
        <OperationContract()>
        Function GetWelfareMng(ByVal _filter As WelfareMngDTO, ByVal IsDissolve As Integer, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal UserLog As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WelfareMngDTO)
        <OperationContract()>
        Function GetWelfareMngById(ByVal Id As Integer) As WelfareMngDTO

        <OperationContract()>
        Function GetComboboxPeriod(ByVal year As Decimal) As List(Of ATPeriodDTO)

        <OperationContract()>
        Function GetYearPeriod() As List(Of ATPeriodDTO)

        <OperationContract()>
        Function CheckWelfareMngEffect(ByVal _filter As List(Of WelfareMngDTO)) As Boolean

        <OperationContract()>
        Function InsertWelfareMng(ByVal lstWelfareMng As WelfareMngDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ModifyWelfareMng(ByVal lstWelfareMng As WelfareMngDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteWelfareMng(ByVal lstWelfareMng() As WelfareMngDTO, ByVal log As UserLog) As Boolean
#End Region

#Region "Working"
        <OperationContract()>
        Function getDtByEmpIDandEffectdate(ByVal obj As WorkingDTO) As List(Of WorkingDTO)
        <OperationContract()>
        Function getValue_ExRate_F_T(ByVal _filter As WorkingDTO) As WorkingDTO
        <OperationContract()>
        Function ApproveListChangeInfoMng(ByVal listID As List(Of Decimal), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetWorking(ByVal _filter As WorkingDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO)


        <OperationContract()>
        Function GetWorkingAllowance(ByVal _filter As WorkingAllowanceDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer,
                               Optional ByVal Sorts As String = "EMPLOYEE_CODE") As List(Of WorkingAllowanceDTO)

        <OperationContract()>
        Function GetWorkingAllowance1(ByVal _filter As HUAllowanceDTO,
                                        ByVal _param As ParamDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of HUAllowanceDTO)


        <OperationContract()>
        Function InsertWorkingAllowance(ByVal objWorking As HUAllowanceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteWorkingAllowance(ByVal lstWorkingAllowance() As HUAllowanceDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ModifyWorkingAllowanceNew(ByVal objWorking As HUAllowanceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyWorkingAllowance(ByVal objWorking As WorkingAllowanceDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function GetWorkingByID(ByVal _filter As WorkingDTO) As WorkingDTO

        <OperationContract()>
        Function GetEmployeCurrentByID(ByVal _filter As WorkingDTO) As WorkingDTO
        <OperationContract()>
        Function InsertListWorking1(ByVal objWorking As WorkingDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function InsertWorking1(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyWorking1(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function InsertWorking(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyWorking(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateNewEdit(ByVal objWorking As HUAllowanceDTO) As Boolean

        <OperationContract()>
        Function DeleteWorking(ByVal objAssetMng As WorkingDTO) As Boolean

        <OperationContract()>
        Function ValidateWorking(ByVal sType As String, ByVal obj As WorkingDTO) As Boolean

        <OperationContract()>
        Function ValEffectdateByEmpCode(ByVal emp_code As String, ByVal effect_date As Date) As Boolean

        <OperationContract()>
        Function GetLastWorkingSalary(ByVal _filter As WorkingDTO) As WorkingDTO

        <OperationContract()>
        Function GetAllowanceByDate(ByVal _filter As WorkingAllowanceDTO) As List(Of WorkingAllowanceDTO)

        <OperationContract()>
        Function GetAllowanceByWorkingID(ByVal _filter As WorkingAllowanceDTO) As List(Of WorkingAllowanceDTO)

        <OperationContract()>
        Function GetWorking3B(ByVal _filter As WorkingDTO,
                               ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO)


        <OperationContract()>
        Function InsertWorking3B(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyWorking3B(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteWorking3B(ByVal objWorking As WorkingDTO) As Boolean

        <OperationContract()>
        Function GetChangeInfoImport(ByVal param As ParamDTO, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function ImportChangeInfo(ByVal lstData As List(Of WorkingDTO),
                                     ByRef dtError As DataTable,
                                     ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function UnApproveWorking(ByVal objWorking As WorkingDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ApproveWorkings(ByVal ids As List(Of Decimal), Optional ByVal log As UserLog = Nothing) As CommandResult
        <OperationContract()>
        Function CheckHasFile(ByVal id As List(Of Decimal)) As Decimal
#End Region

#Region "Discipline"
        <OperationContract()>
        Function CheckHasFileDiscipline(ByVal id As List(Of Decimal)) As Decimal
        <OperationContract()>
        Function ApproveListDiscipline(ByVal listID As List(Of Decimal), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetEmployeeDesciplineID(ByVal DesId As Decimal) As List(Of DisciplineEmpDTO)

        <OperationContract()>
        Function GetDiscipline(ByVal _filter As DisciplineDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of DisciplineDTO)

        <OperationContract()>
        Function GetDisciplineByID(ByVal _filter As DisciplineDTO) As DisciplineDTO

        <OperationContract()>
        Function InsertDiscipline(ByVal objDiscipline As DisciplineDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyDiscipline(ByVal objDiscipline As DisciplineDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateDiscipline(ByVal sType As String, ByVal obj As DisciplineDTO) As Boolean

        <OperationContract()>
        Function DeleteDiscipline(ByVal objAssetMng() As DisciplineDTO) As Boolean

        <OperationContract()>
        Function ApproveDiscipline(ByVal objDiscipline As DisciplineDTO) As Boolean

        <OperationContract()>
        Function Open_ApproveDiscipline(ByVal listID As List(Of Decimal), ByVal log As UserLog) As Boolean

#End Region

#Region "DisciplineSalary"

        <OperationContract()>
        Function GetDisciplineSalary(ByVal _filter As DisciplineSalaryDTO,
                                     ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                     Optional ByVal log As UserLog = Nothing,
                                     Optional ByVal Sorts As String = "YEAR,MONTH,EMPLOYEE_CODE") As List(Of DisciplineSalaryDTO)

        <OperationContract()>
        Function GetDisciplineSalaryByID(ByVal _filter As DisciplineSalaryDTO) As DisciplineSalaryDTO

        <OperationContract()>
        Function EditDisciplineSalary(ByVal obj As DisciplineSalaryDTO) As Boolean

        <OperationContract()>
        Function ValidateDisciplineSalary(ByVal obj As DisciplineSalaryDTO, ByRef sError As String) As Boolean

        <OperationContract()>
        Function ApproveDisciplineSalary(ByVal lstID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function StopDisciplineSalary(ByVal lstID As List(Of Decimal)) As Boolean

#End Region

#Region "Commend"
        <OperationContract()>
        Function CheckHasFileComend(ByVal id As List(Of Decimal)) As Decimal
        <OperationContract()>
        Function GetCommend(ByVal _filter As CommendDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendDTO)
        <OperationContract()>
        Function GetEmployeeCommendByID(ByVal ComId As Decimal) As List(Of CommendEmpDTO)

        <OperationContract()>
        Function GetOrgCommendByID(ByVal ComId As Decimal) As List(Of CommendOrgDTO)

        <OperationContract()>
        Function GetCommendByID(ByVal _filter As CommendDTO) As CommendDTO

        <OperationContract()>
        Function InsertCommend(ByVal objCommend As CommendDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyCommend(ByVal objCommend As CommendDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateCommend(ByVal sType As String, ByVal obj As CommendDTO) As Boolean

        <OperationContract()>
        Function DeleteCommend(ByVal objAssetMng As CommendDTO) As Boolean

        <OperationContract()>
        Function ApproveCommend(ByVal objCommend As CommendDTO) As Boolean

        <OperationContract()>
        Function UnApproveCommend(ByVal objCommend As CommendDTO) As Boolean

        '<OperationContract()>
        'Function GetCommendCode(ByVal id As Decimal) As String

        <OperationContract()>
        Function ApproveListCommend(ByVal listID As List(Of Decimal), ByVal log As UserLog) As Boolean
        '<OperationContract()>
        'Function InsertImportCommend(ByVal lstImport As List(Of ImportCommendDTO), ByVal log As UserLog) As Boolean
        '<OperationContract()>
        'Function GetImportCommend(ByVal _filter As ImportCommendDTO) As List(Of ImportCommendDTO)
        '<OperationContract()>
        'Function GetCommendFormula(ByVal _filter As CommendFormulaDTO, ByVal PageIndex As Integer,
        '                                ByVal PageSize As Integer,
        '                                ByRef Total As Integer,
        '                                Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendFormulaDTO)
        '<OperationContract()>
        'Function GetCommendFormulaID(ByVal ID As Decimal) As CommendFormulaDTO
        '<OperationContract()>
        'Function InsertCommendFormula(ByVal objCommendFormula As CommendFormulaDTO,
        '                           ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        '<OperationContract()>
        'Function ModifyCommendFormula(ByVal objCommendFormula As CommendFormulaDTO,
        '                         ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        '<OperationContract()>
        'Function DeleteCommendFormula(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
        '<OperationContract()>
        'Function ActiveCommendFormula(ByVal lstID As List(Of Decimal), ByVal sActive As String,
        '                          ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetCommendCode(ByVal id As Decimal) As String
#End Region

#Region "Debt"
        <OperationContract()>
        Function GetDebt(ByVal empId As Decimal, ByVal PageIndex As Integer, ByVal PageSize As Integer, ByVal Total As Integer) As List(Of DebtDTO)
        <OperationContract()>
        Function InsertDebt(ByVal objDebt As DebtDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ModifyDebt(ByVal objDebt As DebtDTO, ByVal log As UserLog) As Boolean
#End Region
#Region "Terminate"
        <OperationContract()>
        Function GetCurrentPeriod(ByVal _year As Decimal) As DataTable
        <OperationContract()>
        Function Check_has_Ter(ByVal empid As Decimal) As Decimal
        <OperationContract()>
        Function ApproveListTerminate(ByVal listID As List(Of Decimal), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function CalculateTerminate(ByVal EmployeeId As Decimal, ByVal TerLateDate As Date) As DataTable

        <OperationContract()>
        Function GetLabourProtectByTerminate(ByVal gID As Decimal) As List(Of LabourProtectionMngDTO)

        <OperationContract()>
        Function GetAssetByTerminate(ByVal gID As Decimal) As List(Of AssetMngDTO)

        <OperationContract()>
        Function GetTerminate(ByVal _filter As TerminateDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of TerminateDTO)
        <OperationContract()>
        Function GetTerminateByID(ByVal _filter As TerminateDTO) As TerminateDTO

        <OperationContract()>
        Function GetEmployeeByID(ByVal gEmployeeID As Decimal) As EmployeeDTO

        <OperationContract()>
        Function InsertTerminate(ByVal objTerminate As TerminateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyTerminate(ByVal objTerminate As TerminateDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteTerminate(ByVal objID As Decimal, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function DeleteBlackList(ByVal objID As Decimal, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ApproveTerminate(ByVal objTerminate As TerminateDTO) As Boolean

        <OperationContract()>
        Function CheckTerminateNo(ByVal objTerminate As TerminateDTO) As Boolean

        <OperationContract()>
        Function GetTyleNV() As DataTable

        <OperationContract()>
        Function GetSalaryNew(ByRef P_EMPLOYEEID As Integer) As DataTable
#End Region

#Region "Terminate3b"
        <OperationContract()>
        Function GetTerminate3b(ByVal _filter As Terminate3BDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer, ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "CREATED_DATE desc",
                                 Optional ByVal log As UserLog = Nothing) As List(Of Terminate3BDTO)
        <OperationContract()>
        Function GetTerminate3bByID(ByVal _filter As Terminate3BDTO) As Terminate3BDTO

        <OperationContract()>
        Function GetTerminate3bEmployeeByID(ByVal gEmployeeID As Decimal) As EmployeeDTO

        <OperationContract()>
        Function InsertTerminate3b(ByVal objTerminate3b As Terminate3BDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyTerminate3b(ByVal objTerminate3b As Terminate3BDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteTerminate3b(ByVal objID As Decimal) As Boolean
        <OperationContract()>
        Function ApproveTerminate3b(ByVal objTerminate3b As Terminate3BDTO) As Boolean

        <OperationContract()>
        Function CheckExistApproveTerminate3b(ByVal gID As Decimal) As Boolean

#End Region

#Region "File Management, Tệp văn bản"
        <OperationContract()>
        Function InsertAttatch_Manager(ByVal fileInfo As HuFileDTO, ByVal fileBytes As Byte()) As Boolean
        <OperationContract()>
        Function UpdateAttatch_Manager(ByVal fileInfo As HuFileDTO, ByVal fileBytes As Byte()) As Boolean
        <OperationContract()>
        Function DeleteAttatch_Manager(ByVal fileID As List(Of Decimal)) As Boolean
        <OperationContract()>
        Function GetAttachFile_Manager(ByVal fileId As Decimal) As HuFileDTO
        <OperationContract()>
        Function GetAttachFiles_Manager(ByVal fileType As Decimal, ByVal page As Integer, ByVal pageSize As Integer, ByRef totalPage As Integer, ByVal Employee_id As Decimal) As List(Of HuFileDTO)
        <OperationContract()>
        Function DownloadAttachFile_Manager(ByVal fileID As Decimal, ByVal ext As String, ByRef fileInfo As HuFileDTO) As Byte()
        <OperationContract()>
        Function GetEmployeeHuFile(ByVal _filter As HuFileDTO) As List(Of HuFileDTO)
        <OperationContract()>
        Function TestEmployeeFileDTO() As EmployeeFileDTO

#End Region


#Region "IPORTAL - Quá trình đào tạo trước khi vào công ty"
        <OperationContract()>
        Function GetCertificateType() As List(Of OtherListDTO)
        <OperationContract()>
        Function GetProcessTrainingEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)

        <OperationContract()>
        Function InsertProcessTrainingEdit(ByVal objTitle As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyProcessTrainingEdit(ByVal objTitle As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteProcessTrainingEdit(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function CheckExistProcessTrainingEdit(ByVal pk_key As Decimal) As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT

        <OperationContract()>
        Function SendProcessTrainingEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function UpdateStatusProcessTrainingEdit(ByVal lstID As List(Of Decimal),
                                                   ByVal status As String,
                                                   ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetApproveProcessTrainingEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_ID desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)

#End Region

#Region "IPORTAL - Qúa trình công tác trước khi vào công ty"""
        <OperationContract()>
        Function GetWorkingBeforeEdit(ByVal _filter As WorkingBeforeDTOEdit) As List(Of WorkingBeforeDTOEdit)

        <OperationContract()>
        Function InsertWorkingBeforeEdit(ByVal objWorkingBefore As WorkingBeforeDTOEdit, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyWorkingBeforeEdit(ByVal objWorkingBefore As WorkingBeforeDTOEdit,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteWorkingBeforeEdit(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function CheckExistWorkingBeforeEdit(ByVal pk_key As Decimal) As WorkingBeforeDTOEdit

        <OperationContract()>
        Function SendWorkingBeforeEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function UpdateStatusWorkingBeforeEdit(ByVal lstID As List(Of Decimal),
                                                   ByVal status As String,
                                                   ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetApproveWorkingBeforeEdit(ByVal _filter As WorkingBeforeDTOEdit,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_ID desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of WorkingBeforeDTOEdit)

#End Region
#End Region

#Region "Hoadm"

#Region "Get Combo Data"
        ''' <summary>
        ''' Lấy dữ liệu cho combobox
        ''' </summary>
        ''' <param name="_combolistDTO">Trả về dữ liệu combobox</param>
        ''' <returns>TRUE: Success</returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetComboList(ByRef _combolistDTO As ComboBoxDataDTO) As Boolean
#End Region

#Region "Reminder"
        ''' <summary>
        ''' Lấy danh sách nhắc nhở
        ''' </summary>
        ''' <param name="_dayRemind"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetRemind(ByVal _dayRemind As String, ByVal log As UserLog) As List(Of ReminderLogDTO)

#End Region

#Region "Employee Proccess"
        ''' <summary>
        ''' Lấy danh sách nhân thân
        ''' </summary>
        ''' <param name="_empId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetFamily(ByVal _empId As Decimal) As List(Of FamilyDTO)

        ''' <summary>
        ''' Lấy quá trình công tác trước khi vào công ty
        ''' </summary>
        ''' <param name="_empId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetWorkingBefore(ByVal _empId As Decimal) As List(Of WorkingBeforeDTO)

        ''' <summary>
        ''' Lấy quá trình công tác trong công ty
        ''' </summary>
        ''' <param name="_empCode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetWorkingProccess(ByVal _empId As Decimal?,
                                       Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO)

        ''' <summary>
        ''' Lấy quá trình lương
        ''' </summary>
        ''' <param name="_empId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetSalaryProccess(ByVal _empId As Decimal,
                                       Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO)

        ''' <summary>
        ''' Lấy quá trình phúc lợi
        ''' </summary>
        ''' <param name="_empId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetWelfareProccess(ByVal _empId As Decimal) As List(Of WelfareMngDTO)

        ''' <summary>
        ''' Lấy quá trình hợp đồng
        ''' </summary>
        ''' <param name="_empId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetContractProccess(ByVal _empId As Decimal) As List(Of ContractDTO)

        ''' <summary>
        ''' Lấy quá trình khen thưởng
        ''' </summary>
        ''' <param name="_empId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' 
        <OperationContract()>
        Function GetCommendProccess(ByVal _empId As Decimal) As List(Of CommendDTO)
        ''' <summary>
        ''' Lấy quá trình kỷ luật
        ''' </summary>
        ''' <param name="_empId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetConcurrentlyProccess(ByVal _empId As Decimal) As List(Of TitleConcurrentDTO)
        <OperationContract()>
        Function GetDisciplineProccess(ByVal _empId As Decimal) As List(Of DisciplineDTO)
        ''' <summary>
        ''' Lấy quá trình bảo hiểm
        ''' </summary>
        ''' <param name="_empId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetInsuranceProccess(ByVal _empId As Decimal) As DataTable

        <OperationContract()>
        Function GetEmployeeHistory(ByVal _empId As Decimal) As DataTable

        ''' <summary>
        ''' Qua trinh danh gia KPI
        ''' </summary>
        ''' <param name="_empId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetAssessKPIEmployee(ByVal _empId As Decimal) As List(Of EmployeeAssessmentDTO)

        'qua trinh nang luc
        <OperationContract()>
        Function GetCompetencyEmployee(ByVal _empId As Decimal) As List(Of EmployeeCompetencyDTO)

        'qua trinh bac ngach
        <OperationContract()>
        Function GetSalaryChanged(ByVal _empId As Decimal) As DataTable
#End Region

#Region "Quá trình đào tạo ngoài công ty"
        <OperationContract()>
        Function GetProcessTraining(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTO,
                                      Optional ByRef PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Total As Integer = 0,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HU_PRO_TRAIN_OUT_COMPANYDTO)
        <OperationContract()>
        Function InsertProcessTraining(ByVal objHuPro As HU_PRO_TRAIN_OUT_COMPANYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyProcessTraining(ByVal objHuPro As HU_PRO_TRAIN_OUT_COMPANYDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteProcessTraining(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "Service Auto Update Employee Information"
        <OperationContract()>
        Function CheckAndUpdateEmployeeInformation() As Boolean
#End Region

#Region "Service Send Mail Reminder"
        <OperationContract()>
        Function CheckAndSendMailReminder() As Boolean
#End Region

#End Region
#Region "Manage annual leave plans (ALP)"
        <OperationContract()>
        Function GetListALPByEmpID(ByVal _filter As TrainningManageDTO, ByVal _param As ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningManageDTO)

        <OperationContract()>
        Function GetALP(ByVal _filter As TrainningManageDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of TrainningManageDTO)

        <OperationContract()>
        Function InsertALP(ByVal objContract As TrainningManageDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteALP(ByVal objAssetMng As TrainningManageDTO) As Boolean
        <OperationContract()>
        Function CheckEmployee_Exits(ByVal empCode As String) As Integer
        <OperationContract()>
        Function EffectDate_Check_Same(ByVal emp_code As String, ByVal effect_date As Date) As Boolean
        <OperationContract()>
        Function CheckEmployee_Contract_Count(ByVal empCode As String) As Integer
        <OperationContract()>
        Function CheckEmployee_EffectDate_exits(ByVal empCode As String, ByVal effect_date As String) As Integer
        <OperationContract()>
        Function GetEmpIdByCode(ByVal empCode As String) As EmployeeDTO
        <OperationContract()>
        Function CheckEmployee_Terminate(ByVal empCode As String) As Integer
        <OperationContract()>
        Function ImportAnnualLeave(ByVal P_DOCXML As String, ByVal P_USER As String, ByVal P_YEAR As Decimal) As Boolean
        <OperationContract()>
        Function GetALPByID(ByVal _filter As TrainningManageDTO) As TrainningManageDTO
        <OperationContract()>
        Function ModifyALP(ByVal objContract As TrainningManageDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

#End Region
#Region "Reports"

#Region "Dynamic"
        <OperationContract()>
        Function GetConditionColumn(ByVal _ConditionID As Decimal) As List(Of RptDynamicColumnDTO)
        <OperationContract()>
        Function GetListReportName(ByVal _ViewId As Decimal) As List(Of HuDynamicConditionDTO)
        <OperationContract()>
        Function DeleteDynamicReport(ByVal ID As Decimal, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function SaveDynamicReport(ByVal _report As HuDynamicConditionDTO, ByVal _col As List(Of HuConditionColDTO), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetDynamicReportList() As Dictionary(Of Decimal, String)
        ''' <summary>
        ''' Lấy danh sách các cột của DynamicReport
        ''' </summary>
        ''' <param name="_reportID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetDynamicReportColumn(ByVal _reportID As Decimal) As List(Of RptDynamicColumnDTO)

        ''' <summary>
        ''' Lấy dữ liệu báo cáo động
        ''' </summary>
        ''' <param name="column">Danh sách các cột cần lấy</param>
        ''' <param name="condition">Expression điều kiện</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetDynamicReport(ByVal _reportID As Decimal,
                                     ByVal orgID As Decimal,
                                     ByVal isDissolve As Decimal,
                                     ByVal chkTerminate As Decimal,
                                     ByVal chkHasTerminate As Decimal,
                                     ByVal column As List(Of String),
                                     ByVal condition As String,
                                     ByVal log As UserLog) As DataTable
#End Region


#End Region

#Region "Dashboard"
        ''' <summary>
        ''' Lấy danh sách loại thống kê nhân viên
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetListEmployeeStatistic() As List(Of OtherListDTO)

        ''' <summary>
        ''' Lấy danh sách loại thống kê biến động
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetListChangeStatistic() As List(Of OtherListDTO)

        ''' <summary>
        ''' Lấy nội dung thống kê nhân viên
        ''' </summary>
        ''' <param name="_type"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetEmployeeStatistic(ByVal _type As String, ByVal log As UserLog) As List(Of StatisticDTO)

        ''' <summary>
        ''' Lấy nội dung thống kê biến động
        ''' </summary>
        ''' <param name="_type"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <OperationContract()>
        Function GetChangeStatistic(ByVal _type As String, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetCompanyNewInfo(ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticSeniority(ByVal log As UserLog) As List(Of StatisticDTO)

#End Region

#Region "Hoadm"
        <OperationContract()>
        Function GetOrgFromUsername(ByVal username As String) As Decimal?

        <OperationContract()>
        Function GetLineManager(ByVal username As String) As List(Of EmployeeDTO)

#End Region

#Region "Danh mục bảo hộ lao động"

        <OperationContract()>
        Function GetLabourProtection(ByVal _filter As LabourProtectionDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of LabourProtectionDTO)
        <OperationContract()>
        Function InsertLabourProtection(ByVal objTitle As LabourProtectionDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ModifyLabourProtection(ByVal objTitle As LabourProtectionDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        <OperationContract()>
        Function ValidateLabourProtection(ByVal _validate As LabourProtectionDTO) As Boolean
        <OperationContract()>
        Function ActiveLabourProtection(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        <OperationContract()>
        Function DeleteLabourProtection(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GetLabourProtectionMng(ByVal _filter As LabourProtectionMngDTO, ByVal IsDissolve As Integer, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal UserLog As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of LabourProtectionMngDTO)
        <OperationContract()>
        Function GetLabourProtectionMngById(ByVal Id As Integer
                                        ) As LabourProtectionMngDTO
        <OperationContract()>
        Function InsertLabourProtectionMng(ByVal lstLabourProtectionMng As List(Of LabourProtectionMngDTO), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ModifyLabourProtectionMng(ByVal lstLabourProtectionMng As List(Of LabourProtectionMngDTO), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function DeleteLabourProtectionMng(ByVal objLabourProtectionMng() As LabourProtectionMngDTO, ByVal log As UserLog) As Boolean

#End Region

#Region "Quản lý an toàn lao động"
        <OperationContract()>
        Function GetOccupationalSafety(ByVal _filter As OccupationalSafetyDTO, ByVal IsDissolve As Integer, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal UserLog As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of OccupationalSafetyDTO)
        <OperationContract()>
        Function GetOccupationalSafetyById(ByVal Id As Integer
                                       ) As OccupationalSafetyDTO
        <OperationContract()>
        Function InsertOccupationalSafety(ByVal lstOccupationalSafety As OccupationalSafetyDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ModifyOccupationalSafety(ByVal lstOccupationalSafety As OccupationalSafetyDTO, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function DeleteOccupationalSafety(ByVal objOccupationalSafety() As OccupationalSafetyDTO, ByVal log As UserLog) As Boolean
#End Region

#Region "Danh mục thông tin lương"
        <OperationContract()>
        Function GetPeriodbyYear(ByVal year As Decimal) As List(Of ATPeriodDTO)

#End Region

#Region "BÁO CÁO"
        <OperationContract()>
        Function GetReportById(ByVal _filter As Se_ReportDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CODE ASC") As List(Of Se_ReportDTO)

        <OperationContract()>
        Function ProfileReport(ByVal sPkgName As String, ByVal sStartDate As Date?, ByVal sEndDate As Date?, ByVal sOrg As Integer, ByVal sUserName As String, ByVal sLang As String) As DataTable

        <OperationContract()>
        Function ExportReport(ByVal sPkgName As String,
                              ByVal sStartDate As Date?,
                              ByVal sEndDate As Date?,
                              ByVal sOrg As String,
                              ByVal IsDissolve As Integer,
                              ByVal sUserName As String, ByVal sLang As String) As DataSet

        <OperationContract()>
        Function GetEmployeeCVByID(ByVal sPkgName As String, ByVal sEmployee_id As String) As DataSet

#End Region
#Region "DM Khen thưởng (CommendList)"
        <OperationContract()>
        Function GetCommendList(ByVal _filter As CommendListDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendListDTO)

        <OperationContract()>
        Function GetCommendListID(ByVal ID As Decimal) As List(Of CommendListDTO)

        <OperationContract()>
        Function GetListCommendList(ByVal actflg As String) As List(Of CommendListDTO)

        <OperationContract()>
        Function InsertCommendList(ByVal objCommendList As CommendListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyCommendList(ByVal objCommendList As CommendListDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCommendList(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                  ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteCommendList(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean

        <OperationContract()>
        Function ValidateCommendList(ByVal _validate As CommendListDTO)

        <OperationContract()>
        Function InsertImportCommend(ByVal lstImport As List(Of ImportCommendDTO), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetImportCommend(ByVal _filter As ImportCommendDTO) As List(Of ImportCommendDTO)
#End Region
#Region "Commend_Level - Cấp khen thưởng"
        <OperationContract()>
        Function GetCommendLevel(ByVal _filter As CommendLevelDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendLevelDTO)
        <OperationContract()>
        Function GetCommendLevelID(ByVal ID As Decimal) As CommendLevelDTO

        <OperationContract()>
        Function InsertCommendLevel(ByVal objCommendLevel As CommendLevelDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ValidateCommendLevel(ByVal _validate As CommendLevelDTO)

        <OperationContract()>
        Function ModifyCommendLevel(ByVal objCommendLevel As CommendLevelDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCommendLevel(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteCommendLevel(ByVal lstID As List(Of Decimal)) As Boolean
#End Region

#Region "CommendFormula - Công thức khen thưởng"
        <OperationContract()>
        Function GetCommendFormula(ByVal _filter As CommendFormulaDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CommendFormulaDTO)

        <OperationContract()>
        Function GetCommendFormulaID(ByVal ID As Decimal) As CommendFormulaDTO

        <OperationContract()>
        Function InsertCommendFormula(ByVal objCommendFormula As CommendFormulaDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyCommendFormula(ByVal objCommendFormula As CommendFormulaDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ActiveCommendFormula(ByVal lstID As List(Of Decimal), ByVal sActive As String,
                                  ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function DeleteCommendFormula(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog, ByRef strError As String) As Boolean
#End Region

#Region "Competency Dashboard"
        <OperationContract()>
        Function GetStatisticTop5Competency(ByVal _year As Integer, ByVal log As UserLog) As List(Of StatisticDTO)
        <OperationContract()>
        Function GetStatisticTop5CopAvg(ByVal _year As Integer, ByVal log As UserLog) As List(Of CompetencyAvgEmplDTO)
#End Region
#Region "Portal Dashboard"
        <OperationContract()>
        Function GetEmployeeReg(ByVal _employee_id As Integer) As DataTable
        <OperationContract()>
        Function GetTotalDayOff(ByVal _filter As TotalDayOffDTO,
                              Optional ByVal log As UserLog = Nothing) As TotalDayOffDTO
        <OperationContract()>
        Function GetCertificateExpires(ByVal _employee_id As Integer) As DataTable
#End Region
#Region "Competency Course"

        <OperationContract()>
        Function GetCompetencyCourse(ByVal _filter As CompetencyCourseDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of CompetencyCourseDTO)

        <OperationContract()>
        Function InsertCompetencyCourse(ByVal objCompetencyCourse As CompetencyCourseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyCompetencyCourse(ByVal objCompetencyCourse As CompetencyCourseDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteCompetencyCourse(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetCourseByList(ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
#End Region

#Region "EmployeeCriteriaRecord"
        <OperationContract()>
        Function EmployeeCriteriaRecord(ByVal _filter As EmployeeCriteriaRecordDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of EmployeeCriteriaRecordDTO)
#End Region
#Region "Portal Xem diem, chuc nang tuong ung voi khoa hoc"
        <OperationContract()>
        Function GetPortalCompetencyCourse(ByVal _empId As Decimal) As List(Of EmployeeCriteriaRecordDTO)
#End Region

#Region "Tìm kiếm kế nhiệm (Talent Pool)"

        <OperationContract()>
        Function GetTalentPool(ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer,
                                ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of TalentPoolDTO)

        <OperationContract()>
        Function InsertTalentPool(ByVal lstTalentPool As List(Of TalentPoolDTO), ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function ActiveTalentPool(ByVal objTalentPool As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function FILTER_TALENT_POOL(ByVal obj As FilterParamDTO, ByVal log As UserLog) As DataTable
#End Region

#Region "in phu luc"
        <OperationContract()>
        Function PrintFileContract(ByVal emp_code As String, ByVal fileContract_ID As String) As DataTable

        <OperationContract()>
        Function GetContractForm(ByVal formID As Decimal) As OtherListDTO

        <OperationContract()>
        Function GetFileConTractID(ByVal ID As Decimal) As FileContractDTO

        <OperationContract()>
        Function GetContractAppendixPaging(ByVal _filter As FileContractDTO, ByVal PageIndex As Integer,
                                                ByVal PageSize As Integer,
                                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                                Optional ByVal log As UserLog = Nothing) As List(Of FileContractDTO)

        <OperationContract()>
        Function DeleteFileContract(ByVal objContract As FileContractDTO, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetListContractType(ByVal type As String) As List(Of ContractTypeDTO)

        <OperationContract()>
        Function CheckExpireFileContract(ByVal StartDate As Date, ByVal EndDate As Date, ByVal ID As Decimal) As Boolean

        <OperationContract()>
        Function CheckExistFileContract(ByVal empID As Decimal, ByVal StartDate As Date, ByVal type_id As Decimal) As Boolean

        <OperationContract()>
        Function InsertFileContract(ByVal FileInfo As FileContractDTO, ByVal log As UserLog, ByRef gID As Decimal, ByRef appenNum As String) As Boolean

        <OperationContract()>
        Function UpdateFileContract(ByVal FileInfo As FileContractDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function GetContractList(ByVal empID As Decimal) As List(Of ContractDTO)

        <OperationContract()>
        Function GetOrgList(ByVal ID As Decimal) As String

        <OperationContract()>
        Function GetContractTypeCT(ByVal ID As Decimal) As String

        <OperationContract()>
        Function GetTitileBaseOnEmp(ByVal ID As Decimal) As TitleDTO

        <OperationContract()>
        Function GetFileContract_No(ByVal Contract As ContractDTO, ByRef STT As Decimal) As String

        <OperationContract()>
        Function GetContractAppendix(ByVal _filter As FileContractDTO) As List(Of FileContractDTO)

        <OperationContract()>
        Function GetContractTypeID(ByVal ID As Decimal) As ContractTypeDTO

        <OperationContract()>
        Function GetListContractBaseOnEmp(ByVal ID As Decimal) As List(Of ContractDTO)

        <OperationContract()>
        Function GetListContract(ByVal ID As Decimal) As DataTable

        <OperationContract()>
        Function ApproveListContract(ByVal listID As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GetContractImport() As DataSet
#End Region
#Region "Danh mục người ký"
        <OperationContract()>
        Function GET_HU_SIGNER(ByVal _filter As SignerDTO,
                                  ByVal _param As ParamDTO,
                                   ByVal log As UserLog) As DataTable
        <OperationContract()>
        Function INSERT_HU_SIGNER(ByVal PA As SignerDTO) As Boolean
        <OperationContract()>
        Function UPDATE_HU_SIGNER(ByVal PA As SignerDTO) As Boolean
        <OperationContract()>
        Function CHECK_EXIT(ByVal P_ID As String, ByVal idemp As Decimal, ByVal ORG_ID As Decimal, ByVal title_name As String) As Decimal
        <OperationContract()>
        Function DeactiveAndActiveSigner(ByVal lstID As String, ByVal sActive As Decimal)
        <OperationContract()>
        Function DeleteSigner(ByVal lstID As String)

#End Region
#Region " Quan ly suc khoe"
        <OperationContract()>
        Function GET_HEALTH_MNG_LIST(ByVal _filter As HealthMngDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer, ByVal log As UserLog, ByVal _param As ParamDTO,
                                     Optional ByVal Sorts As String = "CREATED_DATE desc") As DataTable

        <OperationContract()>
        Function Import_Health_Mng(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean

        <OperationContract()>
        Function EXPORT_HEALTH_MNG() As DataSet
        <OperationContract()>
        Function Delete_Health_Mng(ByVal lstHealthMng() As HealthMngDTO,
                                   ByVal log As UserLog) As Boolean
#End Region


#Region "QL kiêm nhiệm"
        <OperationContract()>
        Function GET_LIST_CONCURRENTLY(ByVal _filter As Temp_ConcurrentlyDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of Temp_ConcurrentlyDTO)
        <OperationContract()>
        Function GET_LIST_CONCURRENTLY_BY_ID(ByVal _filter As Temp_ConcurrentlyDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal log As UserLog,
                                        ByVal EMPLOYEE_ID As Decimal,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of Temp_ConcurrentlyDTO)
        <OperationContract()>
        Function GET_CONCURRENTLY_BY_ID(ByVal P_ID As Decimal) As DataTable

        <OperationContract()>
        Function INSERT_CONCURRENTLY(ByVal concurrently As Temp_ConcurrentlyDTO) As Integer

        <OperationContract()>
        Function UPDATE_CONCURRENTLY(ByVal concurrently As Temp_ConcurrentlyDTO) As Integer

        <OperationContract()>
        Function GET_CONCURRENTLY_BY_EMP(ByVal P_ID As Decimal) As DataTable

        <OperationContract()>
        Function GET_TITLE_ORG(ByVal P_ID As Decimal) As DataTable

        <OperationContract()>
        Function INSERT_EMPLOYEE_KN(ByVal P_EMPLOYEE_CODE As String,
                                       ByVal P_ORG_ID As Decimal,
                                       ByVal P_TITLE As Decimal,
                                       ByVal P_DATE As Date,
                                       ByVal P_ID_KN As Decimal) As Boolean

        <OperationContract()>
        Function UPDATE_EMPLOYEE_KN(ByVal P_ID_KN As Decimal,
                                       ByVal P_DATE As Date) As Boolean

        <OperationContract()>
        Function ApproveListChangeCon(ByVal listID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function DeleteConcurrentlyByID(ByVal listID As List(Of Decimal)) As Boolean

        <OperationContract()>
        Function GET_WORK_POSITION_LIST() As DataTable

        <OperationContract()>
        Function GET_ORG_INFOR_PART(ByVal ID As Decimal) As DataTable
#End Region

        <OperationContract()>
        Function GetOrganizationTreeByID(ByVal _filter As OrganizationTreeDTO) As OrganizationTreeDTO

        <OperationContract()>
        Function GetOrgtree(ByVal _org_id As Decimal) As DataTable

        <OperationContract()>
        Function GET_PROCESS_PLCONTRACT(ByVal P_EMP_CODE As String) As DataTable
        <OperationContract()>
        Function EXPORT_PLHD(ByVal _param As ParamDTO, ByVal log As UserLog) As DataSet

        <OperationContract()>
        Function CHECK_EMPLOYEE(ByVal P_EMP_CODE As String) As Integer
        <OperationContract()>
        Function CHECK_CONTRACT(ByVal P_ID As Decimal) As Integer
        <OperationContract()>
        Function CHECK_SALARY(ByVal P_ID As Decimal) As Integer
        <OperationContract()>
        Function CHECK_CONTRACT_EXITS(ByVal P_CONTRACT As Decimal, ByVal P_EMP_CODE As String, ByVal P_DATE As Date) As Integer
        <OperationContract()>
        Function CHECK_SIGN(ByVal P_EMP_CODE As String) As Integer
        <OperationContract()>
        Function INPORT_PLHD(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function EXPORT_QLKT() As DataSet
        <OperationContract()>
        Function INPORT_QLKT(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        <OperationContract()>
        Function GET_EMPLOYEE(ByVal P_EMP_CODE As String) As DataTable
        <OperationContract()>
        Function GET_PROCESS_PLCONTRACT_PORTAL(ByVal P_EMP_ID As Decimal) As DataTable

        <OperationContract()>
        Function CHECK_LOCATION_EXITS(ByVal P_ID As Decimal?, ByVal ORG_ID As Decimal) As Boolean

        <OperationContract()>
        Function GetJobPosition(ByVal _filter As Job_PositionDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of Job_PositionDTO)
        <OperationContract()>
        Function GET_JOB_POSITION_LIST(ByVal P_ORG_ID As Decimal, ByVal P_ID As Decimal) As DataTable

        <OperationContract()>
        Function GET_JOB_POSITION_DETAIL(ByVal P_ID As Decimal) As DataSet

        <OperationContract()>
        Function INSERT_JOB_POSITION(ByVal p_CODE As String,
                                        ByVal p_JOB_NAME As String,
                                        ByVal p_ORG_ID As Decimal,
                                        ByVal p_TITLE_ID As Decimal,
                                        ByVal p_JOB_NOTE As String,
                                        ByVal p_COST_CODE As String,
                                        ByVal p_IS_LEADER As Decimal,
                                        ByVal p_EFFECT_DATE As Date,
                                        Optional ByVal log As UserLog = Nothing) As Integer
        <OperationContract()>
        Function UPDATE_JOB_POSITION(ByVal p_ID As Decimal, ByVal p_CODE As String,
                                        ByVal p_JOB_NAME As String,
                                        ByVal p_ORG_ID As Decimal,
                                        ByVal p_TITLE_ID As Decimal,
                                        ByVal p_JOB_NOTE As String,
                                        ByVal p_COST_CODE As String,
                                        ByVal p_IS_LEADER As Decimal,
                                        ByVal p_EFFECT_DATE As Date,
                                        Optional ByVal log As UserLog = Nothing) As Integer

        <OperationContract()>
        Function INSERT_DIRECT_MANAGER(ByVal P_JOB_POSITION_ID As Decimal, ByVal P_DIRECT_MANAGER As String) As Boolean

        <OperationContract()>
        Function DeleteJob(ByVal objOrgTitle As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function ActiveJob(ByVal objOrgTitle As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function GET_JP_TO_TITLE(ByVal P_ORG_ID As Decimal, ByVal P_TITLE_ID As Decimal, ByVal P_IS_THAYTHE As Decimal, ByVal P_JOB As Decimal) As DataSet

        <OperationContract()>
        Function UPDATE_END_DATE_QD(ByVal P_EMP_ID As Decimal, ByVal P_DATE As Date) As Boolean

        <OperationContract()>
        Function UpdateStatusQD(ByVal lstID As List(Of Decimal),
                               ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function UpdateStatusTer(ByVal lstID As List(Of Decimal),
                               ByVal log As UserLog) As Boolean
        <OperationContract()>
        Function GET_HU_ASSET(ByVal P_EMP_ID As Decimal) As DataTable

        <OperationContract()>
        Function GET_JOB_EMP(ByVal P_EMP_ID As Decimal) As Integer

        <OperationContract()>
        Function GET_JOB_CODE_AUTO(ByVal p_TITLE_ID As Decimal) As String

        <OperationContract()>
        Function INSERT_JOB_POSITION_AUTO(ByVal p_ID As Decimal,
                                       ByVal p_TITLE_ID As Decimal,
                                       ByVal p_NUMBER As Decimal,
                                       Optional ByVal log As UserLog = Nothing) As Boolean

        <OperationContract()>
        Function CHECK_EXITS_JOB(ByVal P_JOB_ID As Decimal, ByVal P_EMP_ID As Decimal) As Integer

        <OperationContract()>
        Function EXPORT_EMP(ByVal P_USERNAME As String, ByVal P_ORGID As Decimal, ByVal P_ISDISSOLVE As Boolean, ByVal P_STARTDATE As Date?, ByVal P_TODATE As Date?, ByVal P_IS_ALL As Boolean) As DataSet

        <OperationContract()>
        Function INPORT_EMP(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean

        <OperationContract()>
        Function CHECK_IS_THHDLD(ByVal P_ID As Decimal, ByVal P_EMP_ID As Decimal, ByVal P_DATE As Date?) As Integer

        <OperationContract()>
        Function GetEmployeeDeduct(ByVal _filter As DeductDTO) As List(Of DeductDTO)

        <OperationContract()>
        Function InsertEmployeeDeduct(ByVal objFamily As DeductDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function ModifyEmployeeDeduct(ByVal objFamily As DeductDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        <OperationContract()>
        Function DeleteEmployeeDeduct(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean

        <OperationContract()>
        Function EXPORT_DISCIPLINE() As DataSet

        <OperationContract()>
        Function INPORT_DISCIPLINE(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
    End Interface

End Namespace

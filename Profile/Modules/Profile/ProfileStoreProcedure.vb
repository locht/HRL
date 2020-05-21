Imports Common
Imports Common.CommonBusiness
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities

Public Class ProfileStoreProcedure
    Public ReadOnly Property Log As UserLog
        Get
            Return LogHelper.GetUserLog
        End Get
    End Property
#Region "ChienNV"
    ''' <summary>
    ''' Create by: ChienNV; Create date:11/10/2017; Lấy danh sách đơn vị đóng bảo hiểm.
    ''' pStruct ="1" lấy dữ liệu , pStruct ="0" chỉ lấy cấu trúc của table
    ''' </summary>
    ''' <param name="pStruct"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListInsurance(Optional ByVal pStruct As String = "1") As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_COMMON_LIST.GET_LIST_INSURANCE", New List(Of Object)(New Object() {pStruct}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GetInsListRegion(Optional ByVal pStruct As String = "1") As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_COMMON_LIST.GET_LIST_INS_REGION", New List(Of Object)(New Object() {pStruct}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_ORG_LEVEL(Optional ByVal P_STRUCT As String = "1") As DataTable
        Try
            Dim dt As New DataTable
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_COMMON_LIST.GET_ORG_LEVEL", New List(Of Object)(New Object() {P_STRUCT}))
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GET_ALL_BRANCH_ORGLEVEL(ByVal P_ORGID As Decimal) As DataTable
        Try
            Dim dt As New DataTable
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_COMMON_LIST.GET_ALL_BRANCH_ORGLEVEL", New List(Of Object)(New Object() {P_ORGID}))
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CREATE_NEW_EMPCODE() As DataTable
        Try
            Dim dt As New DataTable
            Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_COMMON_LIST.CREATE_NEW_EMPCODE", Nothing)
            If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
    Public Function INSERT_INFO_REMINDER_DETAIL(ByVal username As String) As Int32
        Dim objects = hfr.ExecuteStoreScalar("PKG_PROFILE_DASHBOARD.INSERT_INFO_REMINDER_DETAIL",
                                             New List(Of Object)(New Object() {username, OUT_NUMBER}))
        Return Int32.Parse(objects(0).ToString())
    End Function

    Public Function DELETE_INFO_REMINDER(ByVal P_ID As String) As Int32
        Dim objects = hfr.ExecuteStoreScalar("PKG_PROFILE_DASHBOARD.DELETE_INFO_REMINDER",
                                             New List(Of Object)(New Object() {P_ID, OUT_NUMBER}))
        Return Int32.Parse(objects(0).ToString())
    End Function
    Public Function GET_DECISION_TYPE_EXCEPT_NV(Optional ByVal isBlank As Boolean = False) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.GET_DECISION_TYPE_EXCEPT_NV", New List(Of Object)(New Object() {isBlank, Common.Common.SystemLanguage.Name}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function get_org_name_c2(ByVal p_emp_id As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE_LIST.get_org_name_c2", New List(Of Object)(New Object() {p_emp_id}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function Get_list_Welfare_EMP(ByVal p_welfare_id As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE_LIST.GetlistWelfareEMP", New List(Of Object)(New Object() {p_welfare_id}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function Get_list_SafeLabor_EMP(ByVal p_safelabor_id As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE_LIST.GetlistSafeLaborEMP", New List(Of Object)(New Object() {p_safelabor_id}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GetTitle(ByVal titleId As Int32) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.HU_GET_TITLE_BY_ID", New List(Of Object)(New Object() {titleId}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GetTitleName(ByVal title_id As Int32) As String
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.HU_GET_TITLE_BY_ID", New List(Of Object)(New Object() {title_id}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
            If (dt.Rows.Count > 0) Then
                Return dt.Rows(0)(1)
            End If
        End If
        Return ""
    End Function

    Public Function CHECKEXIST_ALLOWANCE(ByVal EMP_ID As Decimal, ByVal ALLOWANCE_ID As Decimal, ByVal EFFECT_DATE As Date) As Int16
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE_BUSINESS.CHECKEXIST_ALLOWANCE", New List(Of Object)(New Object() {EMP_ID, ALLOWANCE_ID, EFFECT_DATE}))
        If Not ds Is Nothing AndAlso Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
            If (dt.Rows.Count > 0) Then
                Return dt.Rows(0)(0)
            End If
        End If
    End Function

    Public Function get_current_work_history(ByVal p_empid As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.GET_CURRENT_WORK_HISTORY", New List(Of Object)(New Object() {p_empid}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GET_CURRENT_SALARY_HISTORY(ByVal p_empid As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.GET_CURRENT_SALARY_HISTORY", New List(Of Object)(New Object() {p_empid}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    ''' <summary>
    ''' Phê duyệt hợp đồng hàng loạt
    ''' </summary>
    ''' <param name="dtb"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function BatchApprovedContract(ByVal dtb As DataTable) As Integer
        Try
            Return hfr.ExecuteBatchCommand("PKG_HU_CONTRACT.APPROVED_CONTRACT", dtb)
        Catch ex As Exception

        End Try
    End Function
    ''' <summary>
    ''' Phê duyệt phụ lục hợp đồng hàng loạt
    ''' </summary>
    ''' <param name="dtb"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function BatchApprovedListContract(ByVal dtb As DataTable) As Integer
        Try
            Return hfr.ExecuteBatchCommand("PKG_PROFILE.APPROVED_FILECONTRACT", dtb)
        Catch ex As Exception

        End Try
    End Function
    Public Function Print_Decision(ByVal empID As Decimal, ByVal ID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PRINT_REPORT.PRINT_DECISION", New List(Of Object)(New Object() {empID, ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function Print_Contract(ByVal emp_code As String, ByVal contract_NO As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PRINT_REPORT.RP_IPROFILE_HDLD", New List(Of Object)(New Object() {emp_code, contract_NO}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function Print_FileContract(ByVal emp_code As String, ByVal fileContract_ID As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PRINT_REPORT.RP_IPROFILE_PLHDLD", New List(Of Object)(New Object() {emp_code, fileContract_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function Print_Terminate(ByVal id As Decimal, ByVal form_id As Decimal) As DataSet
        'Dim dt As New DataSet
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PRINT_REPORT.PRINT_TERMINATE", New List(Of Object)(New Object() {id, form_id}))
        If Not ds Is Nothing Or Not ds.Tables(1) Is Nothing Then
            ds.Tables(1).TableName = "DT1"
            ds.Tables(0).TableName = "DT"
        End If
        Return ds
    End Function

    Public Function Get_Decision_Type(ByVal isBlank As Boolean) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.GET_DECISION_TYPE", New List(Of Object)(New Object() {isBlank}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function Get_Decision_Terminate(ByVal isBlank As Boolean) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.GET_DECISION_TERMINATE", New List(Of Object)(New Object() {isBlank}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function PRINT_DISCIPLINE(ByVal empID As Decimal, ByVal ID As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PRINT_REPORT.PRINT_DISCIPLINE", New List(Of Object)(New Object() {empID, ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function CHECK_WELFARE(ByVal Name As String, ByVal is_Edit As Integer, ByVal org_id As Decimal, ByVal sdate As Date) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.CHECK_WELFARE", New List(Of Object)(New Object() {Name, is_Edit, org_id, sdate}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

#Region "Commend"
    Public Function Get_Commend_Period(ByVal isBlank As Boolean, ByVal year As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_COMMEND.GET_PERIOD_COMMEND", New List(Of Object)(New Object() {isBlank, year}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function Get_Commend_PowerPay(ByVal isBlank As Boolean) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_COMMEND.GET_POWERPAY_COMMEND", New List(Of Object)(New Object() {isBlank}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function Get_Commend_List(ByVal isBlank As Boolean) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_COMMEND.GET_LIST_COMMEND", New List(Of Object)(New Object() {isBlank}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function Get_Commend_Level(ByVal isBlank As Boolean) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_COMMEND.GET_LEVEL_COMMEND", New List(Of Object)(New Object() {isBlank}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function Get_Year_Of_Period(ByVal isBlank As Boolean) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_COMMON_LIST.GET_YEAR_OF_PERIOD", New List(Of Object)(New Object() {isBlank}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function Get_Commend_Formality(ByVal isBlank As Boolean, ByVal OBJ As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_COMMEND.GET_FORMALITY_COMMEND", New List(Of Object)(New Object() {isBlank, OBJ}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function Get_Commend_Title(ByVal isBlank As Boolean, ByVal OBJ As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_COMMEND.GET_TITLE_COMMEND", New List(Of Object)(New Object() {isBlank, OBJ}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function Get_PAY_POWER(ByVal isBlank As Boolean, ByVal year As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_COMMEND.GET_PAY_POWER", New List(Of Object)(New Object() {isBlank, year}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    ''' <summary>
    ''' Load dữ liệu khen thuong đã Imported
    ''' </summary>
    ''' <param name="userName">Lấy nhân viên được phân quyền theo UserName</param>
    ''' <param name="orgID">Org ID</param>
    ''' <returns>Danh sách khen thuong đã import</returns>
    ''' <remarks></remarks>
    Public Function ReadDataForGridViewDataImported(ByVal userName As String, ByVal orgID As Integer, ByVal commendDate As Date, ByVal index As Integer, ByVal obj As Integer) As DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_COMMEND.READ_DATA_IMPORTED", New List(Of Object)({userName, orgID, commendDate, index, obj}))
        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            Return ds.Tables(0)
        Else
            Dim dtb As New DataTable
            dtb.Columns.Add("EMPLOYEE_ID", GetType(Integer))
            dtb.Columns.Add("FULLNAME_CODE", GetType(String))
            dtb.Columns.Add("FULLNAME_VN", GetType(String))
            dtb.Columns.Add("ORG_ID", GetType(Integer))
            dtb.Columns.Add("ORG_NAME", GetType(String))
            Return dtb
        End If
    End Function
    Public Function CreateDataSalaryImportToDatabase(ByVal dtb As DataTable) As Boolean
        Dim rowInsert As Integer = dtb.Rows.Count
        Dim result As Integer = 0

        result = hfr.ExecuteBatchCommand(" " + "CREATE_HU_IMPORT_COMMEND", dtb)

        If rowInsert = result Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Function GET_COMMEND_LIST_IMPORT(ByVal OBJ As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_COMMEND.GET_COMMEND_LIST_IMPORT", New List(Of Object)(New Object() {OBJ}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

#End Region

#Region "Xét duyệt khen thưởng - THANHNT 25/08/2016"

    Public Function ReadDataCommendImported(ByVal orgID As Integer, ByVal userName As String, ByVal commendType As Decimal, ByVal DateReview As String,
                                            ByVal isAll As Decimal) As DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_COMMEND_CALCULATE.READ_COMMEND_CALCULATED", New List(Of Object)(New Object() {orgID, userName, commendType, DateReview, isAll}))
        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            Return ds.Tables(0)
        Else
            Return Nothing
        End If
    End Function

    Public Function ReadListCommendDate(ByVal orgID As Integer, ByVal userName As String, ByVal obj_id As Decimal) As DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_COMMEND_CALCULATE.READ_LIST_COMMEND_DATE", New List(Of Object)(New Object() {orgID, userName, obj_id}))
        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            Return ds.Tables(0)
        Else
            Return Nothing
        End If
    End Function

    'Public Function CalculateCommend(ByVal orgID As Integer, ByVal userName As String, ByVal year As Decimal, ByVal CommendType As Decimal) As Boolean
    '    Dim obj = hfr.ExecuteStoreScalar("PKG_HU_COMMEND_CALCULATE.CALCULATED_COMMEND", New List(Of Object)(New Object() {orgID, userName, year, CommendType, HistaffFrameworkPublic.FrameworkUtilities.OUT_NUMBER}))
    '    If obj Is Nothing Then
    '        Return If(Decimal.Parse(obj(0).ToString()) = 1, True, False)
    '    Else
    '        Return False
    '    End If
    'End Function

#End Region

#Region "Organization"

    ''' <summary>
    ''' Lấy danh sách Sơ đồ tổ chức có cấp Công ty
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GET_ORGID_COMPANY_LEVEL() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.GET_ORGID_COMPANY_LEVEL", New List(Of Object)(New Object() {}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GET_ORGID_COMPANY_LEVEL_USER_ID(ByVal user_id As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.GET_ORGID_COMPANY_LEVEL_USER_ID", New List(Of Object)(New Object() {user_id}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    ''' <summary>
    ''' Lấy danh sách Loại tổ chức
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GET_ORG_TYPE() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.GET_ORG_TYPE", New List(Of Object)(New Object() {}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    Public Function GET_NGACH_LUONG_ACV() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.GET_NGACH_LUONG_ACV", New List(Of Object)(New Object() {}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GET_HEALTH_BY_ID(ByVal empid As Decimal) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE_BUSINESS.GET_HEALTH_BY_ID", New List(Of Object)(New Object() {empid}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function Import_HU_TITLE(ByVal P_USER As String, ByVal P_DOCXML As String) As Boolean
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_HU_IPROFILE.Import_HU_TITLE", New List(Of Object)(New Object() {P_USER, P_DOCXML}))
        If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
            Return CBool(ds.Tables(0)(0)(0))
        End If
    End Function
    Public Function Import_Working(ByVal P_USER As String, ByVal P_DOCXML As String) As Boolean
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE_INTEGRATED.Import_Working", New List(Of Object)(New Object() {P_USER, P_DOCXML}))
        If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
            Return CBool(ds.Tables(0)(0)(0))
        End If
    End Function
    Public Function Import_HU_ALLOWANCE(ByVal P_USER As String, ByVal P_DOCXML As String) As Boolean
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PA_BUSINESS.Import_HU_ALLOWANCE", New List(Of Object)(New Object() {P_USER, P_DOCXML}))
        If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
            Return CBool(ds.Tables(0)(0)(0))
        End If
    End Function
    Public Function GET_ID_EMP(ByVal P_CODE As String) As Int32
        Dim objects = hfr.ExecuteStoreScalar("PKG_PA_BUSINESS.GET_ID_EMP",
                                             New List(Of Object)(New Object() {P_CODE, OUT_NUMBER}))
        Return Int32.Parse(objects(0).ToString())
    End Function
    Public Function CHECK_EXIT_HU_TITLE(ByVal P_CODE As String) As Int32
        Dim objects = hfr.ExecuteStoreScalar("PKG_HU_IPROFILE.CHECK_EXIT_HU_TITLE",
                                             New List(Of Object)(New Object() {P_CODE, OUT_NUMBER}))
        Return Int32.Parse(objects(0).ToString())
    End Function
#Region "baolc"

    Public Function GetAllDistrict() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_COMMON_LIST.GetAllDistrict", New List(Of Object)(New Object() {}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GETALLHU_BANK_BRANCH() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_COMMON_LIST.GETALLHU_BANK_BRANCH", New List(Of Object)(New Object() {}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GETORGANIZATION_NEW(Optional ByVal P_ACTFLG As String = "") As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.GETORGANIZATION_NEW", New List(Of Object)(New Object() {P_ACTFLG}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function ADDNEW_ORGANIZATION(ByVal r As HU_ORGANIZATION_NEW) As Int32
        Dim t_param = New List(Of Object)(New Object() {
                                                   r.CODE,
                                                   r.NAME_EN,
                                                   r.NAME_VN,
                                                   r.PARENT_ID,
                                                   r.DISSOLVE_DATE,
                                                   r.FOUNDATION_DATE,
                                                   r.REMARK,
                                                   r.ADDRESS,
                                                   r.FAX,
                                                   r.REPRESENTATIVE_ID,
                                                   r.ORD_NO,
                                                   r.NUMBER_BUSINESS,
                                                   r.PIT_NO,
                                                   r.U_INSURANCE,
                                                   r.REGION_ID,
                                                   r.SHORT_NAME,
                                                   r.IS_SIGN_CONTRACT,
                                                   r.CONTRACT_CODE,
                                                   r.GROUP_PAID_ID,
                                                   r.UNIT_RANK_ID,
                                                   r.PROVINCE_ID,
                                                   r.DISTRICT_ID,
                                                   r.WEBSITE_LINK,
                                                   r.BANK_NO,
                                                   r.BANK_ID,
                                                   r.BANK_BRACH_ID,
                                                   r.ACCOUNTING_ID,
                                                   r.HR_ID,
                                                   r.PROVINCE_CONTRACT_ID,
                                                   r.DISTRICT_CONTRACT_ID,
                                                   r.AUTHOR_LETTER,
                                                   r.BUSS_REG_NAME,
                                                   r.MAN_UNI_NAME,
                                                   r.UNDER_ID,
                                                   Me.Log.Username,
                                                   OUT_NUMBER
                                                   })
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_PROFILE.ADDNEW_ORGANIZATION", t_param)
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function UPDATE_ORGANIZATION_NEW(ByVal r As HU_ORGANIZATION_NEW) As Int32
        Dim t_param = New List(Of Object)(New Object() {
                                        r.ID,
                                        r.CODE,
                                        r.NAME_EN,
                                        r.NAME_VN,
                                        r.PARENT_ID,
                                        r.DISSOLVE_DATE,
                                        r.FOUNDATION_DATE,
                                        r.REMARK,
                                        r.ADDRESS,
                                        r.FAX,
                                        r.REPRESENTATIVE_ID,
                                        r.ORD_NO,
                                        r.NUMBER_BUSINESS,
                                        r.PIT_NO,
                                        r.U_INSURANCE,
                                        r.REGION_ID,
                                        r.SHORT_NAME,
                                        r.IS_SIGN_CONTRACT,
                                        r.CONTRACT_CODE,
                                        r.GROUP_PAID_ID,
                                        r.UNIT_RANK_ID,
                                        r.PROVINCE_ID,
                                        r.DISTRICT_ID,
                                        r.WEBSITE_LINK,
                                        r.BANK_NO,
                                        r.BANK_ID,
                                        r.BANK_BRACH_ID,
                                        r.ACCOUNTING_ID,
                                        r.HR_ID,
                                        r.PROVINCE_CONTRACT_ID,
                                        r.DISTRICT_CONTRACT_ID,
                                        r.AUTHOR_LETTER,
                                        r.BUSS_REG_NAME,
                                        r.MAN_UNI_NAME,
                                        r.UNDER_ID,
                                        Me.Log.Username,
                                        OUT_NUMBER
                                        })
        Dim obj As Object = hfr.ExecuteStoreScalar("PKG_PROFILE.UPDATE_ORGANIZATION_NEW", t_param)
        Return Int32.Parse(obj(0).ToString())
    End Function
#End Region
#End Region

#Region "Terminate-Nghỉ việc"
    Public Function CHECK_TER_EMPEXIST(ByVal TerId As Decimal, ByVal EmpId As Decimal) As Boolean
        Dim _rs As Boolean
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.CHECK_TER_EMPEXIST", New List(Of Object)(New Object() {TerId, EmpId}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            If ds.Tables(0).Rows.Count > 0 Then
                _rs = True
            Else
                _rs = False
            End If
        Else
            _rs = False
        End If
        Return _rs
    End Function
#End Region
#Region "Determine"
    Public Function GetDetermine(ByVal _filter As DetermineDTO,
                                 ByVal _param As Profile.ProfileBusiness.ParamDTO,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer) As DataTable
        Try
            Dim dtData As DataTable = Nothing
            Dim dsData As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.GET_HOACH_DINH", New List(Of Object)(New Object() {Me.Log.Username.ToUpper, _param.ORG_ID, _param.IS_DISSOLVE}))
            If dsData IsNot Nothing Then
                dtData = dsData.Tables(0)
            End If
            Dim strWhere As String = "1=1 "
            If _filter.BRANCH IsNot Nothing Then
                strWhere += " AND BRANCH LIKE '" + "%" + _filter.BRANCH.ToLower() + "%" + "'"
            End If
            If _filter.ORG_NAME3 IsNot Nothing Then
                strWhere += " AND ORG_NAME3 LIKE '" + "%" + _filter.ORG_NAME3.ToLower() + "%" + "'"
            End If
            If _filter.ORG_NAME4 IsNot Nothing Then
                strWhere += " AND ORG_NAME4 LIKE '" + "%" + _filter.ORG_NAME4.ToLower() + "%" + "'"
            End If
            If _filter.ORG_NAME5 IsNot Nothing Then
                strWhere += " AND ORG_NAME5 LIKE '" + "%" + _filter.ORG_NAME5.ToLower() + "%" + "'"
            End If
            If _filter.TITLE_NAME IsNot Nothing Then
                strWhere += " AND TITLE_NAME LIKE '" + "%" + _filter.TITLE_NAME.ToLower() + "%" + "'"
            End If
            If IsNumeric(_filter.DINHBIEN) Then
                strWhere += " AND DINHBIEN =" + _filter.DINHBIEN.ToString
            End If
            If IsNumeric(_filter.HEADCOUNT) Then
                strWhere += " AND HEADCOUNT =" + _filter.HEADCOUNT.ToString
            End If
            If IsNumeric(_filter.ONE_IN_ONE) Then
                strWhere += " AND ONE_IN_ONE =" + _filter.ONE_IN_ONE.ToString
            End If
            If IsNumeric(_filter.MANY_IN_ONE) Then
                strWhere += " AND MANY_IN_ONE =" + _filter.MANY_IN_ONE.ToString
            End If
            If IsNumeric(_filter.BLANK) Then
                strWhere += " AND BLANK =" + _filter.BLANK
            End If
            dtData = dtData.Select(strWhere).CopyToDataTable()
            Total = dtData.Rows().Count
            If dtData IsNot Nothing Then
                dtData = dtData.AsEnumerable().Skip(PageIndex * PageSize).Take(PageSize).CopyToDataTable
            End If

            Return dtData
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function EXPORT_DETERMINE(ByVal _orgID As Decimal, ByVal _IsDissolve As Boolean)
        Try
            Dim dsData As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.GET_HOACH_DINH", New List(Of Object)(New Object() {Me.Log.Username.ToUpper, _orgID, _IsDissolve}))
            Return dsData
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "CONTRACT TEMPLATE"
    Public Function GetLastAppenNumber(ByVal empID As Decimal) As Integer
        Dim dtData As DataTable
        Dim stt As Integer
        Dim dsData As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE_BUSINESS.GET_LAST_APPEND_NUMBER",
                                             New List(Of Object)(New Object() {empID, OUT_CURSOR}))
        If dsData.Tables.Count > 0 Then
            Return CType(dsData.Tables(0).Rows(0)(0), Integer)
        Else
            Return 0
        End If
    End Function
#End Region

#Region "file"
    Public Function Get_Folder_link(ByVal _pID As Decimal) As String
        Dim dt As DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.GET_FOLDER_LINK", New List(Of Object)(New Object() {_pID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt.Rows(0)(0).ToString
    End Function

    Public Function Delete_folder(ByVal _pID As Decimal) As Decimal
        Dim dt As DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.DELETE_FOLDER", New List(Of Object)(New Object() {_pID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt.Rows(0)(0)
    End Function

    Public Function Insert_UserFile(ByVal P_NAME As String, ByVal P_FOLDERID As Decimal, ByVal P_DESCRIPTION As String, ByVal P_CREATED_BY As String, ByVal P_FILENAME As String) As Decimal
        Dim dt As DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.INSERT_USERFILE", New List(Of Object)(New Object() {P_NAME, P_FOLDERID, P_DESCRIPTION, P_CREATED_BY, P_FILENAME}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt.Rows(0)(0)
    End Function
#End Region
End Class

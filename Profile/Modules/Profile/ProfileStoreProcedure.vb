Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities

Public Class ProfileStoreProcedure
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
    Public Function CHECK_WELFARE(ByVal Name As String, ByVal org_id As Decimal, ByVal sdate As Date) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = hfr.ExecuteToDataSet("PKG_PROFILE.CHECK_WELFARE", New List(Of Object)(New Object() {Name, org_id, sdate}))
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

    Public Function ReadDataCommendImported(ByVal orgID As Integer, ByVal userName As String, ByVal commendType As Decimal, ByVal DateReview As String, _
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

End Class

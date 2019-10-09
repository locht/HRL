Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Common
Imports Telerik.Web.UI
Imports WebAppLog
Imports System.IO
Imports Ionic.Zip
Public Class ctrlHU_ContractNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner2 As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSalaryPopup As ctrlFindSalaryPopup
    Public Overrides Property MustAuthorize As Boolean = False

    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _flag As Boolean = True
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

    ''' <summary>
    ''' isPhysical
    ''' </summary>
    ''' <remarks></remarks>
    Public isPhysical As Decimal = Decimal.Parse(ConfigurationManager.AppSettings("PHYSICAL_PATH"))

#Region "Property"

    Property ListAttachFile As List(Of AttachFilesDTO)
        Get
            Return ViewState(Me.ID & "_ListAttachFile")
        End Get
        Set(ByVal value As List(Of AttachFilesDTO))
            ViewState(Me.ID & "_ListAttachFile") = value
        End Set
    End Property

    Property Contract As ContractDTO
        Get
            Return ViewState(Me.ID & "_Contract")
        End Get
        Set(ByVal value As ContractDTO)
            ViewState(Me.ID & "_Contract") = value
        End Set
    End Property

    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
    Property code_timekeeping As Integer
        Get
            Return ViewState(Me.ID & "_code_timekeeping")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_code_timekeeping") = value
        End Set
    End Property
    Property start_rankid As Integer
        Get
            Return ViewState(Me.ID & "_start_rankid")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_start_rankid") = value
        End Set
    End Property
    Property object_labour As Integer
        Get
            Return ViewState(Me.ID & "_object_labour")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_object_labour") = value
        End Set
    End Property
    Property direct_manager As Integer
        Get
            Return ViewState(Me.ID & "_direct_manager")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_direct_manager") = value
        End Set
    End Property
    '0 - normal
    '1 - Employee
    '2 - Signer
    '3 - Salary
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 06/07/2017 14:36
    ''' </lastupdate>
    ''' <summary>
    ''' Hiển thị thông tin các control trên page
    ''' Cập nhật các trạng thái của các control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetParams()
            Refresh()
            UpdateControlState()

            If (_flag = False) Then
                EnableControlAll_Cus(False, LeftPane)
                btnDownload.Enabled = True
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    '''<lastupdate>
    ''' 06/07/2017 14:39
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo giá trị cho các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            If Not IsPostBack Then
                ViewConfig(LeftPane)
                'GirdConfig(rgAllow)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdated>
    ''' 06/07/2017 14:40
    ''' </lastupdated>
    ''' <summary>
    ''' Phương thức bind dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 14:56
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc khoi tao cac gia tri cho cac control tren page
    ''' Fixed doi voi user la HR.Admin hoac Admin thi them chuc nang "Mo cho phe duyet"
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarContract

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            Dim use As New ProfileRepositoryBase
            If use.Log.Username = "HR.ADMIN" Or use.Log.Username = "ADMIN" Then
                Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("UNLOCK", ToolbarIcons.Unlock,
                                                                     ToolbarAuthorize.Export, Translate("Mở chờ phê duyệt")))
            End If
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 15:17
    ''' </lastupdate>
    ''' <summary>
    ''' Lam moi trang thai cua cac control tren page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Contract = rep.GetContractByID(New ContractDTO With {.ID = hidID.Value})
                    If Contract IsNot Nothing Then
                        hidID.Value = Contract.ID
                        hidEmployeeID.Value = Contract.EMPLOYEE_ID.ToString
                        hidID.Value = Contract.ID.ToString
                        If Contract.WORKING_ID IsNot Nothing Then
                            hidWorkingID.Value = Contract.WORKING_ID
                        End If
                        ListAttachFile = Contract.ListAttachFiles
                        txtUpload.Text = String.Join(",", (From p In Contract.ListAttachFiles.AsEnumerable Select p.ATTACHFILE_NAME).ToArray)
                        txtContractNo.Text = Contract.CONTRACT_NO
                        txtEmployeeCode.Text = Contract.EMPLOYEE_CODE
                        txtEmployeeName.Text = Contract.EMPLOYEE_NAME
                        txtTITLE.Text = Contract.TITLE_NAME
                        txtOrg_Name.Text = Contract.ORG_NAME
                        txtRemark.Text = Contract.REMARK
                        txtSigner.Text = Contract.SIGNER_NAME
                        hidSign.Value = Contract.SIGN_ID.ToString
                        hidSign2.Value = Contract.SIGN_ID2.ToString
                        txtSignName2.Text = Contract.SIGNER_NAME2
                        txtSignTitle2.Text = Contract.SIGNER_TITLE2
                        txtSignTitle.Text = Contract.SIGNER_TITLE
                        txtUploadFile.Text = Contract.ATTACH_FILE
                        txtUpload.Text = Contract.FILENAME
                        cboContractType.SelectedValue = Contract.CONTRACTTYPE_ID
                        If Contract.WORK_STATUS IsNot Nothing Then
                            hidWorkStatus.Value = Contract.WORK_STATUS
                        End If
                        rdStartDate.SelectedDate = Contract.START_DATE
                        rdExpireDate.SelectedDate = Contract.EXPIRE_DATE
                        rdSignDate.SelectedDate = Contract.SIGN_DATE
                        If Contract.Working IsNot Nothing Then
                            SetValueComboBox(cboSalTYPE, Contract.Working.SAL_TYPE_ID, Contract.Working.SAL_TYPE_NAME)
                            SetValueComboBox(cboTaxTable, Contract.Working.TAX_TABLE_ID, Contract.Working.TAX_TABLE_Name)
                            'SalaryInsurance.Value = Contract.Working.SAL_INS
                            PercentSalary.Value = Contract.Working.PERCENT_SALARY
                            rnOtherSalary1.Value = Contract.Working.OTHERSALARY1
                            rnOtherSalary2.Value = Contract.Working.OTHERSALARY2
                            rnOtherSalary3.Value = Contract.Working.OTHERSALARY3
                            Allowance_Total.Value = Contract.Working.ALLOWANCE_TOTAL
                            Salary_Total.Value = Contract.Working.SAL_TOTAL
                            rnBasicSal.Value = Contract.Working.SAL_BASIC
                            'rgAllow.DataSource = Contract.Working.lstAllowance
                            'rgAllow.DataBind()
                            Working_ID.Text = If(Contract.Working.DECISION_NO <> "", Contract.Working.DECISION_NO, Contract.Working.EFFECT_DATE.Value.Date)
                        Else
                            Dim dt As New DataTable
                            'rgAllow.DataSource = dt
                            'rgAllow.DataBind()

                        End If
                        If Contract.STATUS_ID IsNot Nothing Then
                            cboStatus.SelectedValue = Contract.STATUS_ID
                        End If
                        If Contract.ID_SIGN_CONTRACT IsNot Nothing Then
                            cboSignContract.SelectedValue = Contract.ID_SIGN_CONTRACT
                            cboSignContract.Text = Contract.NAME_SIGN_CONTRACT
                        End If

                        If Contract.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Or
                            Contract.STATUS_ID = ProfileCommon.DECISION_STATUS.NOT_APPROVE_ID Then
                            _flag = False
                            EnableControlAll_Cus(False, LeftPane)
                            btnDownload.Enabled = True
                            MainToolBar.Items(0).Enabled = False
                        End If

                        If Contract.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                            MainToolBar.Items(0).Enabled = False
                        End If
                    End If
                Case "NormalView"
                    CurrentState = CommonMessage.STATE_NEW
                    Dim dt As New DataTable
                    'rgAllow.DataSource = New List(Of WorkingAllowanceDTO)
                    'rgAllow.DataBind()
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 06/07/2017 15:41
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện command tren toolbar khi click vao cac item cua no
    ''' Cac command la luu, mo khoa, huy 
    ''' Cập nhật lại trạng thái các control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objContract As New ContractDTO
        Dim rep As New ProfileBusinessRepository
        Dim gID As Decimal
        'Dim stt As OtherListDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim strUrl As String = Request.Url.ToString()
            Dim isPopup As Boolean = False
            If (strUrl.ToUpper.Contains("DIALOG")) Then
                isPopup = True
            End If
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim employee = rep.GetEmployeeByID(Decimal.Parse(hidEmployeeID.Value))
                        If hidWorkingID.Value = "" Then
                            ShowMessage(Translate("Bạn phải chọn Tờ trình/QĐ"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If cboStatus.SelectedValue = 447 Then
                            If txtUpload.Text = "" Then
                                ShowMessage(Translate("Bạn phải đính kèm tập tin khi phê duyệt"), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If
                        If cboSignContract.SelectedValue <> "" Then
                            objContract.ID_SIGN_CONTRACT = cboSignContract.SelectedValue
                        End If

                        objContract.CONTRACTTYPE_ID = Decimal.Parse(cboContractType.SelectedValue)
                        objContract.STATUS_ID = Decimal.Parse(cboStatus.SelectedValue)
                        objContract.EMPLOYEE_ID = Decimal.Parse(hidEmployeeID.Value)
                        objContract.EXPIRE_DATE = rdExpireDate.SelectedDate
                        objContract.CONTRACT_NO = txtContractNo.Text
                        objContract.REMARK = txtRemark.Text
                        objContract.OBJECTTIMEKEEPING = code_timekeeping
                        If direct_manager <> 0 Then
                            objContract.DIRECT_MANAGER = direct_manager
                        End If
                        If start_rankid <> 0 Then
                            objContract.STAFF_RANK_ID = start_rankid
                        End If
                        If object_labour <> 0 Then
                            objContract.OBJECT_LABOUR = object_labour
                        End If
                        If hidSign2.Value <> "" Then
                            objContract.SIGN_ID2 = Decimal.Parse(hidSign2.Value)
                        End If
                        objContract.SIGNER_NAME2 = txtSignName2.Text
                        objContract.SIGNER_TITLE2 = txtSignTitle2.Text
                        If hidSign.Value <> "" Then
                            objContract.SIGN_ID = Decimal.Parse(hidSign.Value)
                        End If
                        objContract.SIGNER_NAME = txtSigner.Text
                        objContract.SIGNER_TITLE = txtSignTitle.Text
                        objContract.SIGN_DATE = rdSignDate.SelectedDate
                        objContract.START_DATE = rdStartDate.SelectedDate
                        objContract.WORKING_ID = hidWorkingID.Value
                        objContract.ORG_ID = employee.ORG_ID
                        objContract.TITLE_ID = employee.TITLE_ID
                        objContract.ATTACH_FILE = txtUploadFile.Text 'Save folder name
                        objContract.FILENAME = txtUpload.Text 'Save attachment file name
                        If ListAttachFile IsNot Nothing Then
                            objContract.ListAttachFiles = ListAttachFile
                        End If
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertContract(objContract, gID) Then
                                    If (isPopup) Then
                                        Dim str As String = "getRadWindow().close('1');"
                                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                    Else
                                        Session("Result") = 1
                                        Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Contract&group=Business")
                                    End If
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objContract.ID = Decimal.Parse(hidID.Value)
                                Dim lstID As New List(Of Decimal)
                                lstID.Add(hidID.Value)
                                If rep.ValidateBusiness("HU_CONTRACT", "ID", lstID) Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                    Exit Sub
                                End If
                                If rep.ModifyContract(objContract, gID) Then
                                    If (isPopup) Then
                                        Dim str As String = "getRadWindow().close('1');"
                                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                    Else
                                        Session("Result") = 1
                                        Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Contract&group=Business")
                                    End If
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        Exit Sub
                    End If
                Case "UNLOCK"
                    objContract.ID = Decimal.Parse(hidID.Value)
                    objContract.STATUS_ID = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID
                    If rep.UnApproveContract(objContract, gID) Then
                        Dim str As String = "getRadWindow().close('1');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    If (isPopup) Then
                        Dim str As String = "getRadWindow().close('1');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    Else
                        Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Contract&group=Business")
                    End If
            End Select
            rep.Dispose()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:34
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho button btnEmployee
    ''' Hien thi popup co isLoadPopup = 1 khi click vao button
    ''' Cap nhat lai trang thai của cac control tren page
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 1
            'LoadPopup(1)
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Xu ly su kien click vao button btnSigner
    ''' Hien thi popup co isLoadPopup = 2 khi click vao button
    ''' Cap nhat lai trang thai cac control tren page hien tai
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSigner_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSigner.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 2
            UpdateControlState()
            'LoadPopup(2)
            ctrlFindSigner.MustHaveContract = True
            ctrlFindSigner.LoadAllOrganization = False
            ctrlFindSigner.IsOnlyWorkingWithoutTer = True
            ctrlFindSigner.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Protected Sub btnSiger2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSiger2.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 4
            UpdateControlState()
            'LoadPopup(2)
            ctrlFindSigner2.MustHaveContract = True
            ctrlFindSigner2.LoadAllOrganization = False
            ctrlFindSigner2.IsOnlyWorkingWithoutTer = True
            ctrlFindSigner2.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    '' <lastupdate>
    '' 06/07/2017 17:44
    '' </lastupdate>
    '' <summary>
    '' Xu ly su kien click khi click vao button btnSalary
    '' Hien thi popup voi isLoadPopup = 3
    '' Cap nhat lai trang thai cac control tren page
    '' </summary>
    '' <param name="sender"></param>
    '' <param name="e"></param>
    '' <remarks></remarks>
    Protected Sub btnSalary_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSalary.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If hidEmployeeID.Value = "" Then
                ShowMessage(Translate("Bạn phải chọn nhân viên."), NotifyType.Warning)
                Exit Sub
            End If
            isLoadPopup = 3
            UpdateControlState()
            ' LoadPopup(3)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:48
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien khi click ctrlFind_CancelClick
    ''' Cap nhat trang thai isLoadPopup = 0
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFind_CancelClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles ctrlFindSigner.CancelClicked, ctrlFindSigner2.CancelClicked,
        ctrlFindEmployeePopup.CancelClicked,
        ctrlFindSalaryPopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, 0, Nothing, "ctrlFind_CancelClick")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:51
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien selected cua control ctrlFindSigner_Employee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindSignPopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindSigner.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstCommonEmployee = CType(ctrlFindSigner.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidSign.Value = item.ID.ToString
                txtSignTitle.Text = item.TITLE_NAME
                txtSigner.Text = item.FULLNAME_VN
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlFindSignPopup2_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindSigner2.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstCommonEmployee = CType(ctrlFindSigner2.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidSign2.Value = item.ID.ToString
                txtSignTitle2.Text = item.TITLE_NAME
                txtSignName2.Text = item.FULLNAME_VN
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:51
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien selected cua control ctrlFindEmployeePopup_Employee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstEmpID As New List(Of Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstEmpID = ctrlFindEmployeePopup.SelectedEmployeeID
            If lstEmpID.Count <> 0 Then
                FillData(lstEmpID(0))
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub FillNewestWorkingData(ByVal employeeId As Decimal)
        Dim working As WorkingDTO
        'Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
        '        .IS_DISSOLVE = ctrlOrg.IsDissolve}
        Using rep As New ProfileBusinessRepository
            working = rep.GetLastWorkingSalary(New WorkingDTO() With {.EMPLOYEE_ID = employeeId}) ' .STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID
        End Using
        If working IsNot Nothing Then
            cboSalTYPE.Text = working.SAL_TYPE_NAME
            cboTaxTable.Text = working.TAX_TABLE_Name
            rnBasicSal.Value = working.SAL_BASIC
            'SalaryInsurance.Value = working.SAL_INS
            PercentSalary.Value = working.PERCENT_SALARY
            rnOtherSalary1.Value = working.OTHERSALARY1
            rnOtherSalary2.Value = working.OTHERSALARY2
            rnOtherSalary3.Value = working.OTHERSALARY3
            Allowance_Total.Value = working.ALLOWANCE_TOTAL
            Salary_Total.Value = working.SAL_TOTAL
        Else
            ClearControlValue(cboSalTYPE, cboTaxTable, rnBasicSal, PercentSalary, rnOtherSalary1, rnOtherSalary2, rnOtherSalary3, Allowance_Total, Salary_Total)
        End If
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien selected cua control ctrlFindSalaryPopup_Salaryq
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindSalaryPopup_SalarySelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindSalaryPopup.SalarySelected
        Dim lstCommon As New List(Of WorkingDTO)
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            lstCommon = CType(ctrlFindSalaryPopup.SelectedSalary, List(Of WorkingDTO))
            If lstCommon.Count <> 0 Then
                Dim item = lstCommon(0)
                hidWorkingID.Value = item.ID
                Dim working = rep.GetWorkingByID(New WorkingDTO() With {.ID = item.ID})
                Working_ID.Text = If(working.DECISION_NO <> "", working.DECISION_NO, working.EFFECT_DATE.Value.Date)
                rnBasicSal.Value = working.SAL_BASIC
                Salary_Total.Value = working.SAL_TOTAL
                'SalaryInsurance.Value = working.SAL_INS
                PercentSalary.Value = working.PERCENT_SALARY
                rnOtherSalary1.Value = working.OTHERSALARY1
                rnOtherSalary2.Value = working.OTHERSALARY2
                rnOtherSalary3.Value = working.OTHERSALARY3
                Allowance_Total.Value = working.ALLOWANCE_TOTAL
                SetValueComboBox(cboSalTYPE, working.SAL_TYPE_ID, working.SAL_TYPE_NAME)
                SetValueComboBox(cboTaxTable, working.TAX_TABLE_ID, working.TAX_TABLE_Name)
                'rgAllow.DataSource = working.lstAllowance
                'rgAllow.Rebind()
            End If
            rep.Dispose()
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command cua control ctrlMessageBox_Button
    ''' Neu command là item xoa thi cap nhat lai trang thai hien tai la xoa
    ''' Cap nhat lai trang thai cua cac control tren page
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien selectedIndexChanged cua control cboContractType
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cboContractType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboContractType.SelectedIndexChanged
        Dim item As New ContractTypeDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If rdStartDate.SelectedDate IsNot Nothing Then
                Dim dExpire As Date = rdStartDate.SelectedDate
                If cboContractType.SelectedValue <> "" Then
                    item = (From p In ListComboData.LIST_CONTRACTTYPE Where p.ID = Decimal.Parse(cboContractType.SelectedValue)).SingleOrDefault
                End If
                If item IsNot Nothing Then
                    If item.PERIOD IsNot Nothing Then
                        hidPeriod.Value = item.PERIOD
                    Else
                        hidPeriod.Value = 0
                    End If
                End If
                If CType(hidPeriod.Value, Double) = 0 Then
                    rdExpireDate.SelectedDate = Nothing
                Else
                    dExpire = dExpire.AddMonths(CType(hidPeriod.Value, Double))
                    dExpire = dExpire.AddDays(CType(-1, Double))
                    rdExpireDate.SelectedDate = dExpire
                End If
            End If

            Dim employeeId As Double = 0
            Double.TryParse(hidEmployeeID.Value, employeeId)
            If cboSignContract.SelectedValue <> "" And rdStartDate.SelectedDate IsNot Nothing Then
                txtContractNo.Text = CreateDynamicContractNo(employeeId)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub cboSignContract_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboSignContract.SelectedIndexChanged
        Dim item As New ContractTypeDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Dim employeeId As Double = 0
            Double.TryParse(hidEmployeeID.Value, employeeId)
            If cboSignContract.SelectedValue <> "" And rdStartDate.SelectedDate IsNot Nothing Then
                txtContractNo.Text = CreateDynamicContractNo(employeeId)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien SelectedDateChanged cua control rdStartDate
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rdStartDate_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdStartDate.SelectedDateChanged
        Dim item As New ContractTypeDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'If cboContractType.SelectedValue = "" Then
            '    If rdStartDate.SelectedDate IsNot Nothing Then
            '        txtContractNo.Text = CreateDynamicContractNo(hidEmployeeID.Value)
            '    End If
            '    Exit Sub
            'End If
            If rdStartDate.SelectedDate IsNot Nothing Then
                Dim dExpire As Date = rdStartDate.SelectedDate
                If cboContractType.SelectedValue <> "" Then
                    item = (From p In ListComboData.LIST_CONTRACTTYPE Where p.ID = Decimal.Parse(cboContractType.SelectedValue)).SingleOrDefault
                End If
                If item.ID > 0 AndAlso item.PERIOD IsNot Nothing Then
                    hidPeriod.Value = item.PERIOD
                Else
                    hidPeriod.Value = 0
                End If
                If CType(hidPeriod.Value, Double) = 0 Then
                    rdExpireDate.SelectedDate = Nothing
                Else
                    dExpire = dExpire.AddMonths(CType(hidPeriod.Value, Double))
                    dExpire = dExpire.AddDays(CType(-1, Double))
                    rdExpireDate.SelectedDate = dExpire
                End If
                Dim employeeId As Double = 0
                Double.TryParse(hidEmployeeID.Value, employeeId)
                If cboSignContract.SelectedValue <> "" Then
                    txtContractNo.Text = CreateDynamicContractNo(employeeId)
                End If
            End If
            If rdStartDate.SelectedDate < rdSignDate.SelectedDate Then
                rdSignDate.SelectedDate = rdStartDate.SelectedDate
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien ServerValidate cua control CompareStartDate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub CompareStartDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CompareStartDate.ServerValidate
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim _filter As New ContractDTO
            _filter.START_DATE = rdStartDate.SelectedDate
            _filter.EMPLOYEE_ID = hidEmployeeID.Value
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                Case CommonMessage.STATE_EDIT
                    _filter.ID = hidID.Value
            End Select
            args.IsValid = rep.ValidateContract("EXIST_EFFECT_DATE", _filter)
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien ServerValidate cua control cusvalContractType
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cusvalContractType_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusvalContractType.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New ContractTypeDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboContractType.SelectedValue
                validate.ACTFLG = "A"
                args.IsValid = rep.ValidateContractType(validate)
            End If
            If Not args.IsValid Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_CONTRACTTYPE = True
                rep.GetComboList(ListComboData)
                FillDropDownList(cboContractType, ListComboData.LIST_CONTRACTTYPE, "NAME", "ID", Common.Common.SystemLanguage, False)
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
            ctrlUpload1.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            txtUpload.Text = ""
            Dim listExtension = New List(Of String)
            listExtension.Add(".xls")
            listExtension.Add(".xlsx")
            listExtension.Add(".txt")
            listExtension.Add(".ctr")
            listExtension.Add(".doc")
            listExtension.Add(".docx")
            listExtension.Add(".xml")
            listExtension.Add(".png")
            listExtension.Add(".jpg")
            listExtension.Add(".bitmap")
            listExtension.Add(".jpeg")
            listExtension.Add(".gif")
            listExtension.Add(".pdf")
            listExtension.Add(".rar")
            listExtension.Add(".zip")
            listExtension.Add(".ppt")
            listExtension.Add(".pptx")
            Dim fileName As String

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/ContractInfo/")
            If ctrlUpload1.UploadedFiles.Count >= 1 Then
                Dim finfo As New AttachFilesDTO
                ListAttachFile = New List(Of AttachFilesDTO)
                Dim file As UploadedFile = ctrlUpload1.UploadedFiles(ctrlUpload1.UploadedFiles.Count - 1)
                If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                    System.IO.Directory.CreateDirectory(strPath)
                    strPath = strPath
                    fileName = System.IO.Path.Combine(strPath, file.FileName)
                    file.SaveAs(fileName, True)
                    txtUpload.Text = file.FileName
                    finfo.FILE_PATH = strPath + file.FileName
                    finfo.ATTACHFILE_NAME = file.FileName
                    finfo.CONTROL_NAME = "ctrlHU_ContractNewEdit"
                    finfo.FILE_TYPE = file.GetExtension
                    ListAttachFile.Add(finfo)
                Else
                    ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"), NotifyType.Warning)
                    Exit Sub
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If txtUpload.Text <> "" Then
                Dim strPath_Down As String = Server.MapPath("~/ReportTemplates/Profile/ContractInfo/" + txtUploadFile.Text + txtUpload.Text)
                ZipFiles(strPath_Down, 2)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"


    Private Sub ZipFiles(ByVal path As String, ByVal _ID As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim fileNameZip As String
            If _ID = 1 Then
                fileNameZip = txtUpload.Text.Trim
            Else
                fileNameZip = txtUpload.Text.Trim
            End If
            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            'Response.ContentType = "application/octet-stream"
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
            Response.WriteFile(file.FullName)
            Response.End()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Hàm cập nhật trạng thái của các control trên page
    ''' Xử lý đăng ký popup ứng với giá trị isLoadPopup
    ''' </summary>
    ''' <remarks></remarks>

    Public Overrides Sub UpdateControlState()
        'Dim rep As New ProfileBusinessRepository

        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            btnEmployee.Enabled = True
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                Case CommonMessage.STATE_EDIT
            End Select
            LoadPopup(isLoadPopup)
            If (hidID.Value = "") Then
                If _toolbar Is Nothing Then Exit Sub
                Dim item As RadToolBarButton
                For i = 0 To _toolbar.Items.Count - 1
                    item = CType(_toolbar.Items(i), RadToolBarButton)
                    'Select Case CurrentState
                    '    Case CommonMessage.STATE_EDIT, CommonMessage.STATE_NEW
                    If item.CommandName = "UNLOCK" Then
                        item.Enabled = False
                    End If
                    'End Select
                Next
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(ViewName, ID, ex)
        End Try

    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc load dữ liệu cho các combobox
    ''' cboContractType, cboStatus
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New ProfileRepository
        ListComboData = New ComboBoxDataDTO
        ListComboData.GET_CONTRACTTYPE = True
        ListComboData.GET_LOCATION = True
        rep.GetComboList(ListComboData)
        FillDropDownList(cboContractType, ListComboData.LIST_CONTRACTTYPE, "NAME", "ID", Common.Common.SystemLanguage, False)
        FillDropDownList(cboSignContract, ListComboData.LIST_LOCATION, "CODE", "ID", Common.Common.SystemLanguage, False)
        rep.Dispose()
        Dim dtData As New DataTable
        'TNG-117	
        'Chuẩn hóa lại bộ trạng thái, sử dụng chung trong hệ thống
        'chuẩn hóa lại đồng bộ trạng thái là 446 447 nên sửa lại cách load data lên
        dtData = rep.GetOtherList(OtherTypes.DecisionStatus, True)
        FillRadCombobox(cboStatus, dtData, "NAME", "ID", True)
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc lấy về parameter "IDSelect"
    ''' Làm mới View hiện thời
    ''' Fill du lieu cho View nếu parameter là "EmpID"
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            If CurrentState Is Nothing Then
                cboContractType.AutoPostBack = True
                If Request.Params("IDSelect") IsNot Nothing Then
                    hidID.Value = Request.Params("IDSelect")
                    Refresh("UpdateView")
                    Exit Sub
                End If
                Refresh("NormalView")
                If Request.Params("EmpID") IsNot Nothing Then
                    FillData(Request.Params("EmpID"))
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức fill dữ liệu cho page
    ''' theo các trạng thái maintoolbar và trạng thái item trên trang
    ''' </summary>
    ''' <param name="empID"></param>
    ''' <remarks></remarks>
    Private Sub FillData(ByVal empID As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Using rep As New ProfileBusinessRepository
                Dim item = rep.GetContractEmployeeByID(empID)
                rdStartDate.MaxDate = Date.MaxValue
                If item.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                    ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                    MainToolBar.Items(0).Enabled = False
                Else
                    MainToolBar.Items(0).Enabled = True

                End If
                If item.CONTRACT_EFFECT_DATE IsNot Nothing Then
                    If item.CONTRACT_EXPIRE_DATE Is Nothing Then
                        ShowMessage(Translate("Nhân viên có hợp đồng không xác định thời hạn. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                        MainToolBar.Items(0).Enabled = False
                        'Else
                        '    rdStartDate.SelectedDate = item.CONTRACT_EXPIRE_DATE.Value.AddDays(1)
                        '    rdStartDate.MaxDate = item.CONTRACT_EXPIRE_DATE.Value.AddDays(1)
                    End If
                End If
                If item.WORK_STATUS IsNot Nothing Then
                    hidWorkStatus.Value = item.WORK_STATUS
                End If
                hidEmployeeID.Value = item.ID.ToString
                hidOrgCode.Value = item.ORG_CODE
                If item.OBJECTTIMEKEEPING.HasValue Then
                    code_timekeeping = item.OBJECTTIMEKEEPING
                End If
                If item.DIRECT_MANAGER IsNot Nothing Then
                    direct_manager = item.DIRECT_MANAGER
                End If
                If item.STAFF_RANK_ID IsNot Nothing Then
                    start_rankid = item.STAFF_RANK_ID
                End If
                If item.OBJECT_LABOR IsNot Nothing Then
                    object_labour = item.OBJECT_LABOR
                End If

                txtEmployeeCode.Text = item.EMPLOYEE_CODE
                txtEmployeeName.Text = item.FULLNAME_VN
                txtTITLE.Text = item.TITLE_NAME_VN
                'txtSTAFF_RANK.Text = item.STAFF_RANK_NAME
                txtOrg_Name.Text = item.ORG_NAME
                GetWorkingMax()
                Dim employeeId As Double = 0
                Double.TryParse(hidEmployeeID.Value, employeeId)
                txtContractNo.Text = CreateDynamicContractNo(employeeId)
                'txtContractNo.Enabled = True
                ClearControlValue(rdStartDate, rdSignDate)
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc tạo số hợp đồng một cách tự động
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CreateDynamicContractNo(ByVal empId As Double) As String
        If empId < 1 Then
            Return String.Empty
        End If
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If CurrentState = CommonMessage.STATE_NORMAL Or
                CurrentState Is Nothing Or
                 String.IsNullOrWhiteSpace(cboContractType.SelectedValue) Then
                Return String.Empty
            End If
            Using rep As New ProfileBusinessRepository
                Return rep.CreateContractNo(New ContractDTO With {
                                                       .START_DATE = rdStartDate.SelectedDate,
                                                       .ORG_NAME = txtOrg_Name.Text,
                                                       .ID_SIGN_CONTRACT = cboSignContract.SelectedValue,
                                                       .EMPLOYEE_ID = empId,
                                                       .EMPLOYEE_CODE = txtEmployeeCode.Text,
                                                       .CONTRACTTYPE_ID = cboContractType.SelectedValue
                                                       })
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(ViewName, ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Return String.Empty
        End Try
    End Function
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức lấy giá trị kinh nghiệm làm việc của nhân viên đạt mức lương cao nhất
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetWorkingMax()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim working As WorkingDTO
            Using rep As New ProfileBusinessRepository
                working = rep.GetLastWorkingSalary(New WorkingDTO With {.EMPLOYEE_ID = hidEmployeeID.Value})
            End Using
            If working IsNot Nothing Then
                hidWorkingID.Value = working.ID
                SetValueComboBox(cboSalTYPE, working.SAL_TYPE_ID, working.SAL_TYPE_NAME)
                SetValueComboBox(cboTaxTable, working.TAX_TABLE_ID, working.TAX_TABLE_Name)
                rnBasicSal.Value = working.SAL_BASIC
                'SalaryInsurance.Value = working.SAL_INS
                PercentSalary.Value = working.PERCENT_SALARY
                rnOtherSalary1.Value = working.OTHERSALARY1
                rnOtherSalary2.Value = working.OTHERSALARY2
                rnOtherSalary3.Value = working.OTHERSALARY3
                Allowance_Total.Value = working.ALLOWANCE_TOTAL
                Salary_Total.Value = working.SAL_TOTAL
                Working_ID.Text = If(working.DECISION_NO <> "", working.DECISION_NO, working.EFFECT_DATE.Value.Date)
                If rdStartDate.SelectedDate Is Nothing Then
                    rdStartDate.SelectedDate = working.EFFECT_DATE
                End If
                'rgAllow.DataSource = working.lstAllowance
                'rgAllow.Rebind()
            Else
                ClearControlValue(cboSalTYPE, cboTaxTable, rnBasicSal, PercentSalary, rnOtherSalary1, rnOtherSalary2, rnOtherSalary3, Allowance_Total, Salary_Total, rdSignDate)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub LoadPopup(ByVal popupType As Int32)
        Select Case popupType
            Case 1
                If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = False
                    ctrlFindEmployeePopup.MustHaveContract = False
                End If
            Case 2
                If Not FindSigner.Controls.Contains(ctrlFindSigner) Then
                    ctrlFindSigner = Me.Register("ctrlFindSigner", "Common", "ctrlFindEmployeePopup")
                    FindSigner.Controls.Add(ctrlFindSigner)
                    ctrlFindSigner.MultiSelect = False
                    ctrlFindSigner.MustHaveContract = True
                    ctrlFindSigner.LoadAllOrganization = True
                End If

            Case 3
                If Not FindSalary.Controls.Contains(ctrlFindSalaryPopup) Then
                    ctrlFindSalaryPopup = Me.Register("ctrlFindSalaryPopup", "Profile", "ctrlFindSalaryPopup", "Shared")
                    ctrlFindSalaryPopup.MultiSelect = False
                    If hidEmployeeID.Value <> "" Then
                        ctrlFindSalaryPopup.EmployeeID = Decimal.Parse(hidEmployeeID.Value)
                        Session("EmployeeID") = Decimal.Parse(hidEmployeeID.Value)
                    End If

                    FindSalary.Controls.Add(ctrlFindSalaryPopup)
                    ctrlFindSalaryPopup.Show()
                End If
            Case 4
                If Not FindSigner.Controls.Contains(ctrlFindSigner2) Then
                    ctrlFindSigner2 = Me.Register("ctrlFindSigner2", "Common", "ctrlFindEmployeePopup")
                    FindSigner.Controls.Add(ctrlFindSigner2)
                    ctrlFindSigner2.MultiSelect = False
                    ctrlFindSigner2.MustHaveContract = True
                    ctrlFindSigner2.LoadAllOrganization = True
                End If

        End Select
    End Sub
#End Region



End Class
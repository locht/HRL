Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports System.IO
Imports WebAppLog
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports HistaffWebAppResources.My.Resources

Public Class ctrlHU_ChangeInfoNewEdit
    Inherits CommonView

    ''' <summary>''' ctrl FindEmployeePopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup

    ''' <summary>''' ctrl FindEmployeeRePopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindEmployeeRePopup As ctrlFindEmployeePopup

    ''' <summary>
    ''' ctrl FindEmployeePopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup

    ''' <summary>
    ''' ctrl FindOrgPopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup

    ''' <summary>
    ''' ctrl FindManager
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindManager As ctrlFindEmployeePopup

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' MustAuthorize
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Property MustAuthorize As Boolean = True

    ''' <summary>
    ''' list WorkingAllowance DTO
    ''' </summary>
    ''' <remarks></remarks>
    Dim lstAllow As New List(Of WorkingAllowanceDTO)

    ''' <summary>
    ''' Obj WorkingDTO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Working As WorkingDTO
        Get
            Return ViewState(Me.ID & "_Working")
        End Get
        Set(ByVal value As WorkingDTO)
            ViewState(Me.ID & "_Working") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj WorkingDTO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WorkingForMessage As WorkingDTO
        Get
            Return ViewState(Me.ID & "_WorkingForMessage")
        End Get
        Set(ByVal value As WorkingDTO)
            ViewState(Me.ID & "_WorkingForMessage") = value
        End Set
    End Property

    ''' <summary>
    ''' isLoadPopup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>
    ''' 0 - normal
    ''' 1 - Employee
    ''' 2 - Sign
    ''' </remarks>
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
    'là khi chinh sua
    Property isEdit As Integer
        Get
            Return ViewState(Me.ID & "_isEdit")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isEdit") = value
        End Set
    End Property

    Property Down_File As String
        Get
            Return ViewState(Me.ID & "_Down_File")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Down_File") = value
        End Set
    End Property
    Property FileOldName As String
        Get
            Return ViewState(Me.ID & "_FileOldName")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_FileOldName") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary> Khoi tao, load trang thai control, page </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            GetParams()
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary>Load control, set thuoc tinh grid</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.MainToolBar = tbarMassTransferSalary
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "clientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            If Not IsPostBack Then
                ViewConfig(LeftPane)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary> Load data cho combox </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtData As DataTable
        Dim store As New ProfileStoreProcedure
        Try
            dtData = store.GET_DECISION_TYPE_EXCEPT_NV()
            FillRadCombobox(cboDecisionType, dtData, "NAME", "ID")
            If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                cboDecisionType.SelectedValue = dtData.Rows(0)("ID")
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary> Khoi tao, load menu toolbar </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim use As New ProfileRepositoryBase
        Try
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary>
    ''' Load page blank khi them moi, set trang thai page, control
    ''' Load page, data theo ID Nhan vien khi sua 
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
                    Working = rep.GetWorkingByID(Working)
                    'hidID.Value = Working.ID.ToString
                    hidEmp.Value = Working.EMPLOYEE_ID
                    If Working.DECISION_TYPE_ID = 7561 Then
                        btnFindOrg.Enabled = False
                        cboTitle.Enabled = False
                        If hidEmp.Value <> "" Then
                            Dim empID = hidEmp.Value
                            FillData(empID)
                        End If
                    Else
                        btnFindOrg.Enabled = True
                        cboTitle.Enabled = True
                        txtOrgName.Text = ""
                        hidOrg.ClearValue()
                        cboTitle.ClearValue()
                    End If
                    If Working.WORKING_OLD IsNot Nothing Then
                        With Working.WORKING_OLD
                            txtTitleNameOld.Text = .TITLE_NAME
                            txtDecisionTypeOld.Text = .DECISION_TYPE_NAME
                            rdEffectDateOld.SelectedDate = .EFFECT_DATE
                            rdExpireDateOld.SelectedDate = .EXPIRE_DATE
                            If Working.WORKING_OLD.TAX_TABLE_ID IsNot Nothing Then
                            End If
                            If .STAFF_RANK_ID IsNot Nothing Then
                                hidStaffRank.Value = .STAFF_RANK_ID
                            End If
                            If .FILENAME IsNot Nothing Then
                                lbFileAttach.Text = Working.WORKING_OLD.FILENAME
                                txtFileAttach_Link.Text = Working.WORKING_OLD.FILENAME
                                txtFileAttach_Link1.Text = Working.WORKING_OLD.ATTACH_FILE
                            End If
                            
                            txtDecisionold.Text = .DECISION_NO
                            txtOrgNameOld.Text = .ORG_NAME

                            ' thong tin them moi old
                            txtJobPositionOld.Text = .JOB_POSITION_NAME
                            txtJobDescriptionOld.Text = .JOB_DESCRIPTION_NAME
                            rdSignDateOld.SelectedDate = .SIGN_DATE
                            txtSignNameOld.Text = .SIGN_NAME
                            txtSignTitleOld.Text = .SIGN_TITLE
                            txtRemarkOld.Text = .REMARK
                            
                        End With
                    End If
                    txtEmployeeCode.Text = Working.EMPLOYEE_CODE
                    txtEmployeeName.Text = Working.EMPLOYEE_NAME
                    ' SetValueComboBox(cboTitle, Working.TITLE_ID, Working.TITLE_NAME)
                    hidOrg.Value = Working.ORG_ID
                    txtOrgName.Text = Working.ORG_NAME

                        If IsNumeric(hidOrg.Value) Then
                        Dim dtdata = (New ProfileRepository).GetTitleByOrgID(Decimal.Parse(hidOrg.Value), True)
                            cboTitle.ClearValue()
                            cboTitle.Items.Clear()
                            For Each item As DataRow In dtdata.Rows
                                Dim radItem As RadComboBoxItem = New RadComboBoxItem(item("NAME").ToString(), item("ID").ToString())
                                radItem.Attributes("GROUP_NAME") = item("GROUP_NAME").ToString()
                                cboTitle.Items.Add(radItem)
                            Next
                        End If

                    cboTitle.SelectedValue = Working.TITLE_ID

                    txtDecision.Text = Working.DECISION_NO
                    If Working.STATUS_ID IsNot Nothing Then
                        cboStatus.SelectedValue = Working.STATUS_ID
                        cboStatus.Text = Working.STATUS_NAME
                    End If
                    txtUploadFile.Text = Working.FILENAME
                    txtRemindLink.Text = If(Working.ATTACH_FILE Is Nothing, "", Working.ATTACH_FILE)
                    loadDatasource(txtUploadFile.Text)
                    FileOldName = If(FileOldName = "", txtUpload.Text, FileOldName)
                    rdEffectDate.SelectedDate = Working.EFFECT_DATE
                    rdExpireDate.SelectedDate = Working.EXPIRE_DATE
                    If Working.DECISION_TYPE_ID IsNot Nothing Then
                        cboDecisionType.SelectedValue = Working.DECISION_TYPE_ID
                        cboDecisionType.Text = Working.DECISION_TYPE_NAME
                    End If

                    If Working.IS_PROCESS IsNot Nothing Then
                        chkIsProcess.Checked = Working.IS_PROCESS
                    End If
                    
                    rdSignDate.SelectedDate = Working.SIGN_DATE

                    If Working.SIGN_ID IsNot Nothing Then
                        hidSign.Value = Working.SIGN_ID
                    End If

                    txtSignName.Text = Working.SIGN_NAME
                    txtSignTitle.Text = Working.SIGN_TITLE
                    txtRemark.Text = Working.REMARK
                    lstAllow = Working.lstAllowance

                    ' them moi
                    chkIsReplace.Checked = Working.IS_REPLACE
                    Dim DSdata As DataSet
                    Using rep1 As New ProfileRepository
                        DSdata = rep1.GET_JP_TO_TITLE(hidOrg.Value, cboTitle.SelectedValue, chkIsReplace.Checked, Working.JOB_POSITION)
                    End Using

                    cboJobPosition.DataSource = DSdata.Tables(0)
                    cboJobPosition.DataTextField = "NAME"
                    cboJobPosition.DataValueField = "ID"
                    cboJobPosition.DataBind()

                    cboJobDescription.DataSource = DSdata.Tables(1)
                    cboJobDescription.DataTextField = "NAME"
                    cboJobDescription.DataValueField = "ID"
                    cboJobDescription.DataBind()

                    cboJobPosition.SelectedValue = Working.JOB_POSITION

                    If Working.JOB_DESCRIPTION IsNot Nothing Then
                        cboJobDescription.SelectedValue = Working.JOB_DESCRIPTION
                    End If

                    rdEffectHdDate.SelectedDate = Working.EFFECT_DH_DATE
                    chkIsHurtful.Checked = Working.IS_HURTFUL
                    txtEmpReplace.Text = Working.EMP_REPLACE_NAME
                    If Working.EMP_REPLACE IsNot Nothing Then
                        hidEmpRe.Value = Working.EMP_REPLACE
                    End If

                    If Working.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Or
                        Working.STATUS_ID = ProfileCommon.DECISION_STATUS.NOT_APPROVE_ID Then
                        EnableControlAll_Cus(False, LeftPane)
                        btnDownload.Enabled = True
                        btnUploadFile.Enabled = True
                    End If

                    If chkIsReplace.Checked Then
                        btnEmpReplace.Enabled = True
                    Else
                        btnEmpReplace.Enabled = False
                    End If

                Case "NormalView"
                    CurrentState = CommonMessage.STATE_NEW
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary>Event click item menu toolbar</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objWorking As New WorkingDTO
        Dim rep As New ProfileBusinessRepository
        Dim rep1 As New ProfileRepository
        Dim gID As Decimal
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then

                        If cboStatus.SelectedValue = 447 And rdEffectDate.SelectedDate > Date.Now.Date Then
                            ShowMessage(Translate("Ngày hiệu lực phải nhỏ hơn bằng ngày hiện tại mới phê duyệt được, Vui lòng kiểm tra lại."), NotifyType.Warning)
                            Exit Sub
                        End If

                        If chkIsReplace.Checked = False Then
                            If rep1.CHECK_EXITS_JOB(cboJobPosition.SelectedValue, hidEmp.Value) > 0 Then
                                ShowMessage(Translate("Vị trí công việc đã tồn tại, Vui lòng kiểm tra lại."), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If

                        If cboTitle.SelectedValue = "" Then
                            ShowMessage(Translate("Bạn phải chọn chức danh"), NotifyType.Warning)
                            cboTitle.Focus()
                            Exit Sub
                        End If

                        If cboDecisionType.SelectedValue = "" Then
                            ShowMessage(Translate("Bạn phải chọn loại quyết định"), NotifyType.Warning)
                            cboDecisionType.Focus()
                            Exit Sub
                        End If

                        If cboStatus.SelectedValue = "" Then
                            ShowMessage(Translate("Bạn phải chọn trạng thái"), NotifyType.Warning)
                            cboStatus.Focus()
                            Exit Sub
                        End If

                        If rdEffectDate.SelectedDate Is Nothing Then
                            ShowMessage(Translate("Bạn phải chọn ngày hiệu lực"), NotifyType.Warning)
                            rdEffectDate.Focus()
                            Exit Sub
                        End If

                        With objWorking
                            .EMPLOYEE_ID = If(IsNumeric(hidEmp.Value), hidEmp.Value, Nothing)

                            If cboTitle.SelectedValue <> "" Then
                                .TITLE_ID = cboTitle.SelectedValue
                            End If

                            .ORG_ID = hidOrg.Value

                            If cboStatus.SelectedValue <> "" Then
                                .STATUS_ID = cboStatus.SelectedValue
                            End If

                            .EFFECT_DATE = rdEffectDate.SelectedDate

                            .EXPIRE_DATE = rdExpireDate.SelectedDate
                            If cboDecisionType.SelectedValue <> "" Then
                                .DECISION_TYPE_ID = cboDecisionType.SelectedValue
                            End If
                            .DECISION_NO = txtDecision.Text

                            .SIGN_DATE = rdSignDate.SelectedDate

                            If hidSign.Value <> "" Then
                                .SIGN_ID = hidSign.Value
                            End If
                            .FILENAME = txtUpload.Text.Trim
                            .ATTACH_FILE = If(Down_File Is Nothing, "", Down_File)
                            If .ATTACH_FILE = "" Then
                                .ATTACH_FILE = If(txtRemindLink.Text Is Nothing, "", txtRemindLink.Text)
                            Else
                                .ATTACH_FILE = If(.ATTACH_FILE Is Nothing, "", .ATTACH_FILE)
                            End If
                            .SIGN_NAME = txtSignName.Text

                            .SIGN_TITLE = txtSignTitle.Text
                            .REMARK = txtRemark.Text
                            .IS_PROCESS = chkIsProcess.Checked
                            .IS_MISSION = True
                            If hidManager.Value <> "" Then
                                .DIRECT_MANAGER = hidManager.Value
                            End If
                            .IS_3B = False
                            .IS_WAGE = IsWageDecisionType(cboDecisionType.SelectedValue)

                            .SAL_INS = .SAL_BASIC
                            .ALLOWANCE_TOTAL = 0
                            .IS_REPLACE = chkIsReplace.Checked
                            If hidEmpRe.Value <> "" Then
                                .EMP_REPLACE = hidEmpRe.Value
                            End If
                            .JOB_POSITION = cboJobPosition.SelectedValue

                            If cboJobDescription.SelectedValue <> "" Then
                                .JOB_DESCRIPTION = cboJobDescription.SelectedValue
                            End If
                            .IS_HURTFUL = chkIsHurtful.Checked
                            .EFFECT_DH_DATE = rdEffectHdDate.SelectedDate

                        End With

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertWorking1(objWorking, gID) Then

                                    ' Cập nhật ngày kết thúc cho hợp đồng cũ
                                    If objWorking.STATUS_ID = 447 Then
                                        rep1.UPDATE_END_DATE_QD(objWorking.EMPLOYEE_ID, objWorking.EFFECT_DATE)
                                    End If

                                    Dim str As String = "getRadWindow().close('1');"
                                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)

                                    'Clear all input
                                    ClearControlValue(txtEmployeeCode, txtEmployeeName,
                                                      txtTitleNameOld,
                                    txtDecisionTypeOld,
                                                      rdEffectDateOld, rdExpireDateOld, rdEffectDate, rdExpireDate,
                                                       rdSignDate, txtSignName, txtSignTitle,
                                                      txtOrgName, txtRemark)
                                    'cboStaffRank.Text = String.Empty
                                    cboTitle.Text = String.Empty
                                    cboDecisionType.Text = String.Empty
                                    chkIsProcess.Checked = False
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_ChangeInfoMng&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objWorking.ID = Decimal.Parse(hidID.Value)
                                If rep.ModifyWorking1(objWorking, gID) Then

                                    ' Cập nhật ngày kết thúc cho hợp đồng cũ
                                    If objWorking.STATUS_ID = 447 Then
                                        rep1.UPDATE_END_DATE_QD(objWorking.EMPLOYEE_ID, objWorking.EFFECT_DATE)
                                    End If

                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_ChangeInfoMng&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If

                        End Select
                    End If

                Case "UNLOCK"
                    objWorking.ID = Decimal.Parse(hidID.Value)
                    objWorking.STATUS_ID = 446
                    If rep.UnApproveWorking(objWorking, gID) Then
                        'Dim str As String = "getRadWindow().close('1');"
                        'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        ''POPUPTOLINK
                        Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_ChangeInfoMng&group=Business")
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_ChangeInfoMng&group=Business")
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New ProfileBusinessRepository
        Dim gID As Decimal
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.ActionName = "CHECK_DIRECTMANAGER" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Select Case CurrentState
                    Case CommonMessage.STATE_NEW
                        If rep.InsertWorking1(WorkingForMessage, gID) Then
                            Dim str As String = "getRadWindow().close('1');"
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)

                            'Clear all input
                            ClearControlValue(txtEmployeeCode, txtEmployeeName,
                                              txtTitleNameOld,
                            txtDecisionTypeOld,
                                              rdEffectDateOld, rdExpireDateOld, rdEffectDate, rdExpireDate,
                                               rdSignDate, txtSignName, txtSignTitle,
                                              txtOrgName, txtRemark)
                            'cboStaffRank.Text = String.Empty
                            cboTitle.Text = String.Empty
                            cboDecisionType.Text = String.Empty
                            chkIsProcess.Checked = False
                            Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_ChangeInfoMng&group=Business")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                    Case CommonMessage.STATE_EDIT
                        WorkingForMessage.ID = Decimal.Parse(hidID.Value)
                        If rep.ModifyWorking1(WorkingForMessage, gID) Then
                            'Dim str As String = "getRadWindow().close('1');"
                            'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            ''POPUPTOLINK
                            Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_ChangeInfoMng&group=Business")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                End Select
            Else
                Exit Sub
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Function IsWageDecisionType(ByVal id As Decimal) As Boolean
        Using rep As New ProfileRepository
            Dim otherlist = rep.GetOtherList(ProfileCommon.DECISION_TYPE.Name)
            If otherlist IsNot Nothing Then
                Return otherlist.AsEnumerable().Any(Function(f) (f.Item("CODE") = ProfileCommon.DECISION_TYPE.Promotion Or
                                                        f.Item("CODE") = ProfileCommon.DECISION_TYPE.AffectSalary) And
                                                  f.Item("ID") = id)
            End If
        End Using
        Return False
    End Function
    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary> Event click button tim kiem Nhan vien</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFindCommon_Click(ByVal sender As Object,
                                        ByVal e As EventArgs) Handles _
                                        btnFindEmployee.Click,
                                        btnFindSign.Click,
                                        btnEmpReplace.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case sender.ID
                Case btnFindEmployee.ID
                    isLoadPopup = 1
                Case btnFindSign.ID
                    isLoadPopup = 2
                Case btnEmpReplace.ID
                    isLoadPopup = 5
            End Select

            UpdateControlState()

            Select Case sender.ID
                Case btnFindEmployee.ID
                    ctrlFindEmployeePopup.Show()
                Case btnFindSign.ID
                    ctrlFindSigner.MustHaveContract = True
                    ctrlFindSigner.LoadAllOrganization = False
                    ctrlFindSigner.IsOnlyWorkingWithoutTer = True
                    ctrlFindSigner.Show()
                Case btnEmpReplace.ID
                    ctrlFindEmployeeRePopup.Show()
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary> Event huy popup List Nhan vien </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFind_CancelClick(ByVal sender As Object,
                                     ByVal e As System.EventArgs) Handles _
                                 ctrlFindEmployeePopup.CancelClicked,
                                 ctrlFindSigner.CancelClicked,
                                 ctrlFindOrgPopup.CancelClicked,
                                 ctrlFindEmployeeRePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary>Event Ok popup List Nhan vien (Giao dien 1)</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim empID = ctrlFindEmployeePopup.SelectedEmployeeID(0)

            FillData(empID)

            'rntxtPercentSalary.Value = 100
            isLoadPopup = 0

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary>Event Ok popup List Nhan vien (Giao dien 1)</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeeRePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeeRePopup.EmployeeSelected
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim JobTemp As Integer
        Try
            Dim empRelace = ctrlFindEmployeeRePopup.SelectedEmployee(0)
            hidEmpRe.Value = empRelace.ID
            txtEmpReplace.Text = empRelace.FULLNAME_VN

            Dim DSdata As DataSet
            Using rep As New ProfileRepository
                DSdata = rep.GET_JP_TO_TITLE(empRelace.ORG_ID, empRelace.TITLE_ID, chkIsReplace.Checked, 0)
                JobTemp = rep.GET_JOB_EMP(empRelace.ID)
            End Using

            cboJobPosition.DataSource = DSdata.Tables(0)
            cboJobPosition.DataTextField = "NAME"
            cboJobPosition.DataValueField = "ID"
            cboJobPosition.DataBind()

            If JobTemp <> 0 Then
                cboJobPosition.SelectedValue = JobTemp
            End If



            isLoadPopup = 0

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary>Event Ok popup List Nhan vien (Giao dien 2)</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindSigner_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindSigner.EmployeeSelected
        Dim objEmployee As CommonBusiness.EmployeePopupFindDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            objEmployee = ctrlFindSigner.SelectedEmployee(0)

            txtSignName.Text = objEmployee.FULLNAME_VN
            txtSignTitle.Text = objEmployee.TITLE_NAME
            hidSign.Value = objEmployee.EMPLOYEE_ID
            isLoadPopup = 0

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim bCheck As Boolean = False
        Try

            If txtUpload.Text <> "" Then
                Dim strPath_Down As String
                If FileOldName = txtUpload.Text.Trim Or FileOldName Is Nothing Then
                    If txtRemindLink.Text IsNot Nothing Then
                        If txtRemindLink.Text <> "" Then
                            strPath_Down = Server.MapPath("~/ReportTemplates/Profile/WorkingInfo/" + txtRemindLink.Text)
                            'bCheck = True
                            ZipFiles(strPath_Down, 1)
                        End If
                    End If
                Else
                    If Down_File <> "" Then
                        strPath_Down = Server.MapPath("~/ReportTemplates/Profile/WorkingInfo/" + Down_File)
                        'bCheck = True
                        ZipFiles(strPath_Down, 1)
                    End If
                End If
                'If bCheck Then
                '    ZipFiles(strPath_Down)
                'End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub btnUploadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFile.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

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
            txtUploadFile.Text = ""
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

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/WorkingInfo/")
            If ctrlUpload1.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload1.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(i)
                    Dim str_Filename = Guid.NewGuid.ToString() + "\"
                    If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        strPath = strPath + str_Filename
                        fileName = System.IO.Path.Combine(strPath, file.FileName)
                        file.SaveAs(fileName, True)
                        txtUploadFile.Text = file.FileName
                        Down_File = str_Filename
                    Else
                        ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                loadDatasource(txtUploadFile.Text)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub btnDownloadOld_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownloadOld.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If txtFileAttach_Link.Text <> "" Then
                Dim strPath_Down As String = Server.MapPath("~/ReportTemplates/Profile/WorkingInfo/" + txtFileAttach_Link1.Text)
                'bCheck = True
                ZipFiles(strPath_Down, 2)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub loadDatasource(ByVal strUpload As String)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try

            If strUpload <> "" Then
                txtUploadFile.Text = strUpload
                FileOldName = txtUpload.Text
                txtUpload.Text = strUpload
            Else

                strUpload = String.Empty
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ZipFiles(ByVal path As String, ByVal _ID As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim crc As New Crc32()
            'Dim pathZip As String = AppDomain.CurrentDomain.BaseDirectory & "Zip\"
            'Dim fileNameZip As String = "ThongTinKhenThuong.zip"
            Dim fileNameZip As String
            If _ID = 1 Then
                fileNameZip = txtUploadFile.Text.Trim
            Else
                fileNameZip = txtFileAttach_Link.Text.Trim
            End If
            'If Not Directory.Exists(pathZip) Then
            '    Directory.CreateDirectory(pathZip)
            'Else
            '    For Each deleteFile In Directory.GetFiles(pathZip, "*.*", SearchOption.TopDirectoryOnly)
            '        File.Delete(deleteFile)
            '    Next
            'End If

            'Dim s As New ZipOutputStream(File.Create(pathZip & fileNameZip))
            's.SetLevel(0)
            '' 0 - store only to 9 - means best compression
            'For i As Integer = 0 To Directory.GetFiles(path).Length - 1
            '    ' Must use a relative path here so that files show up in the Windows Zip File Viewer
            '    ' .. hence the use of Path.GetFileName(...)
            '    Dim fileName As String = System.IO.Path.GetFileName(Directory.GetFiles(path)(i))

            '    Dim entry As New ZipEntry(fileName)
            '    entry.DateTime = DateTime.Now

            '    ' Read in the 
            '    Using fs As FileStream = File.Open(Directory.GetFiles(path)(i), FileMode.Open)
            '        Dim buffer As Byte() = New Byte(fs.Length - 1) {}
            '        fs.Read(buffer, 0, buffer.Length)
            '        entry.Size = fs.Length
            '        fs.Close()
            '        crc.Reset()
            '        crc.Update(buffer)
            '        entry.Crc = crc.Value
            '        s.PutNextEntry(entry)
            '        s.Write(buffer, 0, buffer.Length)
            '    End Using
            'Next
            's.Finish()
            's.Close()

            'Using FileStream = File.Open(path & fileNameZip, FileMode.Open)
            '    Dim buffer As Byte() = New Byte(FileStream.Length - 1) {}
            '    FileStream.Read(buffer, 0, buffer.Length)
            '    Dim rEx As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9_\-\.]+")
            '    Response.Clear()
            '    Response.AddHeader("Content-Disposition", "attachment; filename=" + rEx.Replace(fileNameZip, "_"))
            '    Response.AddHeader("Content-Length", FileStream.Length.ToString())
            '    Response.ContentType = "application/octet-stream"
            '    Response.BinaryWrite(buffer)
            '    FileStream.Close()
            'End Using

            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path & fileNameZip)
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

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary> Event selected Item combobox </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cboCommon_ItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs) _
                                                    Handles cboStatus.ItemsRequested,
                                                            cboTitle.ItemsRequested

        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Using rep As New ProfileRepository
                Dim dtData As DataTable = Nothing
                Dim sText As String = e.Text
                Dim dValue As Decimal
                Select Case sender.ID

                    Case cboStatus.ID
                        dtData = rep.GetOtherList("DECISION_STATUS", True)

                    Case cboTitle.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        dtData = rep.GetTitleByOrgID(dValue, True)

                        'Case cboStaffRank.ID
                        '    dtData = rep.GetStaffRankList(True)

                End Select

                If sText <> String.Empty Then
                    Dim dtExist = (From p In dtData
                                   Where p("NAME") IsNot DBNull.Value AndAlso
                                   p("NAME").ToString.ToUpper = sText.ToUpper)

                    If dtExist.Count = 0 Then
                        Dim dtFilter = (From p In dtData
                                        Where p("NAME") IsNot DBNull.Value AndAlso
                                        p("NAME").ToString.ToUpper.Contains(sText.ToUpper))

                        If dtFilter.Count > 0 Then
                            dtData = dtFilter.CopyToDataTable
                        Else
                            dtData = dtData.Clone
                        End If

                        Dim itemOffset As Integer = e.NumberOfItems
                        Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                        e.EndOfItems = endOffset = dtData.Rows.Count

                        For i As Integer = itemOffset To endOffset - 1
                            Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                            Select Case sender.ID
                                'Case cboSalRank.ID
                                '    radItem.Attributes("SALARY_BASIC") = dtData.Rows(i)("SALARY_BASIC").ToString()
                                Case cboTitle.ID
                                    radItem.Attributes("GROUP_NAME") = dtData.Rows(i)("GROUP_NAME").ToString()
                            End Select
                            sender.Items.Add(radItem)
                        Next

                    Else

                        Dim itemOffset As Integer = e.NumberOfItems
                        Dim endOffset As Integer = dtData.Rows.Count
                        e.EndOfItems = True

                        For i As Integer = itemOffset To endOffset - 1
                            Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                            Select Case sender.ID
                                'Case cboSalRank.ID
                                '    radItem.Attributes("SALARY_BASIC") = dtData.Rows(i)("SALARY_BASIC").ToString()
                                Case cboTitle.ID
                                    radItem.Attributes("GROUP_NAME") = dtData.Rows(i)("GROUP_NAME").ToString()
                            End Select
                            sender.Items.Add(radItem)
                        Next

                    End If
                Else
                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                    e.EndOfItems = endOffset = dtData.Rows.Count

                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                        Select Case sender.ID
                            'Case cboSalRank.ID
                            '    radItem.Attributes("SALARY_BASIC") = dtData.Rows(i)("SALARY_BASIC").ToString()
                            Case cboTitle.ID
                                radItem.Attributes("GROUP_NAME") = dtData.Rows(i)("GROUP_NAME").ToString()
                        End Select
                        sender.Items.Add(radItem)
                    Next
                End If

            End Using

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary> Event Ok popup List don vi </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            Dim dtData As DataTable = Nothing
            If orgItem IsNot Nothing Then
                hidOrg.Value = e.CurrentValue
                txtOrgName.Text = orgItem.NAME_VN
                'txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
                Using rep As New ProfileRepository
                    If IsNumeric(e.CurrentValue) Then
                        dtData = rep.GetTitleByOrgID(Decimal.Parse(e.CurrentValue), True)
                        cboTitle.ClearValue()
                        cboTitle.Items.Clear()
                        For Each item As DataRow In dtData.Rows
                            Dim radItem As RadComboBoxItem = New RadComboBoxItem(item("NAME").ToString(), item("ID").ToString())
                            radItem.Attributes("GROUP_NAME") = item("GROUP_NAME").ToString()
                            cboTitle.Items.Add(radItem)
                        Next
                    End If
                End Using
            End If
            isLoadPopup = 0
            Session.Remove("CallAllOrg")
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary> Event click button tim kiem don vi </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindOrg.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            isLoadPopup = 3
            UpdateControlState()
            ctrlFindOrgPopup.Show()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary>
    ''' Validate Ngay Hieu Luc phai > Ngay Hieu Luc gan nhat (get tu DB)
    ''' rep.ValidateWorking -> Check Ngay Hieu Luc gan nhat
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cusExistEffectDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusExistEffectDate.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If hidEmp.Value = "" Then
                args.IsValid = True
                Exit Sub
            End If

            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    Using rep As New ProfileBusinessRepository
                        args.IsValid = rep.ValidateWorking("EXIST_EFFECT_DATE",
                                                            New WorkingDTO With {
                                                                .EMPLOYEE_ID = hidEmp.Value,
                                                                .EFFECT_DATE = rdEffectDate.SelectedDate})
                    End Using

                Case CommonMessage.STATE_EDIT
                    Using rep As New ProfileBusinessRepository
                        args.IsValid = rep.ValidateWorking("EXIST_EFFECT_DATE",
                                                            New WorkingDTO With {
                                                                .ID = hidID.Value,
                                                                .EMPLOYEE_ID = hidEmp.Value,
                                                                .EFFECT_DATE = rdEffectDate.SelectedDate})
                    End Using

                Case Else
                    args.IsValid = True
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>07/07/2017</lastupdate>
    ''' <summary>
    ''' Validate combobox trạng thái
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalStatus_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalStatus.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New OtherListDTO
        Dim dtData As DataTable = Nothing
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboStatus.SelectedValue
                validate.ACTFLG = "A"
                validate.CODE = "DECISION_STATUS"
                args.IsValid = rep.ValidateOtherList(validate)
            End If
            If Not args.IsValid Then
                dtData = rep.GetOtherList("DECISION_STATUS", True)
                FillRadCombobox(cboStatus, dtData, "NAME", "ID", True)

                cboStatus.Items.Insert(0, New RadComboBoxItem("", ""))
                cboStatus.ClearSelection()
                cboStatus.SelectedIndex = 0
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindManager_EmployeeSelected(sender As Object, e As System.EventArgs) Handles ctrlFindManager.EmployeeSelected
        Dim objEmployee As CommonBusiness.EmployeePopupFindDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            objEmployee = ctrlFindManager.SelectedEmployee(0)
            'txtManagerNew.Text = objEmployee.FULLNAME_VN
            hidManager.Value = objEmployee.EMPLOYEE_ID
            isLoadPopup = 0
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary>
    ''' Load control, Khoi tao popup tim Ma Nhan Vien, Tim Don Vi To Chuc
    ''' Set Trang thai control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    Dim dtdata As DataTable = Nothing
                    dtdata = (New ProfileRepository).GetOtherList("DECISION_STATUS", True)
                    If dtdata IsNot Nothing AndAlso dtdata.Rows.Count > 0 Then
                        FillRadCombobox(cboStatus, dtdata, "NAME", "ID", True)
                        If cboStatus.Text = "Phê duyệt" Then
                            cboStatus.SelectedIndex = 2
                        Else
                            cboStatus.SelectedIndex = 1
                        End If
                    End If
                Case CommonMessage.STATE_EDIT
                    EnableControlAll(False, btnFindEmployee)
            End Select

            Select Case isLoadPopup
                Case 1
                    If Not phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmp.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.LoadAllOrganization = False
                        ctrlFindEmployeePopup.MustHaveContract = True
                    End If

                Case 2
                    If Not phFindSign.Controls.Contains(ctrlFindSigner) Then
                        ctrlFindSigner = Me.Register("ctrlFindSigner", "Common", "ctrlFindEmployeePopup")
                        phFindSign.Controls.Add(ctrlFindSigner)
                        ctrlFindSigner.MultiSelect = False
                        ctrlFindSigner.LoadAllOrganization = True
                        ctrlFindSigner.MustHaveContract = False
                    End If

                Case 3
                    If cboDecisionType.Text = "Quyết định điều động khác pháp nhân" Then
                        HttpContext.Current.Session("CallAllOrg") = "LoadAllOrg"
                    Else
                        Session.Remove("CallAllOrg")
                    End If
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    phFindOrg.Controls.Add(ctrlFindOrgPopup)

                Case 4
                    If Not phFindSign.Controls.Contains(ctrlFindManager) Then
                        ctrlFindManager = Me.Register("ctrlFindManager", "Common", "ctrlFindEmployeePopup")
                        phFindSign.Controls.Add(ctrlFindManager)
                        ctrlFindManager.MultiSelect = False
                        ctrlFindManager.LoadAllOrganization = True
                        ctrlFindManager.MustHaveContract = False
                    End If
                Case 5
                    If Not phFindEmpRe.Controls.Contains(ctrlFindEmployeeRePopup) Then
                        ctrlFindEmployeeRePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmpRe.Controls.Add(ctrlFindEmployeeRePopup)
                        ctrlFindEmployeeRePopup.MultiSelect = False
                        ctrlFindEmployeeRePopup.LoadAllOrganization = False
                        ctrlFindEmployeeRePopup.MustHaveContract = True
                    End If
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary> Get Data theo ID Thay doi thong tin Nhan Vien (page o trang thai sua) </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    Dim ID As String = Request.Params("ID")
                    If Working Is Nothing Then
                        Working = New WorkingDTO With {.ID = Decimal.Parse(ID)}
                    End If
                    hidID.Value = Working.ID
                    Refresh("UpdateView")
                    isEdit = 1
                    Exit Sub
                End If

                If Request.Params("empID") IsNot Nothing Then
                    Dim empID = Request.Params("empID")
                    FillData(empID)
                End If

                Refresh("NormalView")
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary> Fill data theo ID Thay doi thong tin Nhan Vien </summary>
    ''' <param name="empid"></param>
    ''' <remarks></remarks>
    Private Sub FillData(ByVal empid As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Using rep As New ProfileBusinessRepository
                Dim obj = rep.GetEmployeCurrentByID(New WorkingDTO With {.EMPLOYEE_ID = empid})

                If obj.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                    ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                    Exit Sub
                End If
                hidEmp.Value = obj.EMPLOYEE_ID
                txtEmployeeCode.Text = obj.EMPLOYEE_CODE
                txtEmployeeName.Text = obj.EMPLOYEE_NAME
                txtTitleNameOld.Text = obj.TITLE_NAME
                txtDecisionold.Text = obj.DECISION_NO
                txtDecision.Text = obj.DECISION_NO
                txtDecisionTypeOld.Text = obj.DECISION_TYPE_NAME
                cboDecisionType.SelectedValue = obj.DECISION_TYPE_ID
                txtOrgNameOld.Text = obj.ORG_NAME
                rdEffectDateOld.SelectedDate = obj.EFFECT_DATE
                rdExpireDateOld.SelectedDate = obj.EXPIRE_DATE

                ' thong tin them moi
                txtJobPositionOld.Text = obj.JOB_POSITION_NAME
                txtJobDescriptionOld.Text = obj.JOB_DESCRIPTION_NAME
                rdSignDateOld.SelectedDate = obj.SIGN_DATE
                txtSignNameOld.Text = obj.SIGN_NAME
                txtSignTitleOld.Text = obj.SIGN_TITLE
                txtRemarkOld.Text = obj.REMARK


                If obj.FILENAME IsNot Nothing Then
                    lbFileAttach.Text = obj.FILENAME
                    txtFileAttach_Link.Text = obj.FILENAME
                    txtFileAttach_Link1.Text = obj.ATTACH_FILE
                End If

                txtUploadFile.Text = obj.FILENAME
                txtRemindLink.Text = If(obj.ATTACH_FILE Is Nothing, "", obj.ATTACH_FILE)
                loadDatasource(txtUploadFile.Text)
                FileOldName = If(FileOldName = "", txtUpload.Text, FileOldName)

                chkIsHurtful.Checked = obj.IS_HURTFUL
                If IsDate(obj.EFFECT_DH_DATE) Then
                    rdEffectHdDate.SelectedDate = obj.EFFECT_DH_DATE
                End If

                Dim dtdata As DataTable = Nothing
                If obj.ORG_ID IsNot Nothing Then
                    txtOrgName.Text = obj.ORG_NAME
                    hidOrg.Value = obj.ORG_ID
                    If IsNumeric(hidOrg.Value) Then
                        dtdata = (New ProfileRepository).GetTitleByOrgID(Decimal.Parse(hidOrg.Value), True)
                        cboTitle.ClearValue()
                        cboTitle.Items.Clear()
                        For Each item As DataRow In dtdata.Rows
                            Dim radItem As RadComboBoxItem = New RadComboBoxItem(item("NAME").ToString(), item("ID").ToString())
                            radItem.Attributes("GROUP_NAME") = item("GROUP_NAME").ToString()
                            cboTitle.Items.Add(radItem)
                        Next
                    End If
                End If
                If obj.TITLE_ID IsNot Nothing Then
                    cboTitle.SelectedValue = obj.TITLE_ID
                    cboTitle.Text = obj.TITLE_NAME
                End If
                If obj.DECISION_TYPE_ID IsNot Nothing Then
                    cboDecisionType.SelectedValue = obj.DECISION_TYPE_ID
                    cboDecisionType.Text = obj.DECISION_TYPE_NAME
                End If

                ' them moi cac thong tin dieu chinh
                Dim DSdata As DataSet
                Using rep1 As New ProfileRepository
                    DSdata = rep1.GET_JP_TO_TITLE(hidOrg.Value, cboTitle.SelectedValue, chkIsReplace.Checked, obj.JOB_POSITION)
                End Using

                cboJobPosition.DataSource = DSdata.Tables(0)
                cboJobPosition.DataTextField = "NAME"
                cboJobPosition.DataValueField = "ID"
                cboJobPosition.DataBind()
                cboJobPosition.SelectedValue = obj.JOB_POSITION

                cboJobDescription.DataSource = DSdata.Tables(1)
                cboJobDescription.DataTextField = "NAME"
                cboJobDescription.DataValueField = "ID"
                cboJobDescription.DataBind()
                cboJobDescription.SelectedValue = obj.JOB_DESCRIPTION

            End Using

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region
#Region "Util"
    Public Function GetYouMustChoseMsg(ByVal input) As String
        Return String.Format("{0} {1}", Errors.YouMustChose, input)
    End Function
    Public Function GetNullMsg(ByVal input) As String
        Return String.Format("{0} {1}", Errors.NullOrInActive, input)
    End Function

#End Region

    'Private Sub cboDecisionType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboDecisionType.SelectedIndexChanged
    '    Try
    '        'nếu muốn chỉnh sửa k clear thì mở ở dưới ra
    '        ' If isEdit <> 1 Then
    '        If cboDecisionType.SelectedValue = 7561 Then
    '            btnFindOrg.Enabled = False
    '            cboTitle.Enabled = False
    '            'If ctrlFindEmployeePopup.SelectedEmployeeID Is Nothing Then
    '            If hidEmp.Value <> "" Then
    '                Dim empID = hidEmp.Value
    '                FillData(empID)
    '            End If
    '        Else
    '            btnFindOrg.Enabled = True
    '            cboTitle.Enabled = True
    '            txtOrgName.Text = ""
    '            hidOrg.ClearValue()
    '            cboTitle.ClearValue()
    '        End If
    '        ' End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub cboTitle_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTitle.SelectedIndexChanged
        Try
            Dim DSdata As DataSet

            cboJobPosition.ClearValue()
            cboJobDescription.ClearValue()

            Using rep As New ProfileRepository
                DSdata = rep.GET_JP_TO_TITLE(hidOrg.Value, cboTitle.SelectedValue, chkIsReplace.Checked, 0)
            End Using

            'If DSdata.Tables(0).Rows.Count > 0 Then
            cboJobPosition.DataSource = DSdata.Tables(0)
            cboJobPosition.DataTextField = "NAME"
            cboJobPosition.DataValueField = "ID"
            cboJobPosition.DataBind()
            ' End If

            ' If DSdata.Tables(1).Rows.Count > 0 Then
            cboJobDescription.DataSource = DSdata.Tables(1)
            cboJobDescription.DataTextField = "NAME"
            cboJobDescription.DataValueField = "ID"
            cboJobDescription.DataBind()
            ' End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chkIsReplace_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkIsReplace.CheckedChanged
        Try
            Dim DSdata As DataSet
            cboJobPosition.ClearValue()
            Using rep As New ProfileRepository
                DSdata = rep.GET_JP_TO_TITLE(hidOrg.Value, cboTitle.SelectedValue, chkIsReplace.Checked, 0)
            End Using


            cboJobPosition.DataSource = DSdata.Tables(0)
            cboJobPosition.DataTextField = "NAME"
            cboJobPosition.DataValueField = "ID"
            cboJobPosition.DataBind()

            If chkIsReplace.Checked Then
                btnEmpReplace.Enabled = True
            Else
                btnEmpReplace.Enabled = False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
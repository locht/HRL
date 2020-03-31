Imports Framework.UI
Imports Common
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports Framework.UI.Utilities
Imports WebAppLog
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Public Class ctrlHU_TerminateNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = True
    Public WithEvents AjaxManager As RadAjaxManager

    Private psp As New ProfileStoreProcedure
    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _year As Decimal = Year(DateTime.Now)
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

#Region "Property"
    ''' <summary>
    ''' IDDetailSelecting
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IDDebtSelecting As Decimal?
        Get
            Return ViewState(Me.ID & "_IDDebtSelecting")
        End Get

        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_IDDebtSelecting") = value
        End Set
    End Property
    Property lstHandoverContent As List(Of HandoverContentDTO)
        Get
            Return ViewState(Me.ID & "_lstHandoverContent")
        End Get
        Set(ByVal value As List(Of HandoverContentDTO))
            ViewState(Me.ID & "_lstHandoverContent") = value
        End Set
    End Property
    Property lstDebtForEdit As List(Of DebtDTO)
        Get
            Return ViewState(Me.ID & "_lstDebtForEdit")
        End Get
        Set(ByVal value As List(Of DebtDTO))
            ViewState(Me.ID & "_lstDebtForEdit") = value
        End Set
    End Property
    'Property lstReason As List(Of TerminateReasonDTO)
    '    Get
    '        Return ViewState(Me.ID & "_lstReason")
    '    End Get
    '    Set(ByVal value As List(Of TerminateReasonDTO))
    '        ViewState(Me.ID & "_lstReason") = value
    '    End Set
    'End Property

    Property Terminate As TerminateDTO
        Get
            Return ViewState(Me.ID & "_Terminate")
        End Get
        Set(ByVal value As TerminateDTO)
            ViewState(Me.ID & "_Terminate") = value
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

    Property objTerminate As TerminateDTO
        Get
            Return ViewState(Me.ID & "_objTerminate")
        End Get
        Set(ByVal value As TerminateDTO)
            ViewState(Me.ID & "_objTerminate") = value
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

    Property SelectOrg As String
        Get
            Return ViewState(Me.ID & "_SelectOrg")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_SelectOrg") = value
        End Set
    End Property

    '0 - normal
    '1 - Employee
    '2 - Sign
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    '0 - Declare
    '1 - Extent
    '2 - Details
    Dim FormType As Integer
    Dim IDSelect As Decimal?
#End Region

#Region "Page"
    ''' <summary>
    ''' Khởi tạo, Load page, load info cho control theo ID
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            GetParams()
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Khởi tạo load control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgHandoverContent.AllowSorting = False

            Me.MainToolBar = tbarTerminate
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")

            If Not IsPostBack Then
                ViewConfig(RadPane2)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Load data cho combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileRepository
        Try
            GetDataCombo()
            'Dim TYPE_TERMINATE As New ArrayList()
            'TYPE_TERMINATE.Add(New DictionaryEntry("--Chọn loại nghỉ--", ""))
            'TYPE_TERMINATE.Add(New DictionaryEntry("Tự nguyện", "0"))
            'TYPE_TERMINATE.Add(New DictionaryEntry("Khác", "1"))
            'With cboTYPE_TERMINATE
            '    .DataSource = TYPE_TERMINATE
            '    .DataValueField = "VALUE"
            '    .DataTextField = "KEY"
            'End With
            'If ListComboData Is Nothing Then
            '    ListComboData = New ComboBoxDataDTO
            '    ListComboData.GET_TYPE_NGHI = True
            '    rep.GetComboList(ListComboData)
            'End If
            'FillDropDownList(cboTYPE_TERMINATE, ListComboData.LIST_TYPE_NGHI, "NAME_VN", "ID", Common.Common.SystemLanguage, True)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Khởi tạo, Load menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Reset, Load page theo trạng thái page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Terminate = rep.GetTerminateByID(New TerminateDTO With {.ID = IDSelect})
                    If Terminate.WORK_STATUS IsNot Nothing Then
                        hidWorkStatus.Value = Terminate.WORK_STATUS
                    End If
                    txtDecisionNo.Text = Terminate.DECISION_NO

                    If Terminate.IS_BLACK_LIST IsNot Nothing Then
                        chkDSDen.Checked = Terminate.IS_BLACK_LIST
                    End If

                    txtSignerName.Text = Terminate.SIGN_NAME
                    txtSignerTitle.Text = Terminate.SIGN_TITLE
                    txtTerReasonDetail.Text = Terminate.TER_REASON_DETAIL
                    txtRemark.Text = Terminate.REMARK

                    cboStatus.SelectedValue = Terminate.STATUS_ID.ToString

                    rdSignDate.SelectedDate = Terminate.SIGN_DATE
                    rdEffectDate.SelectedDate = Terminate.EFFECT_DATE
                    rdLastDate.SelectedDate = Terminate.LAST_DATE
                    rdSendDate.SelectedDate = Terminate.SEND_DATE
                    rdSignDate.SelectedDate = Terminate.SIGN_DATE
                    'rdContractExpireDate.SelectedDate = Terminate.EXPIRE_DATE

                    hidDecisionID.Value = Terminate.DECISION_ID.ToString
                    hidEmpID.Value = Terminate.EMPLOYEE_ID
                    hidTitleID.Value = Terminate.TITLE_ID
                    hidOrgID.Value = Terminate.ORG_ID
                    hidID.Value = Terminate.ID.ToString
                    FillDataByEmployeeID(Terminate.EMPLOYEE_ID)

                    

                  
                    If Terminate.TER_REASON.HasValue Then
                        cboTerReason.SelectedValue = Terminate.TER_REASON
                    End If
                   
                    If Terminate.DECISION_TYPE.HasValue Then
                        cboDecisionType.SelectedValue = Terminate.DECISION_TYPE.ToString
                    End If

                    lstHandoverContent = Terminate.lstHandoverContent
                   
                    rgHandoverContent.Rebind()
                    For Each i As GridItem In rgHandoverContent.Items
                        i.Edit = True
                    Next
                    rgHandoverContent.Rebind()
                    txtUploadFile.Text = Terminate.FILENAME
                    txtRemindLink.Text = If(Terminate.UPLOADFILE Is Nothing, "", Terminate.UPLOADFILE)
                    loadDatasource(txtUploadFile.Text)
                    FileOldName = If(FileOldName = "", txtUpload.Text, FileOldName)
                    ' phê duyệt và ko phê duyêt
                    If Terminate.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Or
                        Terminate.STATUS_ID = ProfileCommon.DECISION_STATUS.NOT_APPROVE_ID Then
                        EnableControlAll_Cus(False, RadPane2)
                        btnDownload.Enabled = True
                        btnUploadFile.Enabled = True
                        rgHandoverContent.Enabled = True
                        rgHandoverContent.Enabled = True
                        MainToolBar.Items(0).Enabled = True
                       
                    End If

                Case "InsertView"
                    lstHandoverContent = New List(Of HandoverContentDTO)
                    For Each obj In ListComboData.LIST_HANDOVER_CONTENT
                        Dim objHandover As New HandoverContentDTO
                        objHandover.CONTENT_ID = obj.ID
                        objHandover.CONTENT_NAME = obj.NAME_VN
                        lstHandoverContent.Add(objHandover)
                    Next
                   
                    rgHandoverContent.Rebind()
                    For Each i As GridItem In rgHandoverContent.Items
                        i.Edit = True
                    Next
                    rgHandoverContent.Rebind()
                    cboStatus.SelectedValue = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID
                    cboDecisionType.SelectedIndex = 0
                    CurrentState = CommonMessage.STATE_NEW
                Case "NormalView"

                    Refresh("UpdateView")
                    txtDecisionNo.ReadOnly = True
                    btnFindEmployee.Enabled = False
                    btnFindSinger.Enabled = False
                    CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    txtSignerName.ReadOnly = True
                    txtSignerTitle.ReadOnly = True
                    txtTerReasonDetail.ReadOnly = True
                    txtRemark.ReadOnly = True

                    cboStatus.Enabled = False
                    txtDecisionNo.Enabled = False

                    rdSignDate.Enabled = False
                    rdEffectDate.Enabled = False
                    rdLastDate.Enabled = False
                    rdSendDate.Enabled = False
                    rdSignDate.Enabled = False

                    txtContractNo.ReadOnly = True
                    txtEmployeeCode.ReadOnly = True
                    txtEmployeeName.ReadOnly = True
                    txtTitleName.ReadOnly = True
                    txtOrgName.ReadOnly = True
                    rdJoinDateState.Enabled = False
                    rdContractEffectDate.Enabled = False
                    rdContractExpireDate.Enabled = False

                    cboTerReason.Enabled = False
                    cboDecisionType.Enabled = False
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    Protected Sub rdEffectDate_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdEffectDate.SelectedDateChanged
        Dim item As New ContractTypeDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If rdEffectDate.SelectedDate IsNot Nothing Then
                Dim dExpire As Date = rdEffectDate.SelectedDate
                dExpire = dExpire.AddDays(CType(-1, Double))
                rdLastDate.SelectedDate = dExpire

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
#End Region

#Region "Event"
    ''' <summary>
    ''' Event Click item trên menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim rep_Store As New ProfileStoreProcedure
        Dim _filter As New TerminateDTO
        Dim dtData As New DataTable
        Dim _objfilter As New TerminateDTO
        Dim gid As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If rep_Store.CHECK_TER_EMPEXIST(Decimal.Parse(If(hidID.Value = "", 0, hidID.Value)), Decimal.Parse(hidEmpID.Value)) = True Then
                            ShowMessage(Translate("Nhân viên có mã số {0} đã có đơn được phê duyệt. Vui lòng kiểm tra lại !", txtEmployeeCode.Text), NotifyType.Warning)
                            Exit Sub
                        End If
                        
                        If cboStatus.SelectedValue = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                            If txtUpload.Text.Trim = "" Then
                                ShowMessage(Translate("Bạn phải chọn tập tin đính kèm"), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If
                        'If cboStatus.SelectedValue = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID Then
                        'If txtDecisionNo.Text = "" Then
                        '    ShowMessage(Translate("Bạn phải nhập số quyết định"), NotifyType.Warning)
                        '    Exit Sub
                        'End If
                        'End If

                        _objfilter.DECISION_NO = txtDecisionNo.Text
                        If CurrentState = Common.CommonMessage.STATE_EDIT Then
                            _objfilter.ID = hidID.Value
                        End If
                        If Not String.IsNullOrWhiteSpace(txtDecisionNo.Text) Then
                            If Not rep.CheckTerminateNo(_objfilter) Then
                                ShowMessage(Translate("Số quyết định đã tồn tại. Thao tác thực hiện không thành công"), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If


                        objTerminate = New TerminateDTO
                        objTerminate.DECISION_NO = txtDecisionNo.Text
                        objTerminate.EMPLOYEE_ID = hidEmpID.Value
                        objTerminate.ORG_ID = hidOrgID.Value
                        objTerminate.TITLE_ID = hidTitleID.Value
                        objTerminate.STATUS_ID = cboStatus.SelectedValue
                        If CurrentState = Common.CommonMessage.STATE_EDIT Then
                            objTerminate.ID = Decimal.Parse(hidDecisionID.Value)
                        End If
                        objTerminate.FILENAME = txtUpload.Text.Trim
                        objTerminate.UPLOADFILE = If(Down_File Is Nothing, "", Down_File)
                        If objTerminate.UPLOADFILE = "" Then
                            objTerminate.UPLOADFILE = If(txtRemindLink.Text Is Nothing, "", txtRemindLink.Text)
                        Else
                            objTerminate.UPLOADFILE = If(objTerminate.UPLOADFILE Is Nothing, "", objTerminate.UPLOADFILE)
                        End If
                        objTerminate.LAST_DATE = rdLastDate.SelectedDate
                        objTerminate.SEND_DATE = rdSendDate.SelectedDate
                        objTerminate.TER_REASON_DETAIL = txtTerReasonDetail.Text

                        objTerminate.REMARK = txtRemark.Text

                        objTerminate.IS_BLACK_LIST = chkDSDen.Checked

                        objTerminate.EFFECT_DATE = rdEffectDate.SelectedDate
                        objTerminate.SIGN_NAME = txtSignerName.Text
                        objTerminate.SIGN_DATE = rdSignDate.SelectedDate

                        objTerminate.ORG_ABBR = hidOrgAbbr.Value

                        objTerminate.SIGN_TITLE = txtSignerTitle.Text

                        If cboTerReason.Text <> "" Then
                            objTerminate.TER_REASON = cboTerReason.SelectedValue
                        End If
                        objTerminate.DECISION_TYPE = cboDecisionType.SelectedValue.ToString
                        
                        lstHandoverContent = New List(Of HandoverContentDTO)
                        For Each item As GridDataItem In rgHandoverContent.Items
                            Dim handover As New HandoverContentDTO
                            handover.CONTENT_ID = item.GetDataKeyValue("CONTENT_ID")
                            handover.CONTENT_NAME = item.GetDataKeyValue("CONTENT_NAME")
                            Dim chk As CheckBox = CType(item("IS_FINISH").Controls(0), CheckBox)
                            handover.IS_FINISH = chk.Checked
                            lstHandoverContent.Add(handover)
                        Next
                        objTerminate.lstHandoverContent = lstHandoverContent

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertTerminate(objTerminate, gid) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Terminate&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                'If hidWorkStatus.Value = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                                '    ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), NotifyType.Warning)
                                '    Exit Sub
                                'End If

                                objTerminate.ID = Decimal.Parse(hidID.Value)
                                objTerminate.DECISION_ID = Decimal.Parse(hidDecisionID.Value)
                                Dim listID As New List(Of Decimal)
                                listID.Add(hidID.Value)
                                If rep.ValidateBusiness("HU_TERMINATE", "ID", listID) Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                    Exit Sub
                                End If
                                If rep.ModifyTerminate(objTerminate, gid) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Terminate&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select


                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    txtRemindLink.Text = ""
                    FileOldName = ""
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_Terminate&group=Business")
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Event Yes/No trên Message popup hỏi áp dụng, ngừng áp dụng
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New ProfileBusinessRepository
        Dim gID As Decimal
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Select Case CurrentState
                    Case CommonMessage.STATE_NEW
                        If rep.InsertTerminate(objTerminate, gID) Then
                            Dim str As String = "getRadWindow().close('1');"
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                    Case CommonMessage.STATE_EDIT
                        objTerminate.ID = Decimal.Parse(hidID.Value)
                        objTerminate.DECISION_ID = Decimal.Parse(hidDecisionID.Value)
                        If rep.ModifyTerminate(objTerminate, gID) Then
                            Dim str As String = "getRadWindow().close('1');"
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                End Select
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
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
                            strPath_Down = Server.MapPath("~/ReportTemplates/Profile/TerminateInfo/" + txtRemindLink.Text)
                            'bCheck = True
                            ZipFiles(strPath_Down)
                        End If
                    End If
                Else
                    If Down_File <> "" Then
                        strPath_Down = Server.MapPath("~/ReportTemplates/Profile/TerminateInfo/" + Down_File)
                        'bCheck = True
                        ZipFiles(strPath_Down)
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

    Private Sub ZipFiles(ByVal path As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim crc As New Crc32()
            'Dim pathZip As String = AppDomain.CurrentDomain.BaseDirectory & "Zip\"
            'Dim fileNameZip As String = "ThongTinKhenThuong.zip"
            Dim fileNameZip As String = txtUploadFile.Text.Trim

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
            Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/TerminateInfo/")
            If ctrlUpload1.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload1.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(i)
                    Dim str_Filename = Guid.NewGuid.ToString() + "\"
                    If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                        'If Commend IsNot Nothing Then
                        '    If Commend.UPLOADFILE IsNot Nothing Then
                        '        strPath += Commend.UPLOADFILE
                        '    Else
                        '        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        '        strPath = strPath + str_Filename
                        '    End If
                        '    fileName = System.IO.Path.Combine(strPath, file.FileName)
                        '    file.SaveAs(fileName, True)
                        '    Commend.UPLOADFILE = str_Filename
                        '    txtUploadFile.Text = file.FileName
                        'Else
                        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        strPath = strPath + str_Filename
                        fileName = System.IO.Path.Combine(strPath, file.FileName)
                        file.SaveAs(fileName, True)
                        txtUploadFile.Text = file.FileName
                        'End If
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

    Private Sub loadDatasource(ByVal strUpload As String)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            'Dim data As New DataTable
            'data.Columns.Add("FileName")
            'Dim row As DataRow
            'Dim str() As String

            If strUpload <> "" Then
                txtUploadFile.Text = strUpload
                FileOldName = txtUpload.Text
                txtUpload.Text = strUpload
                'str = strUpload.Split(";")

                'For Each s As String In str
                '    If s <> "" Then
                '        row = data.NewRow
                '        row("FileName") = s
                '        data.Rows.Add(row)
                '    End If
                'Next

                'cboUpload.DataSource = data
                'cboUpload.DataTextField = "FileName"
                'cboUpload.DataValueField = "FileName"
                'cboUpload.DataBind()
            Else
                'cboUpload.DataSource = Nothing
                'cboUpload.ClearSelection()
                'cboUpload.ClearCheckedItems()
                'cboUpload.Items.Clear()
                strUpload = String.Empty
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

    ''' <summary>
    ''' Event click button tìm mã nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFindSinger_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindSinger.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            isLoadPopup = 2
            UpdateControlState()
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

    ''' <summary>
    ''' Event button tìm mã nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFindEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindEmployee.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Event OK chọn mã nhân viên (giao diện 1)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                Using rep As New ProfileBusinessRepository
                    Dim check = rep.Check_has_Ter(item.ID)
                    If check = 1 Then
                        ShowMessage(Translate("Nhân viên đã có quyết định nghỉ việc"), NotifyType.Warning)
                        Exit Sub
                    End If
                End Using
                txtEmployeeCode.Text = item.EMPLOYEE_CODE
                'txtDecisionNo.Text = item.EMPLOYEE_CODE.Substring(1) + " / QDTV-KSF"
                FillDataByEmployeeID(item.ID)
                
                GetTerminateCalculate()
            End If

            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Fill data lên các control theo ID truyền đến
    ''' </summary>
    ''' <param name="gID"></param>
    ''' <remarks></remarks>
    Private Sub FillDataByEmployeeID(ByVal gID As Decimal)
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim emp = rep.GetEmployeeByID(gID)
            txtContractNo.Text = emp.CONTRACT_NO
            txtEmployeeCode.Text = emp.EMPLOYEE_CODE
            txtEmployeeName.Text = emp.FULLNAME_VN
            txtTitleName.Text = emp.TITLE_NAME_VN
            txtOrgName.Text = emp.ORG_NAME
            'txtStaffRankName.Text = emp.STAFF_RANK_NAME
            rdJoinDateState.SelectedDate = emp.JOIN_DATE
            rdContractEffectDate.SelectedDate = emp.CONTRACT_EFFECT_DATE
            rdContractExpireDate.SelectedDate = emp.CONTRACT_EXPIRE_DATE
            'rntxtCostSupport.Value = emp.COST_SUPPORT
            'rnt.Value = emp.SAL_BASIC

            hidOrgAbbr.Value = emp.ORG_ID
            hidEmpID.Value = emp.ID
            hidTitleID.Value = emp.TITLE_ID
            hidOrgID.Value = emp.ORG_ID
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    'Thâm niên công tác = ngày làm việc cuối - ngày vào làm
    'ngày < 15 = 0.5 ngày
    'ngày >= 15 = 1 tháng
    Private Function CalculateSeniority(ByVal dStart As Date, ByVal dEnd As Date) As String
        Dim dSoNam As Double
        Dim dSoThang As Double
        Dim dNgayDuThang As Double
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            Dim clsDate As DateDifference = New DateDifference(dStart, dEnd)
            dSoNam = clsDate.Years
            dSoThang = clsDate.Months
            dNgayDuThang = clsDate.Days

            If dNgayDuThang < 15 Then
                dSoThang = dSoThang + 0.5
            Else
                dSoThang = dSoThang + 1
                If dSoThang = 12 Then
                    dSoNam = dSoNam + 1
                    dSoThang = 0
                End If
            End If
            Dim str As String

            If dSoNam = 0 And dSoThang = 0 Then
                str = ""
            End If

            If dSoNam = 0 And dSoThang <> 0 Then
                str = dSoThang & " tháng"
            End If

            If dSoNam <> 0 And dSoThang = 0 Then
                str = dSoNam & " năm"
            End If

            If dSoNam <> 0 And dSoThang <> 0 Then
                str = dSoNam & " năm " & dSoThang & " tháng"
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Return str
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <summary>
    ''' Event OK chọn mã nhân viên (giao diện 2)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindSigner_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindSigner.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            lstCommonEmployee = CType(ctrlFindSigner.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                txtSignerName.Text = item.FULLNAME_VN
                txtSignerTitle.Text = item.TITLE_NAME
            End If
            isLoadPopup = 0
            'rgLabourProtect.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Cancel Popup list mã nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked, ctrlFindSigner.CancelClicked
        isLoadPopup = 0
    End Sub
    ''' <summary>
    ''' Event click radio button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rd_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdLastDate.SelectedDateChanged, rdJoinDateState.SelectedDateChanged, rdEffectDate.SelectedDateChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If rdJoinDateState.SelectedDate IsNot Nothing Then
                If rdEffectDate.SelectedDate IsNot Nothing Then
                    If rdJoinDateState.SelectedDate > rdLastDate.SelectedDate Then
                        ShowMessage(Translate("Ngày thôi việc phải lớn hơn hoặc bằng ngày vào công ty"), NotifyType.Warning)
                    End If
                End If
            End If
            GetTerminateCalculate()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Validate cval_lastdate_sendDate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub cval_LastDate_SendDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cval_LastDate_SendDate.ServerValidate
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If rdLastDate.SelectedDate IsNot Nothing And rdSendDate.SelectedDate IsNot Nothing Then
                If rdSendDate.SelectedDate > rdLastDate.SelectedDate Then
                    args.IsValid = False
                    Exit Sub
                End If
            End If

            args.IsValid = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Validate cvaldpApproveDatejoinDate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Protected Sub cvaldpApproveDateJoinDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvaldpApproveDateJoinDate.ServerValidate
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        If rdJoinDateState.SelectedDate IsNot Nothing And rdApprovalDate.SelectedDate IsNot Nothing Then
    '            If rdApprovalDate.SelectedDate < rdJoinDateState.SelectedDate Then
    '                args.IsValid = False
    '                Exit Sub
    '            End If
    '        End If

    '        args.IsValid = True
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    ''' <summary>
    ''' Validate cval_LastDate_JoinDate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub cval_LastDate_JoinDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cval_LastDate_JoinDate.ServerValidate
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If rdJoinDateState.SelectedDate IsNot Nothing And rdLastDate.SelectedDate IsNot Nothing Then
                If rdLastDate.SelectedDate < rdJoinDateState.SelectedDate Then
                    args.IsValid = False
                    Exit Sub
                End If
            End If

            args.IsValid = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Validate cvaldpSendDate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub cvaldpSendDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvaldpSendDate.ServerValidate
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If rdJoinDateState.SelectedDate IsNot Nothing And rdSendDate.SelectedDate IsNot Nothing Then
                If rdSendDate.SelectedDate < rdJoinDateState.SelectedDate Then
                    args.IsValid = False
                    Exit Sub
                End If
            End If

            args.IsValid = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    
    ''' <summary>
    ''' Reload, load datasource cho grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgHandoverContent_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgHandoverContent.NeedDataSource
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            CreateDataHandoverContent(lstHandoverContent)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Check value min, max grid ly do nghi viec
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgHandoverContent_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgHandoverContent.ItemDataBound
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            'If e.Item.Edit Then
            '    Dim edit = CType(e.Item, GridEditableItem)
            '    Dim rntxt As RadNumericTextBox
            '    rntxt = CType(edit("DENSITY").Controls(0), RadNumericTextBox)
            '    rntxt.MinValue = 0
            '    rntxt.MaxValue = 100
            '    rntxt.Width = Unit.Percentage(100)
            'End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

#End Region

#Region "Custom"
    ''' <summary>
    ''' Lay thong tin tien thanh toan nghi viec theo ID Nhan Vien
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetTerminateCalculate()
        Dim dt As DataTable
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim iYearAllow As Decimal, iSalaryMedium As Decimal = 0
        Try
            If hidEmpID.Value Is Nothing Or rdLastDate.SelectedDate Is Nothing Then
                'rntxtRemainingLeave.ClearValue()
                'rntxtPaymentLeave.ClearValue()
                'rntxtCompensatoryLeave.ClearValue()
                'rntxtCompensatoryPayment.ClearValue()
                'rntxtAllowanceTerminate.ClearValue()
                hiSalbasic.ClearValue()
                Exit Sub
            End If
            Using rep As New ProfileBusinessRepository
                dt = rep.CalculateTerminate(hidEmpID.Value, rdLastDate.SelectedDate)
            End Using
            If dt IsNot Nothing Then
                'Số phép năm còn lại
                'rntxtRemainingLeave.Value = Utilities.ObjToDecima(dt.Rows(0)("LEAVE"))
                'tiền thanh toán phép
                'rntxtPaymentLeave.Value = Utilities.ObjToDecima(dt.Rows(0)("MONEY_LEAVE"))
                'số ngày nghỉ bù còn lại
                'rntxtCompensatoryLeave.Value = Utilities.ObjToDecima(dt.Rows(0)("COMP_LEAVE"))
                'tiền thanh toán nghỉ bù
                'rntxtCompensatoryPayment.Value = Utilities.ObjToDecima(dt.Rows(0)("MONEY_COMP_LEAVE"))

                'trợ cấp thôi việc
                'rntxtAllowanceTerminate.Value = Utilities.ObjToDecima(dt.Rows(0)("SUPPORT_TERMINATE"))


                'Bổ xung lương trùng bình 6 tháng
                Dim dTemp = If(rdEffectDate.SelectedDate Is Nothing, 0, psp.SALARY_LAST_SIX_MONTH(hidEmpID.Value, rdEffectDate.SelectedDate))
                iYearAllow = If(dTemp <> 0, dTemp, 0)

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Khoi tao, load popup list ma Nhan vien
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
            End If
            If phFindSign.Controls.Contains(ctrlFindSigner) Then
                phFindSign.Controls.Remove(ctrlFindSigner)
                'Me.Views.Remove(ctrlFindSigner.ID.ToUpper)
            End If

            Select Case isLoadPopup
                Case 1
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.CurrentValue = SelectOrg
                    ctrlFindEmployeePopup.MustHaveContract = False
                    ctrlFindEmployeePopup.IS_3B = 2
                    phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = False
                Case 2
                    ctrlFindSigner = Me.Register("ctrlFindSigner", "Common", "ctrlFindEmployeePopup")
                    ctrlFindSigner.CurrentValue = SelectOrg
                    'ctrlFindSigner.MustHaveContract = True
                    ctrlFindSigner.LoadAllOrganization = True
                    phFindSign.Controls.Add(ctrlFindSigner)
                    ctrlFindSigner.MultiSelect = False
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Get data cho combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository

        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_TER_REASON = True
                ListComboData.GET_TER_STATUS = True
                ListComboData.GET_TYPE_NGHI = True
                ListComboData.GET_TYPE_INS_STATUS = True
                ListComboData.GET_DEBT_STATUS = True
                ListComboData.GET_DEBT_TYPE = True
                'ListComboData.GET_DECISION_TYPE = True
                ListComboData.GET_TER_DECISION_TYPE = True
                ListComboData.GET_HANDOVER_CONTENT = True
                rep.GetComboList(ListComboData)
            End If
            rep.Dispose()
            ' FillDropDownList(cboStatus, ListComboData.LIST_TER_STATUS, "NAME_VN", "ID", Common.Common.SystemLanguage, True)
            Dim dtData As New DataTable
            dtData = rep.GetOtherList(OtherTypes.DecisionStatus, True)
            FillRadCombobox(cboStatus, dtData, "NAME", "ID", True)
            FillDropDownList(cboTerReason, ListComboData.LIST_TER_REASON, "NAME_VN", "ID", Common.Common.SystemLanguage, True)
            FillRadCombobox(cboDecisionType, ListComboData.LIST_TER_DECISION_TYPE, "NAME_VN", "ID", True)

            cboStatus.SelectedValue = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Get data theo ID Ma nhan vien
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If CurrentState Is Nothing Then
                If Request.Params("FormType") IsNot Nothing Then
                    FormType = Request.Params("FormType")
                End If
                If Request.Params("ID") IsNot Nothing Then
                    IDSelect = Decimal.Parse(Request.Params("ID"))
                End If
                Select Case FormType
                    Case 0
                        Refresh("InsertView")
                    Case 1
                        Refresh("UpdateView")
                    Case 2
                        Refresh("NormalView")
                    Case 3
                        Dim empID = Request.Params("empid")
                        FillDataByEmployeeID(empID)
                       
                        hidEmpID.Value = empID
                        'rgLabourProtect.Rebind()
                        GetTerminateCalculate()
                        Refresh("InsertView")
                End Select
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Load dataSource cho grid Ly Do Nghi Viec
    ''' </summary>
    ''' <param name="lstReason"></param>
    ''' <remarks></remarks>
    Private Sub CreateDataHandoverContent(Optional ByVal lstHandoverContent As List(Of HandoverContentDTO) = Nothing)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If lstHandoverContent Is Nothing Then
                lstHandoverContent = New List(Of HandoverContentDTO)
            End If
            rgHandoverContent.DataSource = lstHandoverContent
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

#End Region

#Region "DateDifference"

    Public Class DateDifference
        ''' <summary>
        ''' defining Number of days in month; index 0=> january and 11=> December
        ''' february contain either 28 or 29 days, that's why here value is -1
        ''' which wil be calculate later.
        ''' </summary>
        Private monthDay() As Integer = {31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}

        ''' <summary>
        ''' từ ngày
        ''' </summary>
        Private fromDate As Date

        ''' <summary>
        ''' đến ngày
        ''' </summary>
        Private toDate As Date

        ''' <summary>
        ''' 3 tham số trả về
        ''' </summary>
        Private year As Integer
        Private month As Integer
        Private day As Integer
        Dim _mylog As New MyLog()
        Dim _pathLog As String = _mylog._pathLog
        Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

        Public Sub New(ByVal d1 As Date, ByVal d2 As Date)
            Dim startTime As DateTime = DateTime.UtcNow
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Try
                Dim increment As Integer

                If d1 > d2 Then
                    Me.fromDate = d2
                    Me.toDate = d1
                Else
                    Me.fromDate = d1
                    Me.toDate = d2
                End If

                ' Tính toán ngày
                increment = 0

                If Me.fromDate.Day > Me.toDate.Day Then
                    increment = Me.monthDay(Me.fromDate.Month - 1)
                End If

                ' Nếu là tháng 2
                If increment = -1 Then
                    ' kiểm tra năm nhuận
                    If Date.IsLeapYear(Me.fromDate.Year) Then
                        ' nếu là năm nhuận tháng 2 có 29 ngày
                        increment = 29
                    Else
                        increment = 28
                    End If
                End If
                ' Nếu không phải tháng 2
                If increment <> 0 Then
                    day = (Me.toDate.Day + increment) - Me.fromDate.Day
                    increment = 1
                Else
                    day = Me.toDate.Day - Me.fromDate.Day
                End If

                ' tính ra số tháng
                If (Me.fromDate.Month + increment) > Me.toDate.Month Then
                    Me.month = (Me.toDate.Month + 12) - (Me.fromDate.Month + increment)
                    increment = 1
                Else
                    Me.month = (Me.toDate.Month) - (Me.fromDate.Month + increment)
                    increment = 0
                End If

                ' tính ra số năm
                Me.year = Me.toDate.Year - (Me.fromDate.Year + increment)
                _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            End Try


        End Sub

        Public ReadOnly Property Years() As Integer
            Get
                Return Me.year
            End Get
        End Property

        Public ReadOnly Property Months() As Integer
            Get
                Return Me.month
            End Get
        End Property

        Public ReadOnly Property Days() As Integer
            Get
                Return Me.day
            End Get
        End Property

    End Class
#End Region

    

  
End Class

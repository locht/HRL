Imports Framework.UI
Imports Common
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Profile.ProfileBusiness
Imports Ionic.Crc

Public Class ctrlRC_RequestNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
    Private rep As New HistaffFrameworkRepository
    Private store As New RecruitmentStoreProcedure()

#Region "Property"

    '0 - normal
    '1 - Employee
    '2 - Org
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
    Property STATUSCODE As Decimal
        Get
            Return ViewState(Me.ID & "_STATUSCODE")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_STATUSCODE") = value
        End Set
    End Property
    Property ListAttachFile As List(Of AttachFilesDTO)
        Get
            Return ViewState(Me.ID & "_ListAttachFile")
        End Get
        Set(ByVal value As List(Of AttachFilesDTO))
            ViewState(Me.ID & "_ListAttachFile") = value
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

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try

            ScriptManager.GetCurrent(Page).RegisterPostBackControl(tbarMain)
            GetParams()
            Refresh()
            UpdateControlState()
            SetVisibleFileAttach()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub

    Public Overrides Sub BindData()
        Try
            GetDataCombo()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Seperator, ToolbarItem.Export)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            CType(MainToolBar.Items(3), RadToolBarButton).Enabled = True
            CType(MainToolBar.Items(3), RadToolBarButton).Text = "Xuất tờ trình"
            Me.MainToolBar.OnClientButtonClicking = "clientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Try
            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim obj = rep.GetRequestByID(New RequestDTO With {.ID = IDSelect})
                    hidOrgID.Value = obj.ORG_ID.ToString()
                    txtOrgName.Text = obj.ORG_NAME
                    If obj.ORG_DESC IsNot Nothing AndAlso obj.ORG_DESC <> "" Then
                        txtOrgName.ToolTip = DrawTreeByString(obj.ORG_DESC)
                    End If
                    'chkIsInPlan.Checked = obj.IS_IN_PLAN

                    LoadComboTitle()
                    If obj.IS_IN_PLAN Then
                        cboTitle.SelectedValue = obj.RC_PLAN_ID
                    Else
                        cboTitle.SelectedValue = obj.TITLE_ID
                    End If

                    rdSendDate.SelectedDate = obj.SEND_DATE
                    If obj.CONTRACT_TYPE_ID IsNot Nothing Then
                        cboContractType.SelectedValue = obj.CONTRACT_TYPE_ID
                    End If
                    If obj.LOCATION_ID IsNot Nothing Then
                        cbolocationWork.SelectedValue = obj.LOCATION_ID
                    End If

                    If obj.RECRUIT_REASON_ID IsNot Nothing Then
                        cboRecruitReason.SelectedValue = obj.RECRUIT_REASON_ID
                    End If
                    txtRecruitReason.Text = obj.RECRUIT_REASON
                    'If obj.IS_IN_PLAN And obj.RC_PLAN_ID IsNot Nothing And obj.SEND_DATE Is Nothing Then
                    '    Dim dt As DataTable = store.PLAN_GET_BY_ID(obj.RC_PLAN_ID)
                    '    If dt.Rows.Count > 0 Then
                    '        If dt.Rows(0)("EDUCATIONLEVEL") <> String.Empty Then
                    '            cboLearningLevel.SelectedValue = Decimal.Parse(dt.Rows(0)("EDUCATIONLEVEL").ToString())
                    '        End If
                    '        If dt.Rows(0)("AGESFROM") IsNot Nothing And dt.Rows(0)("AGESFROM").ToString() <> String.Empty Then
                    '            rntxtAgeFrom.Value = Double.Parse(dt.Rows(0)("AGESFROM").ToString())
                    '        End If
                    '        If dt.Rows(0)("AGESTO") IsNot Nothing And dt.Rows(0)("AGESTO").ToString() <> String.Empty Then
                    '            rntxtAgeTo.Value = Double.Parse(dt.Rows(0)("AGESTO").ToString())
                    '        End If
                    '        If dt.Rows(0)("QUALIFICATION") IsNot Nothing And dt.Rows(0)("QUALIFICATION").ToString() <> String.Empty Then
                    '            cboQualification.SelectedValue = Decimal.Parse(dt.Rows(0)("QUALIFICATION").ToString())
                    '        End If
                    '        If dt.Rows(0)("SPECIALSKILLS") IsNot Nothing And dt.Rows(0)("SPECIALSKILLS").ToString() <> String.Empty Then
                    '            cboSpecialSkills.SelectedValue = Decimal.Parse(dt.Rows(0)("SPECIALSKILLS").ToString())
                    '        End If
                    '        If dt.Rows(0)("LANGUAGE") IsNot Nothing And dt.Rows(0)("LANGUAGE").ToString() <> String.Empty Then
                    '            cboLanguage.SelectedValue = Decimal.Parse(dt.Rows(0)("LANGUAGE").ToString())
                    '        End If
                    '        If dt.Rows(0)("LANGUAGELEVEL") IsNot Nothing And dt.Rows(0)("LANGUAGELEVEL").ToString() <> String.Empty Then
                    '            cboLanguageLevel.SelectedValue = Decimal.Parse(dt.Rows(0)("LANGUAGELEVEL").ToString())
                    '        End If

                    '        txtScores.Text = dt.Rows(0)("LANGUAGESCORES").ToString()
                    '        If dt.Rows(0)("EXPECTED_JOIN_DATE") IsNot Nothing And dt.Rows(0)("EXPECTED_JOIN_DATE").ToString() <> String.Empty Then
                    '            rdExpectedJoinDate.SelectedDate = DateTime.Parse(dt.Rows(0)("EXPECTED_JOIN_DATE").ToString())
                    '        End If
                    '        If dt.Rows(0)("COMPUTER_LEVEL") IsNot Nothing And dt.Rows(0)("COMPUTER_LEVEL").ToString() <> String.Empty Then
                    '            cboComputerLevel.SelectedValue = Decimal.Parse(dt.Rows(0)("COMPUTER_LEVEL").ToString())
                    '        End If
                    '        txtMainTask.Text = dt.Rows(0)("MAINTASK").ToString()
                    '        txtRequestExperience.Text = dt.Rows(0)("QUALIFICATIONREQUEST").ToString()
                    '    End If
                    'Else
                    '    If obj.LEARNING_LEVEL_ID IsNot Nothing Then
                    '        cboLearningLevel.SelectedValue = obj.LEARNING_LEVEL_ID
                    '    End If
                    '    If obj.AGE_FROM IsNot Nothing Then
                    '        rntxtAgeFrom.Value = obj.AGE_FROM
                    '    End If
                    '    If obj.AGE_TO IsNot Nothing Then
                    '        rntxtAgeTo.Value = obj.AGE_TO
                    '    End If
                    '    If obj.QUALIFICATION IsNot Nothing Then
                    '        cboQualification.SelectedValue = obj.QUALIFICATION
                    '    End If
                    '    If obj.SPECIALSKILLS IsNot Nothing Then
                    '        cboSpecialSkills.SelectedValue = obj.SPECIALSKILLS
                    '    End If
                    '    If obj.LANGUAGE IsNot Nothing Then
                    '        cboLanguage.SelectedValue = obj.LANGUAGE
                    '    End If
                    '    If obj.LANGUAGELEVEL IsNot Nothing Then
                    '        cboLanguageLevel.SelectedValue = obj.LANGUAGELEVEL
                    '    End If

                    '    txtScores.Text = If(obj.LANGUAGESCORES Is Nothing, String.Empty, obj.LANGUAGESCORES)
                    '    rdExpectedJoinDate.SelectedDate = obj.EXPECTED_JOIN_DATE
                    '    rntxtExperienceNumber.Value = obj.EXPERIENCE_NUMBER
                    '    If obj.COMPUTER_LEVEL IsNot Nothing Then
                    '        cboComputerLevel.SelectedValue = obj.COMPUTER_LEVEL
                    '    End If
                    '    txtMainTask.Text = obj.MAINTASK
                    '    txtRequestExperience.Text = obj.REQUEST_EXPERIENCE
                    'End If

                    If obj.LEARNING_LEVEL_ID IsNot Nothing Then
                        cboLearningLevel.SelectedValue = obj.LEARNING_LEVEL_ID
                    End If
                    If obj.AGE_FROM IsNot Nothing Then
                        rntxtAgeFrom.Value = obj.AGE_FROM
                    End If
                    If obj.AGE_TO IsNot Nothing Then
                        rntxtAgeTo.Value = obj.AGE_TO
                    End If
                    If obj.QUALIFICATION IsNot Nothing Then
                        cboQualification.SelectedValue = obj.QUALIFICATION
                    End If
                    If obj.SPECIALSKILLS IsNot Nothing Then
                        cboSpecialSkills.SelectedValue = obj.SPECIALSKILLS
                    End If
                    If obj.LANGUAGE IsNot Nothing Then
                        cboLanguage.SelectedValue = obj.LANGUAGE
                    End If
                    If obj.LANGUAGELEVEL IsNot Nothing Then
                        cboLanguageLevel.SelectedValue = obj.LANGUAGELEVEL
                    End If

                    txtScores.Text = If(obj.LANGUAGESCORES Is Nothing, String.Empty, obj.LANGUAGESCORES)
                    rdExpectedJoinDate.SelectedDate = obj.EXPECTED_JOIN_DATE
                    rntxtExperienceNumber.Value = obj.EXPERIENCE_NUMBER
                    If obj.COMPUTER_LEVEL IsNot Nothing Then
                        cboComputerLevel.SelectedValue = obj.COMPUTER_LEVEL
                    End If
                    txtMainTask.Text = obj.MAINTASK
                    txtRequestExperience.Text = obj.REQUEST_EXPERIENCE

                    'rntxtFemaleNumber.Value = obj.FEMALE_NUMBER
                    'rntxtMaleNumber.Value = obj.MALE_NUMBER
                    'Dim sumNumber As Decimal = 0
                    'If rntxtFemaleNumber.Value IsNot Nothing Then
                    '    sumNumber += rntxtFemaleNumber.Value
                    'End If
                    'If rntxtMaleNumber.Value IsNot Nothing Then
                    '    sumNumber += rntxtMaleNumber.Value
                    'End If
                    rntxtRecruitNumber.Value = obj.RECRUIT_NUMBER
                    If obj.RC_RECRUIT_PROPERTY IsNot Nothing Then
                        cboRecruitProperty.SelectedValue = obj.RC_RECRUIT_PROPERTY
                    End If
                    If obj.GENDER_PRIORITY IsNot Nothing Then
                        cboGenderPriority.SelectedValue = obj.GENDER_PRIORITY
                    End If
                    chkIsOver.Checked = If(obj.IS_OVER_LIMIT Is Nothing, False, obj.IS_OVER_LIMIT)
                    chkIsSupport.Checked = If(obj.IS_SUPPORT Is Nothing, False, obj.IS_SUPPORT)
                    txtForeignAbility.Text = obj.FOREIGN_ABILITY
                    txtComputerAppLevel.Text = obj.COMPUTER_APP_LEVEL

                    txtUpload.Text = obj.FILE_NAME
                    txtUploadFile.Text = obj.UPLOAD_FILE

                    txtDescription.Text = obj.DESCRIPTION
                    txtRemark.Text = obj.REMARK
                    txtRequestOther.Text = obj.REQUEST_OTHER
                    hidID.Value = obj.ID
                    hidOrgID.Value = obj.ORG_ID
                    hddFile.Value = obj.DESCRIPTIONATTACHFILE
                    hypFile.Text = obj.DESCRIPTIONATTACHFILE
                    hypFile.NavigateUrl = "http://" & Request.Url.Host & ":" & Request.Url.Port & "/ReportTemplates/Recruitment/Upload/" + obj.DESCRIPTIONATTACHFILE

                    For Each itm In obj.lstEmp
                        Dim item As New RadListBoxItem
                        item.Value = itm.EMPLOYEE_ID
                        item.Text = itm.EMPLOYEE_CODE & " - " & itm.EMPLOYEE_NAME
                        lstEmployee.Items.Add(item)
                    Next

                    If obj.STATUS_ID = RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID Then
                        RadPane2.Enabled = False
                    End If

                    GetTotalEmployeeByTitleID()

                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
                    'chkIsInPlan.Checked = True
                    rdSendDate.AutoPostBack = True

                    Me.MainToolBar = tbarMain
                    CType(MainToolBar.Items(3), RadToolBarButton).Enabled = False
            End Select

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick

        Dim gID As Decimal
        Dim rep As New RecruitmentRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim obj As New RequestDTO
                        Dim lstEmp As New List(Of RequestEmpDTO)
                        obj.ORG_ID = hidOrgID.Value
                        'obj.IS_IN_PLAN = chkIsInPlan.Checked
                        'If obj.IS_IN_PLAN Then
                        '    obj.RC_PLAN_ID = cboTitle.SelectedValue
                        '    Dim dt As DataTable = store.PLAN_GET_BY_ID(obj.RC_PLAN_ID)
                        '    If dt.Rows.Count > 0 Then
                        '        obj.TITLE_ID = Decimal.Parse(dt.Rows(0)("TITLE_ID").ToString())
                        '    End If
                        'Else
                        '    obj.TITLE_ID = cboTitle.SelectedValue
                        'End If

                        obj.TITLE_ID = cboTitle.SelectedValue
                        obj.SEND_DATE = rdSendDate.SelectedDate
                        If cboContractType.SelectedValue <> "" Then
                            obj.CONTRACT_TYPE_ID = cboContractType.SelectedValue
                        End If
                        obj.RECRUIT_REASON = txtRecruitReason.Text
                        If cboLearningLevel.SelectedValue <> "" Then
                            obj.LEARNING_LEVEL_ID = cboLearningLevel.SelectedValue
                        End If
                        obj.AGE_FROM = rntxtAgeFrom.Value
                        obj.AGE_TO = rntxtAgeTo.Value
                        obj.QUALIFICATION = cboQualification.SelectedValue

                        If cboRecruitProperty.SelectedValue <> "" Then
                            obj.RC_RECRUIT_PROPERTY = cboRecruitProperty.SelectedValue
                        End If
                        If cbolocationWork.SelectedValue <> "" Then
                            obj.LOCATION_ID = cbolocationWork.SelectedValue
                        End If

                        obj.IS_OVER_LIMIT = chkIsOver.Checked
                        obj.IS_SUPPORT = chkIsSupport.Checked
                        obj.FOREIGN_ABILITY = txtForeignAbility.Text
                        obj.COMPUTER_APP_LEVEL = txtComputerAppLevel.Text
                        If cboGenderPriority.SelectedValue <> "" Then
                            obj.GENDER_PRIORITY = cboGenderPriority.SelectedValue
                        End If
                        obj.RECRUIT_NUMBER = rntxtRecruitNumber.Value

                        obj.FILE_NAME = txtUpload.Text.Trim
                        obj.UPLOAD_FILE = txtUploadFile.Text.Trim

                        obj.DESCRIPTION = txtDescription.Text
                        obj.EXPERIENCE_NUMBER = rntxtExperienceNumber.Value
                        'obj.FEMALE_NUMBER = rntxtFemaleNumber.Value


                        'obj.MALE_NUMBER = rntxtMaleNumber.Value

                        obj.REQUEST_EXPERIENCE = txtRequestExperience.Text
                        obj.REQUEST_OTHER = txtRequestOther.Text

                        obj.EXPECTED_JOIN_DATE = rdExpectedJoinDate.SelectedDate
                        If cboRecruitReason.SelectedValue <> "" Then
                            obj.RECRUIT_REASON_ID = cboRecruitReason.SelectedValue
                        End If
                        'For Each item As RadListBoxItem In lstEmployee.Items
                        '    Dim emp As New RequestEmpDTO
                        '    emp.EMPLOYEE_ID = item.Value
                        '    lstEmp.Add(emp)
                        'Next
                        obj.REMARK = txtRemark.Text
                        obj.LANGUAGE = cboLanguage.SelectedValue
                        obj.LANGUAGELEVEL = cboLanguageLevel.SelectedValue
                        obj.COMPUTER_LEVEL = cboComputerLevel.SelectedValue
                        If txtScores.Text <> "" Then
                            obj.LANGUAGESCORES = txtScores.Text
                        End If

                        obj.SPECIALSKILLS = cboSpecialSkills.SelectedValue
                        obj.MAINTASK = txtMainTask.Text

                        obj.DESCRIPTIONATTACHFILE = hddFile.Value
                        'obj.lstEmp = lstEmp
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertRequest(obj, gID) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Recruitment&fid=ctrlRC_Request&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                obj.ID = hidID.Value
                                If rep.ModifyRequest(obj, gID) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Recruitment&fid=ctrlRC_Request&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim hfr As New HistaffFrameworkRepository
                    Dim tempPath As String = "ReportTemplates/Recruitment/Report/"
                    Dim obj = hfr.ExecuteToDataSet("PKG_RECRUITMENT.EXPORT_RECRUITMENT_NEEDS", New List(Of Object)({hidID.Value, If(txtPayrollLimit.Text = "", Nothing, txtPayrollLimit.Text), If(txtCurrentNumber.Text = "", Nothing, txtCurrentNumber.Text), rntxtRecruitNumber.Text}))
                    ExportWordMailMerge(System.IO.Path.Combine(Server.MapPath(tempPath), "BM01_TT_Nhu_cau_TD.doc"),
                                        "TT_Nhu_cau_TD_" + DateTime.Now.ToString("HHmmssddMMyyyy") + ".doc",
                                        obj.Tables(0), Response)
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Recruitment&fid=ctrlRC_Request&group=Business")
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
        UpdateControlState()
    End Sub

    Protected Sub btnFindEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindEmployee.Click
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindOrg.Click
        Try
            isLoadPopup = 2
            UpdateControlState()
            ctrlFindOrgPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New RecruitmentRepository
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            lstEmployee.Items.Clear()
            For Each itm In lstCommonEmployee
                Dim item As New RadListBoxItem
                item.Value = itm.EMPLOYEE_ID
                item.Text = itm.EMPLOYEE_CODE & " - " & itm.FULLNAME_VN
                lstEmployee.Items.Add(item)
            Next
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Try
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue 'gán org đã chọn vào hiddenfield
                txtOrgName.Text = orgItem.NAME_VN
                'txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
                'duy fix ngay 11/07
                If orgItem.DESCRIPTION_PATH IsNot Nothing AndAlso orgItem.DESCRIPTION_PATH <> "" Then
                    txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
                End If
            End If
            LoadComboTitle()
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked, ctrlFindOrgPopup.CancelClicked
        isLoadPopup = 0
    End Sub

    Private Sub btnUploadFileDescription_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFileDescription.Click
        ctrlUpload1.Show()
    End Sub

    Private Sub btnDeleteFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteFile.Click
        Dim sPath As String = "~/ReportTemplates//" & "Recruitment" & "/" & "Upload/" & hddFile.Value
        If System.IO.File.Exists(MapPath(sPath)) Then
            System.IO.File.Delete(MapPath(sPath))
            hddFile.Value = ""
            hypFile.Text = ""
            hypFile.NavigateUrl = ""
            SetVisibleFileAttach()
        Else
            ShowMessage(Translate("Không tìm thấy đường dẫn"), NotifyType.Error)
        End If
    End Sub

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        'Dim fileName As String
        Try
            Dim file As UploadedFile = ctrlUpload1.UploadedFiles(0)
            Dim sPath As String = "~/ReportTemplates//" & "Recruitment" & "/" & "Upload/"

            If file.GetExtension = ".pdf" Or file.GetExtension = ".doc" Or file.GetExtension = ".docx" Or file.GetExtension = ".jpg" Or file.GetExtension = ".png" Then
                Dim fileName As String = hidOrgID.Value & "_" & "_" & cboTitle.SelectedValue & Date.Now.ToString("HHmmssffff") & "_" & file.FileName
                If System.IO.Directory.Exists(MapPath(sPath)) Then
                    file.SaveAs(MapPath(sPath) & fileName, True)
                    hddFile.Value = fileName
                    hypFile.Text = file.FileName
                    hypFile.NavigateUrl = "http://" & Request.Url.Host & ":" & Request.Url.Port & "/ReportTemplates/Recruitment/Upload/" + fileName
                    SetVisibleFileAttach()
                Else
                    ShowMessage(Translate("Không tìm thấy đường dẫn"), NotifyType.Error)
                End If

            Else
                ShowMessage(Translate("Vui lòng upload file có đuôi mở rộng: .pdf,.doc, .docx, .jpg, .png"), NotifyType.Error)
                Exit Sub
            End If
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi"), NotifyType.Error)
        End Try
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ctrlUpload2.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
            ctrlUpload2.Show()
            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    Private Sub ctrlUpload2_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload2.OkClicked
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

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Recruitment/RequestRCInfo/")
            If ctrlUpload2.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload2.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload2.UploadedFiles(i)
                    Dim str_Filename = Guid.NewGuid.ToString() + "\"
                    If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        strPath = strPath + str_Filename
                        fileName = System.IO.Path.Combine(strPath, file.FileName)
                        file.SaveAs(fileName, True)
                        txtUpload.Text = file.FileName
                        Down_File = str_Filename
                    Else
                        ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file XLS,XLSX,TXT,CTR,DOC,DOCX,XML,PNG,JPG,BITMAP,JPEG,GIF,PDF,RAR,ZIP,PPT,PPTX"), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                loadDatasource(txtUpload.Text)
            End If
            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    Private Sub btnDownload_Click(sender As Object, e As System.EventArgs) Handles btnDownload.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim strPath_Down As String
        Try
            If txtUpload.Text <> "" Then
                strPath_Down = Server.MapPath("~/ReportTemplates/Recruitment/RequestRCInfo/" + txtUploadFile.Text + txtUpload.Text)
                ZipFiles(strPath_Down, 2)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Custom"

    Public Overrides Sub UpdateControlState()
        Try

            If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
            End If
            If phFindOrg.Controls.Contains(ctrlFindOrgPopup) Then
                phFindOrg.Controls.Remove(ctrlFindOrgPopup)
                'Me.Views.Remove(ctrlFindOrgPopup.ID.ToUpper)
            End If
            Select Case isLoadPopup
                Case 1
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.MustHaveContract = False
                    phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = True
                Case 2
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    ctrlFindOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                    phFindOrg.Controls.Add(ctrlFindOrgPopup)
                    ctrlFindOrgPopup.Show()
            End Select
        Catch ex As Exception
            Throw ex
        End Try
        'ChangeToolbarState()
    End Sub

    Private Sub GetDataCombo()
        Try
            Dim dtData As DataTable
            Using rep As New RecruitmentRepository
                dtData = rep.GetOtherList("RC_RECRUIT_REASON", True)
                FillRadCombobox(cboRecruitReason, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("LEARNING_LEVEL", True)
                FillRadCombobox(cboLearningLevel, dtData, "NAME", "ID")
                'load loai hinh hop dong
                dtData = rep.GetOtherList("LABOR_TYPE", True)
                FillRadCombobox(cboContractType, dtData, "NAME", "ID")
                ' Load data to cbo ACADEMY,LANGUAGE, LANGUAGE_LEVEL,MAJOR,SPECIALSKILLS
                'LANGUAGE
                dtData = rep.GetOtherList("RC_LANGUAGE", True)
                FillRadCombobox(cboLanguage, dtData, "NAME", "ID", True)
                'LANGUAGE_LEVEL
                dtData = rep.GetOtherList("LANGUAGE_LEVEL", True)
                FillRadCombobox(cboLanguageLevel, dtData, "NAME", "ID", True)
                'MAJOR
                dtData = rep.GetOtherList("MAJOR", True)
                FillRadCombobox(cboQualification, dtData, "NAME", "ID", True)
                'SPECIALSKILLS
                dtData = rep.GetOtherList("SPECIALSKILLS", True)
                FillRadCombobox(cboSpecialSkills, dtData, "NAME", "ID", True)
                'COMPUTERLEVEL
                dtData = rep.GetOtherList("RC_COMPUTER_LEVEL", True)
                FillRadCombobox(cboComputerLevel, dtData, "NAME", "ID", True)
                'RECRUIT PROPERTY
                dtData = rep.GetOtherList("RC_RECRUIT_PROPERTY")
                FillRadCombobox(cboRecruitProperty, dtData, "NAME", "ID", True)
                'GENDER
                dtData = rep.GetOtherList("GENDER")
                FillRadCombobox(cboGenderPriority, dtData, "NAME", "ID", True)
                'tinh thanh
                dtData = rep.GetProvinceList("False")
                FillRadCombobox(cbolocationWork, dtData, "NAME", "ID")
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetParams()
        Try
            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    IDSelect = Decimal.Parse(Request.Params("ID"))
                End If
                If IDSelect IsNot Nothing Then
                    Refresh("UpdateView")
                Else
                    Refresh("InsertView")
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SetVisibleFileAttach()
        If hddFile.Value <> "" Then
            btnDeleteFile.Visible = True
            hypFile.Visible = True
            btnUploadFileDescription.Visible = False
        Else
            btnDeleteFile.Visible = False
            hypFile.Visible = False
            btnUploadFileDescription.Visible = True
        End If

    End Sub

    Private Sub ZipFiles(ByVal path As String, ByVal order As Decimal?)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim crc As New CRC32()
            'Dim fileNameZip As String

            'If order = 0 Then
            '    fileNameZip = txtUpload_LG.Text.Trim
            'ElseIf order = 1 Then
            '    fileNameZip = txtUpload_HD.Text.Trim
            'Else
            '    fileNameZip = txtUpload_FT.Text.Trim
            'End If

            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
            Response.WriteFile(file.FullName)
            Response.End()
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
        End Try
    End Sub
#End Region

    Private Sub rntxtRecruitNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rntxtRecruitNumber.TextChanged
        Try
            Dim recruitNum As Decimal = If(IsNumeric(rntxtRecruitNumber.Value), Decimal.Parse(rntxtRecruitNumber.Value), 0)
            Dim payrolLimit As Decimal = If(IsNumeric(txtPayrollLimit.Text), Decimal.Parse(txtPayrollLimit.Text), 0)
            If recruitNum > payrolLimit Then
                chkIsOver.Checked = True
            Else
                chkIsOver.Checked = False
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    'Private Sub chkIsInPlan_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIsInPlan.CheckedChanged
    '    Try
    '        'If chkIsInPlan.Checked Then
    '        '    rdSendDate.AutoPostBack = True
    '        'Else
    '        '    rdSendDate.AutoPostBack = False
    '        'End If

    '        cboTitle.Items.Clear()
    '        cboTitle.ClearSelection()
    '        cboTitle.Text = ""

    '        LoadComboTitle()
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)

    '    End Try
    'End Sub

    'Private Sub rdSendDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdSendDate.SelectedDateChanged
    '    Try
    '        LoadComboTitle()
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)

    '    End Try
    'End Sub

    Private Sub LoadComboTitle()

        If hidOrgID.Value <> "" Then
            Dim dtData As DataTable
            dtData = store.GET_TITLE_IN_PLAN(hidOrgID.Value, 0)
            FillRadCombobox(cboTitle, dtData, "NAME", "ID")
        Else
            cboTitle.Items.Clear()
            cboTitle.ClearSelection()
            cboTitle.Text = ""
        End If

        'If chkIsInPlan.Checked Then
        '    If hidOrgID.Value <> "" And rdSendDate.SelectedDate IsNot Nothing Then
        '        Dim dtData As DataTable
        '        Using rep As New RecruitmentRepository
        '            dtData = rep.GetTitleByOrgListInPlan(hidOrgID.Value, rdSendDate.SelectedDate.Value.Year, True)
        '            FillRadCombobox(cboTitle, dtData, "NAME", "ID")
        '        End Using
        '    Else
        '        cboTitle.Items.Clear()
        '        cboTitle.ClearSelection()
        '        cboTitle.Text = ""
        '    End If
        'Else
        '    If hidOrgID.Value <> "" Then
        '        Dim dtData As DataTable
        '        Using rep As New RecruitmentRepository
        '            dtData = rep.GetTitleByOrgList(hidOrgID.Value, True)
        '            FillRadCombobox(cboTitle, dtData, "NAME", "ID")
        '        End Using
        '    Else
        '        cboTitle.Items.Clear()
        '        cboTitle.ClearSelection()
        '        cboTitle.Text = ""
        '    End If
        'End If

    End Sub

    Protected Sub cboTitle_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTitle.SelectedIndexChanged
        Dim rep As New RecruitmentRepository
        GetTotalEmployeeByTitleID()

        'If chkIsInPlan.Checked Then

        'Else
        '    cboLearningLevel.Text = ""
        '    rntxtAgeFrom.Text = ""
        '    rntxtAgeTo.Text = ""
        '    cboQualification.Text = ""
        '    cboSpecialSkills.Text = ""
        '    cboLanguage.Text = ""
        '    cboLanguageLevel.Text = ""
        '    rdExpectedJoinDate.SelectedDate = Nothing
        '    cboComputerLevel.Text = ""
        '    txtMainTask.Text = ""
        '    txtRequestExperience.Text = ""
        'End If
        If hidID.Value <> "" Then
            Dim obj = rep.GetRequestByID(New RequestDTO With {.ID = Decimal.Parse(hidID.Value)})
            If obj.ID > 0 And cboTitle.SelectedValue = obj.RC_PLAN_ID Then
                If obj.LEARNING_LEVEL_ID IsNot Nothing Then
                    cboLearningLevel.SelectedValue = obj.LEARNING_LEVEL_ID
                End If
                If obj.AGE_FROM IsNot Nothing Then
                    rntxtAgeFrom.Value = obj.AGE_FROM
                End If
                If obj.AGE_TO IsNot Nothing Then
                    rntxtAgeTo.Value = obj.AGE_TO
                End If
                If obj.QUALIFICATION IsNot Nothing Then
                    cboQualification.SelectedValue = obj.QUALIFICATION
                End If
                If obj.SPECIALSKILLS IsNot Nothing Then
                    cboSpecialSkills.SelectedValue = obj.SPECIALSKILLS
                End If
                If obj.LANGUAGE IsNot Nothing Then
                    cboLanguage.SelectedValue = obj.LANGUAGE
                End If
                If obj.LANGUAGELEVEL IsNot Nothing Then
                    cboLanguageLevel.SelectedValue = obj.LANGUAGELEVEL
                End If

                txtScores.Text = If(obj.LANGUAGESCORES Is Nothing, String.Empty, obj.LANGUAGESCORES)
                rdExpectedJoinDate.SelectedDate = obj.EXPECTED_JOIN_DATE
                rntxtExperienceNumber.Value = obj.EXPERIENCE_NUMBER
                If obj.COMPUTER_LEVEL IsNot Nothing Then
                    cboComputerLevel.SelectedValue = obj.COMPUTER_LEVEL
                End If
                txtMainTask.Text = obj.MAINTASK
                txtRequestExperience.Text = obj.REQUEST_EXPERIENCE
            Else
                Dim dt As DataTable = store.PLAN_GET_BY_ID(Int32.Parse(cboTitle.SelectedValue))
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0)("EDUCATIONLEVEL") IsNot Nothing And dt.Rows(0)("EDUCATIONLEVEL").ToString() <> String.Empty Then
                        cboLearningLevel.SelectedValue = Decimal.Parse(dt.Rows(0)("EDUCATIONLEVEL").ToString())
                    End If
                    If dt.Rows(0)("AGESFROM") IsNot Nothing And dt.Rows(0)("AGESFROM").ToString() <> String.Empty Then
                        rntxtAgeFrom.Value = Double.Parse(dt.Rows(0)("AGESFROM").ToString())
                    End If
                    If dt.Rows(0)("AGESTO") IsNot Nothing And dt.Rows(0)("AGESTO").ToString() <> String.Empty Then
                        rntxtAgeTo.Value = Double.Parse(dt.Rows(0)("AGESTO").ToString())
                    End If
                    If dt.Rows(0)("QUALIFICATION") IsNot Nothing And dt.Rows(0)("QUALIFICATION").ToString() <> String.Empty Then
                        cboQualification.SelectedValue = Decimal.Parse(dt.Rows(0)("QUALIFICATION").ToString())
                    End If
                    If dt.Rows(0)("SPECIALSKILLS") IsNot Nothing And dt.Rows(0)("SPECIALSKILLS").ToString() <> String.Empty Then
                        cboSpecialSkills.SelectedValue = Decimal.Parse(dt.Rows(0)("SPECIALSKILLS").ToString())
                    End If
                    If dt.Rows(0)("LANGUAGE") IsNot Nothing And dt.Rows(0)("LANGUAGE").ToString() <> String.Empty Then
                        cboLanguage.SelectedValue = Decimal.Parse(dt.Rows(0)("LANGUAGE").ToString())
                    End If
                    If dt.Rows(0)("LANGUAGELEVEL") IsNot Nothing And dt.Rows(0)("LANGUAGELEVEL").ToString() <> String.Empty Then
                        cboLanguageLevel.SelectedValue = Decimal.Parse(dt.Rows(0)("LANGUAGELEVEL").ToString())
                    End If

                    txtScores.Text = dt.Rows(0)("LANGUAGESCORES").ToString()
                    If dt.Rows(0)("EXPECTED_JOIN_DATE") IsNot Nothing And dt.Rows(0)("EXPECTED_JOIN_DATE").ToString() <> String.Empty Then
                        rdExpectedJoinDate.SelectedDate = DateTime.Parse(dt.Rows(0)("EXPECTED_JOIN_DATE").ToString())
                    End If
                    If dt.Rows(0)("COMPUTER_LEVEL") IsNot Nothing And dt.Rows(0)("COMPUTER_LEVEL").ToString() <> String.Empty Then
                        cboComputerLevel.SelectedValue = Decimal.Parse(dt.Rows(0)("COMPUTER_LEVEL").ToString())
                    End If
                    txtMainTask.Text = dt.Rows(0)("MAINTASK").ToString()
                    txtRequestExperience.Text = dt.Rows(0)("QUALIFICATIONREQUEST").ToString()
                Else
                    cboLearningLevel.Text = ""
                    rntxtAgeFrom.Text = ""
                    rntxtAgeTo.Text = ""
                    cboQualification.Text = ""
                    cboSpecialSkills.Text = ""
                    cboLanguage.Text = ""
                    cboLanguageLevel.Text = ""
                    rdExpectedJoinDate.SelectedDate = Nothing
                    cboComputerLevel.Text = ""
                    txtMainTask.Text = ""
                    txtRequestExperience.Text = ""
                End If
            End If
        Else
            If cboTitle.SelectedValue <> "" Then
                Dim dt As DataTable = store.PLAN_GET_BY_ID(Int32.Parse(cboTitle.SelectedValue))
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0)("EDUCATIONLEVEL") IsNot Nothing And dt.Rows(0)("EDUCATIONLEVEL").ToString() <> String.Empty Then
                        cboLearningLevel.SelectedValue = Decimal.Parse(dt.Rows(0)("EDUCATIONLEVEL").ToString())
                    End If
                    If dt.Rows(0)("AGESFROM") IsNot Nothing And dt.Rows(0)("AGESFROM").ToString() <> String.Empty Then
                        rntxtAgeFrom.Value = Double.Parse(dt.Rows(0)("AGESFROM").ToString())
                    End If
                    If dt.Rows(0)("AGESTO") IsNot Nothing And dt.Rows(0)("AGESTO").ToString() <> String.Empty Then
                        rntxtAgeTo.Value = Double.Parse(dt.Rows(0)("AGESTO").ToString())
                    End If
                    If dt.Rows(0)("QUALIFICATION") IsNot Nothing And dt.Rows(0)("QUALIFICATION").ToString() <> String.Empty Then
                        cboQualification.SelectedValue = Decimal.Parse(dt.Rows(0)("QUALIFICATION").ToString())
                    End If
                    If dt.Rows(0)("SPECIALSKILLS") IsNot Nothing And dt.Rows(0)("SPECIALSKILLS").ToString() <> String.Empty Then
                        cboSpecialSkills.SelectedValue = Decimal.Parse(dt.Rows(0)("SPECIALSKILLS").ToString())
                    End If
                    If dt.Rows(0)("LANGUAGE") IsNot Nothing And dt.Rows(0)("LANGUAGE").ToString() <> String.Empty Then
                        cboLanguage.SelectedValue = Decimal.Parse(dt.Rows(0)("LANGUAGE").ToString())
                    End If
                    If dt.Rows(0)("LANGUAGELEVEL") IsNot Nothing And dt.Rows(0)("LANGUAGELEVEL").ToString() <> String.Empty Then
                        cboLanguageLevel.SelectedValue = Decimal.Parse(dt.Rows(0)("LANGUAGELEVEL").ToString())
                    End If

                    txtScores.Text = dt.Rows(0)("LANGUAGESCORES").ToString()
                    If dt.Rows(0)("EXPECTED_JOIN_DATE") IsNot Nothing And dt.Rows(0)("EXPECTED_JOIN_DATE").ToString() <> String.Empty Then
                        rdExpectedJoinDate.SelectedDate = DateTime.Parse(dt.Rows(0)("EXPECTED_JOIN_DATE").ToString())
                    End If
                    If dt.Rows(0)("COMPUTER_LEVEL") IsNot Nothing And dt.Rows(0)("COMPUTER_LEVEL").ToString() <> String.Empty Then
                        cboComputerLevel.SelectedValue = Decimal.Parse(dt.Rows(0)("COMPUTER_LEVEL").ToString())
                    End If
                    txtMainTask.Text = dt.Rows(0)("MAINTASK").ToString()
                    txtRequestExperience.Text = dt.Rows(0)("QUALIFICATIONREQUEST").ToString()
                Else
                    cboLearningLevel.Text = ""
                    rntxtAgeFrom.Text = ""
                    rntxtAgeTo.Text = ""
                    cboQualification.Text = ""
                    cboSpecialSkills.Text = ""
                    cboLanguage.Text = ""
                    cboLanguageLevel.Text = ""
                    rdExpectedJoinDate.SelectedDate = Nothing
                    cboComputerLevel.Text = ""
                    txtMainTask.Text = ""
                    txtRequestExperience.Text = ""
                End If
            End If
        End If
    End Sub

    Protected Sub GetTotalEmployeeByTitleID()
        Try
            'Dim OUT_NUMBER As String
            'Dim obj = rep.ExecuteStoreScalar("PKG_RECRUITMENT.GET_TOTAL_EMPLOYEE_BY_TITLEID", New List(Of Object)({hidOrgID.Value, cboTitle.SelectedValue, Common.Common.GetUserName(), OUT_NUMBER}))
            'txtCurrentNumber.Text = obj(0).ToString()
            If IsDate(rdSendDate.SelectedDate) Then
                If hidOrgID.Value <> String.Empty Then
                    Dim tab As DataTable = store.GetCurrentManningTitle1(Int32.Parse(hidOrgID.Value), cboTitle.SelectedValue, rdSendDate.SelectedDate)
                    If tab.Rows.Count > 0 Then
                        txtPayrollLimit.Text = tab.Rows(0)("NEW_MANNING").ToString()
                        txtCurrentNumber.Text = tab.Rows(0)("CURRENT_MANNING").ToString()
                        txtDifferenceNumber.Text = tab.Rows(0)("MOBILIZE_COUNT_MANNING").ToString()
                    Else
                        txtPayrollLimit.Text = "0"
                        txtCurrentNumber.Text = "0"
                        txtDifferenceNumber.Text = "0"
                    End If
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Private Sub loadDatasource(ByVal strUpload As String)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If strUpload <> "" Then
                txtUpload.Text = strUpload
                txtUploadFile.Text = Down_File
            Else
                strUpload = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub rdSendDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdSendDate.SelectedDateChanged
        Try
            If IsDate(rdSendDate.SelectedDate) Then
                If hidOrgID.Value <> String.Empty Then
                    Dim tab As DataTable = store.GetCurrentManningTitle1(Int32.Parse(hidOrgID.Value), cboTitle.SelectedValue, rdSendDate.SelectedDate)
                    If tab.Rows.Count > 0 Then
                        txtPayrollLimit.Text = tab.Rows(0)("NEW_MANNING").ToString()
                        txtCurrentNumber.Text = tab.Rows(0)("CURRENT_MANNING").ToString()
                        txtDifferenceNumber.Text = tab.Rows(0)("MOBILIZE_COUNT_MANNING").ToString()
                    Else
                        txtPayrollLimit.Text = "0"
                        txtCurrentNumber.Text = "0"
                        txtDifferenceNumber.Text = "0"
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
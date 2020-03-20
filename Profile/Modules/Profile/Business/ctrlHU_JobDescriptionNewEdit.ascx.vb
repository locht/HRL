Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Xml.Serialization
Imports WebAppLog
Imports Ionic.Crc

Public Class ctrlHU_JobDescriptionNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Public Overrides Property MustAuthorize As Boolean = True
    Private procedure As ProfileStoreProcedure
    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

#Region "Properties"
    '0 - normal
    '1 - Chọn Org
    '2 - Chọn Quản lý trực tiếp
    Property isLoadPopup As Decimal
        Get
            Return PageViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    Property isLoad As Boolean
        Get
            Return PageViewState(Me.ID & "_isLoad")
        End Get
        Set(ByVal value As Boolean)
            PageViewState(Me.ID & "_isLoad") = value
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

    Property vcf As DataSet
        Get
            Return PageViewState(Me.ID & "_vcf")
        End Get
        Set(ByVal value As DataSet)
            PageViewState(Me.ID & "_vcf") = value
        End Set
    End Property
    Property ComboBoxDataDTO As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ComboBoxDataDTO")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ComboBoxDataDTO") = value
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

    Property JobDes As JobDescriptionDTO
        Get
            Return ViewState(Me.ID & "_JobDes")
        End Get
        Set(value As JobDescriptionDTO)
            ViewState(Me.ID & "_JobDes") = value
        End Set
    End Property

#End Region

#Region "Page"
    Public _DIRECT_MANAGER As String
    Public _LEVEL_MANAGER As String
    Public _ORG_ID As String
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetParams()
            UpdateControlState()
            Refresh()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim dtData As New DataTable
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    JobDes = rep.GetJobDesByID(hidID.Value)
                    If JobDes IsNot Nothing Then
                        ListAttachFile = JobDes.ListAttachFiles
                        txtUpload.Text = String.Join(",", (From p In JobDes.ListAttachFiles.AsEnumerable Select p.ATTACHFILE_NAME).ToArray)
                        txtJobCode.Text = JobDes.CODE
                        txtJobName.Text = JobDes.NAME
                        txtJobPower.Text = JobDes.JOB_POWER
                        txtOrgName2.Text = JobDes.ORG_NAME
                        hidOrgID.Value = JobDes.ORG_ID
                        dtData = rep.GET_TITLE_ORG(JobDes.ORG_ID)
                        FillRadCombobox(cboTitle, dtData, "NAME_VN", "ID", True)
                        If JobDes.TITLE_ID IsNot Nothing Then
                            cboTitle.SelectedValue = JobDes.TITLE_ID
                        End If
                        txtUploadFile.Text = JobDes.ATTACH_FILE
                        txtUpload.Text = JobDes.FILENAME
                        If JobDes.TIME_WORKING IsNot Nothing Then
                            cboTimeWorking.SelectedValue = JobDes.TIME_WORKING
                        End If
                        txtJobCondition.Text = JobDes.WORK_CONDITION
                        txtNote.Text = JobDes.NOTE
                        txtJobDescription.Text = JobDes.JOB_DESCRIPTION
                        txtJobParticularties.Text = JobDes.JOB_PARTICULARTIES
                        txtJobEnvironment.Text = JobDes.WORK_ENVIRONMENT
                        txtJobReponsibility.Text = JobDes.JOB_RESPONSIBILITY
                        txtCharacter.Text = JobDes.CHARACTER
                        If JobDes.LEARNING_LEVEL IsNot Nothing Then
                            cboLearningLV.SelectedValue = JobDes.LEARNING_LEVEL
                        End If
                        If JobDes.TRAINING_FORM IsNot Nothing Then
                            cboTrainingForm.SelectedValue = JobDes.TRAINING_FORM
                        End If
                        If JobDes.GRADUATE_SCHOOL IsNot Nothing Then
                            cboGraduateSchool.SelectedValue = JobDes.GRADUATE_SCHOOL
                        End If
                        If JobDes.MAJOR_RANK IsNot Nothing Then
                            cboMajorRank.SelectedValue = JobDes.MAJOR_RANK
                        End If
                        If JobDes.TRAINING_FORM_2 IsNot Nothing Then
                            cboTrainingForm2.SelectedValue = JobDes.TRAINING_FORM_2
                        End If
                        If JobDes.GRADUATE_SCHOOL_2 IsNot Nothing Then
                            cboGraduateSchool2.SelectedValue = JobDes.GRADUATE_SCHOOL_2
                        End If
                        If JobDes.MAJOR_RANK_2 IsNot Nothing Then
                            cboMajorRank2.SelectedValue = JobDes.MAJOR_RANK_2
                        End If
                        If JobDes.COMPUTER_RANK IsNot Nothing Then
                            cboComputerRank.SelectedValue = JobDes.COMPUTER_RANK
                        End If
                        If JobDes.LANGUAGE_1 IsNot Nothing Then
                            cboLanguage1.SelectedValue = JobDes.LANGUAGE_1
                        End If
                        If JobDes.LANGUAGE_RANK_1 IsNot Nothing Then
                            cboLanguageRank1.SelectedValue = JobDes.LANGUAGE_RANK_1
                        End If
                        If JobDes.LANGUAGE_2 IsNot Nothing Then
                            cboLanguage2.SelectedValue = JobDes.LANGUAGE_2
                        End If
                        If JobDes.LANGUAGE_RANK_2 IsNot Nothing Then
                            cboLanguageRank2.SelectedValue = JobDes.LANGUAGE_RANK_2
                        End If
                        If JobDes.LANGUAGE_3 IsNot Nothing Then
                            cboLanguage3.SelectedValue = JobDes.LANGUAGE_3
                        End If
                        If JobDes.LANGUAGE_RANK_3 IsNot Nothing Then
                            cboLanguageRank3.SelectedValue = JobDes.LANGUAGE_RANK_3
                        End If
                        If JobDes.SOFT_SKILL IsNot Nothing Then
                            cboSoftSkill.SelectedValue = JobDes.SOFT_SKILL
                        End If
                    End If
                    'Contract = rep.GetContractByID(New ContractDTO With {.ID = hidID.Value})

                Case "NormalView"
                    CurrentState = CommonMessage.STATE_NEW
            End Select
            rep.Dispose()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            InitControl()
            If Not IsPostBack Then
                'ViewConfig(DetailPane)
                vcf = New DataSet
                Using rep = New CommonRepository
                    vcf.ReadXml(New IO.StringReader(rep.GetConfigView(Me.ID).Rows(0)("config_data").ToString()))
                End Using
                If vcf IsNot Nothing AndAlso vcf.Tables("control") IsNot Nothing Then
                    Dim dtCtrl As DataTable = vcf.Tables("control")
                    For Each ctrs As Control In rtabProfileInfo.Controls
                        Dim row As DataRow
                        Try
                            row = dtCtrl.Select("Ctl_ID ='" + ctrs.ID + "'")(0)
                        Catch ex As Exception
                            Continue For
                        End Try
                        If row IsNot Nothing Then
                            ctrs.Visible = If(IsDBNull(row("Is_Visible")), False, CBool(row("Is_Visible")))
                            Try
                                Dim validator As BaseValidator = rtabProfileInfo.FindControl(row.Field(Of String)("Validator_ID"))
                                Dim labelCtr As Label = rtabProfileInfo.FindControl(row.Field(Of String)("Label_ID").Trim())
                                If labelCtr IsNot Nothing Then
                                    labelCtr.Visible = ctrs.Visible
                                    labelCtr.Text = If(IsDBNull(row("Label_text")), labelCtr.Text, Translate(row("Label_text")))
                                End If
                                If validator IsNot Nothing Then
                                    validator.Enabled = If(IsDBNull(row("Is_Validator")), True, CBool(row("Is_Validator")))
                                    validator.ErrorMessage = If(IsDBNull(row("ErrorMessage")), validator.ErrorMessage, row("ErrorMessage"))
                                    validator.ToolTip = If(IsDBNull(row("ErrorToolTip")), validator.ToolTip, row("ErrorToolTip"))
                                End If
                            Catch ex As Exception
                                Continue For
                            End Try
                        End If
                    Next

                    '========================================================================================================
                    For Each ctrs As Control In rpvEmpInfo.Controls
                        Dim row As DataRow
                        Try
                            row = dtCtrl.Select("Ctl_ID ='" + ctrs.ID + "'")(0)
                        Catch ex As Exception
                            Continue For
                        End Try
                        If row IsNot Nothing Then
                            ctrs.Visible = If(IsDBNull(row("Is_Visible")), False, CBool(row("Is_Visible")))
                            Try
                                Dim validator As BaseValidator = rpvEmpInfo.FindControl(row.Field(Of String)("Validator_ID"))
                                Dim labelCtr As Label = rpvEmpInfo.FindControl(row.Field(Of String)("Label_ID").Trim())
                                If labelCtr IsNot Nothing Then
                                    labelCtr.Visible = ctrs.Visible
                                    labelCtr.Text = If(IsDBNull(row("Label_text")), labelCtr.Text, Translate(row("Label_text")))
                                End If
                                If validator IsNot Nothing Then
                                    validator.Enabled = If(IsDBNull(row("Is_Validator")), True, CBool(row("Is_Validator")))
                                    validator.ErrorMessage = If(IsDBNull(row("ErrorMessage")), validator.ErrorMessage, row("ErrorMessage"))
                                    validator.ToolTip = If(IsDBNull(row("ErrorToolTip")), validator.ToolTip, row("ErrorToolTip"))
                                End If
                            Catch ex As Exception
                                Continue For
                            End Try
                        End If
                    Next

                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtData As DataTable

        Try
            Using rep As New ProfileRepository
                dtData = rep.GetOtherList("")
                Dim lstCombobox As New ComboBoxDataDTO
                lstCombobox.GET_TRAINING_FORM = True
                lstCombobox.GET_MARK_EDU = True
                lstCombobox.GET_GRADUATE_SCHOOL = True
                lstCombobox.GET_LEARNING_LEVEL = True
                lstCombobox.GET_LANGUAGE = True
                lstCombobox.GET_LANGUAGE_LEVEL = True
                lstCombobox.GET_COMPUTER_LEVEL = True
                lstCombobox.GET_SOFT_SKILL = True
                lstCombobox.GET_WORK_TIME = True
                rep.GetComboList(lstCombobox)
                FillDropDownList(cboLanguage1, lstCombobox.LIST_LANGUAGE, "NAME_VN", "ID", , True)
                FillDropDownList(cboLanguage2, lstCombobox.LIST_LANGUAGE, "NAME_VN", "ID")
                FillDropDownList(cboLanguage3, lstCombobox.LIST_LANGUAGE, "NAME_VN", "ID")
                FillDropDownList(cboLanguageRank1, lstCombobox.LIST_LANGUAGE_LEVEL, "NAME_VN", "ID")
                FillDropDownList(cboLanguageRank2, lstCombobox.LIST_LANGUAGE_LEVEL, "NAME_VN", "ID")
                FillDropDownList(cboLanguageRank3, lstCombobox.LIST_LANGUAGE_LEVEL, "NAME_VN", "ID")
                'rep.GET_TITLE_ORG
                FillDropDownList(cboLearningLV, lstCombobox.LIST_LEARNING_LEVEL, "NAME_VN", "ID")
                FillDropDownList(cboTrainingForm, lstCombobox.LIST_TRAINING_FORM, "NAME_VN", "ID")
                FillDropDownList(cboTrainingForm2, lstCombobox.LIST_TRAINING_FORM, "NAME_VN", "ID")
                FillDropDownList(cboMajorRank, lstCombobox.LIST_MARK_EDU, "NAME_VN", "ID")
                FillDropDownList(cboMajorRank2, lstCombobox.LIST_MARK_EDU, "NAME_VN", "ID")
                FillDropDownList(cboGraduateSchool, lstCombobox.LIST_GRADUATE_SCHOOL, "NAME_VN", "ID")
                FillDropDownList(cboGraduateSchool2, lstCombobox.LIST_GRADUATE_SCHOOL, "NAME_VN", "ID")
                FillDropDownList(cboComputerRank, lstCombobox.LIST_COMPUTER_LEVEL, "NAME_VN", "ID")
                FillDropDownList(cboSoftSkill, lstCombobox.LIST_SOFT_SKILLS, "NAME_VN", "ID")
                FillDropDownList(cboTimeWorking, lstCombobox.LIST_WORK_TIME, "NAME_VN", "ID")
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'Khoi tao ToolBar
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar,
                         ToolbarItem.Save,
                         ToolbarItem.Cancel)
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ctrlFindOrgPopup IsNot Nothing AndAlso phPopupOrg.Controls.Contains(ctrlFindOrgPopup) Then
                phPopupOrg.Controls.Remove(ctrlFindOrgPopup)
            End If
            Select Case isLoadPopup
                Case 1
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    phPopupOrg.Controls.Add(ctrlFindOrgPopup)
            End Select
            Dim r As New ProfileBusinessRepository
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnableControlAll(True, btnFindOrg,
                                     cboTitle)

                Case CommonMessage.STATE_EDIT
                    EnableControlAll(False, txtJobCode)
            End Select
            rep.Dispose()
            ChangeToolbarState()
            Me.Send(CurrentState)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New ProfileBusinessRepository
            Dim gID As Decimal
            Dim objJobDes As New JobDescriptionDTO
            Dim strUrl As String = Request.Url.ToString()
            Dim isPopup As Boolean = False
            If (strUrl.ToUpper.Contains("DIALOG")) Then
                isPopup = True
            End If
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_CREATE
                    ResetControlValue()
                    CurrentState = CommonMessage.STATE_NEW
                Case TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        With objJobDes
                            .CODE = txtJobCode.Text.Trim()
                            .NAME = txtJobName.Text
                            .ORG_ID = hidOrgID.Value
                            .TITLE_ID = cboTitle.SelectedValue
                            .JOB_POWER = txtJobPower.Text
                            .ATTACH_FILE = txtUploadFile.Text
                            .FILENAME = txtUpload.Text
                            If cboTimeWorking.SelectedValue <> "" Then
                                .TIME_WORKING = cboTimeWorking.SelectedValue
                            End If
                            .WORK_CONDITION = txtJobCondition.Text
                            .NOTE = txtNote.Text
                            .JOB_DESCRIPTION = txtJobDescription.Text
                            .JOB_PARTICULARTIES = txtJobParticularties.Text
                            .WORK_ENVIRONMENT = txtJobEnvironment.Text
                            .JOB_RESPONSIBILITY = txtJobReponsibility.Text
                            If cboLearningLV.SelectedValue <> "" Then
                                .LEARNING_LEVEL = cboLearningLV.SelectedValue
                            End If

                            If cboLearningLV.SelectedValue <> "" Then
                                .LEARNING_LEVEL = cboLearningLV.SelectedValue
                            End If

                            If cboTrainingForm.SelectedValue <> "" Then
                                .TRAINING_FORM = cboTrainingForm.SelectedValue
                            End If
                            If cboTrainingForm2.SelectedValue <> "" Then
                                .TRAINING_FORM_2 = cboTrainingForm2.SelectedValue
                            End If
                            If cboGraduateSchool.SelectedValue <> "" Then
                                .GRADUATE_SCHOOL = cboGraduateSchool.SelectedValue
                            End If
                            If cboMajorRank.SelectedValue <> "" Then
                                .MAJOR_RANK = cboMajorRank.SelectedValue
                            End If
                            If cboTrainingForm2.SelectedValue <> "" Then
                                .TRAINING_FORM_2 = cboTrainingForm2.SelectedValue
                            End If
                            If cboGraduateSchool2.SelectedValue <> "" Then
                                .GRADUATE_SCHOOL_2 = cboGraduateSchool2.SelectedValue
                            End If
                            If cboMajorRank2.SelectedValue <> "" Then
                                .MAJOR_RANK_2 = cboMajorRank2.SelectedValue
                            End If
                            If cboComputerRank.SelectedValue <> "" Then
                                .COMPUTER_RANK = cboComputerRank.SelectedValue
                            End If
                            If cboLanguage1.SelectedValue <> "" Then
                                .LANGUAGE_1 = cboLanguage1.SelectedValue
                            End If
                            If cboLanguageRank1.SelectedValue <> "" Then
                                .LANGUAGE_RANK_1 = cboLanguageRank1.SelectedValue
                            End If
                            If cboLanguage2.SelectedValue <> "" Then
                                .LANGUAGE_2 = cboLanguage2.SelectedValue
                            End If
                            If cboLanguageRank2.SelectedValue <> "" Then
                                .LANGUAGE_RANK_2 = cboLanguageRank2.SelectedValue
                            End If
                            If cboLanguage3.SelectedValue <> "" Then
                                .LANGUAGE_3 = cboLanguage3.SelectedValue
                            End If
                            If cboLanguageRank3.SelectedValue <> "" Then
                                .LANGUAGE_RANK_3 = cboLanguageRank3.SelectedValue
                            End If
                            If cboSoftSkill.SelectedValue <> "" Then
                                .SOFT_SKILL = cboSoftSkill.SelectedValue
                            End If
                            .CHARACTER = txtCharacter.Text
                            If ListAttachFile IsNot Nothing Then
                                .ListAttachFiles = ListAttachFile
                            End If
                        End With
                        Select Case CurrentState
                            Case STATE_NEW
                                If rep.InserJobDescription(objJobDes) Then
                                    If (isPopup) Then
                                        Dim str As String = "getRadWindow().close('1');"
                                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                    Else
                                        Session("Result") = 1
                                        Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_JobDescription&group=Business")
                                    End If
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case STATE_EDIT
                                objJobDes.ID = Decimal.Parse(hidID.Value)
                                If rep.ModifyJobDescription(objJobDes, gID) Then
                                    If (isPopup) Then
                                        Dim str As String = "getRadWindow().close('1');"
                                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                    Else
                                        Session("Result") = 1
                                        Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_JobDescription&group=Business")
                                    End If
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If
                Case TOOLBARITEM_CANCEL
                    If (isPopup) Then
                        Dim str As String = "getRadWindow().close('1');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    Else
                        Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_JobDescription&group=Business")
                    End If

            End Select
            UpdateControlState()
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            ' DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
    End Sub

    Private Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindOrg.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindOrgPopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlPopupCommon_CancelClicked(ByVal sender As Object, ByVal e As EventArgs) Handles ctrlFindOrgPopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue
                txtOrgName2.Text = e.CurrentText
                Dim dtData As New DataTable
                dtData = rep.GetTitleByOrgID(e.CurrentValue, True)
                cboTitle.ClearValue()
                FillRadCombobox(cboTitle, dtData, "NAME", "ID", True)
            End If
            cboTitle.ClearValue()
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If txtUpload.Text <> "" Then
                Dim strPath_Down As String = Server.MapPath("~/ReportTemplates/Profile/WorkingInfo/" + txtUploadFile.Text)
                'bCheck = True
                ZipFiles(strPath_Down, 2)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/WorkingInfo/")
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
                    finfo.CONTROL_NAME = "ctrlHU_JobDescriptionNewEdit"
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
#End Region

#Region "Custom"
    Public Sub ResetControlValue()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Create by: TUNGLD - 14/05/2019
    ''' Upper First Character
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpperCaseFirst(ByVal str As String) As String
        Try
            If String.IsNullOrEmpty(str) = True Then
                Return ""
            Else
                Return Char.ToUpper(str(0)) + str.Substring(1).ToLower
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub ZipFiles(ByVal path As String, ByVal _ID As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim crc As New CRC32()
            'Dim pathZip As String = AppDomain.CurrentDomain.BaseDirectory & "Zip\"
            'Dim fileNameZip As String = "ThongTinKhenThuong.zip"
            Dim fileNameZip As String

            fileNameZip = txtUpload.Text.Trim
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

    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    hidID.Value = Request.Params("ID")
                    Refresh("UpdateView")
                    Exit Sub
                End If

                Refresh("NormalView")
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region



End Class
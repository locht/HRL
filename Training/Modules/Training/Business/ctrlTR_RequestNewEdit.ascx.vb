Imports Framework.UI
Imports Common
Imports Training.TrainingBusiness
Imports Telerik.Web.UI
Imports Framework.UI.Utilities
Imports System.IO

Public Class ctrlTR_RequestNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Public Overrides Property MustAuthorize As Boolean = False
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
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
    Property dtCourse As DataTable
        Get
            Return ViewState(Me.ID & "_dtCourse")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtCourse") = value
        End Set
    End Property

    '0 - Declare
    '1 - Extent
    '2 - Details
    Dim FormType As Integer
    Dim IDSelect As Decimal?
    Dim Editable As Boolean

    Public Property lstEmployee As List(Of RequestEmpDTO)
        Get
            If ViewState(Me.ID & "_lstEmployee") Is Nothing Then
                ViewState(Me.ID & "_lstEmployee") = New List(Of RequestEmpDTO)
            End If
            Return ViewState(Me.ID & "_lstEmployee")
        End Get
        Set(ByVal value As List(Of RequestEmpDTO))
            ViewState(Me.ID & "_lstEmployee") = value
        End Set
    End Property

    Property AllowOverStudent As Integer
        Get
            Return ViewState(Me.ID & "_AllowOverStudent")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_AllowOverStudent") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        rgData.PageSize = Common.Common.DefaultPageSize
        rgData.AllowCustomPaging = True
        CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        InitControl()
    End Sub
    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(tbarMain)
            GetParams()
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub GetParams()
        Try
            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    IDSelect = Decimal.Parse(Request.Params("ID"))
                End If
                Editable = (Request.Params("EDITABLE") IsNot Nothing)
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
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New TrainingRepository
        Try
            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim obj = rep.GetTrainingRequestsByID(New RequestDTO With {.ID = IDSelect})
                    hidID.Value = obj.ID
                    hidOrgID.Value = obj.ORG_ID.ToString
                    txtOrgName.Text = obj.ORG_NAME
                    txtOrgName.ToolTip = Utilities.DrawTreeByString(obj.ORG_DESC.ToString)
                    rntxtYear.Value = obj.YEAR
                    cbIrregularly.Checked = obj.IS_IRREGULARLY
                    GetPlanInYearOrg()
                    'If obj.COURSE_ID IsNot Nothing Then
                    '    cboPlan.SelectedValue = obj.COURSE_ID
                    '    cboPlan_SelectedIndexChanged(Nothing, Nothing)
                    'End If
                    If obj.TR_PLAN_ID IsNot Nothing Then
                        cboPlan.SelectedValue = obj.TR_PLAN_ID
                    End If
                    txtProgramGroup.Text = obj.PROGRAM_GROUP
                    txtTrainField.Text = obj.TRAIN_FIELD
                    If obj.TRAIN_FORM_ID IsNot Nothing Then
                        cboTrainForm.SelectedValue = obj.TRAIN_FORM_ID
                    End If
                    If obj.PROPERTIES_NEED_ID IsNot Nothing Then
                        cboPropertiesNeed.SelectedValue = obj.PROPERTIES_NEED_ID
                    End If
                    txtContent.Text = obj.CONTENT
                    rdExpectedDate.SelectedDate = obj.EXPECTED_DATE
                    rdStartDate.SelectedDate = obj.START_DATE
                    If obj.CENTERS_ID IsNot Nothing Then
                        Dim listCenter() As String = obj.CENTERS_ID.Split(New Char() {","})
                        For Each item As RadListBoxItem In lstCenter.Items
                            For Each cen As String In listCenter
                                If item.Value = cen Then
                                    item.Checked = True
                                    Exit For
                                End If
                            Next
                        Next
                        LoadTeacher()
                    End If
                    If obj.TEACHERS_ID IsNot Nothing Then
                        Dim listTeacher() As String = obj.TEACHERS_ID.Split(New Char() {","})
                        For Each item As RadListBoxItem In lstTeacher.Items
                            For Each tea As String In listTeacher
                                If item.Value = tea Then
                                    item.Checked = True
                                    Exit For
                                End If
                            Next
                        Next
                    End If
                    If obj.UNIT_ID IsNot Nothing Then
                        cboUnits.SelectedValue = obj.UNIT_ID
                    End If
                    rntxtExpectedCost.Value = obj.EXPECTED_COST
                    If obj.TR_CURRENCY_ID IsNot Nothing Then
                        cboCurrency.SelectedValue = obj.TR_CURRENCY_ID
                    End If
                    If obj.STATUS_ID IsNot Nothing Then
                        cboStatus.SelectedValue = obj.STATUS_ID
                    End If
                    txtTargetTrain.Text = obj.TARGET_TRAIN
                    txtVenue.Text = obj.VENUE
                    If obj.REQUEST_SENDER_ID IsNot Nothing Then
                        hidSenderID.Value = obj.REQUEST_SENDER_ID
                    End If
                    txtSender.Text = obj.SENDER_NAME
                    txtSenderMail.Text = obj.SENDER_EMAIL
                    txtSenderMobile.Text = obj.SENDER_MOBILE
                    rdRequestDate.SelectedDate = obj.REQUEST_DATE
                    lblFilename.Text = obj.ATTACH_FILE
                    txtRemark.Text = obj.REMARK

                    Dim lstE As List(Of RequestEmpDTO) = rep.GetEmployeeByPlanID(New RequestDTO With {.TR_PLAN_ID = obj.TR_PLAN_ID})
                    lblNumOfPlanTrainee.Text = lstE.Count
                    lblNumOfRealTrainee.Text = obj.lstEmp.Count

                    lstEmployee = obj.lstEmp
                    rgData.VirtualItemCount = obj.lstEmp.Count
                    lblNumOfRealTrainee.Text = obj.lstEmp.Count
                    rgData.DataSource = obj.lstEmp
                    rgData.DataBind()

                    If obj.STATUS_ID = TrainingCommon.TR_REQUEST_STATUS.APPROVE_ID Then
                        'CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                        CurrentState = CommonMessage.STATE_NORMAL
                        SetControlState(False)

                    ElseIf obj.STATUS_ID = TrainingCommon.TR_REQUEST_STATUS.NOT_APPROVE_ID Then
                        'CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                        CurrentState = CommonMessage.STATE_NORMAL
                        SetControlState(False)

                    Else
                        'CType(MainToolBar.Items(0), RadToolBarButton).Enabled = True
                        CurrentState = CommonMessage.STATE_EDIT
                        SetControlState(True)
                    End If
                    ChangeToolbarState()

                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Overrides Sub UpdateControlState()
        Try
            If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
            End If
            Select Case isLoadPopup
                Case 1
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.MustHaveContract = False
                    phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = True
                Case 2
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "common", "ctrlFindOrgPopup")
                    ctrlFindOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                    phFindOrg.Controls.Add(ctrlFindOrgPopup)
                    ctrlFindOrgPopup.Show()
                Case 3
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.MustHaveContract = False
                    phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = False
            End Select

        Catch ex As Exception
            Throw ex
        End Try
        ChangeToolbarState()
    End Sub

    Public Overrides Sub BindData()
        Try
            Dim dtData As DataTable
            Dim tsp As New TrainingStoreProcedure
            Using rep As New TrainingRepository
                'Nhóm chương trình
                dtData = tsp.UnitGetList()
                FillRadCombobox(cbGroupProgram, dtData, "NAME", "ID")

                'Hình thức đào tạo
                dtData = rep.GetOtherList("TR_TRAIN_FORM", True)
                FillRadCombobox(cboTrainForm, dtData, "NAME", "ID")

                'Tính chất nhu cầu
                dtData = rep.GetOtherList("TR_PROPERTIES_NEED", True)
                FillRadCombobox(cboPropertiesNeed, dtData, "NAME", "ID")

                'Trung tâm đào tạo
                dtData = tsp.GetCenters()
                lstCenter.CheckBoxes = True
                For Each dr As DataRow In dtData.Rows
                    lstCenter.Items.Add(New RadListBoxItem(dr("NAME").ToString(), dr("ID").ToString()))
                Next

                'Đơn vị chủ trì đào tạo
                dtData = tsp.UnitGetList()
                FillRadCombobox(cboUnits, dtData, "NAME", "ID")

                'Đơn vị tiền tệ
                dtData = rep.GetOtherList("TR_CURRENCY", True)
                FillRadCombobox(cboCurrency, dtData, "NAME", "ID")

                'Trạng thái
                dtData = rep.GetOtherList("DECISION_STATUS", True)
                FillRadCombobox(cboStatus, dtData, "NAME", "ID", True)
            End Using

            rdRequestDate.SelectedDate = Date.Now
            rntxtYear.Value = Date.Now.Year
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick

        Dim gID As Decimal
        Dim rep As New TrainingRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim obj As New RequestDTO

                        If CInt(lblNumOfRealTrainee.Text) > CInt(lblNumOfPlanTrainee.Text) And AllowOverStudent <> 1 Then
                            ctrlMessageBox.MessageText = Translate("Số học viên thực tế > số học viên trong kế hoạch.<br>Bạn có muốn tiếp tục không?")
                            ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_SAVE
                            ctrlMessageBox.DataBind()
                            ctrlMessageBox.Show()
                            Exit Sub
                        End If

                        If rdRequestDate.SelectedDate >= rdExpectedDate.SelectedDate Then
                            ShowMessage(Translate("Thời gian dự kiến phải lớn hơn ngày gửi"), Utilities.NotifyType.Warning)
                            Exit Sub
                        ElseIf rdRequestDate.SelectedDate >= rdStartDate.SelectedDate Then
                            ShowMessage(Translate("Thời gian bắt đầu phải lớn hơn ngày gửi"), Utilities.NotifyType.Warning)
                            Exit Sub
                        ElseIf rdStartDate.SelectedDate IsNot Nothing Then
                            If rdExpectedDate.SelectedDate > rdStartDate.SelectedDate Then
                                ShowMessage(Translate("Thời gian bắt đầu phải lớn hơn hoặc bằng thời gian dự kiến"), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If
                        End If

                        With obj
                            Dim cou_id = cboPlan.SelectedValue
                            Dim dtPlan As DataRow
                            If cbIrregularly.Checked Then
                                dtPlan = dtCourse.Select("ID = " + cou_id).FirstOrDefault
                            Else
                                dtPlan = dtCourse.Select("PLAN_ID = " + cou_id).FirstOrDefault
                            End If
                            If dtPlan Is Nothing Then
                                ShowMessage(Translate("Bạn phải chọn Khóa đào tạo"), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If
                            If cbIrregularly.Checked Then
                                '
                            Else
                                If dtPlan.Table.Columns.Contains("PLAN_ID") Then
                                    .TR_PLAN_ID = dtPlan("PLAN_ID").ToString
                                End If
                            End If

                            If hidOrgID.Value IsNot Nothing Then
                                .ORG_ID = hidOrgID.Value
                            End If
                            .YEAR = rntxtYear.Value
                            .IS_IRREGULARLY = cbIrregularly.Checked
                            If dtPlan.Table.Columns.Contains("ID") Then
                                .COURSE_ID = dtPlan("ID").ToString
                            End If
                            If cboTrainForm.SelectedValue <> "" Then
                                .TRAIN_FORM_ID = cboTrainForm.SelectedValue
                            End If
                            If cboPropertiesNeed.SelectedValue <> "" Then
                                .PROPERTIES_NEED_ID = cboPropertiesNeed.SelectedValue
                            End If
                            .EXPECTED_DATE = rdExpectedDate.SelectedDate
                            .START_DATE = rdStartDate.SelectedDate
                            .CONTENT = txtContent.Text
                            If cboUnits.SelectedValue <> "" Then
                                .UNIT_ID = cboUnits.SelectedValue
                            End If
                            .EXPECTED_COST = rntxtExpectedCost.Value
                            If cboCurrency.SelectedValue <> "" Then
                                .TR_CURRENCY_ID = Decimal.Parse(cboCurrency.SelectedValue)
                            End If
                            If cboStatus.SelectedValue <> "" Then
                                .STATUS_ID = Decimal.Parse(cboStatus.SelectedValue)
                            End If
                            .TARGET_TRAIN = txtTargetTrain.Text
                            .VENUE = txtVenue.Text
                            If hidSenderID.Value <> "" Then
                                .REQUEST_SENDER_ID = hidSenderID.Value
                            End If
                            .REQUEST_DATE = rdRequestDate.SelectedDate
                            .ATTACH_FILE = lblFilename.Text
                            .REMARK = txtRemark.Text

                            .lstCenters = (From item In lstCenter.CheckedItems Select New PlanCenterDTO With {.ID = item.Value}).ToList()
                            .lstTeachers = (From item In lstTeacher.CheckedItems Select New LectureDTO With {.ID = item.Value}).ToList()
                            .GROUP_PROGRAM_ID = CDec(cbGroupProgram.SelectedValue)
                        End With

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertRequest(obj, lstEmployee, gID) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Training&fid=ctrlTR_Request&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                obj.ID = hidID.Value
                                If rep.ModifyRequest(obj, lstEmployee, gID) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Training&fid=ctrlTR_Request&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select

                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Training&fid=ctrlTR_Request&group=Business")

            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
        UpdateControlState()
    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_SAVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                AllowOverStudent = 1
            ElseIf e.ActionName = CommonMessage.TOOLBARITEM_SAVE And e.ButtonID = MessageBoxButtonType.ButtonNo Then
                Dim cou_id = cboPlan.SelectedValue
                '
                If cbIrregularly.Checked Then
                    ' FillRadCombobox(cboPlan, dtCourse, "NAME", "ID")
                Else
                    Dim dtPlan As DataRow = dtCourse.Select("PLAN_ID = " + cou_id).FirstOrDefault
                    Using rep As New TrainingRepository
                        lstEmployee = rep.GetEmployeeByPlanID(New RequestDTO With {.TR_PLAN_ID = dtPlan("PLAN_ID").ToString})
                        lblNumOfPlanTrainee.Text = lstEmployee.Count
                        rgData.Rebind()
                    End Using
                End If

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub btnFindOrg_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindOrg.Click
        Try
            isLoadPopup = 2
            UpdateControlState()
            ctrlFindOrgPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Try
            Select Case isLoadPopup
                Case 2
                    Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
                    If orgItem IsNot Nothing Then
                        hidOrgID.Value = e.CurrentValue 'gán org đã chọn vào hiddenfield
                        txtOrgName.Text = orgItem.name_vn
                        txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.description_path)
                    End If
                    GetPlanInYearOrg()
            End Select
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindOrgPopup.CancelClicked
        isLoadPopup = 0
    End Sub
    Private Sub rntxtYear_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rntxtYear.TextChanged
        Try
            GetPlanInYearOrg()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub cbIrregularly_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cbIrregularly.Click
        Try
            GetPlanInYearOrg()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnFindSender_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindSender.Click
        Try
            isLoadPopup = 3
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Try
            For Each item As GridDataItem In rgData.SelectedItems
                If item.GetDataKeyValue("EMPLOYEE_ID") IsNot Nothing Then
                    Dim obj = (From p In lstEmployee Where p.EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")).FirstOrDefault
                    lstEmployee.Remove(obj)
                End If
            Next
            If rgData.SelectedItems.Count > 0 Then
                rgData.Rebind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Try
            Select Case isLoadPopup
                Case 1 'Employee
                    lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
                    For Each item As CommonBusiness.EmployeePopupFindDTO In lstCommonEmployee
                        If Not (From p In lstEmployee Where p.EMPLOYEE_ID = item.ID).Any Then
                            Dim objEmp As New RequestEmpDTO
                            objEmp.EMPLOYEE_ID = item.ID
                            objEmp.EMPLOYEE_CODE = item.EMPLOYEE_CODE
                            objEmp.EMPLOYEE_NAME = item.FULLNAME_VN
                            objEmp.BIRTH_DATE = item.BIRTH_DATE
                            objEmp.TITLE_NAME = item.TITLE_NAME
                            objEmp.COM_NAME = item.ORG_NAME
                            objEmp.ORG_NAME = item.ORG_NAME
                            objEmp.WORK_INVOLVE = item.WORK_INVOLVE
                            lstEmployee.Add(objEmp)
                        End If
                    Next
                    rgData.Rebind()
                Case 3 'Signer
                    lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
                    Dim signer = lstCommonEmployee.FirstOrDefault
                    If signer IsNot Nothing Then
                        hidSenderID.Value = signer.ID
                        txtSender.Text = signer.FULLNAME_VN
                        txtSenderMail.Text = IIf(signer.WORK_EMAIL IsNot Nothing, signer.WORK_EMAIL, signer.PER_EMAIL)
                        txtSenderMobile.Text = IIf(signer.MOBILE_PHONE IsNot Nothing, signer.MOBILE_PHONE, signer.HOME_PHONE)
                    End If
            End Select
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('TR_REQUEST_EMPLOYEE');", True)
            'D:\HungDQ\Project\SASCO\3.DEPLOYMENT\33.HiStaff\HistaffWebApp\Export.aspx.vb
            'D:\HungDQ\Project\SASCO\3.DEPLOYMENT\33.HiStaff\HistaffWebApp\ReportTemplates\Training\Import\RequestEmployee.xls
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub btnImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImport.Click
        ctrlUpload1.Show()
    End Sub
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Try

            Dim tempPath As String = "Excel"
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)

                '//Instantiate LoadOptions specified by the LoadFormat.
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                If workbook.Worksheets.GetSheetByCodeName("ImportRequest") Is Nothing Then
                    ShowMessage("File mẫu không đúng định dạng", NotifyType.Warning)
                    Exit Sub
                End If
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(3, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))

                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            Dim dtData As DataTable
            For Each dt As DataTable In dsDataPrepare.Tables
                If dtData Is Nothing Then
                    dtData = dt.Clone
                End If
                For Each row In dt.Rows
                    dtData.ImportRow(row)
                Next
            Next

            ImportData(dtData)
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub btnUploadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFile.Click
        ctrlUpload2.MaxFileInput = 1
        ctrlUpload2.isMultiple = False
        ctrlUpload2.AllowedExtensions = "pdf,png,doc,docx,xls,xlsx,jpg,jpeg"
        ctrlUpload2.Show()
    End Sub
    Private Sub ctrlUpload2_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload2.OkClicked
        Dim fileName As String
        Try
            If ctrlUpload2.UploadedFiles.Count > 1 Then
                ShowMessage("Chỉ được chọn 1 file để upload", NotifyType.Error)
                Exit Sub
            Else
                If ctrlUpload2.UploadedFiles.Count > 0 Then
                    Dim file As UploadedFile = ctrlUpload2.UploadedFiles(0)
                    fileName = Server.MapPath("~/ReportTemplates/Training/Upload/Request")
                    If Not Directory.Exists(fileName) Then
                        Directory.CreateDirectory(fileName)
                    End If
                    fileName = System.IO.Path.Combine(fileName, file.FileName)
                    file.SaveAs(fileName, True)
                    lblFilename.Text = file.FileName
                Else
                    ShowMessage(Translate("Chưa upload được file"), NotifyType.Error)
                End If

            End If
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi"), NotifyType.Error)
        End Try
    End Sub

    Private Sub cboPlan_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPlan.SelectedIndexChanged
        Try
            If cboPlan.SelectedValue = "" Then
                ClearControl()
            ElseIf dtCourse Is Nothing Then
                ClearControl()
            ElseIf dtCourse.Rows.Count = 0 Then
                ClearControl()
            Else
                Dim cou_id = cboPlan.SelectedValue
                Dim dtPlan As DataRow
                If cbIrregularly.Checked Then
                    dtPlan = dtCourse.Select("ID = " + cou_id).FirstOrDefault()
                Else
                    dtPlan = dtCourse.Select("PLAN_ID = " + cou_id).FirstOrDefault()
                End If

                If dtPlan Is Nothing Then
                    ClearControl()
                ElseIf dtPlan.Table.Rows.Count = 0 Then
                    ClearControl()
                Else
                    '1
                    If dtPlan.Table.Columns.Contains("PROGRAM_GROUP") Then
                        txtProgramGroup.Text = dtPlan("PROGRAM_GROUP").ToString
                    End If

                    '2
                    If dtPlan.Table.Columns.Contains("TRAIN_FIELD") Then
                        txtTrainField.Text = dtPlan("TRAIN_FIELD").ToString
                    End If

                    '3
                    If dtPlan.Table.Columns.Contains("TRAIN_FORM_ID") Then
                        cboTrainForm.SelectedValue = dtPlan("TRAIN_FORM_ID").ToString
                    End If

                    '4
                    If dtPlan.Table.Columns.Contains("PROPERTIES_NEED_ID") Then
                        cboPropertiesNeed.SelectedValue = dtPlan("PROPERTIES_NEED_ID").ToString
                    End If

                    '5
                    If dtPlan.Table.Columns.Contains("STUDENT_NUMBER") Then
                        lblNumOfPlanTrainee.Text = dtPlan("STUDENT_NUMBER").ToString
                    Else
                        lblNumOfPlanTrainee.Text = "0"
                    End If

                    '6
                    If dtPlan.Table.Columns.Contains("CENTERS") Then
                        Dim listCenter() As String = dtPlan("CENTERS").ToString().Split(New Char() {","})
                        For Each item As RadListBoxItem In lstCenter.Items
                            For Each cen As String In listCenter
                                If item.Value = cen Then
                                    item.Checked = True
                                    Exit For
                                End If
                            Next
                        Next

                        LoadTeacher()
                    End If

                    '6.1
                    If dtPlan.Table.Columns.Contains("UNIT_ID") Then
                        cboUnits.SelectedValue = dtPlan("UNIT_ID").ToString
                    End If
                    '6.2
                    If dtPlan.Table.Columns.Contains("COST_TOTAL") Then
                        rntxtExpectedCost.Value = dtPlan("COST_TOTAL").ToString
                    End If
                    '6.3
                    If dtPlan.Table.Columns.Contains("CURRENCY_ID") Then
                        cboCurrency.SelectedValue = dtPlan("CURRENCY_ID").ToString
                    End If

                    '7
                    If dtPlan.Table.Columns.Contains("TARGET_TRAIN") Then
                        txtTargetTrain.Text = dtPlan("TARGET_TRAIN").ToString
                    End If

                    '8
                    If dtPlan.Table.Columns.Contains("VENUE") Then
                        txtVenue.Text = dtPlan("VENUE").ToString
                    End If

                    '9
                    If cbIrregularly.Checked Then
                        '
                    Else
                        If dtPlan.Table.Columns.Contains("PLAN_ID") Then
                            Using rep As New TrainingRepository
                                lstEmployee = rep.GetEmployeeByPlanID(New RequestDTO With {.TR_PLAN_ID = dtPlan("PLAN_ID").ToString})
                                lblNumOfPlanTrainee.Text = lstEmployee.Count
                                rgData.Rebind()
                            End Using
                        End If
                    End If


                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub lstCenter_ItemCheck(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadListBoxItemEventArgs) Handles lstCenter.ItemCheck
        Try
            LoadTeacher()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    Protected Sub GetPlanInYearOrg()
        Try
            cboPlan.ClearSelection()
            cboPlan.Items.Clear()
            ClearControl()
            If hidOrgID.Value <> "" And rntxtYear.Value IsNot Nothing Then
                Using rep As New TrainingRepository
                    dtCourse = rep.GetTrPlanByYearOrg2(CDec(Val(cbGroupProgram.SelectedValue)), True, rntxtYear.Value, Decimal.Parse(hidOrgID.Value), cbIrregularly.Checked)
                    If cbIrregularly.Checked Then
                        FillRadCombobox(cboPlan, dtCourse, "NAME", "ID")
                    Else
                        FillRadCombobox(cboPlan, dtCourse, "NAME", "PLAN_ID")
                    End If
                End Using
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub LoadTeacher()
        Dim dtData As DataTable
        Dim tsp As New TrainingStoreProcedure
        Dim listCEN As String = ""
        Try
            lstTeacher.Items.Clear()

            For Each item As RadListBoxItem In lstCenter.CheckedItems
                listCEN += item.Value + ","
            Next

            If listCEN = "" Then Exit Sub

            dtData = tsp.GetLecture(listCEN)

            If dtData IsNot Nothing Then
                If dtData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtData.Rows
                        lstTeacher.Items.Add(New RadListBoxItem(dr("NAME").ToString(), dr("ID").ToString()))
                    Next
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub ClearControl()
        '1
        txtProgramGroup.Text = ""
        '2
        txtTrainField.Text = ""
        '3
        cboTrainForm.ClearSelection()
        '4
        cboPropertiesNeed.ClearSelection()
        '5
        lblNumOfPlanTrainee.Text = "0"
        '6
        For Each item As RadListBoxItem In lstCenter.Items
            item.Checked = False
        Next
        lstTeacher.Items.Clear()
        '7
        txtTargetTrain.Text = ""
        '8
        txtVenue.Text = ""

        '9
        lstEmployee = New List(Of RequestEmpDTO)
        rgData.Rebind()
    End Sub

    Protected Sub SetControlState(ByVal state As Boolean)
        btnFindOrg.Enabled = state
        rntxtYear.ReadOnly = Not state
        cbIrregularly.Enabled = state
        cboPlan.Enabled = state
        txtProgramGroup.Enabled = state
        txtTrainField.Enabled = state
        cboTrainForm.Enabled = state
        cboPropertiesNeed.Enabled = state
        rdExpectedDate.Enabled = state
        rdStartDate.Enabled = state
        lstCenter.Enabled = state
        lstTeacher.Enabled = state
        cboUnits.Enabled = state
        rntxtExpectedCost.ReadOnly = Not state
        cboCurrency.Enabled = state
        cboStatus.Enabled = state
        txtTargetTrain.ReadOnly = Not state
        txtVenue.ReadOnly = Not state
        btnFindSender.Enabled = state
        rdRequestDate.Enabled = state
        btnUploadFile.Enabled = state
        txtRemark.ReadOnly = Not state
        btnAdd.Enabled = state
        btnRemove.Enabled = state
        btnImport.Enabled = state
        btnExport.Enabled = state
        rgData.Enabled = state
    End Sub

    Public Sub ImportData(ByVal dtData As DataTable)
        Try
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Exit Sub
            End If
            Dim dtError As DataTable = dtData.Clone
            dtError.TableName = "DATA"
            dtError.Columns.Add("STT", GetType(String))
            Dim iRow As Integer = 7
            Dim IsError As Boolean = False
            Dim lstDtl As New List(Of RequestEmpDTO)
            For Each row As DataRow In dtData.Rows
                Dim sError As String = ""
                Dim rowError = dtError.NewRow
                Dim isRow = ImportValidate.TrimRow(row)
                Dim isScpExist As Boolean = False
                If Not isRow Then
                    iRow += 1
                    Continue For
                End If
                Dim obj As New RequestEmpDTO

                sError = "Chưa nhập Mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, IsError, sError)

                'obj.EMPLOYEE_ID = row("EMPLOYEE_ID").ToString
                obj.EMPLOYEE_CODE = row("EMPLOYEE_CODE").ToString
                obj.EMPLOYEE_NAME = row("EMPLOYEE_NAME").ToString
                obj.BIRTH_DATE = (New TrainingRepository()).ToDate(row("BIRTH_DATE"))
                obj.TITLE_NAME = row("TITLE_NAME").ToString
                obj.COM_NAME = row("COM_NAME").ToString
                obj.ORG_NAME = row("ORG_NAME").ToString
                obj.WORK_INVOLVE = row("WORK_INVOLVE").ToString
                lstDtl.Add(obj)

                If IsError Then
                    dtError.Rows.Add(rowError)
                End If
            Next
            If dtError.Rows.Count > 0 Then
                Session("EXPORTREPORT") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('TR_REQUEST_EMPLOYEE_ERROR')", True)
                'D:\HungDQ\Project\SASCO\3.DEPLOYMENT\33.HiStaff\HistaffWebApp\Export.aspx.vb
                'D:\HungDQ\Project\SASCO\3.DEPLOYMENT\33.HiStaff\HistaffWebApp\ReportTemplates\Training\Import\RequestEmployee_Error.xls
                ShowMessage(Translate("Giao dịch không thành công, chi tiết lỗi tệp tin đính kèm"), NotifyType.Warning)
            Else
                Using rep As New TrainingRepository
                    Dim sError As String = rep.GetEmployeeByImportRequest(lstDtl)
                    If sError <> "" Then
                        ShowMessage(Translate("Nhân viên không tồn tại trên hệ thống: " & sError), NotifyType.Warning)
                        Exit Sub
                    Else
                        Dim isExist As Boolean = False
                        For Each item In lstDtl
                            If Not (From p In lstEmployee Where p.EMPLOYEE_ID = item.EMPLOYEE_ID).Any Then
                                lstEmployee.Add(item)
                            Else
                                isExist = True
                            End If
                        Next
                        rgData.Rebind()
                    End If
                End Using
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Protected Sub CreateDataFilter(Optional ByVal isFull As Boolean = False)
        Try
            rgData.VirtualItemCount = lstEmployee.Count
            lblNumOfRealTrainee.Text = lstEmployee.Count
            rgData.DataSource = lstEmployee
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

    Private Sub cbGroupProgram_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cbGroupProgram.SelectedIndexChanged
        GetPlanInYearOrg()
    End Sub
End Class
Imports Framework.UI
Imports Common
Imports Training.TrainingBusiness
Imports Telerik.Web.UI
Imports Framework.UI.Utilities

Public Class ctrlTR_ProgramNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Public Overrides Property MustAuthorize As Boolean = False
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

    Public Property dtRequest As DataTable
        Get
            If ViewState(Me.ID & "_dtRequest") Is Nothing Then
                ViewState(Me.ID & "_dtRequest") = New DataTable
            End If
            Return ViewState(Me.ID & "_dtRequest")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtRequest") = value
        End Set
    End Property

    Public Property lstOrgCost As List(Of ProgramCostDTO)
        Get
            If ViewState(Me.ID & "_lstOrgCost") Is Nothing Then
                ViewState(Me.ID & "_lstOrgCost") = New List(Of ProgramCostDTO)
            End If
            Return ViewState(Me.ID & "_lstOrgCost")
        End Get
        Set(ByVal value As List(Of ProgramCostDTO))
            ViewState(Me.ID & "_lstOrgCost") = value
        End Set
    End Property

    Property CourseID As Decimal
        Get
            Return ViewState(Me.ID & "_CourseID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_CourseID") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub
    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(MainToolBar.Items(1), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            GetDataCombo()
            rntxtYear.Value = Date.Now.Year
            GetPlanInYear()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub GetDataCombo()
        Try
            Dim dtData As DataTable
            Dim tsp As New TrainingStoreProcedure
            Using rep As New TrainingRepository
                'Hình thức đào tạo
                dtData = rep.GetOtherList("TR_TRAIN_FORM", True)
                FillRadCombobox(cboHinhThuc, dtData, "NAME", "ID")

                'Tính chất nhu cầu
                dtData = rep.GetOtherList("TR_PROPERTIES_NEED", True)
                FillRadCombobox(cboTinhchatnhucau, dtData, "NAME", "ID")

                'Thời gian học
                dtData = rep.GetOtherList("TR_DURATION_STUDY", True)
                FillRadCombobox(cboDurationStudy, dtData, "NAME", "ID")

                'Thời lượng - đơn vị
                dtData = rep.GetOtherList("TR_DURATION_UNIT", True)
                FillRadCombobox(cboDurationType, dtData, "NAME", "ID")
                cboDurationType.SelectedValue = 4005 'Giờ

                'Ngôn ngữ giảng dạy
                dtData = rep.GetOtherList("TR_LANGUAGE", True)
                FillRadCombobox(cboLanguage, dtData, "NAME", "ID")

                'Đơn vị chủ trì đào tạo
                dtData = tsp.UnitGetList()
                FillRadCombobox(cboUnit, dtData, "NAME", "ID")

                'Dim lstCenters As List(Of CenterDTO) = rep.GetCenters()
                'lstCenter.DataSource = lstCenters
                'lstCenter.DataValueField = "ID"
                'If (Common.Common.SystemLanguage.Name = "vi-VN") Then
                '    lstCenter.DataTextField = "Name_VN"
                'Else
                '    lstCenter.DataTextField = "Name_EN"
                'End If

                'Trung tâm đào tạo
                dtData = tsp.GetCenters()
                lstCenter.CheckBoxes = True
                lstCenter.AutoPostBack = True
                For Each dr As DataRow In dtData.Rows
                    lstCenter.Items.Add(New RadListBoxItem(dr("NAME").ToString(), dr("ID").ToString()))
                Next
            End Using
        Catch ex As Exception
            Throw ex
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
                If IDSelect IsNot Nothing And IDSelect <> -1 Then
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
                    CurrentState = CommonMessage.STATE_NORMAL
                    Dim obj = rep.GetProgramById(IDSelect)
                    hidID.Value = obj.ID
                    hidRequestID.Value = obj.TR_REQUEST_ID
                    rntxtYear.Value = obj.YEAR
                    hidOrgID.Value = obj.ORG_ID
                    txtOrgName.Text = obj.ORG_NAME
                    GetPlanInYear()
                    If obj.TR_COURSE_ID IsNot Nothing Then
                        hidCourseID.Value = obj.TR_COURSE_ID
                        cboPlan.SelectedValue = obj.TR_COURSE_ID
                    End If
                    txtName.Text = obj.NAME
                    If obj.TRAIN_FORM_ID IsNot Nothing Then
                        cboHinhThuc.SelectedValue = obj.TRAIN_FORM_ID
                    End If
                    If obj.PROPERTIES_NEED_ID IsNot Nothing Then
                        cboTinhchatnhucau.SelectedValue = obj.PROPERTIES_NEED_ID
                    End If
                    txtNhomCT.Text = obj.PROGRAM_GROUP
                    txtLinhvuc.Text = obj.TRAIN_FIELD
                    rntxtDuration.Value = obj.DURATION
                    If obj.TR_DURATION_UNIT_ID IsNot Nothing Then
                        cboDurationType.SelectedValue = obj.TR_DURATION_UNIT_ID
                    End If
                    rdStartDate.SelectedDate = obj.START_DATE
                    rdEndDate.SelectedDate = obj.END_DATE
                    If obj.DURATION_STUDY_ID IsNot Nothing Then
                        cboDurationStudy.SelectedValue = obj.DURATION_STUDY_ID
                    End If
                    rntxtDurationHC.Value = obj.DURATION_HC
                    rntxtDurationOT.Value = obj.DURATION_OT
                    chkIsReimburse.Checked = False
                    If obj.IS_REIMBURSE IsNot Nothing Then
                        chkIsReimburse.Checked = obj.IS_REIMBURSE
                    End If
                    chkThilai.Checked = False
                    If obj.IS_RETEST IsNot Nothing Then
                        chkThilai.Checked = obj.IS_RETEST
                    End If
                    rntxtNumberStudent.Value = obj.STUDENT_NUMBER
                    rntxtCostCompanyUS.Value = obj.COST_TOTAL_US
                    rntxtCostOfStudentUS.Value = obj.COST_STUDENT_US
                    rntxtCostCompany.Value = obj.COST_TOTAL
                    rntxtCostOfStudent.Value = obj.COST_STUDENT

                    lblDVT.Text = "(Đơn vị tính: " & obj.TR_CURRENCY_NAME & ")"
                    lblDVT.ToolTip = obj.TR_CURRENCY_NAME

                    lstOrgCost = obj.Costs
                    rgChiPhi.Rebind()

                    For Each value In obj.Units
                        lstPartDepts.Items.Add(New RadListBoxItem(value.NAME, value.ID))
                    Next
                    For Each value In obj.Titles
                        lstPositions.Items.Add(New RadListBoxItem(value.NAME, value.ID))
                    Next
                    For Each value In obj.WIs
                        lstWork.Items.Add(New RadListBoxItem(value.NAME, value.ID))
                    Next

                    If obj.TR_LANGUAGE_ID IsNot Nothing Then
                        cboLanguage.SelectedValue = obj.TR_LANGUAGE_ID
                    End If
                    If obj.TR_UNIT_ID IsNot Nothing Then
                        cboUnit.SelectedValue = obj.TR_UNIT_ID
                    End If

                    Dim Centers_ID As String = ""
                    For Each value In obj.Centers
                        Centers_ID &= value.ID.ToString & ","
                        Dim item = lstCenter.FindItemByValue(value.ID.ToString)
                        If item IsNot Nothing Then
                            item.Checked = True
                        End If
                    Next
                    LoadTeacher(Centers_ID)
                    For Each value In obj.Lectures
                        Dim item = lstLecture.FindItemByValue(value.ID.ToString)
                        If item IsNot Nothing Then
                            item.Checked = True
                        End If
                    Next

                    txtContent.Text = obj.CONTENT
                    txtTargetTrain.Text = obj.TARGET_TRAIN
                    txtVenue.Text = obj.VENUE
                    txtRemark.Text = obj.REMARK

                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
                    lstOrgCost = New List(Of ProgramCostDTO)
                    rgChiPhi.Rebind()
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Overrides Sub UpdateControlState()
        Try
            If isLoadPopup = 1 Or isLoadPopup = 2 Then
                ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "common", "ctrlFindOrgPopup")
                ctrlFindOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                Select Case isLoadPopup
                    Case 1 'Chọn phòng ban tham gia
                    Case 2 'Chọn phòng ban cấp chi phí
                        ctrlFindOrgPopup.ShowCheckBoxes = TreeNodeTypes.All
                        ctrlFindOrgPopup.CheckChildNodes = True
                End Select
                phFindOrg.Controls.Add(ctrlFindOrgPopup)
                ctrlFindOrgPopup.Show()
            End If

            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    SetControlState(True)

                Case CommonMessage.STATE_EDIT
                    SetControlState(True)

                Case CommonMessage.STATE_NORMAL
                    SetControlState(False)

            End Select

            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub SetControlState(ByVal state As Boolean)
        Try
            rntxtYear.ReadOnly = Not state
            btnFindOrg.Enabled = state
            EnableRadCombo(cboPlan, state)
            txtName.ReadOnly = Not state
            EnableRadCombo(cboHinhThuc, state)
            EnableRadCombo(cboTinhchatnhucau, state)

            rntxtDuration.ReadOnly = Not state
            EnableRadCombo(cboDurationType, state)
            rdStartDate.Enabled = state
            rdEndDate.Enabled = state
            EnableRadCombo(cboDurationStudy, state)
            rntxtDurationHC.ReadOnly = Not state
            rntxtDurationOT.ReadOnly = Not state

            chkThilai.Enabled = state
            chkIsReimburse.Enabled = state
            btnFindOrgCost.Enabled = state
            btnDel.Enabled = state
            btnCalCost.Enabled = state

            lstPartDepts.Enabled = False
            lstPositions.Enabled = False
            lstWork.Enabled = False

            EnableRadCombo(cboLanguage, state)
            EnableRadCombo(cboUnit, state)
            lstCenter.Enabled = state
            lstLecture.Enabled = state
            txtContent.ReadOnly = Not state
            txtTargetTrain.ReadOnly = Not state
            txtVenue.ReadOnly = Not state
            txtRemark.ReadOnly = Not state
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim gID As Decimal
        Dim rep As New TrainingRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EDIT
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Training&fid=ctrlTR_Program&group=Business")
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim obj As New ProgramDTO

                        With obj
                            .TR_REQUEST_ID = Decimal.Parse(hidRequestID.Value)
                            .TR_COURSE_ID = CourseID
                            .YEAR = rntxtYear.Value
                            .ORG_ID = hidOrgID.Value
                            .NAME = txtName.Text
                            If cboHinhThuc.SelectedValue <> "" Then
                                .TRAIN_FORM_ID = cboHinhThuc.SelectedValue
                            End If
                            If cboTinhchatnhucau.SelectedValue <> "" Then
                                .PROPERTIES_NEED_ID = cboTinhchatnhucau.SelectedValue
                            End If
                            .DURATION = rntxtDuration.Value
                            If cboDurationType.SelectedValue <> "" Then
                                .TR_DURATION_UNIT_ID = Decimal.Parse(cboDurationType.SelectedValue)
                            End If
                            .START_DATE = rdStartDate.SelectedDate
                            .END_DATE = rdEndDate.SelectedDate
                            If cboDurationStudy.SelectedValue <> "" Then
                                .DURATION_STUDY_ID = cboDurationStudy.SelectedValue
                            End If
                            .DURATION_HC = rntxtDurationHC.Value
                            .DURATION_OT = rntxtDurationOT.Value
                            .IS_REIMBURSE = chkIsReimburse.Checked
                            .IS_RETEST = chkThilai.Checked
                            .STUDENT_NUMBER = rntxtNumberStudent.Value
                            .COST_TOTAL_US = rntxtCostCompanyUS.Value
                            .COST_STUDENT_US = rntxtCostOfStudentUS.Value
                            .COST_TOTAL = rntxtCostCompany.Value
                            .COST_STUDENT = rntxtCostOfStudent.Value

                            If lstOrgCost.Count > 0 Then
                                .Costs = lstOrgCost
                            End If

                            If lstPartDepts.Items.Count > 0 Then
                                If lstPartDepts.Items.Count > 1 Or (lstPartDepts.Items.Count = 1 And lstPartDepts.Items(0).Value <> "") Then
                                    .Units = (From item In lstPartDepts.Items Select New ProgramOrgDTO With {.ID = item.Value}).ToList()
                                End If
                                If lstPartDepts.Items.Count > 0 Then
                                    .Departments_NAME = lstPartDepts.Items.Select(Function(x) x.Text).Aggregate(Function(x, y) x & ", " & y)
                                End If
                            End If

                            If lstPositions.Items.Count > 0 Then
                                'If lstPositions.Items.Count > 1 Or (lstPositions.Items.Count = 1 And lstPositions.Items(0).Value <> "") Then
                                .Titles = (From item In lstPositions.Items Select New ProgramTitleDTO With {.ID = item.Value}).ToList()
                                'End If
                                'If lstPositions.CheckedItems.Count > 0 Then
                                .Titles_NAME = lstPositions.Items.Select(Function(x) x.Text).Aggregate(Function(x, y) x & ", " & y)
                                'End If
                            End If

                            If cboLanguage.SelectedValue <> "" Then
                                .TR_LANGUAGE_ID = cboLanguage.SelectedValue
                            End If
                            If cboUnit.SelectedValue <> "" Then
                                .TR_UNIT_ID = cboUnit.SelectedValue
                            End If

                            If lstCenter.Items.Count > 0 Then
                                If lstCenter.Items.Count > 1 Or (lstCenter.Items.Count = 1 And lstCenter.Items(0).Value <> "") Then
                                    .Centers = (From item In lstCenter.CheckedItems Select New ProgramCenterDTO With {.ID = item.Value}).ToList()
                                End If
                                If lstCenter.CheckedItems.Count > 0 Then
                                    .Centers_NAME = lstCenter.CheckedItems.Select(Function(x) x.Text).Aggregate(Function(x, y) x & ", " & y)
                                End If
                            End If

                            If lstLecture.Items.Count > 0 Then
                                If lstLecture.Items.Count > 1 Or (lstLecture.Items.Count = 1 And lstLecture.Items(0).Value <> "") Then
                                    .Lectures = (From item In lstLecture.CheckedItems Select New ProgramLectureDTO With {.ID = item.Value}).ToList()
                                End If
                                If lstLecture.CheckedItems.Count > 0 Then
                                    .Lectures_NAME = lstLecture.CheckedItems.Select(Function(x) x.Text).Aggregate(Function(x, y) x & ", " & y)
                                End If
                            End If

                            .CONTENT = txtContent.Text
                            .TARGET_TRAIN = txtTargetTrain.Text
                            .VENUE = txtVenue.Text
                            .REMARK = txtRemark.Text
                        End With

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertProgram(obj, gID) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Training&fid=ctrlTR_Program&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                obj.ID = Decimal.Parse(hidID.Value)
                                If rep.ModifyProgram(obj, gID) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Training&fid=ctrlTR_Program&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
        UpdateControlState()
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgChiPhi.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Protected Sub CreateDataFilter(Optional ByVal isFull As Boolean = False)
        Try
            rgChiPhi.VirtualItemCount = lstOrgCost.Count
            rgChiPhi.DataSource = lstOrgCost
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rntxtYear_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rntxtYear.TextChanged
        Try
            GetPlanInYear()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub btnFindOrg_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindOrg.Click
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindOrgPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub btnFindOrgCost_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindOrgCost.Click
        Try
            ReadCost()

            isLoadPopup = 2
            UpdateControlState()
            ctrlFindOrgPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub btnDel_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDel.Click
        Try
            ReadCost()

            For Each item As GridDataItem In rgChiPhi.SelectedItems
                If item.GetDataKeyValue("ORG_ID") IsNot Nothing Then
                    Dim obj = (From p In lstOrgCost Where p.ORG_ID = item.GetDataKeyValue("ORG_ID")).FirstOrDefault
                    lstOrgCost.Remove(obj)
                End If
            Next
            If rgChiPhi.SelectedItems.Count > 0 Then
                rgChiPhi.Rebind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ReadCost()
        For Each org As ProgramCostDTO In lstOrgCost
            For Each item As GridDataItem In rgChiPhi.MasterTableView.Items
                If org.ORG_ID = item.GetDataKeyValue("ORG_ID") Then
                    Dim rntxtCost As RadNumericTextBox = CType(item.FindControl("ValueTS"), RadNumericTextBox)
                    org.COST_COMPANY = rntxtCost.Value
                End If
            Next
        Next
    End Sub
    Protected Sub btnCalCost_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCalCost.Click
        Try
            If lblDVT.ToolTip = "" Then
                ShowMessage(Translate("Chưa chọn khóa đào tạo"), Utilities.NotifyType.Warning)
                Exit Sub
            ElseIf rntxtNumberStudent.Value Is Nothing OrElse rntxtNumberStudent.Value = 0 Then
                ShowMessage(Translate("Khóa đào tạo này chưa chọn học viên"), Utilities.NotifyType.Warning)
                Exit Sub
            Else
                Dim total As Decimal = 0
                For Each item As GridDataItem In rgChiPhi.MasterTableView.Items
                    Dim rntxtCost As RadNumericTextBox = CType(item.FindControl("ValueTS"), RadNumericTextBox)
                    total += rntxtCost.Value 'cost
                Next

                If total = 0 Then
                    ShowMessage(Translate("Chưa khai báo chi phí đào tạo cho phòng ban."), Utilities.NotifyType.Warning, 8)
                    Exit Sub
                ElseIf total < 0 Then
                    ShowMessage(Translate("Tổng chi phí đào tạo quá bé. Vui lòng kiểm tra lại."), Utilities.NotifyType.Warning, 8)
                    Exit Sub
                Else
                    Select Case lblDVT.ToolTip.ToUpper
                        Case "USD"
                            rntxtCostCompanyUS.Value = total
                            rntxtCostOfStudentUS.Value = (total / CDec(rntxtNumberStudent.Value))
                        Case Else
                            rntxtCostCompany.Value = total
                            rntxtCostOfStudent.Value = (total / CDec(rntxtNumberStudent.Value))
                    End Select
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Try
            Select Case isLoadPopup
                Case 1
                    Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
                    If orgItem IsNot Nothing Then
                        hidOrgID.Value = e.CurrentValue 'gán org đã chọn vào hiddenfield
                        txtOrgName.Text = orgItem.name_vn
                        txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.description_path)
                    End If
                    GetPlanInYear()
                Case 2
                    Dim lstIds = e.SelectedValues
                    If lstIds IsNot Nothing AndAlso lstIds.Count > 0 AndAlso e.SelectedTexts.Count = e.SelectedValues.Count Then
                        For i = 0 To e.SelectedValues.Count - 1
                            Dim orgID = e.SelectedValues(i)
                            If Not lstOrgCost.Exists(Function(x) x.ORG_ID.ToString = orgID) Then
                                lstOrgCost.Add(New ProgramCostDTO With {.ORG_ID = e.SelectedValues(i),
                                                                        .ORG_NAME = e.SelectedTexts(i),
                                                                        .COST_COMPANY = 0})
                            End If
                        Next
                        rgChiPhi.Rebind()
                    End If

            End Select
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindOrgPopup.CancelClicked
        isLoadPopup = 0
    End Sub

    Protected Sub GetPlanInYear()
        Try
            If rntxtYear.Value IsNot Nothing And hidOrgID.Value <> "" Then
                Using rep As New TrainingRepository
                    dtRequest = rep.GetTrPlanByYearOrg(True, rntxtYear.Value, hidOrgID.Value)
                    FillRadCombobox(cboPlan, dtRequest, "NAME", "REQUEST_ID")
                End Using
            Else
                cboPlan.ClearSelection()
                cboPlan.Items.Clear()
                hidOrgID.Value = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cboPlan_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPlan.SelectedIndexChanged
        Try
            Using rep As New TrainingRepository
                If cboPlan.SelectedValue <> "" Then
                    Dim ReqID As Decimal = 0
                    For Each dr As DataRow In dtRequest.Select("REQUEST_ID = " & cboPlan.SelectedValue)
                        ReqID = CDec(dr("REQUEST_ID").ToString)
                    Next
                    Dim obj As RequestDTO = rep.GetRequestsForProgram(ReqID) 'Decimal.Parse(cboPlan.SelectedValue)
                    If obj IsNot Nothing Then
                        '=Thông tin chung
                        'Mã yêu cầu
                        hidRequestID.Value = obj.ID
                        'Mã khóa
                        CourseID = obj.COURSE_ID
                        'Tên chương trình đào tạo
                        txtName.Text = obj.TR_PLAN_NAME
                        'Hình thức
                        If obj.TRAIN_FORM_ID IsNot Nothing Then
                            cboHinhThuc.SelectedValue = obj.TRAIN_FORM_ID
                        End If
                        'Nhu cầu
                        If obj.PROPERTIES_NEED_ID IsNot Nothing Then
                            cboTinhchatnhucau.SelectedValue = obj.PROPERTIES_NEED_ID
                        End If
                        'Nhóm chương trình
                        txtNhomCT.Text = obj.PROGRAM_GROUP
                        'Lĩnh vực đào tạo
                        txtLinhvuc.Text = obj.TRAIN_FIELD
                        'Số lượng học viên
                        rntxtNumberStudent.Value = obj.NUM_EMP

                        '=Đối tượng tham gia
                        'Đơn vị tham gia
                        If obj.lstOrgs IsNot Nothing Then
                            lstPartDepts.Items.Clear()
                            For Each value In obj.lstOrgs
                                lstPartDepts.Items.Add(New RadListBoxItem(value.NAME, value.ID))
                            Next
                        End If
                        'If obj.ORG_GRO IsNot Nothing And obj.ORG_ID_GRO IsNot Nothing Then
                        '    Dim listORG As New List(Of String)(obj.ORG_GRO.Split(","c))
                        '    Dim listORGID As New List(Of String)(obj.ORG_ID_GRO.Split(","c))
                        '    listORG = listORG.Distinct().ToList
                        '    listORGID = listORGID.Distinct().ToList
                        '    lstPartDepts.Items.Clear()
                        '    For i As Decimal = 0 To listORG.Count - 1
                        '        lstPartDepts.Items.Add(New RadListBoxItem(listORG(i), listORGID(i)))
                        '    Next
                        'End If
                        'Chức danh
                        If obj.lstTits IsNot Nothing Then
                            lstPositions.Items.Clear()
                            For Each value In obj.lstTits
                                lstPositions.Items.Add(New RadListBoxItem(value.NAME, value.ID))
                            Next
                        End If
                        'If obj.TIT_GRO IsNot Nothing And obj.TIT_ID_GRO IsNot Nothing Then
                        '    Dim listTIT As New List(Of String)(obj.TIT_GRO.Split(","c))
                        '    Dim listTITID As New List(Of String)(obj.TIT_ID_GRO.Split(","c))
                        '    listTIT = listTIT.Distinct().ToList
                        '    listTITID = listTITID.Distinct().ToList
                        '    lstPositions.Items.Clear()
                        '    For i As Decimal = 0 To listTIT.Count - 1
                        '        lstPositions.Items.Add(New RadListBoxItem(listTIT(i), listTITID(i)))
                        '    Next
                        'End If
                        'Công việc liên quan
                        If obj.lstWIs IsNot Nothing Then
                            lstWork.Items.Clear()
                            For Each value In obj.lstWIs
                                If value.ID <> 0 Then '0 = Nothing
                                    lstWork.Items.Add(New RadListBoxItem(value.NAME, value.ID))
                                End If
                            Next
                        End If
                        'If obj.WI_GRO IsNot Nothing Then
                        '    Dim listWI As New List(Of String)(obj.WI_GRO.Split(","c))
                        '    lstWork.Items.Clear()
                        '    For Each wi As String In listWI.Distinct()
                        '        lstWork.Items.Add(New RadListBoxItem(wi))
                        '    Next
                        'End If

                        '=Thông tin nhà cung cấp
                        'Trung tâm đào tạo
                        If obj.lstCenters IsNot Nothing Then
                            For Each item As RadListBoxItem In lstCenter.Items
                                For Each cen As PlanCenterDTO In obj.lstCenters
                                    If item.Value = cen.ID Then
                                        item.Checked = True
                                        Exit For
                                    End If
                                Next
                            Next
                            LoadTeacher(obj.lstCenters, obj.lstTeachers)
                        End If
                        'If obj.CENTERS_ID IsNot Nothing Then
                        '    Dim listCenter() As String = obj.CENTERS_ID.Split(New Char() {","})
                        '    For Each item As RadListBoxItem In lstCenter.Items
                        '        For Each cen As String In listCenter
                        '            If item.Value = cen Then
                        '                item.Checked = True
                        '                Exit For
                        '            End If
                        '        Next
                        '    Next
                        '    LoadTeacher(obj.CENTERS_ID, obj.TEACHERS_ID)
                        'End If
                        'Nội dung
                        txtContent.Text = obj.CONTENT
                        'Mục tiêu
                        txtTargetTrain.Text = obj.TARGET_TRAIN
                        'Địa điểm tổ chức
                        txtVenue.Text = obj.VENUE
                        'Ghi chú
                        txtRemark.Text = obj.REMARK
                        'Đơn vị tính
                        If obj.TR_CURRENCY_NAME <> "" Then
                            lblDVT.Text = "(Đơn vị tính: " & obj.TR_CURRENCY_NAME & ")"
                            lblDVT.ToolTip = obj.TR_CURRENCY_NAME
                        Else
                            lblDVT.Text = ""
                            lblDVT.ToolTip = ""
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub lstCenter_ItemCheck(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadListBoxItemEventArgs) Handles lstCenter.ItemCheck
        Try
            Dim Centers_ID As String = ""
            For Each item As RadListBoxItem In lstCenter.CheckedItems
                Centers_ID &= item.Value & ","
            Next
            LoadTeacher(Centers_ID)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    Protected Sub LoadTeacher(ByVal sCenter As List(Of PlanCenterDTO), ByVal sLecture As List(Of LectureDTO))
        Dim dtData As DataTable
        Dim tsp As New TrainingStoreProcedure
        Dim listCEN As String = ""
        Try
            'Giảng viên
            sCenter.ForEach(Sub(x) listCEN &= x.ID & ",")
            dtData = tsp.GetLecture(listCEN)
            If dtData IsNot Nothing Then
                If dtData.Rows.Count > 0 Then
                    lstLecture.Items.Clear()
                    lstLecture.CheckBoxes = True
                    For Each dr As DataRow In dtData.Rows
                        Dim lbi As RadListBoxItem = New RadListBoxItem(dr("NAME").ToString(), dr("ID").ToString())
                        If sLecture.Count > 0 Then
                            For Each tea As LectureDTO In sLecture
                                If dr("ID").ToString() = tea.ID.ToString() Then
                                    lbi.Checked = True
                                    Exit For
                                End If
                            Next
                        End If
                        lstLecture.Items.Add(lbi)
                    Next
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub LoadTeacher(ByVal sCenter As String)
        Dim dtData As DataTable
        Dim tsp As New TrainingStoreProcedure
        Try
            'Giảng viên
            lstLecture.Items.Clear()
            lstLecture.CheckBoxes = True
            dtData = tsp.GetLecture(sCenter)
            If dtData IsNot Nothing Then
                If dtData.Rows.Count > 0 Then
                    For Each dr As DataRow In dtData.Rows
                        Dim lbi As RadListBoxItem = New RadListBoxItem(dr("NAME").ToString(), dr("ID").ToString())
                        lstLecture.Items.Add(lbi)
                    Next
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region
End Class
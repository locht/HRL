Imports Framework.UI
Imports Common
Imports Training.TrainingBusiness
Imports Telerik.Web.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports System.IO

Public Class ctrlTR_PlanNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Protected WithEvents ctrlFindMEmployeePopup As ctrlFindMEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
    Protected repHF As HistaffFrameworkRepository
    Private tsp As New TrainingStoreProcedure()
    Public Class lstOrgDTO
        Public Property ORG_ID As Decimal
        Public Property ORG_NAME As String
    End Class
    Public Class lstTitleDTO
        Public Property TITLE_ID As Decimal
        Public Property TITLE_NAME As String
    End Class
    Public Class lstWORKINVOLVEDTO
        Public Property WI_ID As Decimal
        Public Property WI_NAME As String
    End Class
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
    Public Property lstcostDT As List(Of CostDetailDTO)
        Get
            If ViewState(Me.ID & "_lstcostDT") Is Nothing Then
                ViewState(Me.ID & "_lstcostDT") = New List(Of CostDetailDTO)
            End If
            Return ViewState(Me.ID & "_lstcostDT")
        End Get
        Set(ByVal value As List(Of CostDetailDTO))
            ViewState(Me.ID & "_lstcostDT") = value
        End Set
    End Property
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
    Property STATUSCODE As Decimal
        Get
            Return ViewState(Me.ID & "_STATUSCODE")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_STATUSCODE") = value
        End Set
    End Property

    Property ParticipcatedDetpNames As List(Of String)
        Get
            Return ViewState(Me.ID & "_ParticipcatedDetpNames")
        End Get
        Set(ByVal value As List(Of String))
            ViewState(Me.ID & "_ParticipcatedDetpNames") = value
        End Set
    End Property

    Property ParticipcatedDetpIds As List(Of String)
        Get
            Return ViewState(Me.ID & "_ParticipcatedDetpIds")
        End Get
        Set(ByVal value As List(Of String))
            ViewState(Me.ID & "_ParticipcatedDetpIds") = value
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
            AddDataToCboCenter()


            rntxtYear.Value = Date.Now.Year
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
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

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New TrainingRepository
        Try

            Select Case Message

                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim obj = rep.GetPlanById(IDSelect)
                    txtName.Text = obj.NAME
                    hidID.Value = obj.ID
                    txtOrgName.Text = obj.ORG_NAME
                    hidOrgID.Value = obj.ORG_ID.ToString
                    txtRemark.Text = obj.REMARK
                    'For Each value In obj.Centers
                    '    Dim item = lstCenter.FindItemByValue(value.ID.ToString)
                    '    If item IsNot Nothing Then
                    '        item.Checked = True
                    '    End If
                    'Next
                    txtWork_Relation.Text = obj.WORK_RELATION
                    If obj.IS_EVALUATE = -1 Then
                        chkEvaluate.Checked = True
                    Else
                        chkEvaluate.Checked = False
                    End If
                    If obj.GR_PROGRAM_ID IsNot Nothing Then
                        cboGrProgram.SelectedValue = obj.GR_PROGRAM_ID
                    End If
                    If obj.STATUS_ID IsNot Nothing Then
                        cboStatus.SelectedValue = obj.STATUS_ID
                        If cboStatus.SelectedValue = 4001 Then
                            EnableControlAll_Cus(False, RadPane2)
                        End If
                    End If
                    If obj.LEVEL_PRIOTY IsNot Nothing Then
                        rnPrioty.Value = obj.LEVEL_PRIOTY
                    End If


                    Dim Centers_ID As String = ""
                    For Each value In obj.Centers
                        Centers_ID &= value.ID.ToString & ","
                        Dim item = cboCenter.FindItemByValue(value.ID.ToString)
                        If item IsNot Nothing Then
                            item.Checked = True
                        End If
                    Next
                    For Each item In obj.Units
                        Dim var = lstPartDepts.Items.Where(Function(f) f.Value = item.ID.ToString).ToList
                        If var.Count <= 0 Or lstPartDepts.Items.Count = 0 Then
                            lstPartDepts.Items.Add(New RadListBoxItem(item.NAME, item.ID))
                        End If
                    Next
                    For Each value In obj.Titles
                        Dim var = lstPositions.Items.Where(Function(f) f.Value = value.ID.ToString).ToList
                        If var.Count <= 0 Or lstPositions.Items.Count = 0 Then
                            lstPositions.Items.Add(New RadListBoxItem(value.NAME, value.ID))
                        End If
                    Next
                    'PopulatingListTitle()
                    'getListTitle()
                    'For Each value In obj.Titles
                    '    Dim item = lstPositions.FindItemByValue(value.ID.ToString)
                    '    If item IsNot Nothing Then
                    '        item.Checked = True
                    '    End If
                    'Next
                    cboHinhThuc.SelectedValue = obj.TR_TRAIN_FORM_ID
                    cboTinhchatnhucau.SelectedValue = obj.PROPERTIES_NEED_ID
                    If obj.UNIT_ID IsNot Nothing Then
                        cboUnit.SelectedValue = obj.UNIT_ID
                    End If

                    txtTargetTrain.Text = obj.TARGET_TRAIN
                    txtVenue.Text = obj.VENUE
                    rntxtCostPerEmp.Value = obj.COST_OF_STUDENT
                    rntxtTotal.Value = obj.COST_TOTAL
                    rntxtCostPerEmpUS.Value = obj.COST_OF_STUDENT_USD
                    rntxtTotalUS.Value = obj.COST_TOTAL_USD
                    'rntxtOtherCost.Value = obj.COST_OTHER
                    'rntxtTravleCost.Value = obj.COST_TRAVEL
                    'rntxtTrainingCost.Value = obj.COST_TRAIN


                    'rntxtInccurredCost.Value = obj.COST_INCURRED
                    'If obj.TR_CURRENCY_ID IsNot Nothing Then
                    '    cboCurrency.SelectedValue = obj.TR_CURRENCY_ID
                    'End If
                    rntxtTutors.Value = obj.TEACHER_NUMBER
                    rntxtStudents.Value = obj.STUDENT_NUMBER
                    rntxtDuration.Value = obj.DURATION
                    If obj.TR_DURATION_UNIT_ID IsNot Nothing Then
                        cboDurationType.SelectedValue = obj.TR_DURATION_UNIT_ID
                    End If
                    ' CostCalculating()
                    ' year

                    cbJan.Checked = obj.PLAN_T1
                    cbFeb.Checked = obj.PLAN_T2
                    cbMar.Checked = obj.PLAN_T3
                    cbApr.Checked = obj.PLAN_T4
                    cbMay.Checked = obj.PLAN_T5
                    cbJun.Checked = obj.PLAN_T6
                    cbJul.Checked = obj.PLAN_T7
                    cbAug.Checked = obj.PLAN_T8
                    cbSep.Checked = obj.PLAN_T9
                    cbOct.Checked = obj.PLAN_T10
                    cbNov.Checked = obj.PLAN_T11
                    cbDec.Checked = obj.PLAN_T12
                    lstcostDT.Clear()
                    lstcostDT = rep.GetPlan_Cost_Detail(obj.ID)
                    If obj.TR_COURSE_ID IsNot Nothing Then
                        cboCourse.SelectedValue = obj.TR_COURSE_ID
                        cboCourse.Text = obj.TR_COURSE_NAME
                    End If
                    ' cboCourse_SelectedIndexChanged(Nothing, Nothing)
                    If obj.ATTACHFILE IsNot Nothing Then
                        lblFilename.Text = obj.ATTACHFILE
                        'templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
                        'filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & ("ReportTemplates\Training\Upload\") + obj.ATTACHFILE
                        'If Not File.Exists(filePath) Then

                        'End If
                        lblFilename.NavigateUrl = "http://" & Request.Url.Host & ":" & Request.Url.Port & "/ReportTemplates/Training/Upload/" + obj.ATTACHFILE
                        '   System.IO.Path.Combine(filePath)
                    End If

                    'PopulatingListWI()
                    repHF = New HistaffFrameworkRepository
                    Dim dtData1 = repHF.ExecuteToDataSet("PKG_TRAINING.PLAN_CHECK_REQUEST", New List(Of Object)({obj.ID})).Tables(0)


                    If dtData1 IsNot Nothing Then
                        If dtData1.Rows.Count >= 1 Then
                            RadPane2.Enabled = False
                            CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                        End If
                    End If
                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
            End Select

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "Event"

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        rgISP.AllowPaging = False
        rgISP.AllowCustomPaging = True
    End Sub

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick

        Dim gID As Decimal
        Dim rep As New TrainingRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim obj As New PlanDTO

                        With obj
                            .YEAR = rntxtYear.Value
                            .NAME = txtName.Text
                            If hidOrgID.Value IsNot Nothing Then .ORG_ID = hidOrgID.Value
                            If cboCourse.SelectedValue <> "" Then
                                .TR_COURSE_ID = Decimal.Parse(cboCourse.SelectedValue)
                            End If
                            If cbJan.Checked = False And cbFeb.Checked = False And cbMar.Checked = False And
                       cbMar.Checked = False And cbJul.Checked = False And cbAug.Checked = False And
                       cbSep.Checked = False And cbOct.Checked = False And cbNov.Checked = False And cbDec.Checked = False Then
                                ShowMessage(Translate("Phải chọn ít nhất 1 trong 12 tháng tổ chức, Xin kiểm tra lại"), NotifyType.Warning)
                                Exit Sub
                            End If
                            .PLAN_T1 = cbJan.Checked
                            If cbJan.Checked Then
                                .Months_NAME &= cbJan.Text & ", "
                            End If
                            .PLAN_T2 = cbFeb.Checked
                            If cbFeb.Checked Then
                                .Months_NAME &= cbFeb.Text & ", "
                            End If
                            .PLAN_T3 = cbMar.Checked
                            If cbMar.Checked Then
                                .Months_NAME &= cbMar.Text & ", "
                            End If
                            .PLAN_T4 = cbApr.Checked
                            If cbApr.Checked Then
                                .Months_NAME &= cbApr.Text & ", "
                            End If
                            .PLAN_T5 = cbMay.Checked
                            If cbMay.Checked Then
                                .Months_NAME &= cbMay.Text & ", "
                            End If
                            .PLAN_T6 = cbJun.Checked
                            If cbJun.Checked Then
                                .Months_NAME &= cbJun.Text & ", "
                            End If
                            .PLAN_T7 = cbJul.Checked
                            If cbJul.Checked Then
                                .Months_NAME &= cbJul.Text & ", "
                            End If
                            .PLAN_T8 = cbAug.Checked
                            If cbAug.Checked Then
                                .Months_NAME &= cbAug.Text & ", "
                            End If
                            .PLAN_T9 = cbSep.Checked
                            If cbSep.Checked Then
                                .Months_NAME &= cbSep.Text & ", "
                            End If
                            .PLAN_T10 = cbOct.Checked
                            If cbOct.Checked Then
                                .Months_NAME &= cbOct.Text & ", "
                            End If
                            .PLAN_T11 = cbNov.Checked
                            If cbNov.Checked Then
                                .Months_NAME &= cbNov.Text & ", "
                            End If
                            .PLAN_T12 = cbDec.Checked
                            If cbDec.Checked Then
                                .Months_NAME &= cbDec.Text & ", "
                            End If
                            If .Months_NAME <> "" Then
                                .Months_NAME = .Months_NAME.Substring(0, .Months_NAME.Length - 2)
                            End If
                            .DURATION = rntxtDuration.Value
                            If cboDurationType.SelectedValue <> "" Then
                                .TR_DURATION_UNIT_ID = Decimal.Parse(cboDurationType.SelectedValue)
                            End If
                            .PROPERTIES_NEED_ID = Decimal.Parse(cboTinhchatnhucau.SelectedValue)
                            .TR_TRAIN_FORM_ID = Decimal.Parse(cboHinhThuc.SelectedValue)
                            .CODE = rep.GetCodeCourse(cboCourse.SelectedValue)
                            .TEACHER_NUMBER = rntxtTutors.Value
                            .STUDENT_NUMBER = rntxtStudents.Value

                            '.COST_TRAIN = rntxtTrainingCost.Value
                            '.COST_INCURRED = rntxtInccurredCost.Value
                            '.COST_TRAVEL = rntxtTravleCost.Value
                            '.COST_OTHER = rntxtOtherCost.Value
                            If IsNumeric(rntxtTotal.Value) Then
                                If rntxtTotal.Value > 0 Then
                                    .COST_TOTAL = rntxtTotal.Value
                                Else
                                    ShowMessage(Translate("Tổng chi phí đào tạo phải lớn hơn 0, Xin kiểm tra lại"), NotifyType.Warning)
                                    Exit Sub
                                End If
                            Else
                                ShowMessage(Translate("Phải có số tiền Tổng chi phí đào tạo, Xin nhập chi phí"), NotifyType.Warning)
                                Exit Sub
                            End If


                            .COST_OF_STUDENT = rntxtCostPerEmp.Value
                            .COST_TOTAL_USD = rntxtTotalUS.Value
                            .COST_OF_STUDENT_USD = rntxtCostPerEmpUS.Value
                            If cboUnit.SelectedValue <> "" Then
                                .UNIT_ID = Decimal.Parse(cboUnit.SelectedValue)
                            End If

                            'If cboCurrency.SelectedValue <> "" Then
                            '    .TR_CURRENCY_ID = Decimal.Parse(cboCurrency.SelectedValue)
                            'End If

                            Dim lstitemgrid As New List(Of CostDetailDTO)
                            For Each row As GridDataItem In rgISP.Items
                                ' loops through each rows in RadGrid
                                'For Each col As GridColumn In radgvShipDocs.MasterTableView.Items
                                'loops through each AutoGenerated column in RadGrid
                                Dim itemgrid As New CostDetailDTO
                                Dim moneytxt As String
                                itemgrid.TYPE_ID = Decimal.Parse(row("CODE").Text)
                                Try
                                    moneytxt = DirectCast(row("MONEY_TEMP").FindControl("MONEY"), RadNumericTextBox).Text
                                    If IsNumeric(moneytxt) Then
                                        itemgrid.MONEY = Int32.Parse(DirectCast(row("MONEY_TEMP").FindControl("MONEY"), RadNumericTextBox).Text)
                                    Else
                                        itemgrid.MONEY = 0
                                    End If


                                    itemgrid.MONEY_TYPE = Int32.Parse(DirectCast(row("MONEY_U_TEMP").FindControl("RadMoneyU"), Telerik.Web.UI.RadComboBox).SelectedValue)

                                Catch ex As Exception

                                End Try
                                lstitemgrid.Add(itemgrid)
                            Next
                            .CostDetail = lstitemgrid
                            '.CostDetail = (From item In rgISP.Items Select New CostDetailDTO With {.ID = item}).ToList()
                            .TARGET_TRAIN = txtTargetTrain.Text
                            .VENUE = txtVenue.Text
                            .REMARK = txtRemark.Text
                            ' .Work_inv = (From item In lstWork.Items Select New PlanTitleDTO With {.ID = item.Value}).ToList()
                            .Titles = (From item In lstPositions.Items Select New PlanTitleDTO With {.ID = item.Value}).ToList()
                            .Units = (From item In lstPartDepts.Items Select New PlanOrgDTO With {.ID = item.Value}).ToList()
                            .Centers = (From item In cboCenter.CheckedItems Select New PlanCenterDTO With {.ID = item.Value}).ToList()

                            If cboCenter.CheckedItems.Count > 0 Then
                                .Centers_NAME = cboCenter.CheckedItems.Select(Function(x) x.Text).Aggregate(Function(x, y) x & ", " & y)
                            End If
                            If lstPartDepts.Items.Count > 0 Then
                                .Departments_NAME = lstPartDepts.Items.Select(Function(x) x.Text).Aggregate(Function(x, y) x & ", " & y)
                            End If
                            If lstPositions.Items.Count > 0 Then
                                .Titles_NAME = lstPositions.Items.Select(Function(x) x.Text).Aggregate(Function(x, y) x & ", " & y)
                            End If
                            'If lstWork.Items.Count > 0 Then
                            '    .Work_inv_NAME = lstWork.Items.Select(Function(x) x.Text).Aggregate(Function(x, y) x & ", " & y)
                            'End If
                            .ATTACHFILE = lblFilename.Text
                            .Plan_Emp = lstEmployee
                            If cboStatus.SelectedValue <> "" Then
                                .STATUS_ID = cboStatus.SelectedValue
                            End If
                            .IS_EVALUATE = chkEvaluate.Checked
                            If IsNumeric(rnPrioty.Value) Then
                                If rnPrioty.Value = 1 Or rnPrioty.Value = 2 Or rnPrioty.Value = 3 Or rnPrioty.Value = 4 Or rnPrioty.Value = 5 Then
                                    .LEVEL_PRIOTY = rnPrioty.Value
                                Else
                                    ShowMessage(Translate("Xin kiểm tra lại mức độ ưu tiên"), NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If
                            .WORK_RELATION = txtWork_Relation.Text
                            If cboGrProgram.SelectedValue <> "" Then
                                .GR_PROGRAM_ID = cboGrProgram.SelectedValue
                            End If

                        End With

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertPlan(obj, gID) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Training&fid=ctrlTR_Plan&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                obj.ID = hidID.Value
                                If rep.ModifyPlan(obj, gID) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Training&fid=ctrlTR_Plan&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select

                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Training&fid=ctrlTR_Plan&group=Business")
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
        UpdateControlState()
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

    'Protected Sub btnAddDepts_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddDepts.Click
    '    Try
    '        isLoadPopup = 1
    '        UpdateControlState()
    '        ctrlFindOrgPopup.Show()
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try

    'End Sub

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
                Case 1
                    Dim lstIds = e.SelectedValues
                    If lstIds IsNot Nothing AndAlso lstIds.Count > 0 AndAlso e.SelectedTexts.Count = e.SelectedValues.Count Then
                        Me.ParticipcatedDetpIds = e.SelectedValues
                        Me.ParticipcatedDetpNames = e.SelectedTexts
                        For i = 0 To e.SelectedValues.Count - 1
                            lstPartDepts.Items.Add(New RadListBoxItem(ParticipcatedDetpNames(i), ParticipcatedDetpIds(i)))
                        Next
                        PopulatingListTitle()
                    End If
            End Select
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindOrgPopup.CancelClicked, ctrlFindMEmployeePopup.CancelClicked
        isLoadPopup = 0

    End Sub

    'Protected Sub btnRemoveDepts_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemoveDepts.Click
    '    For Each item In lstPartDepts.SelectedItems
    '        lstPartDepts.Items.Remove(item)
    '        PopulatingListTitle()
    '    Next
    'End Sub

    'Private Sub cboCourse_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCourse.SelectedIndexChanged
    '    If cboCourse.SelectedValue <> "" Then
    '        Using rep As New TrainingRepository
    '            Try
    '                Dim course = rep.GetEntryAndFormByCourseID(cboCourse.SelectedValue, Common.Common.SystemLanguage.Name)
    '                'txtHinhThuc.Text = course.TR_TRAIN_FORM_NAME
    '                'txtKhoanMuc.Text = course.TR_TRAIN_ENTRIES_NAME
    '                txtNhomCT.Text = course.TR_PROGRAM_GROUP_NAME
    '                txtLinhvuc.Text = course.TR_TRAIN_FIELD_NAME
    '            Catch ex As Exception
    '                DisplayException(Me.ViewName, Me.ID, ex)
    '            End Try
    '        End Using
    '    End If
    'End Sub

#End Region

#Region "Custom"

    Public Overrides Sub UpdateControlState()

        Try
            If pgFindMultiEmp.Controls.Contains(ctrlFindMEmployeePopup) Then
                pgFindMultiEmp.Controls.Remove(ctrlFindMEmployeePopup)
            End If
            If isLoadPopup = 1 OrElse isLoadPopup = 2 Then
                ctrlFindOrgPopup = Me.Register("ctrlfindorgpopup", "common", "ctrlfindorgpopup")
                ctrlFindOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                Select Case isLoadPopup
                    Case 2 'Chọn phòng ban tổ chức
                    Case 1 'Chọn phòng ban tham gia
                        ctrlFindOrgPopup.ShowCheckBoxes = TreeNodeTypes.All
                        ctrlFindOrgPopup.CheckChildNodes = True
                End Select
                phFindOrg.Controls.Add(ctrlFindOrgPopup)
                ctrlFindOrgPopup.Show()
            ElseIf isLoadPopup = 3 Then
                ctrlFindMEmployeePopup = Me.Register("ctrlFindMEmployeePopup", "Common", "ctrlFindMEmployeePopup")
                ctrlFindMEmployeePopup.MustHaveContract = True
                ctrlFindMEmployeePopup.IS_3B = 2
                pgFindMultiEmp.Controls.Add(ctrlFindMEmployeePopup)
                ctrlFindMEmployeePopup.MultiSelect = True
            End If
            'If pgFindMultiEmp.Controls.Contains(ctrlFindMEmployeePopup) Then
            '    pgFindMultiEmp.Controls.Remove(ctrlFindMEmployeePopup)
            'End If
            'If isLoadPopup = 1 OrElse isLoadPopup = 2 Then
            '    ctrlFindOrgPopup = Me.Register("ctrlfindorgpopup", "common", "ctrlfindorgpopup")
            '    ctrlFindOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
            '    Select Case isLoadPopup
            '        Case 2 'Chọn phòng ban tổ chức
            '        Case 1 'Chọn phòng ban tham gia
            '            ctrlFindOrgPopup.ShowCheckBoxes = TreeNodeTypes.All
            '            ctrlFindOrgPopup.CheckChildNodes = True
            '    End Select
            '    phFindOrg.Controls.Add(ctrlFindOrgPopup)
            '    ctrlFindOrgPopup.Show()
            'End If
        Catch ex As Exception
            Throw ex
        End Try
        ChangeToolbarState()
    End Sub

    Private Sub GetDataCombo()
        Try
            Dim dtData As DataTable
            Using rep As New TrainingRepository

                dtData = tsp.ProgramGroupGetList()
                dtData = (From P In dtData Where P("ACTFLG_ID") = -1).CopyToDataTable
                FillRadCombobox(cboGrProgram, dtData, "Name", "ID")
                dtData = rep.GetOtherList("TR_DURATION_UNIT", True)
                FillRadCombobox(cboDurationType, dtData, "NAME", "ID")
                cboDurationType.SelectedValue = 4005
                dtData = rep.GetOtherList("TR_REQUEST_STATUS", True)
                FillRadCombobox(cboStatus, dtData, "NAME", "ID")
                cboStatus.SelectedValue = 4000
                dtData = rep.GetOtherList("TR_CURRENCY", True)
                ' FillRadCombobox(cboCurrency, dtData, "NAME", "ID")
                'Dim lst As List(Of CourseDTO)
                'lst = rep.GetCourseList()
                'FillRadCombobox(cboCourse, lst, "NAME", "ID")
                repHF = New HistaffFrameworkRepository
                dtData = repHF.ExecuteToDataSet("PKG_TRAINING.HINH_THUC_DAO_TAO", New List(Of Object)({})).Tables(0)
                FillRadCombobox(cboHinhThuc, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("TR_PROPERTIES_NEED", True)
                FillRadCombobox(cboTinhchatnhucau, dtData, "NAME", "ID")
                dtData = repHF.ExecuteToDataSet("PKG_TRAINING.READ_PLAN_UNIT", New List(Of Object)({})).Tables(0)
                FillRadCombobox(cboUnit, dtData, "NAME", "ID")
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

    Private Sub CostCalculating()
        'Dim trainingCost As Decimal = 0
        'Dim InccurredCost As Decimal = 0
        'Dim TravelCost As Decimal = 0
        'Dim OtherCost As Decimal = 0
        'Dim TotalCost As Decimal = 0
        'Dim EmpQuantity As Integer = 0

        ''If rntxtTrainingCost.Value IsNot Nothing Then Decimal.TryParse(rntxtTrainingCost.Value, trainingCost)
        ''If rntxtInccurredCost.Value IsNot Nothing Then Decimal.TryParse(rntxtInccurredCost.Value, InccurredCost)
        ''If rntxtTravleCost.Value IsNot Nothing Then Decimal.TryParse(rntxtTravleCost.Value, TravelCost)
        ''If rntxtOtherCost.Value IsNot Nothing Then Decimal.TryParse(rntxtOtherCost.Value, OtherCost)
        'If rntxtStudents.Value IsNot Nothing Then Integer.TryParse(rntxtStudents.Value, EmpQuantity)

        'TotalCost = trainingCost + InccurredCost + TravelCost + OtherCost
        'rntxtTotal.Value = TotalCost
        'If (EmpQuantity <> 0) Then rntxtCostPerEmp.Value = TotalCost / EmpQuantity
    End Sub
    Private Sub getListTitle()
        Dim lstOrgIds = (From item In lstPartDepts.Items Select (Decimal.Parse(item.Value))).ToList()

        Using rep As New TrainingBusinessClient
            lstPositions.Items.Clear()
            Dim titles = rep.GetTitlesByOrgs(lstOrgIds, Common.Common.SystemLanguage.Name)
            For Each item In titles
                lstPositions.Items.Add(New RadListBoxItem(item.NAME, item.ID))
            Next
        End Using
    End Sub
    Private Sub PopulatingListTitle()
        Dim lstOrgIds = (From item In lstPartDepts.Items Select (Decimal.Parse(item.Value))).ToList()

        Using rep As New TrainingBusinessClient
            lstPositions.Items.Clear()
            Dim titles = rep.GetTitlesByOrgs(lstOrgIds, Common.Common.SystemLanguage.Name)
            For Each item In titles
                lstPositions.Items.Add(New RadListBoxItem(item.NAME, item.ID))
            Next
        End Using
    End Sub
    Private Sub PopulatingListWI()
        Dim lsttitle = (From item In lstPositions.Items Select (Decimal.Parse(item.Value))).ToList()
        Using rep As New TrainingBusinessClient
            '  lstWork.Items.Clear()
            Dim titles = rep.GetWIByTitle(lsttitle, Common.Common.SystemLanguage.Name)
            For Each item In titles
                '    lstWork.Items.Add(New RadListBoxItem(item.NAME, item.ID))
            Next
        End Using
    End Sub
#End Region
    Private Sub rgISP_ItemDataBound1(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgISP.ItemDataBound
        Dim dt2 As New DataTable
        dt2.Columns.Add("ID", GetType(Integer))
        dt2.Columns.Add("NAME", GetType(String))
        dt2.Rows.Add(1, "VNĐ")
        dt2.Rows.Add(2, "USD")
        'For Each dr As GridDataItem In e.Item.DataItem
        '    Dim raComb = CType(rgISP.FindControl("RadMoneyU"), RadComboBox)
        '    FillRadCombobox(raComb, dt2, "NAME", "ID")
        'Next

        If (TypeOf e.Item Is GridDataItem) Then
            Dim editItem As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim combo As RadComboBox = DirectCast(editItem.FindControl("RadMoneyU"), RadComboBox)
            FillRadCombobox(combo, dt2, "NAME", "ID")
            combo.SelectedValue = 1
        End If
        'Next
        If (TypeOf e.Item Is GridDataItem) Then
            If lstcostDT IsNot Nothing Then
                For Each item As CostDetailDTO In lstcostDT
                    Dim row As GridDataItem = DirectCast(e.Item, GridDataItem)
                    If Decimal.Parse(row("CODE").Text) = item.TYPE_ID Then
                        DirectCast(row("MONEY_TEMP").FindControl("MONEY"), RadNumericTextBox).Text = item.MONEY
                        DirectCast(row("MONEY_U_TEMP").FindControl("RadMoneyU"), Telerik.Web.UI.RadComboBox).SelectedValue = item.MONEY_TYPE
                    End If
                Next
            End If
        End If

    End Sub
    Private Sub rgISP_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgISP.NeedDataSource
        Dim dt As New DataTable
        dt = repHF.ExecuteToDataSet("PKG_TRAINING.CHI_PHI", New List(Of Object)({})).Tables(0)

        'dt.Columns.Add("CODE", GetType(String))
        'dt.Columns.Add("NAME_VN", GetType(String))
        'dt.Columns.Add("MONEY", GetType(Integer))
        'dt.Rows.Add("1", "Mục 1", 0)
        'dt.Rows.Add("2", "Mục 2", 0)
        rgISP.VirtualItemCount = dt.Rows.Count
        rgISP.DataSource = dt
        'Dim dt2 As New DataTable
        'dt2.Columns.Add("ID", GetType(Integer))
        'dt2.Columns.Add("NAME", GetType(String))
        'dt2.Rows.Add(1, "VNĐ")
        'dt2.Rows.Add(2, "USD")

        'For Each dr As GridDataItem In rgISP.Items
        '    Dim raComb = CType(rgISP.FindControl("RadMoneyU"), RadComboBox)
        '    FillRadCombobox(raComb, dt2, "NAME", "ID")


    End Sub
    Private Sub ctrlFindMEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindMEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim lstOrg As New List(Of lstOrgDTO)
        Dim lstTitle As New List(Of lstOrgDTO)
        lstEmployee.Clear()
        lstPartDepts.Items.Clear()
        lstPositions.Items.Clear()
        Try
            lstCommonEmployee = CType(ctrlFindMEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            For Each item As CommonBusiness.EmployeePopupFindDTO In lstCommonEmployee
                ' If Not (From p In lstEmployee Where p.EMPLOYEE_ID = item.ID).Any Then
                Dim objEmp As New RequestEmpDTO
                objEmp.EMPLOYEE_ID = item.ID
                objEmp.EMPLOYEE_CODE = item.EMPLOYEE_CODE
                objEmp.EMPLOYEE_NAME = item.FULLNAME_VN
                'objEmp.BIRTH_DATE = item.BIRTH_DATE
                objEmp.GENDER_NAME = item.GENDER
                objEmp.TITLE_NAME = item.TITLE_NAME

                objEmp.ORG_ID = item.ORG_ID
                lstEmployee.Add(objEmp)
                lstOrg.Add(New lstOrgDTO With {.ORG_ID = item.ORG_ID, .ORG_NAME = item.ORG_NAME})
                lstTitle.Add(New lstOrgDTO With {.ORG_ID = item.TITLE_ID, .ORG_NAME = item.TITLE_NAME})
                ' End If
            Next
            lstOrg.Distinct()
            For Each item In lstOrg
                'If lstPartDepts Is Nothing Or lstPartDepts.Items.Count <= 0 Then
                '    lstPartDepts.Items.Add(New RadListBoxItem(item.ORG_NAME, item.ORG_ID))
                'Else
                Dim var = lstPartDepts.Items.Where(Function(f) f.Value = item.ORG_ID.ToString).ToList
                If var.Count <= 0 Or lstPartDepts.Items.Count = 0 Then
                    lstPartDepts.Items.Add(New RadListBoxItem(item.ORG_NAME, item.ORG_ID))
                End If
                ' End If
            Next
            lstTitle.Distinct()
            For Each item In lstTitle
                Dim var = lstPositions.Items.Where(Function(f) f.Value = item.ORG_ID.ToString).ToList
                If var.Count <= 0 Or lstPositions.Items.Count = 0 Then
                    lstPositions.Items.Add(New RadListBoxItem(item.ORG_NAME, item.ORG_ID))
                End If
            Next
            rntxtStudents.Value = lstEmployee.Count
            'PopulatingListTitle()
            '   PopulatingListWI()
            'Dim lstIds = e.
            'If lstIds IsNot Nothing AndAlso lstIds.Count > 0 AndAlso e.SelectedTexts.Count = e.SelectedValues.Count Then
            'Me.ParticipcatedDetpIds = e.SelectedValues
            'Me.ParticipcatedDetpNames = e.SelectedTexts
            'For i = 0 To e.SelectedValues.Count - 1
            '    lstPartDepts.Items.Add(New RadListBoxItem(ParticipcatedDetpNames(i), ParticipcatedDetpIds(i)))
            'Next
            'PopulatingListTitle()
            'End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub btnFindMEmp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindMEmp.Click
        Try
            isLoadPopup = 3
            UpdateControlState()
            ctrlFindMEmployeePopup.Show()
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
                    fileName = Server.MapPath("~/ReportTemplates/Training/Upload")
                    If Not Directory.Exists(fileName) Then
                        Directory.CreateDirectory(fileName)
                    End If
                    fileName = System.IO.Path.Combine(fileName, file.FileName)
                    file.SaveAs(fileName, True)
                    lblFilename.Text = file.FileName
                    lblFilename.NavigateUrl = "http://" & Request.Url.Host & ":" & Request.Url.Port & "/ReportTemplates/Training/Upload/" + file.FileName

                Else
                    ShowMessage(Translate("Chưa upload được file"), NotifyType.Error)
                End If

            End If
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi"), NotifyType.Error)
        End Try
    End Sub

    Private Sub cusCourse_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusCourse.ServerValidate
        Try
            If cboCourse.SelectedValue = "" Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cusHinhThuc_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusHinhThuc.ServerValidate
        Try
            If cboHinhThuc.SelectedValue = "" Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cusTinhchatnhucau_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusTinhchatnhucau.ServerValidate
        Try
            If cboTinhchatnhucau.SelectedValue = "" Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cboGrProgram_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboGrProgram.SelectedIndexChanged
        Dim dtData As New DataTable
        Try
            If cboGrProgram.SelectedValue <> "" Then
                Using rep As New TrainingRepository
                    Dim lst As List(Of CourseDTO)
                    lst = rep.GetCourseList()
                    lst = (From p In lst Where p.TR_PROGRAM_GROUP_ID = cboGrProgram.SelectedValue
                           Select New CourseDTO() With {.ID = p.ID,
                                                       .NAME = p.NAME,
                                                       .TR_PROGRAM_GROUP_ID = p.TR_PROGRAM_GROUP_ID}).ToList()
                    FillRadCombobox(cboCourse, lst, "NAME", "ID")
                End Using
            Else
                ClearControlValue(cboCourse)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub AddDataToCboCenter()
        Dim rep As New TrainingRepository()
        Try
            Dim cboCenterList = rep.GetCenters()
            For Each i In cboCenterList
                Dim item As New RadComboBoxItem()
                If (Common.Common.SystemLanguage.Name = "vi-VN") Then
                    item.Text = i.NAME_VN
                    item.Value = i.ID
                    cboCenter.Items.Add(item)
                Else
                    item.Text = i.NAME_EN
                    item.Value = i.ID
                    cboCenter.Items.Add(item)
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
End Class
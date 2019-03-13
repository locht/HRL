Imports Framework.UI
Imports Framework.UI.Utilities
Imports Training.TrainingBusiness
Imports Common
Imports Telerik.Web.UI

Public Class ctrlTR_Reimbursement
    Inherits Common.CommonView

    Protected WithEvents ctrlFindReimbursementPopup As ctrlFindEmployeePopup
#Region "Property"

    Public Property Reimbursements As List(Of ReimbursementDTO)
        Get
            Return ViewState(Me.ID & "_Reimbursements")
        End Get
        Set(ByVal value As List(Of ReimbursementDTO))
            ViewState(Me.ID & "_Reimbursements") = value
        End Set
    End Property

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    '0 - normal
    '1 - Reimbursement
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
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        SetGridFilter(rgMain)
        rgMain.AllowCustomPaging = True
        rgMain.PageSize = Common.Common.DefaultPageSize
        rgMain.ClientSettings.EnablePostBackOnRowClick = True
        InitControl()
    End Sub
    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Seperator,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(0), RadToolBarButton).Text = "Thêm nhân viên bồi hoàn"
            CType(Me.MainToolBar.Items(1), RadToolBarButton).Text = "Cập nhật thông tin bồi hoàn"
            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            'CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New TrainingRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()

                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL

                    Case "Cancel"
                        rgMain.MasterTableView.ClearSelectedItems()
                End Select
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub UpdateControlState()
        Dim rep As New TrainingRepository
        Try
            If phFindReimbursement.Controls.Contains(ctrlFindReimbursementPopup) Then
                phFindReimbursement.Controls.Remove(ctrlFindReimbursementPopup)
            End If
            Select Case isLoadPopup
                Case 1
                    ctrlFindReimbursementPopup = Me.Register("ctrlFindReimbursementPopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindReimbursementPopup.MustHaveContract = False
                    phFindReimbursement.Controls.Add(ctrlFindReimbursementPopup)
                    ctrlFindReimbursementPopup.MultiSelect = False
            End Select

            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    UpdateCtrlState(True)

                Case CommonMessage.STATE_NORMAL
                    If rgMain.SelectedValue IsNot Nothing Then
                        Dim item = (From p In Reimbursements Where p.ID = rgMain.SelectedValue).SingleOrDefault
                        rntxtYear.Value = item.YEAR
                        cboCourse.SelectedValue = item.TR_PROGRAM_ID
                        rntxtCostOfStudent.Value = item.COST_OF_STUDENT
                        rdStartDate.SelectedDate = item.START_DATE
                        hidEmployeeID.Value = item.EMPLOYEE_ID
                        txtCode.Text = item.EMPLOYEE_CODE
                        txtName.Text = item.EMPLOYEE_NAME
                    End If
                    UpdateCtrlState(False)
                    rntxtSeachYear.Value = Date.Now.Year

                Case CommonMessage.STATE_EDIT
                    UpdateCtrlState(True)
            End Select
            txtCode.Focus()
            UpdateToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Private Sub UpdateCtrlState(ByVal state As Boolean)
        Try
            rntxtSeachYear.ReadOnly = state
            cboSearchCourse.Enabled = Not state
            txtSeachEmployee.ReadOnly = state
            btnSearch.Enabled = Not state

            rntxtYear.ReadOnly = Not state
            rntxtYear.AutoPostBack = state
            Utilities.EnableRadCombo(cboCourse, state)
            cboCourse.AutoPostBack = state
            cbReserves.ReadOnly = Not state
            btnFindReimbursement.Enabled = True
            EnableRadDatePicker(rdStartDate, state)
            
            EnabledGridNotPostback(rgMain, Not state)
            'EnabledGrid(rgMain, Not state, False)
            rgMain.AllowMultiRowSelection = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ClearCtrlValue()
        Try
            rntxtYear.Value = Date.Now.Year
            cboCourse.ClearSelection()
            cboCourse.Text = ""
            rntxtCostOfStudent.Text = ""
            cbReserves.Checked = False
            rdStartDate.SelectedDate = Nothing
            txtCode.Text = ""
            txtName.Text = ""
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            rntxtSeachYear.Value = Date.Now.Year
            GetPlanInYearOrg2()

            rntxtYear.Value = Date.Now.Year
            GetPlanInYearOrg()

            Dim hidBind As HiddenField = New HiddenField
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("ID", hidBind)

            'Dim dic As New Dictionary(Of String, Control)
            'dic.Add("YEAR", rntxtYear)
            'dic.Add("TR_PROGRAM_ID;TR_PROGRAM_NAME", cboCourse)
            'dic.Add("COST_OF_STUDENT", rntxtCostOfStudent)
            'dic.Add("IS_RESERVES", cbReserves)
            'dic.Add("START_DATE", rdStartDate)
            'dic.Add("EMPLOYEE_ID", hidEmployeeID)
            'dic.Add("EMPLOYEE_CODE", txtCode)
            'dic.Add("EMPLOYEE_NAME", txtName)
            Utilities.OnClientRowSelectedChanged(rgMain, dic)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objReimbursement As New ReimbursementDTO
        Dim rep As New TrainingRepository
        Dim gID As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearCtrlValue()
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rgMain.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    For Each item As GridDataItem In rgMain.SelectedItems
                        If item.GetDataKeyValue("TO_DATE") Is Nothing Then
                            ShowMessage(Translate("Chương trình của nhân viên này không có ngày kết thúc.<br />Bạn vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        If item.GetDataKeyValue("COMMIT_WORK") Is Nothing Then
                            ShowMessage(Translate("Nhân viên này chưa được cam kết đào tạo.<br />Bạn vui lòng kiểm tra lại."), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    CurrentState = CommonMessage.STATE_EDIT
                    'rntxtYear_TextChanged(Nothing, Nothing)
                    'cboCourse.ClearSelection()
                    'cboCourse.Items.Clear()

                    'Dim item As GridDataItem = rgMain.SelectedItems(0)
                    'If item.GetDataKeyValue("TR_PROGRAM_ID") IsNot Nothing Then
                    '    cboCourse.SelectedValue = item.GetDataKeyValue("TR_PROGRAM_ID")
                    'End If
                    'hidEmployeeID.Value = item.GetDataKeyValue("EMPLOYEE_ID")
                    'txtCode.Text = item.GetDataKeyValue("EMPLOYEE_CODE")
                    'txtName.Text = item.GetDataKeyValue("EMPLOYEE_NAME")
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    GridExportExcel(rgMain, "Reimbursement")

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim CW As Decimal = -1
                        Dim TD As Date = Today
                        For Each item As GridDataItem In rgMain.SelectedItems
                            If item.GetDataKeyValue("TO_DATE") IsNot Nothing Then
                                TD = CDate(item.GetDataKeyValue("TO_DATE"))
                            End If
                            If item.GetDataKeyValue("COMMIT_WORK") IsNot Nothing Then
                                CW = CDec(item.GetDataKeyValue("COMMIT_WORK"))
                            End If
                        Next

                        With objReimbursement
                            .YEAR = rntxtYear.Value
                            .TR_PROGRAM_ID = cboCourse.SelectedValue
                            .COST_OF_STUDENT = rntxtCostOfStudent.Value
                            .IS_RESERVES = cbReserves.Checked
                            .START_DATE = rdStartDate.SelectedDate
                            .EMPLOYEE_ID = hidEmployeeID.Value
                            .COMMIT_WORK = CW
                            .TO_DATE = TD
                        End With

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertReimbursement(objReimbursement, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objReimbursement.ID = rgMain.SelectedValue
                                If rep.ModifyReimbursement(objReimbursement, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objReimbursement.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        'ExcuteScript("Resize", "ResizeSplitter()")
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub CreateDataFilter()
        Dim rep As New TrainingRepository
        Dim obj As New ReimbursementDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, obj)

            obj.YEAR = rntxtSeachYear.Value
            If cboSearchCourse.SelectedValue <> "" Then
                obj.TR_PROGRAM_ID = cboSearchCourse.SelectedValue
            End If
            obj.EMPLOYEE_CODE = txtSeachEmployee.Text

            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                Me.Reimbursements = rep.GetReimbursement(obj, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
            Else
                Me.Reimbursements = rep.GetReimbursement(obj, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
            End If

            rgMain.VirtualItemCount = MaximumRows
            rgMain.DataSource = Me.Reimbursements
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub rgData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgMain.SelectedIndexChanged
        Try
            If rgMain.SelectedItems.Count = 0 Then Exit Sub

            Dim item As GridDataItem = CType(rgMain.SelectedItems(0), GridDataItem)
            If item.GetDataKeyValue("YEAR") IsNot Nothing Then
                rntxtYear.Value = CDec(item.GetDataKeyValue("YEAR"))
            Else
                rntxtYear.Value = Nothing
            End If
            GetPlanInYearOrg()

            If item.GetDataKeyValue("TR_PROGRAM_ID") IsNot Nothing Then
                cboCourse.SelectedValue = CDec(item.GetDataKeyValue("TR_PROGRAM_ID"))
            Else
                cboCourse.SelectedValue = Nothing
                cboCourse.ClearSelection()
                cboCourse.Items.Clear()
            End If

            If item.GetDataKeyValue("COST_OF_STUDENT") IsNot Nothing Then
                rntxtCostOfStudent.Value = CDec(item.GetDataKeyValue("COST_OF_STUDENT"))
            Else
                rntxtCostOfStudent.ClearValue()
            End If

            If item.GetDataKeyValue("IS_RESERVES") IsNot Nothing Then
                cbReserves.Checked = CBool(item.GetDataKeyValue("IS_RESERVES"))
            Else
                cbReserves.Checked = False
            End If

            If item.GetDataKeyValue("START_DATE") IsNot Nothing Then
                rdStartDate.SelectedDate = CDate(item.GetDataKeyValue("START_DATE"))
            Else
                rdStartDate.SelectedDate = Nothing
            End If

            If item.GetDataKeyValue("EMPLOYEE_ID") IsNot Nothing Then
                hidEmployeeID.Value = CDec(item.GetDataKeyValue("EMPLOYEE_ID"))
            Else
                hidEmployeeID.Value = Nothing
            End If

            If item.GetDataKeyValue("EMPLOYEE_CODE") IsNot Nothing Then
                txtCode.Text = item.GetDataKeyValue("EMPLOYEE_CODE").ToString
            Else
                txtCode.Text = ""
            End If

            If item.GetDataKeyValue("EMPLOYEE_NAME") IsNot Nothing Then
                txtName.Text = item.GetDataKeyValue("EMPLOYEE_NAME").ToString
            Else
                txtName.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub btnFindReimbursement_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindReimbursement.Click
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindReimbursementPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlFindReimbursementPopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindReimbursementPopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Try
            lstCommonEmployee = CType(ctrlFindReimbursementPopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            Dim itm = lstCommonEmployee(0)
            hidEmployeeID.Value = itm.EMPLOYEE_ID
            txtCode.Text = itm.EMPLOYEE_CODE
            txtName.Text = itm.FULLNAME_VN
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New TrainingRepository
        Dim _validate As New ReimbursementDTO
        Try
            If cboCourse.SelectedValue <> "" Then
                _validate.TR_PROGRAM_ID = cboCourse.SelectedValue
                If CurrentState = CommonMessage.STATE_EDIT Then
                    _validate.ID = rgMain.SelectedValue
                    _validate.EMPLOYEE_ID = hidEmployeeID.Value
                    args.IsValid = rep.ValidateReimbursement(_validate)
                Else
                    _validate.EMPLOYEE_ID = hidEmployeeID.Value
                    args.IsValid = rep.ValidateReimbursement(_validate)
                End If

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rntxtYear_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rntxtYear.TextChanged
        Try
            GetPlanInYearOrg()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub GetPlanInYearOrg()
        Try
            If rntxtYear.Value IsNot Nothing Then
                Using rep As New TrainingRepository
                    Dim dtData = rep.GetTrProgramByYear(True, rntxtYear.Value)
                    FillRadCombobox(cboCourse, dtData, "NAME", "ID")
                End Using
            Else
                cboCourse.ClearSelection()
                cboCourse.Items.Clear()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cboCourse_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCourse.SelectedIndexChanged
        Try
            If cboCourse.SelectedValue <> "" Then
                Using rep As New TrainingRepository
                    Dim obj = rep.GetProgramById(cboCourse.SelectedValue)
                    rntxtCostOfStudent.Value = obj.COST_STUDENT
                End Using
            Else
                rntxtCostOfStudent.Value = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rntxtSeachYear_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rntxtSeachYear.TextChanged
        Try
            GetPlanInYearOrg2()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub GetPlanInYearOrg2()
        Try
            If rntxtSeachYear.Value IsNot Nothing Then
                Using rep As New TrainingRepository
                    Dim dtData = rep.GetTrProgramByYear(True, rntxtSeachYear.Value)
                    FillRadCombobox(cboSearchCourse, dtData, "NAME", "ID")
                End Using
            Else
                cboSearchCourse.ClearSelection()
                cboSearchCourse.Items.Clear()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Custom"
#End Region
End Class
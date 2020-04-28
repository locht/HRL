Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Common
Imports Telerik.Web.UI

Public Class ctrlAllowance
    Inherits Common.CommonView
    Protected WithEvents TerminateView As ViewBase
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
#Region "Property"
    Dim dtData As New DataTable
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
    Property vData As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataView")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataView") = value
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

    'Public Property Allowance As List(Of AllowanceDTO)
    '    Get
    '        Return ViewState(Me.ID & "_Allowance")
    '    End Get
    '    Set(ByVal value As List(Of AllowanceDTO))
    '        ViewState(Me.ID & "_Allowance") = value
    '    End Set
    'End Property
    Property DeleteAllowance As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_DeleteAllowance")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_DeleteAllowance") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
            ctrlOrg.AutoPostBack = True
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrg.CheckBoxes = TreeNodeTypes.None
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgData.AllowCustomPaging = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            ctrlUpload.isMultiple = AsyncUpload.MultipleFileSelection.Disabled
            ctrlUpload.MaxFileInput = 1
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub GetDataCombobox()
        Dim rep As New PayrollRepository
        Try
            Dim objdata As List(Of AllowanceListDTO)
            Dim obj As New AllowanceListDTO
            obj.IS_CONTRACT = 1
            obj.ACTFLG = "A"
            objdata = rep.GetAllowanceList(obj)
            rep.Dispose()

            FillDropDownList(cboPhucap, objdata, "NAME", "ID", Common.Common.SystemLanguage, True)
            FillDropDownList(cbPhucap, objdata, "NAME", "ID", Common.Common.SystemLanguage, True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            GetDataCombobox()
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("EMPLOYEE_CODE", txtCode)
            dic.Add("FULLNAME_VN", txtTennhanvien)
            dic.Add("TITLE_NAME", txtTitle)
            dic.Add("ORG_NAME", txtOrgName)
            dic.Add("ALLOWANCE_TYPE", cboPhucap)
            dic.Add("AMOUNT", txtSotien)
            dic.Add("EFFECT_DATE", dpTungay)
            dic.Add("EXP_DATE", dpDenngay)
            dic.Add("REMARK", txtGhichu)
            dic.Add("EMPLOYEE_ID", hidEmp)
            Utilities.OnClientRowSelectedChanged(rgData, dic)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New PayrollRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        SelectedItemDataGridByKey(rgData, IDSelect, , rgData.CurrentPageIndex)

                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                        SelectedItemDataGridByKey(rgData, IDSelect, )
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If
            rep.Dispose()
            UpdateControlState()
            ChangeToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Export, ToolbarItem.Next, ToolbarItem.Import,
                                       ToolbarItem.Delete)
            CType(Me.MainToolBar.Items(5), RadToolBarButton).Text = Translate("Xuất file mẫu")
            CType(Me.MainToolBar.Items(5), RadToolBarButton).ImageUrl = CType(Me.MainToolBar.Items(4), RadToolBarButton).ImageUrl
            CType(Me.MainToolBar.Items(6), RadToolBarButton).Text = Translate("Nhập file mẫu")
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"


    Protected Sub btnFindCommon_Click(ByVal sender As Object,
                                   ByVal e As EventArgs) Handles _
                               btnFindEmployee.Click
        Try
            UpdateControlState()
            ctrlFindEmployeePopup.Show()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New PayrollRepository
        Try
            If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                ctrlFindEmployeePopup.MultiSelect = False
                ctrlFindEmployeePopup.MustHaveContract = False
            End If
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgData, True)
                    EnableControlAll(False, cboPhucap, txtSotien, txtTennhanvien, txtGhichu, dpTungay, dpDenngay, btnFindEmployee, txtCode, txtTitle, txtOrgName)
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgData, False)
                    EnableControlAll(True, cboPhucap, txtSotien, txtGhichu, dpTungay, dpDenngay, btnFindEmployee)
                    ClearControlValue(cboPhucap, txtEmployeeCode, txtSotien, txtTennhanvien, txtGhichu, dpTungay, dpDenngay, txtCode, txtTitle, txtOrgName)
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgData, False)
                    EnableControlAll(True, cboPhucap, txtEmployeeCode, txtSotien, txtTennhanvien, txtGhichu, dpTungay, dpDenngay, btnFindEmployee, txtCode, txtTitle, txtOrgName)
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAllowance(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAllowance(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_DELETE
                    If rep.DeleteAllowance(DeleteAllowance) Then
                        DeleteAllowance = Nothing
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdateControlState()
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                    End If
            End Select
            rep.Dispose()
            ChangeToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Try
            Dim empID = ctrlFindEmployeePopup.SelectedEmployee(0)
            txtTennhanvien.Text = empID.FULLNAME_VN
            hidEmp.Value = empID.EMPLOYEE_ID
            txtCode.Text = empID.EMPLOYEE_CODE
            txtTitle.Text = empID.TITLE_NAME
            txtOrgName.Text = empID.ORG_NAME
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub




    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            'Dim rep As New PayrollRepository
            'Dim objAllowanceLis As New AllowanceListDTO
            Dim objAllowance As New AllowanceDTO
            Dim gID As Decimal
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstID As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next

                    DeleteAllowance = lstID
                    'Using rep As New PayrollRepository
                    '    If Not rep.CheckExistInDatabase(lstID, TABLE_NAME.PA_SALARY_GROUP) Then
                    '        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                    '        Return
                    '    End If
                    'End Using

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "Allowance")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objAllowance.EMPLOYEE_ID = hidEmp.Value
                        objAllowance.ALLOWANCE_TYPE = cboPhucap.SelectedValue
                        objAllowance.AMOUNT = txtSotien.Text

                        If Not dpTungay.SelectedDate Is Nothing Then
                            objAllowance.EFFECT_DATE = dpTungay.SelectedDate
                        Else
                            objAllowance.EFFECT_DATE = Nothing
                        End If
                        If Not dpDenngay.SelectedDate Is Nothing Then
                            objAllowance.EXP_DATE = dpDenngay.SelectedDate
                        Else
                            objAllowance.EXP_DATE = Nothing
                        End If

                        objAllowance.REMARK = txtGhichu.Text

                        Using rep As New PayrollRepository
                            Select Case CurrentState
                                Case CommonMessage.STATE_NEW
                                    objAllowance.ACTFLG = "A"
                                    If rep.InsertAllowance(objAllowance, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = gID
                                        Refresh("InsertView")
                                        UpdateControlState()
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                                Case CommonMessage.STATE_EDIT
                                    objAllowance.ID = rgData.SelectedValue
                                    If rep.ModifyAllowance(objAllowance, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = objAllowance.ID
                                        Refresh("UpdateView")
                                        UpdateControlState()
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                            End Select
                        End Using
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If

                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    ClearControlValue(txtEmployeeCode, txtGhichu, txtSotien, cboPhucap, txtCode, txtTitle, txtOrgName, txtTennhanvien, dpTungay, dpDenngay)
                    UpdateControlState()
            End Select
            'UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_ACTIVE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
                UpdateControlState()
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Try

            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub rgData_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub



#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New PayrollRepository
        Dim _filter As New AllowanceDTO
        Dim objData As New List(Of AllowanceDTO)
        Try
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                               .IS_DISSOLVE = ctrlOrg.IsDissolve}
            _filter.IS_TER = chkNhanvienghiviec.Checked
            SetValueObjectByRadGrid(rgData, _filter)
            If rdTuNgay.SelectedDate IsNot Nothing Then
                _filter.EFFECT_DATE = rdTuNgay.SelectedDate
            End If

            If rdDenNgay.SelectedDate IsNot Nothing Then
                _filter.EXP_DATE = rdDenNgay.SelectedDate
            End If

            If cbPhucap.SelectedValue <> "" Then
                _filter.ALLOWANCE_TYPE = cbPhucap.SelectedValue
            End If
            If txtEmployeeCode.Text <> "" Then
                If Regex.IsMatch(txtEmployeeCode.Text, "[a-z]") Or Regex.IsMatch(txtEmployeeCode.Text, "[A-Z]") Then
                    _filter.FULLNAME_VN = txtEmployeeCode.Text
                Else
                    _filter.EMPLOYEE_CODE = txtEmployeeCode.Text
                End If
            End If
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetAllowance(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetAllowance(_filter, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    objData = rep.GetAllowance(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, "CREATED_DATE ASC")
                Else
                    objData = rep.GetAllowance(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
                End If
                'rgData.MasterTableView.FilterExpression = ""
                rgData.VirtualItemCount = MaximumRows
                rgData.DataSource = objData
            End If
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            CreateDataFilter()
            rgData.Rebind()
        Catch ex As Exception

        End Try
    End Sub
    
End Class
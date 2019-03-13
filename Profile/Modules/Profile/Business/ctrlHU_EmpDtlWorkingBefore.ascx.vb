Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports Common
Imports Common.Common
Imports Common.CommonMessage
Public Class ctrlHU_EmpDtlWorkingBefore
    Inherits CommonView
    Dim employeeCode As String
    Public Overrides Property MustAuthorize As Boolean = False
#Region "Properties"
    Property SelectedItem As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_SelectedItem")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItem") = value
        End Set
    End Property
    Property EmployeeInfo As EmployeeDTO
        Get
            Return PageViewState(Me.ID & "_EmployeeInfo")
        End Get
        Set(ByVal value As EmployeeDTO)
            PageViewState(Me.ID & "_EmployeeInfo") = value
        End Set
    End Property

    Property isLoad As Boolean
        Get
            Return ViewState(Me.ID & "_isLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isLoad") = value
        End Set
    End Property

    Property lstWorkingBefore As List(Of WorkingBeforeDTO)
        Get
            Return ViewState(Me.ID & "_lstWorkingBefore")
        End Get
        Set(ByVal value As List(Of WorkingBeforeDTO))
            ViewState(Me.ID & "_lstWorkingBefore") = value
        End Set
    End Property

    'Lưu lại đang ở View nào để load khi dùng nút Next và Previous để chuyển sang xem thông tin nhân viên khác.
    Public Property CurrentPlaceHolder As String
        Get
            Return PageViewState(Me.ID & "_CurrentPlaceHolder")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_CurrentPlaceHolder") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ctrlEmpBasicInfo.SetProperty("EmployeeInfo", EmployeeInfo)
            UpdateControlState()
            Refresh()
            Me.CurrentPlaceHolder = Me.ViewName
            ctrlEmpBasicInfo.SetProperty("CurrentPlaceHolder", Me.CurrentPlaceHolder)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgGrid.SetFilter()
            rgGrid.AllowCustomPaging = True
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Seperator, ToolbarItem.Edit,
                         ToolbarItem.Seperator, ToolbarItem.Save, ToolbarItem.Seperator,
                         ToolbarItem.Cancel, ToolbarItem.Seperator, ToolbarItem.Delete)

            CType(Me.MainToolBar.Items(4), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_CREATE
                    If EmployeeInfo.WORK_STATUS Is Nothing Or _
                        (EmployeeInfo.WORK_STATUS IsNot Nothing AndAlso _
                         (EmployeeInfo.WORK_STATUS <> ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Or _
                          (EmployeeInfo.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID And _
                           EmployeeInfo.TER_EFFECT_DATE > Date.Now.Date))) Then
                        CurrentState = CommonMessage.STATE_NEW
                        ResetControlValue()
                    Else
                        ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                        Exit Sub
                    End If
                Case TOOLBARITEM_EDIT
                    ' gọi lại hàm này để lấy những row được select sau khi sửa xong và click sửa thêm 1 lần nữa 
                    rgGrid_SelectedIndexChanged(Nothing, Nothing)
                    If SelectedItem Is Nothing Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If SelectedItem.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If SelectedItem.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If EmployeeInfo.WORK_STATUS Is Nothing Or _
                        (EmployeeInfo.WORK_STATUS IsNot Nothing AndAlso _
                         (EmployeeInfo.WORK_STATUS <> ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Or _
                          (EmployeeInfo.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID And _
                           EmployeeInfo.TER_EFFECT_DATE > Date.Now.Date))) Then
                        CurrentState = CommonMessage.STATE_EDIT
                    Else
                        ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                        Exit Sub
                    End If
                Case TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Select Case CurrentState
                            Case STATE_NEW
                                If Execute() Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    UpdateControlState()
                                    rgGrid.Rebind()
                                    ClearControlValue(txtCompanyName, txtCompanyAddress, txtTelephone, rdJoinDate, rdEndDate,
                                                      txtSalary, txtTitleName, txtLevelName, txtTerReason)
                                    ExcuteScript("Clear", "clRadDatePicker()")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case STATE_EDIT
                                If Not String.IsNullOrEmpty(hidWorkingID.Value) Then
                                    Dim cmRep As New CommonRepository
                                    Dim lstID As New List(Of Decimal)

                                    lstID.Add(Convert.ToDecimal(hidWorkingID.Value))

                                    If cmRep.CheckExistIDTable(lstID, "HU_WORKING_BEFORE", "ID") Then
                                        ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                                        Exit Sub
                                    End If
                                Else
                                    Exit Sub
                                End If

                                If Execute() Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    UpdateControlState()
                                    rgGrid.Rebind()
                                    ClearControlValue(txtCompanyName, txtCompanyAddress, txtTelephone, rdJoinDate, rdEndDate,
                                                      txtSalary, txtTitleName, txtLevelName, txtTerReason)
                                    ExcuteScript("Clear", "clRadDatePicker()")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                        SelectedItem = Nothing
                        isLoad = False
                    End If
                Case TOOLBARITEM_CANCEL
                    SelectedItem = Nothing
                    If CurrentState = STATE_NEW Then
                        ResetControlValue()
                    End If
                    CurrentState = CommonMessage.STATE_NORMAL
                Case TOOLBARITEM_DELETE
                    If SelectedItem Is Nothing Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If SelectedItem.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If EmployeeInfo.WORK_STATUS Is Nothing Or _
                        (EmployeeInfo.WORK_STATUS IsNot Nothing AndAlso _
                         (EmployeeInfo.WORK_STATUS <> ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Or _
                          (EmployeeInfo.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID And _
                           EmployeeInfo.TER_EFFECT_DATE > Date.Now.Date))) Then
                        ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                        ctrlMessageBox.Show()
                    Else
                        ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                        Exit Sub
                    End If
            End Select
            UpdateControlState()
            rgGrid_NeedDataSource(Nothing, Nothing)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgGrid.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            If SelectedItem IsNot Nothing Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                If SelectedItem.Contains(Decimal.Parse(datarow("ID").Text)) Then
                    e.Item.Selected = True
                End If
            End If
        End If
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            If DeleteWorkingBefore() Then  'Xóa
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                rgGrid_NeedDataSource(Nothing, Nothing)
                'Hủy selectedItem của grid.
                SelectedItem = Nothing
                CurrentState = CommonMessage.STATE_NORMAL
                UpdateControlState()
                rgGrid.Rebind()
                ClearControlValue(txtCompanyName, txtCompanyAddress, txtTelephone, rdJoinDate, rdEndDate,
                                                      txtSalary, txtTitleName, txtLevelName, txtTerReason)
                ExcuteScript("Clear", "clRadDatePicker()")
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If

        End If
    End Sub

    Private Sub rgGrid_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGrid.NeedDataSource
        If EmployeeInfo IsNot Nothing Then
            Dim rep As New ProfileBusinessRepository
            Dim MaximumRows As Integer
            Dim objWorkingBefore As New WorkingBeforeDTO
            objWorkingBefore.EMPLOYEE_ID = EmployeeInfo.ID
            hidEmployeeID.Value = EmployeeInfo.ID.ToString()
            lstWorkingBefore = rep.GetEmpWorkingBefore(objWorkingBefore)
            rgGrid.VirtualItemCount = MaximumRows
            rgGrid.DataSource = lstWorkingBefore
            rep.Dispose()
        Else
            rgGrid.DataSource = New List(Of WorkingBeforeDTO)
        End If
    End Sub

    Private Sub rgGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgGrid.SelectedIndexChanged
        Try
            If rgGrid.SelectedItems.Count = 0 Then Exit Sub
            'Lưu vào viewStates để giữ những item được select phục vụ cho phương thức delete.
            SelectedItem = New List(Of Decimal)
            For Each dr As Telerik.Web.UI.GridDataItem In rgGrid.SelectedItems
                SelectedItem.Add(Decimal.Parse(dr("ID").Text))
            Next
            Dim item As GridDataItem = CType(rgGrid.SelectedItems(0), GridDataItem)
            hidWorkingID.Value = item.GetDataKeyValue("ID")
            txtCompanyAddress.Text = item.GetDataKeyValue("COMPANY_ADDRESS")
            txtCompanyName.Text = item.GetDataKeyValue("COMPANY_NAME")
            txtTelephone.Text = item.GetDataKeyValue("TELEPHONE")
            txtTitleName.Text = item.GetDataKeyValue("TITLE_NAME")
            txtLevelName.Text = item.GetDataKeyValue("LEVEL_NAME")
            txtSalary.Text = item.GetDataKeyValue("SALARY")
            txtTerReason.Text = item.GetDataKeyValue("TER_REASON")
            rdJoinDate.SelectedDate = item.GetDataKeyValue("JOIN_DATE")
            rdEndDate.SelectedDate = item.GetDataKeyValue("END_DATE")
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "Custom"
    Private Function Execute() As Boolean
        Try
            Dim objWorkingBefore As New WorkingBeforeDTO
            Dim rep As New ProfileBusinessRepository
            objWorkingBefore.EMPLOYEE_ID = Decimal.Parse(hidEmployeeID.Value)
            objWorkingBefore.COMPANY_ADDRESS = txtCompanyAddress.Text.Trim()
            objWorkingBefore.COMPANY_NAME = txtCompanyName.Text.Trim()
            objWorkingBefore.TITLE_NAME = txtTitleName.Text.Trim()
            objWorkingBefore.LEVEL_NAME = txtLevelName.Text.Trim()
            If txtSalary.Value IsNot Nothing Then
                objWorkingBefore.SALARY = txtSalary.Value
            End If
            objWorkingBefore.TELEPHONE = txtTelephone.Text.Trim()
            objWorkingBefore.TER_REASON = txtTerReason.Text.Trim()
            objWorkingBefore.JOIN_DATE = rdJoinDate.SelectedDate()
            objWorkingBefore.END_DATE = rdEndDate.SelectedDate()
            Dim gID As Decimal
            If hidWorkingID.Value = "" Then
                rep.InsertWorkingBefore(objWorkingBefore, gID)
            Else
                objWorkingBefore.ID = Decimal.Parse(hidWorkingID.Value)
                rep.ModifyWorkingBefore(objWorkingBefore, gID)
            End If
            rgGrid.Rebind()
            rep.Dispose()
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Overrides Sub UpdateControlState()
        Try
            If CurrentState Is Nothing Then
                CurrentState = STATE_NORMAL
            End If
            Select Case CurrentState
                Case STATE_NEW
                    SetStatusControl(True)
                    EnabledGrid(rgGrid, False)
                    hidWorkingID.Value = ""
                Case STATE_EDIT
                    EnabledGrid(rgGrid, False)
                    SetStatusControl(True)
                Case STATE_NORMAL
                    EnabledGrid(rgGrid, True)
                    SetStatusControl(False)
                    rgGrid.Rebind()
                    ResetControlValue()
            End Select
            Me.Send(CurrentState)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub SetStatusToolBar()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SetStatusControl(ByVal sTrangThai As Boolean)
        Try
            txtCompanyName.ReadOnly = Not sTrangThai
            txtLevelName.ReadOnly = Not sTrangThai
            txtTitleName.ReadOnly = Not sTrangThai
            txtCompanyAddress.ReadOnly = Not sTrangThai
            txtSalary.ReadOnly = Not sTrangThai
            txtTelephone.ReadOnly = Not sTrangThai
            txtTerReason.ReadOnly = Not sTrangThai

            rdEndDate.Enabled = sTrangThai
            rdJoinDate.Enabled = sTrangThai
            SetStatusToolBar()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub ResetControlValue()
        hidWorkingID.Value = ""
        txtCompanyName.Text = ""
        txtLevelName.Text = ""
        txtTitleName.Text = ""
        txtCompanyAddress.Text = ""
        txtSalary.Text = ""
        txtTelephone.Text = ""
        txtTerReason.Text = ""
        rdEndDate.Clear()
        rdJoinDate.Clear()
    End Sub

    Private Function DeleteWorkingBefore() As Boolean
        Try
            Dim rep As New ProfileBusinessRepository
            Return rep.DeleteWorkingBefore(SelectedItem)
            rep.Dispose()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class
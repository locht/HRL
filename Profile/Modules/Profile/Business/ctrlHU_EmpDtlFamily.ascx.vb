Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports Common
Imports Common.Common
Imports Common.CommonMessage
Public Class ctrlHU_EmpDtlFamily
    Inherits CommonView
    Dim employeeCode As String
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Properties"
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

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    Property lstFamily As List(Of FamilyDTO)
        Get
            Return ViewState(Me.ID & "_lstFamily")
        End Get
        Set(ByVal value As List(Of FamilyDTO))
            ViewState(Me.ID & "_lstFamily") = value
        End Set
    End Property

    Property SelectedItem As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_SelectedItem")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItem") = value
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
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        rgFamily.SetFilter()
        InitControl()
        If Not IsPostBack Then
            ViewConfig(RightPane)
            GirdConfig(rgFamily)
        End If
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar,
                         ToolbarItem.Create,
                         ToolbarItem.Edit,
                         ToolbarItem.Save,
                         ToolbarItem.Cancel,
                         ToolbarItem.Delete)
            CType(Me.MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            Me.CurrentPlaceHolder = Me.ViewName
            ctrlEmpBasicInfo.SetProperty("CurrentPlaceHolder", Me.CurrentPlaceHolder)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            Using rep As New ProfileRepository
                If ComboBoxDataDTO Is Nothing Then
                    ComboBoxDataDTO = New ComboBoxDataDTO
                    ComboBoxDataDTO.GET_RELATION = True
                    ComboBoxDataDTO.GET_PROVINCE = True
                    rep.GetComboList(ComboBoxDataDTO)
                End If
                
                If ComboBoxDataDTO IsNot Nothing Then
                    FillDropDownList(cboRelationship, ComboBoxDataDTO.LIST_RELATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboRelationship.SelectedValue)
                    FillDropDownList(cboNguyenQuan, ComboBoxDataDTO.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboNguyenQuan.SelectedValue)
                End If

            End Using
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
                        If chkIsDeduct.Checked = True And rdDeductFrom.SelectedDate Is Nothing Then
                            ShowMessage(Translate("Chưa chọn ngày giảm trừ"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                        Select Case CurrentState
                            Case STATE_NEW
                                If Execute() Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    ClearControlValue(txtFullName, cboNguyenQuan, cboRelationship, rdBirthDate, txtIDNO, txtAdress,
                                                        chkIsDeduct, rdDeductReg, rdDeductFrom, rdDeductTo, txtRemark, txtTax, txtCareer, txtTitle)
                                    rgFamily.Rebind()
                                    ExcuteScript("Clear", "clRadDatePicker()")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case STATE_EDIT
                                If Execute() Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    ClearControlValue(txtFullName, cboNguyenQuan, cboRelationship, rdBirthDate, txtIDNO, txtAdress,
                                                        chkIsDeduct, rdDeductReg, rdDeductFrom, rdDeductTo, txtRemark, txtTax, txtCareer, txtTitle)
                                    rgFamily.Rebind()
                                    ExcuteScript("Clear", "clRadDatePicker()")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                        SelectedItem = Nothing
                        isLoad = False
                        rgFamily.Rebind()
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgFamily', pane3ID, pane4ID)")
                    End If
                Case TOOLBARITEM_CANCEL
                    SelectedItem = Nothing
                    ResetControlValue()
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
            ctrlEmpBasicInfo.SetProperty("CurrentState", Me.CurrentState)
            ctrlEmpBasicInfo.Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cvalRelationship_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalRelationship.ServerValidate
        Try
            If cboRelationship.SelectedValue <> "" Then
                args.IsValid = True
            Else
                args.IsValid = False
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgFamily_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgFamily.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                If SelectedItem IsNot Nothing Then
                    If SelectedItem.Contains(Decimal.Parse(datarow.GetDataKeyValue("ID"))) Then
                        e.Item.Selected = True
                    End If
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub rgFamily_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgFamily.NeedDataSource
        If EmployeeInfo IsNot Nothing Then
            Dim rep As New ProfileBusinessRepository
            Dim objFamily As New FamilyDTO
            objFamily.EMPLOYEE_ID = EmployeeInfo.ID
            SetValueObjectByRadGrid(rgFamily, objFamily)
            rep.Dispose()
            lstFamily = rep.GetEmployeeFamily(objFamily)
            rgFamily.DataSource = lstFamily
        Else
            rgFamily.DataSource = New List(Of FamilyDTO)
        End If
    End Sub

    Private Sub rgFamily_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgFamily.SelectedIndexChanged
        Try
            If rgFamily.SelectedItems.Count = 0 Then Exit Sub
            'Lưu vào viewStates để giữ những item được select phục vụ cho phương thức delete.
            SelectedItem = New List(Of Decimal)
            For Each dr As Telerik.Web.UI.GridDataItem In rgFamily.SelectedItems
                SelectedItem.Add(dr.GetDataKeyValue("ID"))
            Next

            Dim item As GridDataItem = rgFamily.SelectedItems(0)
            hidFamilyID.Value = item.GetDataKeyValue("ID")
            txtAdress.Text = item.GetDataKeyValue("ADDRESS")
            txtIDNO.Text = item.GetDataKeyValue("ID_NO")
            txtFullName.Text = item.GetDataKeyValue("FULLNAME")
            txtRemark.Text = item.GetDataKeyValue("REMARK")
            txtTax.Text = item.GetDataKeyValue("TAXTATION")
            rdBirthDate.SelectedDate = item.GetDataKeyValue("BIRTH_DATE")
            chkIsDeduct.Checked = item.GetDataKeyValue("IS_DEDUCT")
            rdDeductReg.SelectedDate = item.GetDataKeyValue("DEDUCT_REG")
            rdDeductFrom.SelectedDate = item.GetDataKeyValue("DEDUCT_FROM")
            rdDeductTo.SelectedDate = item.GetDataKeyValue("DEDUCT_TO")
            cboRelationship.SelectedValue = item.GetDataKeyValue("RELATION_ID")
            cboNguyenQuan.SelectedValue = item.GetDataKeyValue("PROVINCE_ID")
            txtCareer.Text = item.GetDataKeyValue("CAREER")
            txtTitle.Text = item.GetDataKeyValue("TITLE_NAME")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    'Private Sub cvalIDNO_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalIDNO.ServerValidate
    '    Try
    '        Dim rep As New ProfileBusinessRepository
    '        Dim _validate As New FamilyDTO

    '        'Nếu có Số CMND thì check trùng.
    '        If txtIDNO.Text.Trim() <> "" Then
    '            _validate.ID_NO = txtIDNO.Text.Trim()
    '            If hidFamilyID.Value <> "" Then
    '                _validate.ID = Decimal.Parse(hidFamilyID.Value)
    '            End If
    '            If (rep.ValidateFamily(_validate)) Then
    '                args.IsValid = True
    '            Else
    '                args.IsValid = False
    '            End If
    '        End If
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                If DeleteFamily() Then  'Xóa
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    rgFamily.Rebind()
                    SelectedItem = Nothing
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                    ExcuteScript("Clear", "clRadDatePicker()")
                    ClearControlValue(txtFullName, cboNguyenQuan, cboRelationship, rdBirthDate, txtIDNO, txtAdress,
                                        chkIsDeduct, rdDeductReg, rdDeductFrom, rdDeductTo, txtRemark, txtTax, txtCareer, txtTitle)
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                End If

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub chkIsDeduct_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkIsDeduct.CheckedChanged
        rdDeductFrom.Enabled = chkIsDeduct.Checked
        rdDeductTo.Enabled = chkIsDeduct.Checked
    End Sub


#End Region

#Region "Custom"
    Private Function Execute() As Boolean
        Try
            Dim objFamily As New FamilyDTO
            Dim rep As New ProfileBusinessRepository
            objFamily.EMPLOYEE_ID = EmployeeInfo.ID
            objFamily.BIRTH_DATE = rdBirthDate.SelectedDate
            objFamily.DEDUCT_REG = rdDeductReg.SelectedDate
            objFamily.ADDRESS = txtAdress.Text.Trim()
            objFamily.TAXTATION = txtTax.Text
            objFamily.DEDUCT_FROM = rdDeductFrom.SelectedDate()
            objFamily.DEDUCT_TO = rdDeductTo.SelectedDate()
            objFamily.FULLNAME = txtFullName.Text.Trim()
            objFamily.ID_NO = txtIDNO.Text.Trim()
            objFamily.IS_DEDUCT = chkIsDeduct.Checked
            objFamily.REMARK = txtRemark.Text.Trim()
            objFamily.CAREER = txtCareer.Text.Trim()
            objFamily.TITLE_NAME = txtTitle.Text.Trim()
            If cboRelationship.SelectedValue <> "" Then
                objFamily.RELATION_ID = Decimal.Parse(cboRelationship.SelectedValue)
            End If
            If cboNguyenQuan.SelectedValue <> "" Then
                objFamily.PROVINCE_ID = Decimal.Parse(cboNguyenQuan.SelectedValue)
            End If
            Dim gID As Decimal
            If hidFamilyID.Value = "" Then
                rep.InsertEmployeeFamily(objFamily, gID)
            Else
                'objFamily.ID = Decimal.Parse(hidFamilyID.Value)
                'rep.ModifyEmployeeFamily(objFamily, gID)

                objFamily.ID = Decimal.Parse(hidFamilyID.Value)

                Dim objFamilyEdit As New FamilyDTO
                objFamilyEdit.ID = objFamily.ID
                Dim list = rep.GetEmployeeFamily(objFamilyEdit)
                If list.Count > 0 Then
                    rep.ModifyEmployeeFamily(objFamily, gID)
                Else
                    ShowMessage("Dữ liệu không tồn tại!", Utilities.NotifyType.Warning)
                End If

            End If
            rep.Dispose()
            IDSelect = gID
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
                    EnabledGrid(rgFamily, False)
                    SetStatusControl(True)
                Case STATE_EDIT
                    EnabledGrid(rgFamily, False)
                    SetStatusControl(True)
                Case STATE_NORMAL
                    EnabledGrid(rgFamily, True)
                    SetStatusControl(False)
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
        txtAdress.ReadOnly = Not sTrangThai
        txtRemark.ReadOnly = Not sTrangThai
        txtTax.ReadOnly = Not sTrangThai
        txtIDNO.ReadOnly = Not sTrangThai
        txtFullName.ReadOnly = Not sTrangThai
        chkIsDeduct.Enabled = sTrangThai
        txtCareer.ReadOnly = Not sTrangThai
        txtTitle.ReadOnly = Not sTrangThai
        Utilities.ReadOnlyRadComBo(cboRelationship, Not sTrangThai)
        Utilities.ReadOnlyRadComBo(cboNguyenQuan, Not sTrangThai)
        Utilities.EnableRadDatePicker(rdBirthDate, sTrangThai)
        Utilities.EnableRadDatePicker(rdDeductReg, sTrangThai)
        If chkIsDeduct.Checked AndAlso chkIsDeduct.Enabled Then
            rdDeductFrom.Enabled = True
            rdDeductTo.Enabled = True
        Else
            rdDeductFrom.Enabled = False
            rdDeductTo.Enabled = False
        End If
        SetStatusToolBar()
    End Sub

    Private Sub ResetControlValue()
        ClearControlValue(txtFullName, txtAdress, txtAdress, txtIDNO, txtRemark, txtTax, txtCareer, txtTitle,
                          hidFamilyID, chkIsDeduct, cboNguyenQuan, cboRelationship,
                          rdBirthDate, rdDeductFrom, rdDeductReg, rdDeductTo)
        rgFamily.SelectedIndexes.Clear()
    End Sub

    Private Function DeleteFamily() As Boolean
        Try
            Dim rep As New ProfileBusinessRepository
            Return rep.DeleteEmployeeFamily(SelectedItem)
            rep.Dispose()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class
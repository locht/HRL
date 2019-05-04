Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Common.Common
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlPortalFamily_Edit
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"

    Public Property EmployeeID As Decimal

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            rgFamily.SetFilter()
            rgFamilyEdit.SetFilter()
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
        'If Not IsPostBack Then
        '    ViewConfig(RadPane2)
        '    GirdConfig(rgFamily)
        '    GirdConfig(rgFamilyEdit)
        'End If
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Seperator,
                         ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Seperator,
                         ToolbarItem.Submit)
            CType(Me.MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileBusinessRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            Dim ComboBoxDataDTO As New ComboBoxDataDTO
            Using rep As New ProfileRepository
                ComboBoxDataDTO.GET_RELATION = True
                ComboBoxDataDTO.GET_PROVINCE = True
                rep.GetComboList(ComboBoxDataDTO)
                If ComboBoxDataDTO IsNot Nothing Then
                    FillDropDownList(cboRelationship, ComboBoxDataDTO.LIST_RELATION, "NAME", "ID", Common.Common.SystemLanguage, True, cboRelationship.SelectedValue)
                    FillDropDownList(cboNguyenQuan, ComboBoxDataDTO.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboNguyenQuan.SelectedValue)
                End If

            End Using

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("ADDRESS", txtAdress)
            dic.Add("ID_NO", txtIDNO)
            dic.Add("FULLNAME", txtFullName)
            dic.Add("TAXTATION", txtTax)
            dic.Add("REMARK", txtRemark)
            dic.Add("BIRTH_DATE", rdBirthDate)
            dic.Add("IS_DEDUCT", chkIsDeduct)
            dic.Add("DEDUCT_REG", rdDeductReg)
            dic.Add("DEDUCT_FROM", rdDeductFrom)
            dic.Add("DEDUCT_TO", rdDeductTo)
            dic.Add("RELATION_ID", cboRelationship)
            dic.Add("ID", hidFamilyID)
            dic.Add("CAREER", txtCareer)
            dic.Add("TITLE_NAME", txtTitle)
            dic.Add("PROVINCE_ID", cboNguyenQuan)
            Utilities.OnClientRowSelectedChanged(rgFamily, dic)

            Dim dic1 As New Dictionary(Of String, Control)
            dic1.Add("ADDRESS", txtAdress)
            dic1.Add("ID_NO", txtIDNO)
            dic1.Add("FULLNAME", txtFullName)
            dic1.Add("TAXTATION", txtTax)
            dic1.Add("REMARK", txtRemark)
            dic1.Add("BIRTH_DATE", rdBirthDate)
            dic1.Add("IS_DEDUCT", chkIsDeduct)
            dic1.Add("DEDUCT_REG", rdDeductReg)
            dic1.Add("DEDUCT_FROM", rdDeductFrom)
            dic1.Add("DEDUCT_TO", rdDeductTo)
            dic1.Add("RELATION_ID", cboRelationship)
            dic1.Add("ID", hidID)
            dic1.Add("FK_PKEY", hidFamilyID)
            dic1.Add("CAREER", txtCareer)
            dic1.Add("TITLE_NAME", txtTitle)
            dic1.Add("PROVINCE_ID", cboNguyenQuan)
            Utilities.OnClientRowSelectedChanged(rgFamilyEdit, dic1)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    EnableControlAll(False, txtAdress, txtFullName, txtIDNO, txtRemark, txtTax,
                                     rdBirthDate, rdDeductFrom, rdDeductReg, rdDeductTo,
                                     chkIsDeduct, cboRelationship, cboNguyenQuan, txtCareer, txtTitle)

                    If Not chkIsDeduct.Checked Then
                        chkIsDeduct_CheckedChanged(Nothing, Nothing)
                    End If
                    chkIsDeduct.AutoPostBack = False
                    EnabledGridNotPostback(rgFamily, True)
                    EnabledGridNotPostback(rgFamilyEdit, True)
                Case CommonMessage.STATE_NEW
                    EnableControlAll(True, txtAdress, txtFullName, txtIDNO, txtRemark, txtTax,
                                     rdBirthDate, rdDeductFrom, rdDeductReg, rdDeductTo,
                                     chkIsDeduct, cboRelationship, cboNguyenQuan, txtCareer, txtTitle)

                    If Not chkIsDeduct.Checked Then
                        chkIsDeduct_CheckedChanged(Nothing, Nothing)
                    End If
                    chkIsDeduct.AutoPostBack = True
                    EnabledGridNotPostback(rgFamily, False)
                    EnabledGridNotPostback(rgFamilyEdit, False)
                Case CommonMessage.STATE_EDIT
                    EnableControlAll(True, txtAdress, txtFullName, txtIDNO, txtRemark, txtTax,
                                     rdBirthDate, rdDeductFrom, rdDeductReg, rdDeductTo,
                                     chkIsDeduct, cboRelationship, cboNguyenQuan, txtCareer, txtTitle)

                    If Not chkIsDeduct.Checked Then
                        chkIsDeduct_CheckedChanged(Nothing, Nothing)
                    End If
                    chkIsDeduct.AutoPostBack = True
                    EnabledGridNotPostback(rgFamily, False)
                    EnabledGridNotPostback(rgFamilyEdit, False)
            End Select

            ChangeToolbarState()

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(txtAdress, txtFullName, txtIDNO, txtRemark, txtTax,
                                     rdBirthDate, rdDeductFrom, rdDeductReg, rdDeductTo,
                                     chkIsDeduct, hidFamilyID, hidID, cboRelationship, cboNguyenQuan)
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim isInsert As Boolean = True

                        Dim obj As New FamilyEditDTO
                        obj.EMPLOYEE_ID = EmployeeID
                        obj.BIRTH_DATE = rdBirthDate.SelectedDate
                        obj.DEDUCT_REG = rdDeductReg.SelectedDate
                        obj.ADDRESS = txtAdress.Text.Trim()
                        obj.TAXTATION = txtTax.Text
                        obj.DEDUCT_FROM = rdDeductFrom.SelectedDate()
                        obj.DEDUCT_TO = rdDeductTo.SelectedDate()
                        obj.FULLNAME = txtFullName.Text.Trim()
                        obj.ID_NO = txtIDNO.Text.Trim()
                        obj.IS_DEDUCT = chkIsDeduct.Checked
                        obj.REMARK = txtRemark.Text.Trim()
                        If cboRelationship.SelectedValue <> "" Then
                            obj.RELATION_ID = Decimal.Parse(cboRelationship.SelectedValue)
                        End If
                        obj.CAREER = txtCareer.Text.Trim()
                        obj.TITLE_NAME = txtTitle.Text.Trim()
                        If cboNguyenQuan.SelectedValue <> "" Then
                            obj.PROVINCE_ID = Decimal.Parse(cboNguyenQuan.SelectedValue)
                        End If
                        Using rep As New ProfileBusinessRepository
                            If hidFamilyID.Value <> "" Then
                                obj.FK_PKEY = hidFamilyID.Value
                                Dim bCheck = rep.CheckExistFamilyEdit(hidFamilyID.Value)
                                If bCheck IsNot Nothing Then
                                    Dim status = bCheck.STATUS
                                    Dim pkey = bCheck.ID
                                    ' Trạng thái chờ phê duyệt
                                    If status = 1 Then
                                        ShowMessage("Thông đang đang Chờ phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                                        Exit Sub
                                    End If
                                    isInsert = False
                                    obj.ID = pkey
                                End If
                            End If


                            If hidID.Value <> "" Then
                                isInsert = False
                            End If

                            If isInsert Then
                                rep.InsertEmployeeFamilyEdit(obj, 0)
                            Else
                                obj.ID = hidID.Value
                                rep.ModifyEmployeeFamilyEdit(obj, 0)
                            End If

                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgFamily.Rebind()
                            rgFamilyEdit.Rebind()
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                        End Using
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL

                    If rgFamilyEdit.SelectedItems.Count > 0 Then
                        Dim item = CType(rgFamilyEdit.SelectedItems(rgFamilyEdit.SelectedItems.Count - 1), GridDataItem)
                        hidFamilyID.Value = item.GetDataKeyValue("ID")
                        txtAdress.Text = item.GetDataKeyValue("ADDRESS")
                        txtIDNO.Text = item.GetDataKeyValue("ID_NO")
                        txtFullName.Text = item.GetDataKeyValue("FULLNAME")
                        txtTax.Text = item.GetDataKeyValue("TAXTATION")
                        txtRemark.Text = item.GetDataKeyValue("REMARK")
                        rdBirthDate.SelectedDate = item.GetDataKeyValue("BIRTH_DATE")
                        chkIsDeduct.Checked = item.GetDataKeyValue("IS_DEDUCT")
                        rdDeductReg.SelectedDate = item.GetDataKeyValue("DEDUCT_REG")
                        rdDeductFrom.SelectedDate = item.GetDataKeyValue("DEDUCT_FROM")
                        rdDeductTo.SelectedDate = item.GetDataKeyValue("DEDUCT_TO")
                        cboRelationship.SelectedValue = item.GetDataKeyValue("RELATION_ID")
                        txtCareer.Text = item.GetDataKeyValue("CAREER")
                        txtTitle.Text = item.GetDataKeyValue("TITLE_NAME")
                        cboNguyenQuan.SelectedValue = item.GetDataKeyValue("PROVINCE_ID")
                        If item.GetDataKeyValue("FK_PKEY") IsNot Nothing Then
                            hidFamilyID.Value = item.GetDataKeyValue("FK_PKEY")
                        End If
                        hidID.Value = item.GetDataKeyValue("ID")
                        chkIsDeduct_CheckedChanged(Nothing, Nothing)
                    Else
                        ClearControlValue(txtAdress, txtFullName, txtIDNO, txtRemark, txtTax,
                                     rdBirthDate, rdDeductFrom, rdDeductReg, rdDeductTo,
                                     chkIsDeduct, hidFamilyID, hidID, cboRelationship, cboNguyenQuan)
                    End If


                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_SUBMIT
                    If rgFamilyEdit.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim status As String = ""
                    For Each item As GridDataItem In rgFamilyEdit.SelectedItems
                        status = item.GetDataKeyValue("STATUS")
                        If status = 1 Then
                            ShowMessage("Thông tin đang Chờ phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                            Exit Sub
                        End If
                        If status = 2 Then
                            ShowMessage("Thông tin đang phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                            Exit Sub
                        End If
                        If status = 3 Then
                            ShowMessage("Thông tin đang không phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate("Thông tin đã gửi duyệt sẽ không được chỉnh sửa. Bạn chắc chắn muốn gửi duyệt?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()


            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgFamily_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgFamily.NeedDataSource
        Try
            SetValueObjectByRadGrid(rgFamily, New FamilyDTO)

            Using rep As New ProfileBusinessRepository
                rgFamily.DataSource = rep.GetEmployeeFamily(New FamilyDTO With {.EMPLOYEE_ID = EmployeeID})
            End Using
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgFamilyEdit_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgFamilyEdit.NeedDataSource
        Try
            SetValueObjectByRadGrid(rgFamilyEdit, New FamilyEditDTO)
            Using rep As New ProfileBusinessRepository
                rgFamilyEdit.DataSource = rep.GetEmployeeFamilyEdit(New FamilyEditDTO With {.EMPLOYEE_ID = EmployeeID})
            End Using
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgFamilyEdit_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgFamilyEdit.ItemCommand
        Try
            If e.CommandName = "EditRow" Then
                Dim item = CType(e.Item, GridDataItem)
                Dim status As String = ""
                If item.GetDataKeyValue("STATUS") IsNot Nothing Then
                    status = item.GetDataKeyValue("STATUS")
                End If
                Select Case status
                    Case 1
                        ShowMessage("Bản ghi đang Chờ phê duyệt chỉ được xem thông tin", NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case 2
                        ShowMessage("Bản ghi đã Phê duyệt chỉ được xem thông tin", NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case Else
                        CurrentState = CommonMessage.STATE_EDIT
                End Select
                hidFamilyID.Value = item.GetDataKeyValue("ID")
                txtAdress.Text = item.GetDataKeyValue("ADDRESS")
                txtIDNO.Text = item.GetDataKeyValue("ID_NO")
                txtFullName.Text = item.GetDataKeyValue("FULLNAME")
                txtTax.Text = item.GetDataKeyValue("TAXTATION")
                txtRemark.Text = item.GetDataKeyValue("REMARK")
                rdBirthDate.SelectedDate = item.GetDataKeyValue("BIRTH_DATE")
                chkIsDeduct.Checked = item.GetDataKeyValue("IS_DEDUCT")
                rdDeductReg.SelectedDate = item.GetDataKeyValue("DEDUCT_REG")
                rdDeductFrom.SelectedDate = item.GetDataKeyValue("DEDUCT_FROM")
                rdDeductTo.SelectedDate = item.GetDataKeyValue("DEDUCT_TO")
                cboRelationship.SelectedValue = item.GetDataKeyValue("RELATION_ID")
                txtCareer.Text = item.GetDataKeyValue("CAREER")
                txtTitle.Text = item.GetDataKeyValue("TITLE_NAME")
                cboNguyenQuan.SelectedValue = item.GetDataKeyValue("PROVINCE_ID")
                If item.GetDataKeyValue("FK_PKEY") IsNot Nothing Then
                    hidFamilyID.Value = item.GetDataKeyValue("FK_PKEY")
                End If
                hidID.Value = item.GetDataKeyValue("ID")
                chkIsDeduct_CheckedChanged(Nothing, Nothing)
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgFamily_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgFamily.ItemCommand
        Try
            If e.CommandName = "EditRow" Then
                CurrentState = CommonMessage.STATE_EDIT
                Dim item = CType(e.Item, GridDataItem)
                hidFamilyID.Value = item.GetDataKeyValue("ID")
                txtAdress.Text = item.GetDataKeyValue("ADDRESS")
                txtIDNO.Text = item.GetDataKeyValue("ID_NO")
                txtFullName.Text = item.GetDataKeyValue("FULLNAME")
                txtTax.Text = item.GetDataKeyValue("TAXTATION")
                txtRemark.Text = item.GetDataKeyValue("REMARK")
                rdBirthDate.SelectedDate = item.GetDataKeyValue("BIRTH_DATE")
                chkIsDeduct.Checked = item.GetDataKeyValue("IS_DEDUCT")
                rdDeductReg.SelectedDate = item.GetDataKeyValue("DEDUCT_REG")
                rdDeductFrom.SelectedDate = item.GetDataKeyValue("DEDUCT_FROM")
                rdDeductTo.SelectedDate = item.GetDataKeyValue("DEDUCT_TO")
                cboRelationship.SelectedValue = item.GetDataKeyValue("RELATION_ID")
                txtCareer.Text = item.GetDataKeyValue("CAREER")
                txtTitle.Text = item.GetDataKeyValue("TITLE_NAME")
                cboNguyenQuan.SelectedValue = item.GetDataKeyValue("PROVINCE_ID")
                hidFamilyID.Value = item.GetDataKeyValue("ID")
                hidID.Value = ""
                chkIsDeduct_CheckedChanged(Nothing, Nothing)
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    'Private Sub rgFamily_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgFamily.SelectedIndexChanged
    '    Try
    '        If rgFamily.SelectedItems.Count = 0 Then
    '            ClearControlValue(txtAdress, txtFullName, txtIDNO, txtRemark,
    '                             rdBirthDate, rdDeductFrom, rdDeductReg, rdDeductTo,
    '                             chkIsDeduct, hidFamilyID, hidID)
    '            Exit Sub
    '        End If
    '        Dim item = CType(rgFamily.SelectedItems(rgFamily.SelectedItems.Count - 1), GridDataItem)
    '        hidFamilyID.Value = item.GetDataKeyValue("ID")
    '        txtAdress.Text = item.GetDataKeyValue("ADDRESS")
    '        txtIDNO.Text = item.GetDataKeyValue("ID_NO")
    '        txtFullName.Text = item.GetDataKeyValue("FULLNAME")
    '        txtRemark.Text = item.GetDataKeyValue("REMARK")
    '        rdBirthDate.SelectedDate = item.GetDataKeyValue("BIRTH_DATE")
    '        chkIsDeduct.Checked = item.GetDataKeyValue("IS_DEDUCT")
    '        rdDeductReg.SelectedDate = item.GetDataKeyValue("DEDUCT_REG")
    '        rdDeductFrom.SelectedDate = item.GetDataKeyValue("DEDUCT_FROM")
    '        rdDeductTo.SelectedDate = item.GetDataKeyValue("DEDUCT_TO")
    '        cboRelationship.SelectedValue = item.GetDataKeyValue("RELATION_ID")
    '        hidFamilyID.Value = item.GetDataKeyValue("ID")
    '        hidID.Value = ""
    '        chkIsDeduct_CheckedChanged(Nothing, Nothing)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub rgFamilyEdit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgFamilyEdit.SelectedIndexChanged
    '    Try
    '        If rgFamilyEdit.SelectedItems.Count = 0 Then
    '            ClearControlValue(txtAdress, txtFullName, txtIDNO, txtRemark,
    '                             rdBirthDate, rdDeductFrom, rdDeductReg, rdDeductTo,
    '                             chkIsDeduct, hidFamilyID, hidID)
    '            Exit Sub
    '        End If
    '        Dim item = CType(rgFamilyEdit.SelectedItems(rgFamilyEdit.SelectedItems.Count - 1), GridDataItem)
    '        hidFamilyID.Value = item.GetDataKeyValue("ID")
    '        txtAdress.Text = item.GetDataKeyValue("ADDRESS")
    '        txtIDNO.Text = item.GetDataKeyValue("ID_NO")
    '        txtFullName.Text = item.GetDataKeyValue("FULLNAME")
    '        txtRemark.Text = item.GetDataKeyValue("REMARK")
    '        rdBirthDate.SelectedDate = item.GetDataKeyValue("BIRTH_DATE")
    '        chkIsDeduct.Checked = item.GetDataKeyValue("IS_DEDUCT")
    '        rdDeductReg.SelectedDate = item.GetDataKeyValue("DEDUCT_REG")
    '        rdDeductFrom.SelectedDate = item.GetDataKeyValue("DEDUCT_FROM")
    '        rdDeductTo.SelectedDate = item.GetDataKeyValue("DEDUCT_TO")
    '        cboRelationship.SelectedValue = item.GetDataKeyValue("RELATION_ID")
    '        If item.GetDataKeyValue("FK_PKEY") IsNot Nothing Then
    '            hidFamilyID.Value = item.GetDataKeyValue("FK_PKEY")
    '        End If
    '        hidID.Value = item.GetDataKeyValue("ID")
    '        chkIsDeduct_CheckedChanged(Nothing, Nothing)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Protected Sub chkIsDeduct_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkIsDeduct.CheckedChanged
        rdDeductFrom.Enabled = chkIsDeduct.Checked
        rdDeductTo.Enabled = chkIsDeduct.Checked
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

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Using rep As New ProfileBusinessRepository
                    Dim lstID As New List(Of Decimal)
                    For Each item As GridDataItem In rgFamilyEdit.SelectedItems
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next
                    If lstID.Count > 0 Then
                        rep.SendEmployeeFamilyEdit(lstID)
                    End If
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgFamily.Rebind()
                    rgFamilyEdit.Rebind()
                End Using
            End If


        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

#End Region

End Class
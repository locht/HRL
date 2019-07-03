Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Common.Common
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlInfoCertificate
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"

    Public Property EmployeeID As Decimal
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            rgCetificate.SetFilter()
            rgCetificateEdit.SetFilter()
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
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
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
                'ComboBoxDataDTO.GET_RELATION = True
                'ComboBoxDataDTO.GET_PROVINCE = True
                ComboBoxDataDTO.GET_FIELD_TRAIN = True
                ComboBoxDataDTO.GET_MAJOR_TRAIN = True
                ComboBoxDataDTO.GET_LEVEL_TRAIN = True
                rep.GetComboList(ComboBoxDataDTO)
                If ComboBoxDataDTO IsNot Nothing Then
                    FillDropDownList(cbField, ComboBoxDataDTO.LIST_FIELD_TRAIN, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cbField.SelectedValue)
                    FillDropDownList(cbLevel, ComboBoxDataDTO.LIST_LEVEL_TRAIN, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cbLevel.SelectedValue)
                    FillDropDownList(cbMajor, ComboBoxDataDTO.LIST_MAJOR_TRAIN, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cbMajor.SelectedValue)

                End If
            End Using

            Dim dic As New Dictionary(Of String, Control)
            'dic.Add("ADDRESS", txtAdress)
            'dic.Add("ID_NO", txtIDNO)
            'dic.Add("FULLNAME", txtFullName)
            'dic.Add("TAXTATION", txtTax)
            'dic.Add("REMARK", txtRemark)
            'dic.Add("BIRTH_DATE", rdBirthDate)
            'dic.Add("IS_DEDUCT", chkIsDeduct)
            'dic.Add("DEDUCT_REG", rdDeductReg)
            'dic.Add("DEDUCT_FROM", rdDeductFrom)
            'dic.Add("DEDUCT_TO", rdDeductTo)
            'dic.Add("RELATION_ID", cboRelationship)
            'dic.Add("ID", hidFamilyID)
            'dic.Add("CAREER", txtCareer)
            'dic.Add("TITLE_NAME", txtTitle)
            'dic.Add("PROVINCE_ID", cboNguyenQuan)
            Utilities.OnClientRowSelectedChanged(rgCetificate, dic)

            Dim dic1 As New Dictionary(Of String, Control)
            'dic1.Add("ADDRESS", txtAdress)
            'dic1.Add("ID_NO", txtIDNO)
            'dic1.Add("FULLNAME", txtFullName)
            'dic1.Add("TAXTATION", txtTax)
            'dic1.Add("REMARK", txtRemark)
            'dic1.Add("BIRTH_DATE", rdBirthDate)
            'dic1.Add("IS_DEDUCT", chkIsDeduct)
            'dic1.Add("DEDUCT_REG", rdDeductReg)
            'dic1.Add("DEDUCT_FROM", rdDeductFrom)
            'dic1.Add("DEDUCT_TO", rdDeductTo)
            'dic1.Add("RELATION_ID", cboRelationship)
            'dic1.Add("ID", hidID)
            'dic1.Add("FK_PKEY", hidFamilyID)
            'dic1.Add("CAREER", txtCareer)
            'dic1.Add("TITLE_NAME", txtTitle)
            'dic1.Add("PROVINCE_ID", cboNguyenQuan)
            Utilities.OnClientRowSelectedChanged(rgCetificateEdit, dic1)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    'dung
                    EnableControlAll(False, cbField, rdFromDate, rdToDate, txtSchool, cbMajor, cbLevel,
                                     txtMark, txtContentTrain, txtTypeTrain, txtCodeCertificate, rdEffectFrom,
                                     rdEffectTo, txtClassification, txtYear, is_Renew, txtRemark, txtUploadFile, btnUploadFile, btnDownload)

                    'If Not chkIsDeduct.Checked Then
                    '    chkIsDeduct_CheckedChanged(Nothing, Nothing)
                    'End If
                    'chkIsDeduct.AutoPostBack = False
                    EnabledGridNotPostback(rgCetificate, True)
                    EnabledGridNotPostback(rgCetificateEdit, True)
                Case CommonMessage.STATE_NEW
                    'dung
                    EnableControlAll(True, cbField, rdFromDate, rdToDate, txtSchool, cbMajor, cbLevel,
                                     txtMark, txtContentTrain, txtTypeTrain, txtCodeCertificate, rdEffectFrom,
                                     rdEffectTo, txtClassification, txtYear, is_Renew, txtRemark, txtUploadFile, btnUploadFile, btnDownload)

                    'If Not chkIsDeduct.Checked Then
                    '    chkIsDeduct_CheckedChanged(Nothing, Nothing)
                    'End If
                    'chkIsDeduct.AutoPostBack = True
                    EnabledGridNotPostback(rgCetificate, False)
                    EnabledGridNotPostback(rgCetificateEdit, False)
                Case CommonMessage.STATE_EDIT
                    'dung
                    EnableControlAll(True, cbField, rdFromDate, rdToDate, txtSchool, cbMajor, cbLevel,
                                     txtMark, txtContentTrain, txtTypeTrain, txtCodeCertificate, rdEffectFrom,
                                     rdEffectTo, txtClassification, txtYear, is_Renew, txtRemark, txtUploadFile, btnUploadFile, btnDownload)

                    'If Not chkIsDeduct.Checked Then
                    '    chkIsDeduct_CheckedChanged(Nothing, Nothing)
                    'End If
                    'chkIsDeduct.AutoPostBack = True
                    EnabledGridNotPostback(rgCetificate, False)
                    EnabledGridNotPostback(rgCetificateEdit, False)
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
                    'dung
                    ClearControlValue(cbField, rdFromDate, rdToDate, txtSchool, cbMajor, cbLevel,
                                     txtMark, txtContentTrain, txtTypeTrain, txtCodeCertificate, rdEffectFrom,
                                     rdEffectTo, txtClassification, txtYear, is_Renew, txtRemark, txtUploadFile, btnUploadFile, btnDownload)
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim isInsert As Boolean = True

                        Dim obj As New CETIFICATE_EDITDTO
                        obj.EMPLOYEE_ID = EmployeeID
                        If cbField.SelectedValue <> "" Then
                            obj.FIELD = cbField.SelectedValue
                        End If
                        obj.FROM_DATE = rdFromDate.SelectedDate
                        obj.TO_DATE = rdToDate.SelectedDate
                        obj.SCHOOL_NAME = txtSchool.Text
                        If cbMajor.SelectedValue <> "" Then
                            obj.MAJOR = cbMajor.SelectedValue
                        End If
                        If cbLevel.SelectedValue <> "" Then
                            obj.LEVEL = cbLevel.SelectedValue
                        End If
                        obj.MARK = txtMark.Text
                        obj.CONTENT_NAME = txtContentTrain.Text
                        obj.TYPE_NAME = txtTypeTrain.Text
                        obj.CODE_CERTIFICATE = txtCodeCertificate.Text
                        obj.EFFECT_FROM = rdEffectFrom.SelectedDate
                        obj.EFFECT_TO = rdEffectTo.SelectedDate
                        obj.CLASSIFICATION = txtClassification.Text
                        obj.YEAR = txtYear.Text
                        obj.RENEW = is_Renew.Checked
                        obj.REMARK = txtRemark.Text

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

                            'If isInsert Then
                            '    rep.InsertEmployeeFamilyEdit(obj, 0)
                            'Else
                            '    obj.ID = hidID.Value
                            '    rep.ModifyEmployeeFamilyEdit(obj, 0)
                            'End If

                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgCetificate.Rebind()
                            rgCetificateEdit.Rebind()
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                        End Using
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL

                    If rgCetificateEdit.SelectedItems.Count > 0 Then
                        Dim item = CType(rgCetificateEdit.SelectedItems(rgCetificateEdit.SelectedItems.Count - 1), GridDataItem)
                        hidFamilyID.Value = item.GetDataKeyValue("ID")
                        'txtAdress.Text = item.GetDataKeyValue("ADDRESS")
                        'txtIDNO.Text = item.GetDataKeyValue("ID_NO")
                        'txtFullName.Text = item.GetDataKeyValue("FULLNAME")
                        'txtTax.Text = item.GetDataKeyValue("TAXTATION")
                        'txtRemark.Text = item.GetDataKeyValue("REMARK")
                        'rdBirthDate.SelectedDate = item.GetDataKeyValue("BIRTH_DATE")
                        'chkIsDeduct.Checked = item.GetDataKeyValue("IS_DEDUCT")
                        'rdDeductReg.SelectedDate = item.GetDataKeyValue("DEDUCT_REG")
                        'rdDeductFrom.SelectedDate = item.GetDataKeyValue("DEDUCT_FROM")
                        'rdDeductTo.SelectedDate = item.GetDataKeyValue("DEDUCT_TO")
                        'cboRelationship.SelectedValue = item.GetDataKeyValue("RELATION_ID")
                        'txtCareer.Text = item.GetDataKeyValue("CAREER")
                        'txtTitle.Text = item.GetDataKeyValue("TITLE_NAME")
                        'cboNguyenQuan.SelectedValue = item.GetDataKeyValue("PROVINCE_ID")
                        'txtHouseCertificate_Code.Text = item.GetDataKeyValue("CERTIFICATE_CODE")
                        'txtHouseCertificate_Num.Text = item.GetDataKeyValue("CERTIFICATE_NUM")
                        'txtTempAdress.Text = item.GetDataKeyValue("ADDRESS_TT")
                        'cbPROVINCE_ID.SelectedValue = item.GetDataKeyValue("AD_PROVINCE_ID")
                        'cbDISTRICT_ID.SelectedValue = item.GetDataKeyValue("AD_DISTRICT_ID")
                        'cbWARD_ID.SelectedValue = item.GetDataKeyValue("AD_WARD_ID")
                        'txtAD_Village.Text = item.GetDataKeyValue("AD_VILLAGE")
                        'cbTempPROVINCE_ID.SelectedValue = item.GetDataKeyValue("TT_PROVINCE_ID")
                        'cbTempDISTRICT_ID.SelectedValue = item.GetDataKeyValue("TT_DISTRICT_ID")
                        'cbTempWARD_ID.SelectedValue = item.GetDataKeyValue("TT_WARD_ID")
                        'chkIs_Owner.Checked = item.GetDataKeyValue("IS_OWNER")
                        'chkIs_Pass.Checked = item.GetDataKeyValue("IS_PASS")
                        If item.GetDataKeyValue("FK_PKEY") IsNot Nothing Then
                            hidFamilyID.Value = item.GetDataKeyValue("FK_PKEY")
                        End If
                        hidID.Value = item.GetDataKeyValue("ID")
                        ' chkIsDeduct_CheckedChanged(Nothing, Nothing)
                    Else
                        'dung
                        ClearControlValue(cbField, rdFromDate, rdToDate, txtSchool, cbMajor, cbLevel,
                                     txtMark, txtContentTrain, txtTypeTrain, txtCodeCertificate, rdEffectFrom,
                                     rdEffectTo, txtClassification, txtYear, is_Renew, txtRemark, txtUploadFile, btnUploadFile, btnDownload)
                    End If


                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_SUBMIT
                    If rgCetificateEdit.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim status As String = ""
                    For Each item As GridDataItem In rgCetificateEdit.SelectedItems
                        status = item.GetDataKeyValue("STATUS")
                        If status = 1 Then
                            ShowMessage("Thông tin đang Chờ phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                            Exit Sub
                        End If
                        If status = 2 Then
                            ShowMessage("Thông tin đang phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                            Exit Sub
                        End If
                        'If status = 3 Then
                        '   ShowMessage("Thông tin đang không phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                        '   Exit Sub
                        'End If
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

    Private Sub rgCetificate_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCetificate.NeedDataSource
        Try
            SetValueObjectByRadGrid(rgCetificate, New CETIFICATEDTO)

            Using rep As New ProfileBusinessRepository
                rgCetificate.DataSource = rep.GetCertificate(New CETIFICATEDTO With {.EMPLOYEE_ID = EmployeeID})
            End Using
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgCetificateEdit_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCetificateEdit.NeedDataSource
        Try
            SetValueObjectByRadGrid(rgCetificateEdit, New CETIFICATE_EDITDTO)
            Using rep As New ProfileBusinessRepository
                rgCetificateEdit.DataSource = rep.GetCertificateEdit(New CETIFICATE_EDITDTO With {.EMPLOYEE_ID = EmployeeID})
            End Using
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgCetificateEdit_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgCetificateEdit.ItemCommand
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
                'txtAdress.Text = item.GetDataKeyValue("ADDRESS")
                'txtIDNO.Text = item.GetDataKeyValue("ID_NO")
                'txtFullName.Text = item.GetDataKeyValue("FULLNAME")
                'txtTax.Text = item.GetDataKeyValue("TAXTATION")
                'txtRemark.Text = item.GetDataKeyValue("REMARK")
                'rdBirthDate.SelectedDate = item.GetDataKeyValue("BIRTH_DATE")
                'chkIsDeduct.Checked = item.GetDataKeyValue("IS_DEDUCT")
                'rdDeductReg.SelectedDate = item.GetDataKeyValue("DEDUCT_REG")
                'rdDeductFrom.SelectedDate = item.GetDataKeyValue("DEDUCT_FROM")
                'rdDeductTo.SelectedDate = item.GetDataKeyValue("DEDUCT_TO")
                'cboRelationship.SelectedValue = item.GetDataKeyValue("RELATION_ID")
                'txtCareer.Text = item.GetDataKeyValue("CAREER")
                'txtTitle.Text = item.GetDataKeyValue("TITLE_NAME")
                'cboNguyenQuan.SelectedValue = item.GetDataKeyValue("PROVINCE_ID")
                'txtHouseCertificate_Code.Text = item.GetDataKeyValue("CERTIFICATE_CODE")
                'txtHouseCertificate_Num.Text = item.GetDataKeyValue("CERTIFICATE_NUM")
                'txtTempAdress.Text = item.GetDataKeyValue("ADDRESS_TT")
                'txtAD_Village.Text = item.GetDataKeyValue("AD_VILLAGE")
                'If IsNumeric(item.GetDataKeyValue("AD_PROVINCE_ID")) Then
                '    cbPROVINCE_ID.SelectedValue = item.GetDataKeyValue("AD_PROVINCE_ID")
                'End If
                'If IsNumeric(item.GetDataKeyValue("TT_PROVINCE_ID")) Then
                '    cbTempPROVINCE_ID.SelectedValue = item.GetDataKeyValue("TT_PROVINCE_ID")
                'End If
                'Using rep As New ProfileRepository
                '    If cbPROVINCE_ID.SelectedValue <> "" Then
                '        Dim dt As DataTable = rep.GetDistrictList(cbPROVINCE_ID.SelectedValue, False)
                '        FillRadCombobox(cbDISTRICT_ID, dt, "NAME", "ID")
                '    End If
                '    If IsNumeric(item.GetDataKeyValue("AD_DISTRICT_ID")) Then
                '        cbDISTRICT_ID.SelectedValue = item.GetDataKeyValue("AD_DISTRICT_ID")
                '    End If
                '    If cbDISTRICT_ID.SelectedValue <> "" Then
                '        Dim dt As DataTable = rep.GetWardList(cbDISTRICT_ID.SelectedValue, False)
                '        FillRadCombobox(cbWARD_ID, dt, "NAME", "ID")
                '    End If
                '    If IsNumeric(item.GetDataKeyValue("AD_WARD_ID")) Then
                '        cbWARD_ID.SelectedValue = item.GetDataKeyValue("AD_WARD_ID")
                '    End If
                '    If cbTempPROVINCE_ID.SelectedValue <> "" Then
                '        Dim dt As DataTable = rep.GetDistrictList(cbTempPROVINCE_ID.SelectedValue, False)
                '        FillRadCombobox(cbTempDISTRICT_ID, dt, "NAME", "ID")
                '    End If
                '    If IsNumeric(item.GetDataKeyValue("TT_DISTRICT_ID")) Then
                '        cbTempDISTRICT_ID.SelectedValue = item.GetDataKeyValue("TT_DISTRICT_ID")
                '    End If
                '    If cbTempDISTRICT_ID.SelectedValue <> "" Then
                '        Dim dt As DataTable = rep.GetWardList(cbTempDISTRICT_ID.SelectedValue, False)
                '        FillRadCombobox(cbTempWARD_ID, dt, "NAME", "ID")
                '    End If
                '    If IsNumeric(item.GetDataKeyValue("TT_WARD_ID")) Then
                '        cbTempWARD_ID.SelectedValue = item.GetDataKeyValue("TT_WARD_ID")
                '    End If
                'End Using
                'chkIs_Owner.Checked = item.GetDataKeyValue("IS_OWNER")
                'chkIs_Pass.Checked = item.GetDataKeyValue("IS_PASS")
                If item.GetDataKeyValue("FK_PKEY") IsNot Nothing Then
                    hidFamilyID.Value = item.GetDataKeyValue("FK_PKEY")
                End If
                hidID.Value = item.GetDataKeyValue("ID")
                'chkIsDeduct_CheckedChanged(Nothing, Nothing)
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgCetificate_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgCetificate.ItemCommand
        Try
            If e.CommandName = "EditRow" Then
                CurrentState = CommonMessage.STATE_EDIT
                Dim item = CType(e.Item, GridDataItem)
                hidFamilyID.Value = item.GetDataKeyValue("ID")
                'txtAdress.Text = item.GetDataKeyValue("ADDRESS")
                'txtIDNO.Text = item.GetDataKeyValue("ID_NO")
                'txtFullName.Text = item.GetDataKeyValue("FULLNAME")
                'txtTax.Text = item.GetDataKeyValue("TAXTATION")
                'txtRemark.Text = item.GetDataKeyValue("REMARK")
                'rdBirthDate.SelectedDate = item.GetDataKeyValue("BIRTH_DATE")
                'chkIsDeduct.Checked = item.GetDataKeyValue("IS_DEDUCT")
                'rdDeductReg.SelectedDate = item.GetDataKeyValue("DEDUCT_REG")
                'rdDeductFrom.SelectedDate = item.GetDataKeyValue("DEDUCT_FROM")
                'rdDeductTo.SelectedDate = item.GetDataKeyValue("DEDUCT_TO")
                'cboRelationship.SelectedValue = item.GetDataKeyValue("RELATION_ID")
                'txtCareer.Text = item.GetDataKeyValue("CAREER")
                'txtTitle.Text = item.GetDataKeyValue("TITLE_NAME")
                'cboNguyenQuan.SelectedValue = item.GetDataKeyValue("PROVINCE_ID")
                'txtHouseCertificate_Code.Text = item.GetDataKeyValue("CERTIFICATE_CODE")
                'txtHouseCertificate_Num.Text = item.GetDataKeyValue("CERTIFICATE_NUM")
                'txtTempAdress.Text = item.GetDataKeyValue("ADDRESS_TT")
                'txtAD_Village.Text = item.GetDataKeyValue("AD_VILLAGE")
                'If IsNumeric(item.GetDataKeyValue("AD_PROVINCE_ID")) Then
                '    cbPROVINCE_ID.SelectedValue = item.GetDataKeyValue("AD_PROVINCE_ID")
                'End If
                'If IsNumeric(item.GetDataKeyValue("TT_PROVINCE_ID")) Then
                '    cbTempPROVINCE_ID.SelectedValue = item.GetDataKeyValue("TT_PROVINCE_ID")
                'End If
                'Using rep As New ProfileRepository
                '    If cbPROVINCE_ID.SelectedValue <> "" Then
                '        Dim dt As DataTable = rep.GetDistrictList(cbPROVINCE_ID.SelectedValue, False)
                '        FillRadCombobox(cbDISTRICT_ID, dt, "NAME", "ID")
                '    End If
                '    If IsNumeric(item.GetDataKeyValue("AD_DISTRICT_ID")) Then
                '        cbDISTRICT_ID.SelectedValue = item.GetDataKeyValue("AD_DISTRICT_ID")
                '    End If
                '    If cbDISTRICT_ID.SelectedValue <> "" Then
                '        Dim dt As DataTable = rep.GetWardList(cbDISTRICT_ID.SelectedValue, False)
                '        FillRadCombobox(cbWARD_ID, dt, "NAME", "ID")
                '    End If
                '    If IsNumeric(item.GetDataKeyValue("AD_WARD_ID")) Then
                '        cbWARD_ID.SelectedValue = item.GetDataKeyValue("AD_WARD_ID")
                '    End If
                '    If cbTempPROVINCE_ID.SelectedValue <> "" Then
                '        Dim dt As DataTable = rep.GetDistrictList(cbTempPROVINCE_ID.SelectedValue, False)
                '        FillRadCombobox(cbTempDISTRICT_ID, dt, "NAME", "ID")
                '    End If
                '    If IsNumeric(item.GetDataKeyValue("TT_DISTRICT_ID")) Then
                '        cbTempDISTRICT_ID.SelectedValue = item.GetDataKeyValue("TT_DISTRICT_ID")
                '    End If
                '    If cbTempDISTRICT_ID.SelectedValue <> "" Then
                '        Dim dt As DataTable = rep.GetWardList(cbTempDISTRICT_ID.SelectedValue, False)
                '        FillRadCombobox(cbTempWARD_ID, dt, "NAME", "ID")
                '    End If
                '    If IsNumeric(item.GetDataKeyValue("TT_WARD_ID")) Then
                '        cbTempWARD_ID.SelectedValue = item.GetDataKeyValue("TT_WARD_ID")
                '    End If
                'End Using
                'chkIs_Owner.Checked = item.GetDataKeyValue("IS_OWNER")
                'chkIs_Pass.Checked = item.GetDataKeyValue("IS_PASS")
                hidFamilyID.Value = item.GetDataKeyValue("ID")
                hidID.Value = ""
                ' chkIsDeduct_CheckedChanged(Nothing, Nothing)
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    'Protected Sub chkIsDeduct_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkIsDeduct.CheckedChanged
    '    rdDeductFrom.Enabled = chkIsDeduct.Checked
    '    rdDeductTo.Enabled = chkIsDeduct.Checked
    'End Sub

    'Protected Sub chkIsOwner_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkIs_Owner.CheckedChanged
    '    Try
    '        If CurrentState = CommonMessage.STATE_NEW Then
    '            If chkIs_Owner.Checked Then
    '                txtHouseCertificate_Num.ReadOnly = False
    '                txtHouseCertificate_Code.ReadOnly = False
    '            Else
    '                txtHouseCertificate_Num.ReadOnly = True
    '                txtHouseCertificate_Code.ReadOnly = True
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub

    'Protected Sub chkIs_Pass_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkIs_Pass.CheckedChanged
    '    If chkIs_Pass.Checked Then
    '        chkIsDeduct.Checked = False
    '        chkIsDeduct.Enabled = False
    '    Else
    '        chkIsDeduct.Enabled = True
    '    End If
    'End Sub

    'Private Sub cvalRelationship_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalRelationship.ServerValidate
    '    Try
    '        If cboRelationship.SelectedValue <> "" Then
    '            args.IsValid = True
    '        Else
    '            args.IsValid = False
    '        End If
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Using rep As New ProfileBusinessRepository
                    Dim lstID As New List(Of Decimal)
                    For Each item As GridDataItem In rgCetificateEdit.SelectedItems
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next
                    If lstID.Count > 0 Then
                        rep.SendEmployeeFamilyEdit(lstID)
                    End If
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgCetificate.Rebind()
                    rgCetificateEdit.Rebind()
                End Using
            End If


        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

#End Region

    
  

  
    

    Protected Sub rgFamilyEdit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rgCetificateEdit.SelectedIndexChanged
        Try
            'dung
            ClearControlValue(cbField, rdFromDate, rdToDate, txtSchool, cbMajor, cbLevel,
                                     txtMark, txtContentTrain, txtTypeTrain, txtCodeCertificate, rdEffectFrom,
                                     rdEffectTo, txtClassification, txtYear, is_Renew, txtRemark, txtUploadFile, btnUploadFile, btnDownload)

            Dim item = CType(rgCetificateEdit.SelectedItems(rgCetificateEdit.SelectedItems.Count - 1), GridDataItem)
            CurrentState = CommonMessage.STATE_NORMAL
            hidFamilyID.Value = item.GetDataKeyValue("ID")
            'txtAdress.Text = item.GetDataKeyValue("ADDRESS")
            'txtIDNO.Text = item.GetDataKeyValue("ID_NO")
            'txtFullName.Text = item.GetDataKeyValue("FULLNAME")
            'txtTax.Text = item.GetDataKeyValue("TAXTATION")
            'txtRemark.Text = item.GetDataKeyValue("REMARK")
            'rdBirthDate.SelectedDate = item.GetDataKeyValue("BIRTH_DATE")
            'chkIsDeduct.Checked = item.GetDataKeyValue("IS_DEDUCT")
            'rdDeductReg.SelectedDate = item.GetDataKeyValue("DEDUCT_REG")
            'rdDeductFrom.SelectedDate = item.GetDataKeyValue("DEDUCT_FROM")
            'rdDeductTo.SelectedDate = item.GetDataKeyValue("DEDUCT_TO")
            'cboRelationship.SelectedValue = item.GetDataKeyValue("RELATION_ID")
            'txtCareer.Text = item.GetDataKeyValue("CAREER")
            'txtTitle.Text = item.GetDataKeyValue("TITLE_NAME")
            'cboNguyenQuan.SelectedValue = item.GetDataKeyValue("PROVINCE_ID")
            'txtHouseCertificate_Code.Text = item.GetDataKeyValue("CERTIFICATE_CODE")
            'txtHouseCertificate_Num.Text = item.GetDataKeyValue("CERTIFICATE_NUM")
            'txtTempAdress.Text = item.GetDataKeyValue("ADDRESS_TT")
            'If IsNumeric(item.GetDataKeyValue("AD_PROVINCE_ID")) Then
            '    cbPROVINCE_ID.SelectedValue = item.GetDataKeyValue("AD_PROVINCE_ID")
            'End If
            'If IsNumeric(item.GetDataKeyValue("TT_PROVINCE_ID")) Then
            '    cbTempPROVINCE_ID.SelectedValue = item.GetDataKeyValue("TT_PROVINCE_ID")
            'End If
            'Using rep As New ProfileRepository
            '    If cbPROVINCE_ID.SelectedValue <> "" Then
            '        Dim dt As DataTable = rep.GetDistrictList(cbPROVINCE_ID.SelectedValue, False)
            '        FillRadCombobox(cbDISTRICT_ID, dt, "NAME", "ID")
            '    End If
            '    If IsNumeric(item.GetDataKeyValue("AD_DISTRICT_ID")) Then
            '        cbDISTRICT_ID.SelectedValue = item.GetDataKeyValue("AD_DISTRICT_ID")
            '    End If
            '    If cbDISTRICT_ID.SelectedValue <> "" Then
            '        Dim dt As DataTable = rep.GetWardList(cbDISTRICT_ID.SelectedValue, False)
            '        FillRadCombobox(cbWARD_ID, dt, "NAME", "ID")
            '    End If
            '    If IsNumeric(item.GetDataKeyValue("AD_WARD_ID")) Then
            '        cbWARD_ID.SelectedValue = item.GetDataKeyValue("AD_WARD_ID")
            '    End If
            '    If cbTempPROVINCE_ID.SelectedValue <> "" Then
            '        Dim dt As DataTable = rep.GetDistrictList(cbTempPROVINCE_ID.SelectedValue, False)
            '        FillRadCombobox(cbTempDISTRICT_ID, dt, "NAME", "ID")
            '    End If
            '    If IsNumeric(item.GetDataKeyValue("TT_DISTRICT_ID")) Then
            '        cbTempDISTRICT_ID.SelectedValue = item.GetDataKeyValue("TT_DISTRICT_ID")
            '    End If
            '    If cbTempDISTRICT_ID.SelectedValue <> "" Then
            '        Dim dt As DataTable = rep.GetWardList(cbTempDISTRICT_ID.SelectedValue, False)
            '        FillRadCombobox(cbTempWARD_ID, dt, "NAME", "ID")
            '    End If
            '    If IsNumeric(item.GetDataKeyValue("TT_WARD_ID")) Then
            '        cbTempWARD_ID.SelectedValue = item.GetDataKeyValue("TT_WARD_ID")
            '    End If
            'End Using
            'txtAD_Village.Text = item.GetDataKeyValue("AD_VILLAGE")
            'chkIs_Owner.Checked = item.GetDataKeyValue("IS_OWNER")
            'chkIs_Pass.Checked = item.GetDataKeyValue("IS_PASS")
            If item.GetDataKeyValue("FK_PKEY") IsNot Nothing Then
                hidFamilyID.Value = item.GetDataKeyValue("FK_PKEY")
            End If
            hidID.Value = item.GetDataKeyValue("ID")
            'chkIsDeduct_CheckedChanged(Nothing, Nothing)
            UpdateControlState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rgCetificate_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rgCetificate.SelectedIndexChanged
        Try
            If rgCetificate.SelectedItems.Count = 0 Then
                'dung
                ClearControlValue(cbField, rdFromDate, rdToDate, txtSchool, cbMajor, cbLevel,
                                     txtMark, txtContentTrain, txtTypeTrain, txtCodeCertificate, rdEffectFrom,
                                     rdEffectTo, txtClassification, txtYear, is_Renew, txtRemark, txtUploadFile, btnUploadFile, btnDownload)
                Exit Sub
            End If
            'dung
            ClearControlValue(cbField, rdFromDate, rdToDate, txtSchool, cbMajor, cbLevel,
                                     txtMark, txtContentTrain, txtTypeTrain, txtCodeCertificate, rdEffectFrom,
                                     rdEffectTo, txtClassification, txtYear, is_Renew, txtRemark, txtUploadFile, btnUploadFile, btnDownload)
            CurrentState = CommonMessage.STATE_NORMAL
            Dim item = CType(rgCetificate.SelectedItems(rgCetificate.SelectedItems.Count - 1), GridDataItem)
            hidFamilyID.Value = item.GetDataKeyValue("ID")
            'txtAdress.Text = item.GetDataKeyValue("ADDRESS")
            'txtIDNO.Text = item.GetDataKeyValue("ID_NO")
            'txtFullName.Text = item.GetDataKeyValue("FULLNAME")
            'txtTax.Text = item.GetDataKeyValue("TAXTATION")
            'txtRemark.Text = item.GetDataKeyValue("REMARK")
            'rdBirthDate.SelectedDate = item.GetDataKeyValue("BIRTH_DATE")
            'chkIsDeduct.Checked = item.GetDataKeyValue("IS_DEDUCT")
            'rdDeductReg.SelectedDate = item.GetDataKeyValue("DEDUCT_REG")
            'rdDeductFrom.SelectedDate = item.GetDataKeyValue("DEDUCT_FROM")
            'rdDeductTo.SelectedDate = item.GetDataKeyValue("DEDUCT_TO")
            'cboRelationship.SelectedValue = item.GetDataKeyValue("RELATION_ID")
            'txtCareer.Text = item.GetDataKeyValue("CAREER")
            'txtTitle.Text = item.GetDataKeyValue("TITLE_NAME")
            'cboNguyenQuan.SelectedValue = item.GetDataKeyValue("PROVINCE_ID")
            'txtHouseCertificate_Code.Text = item.GetDataKeyValue("CERTIFICATE_CODE")
            'txtHouseCertificate_Num.Text = item.GetDataKeyValue("CERTIFICATE_NUM")
            'txtTempAdress.Text = item.GetDataKeyValue("ADDRESS_TT")
            'txtAD_Village.Text = item.GetDataKeyValue("AD_VILLAGE")
            'Using rep As New ProfileRepository
            '    If cbPROVINCE_ID.SelectedValue <> "" Then
            '        Dim dt As DataTable = rep.GetDistrictList(cbPROVINCE_ID.SelectedValue, False)
            '        FillRadCombobox(cbDISTRICT_ID, dt, "NAME", "ID")
            '    End If
            '    If IsNumeric(item.GetDataKeyValue("AD_DISTRICT_ID")) Then
            '        cbDISTRICT_ID.SelectedValue = item.GetDataKeyValue("AD_DISTRICT_ID")
            '    End If
            '    If cbDISTRICT_ID.SelectedValue <> "" Then
            '        Dim dt As DataTable = rep.GetWardList(cbDISTRICT_ID.SelectedValue, False)
            '        FillRadCombobox(cbWARD_ID, dt, "NAME", "ID")
            '    End If
            '    If IsNumeric(item.GetDataKeyValue("AD_WARD_ID")) Then
            '        cbWARD_ID.SelectedValue = item.GetDataKeyValue("AD_WARD_ID")
            '    End If
            '    If cbTempPROVINCE_ID.SelectedValue <> "" Then
            '        Dim dt As DataTable = rep.GetDistrictList(cbTempPROVINCE_ID.SelectedValue, False)
            '        FillRadCombobox(cbTempDISTRICT_ID, dt, "NAME", "ID")
            '    End If
            '    If IsNumeric(item.GetDataKeyValue("TT_DISTRICT_ID")) Then
            '        cbTempDISTRICT_ID.SelectedValue = item.GetDataKeyValue("TT_DISTRICT_ID")
            '    End If
            '    If cbTempDISTRICT_ID.SelectedValue <> "" Then
            '        Dim dt As DataTable = rep.GetWardList(cbTempDISTRICT_ID.SelectedValue, False)
            '        FillRadCombobox(cbTempWARD_ID, dt, "NAME", "ID")
            '    End If
            '    If IsNumeric(item.GetDataKeyValue("TT_WARD_ID")) Then
            '        cbTempWARD_ID.SelectedValue = item.GetDataKeyValue("TT_WARD_ID")
            '    End If
            'End Using
            'chkIs_Owner.Checked = item.GetDataKeyValue("IS_OWNER")
            'chkIs_Pass.Checked = item.GetDataKeyValue("IS_PASS")
            hidFamilyID.Value = item.GetDataKeyValue("ID")
            hidID.Value = ""
            'chkIsDeduct_CheckedChanged(Nothing, Nothing)
            UpdateControlState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class

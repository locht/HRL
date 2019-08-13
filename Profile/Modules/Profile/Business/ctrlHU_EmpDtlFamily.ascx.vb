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
        'If Not IsPostBack Then
        '    ViewConfig(RightPane)
        '    GirdConfig(rgFamily)
        'End If
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
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
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
                    ComboBoxDataDTO.GET_DISTRICT = True
                    ComboBoxDataDTO.GET_NATION = True
                    rep.GetComboList(ComboBoxDataDTO)
                End If

                If ComboBoxDataDTO IsNot Nothing Then
                    FillDropDownList(cboRelationship, ComboBoxDataDTO.LIST_RELATION, "NAME", "ID", Common.Common.SystemLanguage, True, cboRelationship.SelectedValue)
                    FillDropDownList(cboNguyenQuan, ComboBoxDataDTO.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboNguyenQuan.SelectedValue)
                    FillDropDownList(cboProvince_City1, ComboBoxDataDTO.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboProvince_City1.SelectedValue)
                    'FillDropDownList(cboDistrict1, ComboBoxDataDTO.LIST_DISTRICT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboDistrict1.SelectedValue)
                    ''FillDropDownList(cboCommune1, ComboBoxDataDTO.LIST_NATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboCommune1.SelectedValue)
                    FillDropDownList(cboProvince_City2, ComboBoxDataDTO.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboProvince_City2.SelectedValue)
                    'FillDropDownList(cboDistrict2, ComboBoxDataDTO.LIST_DISTRICT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboDistrict2.SelectedValue)
                    'FillDropDownList(cboCommune1, ComboBoxDataDTO.LIST_NATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboCommune1.SelectedValue)

                    FillDropDownList(cbTempKtPROVINCE_ID, ComboBoxDataDTO.LIST_PROVINCE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cbTempKtPROVINCE_ID.SelectedValue)
                    FillDropDownList(cboNationlity, ComboBoxDataDTO.LIST_NATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboNationlity.SelectedValue)
                    FillDropDownList(cboNATIONALITYFAMILY, ComboBoxDataDTO.LIST_NATION, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboNATIONALITYFAMILY.SelectedValue)

                    cboNationlity.SelectedValue = 244
                    cboNATIONALITYFAMILY.SelectedValue = 244
                End If

                If Not IsPostBack Then
                    cboGender.DataSource = rep.GetOtherList("GENDER", True)
                    cboGender.DataTextField = "NAME"
                    cboGender.DataValueField = "ID"
                    cboGender.DataBind()
                End If

                'Dim dtPlace
                'dtPlace = rep.GetProvinceList(True)
                ''FillRadCombobox(cboProvince_City1, dtPlace, "NAME", "ID")
                'FillRadCombobox(cboProvince_City2, dtPlace, "NAME", "ID")
               
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


                    If rgFamily.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgFamily.SelectedItems.Count > 1 Then
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
                                    ClearControlValue(txtFullName, cboNguyenQuan, cboRelationship, rdBirthDate, txtIDNO, txtAdress1, txtAdress_TT,
                                                        chkIsDeduct, rdDeductReg, rdDeductFrom, rdDeductTo, txtRemark, txtTax, txtCareer, txtTitle,
                                                         txtSoHoKhau, txtMaHoGiaDinh, cboProvince_City1, cboDistrict1, cboCommune1,
                                                          cboProvince_City2, cboDistrict2, cboCommune2, txtHamlet1, chkHousehold, chkDaMat)
                                    rgFamily.Rebind()
                                    ExcuteScript("Clear", "clRadDatePicker()")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case STATE_EDIT
                                If Execute() Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    ClearControlValue(txtFullName, cboNguyenQuan, cboRelationship, rdBirthDate, txtIDNO, txtAdress1, txtAdress_TT,
                                                        chkIsDeduct, rdDeductReg, rdDeductFrom, rdDeductTo, txtRemark, txtTax, txtCareer, txtTitle,
                                                         txtSoHoKhau, txtMaHoGiaDinh, cboProvince_City1, cboDistrict1, cboCommune1,
                                                          cboProvince_City2, cboDistrict2, cboCommune2, txtHamlet1, chkHousehold, chkDaMat)
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
            Dim itemSelected = New List(Of Decimal)
            For Each dr As Telerik.Web.UI.GridDataItem In rgFamily.SelectedItems
                itemSelected.Add(dr.GetDataKeyValue("ID"))
            Next
            'Dim item As GridDataItem = rgFamily.SelectedItems(0)
            Dim item = CType(rgFamily.SelectedItems(rgFamily.SelectedItems.Count - 1), GridDataItem)
            hidFamilyID.Value = item.GetDataKeyValue("ID")
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

            txtAdress1.Text = item.GetDataKeyValue("ADDRESS")
            txtAdress_TT.Text = item.GetDataKeyValue("ADDRESS_TT")
            txtMaHoGiaDinh.Text = item.GetDataKeyValue("CERTIFICATE_CODE")
            txtSoHoKhau.Text = item.GetDataKeyValue("CERTIFICATE_NUM")
            txtHamlet1.Text = item.GetDataKeyValue("AD_VILLAGE")
            chkHousehold.Checked = item.GetDataKeyValue("IS_OWNER")
            chkDaMat.Checked = item.GetDataKeyValue("IS_PASS")

            

            Using rep As New ProfileRepository
                If IsNumeric(item.GetDataKeyValue("AD_PROVINCE_ID")) Then
                    Dim dt As DataTable = rep.GetProvinceList(False)
                    FillRadCombobox(cboProvince_City1, dt, "NAME", "ID")
                    cboProvince_City1.SelectedValue = item.GetDataKeyValue("AD_PROVINCE_ID")
                    cboProvince_City1.Text = item.GetDataKeyValue("AD_PROVINCE_NAME")
                End If
                If IsNumeric(item.GetDataKeyValue("TT_PROVINCE_ID")) Then
                    Dim dt As DataTable = rep.GetProvinceList(False)
                    FillRadCombobox(cboProvince_City2, dt, "NAME", "ID")
                    cboProvince_City2.SelectedValue = item.GetDataKeyValue("TT_PROVINCE_ID")
                    cboProvince_City2.Text = item.GetDataKeyValue("TT_PROVINCE_NAME")
                End If
                If cboProvince_City1.SelectedValue <> "" Then
                    Dim dt As DataTable = rep.GetDistrictList(cboProvince_City1.SelectedValue, False)
                    FillRadCombobox(cboDistrict1, dt, "NAME", "ID")
                End If
                If IsNumeric(item.GetDataKeyValue("AD_DISTRICT_ID")) Then
                    cboDistrict1.SelectedValue = item.GetDataKeyValue("AD_DISTRICT_ID")
                    cboDistrict1.Text = item.GetDataKeyValue("AD_DISTRICT_NAME")
                End If
                If cboDistrict1.SelectedValue <> "" Then
                    Dim dt As DataTable = rep.GetWardList(cboDistrict1.SelectedValue, False)
                    FillRadCombobox(cboCommune1, dt, "NAME", "ID")
                End If
                If IsNumeric(item.GetDataKeyValue("AD_WARD_ID")) Then
                    cboCommune1.SelectedValue = item.GetDataKeyValue("AD_WARD_ID")
                    cboCommune1.Text = item.GetDataKeyValue("AD_WARD_NAME")
                End If
                If cboProvince_City2.SelectedValue <> "" Then
                    Dim dt As DataTable = rep.GetDistrictList(cboProvince_City2.SelectedValue, False)
                    FillRadCombobox(cboDistrict2, dt, "NAME", "ID")
                End If
                If IsNumeric(item.GetDataKeyValue("TT_DISTRICT_ID")) Then
                    cboDistrict2.SelectedValue = item.GetDataKeyValue("TT_DISTRICT_ID")
                    cboDistrict2.Text = item.GetDataKeyValue("TT_DISTRICT_NAME")
                End If
                If cboDistrict2.SelectedValue <> "" Then
                    Dim dt As DataTable = rep.GetWardList(cboDistrict2.SelectedValue, False)
                    FillRadCombobox(cboCommune2, dt, "NAME", "ID")
                End If
                If IsNumeric(item.GetDataKeyValue("TT_WARD_ID")) Then
                    cboCommune2.SelectedValue = item.GetDataKeyValue("TT_WARD_ID")
                    cboCommune2.Text = item.GetDataKeyValue("TT_WARD_NAME")
                End If

                If IsNumeric(item.GetDataKeyValue("NATION_ID")) Then
                    cboNationlity.SelectedValue = item.GetDataKeyValue("NATION_ID")
                End If

                If IsNumeric(item.GetDataKeyValue("BIRTH_NATION_ID")) Then
                    cboNATIONALITYFAMILY.SelectedValue = item.GetDataKeyValue("BIRTH_NATION_ID")
                End If

                If IsNumeric(item.GetDataKeyValue("BIRTH_PROVINCE_ID")) Then
                    cbTempKtPROVINCE_ID.SelectedValue = item.GetDataKeyValue("BIRTH_PROVINCE_ID")
                End If

                If IsNumeric(item.GetDataKeyValue("BIRTH_PROVINCE_ID")) Then
                    Dim dt As DataTable = rep.GetDistrictList(cbTempKtPROVINCE_ID.SelectedValue, False)
                    FillRadCombobox(cbTempKtDISTRICT_ID, dt, "NAME", "ID")
                End If

                If IsNumeric(item.GetDataKeyValue("BIRTH_DISTRICT_ID")) Then
                    cbTempKtDISTRICT_ID.SelectedValue = item.GetDataKeyValue("BIRTH_DISTRICT_ID")
                End If

                If IsNumeric(item.GetDataKeyValue("BIRTH_WARD_ID")) Then
                    Dim dt As DataTable = rep.GetWardList(cbTempKtDISTRICT_ID.SelectedValue, False)
                    FillRadCombobox(cbTempKtWARD_ID, dt, "NAME", "ID")
                    cbTempKtWARD_ID.SelectedValue = item.GetDataKeyValue("BIRTH_WARD_ID")
                End If

                cboGender.SelectedValue = item.GetDataKeyValue("GENDER")

                rdIDDate.SelectedDate = item.GetDataKeyValue("ID_NO_DATE")
                txtIDPlace.Text = item.GetDataKeyValue("ID_NO_PLACE_NAME")
                txtPhone.Text = item.GetDataKeyValue("PHONE")
                rdMSTDate.SelectedDate = item.GetDataKeyValue("TAXTATION_DATE")
                txt_MSTPLACE.Text = item.GetDataKeyValue("TAXTATION_PLACE")
                txtBIRTH_CODE.Text = item.GetDataKeyValue("BIRTH_CODE")
                txtQuyen.Text = item.GetDataKeyValue("QUYEN")
            End Using
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
                    ClearControlValue(txtFullName, cboNguyenQuan, cboRelationship, rdBirthDate, txtIDNO, txtAdress1, txtAdress_TT,
                                        chkIsDeduct, rdDeductReg, rdDeductFrom, rdDeductTo, txtRemark, txtTax, txtCareer, txtTitle, cboGender, rdIDDate, txtIDPlace, cboNationlity, txtPhone, rdMSTDate, txtBIRTH_CODE, cboNATIONALITYFAMILY, cbTempKtPROVINCE_ID, cbTempKtDISTRICT_ID, cbTempKtWARD_ID, txtQuyen, txt_MSTPLACE,
                                           txtSoHoKhau, txtMaHoGiaDinh, cboProvince_City1, cboDistrict1, cboCommune1,
                                                          cboProvince_City2, cboDistrict2, cboCommune2, txtHamlet1, chkHousehold, chkDaMat)
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                End If

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub


    Protected Sub cboCommon_ItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs) _
 Handles cboProvince_City1.ItemsRequested, cboDistrict1.ItemsRequested, cboCommune1.ItemsRequested,
        cboProvince_City2.ItemsRequested, cboDistrict2.ItemsRequested, cboCommune2.ItemsRequested,
        cbTempKtPROVINCE_ID.ItemsRequested, cbTempKtDISTRICT_ID.ItemsRequested, cbTempKtWARD_ID.ItemsRequested

        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Using rep As New ProfileRepository
                Dim dtData As DataTable
                Dim sText As String = e.Text
                Dim dValue As Decimal
                Dim sSelectValue As String = IIf(e.Context("value") IsNot Nothing, e.Context("value"), "")
                Select Case sender.ID
                    Case cboProvince_City1.ID, cboProvince_City2.ID, cbTempKtPROVINCE_ID.ID
                        dtData = rep.GetProvinceList(True)
                    Case cboDistrict1.ID, cboDistrict2.ID, cbTempKtDISTRICT_ID.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        dtData = rep.GetDistrictList(dValue, True)
                    Case cboCommune1.ID, cboCommune2.ID, cbTempKtWARD_ID.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        dtData = rep.GetWardList(dValue, True)
                End Select
                If sText <> "" Then
                    Dim dtExist = (From p In dtData
                                   Where p("NAME") IsNot DBNull.Value AndAlso
                                  p("NAME").ToString.ToUpper = sText.ToUpper)

                    'If dtExist.Count = 0 Then
                    Dim dtFilter = (From p In dtData
                                    Where p("NAME") IsNot DBNull.Value AndAlso
                              p("NAME").ToString.ToUpper.Contains(sText.ToUpper))

                    If dtFilter.Count > 0 Then
                        dtData = dtFilter.CopyToDataTable
                    Else
                        dtData = dtData.Clone
                    End If

                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                    e.EndOfItems = endOffset = dtData.Rows.Count
                    sender.Items.Clear()
                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                        'Select Case sender.ID
                        '    Case cboTitle.ID
                        '        radItem.Attributes("GROUP_NAME") = dtData.Rows(i)("GROUP_NAME").ToString()
                        'End Select
                        sender.Items.Add(radItem)
                    Next
                    'Else

                    '    Dim itemOffset As Integer = e.NumberOfItems
                    '    Dim endOffset As Integer = dtData.Rows.Count
                    '    e.EndOfItems = True
                    '    sender.Items.Clear()
                    '    For i As Integer = itemOffset To endOffset - 1
                    '        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                    '        Select Case sender.ID
                    '            Case cboTitle.ID
                    '                radItem.Attributes("GROUP_NAME") = dtData.Rows(i)("GROUP_NAME").ToString()
                    '        End Select
                    '        sender.Items.Add(radItem)
                    '    Next
                    'End If
                Else
                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                    e.EndOfItems = endOffset = dtData.Rows.Count
                    sender.Items.Clear()
                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                        'Select Case sender.ID
                        '    Case cboTitle.ID
                        '        radItem.Attributes("GROUP_NAME") = dtData.Rows(i)("GROUP_NAME").ToString()
                        'End Select
                        sender.Items.Add(radItem)
                    Next
                End If
            End Using

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub chkHousehold_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkHousehold.CheckedChanged
        If chkHousehold.Checked Then
            txtMaHoGiaDinh.Enabled = True
            txtSoHoKhau.Enabled = True
        Else
            txtMaHoGiaDinh.Enabled = False
            txtSoHoKhau.Enabled = False
        End If
    End Sub

    Private Sub chkIsDeduct_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkIsDeduct.CheckedChanged
        If chkIsDeduct.Checked Then
            rdDeductFrom.Enabled = True
            rdDeductTo.Enabled = True
            rdDeductReg.Enabled = True
            txtTax.Enabled = True
        Else
            rdDeductReg.Enabled = False
            rdDeductFrom.Enabled = False
            rdDeductTo.Enabled = False
            txtTax.Enabled = True
        End If
    End Sub

    Private Sub chkDaMat_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkDaMat.CheckedChanged
        If chkDaMat.Checked Then
            chkIsDeduct.Enabled = False
            rdDeductReg.Enabled = False
            rdDeductFrom.Enabled = False
            rdDeductTo.Enabled = False
            txtTax.Enabled = False
            txtTax.ClearValue()
            rdDeductFrom.ClearValue()
            rdDeductTo.ClearValue()
            chkIsDeduct.ClearValue()
            rdDeductReg.ClearValue()
        Else
            chkIsDeduct.Enabled = True

        End If
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
            objFamily.TAXTATION = txtTax.Text
            objFamily.DEDUCT_FROM = rdDeductFrom.SelectedDate()
            objFamily.DEDUCT_TO = rdDeductTo.SelectedDate()
            objFamily.FULLNAME = txtFullName.Text.Trim()
            objFamily.ID_NO = txtIDNO.Text.Trim()
            objFamily.IS_DEDUCT = chkIsDeduct.Checked
            objFamily.REMARK = txtRemark.Text.Trim()
            objFamily.CAREER = txtCareer.Text.Trim()
            objFamily.TITLE_NAME = txtTitle.Text.Trim()
            objFamily.AD_VILLAGE = txtHamlet1.Text.Trim()
            objFamily.CERTIFICATE_CODE = txtMaHoGiaDinh.Text
            objFamily.CERTIFICATE_NUM = txtSoHoKhau.Text
            objFamily.IS_OWNER = chkHousehold.Checked
            objFamily.IS_PASS = chkDaMat.Checked
            objFamily.ADDRESS = txtAdress1.Text
            objFamily.ADDRESS_TT = txtAdress_TT.Text

            If cboRelationship.SelectedValue <> "" Then
                objFamily.RELATION_ID = Decimal.Parse(cboRelationship.SelectedValue)
            End If
            If cboNguyenQuan.SelectedValue <> "" Then
                objFamily.PROVINCE_ID = Decimal.Parse(cboNguyenQuan.SelectedValue)
            End If

            If cboProvince_City1.SelectedValue <> "" Then
                objFamily.AD_PROVINCE_ID = Decimal.Parse(cboProvince_City1.SelectedValue)
            End If
            If cboDistrict1.SelectedValue <> "" Then
                objFamily.AD_DISTRICT_ID = Decimal.Parse(cboDistrict1.SelectedValue)
            End If
            If cboCommune1.SelectedValue <> "" Then
                objFamily.AD_WARD_ID = Decimal.Parse(cboCommune1.SelectedValue)
            End If
            If cboProvince_City2.SelectedValue <> "" Then
                objFamily.TT_PROVINCE_ID = Decimal.Parse(cboProvince_City2.SelectedValue)
            End If
            If cboDistrict2.SelectedValue <> "" Then
                objFamily.TT_DISTRICT_ID = Decimal.Parse(cboDistrict2.SelectedValue)
            End If
            If cboCommune2.SelectedValue <> "" Then
                objFamily.TT_WARD_ID = Decimal.Parse(cboCommune2.SelectedValue)
            End If


            If cboGender.SelectedValue <> "" Then
                objFamily.GENDER = cboGender.SelectedValue
            End If

            If cboNationlity.SelectedValue <> "" Then
                objFamily.NATION_ID = cboNationlity.SelectedValue
            End If

            objFamily.ID_NO_DATE = rdIDDate.SelectedDate
            objFamily.ID_NO_PLACE_NAME = txtIDPlace.Text
            objFamily.PHONE = txtPhone.Text
            objFamily.TAXTATION_DATE = rdMSTDate.SelectedDate
            objFamily.TAXTATION_PLACE = txt_MSTPLACE.Text
            objFamily.BIRTH_CODE = txtBIRTH_CODE.Text
            objFamily.QUYEN = txtQuyen.Text

            If cboNATIONALITYFAMILY.SelectedValue <> "" Then
                objFamily.BIRTH_NATION_ID = cboNATIONALITYFAMILY.SelectedValue
            End If

            If cbTempKtPROVINCE_ID.SelectedValue <> "" Then
                objFamily.BIRTH_PROVINCE_ID = cbTempKtPROVINCE_ID.SelectedValue
            End If

            If cbTempKtDISTRICT_ID.SelectedValue <> "" Then
                objFamily.BIRTH_DISTRICT_ID = cbTempKtDISTRICT_ID.SelectedValue
            End If

            If cbTempKtWARD_ID.SelectedValue <> "" Then
                objFamily.BIRTH_WARD_ID = cbTempKtWARD_ID.SelectedValue
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
        txtAdress1.ReadOnly = Not sTrangThai
        txtAdress_TT.ReadOnly = Not sTrangThai
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
        'Utilities.EnableRadDatePicker(rdDeductReg, sTrangThai)
        chkHousehold.Enabled = sTrangThai
        chkDaMat.Enabled = sTrangThai
        Utilities.ReadOnlyRadComBo(cboProvince_City1, Not sTrangThai)
        Utilities.ReadOnlyRadComBo(cboDistrict1, Not sTrangThai)
        Utilities.ReadOnlyRadComBo(cboCommune1, Not sTrangThai)
        Utilities.ReadOnlyRadComBo(cboProvince_City2, Not sTrangThai)
        Utilities.ReadOnlyRadComBo(cboDistrict2, Not sTrangThai)
        Utilities.ReadOnlyRadComBo(cboCommune2, Not sTrangThai)

        Utilities.ReadOnlyRadComBo(cboGender, Not sTrangThai)
        Utilities.ReadOnlyRadComBo(cboNationlity, Not sTrangThai)
        Utilities.EnableRadDatePicker(rdIDDate, sTrangThai)
        Utilities.EnableRadDatePicker(rdMSTDate, sTrangThai)
        txtIDPlace.ReadOnly = Not sTrangThai
        txtPhone.ReadOnly = Not sTrangThai
        txt_MSTPLACE.ReadOnly = Not sTrangThai
        txtBIRTH_CODE.ReadOnly = Not sTrangThai
        txtQuyen.ReadOnly = Not sTrangThai
        Utilities.ReadOnlyRadComBo(cboNATIONALITYFAMILY, Not sTrangThai)
        Utilities.ReadOnlyRadComBo(cbTempKtPROVINCE_ID, Not sTrangThai)
        Utilities.ReadOnlyRadComBo(cbTempKtDISTRICT_ID, Not sTrangThai)
        Utilities.ReadOnlyRadComBo(cbTempKtWARD_ID, Not sTrangThai)

        txtHamlet1.ReadOnly = Not sTrangThai
        SetStatusToolBar()
    End Sub

    Private Sub ResetControlValue()
        ClearControlValue(txtFullName, txtAdress1, txtAdress_TT, txtIDNO, txtRemark, txtTax, txtCareer, txtTitle,
                          hidFamilyID, chkIsDeduct, cboNguyenQuan, cboRelationship, cboGender, rdIDDate, txtIDPlace, cboNationlity, txtPhone, rdMSTDate, txtBIRTH_CODE, cboNATIONALITYFAMILY, cbTempKtPROVINCE_ID, cbTempKtDISTRICT_ID, cbTempKtWARD_ID, txtQuyen, txt_MSTPLACE,
                          rdBirthDate, rdDeductFrom, rdDeductReg, rdDeductTo,
                                                         txtSoHoKhau, txtMaHoGiaDinh, cboProvince_City1, cboDistrict1, cboCommune1,
                                                          cboProvince_City2, cboDistrict2, cboCommune2, txtHamlet1, chkHousehold, chkDaMat)
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
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Common
Imports Telerik.Web.UI
Imports Common.CommonBusiness
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports WebAppLog
Imports Ionic.Crc

Public Class ctrlLocation
    Inherits Common.CommonView

    'Popup
    Protected WithEvents ctrlFindEmployee As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindEmployee_Contract As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Private psp As New ProfileStoreProcedure()
    Dim log As UserLog = LogHelper.GetUserLog
    Private hfr As New HistaffFrameworkRepository

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\List" + Me.GetType().Name.ToString()

#Region "Property"
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    Property Location As LocationDTO
        Get
            Return ViewState(Me.ID & "_Location")
        End Get
        Set(ByVal value As LocationDTO)
            ViewState(Me.ID & "_Location") = value
        End Set
    End Property

    Property Locations As List(Of LocationDTO)
        Get
            Return ViewState(Me.ID & "_Locations")
        End Get
        Set(value As List(Of LocationDTO))
            ViewState(Me.ID & "_Locations") = value
        End Set
    End Property

    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    Property ItemList As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_ItemList")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_ItemList") = value
        End Set
    End Property

    Property ListComboData As Profile.ProfileBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Profile.ProfileBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    Property ComboData As Profile.ProfileBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ComboData")
        End Get
        Set(ByVal value As Profile.ProfileBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ComboData") = value
        End Set
    End Property

    Property dtData As DataTable
        Get
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    Property OldBankID As String
        Get
            Return ViewState(Me.ID & "_OldBankID")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_OldBankID") = value
        End Set
    End Property

    Property IsUpload As Decimal
        Get
            Return ViewState(Me.ID & "_IsUpload")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IsUpload") = value
        End Set
    End Property

    Property ActiveLocations As List(Of LocationDTO)
        Get
            Return ViewState(Me.ID & "_ActiveLocations")
        End Get
        Set(value As List(Of LocationDTO))
            ViewState(Me.ID & "_ActiveLocations") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            If Not IsPostBack Then
                'Định nghĩa Property
                InitProperty()

                'Định nghĩa ToolBar
                InitToolBar()

                GetDataCombo()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            UpdatePage()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    'Cập nhật lại trang
    Protected Sub UpdatePage(Optional ByVal Message As String = "")
        Dim rep As New ProfileRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If

            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    'CleanControl()
                    PaneMain.Enabled = False
                    PaneGrid.Enabled = True
                    'rgLocation.Rebind()
                Case CommonMessage.STATE_EDIT
                    PaneMain.Enabled = True
                    PaneGrid.Enabled = False
                Case CommonMessage.STATE_NEW
                    If isLoadPopup = 0 Then
                        'CleanControl()
                        PaneMain.Enabled = True
                    End If
                    PaneGrid.Enabled = False
                Case CommonMessage.STATE_DELETE
                Case CommonMessage.STATE_DETAIL
                Case CommonMessage.STATE_DEACTIVE
                    If rep.ActiveLocationID(Location, "I") Then
                        Refresh("UpdateView")
                        UpdatePage()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdatePage()
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                    End If
                Case CommonMessage.STATE_ACTIVE
                    If rep.ActiveLocationID(Location, "A") Then
                        Refresh("UpdateView")
                        UpdatePage()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdatePage()
                    End If
            End Select
            FindEmployee.Controls.Clear()
            FindEmployee_Contract.Controls.Clear()
            'Hiển thị Popup
            Select Case isLoadPopup

                Case 1
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    phPopupOrg.Controls.Add(ctrlFindOrgPopup)
                Case 2
                    ctrlFindEmployee = Me.Register("ctrlFindEmployee", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployee.MultiSelect = False
                    ctrlFindEmployee.MustHaveContract = False
                    ctrlFindEmployee.LoadAllOrganization = True
                    FindEmployee.Controls.Add(ctrlFindEmployee)
                Case 3
                    ctrlFindEmployee_Contract = Me.Register("ctrlFindEmployee_Contract", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployee_Contract.MultiSelect = False
                    ctrlFindEmployee_Contract.MustHaveContract = False
                    ctrlFindEmployee_Contract.LoadAllOrganization = True
                    FindEmployee_Contract.Controls.Add(ctrlFindEmployee_Contract)
            End Select
            ChangeToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    'Đổ dữ liệu vào Combobox
    Private Sub GetDataCombo()
        Dim rep As New ProfileRepository
        Try
            If ListComboData Is Nothing Then
                ListComboData = New Profile.ProfileBusiness.ComboBoxDataDTO
                ListComboData.GET_BANK = True
                'ListComboData.GET_BUSINESS = True
                'ListComboData.GET_BUSINESSTYPE = True
                ListComboData.GET_NATION = True
                ListComboData.GET_PROVINCE = True
                ListComboData.GET_DISTRICT = True
                rep.GetComboList(ListComboData)
                ComboData = ListComboData
            End If

            'Provide
            Dim dtPlace = rep.GetProvinceList(True)
            FillRadCombobox(cboProvince, dtPlace, "NAME", "ID")

            FillDropDownList(cbBank, ListComboData.LIST_BANK, "NAME", "ID", Common.Common.SystemLanguage, True, cbBank.SelectedValue)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objLocationFunction As New LocationDTO
        Dim gID As Decimal
        Dim result As Int32 = 0
        Dim rep As New ProfileRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdatePage()
                    CleanControl()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgLocation.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgLocation.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdatePage()

                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgLocation.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ActiveLocations = RepareDataForAction()

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgLocation.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ActiveLocations = RepareDataForAction()

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT

                    Dim _error As Integer = 0
                    Using xls As New ExcelCommon
                        Dim bCheck = xls.ExportExcelTemplate(
                            Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/" & Request.Params("fid") & ".xls"),
                            "Location", dtData, Response, _error)
                        If Not bCheck Then
                            Select Case _error
                                Case 1
                                    ShowMessage(Translate("Mẫu báo cáo không tồn tại"), NotifyType.Warning)
                                Case 2
                                    ShowMessage(Translate("Dữ liệu không tồn tại"), NotifyType.Warning)
                            End Select
                            Exit Sub
                        End If
                    End Using

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        ' objLocationFunction.CODE = txtLocation.Text
                        If hfOrg.Value IsNot Nothing And hfOrg.Value <> "" Then
                            objLocationFunction.ORG_ID = hfOrg.Value
                        End If
                        objLocationFunction.LOCATION_EN_NAME = txtLocationEn.Text
                        objLocationFunction.LOCATION_VN_NAME = txtLocationVN.Text
                        objLocationFunction.ADDRESS = txtAddress.Text
                        'ObjLocation.Item("WORK_ADDRESS") = txtWorkAddress.Text
                        objLocationFunction.PHONE = txtPhone.Text
                        objLocationFunction.FAX = txtFax.Text
                        objLocationFunction.WORK_ADDRESS = txtAddress_Emp.Text
                        objLocationFunction.CONTRACT_PLACE = txtOfficePlace.Text
                        objLocationFunction.WEBSITE = txtWebsite.Text
                        objLocationFunction.ACCOUNT_NUMBER = txtAccountNumber.Text
                        objLocationFunction.LOCATION_SHORT_NAME = txtLocationShort.Text

                        If hfBank.Value IsNot Nothing And hfBank.Value <> "" Then
                            objLocationFunction.BANK_ID = hfBank.Value
                        End If
                        If hfBank_Branch.Value IsNot Nothing And hfBank_Branch.Value <> "" Then
                            objLocationFunction.BANK_BRANCH_ID = hfBank_Branch.Value
                        End If
                        objLocationFunction.TAX_CODE = txtTaxCode.Text

                        objLocationFunction.TAX_DATE = rdpTaxDate.SelectedDate

                        If hfLawAgent.Value IsNot Nothing And hfLawAgent.Value <> "" Then
                            objLocationFunction.EMP_LAW_ID = hfLawAgent.Value
                        End If
                        If hfSignUpAgent.Value IsNot Nothing And hfSignUpAgent.Value <> "" Then
                            objLocationFunction.EMP_SIGNCONTRACT_ID = hfSignUpAgent.Value
                        End If
                        objLocationFunction.TAX_PLACE = txtTaxPlace.Text
                        objLocationFunction.BUSINESS_NAME = txtNameBusiness.Text
                        objLocationFunction.BUSINESS_NUMBER = txtNumberBusiness.Text
                        objLocationFunction.BUSINESS_REG_DATE = rdRegisterDate.SelectedDate
                        objLocationFunction.ACTFLG = "A"
                        objLocationFunction.NOTE = txtNote.Text

                        objLocationFunction.IS_SIGN_CONTRACT = If(ckIsSignContract.Checked = True, 1, 0)
                        objLocationFunction.PROVINCE_ID = If(cboProvince.SelectedValue <> "", Decimal.Parse(cboProvince.SelectedValue), 0)
                        objLocationFunction.DISTRICT_ID = If(cboDistrict.SelectedValue <> "", Decimal.Parse(cboDistrict.SelectedValue), 0)
                        objLocationFunction.WARD_ID = If(cboWard.SelectedValue <> "", Decimal.Parse(cboWard.SelectedValue), 0)

                        objLocationFunction.FILE_LOGO = txtUpload_LG.Text.Trim
                        objLocationFunction.FILE_HEADER = txtUpload_HD.Text.Trim
                        objLocationFunction.FILE_FOOTER = txtUpload_FT.Text.Trim

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                'insert vao db
                                If rep.InsertLocation(objLocationFunction, gID) Then
                                    IDSelect = gID
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    CleanControl()
                                    Refresh("InsertView")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                                UpdateControlState()
                                '  CurrentState = CommonMessage.STATE_NEW
                            Case CommonMessage.STATE_EDIT
                                objLocationFunction.ID = hfID.Value
                                'update vao db
                                If rep.ModifyLocation(objLocationFunction, gID) Then
                                    IDSelect = objLocationFunction.ID
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    Refresh("UpdateView")
                                Else
                                    CurrentState = CommonMessage.STATE_EDIT
                                End If
                        End Select

                        'Hiển thị thông báo và Refresh control
                        If result = 1 Then
                            Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                        Else
                            Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If

                        UpdatePage()
                    End If
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgLocation.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    isLoadPopup = 0
                    Refresh("Cancel")
                    UpdatePage()
                    'Exit Sub
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New ProfileRepository
        Dim result As Boolean
        Dim idList As String = String.Empty
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                'For Each dr As Decimal In ItemList
                '    idList &= IIf(idList = vbNullString, dr, "," & dr)
                'Next
                Select Case e.ActionName
                    Case CommonMessage.ACTION_ACTIVE
                        result = rep.ActiveLocation(ActiveLocations, "A")
                    Case CommonMessage.ACTION_DEACTIVE
                        result = rep.ActiveLocation(ActiveLocations, "I")
                    Case CommonMessage.TOOLBARITEM_DELETE
                        result = rep.DeleteLocationID(Location.ID)
                        'Case CommonMessage.TOOLBARITEM_DELETE
                        '    result = psp.Location_Delete(idList)
                End Select

                ActiveLocations = Nothing

                'ItemList = Nothing

                If result Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgLocation.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                End If
                CurrentState = CommonMessage.STATE_NORMAL
                UpdatePage()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgLocation_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgLocation.NeedDataSource
        Dim rep As New ProfileRepository
        Dim repC As New CommonRepository
        Dim lstID As New List(Of Decimal)

        Dim lstOrgPermission As New List(Of Common.CommonBusiness.OrganizationDTO)

        If Common.Common.OrganizationLocationDataSession Is Nothing Then
            lstOrgPermission = repC.GetOrganizationLocationTreeView()
        Else
            lstOrgPermission = Common.Common.OrganizationLocationDataSession
        End If

        For Each org In lstOrgPermission
            lstID.Add(org.ID)
        Next

        Locations = rep.GetLocation("", lstID)
        Try
            rgLocation.VirtualItemCount = Locations.Count
            rgLocation.DataSource = Locations
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgLocation_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rgLocation.SelectedIndexChanged
        Dim rep As New ProfileRepository
        If CurrentState <> CommonMessage.STATE_EDIT AndAlso CurrentState <> CommonMessage.STATE_NEW Then
            If rgLocation.SelectedItems.Count > 0 Then
                CleanControl()
                Dim slItem As GridDataItem
                slItem = rgLocation.SelectedItems(0)
                hfID.Value = slItem("ID").Text
                'Lấy thông tin Record
                Dim dt = rep.GetLocationID(hfID.Value)
                If dt IsNot Nothing Then
                    hfOrg.Value = dt.ORG_ID
                    ' txtLocation.Text = dt.CODE
                    txtLocationEn.Text = dt.LOCATION_EN_NAME
                    txtLocationVN.Text = dt.LOCATION_VN_NAME
                    txtAddress.Text = dt.ADDRESS
                    txtPhone.Text = dt.PHONE
                    txtFax.Text = dt.FAX
                    txtAddress_Emp.Text = dt.WORK_ADDRESS
                    txtOfficePlace.Text = dt.CONTRACT_PLACE
                    txtWebsite.Text = dt.WEBSITE
                    txtAccountNumber.Text = dt.ACCOUNT_NUMBER
                    OldBankID = dt.ACCOUNT_NUMBER
                    txtLocationShort.Text = dt.LOCATION_SHORT_NAME

                    cbBank.ClearSelection()
                    If dt.BANK_ID IsNot Nothing Then
                        cbBank.SelectedValue = dt.BANK_ID
                        hfBank.Value = dt.BANK_ID
                        FillRadCombobox(cboRank_Banch, rep.GetBankBranchByBankID(hfBank.Value), "NAME", "ID", True)
                    End If

                    cboRank_Banch.ClearSelection()
                    cboRank_Banch.Text = ""
                    If dt.BANK_BRANCH_ID IsNot Nothing And cbBank.SelectedValue <> "" Then
                        cboRank_Banch.SelectedValue = dt.BANK_BRANCH_ID
                        hfBank_Branch.Value = dt.BANK_BRANCH_ID
                    End If
                    txtTaxCode.Text = dt.TAX_CODE
                    rdpTaxDate.SelectedDate = dt.TAX_DATE
                    If dt.EMP_LAW_ID IsNot Nothing Then
                        LoadEmpInfor(1, dt.EMP_LAW_ID, dt)
                    Else
                        ClearControlValue(txtLawAgentId, txtLawAgentNationality, txtLawAgentTitle, hfLawAgent)
                    End If

                    cboProvince.ClearSelection()
                    If dt.PROVINCE_ID IsNot Nothing Then
                        cboProvince.SelectedValue = dt.PROVINCE_ID
                        FillRadCombobox(cboDistrict, rep.GetDistrictList(dt.PROVINCE_ID, True), "NAME", "ID")
                    End If

                    cboDistrict.ClearSelection()
                    cboDistrict.Text = ""
                    If dt.DISTRICT_ID IsNot Nothing Then
                        cboDistrict.SelectedValue = dt.DISTRICT_ID
                        FillRadCombobox(cboWard, rep.GetWardList(dt.DISTRICT_ID, True), "NAME", "ID")
                    End If

                    cboWard.ClearSelection()
                    cboWard.Text = ""
                    If dt.WARD_ID IsNot Nothing Then
                        cboDistrict.SelectedValue = dt.WARD_ID
                    End If

                    If dt.IS_SIGN_CONTRACT Is Nothing Or dt.IS_SIGN_CONTRACT = 0 Then
                        ckIsSignContract.Checked = False
                    Else
                        ckIsSignContract.Checked = True
                    End If

                    txtUpload_LG.Text = dt.FILE_LOGO
                    txtUpload_HD.Text = dt.FILE_HEADER
                    txtUpload_FT.Text = dt.FILE_FOOTER

                    If dt.EMP_SIGNCONTRACT_ID IsNot Nothing Then
                        LoadEmpInfor(2, dt.EMP_SIGNCONTRACT_ID, dt)
                    Else
                        ClearControlValue(txtSignupAgent, txtSignupAgentTitle, txtSigupAgentNationality, hfSignUpAgent)
                    End If

                    txtTaxPlace.Text = dt.TAX_PLACE
                    txtNameBusiness.Text = dt.BUSINESS_NAME
                    txtNumberBusiness.Text = dt.BUSINESS_NUMBER

                    rdRegisterDate.SelectedDate = dt.BUSINESS_REG_DATE
                    txtNote.Text = dt.NOTE



                    'gan vao bien cuc bo
                    'Location = dt
                End If
            End If
        End If
    End Sub

    Protected Sub btnLawAgentId_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLawAgentId.Click
        Try
            isLoadPopup = 2
            'UpdateControlState()
            UpdatePage()
            ctrlFindEmployee.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub btnSignupAgent_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSignupAgent.Click
        Try
            isLoadPopup = 3
            'UpdateControlState()
            UpdatePage()

            ctrlFindEmployee_Contract.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindEmployeeContract_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployee_Contract.EmployeeSelected
        Try
            Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
            Dim nation As String = String.Empty

            'Kiểm tra danh sách nhân viên trả về
            lstCommonEmployee = CType(ctrlFindEmployee_Contract.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))

            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                Dim dt = psp.EmployeeCV_GetInfo(item.EMPLOYEE_ID)
                If dt IsNot Nothing And dt.Rows.Count > 0 Then
                    nation = dt.Rows(0)("NATION_NAME_VN").ToString()
                End If

                txtSignupAgent.Text = item.FULLNAME_VN
                txtSignupAgentTitle.Text = item.TITLE_NAME
                txtSigupAgentNationality.Text = nation
                hfSignUpAgent.Value = item.ID
            End If
            isLoadPopup = 0
            ' UpdatePage()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindEmployee_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployee.EmployeeSelected
        Try
            Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
            Dim nation As String = String.Empty

            'Kiểm tra danh sách nhân viên trả về
            lstCommonEmployee = CType(ctrlFindEmployee.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))

            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                Dim dt = psp.EmployeeCV_GetInfo(item.EMPLOYEE_ID)
                If dt IsNot Nothing And dt.Rows.Count > 0 Then
                    nation = dt.Rows(0)("NATION_NAME_VN").ToString()
                End If

                txtLawAgentId.Text = item.FULLNAME_VN
                txtLawAgentTitle.Text = item.TITLE_NAME
                txtLawAgentNationality.Text = nation
                hfLawAgent.Value = item.ID

            End If

            isLoadPopup = 0
            ' UpdatePage()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindOrg.Click
        Try
            isLoadPopup = 1
            'UpdateControlState()
            UpdatePage()
            ctrlFindOrgPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub cboCommon_ItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs) Handles cboProvince.ItemsRequested, cboDistrict.ItemsRequested, cboWard.ItemsRequested
        Dim startTime As DateTime = DateTime.UtcNow
        Using rep As New ProfileRepository
            Try
                Dim dtData As DataTable
                Dim sText As String = e.Text
                Dim dValue As Decimal

                Select Case sender.ID
                    Case cboProvince.ID
                        dtData = rep.GetProvinceList(True)
                    Case cboDistrict.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        dtData = rep.GetDistrictList(dValue, True)
                    Case cboWard.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        dtData = rep.GetWardList(dValue, True)
                End Select
                If sText <> "" Then
                    'Dim dtExist = (From p In dtData
                    '               Where p("NAME") IsNot DBNull.Value AndAlso
                    '              p("NAME").ToString.ToUpper = sText.ToUpper)

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
                        sender.Items.Add(radItem)
                    Next
                Else
                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                    e.EndOfItems = endOffset = dtData.Rows.Count
                    sender.Items.Clear()
                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                        sender.Items.Add(radItem)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Sub

    ''' <summary>
    ''' Xử lý sự kiện khi click upload file LOGO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUploadFile_LG_Click(sender As Object, e As System.EventArgs) Handles btnUploadFile_LG.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            IsUpload = 0
            ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,pdf"
            ctrlUpload1.Show()
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Xử lý sự kiện khi click upload file HEADER
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUploadFile_HD_Click(sender As Object, e As System.EventArgs) Handles btnUploadFile_HD.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            IsUpload = 1
            ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,pdf"
            ctrlUpload1.Show()
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Xử lý sự kiện khi click upload file FOOTER
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUploadFile_FT_Click(sender As Object, e As System.EventArgs) Handles btnUploadFile_FT.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            IsUpload = 2
            ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,pdf"
            ctrlUpload1.Show()
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>Xử lý sự kiện khi click [OK] xác nhận sẽ Upload file</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim listExtension = New List(Of String)
            listExtension.Add(".xls")
            listExtension.Add(".xlsx")
            listExtension.Add(".txt")
            listExtension.Add(".ctr")
            listExtension.Add(".doc")
            listExtension.Add(".docx")
            listExtension.Add(".xml")
            listExtension.Add(".png")
            listExtension.Add(".jpg")
            listExtension.Add(".bitmap")
            listExtension.Add(".jpeg")
            listExtension.Add(".pdf")
            Dim fileName As String

            Dim strPath As String = Server.MapPath("~/AttachFile/Profile/ctrlLocation/")
            If ctrlUpload1.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload1.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(i)
                    If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                        System.IO.Directory.CreateDirectory(strPath)
                        fileName = System.IO.Path.Combine(strPath, file.FileName)
                        file.SaveAs(fileName, True)
                        If IsUpload = 0 Then
                            txtUpload_LG.Text = hfOrg.Value + file.FileName
                        ElseIf IsUpload = 1 Then
                            txtUpload_HD.Text = hfOrg.Value + file.FileName
                        Else
                            txtUpload_FT.Text = hfOrg.Value + file.FileName
                        End If
                    Else
                        ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file XLS, XLSX, TXT, CTR, DOC, DOCX, XML, PNG, JPG, BITMAP, JPEG, PDF"), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Xử lý sự kiện click btn Tải LOGO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDownload_LG_Click(sender As Object, e As System.EventArgs) Handles btnDownload_LG.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim strPath_Down As String
        Try
            If txtUpload_LG.Text <> "" Then
                strPath_Down = Server.MapPath("~/AttachFile/Profile/ctrlLocation/" + txtUpload_LG.Text.Trim)
                ZipFiles(strPath_Down, 0)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Xử lý sự kiện click btn Tải HEADER
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDownload_HD_Click(sender As Object, e As System.EventArgs) Handles btnDownload_HD.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim strPath_Down As String
        Try
            If txtUpload_HD.Text <> "" Then
                strPath_Down = Server.MapPath("~/AttachFile/Profile/ctrlLocation/" + txtUpload_HD.Text.Trim)
                ZipFiles(strPath_Down, 1)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Xử lý sự kiện click btn Tải FOOTER
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDownload_FT_Click(sender As Object, e As System.EventArgs) Handles btnDownload_FT.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim strPath_Down As String
        Try
            If txtUpload_FT.Text <> "" Then
                strPath_Down = Server.MapPath("~/AttachFile/Profile/ctrlLocation/" + txtUpload_FT.Text.Trim)
                ZipFiles(strPath_Down, 2)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"

    ''' <summary>
    ''' Định nghĩa ToolBar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitToolBar()
        Try
            rgLocation.SetFilter()

            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If
            Common.Common.BuildToolbar(Me.MainToolBar,
                                 ToolbarItem.Create,
                                 ToolbarItem.Edit,
                                 ToolbarItem.Seperator,
                                 ToolbarItem.Save,
                                 ToolbarItem.Cancel,
                                 ToolbarItem.Active,
                                 ToolbarItem.Deactive,
                                 ToolbarItem.Delete)
            CType(Me.MainToolBar.Items(3), RadToolBarButton).CausesValidation = True

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Định nghĩa Property cho tất cả Control
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitProperty()
        Try
            isLoadPopup = 0

            'Grid
            rgLocation.AllowPaging = True
            rgLocation.AllowCustomPaging = True
            rgLocation.PageSize = 10
            rgLocation.ClientSettings.EnablePostBackOnRowClick = True
            rgLocation.MasterTableView.FilterExpression = ""

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    'Lấy danh sách Select cho sự kiện
    Protected Function RepareDataForAction() As List(Of LocationDTO)
        Dim lst As New List(Of LocationDTO)
        For Each dr As GridDataItem In rgLocation.SelectedItems
            Dim item As New LocationDTO
            item.ID = Decimal.Parse(dr("ID").Text)
            lst.Add(item)
        Next
        Return lst
    End Function

    'Clean lại control
    Protected Sub CleanControl()
        For Each control As Control In PaneMain.Controls
            If TypeOf control Is RadTextBox Then
                DirectCast(control, RadTextBox).Text = String.Empty
            End If
            If TypeOf control Is RadComboBox Then
                If DirectCast(control, RadComboBox).Items.Count > 0 Then
                    DirectCast(control, RadComboBox).SelectedIndex = 0
                End If
            End If
            If TypeOf control Is RadDatePicker Then
                DirectCast(control, RadDatePicker).Clear()
            End If
        Next
        rdpTaxDate.Clear()
    End Sub

    'Clean lại Grid
    Protected Sub CleanGrid()
        If rgLocation.MasterTableView.Items().Count > 0 Then
            rgLocation.MasterTableView.ClearSelectedItems()
        End If
    End Sub

    'Kiểm tra số tài khoản tồn tại?
    Protected Sub CustomValidator1_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    If txtAccountNumber.Text().Trim() <> "" Then
                        If (psp.BankID_HasExist(txtAccountNumber.Text()) > 0) Then
                            args.IsValid = False
                            Me.ShowMessage(Translate("Số tài khoản đã tồn tại"), Utilities.NotifyType.Error)
                        Else
                            args.IsValid = True
                        End If
                    Else
                        args.IsValid = True
                    End If
                Case CommonMessage.STATE_EDIT
                    If txtAccountNumber.Text().Trim() <> "" Then
                        If OldBankID <> txtAccountNumber.Text().Trim() Then
                            If psp.BankID_HasExist(txtAccountNumber.Text()) > 0 Then
                                args.IsValid = False
                                Me.ShowMessage(Translate("Số tài khoản đã tồn tại"), Utilities.NotifyType.Error)
                            Else
                                args.IsValid = True
                            End If
                        End If
                    Else
                        args.IsValid = True
                    End If
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Try
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hfOrg.Value = e.CurrentValue
                ' txtLocation.Text = orgItem.CODE
                txtLocationVN.Text = orgItem.NAME_VN
                txtLocationEn.Text = orgItem.NAME_EN

                'txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
            End If
            'cboTitle.ClearValue()
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub

    ''' <summary>
    ''' Load datasource cho cbo bank branch
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cbBank_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbBank.SelectedIndexChanged
        Dim rep As New ProfileRepository
        Try
            If cbBank.SelectedValue <> "" Then
                hfBank.Value = Double.Parse(cbBank.SelectedValue)
                Dim bankData = rep.GetBankBranchByBankID(hfBank.Value)
                FillRadCombobox(cboRank_Banch, bankData, "NAME", "ID", True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cboRank_Banch_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboRank_Banch.SelectedIndexChanged
        If cboRank_Banch.SelectedValue <> "" Then
            hfBank_Branch.Value = Double.Parse(cboRank_Banch.SelectedValue)
        End If
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgLocation.Rebind()
                        SelectedItemDataGridByKey(rgLocation, IDSelect, , rgLocation.CurrentPageIndex)

                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgLocation.CurrentPageIndex = 0
                        rgLocation.MasterTableView.SortExpressions.Clear()
                        rgLocation.Rebind()
                        SelectedItemDataGridByKey(rgLocation, IDSelect, )
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgLocation.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load thong tin nguoi dang ky HDLD va nguoi dai dien phap luat (dai dien phap luat la 1, HDlD la 2)
    ''' </summary>
    ''' <param name="index"></param>
    ''' <param name="emp_id"></param>
    ''' <param name="location"></param>
    ''' <remarks></remarks>
    Private Sub LoadEmpInfor(ByVal index As Integer, ByVal emp_id As Integer, ByVal location As LocationDTO)
        Dim rep_emp As New CommonRepository
        Dim get_nation As DataTable
        Dim nation As String
        Try
            If index = 1 Then
                Dim emp_law = rep_emp.GetEmployeeID(location.EMP_LAW_ID)
                If emp_law IsNot Nothing Then
                    hfLawAgent.Value = emp_law.ID
                    txtLawAgentId.Text = emp_law.FULLNAME_VN
                    txtLawAgentTitle.Text = emp_law.TITLE_NAME
                    get_nation = psp.EmployeeCV_GetInfo(emp_law.EMPLOYEE_ID)
                    If get_nation IsNot Nothing And get_nation.Rows.Count > 0 Then
                        nation = get_nation.Rows(0)("NATION_NAME_VN").ToString()
                        txtLawAgentNationality.Text = nation
                    End If
                End If
            Else
                Dim emp_sign = rep_emp.GetEmployeeID(emp_id)
                If emp_sign IsNot Nothing Then
                    hfSignUpAgent.Value = emp_sign.ID
                    txtSignupAgent.Text = emp_sign.FULLNAME_VN
                    txtSignupAgentTitle.Text = emp_sign.TITLE_NAME
                    get_nation = psp.EmployeeCV_GetInfo(emp_sign.EMPLOYEE_ID)
                    If get_nation IsNot Nothing And get_nation.Rows.Count > 0 Then
                        nation = get_nation.Rows(0)("NATION_NAME_VN").ToString()
                        txtSigupAgentNationality.Text = nation
                    End If
                End If
            End If
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ZipFiles(ByVal path As String, ByVal order As Decimal?)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim crc As New CRC32()
            Dim fileNameZip As String

            If order = 0 Then
                fileNameZip = txtUpload_LG.Text.Trim
            ElseIf order = 1 Then
                fileNameZip = txtUpload_HD.Text.Trim
            Else
                fileNameZip = txtUpload_FT.Text.Trim
            End If

            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path & fileNameZip)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            'Response.ContentType = "application/octet-stream"
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
            Response.WriteFile(file.FullName)
            Response.End()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

End Class
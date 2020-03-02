Imports System.IO
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Imports Ionic.Zip

Public Class ctrlHU_Organization
    Inherits Common.CommonView

    Private procedure As ProfileStoreProcedure
    Dim dtOrgLevel As DataTable = Nothing
    Dim dtRegion As DataTable = Nothing
    Dim dtIsunace As DataTable = Nothing
    Public isPhysical As Decimal = Decimal.Parse(ConfigurationManager.AppSettings("PHYSICAL_PATH"))

#Region "Property"

    Property Organization As OrganizationDTO
        Get
            Return ViewState(Me.ID & "_Organization")
        End Get
        Set(ByVal value As OrganizationDTO)
            ViewState(Me.ID & "_Organization") = value
        End Set
    End Property

    Public Property Organizations As List(Of OrganizationDTO)
        Get
            Return ViewState(Me.ID & "_Organizations")
        End Get
        Set(ByVal value As List(Of OrganizationDTO))
            ViewState(Me.ID & "_Organizations") = value
        End Set
    End Property

    Property ActiveOrganizations As List(Of OrganizationDTO)
        Get
            Return ViewState(Me.ID & "_ActiveOrganizations")
        End Get
        Set(ByVal value As List(Of OrganizationDTO))
            ViewState(Me.ID & "_ActiveOrganizations") = value
        End Set
    End Property

    Property SelectOrgFunction As String
        Get
            Return ViewState(Me.ID & "_SelectOrgFunction")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_SelectOrgFunction") = value
        End Set
    End Property

    '0 - normal
    '1 - Employee
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
    Property check As String
        Get
            Return ViewState(Me.ID & "_check")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_check") = value
        End Set
    End Property

    Property ImageFile As Telerik.Web.UI.UploadedFile
        Get
            Return ViewState(Me.ID & "_ImageFile")
        End Get
        Set(ByVal value As Telerik.Web.UI.UploadedFile)
            ViewState(Me.ID & "_ImageFile") = value
        End Set
    End Property

    Property LicenseFile As Telerik.Web.UI.UploadedFile
        Get
            Return ViewState(Me.ID & "_LicenseFile")
        End Get
        Set(ByVal value As Telerik.Web.UI.UploadedFile)
            ViewState(Me.ID & "_LicenseFile") = value
        End Set
    End Property

#End Region

#Region "Page"

    Dim org_id As String
    Dim _mylog As New MyLog()

    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/Setting/" + Me.GetType().Name.ToString()

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Refresh()
            UpdateControlState()
            'FillDataByTree()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
        'If Not IsPostBack Then
        '    ViewConfig(MainPane)
        'End If
    End Sub

    Public Overrides Sub BindData()
        Try
            GetDataCombo()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarOrgFunctions
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Seperator,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel, ToolbarItem.Seperator,
                                       ToolbarItem.Active,
                                       ToolbarItem.Deactive)
            CType(Me.MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim lstOrganization As List(Of OrganizationDTO)
            Dim rep As New ProfileRepository
            SelectOrgFunction = String.Empty
            Try
                Dim startTime As DateTime = DateTime.UtcNow
                If Not IsPostBack Then
                    Dim callFunction = Common.CommonRepository.GetOrganizationLocationTreeView()
                    lstOrganization = rep.GetOrganization()
                    Dim lstorgper = (From p In Common.Common.OrganizationLocationDataSession Select p.ID).ToList()
                    Dim lst = (From p In lstOrganization Where lstorgper.Contains(p.ID)).ToList()
                    Me.Organizations = lst
                    CurrentState = CommonMessage.STATE_NORMAL
                    org_id = Request.QueryString("id")
                    If org_id IsNot String.Empty Then
                        SelectOrgFunction = org_id
                    End If
                Else

                    Select Case Message
                        Case "UpdateView"
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            Dim callFunction = Common.CommonRepository.GetOrganizationLocationTreeView()
                            lstOrganization = rep.GetOrganization()
                            Dim lstorgper = (From p In Common.Common.OrganizationLocationDataSession Select p.ID).ToList()
                            Dim lst = (From p In lstOrganization Where lstorgper.Contains(p.ID)).ToList()
                            Me.Organizations = lst
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateToolbarState(CurrentState)
                        Case "InsertView"
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            Dim callFunction = Common.CommonRepository.GetOrganizationLocationTreeView()
                            lstOrganization = rep.GetOrganization()
                            Dim lstorgper = (From p In Common.Common.OrganizationLocationDataSession Select p.ID).ToList()
                            Dim lst = (From p In lstOrganization Where lstorgper.Contains(p.ID)).ToList()
                            Me.Organizations = lst
                            CurrentState = CommonMessage.STATE_NORMAL
                    End Select
                End If

                'Đưa dữ liệu vào Grid
                If Me.Organizations IsNot Nothing Then
                    If SelectOrgFunction Is String.Empty Then
                        SelectOrgFunction = treeOrgFunction.SelectedValue
                    End If
                    BuildTreeNode(treeOrgFunction, Me.Organizations, cbDissolve.Checked)
                End If
                rep.Dispose()
                _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                DisplayException(Me.ViewName, Me.ID, ex)
                _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            End Try
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <summary>
    ''' Author: ChienNV,Edit date:11/10/2017
    ''' FillRadCombobox(cboU_insurance)
    ''' FillRadCombobox(cboRegion)
    ''' FillRadCombobox(cboOrg_level)
    ''' Author Edit/Modify: TUNGNT
    ''' Des: Binding data cho combobox dv BH, vung BH, danh sach cap don vi
    ''' Date Edit/Modify: 13/05/2018
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        'Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim startTime As DateTime = DateTime.UtcNow
        Try
            'If procedure Is Nothing Then
            '    procedure = New ProfileStoreProcedure()
            'End If
            'Lấy danh sách dv đóng bảo hiểm.
            Dim rep1 = New ProfileStoreProcedure()
            dtIsunace = rep1.GetListInsurance()
            If Not dtIsunace Is Nothing AndAlso dtIsunace.Rows.Count > 0 Then
                FillRadCombobox(cboU_insurance, dtIsunace, "NAME", "ID")
            End If
            'Lấy danh sách vùng bảo hiểm
            dtRegion = rep1.GetInsListRegion()
            If Not dtRegion Is Nothing AndAlso dtRegion.Rows.Count > 0 Then
                FillRadCombobox(cboRegion, dtRegion, "REGION_NAME", "ID")
            End If

            Dim rep As New ProfileRepository
            Dim dtcbo = rep.GetOtherList("GROUP_PAID", True)
            FillRadCombobox(cboGROUP_PAID_ID, dtcbo, "NAME", "ID")

            Dim dtcbo1 = rep.GetOtherList("ORG_LEVEL", True)
            FillRadCombobox(cboUNIT_RANK_ID, dtcbo1, "NAME", "ID")

            Dim dtcbo2 = rep.GetProvinceList(True)
            FillRadCombobox(cboPROVINCE_ID, dtcbo2, "NAME", "ID")
            FillRadCombobox(cboPROVINCE_CONTRACT_ID, dtcbo2, "NAME", "ID")

            'Dim dtcbo3 = rep.GetDistrictList(0, False)
            'FillRadCombobox(cboDISTRICT_ID, dtcbo3, "NAME", "ID")

            Dim dtcbo6 = rep.GetDistrictList(0, False)
            FillRadCombobox(cboDISTRICT_CONTRACT_ID, dtcbo6, "NAME", "ID")

            Dim dtcbo4 = rep.GetBankList(True)
            FillRadCombobox(cboBANK_ID, dtcbo4, "NAME", "ID")

            Dim dtcbo5 = rep.GetBankBranchList(0)
            FillRadCombobox(cboBANK_BRACH_ID, dtcbo5, "NAME", "ID")

            Dim dt7 = rep1.GetAllDistrict()
            Dim json_obj = From n In dt7.AsEnumerable
                           Select New With {
                               .ID = n.Field(Of Decimal)("ID"),
                               .NAME_VN = n.Field(Of String)("NAME_VN"),
                               .PROVINCE_ID = n.Field(Of Decimal?)("PROVINCE_ID")
                             }
            Dim s = New Script.Serialization.JavaScriptSerializer().Serialize(json_obj.ToArray)
            hidListDistrict.Value = s

            Dim dt8 = rep1.GETALLHU_BANK_BRANCH()
            Dim json_obj1 = From n In dt8.AsEnumerable
                            Select New With {
                               .ID = n.Field(Of Decimal)("ID"),
                               .NAME_VN = n.Field(Of String)("NAME"),
                               .PROVINCE_ID = n.Field(Of Decimal?)("BANK_ID")
                             }
            Dim s1 = New Script.Serialization.JavaScriptSerializer().Serialize(json_obj1.ToArray)
            hidListbankBrach.Value = s1
            'Lấy danh sách cấp đơn vị
            'dtOrgLevel = procedure.GET_ORG_LEVEL()
            'If Not dtOrgLevel Is Nothing AndAlso dtOrgLevel.Rows.Count > 0 Then
            '    FillRadCombobox(cboOrg_level, dtOrgLevel, "NAME_VN", "ID")
            'End If

            'Dim UNIT_LEVEL As New ComboBoxDataDTO
            'UNIT_LEVEL.GET_UNIT_LEVEL = True
            'Dim isUnitlevel = rep.GetComboList(UNIT_LEVEL)
            'If isUnitlevel AndAlso cbUNIT_LEVEL.Items.Count < 1 Then
            '    'FillRadCombobox(cbUNIT_LEVEL, UNIT_LEVEL.LIST_UNIT_LEVEL, "NAME_VN", "ID")
            '    FillDropDownList(cbUNIT_LEVEL, UNIT_LEVEL.LIST_UNIT_LEVEL, "NAME_VN", "ID")
            'End If
            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        Finally
            'procedure = Nothing
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim objOrgFunction As New OrganizationDTO

            Dim gID As Decimal
            Dim rep As New ProfileRepository
            Try
                Dim startTime As DateTime = DateTime.UtcNow
                Select Case CType(e.Item, RadToolBarButton).CommandName
                    Case CommonMessage.TOOLBARITEM_CREATE
                        If Organizations.Count > 0 Then
                            If treeOrgFunction.SelectedNode Is Nothing Then
                                ShowMessage("Chưa chọn phòng ban?", NotifyType.Warning)
                                Exit Sub
                            End If
                            txtParent_Name.Text = (From p In Organizations Where p.ID = Decimal.Parse(treeOrgFunction.SelectedNode.Value)).SingleOrDefault.NAME_VN
                        End If
                        CurrentState = CommonMessage.STATE_NEW
                        txtCode.Focus()
                        'tr01.Visible = False
                        'tr02.Visible = False
                        'cbUNIT_LEVEL.ClearValue()
                        objOrgFunction.REPRESENTATIVE_ID = Nothing
                        'hidRepresentative.Value = Nothing
                        If treeOrgFunction.SelectedNode.Level = 0 Then
                            'tr01.Visible = True
                            'tr02.Visible = True
                        End If
                    Case CommonMessage.TOOLBARITEM_EDIT
                        If treeOrgFunction.SelectedNode Is Nothing Then
                            ShowMessage("Chưa chọn phòng ban?", NotifyType.Warning)
                            Exit Sub
                        End If

                        Organization = (From p In Organizations Where p.ID = Decimal.Parse(treeOrgFunction.SelectedNode.Value)).SingleOrDefault
                        CurrentState = CommonMessage.STATE_EDIT
                        txtNameVN.Focus()
                    Case CommonMessage.TOOLBARITEM_ACTIVE, CommonMessage.TOOLBARITEM_DEACTIVE
                        Dim sActive As String
                        If treeOrgFunction.SelectedNode Is Nothing Then
                            ShowMessage("Chưa chọn phòng ban?", NotifyType.Warning)
                            Exit Sub
                        End If
                        ActiveOrganizations = (From p In Organizations Where p.ID = Decimal.Parse(treeOrgFunction.SelectedNode.Value)).ToList
                        If ActiveOrganizations.Count > 0 Then
                            sActive = ActiveOrganizations(0).ACTFLG
                            If sActive = "A" Then
                                If CType(e.Item, RadToolBarButton).CommandName = CommonMessage.TOOLBARITEM_ACTIVE Then
                                    ShowMessage("Các bản ghi đã ở trạng thái áp dụng trước đó, kiểm tra lại!", NotifyType.Warning)
                                    Exit Sub
                                End If
                            Else
                                If CType(e.Item, RadToolBarButton).CommandName = CommonMessage.TOOLBARITEM_DEACTIVE Then
                                    ShowMessage("Các bản ghi đã ở trạng thái ngưng áp dụng trước đó, kiểm tra lại!", NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If

                            If sActive = "A" Then
                                If Not rep.CheckEmployeeInOrganization(GetAllChild(ActiveOrganizations(0).ID)) Then
                                    ShowMessage("Bạn phải điều chuyển toàn bộ nhân viên trước khi giải thể.", NotifyType.Warning)
                                    Exit Sub
                                End If
                                ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                            Else

                                ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)

                            End If
                            ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                            ctrlMessageBox.DataBind()
                            ctrlMessageBox.Show()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        End If
                        Common.Common.OrganizationLocationDataSession = Nothing
                    Case CommonMessage.TOOLBARITEM_CANCEL
                        CurrentState = CommonMessage.STATE_NORMAL
                        'FillDataByTree()
                    Case CommonMessage.TOOLBARITEM_SAVE
                        Dim strFiles As String = String.Empty
                        If Not Page.IsValid Then
                            Exit Sub
                        End If
                        Dim row = New HU_ORGANIZATION_NEW
                        row.CODE = txtCode.Text
                        row.NAME_EN = txtNameEN.Text
                        row.NAME_VN = txtNameVN.Text
                        row.REMARK = txtREMARK.Text
                        row.ADDRESS = rtADDRESS.Text


                        If IsDate(rdFOUNDATION_DATE.SelectedDate) Then
                            row.FOUNDATION_DATE = rdFOUNDATION_DATE.SelectedDate
                        End If

                        If IsNumeric(rdOrdNo.Value) Then
                            row.ORD_NO = rdOrdNo.Value
                        Else
                            row.ORD_NO = 0
                        End If

                        Dim ISURANCE As Decimal = 0.0
                        If Decimal.TryParse(cboU_insurance.SelectedValue.ToString, ISURANCE) Then
                            row.U_INSURANCE = cboU_insurance.SelectedValue
                        Else
                            row.U_INSURANCE = ISURANCE
                        End If

                        Dim REGION As Decimal = 0.0
                        If Decimal.TryParse(cboRegion.SelectedValue.ToString, REGION) Then
                            row.REGION_ID = cboRegion.SelectedValue
                        Else
                            row.REGION_ID = REGION
                        End If
                        'If hidRepresentative.Value = "" Then
                        '    row.REPRESENTATIVE_ID = Nothing
                        'Else
                        '    row.REPRESENTATIVE_ID = hidRepresentative.Value
                        'End If


                        If hidParentID.Value IsNot Nothing Then
                            If hidParentID.Value <> "" Then
                                row.PARENT_ID = Decimal.Parse(hidParentID.Value)
                            End If
                        End If
                        '--------------
                        row.SHORT_NAME = txtSHORT_NAME.Text
                        row.IS_SIGN_CONTRACT = chkIsSignContract.Checked
                        row.CONTRACT_CODE = txtCONTRACT_CODE.Text
                        row.FAX = txtFAX.Text
                        row.WEBSITE_LINK = txtWEBSITE_LINK.Text
                        row.BANK_NO = txtBANK_NO.Text
                        row.PIT_NO = txtPIT_NO.Text
                        row.NUMBER_BUSINESS = txtNUMBER_BUSINESS.Text
                        row.BUSS_REG_NAME = txtBUSS_REG_NAME.Text
                        row.MAN_UNI_NAME = txtMAN_UNI_NAME.Text
                        row.AUTHOR_LETTER = txtAUTHOR_LETTER.Text

                        If cboGROUP_PAID_ID.SelectedValue <> "" Then
                            row.GROUP_PAID_ID = cboGROUP_PAID_ID.SelectedValue
                        End If
                        If cboUNIT_RANK_ID.SelectedValue <> "" Then
                            row.UNIT_RANK_ID = cboUNIT_RANK_ID.SelectedValue
                        End If
                        If cboPROVINCE_ID.SelectedValue <> "" Then
                            row.PROVINCE_ID = cboPROVINCE_ID.SelectedValue
                        End If
                        If cboDISTRICT_ID.SelectedValue <> "" Then
                            row.DISTRICT_ID = cboDISTRICT_ID.SelectedValue
                        End If
                        If cboPROVINCE_ID.SelectedValue <> "" Then
                            row.PROVINCE_ID = cboPROVINCE_ID.SelectedValue
                        End If
                        If cboBANK_ID.SelectedValue <> "" Then
                            row.BANK_ID = cboBANK_ID.SelectedValue
                        End If
                        If cboBANK_BRACH_ID.SelectedValue <> "" Then
                            row.PROVINCE_ID = cboBANK_BRACH_ID.SelectedValue
                        End If
                        If cboPROVINCE_CONTRACT_ID.SelectedValue <> "" Then
                            row.PROVINCE_CONTRACT_ID = cboPROVINCE_CONTRACT_ID.SelectedValue
                        End If
                        If cboDISTRICT_CONTRACT_ID.SelectedValue <> "" Then
                            row.DISTRICT_CONTRACT_ID = cboDISTRICT_CONTRACT_ID.SelectedValue
                        End If
                        Exit Sub


                        'btnREPRESENTATIVE_ID.Enabled = IsEnable

                        'btnHR_ID.Enabled = IsEnable

                        'rdDISSOLVE_DATE.Enabled = IsEnable
                        '--------------
                        If row.PARENT_ID = 0 Then
                            If Organizations.Select(Function(p) p.PARENT_ID = 0).Count > 0 Then
                                ShowMessage("Đã tồn tại phòng ban cao nhất?", NotifyType.Warning)
                                Exit Sub
                            End If
                        End If


                        '-----------------------------
                        'objOrgFunction.CODE = txtCode.Text
                        'objOrgFunction.NAME_EN = txtNameEN.Text
                        'objOrgFunction.NAME_VN = txtNameVN.Text
                        'objOrgFunction.REMARK = txtREMARK.Text
                        'objOrgFunction.ADDRESS = rtADDRESS.Text
                        'objOrgFunction.FILES = strFiles
                        'Dim ISURANCE As Decimal = 0.0
                        'Dim REGION As Decimal = 0.0
                        'Dim ORG_LEVEL As Decimal = 0.0
                        'If IsDate(rdFOUNDATION_DATE.SelectedDate) Then
                        '    objOrgFunction.FOUNDATION_DATE = rdFOUNDATION_DATE.SelectedDate
                        'End If

                        'If IsNumeric(rdOrdNo.Value) Then
                        '    objOrgFunction.ORD_NO = rdOrdNo.Value
                        'Else
                        '    objOrgFunction.ORD_NO = 0
                        'End If
                        'If Decimal.TryParse(cboU_insurance.SelectedValue.ToString, ISURANCE) Then
                        '    objOrgFunction.U_INSURANCE = cboU_insurance.SelectedValue
                        'Else
                        '    objOrgFunction.U_INSURANCE = ISURANCE
                        'End If
                        'If Decimal.TryParse(cboRegion.SelectedValue.ToString, REGION) Then
                        '    objOrgFunction.REGION_ID = cboRegion.SelectedValue
                        'Else
                        '    objOrgFunction.REGION_ID = REGION
                        'End If
                        'If hidRepresentative.Value = "" Then
                        '    objOrgFunction.REPRESENTATIVE_ID = Nothing
                        'Else
                        '    objOrgFunction.REPRESENTATIVE_ID = hidRepresentative.Value
                        'End If
                        'Dim objPath As OrganizationPathDTO
                        'Dim lstPath As New List(Of OrganizationPathDTO)
                        'If hidID.Value IsNot Nothing Then
                        '    If hidID.Value <> "" Then
                        '        objOrgFunction.PARENT_ID = Decimal.Parse(hidID.Value)
                        '    End If
                        'End If
                        '-----------------------------


                        'Try
                        '    strFiles = lstFile.CheckedItems.Select(Function(x) x.Text).Aggregate(Function(x, y) x & "# " & y)
                        'Catch ex As Exception
                        '    strFiles = ""
                        'End Try

                        'objOrgFunction.NUMBER_DECISION = txtNumberDecision.Text
                        'objOrgFunction.TYPE_DECISION = txtTypeDecision.Text
                        'objOrgFunction.LOCATION_WORK = txtLocationWork.Text

                        'objOrgFunction.NUMBER_BUSINESS = rtNUMBER_BUSINESS.Text
                        'objOrgFunction.DATE_BUSINESS = rdDATE_BUSINESS.SelectedDate

                        'If IsNumeric(cbUNIT_LEVEL.SelectedValue) Then
                        '    objOrgFunction.UNIT_LEVEL = cbUNIT_LEVEL.SelectedValue
                        'End If
                        'If IsDate(rdEffectDate.SelectedDate) Then
                        '    objOrgFunction.EFFECT_DATE = rdEffectDate.SelectedDate
                        'End If
                        'If IsDate(rdDATE_BUSINESS.SelectedDate) Then
                        '    objOrgFunction.DATE_BUSINESS = rdDATE_BUSINESS.SelectedDate
                        'End If



                        'If IsDate(rdDicision_Date.SelectedDate) Then
                        '    objOrgFunction.DISSOLVE_DATE = rdDicision_Date.SelectedDate
                        'End If
                        'If chkOrgChart.Checked = True Then
                        '    objOrgFunction.CHK_ORGCHART = True
                        'Else
                        '    objOrgFunction.CHK_ORGCHART = False
                        'End If

                        'If Decimal.TryParse(cboOrg_level.SelectedValue.ToString, ORG_LEVEL) Then
                        '    objOrgFunction.ORG_LEVEL = cboOrg_level.SelectedValue
                        'Else
                        '    objOrgFunction.ORG_LEVEL = ORG_LEVEL
                        'End If

                        'If treeOrgFunction.SelectedNode IsNot Nothing Then
                        '    objPath = GetUpLevelByNode(treeOrgFunction.SelectedNode)
                        '    objOrgFunction.HIERARCHICAL_PATH = objPath.HIERARCHICAL_PATH
                        '    objOrgFunction.DESCRIPTION_PATH = objPath.DESCRIPTION_PATH
                        'End If

                        Dim objPath As OrganizationPathDTO
                        Dim lstPath As New List(Of OrganizationPathDTO)
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                'objOrgFunction.ACTFLG = "A"
                                'If treeOrgFunction.Nodes.Count > 0 Then
                                '    GetDownLevelByNode(treeOrgFunction.Nodes(0), lstPath)
                                'End If
                                'Dim id As Decimal
                                'id = rep.GetMaxId()
                                'id = id + 1
                                'If treeOrgFunction.SelectedNode IsNot Nothing Then
                                '    objPath = GetUpLevelByNode(treeOrgFunction.SelectedNode)
                                '    row.HIERARCHICAL_PATH = objPath.HIERARCHICAL_PATH + ";" + id.ToString
                                '    ' objOrgFunction.DESCRIPTION_PATH = objPath.DESCRIPTION_PATH
                                '    Dim mang()
                                '    Dim str As String = ""
                                '    mang = objPath.HIERARCHICAL_PATH.Split(";")
                                '    For Each line In mang
                                '        Dim chuoi = rep.GetNameOrg(line)
                                '        str += chuoi + ";"
                                '    Next
                                '    str += txtNameVN.Text
                                '    row.DESCRIPTION_PATH = str
                                'End If
                                'GetDownLevelByNode(treeOrgFunction.Nodes(0), lstPath)

                                Dim rep1 = New ProfileStoreProcedure
                                If rep1.ADDNEW_ORGANIZATION(row) > 0 Then
                                    'If rep.InsertOrganization(objOrgFunction, gID) Then
                                    Refresh("InsertView")
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    Common.Common.OrganizationLocationDataSession = Nothing
                                    '' số bản ghi cập nhật
                                    'Dim iSoBanGhi As Integer = 50
                                    '' số dư để làm tròn
                                    'Dim iSoDu As Integer = lstPath.Count Mod iSoBanGhi
                                    'Dim iTongVongLap As Integer
                                    '' số vòng lặp khi làm tròn vs số bản ghi cập nhật
                                    'If iSoDu = 0 Then
                                    '    iTongVongLap = (lstPath.Count - iSoDu) / iSoBanGhi
                                    'Else
                                    '    iTongVongLap = ((lstPath.Count - iSoDu) / iSoBanGhi) + 1
                                    'End If

                                    'For item = 0 To iTongVongLap - 1
                                    '    ' cập nhật từng đợt ( tối đa = số bản ghi cập nhật )
                                    '    Dim lstUpdate As New List(Of OrganizationPathDTO)

                                    '    If item <> iTongVongLap - 1 Then
                                    '        For idx = item * iSoBanGhi To (item + 1) * iSoBanGhi - 1
                                    '            lstUpdate.Add(lstPath(idx))
                                    '        Next
                                    '    Else
                                    '        For idx = item * iSoBanGhi To lstPath.Count - 1
                                    '            lstUpdate.Add(lstPath(idx))
                                    '        Next
                                    '    End If
                                    '    ' cập nhật bản ghi
                                    '    rep.ModifyOrganizationPath(lstUpdate)
                                    'Next
                                    'Using commonRepo = New CommonRepository()
                                    '    Dim orgIds = commonRepo.GetUserOrganization(LogHelper.CurrentUser.ID)
                                    '    Dim userOrgAccessDTOs = orgIds.Select(Function(s) New CommonBusiness.UserOrgAccessDTO() With {
                                    '        .USER_ID = LogHelper.CurrentUser.ID,
                                    '        .ORG_ID = s
                                    '                                              }).ToList

                                    '    userOrgAccessDTOs.Add(New CommonBusiness.UserOrgAccessDTO With {.USER_ID = LogHelper.CurrentUser.ID,
                                    '                          .ORG_ID = gID})

                                    '    commonRepo.UpdateUserOrganization(userOrgAccessDTOs)
                                    'End Using

                                    'CurrentState = CommonMessage.STATE_NORMAL
                                    'Refresh("InsertView")


                                    'If treeOrgFunction.Nodes.Count > 0 Then
                                    '    GetDownLevelByNode(treeOrgFunction.Nodes(0), lstPath)
                                    'End If
                                    ' số bản ghi cập nhật
                                    'Dim iSoBanGhi As Integer = 50
                                    ' số dư để làm tròn
                                    'Dim iSoDu As Integer = lstPath.Count Mod iSoBanGhi
                                    'Dim iTongVongLap As Integer
                                    ' số vòng lặp khi làm tròn vs số bản ghi cập nhật
                                    'If iSoDu = 0 Then
                                    '    iTongVongLap = (lstPath.Count - iSoDu) / iSoBanGhi
                                    'Else
                                    '    iTongVongLap = ((lstPath.Count - iSoDu) / iSoBanGhi) + 1
                                    'End If
                                    'For item = 0 To iTongVongLap - 1
                                    '    ' cập nhật từng đợt ( tối đa = số bản ghi cập nhật )
                                    '    Dim lstUpdate As New List(Of OrganizationPathDTO)

                                    '    If item <> iTongVongLap - 1 Then
                                    '        For idx = item * iSoBanGhi To (item + 1) * iSoBanGhi - 1
                                    '            lstUpdate.Add(lstPath(idx))
                                    '        Next
                                    '    Else
                                    '        For idx = item * iSoBanGhi To lstPath.Count - 1
                                    '            lstUpdate.Add(lstPath(idx))
                                    '        Next
                                    '    End If
                                    '    ' cập nhật bản ghi
                                    '    rep.ModifyOrganizationPath(lstUpdate)
                                    'Next

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                                Common.Common.OrganizationLocationDataSession = Nothing
                            Case CommonMessage.STATE_EDIT
                                If treeOrgFunction.SelectedNode IsNot Nothing Then
                                    objPath = GetUpLevelByNode(treeOrgFunction.SelectedNode)
                                    objOrgFunction.HIERARCHICAL_PATH = objPath.HIERARCHICAL_PATH
                                    objOrgFunction.DESCRIPTION_PATH = objPath.DESCRIPTION_PATH
                                End If
                                objOrgFunction.ID = Decimal.Parse(hidID.Value)
                                'objOrgFunction.ID = Decimal.Parse(hidID.Value)
                                'GetDownLevelByNode(treeOrgFunction.Nodes(0), lstPath)
                                'objOrgFunction.DESCRIPTION_PATH = objPath.DESCRIPTION_PATH.Replace(treeOrgFunction.SelectedNode.Text, txtCode.Text + " - " + txtNameVN.Text)
                                If rep.ModifyOrganization(objOrgFunction, gID) Then
                                    Refresh("UpdateView")
                                    GetDownLevelByNode(treeOrgFunction.Nodes(0), lstPath)
                                    ' số bản ghi cập nhật
                                    Dim iSoBanGhi As Integer = 50
                                    ' số dư để làm tròn
                                    Dim iSoDu As Integer = lstPath.Count Mod iSoBanGhi
                                    Dim iTongVongLap As Integer
                                    ' số vòng lặp khi làm tròn vs số bản ghi cập nhật
                                    If iSoDu = 0 Then
                                        iTongVongLap = (lstPath.Count - iSoDu) / iSoBanGhi
                                    Else
                                        iTongVongLap = ((lstPath.Count - iSoDu) / iSoBanGhi) + 1
                                    End If

                                    For item = 0 To iTongVongLap - 1
                                        ' cập nhật từng đợt ( tối đa = số bản ghi cập nhật )
                                        Dim lstUpdate As New List(Of OrganizationPathDTO)

                                        If item <> iTongVongLap - 1 Then
                                            For idx = item * iSoBanGhi To (item + 1) * iSoBanGhi - 1
                                                lstUpdate.Add(lstPath(idx))
                                            Next
                                        Else
                                            For idx = item * iSoBanGhi To lstPath.Count - 1
                                                lstUpdate.Add(lstPath(idx))
                                            Next
                                        End If
                                        ' cập nhật bản ghi
                                        rep.ModifyOrganizationPath(lstUpdate)
                                    Next

                                    CurrentState = CommonMessage.STATE_NORMAL

                                    Common.Common.OrganizationLocationDataSession = Nothing
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                        If gID <> 0 Then
                            If ImageFile IsNot Nothing Then
                                Dim filePath = AppDomain.CurrentDomain.BaseDirectory & "ReportTemplates\Profile\Organization\Logo\"
                                If Not Directory.Exists(filePath) Then
                                    Directory.CreateDirectory(filePath)
                                End If
                                Dim fileName As String = gID & "_" & ImageFile.GetName
                                Dim dirs As String() = Directory.GetFiles(filePath, gID & "_*")
                                If dirs.Length > 0 Then
                                    For Each str As String In dirs
                                        If File.Exists(str) Then
                                            Try
                                                File.Delete(str)
                                            Catch ex As Exception
                                            End Try
                                        End If
                                    Next
                                End If
                                ImageFile.SaveAs(filePath & fileName)
                                'rbiEmployeeImage.Visible = True
                            End If
                            If LicenseFile IsNot Nothing Then
                                Dim filePath = AppDomain.CurrentDomain.BaseDirectory & "ReportTemplates\Profile\Organization\License\"
                                If Not Directory.Exists(filePath) Then
                                    Directory.CreateDirectory(filePath)
                                End If
                                Dim fileName As String = gID & "_" & LicenseFile.GetName
                                Dim dirs As String() = Directory.GetFiles(filePath, gID & "_*")
                                If dirs.Length > 0 Then
                                    For Each str As String In dirs
                                        If File.Exists(str) Then
                                            Try
                                                File.Delete(str)
                                            Catch ex As Exception
                                            End Try
                                        End If
                                    Next
                                End If
                                LicenseFile.SaveAs(filePath & fileName)
                                'btnDownFile.Visible = True
                            End If
                        End If
                End Select
                rep.Dispose()
                UpdateToolbarState(CurrentState)
                UpdateControlState()
                'FillDataByTree()
                check = ""
                If CurrentState.ToUpper() = "NEW" Then
                    'chkOrgChart.Checked = True
                    txtNameVN.Text = ""
                    txtNameEN.Text = ""
                    'txtRepresentativeName.Text = ""
                    txtCode.Text = ""
                    'rtNUMBER_BUSINESS.Text = ""
                    rtADDRESS.Text = ""
                    'txtLocationWork.Text = ""
                    'txtTypeDecision.Text = ""
                    'txtNumberDecision.Text = ""
                    'rdEffectDate.SelectedDate = Nothing
                    rdFOUNDATION_DATE.SelectedDate = Nothing
                    rdOrdNo.Value = Nothing
                    'rdDATE_BUSINESS.SelectedDate = Nothing
                    'rdDicision_Date.SelectedDate = Nothing
                    txtREMARK.Text = ""
                    rdOrdNo.Value = Nothing
                    GetDataCombo()
                End If
                _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                DisplayException(Me.ViewName, Me.ID, ex)
                _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            End Try
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    Public Sub ctrlOrganization_OnReceiveData(ByVal sender As IViewListener(Of ViewBase), ByVal e As ViewCommunicationEventArgs) Handles Me.OnReceiveData
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim mess As MessageDTO = CType(e.EventData, MessageDTO)
            Select Case e.FromViewID
                Case "ctrlOrganizationNew"
                    Select Case mess.Mess
                        Case CommonMessage.ACTION_INSERTED
                            CurrentState = CommonMessage.STATE_NEW
                            Refresh("InsertView")
                            UpdateControlState()
                        Case CommonMessage.TOOLBARITEM_CANCEL
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                    End Select
                Case "ctrlOrganizationEdit"
                    Select Case mess.Mess
                        Case CommonMessage.ACTION_UPDATED
                            CurrentState = CommonMessage.STATE_NORMAL
                            Refresh("UpdateView")
                            UpdateControlState()
                        Case CommonMessage.TOOLBARITEM_CANCEL
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                    End Select
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                If ActiveOrganizations(0).ACTFLG = "A" Then
                    CurrentState = CommonMessage.STATE_DEACTIVE
                Else
                    CurrentState = CommonMessage.STATE_ACTIVE
                End If
                UpdateControlState()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    Protected Sub cbDissolve_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbDissolve.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SelectOrgFunction = treeOrgFunction.SelectedValue
            BuildTreeNode(treeOrgFunction, Organizations, cbDissolve.Checked)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

    Protected Sub treeOrgFunction_NodeClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles treeOrgFunction.NodeClick
        Dim lstPath As New List(Of OrganizationPathDTO)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If treeOrgFunction.SelectedNode Is Nothing Then
                Exit Sub
            End If
            GetUpLevelByNode(treeOrgFunction.SelectedNode)
            hidID.Value = treeOrgFunction.SelectedNode.Value
            SelectOrgFunction = treeOrgFunction.SelectedNode.Value
            treeOrgFunction.SelectedNode.ExpandParentNodes()
            FillDataByTree()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub btnREPRESENTATIVE_ID_Click(sender As Object, e As System.EventArgs) Handles btnREPRESENTATIVE_ID.Click
        Try
            ctrlREPRESENTATIVE_ID.Show()
        Catch ex As Exception
            ShowMessage(ex.ToString, NotifyType.Error)
        End Try
    End Sub

    Private Sub ctrlREPRESENTATIVE_ID_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlREPRESENTATIVE_ID.EmployeeSelected
        Try
            Dim lstCommonEmployee = ctrlREPRESENTATIVE_ID.SelectedEmployee
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidREPRESENTATIVE_ID.Value = item.ID
                txtREPRESENTATIVE_ID.Text = item.FULLNAME_VN
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnACCOUNTING_ID_Click(sender As Object, e As System.EventArgs) Handles btnACCOUNTING_ID.Click
        Try
            CtrlhidACCOUNTING_ID.Show()
        Catch ex As Exception
            ShowMessage(ex.ToString, NotifyType.Error)
        End Try
    End Sub

    Private Sub CtrlhidACCOUNTING_ID_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlhidACCOUNTING_ID.EmployeeSelected
        Try
            Dim lstCommonEmployee = CtrlhidACCOUNTING_ID.SelectedEmployee
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidACCOUNTING_ID.Value = item.ID
                txtACCOUNTING_ID.Text = item.FULLNAME_VN
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnHR_ID_Click(sender As Object, e As System.EventArgs) Handles btnHR_ID.Click
        Try
            CtrlHR_ID.Show()
        Catch ex As Exception
            ShowMessage(ex.ToString, NotifyType.Error)
        End Try
    End Sub

    Private Sub CtrlHR_ID_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlHR_ID.EmployeeSelected
        Try
            Dim lstCommonEmployee = CtrlHR_ID.SelectedEmployee
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidHR_ID.Value = item.ID
                txtHR_ID.Text = item.FULLNAME_VN
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#Region "old"
    ''' <summary>
    ''' Author: TUNGNT
    ''' Des: Show Pop up chon nhan vien quan ly don vi
    ''' Date: 13/05/2018
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    'Protected Sub btnFindRepresentative_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindRepresentative.Click
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        isLoadPopup = 1
    '        UpdateControlState()
    '        ctrlFindEmployeePopup.Show()
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try

    'End Sub
    'Private Sub ctrlFindEmployeePopup_CancelClick(ByVal sender As Object, ByVal e As System.EventArgs) _
    '    Handles ctrlFindEmployeePopup.CancelClicked
    '    isLoadPopup = 0
    'End Sub

    'Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
    '    Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
    '    'Dim rep As New ProfileBusinessRepository
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

    '    Try
    '        lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
    '        If lstCommonEmployee.Count <> 0 Then
    '            Dim item = lstCommonEmployee(0)
    '            hidRepresentative.Value = item.ID.ToString
    '            'txtRepresentativeName.Text = item.FULLNAME_VN
    '            'lblChucDanh.Text = item.TITLE_NAME
    '            DisplayImage(item.ID, item.IMAGE)
    '        End If
    '        isLoadPopup = 0
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
#End Region


#End Region

#Region "Custom"

    Public Overrides Sub UpdateControlState()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            'Select Case isLoadPopup
            '    Case 1
            '        If Not phPopup.Controls.Contains(ctrlFindEmployeePopup) Then
            '            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
            '            phPopup.Controls.Add(ctrlFindEmployeePopup)
            '            ctrlFindEmployeePopup.MultiSelect = False
            '            ctrlFindEmployeePopup.MustHaveContract = False
            '            ctrlFindEmployeePopup.LoadAllOrganization = True
            '        End If
            'End Select
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                Case CommonMessage.STATE_NORMAL
                    UpdateToolbarState(CurrentState)
                Case CommonMessage.STATE_EDIT

                Case CommonMessage.STATE_DEACTIVE
                    If rep.ActiveOrganization(ActiveOrganizations, "I") Then
                        ActiveOrganizations = Nothing
                        Refresh("UpdateView")
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                Case CommonMessage.STATE_ACTIVE
                    If rep.ActiveOrganization(ActiveOrganizations, "A") Then
                        ActiveOrganizations = Nothing
                        Refresh("UpdateView")
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    Private Sub UpdateToolbarState(ByVal sState As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case sState
                Case CommonMessage.STATE_NORMAL
                    EnableControl(False)
                Case CommonMessage.STATE_NEW
                    EnableControl(True)
                    ClearControl()
                Case (CommonMessage.STATE_EDIT)
                    EnableControl(True)
                    treeOrgFunction.Enabled = False
                Case "Nothing"
                    txtCode.ReadOnly = False
                    txtCode.Text = ""
                    txtNameVN.Text = ""
                    hidID.Value = ""
                    hidParentID.Value = ""
                    'hidRepresentative.Value = ""
                    txtParent_Name.Text = ""
                    rtADDRESS.Text = ""
                    txtREMARK.Text = ""
                    cboU_insurance.Text = ""
                    cboRegion.Text = ""
                    _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            End Select
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    Sub ClearControl()
        txtCode.Text = ""
        txtREMARK.Text = ""
        rtADDRESS.Text = ""
        check = "EDITFILE"
        rtADDRESS.Text = ""
        txtREMARK.Text = ""
        cboU_insurance.Text = ""
        cboRegion.Text = ""
        rdFOUNDATION_DATE.SelectedDate = Nothing
        rdOrdNo.Value = Nothing

        chkIsSignContract.Checked = False
        txtCONTRACT_CODE.Text = ""
        cboGROUP_PAID_ID.Text = ""
        cboUNIT_RANK_ID.Text = ""
        cboPROVINCE_ID.Text = ""
        cboDISTRICT_ID.Text = ""
        rdOrdNo.Value = Nothing
        cboU_insurance.Text = ""
        cboRegion.Text = ""

        cbDissolve.Text = ""
        txtFAX.Text = ""
        txtWEBSITE_LINK.Text = ""
        txtBANK_NO.Text = ""
        cboBANK_ID.Text = ""
        cboBANK_BRACH_ID.Text = ""
        txtPIT_NO.Text = ""
        'btnREPRESENTATIVE_ID.Enabled = IsEnable
        txtACCOUNTING_ID.Text = ""
        'btnHR_ID.Enabled = IsEnable
        txtAUTHOR_LETTER.Text = ""
        cboPROVINCE_CONTRACT_ID.Text = ""
        cboDISTRICT_CONTRACT_ID.Text = ""
        txtNUMBER_BUSINESS.Text = ""
        txtBUSS_REG_NAME.Text = ""
        txtMAN_UNI_NAME.Text = ""
        rdDISSOLVE_DATE.SelectedDate = Nothing
    End Sub
    Sub EnableControl(ByVal IsEnable As Boolean)
        treeOrgFunction.Enabled = Not IsEnable
        txtCode.Enabled = IsEnable
        'txtCode.ReadOnly = Not IsEnable
        txtNameVN.ReadOnly = Not IsEnable
        'txtNameVN.CausesValidation = IsEnable
        txtNameEN.ReadOnly = Not IsEnable
        'txtCode.CausesValidation = IsEnable

        txtREMARK.ReadOnly = Not IsEnable
        rtADDRESS.ReadOnly = Not IsEnable
        txtSHORT_NAME.ReadOnly = Not IsEnable
        chkIsSignContract.Enabled = IsEnable
        txtCONTRACT_CODE.ReadOnly = Not IsEnable
        cboGROUP_PAID_ID.Enabled = IsEnable
        cboUNIT_RANK_ID.Enabled = IsEnable
        cboPROVINCE_ID.Enabled = IsEnable
        cboDISTRICT_ID.Enabled = IsEnable
        rdFOUNDATION_DATE.Enabled = IsEnable
        rdOrdNo.Enabled = IsEnable
        cboU_insurance.Enabled = IsEnable
        cboRegion.Enabled = IsEnable

        cbDissolve.Enabled = Not IsEnable
        txtFAX.ReadOnly = Not IsEnable
        txtWEBSITE_LINK.ReadOnly = Not IsEnable
        txtBANK_NO.ReadOnly = Not IsEnable
        cboBANK_ID.Enabled = IsEnable
        cboBANK_BRACH_ID.Enabled = IsEnable
        txtPIT_NO.ReadOnly = Not IsEnable
        btnREPRESENTATIVE_ID.Enabled = IsEnable
        btnACCOUNTING_ID.Enabled = IsEnable
        btnHR_ID.Enabled = IsEnable
        txtAUTHOR_LETTER.ReadOnly = Not IsEnable
        cboPROVINCE_CONTRACT_ID.Enabled = IsEnable
        cboDISTRICT_CONTRACT_ID.Enabled = IsEnable
        txtNUMBER_BUSINESS.ReadOnly = Not IsEnable
        txtBUSS_REG_NAME.ReadOnly = Not IsEnable
        txtMAN_UNI_NAME.ReadOnly = Not IsEnable
        rdDISSOLVE_DATE.Enabled = IsEnable
    End Sub
    Protected Sub BuildTreeNode(ByVal tree As RadTreeView,
                                ByVal list As List(Of OrganizationDTO),
                                ByVal bCheck As Boolean)
        Dim node As New RadTreeNode
        Dim bSelected As Boolean = False
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try

            tree.Nodes.Clear()
            If list.Count = 0 Then
                Exit Sub
            End If
            Dim listTemp = (From t In list
                            Where t.PARENT_ID Is Nothing Select t).FirstOrDefault
            Select Case Common.Common.SystemLanguage.Name
                Case "vi-VN"
                    node.Text = listTemp.NAME_VN
                    node.ToolTip = listTemp.NAME_VN
                Case Else
                    node.Text = listTemp.NAME_EN
                    node.ToolTip = listTemp.NAME_EN
            End Select

            node.Value = listTemp.ID.ToString
            If SelectOrgFunction IsNot Nothing Then
                If node.Value = SelectOrgFunction Then
                    node.Selected = True
                    bSelected = True
                End If

            End If

            tree.Nodes.Add(node)
            BuildTreeChildNode(node, list, bCheck, bSelected)
            'tree.ExpandAllNodes()
            If SelectOrgFunction = "" Then
                If tree.Nodes.Count > 0 Then
                    If tree.Nodes(0).Nodes.Count > 0 Then
                        tree.Nodes(0).Nodes(0).Selected = True
                        tree.Nodes(0).ExpandParentNodes()
                        treeOrgFunction_NodeClick(Nothing, Nothing)
                        bSelected = True
                    End If
                End If
            Else
                Dim treeNode As RadTreeNode = Nothing
                For Each n As RadTreeNode In tree.Nodes
                    treeNode = getNodeSelected(n)
                    If treeNode IsNot Nothing Then
                        Exit For
                    End If
                Next
                If treeNode IsNot Nothing Then
                    treeNode.Selected = True
                    treeNode.ExpandParentNodes()

                End If
            End If
            ' Nếu không có node nào được chọn thì load lại control
            If Not bSelected Then
                SelectOrgFunction = ""
                UpdateToolbarState("Nothing")
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Function getNodeSelected(ByVal node As RadTreeNode) As RadTreeNode
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If node.Value = SelectOrgFunction Then
                Return node
            End If
            For Each n As RadTreeNode In node.Nodes
                If n.Value = SelectOrgFunction Then
                    Return n
                End If
                Dim _node As RadTreeNode = getNodeSelected(n)
                If _node IsNot Nothing Then
                    Return _node
                End If
            Next
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
        Return Nothing
    End Function

    Protected Sub BuildTreeChildNode(ByVal nodeParent As RadTreeNode,
                                     ByVal list As List(Of OrganizationDTO),
                                     ByVal bCheck As Boolean,
                                     ByRef bSelected As Boolean)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Dim listTemp As List(Of OrganizationDTO)
        Try
            If bCheck Then
                listTemp = (From t In list
                            Where t.PARENT_ID = Decimal.Parse(nodeParent.Value)).ToList
            Else
                listTemp = (From t In list
                            Where t.PARENT_ID = Decimal.Parse(nodeParent.Value) _
                            And (t.DISSOLVE_DATE Is Nothing Or (t.DISSOLVE_DATE IsNot Nothing And t.DISSOLVE_DATE >= Date.Now)) _
                            And t.ACTFLG = "A").ToList
            End If

            If listTemp.Count = 0 Then
                Exit Sub
            End If
            For index As Integer = 0 To listTemp.Count - 1
                Dim node As New RadTreeNode
                Select Case Common.Common.SystemLanguage.Name
                    Case "vi-VN"
                        'node.Text = listTemp(index).CODE & " - " & listTemp(index).NAME_VN
                        node.Text = listTemp(index).NAME_VN
                        node.ToolTip = listTemp(index).NAME_VN
                    Case Else
                        'node.Text = listTemp(index).CODE & " - " & listTemp(index).NAME_EN
                        node.Text = listTemp(index).NAME_EN
                        node.ToolTip = listTemp(index).NAME_EN
                End Select

                node.Value = listTemp(index).ID.ToString
                If bCheck Then
                    If listTemp(index).DISSOLVE_DATE IsNot Nothing Then
                        If listTemp(index).DISSOLVE_DATE < Date.Now Then
                            node.BackColor = Drawing.Color.Yellow
                        End If
                    End If
                End If
                If listTemp(index).ACTFLG = "I" Then
                    node.BackColor = Drawing.Color.Yellow
                End If
                If SelectOrgFunction IsNot Nothing Then
                    If node.Value = SelectOrgFunction Then
                        node.Selected = True
                        bSelected = True
                    End If
                End If
                nodeParent.Nodes.Add(node)
                BuildTreeChildNode(node, list, bCheck, bSelected)
            Next
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Author: TUNGNT
    ''' Des: chinh sua theo tinh nang man hinh moi
    ''' Date: 13/05/2018
    ''' </summary>
    Private Sub FillDataByTree()
        Dim orgItem As OrganizationDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If treeOrgFunction.SelectedNode Is Nothing Then
                Exit Sub
            End If
            orgItem = (From p In Organizations Where p.ID = Decimal.Parse(treeOrgFunction.SelectedNode.Value)).SingleOrDefault
            If orgItem IsNot Nothing Then
                hidParentID.Value = orgItem.PARENT_ID.ToString
                txtParent_Name.Text = orgItem.PARENT_NAME
                txtCode.Text = orgItem.CODE
                'txtLocationWork.Text = orgItem.LOCATION_WORK
                'txtNumberDecision.Text = orgItem.NUMBER_DECISION
                'txtTypeDecision.Text = orgItem.TYPE_DECISION

                txtNameVN.Text = orgItem.NAME_VN
                txtNameEN.Text = orgItem.NAME_EN
                'Dim strFiles As String = orgItem.FILES
                'If check = "EDITFILE" Then
                '    strFiles = ""
                'End If
                'If strFiles <> "" Then
                '    For Each items As String In strFiles.Split("#")
                '        Dim i As New RadListBoxItem(items, items)
                '        i.Checked = True
                '        lstFile.Items.Add(i)
                '    Next
                'End If
                If IsNumeric(orgItem.U_INSURANCE) AndAlso orgItem.U_INSURANCE > 0 Then
                    cboU_insurance.SelectedValue = orgItem.U_INSURANCE
                Else
                    ClearControlValue(cboU_insurance)
                End If
                'cboU_insurance.SelectedValue = orgItem.U_INSURANCE
                'If IsNumeric(orgItem.ORG_LEVEL) AndAlso orgItem.ORG_LEVEL > 0 Then
                '    cboOrg_level.SelectedValue = orgItem.ORG_LEVEL
                'Else
                '    ClearControlValue(cboOrg_level)
                'End If
                'cboOrg_level.SelectedValue = orgItem.ORG_LEVEL
                If orgItem.REGION_ID > 0 Then
                    cboRegion.SelectedValue = orgItem.REGION_ID
                Else
                    ClearControlValue(cboRegion)
                End If
                'cboRegion.SelectedValue = If(orgItem.REGION_ID > 0, orgItem.REGION_ID, -1)
                'txtRepresentativeName.Text = orgItem.REPRESENTATIVE_NAME
                'rtNUMBER_BUSINESS.Text = orgItem.NUMBER_BUSINESS
                'rdDATE_BUSINESS.SelectedDate = Nothing
                'If IsDate(orgItem.DATE_BUSINESS) Then
                '    rdDATE_BUSINESS.SelectedDate = orgItem.DATE_BUSINESS
                'End If
                'rdEffectDate.SelectedDate = Nothing
                'If IsDate(orgItem.EFFECT_DATE) Then
                '    rdEffectDate.SelectedDate = orgItem.EFFECT_DATE
                'Else
                '    rdEffectDate.SelectedDate = Nothing
                'End If
                If IsDate(orgItem.FOUNDATION_DATE) Then
                    rdFOUNDATION_DATE.SelectedDate = orgItem.FOUNDATION_DATE
                Else
                    ClearControlValue(rdFOUNDATION_DATE)
                End If
                If IsNumeric(orgItem.ORD_NO) Then
                    rdOrdNo.Value = orgItem.ORD_NO
                Else
                    rdOrdNo.Value = 0
                End If
                'rdDicision_Date.SelectedDate = Nothing
                'If IsDate(orgItem.DISSOLVE_DATE) Then
                '    rdDicision_Date.SelectedDate = orgItem.DISSOLVE_DATE
                'End If
                'If orgItem.CHK_ORGCHART = True Then
                '    chkOrgChart.Checked = True
                'Else
                '    chkOrgChart.Checked = False
                'End If
                rtADDRESS.Text = orgItem.ADDRESS
                txtREMARK.Text = orgItem.REMARK
                'If orgItem.REPRESENTATIVE_ID IsNot Nothing Then
                '    hidRepresentative.Value = orgItem.REPRESENTATIVE_ID
                'End If
                'If IsNumeric(orgItem.UNIT_LEVEL) AndAlso orgItem.UNIT_LEVEL > 0 Then
                '    cbUNIT_LEVEL.SelectedValue = orgItem.UNIT_LEVEL
                'Else
                '    ClearControlValue(cbUNIT_LEVEL)
                'End If
                DisplayImage(Utilities.ObjToDecima(orgItem.REPRESENTATIVE_ID), Utilities.ObjToString(orgItem.IMAGE))
                If treeOrgFunction.SelectedNode.Level = 1 Then
                    Dim logoPath = AppDomain.CurrentDomain.BaseDirectory & "ReportTemplates\Profile\Organization\Logo\"
                    If Not Directory.Exists(logoPath) Then
                        Directory.CreateDirectory(logoPath)
                    End If
                    Dim logo = treeOrgFunction.SelectedNode.Value & "_*"
                    Dim dirs As String() = Directory.GetFiles(logoPath, logo)
                    Dim licensePath = AppDomain.CurrentDomain.BaseDirectory & "ReportTemplates\Profile\Organization\License\"
                    If Not Directory.Exists(licensePath) Then
                        Directory.CreateDirectory(licensePath)
                    End If
                    Dim license = treeOrgFunction.SelectedNode.Value & "_*"
                    dirs = Directory.GetFiles(logoPath, license)
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    Private Sub DisplayImage(ByVal empid As String, ByVal image As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'Dim rep As New ProfileBusinessRepository
            Dim sError As String = ""
            If image IsNot Nothing Then
                'rbiImage.DataValue = rep.GetEmployeeImage(empid, sError) 'Lấy ảnh của nhân viên.
            Else
                'rbiImage.DataValue = rep.GetEmployeeImage(0, sError) 'Lấy ảnh mặc định (NoImage.jpg)
            End If
            Exit Sub
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Function GetUpLevelByNode(ByVal node As RadTreeNode) As OrganizationPathDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Dim strText As String = ""
        Dim strValue As String = ""
        Dim iLevel As Integer
        Dim curNode As RadTreeNode
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            curNode = node
            iLevel = curNode.Level

            For idx = 0 To iLevel
                If idx = 0 Then
                    strValue = curNode.Value
                    strText = curNode.Text
                Else
                    curNode = curNode.ParentNode
                    strValue = curNode.Value + ";" + strValue
                    strText = curNode.Text + ";" + strText
                End If
            Next

            Return New OrganizationPathDTO With {.ID = Decimal.Parse(node.Value),
                                                 .HIERARCHICAL_PATH = strValue,
                                                 .DESCRIPTION_PATH = strText}
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    Private Sub GetDownLevelByNode(ByVal node As RadTreeNode, ByRef lstPath As List(Of OrganizationPathDTO))
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If node.Nodes.Count > 0 Then
                For Each child As RadTreeNode In node.Nodes
                    Dim objPath = GetUpLevelByNode(child)
                    objPath.DESCRIPTION_PATH = objPath.DESCRIPTION_PATH.Replace(treeOrgFunction.SelectedNode.Text, txtNameVN.Text)
                    lstPath.Add(objPath)
                    GetDownLevelByNode(child, lstPath)
                Next

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Function GetAllChild(ByVal _orgId As Decimal) As List(Of Decimal)
        Dim query As List(Of Decimal)
        Dim list As List(Of Decimal)
        Dim result As New List(Of Decimal)
        'Dim rep As New CommonRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try

            result.Add(_orgId)
            Dim lstOrgs As List(Of OrganizationDTO) = Organizations

            query = (From p In lstOrgs Where p.PARENT_ID = _orgId
                     Select p.ID).ToList
            For Each q As Decimal In query
                list = GetAllChild(q)
                result.InsertRange(0, list)
            Next
            Return result
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Function


#End Region

End Class
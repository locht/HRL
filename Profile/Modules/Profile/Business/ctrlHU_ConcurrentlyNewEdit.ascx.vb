Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Profile
Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports Telerik.Web.UI
Imports System.IO
Imports Common.CommonBusiness
Imports Ionic.Zip
Imports System.Reflection

Public Class ctrlHU_ConcurrentlyNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindEmployeeSign As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindEmployeeSign2 As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindEmployeeSignStop As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindEmployeeSignStop2 As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindOrgGocPopup As ctrlFindOrgPopup
    Dim com As New CommonProcedureNew
    Dim log As New UserLog
    Dim psp As New ProfileStoreProcedure
    Dim cons_com As New Contant_Common
#Region "Property"
    Property Is_con As Integer
        Get
            Return ViewState(Me.ID & "_Is_con")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_Is_con") = value
        End Set

    End Property
    Property LinkEmpId As String
        Get
            Return ViewState(Me.ID & "_LinkEmpId")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_LinkEmpId") = value
        End Set

    End Property

    Property checkAuthor As Integer
        Get
            Return ViewState(Me.ID & "_checkAuthor")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_checkAuthor") = value
        End Set
    End Property

    Private Property checkStatus As String
        Get
            Return PageViewState(Me.ID & "_checkStatus")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_checkStatus") = value
        End Set

    End Property

    Private Property FileUpLoad As String
        Get
            Return PageViewState(Me.ID & "_FileUpLoad")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_FileUpLoad") = value
        End Set
    End Property

    Private Property tempPathFile As String
        Get
            Return PageViewState(Me.ID & "_tempPathFile")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_tempPathFile") = value
        End Set
    End Property

    Private Property lstFile As String
        Get
            Return PageViewState(Me.ID & "_lstFile")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_lstFile") = value
        End Set

    End Property

    Private Property strID As String
        Get
            Return PageViewState(Me.ID & "_strID")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_strID") = value
        End Set

    End Property

    Property APPROVAL As Integer
        Get
            Return ViewState(Me.ID & "_APPROVAL")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_APPROVAL") = value
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

    'Property SalaryList As List(Of EmployeeSalaryDTO)
    '    Get
    '        Return ViewState(Me.ID & "_SalaryList")
    '    End Get
    '    Set(ByVal value As List(Of EmployeeSalaryDTO))
    '        ViewState(Me.ID & "_SalaryList") = value
    '    End Set
    'End Property
    Property OrgTitle As List(Of OrgTitleDTO)
        Get
            Return ViewState(Me.ID & "_OrgTitle")
        End Get
        Set(ByVal value As List(Of OrgTitleDTO))
            ViewState(Me.ID & "_OrgTitle") = value
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

    Property SelectedItem As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_SelectedItem")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItem") = value
        End Set
    End Property

    '0 - normal
    '1 - Employee
    '2 - Sign
    '3 - Org
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    Property LoadNew As Boolean = True
    Property FormType As String
        Get
            Return ViewState(Me.ID & "_FormType")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_FormType") = value
        End Set
    End Property

    Property dtCon As DataTable
        Get
            Return ViewState(Me.ID & "_dtCon")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtCon") = value
        End Set
    End Property

    Property dtSalary As DataTable
        Get
            Return ViewState(Me.ID & "_dtSalary")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtSalary") = value
        End Set
    End Property

    Private Property lstFile1 As String
        Get
            Return PageViewState(Me.ID & "_lstFile1")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_lstFile1") = value
        End Set

    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            If Not IsPostBack Then
                GetParams()
                tempPathFile = Server.MapPath(ConfigurationManager.AppSettings("PathFileAppendixContract"))
                FileUpLoad = ConfigurationManager.AppSettings("FileUpload")
            End If
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            If Request.Params("FormType") IsNot Nothing Then
                FormType = Request.Params("FormType")
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            GetDataCombo()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub GetDataCombo()
        Dim dtData As New DataTable
        Try
            'Default là trạng thái 
            dtData = com.GET_COMBOBOX("OT_OTHER_LIST", "CODE", "NAME_VN", " TYPE_CODE='" + "APPROVE_STATUS2" + "' ", "NAME_VN", True)
            If dtData.Rows.Count > 0 Then
                FillRadCombobox(cboStatus, dtData, "NAME_VN", "CODE", False)

                FillRadCombobox(cbSTATUS_STOP, dtData, "NAME_VN", "CODE", False)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarCon
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel,
                                       ToolbarItem.Delete)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            Select Case FormType
                Case 0
                    CurrentState = CommonMessage.STATE_NEW
                Case 1
                    CurrentState = CommonMessage.STATE_EDIT
                    If Not IsPostBack Then
                        FillData()
                        rgConcurrently.Rebind()
                        SelectedItemDataGridByKey(rgConcurrently, Decimal.Parse(IDSelect))
                        EnabledGrid(rgConcurrently, False)
                    End If
                Case 2
                    CurrentState = CommonMessage.STATE_NORMAL
                    If Not IsPostBack Then
                        FillData()
                        rgConcurrently.Rebind()
                        SelectedItemDataGridByKey(rgConcurrently, Decimal.Parse(IDSelect))
                    End If
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objWorking As New WorkingDTO
        Dim tempPath As String = ConfigurationManager.AppSettings("WordFileFolder")
        Dim _err As String = ""
        Dim status As Integer
        Dim id As Integer
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    FormType = 0
                    'com.ResetControlValue(LeftPane)
                    IDSelect = 0
                    hidEmpID.Value = ""
                    checkStatus = "0"
                    rgConcurrently.Rebind()
                    EnabledGrid(rgConcurrently, False)
                    cboStatus.Text = ""
                    cboStatus.SelectedValue = cons_com.AWAITING_APPROVAL
                    cbSTATUS_STOP.Text = ""
                    cbSTATUS_STOP.SelectedValue = cons_com.AWAITING_APPROVAL
                Case CommonMessage.TOOLBARITEM_EDIT
                    If cboStatus.SelectedValue = "1" Then
                        ShowMessage("Bản ghi đã được phê duyệt", NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgConcurrently.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    FormType = 1
                    EnabledGrid(rgConcurrently, False)
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If Save(strID, _err) Then
                            FillData()
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                        End If
                        FormType = 2
                        EnabledGrid(rgConcurrently, True)
                        rgConcurrently.Rebind()
                        SelectedItemDataGridByKey(rgConcurrently, Decimal.Parse(IDSelect))
                        FillData()
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    FormType = 2
                    FillData()
                    EnabledGrid(rgConcurrently, True)
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgConcurrently.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each grid As GridDataItem In rgConcurrently.SelectedItems
                        id = Decimal.Parse(grid.GetDataKeyValue("ID").ToString())
                        status = If(grid.GetDataKeyValue("STATUS").ToString() = "", 0, Decimal.Parse(grid.GetDataKeyValue("STATUS").ToString()))
                        If status = 1 Then
                            ShowMessage("Bản ghi đã được phê duyệt", NotifyType.Warning)
                            Exit Sub
                        End If
                        strID &= IIf(strID = vbNullString, id, "," & id)
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                    'Case CommonMessage.TOOLBARITEM_PRINT
                    '    PrintConcurrently("PCKN")
                    'Case CommonMessage.TOOLBARITEM_REJECT
                    '    PrintConcurrently("TPCKN")
            End Select
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        'Dim rep As New ProfileBusinessRepository
        log = LogHelper.GetUserLog
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                'If com.DELETE_DATA_BY_TABLE(cons.HU_CONCURRENTLY, strID, log.Username, log.Ip + "/" + log.ComputerName) = 1 Then
                '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                '    FormType = 2
                '    UpdateControlState()
                '    com.ResetControlValue(LeftPane)
                '    rgConcurrently.Rebind()
                'Else
                '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                'End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    'Private Sub cbTitle_ORG_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbTitle_ORG.SelectedIndexChanged
    '    If cbTitle_ORG.SelectedValue <> 2 Then
    '        cbTITILE_ORG_TEMP.SelectedValue = cbTitle_ORG.SelectedValue
    '        cbTITILE_ORG_TEMP.Text = cbTitle_ORG.Text
    '    End If
    'End Sub

    Protected Sub btnEmployee_Click(ByVal sender As Object,
                                    ByVal e As EventArgs) Handles _
                                btnFindEmp.Click,
                                btnORG_CON.Click,
                                btnSIGN.Click,
                                btnSIGN_STOP.Click,
                                btnOrgId.Click,
                                btnSIGN2.Click,
                                btnSIGN_STOP2.Click
        Try
            Select Case sender.ID
                Case btnFindEmp.ID
                    isLoadPopup = 1
                Case btnORG_CON.ID
                    isLoadPopup = 2
                Case btnSIGN.ID
                    isLoadPopup = 3
                Case btnSIGN_STOP.ID
                    isLoadPopup = 4
                Case btnOrgId.ID
                    isLoadPopup = 5
                Case btnSIGN2.ID
                    isLoadPopup = 6
                Case btnSIGN_STOP2.ID
                    isLoadPopup = 7
            End Select

            UpdateControlState()
            Select Case sender.ID
                Case btnFindEmp.ID
                    ctrlFindEmployeePopup.Show()
                Case btnORG_CON.ID
                    ctrlFindOrgPopup.Show()
                Case btnSIGN.ID
                    ctrlFindEmployeeSign.Show()
                Case btnSIGN_STOP.ID
                    ctrlFindEmployeeSignStop.Show()
                Case btnSIGN2.ID
                    ctrlFindEmployeeSign2.Show()
                Case btnSIGN_STOP2.ID
                    ctrlFindEmployeeSignStop2.Show()
                Case btnOrgId.ID
                    ctrlFindOrgGocPopup.Show()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFind_CancelClick(ByVal sender As Object,
                                     ByVal e As System.EventArgs) Handles _
                                    ctrlFindEmployeeSign.CancelClicked,
                                    ctrlFindOrgPopup.CancelClicked,
                                    ctrlFindEmployeeSign.CancelClicked,
                                    ctrlFindEmployeeSignStop.CancelClicked,
                                    ctrlFindOrgGocPopup.CancelClicked,
                                    ctrlFindEmployeeSign2.CancelClicked,
                                    ctrlFindEmployeeSignStop.CancelClicked,
                                    ctrlFindEmployeeSignStop.CancelClicked
        isLoadPopup = 0
    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New ProfileBusinessRepository
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidEmpID.Value = item.ID
                EmpOrgNameToolTip.Text = DrawTreeByString(item.ORG_DESC)
                txtEmpCode.Text = item.EMPLOYEE_CODE
                txtEmpName.Text = item.FULLNAME_VN
                txtEmpOrgName.Text = item.ORG_NAME
                hidOrgId.Value = item.ORG_ID
                Dim dtData As DataTable = rep.GET_TITLE_ORG(hidOrgId.Value)
                If dtData.Rows.Count > 0 Then
                    FillRadCombobox(cboTitleId, dtData, "NAME_VN", "ID", True)
                    cboTitleId.Text = String.Empty
                End If
                cboTitleId.SelectedValue = item.TITLE_ID
                rep.Dispose()
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Dim dtData As New DataTable
        Dim rep As New ProfileBusinessRepository
        Try
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgCOn.Value = e.CurrentValue
                txtORG_CONName.Text = orgItem.NAME_VN
                ORG_CONNameToolTip.Text = DrawTreeByString(orgItem.Description_Path)
                ' Lay chuc danh theo phong ban
                dtData = rep.GET_TITLE_ORG(e.CurrentValue)
                If dtData.Rows.Count > 0 Then
                    FillRadCombobox(cboTITLE_CON, dtData, "NAME_VN", "ID", True)
                    cboTITLE_CON.Text = String.Empty
                End If
            End If
            rep.Dispose()
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub

    Private Sub ctrlOrgGocPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgGocPopup.OrganizationSelected
        Dim dtData As New DataTable
        Dim rep As New ProfileBusinessRepository
        Try
            Dim orgItem = ctrlFindOrgGocPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgId.Value = e.CurrentValue
                txtEmpOrgName.Text = orgItem.NAME_VN
                EmpOrgNameToolTip.Text = DrawTreeByString(orgItem.Description_Path)
                ' Lay chuc danh theo phong ban
                dtData = rep.GET_TITLE_ORG(e.CurrentValue)
                If dtData.Rows.Count > 0 Then
                    FillRadCombobox(cboTitleId, dtData, "NAME_VN", "ID", True)
                    cboTitleId.Text = String.Empty
                End If
            End If
            rep.Dispose()
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub

    Private Sub ctrlFindEmployeeSign_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeeSign.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New ProfileBusinessRepository
        Try
            lstCommonEmployee = CType(ctrlFindEmployeeSign.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidSign.Value = item.ID
                txtSIGN.Text = item.FULLNAME_VN
                txtSIGN_TITLE.Text = item.TITLE_NAME
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindEmployeeSign_EmployeeSelected2(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeeSign2.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New ProfileBusinessRepository
        Try
            lstCommonEmployee = CType(ctrlFindEmployeeSign2.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidSign2.Value = item.ID
                txtSIGN2.Text = item.FULLNAME_VN
                txtSIGN_TITLE2.Text = item.TITLE_NAME
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindEmployeeSignStop_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeeSignStop.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New ProfileBusinessRepository
        Try
            lstCommonEmployee = CType(ctrlFindEmployeeSignStop.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidSignStop.Value = item.ID
                txtSIGN_STOP.Text = item.FULLNAME_VN
                txtSIGN_STOP_TITLE.Text = item.TITLE_NAME
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFindEmployeeSignStop_EmployeeSelected2(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeeSignStop2.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New ProfileBusinessRepository
        Try
            lstCommonEmployee = CType(ctrlFindEmployeeSignStop2.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidSignStop2.Value = item.ID
                txtSIGN_STOP2.Text = item.FULLNAME_VN
                txtSIGN_STOP_TITLE2.Text = item.TITLE_NAME
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub rgConcurrently_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgConcurrently.NeedDataSource
        Try
            Dim rep As New ProfileBusinessRepository
            If hidEmpID.Value <> "" Then
                dtCon = rep.GET_CONCURRENTLY_BY_EMP(hidEmpID.Value)
                DesignGrid(dtCon)
                If hidEmpID.Value <> "" Then
                    rgConcurrently.DataSource = dtCon
                Else
                    rgConcurrently.DataSource = Nothing
                End If
            Else
                rgConcurrently.DataSource = Nothing
            End If
            rep.Dispose()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub rgConcurrently_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgConcurrently.SelectedIndexChanged
        Dim item As GridDataItem
        Try
            item = CType(rgConcurrently.SelectedItems(0), GridDataItem)
            IDSelect = item.GetDataKeyValue("ID").ToString
            hidID.Value = IDSelect
            FillData()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ' Cảnh báo nếu trạng thái phê quyệt thì bắt buộc nhập số quyết định
    Private Sub curDecisionNo_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles curDecisionNo.ServerValidate
        Try

            If cboStatus.SelectedValue <> "" Then
                If cboStatus.SelectedValue = 1 AndAlso txtCON_NO.Text.Trim = "" Then
                    args.IsValid = False
                Else
                    args.IsValid = True
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ' Cảnh báo nếu trạng thái phê quyệt thì bắt buộc nhập ngày hiệu lực
    Private Sub curStartDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles curStartDate.ServerValidate
        Try

            If cboStatus.SelectedValue <> "" Then
                If cboStatus.SelectedValue = 1 AndAlso rdSIGN_DATE.SelectedDate Is Nothing Then
                    args.IsValid = False
                Else
                    args.IsValid = True
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub CusToDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cval_EffectDate_ExpireDate.ServerValidate
        Try
            'Nếu ngày nghỉ bé hơn ngày vào thì cảnh báo
            If rdEFFECT_DATE_CON.SelectedDate IsNot Nothing AndAlso rdEXPIRE_DATE_CON.SelectedDate IsNot Nothing Then
                If rdEFFECT_DATE_CON.SelectedDate > rdEXPIRE_DATE_CON.SelectedDate Then
                    args.IsValid = False
                Else
                    args.IsValid = True
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub CusDecisionNoSame_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CusDecisionNoSame.ServerValidate
        'Try
        '    If txtCON_NO.Text = "" Then
        '        args.IsValid = True
        '        Exit Sub
        '    End If
        '    Select Case CurrentState
        '        Case STATE_NEW
        '            Using rep As New ProfileBusinessRepository
        '                args.IsValid = psp.CHECK_CON_CODE(IDSelect, txtCON_NO.Text.Trim, 0, 0, 1)
        '            End Using
        '        Case STATE_EDIT
        '            Using rep As New ProfileBusinessRepository
        '                args.IsValid = psp.CHECK_CON_CODE(IDSelect, txtCON_NO.Text.Trim, hidEmpID.Value, 1, 1)
        '            End Using
        '        Case Else
        '            args.IsValid = True
        '    End Select
        'Catch ex As Exception
        '    DisplayException(Me.ViewName, Me.ID, ex)
        'End Try
    End Sub

#End Region

#Region "Custom"

    Public Overrides Sub UpdateControlState()
        'Dim rep As New ProfileBusinessRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    DisableControls(LeftPane, False)
                    EnabledGrid(rgConcurrently, True)
                Case CommonMessage.STATE_NEW
                    DisableControls(LeftPane, True)
                    EnabledGrid(rgConcurrently, False)
                    ' Nếu đang ở trạng thái thêm mới thì disable thông tin thôi kiêm nhiệm
                    txtCON_NO_STOP.ReadOnly = True
                    rdEFFECT_DATE_STOP.Enabled = False
                    rdSIGN_DATE_STOP.Enabled = False
                    cbSTATUS_STOP.Enabled = False
                    btnSIGN_STOP.Enabled = False
                    btnSIGN_STOP2.Enabled = False
                    txtREMARK_STOP.ReadOnly = True
                    btnUploadFile1.Enabled = False

                    If txtCON_NO.Text = "" Then
                        txtCON_NO.Text = "/QĐ-TGĐ." & DateTime.Now.Year.ToString.Substring(2, 2)
                    End If
                    If txtCON_NO_STOP.Text = "" Then
                        txtCON_NO_STOP.Text = "/QĐ-TGĐ." & DateTime.Now.Year.ToString.Substring(2, 2)
                    End If
                Case CommonMessage.STATE_EDIT
                    DisableControls(LeftPane, True)
                    '' Nếu trạng thái sửa bảng ghi phê duyệt thì disable trạng thái
                    'If checkStatus = "1" Then
                    '    cboStatus.Enabled = False
                    'End If
                    If cboStatus.SelectedValue <> "1" Then
                        ' Nếu đang ở trạng thái thêm mới thì disable thông tin thôi kiêm nhiệm
                        txtCON_NO_STOP.ReadOnly = True
                        rdEFFECT_DATE_STOP.Enabled = False
                        rdSIGN_DATE_STOP.Enabled = False
                        cbSTATUS_STOP.Enabled = False
                        btnSIGN_STOP.Enabled = False
                        txtREMARK_STOP.ReadOnly = True
                        btnUploadFile1.Enabled = False
                        btnDownload1.Enabled = False
                        btnSIGN_STOP2.Enabled = False
                    End If

                    EnabledGrid(rgConcurrently, False)
            End Select

            If ctrlFindEmployeePopup IsNot Nothing AndAlso phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
            End If
            If phFindOrgCon IsNot Nothing AndAlso phFindOrgCon.Controls.Contains(ctrlFindOrgPopup) Then
                phFindOrgCon.Controls.Remove(ctrlFindOrgPopup)
            End If
            If phFindEmployeeGoc IsNot Nothing AndAlso phFindEmployeeGoc.Controls.Contains(ctrlFindOrgGocPopup) Then
                phFindOrgCon.Controls.Remove(ctrlFindOrgGocPopup)
            End If
            If ctrlFindEmployeeSign IsNot Nothing AndAlso phFindSign.Controls.Contains(ctrlFindEmployeeSign) Then
                phFindSign.Controls.Remove(ctrlFindEmployeeSign)
            End If
            If ctrlFindEmployeeSignStop IsNot Nothing AndAlso phFindSignStop.Controls.Contains(ctrlFindEmployeeSignStop) Then
                phFindSignStop.Controls.Remove(ctrlFindEmployeeSignStop)
            End If
            If ctrlFindEmployeeSign2 IsNot Nothing AndAlso phFindSign2.Controls.Contains(ctrlFindEmployeeSign) Then
                phFindSign2.Controls.Remove(ctrlFindEmployeeSign)
            End If
            If ctrlFindEmployeeSignStop2 IsNot Nothing AndAlso phFindSignStop2.Controls.Contains(ctrlFindEmployeeSignStop2) Then
                phFindSignStop2.Controls.Remove(ctrlFindEmployeeSignStop2)
            End If
            ' Tạo các popup
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ' Popup tìm nhân viên
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.IsHideTerminate = False
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.LoadAllOrganization = False
                    End If
                Case 2
                    ' Popup phòng ban
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    phFindOrgCon.Controls.Add(ctrlFindOrgPopup)
                Case 3
                    If Not phFindSign.Controls.Contains(ctrlFindEmployeeSign) Then
                        'Popup người ký
                        ctrlFindEmployeeSign = Me.Register("ctrlFindEmployeeSign", "Common", "ctrlFindEmployeePopup")
                        phFindSign.Controls.Add(ctrlFindEmployeeSign)
                        ctrlFindEmployeeSign.IsHideTerminate = False
                        ctrlFindEmployeeSign.MultiSelect = False
                        ctrlFindEmployeeSign.LoadAllOrganization = True
                    End If


                Case 4
                    If Not phFindSignStop.Controls.Contains(ctrlFindEmployeeSignStop) Then
                        'Popup người ký thôi kiêm nhiệm
                        ctrlFindEmployeeSignStop = Me.Register("ctrlFindEmployeeSignStop", "Common", "ctrlFindEmployeePopup")
                        phFindSignStop.Controls.Add(ctrlFindEmployeeSignStop)
                        ctrlFindEmployeeSignStop.IsHideTerminate = False
                        ctrlFindEmployeeSignStop.MultiSelect = False
                        ctrlFindEmployeeSignStop.LoadAllOrganization = True
                    End If

                Case 5
                    ' Popup phòng ban
                    ctrlFindOrgGocPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    phFindOrgCon.Controls.Add(ctrlFindOrgGocPopup)
                Case 6
                    If Not phFindSign2.Controls.Contains(ctrlFindEmployeeSign) Then
                        'Popup người ký 2
                        ctrlFindEmployeeSign2 = Me.Register("ctrlFindEmployeeSign2", "Common", "ctrlFindEmployeePopup")
                        phFindSign2.Controls.Add(ctrlFindEmployeeSign2)
                        ctrlFindEmployeeSign2.IsHideTerminate = False
                        ctrlFindEmployeeSign2.MultiSelect = False
                        ctrlFindEmployeeSign2.LoadAllOrganization = True
                    End If
                Case 7
                    If Not phFindSignStop2.Controls.Contains(ctrlFindEmployeeSignStop2) Then
                        'Popup người ký thôi kiêm nhiệm
                        ctrlFindEmployeeSignStop2 = Me.Register("ctrlFindEmployeeSignStop2", "Common", "ctrlFindEmployeePopup")
                        phFindSignStop.Controls.Add(ctrlFindEmployeeSignStop2)
                        ctrlFindEmployeeSignStop2.IsHideTerminate = False
                        ctrlFindEmployeeSignStop2.MultiSelect = False
                        ctrlFindEmployeeSignStop2.LoadAllOrganization = True
                    End If

            End Select
            ' Nếu trạng thái khai báo thôi kiêm nhiệm
            If Is_con = 1 Then
                btnFindEmp.Enabled = False
                btnORG_CON.Enabled = False
                btnOrgId.Enabled = False
                cboTITLE_CON.Enabled = False
                rntxtALLOW_MONEY.ReadOnly = True
                rdEFFECT_DATE_CON.Enabled = False
                rdEXPIRE_DATE_CON.Enabled = False
                txtCON_NO.ReadOnly = True
                rdSIGN_DATE.Enabled = False
                cboStatus.Enabled = False
                btnSIGN.Enabled = False
                btnSIGN2.Enabled = False
                txtRemark.ReadOnly = True
                chkALLOW.Enabled = False
                chkIsChuyen.Enabled = False
                cboUpload.Enabled = False
                btnUploadFile.Enabled = False
            End If
            ChangeToolbarState()
            Me.Send(CurrentState)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    ' Fill dữ liệu working lên control
    Private Sub FillData()
        Dim rep As New ProfileBusinessRepository
        Try
            If IDSelect <> 0 Then
                Dim dr = rep.GET_CONCURRENTLY_BY_ID(IDSelect)
                If dr.Rows.Count > 0 Then
                    EmpOrgNameToolTip.Text = Utilities.DrawTreeByString(dr(0)("Description_Path").ToString)
                    ORG_CONNameToolTip.Text = Utilities.DrawTreeByString(dr(0)("OrgCon_Description_Path").ToString)
                    hidEmpID.Value = dr(0)("EMPLOYEE_ID").ToString
                    txtEmpCode.Text = dr(0)("EMPLOYEE_CODE").ToString
                    txtEmpName.Text = dr(0)("FULLNAME_VN").ToString
                    cboTitleId.SelectedValue = dr(0)("TITLE_id").ToString
                    cboTitleId.Text = dr(0)("TITLE_NAME").ToString
                    txtEmpOrgName.Text = dr(0)("ORG_NAME").ToString
                    hidOrgId.Value = dr(0)("ORG_ID").ToString
                    hidOrgCOn.Value = dr(0)("ORG_CON").ToString
                    txtORG_CONName.Text = dr(0)("ORG_CON_NAME").ToString
                    cboTITLE_CON.SelectedValue = dr(0)("TITLE_CON").ToString
                    cboTITLE_CON.Text = dr(0)("TITLE_CON_NAME").ToString
                    If dr(0)("EFFECT_DATE_CON").ToString <> "" Then
                        rdEFFECT_DATE_CON.SelectedDate = dr(0)("EFFECT_DATE_CON").ToString
                    End If
                    If dr(0)("EXPIRE_DATE_CON").ToString <> "" Then
                        rdEXPIRE_DATE_CON.SelectedDate = dr(0)("EXPIRE_DATE_CON").ToString
                    End If
                    rntxtALLOW_MONEY.Value = dr(0)("ALLOW_MONEY").ToString
                    txtCON_NO.Text = dr(0)("CON_NO").ToString
                    cboStatus.SelectedValue = dr(0)("STATUS").ToString
                    cboStatus.Text = dr(0)("STATUS_NAME").ToString
                    If dr(0)("SIGN_DATE").ToString <> "" Then
                        rdSIGN_DATE.SelectedDate = dr(0)("SIGN_DATE").ToString
                    End If

                    hidSign.Value = dr(0)("SIGN_ID").ToString
                    txtSIGN.Text = dr(0)("SIGN_NAME").ToString
                    txtSIGN_TITLE.Text = dr(0)("SIGN_TITLE_NAME").ToString

                    hidSign2.Value = dr(0)("SIGN_ID_2").ToString
                    txtSIGN2.Text = dr(0)("SIGN_NAME2").ToString
                    txtSIGN_TITLE2.Text = dr(0)("SIGN_TITLE_NAME2").ToString

                    txtRemark.Text = dr(0)("REMARK").ToString
                    txtCON_NO_STOP.Text = dr(0)("CON_NO_STOP").ToString
                    If dr(0)("SIGN_DATE_STOP").ToString <> "" Then
                        rdSIGN_DATE_STOP.SelectedDate = dr(0)("SIGN_DATE_STOP").ToString
                    End If
                    If dr(0)("EFFECT_DATE_STOP").ToString <> "" Then
                        rdEFFECT_DATE_STOP.SelectedDate = dr(0)("EFFECT_DATE_STOP").ToString
                    End If

                    cbSTATUS_STOP.SelectedValue = dr(0)("STATUS_STOP").ToString
                    cbSTATUS_STOP.Text = dr(0)("STATUS_STOP_NAME").ToString
                    hidSignStop.Value = dr(0)("SIGN_ID_STOP").ToString
                    txtSIGN_STOP.Text = dr(0)("SIGN_NAME_STOP").ToString
                    txtSIGN_STOP_TITLE.Text = dr(0)("SIGN_TITLE_NAME_STOP").ToString

                    hidSignStop2.Value = dr(0)("SIGN_ID_STOP_2").ToString
                    txtSIGN_STOP2.Text = dr(0)("SIGN_NAME_STOP2").ToString
                    txtSIGN_STOP_TITLE2.Text = dr(0)("SIGN_TITLE_NAME_STOP2").ToString


                    txtREMARK_STOP.Text = dr(0)("REMARK_STOP").ToString

                    lstFile = dr(0)("FILE_BYTE").ToString
                    LoadListFileUpload(dr(0)("FILE_BYTE").ToString)
                    lstFile1 = dr(0)("FILE_BYTE1").ToString
                    LoadListFileUpload1(dr(0)("FILE_BYTE1").ToString)
                    If dr(0)("IS_ALLOW").ToString <> "0" Then
                        chkALLOW.Checked = True
                    Else
                        chkALLOW.Checked = False
                    End If
                    If dr(0)("IS_CHUYEN").ToString <> "0" Then
                        chkIsChuyen.Checked = True
                    Else
                        chkIsChuyen.Checked = False
                    End If
                End If
                rep.Dispose()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SetStatusControl(ByVal sTrangThai As Boolean)

        SetStatusToolBar()
    End Sub

    Protected Sub SetStatusToolBar()
        Try
            Select Case CurrentState
                Case STATE_NORMAL
                    Me.MainToolBar.Items(0).Enabled = True 'Add
                    Me.MainToolBar.Items(1).Enabled = True 'Edit
                    Me.MainToolBar.Items(7).Enabled = True 'Delete
                    Me.MainToolBar.Items(5).Enabled = True 'Thôi kiêm nhiệm
                    Me.MainToolBar.Items(3).Enabled = False 'Save
                    Me.MainToolBar.Items(4).Enabled = False 'Cancel
                Case STATE_NEW, STATE_EDIT
                    Me.MainToolBar.Items(0).Enabled = False 'Add
                    Me.MainToolBar.Items(1).Enabled = False 'Edit
                    Me.MainToolBar.Items(7).Enabled = False 'Delete
                    Me.MainToolBar.Items(5).Enabled = False 'Thôi kiêm nhiệm
                    Me.MainToolBar.Items(3).Enabled = True 'Save
                    Me.MainToolBar.Items(4).Enabled = True 'Cancel
            End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Private Sub GetParams()
        Dim ID As String = ""
        Try

            If Request.Params("empID") IsNot Nothing Then
                hidEmpID.Value = Request.Params("empID")
            End If
            If Request.Params("IDSelect") IsNot Nothing Then
                IDSelect = Request.Params("IDSelect")
                hidID.Value = Request.Params("IDSelect")
            End If
            If Request.Params("APPROVAL") IsNot Nothing Then
                APPROVAL = Request.Params("APPROVAL")
            End If
            If Request.Params("Check") IsNot Nothing Then
                checkStatus = Request.Params("Check")
            End If
            If Request.Params("LinkEmpId") IsNot Nothing Then
                LinkEmpId = Request.Params("LinkEmpId")
            End If
            If LinkEmpId <> "" Then
                LoadEmployeeLink()
            End If
            If Request.Params("Is_con") IsNot Nothing Then
                Is_con = Request.Params("Is_con")
            End If

            'Select Case CurrentState
            '    Case CommonMessage.STATE_NEW
            '        Refresh("NewView")
            '    Case CommonMessage.STATE_EDIT
            '        Refresh("UpdateView")
            '    Case Else
            '        Refresh("NormalView")
            'End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub DesignGrid(ByVal dt As DataTable)
        Dim rCol As GridBoundColumn
        Dim rColCheck As GridClientSelectColumn
        Dim rColDate As GridDateTimeColumn
        rgConcurrently.MasterTableView.Columns.Clear()
        For Each column As DataColumn In dt.Columns
            If column.ColumnName = "CBSTATUS" Then
                rColCheck = New GridClientSelectColumn()
                rgConcurrently.MasterTableView.Columns.Add(rColCheck)
                rColCheck.HeaderStyle.Width = 30
                rColCheck.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            End If
            If Not column.ColumnName.Contains("ID") And column.ColumnName <> "STATUS" And _
             column.ColumnName <> "CBSTATUS" And Not column.ColumnName.Contains("DATE") Then
                rCol = New GridBoundColumn()
                rgConcurrently.MasterTableView.Columns.Add(rCol)
                rCol.DataField = column.ColumnName
                rCol.HeaderText = Translate(column.ColumnName)
                rCol.HeaderStyle.Width = 150
                rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rCol.AutoPostBackOnFilter = True
                rCol.CurrentFilterFunction = GridKnownFunction.Contains
                rCol.ShowFilterIcon = False
                rCol.FilterControlWidth = 130
            End If
            If column.ColumnName.Contains("DATE") Then
                rColDate = New GridDateTimeColumn()
                rgConcurrently.MasterTableView.Columns.Add(rColDate)
                rColDate.DataField = column.ColumnName
                rColDate.DataFormatString = ConfigurationManager.AppSettings("FDATEGRID")
                rColDate.HeaderText = Translate(column.ColumnName)
                rColDate.HeaderStyle.Width = 150
                rColDate.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                rColDate.AutoPostBackOnFilter = True
                rColDate.CurrentFilterFunction = GridKnownFunction.EqualTo
                rColDate.ShowFilterIcon = False
                rColDate.FilterControlWidth = 130
            End If
        Next
    End Sub

    Private Function Save(ByRef strID As Decimal, Optional ByRef _err As String = "") As Boolean
        Dim rep As New ProfileBusinessRepository
        Dim result As Integer
        Dim CON As New Temp_ConcurrentlyDTO
        log = LogHelper.GetUserLog
        If hidID.Value <> "" Then
            CON.ID = hidID.Value
        End If
        CON.EMPLOYEE_ID = hidEmpID.Value
        If hidOrgCOn.Value <> "" Then
            CON.ORG_CON = hidOrgCOn.Value
        End If
        If cboTITLE_CON.SelectedValue <> "" Then
            CON.TITLE_CON = cboTITLE_CON.SelectedValue
        End If

        If hidOrgId.Value <> "" Then
            CON.ORG_ID = hidOrgId.Value
        End If

        If cboTitleId.SelectedValue <> "" Then
            CON.TITLE_ID = cboTitleId.SelectedValue
        End If

        CON.EFFECT_DATE_CON = rdEFFECT_DATE_CON.SelectedDate
        CON.EXPIRE_DATE_CON = rdEXPIRE_DATE_CON.SelectedDate
        If rntxtALLOW_MONEY.Text <> "" Then
            CON.ALLOW_MONEY = rntxtALLOW_MONEY.Value
        End If
        CON.CON_NO = txtCON_NO.Text
        CON.STATUS = cboStatus.SelectedValue
        CON.SIGN_DATE = rdSIGN_DATE.SelectedDate
        If hidSign.Value <> "" Then
            CON.SIGN_ID = hidSign.Value
        End If
        If hidSign2.Value <> "" Then
            CON.SIGN_ID_2 = hidSign2.Value
        End If
        CON.REMARK = txtRemark.Text
        CON.CON_NO_STOP = txtCON_NO_STOP.Text
        CON.SIGN_DATE_STOP = rdSIGN_DATE_STOP.SelectedDate
        CON.EFFECT_DATE_STOP = rdEFFECT_DATE_STOP.SelectedDate
        CON.STATUS_STOP = cbSTATUS_STOP.SelectedValue
        If hidSignStop.Value <> "" Then
            CON.SIGN_ID_STOP = hidSignStop.Value
        End If
        If hidSignStop2.Value <> "" Then
            CON.SIGN_ID_STOP_2 = hidSignStop2.Value
        End If

        CON.REMARK_STOP = txtREMARK_STOP.Text

        CON.CREATED_BY = log.Username
        CON.CREATED_LOG = log.Ip + "/" + log.ComputerName

        If chkALLOW.Checked = True Then
            CON.IS_ALLOW = 1
        Else
            CON.IS_ALLOW = 0
        End If

        If chkIsChuyen.Checked = True Then
            CON.IS_CHUYEN = 1
        Else
            CON.IS_CHUYEN = 0
        End If

        CON.FILE_BYTE = lstFile
        CON.FILE_BYTE1 = lstFile1

        If IDSelect <> 0 Then
            CON.ID = IDSelect
            result = rep.UPDATE_CONCURRENTLY(CON)
        Else
            result = rep.INSERT_CONCURRENTLY(CON)
        End If

        If cboStatus.SelectedValue = "1" AndAlso chkIsChuyen.Checked = True AndAlso rdEFFECT_DATE_STOP.SelectedDate Is Nothing Then
            rep.INSERT_EMPLOYEE_KN(txtEmpCode.Text, hidOrgCOn.Value, cboTITLE_CON.SelectedValue, rdEFFECT_DATE_CON.SelectedDate, result)
        End If

        If rdEFFECT_DATE_STOP.SelectedDate IsNot Nothing AndAlso cbSTATUS_STOP.SelectedValue = "1" Then
            rep.UPDATE_EMPLOYEE_KN(result, rdEFFECT_DATE_STOP.SelectedDate)
        End If

        rep.Dispose()

        If result = True Then
            hidID.Value = result
            IDSelect = result
            Return True
        Else
            Return False
        End If

    End Function

    'Lấy thông tin nhân viên từ tiện ích bên hồ sơ nhân viên
    Private Sub LoadEmployeeLink()
        ' Dim item = psp.GET_EMPLOYEE_LINK(LinkEmpId)
        'hidEmpID.Value = item.ID
        'txtEmpCode.Text = item.EMPLOYEE_CODE
        'txtEmpName.Text = item.FULLNAME_VN
        'txtEmpOrgName.Text = item.ORG_NAME
        'hidOrgId.Value = item.ORG_ID
        '' Dim dtData As DataTable = psp.GET_TITLE_ORG(hidOrgId.Value)
        'If dtData.Rows.Count > 0 Then
        '    FillRadCombobox(cboTitleId, dtData, "NAME_VN", "ID", True)
        '    cboTitleId.Text = String.Empty
        'End If
        'cboTitleId.SelectedValue = item.TITLE_ID

    End Sub

    Private Sub PrintConcurrently(ByVal print_type As String)
        Dim tempPath As String = ConfigurationManager.AppSettings("WordFileFolder")
        Dim dtData As DataTable
        Dim reportName As String = ""
        Dim reportNameOut As String = ""
        Try
            If rgConcurrently.Items.Count > 0 Then
                If rgConcurrently.SelectedItems.Count = 0 Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    Exit Sub
                End If
                If IDSelect <> 0 Then
                    ' dtData = psp.PRINT_CONCURRENTLY(IDSelect.ToString)
                End If
                If dtData Is Nothing OrElse dtData.Rows.Count <= 0 Then
                    ShowMessage("Không có dữ liệu in quyết định", NotifyType.Warning)
                    Exit Sub
                End If

                If print_type = "TPCKN" Then
                    'reportName = "Decision\" + cboCancelConcurrently.SelectedValue + ".doc"
                    reportNameOut = "Chấm dứt phân công kiêm nhiệm" + ".doc"
                Else
                    'reportName = "Decision\" + cboConcurrentlyType.SelectedValue + ".doc"
                    reportNameOut = "Phân công kiểm nhiệm" + ".doc"
                End If

                If File.Exists(System.IO.Path.Combine(Server.MapPath(tempPath), reportName)) Then
                    ExportWordMailMerge(System.IO.Path.Combine(Server.MapPath(tempPath), reportName),
                           reportNameOut,
                           dtData,
                           Response)
                Else
                    ShowMessage("Mẫu báo cáo không tồn tại", NotifyType.Error)
                End If
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ' Chuyen trang thai control
    Public Sub DisableControls(ByVal control As System.Web.UI.Control, ByVal status As Boolean)
        For Each c As System.Web.UI.Control In control.Controls
            If TypeOf c Is RadTabStrip Then
            Else
                ' Get the Enabled property by reflection.
                Dim type As Type = c.GetType
                Dim prop As PropertyInfo = type.GetProperty("Enabled")

                ' Set it to False to disable the control.
                If Not prop Is Nothing Then
                    prop.SetValue(c, status, Nothing)
                End If
                ' Recurse into child controls.
                If c.Controls.Count > 0 Then
                    Me.DisableControls(c, status)
                End If
            End If

        Next

    End Sub

    Private Sub btnUploadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFile.Click
        ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf"
        ctrlUpload1.Show()
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        If cboUpload.CheckedItems.Count >= 1 Then
            Using zip As New ZipFile
                zip.AlternateEncodingUsage = ZipOption.AsNecessary
                zip.AddDirectoryByName("Files")
                For Each item As RadComboBoxItem In cboUpload.CheckedItems
                    Dim file As System.IO.FileInfo = New System.IO.FileInfo(System.IO.Path.Combine(tempPathFile + "\" + txtEmpCode.Text, item.Text))
                    If file.Exists Then
                        zip.AddFile(file.FullName, "Files")
                    End If
                Next

                Response.Clear()
                Response.BufferOutput = False
                Dim zipName As String = [String].Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"))
                Response.ContentType = "application/zip"
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName)
                zip.Save(Response.OutputStream)
                Response.[End]()

            End Using
        Else
            ShowMessage("File không tồn tại.", NotifyType.Warning)
        End If

    End Sub

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim pathTemp As String
        Dim fileName As String
        Try

            ' Tạo đường dẫn để lưu file được định nghĩa trong webconfig app
            pathTemp = tempPathFile + "\" + txtEmpCode.Text
            txtUploadFile.Text = ""
            If ctrlUpload1.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload1.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(i)
                    ' Nếu thư mục lưu trữ file không tồn tại thì tạo mới
                    If Not Directory.Exists(pathTemp) Then
                        Directory.CreateDirectory(pathTemp)
                    End If
                    ' Lưu file xuống 
                    fileName = System.IO.Path.Combine(pathTemp, file.FileName)
                    file.SaveAs(fileName, True)
                    ' Gán list tên file vào chuỗi sau để lưu vào database
                    If ctrlUpload1.UploadedFiles.Count >= 2 Then
                        If lstFile IsNot Nothing AndAlso lstFile.ToUpper.Contains(file.FileName.ToUpper) Then
                            ShowMessage("File bị trùng, vui lòng kiểm tra lại.", NotifyType.Warning)
                            Exit Sub
                        End If
                        If lstFile <> "" Then
                            lstFile = lstFile + ";" + file.FileName + ";"
                        Else
                            lstFile = file.FileName + ";"
                        End If
                    Else
                        If lstFile IsNot Nothing AndAlso lstFile.ToUpper.Contains(file.FileName.ToUpper) Then
                            ShowMessage("File bị trùng, vui lòng kiểm tra lại.", NotifyType.Warning)
                            Exit Sub
                        End If
                        If lstFile <> "" Then
                            lstFile = lstFile + ";" + file.FileName
                        Else
                            lstFile = file.FileName
                        End If
                    End If
                Next
                LoadListFileUpload(lstFile)
                ShowMessage("Lưu file thành công.", NotifyType.Success)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub LoadListFileUpload(ByVal lstfile As String)
        Dim data As New DataTable
        data.Columns.Add("FileName")
        Dim row As DataRow
        Dim str() As String
        If lstfile <> "" Then
            ClearCboUpload()
            str = lstfile.Split(";")
            For Each s As String In str
                If s <> "" Then
                    row = data.NewRow
                    row("FileName") = s
                    data.Rows.Add(row)
                End If
            Next
            cboUpload.DataSource = data
            cboUpload.DataTextField = "FileName"
            cboUpload.DataValueField = "FileName"
            cboUpload.DataBind()

        Else
            ClearCboUpload()
        End If
    End Sub

    Private Sub ClearCboUpload()
        cboUpload.DataSource = Nothing
        cboUpload.ClearSelection()
        cboUpload.ClearCheckedItems()
        cboUpload.Items.Clear()
    End Sub


    Private Sub btnUploadFile1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFile1.Click
        ctrlUpload2.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf"
        ctrlUpload2.Show()
    End Sub

    Private Sub btnDownload1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload1.Click
        If cboUpload.CheckedItems.Count >= 1 Then
            Using zip As New ZipFile
                zip.AlternateEncodingUsage = ZipOption.AsNecessary
                zip.AddDirectoryByName("Files")
                For Each item As RadComboBoxItem In cboUpload.CheckedItems
                    Dim file As System.IO.FileInfo = New System.IO.FileInfo(System.IO.Path.Combine(tempPathFile + "\" + txtEmpCode.Text, item.Text))
                    If file.Exists Then
                        zip.AddFile(file.FullName, "Files")
                    End If
                Next

                Response.Clear()
                Response.BufferOutput = False
                Dim zipName As String = [String].Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"))
                Response.ContentType = "application/zip"
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName)
                zip.Save(Response.OutputStream)
                Response.[End]()

            End Using
        Else
            ShowMessage("File không tồn tại.", NotifyType.Warning)
        End If

    End Sub

    Private Sub ctrlUpload2_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload2.OkClicked
        Dim pathTemp As String
        Dim fileName As String
        Try
            ' Tạo đường dẫn để lưu file được định nghĩa trong webconfig app
            pathTemp = tempPathFile + "\" + txtEmpCode.Text
            txtUploadFile.Text = ""
            If ctrlUpload2.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload2.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload2.UploadedFiles(i)
                    ' Nếu thư mục lưu trữ file không tồn tại thì tạo mới
                    If Not Directory.Exists(pathTemp) Then
                        Directory.CreateDirectory(pathTemp)
                    End If
                    ' Lưu file xuống 
                    fileName = System.IO.Path.Combine(pathTemp, file.FileName)
                    file.SaveAs(fileName, True)
                    ' Gán list tên file vào chuỗi sau để lưu vào database
                    If ctrlUpload2.UploadedFiles.Count >= 2 Then
                        If lstFile1 IsNot Nothing AndAlso lstFile1.ToUpper.Contains(file.FileName.ToUpper) Then
                            ShowMessage("File bị trùng, vui lòng kiểm tra lại.", NotifyType.Warning)
                            Exit Sub
                        End If
                        If lstFile1 <> "" Then
                            lstFile1 = lstFile1 + ";" + file.FileName + ";"
                        Else
                            lstFile1 = file.FileName + ";"
                        End If
                    Else
                        If lstFile IsNot Nothing AndAlso lstFile.ToUpper.Contains(file.FileName.ToUpper) Then
                            ShowMessage("File bị trùng, vui lòng kiểm tra lại.", NotifyType.Warning)
                            Exit Sub
                        End If
                        If lstFile1 <> "" Then
                            lstFile1 = lstFile1 + ";" + file.FileName
                        Else
                            lstFile1 = file.FileName
                        End If
                    End If
                Next
                LoadListFileUpload1(lstFile1)
                ShowMessage("Lưu file thành công.", NotifyType.Success)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub LoadListFileUpload1(ByVal lstfile As String)
        Dim data As New DataTable
        data.Columns.Add("FileName")
        Dim row As DataRow
        Dim str() As String
        If lstfile <> "" Then
            ClearCboUpload()
            str = lstfile.Split(";")
            For Each s As String In str
                If s <> "" Then
                    row = data.NewRow
                    row("FileName") = s
                    data.Rows.Add(row)
                End If
            Next
            cboUpload1.DataSource = data
            cboUpload1.DataTextField = "FileName"
            cboUpload1.DataValueField = "FileName"
            cboUpload1.DataBind()

        Else
            ClearCboUpload1()
        End If
    End Sub

    Private Sub ClearCboUpload1()
        cboUpload1.DataSource = Nothing
        cboUpload1.ClearSelection()
        cboUpload1.ClearCheckedItems()
        cboUpload1.Items.Clear()
    End Sub

#End Region

End Class
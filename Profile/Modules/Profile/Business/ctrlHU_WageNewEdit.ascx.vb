Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports PayrollDAL
Imports Common
Imports Telerik.Web.UI
Imports System.IO
Imports WebAppLog
Imports HistaffWebAppResources.My.Resources
Public Class ctrlHU_WageNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False

    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
#Region "Property"

    Dim lstAllow As New List(Of WorkingAllowanceDTO)
    Dim _allowDataCache As New List(Of AllowanceListDTO)
    Property Working As WorkingDTO
        Get
            Return ViewState(Me.ID & "_Working")
        End Get
        Set(ByVal value As WorkingDTO)
            ViewState(Me.ID & "_Working") = value
        End Set
    End Property
    'Kieu man hinh tim kiem
    '0 - normal
    '1 - Employee
    '2 - Sign
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
    ''' <summary>
    ''' Khoi tao page, load control, menu toolbar, data grid
    ''' Set trang thai page, control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetParams()
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Khoi tao cac control
    ''' set thuoc tinh grid
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgAllow.AllowSorting = False
            'rgAllowCur.AllowSorting = False
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            If Not IsPostBack Then
                ViewConfig(LeftPane)
                GirdConfig(rgAllow)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Load data cho combobox: Loai to trinh/QD
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim dtData As DataTable = New DataTable()
            Using rep As New ProfileRepository
                dtData = rep.GetSalaryGroupCombo(Date.Now, True)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    FillRadCombobox(cbSalaryGroup, dtData, "NAME", "ID", True)
                End If
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' khoi tao menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarMassTransferSalary
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Using rep As New ProfileRepository
                Dim taxTables = rep.GetOtherList(OtherTypes.TaxTable)
                cboTaxTable.DataSource = taxTables
                cboTaxTable.DataTextField = "Name"
                cboTaxTable.DataValueField = "ID"
            End Using
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Load data theo ID nhan vien neu page o trang thai edit
    ''' Load trang thai page
    ''' </summary>
    ''' <param name="Message">Check trang thai cua page</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Working = rep.GetWorkingByID(Working)
                    hidID.Value = Working.ID.ToString
                    hidEmp.Value = Working.EMPLOYEE_ID
                    txtEmployeeCode.Text = Working.EMPLOYEE_CODE
                    txtEmployeeName.Text = Working.EMPLOYEE_NAME
                    hidTitle.Value = Working.TITLE_ID
                    txtTitleName.Text = Working.TITLE_NAME
                    ' txtTitleGroup.Text = Working.TITLE_GROUP_NAME
                    hidOrg.Value = Working.ORG_ID
                    txtOrgName.Text = Working.ORG_NAME
                    If Working.STATUS_ID IsNot Nothing Then
                        cboStatus.SelectedValue = Working.STATUS_ID
                        cboStatus.Text = Working.STATUS_NAME
                    End If
                    If Working.SAL_TYPE_ID IsNot Nothing Then
                        cboSalTYPE.SelectedValue = Working.SAL_TYPE_ID
                        cboSalTYPE.Text = Working.SAL_TYPE_NAME
                    End If

                    If Working.SALE_COMMISION_ID IsNot Nothing Then
                        cboSaleCommision.SelectedValue = Working.SALE_COMMISION_ID
                        cboSaleCommision.Text = Working.SALE_COMMISION_NAME
                    End If

                    rdEffectDate.SelectedDate = Working.EFFECT_DATE
                    rdExpireDate.SelectedDate = Working.EXPIRE_DATE
                    'If Working.DECISION_TYPE_ID IsNot Nothing Then
                    '    cboDecisionType.SelectedValue = Working.DECISION_TYPE_ID
                    '    cboDecisionType.Text = Working.DECISION_TYPE_NAME
                    'End If
                    txtDecisionNo.Text = Working.DECISION_NO
                    'If Working.SAL_GROUP_ID IsNot Nothing Then
                    '    cboSalGroup.SelectedValue = Working.SAL_GROUP_ID
                    '    cboSalGroup.Text = Working.SAL_GROUP_NAME
                    'End If
                    'If Working.SAL_LEVEL_ID IsNot Nothing Then
                    '    cboSalLevel.SelectedValue = Working.SAL_LEVEL_ID
                    '    cboSalLevel.Text = Working.SAL_LEVEL_NAME
                    'End If
                    'If Working.SAL_RANK_ID IsNot Nothing Then
                    '    cboSalRank.SelectedValue = Working.SAL_RANK_ID
                    '    cboSalRank.Text = Working.SAL_RANK_NAME
                    'End If

                    If Working.STAFF_RANK_ID IsNot Nothing Then
                        hidStaffRank.Value = Working.STAFF_RANK_ID
                        ' txtStaffRank.Text = Working.STAFF_RANK_NAME
                    End If
                    'rntxtSalBasic.Value = Working.SAL_BASIC
                    'rntxtCostSupport.Value = Working.COST_SUPPORT
                    ' rntxtPercentSalary.Value = Working.PERCENT_SALARY
                    '  rntxtSalTotal.Value = 0
                    If Working.SAL_BASIC IsNot Nothing Then
                        '     rntxtSalTotal.Value += Working.SAL_BASIC
                    End If
                    If Working.COST_SUPPORT IsNot Nothing Then
                        '     rntxtSalTotal.Value += Working.COST_SUPPORT
                    End If
                    rdSignDate.SelectedDate = Working.SIGN_DATE
                    If Working.SIGN_ID IsNot Nothing Then
                        hidSign.Value = Working.SIGN_ID
                    End If
                    txtSignName.Text = Working.SIGN_NAME
                    txtSignTitle.Text = Working.SIGN_TITLE
                    txtRemark.Text = Working.REMARK
                    ' rntxtPercentSalary.Value = Working.PERCENT_SALARY
                    lstAllow = Working.lstAllowance
                    rgAllow.Rebind()
                    'Dim total As Decimal = 0
                    'If rntxtCostSupport.Value IsNot Nothing Then
                    '    total = total + rntxtCostSupport.Value
                    'End If
                    'If rntxtSalBasic.Value IsNot Nothing Then
                    '    total = total + rntxtSalBasic.Value
                    'End If
                    'For Each item As GridDataItem In rgAllow.Items
                    '    If item.GetDataKeyValue("AMOUNT") IsNot Nothing Then
                    '        total = total + item.GetDataKeyValue("AMOUNT")
                    '    End If
                    'Next
                    'rntxtTotal.Value = total

                    If Working.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Or
                        Working.STATUS_ID = ProfileCommon.DECISION_STATUS.NOT_APPROVE_ID Then
                        LeftPane.Enabled = False
                        MainToolBar.Items(0).Enabled = False
                    End If

                    basicSalary.Value = Working.SAL_BASIC
                    If Working.TAX_TABLE_ID IsNot Nothing Then
                        cboTaxTable.SelectedValue = Working.TAX_TABLE_ID
                        cboTaxTable.Text = Working.TAX_TABLE_Name
                    End If
                    If Working.SAL_INS IsNot Nothing Then
                        SalaryInsurance.Text = Working.SAL_INS
                    End If
                    If Working.ALLOWANCE_TOTAL IsNot Nothing Then
                        Allowance_Total.Text = Working.ALLOWANCE_TOTAL
                    End If
                    If Working.SAL_TOTAL IsNot Nothing Then
                        Salary_Total.Text = Working.SAL_TOTAL
                    End If



                    'RebindValue()
                Case "NormalView"
                    CurrentState = CommonMessage.STATE_NEW
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"
    Private Sub cbSalaryGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbSalaryGroup.SelectedIndexChanged
        Try
            Dim dtData As DataTable = New DataTable()
            Using rep As New ProfileRepository
                If IsNumeric(cbSalaryGroup.SelectedValue) Then
                    dtData = rep.GetSalaryLevelCombo(cbSalaryGroup.SelectedValue, True)
                End If
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    FillRadCombobox(cbSalaryLevel, dtData, "NAME", "ID", True)
                End If
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cbSalaryLevel_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbSalaryLevel.SelectedIndexChanged
        Try
            Dim dtData As DataTable = New DataTable()
            Using rep As New ProfileRepository
                If IsNumeric(cbSalaryLevel.SelectedValue) Then
                    dtData = rep.GetSalaryRankCombo(cbSalaryLevel.SelectedValue, True)
                End If
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    FillRadCombobox(cbSalaryRank, dtData, "NAME", "ID", True)
                End If
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Event click item cua menu toolbar
    ''' Check validate page khi an luu
    ''' Redirect ve trang Quan ly ho so luong khi an huy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objWorking As New WorkingDTO
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE

                    If Page.IsValid Then
                        'If cboStatus.SelectedValue = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                        '    If txtDecisionNo.Text = "" Then
                        '        ShowMessage(Translate("Bạn phải nhập số tờ trình"), NotifyType.Warning)
                        '        Exit Sub
                        '    End If
                        'End If
                        If cboSalTYPE.SelectedValue = "" Then
                            ShowMessage(Translate("Bạn phải chọn đối tượng lương"), NotifyType.Warning)
                            Exit Sub
                        End If

                        'If cboSalGroup.SelectedValue = "" Then
                        '    ShowMessage(Translate("Bạn phải chọn Thang lương"), NotifyType.Warning)
                        '    Exit Sub
                        'End If
                        'If cboSalLevel.SelectedValue = "" Then
                        '    ShowMessage(Translate("Bạn phải chọn Nhóm lương"), NotifyType.Warning)
                        '    Exit Sub
                        'End If
                        'If cboSalLevel.SelectedValue = "" Then
                        '    ShowMessage(Translate("Bạn phải chọn Bậc lương"), NotifyType.Warning)
                        '    Exit Sub
                        'End If
                        'If cboDecisionType.SelectedValue = "" Then
                        '    ShowMessage(Translate("Bạn phải chọn Loại tờ trình"), NotifyType.Warning)
                        '    cboDecisionType.Focus()
                        '    Exit Sub
                        'End If
                        'If cboStatus.SelectedValue = "" Then
                        '    ShowMessage(Translate("Bạn phải chọn Trạng thái"), NotifyType.Warning)
                        '    cboStatus.Focus()
                        '    Exit Sub
                        'End If
                        If rdEffectDate.SelectedDate Is Nothing Then
                            ShowMessage(Translate("Bạn phải chọn Ngày hiệu lực"), NotifyType.Warning)
                            rdEffectDate.Focus()
                            Exit Sub
                        End If
                        'If rntxtPercentSalary Is Nothing Then
                        '    ShowMessage(Translate("Bạn phải nhập % hưởng lương"), NotifyType.Warning)
                        '    rntxtPercentSalary.Focus()
                        '    Exit Sub
                        'End If
                        'RebindValue()
                        Dim gID As Decimal
                        With objWorking
                            .EMPLOYEE_ID = hidEmp.Value
                            .TITLE_ID = hidTitle.Value
                            .ORG_ID = hidOrg.Value
                            If cboStatus.SelectedValue <> "" Then
                                .STATUS_ID = cboStatus.SelectedValue
                            End If
                            .EFFECT_DATE = rdEffectDate.SelectedDate
                            .EXPIRE_DATE = rdExpireDate.SelectedDate
                            'If cboDecisionType.SelectedValue <> "" Then
                            '    .DECISION_TYPE_ID = cboDecisionType.SelectedValue
                            'End If
                            .DECISION_NO = txtDecisionNo.Text
                            If cboSalTYPE.SelectedValue <> "" Then
                                .SAL_TYPE_ID = cboSalTYPE.SelectedValue
                            End If

                            If cboSaleCommision.SelectedValue <> "" Then
                                .SALE_COMMISION_ID = cboSaleCommision.SelectedValue
                            End If

                            'If cboSalGroup.SelectedValue <> "" Then
                            '    .SAL_GROUP_ID = cboSalGroup.SelectedValue
                            'End If
                            'If cboSalLevel.SelectedValue <> "" Then
                            '    .SAL_LEVEL_ID = cboSalLevel.SelectedValue
                            'End If
                            'If cboSalRank.SelectedValue <> "" Then
                            '    .SAL_RANK_ID = cboSalRank.SelectedValue
                            'End If
                            If hidStaffRank.Value <> "" Then
                                .STAFF_RANK_ID = hidStaffRank.Value
                            End If


                            .SAL_BASIC = basicSalary.Value
                            '.COST_SUPPORT = rntxtCostSupport.Value
                            .SIGN_DATE = rdSignDate.SelectedDate
                            If hidSign.Value <> "" Then
                                .SIGN_ID = hidSign.Value
                            End If
                            .SIGN_NAME = txtSignName.Text
                            .SIGN_TITLE = txtSignTitle.Text
                            .REMARK = txtRemark.Text
                            .IS_PROCESS = True
                            .IS_MISSION = False
                            .IS_WAGE = True
                            .IS_3B = False
                            '.PERCENT_SALARY = rntxtPercentSalary.Value
                            Dim isError As Boolean = False
                            For Each item As GridDataItem In rgAllow.Items
                                Dim allow = New WorkingAllowanceDTO
                                allow.ALLOWANCE_LIST_ID = item.GetDataKeyValue("ALLOWANCE_LIST_ID")
                                allow.ALLOWANCE_LIST_NAME = item.GetDataKeyValue("ALLOWANCE_LIST_NAME")
                                allow.AMOUNT = item.GetDataKeyValue("AMOUNT")
                                allow.IS_INSURRANCE = item.GetDataKeyValue("IS_INSURRANCE")
                                allow.EFFECT_DATE = rdEffectDate.SelectedDate
                                allow.EXPIRE_DATE = item.GetDataKeyValue("EXPIRE_DATE")
                                If allow.EXPIRE_DATE IsNot Nothing Then
                                    If allow.EXPIRE_DATE <= rdEffectDate.SelectedDate Then
                                        isError = True
                                    End If
                                End If
                                lstAllow.Add(allow)
                                .ALLOWANCE_TOTAL = .ALLOWANCE_TOTAL + allow.AMOUNT
                                'If allow.IS_INSURRANCE Then
                                '    .SAL_INS += allow.AMOUNT
                                'End If
                            Next
                            If isError Then
                                ShowMessage("Ngày kết thúc phụ cấp phải lớn hơn Ngày hiệu lực tờ trình", NotifyType.Warning)
                                rgAllow.Rebind()
                                Exit Sub
                            End If
                            .lstAllowance = lstAllow
                            .SAL_TOTAL = Salary_Total.Value
                            .TAX_TABLE_ID = Decimal.Parse(cboTaxTable.SelectedValue)
                            .SAL_INS = SalaryInsurance.Value

                        End With

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If Not ValidateDecisionNo(objWorking) Then
                                    ShowMessage(Translate("Số tờ trình bị trùng"), NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rep.InsertWorking(objWorking, gID) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_WageMng&group=Business")

                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objWorking.ID = Decimal.Parse(hidID.Value)
                                If rep.ModifyWorking(objWorking, gID) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_WageMng&group=Business")

                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If


                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_WageMng&group=Business")
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Event click button search ma nhan vien
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFindCommon_Click(ByVal sender As Object,
                                    ByVal e As EventArgs) Handles _
                                btnFindEmployee.Click,
                                btnFindSign.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case sender.ID
                Case btnFindEmployee.ID
                    isLoadPopup = 1
                Case btnFindSign.ID
                    isLoadPopup = 2
            End Select

            UpdateControlState()
            Select Case sender.ID
                Case btnFindEmployee.ID
                    ctrlFindEmployeePopup.Show()
                Case btnFindSign.ID
                    ctrlFindSigner.MustHaveContract = False
                    ctrlFindSigner.Show()
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Event click huy tren form popup list Nhan vien
    ''' close popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFind_CancelClick(ByVal sender As Object,
                                     ByVal e As System.EventArgs) Handles _
                                 ctrlFindEmployeePopup.CancelClicked,
                                 ctrlFindSigner.CancelClicked
        isLoadPopup = 0
    End Sub
    ''' <summary>
    ''' event click Chon ma nhan vien tu popup list nhan vien
    ''' close popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim empID = ctrlFindEmployeePopup.SelectedEmployeeID(0)
            FillData(empID)
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' event click Chon ma nhan vien tu popup list nhan vien voi man hinh tim kiem signer (gia tri isLoadPopup = 2)
    ''' close popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindSigner_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindSigner.EmployeeSelected
        Dim objEmployee As CommonBusiness.EmployeePopupFindDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            objEmployee = ctrlFindSigner.SelectedEmployee(0)
            txtSignName.Text = objEmployee.FULLNAME_VN
            txtSignTitle.Text = objEmployee.TITLE_NAME
            hidSign.Value = objEmployee.EMPLOYEE_ID
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Load data cho cac combobox khi click vao combobox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cboCommon_ItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs) _
    Handles cboSalTYPE.ItemsRequested,
         cboStatus.ItemsRequested, cboAllowance.ItemsRequested, cboSaleCommision.ItemsRequested
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Using rep As New ProfileRepository
                Dim dtData As DataTable
                Dim sText As String = e.Text
                'Dim dValue As Decimal
                Dim dateValue As Date
                Select Case sender.ID
                    Case cboAllowance.ID
                        dtData = rep.GetHU_AllowanceList()
                        _allowDataCache.Clear()
                        For i As Integer = 0 To dtData.Rows.Count - 1
                            _allowDataCache.Add(New AllowanceListDTO With {.ID = Decimal.Parse(dtData.Rows(i)("ID")),
                                                .IS_INSURANCE = CType(dtData.Rows(i)("IS_INSURANCE"), Boolean)})
                        Next
                    Case cboSalTYPE.ID
                        'If e.Context("valueCustom") Is Nothing Then
                        '    dateValue = Date.Now
                        'Else
                        '    dateValue = Date.Parse(e.Context("valueCustom"))
                        'End If
                        dtData = rep.GetSalaryTypeList(dateValue, True)
                        'Case cboSalGroup.ID
                        '    If e.Context("valueCustom") Is Nothing Then
                        '        dateValue = Date.Now
                        '    Else
                        '        dateValue = Date.Parse(e.Context("valueCustom"))
                        '    End If
                        '    dtData = rep.GetSalaryGroupList(dateValue, True)
                        'Case cboSalLevel.ID
                        '    dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        '    dtData = rep.GetSalaryLevelList(dValue, True)
                        'Case cboSalRank.ID
                        '    dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        '    dtData = rep.GetSalaryRankList(dValue, True)
                    Case cboStatus.ID
                        dtData = rep.GetOtherList(OtherTypes.DecisionStatus)
                    Case cboTaxTable.ID
                        dtData = rep.GetOtherList(OtherTypes.TaxTable)
                    Case cboSaleCommision.ID
                        dtData = rep.GetSaleCommisionList(dateValue, True)
                End Select

                If sText <> "" Then
                    Dim dtExist = (From p In dtData
                                   Where p("NAME") IsNot DBNull.Value AndAlso
                                  p("NAME").ToString.ToUpper = sText.ToUpper)

                    If dtExist.Count = 0 Then
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

                        For i As Integer = itemOffset To endOffset - 1
                            Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                            Select Case sender.ID
                                'Case cboSalRank.ID
                                '    radItem.Attributes("SALARY_BASIC") = dtData.Rows(i)("SALARY_BASIC").ToString()
                            End Select
                            sender.Items.Add(radItem)
                        Next
                    Else

                        Dim itemOffset As Integer = e.NumberOfItems
                        Dim endOffset As Integer = dtData.Rows.Count
                        e.EndOfItems = True

                        For i As Integer = itemOffset To endOffset - 1
                            Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                            Select Case sender.ID
                                'Case cboSalRank.ID
                                '    radItem.Attributes("SALARY_BASIC") = dtData.Rows(i)("SALARY_BASIC").ToString()
                            End Select
                            sender.Items.Add(radItem)
                        Next
                    End If
                Else
                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                    e.EndOfItems = endOffset = dtData.Rows.Count

                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                        Select Case sender.ID
                            'Case cboSalRank.ID
                            '    radItem.Attributes("SALARY_BASIC") = dtData.Rows(i)("SALARY_BASIC").ToString()
                        End Select
                        sender.Items.Add(radItem)
                    Next
                End If
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Event them phu cap xuong grid phu cap, Xoa phu cap tren grid phu cap
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgAllow_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgAllow.ItemCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case e.CommandName
                Case "InsertAllow"
                    If cboAllowance.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải chọn phụ cấp"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rntxtAmount.Value Is Nothing Then
                        ShowMessage(Translate("Bạn phải nhập số tiền"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rdEffectDate.SelectedDate Is Nothing Then
                        ShowMessage(Translate("Bạn phải nhập Ngày hiệu lực hồ sơ lương"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rdAllowExpireDate.SelectedDate IsNot Nothing Then
                        If rdAllowExpireDate.SelectedDate <= rdEffectDate.SelectedDate Then
                            ShowMessage(Translate("Ngày kết thúc phụ cấp phải lớn hơn Ngày hiệu lực tờ trình"), NotifyType.Warning)
                            Exit Sub
                        End If
                    End If
                    Dim lstAllowList As New List(Of Decimal)
                    For Each item As GridDataItem In rgAllow.Items
                        lstAllowList.Add(item.GetDataKeyValue("ALLOWANCE_LIST_ID"))
                    Next
                    If lstAllowList.Contains(cboAllowance.SelectedValue) Then
                        ShowMessage(Translate("Phụ cấp đã tồn tại dưới lưới"), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgAllow.Items
                        Dim allow = New WorkingAllowanceDTO
                        allow.ALLOWANCE_LIST_ID = item.GetDataKeyValue("ALLOWANCE_LIST_ID")
                        allow.ALLOWANCE_LIST_NAME = item.GetDataKeyValue("ALLOWANCE_LIST_NAME")
                        allow.AMOUNT = item.GetDataKeyValue("AMOUNT")
                        allow.IS_INSURRANCE = item.GetDataKeyValue("IS_INSURRANCE")
                        allow.EFFECT_DATE = item.GetDataKeyValue("EFFECT_DATE")
                        allow.EXPIRE_DATE = item.GetDataKeyValue("EXPIRE_DATE")
                        lstAllow.Add(allow)
                    Next
                    Dim allow1 As WorkingAllowanceDTO
                    allow1 = New WorkingAllowanceDTO
                    allow1.ALLOWANCE_LIST_ID = cboAllowance.SelectedValue
                    allow1.ALLOWANCE_LIST_NAME = cboAllowance.Text
                    allow1.AMOUNT = rntxtAmount.Value
                    allow1.IS_INSURRANCE = chkIsInsurrance.Checked
                    If rdAllowEffectDate.SelectedDate.HasValue Then
                        allow1.EFFECT_DATE = rdAllowEffectDate.SelectedDate.Value
                    Else
                        allow1.EFFECT_DATE = rdEffectDate.SelectedDate
                    End If
                    allow1.EXPIRE_DATE = rdAllowExpireDate.SelectedDate
                    lstAllow.Add(allow1)
                    ClearControlValue(cboAllowance, rntxtAmount, chkIsInsurrance, rdAllowExpireDate)
                    rgAllow.Rebind()
                    'Dim total As Decimal = 0
                    'If rntxtCostSupport.Value IsNot Nothing Then
                    '    total = total + rntxtCostSupport.Value
                    'End If
                    'If rntxtSalBasic.Value IsNot Nothing Then
                    '    total = total + rntxtSalBasic.Value
                    'End If
                    'For Each item As GridDataItem In rgAllow.Items
                    '    If item.GetDataKeyValue("AMOUNT") IsNot Nothing Then
                    '        total = total + item.GetDataKeyValue("AMOUNT")
                    '    End If
                    'Next
                    'rntxtTotal.Value = total
                Case "DeleteAllow"
                    For Each item As GridDataItem In rgAllow.Items
                        Dim isExist As Boolean = False
                        For Each selected As GridDataItem In rgAllow.SelectedItems
                            If item.GetDataKeyValue("ALLOWANCE_LIST_ID") = selected.GetDataKeyValue("ALLOWANCE_LIST_ID") Then
                                isExist = True
                                Exit For
                            End If
                        Next

                        If Not isExist Then
                            Dim allow As New WorkingAllowanceDTO
                            allow.ALLOWANCE_LIST_ID = item.GetDataKeyValue("ALLOWANCE_LIST_ID")
                            allow.ALLOWANCE_LIST_NAME = item.GetDataKeyValue("ALLOWANCE_LIST_NAME")
                            allow.AMOUNT = item.GetDataKeyValue("AMOUNT")
                            allow.IS_INSURRANCE = item.GetDataKeyValue("IS_INSURRANCE")
                            allow.EFFECT_DATE = item.GetDataKeyValue("EFFECT_DATE")
                            allow.EXPIRE_DATE = item.GetDataKeyValue("EXPIRE_DATE")
                            lstAllow.Add(allow)
                        End If
                    Next
                    rgAllow.Rebind()
                    'Dim total As Decimal = 0
                    'If rntxtCostSupport.Value IsNot Nothing Then
                    '    total = total + rntxtCostSupport.Value
                    'End If
                    'If rntxtSalBasic.Value IsNot Nothing Then
                    '    total = total + rntxtSalBasic.Value
                    'End If
                    'For Each item As GridDataItem In rgAllow.Items
                    '    If item.GetDataKeyValue("AMOUNT") IsNot Nothing Then
                    '        total = total + item.GetDataKeyValue("AMOUNT")
                    '    End If
                    'Next
                    'rntxtTotal.Value = total


            End Select
            'RebindValue()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' load data len grid phu cap
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgAllow_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgAllow.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataAllowance(lstAllow)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception

            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Validate datetimepicker Ngay Hieu luc phai lon hon ngay hieu luc gan nhat
    ''' rep.ValidateWorking => check ngay hieu luc gan nhat tren database
    ''' Khong cho phep cung 1 quyet dinh trong cung 1 khoang thoi gian nhung lai co 2 gia tri(luong, phucap) khac nhau
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Private Sub cusExistEffectDate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusExistEffectDate.ServerValidate
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If hidEmp.Value = "" Then
    '            args.IsValid = True
    '            Exit Sub
    '        End If
    '        Select Case CurrentState
    '            Case CommonMessage.STATE_NEW
    '                Using rep As New ProfileBusinessRepository
    '                    args.IsValid = rep.ValidateWorking("EXIST_EFFECT_DATE",
    '                                                        New WorkingDTO With {
    '                                                            .EMPLOYEE_ID = hidEmp.Value,
    '                                                            .EFFECT_DATE = rdEffectDate.SelectedDate})
    '                End Using
    '            Case CommonMessage.STATE_EDIT
    '                Using rep As New ProfileBusinessRepository
    '                    args.IsValid = rep.ValidateWorking("EXIST_EFFECT_DATE",
    '                                                        New WorkingDTO With {
    '                                                            .ID = hidID.Value,
    '                                                            .EMPLOYEE_ID = hidEmp.Value,
    '                                                            .EFFECT_DATE = rdEffectDate.SelectedDate})
    '                End Using
    '            Case Else
    '                args.IsValid = True
    '        End Select
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    ''' <summary>
    ''' Chon ngay het hieu luc phai lon hon ngay hieu luc
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rdEffectDate_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdEffectDate.SelectedDateChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            For Each item As GridDataItem In rgAllow.Items
                Dim eff = item.GetDataKeyValue("EFFECT_DATE")
                Dim exp = item.GetDataKeyValue("EXPIRE_DATE")
                If exp IsNot Nothing Then
                    If rdEffectDate.SelectedDate > exp Then
                        item("EXPIRE_DATE").Text = ""
                    End If
                End If
            Next
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Event selected item combobox phu cap
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboAllowance_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboAllowance.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Using rep As New ProfileRepository
                Dim allownace = rep.GetAllowanceList(New AllowanceListDTO With {.ID = cboAllowance.SelectedValue}).FirstOrDefault
                If allownace IsNot Nothing Then
                    chkIsInsurrance.Checked = allownace.IS_INSURANCE
                Else
                    chkIsInsurrance.Checked = False
                End If
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Check validate tong luong phai > luong co ban toi thieu vung
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Private Sub cusPercentSalary_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) _
    '    Handles cusPercentSalary.ServerValidate
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If hidEmp.Value = "" Then
    '            args.IsValid = True
    '            Exit Sub
    '        End If
    '        Dim sumSal As Decimal = 0
    '        If rntxtSalBasic.Value IsNot Nothing Then
    '            sumSal += rntxtSalBasic.Value
    '        End If
    '        If rntxtCostSupport.Value IsNot Nothing Then
    '            sumSal += rntxtCostSupport.Value
    '        End If
    '        If rntxtPercentSalary.Value IsNot Nothing Then
    '            sumSal = sumSal * rntxtPercentSalary.Value / 100
    '        Else
    '            sumSal = 0
    '        End If
    '        Select Case CurrentState
    '            Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
    '                Using rep As New ProfileBusinessRepository
    '                    args.IsValid = rep.ValidateWorking("VALIDATE_INS_REGION",
    '                                                        New WorkingDTO With {
    '                                                            .EMPLOYEE_ID = hidEmp.Value,
    '                                                            .SAL_BASIC = sumSal})
    '                End Using
    '            Case Else
    '                args.IsValid = True
    '        End Select
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

#End Region

#Region "Custom"
    ''' <summary>
    ''' Khoi tao control, Khoi tao popup list Danh sach nhan vien theo 2 loai man hinh
    ''' Set trang thai page, trang thai control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileBusinessRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    SalaryInsurance.Enabled = True
                Case CommonMessage.STATE_EDIT
                    EnableControlAll(False, btnFindEmployee)
                    SalaryInsurance.Enabled = True
            End Select
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmp.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.LoadAllOrganization = False
                        ctrlFindEmployeePopup.MustHaveContract = False
                    End If
                Case 2
                    If Not phFindSign.Controls.Contains(ctrlFindSigner) Then
                        ctrlFindSigner = Me.Register("ctrlFindSigner", "Common", "ctrlFindEmployeePopup")
                        phFindSign.Controls.Add(ctrlFindSigner)
                        ctrlFindSigner.MultiSelect = False
                        ctrlFindSigner.LoadAllOrganization = True
                        ctrlFindSigner.MustHaveContract = False
                    End If
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Get ID Nhan vien tu ctrlHU_WageMng khi o trang thai Edit
    ''' Fill data theo ID Nhan vien len cac control khi o trang thai sua
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    Dim ID As String = Request.Params("ID")
                    If Working Is Nothing Then
                        Working = New WorkingDTO With {.ID = Decimal.Parse(ID)}
                    End If
                    Refresh("UpdateView")
                    Exit Sub
                End If
                If Request.Params("empID") IsNot Nothing Then
                    Dim empID = Request.Params("empID")
                    FillData(empID)
                End If
                Refresh("NormalView")
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Fill data len control theo ID
    ''' </summary>
    ''' <param name="empid">Ma nhan vien</param>
    ''' <remarks></remarks>
    Private Sub FillData(ByVal empid As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Using rep As New ProfileBusinessRepository
                Dim obj = rep.GetEmployeCurrentByID(New WorkingDTO With {.EMPLOYEE_ID = empid})
                If obj.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                    ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                    Exit Sub
                End If
                hidID.Value = obj.ID.ToString
                hidEmp.Value = obj.EMPLOYEE_ID
                txtEmployeeCode.Text = obj.EMPLOYEE_CODE
                txtEmployeeName.Text = obj.EMPLOYEE_NAME
                hidTitle.Value = obj.TITLE_ID
                txtTitleName.Text = obj.TITLE_NAME
                ' txtTitleGroup.Text = obj.TITLE_GROUP_NAME
                hidOrg.Value = obj.ORG_ID
                txtOrgName.Text = obj.ORG_NAME
                'If obj.SAL_GROUP_ID IsNot Nothing Then
                '    cboSalGroup.SelectedValue = obj.SAL_GROUP_ID
                '    cboSalGroup.Text = obj.SAL_GROUP_NAME
                'Else
                '    cboSalGroup.SelectedValue = ""
                'End If
                'If obj.SAL_LEVEL_ID IsNot Nothing Then
                '    cboSalLevel.SelectedValue = obj.SAL_LEVEL_ID
                '    cboSalLevel.Text = obj.SAL_LEVEL_NAME
                'Else
                '    cboSalLevel.SelectedValue = ""
                'End If
                'If obj.SAL_RANK_ID IsNot Nothing Then
                '    cboSalRank.SelectedValue = obj.SAL_RANK_ID
                '    cboSalRank.Text = obj.SAL_RANK_NAME
                'Else
                '    cboSalRank.SelectedValue = ""
                'End If
                If obj.STAFF_RANK_ID IsNot Nothing Then
                    hidStaffRank.Value = obj.STAFF_RANK_ID
                    'txtStaffRank.Text = obj.STAFF_RANK_NAME
                Else
                    hidStaffRank.Value = ""
                    '  txtStaffRank.Text = vbNullString
                End If
                SalaryInsurance.Text = obj.SAL_INS

                'rntxtSalBasic.Value = obj.SAL_BASIC
                'rntxtCostSupport.Value = obj.COST_SUPPORT
                '    rntxtPercentSalary.Value = 100
                ' rntxtSalTotal.Value = 0
                'If rntxtSalBasic.Value IsNot Nothing Then
                '    '   rntxtSalTotal.Value += rntxtSalBasic.Value
                'End If
                'If rntxtCostSupport.Value IsNot Nothing Then
                '    '     rntxtSalTotal.Value += rntxtCostSupport.Value
                'End If
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Load data phu cap len grid phu cap
    ''' </summary>
    ''' <param name="lstAllow"></param>
    ''' <remarks></remarks>
    Private Sub CreateDataAllowance(Optional ByVal lstAllow As List(Of WorkingAllowanceDTO) = Nothing)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If lstAllow Is Nothing Then
                lstAllow = New List(Of WorkingAllowanceDTO)
            End If
            rgAllow.DataSource = lstAllow
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
#End Region


#Region "Utitily"""
    Public Function GetYouMustChoseMsg(ByVal input) As String
        Return String.Format("{0} {1}", Errors.YouMustChose, input)
    End Function
    
    Private Function ConvertNumber(ByVal value As Decimal?) As Decimal
        If value.HasValue Then
            Return value.Value
        End If
        Return 0
    End Function

    Private Function ValidateDecisionNo(ByVal working As WorkingDTO) As Boolean
        Using rep As New ProfileBusinessRepository
            Return rep.ValidateWorking("EXIST_DECISION_NO", working)
        End Using
    End Function

    Protected Sub cboSalTYPE_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboSalTYPE.SelectedIndexChanged
        ' Dim salaryType As New PA_SALARY_TYPEDTO
        Dim taxTables As New List(Of OtherListDTO)
        If String.IsNullOrWhiteSpace(cboSalTYPE.SelectedValue) Then
            Exit Sub
        End If
        Using rep As New ProfileRepository
            taxTables = rep.GetOtherList(OtherTypes.TaxTable).ToList(Of OtherListDTO)
        End Using
        
    End Sub
    Private Sub SetTaxTableByCode(ByVal taxTables As List(Of OtherListDTO), ByVal code As String)
        Dim taxTable = taxTables.FirstOrDefault(Function(f) f.CODE = code)
        cboTaxTable.ClearSelection()
        If taxTable IsNot Nothing Then
            SetValueComboBox(cboTaxTable, taxTable.ID, taxTable.NAME_VN)
        End If
    End Sub
#End Region

End Class
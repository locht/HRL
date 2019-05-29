Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Common.Common
Imports Common.CommonBusiness
Imports Attendance.AttendanceBusiness
Imports Telerik.Web.UI
Imports HistaffFrameworkPublic.HistaffFrameworkEnum
Imports System.IO


Public Class ctrlLeaveRegistrationNewEdit
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"
    Public ReadOnly Property CurrentUser As UserDTO
        Get
            Return LogHelper.CurrentUser
        End Get
    End Property
    Public ReadOnly Property ApproveProcess As String
        Get
            Return ATConstant.GSIGNCODE_LEAVE
        End Get
    End Property

    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
    Protected Property EmployeeDto As DataTable
        Get
            Return PageViewState(Me.ID & "_EmployeeDto")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_EmployeeDto") = value
        End Set
    End Property
    Protected Property ListManual As List(Of AT_TIME_MANUALDTO)
        Get
            Return PageViewState(Me.ID & "_ListFML")
        End Get
        Set(ByVal value As List(Of AT_TIME_MANUALDTO))
            PageViewState(Me.ID & "_ListFML") = value
        End Set
    End Property
    Property ListComboData As Attendance.AttendanceBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Attendance.AttendanceBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
    Property leaveMaster As AT_PORTAL_REG_DTO
        Get
            Return ViewState(Me.ID & "_leaveMaster")
        End Get
        Set(ByVal value As AT_PORTAL_REG_DTO)
            ViewState(Me.ID & "_leaveMaster") = value
        End Set
    End Property
    Property leaveDetails As List(Of AT_PORTAL_REG_DTO)
        Get
            Return ViewState(Me.ID & "_leaveDetails")
        End Get
        Set(ByVal value As List(Of AT_PORTAL_REG_DTO))
            ViewState(Me.ID & "_leaveDetails") = value
        End Set
    End Property
    Property leaveEmpDetails As List(Of LEAVE_DETAIL_EMP_DTO)
        Get
            Return ViewState(Me.ID & "_leaveEmpDetails")
        End Get
        Set(ByVal value As List(Of LEAVE_DETAIL_EMP_DTO))
            ViewState(Me.ID & "_leaveEmpDetails") = value
        End Set
    End Property
    Property leaveInOutKH As DataTable
        Get
            Return ViewState(Me.ID & "_leaveInOutKH")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_leaveInOutKH") = value
        End Set
    End Property
    Property CHECKSHIFT As DataTable
        Get
            Return ViewState(Me.ID & "_CHECKSHIFT")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_CHECKSHIFT") = value
        End Set
    End Property
    Property CHECKCONTRACT As DataTable
        Get
            Return ViewState(Me.ID & "_CHECKCONTRACT")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_CHECKCONTRACT") = value
        End Set
    End Property

    Property dayholiday As List(Of AT_HOLIDAYDTO)
        Get
            Return ViewState(Me.ID & "_dayholiday")
        End Get
        Set(ByVal value As List(Of AT_HOLIDAYDTO))
            ViewState(Me.ID & "_dayholiday") = value
        End Set
    End Property

    Property userType As String
        Get
            Return ViewState(Me.ID & "_userType")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_userType") = value
        End Set
    End Property
    Property check As Integer
        Get
            Return ViewState(Me.ID & "_check")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_check") = value
        End Set
    End Property


#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try

            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub
    Public Overrides Sub BindData()
        Dim rep As New AttendanceRepository
        Try
            If ListComboData Is Nothing Then
                ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO
                ListComboData.GET_LIST_TYPE_MANUAL_LEAVE = True
                rep.GetComboboxData(ListComboData)
            End If
            FillRadCombobox(cboleaveType, ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE, "NAME_VN", "ID", True)
            ListManual = ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE
            If ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE.Count > 0 Then
                cboleaveType.SelectedIndex = 0
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                Dim id As Decimal = 0
                Dim empId As Decimal = 0
                Decimal.TryParse(Request.QueryString("id"), id)
                Decimal.TryParse(Request.QueryString("empId"), empId)
                hidID.Value = id
                hidValid.Value = 0
                Dim state = Request.QueryString("id")
                check = state
                userType = Request.QueryString("typeUser")
                CurrentState = CommonMessage.STATE_NORMAL
                Dim dto As New AT_PORTAL_REG_DTO
                dto.ID = hidID.Value
                If hidID.Value = 0 Then
                    EmployeeID = LogHelper.CurrentUser.EMPLOYEE_ID
                    dto.ID_EMPLOYEE = EmployeeID
                Else
                    EmployeeID = If(empId = 0, LogHelper.CurrentUser.EMPLOYEE_ID, empId)
                    dto.ID_EMPLOYEE = If(empId = 0, LogHelper.CurrentUser.EMPLOYEE_ID, empId)
                End If

                leaveMaster = New AT_PORTAL_REG_DTO
                leaveDetails = New List(Of AT_PORTAL_REG_DTO)
                EmployeeDto = New DataTable




                Using rep As New AttendanceRepository
                    EmployeeDto = rep.GetEmployeeInfor(EmployeeID, Nothing, rdFromDate.SelectedDate)
                    If dto.ID > 0 Then
                        leaveMaster = rep.GetLeaveRegistrationById(dto)
                        If leaveMaster.ID > 0 Then
                            leaveDetails = rep.GetLeaveRegistrationDetailById(leaveMaster.ID)
                        End If
                    End If
                    dayholiday = rep.GetDayHoliday()
                    CHECKSHIFT = rep.PRS_COUNT_SHIFT(EmployeeID)
                    CHECKCONTRACT = rep.CHECK_CONTRACT(EmployeeID)
                End Using
                If EmployeeDto IsNot Nothing AndAlso EmployeeDto.Rows.Count > 0 Then
                    txtFullName.Text = EmployeeDto.Rows(0)("FULLNAME_VN")
                    txtEmpCode.Text = EmployeeDto.Rows(0)("EMPLOYEE_CODE")
                    txtDepartment.Text = EmployeeDto.Rows(0)("ORG_NAME")
                    txtTitle.Text = EmployeeDto.Rows(0)("TITLE_NAME_EN")
                    'rntEntitlement.Value = If(EmployeeDto.Rows(0)("DAY_LEAVE_NUMBER_ADJUST") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("DAY_LEAVE_NUMBER_ADJUST").ToString()))
                    'rntSeniority.Value = If(EmployeeDto.Rows(0)("WORKING_TIME_HAVE_ADJUST") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("WORKING_TIME_HAVE_ADJUST").ToString()))
                    'rntBrought.Value = If(EmployeeDto.Rows(0)("PREV_HAVE") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("PREV_HAVE").ToString()))
                    'rntTotal.Value = If(EmployeeDto.Rows(0)("TOTAL_HAVE") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("TOTAL_HAVE").ToString()))
                    'rntTotalTaken.Value = If(EmployeeDto.Rows(0)("CUR_USED") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("CUR_USED").ToString()))
                    'rntBalance.Value = If(EmployeeDto.Rows(0)("CUR_HAVE") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("CUR_HAVE").ToString()))
                End If
                Dim _filter As New TotalDayOffDTO
                _filter.DATE_REGISTER = Date.Now
                _filter.LEAVE_TYPE = 251
                _filter.EMPLOYEE_ID = EmployeeID
                If check <> 0 Then
                    _filter.ID_PORTAL_REG = check
                Else
                    _filter.ID_PORTAL_REG = 0
                End If
                Dim obj As New TotalDayOffDTO
                Using rep As New AttendanceRepository
                    obj = rep.GetTotalDayOff(_filter)
                    If obj IsNot Nothing Then
                        'PHÉP NGOÀI CÔNG TY
                        If obj.TIME_OUTSIDE_COMPANY IsNot Nothing Then
                            rntTotal.Text = If(obj.TIME_OUTSIDE_COMPANY = 0, 0, Decimal.Parse(obj.TIME_OUTSIDE_COMPANY).ToString())
                        Else
                            rntTotal.Text = Decimal.Parse(0).ToString()
                        End If
                        'phep chế độ
                        If obj.TOTAL_HAVE1 IsNot Nothing Then
                            rntEntitlement.Text = If(obj.TOTAL_HAVE1 = 0, 0, Decimal.Parse(obj.TOTAL_HAVE1).ToString())
                        Else
                            rntEntitlement.Text = Decimal.Parse(0).ToString()
                        End If
                        'phep dã nghĩ
                        If obj.USED_DAY IsNot Nothing Then
                            rntSeniority.Text = If(obj.USED_DAY = 0, 0, Decimal.Parse(obj.USED_DAY).ToString())
                        Else
                            rntSeniority.Text = Decimal.Parse(0).ToString()
                        End If
                        'phep tham nien
                        If obj.SENIORITYHAVE IsNot Nothing Then
                            rntBrought.Text = If(obj.SENIORITYHAVE = 0, 0, Decimal.Parse(obj.SENIORITYHAVE).ToString())
                        Else
                            rntBrought.Text = Decimal.Parse(0).ToString()
                        End If
                        'phep nam truoc con lai
                        If obj.PREVTOTAL_HAVE IsNot Nothing Then
                            rntTotalTaken.Text = If(obj.PREVTOTAL_HAVE = 0, 0, Decimal.Parse(obj.PREVTOTAL_HAVE).ToString())
                        Else
                            rntTotalTaken.Text = Decimal.Parse(0).ToString()
                        End If
                        'phép còn lại
                        If obj.REST_DAY IsNot Nothing Then
                            rntBalance.Text = If(obj.REST_DAY = 0, 0, Decimal.Parse(obj.REST_DAY).ToString())
                        Else
                            rntBalance.Text = Decimal.Parse(0).ToString()
                        End If
                    Else
                        rntSeniority.Text = Decimal.Parse(0).ToString()
                        rntBrought.Text = Decimal.Parse(0).ToString()
                        rntTotalTaken.Text = Decimal.Parse(0).ToString()
                        rntBalance.Text = Decimal.Parse(0).ToString()
                        rntEntitlement.Text = Decimal.Parse(0).ToString()
                        rntTotal.Text = Decimal.Parse(0).ToString()
                    End If
                End Using
                If leaveMaster IsNot Nothing Then
                    hidStatus.Value = If(leaveMaster.STATUS.HasValue, leaveMaster.STATUS, 5)
                    If leaveMaster.ID_SIGN.HasValue Then
                        cboleaveType.SelectedValue = leaveMaster.ID_SIGN
                    End If
                    If leaveMaster.TOTAL_LEAVE.HasValue Then
                        txtDayRegist.Text = leaveMaster.TOTAL_LEAVE
                        rntxDayRegist.Value = leaveMaster.TOTAL_LEAVE
                    End If
                    If leaveMaster.FROM_DATE.HasValue Then
                        rdFromDate.SelectedDate = leaveMaster.FROM_DATE
                    End If
                    If leaveMaster.TO_DATE.HasValue Then
                        rdToDate.SelectedDate = leaveMaster.TO_DATE
                    End If

                    txtNote.Text = leaveMaster.NOTE
                    rgData.Rebind()
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Try
            If IsPostBack Then
                Exit Sub
            End If
            tbarMainToolBar.Items(1).Enabled = True
            tbarMainToolBar.Items(0).Enabled = False

            Select Case hidStatus.Value
                '0 Khai báo thông tin 
                '16 Da luu
                '19 Khong duyet qltt
                '20 Khong xac nhan nhan su
                '22 Khong duyet GM
                Case 5, PortalStatus.Saved, PortalStatus.UnApprovedByLM
                    ', PortalStatus.Saved, PortalStatus.UnApprovedByLM, PortalStatus.UnVerifiedByHr
                    If userType = "User" Then
                        tbarMainToolBar.Items(0).Enabled = True
                        EnableControlAll(True, cboleaveType, rdFromDate, rdToDate, txtNote)
                    Else
                        tbarMainToolBar.Items(0).Enabled = False
                        EnableControlAll(False, cboleaveType, rdFromDate, rdToDate, txtNote)
                    End If
                Case Else
                    EnableControlAll(False, cboleaveType, rdFromDate, rdToDate, txtNote)
            End Select
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
                    ClearControlValue(cboleaveType, rdFromDate, rdToDate, txtNote, hidID)

                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If rntxDayRegist.Value.HasValue AndAlso rntxDayRegist.Value <= 0 Then
                            ShowMessage(Translate("Số ngày nghỉ phải > 0."), NotifyType.Warning)
                            UpdateControlState()
                            Exit Sub
                        End If
                        If cboleaveType.SelectedValue = "" Then
                            ShowMessage(Translate("Chọn loại nghỉ."), NotifyType.Warning)
                            UpdateControlState()
                            Exit Sub
                        End If
                        Dim ktra = (From p In ListManual Where p.ID = cboleaveType.SelectedValue And p.CODE = "P").ToList.Count
                        Dim selectedFromDate1 = rdFromDate.SelectedDate
                        Dim selectedToDate1 = rdToDate.SelectedDate
                        If ktra = 1 Then
                            While selectedFromDate1.Value.Date <= selectedToDate1.Value.Date
                                Dim checkhopdong = (From P In CHECKCONTRACT.AsEnumerable Where P("STARTDATE") <= selectedFromDate1 And P("ENDDATE") >= selectedFromDate1 Select P).ToList.Count
                                If checkhopdong > 0 Then
                                    ShowMessage(Translate("Không được đăng ký phép năm trong thời gian thử việc"), NotifyType.Warning)
                                    UpdateControlState()
                                    Exit Sub
                                End If
                                selectedFromDate1 = selectedFromDate1.Value.AddDays(1)
                            End While
                          
                            If rntxDayRegist.Value > rntBalance.Value Then
                                ShowMessage(Translate("Đã vượt quá số lượng phép còn lại."), NotifyType.Warning)
                                UpdateControlState()
                                Exit Sub
                            End If
                        End If
                        If txtNote.Text = "" Then
                            ShowMessage(Translate("Nhập lý do nghỉ."), NotifyType.Warning)
                            UpdateControlState()
                            Exit Sub
                        End If
                        If rdFromDate.SelectedDate Is Nothing Then
                            ShowMessage(Translate("Chọn thời gian bắt đầu."), NotifyType.Warning)
                            UpdateControlState()
                            Exit Sub
                        End If
                        If rdToDate.SelectedDate Is Nothing Then
                            ShowMessage(Translate("Chọn thời gian kết thúc."), NotifyType.Warning)
                            UpdateControlState()
                            Exit Sub
                        End If
                     

                        Dim selectedFromDate = rdFromDate.SelectedDate
                        Dim selectedToDate = rdToDate.SelectedDate
                        While selectedFromDate.Value.Date <= selectedToDate.Value.Date
                            Dim CHECK1 = (From P In CHECKSHIFT.AsEnumerable Where P("WORKINGDAY1") <= selectedFromDate And P("WORKINGDAY1") >= selectedFromDate Select P).ToList.Count
                            If CHECK1 = 0 Then
                                ShowMessage(Translate("Thời gian bạn chọn không có trong ca làn việc,bạn chọn lại."), NotifyType.Warning)
                                UpdateControlState()
                                Exit Sub
                            End If
                            selectedFromDate = selectedFromDate.Value.AddDays(1)
                        End While
                        If rtxtdayinkh.Value IsNot Nothing AndAlso rtxtdayoutkh.Value IsNot Nothing Then
                            ctrlMessageBox.MessageText = Translate("Số ngày trong kế hoạch là " + rtxtdayinkh.Text + " số ngày ngoài kế hoạch là " + rtxtdayoutkh.Text + ". Bạn có muốn tiếp tục ?")
                            ctrlMessageBox.ActionName = CommonMessage.ACTION_SAVED
                            ctrlMessageBox.DataBind()
                            ctrlMessageBox.Show()
                            Exit Sub
                        End If


                        Dim isInsert As Boolean = True
                        Dim obj As New AT_PORTAL_REG_DTO
                        Dim itemExist = New AT_PORTAL_REG_DTO
                        Dim isOverAnnualLeave As Boolean = False
                        Using rep As New AttendanceRepository
                            If hidID.Value <> 0 Then
                                isInsert = False
                            End If
                            If isInsert Then
                                Dim itemInsert As New AttendanceBusiness.AT_PORTAL_REG_DTO With {
                           .ID_EMPLOYEE = CurrentUser.EMPLOYEE_ID,
                           .ID_SIGN = Decimal.Parse(cboleaveType.SelectedValue),
                           .FROM_DATE = rdFromDate.SelectedDate,
                           .TO_DATE = rdToDate.SelectedDate,
                           .NVALUE = 1,
                           .SVALUE = ApproveProcess,
                           .NOTE = txtNote.Text,
                           .NOTE_AT = txtNote.Text,
                           .STATUS = 3,
                           .DAYIN_KH = If(rtxtdayinkh.Text = "", Nothing, rtxtdayinkh.Text),
                           .DAYOUT_KH = If(rtxtdayoutkh.Text = "", Nothing, rtxtdayoutkh.Text),
                           .MODIFIED_BY = EmployeeID,
                                .PROCESS = ApproveProcess
                       }
                                AttendanceRepositoryStatic.Instance.InsertPortalRegister(itemInsert)
                                obj.ID = hidID.Value
                            Else
                                Dim itemInsert As New AttendanceBusiness.AT_PORTAL_REG_DTO With {
                           .ID_EMPLOYEE = CurrentUser.EMPLOYEE_ID,
                           .ID_SIGN = Decimal.Parse(cboleaveType.SelectedValue),
                           .FROM_DATE = rdFromDate.SelectedDate,
                           .TO_DATE = rdToDate.SelectedDate,
                           .NVALUE = 1,
                           .SVALUE = ApproveProcess,
                           .NOTE = txtNote.Text,
                           .NOTE_AT = txtNote.Text,
                           .STATUS = 3,
                         .DAYIN_KH = rtxtdayinkh.Text,
                           .DAYOUT_KH = rtxtdayoutkh.Text,
                            .MODIFIED_BY = EmployeeID,
                                .PROCESS = ApproveProcess
                       }
                                obj.ID = hidID.Value
                                rep.ModifyPortalRegList(obj, itemInsert)
                            End If

                            If isOverAnnualLeave Then
                                ShowMessage(Translate("Không được đăng ký nghỉ vượt quá số ngày phép con lại trong năm"), NotifyType.Warning)
                                Return
                            End If

                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlLeaveRegistration")
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                        End Using
                    End If
                Case CommonMessage.TOOLBARITEM_SUBMIT
                    ctrlMessageBox.MessageText = Translate("Thông tin đã gửi duyệt sẽ không được chỉnh sửa. Bạn chắc chắn muốn gửi duyệt?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    If userType = "User" Then
                        Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlLeaveRegistration")
                    ElseIf userType = "LM" Then
                        Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlLeaveRegistrationByManager")

                    ElseIf userType = "HR" Then
                        Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlLeaveRegistrationByHR")
                    End If


            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.ACTION_SAVED And e.ButtonID = MessageBoxButtonType.ButtonYes Then

                Dim isInsert As Boolean = True
                Dim obj As New AT_PORTAL_REG_DTO
                Dim itemExist = New AT_PORTAL_REG_DTO
                Dim isOverAnnualLeave As Boolean = False
                Using rep As New AttendanceRepository
                    If hidID.Value <> 0 Then
                        isInsert = False
                    End If
                    If isInsert Then
                        Dim itemInsert As New AttendanceBusiness.AT_PORTAL_REG_DTO With {
                   .ID_EMPLOYEE = CurrentUser.EMPLOYEE_ID,
                   .ID_SIGN = Decimal.Parse(cboleaveType.SelectedValue),
                   .FROM_DATE = rdFromDate.SelectedDate,
                   .TO_DATE = rdToDate.SelectedDate,
                   .NVALUE = 1,
                   .SVALUE = ApproveProcess,
                   .NOTE = txtNote.Text,
                   .NOTE_AT = txtNote.Text,
                   .STATUS = 3,
                   .DAYIN_KH = If(rtxtdayinkh.Text = "", Nothing, rtxtdayinkh.Text),
                   .DAYOUT_KH = If(rtxtdayoutkh.Text = "", Nothing, rtxtdayoutkh.Text),
                   .MODIFIED_BY = EmployeeID,
                        .PROCESS = ApproveProcess
               }
                        AttendanceRepositoryStatic.Instance.InsertPortalRegister(itemInsert)
                        obj.ID = hidID.Value
                    Else
                        Dim itemInsert As New AttendanceBusiness.AT_PORTAL_REG_DTO With {
                   .ID_EMPLOYEE = CurrentUser.EMPLOYEE_ID,
                   .ID_SIGN = Decimal.Parse(cboleaveType.SelectedValue),
                   .FROM_DATE = rdFromDate.SelectedDate,
                   .TO_DATE = rdToDate.SelectedDate,
                   .NVALUE = 1,
                   .SVALUE = ApproveProcess,
                   .NOTE = txtNote.Text,
                   .NOTE_AT = txtNote.Text,
                   .STATUS = 3,
                 .DAYIN_KH = rtxtdayinkh.Text,
                   .DAYOUT_KH = rtxtdayoutkh.Text,
                    .MODIFIED_BY = EmployeeID,
                        .PROCESS = ApproveProcess
               }
                        obj.ID = hidID.Value
                        rep.ModifyPortalRegList(obj, itemInsert)
                    End If

                    If isOverAnnualLeave Then
                        ShowMessage(Translate("Không được đăng ký nghỉ vượt quá số ngày phép con lại trong năm"), NotifyType.Warning)
                        Return
                    End If

                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlLeaveRegistration")
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                End Using
            End If


        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    'Protected Sub btnDetail_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDetail.Click
    '    Try
    '        'Hien thi thong tin grid
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
    Protected Sub rdFromDate_Select(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdFromDate.SelectedDateChanged
        Try
            leaveMaster = New AT_PORTAL_REG_DTO
            Using rep As New AttendanceRepository
                EmployeeDto = rep.GetEmployeeInfor(EmployeeID, Nothing, rdFromDate.SelectedDate)
                If EmployeeDto IsNot Nothing AndAlso EmployeeDto.Rows.Count > 0 Then
                    txtFullName.Text = EmployeeDto.Rows(0)("FULLNAME_VN")
                    txtEmpCode.Text = EmployeeDto.Rows(0)("EMPLOYEE_CODE")
                    txtDepartment.Text = EmployeeDto.Rows(0)("ORG_NAME")
                    txtTitle.Text = EmployeeDto.Rows(0)("TITLE_NAME_EN")
                    'rntEntitlement.Value = If(EmployeeDto.Rows(0)("DAY_LEAVE_NUMBER_ADJUST") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("DAY_LEAVE_NUMBER_ADJUST").ToString()))
                    'rntSeniority.Value = If(EmployeeDto.Rows(0)("WORKING_TIME_HAVE_ADJUST") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("WORKING_TIME_HAVE_ADJUST").ToString()))
                    'rntBrought.Value = If(EmployeeDto.Rows(0)("PREV_HAVE") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("PREV_HAVE").ToString()))
                    'rntTotal.Value = If(EmployeeDto.Rows(0)("TOTAL_HAVE") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("TOTAL_HAVE").ToString()))
                    'rntTotalTaken.Value = If(EmployeeDto.Rows(0)("CUR_USED") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("CUR_USED").ToString()))
                    'rntBalance.Value = If(EmployeeDto.Rows(0)("CUR_HAVE") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("CUR_HAVE").ToString()))
                End If
            End Using
            Dim _filter As New TotalDayOffDTO
            _filter.DATE_REGISTER = rdFromDate.SelectedDate
            _filter.LEAVE_TYPE = 251
            _filter.EMPLOYEE_ID = EmployeeID
            If check <> 0 Then
                _filter.ID_PORTAL_REG = check
            Else
                _filter.ID_PORTAL_REG = 0
            End If

            Dim obj As New TotalDayOffDTO
            Using rep As New AttendanceRepository
                obj = rep.GetTotalDayOff(_filter)
                If obj IsNot Nothing Then
                    'PHÉP NGOÀI CÔNG TY
                    If obj.TIME_OUTSIDE_COMPANY IsNot Nothing Then
                        rntTotal.Text = If(obj.TIME_OUTSIDE_COMPANY = 0, 0, Decimal.Parse(obj.TIME_OUTSIDE_COMPANY).ToString())
                    Else
                        rntTotal.Text = Decimal.Parse(0).ToString()
                    End If
                    'phep chế độ
                    If obj.TOTAL_HAVE1 IsNot Nothing Then
                        rntEntitlement.Text = If(obj.TOTAL_HAVE1 = 0, 0, Decimal.Parse(obj.TOTAL_HAVE1).ToString())
                    Else
                        rntEntitlement.Text = Decimal.Parse(0).ToString()
                    End If
                    'phep dã nghĩ
                    If obj.USED_DAY IsNot Nothing Then
                        rntSeniority.Text = If(obj.USED_DAY = 0, 0, Decimal.Parse(obj.USED_DAY).ToString())
                    Else
                        rntSeniority.Text = Decimal.Parse(0).ToString()
                    End If
                    'phep tham nien
                    If obj.SENIORITYHAVE IsNot Nothing Then
                        rntBrought.Text = If(obj.SENIORITYHAVE = 0, 0, Decimal.Parse(obj.SENIORITYHAVE).ToString())
                    Else
                        rntBrought.Text = Decimal.Parse(0).ToString()
                    End If
                    'phep nam truoc con lai
                    If obj.PREVTOTAL_HAVE IsNot Nothing Then
                        rntTotalTaken.Text = If(obj.PREVTOTAL_HAVE = 0, 0, Decimal.Parse(obj.PREVTOTAL_HAVE).ToString())
                    Else
                        rntTotalTaken.Text = Decimal.Parse(0).ToString()
                    End If
                    'phép còn lại
                    If obj.REST_DAY IsNot Nothing Then
                        rntBalance.Text = If(obj.REST_DAY = 0, 0, Decimal.Parse(obj.REST_DAY).ToString())
                    Else
                        rntBalance.Text = Decimal.Parse(0).ToString()
                    End If
                Else
                    rntSeniority.Text = Decimal.Parse(0).ToString()
                    rntBrought.Text = Decimal.Parse(0).ToString()
                    rntTotalTaken.Text = Decimal.Parse(0).ToString()
                    rntBalance.Text = Decimal.Parse(0).ToString()
                    rntEntitlement.Text = Decimal.Parse(0).ToString()
                    rntTotal.Text = Decimal.Parse(0).ToString()
                End If
            End Using

            If rdFromDate.SelectedDate IsNot Nothing And rdToDate.SelectedDate IsNot Nothing Then
                rgData.Rebind()
                For Each item As GridDataItem In rgData.MasterTableView.Items
                    item.Edit = True
                Next
                rgData.MasterTableView.Rebind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub rdToDate_Select(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdToDate.SelectedDateChanged
        Try
            If rdFromDate.SelectedDate IsNot Nothing And rdToDate.SelectedDate IsNot Nothing Then
                rgData.Rebind()
                For Each item As GridDataItem In rgData.MasterTableView.Items
                    item.Edit = True
                Next
                rgData.MasterTableView.Rebind()
            End If
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

    Protected Sub rgData_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemCreated
        If TypeOf e.Item Is GridDataItem Then
            Dim control As GridDataItem = CType(e.Item, GridDataItem)
            Dim combo As RadComboBox = CType(control.FindControl("cboLeaveValue"), RadComboBox) ' CType(insertItem("cboLeaveValue").FindControl("radComboBoxOccCode"), RadComboBox)

            If combo IsNot Nothing Then

                combo.AutoPostBack = True
                AddHandler combo.SelectedIndexChanged, AddressOf combo_SelectedIndexChanged
                combo.Enabled = False
            End If
        End If
        If Not String.IsNullOrEmpty(hidID.Value) AndAlso hidID.Value > 0 Then
            Dim cmdItem As GridItem = rgData.MasterTableView.GetItems(GridItemType.CommandItem)(0)
            Dim editDetail As RadButton = CType(cmdItem.FindControl("btnEditDetail"), RadButton)
            editDetail.Enabled = False
        End If
    End Sub

    Protected Sub combo_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            'For Each item As GridDataItem In rgData.EditItems
            '    Dim edit = CType(item, GridEditableItem)
            '    Dim effect As Date = item.GetDataKeyValue("EFFECTIVEDATE")
            '    Dim detail = leaveEmpDetails.First(Function(f) f.EFFECTIVEDATE = effect)
            '    Dim index = leaveEmpDetails.IndexOf(detail)
            '    Dim cbo As RadComboBox = CType(edit.FindControl("cboLeaveValue"), RadComboBox)
            '    If cbo IsNot Nothing Then
            '        If cbo.SelectedValue = "0.5" Then
            '            detail.LEAVE_VALUE = 0.5
            '        ElseIf cbo.SelectedValue = "1" Then
            '            detail.LEAVE_VALUE = 1
            '        Else
            '            detail.LEAVE_VALUE = 0
            '        End If
            '        leaveEmpDetails(index) = detail
            '    End If
            'Next
            'txtDayRegist.Text = leaveEmpDetails.Select(Function(f) f.LEAVE_VALUE).Sum
            'rntxDayRegist.Value = leaveEmpDetails.Select(Function(f) f.LEAVE_VALUE).Sum
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", "showDetail('block');", True)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub rgData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        If e.Item.Edit Then
            Dim edit = CType(e.Item, GridEditableItem)
            Dim cbo As RadComboBox
            cbo = CType(edit.FindControl("cboLeaveValue"), RadComboBox)

            If cbo IsNot Nothing Then
                FillRadCombobox(cbo, ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE, "NAME_VN", "ID", True)
                If cboleaveType.SelectedValue <> "" Then
                    cbo.SelectedValue = cboleaveType.SelectedValue
                End If
                cbo.Enabled = False
                cbo.Width = Unit.Percentage(100)
            End If
        End If
    End Sub


    Private Sub rgData_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgData.ItemCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case e.CommandName
                Case "EditDetail"
                    'rdToDate_Select(sender, Nothing)
                    'rgData.Rebind()
                    For Each item As GridDataItem In rgData.MasterTableView.Items
                        item.Edit = True
                    Next
                    rgData.MasterTableView.Rebind()
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", "showDetail('');", True)

            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"
    Private Function Valid() As Boolean
        Dim value As Double
        'value = IIf(String.IsNullOrEmpty(txtLeaveReUse.Text), 0, Double.Parse(txtLeaveReUse.Text)
    End Function
    Protected Function CreateDataFilter(Optional ByVal fromDate As Date? = Nothing, Optional ByVal toDate As Date? = Nothing, Optional ByVal isFull As Boolean = False) As DataTable
        Try
            Dim calDay As Integer = 0
            Dim ktra As Integer
            Dim calDay1 As Integer = 0
            Using rep As New AttendanceRepository
                If fromDate IsNot Nothing Or toDate IsNot Nothing Then
                    leaveEmpDetails = rep.GetLeaveEmpDetail(EmployeeID, fromDate.Value, toDate.Value, If(IsNumeric(hidID.Value), hidID.Value, 0) <> 0)
                ElseIf rdFromDate.SelectedDate IsNot Nothing And rdToDate.SelectedDate IsNot Nothing Then
                    leaveEmpDetails = rep.GetLeaveEmpDetail(EmployeeID, rdFromDate.SelectedDate, rdToDate.SelectedDate, If(IsNumeric(hidID.Value), hidID.Value, 0) <> 0)
                    'Truog hop k co phan ca thi generate ca default
                    If check = 0 Then
                        If leaveEmpDetails.Count <> 0 Then
                            ShowMessage(Translate("Ngày nghỉ đã được đăng ký."), NotifyType.Warning)
                            Exit Function
                        Else
                            If leaveEmpDetails IsNot Nothing AndAlso leaveEmpDetails.Count = 0 Then
                                leaveEmpDetails = New List(Of LEAVE_DETAIL_EMP_DTO)
                                'Initial default list 
                                Dim selectedFromDate = rdFromDate.SelectedDate
                                Dim selectedToDate = rdToDate.SelectedDate
                                While selectedFromDate.Value.Date <= selectedToDate.Value.Date
                                    Dim leaveEmpDetail = New LEAVE_DETAIL_EMP_DTO
                                    leaveEmpDetail.EFFECTIVEDATE = selectedFromDate
                                    leaveEmpDetail.LEAVE_VALUE = 1
                                    leaveEmpDetail.IS_UPDATE = 0
                                    leaveEmpDetails.Add(leaveEmpDetail)
                                    selectedFromDate = selectedFromDate.Value.AddDays(1)
                                End While
                            End If
                        End If

                    End If
                    'trường hợp sửa
                Else
                    leaveEmpDetails = New List(Of LEAVE_DETAIL_EMP_DTO)
                End If
                If leaveDetails IsNot Nothing Then
                    For Each item In leaveDetails
                        If leaveEmpDetails IsNot Nothing AndAlso leaveEmpDetails.Count > 0 Then
                            Dim detail = leaveEmpDetails.FirstOrDefault(Function(f) f.FROM_DATE = item.FROM_DATE)
                            If detail IsNot Nothing Then
                                Dim index = leaveEmpDetails.IndexOf(detail)
                                detail.LEAVE_NAME = item.NVALUE_NAME
                                detail.EFFECTIVEDATE = item.FROM_DATE
                                leaveEmpDetails(index) = detail
                            End If
                        End If
                    Next
                End If
            End Using
            If rdFromDate.SelectedDate IsNot Nothing And rdToDate.SelectedDate IsNot Nothing Then
                Dim selectedFromDate1 = rdFromDate.SelectedDate
                Dim selectedToDate1 = rdToDate.SelectedDate

                While selectedFromDate1.Value.Date <= selectedToDate1.Value.Date
                    calDay += 1
                    Using rep As New AttendanceRepository
                        leaveInOutKH = rep.PRS_COUNT_INOUTKH(EmployeeID, Year(rdToDate.SelectedDate))
                        Dim COUNT = (From P In leaveInOutKH.AsEnumerable Where P("START_DATE") <= selectedFromDate1 And P("END_DATE") >= selectedFromDate1 Select P).ToList.Count
                        If cboleaveType.SelectedValue = "" Then
                            ShowMessage(Translate("Chọn loại nghỉ"), NotifyType.Warning)
                            Exit Function
                        End If
                        ktra = (From p In ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE Where p.ID = cboleaveType.SelectedValue And p.CODE = "P").ToList.Count
                        If ktra = 1 Then
                            If COUNT > 0 Then
                                calDay1 += 1
                            End If
                        End If
                        Dim check = (From p In dayholiday Where p.WORKINGDAY <= selectedFromDate1 And p.WORKINGDAY >= selectedFromDate1 Select p).ToList.Count
                        If check > 0 Then
                            calDay -= 1
                        End If
                    End Using
                    selectedFromDate1 = selectedFromDate1.Value.AddDays(1)
                End While
            End If

            If leaveEmpDetails IsNot Nothing Then
                txtDayRegist.Text = 0
                rntxDayRegist.Value = 0
                If leaveEmpDetails IsNot Nothing Then
                    txtDayRegist.Text = calDay.ToString
                    rntxDayRegist.Value = Decimal.Parse(calDay)
                    If ktra = 1 Then
                        rtxtdayinkh.Value = Decimal.Parse(calDay1)
                        rtxtdayoutkh.Value = Decimal.Parse(calDay) - Decimal.Parse(calDay1)
                    Else
                        rtxtdayinkh.Text = Nothing
                        rtxtdayoutkh.Text = Nothing
                    End If
                End If
                rgData.DataSource = leaveEmpDetails.OrderBy(Function(f) f.EFFECTIVEDATE)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function LoadComboLeaveValue() As DataTable
        Try
            Dim table As New DataTable
            table.Columns.Add("VALUE", GetType(String))
            table.Columns.Add("ID", GetType(String))
            Dim row As DataRow
            row = table.NewRow
            row("ID") = "0"
            row("VALUE") = ""
            table.Rows.Add(row)

            row = table.NewRow
            row("ID") = "0.5"
            row("VALUE") = "0.5"
            table.Rows.Add(row)

            row = table.NewRow
            row("ID") = "1"
            row("VALUE") = "1"
            table.Rows.Add(row)
            Return table
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
#End Region

    Private Sub cboleaveType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboleaveType.SelectedIndexChanged
        Try
            CreateDataFilter()
        Catch ex As Exception

        End Try
    End Sub


End Class
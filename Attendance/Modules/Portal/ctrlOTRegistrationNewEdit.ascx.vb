Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports Telerik.Web.UI
Imports Attendance.AttendanceBusiness
Imports Common.CommonBusiness
Imports System.Globalization
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports System.IO

Public Class ctrlOTRegistrationNewEdit
    Inherits CommonView

    Public Overrides Property MustAuthorize As Boolean = False
    Dim log As New UserLog
    Dim psp As New AttendanceStoreProcedure
    Dim com As New CommonProcedureNew
    Dim cons As New Contant_OtherList_Attendance
    Dim cons_com As New Contant_Common
    Private rep As New HistaffFrameworkRepository

#Region "Property"
    Property OtRegistration As AT_PORTAL_REG_DTO
        Get
            Return ViewState(Me.ID & "_OTRegistration")
        End Get
        Set(ByVal value As AT_PORTAL_REG_DTO)
            ViewState(Me.ID & "_OTRegistration") = value
        End Set
    End Property
    Protected Property EmployeeDto As DataTable
        Get
            Return PageViewState(Me.ID & "_EmployeeDto")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_EmployeeDto") = value
        End Set
    End Property
    Private Property dtTable As DataTable
        Get
            Return ViewState(Me.ID & "_dtTable")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtTable") = value
        End Set
    End Property
    Private Property EmployeeId As Decimal
        Get
            Return PageViewState(Me.ID & "_EmployeeId")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_EmployeeId") = value
        End Set
    End Property
    Private Property perioId As Integer
        Get
            Return ViewState(Me.ID & "_perioId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_perioId") = value
        End Set
    End Property

    Private Property orgId As Integer
        Get
            Return ViewState(Me.ID & "_orgId")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_orgId") = value
        End Set


    End Property
    Private Property TotalOT As Decimal
        Get
            Return ViewState(Me.ID & "_TotalOT")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_TotalOT") = value
        End Set


    End Property
    Private Property ListComboValue As Attendance.AttendanceBusiness.ComboBoxDataDTO
        Get
            Return PageViewState(Me.ID & "_ComboBoxDataDTO")
        End Get
        Set(ByVal value As Attendance.AttendanceBusiness.ComboBoxDataDTO)
            PageViewState(Me.ID & "_ComboBoxDataDTO") = value
        End Set
    End Property

    Private Property SelectedAppointmentList As List(Of AT_TIMESHEET_REGISTERDTO)
        Get
            Return PageViewState(Me.ID & "_SelectedAppointmentList")
        End Get
        Set(ByVal value As List(Of AT_TIMESHEET_REGISTERDTO))
            PageViewState(Me.ID & "_SelectedAppointmentList") = value
        End Set
    End Property

    Private Property SelectedAppointmentListId As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_SelectedAppointmentListId")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_SelectedAppointmentListId") = value
        End Set
    End Property

    Public ReadOnly Property ApproveProcess As String
        Get
            Return ATConstant.GSIGNCODE_OVERTIME
        End Get
    End Property

    Public ReadOnly Property PageId As String
        Get
            Return Me.ID
        End Get
    End Property

    Public ReadOnly Property CurrentUser As UserDTO
        Get
            Return LogHelper.CurrentUser
        End Get
    End Property

    Private Property AppointmentList As List(Of APPOINTMENT_DTO)
        Get
            Return PageViewState(Me.ID & "_AppointmentList")
        End Get
        Set(ByVal value As List(Of APPOINTMENT_DTO))
            PageViewState(Me.ID & "_AppointmentList") = value
        End Set
    End Property

    Private Property lstHolidays As List(Of Date)
        Get
            Return ViewState(Me.ID & "_lstHolidays")
        End Get
        Set(ByVal value As List(Of Date))
            ViewState(Me.ID & "_lstHolidays") = value
        End Set
    End Property
    Protected Property ListManual As List(Of OT_OTHERLIST_DTO)
        Get
            Return PageViewState(Me.ID & "_ListManual")
        End Get
        Set(ByVal value As List(Of OT_OTHERLIST_DTO))
            PageViewState(Me.ID & "_ListManual") = value
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
    Property userType As String
        Get
            Return ViewState(Me.ID & "_userType")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_userType") = value
        End Set
    End Property
    Property ID_REG_GROUP As Decimal?
        Get
            Return ViewState(Me.ID & "_ID_REG_GROUP")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_ID_REG_GROUP") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
            CreateDataTableUpdate()
            perioId = AttendanceRepositoryStatic.Instance.GET_PERIOD(FirstDayOfMonth(Date.Now))
            orgId = AttendanceRepositoryStatic.Instance.GET_ORGID(EmployeeId)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
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

    Public Overrides Sub BindData()
        Dim atRepo As New AttendanceRepository
        Dim dtData As DataTable
        Try
            If Not IsPostBack Then
                dtData = AttendanceRepositoryStatic.Instance.GET_LIST_HOURS()
                If dtData.Rows.Count > 0 Then
                    FillRadCombobox(cboHoursFrom, dtData, "NAME", "ID", False)
                    FillRadCombobox(cboHoursTo, dtData, "NAME", "ID", False)
                End If
                dtData = AttendanceRepositoryStatic.Instance.GET_LIST_MINUTE()
                If dtData.Rows.Count > 0 Then
                    FillRadCombobox(cboMinuteFrom, dtData, "NAME", "ID", False)
                    FillRadCombobox(cboMinuteTo, dtData, "NAME", "ID", False)
                End If
                EmployeeDto = atRepo.GetEmployeeInfor(EmployeeId, Nothing)
                If EmployeeDto IsNot Nothing AndAlso EmployeeDto.Rows.Count > 0 Then
                    txtFullName.Text = EmployeeDto.Rows(0)("FULLNAME_VN")
                    txtEmpCode.Text = EmployeeDto.Rows(0)("EMPLOYEE_CODE")
                    txtDepartment.Text = EmployeeDto.Rows(0)("ORG_NAME")
                    txtTitle.Text = EmployeeDto.Rows(0)("TITLE_NAME_VN")
                End If

            End If
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
                userType = Request.QueryString("typeUser")
                hidID.Value = id
                hidValid.Value = 0
                Dim dto As New AT_PORTAL_REG_DTO
                dto.ID = hidID.Value
                If hidID.Value = 0 Then
                    EmployeeId = LogHelper.CurrentUser.EMPLOYEE_ID
                    dto.ID_EMPLOYEE = EmployeeId
                Else
                    EmployeeId = empId
                    dto.ID_EMPLOYEE = empId
                End If
                Using rep As New AttendanceRepository

                    If dto.ID > 0 Then
                        Dim data = rep.GetOtRegistration(dto)
                        If data IsNot Nothing Then
                            OtRegistration = data.FirstOrDefault
                            'EmployeeShift = rep.GetEmployeeShifts(EmployeeID, OtRegistration.REGIST_DATE, OtRegistration.REGIST_DATE).FirstOrDefault
                        End If
                    End If
                    EmployeeDto = rep.GetEmployeeInfor(EmployeeId, Nothing)
                    If EmployeeDto IsNot Nothing AndAlso EmployeeDto.Rows.Count > 0 Then
                        txtFullName.Text = EmployeeDto.Rows(0)("FULLNAME_VN")
                        txtEmpCode.Text = EmployeeDto.Rows(0)("EMPLOYEE_CODE")
                        txtDepartment.Text = EmployeeDto.Rows(0)("ORG_NAME")
                        txtTitle.Text = EmployeeDto.Rows(0)("TITLE_NAME_VN")
                    End If

                End Using

                If OtRegistration IsNot Nothing Then
                    hidStatus.Value = If(OtRegistration.STATUS.HasValue, OtRegistration.STATUS, 0)
                    If OtRegistration.FROM_DATE.HasValue Then
                        rdFromDate.SelectedDate = OtRegistration.FROM_DATE
                    End If
                    If OtRegistration.TO_DATE.HasValue Then
                        rdToDate.SelectedDate = OtRegistration.TO_DATE
                    End If
                    If OtRegistration.FROM_HOUR.HasValue Then
                        cboHoursFrom.Text = OtRegistration.FROM_HOUR.Value.Hour.ToString
                        cboMinuteFrom.Text = OtRegistration.FROM_HOUR.Value.Minute.ToString
                    End If
                    If OtRegistration.TO_HOUR.HasValue Then
                        cboHoursTo.Text = OtRegistration.TO_HOUR.Value.Hour.ToString
                        cboMinuteTo.Text = OtRegistration.TO_HOUR.Value.Minute.ToString
                    End If
                    txtReason.Text = OtRegistration.NOTE
                    ID_REG_GROUP = OtRegistration.ID_REGGROUP
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

            CurrentState = CommonMessage.STATE_NORMAL
            tbarMainToolBar.Items(1).Enabled = True
            tbarMainToolBar.Items(0).Enabled = False
            Select Case hidStatus.Value
                '0 Khai báo thông tin 
                '16 Da luu
                '19 Khong duyet qltt
                '20 Khong xac nhan nhan su
                '22 Khong duyet GM
                Case "", 0, PortalStatus.unsent
                    If userType = "User" Then
                        tbarMainToolBar.Items(0).Enabled = True
                        EnableControlAll(True, rdFromDate, rdToDate, cboHoursFrom, cboHoursTo, cboMinuteFrom, cboMinuteTo, txtReason)
                    Else
                        tbarMainToolBar.Items(0).Enabled = False
                        EnableControlAll(False, rdFromDate, rdToDate, cboHoursFrom, cboHoursTo, cboMinuteFrom, cboMinuteTo, txtReason)
                    End If
                    If Not String.IsNullOrEmpty(hidID.Value) AndAlso hidID.Value > 0 Then
                        CurrentState = CommonMessage.STATE_EDIT
                    Else
                        CurrentState = CommonMessage.STATE_NEW
                    End If
                Case Else
                    EnableControlAll(False, rdFromDate, rdToDate, cboHoursFrom, cboHoursTo, cboMinuteFrom, cboMinuteTo, txtReason)
            End Select
            'ChangeToolbarState()

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
                    ClearControlValue(rdFromDate, rdToDate, cboHoursFrom, cboHoursTo, cboMinuteFrom, cboMinuteTo, txtReason)

                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If CurrentState = CommonMessage.STATE_EDIT Then
                            com.DELETE_REG_BY_IDGROUP(ID_REG_GROUP)
                        End If
                        AppointmentList = AttendanceRepositoryStatic.Instance.GET_REG_PORTAL(CurrentUser.EMPLOYEE_ID, rdFromDate.SelectedDate, rdToDate.SelectedDate, "", cons.OVERTIME)
                        Dim startDate As Date?
                        Dim startDate_temp As Date?
                        Dim endDate As Date?
                        Dim Sum_Values As Decimal = 0
                        Dim Values As Decimal = 0
                        Dim SumPRgtUsed As Decimal? = 0 ' Phep nam da dang ky
                        Dim SumPRgtCurrent As Decimal? = 0 ' Phep nam dang dang ky
                        Dim difference As TimeSpan
                        If CheckValiadte() Then
                            perioId = AttendanceRepositoryStatic.Instance.GET_PERIOD(rdFromDate.SelectedDate)
                            'Kiẻm tra trạng thái bảng công
                            If Not CheckOrgPeriodCloseOT(orgId, perioId) Then
                                Exit Sub
                            End If
                            perioId = AttendanceRepositoryStatic.Instance.GET_PERIOD(rdToDate.SelectedDate)
                            'Kiểm tra trạng thái bảng công
                            If Not CheckOrgPeriodCloseOT(orgId, perioId) Then
                                Exit Sub
                            End If

                            If Not CheckEmployee(EmployeeId, rdFromDate.SelectedDate) Then
                                Exit Sub
                            End If

                            Dim dateFrom As Date
                            Dim dateTo As Date
                            Dim strDatafrom As String = rdFromDate.SelectedDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                            Dim strDataTo As String = rdToDate.SelectedDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)

                            DateTime.TryParseExact(String.Format("{0} {1}:{2}", strDatafrom, cboHoursFrom.SelectedValue, cboMinuteFrom.SelectedValue), "dd/MM/yyyy HH:mm", New CultureInfo("en-US"), DateTimeStyles.None, dateFrom)
                            DateTime.TryParseExact(String.Format("{0} {1}:{2}", strDataTo, cboHoursTo.SelectedValue, cboMinuteTo.SelectedValue), "dd/MM/yyyy HH:mm", New CultureInfo("en-US"), DateTimeStyles.None, dateTo)

                            If dateFrom > dateTo Then
                                difference = dateTo.AddDays(1) - dateFrom
                            Else
                                difference = dateTo - dateFrom
                            End If
                            Values = difference.TotalHours

                            startDate = rdFromDate.SelectedDate
                            endDate = rdToDate.SelectedDate
                            startDate_temp = rdFromDate.SelectedDate

                            Dim RGT_OT_TO As New Date?
                            Do
                                For Each item As APPOINTMENT_DTO In AppointmentList.Where(Function(f) f.WORKINGDAY >= startDate_temp AndAlso f.WORKINGDAY <= startDate_temp)
                                    ' Từ giờ đã đăng ký trước đó
                                    Dim FROM_HOUR = DateTime.ParseExact(startDate_temp.Value.Date.ToString("dd/MM/yyyy") + " " + Format(item.FROMHOUR, "HH:mm").ToString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                                    ' Đến giờ đã đăng ký trước đó
                                    Dim TO_HOUR = DateTime.ParseExact(startDate_temp.Value.Date.ToString("dd/MM/yyyy") + " " + Format(item.TOHOUR, "HH:mm").ToString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                                    ' Từ giờ đăng ký
                                    Dim RGT_OT_FROM = DateTime.ParseExact(String.Format("{0} {1}:{2}", startDate_temp.Value.Date.ToString("dd/MM/yyyy"), cboHoursFrom.SelectedValue, cboMinuteFrom.SelectedValue), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                                    'Đến giờ đăng ký
                                    If dateFrom > dateTo Then
                                        RGT_OT_TO = DateTime.ParseExact(String.Format("{0} {1}:{2}", startDate_temp.Value.AddDays(1).Date.ToString("dd/MM/yyyy"), cboHoursTo.SelectedValue, cboMinuteTo.SelectedValue), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                                    Else
                                        RGT_OT_TO = DateTime.ParseExact(String.Format("{0} {1}:{2}", startDate_temp.Value.Date.ToString("dd/MM/yyyy"), cboHoursTo.SelectedValue, cboMinuteTo.SelectedValue), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                                    End If

                                    If RGT_OT_FROM >= FROM_HOUR AndAlso RGT_OT_FROM < TO_HOUR AndAlso TO_HOUR <= RGT_OT_TO Then
                                        ShowMessage(Translate("Không thỏa điều kiện đăng ký"), NotifyType.Warning)
                                        Exit Sub
                                    End If

                                    If RGT_OT_FROM < FROM_HOUR AndAlso FROM_HOUR < RGT_OT_TO Then
                                        ShowMessage(Translate("Không thỏa điều kiện đăng ký"), NotifyType.Warning)
                                        Exit Sub
                                    End If
                                    If RGT_OT_FROM >= FROM_HOUR AndAlso RGT_OT_TO <= TO_HOUR Then
                                        ShowMessage(Translate("Không thỏa điều kiện đăng ký"), NotifyType.Warning)
                                        Exit Sub
                                    End If
                                Next
                                Sum_Values = Sum_Values + Values
                                startDate_temp = startDate_temp.Value.AddDays(1)
                            Loop Until startDate_temp > endDate
                            startDate_temp = startDate

                            Dim total_value_rgt = AttendanceRepositoryStatic.Instance.GET_TOTAL_OT_APPROVE3(EmployeeId, endDate)
                            Dim check = AttendanceRepositoryStatic.Instance.CHECK_RGT_OT(EmployeeId, startDate, endDate, String.Format("{0}:{1}", cboHoursFrom.SelectedValue, cboMinuteFrom.SelectedValue) _
                                                        , String.Format("{0}:{1}", cboHoursTo.SelectedValue, cboMinuteTo.SelectedValue), Sum_Values + total_value_rgt)
                            If check = 1 Then
                                ShowMessage(Translate("Nhân viên chưa được thiết lập ca hoặc ca không tồn tại, vui lòng kiểm tra lại"), NotifyType.Warning)
                                Exit Sub
                            ElseIf check = 2 Then
                                ShowMessage(Translate("Chức danh của nhân viên này không được đăng ký làm thêm, vui lòng kiểm tra lại"), NotifyType.Warning)
                                Exit Sub
                            ElseIf check = 3 Then
                                ShowMessage(Translate("Thời gian đăng ký OT không nằm trong khoảng ca làm việc, vui lòng kiểm tra lại"), NotifyType.Warning)
                                Exit Sub
                            ElseIf check = 4 Then
                                ShowMessage(Translate("Thời gian đăng ký nằm trong khoảng thời gian không tính tăng ca, vui lòng kiểm tra lại"), NotifyType.Warning)
                                Exit Sub
                            ElseIf check = 5 Then
                                ShowMessage(Translate("Số giờ đăng ký vượt quá quy định, vui lòng kiểm tra lại"), NotifyType.Warning)
                                Exit Sub
                            ElseIf check = 6 Then
                                ShowMessage(Translate("Không cho đăng ký làm thêm đối với trường hợp thai sản , vui lòng kiểm tra lại"), NotifyType.Warning)
                                Exit Sub
                            ElseIf check = 7 Then
                                ShowMessage(Translate("Không cho đăng ký làm thêm đối với trường hợp nuôi con dưới 12 tháng tuổi, vui lòng kiểm tra lại"), NotifyType.Warning)
                                Exit Sub
                            ElseIf check = 8 Then
                                ShowMessage(Translate("Nhân viên thử việc không được đăng ký làm thêm, vui lòng kiểm tra lại"), NotifyType.Warning)
                                Exit Sub
                            End If
                            GetIDREGGROUP_AND_SAVE()
                        Else
                            Exit Sub
                        End If
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    If userType = "User" Then
                        Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlOTRegistration")
                    ElseIf userType = "LM" Then
                        Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlOTRegistrationByManager")
                    ElseIf userType = "HR" Then
                        Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlOTRegistrationByHR")
                    End If
            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub GetIDREGGROUP_AND_SAVE()
        Dim rtnVal As Decimal
        Dim startDate As Date?
        Dim startDate_temp As Date?
        Dim endDate As Date?
        startDate = rdFromDate.SelectedDate
        endDate = rdToDate.SelectedDate
        Dim NOTE As String = ""
        Dim strId As String
        Dim lstId As New List(Of Decimal)
        'Dim VALUES As Decimal?

        log = LogHelper.GetUserLog
        Try
            dtTable.Clear()
            Dim IdGroup = AttendanceRepositoryStatic.Instance.GET_SEQ_PORTAL_RGT()

            If AppointmentList IsNot Nothing Then
                For Each item As APPOINTMENT_DTO In AppointmentList.Where(Function(f) f.WORKINGDAY >= startDate And f.WORKINGDAY <= endDate And f.STATUS = 0)
                    lstId.Add(item.ID)
                    NOTE = item.NOTE
                Next
                ' Lấy danh sách ID portal register
                For Each dr As Decimal In lstId
                    strId &= IIf(strId = vbNullString, dr, "," & dr)
                Next
                If strId = "," Then
                    strId = ""
                End If
            End If
            dtTable.TableName = "DATA"
            startDate_temp = rdFromDate.SelectedDate

            Dim dateFrom As Date
            Dim dateTo As Date
            Dim strDatafrom As String = rdFromDate.SelectedDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
            Dim strDataTo As String = rdToDate.SelectedDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)

            DateTime.TryParseExact(String.Format("{0} {1}:{2}", strDatafrom, cboHoursFrom.SelectedValue, cboMinuteFrom.SelectedValue), "dd/MM/yyyy HH:mm", New CultureInfo("en-US"), DateTimeStyles.None, dateFrom)
            DateTime.TryParseExact(String.Format("{0} {1}:{2}", strDataTo, cboHoursTo.SelectedValue, cboMinuteTo.SelectedValue), "dd/MM/yyyy HH:mm", New CultureInfo("en-US"), DateTimeStyles.None, dateTo)

            Dim difference As TimeSpan = dateTo - dateFrom
            Dim atRepo As New AttendanceRepository
            Do
                Dim dr As DataRow = dtTable.NewRow()
                Dim atRegDTO As New AT_PORTAL_REG_DTO
                dr("P_EMPLOYEE_ID") = CurrentUser.EMPLOYEE_ID
                dr("P_WORKINGDAY") = startDate_temp
                dr("P_SIGN_CODE") = ATConstant.SIGINCODE_OT
                dr("P_NVALUE") = difference.TotalHours
                If cboHoursFrom.Text <> "" And cboMinuteFrom.Text <> "" Then
                    dr("P_FROM_HOUR_STRING") = String.Format("{0}:{1}", cboHoursFrom.SelectedValue, cboMinuteFrom.SelectedValue)
                End If
                If cboHoursTo.Text <> "" And cboMinuteTo.Text <> "" Then
                    dr("P_TO_HOUR_STRING") = String.Format("{0}:{1}", cboHoursTo.SelectedValue, cboMinuteTo.SelectedValue)
                End If
                dr("P_NOTE") = txtReason.Text
                dr("P_ID_GROUP") = IdGroup
                dr("P_CREATED_BY") = log.Username
                dr("P_CREATED_LOG") = log.Ip + "/" + log.ComputerName
                dr("P_NB") = If(chkNB.Checked, 1, 0)
                dr("FROM_DATE") = rdFromDate.SelectedDate
                dr("TO_DATE") = rdToDate.SelectedDate
                dtTable.Rows.Add(dr)
                startDate_temp = startDate_temp.Value.AddDays(1)
            Loop Until startDate_temp > endDate
            rtnVal = CreatePlanningAppointment(dtTable)
            If rtnVal Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlOTRegistration")
                CurrentState = CommonMessage.STATE_NORMAL
                UpdateControlState()
            Else : ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, NotifyType.Error)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As Common.MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rtnVal As Decimal
        Dim startDate As Date?
        Dim startDate_temp As Date?
        Dim endDate As Date?
        startDate = rdFromDate.SelectedDate
        endDate = rdToDate.SelectedDate
        Dim NOTE As String = ""
        Dim strId As String
        Dim lstId As New List(Of Decimal)
        'Dim VALUES As Decimal?

        log = LogHelper.GetUserLog
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                dtTable.Clear()
                Select Case e.ActionName
                    Case "BTNREGISTER"

                        'Dim NEXTDATE = DateTime.Now.AddMonths(1)

                        'If (rdFromDate.SelectedDate > NEXTDATE And rdToDate.SelectedDate > NEXTDATE) Or (NEXTDATE >= rdFromDate.SelectedDate And rdToDate.SelectedDate > NEXTDATE) Then
                        '    ShowMessage(Translate("Thời gian đăng ký làm thêm không hợp lệ, vui lòng kiểm tra lại"), NotifyType.Warning)
                        '    Exit Sub
                        'End If

                        'Lay ID group moi lan dang ky portal ( muc dich de su dung bao cao)
                        Dim IdGroup = AttendanceRepositoryStatic.Instance.GET_SEQ_PORTAL_RGT()
                        If AppointmentList IsNot Nothing Then
                            For Each item As APPOINTMENT_DTO In AppointmentList.Where(Function(f) f.WORKINGDAY >= startDate And f.WORKINGDAY <= endDate And f.STATUS = 0)
                                lstId.Add(item.ID)
                                NOTE = item.NOTE
                            Next
                            ' Lấy danh sách ID portal register
                            For Each dr As Decimal In lstId
                                strId &= IIf(strId = vbNullString, dr, "," & dr)
                            Next
                            If strId = "," Then
                                strId = ""
                            End If
                        End If
                        dtTable.TableName = "DATA"
                        startDate_temp = rdFromDate.SelectedDate

                        Dim dateFrom As Date
                        Dim dateTo As Date
                        Dim strDatafrom As String = rdFromDate.SelectedDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                        Dim strDataTo As String = rdToDate.SelectedDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)

                        DateTime.TryParseExact(String.Format("{0} {1}:{2}", strDatafrom, cboHoursFrom.SelectedValue, cboMinuteFrom.SelectedValue), "dd/MM/yyyy HH:mm", New CultureInfo("en-US"), DateTimeStyles.None, dateFrom)
                        DateTime.TryParseExact(String.Format("{0} {1}:{2}", strDataTo, cboHoursTo.SelectedValue, cboMinuteTo.SelectedValue), "dd/MM/yyyy HH:mm", New CultureInfo("en-US"), DateTimeStyles.None, dateTo)

                        Dim difference As TimeSpan = dateTo - dateFrom
                        Dim atRepo As New AttendanceRepository
                        Do
                            Dim dr As DataRow = dtTable.NewRow()
                            Dim atRegDTO As New AT_PORTAL_REG_DTO
                            dr("P_EMPLOYEE_ID") = CurrentUser.EMPLOYEE_ID
                            dr("P_WORKINGDAY") = startDate_temp
                            dr("P_SIGN_CODE") = ATConstant.SIGINCODE_OT
                            dr("P_NVALUE") = difference.TotalHours
                            If cboHoursFrom.Text <> "" And cboMinuteFrom.Text <> "" Then
                                dr("P_FROM_HOUR_STRING") = String.Format("{0}:{1}", cboHoursFrom.SelectedValue, cboMinuteFrom.SelectedValue)
                            End If
                            If cboHoursTo.Text <> "" And cboMinuteTo.Text <> "" Then
                                dr("P_TO_HOUR_STRING") = String.Format("{0}:{1}", cboHoursTo.SelectedValue, cboMinuteTo.SelectedValue)
                            End If
                            dr("P_NOTE") = txtReason.Text
                            dr("P_ID_GROUP") = IdGroup
                            dr("P_CREATED_BY") = log.Username
                            dr("P_CREATED_LOG") = log.Ip + "/" + log.ComputerName
                            dr("P_NB") = If(chkNB.Checked, 1, 0)
                            dtTable.Rows.Add(dr)
                            startDate_temp = startDate_temp.Value.AddDays(1)
                            'atRegDTO.ID_EMPLOYEE = CurrentUser.EMPLOYEE_ID
                            'atRegDTO.FROM_DATE = startDate
                            'atRegDTO.TO_DATE = endDate
                            'atRegDTO.SIGN_CODE = ATConstant.SIGINCODE_OT
                            'atRegDTO.NVALUE = difference.TotalHours
                            'If cboHoursFrom.Text <> "" And cboMinuteFrom.Text <> "" Then
                            '    atRegDTO.FROM_HOUR = String.Format("{0}:{1}", cboHoursFrom.SelectedValue, cboMinuteFrom.SelectedValue)
                            'End If
                            'If cboHoursTo.Text <> "" And cboMinuteTo.Text <> "" Then
                            '    atRegDTO.TO_HOUR = String.Format("{0}:{1}", cboHoursTo.SelectedValue, cboMinuteTo.SelectedValue)
                            'End If
                            'atRegDTO.NOTE = txtReason.Text
                            'atRegDTO.ID_REGGROUP = IdGroup

                            'atRegDTO.ID_NB = If(chkNB.Checked, 1, 0)
                            'rtnVal = atRepo.InsertPortalRegister(atRegDTO)                            
                        Loop Until startDate_temp > endDate
                        rtnVal = CreatePlanningAppointment(dtTable)
                        If rtnVal Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlOTRegistration")
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                        Else : ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                End Select

            End If
        Catch ex As Exception
            ShowMessage(ex.Message, NotifyType.Error)
        End Try
    End Sub
    
#End Region

#Region "Custom"
    Public Function CreatePlanningAppointment(ByVal dtTable As DataTable) As Integer
        Try
            Return rep.ExecuteBatchCommand("PKG_AT_ATTENDANCE_PORTAL.AT_INSERT_PORTAL_REG", dtTable)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function CreateDataTableUpdate() As DataTable
        'Các cột đi theo thứ tự của các cột trong store update
        dtTable = New DataTable
        dtTable.TableName = "DATA"
        dtTable.Columns.Add("P_EMPLOYEE_ID", GetType(Decimal))
        dtTable.Columns.Add("P_WORKINGDAY", GetType(Date))
        dtTable.Columns.Add("P_SIGN_CODE", GetType(String))
        dtTable.Columns.Add("P_NVALUE", GetType(Decimal))
        dtTable.Columns.Add("P_TYPE_LEAVE", GetType(String))
        dtTable.Columns.Add("P_FROM_HOUR_STRING", GetType(String))
        dtTable.Columns.Add("P_TO_HOUR_STRING", GetType(String))
        dtTable.Columns.Add("P_NOTE", GetType(String))
        dtTable.Columns.Add("P_ID_GROUP", GetType(Decimal))
        dtTable.Columns.Add("P_CREATED_BY", GetType(String))
        dtTable.Columns.Add("P_CREATED_LOG", GetType(String))
        dtTable.Columns.Add("P_NB", GetType(Decimal))
        dtTable.Columns.Add("FROM_DATE", GetType(Date))
        dtTable.Columns.Add("TO_DATE", GetType(Date))
        Return dtTable
    End Function

    Private Function CheckOrgPeriodCloseOT(ByVal ORG_ID As String, ByVal perioId As Decimal) As Boolean
        If perioId <> 0 Then
            Dim check = AttendanceRepositoryStatic.Instance.AT_CHECK_ORG_PERIOD_STATUS_OT(ORG_ID, perioId)
            If check = 1 Then
                ShowMessage(Translate("Bảng công đã được đóng trước đó, vui lòng kiểm tra lại"), NotifyType.Warning)
                Return False
            ElseIf check = 2 Then
                ShowMessage(Translate("Phòng ban chưa được thiết lập trạng thái kỳ công"), NotifyType.Warning)
                Return False
            End If
        Else
            ShowMessage(Translate("Kỳ công không tồn tại, vui lòng kiểm tra lại"), NotifyType.Warning)
            Return False
        End If
        Return True
    End Function
    Private Function GET_TOTAL_OT_APPROVE(ByVal EMPID As Decimal?, ByVal ENDDATE As Date) As Decimal
        Dim result = AttendanceRepositoryStatic.Instance.GET_TOTAL_OT_APPROVE(EMPID, ENDDATE)
        Return result
    End Function
    'Private Function CheckEmployeeAppointment(ByVal lstEmp As List(Of EmployeeDTO), ByVal startdate As Date, ByVal enddate As Date, ByVal sign_code As AT_TIMESHEET_REGISTERDTO, ByRef sAction As String)
    '    Try
    '        Return AttendanceRepositoryStatic.Instance.CheckRegisterAppointmentByEmployee(lstEmp, startdate, enddate, ListSign, sign_code, sAction)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    Private Function CheckValiadte() As Boolean

        If rdFromDate.SelectedDate Is Nothing Or rdToDate.SelectedDate Is Nothing Then
            ShowMessage(Translate("Bạn chưa chọn ngày, vui lòng kiểm tra lại "), NotifyType.Warning)
            Return False
        End If
        'If rtpFROM.SelectedTime Is Nothing Or rtpTO.SelectedTime Is Nothing Then
        '    ShowMessage(Translate("Bạn chưa thời gian, vui lòng kiểm tra lại"), NotifyType.Warning)
        '    Return False
        'End If
        If rdFromDate.SelectedDate IsNot Nothing AndAlso rdToDate.SelectedDate IsNot Nothing AndAlso _
            rdToDate.SelectedDate < rdFromDate.SelectedDate Then
            ShowMessage(Translate("Đến ngày phải lớn hơn từ ngày, vui lòng kiểm tra lại"), NotifyType.Warning)
            Return False
        End If
        'If rtpFROM.SelectedTime IsNot Nothing AndAlso rtpTO.SelectedTime IsNot Nothing AndAlso rtpTO.SelectedDate < rtpFROM.SelectedDate Then
        '    ShowMessage(Translate("Thời gian kết thúc phải lớn hơn thời gian bắt đầu, vui lòng kiểm tra lại"), NotifyType.Warning)
        '    Return False
        'End If
        If txtReason.Text.Trim = "" Then
            ShowMessage(Translate("Bạn chưa nhập lý do "), NotifyType.Warning)
            Return False
        End If
        'For Each item As APPOINTMENT_DTO In AppointmentList.Where(Function(f) f.WORKINGDAY >= rdFromdate.SelectedDate And f.WORKINGDAY <= rdToDate.SelectedDate)
        '    If rdFromdate.SelectedDate = item.WORKINGDAY Then
        '        ShowMessage(Translate("Không thỏa điều kiện đăng ký"), NotifyType.Warning)
        '        Return False
        '    End If
        'Next
        Return True
    End Function

    Private Function CheckEmployee(ByVal empID As Decimal?, ByVal ENDDATE As Date) As Boolean
        Dim check = AttendanceRepositoryStatic.Instance.AT_CHECK_EMPLOYEE(empID, ENDDATE)
        If check = 1 Then
            ShowMessage(Translate("Nhân viên chưa có quyết định, vui lòng kiểm tra lại"), NotifyType.Warning)
            Return False
        ElseIf check = 2 Then
            ShowMessage(Translate("Cộng tác viên không thể đăng ký, vui lòng kiểm tra lại"), NotifyType.Warning)
            Return False
        End If
        Return True
    End Function

    Public Function FirstDayOfMonth(ByVal sourceDate As DateTime) As DateTime
        Return New DateTime(sourceDate.Year, sourceDate.Month, 1)
    End Function

    'Get the last day of the month
    Public Function LastDayOfMonth(ByVal sourceDate As DateTime) As DateTime
        Dim lastDay As DateTime = New DateTime(sourceDate.Year, sourceDate.Month, 1)
        Return lastDay.AddMonths(1).AddDays(-1)
    End Function
#End Region

End Class
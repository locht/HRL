Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Telerik.Web.UI
Imports Profile
Imports Attendance.AttendanceBusiness
Imports Common.CommonBusiness
Imports System.IO

Public Class ctrlOTRegister
    Inherits CommonView

    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"

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

    Private Property AppointmentList As List(Of AT_TIMESHEET_REGISTERDTO)
        Get
            Return PageViewState(Me.ID & "_AppointmentList")
        End Get
        Set(ByVal value As List(Of AT_TIMESHEET_REGISTERDTO))
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
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)

    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
    End Sub

    Public Overrides Sub BindData()
        Dim rep As New AttendanceRepository
        Try
            If ListComboData Is Nothing Then
                ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO
                ListComboData.GET_LIST_HS_OT = True
                rep.GetComboboxData(ListComboData)
            End If
            FillRadCombobox(cboleaveType, ListComboData.LIST_LIST_HS_OT, "CODE", "ID", True)
            If ListComboData.LIST_LIST_HS_OT.Count > 0 Then
                cboleaveType.SelectedIndex = 0
                ListManual = ListComboData.LIST_LIST_HS_OT
            End If
        Catch ex As Exception
            Throw ex
        End Try

        LoadEmployeeAppointment()
    End Sub
#End Region

#Region "Event"
    Protected Sub chkStatus_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkRegister.CheckedChanged,
        chkWaitForApprove.CheckedChanged, chkApproved.CheckedChanged, chkDenied.CheckedChanged
        LoadEmployeeAppointment()
    End Sub

    ''' <summary>
    ''' Handles the TimeSlotContextMenuItemClicked event of the sdlRegister control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Telerik.Web.UI.TimeSlotContextMenuItemClickedEventArgs" /> instance containing the event data.</param>
    Protected Sub sdlRegister_TimeSlotContextMenuItemClicked(ByVal sender As Object, ByVal e As Telerik.Web.UI.TimeSlotContextMenuItemClickedEventArgs) Handles sdlRegister.TimeSlotContextMenuItemClicked
        Dim lstAppointment As New List(Of AT_TIMESHEET_REGISTERDTO)

        Dim appointment As AT_TIMESHEET_REGISTERDTO
        Dim startDate As Date?
        Dim endDate As Date?
        Dim startDateInc As Date?

        Dim startindex As Integer = 0
        Dim lstUIAppointment As List(Of Telerik.Web.UI.Appointment)
        Dim lstEmp As New List(Of EmployeeDTO)
        Try
            startDate = hifStartDate.Value.ToDate()
            endDate = hifEndDate.Value.ToDate()
            If startDate Is Nothing Then
                startDate = e.TimeSlot.Start.Date
            End If
            If endDate Is Nothing Then
                endDate = e.TimeSlot.Start.Date
            End If
            Select Case e.MenuItem.Value
                Case "ADD"
                    If String.IsNullOrEmpty(cboleaveType.SelectedValue) Then
                        ShowMessage(Translate("Chưa chọn hệ số làm thêm để đăng ký."), NotifyType.Warning)
                        Exit Sub
                    End If

                    If Not tpkFrom.SelectedTime.HasValue OrElse Not tpkTo.SelectedTime.HasValue Then
                        ShowMessage(Translate("Chưa chọn giá trị để đăng ký."), NotifyType.Warning)
                        Exit Sub
                    End If

                    'If String.IsNullOrEmpty(cboNValue.SelectedValue) Then
                    '    ShowMessage(Translate("Chưa chọn thời gian nghỉ để đăng ký"), NotifyType.Warning)
                    '    Exit Sub
                    'End If

                    Dim query = (From p In sdlRegister.Appointments
                             Where p.Start >= startDate And p.Start <= endDate
                             Select p.ID).ToList()
                    If query IsNot Nothing AndAlso query.Count > 0 Then
                        ShowMessage(Translate("Không thể đăng ký cho những ngày đã đăng ký."), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim sAction = ""
                    lstEmp.Add(New EmployeeDTO With {.ID = CurrentUser.EMPLOYEE_ID,
                                                       .EMPLOYEE_CODE = CurrentUser.EMPLOYEE_CODE,
                                                       .FULLNAME_VN = CurrentUser.FULLNAME})

                    If Not AttendanceRepositoryStatic.Instance.CheckPeriodClose(lstEmp.Select(Function(f) f.ID).ToList,
                                                                                startDate, endDate, sAction) Then
                        ShowMessage(Translate(sAction), NotifyType.Warning)
                        Exit Sub
                    End If

                    ' Check kế hoạch
                    sAction = "IsPlan"

                    Dim itemInsert As New AttendanceBusiness.AT_PORTAL_REG_DTO With {
                        .ID_EMPLOYEE = CurrentUser.EMPLOYEE_ID,
                        .ID_SIGN = Decimal.Parse(cboleaveType.SelectedValue),
                        .FROM_DATE = startDate,
                        .TO_DATE = endDate,
                        .FROM_HOUR = tpkFrom.SelectedDate,
                        .TO_HOUR = tpkTo.SelectedDate,
                        .NVALUE = 1,
                        .SVALUE = ApproveProcess,
                        .PROCESS = ApproveProcess,
                        .NOTE = Translate("Lý do OT : ") & txtReason.Text & "<br />",
                        .ID_NB = checkNB.Checked
                    }

                    AttendanceRepositoryStatic.Instance.InsertPortalRegister(itemInsert)

                    LoadEmployeeAppointment()

                Case "DELINDATE"
                    SelectedAppointmentList = New List(Of AT_TIMESHEET_REGISTERDTO)
                    startDateInc = startDate
                    Do
                        appointment = New AT_TIMESHEET_REGISTERDTO
                        appointment.EMPLOYEEID = CurrentUser.EMPLOYEE_ID
                        appointment.WORKINGDAY = startDateInc.Value

                        lstUIAppointment = sdlRegister.GetTimeSlotFromIndex(e.TimeSlot.Index + startindex).Appointments.ToList()
                        If lstUIAppointment.Count > 0 Then
                            If lstUIAppointment(0).ForeColor = chkApproved.ForeColor Then
                                ShowMessage("Không thể xoá những đăng ký đã phê duyệt.", NotifyType.Warning)
                                Exit Sub
                            End If
                            If lstUIAppointment(0).ForeColor = chkWaitForApprove.ForeColor Then
                                If txtCancel.Text = "" Then
                                    ShowMessage("Bạn hãy nhập lý do hủy.", NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If
                        End If

                        lstAppointment.Add(appointment)
                        startDateInc = startDateInc.Value.AddDays(1)
                        startindex += 1
                    Loop Until startDateInc > endDate

                    SelectedAppointmentList = lstAppointment
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.MessageTitle = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE_TITLE)
                    ctrlMessageBox.ActionName = "DELINDATE"
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case "SENDAPPROVEINDATE"

                    Dim lstAppoint = (From p In AppointmentList
                                      Where p.WORKINGDAY >= startDate _
                                      And p.WORKINGDAY <= endDate _
                                      And p.STATUS = 0).ToList

                    SelectedAppointmentListId = lstAppoint.Select(Function(f) f.ID).ToList

                    If lstAppoint.Count = 0 Then
                        ShowMessage(Translate("Không tồn tại bản ghi ở trạng thái chưa gửi duyệt. Thao tác thực hiện không thành công."), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate("Bạn muốn gửi tất cả những đăng ký trong ngày để phê duyệt?")
                    ctrlMessageBox.MessageTitle = Translate("Gửi duyệt")
                    ctrlMessageBox.ActionName = "SENDAPPROVEINDATE"
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case "TODAY"
                    sdlRegister.SelectedDate = DateTime.Today
                    LoadEmployeeAppointment()

            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Handles the AppointmentContextMenuItemClicked event of the sdlRegister control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Telerik.Web.UI.AppointmentContextMenuItemClickedEventArgs" /> instance containing the event data.</param>
    Protected Sub sdlRegister_AppointmentContextMenuItemClicked(ByVal sender As Object, ByVal e As Telerik.Web.UI.AppointmentContextMenuItemClickedEventArgs) Handles sdlRegister.AppointmentContextMenuItemClicked
        Try
            Select Case e.MenuItem.Value
                Case "DEL"
                    If e.Appointment.ForeColor = chkApproved.ForeColor Then
                        ShowMessage("Không thể xoá những đăng ký đã phê duyệt.", NotifyType.Warning)
                        Exit Sub
                    End If

                    If e.Appointment.ForeColor = chkWaitForApprove.ForeColor Then
                        If txtCancel.Text = "" Then
                            ShowMessage("Bạn hãy nhập lý do hủy.", NotifyType.Warning)
                            Exit Sub
                        End If
                    End If

                    SelectedAppointmentList = New List(Of AT_TIMESHEET_REGISTERDTO)
                    SelectedAppointmentList.Add(New AT_TIMESHEET_REGISTERDTO With {.ID = e.Appointment.ID})

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.MessageTitle = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE_TITLE)
                    ctrlMessageBox.ActionName = "DELINDATE"
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case "SENDAPPROVE"
                    If e.Appointment.ForeColor = chkApproved.ForeColor Then
                        ShowMessage("Đăng ký đã được phê duyệt.", NotifyType.Warning)
                        Exit Sub
                    End If

                    If e.Appointment.ForeColor = chkWaitForApprove.ForeColor Then
                        ShowMessage("Đăng ký đang được chờ phê duyệt.", NotifyType.Warning)
                        Exit Sub
                    End If

                    If e.Appointment.ForeColor = chkDenied.ForeColor Then
                        ShowMessage("Đăng ký đã bị từ chối.", NotifyType.Warning)
                        Exit Sub
                    End If

                    SelectedAppointmentListId = New List(Of Decimal)
                    SelectedAppointmentListId.Add(e.Appointment.ID)
                    ctrlMessageBox.MessageText = Translate("Bạn muốn gửi đăng ký để phê duyệt?")
                    ctrlMessageBox.MessageTitle = Translate("Gửi duyệt")
                    ctrlMessageBox.ActionName = "SENDAPPROVE"
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case "TODAY"
                    sdlRegister.SelectedDate = DateTime.Today
                    LoadEmployeeAppointment()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub sdlRegister_NavigationComplete(ByVal sender As Object, ByVal e As Telerik.Web.UI.SchedulerNavigationCompleteEventArgs) Handles sdlRegister.NavigationComplete
        Select Case e.Command
            Case SchedulerNavigationCommand.NavigateToNextPeriod,
               SchedulerNavigationCommand.NavigateToPreviousPeriod,
               SchedulerNavigationCommand.SwitchToSelectedDay,
               SchedulerNavigationCommand.NavigateToSelectedDate
                lstHolidays = AttendanceRepositoryStatic.Instance.GetHolidayByCalender(sdlRegister.SelectedDate.FirstDateOfMonth.FirstDateOfWeek,
                                                                                   sdlRegister.SelectedDate.LastDateOfMonth.LastDateOfWeek)
                LoadEmployeeAppointment()
        End Select
    End Sub

    Protected Sub sdlRegister_AppointmentDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.SchedulerEventArgs) Handles sdlRegister.AppointmentDataBound
        Dim objData As AT_TIMESHEET_REGISTERDTO = CType(e.Appointment.DataItem, AT_TIMESHEET_REGISTERDTO)
        e.Appointment.ToolTip = objData.NOTE
        Select Case objData.STATUS
            Case chkRegister.Value
                e.Appointment.ForeColor = chkRegister.ForeColor
                e.Appointment.BorderColor = chkRegister.ForeColor
            Case chkWaitForApprove.Value
                e.Appointment.ForeColor = chkWaitForApprove.ForeColor
                e.Appointment.BorderColor = chkWaitForApprove.ForeColor
            Case chkApproved.Value
                e.Appointment.ForeColor = chkApproved.ForeColor
                e.Appointment.BorderColor = chkApproved.ForeColor
            Case chkDenied.Value
                e.Appointment.ForeColor = chkDenied.ForeColor
                e.Appointment.BorderColor = chkDenied.ForeColor
        End Select
    End Sub

    Protected Sub sdlRegister_TimeSlotCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.TimeSlotCreatedEventArgs) Handles sdlRegister.TimeSlotCreated
        If lstHolidays Is Nothing Then
            lstHolidays = AttendanceRepositoryStatic.Instance.GetHolidayByCalender(sdlRegister.SelectedDate.FirstDateOfMonth.FirstDateOfWeek,
                                                                                   sdlRegister.SelectedDate.LastDateOfMonth.LastDateOfWeek)
        End If
        For Each dt As DateTime In lstHolidays
            If DateTime.Compare(e.TimeSlot.Start.[Date], dt) = 0 Then
                e.TimeSlot.CssClass = "Holidays"
            End If
        Next
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As Common.MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rtnVal As Boolean
        Dim rtnString As String
        Dim rep As New AttendanceRepository
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Select Case e.ActionName
                    Case "DELINDATE"
                        For Each item As AT_TIMESHEET_REGISTERDTO In SelectedAppointmentList
                            Dim receiver As String = ""
                            Dim cc As String = ""
                            Dim subject As String
                            Dim body As String = ""
                            Dim fileAttachments As String = String.Empty
                            Dim reader As StreamReader
                            Dim data As DataTable
                            Dim view As String = ""

                            data = rep.GET_AT_PORTAL_REG_OT(item.ID, item.EMPLOYEEID, item.WORKINGDAY)
                            If data(0)("STATUS").ToString = "0" Then
                                Continue For
                            End If
                            If txtCancel.Text = "" Then
                                ShowMessage("Bạn hãy nhập lý do hủy.", NotifyType.Warning)
                                Exit Sub
                            End If

                            subject = String.Format("V/v {0} đăng ký làm thêm giờ ngày {1} đã được rút lại", data(0)("HOTEN"), data(0)("FROM_DATE"))
                            reader = New StreamReader(Server.MapPath("~/Modules/Attendance/Templates/AT_TIME_OT_CANCEL.html"))

                            body = reader.ReadToEnd
                            body = body.Replace("{HOTEN}", data(0)("HOTEN").ToString)
                            body = body.Replace("{TUGIO}", data(0)("FROM_HOUR").ToString)
                            body = body.Replace("{DENGIO}", data(0)("TO_HOUR").ToString)
                            body = body.Replace("{TUNGAY}", data(0)("FROM_DATE").ToString)
                            body = body.Replace("{LYDO}", txtCancel.Text)

                            cc = data(0)("WORK_EMAIL").ToString
                            receiver = data(0)("MAIL_TO").ToString
                            view = "AT_TIME_OT_CANCEL.html"

                            If Common.Common.sendEmailByServerMail(receiver, cc, subject, body, fileAttachments, view) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                                Exit Sub
                            End If
                        Next
                        rtnVal = AttendanceRepositoryStatic.Instance.DeletePortalRegisterByDateOT(SelectedAppointmentList, ListManual)

                    Case "DEL"
                        rtnVal = AttendanceRepositoryStatic.Instance.DeletePortalRegister(SelectedAppointmentList(0).ID)

                    Case "SENDAPPROVEINDATE", "SENDAPPROVE"
                        rtnString = AttendanceRepositoryStatic.Instance.SendRegisterToApprove(SelectedAppointmentListId,
                                                                                              ApproveProcess,
                                                                                              Request.Url.Scheme & Uri.SchemeDelimiter & Request.Url.DnsSafeHost)

                        If String.IsNullOrEmpty(rtnString) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            LoadEmployeeAppointment()
                        Else : ShowMessage(Translate(rtnString), NotifyType.Error)
                        End If
                        Exit Sub
                End Select

                If rtnVal Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    LoadEmployeeAppointment()
                Else : ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, NotifyType.Error)
        End Try
    End Sub
#End Region

#Region "Custom"
    ''' <summary>
    ''' Loads the employee appointment.
    ''' </summary>
    Private Sub LoadEmployeeAppointment()
        Dim startdate As Date
        Dim enddate As Date
        Dim curEmpId As Decimal = LogHelper.CurrentUser.EMPLOYEE_ID
        Dim status As New List(Of Short)

        sdlRegister.SelectedView = SchedulerViewType.MonthView
        startdate = sdlRegister.SelectedDate.FirstDateOfMonth.FirstDateOfWeek
        enddate = sdlRegister.SelectedDate.LastDateOfMonth.LastDateOfWeek

        If chkRegister.Checked Then status.Add(chkRegister.Value)
        If chkWaitForApprove.Checked Then status.Add(chkWaitForApprove.Value)
        If chkApproved.Checked Then status.Add(chkApproved.Value)
        If chkDenied.Checked Then status.Add(chkDenied.Value)

        AppointmentList = AttendanceRepositoryStatic.Instance.GetRegisterAppointmentInPortalOT(curEmpId, startdate, enddate, ListManual, status)
        sdlRegister.DataSource = AppointmentList
        sdlRegister.DataBind()
    End Sub

    'Private Function CheckEmployeeAppointment(ByVal lstEmp As List(Of EmployeeDTO), ByVal startdate As Date, ByVal enddate As Date, ByVal sign_code As AT_TIMESHEET_REGISTERDTO, ByRef sAction As String)
    '    Try
    '        Return AttendanceRepositoryStatic.Instance.CheckRegisterAppointmentByEmployee(lstEmp, startdate, enddate, ListSign, sign_code, sAction)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
#End Region

End Class
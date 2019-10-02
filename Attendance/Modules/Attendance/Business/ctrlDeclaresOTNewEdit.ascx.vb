Imports Framework.UI
Imports Common
Imports Attendance.ctrlNewInOut
Imports Telerik.Web.UI
Imports Framework.UI.Utilities
Imports Attendance.AttendanceRepository
Imports Attendance.AttendanceBusiness
Imports Common.CommonBusiness
Imports WebAppLog
Imports HistaffFrameworkPublic
Imports Common.Common
Imports HistaffFrameworkPublic.HistaffFrameworkEnum
Imports HistaffFrameworkPublic.FrameworkUtilities

Public Class ctrlDeclaresOTNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Dim checkSave As Boolean = False

#Region "Property"

    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
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
    Private Property EmployeeShift As EMPLOYEE_SHIFT_DTO
        Get
            Return PageViewState(Me.ID & "_EmployeeShift")
        End Get
        Set(ByVal value As EMPLOYEE_SHIFT_DTO)
            PageViewState(Me.ID & "_EmployeeShift") = value
        End Set
    End Property
    Private Property lstdtHoliday As DataTable
        Get
            Return PageViewState(Me.ID & "_lstdtHoliday")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_lstdtHoliday") = value
        End Set
    End Property
    Protected Property ListManual As List(Of AT_FMLDTO)
        Get
            Return PageViewState(Me.ID & "_ListFML")
        End Get
        Set(ByVal value As List(Of AT_FMLDTO))
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
    Property OtRegistration As AT_OT_REGISTRATIONDTO
        Get
            Return ViewState(Me.ID & "_OTRegistration")
        End Get
        Set(ByVal value As AT_OT_REGISTRATIONDTO)
            ViewState(Me.ID & "_OTRegistration") = value
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

    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lay params 
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim rep As New AttendanceRepository
        Try
            If Not IsPostBack Then
                If ListComboData Is Nothing Then
                    ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO
                    ListComboData.GET_LIST_OT_TYPE = True
                    ListComboData.GET_LIST_HS_OT = True
                    rep.GetComboboxData(ListComboData)
                End If

                FillRadCombobox(cboTypeOT, ListComboData.LIST_LIST_OT_TYPE, "NAME_VN", "ID", True)
                If ListComboData.LIST_LIST_OT_TYPE.Count > 0 Then
                    cboTypeOT.SelectedValue = 6922
                End If

                FillRadCombobox(cbohs_ot, ListComboData.LIST_LIST_HS_OT, "CODE", "CODE", True)

                If ListComboData.LIST_LIST_HS_OT.Count > 0 Then
                    cbohs_ot.SelectedIndex = 0
                End If

                Dim table As DataTable = LoadComboMinute()
                FillRadCombobox(cboFromAM, table, "NAME_VN", "ID", True)
                FillRadCombobox(cboToAM, table, "NAME_VN", "ID", True)
                FillRadCombobox(cboFromPM, table, "NAME_VN", "ID", True)
                FillRadCombobox(cboToPM, table, "NAME_VN", "ID", True)

                If hidEmpId.Value <> "" Then
                    EmployeeDto = rep.GetEmployeeInfor(hidEmpId.Value, Nothing)
                    If EmployeeDto IsNot Nothing AndAlso EmployeeDto.Rows.Count > 0 Then
                        txtFullName.Text = EmployeeDto.Rows(0)("FULLNAME_VN")
                        txtEmpCode.Text = EmployeeDto.Rows(0)("EMPLOYEE_CODE")
                        txtDepartment.Text = EmployeeDto.Rows(0)("ORG_NAME")
                        txtTitle.Text = EmployeeDto.Rows(0)("TITLE_NAME_VN")
                    End If
                End If
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
                Decimal.TryParse(Request.QueryString("employeeID"), empId)
                userType = Request.QueryString("typeUser")
                hidID.Value = id
                hidValid.Value = 0
                hidEmpId.Value = empId
                Dim dto As New AT_OT_REGISTRATIONDTO
                dto.ID = hidID.Value
                If hidID.Value = 0 Then
                    dto.EMPLOYEE_ID = hidEmpId.Value
                Else
                    dto.EMPLOYEE_ID = empId
                End If
                dto.P_MANAGER = "APP"
                Using rep As New AttendanceRepository

                    If dto.ID > 0 Then
                        Dim data = rep.GetOtRegistration(dto)
                        If data IsNot Nothing Then
                            OtRegistration = data.FirstOrDefault
                            EmployeeShift = rep.GetEmployeeShifts(empId, OtRegistration.REGIST_DATE, OtRegistration.REGIST_DATE).FirstOrDefault
                            hidSignId.Value = EmployeeShift.ID_SIGN
                            hidSignName.Value = EmployeeShift.SIGN_CODE
                        End If
                    End If
                    EmployeeDto = rep.GetEmployeeInfor(empId, Nothing)
                    If EmployeeDto IsNot Nothing AndAlso EmployeeDto.Rows.Count > 0 Then
                        txtFullName.Text = EmployeeDto.Rows(0)("FULLNAME_VN")
                        txtEmpCode.Text = EmployeeDto.Rows(0)("EMPLOYEE_CODE")
                        txtDepartment.Text = EmployeeDto.Rows(0)("ORG_NAME")
                        txtTitle.Text = EmployeeDto.Rows(0)("TITLE_NAME_VN")
                    End If

                End Using

                If OtRegistration IsNot Nothing Then
                    hidStatus.Value = If(OtRegistration.STATUS.HasValue, OtRegistration.STATUS, 0)
                    rdRegDate.SelectedDate = OtRegistration.REGIST_DATE
                    txtNote.Text = OtRegistration.NOTE
                    hid100.Value = OtRegistration.OT_100
                    hid150.Value = OtRegistration.OT_150
                    hid200.Value = OtRegistration.OT_200
                    hid210.Value = OtRegistration.OT_210
                    hid270.Value = OtRegistration.OT_270
                    hid300.Value = OtRegistration.OT_300
                    hid390.Value = OtRegistration.OT_370
                    hid180.Value = OtRegistration.OT_180

                    If OtRegistration.OT_100 IsNot Nothing AndAlso OtRegistration.OT_100 <> 0 Then
                        cbohs_ot.SelectedValue = "1.0"
                    End If

                    If OtRegistration.OT_150 IsNot Nothing AndAlso OtRegistration.OT_150 <> 0 Then
                        cbohs_ot.SelectedValue = "1.5"
                    End If

                    If OtRegistration.OT_180 IsNot Nothing AndAlso OtRegistration.OT_180 <> 0 Then
                        cbohs_ot.SelectedValue = "1.8"
                    End If

                    If OtRegistration.OT_200 IsNot Nothing AndAlso OtRegistration.OT_200 <> 0 Then
                        cbohs_ot.SelectedValue = "2.0"
                    End If

                    If OtRegistration.OT_210 IsNot Nothing AndAlso OtRegistration.OT_210 <> 0 Then
                        cbohs_ot.SelectedValue = "2.1"
                    End If

                    If OtRegistration.OT_270 IsNot Nothing AndAlso OtRegistration.OT_270 <> 0 Then
                        cbohs_ot.SelectedValue = "2.7"
                    End If

                    If OtRegistration.OT_300 IsNot Nothing AndAlso OtRegistration.OT_300 <> 0 Then
                        cbohs_ot.SelectedValue = "3.0"
                    End If

                    If OtRegistration.OT_370 IsNot Nothing AndAlso OtRegistration.OT_370 <> 0 Then
                        cbohs_ot.SelectedValue = "3.9"
                    End If

                    If OtRegistration.OT_TYPE_ID.HasValue Then
                        cboTypeOT.SelectedValue = OtRegistration.OT_TYPE_ID
                    End If
                    If OtRegistration.FROM_AM.HasValue Then
                        rntbFromAM.Value = OtRegistration.FROM_AM
                    End If
                    If OtRegistration.FROM_AM_MN.HasValue Then
                        cboFromAM.SelectedValue = OtRegistration.FROM_AM_MN
                    End If
                    If OtRegistration.TO_AM.HasValue Then
                        rntbToAM.Value = OtRegistration.TO_AM
                    End If
                    If OtRegistration.TO_AM_MN.HasValue Then
                        cboToAM.SelectedValue = OtRegistration.TO_AM_MN
                    End If
                    If OtRegistration.FROM_PM.HasValue Then
                        rntbFromPM.Value = OtRegistration.FROM_PM
                    End If
                    If OtRegistration.TO_PM_MN.HasValue Then
                        cboFromPM.SelectedValue = OtRegistration.FROM_PM_MN
                    End If
                    If OtRegistration.TO_PM.HasValue Then
                        rntbToPM.Value = OtRegistration.TO_PM
                    End If
                    If OtRegistration.TO_PM_MN.HasValue Then
                        cboToPM.SelectedValue = OtRegistration.TO_PM_MN
                    End If

                    Using rep As New HistaffFrameworkRepository
                        Dim response = rep.ExecuteStoreScalar("PKG_ATTENDANCE_BUSINESS.GET_TOTAL_ACCUMULATIVE_OT", New List(Of Object)(New Object() {empId, rdRegDate.SelectedDate.Value, OUT_NUMBER}))
                        If response(0).ToString() <> "" Then
                            rntTotalAccumulativeOTHours.Text = Decimal.Parse(response(0).ToString()).ToString("N1")
                        End If
                    End Using
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Try
            ShowPopupEmployee()
            If IsPostBack Then
                Exit Sub
            End If
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
                    ClearControlValue(rdRegDate, rntbFromAM, cboFromAM, rntbToAM, cboToAM, rntbToPM, rntbFromPM, cboFromPM, rntbToPM, cboToPM, txtNote)

                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If rntbFromAM.Value.HasValue Or rntbToAM.Value.HasValue Then
                            If Not rntbFromAM.Value.HasValue Then
                                ShowMessage(Translate("Phải nhập từ giờ làm thêm AM."), NotifyType.Warning)
                                rntbFromAM.Focus()
                                Exit Sub
                            Else
                                If cboFromAM.SelectedValue = "" Then
                                    ShowMessage(Translate("Vui lòng nhập phút AM"), NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If

                            If Not rntbToAM.Value.HasValue Then
                                ShowMessage(Translate("Phải nhập đến giờ làm thêm AM."), NotifyType.Warning)
                                rntbToAM.Focus()
                                Exit Sub
                            Else
                                If cboToAM.SelectedValue = "" Then
                                    ShowMessage(Translate("Vui lòng nhập phút AM"), NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If
                        End If
                        If rntbFromPM.Value.HasValue Or rntbToPM.Value.HasValue Then
                            If Not rntbFromPM.Value.HasValue Then
                                ShowMessage(Translate("Phải nhập từ giờ làm thêm PM."), NotifyType.Warning)
                                rntbFromPM.Focus()
                                Exit Sub
                            Else
                                If cboFromPM.SelectedValue = "" Then
                                    ShowMessage(Translate("Vui lòng nhập phút PM"), NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If

                            If Not rntbToPM.Value.HasValue Then
                                ShowMessage(Translate("Phải nhập đến giờ làm thêm PM."), NotifyType.Warning)
                                rntbToPM.Focus()
                                Exit Sub
                            Else
                                If cboToPM.SelectedValue = "" Then
                                    ShowMessage(Translate("Vui lòng nhập phút PM"), NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If
                        End If
                        If rntbFromAM.Value.HasValue AndAlso rntbFromAM.Value = 12 AndAlso cboFromAM.SelectedValue = 30 Then
                            ShowMessage(Translate("Chỉ được nhập 0 phút cho 12 giờ."), NotifyType.Warning)
                            cboFromAM.Focus()
                            Exit Sub
                        End If
                        If rntbToAM.Value.HasValue AndAlso rntbToAM.Value = 12 AndAlso cboToAM.SelectedValue = 30 Then
                            ShowMessage(Translate("Chỉ được nhập 0 phút cho 12 giờ."), NotifyType.Warning)
                            cboToAM.Focus()
                            Exit Sub
                        End If
                        If rntbFromPM.Value.HasValue AndAlso rntbFromPM.Value = 12 AndAlso cboFromPM.SelectedValue = 30 Then
                            ShowMessage(Translate("Chỉ được nhập 0 phút cho 12 giờ."), NotifyType.Warning)
                            cboFromPM.Focus()
                            Exit Sub
                        End If
                        If rntbToPM.Value.HasValue AndAlso rntbToPM.Value = 12 AndAlso cboToPM.SelectedValue = 30 Then
                            ShowMessage(Translate("Chỉ được nhập 0 phút cho 12 giờ."), NotifyType.Warning)
                            cboToPM.Focus()
                            Exit Sub
                        End If

                        
                        'If String.IsNullOrEmpty(hidTotal.Value) Or (Not String.IsNullOrEmpty(hidTotal.Value) AndAlso Decimal.Parse(hidTotal.Value) <= 0) Then
                        '    ShowMessage(Translate("Tổng đăng ký tăng ca không hợp lệ."), NotifyType.Warning)
                        '    UpdateControlState()
                        '    Exit Sub
                        'End If
                        Using rep As New AttendanceRepository
                            Dim periodid = rep.GetperiodID(hidEmpId.Value, rdRegDate.SelectedDate, rdRegDate.SelectedDate)
                            If periodid = 0 Then
                                ShowMessage(Translate("Kiểm tra lại kì công"), NotifyType.Warning)
                                Exit Sub
                            End If
                            Dim checkKicong = rep.CHECK_PERIOD_CLOSE(periodid)
                            If checkKicong = 0 Then
                                ShowMessage(Translate("Kì công đã đóng. Vui lòng kiểm tra lại!"), NotifyType.Warning)
                                Exit Sub
                            End If
                        End Using
                        Dim isInsert As Boolean = True

                        Dim obj As New AT_OT_REGISTRATIONDTO
                        obj.EMPLOYEE_ID = hidEmpId.Value
                        obj.IS_DELETED = 0
                        obj.NOTE = txtNote.Text
                        'obj.OT_TYPE_ID = ListComboData.LIST_LIST_OT_TYPE.Where(Function(f) f.CODE = "OT").Select(Function(g) g.ID).FirstOrDefault
                        obj.OT_TYPE_ID = cboTypeOT.SelectedValue
                        obj.REGIST_DATE = rdRegDate.SelectedDate
                        If hidSignId.Value.ToString = "" Then 'chua dang ky ca
                            lstdtHoliday = AttendanceRepositoryStatic.Instance.GetHolidayByCalenderToTable(rdRegDate.SelectedDate, rdRegDate.SelectedDate)
                            If lstdtHoliday.Rows.Count <= 0 Then 'Ko phai ngay le
                                ShowMessage("Nhân viên chưa được gán ca. Vui lòng kiểm tra lại!", NotifyType.Warning)
                                Exit Sub
                            End If
                        End If

                        obj.SIGN_ID = If(hidSignId.Value.ToString <> "", hidSignId.Value, Nothing)
                        obj.SIGN_CODE = hidSignName.Value

                        CalculateOT()

                        obj.TOTAL_OT = ObjToDecima(hidTotal.Value, 0)
                        obj.OT_100 = ObjToDecima(hid100.Value, 0)
                        obj.OT_150 = ObjToDecima(hid150.Value, 0)
                        obj.OT_200 = ObjToDecima(hid200.Value, 0)
                        obj.OT_210 = ObjToDecima(hid210.Value, 0)
                        obj.OT_270 = ObjToDecima(hid270.Value, 0)
                        obj.OT_300 = ObjToDecima(hid300.Value, 0)
                        obj.OT_370 = ObjToDecima(hid390.Value, 0)
                        obj.OT_180 = ObjToDecima(hid180.Value, 0)

                        obj.FROM_AM = rntbFromAM.Value
                        If Not String.IsNullOrEmpty(cboFromAM.SelectedValue) Then
                            obj.FROM_AM_MN = cboFromAM.SelectedValue
                        End If
                        obj.TO_AM = rntbToAM.Value
                        If Not String.IsNullOrEmpty(cboToAM.SelectedValue) Then
                            obj.TO_AM_MN = cboToAM.SelectedValue
                        End If

                        obj.FROM_PM = rntbFromPM.Value
                        If Not String.IsNullOrEmpty(cboFromPM.SelectedValue) Then
                            obj.FROM_PM_MN = cboFromPM.SelectedValue
                        End If
                        obj.TO_PM = rntbToPM.Value
                        If Not String.IsNullOrEmpty(cboToPM.SelectedValue) Then
                            obj.TO_PM_MN = cboToPM.SelectedValue
                        End If

                        obj.STATUS = 1
                        hidStatus.Value = 1

                        Using rep As New AttendanceRepository

                            If String.IsNullOrEmpty(hidID.Value) Then
                                hidID.Value = 0
                            End If
                            If hidID.Value <> 0 Then
                                isInsert = False
                            End If
                            obj.ID = hidID.Value
                            Dim valid = rep.ValidateOtRegistration(obj)
                            If valid Then
                                ShowMessage(Translate("Ngày OT đã được đăng ký, vui lòng chọn ngày khác."), NotifyType.Warning)
                                UpdateControlState()
                                Exit Sub
                            End If

                            If isInsert Then
                                rep.InsertOtRegistration(obj, hidID.Value)
                                obj.ID = hidID.Value
                            Else
                                obj.ID = hidID.Value
                                rep.ModifyotRegistration(obj, hidID.Value)
                            End If

                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlDeclaresOT&group=Business")
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                        End Using
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlDeclaresOT&group=Business")
            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ''' <lastupdate>
    ''' 06/07/2017 17:34
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho button btnEmployee
    ''' Hien thi popup co isLoadPopup = 1 khi click vao button
    ''' Cap nhat lai trang thai của cac control tren page
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/07/2017 17:48
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien khi click ctrlFind_CancelClick
    ''' Cap nhat trang thai isLoadPopup = 0
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFind_CancelClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles ctrlFindEmployeePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            isLoadPopup = 0
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of Common.CommonBusiness.EmployeePopupFindDTO)
        Try
            Dim a As Object = ctrlFindEmployeePopup.SelectedEmployee
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of Common.CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidEmpId.Value = item.EMPLOYEE_ID
                txtFullName.Text = item.FULLNAME_VN
                txtDepartment.Text = item.ORG_NAME
                txtEmpCode.Text = item.EMPLOYEE_CODE
                txtTitle.Text = item.TITLE_NAME
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ShowPopupEmployee()
        Try
            If FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                FindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
            End If

            If isLoadPopup = 1 Then
                ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                'ctrlFindEmployeePopup.MustHaveTerminate = True
                ctrlFindEmployeePopup.MustHaveContract = False
                ctrlFindEmployeePopup.MultiSelect = False
                FindEmployee.Controls.Add(ctrlFindEmployeePopup)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub rdRegDate_Select(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdRegDate.SelectedDateChanged
        Try
            If rdRegDate.SelectedDate IsNot Nothing Then
                Using rep As New AttendanceRepository
                    Dim dto As New AT_OT_REGISTRATIONDTO
                    dto.REGIST_DATE = rdRegDate.SelectedDate
                    dto.EMPLOYEE_ID = hidEmpId.Value
                    Dim validRegistDate = rep.CheckRegDateBetweenJoinAndTerDate(hidEmpId.Value, rdRegDate.SelectedDate)
                    If Not validRegistDate Then
                        ShowMessage(Translate("Ngày làm thêm phải sau ngày vào công ty và trước ngày nghỉ việc."), NotifyType.Warning)
                        rdRegDate.ClearValue()

                        hidSignId.Value = Nothing
                        rdRegDate.Focus()
                        Exit Sub
                    End If
                    'Dim data = rep.GetOtRegistration(dto)
                    'If data IsNot Nothing AndAlso data.Where(Function(f) f.ID <> hidID.Value).FirstOrDefault IsNot Nothing Then
                    '    ShowMessage(Translate("Ngày làm thêm đã được đăng ký"), NotifyType.Warning)
                    '    rdRegDate.ClearValue()
                    '    hidSignId.Value = Nothing
                    '    rdRegDate.Focus()
                    '    Exit Sub
                    'End If

                    EmployeeShift = rep.GetEmployeeShifts(hidEmpId.Value, rdRegDate.SelectedDate, rdRegDate.SelectedDate).FirstOrDefault
                    If EmployeeShift IsNot Nothing Then
                        hidSignId.Value = EmployeeShift.ID_SIGN
                        hidSignName.Value = EmployeeShift.SIGN_CODE
                        'lstdtHoliday = AttendanceRepositoryStatic.Instance.GetHolidayByCalenderToTable(rdRegDate.SelectedDate, rdRegDate.SelectedDate)
                        'CalculateOT()
                    End If

                End Using

                Using rep As New HistaffFrameworkRepository
                    Dim response = rep.ExecuteStoreScalar("PKG_ATTENDANCE_BUSINESS.GET_TOTAL_ACCUMULATIVE_OT", New List(Of Object)(New Object() {hidEmpId.Value, rdRegDate.SelectedDate.Value, OUT_NUMBER}))
                    If response IsNot Nothing Then
                        rntTotalAccumulativeOTHours.Text = Decimal.Parse(response(0).ToString()).ToString("N1")
                    End If
                End Using
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

    Private Function LoadComboMinute() As DataTable
        Try
            Dim table As New DataTable
            table.Columns.Add("NAME_VN", GetType(String))
            table.Columns.Add("ID", GetType(Decimal))
            Dim row As DataRow
            row = table.NewRow
            row("ID") = 0
            row("NAME_VN") = "0"
            table.Rows.Add(row)

            row = table.NewRow
            row("ID") = 30
            row("NAME_VN") = "30"
            table.Rows.Add(row)
            Return table
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    Private Function CalculateOT() As Boolean
        If rdRegDate.SelectedDate.HasValue Then
            Dim totalHour As Decimal = 0.0
            Dim fromAM As Decimal = 0.0
            Dim fromMNAM As Decimal = 0.0
            Dim toAM As Decimal = 0.0
            Dim toMMAM As Decimal = 0.0
            Dim fromPM As Decimal = 0.0
            Dim fromMNPM As Decimal = 0.0
            Dim toPM As Decimal = 0.0
            Dim toMNPM As Decimal = 0.0

            Dim totalFromAM As Decimal = 0.0
            Dim totalToAM As Decimal = 0.0
            Dim totalFromPM As Decimal = 0.0
            Dim totalToPM As Decimal = 0.0
            Dim OTAM As Decimal = 0.0 'tong tgian OT tu 6am - 12am
            Dim AM As Decimal = 0.0 'tong tgian OT tu 0am - 6am
            Dim OTPM As Decimal = 0.0 'tong tgian OT tu 12h01pm - 10pm
            Dim PM As Decimal = 0.0 'tong tgian OT tu 10pm - 11h59pm


            Try
                'AM
                If rntbFromAM.Value.HasValue And rntbToAM.Value.HasValue Then
                    fromAM = IIf(rntbFromAM.Value.HasValue, rntbFromAM.Value, 0.0)
                    fromMNAM = Decimal.Parse(If(cboFromAM.SelectedValue = 30, 0.5, 0))
                    toAM = IIf(rntbToAM.Value.HasValue, rntbToAM.Value, 0.0)
                    toMMAM = Decimal.Parse(If(cboToAM.SelectedValue = 30, 0.5, 0))
                    totalFromAM = fromAM + fromMNAM
                    totalToAM = toAM + toMMAM
                    If totalFromAM > totalToAM Then
                        ShowMessage(Translate("Giờ làm thêm AM không hợp lệ."), NotifyType.Warning)
                        rntbToAM.Focus()
                        Return False
                    End If
                    AM = totalToAM - totalFromAM
                    'If totalFromAM >= 0 And totalFromAM <= 6 AndAlso totalToAM <= 6 Then 'OT ban dem
                    '    OTAM = 0
                    '    AM = totalToAM - totalFromAM
                    'ElseIf totalFromAM >= 0 And totalFromAM <= 6 AndAlso totalToAM > 6 Then 'OT ban dem, co them OT ban ngay
                    '    OTAM = totalToAM - 6
                    '    AM = 6 - totalFromAM
                    'ElseIf totalFromAM > 6 Then 'OT ban ngay
                    '    OTAM = totalToAM - totalFromAM
                    '    AM = 0
                    'End If
                End If
                'PM
                If rntbFromPM.Value.HasValue And rntbToPM.Value.HasValue Then
                    fromPM = IIf(rntbFromPM.Value.HasValue, rntbFromPM.Value, 0.0)
                    fromMNPM = Decimal.Parse(If(cboFromPM.SelectedValue = 30, 0.5, 0))
                    toPM = IIf(rntbToPM.Value.HasValue, rntbToPM.Value, 0.0)
                    toMNPM = Decimal.Parse(If(cboToPM.SelectedValue = 30, 0.5, 0))
                    totalFromPM = fromPM + fromMNPM
                    totalToPM = toPM + toMNPM
                    If totalFromPM > totalToPM Then
                        ShowMessage(Translate("Giờ làm thêm PM không hợp lệ."), NotifyType.Warning)
                        rntbFromPM.Focus()
                        Return False
                    End If
                    PM = totalToPM - totalFromPM
                    'If totalFromPM >= 10 Then 'OT ban dem
                    '    OTPM = totalToPM - totalFromPM
                    '    PM = 0
                    'ElseIf totalFromPM >= 0 And totalFromPM < 10 AndAlso totalToPM > 10 Then 'OT ban ngay, co them OT ban dem
                    '    OTPM = 10 - totalFromPM
                    '    PM = totalToPM - 10
                    'ElseIf totalFromPM >= 0 And totalFromPM < 10 AndAlso totalToPM <= 10 Then 'OT ban ngay
                    '    OTPM = totalToPM - totalFromPM
                    '    PM = 0
                    'End If
                End If
                'totalHour = OTAM + AM + OTPM + PM 'vd: 6h pm - 12h pm -> OTPM = 4, PM = 2 
                totalHour = AM + PM
                hidTotal.Value = totalHour
                hid100.Value = 0.0
                hid150.Value = 0.0
                hid180.Value = 0.0
                hid200.Value = 0.0
                hid210.Value = 0.0
                hid270.Value = 0.0
                hid300.Value = 0.0
                hid390.Value = 0.0

                If cbohs_ot.SelectedValue = "1.0" Then
                    hid100.Value = totalHour
                End If

                If cbohs_ot.SelectedValue = "1.5" Then
                    hid150.Value = totalHour
                End If

                If cbohs_ot.SelectedValue = "1.8" Then
                    hid180.Value = totalHour
                End If

                If cbohs_ot.SelectedValue = "2.0" Then
                    hid200.Value = totalHour
                End If

                If cbohs_ot.SelectedValue = "2.1" Then
                    hid210.Value = totalHour
                End If

                If cbohs_ot.SelectedValue = "2.7" Then
                    hid270.Value = totalHour
                End If

                If cbohs_ot.SelectedValue = "3.0" Then
                    hid300.Value = totalHour
                End If

                If cbohs_ot.SelectedValue = "3.9" Then
                    hid390.Value = totalHour
                End If


                'lstdtHoliday = AttendanceRepositoryStatic.Instance.GetHolidayByCalenderToTable(rdRegDate.SelectedDate, rdRegDate.SelectedDate)
                'If lstdtHoliday IsNot Nothing AndAlso lstdtHoliday.Rows.Count > 0 Then
                '    'nghi bu
                '    hid300.Value = OTPM + OTAM
                '    hid390.Value = AM + PM
                '    'ElseIf (rdRegDate.SelectedDate.Value.ToString("dd-MM") = "25-12") Or (EmployeeShift IsNot Nothing AndAlso EmployeeShift.SIGN_CODE = "OFF") Then
                '    'hid200.Value = AM + PM
                '    'hid270.Value = OTAM + OTPM
                'ElseIf lstdtHoliday.Rows.Count <= 0 Then
                '    hid200.Value = OTPM + OTAM
                '    hid270.Value = AM + PM
                'Else
                '    'If workingType <> 1090 Then 'Khac Office Type 
                '    '    hid200.Value = AM + PM
                '    '    hid270.Value = OTAM + OTPM
                '    'Else
                '    hid150.Value = OTPM + OTAM
                '    hid210.Value = AM + PM
                '    'End If
                '    'End If
                'End If
                Return True
            Catch ex As Exception
                ShowMessage(Translate("Thời gian OT không hợp lệ."), NotifyType.Warning)
                Return False
            End Try
        End If
    End Function


#End Region

End Class


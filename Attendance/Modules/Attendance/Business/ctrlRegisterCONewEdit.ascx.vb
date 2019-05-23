
Imports Framework.UI
Imports Common
Imports Attendance.ctrlNewInOut
Imports Telerik.Web.UI
Imports Framework.UI.Utilities
Imports Attendance.AttendanceRepository
Imports Attendance.AttendanceBusiness
Imports Common.CommonBusiness
Imports WebAppLog

Public Class ctrlRegisterCONewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String

    Dim _myLog As New MyLog()
    Dim pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()

#Region "Property"
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    Property isEdit As Boolean
        Get
            Return ViewState(Me.ID & "_isEdit")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isEdit") = value
        End Set
    End Property

    Property Employee_id As Integer
        Get
            Return ViewState(Me.ID & "_Employee_id")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_Employee_id") = value
        End Set
    End Property

    Property _Value As Decimal?
        Get
            Return ViewState(Me.ID & "_Value")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_Value") = value
        End Set
    End Property

    Property RegisterLeave As AT_LEAVESHEETDTO
        Get
            Return ViewState(Me.ID & "_AT_LEAVESHEETDTO")
        End Get
        Set(ByVal value As AT_LEAVESHEETDTO)
            ViewState(Me.ID & "_AT_LEAVESHEETDTO") = value
        End Set
    End Property

    Property RegisterLeaveList As List(Of AT_LEAVESHEETDTO)
        Get
            Return ViewState(Me.ID & "_AT_LEAVESHEETLISTDTO")
        End Get
        Set(ByVal value As List(Of AT_LEAVESHEETDTO))
            ViewState(Me.ID & "_AT_LEAVESHEETLISTDTO") = value
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

    Public ReadOnly Property CurrentUser As UserDTO
        Get
            Return LogHelper.CurrentUser
        End Get
    End Property

    Property totalDayResOld As Integer
        Get
            Return ViewState(Me.ID & "_totalDayResOld")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_totalDayResOld") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CurrentState = CommonMessage.STATE_EDIT
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Xét các trạng thái của grid rgWorkschedule
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SetGridFilter(rgWorkschedule)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgWorkschedule.AllowCustomPaging = True
            rgWorkschedule.ClientSettings.EnablePostBackOnRowClick = False
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Bind lai du lieu cho grid rgWorkschedule
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New AttendanceRepository
        'Dim periodid As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Message = Request.Params("VIEW")
            'If Not IsPostBack Then
            '    If Request.Params("periodid") IsNot Nothing AndAlso Request.Params("periodid").ToString <> "" Then
            '        periodid = Request.Params("periodid")
            '        Dim period = rep.LOAD_PERIODByID(New AT_PERIODDTO With {.PERIOD_ID = periodid})
            '        'rdFromdate.SelectedDate = period.START_DATE
            '        'rdEnddate.SelectedDate = period.END_DATE
            '        rdLeaveFrom.MinDate = period.START_DATE
            '        rdLeaveFrom.MaxDate = period.END_DATE
            '        rdLeaveTo.MinDate = period.START_DATE
            '        rdLeaveTo.MaxDate = period.END_DATE
            '    End If
            'End If
            Select Case Message
                Case "TRUE"
                    Dim obj As New AT_LEAVESHEETDTO
                    obj.ID = Decimal.Parse(Request.Params("ID"))
                    CurrentState = CommonMessage.STATE_EDIT
                    obj = rep.GetLeaveById(obj.ID)
                    RegisterLeave = New AT_LEAVESHEETDTO
                    If obj IsNot Nothing Then
                        rgWorkschedule.Enabled = False
                        RegisterLeaveList = New List(Of AT_LEAVESHEETDTO)
                        Dim item As New AT_LEAVESHEETDTO
                        item.EMPLOYEE_CODE = obj.EMPLOYEE_CODE
                        item.VN_FULLNAME = obj.VN_FULLNAME
                        item.TITLE_NAME = obj.TITLE_NAME
                        item.ORG_ID = obj.ORG_ID
                        item.ORG_NAME = obj.ORG_NAME
                        item.EMPLOYEE_ID = obj.EMPLOYEE_ID
                        item.BALANCE_NOW = obj.BALANCE_NOW
                        item.NBCL = obj.NBCL
                        RegisterLeaveList.Add(item)

                        Employee_id = obj.EMPLOYEE_ID

                        RegisterLeave.ORG_ID = obj.ORG_ID
                        Employee_id = obj.EMPLOYEE_ID
                        txtnote.Text = obj.NOTE_APP
                        rdLeaveFrom.SelectedDate = obj.LEAVE_FROM
                        rdLeaveTo.SelectedDate = obj.LEAVE_TO
                        If obj.DAY_NUM.HasValue Then
                            txtDayNum.Text = obj.DAY_NUM.ToString
                        End If
                        chkIsWorkingDay.Checked = obj.IS_WORKING_DAY
                        cboKieuCong.SelectedValue = obj.MANUAL_ID
                        For Each x As AttendanceBusiness.AT_TIME_MANUALDTO In ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE
                            If (x.ID = obj.MANUAL_ID) Then
                                cboKieuCong.SelectedValue = obj.MANUAL_ID
                            End If
                        Next
                        For Each x As AttendanceBusiness.AT_FMLDTO In ListComboData.LIST_LIST_SIGN
                            If (x.ID = obj.MORNING_ID) Then
                                cbosang.SelectedValue = obj.MORNING_ID
                            End If
                        Next
                        For Each x As AttendanceBusiness.AT_FMLDTO In ListComboData.LIST_LIST_SIGN
                            If (x.ID = obj.AFTERNOON_ID) Then
                                cboChieu.SelectedValue = obj.AFTERNOON_ID
                            End If
                        Next
                        _Value = obj.ID
                        ' tính số ngày đăng ký cũ trước khi sửa
                        totalDayResOld = rep.GetCAL_DAY_LEAVE_OLD(obj.EMPLOYEE_ID, obj.LEAVE_FROM, obj.LEAVE_TO).Rows(0)(0)
                    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức BindData
    ''' Gọi phương thức load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()
            Refresh()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham khoi taso toolbar voi cac button them moi, sua, xuat file, xoa, in hop dong
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarOT
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click nut cancel cua popup ctrlFindPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 0
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command khi click item tren toolbar
    ''' Command xuat luu, huy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As AT_LEAVESHEETDTO
        Dim rep As New AttendanceRepository
        Dim gCODE As String = ""
        Dim gstatus As Integer = 0
        Dim lstEmp As New List(Of EmployeeDTO)
        Dim sAction As String
        Dim dtDataUserPHEPNAM As New DataTable
        Dim dtDataUserPHEPBU As New DataTable
        Dim totalDayRes As New DataTable
        Dim at_entilement As New AT_ENTITLEMENTDTO
        Dim at_compensatory As New AT_COMPENSATORYDTO
        Dim gListPhepEmp As String = ""
        Dim gListBuEmp As String = ""
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If Decimal.Parse(cboChieu.SelectedValue) = 121 And Decimal.Parse(cbosang.SelectedValue) = 121 Then
                            ShowMessage(Translate("Không thể chọn kiểu công sáng và chiều là đi làm"), Utilities.NotifyType.Error)
                            Exit Sub
                        End If
                        If RegisterLeaveList() Is Nothing Then
                            ShowMessage(Translate("Vui lòng chọn nhân viên cần đăng ký làm thêm!"), NotifyType.Error)
                            Exit Sub
                        End If
                        If _Value.HasValue Then
                            Dim repCheck As New CommonRepository
                            Dim lstCheck As New List(Of Decimal)
                            lstCheck.Add(_Value)
                            If repCheck.CheckExistIDTable(lstCheck, "AT_LEAVESHEET", "ID") Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                Exit Sub
                            End If
                        End If
                        For index = 0 To RegisterLeaveList.Count - 1
                            lstEmp.Add(New EmployeeDTO With {.ID = RegisterLeaveList(index).EMPLOYEE_ID,
                                                        .EMPLOYEE_CODE = RegisterLeaveList(index).EMPLOYEE_CODE,
                                                        .FULLNAME_VN = RegisterLeaveList(index).VN_FULLNAME})

                            If Not AttendanceRepositoryStatic.Instance.CheckPeriodClose(lstEmp.Select(Function(f) f.ID).ToList,
                                                                                        rdLeaveFrom.SelectedDate, rdLeaveTo.SelectedDate, sAction) Then
                                ShowMessage(Translate(sAction), NotifyType.Warning)
                                Exit Sub
                            End If

                            dtDataUserPHEPNAM = rep.GetTotalPHEPNAM(RegisterLeaveList(index).EMPLOYEE_ID, rdLeaveTo.SelectedDate.Value.Year, cboKieuCong.SelectedValue)
                            at_entilement = rep.GetPhepNam(RegisterLeaveList(index).EMPLOYEE_ID, rdLeaveTo.SelectedDate.Value.Year)
                            at_compensatory = rep.GetNghiBu(RegisterLeaveList(index).EMPLOYEE_ID, rdLeaveTo.SelectedDate.Value.Year)
                            totalDayRes = rep.GetTotalDAY(RegisterLeaveList(index).EMPLOYEE_ID, 251, rdLeaveFrom.SelectedDate, rdLeaveTo.SelectedDate)
                            ' nếu là kiểu đăng ký nghỉ phép
                            If cbosang.SelectedValue = 251 And cboChieu.SelectedValue = 251 Then
                                If dtDataUserPHEPNAM IsNot Nothing And at_entilement IsNot Nothing Then
                                    If at_entilement.TOTAL_HAVE.Value - (dtDataUserPHEPNAM.Rows(0)(0) + totalDayRes.Rows(0)(0)) + totalDayResOld < -3 Then
                                        gListPhepEmp = gListPhepEmp & RegisterLeaveList(index).EMPLOYEE_CODE & ","
                                        Continue For
                                    End If
                                End If
                            ElseIf cbosang.SelectedValue = 251 Or cboChieu.SelectedValue = 251 Then
                                If dtDataUserPHEPNAM IsNot Nothing And at_entilement IsNot Nothing Then
                                    If at_entilement.TOTAL_HAVE.Value - (dtDataUserPHEPNAM.Rows(0)(0) + totalDayRes.Rows(0)(0) / 2) + totalDayResOld < -3 Then
                                        gListPhepEmp = gListPhepEmp & RegisterLeaveList(index).EMPLOYEE_CODE & ","
                                        Continue For
                                    End If
                                End If
                            End If
                            ' nếu là kiểu đăng ký nghỉ bù
                            dtDataUserPHEPBU = rep.GetTotalPHEPBU(RegisterLeaveList(index).EMPLOYEE_ID, rdLeaveTo.SelectedDate.Value.Year, cboKieuCong.SelectedValue)
                            totalDayRes = rep.GetTotalDAY(RegisterLeaveList(index).EMPLOYEE_ID, 255, rdLeaveFrom.SelectedDate, rdLeaveTo.SelectedDate)
                            If cbosang.SelectedValue = 255 And cboChieu.SelectedValue = 255 Then
                                If dtDataUserPHEPBU IsNot Nothing And at_compensatory IsNot Nothing Then
                                    If at_compensatory.TOTAL_HAVE.Value - (dtDataUserPHEPBU.Rows(0)(0) + totalDayRes.Rows(0)(0)) + totalDayResOld < 0 Then
                                        gListBuEmp = gListBuEmp & RegisterLeaveList(index).EMPLOYEE_CODE & ","
                                        Continue For
                                    End If
                                End If
                            ElseIf cbosang.SelectedValue = 255 Or cboChieu.SelectedValue = 255 Then
                                If dtDataUserPHEPBU IsNot Nothing And at_compensatory IsNot Nothing Then
                                    If at_compensatory.TOTAL_HAVE.Value - (dtDataUserPHEPBU.Rows(0)(0) + totalDayRes.Rows(0)(0) / 2) + totalDayResOld < 0 Then
                                        gListBuEmp = gListBuEmp & RegisterLeaveList(index).EMPLOYEE_CODE & ","
                                        Continue For
                                    End If
                                End If
                            End If
                        Next
                        If Not gListBuEmp.Equals("") Or Not gListPhepEmp.Equals("") Then
                            ShowMessage("Nhân viên đăng ký nghỉ phép vượt quá mức cho phép: " & gListPhepEmp & " Nhân viên đăng ký nghỉ bù vượt quá mức cho phép: " & gListBuEmp, NotifyType.Error)
                            Exit Sub
                        End If

                        If _Value.HasValue Then
                            obj = New AT_LEAVESHEETDTO
                            obj.ID = _Value
                            obj.LEAVE_FROM = rdLeaveFrom.SelectedDate
                            obj.LEAVE_TO = rdLeaveTo.SelectedDate
                            obj.MANUAL_ID = cboKieuCong.SelectedValue
                            obj.NOTE_APP = txtnote.Text
                            obj.IS_WORKING_DAY = chkIsWorkingDay.Checked
                            obj.DAY_NUM = If(IsNumeric(txtDayNum.Text.ToString), Decimal.Parse(txtDayNum.Text.ToString), 0)
                            Dim employeeData = rgWorkschedule.MasterTableView.Items(0)
                            obj.EMPLOYEE_ID = employeeData.GetDataKeyValue("EMPLOYEE_ID")
                            obj.EMPLOYEE_CODE = employeeData.GetDataKeyValue("EMPLOYEE_CODE")
                            rep.ModifyLeaveSheet(obj, gstatus)
                        Else
                            obj = New AT_LEAVESHEETDTO
                            obj.LEAVE_FROM = rdLeaveFrom.SelectedDate
                            obj.LEAVE_TO = rdLeaveTo.SelectedDate
                            obj.MANUAL_ID = cboKieuCong.SelectedValue
                            obj.NOTE_APP = txtnote.Text
                            obj.IS_WORKING_DAY = chkIsWorkingDay.Checked
                            obj.DAY_NUM = If(IsNumeric(txtDayNum.Text.ToString), Decimal.Parse(txtDayNum.Text.ToString), 0)
                            rep.InsertLeaveSheetList(RegisterLeaveList, obj, gstatus)
                        End If

                        'Dim str As String = "getRadWindow().close('1');"
                        'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        ''POPUPTOLINK
                        Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlRegisterCO&group=Business")
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule')")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlRegisterCO&group=Business")
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien SelectedIndexChanged cho cboKieuCong
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboKieuCong_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboKieuCong.SelectedIndexChanged
        Dim dtData As AT_TIME_MANUALDTO
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not String.IsNullOrEmpty(cboKieuCong.SelectedValue) Then
                dtData = rep.GetAT_TIME_MANUALById(cboKieuCong.SelectedValue)
                cbosang.SelectedValue = dtData.MORNING_ID
                cboChieu.SelectedValue = dtData.AFTERNOON_ID
            End If
            'rdLeaveFrom_SelectedDateChanged(Nothing, Nothing)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cho rgWorkschedule
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgWorkschedule_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgWorkschedule.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If RegisterLeaveList() Is Nothing Then
                rgWorkschedule.VirtualItemCount = 0
                rgWorkschedule.DataSource = New List(Of String)
            Else
                'check employee exist in the rad gird
                If (rgWorkschedule.VirtualItemCount > 0) Then
                    For idx = 0 To rgWorkschedule.VirtualItemCount - 1
                        Dim item As GridDataItem = rgWorkschedule.Items(idx)
                        Dim at_item As List(Of AT_LEAVESHEETDTO) = RegisterLeaveList.Where(Function(s) s.EMPLOYEE_CODE = item.GetDataKeyValue("EMPLOYEE_CODE")).ToList()
                        If (at_item IsNot Nothing) Then
                            If (at_item.Count = 2) Then
                                RegisterLeaveList.Remove(at_item.Item(0))
                            End If
                        End If
                    Next
                End If

                rgWorkschedule.VirtualItemCount = RegisterLeaveList.Count
                rgWorkschedule.DataSource = RegisterLeaveList()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien ItemCommand cho rgWorkschedule
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgWorkschedule_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgWorkschedule.ItemCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case e.CommandName
                Case "FindEmployee"
                    isLoadPopup = 1
                    UpdateControlState()
                    ctrlFindEmployeePopup.Show()
                Case "DeleteEmployee"
                    For Each i As GridDataItem In rgWorkschedule.SelectedItems
                        Dim s = (From q In RegisterLeaveList() Where
                                 q.EMPLOYEE_ID = i.GetDataKeyValue("EMPLOYEE_ID")).FirstOrDefault
                        RegisterLeaveList.Remove(s)
                    Next
                    rgWorkschedule.Rebind()
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien ServerValidate cho cboKieuCong
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalKieuCong_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalKieuCong.ServerValidate
        Dim rep As New AttendanceRepository
        Dim validate As New AT_TIME_MANUALDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboKieuCong.SelectedValue
                validate.ACTFLG = "A"
                args.IsValid = rep.ValidateAT_TIME_MANUAL(validate)
            End If
            If Not args.IsValid Then
                ListComboData.GET_LIST_TYPE_MANUAL_LEAVE = True
                FillRadCombobox(cboKieuCong, ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE, "NAME_VN", "ID", True)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức update trạng thái của các control theo state
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = True
                        ctrlFindEmployeePopup.IsHideTerminate = False
                    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien EmployeeSelected cho ctrlFindEmployeePopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                If RegisterLeaveList Is Nothing Then
                    RegisterLeaveList = New List(Of AT_LEAVESHEETDTO)
                End If

                For idx = 0 To lstCommonEmployee.Count - 1
                    Dim item As New AT_LEAVESHEETDTO
                    item.EMPLOYEE_ID = lstCommonEmployee(idx).ID
                    RegisterLeaveList.Add(item)
                Next
                RegisterLeaveList = rep.GetPHEPBUCONLAI(RegisterLeaveList, Date.Now.Year)
                isLoadPopup = 0
            Else
                isLoadPopup = 0
            End If
            rgWorkschedule.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien load data cho cac combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ListComboData Is Nothing Then
                ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO
                ListComboData.GET_LIST_SIGN = True
                ListComboData.GET_LIST_TYPE_MANUAL_LEAVE = True
                rep.GetComboboxData(ListComboData)
            End If
            FillRadCombobox(cboKieuCong, ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE, "NAME_VN", "ID", True)
            FillRadCombobox(cbosang, ListComboData.LIST_LIST_SIGN, "NAME_VN", "ID", True)
            FillRadCombobox(cboChieu, ListComboData.LIST_LIST_SIGN, "NAME_VN", "ID", True)
            If ListComboData.LIST_LIST_SIGN.Count > 0 Then
                cbosang.SelectedIndex = 0
                cboChieu.SelectedIndex = 0
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

End Class


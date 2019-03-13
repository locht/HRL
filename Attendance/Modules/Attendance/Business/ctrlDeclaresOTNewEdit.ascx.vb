Imports Framework.UI
Imports Common
Imports Attendance.ctrlNewInOut
Imports Telerik.Web.UI
Imports Framework.UI.Utilities
Imports Attendance.AttendanceRepository
Imports Attendance.AttendanceBusiness
Imports Common.CommonBusiness
Imports WebAppLog

Public Class ctrlDeclaresOTNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Setting/" + Me.GetType().Name.ToString()
#Region "Property"
    Property ListComboData As Attendance.AttendanceBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Attendance.AttendanceBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
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
    Property Employee_Code As String
        Get
            Return ViewState(Me.ID & "_Employee_Code")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Employee_Code") = value
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
    Property RegisterOT As AT_REGISTER_OTDTO
        Get
            Return ViewState(Me.ID & "_objRegisterOT")
        End Get
        Set(ByVal value As AT_REGISTER_OTDTO)
            ViewState(Me.ID & "_objRegisterOT") = value
        End Set
    End Property
    Property RegisterOTList As List(Of AT_REGISTER_OTDTO)
        Get
            Return ViewState(Me.ID & "_objRegisterOTList")
        End Get
        Set(ByVal value As List(Of AT_REGISTER_OTDTO))
            ViewState(Me.ID & "_objRegisterOTList") = value
        End Set
    End Property
#End Region

#Region "Page"
    ''' <summary>
    ''' Khởi tạo, Load page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Refresh()
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Khởi tạo page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            SetGridFilter(rgWorkschedule)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgWorkschedule.AllowCustomPaging = True
            rgWorkschedule.ClientSettings.EnablePostBackOnRowClick = False
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Get data to combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            GetDataCombo()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Khởi tạo, load menu
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.MainToolBar = tbarOT
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Reload page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Try
            Message = Request.Params("VIEW")

            If Not IsPostBack Then
                If Request.Params("periodid") IsNot Nothing AndAlso Request.Params("periodid").ToString <> "" Then
                    Dim periodid = Request.Params("periodid")
                    Dim period = rep.LOAD_PERIODByID(New AT_PERIODDTO With {.PERIOD_ID = periodid})
                    'rdregisterDate.SelectedDate = period.START_DATE
                    rdregisterDate.MinDate = period.START_DATE
                    rdregisterDate.MaxDate = period.END_DATE
                End If
            End If
            Select Case Message
                Case "TRUE"
                    Dim obj As New AT_REGISTER_OTDTO
                    obj.ID = Decimal.Parse(Request.Params("ID"))
                    CurrentState = CommonMessage.STATE_EDIT
                    obj = rep.GetRegisterById(obj.ID)
                    If obj IsNot Nothing Then
                        rgWorkschedule.Enabled = False
                        RegisterOTList = New List(Of AT_REGISTER_OTDTO)
                        Dim item As New AT_REGISTER_OTDTO
                        item.EMPLOYEE_CODE = obj.EMPLOYEE_CODE
                        item.VN_FULLNAME = obj.VN_FULLNAME
                        item.TITLE_NAME = obj.TITLE_NAME
                        item.ORG_ID = obj.ORG_ID
                        item.ORG_NAME = obj.ORG_NAME
                        item.EMPLOYEE_ID = obj.EMPLOYEE_ID
                        RegisterOTList.Add(item)

                        Employee_id = obj.EMPLOYEE_ID
                        Employee_Code = obj.EMPLOYEE_CODE
                        rdregisterDate.SelectedDate = obj.WORKINGDAY
                        txtTuGio.SelectedDate = obj.FROM_HOUR
                        txtDenGio.SelectedDate = obj.TO_HOUR
                        txtGhiChu.Text = obj.NOTE
                        cbohs_ot.SelectedValue = obj.HS_OT
                        cboIs_nb.Checked = obj.IS_NB
                        txtBreak_Hour.Value = obj.BREAK_HOUR
                        'cbotypeot.SelectedValue = obj.TYPE_OT
                        _Value = obj.ID
                    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    ''' <summary>
    ''' Event Cancel popup ctrlFindEmployeePopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub
    ''' <summary>
    ''' Event Click menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim obj As AT_REGISTER_OTDTO
        Dim rep As New AttendanceRepository
        Dim gCODE As String = ""
        Dim gstatus As Integer = 0
        Dim sAction As String
        Dim lstEmp As New List(Of EmployeeDTO)
        Dim lstEmployeeCode As String = ""
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If RegisterOTList() Is Nothing Then
                        ShowMessage(Translate("Vui lòng chọn nhân viên cần đăng ký làm thêm!"), NotifyType.Error)
                        Exit Sub
                    End If
                    For index = 0 To RegisterOTList.Count - 1
                        lstEmp.Add(New EmployeeDTO With {.ID = RegisterOTList(index).EMPLOYEE_ID,
                                                    .EMPLOYEE_CODE = RegisterOTList(index).EMPLOYEE_CODE,
                                                    .FULLNAME_VN = RegisterOTList(index).VN_FULLNAME})
                    Next
                    If Not AttendanceRepositoryStatic.Instance.CheckPeriodClose(lstEmp.Select(Function(f) f.ID).ToList,
                                                                               rdregisterDate.SelectedDate, rdregisterDate.SelectedDate, sAction) Then
                        ShowMessage(Translate(sAction), NotifyType.Warning)
                        Exit Sub
                    End If
                    If _Value.HasValue Then
                        obj = New AT_REGISTER_OTDTO
                        obj.ID = _Value.Value
                        obj.EMPLOYEE_CODE = Employee_Code
                        obj.WORKINGDAY = rdregisterDate.SelectedDate
                        obj.FROM_HOUR = rdregisterDate.SelectedDate.Value.AddHours(Double.Parse(txtTuGio.SelectedTime.Value.TotalHours))
                        obj.TO_HOUR = rdregisterDate.SelectedDate.Value.AddHours(Double.Parse(txtDenGio.SelectedTime.Value.TotalHours))
                        If obj.TO_HOUR < obj.FROM_HOUR Then
                            obj.TO_HOUR = rdregisterDate.SelectedDate.Value.AddDays(1).AddHours(Double.Parse(txtDenGio.SelectedTime.Value.TotalHours))
                            obj.HOUR = Math.Round((rdregisterDate.SelectedDate.Value.AddDays(1).AddHours(Double.Parse(txtDenGio.SelectedTime.Value.TotalHours)) - rdregisterDate.SelectedDate.Value.AddHours(Double.Parse(txtTuGio.SelectedTime.Value.TotalHours))).TotalHours, 2)
                        Else
                            obj.TO_HOUR = rdregisterDate.SelectedDate.Value.AddHours(Double.Parse(txtDenGio.SelectedTime.Value.TotalHours))
                            obj.HOUR = Math.Round((rdregisterDate.SelectedDate.Value.AddHours(Double.Parse(txtDenGio.SelectedTime.Value.TotalHours)) - rdregisterDate.SelectedDate.Value.AddHours(Double.Parse(txtTuGio.SelectedTime.Value.TotalHours))).TotalHours, 2)
                        End If
                        If txtBreak_Hour.Value IsNot Nothing Then
                            obj.BREAK_HOUR = txtBreak_Hour.Value
                            obj.HOUR = obj.HOUR - obj.BREAK_HOUR
                        End If

                        If _Value <= 0 Then
                            obj.ORG_ID = Me.RegisterOT.ORG_ID
                        End If
                        obj.TYPE_INPUT = False
                        If cbohs_ot.SelectedValue Is Nothing Then
                            ShowMessage(Translate("Bạn chưa chọn hệ số làm thêm"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If cbohs_ot.SelectedValue = "" Then
                            ShowMessage(Translate("Bạn chưa chọn hệ số làm thêm"), NotifyType.Warning)
                            Exit Sub
                        End If
                        obj.HS_OT = Decimal.Parse(cbohs_ot.SelectedValue)
                        obj.IS_NB = cboIs_nb.Checked
                        obj.NOTE = txtGhiChu.Text
                        If rep.CheckImporAddNewtOT(obj) Then
                            If rep.ModifyRegisterOT(obj, gstatus) Then
                                'Dim str As String = "getRadWindow().close('1');"
                                'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                ''POPUPTOLINK
                                Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlDeclaresOT&group=Business")
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Success)
                            End If
                        Else
                            ShowMessage("Bạn đang đăng ký có thời gian bị trùng", NotifyType.Warning)
                            Exit Sub
                        End If

                    Else
                        obj = New AT_REGISTER_OTDTO
                        obj.WORKINGDAY = rdregisterDate.SelectedDate
                        obj.FROM_HOUR = rdregisterDate.SelectedDate.Value.AddHours(Double.Parse(txtTuGio.SelectedTime.Value.TotalHours))
                        obj.TO_HOUR = rdregisterDate.SelectedDate.Value.AddHours(Double.Parse(txtDenGio.SelectedTime.Value.TotalHours))
                        If obj.TO_HOUR < obj.FROM_HOUR Then
                            obj.TO_HOUR = rdregisterDate.SelectedDate.Value.AddDays(1).AddHours(Double.Parse(txtDenGio.SelectedTime.Value.TotalHours))
                            obj.HOUR = Math.Round((rdregisterDate.SelectedDate.Value.AddDays(1).AddHours(Double.Parse(txtDenGio.SelectedTime.Value.TotalHours)) - rdregisterDate.SelectedDate.Value.AddHours(Double.Parse(txtTuGio.SelectedTime.Value.TotalHours))).TotalHours, 2)
                        Else
                            obj.TO_HOUR = rdregisterDate.SelectedDate.Value.AddHours(Double.Parse(txtDenGio.SelectedTime.Value.TotalHours))
                            obj.HOUR = Math.Round((rdregisterDate.SelectedDate.Value.AddHours(Double.Parse(txtDenGio.SelectedTime.Value.TotalHours)) - rdregisterDate.SelectedDate.Value.AddHours(Double.Parse(txtTuGio.SelectedTime.Value.TotalHours))).TotalHours, 2)
                        End If
                        If _Value <= 0 Then
                            obj.ORG_ID = Me.RegisterOT.ORG_ID
                        End If
                        obj.TYPE_INPUT = False
                        If txtBreak_Hour.Value IsNot Nothing Then
                            obj.BREAK_HOUR = txtBreak_Hour.Value
                            obj.HOUR = obj.HOUR - obj.BREAK_HOUR
                        End If
                        If cbohs_ot.SelectedValue Is Nothing Then
                            ShowMessage(Translate("Bạn chưa chọn hệ số làm thêm"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If cbohs_ot.SelectedValue = "" Then
                            ShowMessage(Translate("Bạn chưa chọn hệ số làm thêm"), NotifyType.Warning)
                            Exit Sub
                        End If
                        obj.HS_OT = Decimal.Parse(cbohs_ot.SelectedValue)
                        obj.IS_NB = cboIs_nb.Checked
                        'obj.TYPE_OT = Decimal.Parse(cbotypeot.SelectedValue)
                        obj.NOTE = txtGhiChu.Text
                        If rep.CheckDataListImportAddNew(RegisterOTList, obj, lstEmployeeCode) Then
                            If rep.InsertDataRegisterOT(RegisterOTList, obj, gstatus) Then
                                'Dim str As String = "getRadWindow().close('1');"
                                'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                ''POPUPTOLINK
                                Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlDeclaresOT&group=Business")
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Success)
                            End If
                        Else
                            ShowMessage("Nhân viên có thời gian đăng ký bị ghi đè:" & lstEmployeeCode, NotifyType.Warning)
                            Exit Sub
                        End If

                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlDeclaresOT&group=Business")
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Reload grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgWorkschedule_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgWorkschedule.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If RegisterOTList() Is Nothing Then
                rgWorkschedule.VirtualItemCount = 0
                rgWorkschedule.DataSource = New List(Of String)
            Else
                rgWorkschedule.VirtualItemCount = RegisterOTList.Count
                rgWorkschedule.DataSource = RegisterOTList()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgWorkschedule_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgWorkschedule.ItemCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case e.CommandName
                Case "FindEmployee"
                    isLoadPopup = 1
                    UpdateControlState()
                    ctrlFindEmployeePopup.Show()
                Case "DeleteEmployee"
                    For Each i As GridDataItem In rgWorkschedule.SelectedItems
                        Dim s = (From q In RegisterOTList Where
                                 q.EMPLOYEE_ID = i.GetDataKeyValue("EMPLOYEE_ID")).FirstOrDefault
                        RegisterOTList.Remove(s)
                    Next
                    rgWorkschedule.Rebind()
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"
    ''' <summary>
    ''' Load popup
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Try
                Select Case isLoadPopup
                    Case 1
                        If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                            phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                            ctrlFindEmployeePopup.MultiSelect = True
                            ctrlFindEmployeePopup.IsHideTerminate = False

                        End If
                End Select
            Catch ex As Exception
                Throw ex
            End Try
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Event Select nhân viên trên popup nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New AttendanceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                If RegisterOTList Is Nothing Then
                    RegisterOTList = New List(Of AT_REGISTER_OTDTO)
                End If
                'RegisterOTList = New List(Of AT_REGISTER_OTDTO)
                For idx = 0 To lstCommonEmployee.Count - 1
                    If CheckExistEmployee(lstCommonEmployee(idx).ID) Then
                        Dim item As New AT_REGISTER_OTDTO
                        item.EMPLOYEE_CODE = lstCommonEmployee(idx).EMPLOYEE_CODE
                        item.VN_FULLNAME = lstCommonEmployee(idx).FULLNAME_VN
                        item.TITLE_NAME = lstCommonEmployee(idx).TITLE_NAME
                        item.ORG_ID = lstCommonEmployee(idx).ORG_ID
                        item.ORG_NAME = lstCommonEmployee(idx).ORG_NAME
                        item.EMPLOYEE_ID = lstCommonEmployee(idx).ID
                        RegisterOTList.Add(item)
                    End If
                Next
                isLoadPopup = 0
            Else
                isLoadPopup = 0
            End If
            rgWorkschedule.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Function CheckExistEmployee(ByVal value As Decimal) As Boolean
        For Each r As AT_REGISTER_OTDTO In RegisterOTList
            If r.EMPLOYEE_ID = value Then
                Return False
            End If
        Next
        Return True
    End Function
    ''' <summary>
    ''' Get data cho combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Try
            If ListComboData Is Nothing Then
                ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO
                ListComboData.GET_LIST_HS_OT = True
                ListComboData.GET_LIST_TYPE_OT = True
                rep.GetComboboxData(ListComboData)
            End If
            FillRadCombobox(cbohs_ot, ListComboData.LIST_LIST_HS_OT, "CODE", "ID", True)
            If ListComboData.LIST_LIST_HS_OT.Count > 0 Then
                cbohs_ot.SelectedIndex = 0
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region
End Class


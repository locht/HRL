Imports Common
'Imports Ionic.Zip
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Attendance.AttendanceBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Imports Aspose.Words
Imports Aspose.Words.Reporting
Imports System.IO
Imports HistaffFrameworkPublic
Imports Common.CommonBusiness

Public Class ctrlAtShiftNewEdit
    Inherits CommonView

    ''' <summary>
    ''' ctrl FindEmployeePopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindEmployee2GridPopup As ctrlFindEmployee2GridPopup
    Public Overrides Property MustAuthorize As Boolean = False
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String

    Private Property dtLogs As DataTable
        Get
            Return PageViewState(Me.ID & "_dtLogs")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtLogs") = value
        End Set
    End Property


    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

#Region "Property"


    ''' <summary>
    ''' ListComboData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListComboData As Attendance.AttendanceBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Attendance.AttendanceBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    ''' <summary>
    ''' AtShift
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AtShiftSign As AtPortalRegistrationShiftDTO
        Get
            Return ViewState(Me.ID & "_AtShiftSign")
        End Get
        Set(ByVal value As AtPortalRegistrationShiftDTO)
            ViewState(Me.ID & "_AtShiftSign") = value
        End Set
    End Property

    ''' <summary>
    ''' isLoadPopup
    ''' </summary>
    ''' <value></value>
    ''' <returns>
    ''' 0 - normal
    ''' 1 - Employee
    ''' 2 - Org
    ''' 3 - Sign
    ''' </returns>
    ''' <remarks></remarks>
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    ''' <summary>
    ''' IDSelect
    ''' </summary>
    ''' <remarks></remarks>
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property


    Public Property Employee_list As List(Of EmployeeDTO)
        Get
            Return PageViewState(Me.ID & "_Employee_list")
        End Get
        Set(value As List(Of EmployeeDTO))
            PageViewState(Me.ID & "_Employee_list") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            GetParams()
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            InitControl()
            
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Fill du lieu len combobox</summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            GetDataCombo()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Me.MainToolBar = tbarMain

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Seperator,
                                       ToolbarItem.Cancel)

            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New AttendanceRepository
        Try
            Select Case Message
                Case "UpdateView"
                    Dim AtShift As New AtPortalRegistrationShiftDTO
                    AtShift = rep.GetRegShiftByID(IDSelect)
                    CurrentState = CommonMessage.STATE_EDIT

                    If IsNumeric(AtShift.SHIFT_ID) Then
                        cboShift.SelectedValue = AtShift.SHIFT_ID
                        cboShift.Text = AtShift.SHIFT_CODE
                    End If
                    If IsDate(AtShift.DATE_FROM) Then
                        rdDateFrom.SelectedDate = AtShift.DATE_FROM
                    End If
                    If IsDate(AtShift.DATE_TO) Then
                        rdDateTo.SelectedDate = AtShift.DATE_TO
                    End If
                    txtReason.Text = AtShift.REASON
                    Employee_list = AtShift.EMPLOYEE

                    rgEmployee.Rebind()
                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
            End Select
            'rep.Dispose()
            rgEmployee.Rebind()


            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary> 
    ''' Event click item tren menu toolbar
    ''' Set trang thai control sau khi process xong 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objShift As New AtPortalRegistrationShiftDTO
        Dim rep As New AttendanceRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim store As New AttendanceStoreProcedure
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If rgEmployee.Items.Count = 0 Then
                            ShowMessage(Translate("Chưa chọn nhân viên, kiểm tra lại!!"), NotifyType.Warning)
                            Exit Sub
                        End If
                        For Each item As GridDataItem In rgEmployee.Items
                            Dim emp_id As Decimal = CDec(item.GetDataKeyValue("ID"))
                            Dim _param = New Attendance.AttendanceBusiness.ParamDTO With {.ORG_ID = item.GetDataKeyValue("ORG_ID"),
                                            .PERIOD_ID = Decimal.Parse(rep.GET_PERIOD(rdDateFrom.SelectedDate))}

                            If Not rep.IS_PERIODSTATUS(_param) Then
                                ShowMessage(Translate("Kỳ công của nhân viên " & item.GetDataKeyValue("EMPLOYEE_CODE") & " đã đóng !!"), NotifyType.Warning)
                                Exit Sub
                            End If
                            If store.CHECK_EXIST_SHIFT(emp_id, IDSelect, rdDateFrom.SelectedDate, rdDateTo.SelectedDate) > 0 Then
                                ShowMessage(Translate("Nhân viên " & item.GetDataKeyValue("EMPLOYEE_CODE") & " tồn tại dữ liệu ca làm việc trong khoảng thời gian này !!"), NotifyType.Warning)
                                Exit Sub
                            End If
                        Next
                        objShift.SHIFT_ID = cboShift.SelectedValue
                        objShift.DATE_TO = rdDateTo.SelectedDate
                        objShift.DATE_FROM = rdDateFrom.SelectedDate
                        objShift.REASON = txtReason.Text
                        objShift.CREATED_BY = LogHelper.CurrentUser.EMPLOYEE_ID
                        objShift.CREATED_DATE = DateTime.Now
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                For Each item As GridDataItem In rgEmployee.Items
                                    objShift.EMPLOYEE_ID = item.GetDataKeyValue("ID")
                                    rep.AddAtShift(objShift)
                                Next
                            Case CommonMessage.STATE_EDIT
                                Dim lst_id As New List(Of Decimal)
                                lst_id.Add(IDSelect)
                                rep.DeleteAtShift(lst_id)
                                For Each item As GridDataItem In rgEmployee.Items
                                    objShift.EMPLOYEE_ID = item.GetDataKeyValue("ID")
                                    rep.AddAtShift(objShift)
                                Next
                        End Select

                        'Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlPortalAtRegistrationShift")
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "close", "getRadWindow().close(1);", True)
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlPortalAtRegistrationShift")
            End Select
            'rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện khi 1 Employee được chọn ở popup FindEmployee</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployee2GridPopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployee2GridPopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New ProfileBusinessRepository
        'Dim repNew As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            lstCommonEmployee = CType(ctrlFindEmployee2GridPopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count > 0 Then
                Dim item = lstCommonEmployee(0)
                If Employee_list Is Nothing Then
                    Employee_list = New List(Of EmployeeDTO)
                End If
                'If cboCommendObj.SelectedValue = 388 Then ' Ca nhan
                '    Employee_Commend.Clear()
                'End If
                For Each emp As CommonBusiness.EmployeePopupFindDTO In lstCommonEmployee
                    If Employee_list.Any(Function(f) f.EMPLOYEE_CODE = emp.EMPLOYEE_CODE) Then
                        ShowMessage(Translate("Nhân viên đã tồn tại."), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim employee As New EmployeeDTO
                    employee.ID = emp.ID
                    employee.EMPLOYEE_CODE = emp.EMPLOYEE_CODE
                    employee.FULLNAME_VN = emp.FULLNAME_VN
                    employee.ORG_NAME = emp.ORG_NAME
                    employee.TITLE_NAME_VN = emp.TITLE_NAME
                    employee.ORG_ID = emp.ORG_ID
                    employee.TITLE_ID = emp.TITLE_ID
                    Employee_list.Add(employee)
                Next

                rgEmployee.Rebind()

                For Each i As GridItem In rgEmployee.Items
                    i.Edit = True
                Next

                rgEmployee.Rebind()

            End If
            Session.Remove("PortalAtShift")
            'rep.Dispose()
            isLoadPopup = 0

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện cho nút hủy của popup</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployee2GridPopup.CancelClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            isLoadPopup = 0
            Session.Remove("PortalAtShift")
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Load data cho grid Employee</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgEmployee_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEmployee.NeedDataSource
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            rgEmployee.DataSource = Employee_list
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện ItemCommand cho grid Employee</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgEmployee_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rgEmployee.ItemCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            Select Case e.CommandName

                Case "FindEmployee"
                    isLoadPopup = 1
                    UpdateControlState()
                    ctrlFindEmployee2GridPopup.MultiSelect = True
                    ctrlFindEmployee2GridPopup.Show()

                Case "DeleteEmployee"
                    For Each i As GridDataItem In rgEmployee.SelectedItems
                        Dim s = (From q In Employee_list Where
                                 q.ID = i.GetDataKeyValue("ID")).FirstOrDefault
                        Employee_list.Remove(s)
                    Next
                    rgEmployee.Rebind()
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Cập nhật trạn thái của control</summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If FindEmployee.Controls.Contains(ctrlFindEmployee2GridPopup) Then
                FindEmployee.Controls.Remove(ctrlFindEmployee2GridPopup)
                'Me.Views.Remove(ctrlFindEmployee2GridPopup.ID.ToUpper)
            End If

            Select Case isLoadPopup
                Case 1
                    HttpContext.Current.Session("PortalAtShift") = LogHelper.CurrentUser.EMPLOYEE_ID
                    ctrlFindEmployee2GridPopup = Me.Register("ctrlFindEmployee2GridPopup", "Common", "ctrlFindEmployee2GridPopup")
                    ctrlFindEmployee2GridPopup.NotIs_Load_CtrlOrg = True
                    FindEmployee.Controls.Add(ctrlFindEmployee2GridPopup)
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Load dữ liệu cho combobox</summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim store As New AttendanceStoreProcedure
        Dim dtdata As New DataTable

        Try
            dtdata = store.GET_PORTAL_SHIFT()
            FillRadCombobox(cboShift, dtdata, "NAME_VN", "ID")
            'If ListComboData Is Nothing Then
            '    ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO
            '    ListComboData.GET_LIST_SHIFT = True
            '    rep.GetComboboxData(ListComboData)
            'End If
            'FillDropDownList(cboShift, ListComboData.LIST_LIST_SHIFT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboShift.SelectedValue)
            'rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Get parameters</summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    IDSelect = If(IsNumeric(Request.Params("ID")), CDec(Request.Params("ID")), 0)
                End If

                If IDSelect <> 0 Then
                    Refresh("UpdateView")
                Else
                    Refresh("InsertView")
                    If Employee_list Is Nothing Then
                        Employee_list = New List(Of EmployeeDTO)
                    End If
                End If

            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Đổ dữ liệu lên grid</summary>
    ''' <remarks></remarks>
    Private Sub FilldataGridView(ByVal id As Decimal)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            'Using rep As New ProfileBusinessRepository
            '    If id = 0 Then
            '        Employee_Commend = rep.GetEmployeeCommendByID(Utilities.ObjToDecima(IDSelect))
            '    Else
            '        List_Org = rep.GetOrgCommendByID(Utilities.ObjToDecima(IDSelect))
            '    End If
            'End Using

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region



End Class
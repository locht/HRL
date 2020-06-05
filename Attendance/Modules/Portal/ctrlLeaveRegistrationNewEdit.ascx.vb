Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Common.Common
Imports Common.CommonBusiness
Imports Attendance.AttendanceBusiness
Imports Telerik.Web.UI
Imports HistaffFrameworkPublic.HistaffFrameworkEnum
Imports System.IO
Imports Telerik.Web.UI.Calendar
Imports Profile.ProfileBusiness
Imports WebAppLog

Public Class ctrlLeaveRegistrationNewEdit
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False
    Dim psp As New AttendanceStoreProcedure
    Protected WithEvents ctrlFindEmployee2GridPopup As ctrlFindEmployee2GridPopup
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
#Region "Property"
    Public Property IDCtrl As String
        Get
            Return ViewState(Me.ID & "_IDCtrl")
        End Get
        Set(value As String)
            ViewState(Me.ID & "_IDCtrl") = value
        End Set
    End Property
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
    'EmployeeID
    Property EmployeeID As Decimal
        Get
            Return ViewState(Me.ID & "_EmployeeID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_EmployeeID") = value
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

    Property dtDetail As DataTable
        Get
            Return ViewState(Me.ID & "_dtDetail")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDetail") = value
        End Set
    End Property
    Property rPH As DataRow
        Get
            Return ViewState(Me.ID & "_rPH")
        End Get
        Set(ByVal value As DataRow)
            ViewState(Me.ID & "_rPH") = value
        End Set
    End Property
    Property isFlag As Boolean
        Get
            Return ViewState(Me.ID & "_isFlag")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isFlag") = value
        End Set
    End Property

    ''' <summary>
    ''' isLoadPopup
    ''' </summary>
    ''' <value></value>
    ''' <returns>
    ''' 0 - normal
    ''' 1 - Employee
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

    Public Property Employee_list As List(Of CommonBusiness.EmployeeDTO)
        Get
            Return PageViewState(Me.ID & "_Employee_list")
        End Get
        Set(value As List(Of CommonBusiness.EmployeeDTO))
            PageViewState(Me.ID & "_Employee_list") = value
        End Set
    End Property

    Public Property dtLeaveType As DataTable
        Get
            Return PageViewState(Me.ID & "_dtLeaveType")
        End Get
        Set(value As DataTable)
            PageViewState(Me.ID & "_dtLeaveType") = value
        End Set
    End Property

    Public Property dtLeaveSession As DataTable
        Get
            Return PageViewState(Me.ID & "_dtLeaveSession")
        End Get
        Set(value As DataTable)
            PageViewState(Me.ID & "_dtLeaveSession") = value
        End Set
    End Property

    Property gID As Decimal
        Get
            Return ViewState(Me.ID & "_gID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_gID") = value
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
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            InitControl()
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()
            Refresh()
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
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep2 As New CommonRepository
        Dim dsLeaveSheet As New DataSet()
        Try
            'If IsPostBack Then
            '    rtEmployee_id.Text = LogHelper.CurrentUser.EMPLOYEE_ID
            '    Using rep As New AttendanceRepository
            '        EmployeeDto = rep.GetEmployeeInfor(EmployeeID, Nothing)
            '    End Using
            'End If
            If Not IsPostBack Then


                Dim startTime As DateTime = DateTime.UtcNow
                IDCtrl = Request.Params("idCtrl")
                Message = Request.Params("VIEW")
                Dim Struct As Decimal = 1
                Dim ID_PH As Decimal = 0
                If IsNumeric(Request.Params("ID")) Then
                    Struct = 0
                    ID_PH = Decimal.Parse(Request.Params("ID"))
                    If Decimal.Parse(Request.Params("ID")) = 0 Then
                        If Employee_list Is Nothing Then
                            Employee_list = New List(Of CommonBusiness.EmployeeDTO)
                        End If
                        CurrentState = CommonMessage.STATE_NEW
                    Else
                        CurrentState = CommonMessage.STATE_EDIT
                    End If
                End If
                Using rep As New AttendanceRepository
                    dsLeaveSheet = rep.GetLeaveSheet_ById(ID_PH, Struct)
                End Using
                If dsLeaveSheet.Tables(0).Rows.Count > 0 AndAlso IsNumeric(dsLeaveSheet.Tables(0).Rows(0)("EMPLOYEE_ID")) Then
                    Employee_list = New List(Of CommonBusiness.EmployeeDTO)
                    Dim lst_emp As New List(Of Decimal)
                    lst_emp.Add(dsLeaveSheet.Tables(0).Rows(0)("EMPLOYEE_ID"))
                    Dim emp = rep2.GetEmployeeToPopupFind_EmployeeID(lst_emp)
                    Dim employee As New CommonBusiness.EmployeeDTO
                    employee.ID = emp.Item(0).ID
                    employee.EMPLOYEE_CODE = emp.Item(0).EMPLOYEE_CODE
                    employee.FULLNAME_VN = emp.Item(0).FULLNAME_VN
                    employee.ORG_NAME = emp.Item(0).ORG_NAME
                    employee.TITLE_NAME_VN = emp.Item(0).TITLE_NAME
                    employee.ORG_ID = emp.Item(0).ORG_ID
                    employee.TITLE_ID = emp.Item(0).TITLE_ID
                    employee.EMPLOYEE_OBJECT = emp.Item(0).EMPLOYEE_OBJECT
                    employee.EMPLOYEE_OBJECT_CODE = emp.Item(0).EMPLOYEE_OBJECT_CODE
                    employee.EMPLOYEE_OBJECT_NAME = emp.Item(0).EMPLOYEE_OBJECT_NAME
                    Dim dtData As New DataTable
                    Using rep As New AttendanceRepository
                        dtData = rep.GET_INFO_PHEPNAM(emp.Item(0).ID, dsLeaveSheet.Tables(0).Rows(0)("LEAVE_FROM"))
                        If dtData.Rows.Count > 0 Then
                            employee.EMP_ENTITLEMENT = If(dtData.Rows(0)("PHEP_CONLAI") Is Nothing, "0", dtData.Rows(0)("PHEP_CONLAI").ToString())
                        End If
                    End Using
                    Employee_list.Add(employee)
                End If
                If dsLeaveSheet IsNot Nothing Then
                    If dsLeaveSheet.Tables(0) IsNot Nothing Then
                        rPH = dsLeaveSheet.Tables(0).NewRow
                        If dsLeaveSheet.Tables(0).Rows.Count > 0 Then
                            rPH = dsLeaveSheet.Tables(0).Rows(0)
                        End If
                    End If
                    If dsLeaveSheet.Tables(1) IsNot Nothing AndAlso dtDetail Is Nothing Then
                        dtDetail = dsLeaveSheet.Tables(1).Clone()
                        dtDetail = dsLeaveSheet.Tables(1)
                    End If
                End If
                Select Case Message
                    Case "TRUE"
                        CreateDataBinDing(1)
                End Select
            End If
        Catch ex As Exception
            Throw ex
        Finally
            dsLeaveSheet.Dispose()
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Try
            If Utilities.ObjToString(rPH("S_CODE")) = "R" Or Utilities.ObjToString(rPH("S_CODE")) = "U" Or Utilities.ObjToString(rPH("S_CODE")) = "" Or Utilities.ObjToString(rPH("S_CODE")) = "W" Or Utilities.ObjToString(rPH("S_CODE")) = "RST" Then
                RadPane1.Enabled = True
                CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
            Else
                RadPane1.Enabled = False
                CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
            End If
            Select Case isLoadPopup
                Case 1
                    HttpContext.Current.Session("PortalAtShift") = LogHelper.CurrentUser.EMPLOYEE_ID
                    ctrlFindEmployee2GridPopup = Me.Register("ctrlFindEmployee2GridPopup", "Common", "ctrlFindEmployee2GridPopup")
                    ctrlFindEmployee2GridPopup.NotIs_Load_CtrlOrg = True
                    FindEmployee.Controls.Add(ctrlFindEmployee2GridPopup)
            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    'Protected Sub cbSTATUS_SHIFT_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs)
    '    Try
    '        Dim edit = CType(sender, RadComboBox)
    '        If edit.Enabled = False Then
    '            Exit Sub
    '        End If
    '        Dim item = CType(edit.NamingContainer, GridEditableItem)
    '        ' If Not IsNumeric(edit.SelectedValue) Then Exit Sub
    '        Dim EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")
    '        Dim LEAVE_DAY = item.GetDataKeyValue("LEAVE_DAY")
    '        For Each rows In dtDetail.Rows
    '            If rows("LEAVE_DAY") = LEAVE_DAY Then
    '                rows("STATUS_SHIFT") = If(IsNumeric(edit.SelectedValue), edit.SelectedValue, 0)
    '                If edit.SelectedValue IsNot Nothing AndAlso edit.SelectedValue <> "" Then
    '                    'rows("DAY_NUM") = 0.5
    '                    rows("DAY_NUM") = Decimal.Parse(rows("SHIFT_DAY")) / 2
    '                Else
    '                    'rows("DAY_NUM") = 1
    '                    rows("DAY_NUM") = Decimal.Parse(rows("SHIFT_DAY"))
    '                End If
    '                Exit For
    '            End If
    '        Next
    '        rgData.Rebind()
    '        For Each items As GridDataItem In rgData.MasterTableView.Items
    '            items.Edit = True
    '        Next
    '        rgData.MasterTableView.Rebind()
    '        Cal_DayLeaveSheet()

    '        ScriptManager.RegisterStartupScript(Me.Page, Page.GetType(), "text", "IsBlock()", True)
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New AttendanceRepository
        Dim store As New AttendanceStoreProcedure
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim objValidate As New AT_LEAVESHEETDTO
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If Utilities.ObjToString(rPH("S_CODE")) = "A" Then 'TRANG THAI approve
                            ShowMessage(Translate("Đơn đã Phê duyệt. Không thể chỉnh sửa !"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If rgEmployee.Items.Count = 0 Then
                            ShowMessage(Translate("Chưa chọn nhân viên, kiểm tra lại!!"), NotifyType.Warning)
                            Exit Sub
                        End If

                        If Not IsNumeric(rnDAY_NUM.Value) OrElse rnDAY_NUM.Value <= 0 Then
                            ShowMessage(Translate("Số ngày đăng ký nghỉ phải lơn hơn 0"), NotifyType.Warning)
                            Exit Sub
                        End If
                        Dim type As String = (From p In dtLeaveType Where p("ID") = cboMANUAL_ID.SelectedValue Select p("CODE")).FirstOrDefault

                        objValidate.LEAVE_FROM = rdLEAVE_FROM.SelectedDate
                        objValidate.LEAVE_TO = rdLEAVE_TO.SelectedDate
                        objValidate.FROM_SESSION = cboFROM_SESSION.SelectedValue
                        objValidate.TO_SESSION = cboTO_SESSION.SelectedValue
                        objValidate.ID = Utilities.ObjToDecima(rPH("ID"))
                        Dim id As Decimal = 0
                        For Each item As GridDataItem In rgEmployee.Items
                            Dim emp_id As Decimal = CDec(item.GetDataKeyValue("ID"))
                            Dim _param = New Attendance.AttendanceBusiness.ParamDTO With {.ORG_ID = item.GetDataKeyValue("ORG_ID"),
                                            .PERIOD_ID = Decimal.Parse(rep.GET_PERIOD(rdLEAVE_FROM.SelectedDate))}

                            If Not rep.IS_PERIODSTATUS(_param) Then
                                ShowMessage(Translate("Kỳ công của nhân viên " & item.GetDataKeyValue("EMPLOYEE_CODE") & " đã đóng !!"), NotifyType.Warning)
                                Exit Sub
                            End If

                            If type = "F" AndAlso item.GetDataKeyValue("EMP_ENTITLEMENT") < rnDAY_NUM.Value Then
                                ShowMessage(Translate("Số ngày đăng ký của nhân viên " & item.GetDataKeyValue("EMPLOYEE_CODE") & " lơn hơn số phép năm còn lại !!"), NotifyType.Warning)
                                Exit Sub
                            End If
                            If (New AttendanceBusinessClient).ValidateLeaveSheetDetail(objValidate) = False Then
                                ShowMessage(Translate("Ngày đăng ký nghỉ của nhân viên " & item.GetDataKeyValue("EMPLOYEE_CODE") & " đã bị trùng"), NotifyType.Warning)
                                Exit Sub
                            End If
                        Next
                        'check so ngay dang ky nghi


                        CreateDataBinDing(0)
                        If CurrentState = CommonMessage.STATE_NEW Then
                            rPH("CREATED_BY_EMP") = LogHelper.CurrentUser.EMPLOYEE_ID
                            rPH("CREATED_BY") = LogHelper.CurrentUser.FULLNAME
                        End If
                        rPH("IS_APP") = -1
                        rPH("STATUS") = 0
                        rPH("MODIFIED_BY_EMP") = LogHelper.CurrentUser.EMPLOYEE_ID
                        rPH("MODIFIED_BY") = LogHelper.CurrentUser.FULLNAME
                        rPH("MODIFIED_DATE") = DateTime.Now
                        rPH("MODIFIED_LOG") = LogHelper.GetUserLog.Ip + "\" + LogHelper.GetUserLog.ComputerName
                        Dim lst_delete As New List(Of AT_LEAVESHEETDTO)
                        If Not IsDBNull(rPH("ID")) Then
                            Dim at_leave As New AT_LEAVESHEETDTO
                            at_leave.ID = rPH("ID")
                            lst_delete.Add(at_leave)
                            rep.DeleteLeaveSheet(lst_delete)
                        End If
                        For Each item As GridDataItem In rgEmployee.Items
                            rPH("EMPLOYEE_ID") = item.GetDataKeyValue("ID")
                            rPH("ID") = 0
                            Dim emp_id As Decimal = CDec(item.GetDataKeyValue("ID"))
                            GetLeaveSheet_Detail(emp_id)
                            SaveDB()
                            Dim objATL As New AT_LEAVESHEETDTO
                            SetData_Controls(objATL, 0, item.GetDataKeyValue("ID"))
                            Dim objSend As New AtSendApproveLetterDTO
                            objSend.LEAVE_ID = gID
                            objSend.USERNAME = LogHelper.GetUserLog.Username.ToUpper
                            objSend.ACTION_DATE = DateTime.Now
                            objSend.ACTION_STATUS = 0
                            rep.InsertSendLetter(objSend)
                        Next
                        Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlLeaveRegistration")
                        'If valSum.Page.IsValid Then

                        'If SaveDB() Then
                        '    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlLeaveRegistration")
                        'Else
                        '    ShowMessage(Translate("Xảy ra lỗi"), NotifyType.Error)
                        'End If
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule')")
                    End If
                Case CommonMessage.TOOLBARITEM_SUBMIT

                    If rdLEAVE_FROM.SelectedDate IsNot Nothing Then
                        If IS_PERIOD_CLOSE(rdLEAVE_FROM.SelectedDate) = False Then
                            ShowMessage(Translate("Kì công đã đóng, xin kiểm tra lại !"), NotifyType.Warning)
                            Exit Sub
                        End If
                    End If
                    If rdLEAVE_TO.SelectedDate IsNot Nothing Then
                        If IS_PERIOD_CLOSE(rdLEAVE_TO.SelectedDate) = False Then
                            ShowMessage(Translate("Kì công đã đóng, xin kiểm tra lại !"), NotifyType.Warning)
                            Exit Sub
                        End If
                    End If

                    If Utilities.ObjToString(rPH("S_CODE")) = "R" Then
                        ctrlMessageBox.MessageText = Translate("Bạn có muốn gửi phê duyệt?")
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_SUBMIT
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else
                        ShowMessage(Translate("Chỉ gửi đơn ở trạng thái Chưa gửi duyệt. Vui lòng thử lại !"), NotifyType.Warning)
                        Exit Sub
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=" + IDCtrl)
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim objValidate As New AT_LEAVESHEETDTO
        Try
            'If e.ActionName = CommonMessage.TOOLBARITEM_SUBMIT And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            '    SetData_Controls(objValidate, 0)
            'End If
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

            ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", "showDetail('block');", True)
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub rgData_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles rgData.ItemDataBound
    '    Dim cbo As New RadComboBox
    '    Dim arr As New ArrayList()
    '    Try
    '        If e.Item.Edit Then
    '            Dim edit = CType(e.Item, GridEditableItem)
    '            Dim item As GridDataItem = CType(e.Item, GridDataItem)
    '            cbo = CType(edit.FindControl("cbSTATUS_SHIFT"), RadComboBox)
    '            arr.Add(New DictionaryEntry("", Nothing))
    '            arr.Add(New DictionaryEntry("Đầu ca", 1))
    '            arr.Add(New DictionaryEntry("Cuối ca", 2))
    '            With cbo
    '                .DataSource = arr
    '                .DataValueField = "Value"
    '                .DataTextField = "Key"
    '                cbo.DataBind()
    '                .SelectedIndex = 0
    '            End With
    '            SetDataToGrid(edit)
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        cbo.Dispose()
    '        arr = Nothing
    '    End Try
    'End Sub

    Private Sub rgData_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgData.ItemCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case e.CommandName
                Case "EditDetail"
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
        Dim dtSourceEntitlement As New DataTable()

        Try
            lstCommonEmployee = CType(ctrlFindEmployee2GridPopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count > 0 Then
                Dim item = lstCommonEmployee(0)
                If Employee_list Is Nothing Then
                    Employee_list = New List(Of CommonBusiness.EmployeeDTO)
                End If
                'If cboCommendObj.SelectedValue = 388 Then ' Ca nhan
                '    Employee_Commend.Clear()
                'End If
                For Each emp As CommonBusiness.EmployeePopupFindDTO In lstCommonEmployee
                    If Employee_list.Any(Function(f) f.EMPLOYEE_CODE = emp.EMPLOYEE_CODE) Then
                        ShowMessage(Translate("Nhân viên đã tồn tại."), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim employee As New CommonBusiness.EmployeeDTO
                    employee.ID = emp.ID
                    employee.EMPLOYEE_CODE = emp.EMPLOYEE_CODE
                    employee.FULLNAME_VN = emp.FULLNAME_VN
                    employee.ORG_NAME = emp.ORG_NAME
                    employee.TITLE_NAME_VN = emp.TITLE_NAME
                    employee.ORG_ID = emp.ORG_ID
                    employee.TITLE_ID = emp.TITLE_ID
                    employee.EMPLOYEE_OBJECT = emp.EMPLOYEE_OBJECT
                    employee.EMPLOYEE_OBJECT_CODE = emp.EMPLOYEE_OBJECT_CODE
                    employee.EMPLOYEE_OBJECT_NAME = emp.EMPLOYEE_OBJECT_NAME
                    Using rep As New AttendanceRepository
                        dtSourceEntitlement = rep.GET_INFO_PHEPNAM(emp.ID, If(IsNothing(rdLEAVE_FROM.SelectedDate), DateTime.Now, rdLEAVE_FROM.SelectedDate))
                        If dtSourceEntitlement.Rows.Count > 0 Then
                            employee.EMP_ENTITLEMENT = If(dtSourceEntitlement.Rows(0)("PHEP_CONLAI") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CONLAI").ToString())
                        End If
                    End Using
                    Employee_list.Add(employee)
                Next
                'rgEmployee.Rebind()

                'For Each i As GridItem In rgEmployee.Items
                '    i.Edit = True
                'Next

                rgEmployee.Rebind()
                RefreshCboSession()
                Cal_DayLeaveSheet()
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
            Dim count = 0
            For Each item In Employee_list
                If item.EMPLOYEE_OBJECT_CODE <> "HC1" And item.EMPLOYEE_OBJECT_CODE <> "HC2" Then
                    cboFROM_SESSION.Enabled = False
                    cboTO_SESSION.Enabled = False
                    Exit For
                Else
                    count += 1
                End If
            Next
            If cboMANUAL_ID.SelectedValue <> "" Then
                Dim day_half As Decimal = CDec((From p In dtLeaveType Where p("ID") = cboMANUAL_ID.SelectedValue Select p("IS_DAY_HALF")).FirstOrDefault)
                If count = Employee_list.Count And day_half = -1 Then
                    cboFROM_SESSION.Enabled = True
                    cboTO_SESSION.Enabled = True
                Else
                    cboFROM_SESSION.Enabled = False
                    cboTO_SESSION.Enabled = False
                End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    Private Sub cboMANUAL_ID_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboMANUAL_ID.SelectedIndexChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            txtManual_Note.ClearValue()
            RefreshCboSession()
            If cboMANUAL_ID.SelectedValue <> "" Then
                Dim note As String = (From p In dtLeaveType Where p("ID") = cboMANUAL_ID.SelectedValue Select p("NOTE")).FirstOrDefault.ToString
                Dim is_day_half As Decimal = CDec((From p In dtLeaveType Where p("ID") = cboMANUAL_ID.SelectedValue Select p("IS_DAY_HALF")).FirstOrDefault)
                txtManual_Note.Text = note
                For Each item As GridDataItem In rgEmployee.Items
                    If item.GetDataKeyValue("EMPLOYEE_OBJECT_CODE") <> "HC1" And item.GetDataKeyValue("EMPLOYEE_OBJECT_CODE") <> "HC2" Then
                        cboFROM_SESSION.Enabled = False
                        cboTO_SESSION.Enabled = False
                        Exit Sub
                    End If
                Next
                If is_day_half = -1 Then
                    cboFROM_SESSION.Enabled = True
                    cboTO_SESSION.Enabled = True
                Else
                    cboFROM_SESSION.Enabled = False
                    cboTO_SESSION.Enabled = False
                End If
            End If
            'If (Not IsDate(rdLEAVE_FROM.SelectedDate) OrElse Not IsDate(rdLEAVE_TO.SelectedDate) OrElse dtDetail Is Nothing OrElse Not IsNumeric(rtEmployee_id.Text)) Then Exit Sub
            'GetLeaveSheet_Detail()
            Cal_DayLeaveSheet()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub cboFROM_SESSION_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboFROM_SESSION.SelectedIndexChanged
        Cal_DayLeaveSheet()
    End Sub

    Private Sub cboTO_SESSION_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTO_SESSION.SelectedIndexChanged
        Cal_DayLeaveSheet()
    End Sub
    Private Sub rdLEAVE_FROM_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdLEAVE_FROM.SelectedDateChanged
        Cal_DayLeaveSheet()
    End Sub

    Private Sub rdLEAVE_TO_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdLEAVE_TO.SelectedDateChanged
        Cal_DayLeaveSheet()
    End Sub
#End Region

#Region "Custom"

    'Public Sub UpdateSessionState()
    '    Try
    '        For Each item As GridDataItem In rgEmployee.Items
    '            If item.GetDataKeyValue("EMPLOYEE_OBJECT_CODE") <> "HC1" Or item.GetDataKeyValue("EMPLOYEE_OBJECT_CODE") <> "HC2" Then
    '                cboFROM_SESSION.Enabled = False
    '                cboTO_SESSION.Enabled = False
    '                Exit Sub
    '            End If
    '        Next
    '        If cboMANUAL_ID.SelectedValue <> "" Then

    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Protected Function CreateDataFilter(Optional ByVal fromDate As Date? = Nothing, Optional ByVal toDate As Date? = Nothing, Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_LEAVESHEETDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgData.VirtualItemCount = dtDetail.Rows.Count
            rgData.DataSource = dtDetail
            Cal_DayLeaveSheet()
        Catch ex As Exception
        End Try
        Return New DataTable()
    End Function

    Private Sub Cal_DayLeaveSheet()
        Try
            'Dim sumDay As Decimal = dtDetail.Compute("SUM(DAY_NUM)", "1=1")
            'rnDAY_NUM.NumberFormat.AllowRounding = False
            'rnDAY_NUM.NumberFormat.DecimalDigits = 2
            'rnDAY_NUM.Value = sumDay
            If Not IsDate(rdLEAVE_FROM.SelectedDate) OrElse Not IsDate(rdLEAVE_TO.SelectedDate) Then
                Exit Sub
            End If
            Dim Date_from As Date = If(IsDate(rdLEAVE_FROM.SelectedDate), rdLEAVE_FROM.SelectedDate, DateTime.Now)
            Dim Date_to As Date = If(IsDate(rdLEAVE_TO.SelectedDate), rdLEAVE_TO.SelectedDate, DateTime.Now)
            Dim sumDay As Decimal = DateDiff(DateInterval.Day, Date_from, Date_to) + 1
            Dim ss1 As String = (From p In dtLeaveSession Where p("ID") = cboFROM_SESSION.SelectedValue Select p("CODE")).FirstOrDefault
            Dim ss2 As String = (From p In dtLeaveSession Where p("ID") = cboTO_SESSION.SelectedValue Select p("CODE")).FirstOrDefault
            If ss1 = "AFT" Then
                sumDay -= New Decimal(0.5)
            End If
            If ss2 = "MOR" Then
                sumDay -= New Decimal(0.5)
            End If
            rnDAY_NUM.NumberFormat.AllowRounding = False
            rnDAY_NUM.NumberFormat.DecimalDigits = 2
            rnDAY_NUM.Value = sumDay
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Private Sub Cal_DayEntitlement()
    '    Try
    '        If rtEmployee_id.Text = "" Or rdLEAVE_FROM.SelectedDate Is Nothing Then
    '            Exit Sub
    '        End If

    '        Dim dtSourceEntitlement As New DataTable()
    '        Dim dtManual As New DataTable()
    '        Dim employee_id As Decimal = Decimal.Parse(rtEmployee_id.Text.Trim)

    '        Try
    '            Using rep As New AttendanceRepository
    '                Dim manualID As Decimal = Decimal.Parse(cboMANUAL_ID.SelectedValue)
    '                dtManual = rep.GET_MANUAL_BY_ID(manualID)
    '            End Using

    '            If dtManual.Rows.Count > 0 Then
    '                Dim strCode As String = dtManual.Rows(0)("CODE").ToString()
    '                Dim strMorning As String = If(dtManual.Rows(0)("MORNING_ID") IsNot Nothing, dtManual.Rows(0)("MORNING_ID").ToString(), Nothing)
    '                Dim strAfternoon As String = If(dtManual.Rows(0)("AFTERNOON_ID") IsNot Nothing, dtManual.Rows(0)("AFTERNOON_ID").ToString(), Nothing)

    '                If (strCode.ToUpper() = "P" And (strMorning = "251" Or strAfternoon = "251")) Then
    '                    Using rep As New AttendanceRepository
    '                        dtSourceEntitlement = rep.GET_INFO_PHEPNAM(employee_id, rdLEAVE_FROM.SelectedDate)
    '                        If dtSourceEntitlement.Rows.Count > 0 Then
    '                            isFlag = True
    '                            rnCUR_HAVE.Text = If(dtSourceEntitlement.Rows(0)("PHEP_CHE_DO") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CHE_DO").ToString())
    '                            rnSENIORITYHAVE.Text = If(dtSourceEntitlement.Rows(0)("PHEP_THAM_NIEN") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_THAM_NIEN").ToString())
    '                            rnPREV_HAVE.Text = If(dtSourceEntitlement.Rows(0)("PHEP_CU_CHUYEN_QUA") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CU_CHUYEN_QUA").ToString())
    '                            rnCUR_USED.Text = If(dtSourceEntitlement.Rows(0)("PHEP_CU_DA_NGHI") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CU_DA_NGHI").ToString())
    '                            rnCUR_HAVE_INMONTH.Text = If(dtSourceEntitlement.Rows(0)("PHEP_THANG_LVIEC") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_THANG_LVIEC").ToString())
    '                            rnPREVTOTAL_HAVE.Text = If(dtSourceEntitlement.Rows(0)("PHEP_CU_CON_HLUC") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CU_CON_HLUC").ToString())
    '                            rtCUR_USED_INMONTH.Text = If(dtSourceEntitlement.Rows(0)("PHEP_DA_NGHI") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_DA_NGHI").ToString())
    '                            rnCUR_DANGKY.Text = If(dtSourceEntitlement.Rows(0)("PHEP_DA_DANGKY") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_DA_DANGKY").ToString())
    '                            rnBALANCE.Text = If(dtSourceEntitlement.Rows(0)("PHEP_CONLAI") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CONLAI").ToString())
    '                        Else
    '                            Clearn__DayEntitlement()
    '                        End If
    '                    End Using
    '                Else
    '                    Clearn__DayEntitlement()
    '                End If
    '            Else
    '                Clearn__DayEntitlement()
    '            End If
    '        Catch ex As Exception
    '            Throw ex
    '        Finally
    '            dtSourceEntitlement.Dispose()
    '            dtManual.Dispose()
    '        End Try
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Public Sub CreateDataBinDing(ByVal Mode As Decimal)
        '1 set data in list control
        '0 get data to list control
        Select Case Mode
            Case 1
                For Each ctrs As Control In RadPane1.Controls
                    Try
                        Select Case ctrs.ID.ToString.ToUpper.Substring(0, 2)
                            Case "cb".ToUpper
                                CType(ctrs, RadComboBox).SelectedValue = rPH(ctrs.ID.ToString.ToUpper.Substring(3))
                            Case "rt".ToUpper
                                CType(ctrs, RadTextBox).Text = rPH(ctrs.ID.ToString.ToUpper.Substring(2))
                            Case "rn".ToUpper
                                CType(ctrs, RadNumericTextBox).Value = rPH(ctrs.ID.ToString.ToUpper.Substring(2))
                            Case "rd".ToUpper
                                CType(ctrs, RadDatePicker).SelectedDate = rPH(ctrs.ID.ToString.ToUpper.Substring(2))
                            Case Else
                                Continue For
                        End Select
                    Catch ex As Exception
                        Continue For
                    End Try
                Next
            Case 0
                For Each ctrs As Control In RadPane1.Controls
                    Try
                        Select Case ctrs.ID.ToString.ToUpper.Substring(0, 2)
                            Case "cb".ToUpper
                                rPH(ctrs.ID.ToString.ToUpper.Substring(3)) = CType(ctrs, RadComboBox).SelectedValue
                            Case "rt".ToUpper
                                rPH(ctrs.ID.ToString.ToUpper.Substring(2)) = CType(ctrs, RadTextBox).Text
                            Case "rn".ToUpper
                                rPH(ctrs.ID.ToString.ToUpper.Substring(2)) = CType(ctrs, RadNumericTextBox).Value
                            Case "rd".ToUpper
                                rPH(ctrs.ID.ToString.ToUpper.Substring(2)) = CType(ctrs, RadDatePicker).SelectedDate
                            Case Else
                                Continue For
                        End Select
                    Catch ex As Exception
                        Continue For
                    End Try
                Next
        End Select

    End Sub

    Private Sub SetDataToGrid(ByVal EditItem As GridEditableItem)
        Dim cbo As New RadComboBox
        Dim arr As New ArrayList()
        Try
            Dim LEAVE_DAY = EditItem.GetDataKeyValue("LEAVE_DAY")
            Dim STATUS_SHIFT As New Object
            Dim MANUAL_ID As New Object
            Dim IS_DEDUCT_SHIFT As New Object
            Dim SHIFT_DAY As New Object
            For Each rows As DataRow In dtDetail.Rows
                If rows("LEAVE_DAY") = LEAVE_DAY Then
                    STATUS_SHIFT = rows("STATUS_SHIFT")
                    MANUAL_ID = rows("MANUAL_ID")
                    IS_DEDUCT_SHIFT = rows("IS_DEDUCT_SHIFT")
                    SHIFT_DAY = rows("SHIFT_DAY")
                    Exit For
                End If
            Next
            cbo = CType(EditItem.FindControl("cbSTATUS_SHIFT"), RadComboBox)
            arr.Add(New DictionaryEntry("", Nothing))
            arr.Add(New DictionaryEntry("Đầu ca", 1))
            arr.Add(New DictionaryEntry("Cuối ca", 2))
            With cbo
                .DataSource = arr
                .DataValueField = "Value"
                .DataTextField = "Key"
                cbo.DataBind()
                .SelectedIndex = 0
            End With
            If IsNumeric(STATUS_SHIFT) Then
                cbo.SelectedValue = STATUS_SHIFT
                cbo.Enabled = If(Not IsNumeric(MANUAL_ID), False, True)
            End If
            If IS_DEDUCT_SHIFT = 0 Then
                cbo.Enabled = False
            Else
                If IsDBNull(SHIFT_DAY) = True Then
                    cbo.Enabled = False
                Else
                    If SHIFT_DAY <= 0.5 Or SHIFT_DAY > 1 Then
                        cbo.Enabled = False
                    Else
                        cbo.Enabled = True
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function SaveDB() As Boolean
        Dim rep As New AttendanceRepository
        Dim PH As DataTable = New DataTable()
        Dim dr As DataRow() = New DataRow() {rPH}
        dr(0)("STATUS") = 3 'Chờ phê duyệt
        PH = dr.CopyToDataTable()
        PH.TableName = "PH"
        Dim dsLeaveSheet As New DataSet("DATA")
        Dim CT As New DataTable()
        dsLeaveSheet.Tables.Add(PH)
        CT = dtDetail
        CT.TableName = "CT"
        'dsLeaveSheet.Tables.Remove("CT")
        dsLeaveSheet.Tables.Add(CT.Copy())
        Try
            Return rep.SaveLeaveSheet(dsLeaveSheet, gID)
        Catch ex As Exception
            Return False
        Finally
            rep.Dispose()
            CT.Dispose()
            PH.Dispose()
        End Try
    End Function

    Private Sub GetDataCombo()
        Dim rep As New AttendanceRepository
        Dim store As New AttendanceStoreProcedure
        Dim dtdata As DataTable
        Dim arr As New ArrayList()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'If ListComboData Is Nothing Then
            '    ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO

            '    'Điều chỉnh Loại nghỉ (thêm điều kiện Loại xử lý Kiểu công: Đăng ký)
            '    ListComboData.GET_LIST_TYPE_MANUAL_LEAVE = True
            '    rep.GetComboboxData(ListComboData)
            '    FillRadCombobox(cboMANUAL_ID, ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE, "NAME_VN", "ID", True)
            'End If
            dtLeaveType = store.GET_LEAVE_TYPE(True)
            dtLeaveSession = rep.GetOtherList("LEAVE_SESSION")
            FillRadCombobox(cboMANUAL_ID, dtLeaveType, "NAME", "ID")
            FillRadCombobox(cboFROM_SESSION, dtLeaveSession, "NAME", "ID")
            FillRadCombobox(cboTO_SESSION, dtLeaveSession, "NAME", "ID")
            RefreshCboSession()
            dtdata = rep.GetOtherList("LEAVE_REASON", True)
            FillRadCombobox(cboREASON_LEAVE, dtdata, "NAME", "ID")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub GetLeaveSheet_Detail(ByVal _emp_id As Decimal)
        Dim dtSource As New DataTable()
        Try
            Using rep As New AttendanceRepository
                Dim manualID As Decimal = Decimal.Parse(cboMANUAL_ID.SelectedValue)
                dtSource = rep.GetLeaveSheet_Detail_ByDate(_emp_id, rdLEAVE_FROM.SelectedDate, rdLEAVE_TO.SelectedDate, manualID)
            End Using
            dtDetail = dtSource
            If dtDetail.Rows.Count > 0 Then
                Dim ss1 As String = (From p In dtLeaveSession Where p("ID") = cboFROM_SESSION.SelectedValue Select p("CODE")).FirstOrDefault
                Dim ss2 As String = (From p In dtLeaveSession Where p("ID") = cboTO_SESSION.SelectedValue Select p("CODE")).FirstOrDefault
                If Not String.IsNullOrEmpty(ss1) Then
                    dtDetail.Rows(0)("DAY_NUM") = "0.5"
                    If ss1 = "MOR" Then
                        dtDetail.Rows(0)("STATUS_SHIFT") = 1
                    ElseIf ss1 = "AFT" Then
                        dtDetail.Rows(0)("STATUS_SHIFT") = 2
                    End If
                End If
                If Not String.IsNullOrEmpty(ss1) Then
                    dtDetail.Rows(dtDetail.Rows.Count - 1)("DAY_NUM") = "0.5"
                    If ss1 = "MOR" Then
                        dtDetail.Rows(dtDetail.Rows.Count - 1)("STATUS_SHIFT") = 1
                    ElseIf ss1 = "AFT" Then
                        dtDetail.Rows(dtDetail.Rows.Count - 1)("STATUS_SHIFT") = 2
                    End If
                End If
            End If
            'rgData.Rebind()
            'For Each item As GridDataItem In rgData.MasterTableView.Items
            '    item.Edit = True
            'Next
            'rgData.MasterTableView.Rebind()
        Catch ex As Exception
            Throw ex
        Finally
            dtSource.Dispose()
        End Try
    End Sub

    ''' <summary>
    ''' Fill data len control theo ID
    ''' </summary>
    ''' <param name="empid">Ma nhan vien</param>
    ''' <remarks></remarks>

    Private Sub SetData_Controls(ByVal atLeave As AT_LEAVESHEETDTO, ByVal id_state As Decimal, ByVal _emp_id As Decimal)
        Try

            'check so ngay dang ky nghi
            'If Not IsNumeric(rnDAY_NUM.Value) OrElse rnDAY_NUM.Value < 1 Then
            '    ShowMessage(Translate("Số ngày đăng ký nghỉ phải lơn hơn 0"), NotifyType.Warning)
            '    Exit Sub
            'End If
            rnSTATUS.Text = id_state.ToString
            CreateDataBinDing(0)
            atLeave.LEAVE_FROM = rdLEAVE_FROM.SelectedDate
            atLeave.LEAVE_TO = rdLEAVE_TO.SelectedDate
            atLeave.ID = gID
            atLeave.EMPLOYEE_ID = _emp_id
            'If (New AttendanceBusinessClient).ValidateLeaveSheetDetail(atLeave) = False Then
            '    ShowMessage(Translate("Ngày đăng ký nghỉ đã bị trùng"), NotifyType.Warning)
            '    Exit Sub
            'End If

            Dim dtCheckSendApprove As DataTable = psp.CHECK_APPROVAL(atLeave.ID)
            Dim period_id As Integer = Utilities.ObjToDecima(dtCheckSendApprove.Rows(0)("PERIOD_ID"))
            Dim sign_id As Integer = Utilities.ObjToDecima(dtCheckSendApprove.Rows(0)("SIGN_ID"))
            Dim id_group As Integer = Utilities.ObjToDecima(dtCheckSendApprove.Rows(0)("ID_REGGROUP"))
            Dim sumday As Integer = Utilities.ObjToDecima(dtCheckSendApprove.Rows(0)("SUMDAY"))

            Dim outNumber As Decimal
            Try
                Dim IAttendance As IAttendanceBusiness = New AttendanceBusinessClient()
                'outNumber = IAttendance.PRI_PROCESS_APP(EmployeeID, period_id, "LEAVE", 0, sumday, sign_id, id_group)
                outNumber = IAttendance.PRI_PROCESS_APP(_emp_id, period_id, "LEAVE", 0, sumday, sign_id, id_group, LogHelper.CurrentUser.EMPLOYEE_ID)
            Catch ex As Exception
                ShowMessage(ex.ToString, NotifyType.Error)
            End Try

            If outNumber = 0 Then
                'Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlLeaveRegistration")
            ElseIf outNumber = 1 Then
                ShowMessage(Translate("Quy trình phê duyệt chưa được thiết lập"), NotifyType.Success)
            ElseIf outNumber = 2 Then
                ShowMessage(Translate("Thao tác xảy ra lỗi,bạn kiểm tra lại quy trình"), NotifyType.Error)
            ElseIf outNumber = 3 Then
                ShowMessage(Translate("Nhân viên chưa có thiết lập nhóm chức danh"), NotifyType.Error)
            End If

            'If SaveDB("SUBMIT") Then
            '    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlLeaveRegistration")
            'Else
            '    ShowMessage(Translate("Xảy ra lỗi"), NotifyType.Error)
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Xử lý và kiểm tra Kỳ công đóng mở theo ngày đăng ký nghỉ
    ''' </summary>
    ''' <param name="_date"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IS_PERIOD_CLOSE(ByVal _date As Date?) As Boolean
        Dim dtData As DataTable
        Dim period_id As Integer
        Dim rs As Boolean = False
        Try
            dtData = psp.GET_PERIOD_BYDATE(_date)
            If dtData.Rows.Count > 0 Then
                period_id = Utilities.ObjToDecima(dtData.Rows(0)("PERIOD_ID"))
                Using rep As New AttendanceRepository
                    Dim check = rep.CHECK_PERIOD_CLOSE(period_id)
                    If check = 0 Then
                        rs = False
                    Else
                        rs = True
                    End If
                End Using
            End If
            Return rs
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub RefreshCboSession()
        Try
            Dim ss1 As Decimal = (From p In dtLeaveSession Where p("CODE").ToString.ToUpper.Equals("MOR") Select p("ID")).FirstOrDefault
            Dim ss2 As Decimal = (From p In dtLeaveSession Where p("CODE").ToString.ToUpper.Equals("AFT") Select p("ID")).FirstOrDefault
            cboFROM_SESSION.SelectedValue = ss1
            FillRadCombobox(cboTO_SESSION, dtLeaveSession, "NAME", "ID")
            cboTO_SESSION.SelectedValue = ss2
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region



End Class
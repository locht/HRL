Imports Common
'Imports Ionic.Zip
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Attendance.AttendanceBusiness
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Imports Aspose.Words
Imports Aspose.Words.Reporting
Imports System.IO
Imports HistaffFrameworkPublic
Imports Profile

Public Class ctrlShiftRegNewEdit
    Inherits CommonView


    ''' <summary>
    ''' ctrl FindEmployeePopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup

    ''' <summary>
    ''' Must Authorize
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Property MustAuthorize As Boolean = True

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
    Dim _classPath As String = "Attendance\Modules\Attendance\Business" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' Commend
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Shift As AtShiftRegMngDTO
        Get
            Return ViewState(Me.ID & "_Shift")
        End Get
        Set(ByVal value As AtShiftRegMngDTO)
            ViewState(Me.ID & "_Shift") = value
        End Set
    End Property

    Property Down_File As String
        Get
            Return ViewState(Me.ID & "_Down_File")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Down_File") = value
        End Set
    End Property
    Property FileOldName As String
        Get
            Return ViewState(Me.ID & "_FileOldName")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_FileOldName") = value
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
    ''' Employee_lst
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Employee_lst As List(Of CommonBusiness.EmployeeDTO)
        Get
            Return ViewState(Me.ID & "_Employee_lst")
        End Get
        Set(value As List(Of CommonBusiness.EmployeeDTO))
            ViewState(Me.ID & "_Employee_lst") = value
        End Set
    End Property

    ''' <summary>
    ''' ListComboData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListComboData As AttendanceBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As AttendanceBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
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
            Me.MainToolBar = tbarCommend

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Seperator,
                                       ToolbarItem.Cancel)

            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
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
        Dim rep As New AttendanceRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case Message
                Case "UpdateView"
                    Shift = rep.GetShiftRegById(IDSelect)
                    CurrentState = CommonMessage.STATE_EDIT

                    Employee_lst = Shift.EMP_LST
                    If Shift.SHIFT_ID IsNot Nothing Then
                        cboShift.SelectedValue = Shift.SHIFT_ID
                    End If
                    rdRegDate.SelectedDate = Shift.WORKING_DAY
                    txtNote.Text = Shift.NOTE
                    If Shift.WEEKEND IsNot Nothing Then
                        ckWeekend.Checked = True
                    End If
                    rgEmployee.Rebind()

                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
                    Employee_lst = New List(Of CommonBusiness.EmployeeDTO)
            End Select
            rep.Dispose()
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
        Dim objShiftReg As New AtShiftRegMngDTO
        Dim gID As Decimal
        Dim rep As New AttendanceRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If rgEmployee.Items.Count = 0 Then
                            ShowMessage(Translate("Chưa chọn nhân viên, kiểm tra lại!!"), NotifyType.Warning)
                            Exit Sub
                        End If
                        objShiftReg.ID = If(IsNumeric(IDSelect), IDSelect, 0)
                        objShiftReg.WORKING_DAY = rdRegDate.SelectedDate
                        objShiftReg.NOTE = txtNote.Text
                        objShiftReg.SHIFT_ID = cboShift.SelectedValue
                        objShiftReg.WEEKEND = ckWeekend.Checked
                        objShiftReg.MODIFIED_BY = LogHelper.GetUserLog.Username.ToUpper
                        objShiftReg.MODIFIED_DATE = DateTime.Now
                        objShiftReg.MODIFIED_LOG = LogHelper.GetUserLog.Ip + "\" + LogHelper.GetUserLog.ComputerName
                        For Each item As GridDataItem In rgEmployee.Items
                            Dim emp_id As Decimal = CDec(item.GetDataKeyValue("ID"))
                            Dim _param = New Attendance.AttendanceBusiness.ParamDTO With {.ORG_ID = item.GetDataKeyValue("ORG_ID"),
                                            .PERIOD_ID = Decimal.Parse(rep.GET_PERIOD(rdRegDate.SelectedDate))}

                            If Not rep.IS_PERIODSTATUS(_param) Then
                                ShowMessage(Translate("Kỳ công của nhân viên " & item.GetDataKeyValue("EMPLOYEE_CODE") & " đã đóng !!"), NotifyType.Warning)
                                Exit Sub
                            End If
                            objShiftReg.EMPLOYEE_ID = emp_id
                            If rep.ValidateShiftReg(objShiftReg) = False Then
                                ShowMessage(Translate("Ngày đăng ký của nhân viên " & item.GetDataKeyValue("EMPLOYEE_CODE") & " đã bị trùng"), NotifyType.Warning)
                                Exit Sub
                            End If
                        Next
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objShiftReg.CREATED_BY = LogHelper.GetUserLog.Username.ToUpper
                                objShiftReg.CREATED_DATE = DateTime.Now
                                objShiftReg.CREATED_LOG = LogHelper.GetUserLog.Ip + "\" + LogHelper.GetUserLog.ComputerName
                            Case CommonMessage.STATE_EDIT
                                Dim lstId As New List(Of Decimal)
                                lstId.Add(IDSelect)
                                rep.DeleteShiftReg(lstId)
                                objShiftReg.ID = 0
                        End Select
                        For Each item As GridDataItem In rgEmployee.Items
                            Dim emp_id As Decimal = CDec(item.GetDataKeyValue("ID"))
                            objShiftReg.EMPLOYEE_ID = emp_id
                            rep.AddShiftReg(objShiftReg)
                        Next
                    End If
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlShiftRegMng&group=Business")
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlShiftRegMng&group=Business")
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Function IsTienMatCommendPay(ByVal commend_pay_id) As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Using rep As New ProfileRepository
                Dim commend_pays = rep.GetOtherList("COMMEND_PAY", True).ToList(Of OtherListDTO)()
                Dim current = commend_pays.FirstOrDefault(Function(f) f.ID = commend_pay_id)
                If current IsNot Nothing Then
                    Return current.CODE = ProfileCommon.Commend_Pay.TienMat
                End If
            End Using
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(ViewName, ID, ex)
            Return False
        End Try
        Return False
    End Function

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện khi 1 Employee được chọn ở popup FindEmployee</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim repNew As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count > 0 Then
                If Employee_lst Is Nothing Then
                    Employee_lst = New List(Of CommonBusiness.EmployeeDTO)
                End If
                For Each emp As CommonBusiness.EmployeePopupFindDTO In lstCommonEmployee
                    If Employee_lst.Any(Function(f) f.EMPLOYEE_CODE = emp.EMPLOYEE_CODE) Then
                        ShowMessage(Translate("Nhân viên đã tồn tại."), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim employee As New Common.CommonBusiness.EmployeeDTO
                    employee.EMPLOYEE_CODE = emp.EMPLOYEE_CODE
                    employee.ID = emp.ID
                    employee.FULLNAME_VN = emp.FULLNAME_VN
                    employee.ORG_NAME = emp.ORG_NAME
                    employee.TITLE_NAME_VN = emp.TITLE_NAME
                    employee.ORG_ID = emp.ORG_ID
                    employee.TITLE_ID = emp.TITLE_ID

                    Employee_lst.Add(employee)
                Next

                rgEmployee.Rebind()

                For Each i As GridItem In rgEmployee.Items
                    i.Edit = True
                Next

                rgEmployee.Rebind()
            End If
            isLoadPopup = 0

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý sự kiện cho nút hủy của popup</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            isLoadPopup = 0

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
            rgEmployee.DataSource = Employee_lst
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
                    ctrlFindEmployeePopup.MultiSelect = True
                    ctrlFindEmployeePopup.Show()
                Case "DeleteEmployee"
                    For Each i As GridDataItem In rgEmployee.SelectedItems
                        Dim s = (From q In Employee_lst Where
                                 q.ID = i.GetDataKeyValue("ID")).FirstOrDefault
                        Employee_lst.Remove(s)
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
            If FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                FindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
            End If

            

            Select Case isLoadPopup
                Case 1
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.MustHaveContract = True
                    ctrlFindEmployeePopup.IsOnlyWorkingWithoutTer = True
                    FindEmployee.Controls.Add(ctrlFindEmployeePopup)
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
        Dim rep As New AttendanceRepository

        Try
            If ListComboData Is Nothing Then
                ListComboData = New AttendanceBusiness.ComboBoxDataDTO
                ListComboData.GET_LIST_SHIFT = True
                rep.GetComboboxData(ListComboData)
            End If
            FillDropDownList(cboShift, ListComboData.LIST_LIST_SHIFT, "NAME_VN", "ID")
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
                    IDSelect = Decimal.Parse(Request.Params("ID"))
                End If

                If IsNumeric(IDSelect) Then
                    Refresh("UpdateView")
                    CurrentState = CommonMessage.STATE_EDIT
                    rdRegDate.Enabled = False
                Else
                    Refresh("InsertView")
                    CurrentState = CommonMessage.STATE_NEW
                    rdRegDate.Enabled = True

                End If
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region



End Class
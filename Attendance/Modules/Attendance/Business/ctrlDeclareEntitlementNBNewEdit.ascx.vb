Imports Framework.UI
Imports Common
Imports Attendance.ctrlNewInOut
Imports Telerik.Web.UI
Imports Framework.UI.Utilities
Imports Attendance.AttendanceRepository
Imports Attendance.AttendanceBusiness
Imports WebAppLog
Public Class ctrlDeclareEntitlementNBNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Setting/" + Me.GetType().Name.ToString()
#Region "Property"
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
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
    Property STAFF_RANK_ID As Integer
        Get
            Return ViewState(Me.ID & "_STAFF_RANK_ID")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_STAFF_RANK_ID") = value
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
#End Region

#Region "Page"
    ''' <summary>
    ''' Khởi tạo, load page
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
    ''' Khởi tạo control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Set value cho rntxtYear
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Try
            rntxtYear.Value = Date.Now.Year
        Catch ex As Exception
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
            Select Case Message
                Case "TRUE"
                    Dim obj As New AT_DECLARE_ENTITLEMENTDTO
                    obj.ID = Decimal.Parse(Request.Params("ID"))
                    CurrentState = CommonMessage.STATE_EDIT
                    obj = rep.GetDelareEntitlementNBById(obj.ID)
                    If obj IsNot Nothing Then
                        isEdit = True
                        txtCode.Text = obj.EMPLOYEE_CODE
                        txtName.Text = obj.VN_FULLNAME
                        txtChucDanh.Text = obj.TITLE_NAME
                        Employee_id = obj.EMPLOYEE_ID
                        txtDonVi.Text = obj.ORG_NAME
                        rntxtADJUST_MONTH_TN2.Value = obj.ADJUST_MONTH_TN
                        txtREMARK_TN.Text = obj.REMARK_TN
                        rntxtADJUST_ENTITLEMENT2.Value = obj.ADJUST_ENTITLEMENT
                        If obj.ADJUST_MONTH_ENTITLEMENT IsNot Nothing Then
                            cboMonth.SelectedValue = obj.ADJUST_MONTH_ENTITLEMENT
                        End If
                        If obj.START_MONTH_TN IsNot Nothing Then
                            cboStartMonth.SelectedValue = obj.START_MONTH_TN
                        End If
                        If obj.START_MONTH_EXTEND IsNot Nothing Then
                            cboStartCur.SelectedValue = obj.START_MONTH_EXTEND
                        End If
                        rntxtYear.Value = obj.YEAR
                        txtREMARK_EN.Text = obj.REMARK_ENTITLEMENT
                        If obj.START_MONTH_NB IsNot Nothing Then
                            cboSTART_MONTH_NB.SelectedValue = obj.START_MONTH_NB
                        End If
                        nmADJUST_NB.Value = obj.ADJUST_NB
                        txtREMARK_NB.Text = obj.REMARK_NB
                        If obj.MONTH_EXTENSION_NB IsNot Nothing Then
                            cboMonth_Extension_NB.SelectedValue = obj.MONTH_EXTENSION_NB
                        End If
                        nmCOM_PAY.Value = obj.COM_PAY
                        nmENT_PAY.Value = obj.ENT_PAY
                        _Value = obj.ID
                    Else
                        isEdit = False
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
    ''' Event click cancel popup tìm nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub
    ''' <summary>
    ''' Event Click Ok popup chọn nhân viên
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnEmployee_Click(ByVal sender As Object,
                                  ByVal e As EventArgs) Handles btnFindEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case sender.ID
                Case btnFindEmployee.ID
                    isLoadPopup = 1
            End Select

            UpdateControlState()
            Select Case sender.ID
                Case btnFindEmployee.ID
                    ctrlFindEmployeePopup.Show()
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event click item menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim obj As AT_DECLARE_ENTITLEMENTDTO
        Dim rep As New AttendanceRepository
        Dim gCODE As String = ""
        Dim gstatus As Integer = 0
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Employee_id <= 0 Then
                        ShowMessage(Translate("Chưa chọn nhân viên cần đăng ký"), NotifyType.Error)
                        Exit Sub
                    End If
                    If Page.IsValid Then
                        Dim startdate As New Date
                        Dim checkMonthNB As Boolean = False
                        Dim checkMonthNP As Boolean = False
                        obj = New AT_DECLARE_ENTITLEMENTDTO
                        obj.EMPLOYEE_ID = Employee_id
                        obj.ADJUST_MONTH_TN = rntxtADJUST_MONTH_TN2.Value
                        obj.ADJUST_ENTITLEMENT = rntxtADJUST_ENTITLEMENT2.Value
                        If cboMonth.SelectedValue <> "" Then
                            obj.ADJUST_MONTH_ENTITLEMENT = cboMonth.SelectedValue
                        End If
                        If cboStartMonth.SelectedValue <> "" Then
                            obj.START_MONTH_TN = cboStartMonth.SelectedValue
                        End If
                        If cboStartCur.SelectedValue <> "" Then
                            obj.START_MONTH_EXTEND = cboStartCur.SelectedValue
                        End If
                        obj.YEAR = rntxtYear.Value
                        obj.YEAR_ENTITLEMENT = rntxtYear.Value
                        obj.YEAR_NB = rntxtYear.Value
                        If cboSTART_MONTH_NB.SelectedValue <> "" Then
                            obj.START_MONTH_NB = cboSTART_MONTH_NB.SelectedValue
                        End If
                        obj.ADJUST_NB = nmADJUST_NB.Value
                        obj.REMARK_TN = txtREMARK_TN.Text
                        obj.REMARK_NB = txtREMARK_NB.Text
                        obj.REMARK_ENTITLEMENT = txtREMARK_EN.Text
                        If cboMonth_Extension_NB.SelectedValue <> "" Then
                            obj.MONTH_EXTENSION_NB = cboMonth_Extension_NB.SelectedValue
                        End If
                        If _Value.HasValue Then
                            obj.ID = _Value
                        End If
                        obj.COM_PAY = nmCOM_PAY.Value
                        obj.ENT_PAY = nmENT_PAY.Value

                        rep.InsertDelareEntitlementNB(obj, gstatus, checkMonthNB, checkMonthNP)
                        If checkMonthNB Then
                            ShowMessage(Translate("Nhân viên này đã được gia hạn nghỉ bù"), NotifyType.Error)
                            cboMonth_Extension_NB.Focus()
                            Exit Sub
                        ElseIf checkMonthNP Then
                            ShowMessage(Translate("Nhân viên này đã được gia hạn nghỉ phép"), NotifyType.Error)
                            cboStartCur.Focus()
                            Exit Sub
                        End If
                        'Dim str As String = "getRadWindow().close('1');"
                        'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        ''POPUPTOLINK
                        Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlDeclareEntitlementNB&group=Business")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlDeclareEntitlementNB&group=Business")
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
    ''' Update trạng thái control
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
                            ctrlFindEmployeePopup.MultiSelect = False
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
    ''' Event chọn nhân viên trên popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                For idx = 0 To lstCommonEmployee.Count - 1
                    Dim item As New AT_DECLARE_ENTITLEMENTDTO
                    item.EMPLOYEE_ID = lstCommonEmployee(idx).ID
                    item.EMPLOYEE_CODE = lstCommonEmployee(idx).EMPLOYEE_CODE
                    item.VN_FULLNAME = lstCommonEmployee(idx).FULLNAME_VN
                    item.ORG_ID = lstCommonEmployee(idx).ORG_ID
                    item.ORG_NAME = lstCommonEmployee(idx).ORG_NAME
                    item.TITLE_NAME = lstCommonEmployee(idx).TITLE_NAME
                    item.EMPLOYEE_ID = lstCommonEmployee(idx).ID
                    item.STAFF_RANK_ID = lstCommonEmployee(idx).STAFF_RANK_ID
                    isLoadPopup = 0
                    Employee_id = item.EMPLOYEE_ID
                    txtCode.Text = item.EMPLOYEE_CODE
                    txtName.Text = item.VN_FULLNAME
                    txtChucDanh.Text = item.TITLE_NAME
                    txtDonVi.Text = item.ORG_NAME
                Next
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


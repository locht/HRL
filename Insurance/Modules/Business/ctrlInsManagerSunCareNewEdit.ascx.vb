Imports Framework.UI
Imports Common
Imports Telerik.Web.UI
Imports Framework.UI.Utilities
Imports Insurance.InsuranceRepository
Imports Insurance.InsuranceBusiness
Imports WebAppLog

Public Class ctrlInsManagerSunCareNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False

    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Insurance/Module/Insurance/Business/" + Me.GetType().Name.ToString()

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
    Property INSREMIGE As INS_REMIGE_MANAGER_DTO
        Get
            Return ViewState(Me.ID & "_INSREMIGES")
        End Get
        Set(ByVal value As INS_REMIGE_MANAGER_DTO)
            ViewState(Me.ID & "_INSREMIGES") = value
        End Set
    End Property
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 06/09/2017 14:00
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
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
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
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
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
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Xet cac tuy chon gia tri cua Message la UpdateView, InsertView
    ''' Bind lai du lieu cho grid rgRegisterLeave
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Message = Request.Params("VIEW")
            Select Case Message
                Case "TRUE"
                    Dim obj As New INS_SUN_CARE_DTO
                    obj.ID = Decimal.Parse(Request.Params("ID"))
                    CurrentState = CommonMessage.STATE_EDIT
                    obj = rep.GetSunCareById(obj.ID)
                    INSREMIGE = New INS_REMIGE_MANAGER_DTO
                    If obj IsNot Nothing Then

                        hidEmpID.Value = obj.EMPLOYEE_ID
                        hidID.Value = obj.ID
                        txtEMPLOYEE_CODE.Text = obj.EMPLOYEE_CODE
                        txtEMPLOYEE_NAME.Text = obj.EMPLOYEE_NAME
                        txtORG_NAME.Text = obj.ORG_NAME
                        txtTITLE_NAME.Text = obj.TITLE_NAME
                        txtCapNS.Text = obj.STAFF_RANK_NAME
                        txtNote.Text = obj.NOTE
                        If obj.THOIDIEMHUONG IsNot Nothing Then
                            rdThoiDiem.SelectedDate = obj.THOIDIEMHUONG
                        End If
                        If obj.START_DATE IsNot Nothing Then
                            dpSTART_DATE.SelectedDate = obj.START_DATE
                        End If
                        If obj.END_DATE IsNot Nothing Then
                            dpEND_DATE.SelectedDate = obj.END_DATE
                        End If
                        If obj.LEVEL_ID IsNot Nothing Then
                            For Each item As RadComboBoxItem In cboLEVEL.Items
                                If item.Value = obj.LEVEL_ID.ToString() Then
                                    cboLEVEL.SelectedValue = obj.LEVEL_ID
                                End If
                            Next
                        End If
                        If obj.COST IsNot Nothing Then
                            nmCOST.Value = obj.COST
                        End If

                        If obj.COST_SAL IsNot Nothing Then
                            nmCOSTSAL.Value = obj.COST_SAL
                        End If
                        _Value = obj.ID
                    End If
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho button ctrFindPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho button btnEmployee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case sender.ID
                Case btnFindEmployee.ID
                    isLoadPopup = 1
            End Select

            UpdateControlState()
            Select Case sender.ID
                Case btnFindEmployee.ID
                    ctrlFindEmployeePopup.Show()
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objData As INS_SUN_CARE_DTO
        Dim rep As New InsuranceRepository
        Dim gID As Integer = 0
        Dim gstatus As Integer = 0
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then

                        If dpEND_DATE.SelectedDate < rdThoiDiem.SelectedDate Then
                            ShowMessage(Translate("Ngày cấp phải nhỏ hơn ngày hết hiệu lực"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If

                        If _Value.HasValue Then
                            objData = New INS_SUN_CARE_DTO
                            objData.ID = hidID.Value
                            objData.EMPLOYEE_ID = hidEmpID.Value
                            objData.EMPLOYEE_CODE = txtEMPLOYEE_CODE.Text.Trim
                            objData.EMPLOYEE_NAME = txtEMPLOYEE_NAME.Text.Trim
                            objData.ORG_NAME = txtORG_NAME.Text.Trim
                            objData.TITLE_NAME = txtTITLE_NAME.Text.Trim
                            objData.STAFF_RANK_NAME = txtCapNS.Text.Trim
                            objData.THOIDIEMHUONG = rdThoiDiem.SelectedDate
                            objData.START_DATE = dpSTART_DATE.SelectedDate
                            objData.END_DATE = dpEND_DATE.SelectedDate
                            If cboLEVEL.SelectedValue = "" Then
                                objData.LEVEL_ID = Nothing
                            Else
                                objData.LEVEL_ID = cboLEVEL.SelectedValue
                            End If
                            objData.COST = nmCOST.Value
                            objData.COST_SAL = nmCOSTSAL.Value
                            objData.NOTE = txtNote.Text
                            gID = Utilities.ObjToDecima(hidID.Value)
                            rep.ModifySunCare(objData, gID)
                        Else
                            objData = New INS_SUN_CARE_DTO
                            objData.EMPLOYEE_ID = hidEmpID.Value
                            objData.EMPLOYEE_CODE = txtEMPLOYEE_CODE.Text.Trim
                            objData.EMPLOYEE_NAME = txtEMPLOYEE_NAME.Text.Trim
                            objData.ORG_NAME = txtORG_NAME.Text.Trim
                            objData.TITLE_NAME = txtTITLE_NAME.Text.Trim
                            objData.STAFF_RANK_NAME = txtCapNS.Text.Trim
                            objData.THOIDIEMHUONG = rdThoiDiem.SelectedDate
                            objData.START_DATE = dpSTART_DATE.SelectedDate
                            objData.END_DATE = dpEND_DATE.SelectedDate
                            If cboLEVEL.SelectedValue = "" Then
                                objData.LEVEL_ID = Nothing
                            Else
                                objData.LEVEL_ID = cboLEVEL.SelectedValue
                            End If
                            objData.COST = nmCOST.Value
                            objData.COST_SAL = nmCOSTSAL.Value
                            objData.NOTE = txtNote.Text
                            gID = Utilities.ObjToDecima(hidID.Value)
                            rep.InsertSunCare(objData, gID)
                        End If
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        rdThoiDiem.Enabled = False
                        dpSTART_DATE.Enabled = False
                        dpEND_DATE.Enabled = False
                        cboLEVEL.Enabled = False
                        nmCOST.Enabled = False
                        nmCOSTSAL.Enabled = False
                        'Dim str As String = "getRadWindow().close('1');"
                        'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        ''POPUPTOLINK()
                        'Response.Redirect("/Default.aspx?mid=Insurance&fid=ctrlInsManagerSunCare&group=Business")

                        CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                        CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Insurance&fid=ctrlInsManagerSunCare&group=Business")
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien SelectedIndexChanged cho control cboLEVEL
    ''' </summary>
    ''' <param name="args"></param>
    ''' <param name="source"></param>
    ''' <remarks></remarks>
    'Private Sub cvalLEVER_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalLEVEL.ServerValidate
    '    Dim rep As New InsuranceRepository
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If cboLEVEL.SelectedValue <> "" Then
    '            Dim _validate As New INS_COST_FOLLOW_LEVERDTO
    '            _validate.ID = cboLEVEL.SelectedValue
    '            _validate.ACTFLG = "A"
    '            args.IsValid = rep.ValidateINS_COST_FOLLOW_LEVER(_validate)
    '            If Not args.IsValid Then
    '                GetDataCombo()
    '            End If
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method,
    '                                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load du lieu cho cac combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_LIST_ORG_ID_INS = True
                rep.GetComboboxData(ListComboData)
            End If
            FillDropDownList(cboLEVEL, ListComboData.LIST_LIST_ORG_ID_INS, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboLEVEL.SelectedValue)
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Update trang thai cua cac control theo state
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
                        ctrlFindEmployeePopup.MultiSelect = False
                    End If
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien chon nhan vien cho ctrFindEmployee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                hidEmpID.Value = lstCommonEmployee(0).EMPLOYEE_ID
                txtEMPLOYEE_CODE.Text = lstCommonEmployee(0).EMPLOYEE_CODE
                txtORG_NAME.Text = lstCommonEmployee(0).ORG_NAME
                txtTITLE_NAME.Text = lstCommonEmployee(0).TITLE_NAME
                txtEMPLOYEE_NAME.Text = lstCommonEmployee(0).FULLNAME_VN
                txtCapNS.Text = lstCommonEmployee(0).STAFF_RANK_NAME

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien SelectedIndexChanged cho control cboLEVEL
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboLEVEL_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboLEVEL.SelectedIndexChanged
        Dim rep As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If cboLEVEL.SelectedValue <> "" Then
                Dim dtCost As New INS_COST_FOLLOW_LEVERDTO
                dtCost = rep.GetIns_Cost_LeverByID(Utilities.ObjToInt(cboLEVEL.SelectedValue))
                If dtCost IsNot Nothing Then
                    nmCOST.Value = dtCost.COST_MONEY
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

End Class


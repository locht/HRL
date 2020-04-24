Imports Framework.UI
Imports Common
Imports Telerik.Web.UI
Imports Framework.UI.Utilities
Imports Insurance.InsuranceRepository
Imports Insurance.InsuranceBusiness
Imports WebAppLog

Public Class ctrlInsAccidentRiskNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = True

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
                    Dim obj As New INS_ACCIDENT_RISKDTO
                    obj.ID = Decimal.Parse(Request.Params("ID"))
                    CurrentState = CommonMessage.STATE_EDIT
                    obj = rep.GET_INS_ACCIDENT_RISK(obj.ID)
                    If obj IsNot Nothing Then
                        hidEmpID.Value = obj.EMPLOYEE_ID
                        hidID.Value = obj.ID
                        txtEMPLOYEE_CODE.Text = obj.EMPLOYEE_CODE
                        txtEMPLOYEE_NAME.Text = obj.EMPLOYEE_NAME
                        txtORG_NAME.Text = obj.ORG_NAME
                        txtJOB_NAME.Text = obj.JOB_NAME
                        rdBIRTH_DATE.SelectedDate = obj.BIRTH_DATE
                        txtGENDER_NAME.Text = obj.GENDER_NAME
                        txtADDRESS.Text = obj.ADDRESS
                        txtCONTRACT_NO.Text = obj.CONTRACT_NO
                        txtROWNUM_NO.Text = obj.ROWNUM_NO
                        rdCONTRACT_SIGN_DATE.SelectedDate = obj.CONTRACT_SIGN_DATE
                        rdCONTRACT_START_DATE.SelectedDate = obj.CONTRACT_START_DATE
                        rdCONTRACT_EXPIRE_DATE.SelectedDate = obj.CONTRACT_EXPIRE_DATE
                        txtPLHD_CONTRACT_NO.Text = obj.PLHD_CONTRACT_NO
                        rdPLHD_SIGN_DATE.SelectedDate = obj.PLHD_SIGN_DATE
                        rdPLHD_START_DATE.SelectedDate = obj.PLHD_START_DATE
                        rdPLHD_EXPIRE_DATE.SelectedDate = obj.PLHD_EXPIRE_DATE
                        txtINS_ORG_NAME.Text = obj.INS_ORG_NAME
                        txtREMARK.Text = obj.REMARK
                        If obj.INS_ORG_ID IsNot Nothing Then
                            hidInsOrgID.Value = obj.INS_ORG_ID
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
        Dim objData As INS_ACCIDENT_RISKDTO
        Dim rep As New InsuranceRepository
        Dim gID As Integer = 0
        Dim gstatus As Integer = 0
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    objData = New INS_ACCIDENT_RISKDTO
                    objData.EMPLOYEE_ID = hidEmpID.Value
                    objData.CONTRACT_NO = txtCONTRACT_NO.Text
                    objData.ROWNUM_NO = txtROWNUM_NO.Text
                    objData.CONTRACT_SIGN_DATE = rdCONTRACT_SIGN_DATE.SelectedDate
                    objData.CONTRACT_START_DATE = rdCONTRACT_START_DATE.SelectedDate
                    objData.CONTRACT_EXPIRE_DATE = rdCONTRACT_EXPIRE_DATE.SelectedDate
                    objData.PLHD_CONTRACT_NO = txtPLHD_CONTRACT_NO.Text
                    objData.PLHD_SIGN_DATE = rdCONTRACT_SIGN_DATE.SelectedDate
                    objData.PLHD_START_DATE = rdPLHD_START_DATE.SelectedDate
                    objData.PLHD_EXPIRE_DATE = rdPLHD_EXPIRE_DATE.SelectedDate
                    If hidInsOrgID.Value <> "" Then
                        objData.INS_ORG_ID = hidInsOrgID.Value
                    End If
                    objData.REMARK = txtREMARK.Text

                    If Page.IsValid Then
                        If _Value.HasValue Then
                            objData.ID = hidID.Value
                            gID = Utilities.ObjToDecima(hidID.Value)
                            rep.UPDATE_INS_ACCIDENT_RISK(objData)
                        Else
                            rep.INSERT_INS_ACCIDENT_RISK(objData)
                        End If
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)

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
    ''' Load du lieu cho cac combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
           
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
                txtEMPLOYEE_NAME.Text = lstCommonEmployee(0).FULLNAME_VN

                Dim obj As New INS_ACCIDENT_RISKDTO
                obj = rep.GET_EMPLOYEE_ACCIDENT_RISK(lstCommonEmployee(0).EMPLOYEE_ID)
                If obj IsNot Nothing Then
                    txtJOB_NAME.Text = obj.JOB_NAME
                    rdBIRTH_DATE.SelectedDate = obj.BIRTH_DATE
                    txtGENDER_NAME.Text = obj.GENDER_NAME
                    txtADDRESS.Text = obj.ADDRESS
                    txtCONTRACT_NO.Text = obj.CONTRACT_NO
                    rdCONTRACT_SIGN_DATE.SelectedDate = obj.CONTRACT_SIGN_DATE
                    rdCONTRACT_START_DATE.SelectedDate = obj.CONTRACT_START_DATE
                    rdCONTRACT_EXPIRE_DATE.SelectedDate = obj.CONTRACT_EXPIRE_DATE
                    txtPLHD_CONTRACT_NO.Text = obj.PLHD_CONTRACT_NO
                    rdPLHD_SIGN_DATE.SelectedDate = obj.PLHD_SIGN_DATE
                    rdPLHD_START_DATE.SelectedDate = obj.PLHD_START_DATE
                    rdPLHD_EXPIRE_DATE.SelectedDate = obj.PLHD_EXPIRE_DATE
                    txtINS_ORG_NAME.Text = obj.INS_ORG_NAME
                    hidInsOrgID.Value = obj.INS_ORG_ID
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


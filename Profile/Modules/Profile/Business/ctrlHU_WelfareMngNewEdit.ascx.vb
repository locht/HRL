Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Common
Imports Telerik.Web.UI
Imports System.IO
Imports WebAppLog
Public Class ctrlHU_WelfareMngNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
#Region "Property"

    ''' <summary>
    ''' Obj ListComboData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj WelfareMng
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WelfareMng As WelfareMngDTO
        Get
            Return ViewState(Me.ID & "_WelfareMngDTO")
        End Get
        Set(ByVal value As WelfareMngDTO)
            ViewState(Me.ID & "_WelfareMngDTO") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj _Id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property _Id As Integer
        Get
            Return ViewState(Me.ID & "_Id")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_Id") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj popupID
    ''' 0 - normal
    ''' 1 - Employee
    ''' 2 - Sign
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 10/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Not Page.IsPostBack Then
                GetParams()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo giá trị cho các control trên page
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                        ToolbarItem.Seperator, ToolbarItem.Cancel)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            cboIS_TAXION.CausesValidation = False
            nmYear.CausesValidation = False
            cboWELFARE_ID.CausesValidation = False
            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            LoadControlPopup()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 09:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức bind dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            GetDataCombo()
            cboIS_TAXION.SelectedIndex = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 10/07/2017 10:10
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien selected cua control ctrlFindEmployeePopup_Employee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                hidIDEmp.Value = lstCommonEmployee(0).EMPLOYEE_ID
                txtEmployeeCode.Text = lstCommonEmployee(0).EMPLOYEE_CODE
                txtEMPLOYEE.Text = lstCommonEmployee(0).FULLNAME_VN
                txtORG.Text = lstCommonEmployee(0).ORG_NAME
                txtTITLE.Text = lstCommonEmployee(0).TITLE_NAME
                ' txtSTAFF_RANK.Text = lstCommonEmployee(0).STAFF_RANK_NAME
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:10
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho button btnFindEmployee
    ''' Hien thi popup co isLoadPopup = 1 khi click vao button
    ''' Cap nhat lai trang thai của cac control tren page
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFindEmployee_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindEmployee.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            LoadControlPopup()
            ctrlFindEmployeePopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:10
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện command tren toolbar khi click vao cac item cua no
    ''' Cac command la luu, huy 
    ''' Cập nhật lại trạng thái các control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim objdata As WelfareMngDTO
            Dim objList As New List(Of WelfareMngDTO)
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    objdata = New WelfareMngDTO
                    objdata.WELFARE_ID = Utilities.ObjToDecima(cboWELFARE_ID.SelectedValue)
                    objdata.EMPLOYEE_ID = hidIDEmp.Value
                    objdata.MONEY = rntxtAmount.Value
                    objdata.EFFECT_DATE = dpEFFECT_DATE.SelectedDate
                    objdata.SDESC = txtSDESC.Text
                    objdata.IS_TAXION = Utilities.ObjToDecima(cboIS_TAXION.SelectedValue)
                    If Utilities.ObjToDecima(cboIS_TAXION.SelectedValue) > 0 Then
                        If cboPeriod.SelectedValue = "" Then
                            ShowMessage(Translate("Bạn phải chọn kỳ lương"), NotifyType.Error)
                            Return
                        End If
                        objdata.PERIOD_ID = cboPeriod.SelectedValue
                    End If
                    objList.Add(objdata)
                    If CurrentState = CommonMessage.STATE_NEW Then
                        Dim rep As New ProfileBusinessRepository
                        If rep.InsertWelfareMng(objList) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            ''POPUPTOLINK
                            Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_WelfareMng&group=Business")

                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                        rep.Dispose()
                    ElseIf CurrentState = CommonMessage.STATE_EDIT Then
                        Dim rep As New ProfileBusinessRepository
                        objdata.ID = WelfareMng.ID
                        Dim lstID As New List(Of Decimal)
                        lstID.Add(objdata.ID)
                        If rep.ValidateBusiness("HU_WELFARE_MNG", "ID", lstID) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                            Exit Sub
                        End If
                        If rep.ModifyWelfareMng(objList) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            ''POPUPTOLINK
                            Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_WelfareMng&group=Business")

                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                        rep.Dispose()
                        'ChangeToolbarState()
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_WelfareMng&group=Business")
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:10
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện dữ liệu được đưa vào cho combobox cboWELFARE_ID
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cboWELFARE_ID_ItemDataBound(ByVal sender As Object, ByVal e As RadComboBoxItemEventArgs) Handles cboWELFARE_ID.ItemDataBound
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dataItem As WelfareListDTO = CType(e.Item.DataItem, WelfareListDTO)
        Try
            If dataItem.MONEY IsNot Nothing Then
                e.Item.Attributes("MONEY") = dataItem.MONEY
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:10
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện selectedIndexChanged cho combobox cboIS_TAXION
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboIS_TAXION_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboIS_TAXION.SelectedIndexChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Utilities.ObjToInt(cboIS_TAXION.SelectedValue) > 0 Then
                cboPeriod.Enabled = True
                nmYear.Enabled = True
                nmYear.Value = Date.Now.Year
                nmYear_TextChanged(Nothing, Nothing)
                cboPeriod.SelectedIndex = 0
            Else
                nmYear.Enabled = False
                cboPeriod.Enabled = False
                nmYear.ClearValue()
                cboPeriod.ClearValue()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:10
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện TextChanged cho textbox nmYear
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub nmYear_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles nmYear.TextChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If nmYear.Text.Length = 4 Then
                Using rep As New ProfileRepository
                    FillDropDownList(cboPeriod, rep.GetPeriodbyYear(nmYear.Value), "PERIOD_NAME", "ID", Common.Common.SystemLanguage, False, cboPeriod.SelectedValue)
                End Using
            Else
                cboPeriod.ClearValue()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:10
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện validation ngừng sử dụng cho combobox cboWELFARE_ID
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cusValWELFARE_ID_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusValWELFARE_ID.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New WelfareListDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboWELFARE_ID.SelectedValue
                validate.ACTFLG = "A"
                args.IsValid = rep.ValidateWelfareList(validate)
            End If
            If Not args.IsValid Then
                GetDataCombo()
            End If
            rep.Dispose()
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
    ''' 10/07/2017 10:20
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc load các control cho popup ctrlFindEmployeePopup
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadControlPopup()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Not phFindSign.Controls.Contains(ctrlFindEmployeePopup) Then
                ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                phFindSign.Controls.Add(ctrlFindEmployeePopup)
                ctrlFindEmployeePopup.MultiSelect = False
                ctrlFindEmployeePopup.LoadAllOrganization = False
                ctrlFindEmployeePopup.MustHaveContract = True
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:20
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc lấy về parameter "gUID"
    ''' Làm mới View hiện thời
    ''' Fill du lieu cho View nếu parameter là "gUID"
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Request.Params("gUID") IsNot Nothing Then
                _Id = Integer.Parse(Request.Params("gUID"))
                CurrentState = CommonMessage.STATE_EDIT
                LoadData()
            Else
                CurrentState = CommonMessage.STATE_NEW
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:20
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository
        Try
            ListComboData = New ComboBoxDataDTO
            ListComboData.GET_WELFARE = True
            rep.GetComboList(ListComboData)
            FillRadCombobox(cboWELFARE_ID, ListComboData.LIST_WELFARE, "NAME", "ID")
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:20
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc load dữ liệu cho các textbox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Dim rep As New ProfileBusinessRepository
        Try
            WelfareMng = rep.GetWelfareMngById(_Id)
            If Not WelfareMng Is Nothing Then
                cboWELFARE_ID.AutoPostBack = False
                hidIDEmp.Value = WelfareMng.EMPLOYEE_ID
                txtEmployeeCode.Text = WelfareMng.EMPLOYEE_CODE
                txtTITLE.Text = WelfareMng.TITLE_NAME
                txtEMPLOYEE.Text = WelfareMng.EMPLOYEE_NAME
                txtORG.Text = WelfareMng.ORG_NAME

                If Utilities.ObjToInt(WelfareMng.WELFARE_ID) > 0 Then
                    cboWELFARE_ID.SelectedValue = WelfareMng.WELFARE_ID
                End If

                rntxtAmount.Value = WelfareMng.MONEY
                dpEFFECT_DATE.SelectedDate = WelfareMng.EFFECT_DATE

                If Utilities.ObjToInt(WelfareMng.IS_TAXION) > 0 Then
                    cboIS_TAXION.SelectedValue = WelfareMng.IS_TAXION
                End If

                If Utilities.ObjToInt(WelfareMng.IS_TAXION) > 0 Then
                    nmYear.Enabled = True
                    cboPeriod.Enabled = True
                    nmYear.Value = WelfareMng.YEAR_PERIOD
                    nmYear_TextChanged(Nothing, Nothing)
                    If Utilities.ObjToInt(WelfareMng.PERIOD_ID) > 0 Then
                        cboPeriod.SelectedValue = WelfareMng.PERIOD_ID
                    End If

                Else
                    nmYear.Enabled = False
                    cboPeriod.Enabled = False
                    nmYear.ClearValue()
                    cboPeriod.ClearValue()
                End If
                txtSDESC.Text = WelfareMng.SDESC
                cboWELFARE_ID.AutoPostBack = True

                If WelfareMng.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                    MainToolBar.Items(0).Enabled = False
                    LeftPane.Enabled = False
                End If

            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region


End Class
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
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
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
    Property Employee_PL As List(Of Welfatemng_empDTO)
        Get
            Return ViewState(Me.ID & "_Employee_PL")
        End Get
        Set(value As List(Of Welfatemng_empDTO))
            ViewState(Me.ID & "_Employee_PL") = value
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
    Property checkDelete As Integer
        Get
            Return ViewState(Me.ID & "_checkDelete")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_checkDelete") = value
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
                '  GetParams()
            End If
            UpdateControlState()
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
        SetGridFilter(rgEmployee)
        AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
        AjaxManagerId = AjaxManager.ClientID
        rgEmployee.AllowCustomPaging = True
        rgEmployee.ClientSettings.EnablePostBackOnRowClick = False
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
            cboWELFARE_ID.CausesValidation = False
            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            'LoadControlPopup()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub rgEmployee_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rgEmployee.ItemCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case e.CommandName
                Case "FindEmployee"
                    isLoadPopup = 1
                    UpdateControlState()
                    ctrlFindEmployeePopup.Show()
                Case "DeleteEmployee"
                    For Each i As GridDataItem In rgEmployee.SelectedItems
                        Dim s = (From q In Employee_PL Where
                                 q.ID = i.GetDataKeyValue("ID")).FirstOrDefault
                        Employee_PL.Remove(s)
                    Next
                    '_result = False
                    checkDelete = 1
                    rgEmployee.Rebind()
            End Select
        Catch ex As Exception

        End Try
    End Sub
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Try
                If phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                    phFindEmp.Controls.Remove(ctrlFindEmployeePopup)
                    'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
                End If
                Select Case isLoadPopup
                    Case 1
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        ctrlFindEmployeePopup.MustHaveContract = True
                        ctrlFindEmployeePopup.IsOnlyWorkingWithoutTer = True
                        phFindEmp.Controls.Add(ctrlFindEmployeePopup)
                End Select
            Catch ex As Exception
                Throw ex
            End Try
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub



    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                If Employee_PL Is Nothing Then
                    Employee_PL = New List(Of Welfatemng_empDTO)
                End If
                For Each emp As CommonBusiness.EmployeePopupFindDTO In lstCommonEmployee
                    Dim employee As New Welfatemng_empDTO
                    employee.EMPLOYEE_CODE = emp.EMPLOYEE_CODE
                    employee.EMPLOYEE_ID = emp.EMPLOYEE_ID
                    employee.ID = emp.ID
                    employee.EMPLOYEE_NAME = emp.FULLNAME_VN
                    employee.ORG_NAME = emp.ORG_NAME
                    employee.TITLE_NAME = emp.TITLE_NAME
                    employee.ORG_ID = emp.ORG_ID
                    employee.TITLE_ID = emp.TITLE_ID
                    Using rep As New ProfileBusinessRepository
                        Dim dtdata = rep.GET_DETAILS_EMP(emp.ID)
                        If dtdata.Rows.Count > 0 Then
                            Dim total_child = dtdata(0)("TOTAL_CHILD").ToString()
                            Dim money_total = dtdata(0)("MONEY_TOTAL").ToString()
                            Dim money_pl = dtdata(0)("MONEY_PL").ToString()
                            Dim gender_id = dtdata(0)("GENDER_ID").ToString()
                            Dim contract_type = dtdata(0)("CONTRACT_ID").ToString()
                            Dim contract_name = dtdata(0)("CONTRACT_TYPE").ToString()
                            Dim seniority = dtdata(0)("SENIORITY").ToString()
                            Dim gender_name = dtdata(0)("GENDER_NAME").ToString()
                            employee.TOTAL_CHILD = total_child
                            employee.MONEY_TOTAL = money_total
                            employee.SENIORITY = seniority
                            employee.MONEY_PL = money_pl
                            employee.GENDER_NAME = gender_name
                            employee.GENDER_ID = gender_id
                            employee.CONTRACT_TYPE = contract_type
                            employee.CONTRACT_NAME = contract_name
                        End If
                    End Using
                    Employee_PL.Add(employee)
                Next
                '_result = False
                rgEmployee.Rebind()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
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
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    Private Sub ctrlFindEmployeePopup_CancelClicked(sender As Object, e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:10
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien selected cua control ctrlFindEmployeePopup_Employee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
    '    Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
    '        If lstCommonEmployee.Count <> 0 Then
    '            hidIDEmp.Value = lstCommonEmployee(0).EMPLOYEE_ID
    '            'txtEmployeeCode.Text = lstCommonEmployee(0).EMPLOYEE_CODE
    '            'txtEMPLOYEE.Text = lstCommonEmployee(0).FULLNAME_VN
    '            'txtORG.Text = lstCommonEmployee(0).ORG_NAME
    '            'txtTITLE.Text = lstCommonEmployee(0).TITLE_NAME
    '            ' txtSTAFF_RANK.Text = lstCommonEmployee(0).STAFF_RANK_NAME
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

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
    'Private Sub btnFindEmployee_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindEmployee.Click
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        LoadControlPopup()
    '        ctrlFindEmployeePopup.Show()
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try

    'End Sub

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
            Dim lstemp As New List(Of Welfatemng_empDTO)
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    objdata = New WelfareMngDTO
                    objdata.WELFARE_ID = cboWELFARE_ID.SelectedValue
                    'objdata.EMPLOYEE_ID = hidIDEmp.Value
                    objdata.EFFECT_DATE = dpEFFECT_DATE.SelectedDate
                    objdata.SDESC = txtSDESC.Text
                    For Each item In Employee_PL
                        Dim o As New Welfatemng_empDTO
                        o.EMPLOYEE_ID = item.EMPLOYEE_ID
                        o.EMPLOYEE_CODE = item.EMPLOYEE_CODE
                        o.GENDER_ID = item.GENDER_ID
                        o.TITLE_ID = item.TITLE_ID
                        o.ORG_ID = item.ORG_ID
                        o.TOTAL_CHILD = item.TOTAL_CHILD
                        o.MONEY_TOTAL = item.MONEY_TOTAL
                        o.MONEY_PL = item.MONEY_PL
                        o.CONTRACT_TYPE = item.CONTRACT_TYPE
                        o.WELFARE_ID = cboWELFARE_ID.SelectedValue
                        o.SENIORITY = item.SENIORITY
                        objdata.EMPLOYEE_ID = item.EMPLOYEE_ID
                        lstemp.Add(o)
                    Next
                    objdata.LST_WELFATE_EMP = lstemp
                    'objList.Add(objdata)
                    If CurrentState = CommonMessage.STATE_NEW Then
                        Dim rep As New ProfileBusinessRepository
                        If rep.InsertWelfareMng(objdata) Then
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
                        If rep.ModifyWelfareMng(objdata) Then
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


    ''' <lastupdate>
    ''' 10/07/2017 10:10
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện TextChanged cho textbox nmYear
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>


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
    'Sub LoadControlPopup()
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        If Not phFindSign.Controls.Contains(ctrlFindEmployeePopup) Then
    '            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
    '            phFindSign.Controls.Add(ctrlFindEmployeePopup)
    '            ctrlFindEmployeePopup.MultiSelect = False
    '            ctrlFindEmployeePopup.LoadAllOrganization = False
    '            ctrlFindEmployeePopup.MustHaveContract = True
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try

    'End Sub

    ''' <lastupdate>
    ''' 10/07/2017 10:20
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc lấy về parameter "gUID"
    ''' Làm mới View hiện thời
    ''' Fill du lieu cho View nếu parameter là "gUID"
    ''' </summary>
    ''' <remarks></remarks>
    'Private Sub GetParams()
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        If Request.Params("gUID") IsNot Nothing Then
    '            _Id = Integer.Parse(Request.Params("gUID"))
    '            CurrentState = CommonMessage.STATE_EDIT
    '            LoadData()
    '        Else
    '            CurrentState = CommonMessage.STATE_NEW
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        Throw ex
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

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
                'txtEmployeeCode.Text = WelfareMng.EMPLOYEE_CODE
                'txtTITLE.Text = WelfareMng.TITLE_NAME
                'txtEMPLOYEE.Text = WelfareMng.EMPLOYEE_NAME
                'txtORG.Text = WelfareMng.ORG_NAME
                If Utilities.ObjToInt(WelfareMng.WELFARE_ID) > 0 Then
                    cboWELFARE_ID.SelectedValue = WelfareMng.WELFARE_ID
                End If
                dpEFFECT_DATE.SelectedDate = WelfareMng.EFFECT_DATE
                txtSDESC.Text = WelfareMng.SDESC
                cboWELFARE_ID.AutoPostBack = True
                If WelfareMng.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                    MainToolBar.Items(0).Enabled = False
                    LeftPane.Enabled = False
                End If
                If checkDelete <> 1 Then
                    Employee_PL = rep.GetlistWelfareEMP(_Id)
                End If


            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region
    Private Sub rgEmployee_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEmployee.NeedDataSource
        Try
            Dim rep As New ProfileBusinessRepository
            If Request.Params("gUID") IsNot Nothing Then
                _Id = Integer.Parse(Request.Params("gUID"))
                CurrentState = CommonMessage.STATE_EDIT
                LoadData()

            Else
                CurrentState = CommonMessage.STATE_NEW
                If Not IsPostBack Then
                    Employee_PL = New List(Of Welfatemng_empDTO)
                End If
            End If
            rgEmployee.VirtualItemCount = Employee_PL.Count
            rgEmployee.DataSource = Employee_PL
        Catch ex As Exception

        End Try
    End Sub

End Class
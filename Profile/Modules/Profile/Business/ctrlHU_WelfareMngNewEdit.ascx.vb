Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Common
Imports Telerik.Web.UI
Imports System.IO
Imports WebAppLog
Imports System.Globalization
Imports HistaffFrameworkPublic

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
    Private Property dtTable As DataTable
        Get
            Return ViewState(Me.ID & "_dtTable")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtTable") = value
        End Set
    End Property
    Private Property dtbImport As DataTable
        Get
            Return PageViewState(Me.ID & "_dtbImport")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtbImport") = value
        End Set
    End Property
    Public Property dem As Integer
        Get
            Return ViewState(Me.ID & "_dem")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_dem") = value
        End Set
    End Property
    Private Property dtAllowList As DataTable
        Get
            Return PageViewState(Me.ID & "_dtAllowList")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtAllowList") = value
        End Set
    End Property
    Public Property ColumnImportWelfare As Integer
        Get
            Return ViewState(Me.ID & "_ColumnImportWelfare")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_ColumnImportWelfare") = value
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
    Property Total_money As Decimal
        Get
            Return ViewState(Me.ID & "_Total_money")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Total_money") = value
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
            Refresh()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Request.Params("gUID") IsNot Nothing Then
                _Id = Integer.Parse(Request.Params("gUID"))
                LoadData()
                rgEmployee.Rebind()
                For Each i As GridItem In rgEmployee.Items
                    i.Edit = True
                Next
                rgEmployee.Rebind()
            End If


        Catch ex As Exception

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
        ColumnImportWelfare = 11
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
                    If cboWELFARE_ID.SelectedValue <> "" And dpEFFECT_DATE.SelectedDate IsNot Nothing Then
                        ctrlFindEmployeePopup.Show()
                    Else
                        ShowMessage(Translate("chọn loại phúc lợi và ngày thanh toán"), NotifyType.Warning)
                        Exit Sub
                    End If
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
                        Dim dtdata = rep.GET_DETAILS_EMP(emp.ID, cboWELFARE_ID.SelectedValue, dpEFFECT_DATE.SelectedDate)
                        If dtdata.Rows.Count > 0 Then
                            Dim total_child = dtdata(0)("TOTAL_CHILD").ToString()
                            Dim money_total = dtdata(0)("MONEY_TOTAL").ToString()
                            Dim money_pl = dtdata(0)("MONEY_PL").ToString()
                            Dim gender_id = dtdata(0)("GENDER_ID").ToString()
                            Dim contract_type = dtdata(0)("CONTRACT_ID").ToString()
                            Dim contract_name = dtdata(0)("CONTRACT_TYPE").ToString()
                            Dim seniority = dtdata(0)("SENIORITY").ToString()
                            Dim gender_name = dtdata(0)("GENDER_NAME").ToString()
                            If total_child <> "" Then
                                employee.TOTAL_CHILD = Decimal.Parse(total_child)
                            End If
                            If money_total <> "" Then
                                employee.MONEY_TOTAL = Decimal.Parse(money_total)
                            End If
                            If money_pl <> "" Then
                                employee.MONEY_PL = Decimal.Parse(money_pl)
                            End If
                            employee.SENIORITY = seniority
                            employee.GENDER_NAME = gender_name
                            If gender_id <> "" Then
                                employee.GENDER_ID = Decimal.Parse(gender_id)
                            End If
                            If contract_type <> "" Then
                                employee.CONTRACT_TYPE = Decimal.Parse(contract_type)
                            End If
                            employee.CONTRACT_NAME = contract_name
                        End If
                    End Using
                    Employee_PL.Add(employee)
                Next
                '_result = False
                rgEmployee.Rebind()
                For Each i As GridItem In rgEmployee.Items
                    i.Edit = True
                Next
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
                    Dim ValidGrid As Tuple(Of Boolean, String)
                    ValidGrid = ValidateGrid_Emp()
                    If ValidateGrid_Emp.Item1 = False Then
                        ShowMessage(ValidateGrid_Emp.Item2, NotifyType.Warning)
                        Exit Sub
                    End If
                    'For Each items As GridDataItem In rgEmployee.Items
                    '    Dim txtMoney = CType(items.FindControl("rnMONEY"), RadNumericTextBox)
                    '    Dim o As New Welfatemng_empDTO
                    '    o.EMPLOYEE_ID = items.GetDataKeyValue("EMPLOYEE_ID")
                    '    o.EMPLOYEE_CODE = items.GetDataKeyValue("EMPLOYEE_CODE")
                    '    o.GENDER_ID = items.GetDataKeyValue("GENDER_ID")
                    '    o.GENDER_NAME = items.GetDataKeyValue("GENDER_NAME")
                    '    o.TITLE_ID = items.GetDataKeyValue("TITLE_ID")
                    '    o.ORG_ID = items.GetDataKeyValue("ORG_ID")
                    '    o.TOTAL_CHILD = items.GetDataKeyValue("TOTAL_CHILD")
                    '    o.MONEY_TOTAL = txtMoney.Value
                    '    o.MONEY_PL = items.GetDataKeyValue("MONEY_PL")
                    '    o.CONTRACT_TYPE = items.GetDataKeyValue("CONTRACT_TYPE")
                    '    o.CONTRACT_NAME = items.GetDataKeyValue("CONTRACT_NAME")
                    '    o.WELFARE_ID = cboWELFARE_ID.SelectedValue
                    '    o.SENIORITY = items.GetDataKeyValue("SENIORITY")
                    '    objdata.EMPLOYEE_ID = items.GetDataKeyValue("EMPLOYEE_ID")
                    '    lstemp.Add(o)
                    '    ' Total_money = txtMoney.Value
                    'Next
                    'For Each item In Employee_PL
                    '    Dim o As New Welfatemng_empDTO
                    '    o.EMPLOYEE_ID = item.EMPLOYEE_ID
                    '    o.EMPLOYEE_CODE = item.EMPLOYEE_CODE
                    '    o.GENDER_ID = item.GENDER_ID
                    '    o.GENDER_NAME = item.GENDER_NAME
                    '    o.TITLE_ID = item.TITLE_ID
                    '    o.ORG_ID = item.ORG_ID
                    '    o.TOTAL_CHILD = item.TOTAL_CHILD
                    '    o.MONEY_TOTAL = Total_money
                    '    o.MONEY_PL = item.MONEY_PL
                    '    o.CONTRACT_TYPE = item.CONTRACT_TYPE
                    '    o.CONTRACT_NAME = item.CONTRACT_NAME
                    '    o.WELFARE_ID = cboWELFARE_ID.SelectedValue
                    '    o.SENIORITY = item.SENIORITY
                    '    objdata.EMPLOYEE_ID = item.EMPLOYEE_ID
                    '    lstemp.Add(o)
                    'Next
                    Dim dtrgEmployee As DataTable = GetDataFromGrid(rgEmployee)
                    For Each row As DataRow In dtrgEmployee.Rows
                        Dim o As New Welfatemng_empDTO
                        o.EMPLOYEE_ID = If(row("EMPLOYEE_ID") <> "", Decimal.Parse(row("EMPLOYEE_ID")), Nothing)
                        o.EMPLOYEE_CODE = row("EMPLOYEE_CODE").ToString
                        o.GENDER_ID = If(row("GENDER_ID") <> "", Decimal.Parse(row("GENDER_ID")), Nothing)
                        o.GENDER_NAME = row("GENDER_NAME").ToString
                        o.TITLE_ID = If(row("TITLE_ID") <> "", Decimal.Parse(row("TITLE_ID")), Nothing)
                        o.ORG_ID = If(row("ORG_ID") <> "", Decimal.Parse(row("ORG_ID")), Nothing)
                        o.TOTAL_CHILD = If(row("TOTAL_CHILD") <> "", Decimal.Parse(row("TOTAL_CHILD")), Nothing)
                        o.MONEY_TOTAL = If(row("MONEY_TOTAL") <> "", Decimal.Parse(row("MONEY_TOTAL")), Nothing)
                        o.MONEY_PL = If(row("MONEY_PL") <> "", Decimal.Parse(row("MONEY_PL")), Nothing)
                        If IsDBNull(row("CONTRACT_TYPE")) = False Then
                            o.CONTRACT_TYPE = If(row("CONTRACT_TYPE") <> "", Decimal.Parse(row("CONTRACT_TYPE")), Nothing)
                        Else
                            o.CONTRACT_TYPE = Nothing
                        End If
                        o.CONTRACT_NAME = row("CONTRACT_NAME").ToString
                        o.WELFARE_ID = cboWELFARE_ID.SelectedValue
                        o.SENIORITY = row("SENIORITY").ToString
                        o.REMARK = row("REMARK").ToString
                        objdata.EMPLOYEE_ID = If(row("EMPLOYEE_ID") <> "", Decimal.Parse(row("EMPLOYEE_ID")), Nothing)
                        'o.MONEY = If(row("MONEY") <> "", Decimal.Parse(row("MONEY")), Nothing)
                        'o.COMMEND_PAY = If(row("COMMEND_PAY") <> "", Decimal.Parse(row("COMMEND_PAY")), Nothing)
                        'o.ORG_ID = If(row("ORG_ID") <> "", Decimal.Parse(row("ORG_ID")), Nothing)
                        'o.TITLE_ID = If(row("TITLE_ID") <> "", Decimal.Parse(row("TITLE_ID")), Nothing)
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
    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            Dim dtData As DataTable
            Using xls As New ExcelCommon
                dtData = Employee_PL.ToTable
                If dtData.Rows.Count = 0 Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                    Exit Sub
                ElseIf dtData.Rows.Count > 0 Then
                    rgEmployee.ExportExcel(Server, Response, dtData, "Welfare")
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                End If
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnImportFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportFile.Click
        Try
            ctrlUpload.isMultiple = False
            ctrlUpload.Show()
            CurrentState = CommonMessage.TOOLBARITEM_IMPORT
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked
        Import_Welfare()
    End Sub
    Private Sub Import_Welfare()
        Dim SumAllColImport As Integer
        Dim fileName As String
        Dim value As String
        Try
            '1. Đọc dữ liệu từ file Excel
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim countFile As Integer = ctrlUpload.UploadedFiles.Count
            Dim ds As New DataSet
            Dim dt As New DataTable
            Dim newRow1 As DataRow
            Dim newRow2 As DataRow
            'colStart = 0            'Bat dau doc tu cot
            'rowStart = 1            'Bat dau doc tu dong
            SumAllColImport = Count_All_Column() 'Tong so cot
            dtbImport = Employee_PL.ToTable()
            dtbImport.Clear()
            CreateDataTableUpdate()
            If countFile > 0 Then
                Dim file As UploadedFile = ctrlUpload.UploadedFiles(countFile - 1)
                fileName = System.IO.Path.Combine(savepath, file.FileName)
                '1.1 Lưu file lên server
                file.SaveAs(fileName, True)

                '2.1 Đọc dữ liệu trong file Excel
                Using ep As New ExcelPackage
                    ds = ep.ReadExcelToDataSet(fileName, False)
                End Using

                dt = ds.Tables(0)
                For i = 1 To dt.Rows.Count - 1
                    value = dt(i)("column1").ToString
                    If value <> "" Then
                        newRow1 = dtbImport.NewRow()
                        newRow2 = dt.NewRow()
                        newRow2 = dt(i)

                        For j = 0 To SumAllColImport - 1
                            newRow1(j + 1) = newRow2(j)
                        Next
                        dtbImport.Rows.Add(newRow1)
                    End If
                Next
                rgEmployee.DataSource = dtbImport
                dem = 0
                'AssignHeader()
                rgEmployee.DataBind()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Function CreateDataTableUpdate() As DataTable
        Try
            dtbImport = New DataTable
            dtbImport.TableName = "DATA"
            dtbImport.Columns.Add("EMPLOYEE_CODE", GetType(String))
            dtbImport.Columns.Add("EMPLOYEE_NAME", GetType(String))
            dtbImport.Columns.Add("ORG_NAME", GetType(String))
            dtbImport.Columns.Add("TITLE_NAME", GetType(String))
            dtbImport.Columns.Add("GENDER_NAME", GetType(String))
            dtbImport.Columns.Add("CONTRACT_NAME", GetType(String))
            dtbImport.Columns.Add("SENIORITY", GetType(String))
            dtbImport.Columns.Add("TOTAL_CHILD", GetType(Decimal))
            dtbImport.Columns.Add("MONEY_PL", GetType(Decimal))
            dtbImport.Columns.Add("MONEY_TOTAL", GetType(Decimal))
            dtbImport.Columns.Add("REMARK", GetType(String))
            Return dtbImport
        Catch ex As Exception

        End Try
    End Function
    Private Function Count_All_Column() As Integer
        If dtAllowList Is Nothing Then
            dtAllowList = Employee_PL.ToTable()
        End If

        Dim count As Integer
        Dim SumAllColImport As Integer

        count = dtAllowList.Rows.Count
        SumAllColImport = ColumnImportWelfare + count
        Return SumAllColImport
    End Function
    Private Function GetDataFromGrid(ByVal gr As RadGrid) As DataTable
        Dim bsSource As DataTable
        Try
            bsSource = New DataTable()
            For Each Col As GridColumn In gr.Columns
                Dim DataColumn As DataColumn = New DataColumn(Col.UniqueName)
                bsSource.Columns.Add(DataColumn)
            Next
            'coppy data to grid
            For Each Item As GridDataItem In gr.EditItems
                If Item.Display = False Then Continue For
                Dim Dr As DataRow = bsSource.NewRow()
                For Each col As GridColumn In gr.Columns
                    Try
                        If col.UniqueName = "cbStatus" Then
                            If Item.Selected = True Then
                                Dr(col.UniqueName) = 1
                            Else
                                Dr(col.UniqueName) = 0
                            End If
                            Continue For
                        End If
                        If InStr(",MONEY_TOTAL,REMARK,", "," + col.UniqueName + ",") > 0 Then
                            Select Case Item(col.UniqueName).Controls(1).ID.ToString.Substring(0, 2)
                                Case "cb"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadComboBox).SelectedValue
                                Case "rn"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox).Value
                                Case "rt"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadTextBox).Text.Trim
                                Case "rd"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadDatePicker).SelectedDate
                            End Select
                        Else
                            Dr(col.UniqueName) = Item.GetDataKeyValue(col.UniqueName)
                        End If
                    Catch ex As Exception
                        Continue For
                    End Try
                Next
                bsSource.Rows.Add(Dr)
            Next
            bsSource.AcceptChanges()
            Return bsSource
        Catch ex As Exception
        End Try
    End Function
    ''' <lastupdate>
    ''' 10/07/2017 10:10
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện dữ liệu được đưa vào cho combobox cboWELFARE_ID
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Protected Sub cboWELFARE_ID_ItemDataBound(ByVal sender As Object, ByVal e As RadComboBoxItemEventArgs) Handles cboWELFARE_ID.ItemDataBound
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim dataItem As WelfareListDTO = CType(e.Item.DataItem, WelfareListDTO)
    '    Try
    '        If dataItem.MONEY IsNot Nothing Then
    '            e.Item.Attributes("MONEY") = dataItem.MONEY
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try

    'End Sub
    Private Sub rgEmployee_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rgEmployee.ItemDataBound
        Try
            If e.Item.Edit Then
                Dim edit = CType(e.Item, GridEditableItem)
                Dim item As GridDataItem = CType(e.Item, GridDataItem)
                Dim rtxtmoney As New RadNumericTextBox
                rtxtmoney = CType(edit.FindControl("rnMONEY"), RadNumericTextBox)
                SetDataToGrid_Org(edit)
                edit.Dispose()
                item.Dispose()
            End If
        Catch ex As Exception

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
                If WelfareMng.EFFECT_DATE < Date.Now Then
                    MainToolBar.Items(0).Enabled = False
                    'LeftPane.Enabled = False
                End If
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

            Else
                CurrentState = CommonMessage.STATE_NEW
                If Not IsPostBack Then
                    Employee_PL = New List(Of Welfatemng_empDTO)
                End If
            End If
            'For Each i As GridItem In rgEmployee.Items
            '    i.Edit = True
            'Next
            rgEmployee.VirtualItemCount = Employee_PL.Count
            rgEmployee.DataSource = Employee_PL
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetDataToGrid_Org(ByVal EditItem As GridEditableItem)
        Try
            For Each col As GridColumn In rgEmployee.Columns
                Try
                    'Dim groupid
                    'For Each LINE In Employee_PL
                    '    groupid = LINE.GROUP_ID
                    'Next
                    Dim empployee_id = EditItem.GetDataKeyValue("EMPLOYEE_ID")
                    Dim rowData As Welfatemng_empDTO = Employee_PL.Find(Function(f) f.EMPLOYEE_ID = empployee_id)
                    If rowData Is Nothing Then
                        Exit Sub
                    End If
                    If InStr(",MONEY_TOTAL,REMARK,", "," + col.UniqueName + ",") > 0 Then
                        Select Case EditItem(col.UniqueName).Controls(1).ID.ToString.Substring(0, 2)
                            Case "cb"

                            Case "rt"
                                Dim radTextBox As New RadTextBox
                                radTextBox = CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadTextBox)
                                radTextBox.ClearValue()
                                radTextBox.Text = rowData.REMARK
                            Case "rn"
                                Dim radNumber As New RadNumericTextBox
                                radNumber = CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox)
                                radNumber.ClearValue()
                                radNumber.NumberFormat.AllowRounding = False
                                radNumber.NumberFormat.DecimalDigits = 2
                                radNumber.Text = rowData.MONEY_TOTAL.ToString()
                        End Select
                    Else
                        Continue For
                    End If
                Catch ex As Exception
                    Continue For
                End Try
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Function ValidateGrid_Emp() As Object
        Dim flag As Boolean = True
        Dim msgError As String = "Bạn chưa nhập đầy đủ thông tin. Vui lòng xem vị trí tô màu đỏ và gợi nhắc ở lưới."
        Try
            For Each items In rgEmployee.Items
                Dim txtMoney = CType(items.FindControl("rnMONEY"), RadNumericTextBox)
                If IsNumeric(txtMoney.Value) = False Then
                    txtMoney.BackColor = System.Drawing.Color.Red
                    txtMoney.ToolTip = "Bạn phải nhập mức thưởng"
                    flag = False
                End If
                Total_money = txtMoney.Value
            Next
        Catch ex As Exception
        End Try
        Dim Tuple As New Tuple(Of Boolean, String)(flag, msgError)
        Return Tuple
    End Function


End Class
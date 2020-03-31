Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Common
Imports Telerik.Web.UI
Imports System.IO
Imports WebAppLog
Imports System.Globalization
Imports HistaffFrameworkPublic

Public Class ctrlHU_SafeLaborMngNewEdit
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
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
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
    Property WelfareMng As SAFELABOR_MNGDTO
        Get
            Return ViewState(Me.ID & "_WelfareMngDTO")
        End Get
        Set(ByVal value As SAFELABOR_MNGDTO)
            ViewState(Me.ID & "_WelfareMngDTO") = value
        End Set
    End Property
    Property Employee_PL As List(Of SAFELABOR_MNG_EMPDTO)
        Get
            Return ViewState(Me.ID & "_Employee_PL")
        End Get
        Set(value As List(Of SAFELABOR_MNG_EMPDTO))
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
                Refresh()
            End If
            UpdateControlState()

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
        ColumnImportWelfare = 13
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
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
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
                    dtbImport = Employee_PL.ToTable()
                    rgEmployee.DataSource = dtbImport
                    rgEmployee_NeedDataSource(Nothing, Nothing)
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
                    Case 2
                        ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                        phFindOrg.Controls.Add(ctrlFindOrgPopup)
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
                    Employee_PL = New List(Of SAFELABOR_MNG_EMPDTO)
                End If
                For Each emp As CommonBusiness.EmployeePopupFindDTO In lstCommonEmployee
                    Dim employee As New SAFELABOR_MNG_EMPDTO
                    employee.EMPLOYEE_CODE = emp.EMPLOYEE_CODE
                    employee.EMPLOYEE_ID = emp.EMPLOYEE_ID
                    employee.ID = emp.ID
                    employee.EMPLOYEE_NAME = emp.FULLNAME_VN
                    employee.ORG_NAME = emp.ORG_NAME
                    employee.TITLE_NAME = emp.TITLE_NAME
                    Dim checkEmployeeCode As SAFELABOR_MNG_EMPDTO = Employee_PL.Find(Function(p) p.EMPLOYEE_CODE = emp.EMPLOYEE_CODE)
                    If (Not checkEmployeeCode Is Nothing) Then
                        Continue For
                    End If
                    Employee_PL.Add(employee)
                Next
                '_result = False
                dtbImport = Employee_PL.ToTable()
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
            Dim objdata As SAFELABOR_MNGDTO
            Dim objList As New List(Of SAFELABOR_MNGDTO)
            Dim lstemp As New List(Of SAFELABOR_MNG_EMPDTO)
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    objdata = New SAFELABOR_MNGDTO
                    objdata.CODE = txtCode.Text
                    objdata.NAME = txtName.Text
                    objdata.ORG_ID = hidOrg.Value
                    objdata.DATE_OCCUR = rdDateOccurAccident.SelectedDate
                    objdata.HOUR_OCCUR = rdDateOccurAccident.SelectedDate.Value.AddHours(Double.Parse(rtHourOccur.SelectedTime.Value.TotalHours))
                    If cboTypeAccident.SelectedValue <> "" Then
                        objdata.TYPE_ACCIDENT = cboTypeAccident.SelectedValue
                    End If
                    objdata.PLACE_ACCIDENT = txtPlaceAccident.Text
                    If cboReasonAccident.SelectedValue <> "" Then
                        objdata.REASON_ACCIDENT = cboReasonAccident.SelectedValue
                    End If
                    objdata.REASON_DETAIL = txtReasonDetail.Text
                    If IsNumeric(rnCost.Value) Then
                        objdata.COST_ACCIDENT = rnCost.Value
                    End If
                    objdata.REMARK = txtRemark.Text
                    'Dim ValidGrid As Tuple(Of Boolean, String)
                    'ValidGrid = ValidateGrid_Emp()
                    'If ValidateGrid_Emp.Item1 = False Then
                    '    ShowMessage(ValidateGrid_Emp.Item2, NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    Dim dtrgEmployee As DataTable = GetDataFromGrid(rgEmployee)
                    For Each row As DataRow In dtrgEmployee.Rows
                        Dim o As New SAFELABOR_MNG_EMPDTO
                        o.EMPLOYEE_ID = If(row("EMPLOYEE_ID") <> "", Decimal.Parse(row("EMPLOYEE_ID")), Nothing)
                        o.NUMBER_DATE = If(row("NUMBER_DATE") <> "", Decimal.Parse(row("NUMBER_DATE")), Nothing)
                        o.LEVEL_INJURED = row("LEVEL_INJURED").ToString
                        o.LEVEL_DECLINE = If(row("LEVEL_DECLINE") <> "", Decimal.Parse(row("LEVEL_DECLINE")), Nothing)
                        o.MONEY_MEDICAL = If(row("MONEY_MEDICAL") <> "", Decimal.Parse(row("MONEY_MEDICAL")), Nothing)
                        o.COST_SALARY = If(row("COST_SALARY") <> "", Decimal.Parse(row("COST_SALARY")), Nothing)
                        o.MONEY_INDEMNIFY = If(row("MONEY_INDEMNIFY") <> "", Decimal.Parse(row("MONEY_INDEMNIFY")), Nothing)
                        o.COMPANY_PAY = If(row("MONEY_MEDICAL") <> "", Decimal.Parse(row("MONEY_MEDICAL")), Nothing) + If(row("COST_SALARY") <> "", Decimal.Parse(row("COST_SALARY")), Nothing) + If(row("MONEY_INDEMNIFY") <> "", Decimal.Parse(row("MONEY_INDEMNIFY")), Nothing)
                        o.DATE_INS_PAY = If(row("DATE_INS_PAY") <> "", ToDate(row("DATE_INS_PAY")), Nothing)
                        o.MONEY_INS_PAY = If(row("MONEY_INS_PAY") <> "", Decimal.Parse(row("MONEY_INS_PAY")), Nothing)
                        o.MONEY_DIFFERENCE = o.COMPANY_PAY - If(row("MONEY_INS_PAY") <> "", Decimal.Parse(row("MONEY_INS_PAY")), Nothing)
                        o.REMARK = row("REMARK").ToString
                        lstemp.Add(o)
                    Next
                    objdata.LST_SAFELABOR_EMP = lstemp
                    'objList.Add(objdata)
                    If CurrentState = CommonMessage.STATE_NEW Then
                        Dim rep As New ProfileBusinessRepository
                        If rep.InsertSafeLaborMng(objdata) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            ''POPUPTOLINK
                            Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_SafeLaborMng&group=Business")
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
                        If rep.ModifySafeLaborMng(objdata) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            ''POPUPTOLINK
                            Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_SafeLaborMng&group=Business")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                        rep.Dispose()
                        'ChangeToolbarState()
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_SafeLaborMng&group=Business")
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

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
                        If InStr(",NUMBER_DATE,LEVEL_INJURED,LEVEL_DECLINE,MONEY_MEDICAL,COST_SALARY,MONEY_INDEMNIFY,COMPANY_PAY,DATE_INS_PAY,MONEY_INS_PAY,MONEY_DIFFERENCE,REMARK,", "," + col.UniqueName + ",") > 0 Then
                            Select Case Item(col.UniqueName).Controls(1).ID.ToString.Substring(0, 2)
                                Case "r1"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox).Value
                                Case "r2"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadTextBox).Text.Trim
                                Case "r3"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox).Value
                                Case "r4"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox).Value
                                Case "r5"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox).Value
                                Case "r6"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox).Value
                                Case "r7"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox).Value
                                Case "r8"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadDatePicker).SelectedDate
                                Case "r9"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox).Value
                                Case "rn"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox).Value
                                Case "rt"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadTextBox).Text.Trim
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

    Private Sub rgEmployee_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rgEmployee.ItemDataBound
        Try
            If e.Item.Edit Then
                Dim edit = CType(e.Item, GridEditableItem)
                Dim item As GridDataItem = CType(e.Item, GridDataItem)
                'Dim rtxtmoney As New RadNumericTextBox
                'rtxtmoney = CType(edit.FindControl("rnMONEY"), RadNumericTextBox)
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


#End Region

#Region "Custom"

    Private Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindOrg.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            isLoadPopup = 2
            UpdateControlState()
            ctrlFindOrgPopup.Show()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            Dim dtData As DataTable = Nothing
            If orgItem IsNot Nothing Then
                hidOrg.Value = e.CurrentValue
                txtOrgName.Text = orgItem.NAME_VN
                'txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
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
            Dim dtData As New DataTable
            dtData = rep.GetOtherList("REASON", False)
            FillRadCombobox(cboReasonAccident, dtData, "NAME", "ID", True)
            dtData = rep.GetOtherList("TYPE_ACCIDENT", False)
            FillRadCombobox(cboTypeAccident, dtData, "NAME", "ID", True)
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
            WelfareMng = rep.GetSafeLaborMngById(_Id)
            If Not WelfareMng Is Nothing Then
                txtCode.Text = WelfareMng.CODE
                txtName.Text = WelfareMng.NAME
                hidOrg.Value = WelfareMng.ORG_ID
                txtOrgName.Text = WelfareMng.ORG_NAME
                rdDateOccurAccident.SelectedDate = WelfareMng.DATE_OCCUR
                rtHourOccur.SelectedDate = WelfareMng.HOUR_OCCUR
                If WelfareMng.TYPE_ACCIDENT IsNot Nothing Then
                    cboTypeAccident.SelectedValue = WelfareMng.TYPE_ACCIDENT
                End If
                If WelfareMng.REASON_ACCIDENT IsNot Nothing Then
                    cboReasonAccident.SelectedValue = WelfareMng.REASON_ACCIDENT
                End If
                txtReasonDetail.Text = WelfareMng.REASON_DETAIL
                If WelfareMng.COST_ACCIDENT IsNot Nothing Then
                    rnCost.Value = WelfareMng.COST_ACCIDENT
                End If
                txtRemark.Text = WelfareMng.REMARK
                If checkDelete <> 1 Then
                    Dim repst = New ProfileStoreProcedure
                    dtbImport = repst.Get_list_Welfare_EMP(_Id)
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
                    Employee_PL = New List(Of SAFELABOR_MNG_EMPDTO)
                    dtbImport = Employee_PL.ToTable()
                End If
            End If
            rgEmployee.VirtualItemCount = dtbImport.Rows.Count 'Employee_PL.Count
            rgEmployee.DataSource = dtbImport

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
                    ' Employee_PL.Find(Function(f) f.EMPLOYEE_CODE = empployee_code)
                    Dim empployee_code = EditItem.GetDataKeyValue("EMPLOYEE_CODE")
                    'Dim ds = dtbImport.AsEnumerable().ToList()
                    Dim rowData = (From p In dtbImport Where p("EMPLOYEE_CODE") = empployee_code).ToList
                    If rowData Is Nothing Then
                        Exit Sub
                    End If
                    If InStr(",NUMBER_DATE,LEVEL_INJURED,LEVEL_DECLINE,MONEY_MEDICAL,COST_SALARY,MONEY_INDEMNIFY,COMPANY_PAY,DATE_INS_PAY,MONEY_INS_PAY,MONEY_DIFFERENCE,REMARK,", "," + col.UniqueName + ",") > 0 Then
                        Select Case EditItem(col.UniqueName).Controls(1).ID.ToString.Substring(0, 2)
                            Case "r1"
                                Dim radNumber As New RadNumericTextBox
                                radNumber = CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox)
                                radNumber.ClearValue()
                                radNumber.NumberFormat.AllowRounding = False
                                radNumber.NumberFormat.DecimalDigits = 2
                                radNumber.Text = rowData(0)("NUMBER_DATE").ToString()
                            Case "r2"
                                Dim radTextBox As New RadTextBox
                                radTextBox = CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadTextBox)
                                radTextBox.ClearValue()
                                radTextBox.Text = rowData(0)("LEVEL_INJURED")
                            Case "r3"
                                Dim radNumber As New RadNumericTextBox
                                radNumber = CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox)
                                radNumber.ClearValue()
                                radNumber.NumberFormat.AllowRounding = False
                                radNumber.NumberFormat.DecimalDigits = 2
                                radNumber.Text = rowData(0)("LEVEL_DECLINE").ToString()
                            Case "r4"
                                Dim radNumber As New RadNumericTextBox
                                radNumber = CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox)
                                radNumber.ClearValue()
                                radNumber.NumberFormat.AllowRounding = False
                                radNumber.NumberFormat.DecimalDigits = 2
                                radNumber.Text = rowData(0)("MONEY_MEDICAL").ToString()
                            Case "r5"
                                Dim radNumber As New RadNumericTextBox
                                radNumber = CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox)
                                radNumber.ClearValue()
                                radNumber.NumberFormat.AllowRounding = False
                                radNumber.NumberFormat.DecimalDigits = 2
                                radNumber.Text = rowData(0)("COST_SALARY").ToString()
                            Case "r6"
                                Dim radNumber As New RadNumericTextBox
                                radNumber = CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox)
                                radNumber.ClearValue()
                                radNumber.NumberFormat.AllowRounding = False
                                radNumber.NumberFormat.DecimalDigits = 2
                                radNumber.Text = rowData(0)("MONEY_INDEMNIFY").ToString()
                            Case "r7"
                                Dim radNumber As New RadNumericTextBox
                                radNumber = CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox)
                                radNumber.ClearValue()
                                radNumber.NumberFormat.AllowRounding = False
                                radNumber.NumberFormat.DecimalDigits = 2
                                radNumber.Text = rowData(0)("COMPANY_PAY").ToString()
                            Case "r8"
                                Dim radNumber As New RadDatePicker
                                radNumber = CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadDatePicker)
                                radNumber.ClearValue()
                                radNumber.SelectedDate = rowData(0)("DATE_INS_PAY")
                            Case "r9"
                                Dim radNumber As New RadNumericTextBox
                                radNumber = CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox)
                                radNumber.ClearValue()
                                radNumber.NumberFormat.AllowRounding = False
                                radNumber.NumberFormat.DecimalDigits = 2
                                radNumber.Text = rowData(0)("MONEY_INS_PAY").ToString()
                            Case "rn"
                                Dim radNumber As New RadNumericTextBox
                                radNumber = CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox)
                                radNumber.ClearValue()
                                radNumber.NumberFormat.AllowRounding = False
                                radNumber.NumberFormat.DecimalDigits = 2
                                radNumber.Text = rowData(0)("MONEY_DIFFERENCE").ToString()
                            Case "rt"
                                Dim radTextBox As New RadTextBox
                                radTextBox = CType(EditItem.FindControl(EditItem(col.UniqueName).Controls(1).ID.ToString), RadTextBox)
                                radTextBox.ClearValue()
                                radTextBox.Text = rowData(0)("REMARK")
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
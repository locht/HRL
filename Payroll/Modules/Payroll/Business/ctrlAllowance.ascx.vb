Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Common
Imports Telerik.Web.UI
Imports System.IO
Imports Profile
Imports System.Globalization

Public Class ctrlAllowance
    Inherits Common.CommonView
    Protected WithEvents TerminateView As ViewBase
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
#Region "Property"
    Dim dtData As New DataTable
    Property dtData1 As DataTable
        Get
            If ViewState(Me.ID & "_dtData1") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("FULLNAME_VN", GetType(String))
                dt.Columns.Add("TITLE_NAME", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("TYPE_ALLOW_NAME", GetType(String))
                dt.Columns.Add("TYPE_ALLOW_ID", GetType(String))
                dt.Columns.Add("AMOUNT", GetType(String))
                dt.Columns.Add("EFFECT_DATE", GetType(String))
                dt.Columns.Add("EXPIRE_DATE", GetType(String))
                dt.Columns.Add("REMARK", GetType(String))
                ViewState(Me.ID & "_dtData1") = dt
            End If
            Return ViewState(Me.ID & "_dtData1")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData1") = value
        End Set
    End Property
    Property dtDataImportWorking As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataImportWorking")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataImportWorking") = value
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
    Property vData As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataView")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataView") = value
        End Set
    End Property

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    'Public Property Allowance As List(Of AllowanceDTO)
    '    Get
    '        Return ViewState(Me.ID & "_Allowance")
    '    End Get
    '    Set(ByVal value As List(Of AllowanceDTO))
    '        ViewState(Me.ID & "_Allowance") = value
    '    End Set
    'End Property
    Property DeleteAllowance As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_DeleteAllowance")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_DeleteAllowance") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
            ctrlOrg.AutoPostBack = True
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrg.CheckBoxes = TreeNodeTypes.None
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgData.AllowCustomPaging = True
            rgData.SetFilter()
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            ctrlUpload.isMultiple = AsyncUpload.MultipleFileSelection.Disabled
            ctrlUpload.MaxFileInput = 1
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub GetDataCombobox()
        Dim rep As New PayrollRepository
        Try
            Dim objdata As List(Of AllowanceListDTO)
            Dim obj As New AllowanceListDTO
            obj.IS_CONTRACT = 1
            obj.ACTFLG = "A"
            objdata = rep.GetAllowanceList(obj)
            rep.Dispose()

            FillDropDownList(cboPhucap, objdata, "NAME", "ID", Common.Common.SystemLanguage, True)
            FillDropDownList(cbPhucap, objdata, "NAME", "ID", Common.Common.SystemLanguage, True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            GetDataCombobox()
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("EMPLOYEE_ID", hidEmp)
            dic.Add("EMPLOYEE_CODE", txtCode)
            dic.Add("FULLNAME_VN", txtTennhanvien)
            dic.Add("TITLE_NAME", txtTitle)
            dic.Add("ORG_NAME", txtOrgName)
            dic.Add("AMOUNT", txtSotien)
            dic.Add("REMARK", txtGhichu)
            dic.Add("EFFECT_DATE", dpTungay)
            dic.Add("ALLOWANCE_TYPE", cboPhucap)
            dic.Add("EXP_DATE", dpDenngay)
            dic.Add("ID", txtID)
            Utilities.OnClientRowSelectedChanged(rgData, dic)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New PayrollRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        SelectedItemDataGridByKey(rgData, IDSelect, , rgData.CurrentPageIndex)

                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                        SelectedItemDataGridByKey(rgData, IDSelect, )
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If
            rep.Dispose()
            UpdateControlState()
            ChangeToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Export, ToolbarItem.Next, ToolbarItem.Import,
                                       ToolbarItem.Delete)
            CType(Me.MainToolBar.Items(5), RadToolBarButton).Text = Translate("Xuất file mẫu")
            CType(Me.MainToolBar.Items(5), RadToolBarButton).ImageUrl = CType(Me.MainToolBar.Items(4), RadToolBarButton).ImageUrl
            CType(Me.MainToolBar.Items(6), RadToolBarButton).Text = Translate("Nhập file mẫu")
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"


    Protected Sub btnFindCommon_Click(ByVal sender As Object,
                                   ByVal e As EventArgs) Handles _
                               btnFindEmployee.Click
        Try
            UpdateControlState()
            ctrlFindEmployeePopup.Show()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New PayrollRepository
        Try
            If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                ctrlFindEmployeePopup.MultiSelect = False
                ctrlFindEmployeePopup.MustHaveContract = False
            End If
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgData, True)
                    EnableControlAll(False, cboPhucap, txtSotien, txtTennhanvien, txtGhichu, dpTungay, dpDenngay, btnFindEmployee, txtCode, txtTitle, txtOrgName)
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgData, False)
                    EnableControlAll(True, cboPhucap, txtSotien, txtGhichu, dpTungay, dpDenngay, btnFindEmployee)
                    ClearControlValue(cboPhucap, txtEmployeeCode, txtSotien, txtTennhanvien, txtGhichu, dpTungay, dpDenngay, txtCode, txtTitle, txtOrgName)
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgData, False)
                    EnableControlAll(True, cboPhucap, txtEmployeeCode, txtSotien, txtTennhanvien, txtGhichu, dpTungay, dpDenngay, btnFindEmployee, txtCode, txtTitle, txtOrgName)
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAllowance(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAllowance(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_DELETE
                    If rep.DeleteAllowance(DeleteAllowance) Then
                        DeleteAllowance = Nothing
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdateControlState()
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                    End If
            End Select
            rep.Dispose()
            ChangeToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Try
            Dim empID = ctrlFindEmployeePopup.SelectedEmployee(0)
            txtTennhanvien.Text = empID.FULLNAME_VN
            hidEmp.Value = empID.EMPLOYEE_ID
            txtCode.Text = empID.EMPLOYEE_CODE
            txtTitle.Text = empID.TITLE_NAME
            txtOrgName.Text = empID.ORG_NAME
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub




    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            'Dim rep As New PayrollRepository
            'Dim objAllowanceLis As New AllowanceListDTO
            Dim objAllowance As New AllowanceDTO
            Dim gID As Decimal
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstID As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next

                    DeleteAllowance = lstID
                    'Using rep As New PayrollRepository
                    '    If Not rep.CheckExistInDatabase(lstID, TABLE_NAME.PA_SALARY_GROUP) Then
                    '        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                    '        Return
                    '    End If
                    'End Using

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "Allowance")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objAllowance.EMPLOYEE_ID = hidEmp.Value
                        objAllowance.ALLOWANCE_TYPE = cboPhucap.SelectedValue
                        objAllowance.AMOUNT = txtSotien.Text

                        If Not dpTungay.SelectedDate Is Nothing Then
                            objAllowance.EFFECT_DATE = dpTungay.SelectedDate
                        Else
                            objAllowance.EFFECT_DATE = Nothing
                        End If
                        If Not dpDenngay.SelectedDate Is Nothing Then
                            objAllowance.EXP_DATE = dpDenngay.SelectedDate
                        Else
                            objAllowance.EXP_DATE = Nothing
                        End If

                        objAllowance.REMARK = txtGhichu.Text

                        Using rep As New PayrollRepository
                            Select Case CurrentState
                                Case CommonMessage.STATE_NEW
                                    objAllowance.ACTFLG = "A"
                                    If Not rep.CheckAllowance(hidEmp.Value, dpTungay.SelectedDate, cboPhucap.SelectedValue, 0) Then
                                        ShowMessage(Translate("Đã tồn tại loại phụ cấp này,xin kiểm tra lại"), NotifyType.Warning)
                                        Exit Sub
                                    End If
                                    If rep.InsertAllowance(objAllowance, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = gID
                                        Refresh("InsertView")
                                        UpdateControlState()
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                                Case CommonMessage.STATE_EDIT
                                    objAllowance.ID = rgData.SelectedValue
                                    If Not rep.CheckAllowance(hidEmp.Value, dpTungay.SelectedDate, cboPhucap.SelectedValue, objAllowance.ID) Then
                                        ShowMessage(Translate("Đã tồn tại loại phụ cấp này,xin kiểm tra lại"), NotifyType.Warning)
                                        Exit Sub
                                    End If
                                    If rep.ModifyAllowance(objAllowance, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = objAllowance.ID
                                        Refresh("UpdateView")
                                        UpdateControlState()
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                            End Select
                        End Using
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If

                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_NEXT
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Allowance')", True)
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    ClearControlValue(txtEmployeeCode, txtGhichu, txtSotien, cboPhucap, txtCode, txtTitle, txtOrgName, txtTennhanvien, dpTungay, dpDenngay)
                    UpdateControlState()
            End Select
            'UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_ACTIVE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
                UpdateControlState()
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Try

            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()
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



#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New PayrollRepository
        Dim _filter As New AllowanceDTO
        Dim objData As New List(Of AllowanceDTO)
        Try
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                               .IS_DISSOLVE = ctrlOrg.IsDissolve}
            _filter.IS_TER = chkNhanvienghiviec.Checked
            SetValueObjectByRadGrid(rgData, _filter)
            If rdTuNgay.SelectedDate IsNot Nothing Then
                _filter.EFFECT_DATE = rdTuNgay.SelectedDate
            End If

            If rdDenNgay.SelectedDate IsNot Nothing Then
                _filter.EXP_DATE = rdDenNgay.SelectedDate
            End If

            If cbPhucap.SelectedValue <> "" Then
                _filter.ALLOWANCE_TYPE = cbPhucap.SelectedValue
            End If
            If txtEmployeeCode.Text <> "" Then
                If Regex.IsMatch(txtEmployeeCode.Text, "[a-z]") Or Regex.IsMatch(txtEmployeeCode.Text, "[A-Z]") Then
                    _filter.FULLNAME_VN = txtEmployeeCode.Text
                Else
                    _filter.EMPLOYEE_CODE = txtEmployeeCode.Text
                End If
            End If
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetAllowance(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetAllowance(_filter, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    objData = rep.GetAllowance(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, "CREATED_DATE ASC")
                Else
                    objData = rep.GetAllowance(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
                End If
                'rgData.MasterTableView.FilterExpression = ""
                rgData.VirtualItemCount = MaximumRows
                rgData.DataSource = objData
            End If
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            CreateDataFilter()
            rgData.Rebind()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet

        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData1 = dtData1.Clone()
            TableMapping(dsDataPrepare.Tables(0))
            For Each rows As DataRow In dsDataPrepare.Tables(0).Select("EMPLOYEE_CODE<>'""'").CopyToDataTable.Rows
                If IsDBNull(rows("EMPLOYEE_CODE")) OrElse rows("EMPLOYEE_CODE") = "" Then Continue For
                Dim newRow As DataRow = dtData1.NewRow
                newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")
                newRow("FULLNAME_VN") = rows("FULLNAME_VN")
                newRow("TITLE_NAME") = rows("TITLE_NAME")
                newRow("ORG_NAME") = rows("ORG_NAME")
                newRow("TYPE_ALLOW_NAME") = rows("TYPE_ALLOW_NAME")
                newRow("TYPE_ALLOW_ID") = If(IsNumeric(rows("TYPE_ALLOW_ID")), rows("TYPE_ALLOW_ID"), 0)
                newRow("AMOUNT") = If(IsNumeric(rows("AMOUNT")), Decimal.Parse(rows("AMOUNT")), 0)
                newRow("EFFECT_DATE") = rows("EFFECT_DATE")
                newRow("EXPIRE_DATE") = rows("EXPIRE_DATE")
                newRow("REMARK") = rows("REMARK")
                dtData1.Rows.Add(newRow)
            Next
            dtData1.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData1.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim sp As New ProfileStoreProcedure()
                If sp.Import_HU_ALLOWANCE(LogHelper.GetUserLog().Username.ToUpper, DocXml) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
                'End edit;
                rgData.Rebind()
            End If

        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub
    Private Sub TableMapping(ByVal dtdata As DataTable)
        Dim row As DataRow = dtdata.Rows(1)
        Dim index As Integer = 0
        For Each cols As DataColumn In dtdata.Columns
            Try
                cols.ColumnName = row(index)
                index += 1
                If index > row.ItemArray.Length - 1 Then Exit For
            Catch ex As Exception
                Exit For
            End Try
        Next
        dtdata.Rows(0).Delete()
        dtdata.Rows(0).Delete()
        dtdata.AcceptChanges()
    End Sub
    Function loadToGrid() As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtError As New DataTable("ERROR")
        Try
           
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            dtDataImportWorking = dtData1.Clone
            dtError = dtData1.Clone
            Dim iRow = 1
            'Dim _filter As New WorkingDTO
            Dim rep As New ProfileBusinessRepository
            Dim atCall As New ProfileStoreProcedure
            'Dim IBusiness As IProfileBusiness = New ProfileBusinessClient()

            Dim empId As Integer
            For Each row As DataRow In dtData1.Rows
                rowError = dtError.NewRow
                isError = False
                Dim check = 0
                empId = rep.CheckEmployee_Exits(row("EMPLOYEE_CODE"))
                If empId = 0 Then
                    sError = "Mã nhân viên - Không tồn tại"
                    ImportValidate.IsValidTime("EMPLOYEE_CODE", row, rowError, isError, sError)
                    check = 1
                End If
                If row("TYPE_ALLOW_NAME") Is DBNull.Value OrElse row("TYPE_ALLOW_NAME") = "" Then
                    sError = "Chưa chọn LOẠI PHỤ CẤP"
                    ImportValidate.EmptyValue("TYPE_ALLOW_NAME", row, rowError, isError, sError)
                    check = 1
                End If
                If row("AMOUNT") = "" Or row("AMOUNT") = 0 Then
                    sError = "Chưa nhập số tiền"
                    ImportValidate.EmptyValue("AMOUNT", row, rowError, isError, sError)
                End If
                If CheckDate(row("EFFECT_DATE")) = False Or row("EFFECT_DATE") = "" Then
                    sError = "Ngày hiệu lực - không đúng định dạng"
                    ImportValidate.IsValidTime("EFFECT_DATE", row, rowError, isError, sError)
                    check = 1
                End If
                If CheckDate(row("EXPIRE_DATE")) = False Then
                    sError = "Ngày hết hiệu lực - không đúng định dạng"
                    ImportValidate.IsValidTime("EXPIRE_DATE", row, rowError, isError, sError)
                End If
                If check = 0 Then
                    Using rep1 As New PayrollRepository
                        Dim idNv = atCall.GET_ID_EMP(row("EMPLOYEE_CODE"))
                        If Not rep1.CheckAllowance(idNv, ToDate(row("EFFECT_DATE")), Decimal.Parse(row("TYPE_ALLOW_ID")), 0) Then
                            sError = "Đã tồn tại loại phụ cấp này,xin kiểm tra lại"
                            row("TYPE_ALLOW_NAME") = ""
                            ImportValidate.EmptyValue("TYPE_ALLOW_NAME", row, rowError, isError, sError)
                            isError = True
                        End If
                    End Using
                End If
                check = 0
                If isError Then
                    'rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    dtError.Rows.Add(rowError)
                Else
                    dtDataImportWorking.ImportRow(row)
                End If
                iRow = iRow + 1
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                ' gộp các lỗi vào 1 cột ghi chú 
                Dim dtErrorGroup As New DataTable
                Dim RowErrorGroup As DataRow
                dtErrorGroup.Columns.Add("STT")
                dtErrorGroup.Columns.Add("NOTE")
                For j As Integer = 0 To dtError.Rows.Count - 1
                    Dim strNote As String = String.Empty
                    RowErrorGroup = dtErrorGroup.NewRow
                    For k As Integer = 1 To dtError.Columns.Count - 1
                        If Not dtError.Rows(j)(k) Is DBNull.Value Then
                            strNote &= dtError.Rows(j)(k) & "\"
                        End If
                    Next
                    RowErrorGroup("STT") = dtError.Rows(j)("EMPLOYEE_CODE")
                    RowErrorGroup("NOTE") = strNote
                    dtErrorGroup.Rows.Add(RowErrorGroup)
                Next
                dtErrorGroup.TableName = "DATA"
                Session("EXPORTREPORT") = dtErrorGroup

                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_importIO_error');", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
            If isError OrElse (dtError IsNot Nothing AndAlso dtError.Rows.Count > 0) Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
        End Try
    End Function
    Private Function CheckDate(ByVal value As String) As Boolean
        Dim dateCheck As Boolean
        Dim result As Date
        If value = "" Or value = "&nbsp;" Then
            value = ""
            Return True
        End If
        Try
            dateCheck = DateTime.TryParseExact(value, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, result)
            Return dateCheck
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
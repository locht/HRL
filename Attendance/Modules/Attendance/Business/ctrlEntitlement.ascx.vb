Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Telerik.Web.UI
Imports Common.Common
Imports Common.CommonMessage
Imports Attendance.AttendanceRepository
Imports Attendance.AttendanceBusiness
Imports WebAppLog
Imports System.IO
Imports HistaffFrameworkPublic
Imports Profile
Imports Common.CommonBusiness

Public Class ctrlEntitlement
    Inherits Common.CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()
    Dim log As New UserLog
#Region "Properties"
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Public Property p_END_DATE As Date?
    Public Property Entitlement As List(Of AT_ENTITLEMENTDTO)
        Get
            Return ViewState(Me.ID & "_Entitlement_DTO")
        End Get
        Set(ByVal value As List(Of AT_ENTITLEMENTDTO))
            ViewState(Me.ID & "_Entitlement_DTO") = value
        End Set
    End Property
    Private Property CAL_DATE As Date
        Get
            Return ViewState(Me.ID & "_CAL_DATE")
        End Get
        Set(ByVal value As Date)
            ViewState(Me.ID & "_CAL_DATE") = value
        End Set
    End Property

    Private Property PERIOD As List(Of AT_PERIODDTO)
        Get
            Return ViewState(Me.ID & "_PERIOD")
        End Get
        Set(ByVal value As List(Of AT_PERIODDTO))
            ViewState(Me.ID & "_PERIOD") = value
        End Set
    End Property

    Private Property dtLogs As DataTable
        Get
            Return PageViewState(Me.ID & "_dtLogs")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtLogs") = value
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
        If Not Page.IsPostBack Then
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Dim startTime As DateTime = DateTime.UtcNow
            Try
                'If Not IsPostBack Then
                '    chkOnlyOrgSelected.Checked = False
                'End If
                ctrlOrganization.AutoPostBack = True
                ctrlOrganization.LoadDataAfterLoaded = True
                ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
                ctrlOrganization.CheckBoxes = TreeNodeTypes.None

                Refresh()
                _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
                DisplayException(Me.ViewName, Me.ID, ex)
            End Try
        End If
    End Sub
    ''' <summary>
    ''' Khởi tạo, load control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            SetGridFilter(rgEntitlement)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgEntitlement.AllowCustomPaging = True

            rgEntitlement.ClientSettings.EnablePostBackOnRowClick = False
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Khởi tạo load control, menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Calculate,
                                       ToolbarItem.Export,
                                       ToolbarItem.Import,
                                       ToolbarItem.Submit)
            MainToolBar.Items(0).Text = Translate("Tổng hợp")
            MainToolBar.Items(2).Text = Translate("Import số ngày ngoài cơ quan")
            MainToolBar.Items(3).Text = Translate("Kết phép")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load data to combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim lsData As List(Of AT_PERIODDTO)
            Dim rep As New AttendanceRepository
            Dim table As New DataTable
            table.Columns.Add("YEAR", GetType(Integer))
            table.Columns.Add("ID", GetType(Integer))
            Dim row As DataRow
            For index = 2015 To Date.Now.Year + 1
                row = table.NewRow
                row("ID") = index
                row("YEAR") = index
                table.Rows.Add(row)
            Next
            FillRadCombobox(cboYear, table, "YEAR", "ID")
            cboYear.SelectedValue = Date.Now.Year
            Dim period As New AT_PERIODDTO
            period.ORG_ID = 46
            period.YEAR = Date.Now.Year
            lsData = rep.LOAD_PERIODBylinq(period)
            Me.PERIOD = lsData
            FillRadCombobox(cboPeriod, lsData, "PERIOD_NAME", "PERIOD_ID", True)
            If lsData.Count > 0 Then
                Dim periodid = (From d In lsData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                Else
                    cboPeriod.SelectedIndex = lsData.Count - 1
                End If
                rgEntitlement.Rebind()
            Else
                rgEntitlement.VirtualItemCount = 0
                rgEntitlement.DataSource = New List(Of AT_ENTITLEMENTDTO)
                rgEntitlement.DataBind()
            End If
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
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgEntitlement.Rebind()
                        'SelectedItemDataGridByKey(rgDanhMuc, IDSelect, , rgDanhMuc.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgEntitlement.CurrentPageIndex = 0
                        rgEntitlement.MasterTableView.SortExpressions.Clear()
                        rgEntitlement.Rebind()
                        'SelectedItemDataGridByKey(rgDanhMuc, IDSelect, )
                    Case "Cancel"
                        rgEntitlement.MasterTableView.ClearSelectedItems()
                End Select
            End If
            UpdateControlState()
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
    ''' Event SeletedNodeChanged sơ đồ tổ chức
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        'If Not IsPostBack Then
        '    ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
        'End If
        Try
            rgEntitlement.CurrentPageIndex = 0
            rgEntitlement.Rebind()
        Catch ex As Exception

        End Try

    End Sub
    ''' <summary>
    ''' Event click item menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_DELETE
                    'Kiểm tra các điều kiện để xóa.
                    If rgEntitlement.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi xóa"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case TOOLBARTIEM_CALCULATE
                    Dim rep As New AttendanceRepository
                    If String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                        ShowMessage(Translate("Bạn phải chọn kỳ tính"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    Dim _param = New Attendance.AttendanceBusiness.ParamDTO With {.ORG_ID = ctrlOrganization.CurrentValue, _
                                                    .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue), _
                                                    .IS_DISSOLVE = ctrlOrganization.IsDissolve}
                    Dim lsEmployee As New List(Of Decimal?)
                    Dim employee_id As Decimal?
                    For Each items As GridDataItem In rgEntitlement.MasterTableView.GetSelectedItems()
                        Dim item = Decimal.Parse(items.GetDataKeyValue("EMPLOYEE_ID"))
                        employee_id = Decimal.Parse(item)
                        lsEmployee.Add(employee_id)
                    Next
                    If getSE_CASE_CONFIG("ctrlEntitlement_case1") > 0 Then
                        If rep.CALCULATE_ENTITLEMENT_HOSE(_param, lsEmployee) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            Refresh("UpdateView")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            Exit Sub
                        End If
                    Else
                        If rep.CALCULATE_ENTITLEMENT(_param, lsEmployee) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            Refresh("UpdateView")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            Exit Sub
                        End If
                    End If
                    

                Case TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgEntitlement.ExportExcel(Server, Response, dtDatas, "DataEntitlement")
                        End If
                    End Using
                Case TOOLBARITEM_IMPORT
                    If cboPeriod.SelectedValue Is Nothing Then
                        ShowMessage("Bạn phải chọn kỳ công", Utilities.NotifyType.Warning)
                        Return
                    End If
                    ctrlUpload.isMultiple = True
                    ctrlUpload.Show()

                Case TOOLBARITEM_SUBMIT
                    Dim rep As New AttendanceRepository
                    If String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                        ShowMessage(Translate("Bạn phải chọn kỳ công tính"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If

                    Dim periodnext As Integer

                    If rep.CheckPeriodMonth(cboYear.SelectedValue, cboPeriod.SelectedValue, periodnext) Then
                        ShowMessage(Translate("Bạn phải chọn kỳ công tháng 12 để kết phép."), Utilities.NotifyType.Error)
                        Exit Sub
                    End If

                    Dim _param1 = New Attendance.AttendanceBusiness.ParamDTO With {.ORG_ID = ctrlOrganization.CurrentValue, _
                                                        .PERIOD_ID = periodnext, _
                                                        .IS_DISSOLVE = ctrlOrganization.IsDissolve}
                    If Not rep.IS_PERIODSTATUS(_param1) Then
                        ShowMessage(Translate("Bảng Công tháng 1 năm sau đã đóng không thể thực hiện chức năng này."), Utilities.NotifyType.Error)
                        Exit Sub
                    End If


                    Dim _param = New Attendance.AttendanceBusiness.ParamDTO With {.ORG_ID = ctrlOrganization.CurrentValue, _
                                                    .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue), _
                                                    .IS_DISSOLVE = ctrlOrganization.IsDissolve}
                    Dim lsEmployee As New List(Of Decimal?)

                    If rep.AT_ENTITLEMENT_PREV_HAVE(_param, lsEmployee) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        Refresh("UpdateView")
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        Exit Sub
                    End If
            End Select
            ' 
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load data to grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateDataFilter(Optional ByVal isFull As Boolean = False)
        If cboPeriod.SelectedValue Is Nothing Then
            rgEntitlement.VirtualItemCount = 0
            rgEntitlement.DataSource = New List(Of AT_ENTITLEMENTDTO)
            rgEntitlement.DataBind()
            Return rgEntitlement.DataSource
        End If
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim obj As New AT_ENTITLEMENTDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgEntitlement, obj)
            Dim _param = New Attendance.AttendanceBusiness.ParamDTO With {.ORG_ID = ctrlOrganization.CurrentValue, _
                                            .IS_DISSOLVE = ctrlOrganization.IsDissolve,
                                            .IS_FULL = True}
            Dim Sorts As String = rgEntitlement.MasterTableView.SortExpressions.GetSortString()

            obj.YEAR = cboYear.SelectedValue
            obj.IS_TERMINATE = chkChecknghiViec.Checked
            obj.CAL_PERIOD = Decimal.Parse(cboPeriod.SelectedValue)
            'obj.END_DATE = p_END_DATE
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.Entitlement = rep.GetEntitlement(obj, _param, MaximumRows, rgEntitlement.CurrentPageIndex, rgEntitlement.PageSize, "CREATED_DATE desc")
                Else
                    Me.Entitlement = rep.GetEntitlement(obj, _param, MaximumRows, rgEntitlement.CurrentPageIndex, rgEntitlement.PageSize)
                End If
            Else
                Return rep.GetEntitlement(obj, _param).ToTable()
            End If

            rgEntitlement.VirtualItemCount = MaximumRows
            rgEntitlement.DataSource = Me.Entitlement
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
    ''' <summary>
    ''' Reload grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgEntitlement_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgEntitlement.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            CreateDataFilter(False)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked
        Try
            Import_Data()
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi"), NotifyType.Error)
        End Try

    End Sub

    Private Sub Import_Data()
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim rep As New AttendanceRepository
            log = LogHelper.GetUserLog
            Dim ds As New DataSet
            If countFile > 0 Then
                Dim file As UploadedFile = ctrlUpload.UploadedFiles(countFile - 1)
                fileName = System.IO.Path.Combine(savepath, file.FileName)
                file.SaveAs(fileName, True)
                Using ep As New ExcelPackage
                    ds = ep.ReadExcelToDataSet(fileName, False)
                End Using
            End If
            If ds Is Nothing Then
                Exit Sub
            End If
            TableMapping(ds.Tables(0))

            If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then
                Dim count As Integer = ds.Tables(0).Columns.Count - 12
                For i = 0 To count
                    If ds.Tables(0).Columns(i).ColumnName.Contains("Column") Then
                        ds.Tables(0).Columns.RemoveAt(i)
                        If ds.Tables(0).Columns(i).ColumnName.Contains("Column14") Then
                            ds.Tables(0).Columns.RemoveAt(i)
                            i = i + 1
                        Else
                            i = i - 1
                        End If

                    End If
                Next

                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    If rep.ImportEntitlementLeave(DocXml, log.Username, cboPeriod.SelectedValue) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                    Else
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Warning)
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
        dtTemp.Columns(0).ColumnName = "EMPLOYEE_CODE"
        dtTemp.Columns(6).ColumnName = "LEAVE"

        'XOA DONG TIEU DE VA HEADER
        dtTemp.Rows(0).Delete()
        dtTemp.Rows(1).Delete()
        dtTemp.Rows(2).Delete()
        dtTemp.Rows(3).Delete()

        ' add Log
        Dim _error As Boolean = True
        Dim count As Integer
        Dim newRow As DataRow
        Dim empId As Integer
        Dim rep As New ProfileBusinessRepository
        If dtLogs Is Nothing Then
            dtLogs = New DataTable("data")
            dtLogs.Columns.Add("ID", GetType(Integer))
            dtLogs.Columns.Add("EMPLOYEE_CODE", GetType(String))
            dtLogs.Columns.Add("DISCIPTION", GetType(String))
        End If
        dtLogs.Clear()

        'XOA NHUNG DONG DU LIEU NULL EMPLOYYE CODE
        Dim rowDel As DataRow
        For i As Integer = 0 To dtTemp.Rows.Count - 1
            If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
            rowDel = dtTemp.Rows(i)
            If rowDel("EMPLOYEE_CODE").ToString.Trim = "" Then
                dtTemp.Rows(i).Delete()
            End If
        Next

        For Each rows As DataRow In dtTemp.Rows
            If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For

            newRow = dtLogs.NewRow
            newRow("ID") = count + 1
            newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")

            empId = rep.CheckEmployee_Exits(rows("EMPLOYEE_CODE"))

            If empId = 0 Then
                newRow("DISCIPTION") = "Mã nhân viên - Không tồn tại,"
                _error = False
            Else
                rows("EMPLOYEE_CODE") = empId
            End If

            If IsDBNull(rows("LEAVE")) Then
                rows("LEAVE") = 0
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Chi phí cho 1 học viên (USD) - Bắt buộc nhập,"
                _error = False
            Else
                If Not (IsNumeric(rows("LEAVE"))) Then
                    rows("LEAVE") = 0
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Chi phí cho 1 học viên (USD) - Không đúng định dạng,"
                    _error = False
                End If
            End If

            If _error = False Then
                dtLogs.Rows.Add(newRow)
                _error = True
            End If
        Next
        dtTemp.AcceptChanges()
    End Sub
    ''' <summary>
    ''' Event click button tìm kiếm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            rgEntitlement.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgEntitlement_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgEntitlement.PageIndexChanged
        Try
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event SelectedIndexChanged combobox năm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtData As List(Of AT_PERIODDTO)
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO
        Try
            period.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            dtData = rep.LOAD_PERIODBylinq(period)
            cboPeriod.ClearSelection()
            FillRadCombobox(cboPeriod, dtData, "PERIOD_NAME", "PERIOD_ID", True)
            If dtData.Count > 0 Then
                Dim periodid = (From d In dtData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                Else
                    cboPeriod.SelectedIndex = dtData.Count - 1
                End If
                rgEntitlement.Rebind()
            Else
                rgEntitlement.VirtualItemCount = 0
                rgEntitlement.DataSource = New List(Of AT_ENTITLEMENTDTO)
                rgEntitlement.DataBind()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Event SelectedIndexChanged combobox kỳ công
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboPeriod_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPeriod.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            'Dim p = (From o In Me.PERIOD Where o.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)).FirstOrDefault
            'If p IsNot Nothing Then
            '    p_END_DATE = p.END_DATE
            'End If
            rgEntitlement.Rebind()
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
    ''' Event Request AjaxManager
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim url = e.Argument
            If (url.Contains("reload=1")) Then
                rgEntitlement.CurrentPageIndex = 0
                rgEntitlement.Rebind()
                If rgEntitlement.Items IsNot Nothing AndAlso rgEntitlement.Items.Count > 0 Then
                    rgEntitlement.Items(0).Selected = True
                End If
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
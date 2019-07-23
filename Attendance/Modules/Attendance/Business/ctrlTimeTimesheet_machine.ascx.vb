Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Telerik.Web.UI
Imports Common.Common
Imports Common.CommonMessage
Imports Attendance.AttendanceRepository
Imports Attendance.AttendanceBusiness
Imports WebAppLog
Public Class ctrlTimeTimesheet_machine
    Inherits Common.CommonView
    Private store As New CommonProcedureNew
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()
#Region "Property"
    Public Property TIME_TIMESHEET_MACHINETDTO As List(Of AT_TIME_TIMESHEET_MACHINETDTO)
        Get
            Return ViewState(Me.ID & "_TIME_TIMESHEET_MACHINETDTO")
        End Get
        Set(ByVal value As List(Of AT_TIME_TIMESHEET_MACHINETDTO))
            ViewState(Me.ID & "_TIME_TIMESHEET_MACHINETDTO") = value
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
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("WORKINGDAY", GetType(String))
                dt.Columns.Add("TIMEIN_REALITY", GetType(String))
                dt.Columns.Add("TIMEOUT_REALITY", GetType(String))
                dt.Columns.Add("NOTE", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
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
    End Sub
    ''' <summary>
    ''' Khởi tạo control, grid
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            SetGridFilter(rgTimeTimesheet_machine)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgTimeTimesheet_machine.AllowCustomPaging = True
            rgTimeTimesheet_machine.ClientSettings.EnablePostBackOnRowClick = False
            InitControl()
            If Not IsPostBack Then
                GirdConfig(rgTimeTimesheet_machine)
            End If
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
            period.ORG_ID = 1
            period.YEAR = Date.Now.Year
            lsData = rep.LOAD_PERIODBylinq(period)
            Me.PERIOD = lsData
            FillRadCombobox(cboPeriod, lsData, "PERIOD_NAME", "PERIOD_ID", True)
            If lsData.Count > 0 Then
                Dim periodid = (From d In lsData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                    rdtungay.SelectedDate = periodid.START_DATE
                    rdDenngay.SelectedDate = periodid.END_DATE
                Else
                    cboPeriod.SelectedValue = lsData.Item(0).PERIOD_ID.ToString()
                    rdtungay.SelectedDate = lsData.Item(0).START_DATE
                    rdDenngay.SelectedDate = lsData.Item(0).END_DATE
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load, khởi tạo menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Calculate,
                                       ToolbarItem.Export)

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_TEMP",
                                                                     ToolbarIcons.Export,
                                                                     ToolbarAuthorize.Export,
                                                                     Translate("Xuất file mẫu")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP",
                                                                     ToolbarIcons.Import,
                                                                     ToolbarAuthorize.Import,
                                                                     Translate("Nhập file mẫu")))
            MainToolBar.Items(0).Text = Translate("Tổng hợp")
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
                        rgTimeTimesheet_machine.Rebind()
                        'SelectedItemDataGridByKey(rgDanhMuc, IDSelect, , rgDanhMuc.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgTimeTimesheet_machine.CurrentPageIndex = 0
                        rgTimeTimesheet_machine.MasterTableView.SortExpressions.Clear()
                        rgTimeTimesheet_machine.Rebind()
                        'SelectedItemDataGridByKey(rgDanhMuc, IDSelect, )
                    Case "Cancel"
                        rgTimeTimesheet_machine.MasterTableView.ClearSelectedItems()
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
    ''' Event SelectedNodeChanged sơ đồ tổ chức
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If Not IsPostBack Then
                ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
            End If
            rgTimeTimesheet_machine.CurrentPageIndex = 0
            rgTimeTimesheet_machine.Rebind()
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
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim rep As New AttendanceRepository

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARTIEM_CALCULATE
                    Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue), _
                                                   .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
                                                    .IS_DISSOLVE = ctrlOrganization.IsDissolve}
                    If ctrlOrganization.IsDissolve = True Then
                        _param.IS_DISSOLVE = True
                    Else
                        _param.IS_DISSOLVE = False
                    End If
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    If Not rdtungay.SelectedDate.HasValue Or Not rdDenngay.SelectedDate.HasValue Then
                        ShowMessage(Translate("Từ ngày đến ngày chưa được chọn"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    'LAY THONG TIN CONFIG CASE :
                    Dim lsEmployee As New List(Of Decimal?)
                    Dim employee_id As Decimal?
                    For Each items As GridDataItem In rgTimeTimesheet_machine.MasterTableView.GetSelectedItems()
                        Dim item = Decimal.Parse(items.GetDataKeyValue("EMPLOYEE_ID"))
                        employee_id = Decimal.Parse(item)
                        lsEmployee.Add(employee_id)
                    Next
                    If getSE_CASE_CONFIG("ctrlTimeTimesheet_machine_case1") > 0 Then
                        rep.Init_TimeTImesheetMachines(_param, rdtungay.SelectedDate, rdDenngay.SelectedDate,
                                                   Decimal.Parse(ctrlOrganization.CurrentValue), lsEmployee, 0, "ctrlTimeTimesheet_machine_case1")
                        Refresh("UpdateView")
                    Else
                        rep.Init_TimeTImesheetMachines(_param, rdtungay.SelectedDate, rdDenngay.SelectedDate,
                                                   Decimal.Parse(ctrlOrganization.CurrentValue), lsEmployee, 0, "")
                        Refresh("UpdateView")
                    End If
                    
                    'If rep.Init_TimeTImesheetMachines(_param, rdtungay.SelectedDate, rdDenngay.SelectedDate,
                    '                               Decimal.Parse(ctrlOrganization.CurrentValue), lsEmployee) Then
                    '    Refresh("UpdateView")
                    'Else
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Success)
                    '    Exit Sub
                    'End If
                Case TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgTimeTimesheet_machine.ExportExcel(Server, Response, dtDatas, "DataTimeSheetMachine")
                        End If
                    End Using
                Case "IMPORT_TEMP"
                    ctrlUpload1.Show()
                Case "EXPORT_TEMP"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Timesheet_machineExport&PERIOD_ID=" & "1" & "&orgid=" & "1" & "&IS_DISSOLVE=" & IIf(1, "1", "0") & "');", True)
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
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim obj As New AT_TIME_TIMESHEET_MACHINETDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgTimeTimesheet_machine, obj)
            Dim _param = New ParamDTO With {.ORG_ID = ctrlOrganization.CurrentValue, _
                                            .IS_DISSOLVE = ctrlOrganization.IsDissolve,
                                            .IS_FULL = True}
            Dim Sorts As String = rgTimeTimesheet_machine.MasterTableView.SortExpressions.GetSortString()

            If rdtungay.SelectedDate IsNot Nothing Then
                obj.FROM_DATE = rdtungay.SelectedDate
            End If
            If rdDenngay.SelectedDate IsNot Nothing Then
                obj.END_DATE = rdDenngay.SelectedDate
            End If
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.TIME_TIMESHEET_MACHINETDTO = rep.GetMachines(obj, _param, MaximumRows, rgTimeTimesheet_machine.CurrentPageIndex, rgTimeTimesheet_machine.PageSize, Sorts) '"CREATED_DATE desc")
                Else
                    Me.TIME_TIMESHEET_MACHINETDTO = rep.GetMachines(obj, _param, MaximumRows, rgTimeTimesheet_machine.CurrentPageIndex, rgTimeTimesheet_machine.PageSize)
                End If
            Else
                Return rep.GetMachines(obj, _param).ToTable()
            End If

            rgTimeTimesheet_machine.VirtualItemCount = MaximumRows
            rgTimeTimesheet_machine.DataSource = Me.TIME_TIMESHEET_MACHINETDTO
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
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
            rgTimeTimesheet_machine.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event SelectedIndexChange combobox năm
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
                    rdtungay.SelectedDate = periodid.START_DATE
                    rdDenngay.SelectedDate = periodid.END_DATE
                    rgTimeTimesheet_machine.Rebind()
                Else
                    cboPeriod.SelectedIndex = 0
                    Dim per = (From c In dtData Where c.PERIOD_ID = cboPeriod.SelectedValue).FirstOrDefault
                    rdtungay.SelectedDate = per.START_DATE
                    rdDenngay.SelectedDate = per.END_DATE
                End If
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
    Private Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriod.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            period.ORG_ID = 46
            If cboPeriod.SelectedValue <> "" Then
                Dim Lstperiod = rep.LOAD_PERIODBylinq(period)

                Dim p = (From o In Lstperiod Where o.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)).FirstOrDefault
                rdtungay.SelectedDate = p.START_DATE
                rdDenngay.SelectedDate = p.END_DATE
            End If
            'rgTimeTimesheet_machine.Rebind()
            ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Reload grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgTimeTimesheet_machine.NeedDataSource
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
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgTimeTimesheet_machine_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgTimeTimesheet_machine.PageIndexChanged
        Try
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event Request Ajax
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
                rgTimeTimesheet_machine.CurrentPageIndex = 0
                rgTimeTimesheet_machine.Rebind()
                If rgTimeTimesheet_machine.Items IsNot Nothing AndAlso rgTimeTimesheet_machine.Items.Count > 0 Then
                    rgTimeTimesheet_machine.Items(0).Selected = True
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện OkClicked của ctrlUpload1
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Dim dtDataHeader As DataTable = New DataTable()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xlsx")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                If workbook.Worksheets.GetSheetByCodeName("DATA") Is Nothing Then
                    ShowMessage(Translate("File mẫu không đúng định dạng"), NotifyType.Warning)
                    Exit Sub
                End If
                worksheet = workbook.Worksheets(0)
                dtDataHeader = worksheet.Cells.ExportDataTableAsString(0, 0, 2, worksheet.Cells.MaxColumn + 1, True)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(1, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            'dtDataHeader.Rows.RemoveAt(0)
            For col As Integer = 0 To dtDataHeader.Columns.Count - 1
                Dim colName = dtDataHeader.Rows(0)(col)
                If colName IsNot DBNull.Value Then
                    dtDataHeader.Columns(col).ColumnName = colName
                End If
            Next
            dtDataHeader.Rows.RemoveAt(0)
            For Each dt As DataTable In dsDataPrepare.Tables
                For Each row In dt.Rows
                    Dim isRow = ImportValidate.TrimRow(row)
                    If isRow Then
                        dtData.ImportRow(row)
                    End If
                Next
            Next
            If loadToGrid(dtDataHeader) = False Then
            Else
                Dim lstobjUdp As New List(Of AT_TIME_TIMESHEET_MACHINETDTO)
                Dim objUdp As AT_TIME_TIMESHEET_MACHINETDTO
                For Each row As DataRow In dtData.Rows
                    objUdp = New AT_TIME_TIMESHEET_MACHINETDTO
                    objUdp.EMPLOYEE_CODE = row("EMPLOYEE_CODE")
                    Dim working_day As Date
                    Dim strdate = row("WORKINGDAY").ToString.Split("/")
                    Dim strDateTimeIn = row("TIMEIN_REALITY").ToString().Split(":")
                    Dim strDateTimeOut = row("TIMEOUT_REALITY").ToString().Split(":")
                    If strdate.Length <> 3 Then Continue For
                    working_day = New Date(CDec(strdate(2)), CDec(strdate(1)), CDec(strdate(0)))
                    objUdp.WORKINGDAY = working_day
                    If strDateTimeIn.Length = 2 Then
                        objUdp.TIMEIN_REALITY = New Date(CDec(strdate(2)), CDec(strdate(1)), CDec(strdate(0)), CDec(strDateTimeIn(0)), CDec(strDateTimeIn(1)), 0)
                    End If
                    If strDateTimeOut.Length = 2 Then
                        objUdp.TIMEOUT_REALITY = New Date(CDec(strdate(2)), CDec(strdate(1)), CDec(strdate(0)), CDec(strDateTimeOut(0)), CDec(strDateTimeOut(1)), 0)
                    End If
                    objUdp.NOTE = row("NOTE")
                    lstobjUdp.Add(objUdp)
                Next
                Using rep As New AttendanceRepository
                    If rep.Upd_TimeTImesheetMachines(lstobjUdp) Then
                        Refresh("InsertView")
                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdateControlState()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
                End Using
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        Finally
            dsDataPrepare.Dispose()
            dtDataHeader.Dispose()
        End Try
    End Sub
#End Region
#Region "FUNCTION/PROCEDURE"
    Function loadToGrid(dtDataHeader As DataTable) As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO
        Dim rowError As DataRow
        Dim isError As Boolean = False
        Dim sError As String = String.Empty
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dtData.Rows.Count = 0 Then
                ShowMessage("File mẫu không đúng định dạng", NotifyType.Warning)
                Return False
            End If
            dtError = dtData.Clone
            For Each row As DataRow In dtData.Rows
                Dim isRow = ImportValidate.TrimRow(row)
                If Not isRow Then
                    Continue For
                End If
                isError = False
                ImportValidate.TrimRow(row)
                rowError = dtError.NewRow
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)
                If isError Then
                    rowError("STT") = row("STT")
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    rowError("WORKINGDAY") = row("WORKINGDAY").ToString
                    rowError("TIMEIN_REALITY") = row("TIMEIN_REALITY").ToString
                    rowError("TIMEOUT_REALITY") = row("TIMEOUT_REALITY").ToString
                    rowError("NOTE") = row("NOTE").ToString
                    dtError.Rows.Add(rowError)
                End If
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                dtDataHeader.TableName = "DATA_HEADER"
                Dim dsData As New DataSet
                dsData.Tables.Add(dtError)
                dsData.Tables.Add(dtDataHeader)
                Session("EXPORTREPORT") = dsData
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_GiaiTrinhNgayCong_error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Return False
            End If
            Return True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
#End Region
End Class
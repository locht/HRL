Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Telerik.Web.UI
Imports Common.Common
Imports Common.CommonMessage
Imports Attendance.AttendanceRepository
Imports Attendance.AttendanceBusiness
Imports WebAppLog
Public Class ctrlTime_Timesheet_CTT
    Inherits Common.CommonView
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()
#Region "Property"
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("EMPLOYEE_ID", GetType(String))
                dt.Columns.Add("VN_FULLNAME", GetType(String))
                dt.Columns.Add("TITLE_NAME", GetType(String))
                dt.Columns.Add("ORG_ID", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("ORG_PATH", GetType(String))
                dt.Columns.Add("STAFF_RANK_NAME", GetType(String))
                dt.Columns.Add("D1", GetType(String))
                dt.Columns.Add("D2", GetType(String))
                dt.Columns.Add("D3", GetType(String))
                dt.Columns.Add("D4", GetType(String))
                dt.Columns.Add("D5", GetType(String))
                dt.Columns.Add("D6", GetType(String))
                dt.Columns.Add("D7", GetType(String))
                dt.Columns.Add("D8", GetType(String))
                dt.Columns.Add("D9", GetType(String))
                dt.Columns.Add("D10", GetType(String))
                dt.Columns.Add("D11", GetType(String))
                dt.Columns.Add("D12", GetType(String))
                dt.Columns.Add("D13", GetType(String))
                dt.Columns.Add("D14", GetType(String))
                dt.Columns.Add("D15", GetType(String))
                dt.Columns.Add("D16", GetType(String))
                dt.Columns.Add("D17", GetType(String))
                dt.Columns.Add("D18", GetType(String))
                dt.Columns.Add("D19", GetType(String))
                dt.Columns.Add("D20", GetType(String))
                dt.Columns.Add("D21", GetType(String))
                dt.Columns.Add("D22", GetType(String))
                dt.Columns.Add("D23", GetType(String))
                dt.Columns.Add("D24", GetType(String))
                dt.Columns.Add("D25", GetType(String))
                dt.Columns.Add("D26", GetType(String))
                dt.Columns.Add("D27", GetType(String))
                dt.Columns.Add("D28", GetType(String))
                dt.Columns.Add("D29", GetType(String))
                dt.Columns.Add("D30", GetType(String))
                dt.Columns.Add("D31", GetType(String))

                dt.Columns.Add("D1_COLOR", GetType(String))
                dt.Columns.Add("D2_COLOR", GetType(String))
                dt.Columns.Add("D3_COLOR", GetType(String))
                dt.Columns.Add("D4_COLOR", GetType(String))
                dt.Columns.Add("D5_COLOR", GetType(String))
                dt.Columns.Add("D6_COLOR", GetType(String))
                dt.Columns.Add("D7_COLOR", GetType(String))
                dt.Columns.Add("D8_COLOR", GetType(String))
                dt.Columns.Add("D9_COLOR", GetType(String))
                dt.Columns.Add("D10_COLOR", GetType(String))
                dt.Columns.Add("D11_COLOR", GetType(String))
                dt.Columns.Add("D12_COLOR", GetType(String))
                dt.Columns.Add("D13_COLOR", GetType(String))
                dt.Columns.Add("D14_COLOR", GetType(String))
                dt.Columns.Add("D15_COLOR", GetType(String))
                dt.Columns.Add("D16_COLOR", GetType(String))
                dt.Columns.Add("D17_COLOR", GetType(String))
                dt.Columns.Add("D18_COLOR", GetType(String))
                dt.Columns.Add("D19_COLOR", GetType(String))
                dt.Columns.Add("D20_COLOR", GetType(String))
                dt.Columns.Add("D21_COLOR", GetType(String))
                dt.Columns.Add("D22_COLOR", GetType(String))
                dt.Columns.Add("D23_COLOR", GetType(String))
                dt.Columns.Add("D24_COLOR", GetType(String))
                dt.Columns.Add("D25_COLOR", GetType(String))
                dt.Columns.Add("D26_COLOR", GetType(String))
                dt.Columns.Add("D27_COLOR", GetType(String))
                dt.Columns.Add("D28_COLOR", GetType(String))
                dt.Columns.Add("D29_COLOR", GetType(String))
                dt.Columns.Add("D30_COLOR", GetType(String))
                dt.Columns.Add("D31_COLOR", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    Public Property TIME_TIMESHEET_DAILYDTO As List(Of AT_TIME_TIMESHEET_DAILYDTO)
        Get
            Return ViewState(Me.ID & "_TIME_TIMESHEET_DAILYDTO")
        End Get
        Set(ByVal value As List(Of AT_TIME_TIMESHEET_DAILYDTO))
            ViewState(Me.ID & "_TIME_TIMESHEET_DAILYDTO") = value
        End Set
    End Property
    Public Property dtTIME_TIMESHEET As DataSet
        Get
            Return ViewState(Me.ID & "_dtTIME_TIMESHEET")
        End Get
        Set(ByVal value As DataSet)
            ViewState(Me.ID & "_dtTIME_TIMESHEET") = value
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
    ''' <summary>
    ''' Khởi tạo, load page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            rgTimeTimesheet_cct.SetFilter()
            rgTimeTimesheet_cct.AllowCustomPaging = True
            rgTimeTimesheet_cct.ClientSettings.EnablePostBackOnRowClick = False
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
    ''' Khởi tạo control, popup
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            ctrlUpload1.isMultiple = AsyncUpload.MultipleFileSelection.Disabled
            ctrlUpload1.MaxFileInput = 1
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Khởi tạo menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar

            'Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit, ToolbarItem.Export)
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Export)

            'MainToolBar.Items(0).Text = Translate("Tổng hợp")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_TEMP",
                                                                  ToolbarIcons.Export,
                                                                  ToolbarAuthorize.Export,
                                                                  Translate("Xuất file mẫu")))

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP",
                                                                     ToolbarIcons.Import,
                                                                     ToolbarAuthorize.Import,
                                                                     Translate("Nhập file mẫu")))

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_ORIGIN",
                                                                  ToolbarIcons.Export,
                                                                  ToolbarAuthorize.Export,
                                                                  Translate("Xuất bảng công gốc")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("CALCULATE",
                                                                  ToolbarIcons.Calculator,
                                                                  ToolbarAuthorize.None,
                                                                  Translate("Tổng hợp")))

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
                    cboPeriod.SelectedIndex = 0
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
    ''' REload page
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
                        rgTimeTimesheet_cct.Rebind()
                        'SelectedItemDataGridByKey(rgDeclareiTimeRice, IDSelect, , rgDeclareiTimeRice.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgTimeTimesheet_cct.CurrentPageIndex = 0
                        rgTimeTimesheet_cct.MasterTableView.SortExpressions.Clear()
                        rgTimeTimesheet_cct.Rebind()
                        'SelectedItemDataGridByKey(rgDeclareiTimeRice, IDSelect, )
                    Case "Cancel"
                        rgTimeTimesheet_cct.MasterTableView.ClearSelectedItems()
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

    ''' <lastupdate>16/08/2017</lastupdate>
    ''' <summary>
    ''' Event Yes, No tren popup message khi click nut: xoa, ap dung, ngung ap dung
    ''' va Set trang thai cho form va update trang thai control 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            ElseIf e.ActionName = CommonMessage.TOOLBARTIEM_CALCULATE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_APPROVE
                UpdateControlState()
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Event selectedNodeChange Sơ đồ tổ chức
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim repS As New AttendanceStoreProcedure
        Try
            If Not IsPostBack Then
                ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
            Else
                If Not String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                    Dim strOrgs As String = String.Join(",", (From row In ctrlOrganization.GetAllChild(ctrlOrganization.CurrentValue) Select row).ToArray)
                    Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                                    .S_ORG_ID = strOrgs,
                                                    .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
                                                    .IS_DISSOLVE = ctrlOrganization.IsDissolve}

                    Dim btnEnable As Boolean = False
                    btnEnable = repS.IS_PERIODSTATUS(_param.S_ORG_ID, _param.PERIOD_ID)
                    CType(_toolbar.Items(0), RadToolBarButton).Enabled = btnEnable
                    CType(_toolbar.Items(3), RadToolBarButton).Enabled = btnEnable
                End If

                rgTimeTimesheet_cct.CurrentPageIndex = 0
                rgTimeTimesheet_cct.Rebind()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Event click ok popup import file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Dim dtDataHeader As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                If workbook.Worksheets.GetSheetByCodeName("IMPORTSHIFTCTT") Is Nothing Then
                    ShowMessage(Translate("File mẫu không đúng định dạng"), NotifyType.Warning)
                    Exit Sub
                End If
                worksheet = workbook.Worksheets(0)
                dtDataHeader = worksheet.Cells.ExportDataTableAsString(0, 0, 4, worksheet.Cells.MaxColumn + 1, True)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(3, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            dtDataHeader.Rows.RemoveAt(0)
            dtDataHeader.Rows.RemoveAt(0)
            For col As Integer = 0 To dtDataHeader.Columns.Count - 1
                Dim colName = dtDataHeader.Rows(0)(col)
                dtDataHeader.Columns(col).ColumnName = colName
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
                If saveGrid() Then
                    Refresh("InsertView")
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                End If

            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub
    ''' <summary>
    ''' Event click item menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim rep As New AttendanceRepository
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                            .PERIOD_ID = Decimal.Parse(IIf(cboPeriod.SelectedValue = "", -1, cboPeriod.SelectedValue)),
                                            .IS_DISSOLVE = ctrlOrganization.IsDissolve}
            ' kiem tra ky cong da dong chua?

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARTIEM_CALCULATE
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
                    Dim lsEmployee As New List(Of Decimal?)
                    Dim employee_id As Decimal?
                    For Each items As GridDataItem In rgTimeTimesheet_cct.MasterTableView.GetSelectedItems()
                        Dim item = Decimal.Parse(items.GetDataKeyValue("EMPLOYEE_ID"))
                        employee_id = Decimal.Parse(item)
                        lsEmployee.Add(employee_id)
                    Next
                    Dim is_delete As Decimal = 0

                    If chkSummary.Checked Then
                        is_delete = 1
                    Else
                        is_delete = 0
                    End If
                    If is_delete = 1 Then
                        ctrlMessageBox.MessageText = Translate("Tất cả dữ liệu sẽ được tạo mới lại bao gồm cả dữ liệu được nhập từ excel, Bạn có tiếp tục?")
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARTIEM_CALCULATE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else
                        If getSE_CASE_CONFIG("ctrlTime_Timesheet_CTT") > 0 Then
                            rep.Init_TimeTImesheetMachines(_param, rdtungay.SelectedDate, rdDenngay.SelectedDate,
                                                       Decimal.Parse(ctrlOrganization.CurrentValue), lsEmployee, is_delete, "ctrlTime_Timesheet_CTT")
                            Refresh("UpdateView")
                        Else
                            rep.Init_TimeTImesheetMachines(_param, rdtungay.SelectedDate, rdDenngay.SelectedDate,
                                                       Decimal.Parse(ctrlOrganization.CurrentValue), lsEmployee, is_delete, "ctrlTime_Timesheet_CTT")
                            Refresh("UpdateView")
                        End If
                    End If
                Case TOOLBARITEM_DELETE
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    'Kiểm tra các điều kiện để xóa.
                    If rgTimeTimesheet_cct.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi xóa"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case "IMPORT_TEMP"
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    ctrlUpload1.Show()
                Case "EXPORT_TEMP"
                    If String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                        ShowMessage(Translate("Kỳ công chưa được chọn"), NotifyType.Error)
                        Exit Sub
                    End If
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    Dim dtDatas As DataTable
                    dtDatas = CreateDataFilter(True)
                    Session("EXPORTTIMESHEETDAILY") = dtDatas
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Time_TimeSheetCCT&PERIOD_ID=" & cboPeriod.SelectedValue & "&orgid=" & ctrlOrganization.CurrentValue & "&IS_DISSOLVE=" & IIf(ctrlOrganization.IsDissolve, "1", "0") & "')", True)
                Case TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgTimeTimesheet_cct.ExportExcel(Server, Response, dtDatas, "Time_Timesheet_CTT")
                        End If
                    End Using
                Case "EXPORT_ORIGIN"
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = GetDataOrigin()
                        If dtData.Rows.Count > 0 Then
                            rgTimeTimesheet_cct.ExportExcel(Server, Response, dtData, "Timesheet_Origin")
                        End If
                    End Using
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
        Dim rep As New AttendanceRepository
        Dim obj As New AT_TIME_TIMESHEET_DAILYDTO
        Dim startdate As Date '= CType("01/01/2016", Date)
        Dim enddate As Date '= CType("31/01/2016", Date)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Try
                Dim year As String = cboYear.SelectedValue.ToString
                If (rdtungay.SelectedDate IsNot Nothing) Then
                    startdate = rdtungay.SelectedDate
                Else
                    If (year IsNot Nothing) Then
                        startdate = CType("01/01/" & year, Date)
                        rdtungay.SelectedDate = startdate
                    End If
                End If
                If (rdDenngay.SelectedDate IsNot Nothing) Then
                    enddate = rdDenngay.SelectedDate
                Else
                    If (year IsNot Nothing) Then
                        If (Common.Common.GetShortDatePattern().Trim.ToUpper.Contains("DD/MM/YYYY")) Then
                            enddate = CType("31/01/" & year, Date)
                        Else
                            enddate = CType("01/31/" & year, Date)
                        End If
                        rdDenngay.SelectedDate = enddate
                    End If
                End If
                For i = 1 To 31
                    If startdate <= enddate Then
                        rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM")
                        rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).Visible = True
                        rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).HeaderStyle.CssClass = "Removecss3"
                        If startdate.DayOfWeek = DayOfWeek.Sunday Then
                            rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> CN"
                            rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).HeaderStyle.CssClass = "css3"
                        ElseIf startdate.DayOfWeek = DayOfWeek.Monday Then
                            rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T2"
                        ElseIf startdate.DayOfWeek = DayOfWeek.Tuesday Then
                            rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T3"
                        ElseIf startdate.DayOfWeek = DayOfWeek.Wednesday Then
                            rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T4"
                        ElseIf startdate.DayOfWeek = DayOfWeek.Thursday Then
                            rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T5"
                        ElseIf startdate.DayOfWeek = DayOfWeek.Friday Then
                            rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T6"
                        ElseIf startdate.DayOfWeek = DayOfWeek.Saturday Then
                            rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM") & "<br> T7"
                        End If
                        startdate = startdate.AddDays(1)
                    Else
                        rgTimeTimesheet_cct.MasterTableView.GetColumn("D" & i).Visible = False
                    End If
                Next
            Catch ex As Exception
                Throw ex
            End Try
            Try
                SetValueObjectByRadGrid(rgTimeTimesheet_cct, obj)
                Dim Sorts As String = rgTimeTimesheet_cct.MasterTableView.SortExpressions.GetSortString()
                If cboPeriod.SelectedValue <> "" Then
                    obj.PERIOD_ID = cboPeriod.SelectedValue
                End If
                If rdtungay.SelectedDate IsNot Nothing Then
                    obj.FROM_DATE = rdtungay.SelectedDate
                End If
                If rdDenngay.SelectedDate IsNot Nothing Then
                    obj.END_DATE = rdDenngay.SelectedDate
                End If
                obj.PAGE_INDEX = rgTimeTimesheet_cct.CurrentPageIndex + 1
                obj.PAGE_SIZE = rgTimeTimesheet_cct.PageSize
                obj.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
                obj.IS_DISSOLVE = ctrlOrganization.IsDissolve
            Catch ex As Exception
                Throw ex
            End Try
            Dim ds = rep.GetCCT(obj)
            If Not isFull Then
                If ds IsNot Nothing Then
                    Dim tableCct = ds.Tables(0)
                    rgTimeTimesheet_cct.VirtualItemCount = Decimal.Parse(ds.Tables(1).Rows(0)("TOTAL"))
                    rgTimeTimesheet_cct.DataSource = tableCct
                Else
                    rgTimeTimesheet_cct.VirtualItemCount = 0
                    rgTimeTimesheet_cct.DataSource = New DataTable
                End If
            Else
                obj.PAGE_INDEX = 1
                obj.PAGE_SIZE = Integer.MaxValue
                Return rep.GetCCT(obj).Tables(0)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
    ''' <summary>
    ''' Load data cho table để set datasource cho grid
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDataOrigin() As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_TIME_TIMESHEET_DAILYDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            SetValueObjectByRadGrid(rgTimeTimesheet_cct, obj)

            Dim Sorts As String = rgTimeTimesheet_cct.MasterTableView.SortExpressions.GetSortString()

            If cboPeriod.SelectedValue <> "" Then
                obj.PERIOD_ID = cboPeriod.SelectedValue
            End If
            If rdtungay.SelectedDate IsNot Nothing Then
                obj.FROM_DATE = rdtungay.SelectedDate
            End If
            If rdDenngay.SelectedDate IsNot Nothing Then
                obj.END_DATE = rdDenngay.SelectedDate
            End If
            obj.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
            obj.IS_DISSOLVE = ctrlOrganization.IsDissolve

            Dim ds = rep.GetCCT_Origin(obj)
            Return ds
        Catch ex As Exception
            Throw ex
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
            rgTimeTimesheet_cct.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event SelectedIndexChanged combobox Năm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim dtData As List(Of AT_PERIODDTO)
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim repS As New AttendanceStoreProcedure
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
                Else
                    cboPeriod.SelectedIndex = 0
                    rdtungay.SelectedDate = CType("01/01/" & cboYear.SelectedValue.ToString(), Date)
                    rdDenngay.SelectedDate = CType("31/01/" & cboYear.SelectedValue.ToString(), Date)
                End If

                If Not String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                    Dim strOrgs As String = String.Join(",", (From row In ctrlOrganization.GetAllChild(ctrlOrganization.CurrentValue) Select row).ToArray)
                    Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                                    .S_ORG_ID = strOrgs,
                                                    .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
                                                    .IS_DISSOLVE = ctrlOrganization.IsDissolve}

                    Dim btnEnable As Boolean = False
                    btnEnable = repS.IS_PERIODSTATUS(_param.S_ORG_ID, _param.PERIOD_ID)
                    CType(_toolbar.Items(0), RadToolBarButton).Enabled = btnEnable
                    CType(_toolbar.Items(3), RadToolBarButton).Enabled = btnEnable
                End If
                'rgTimeTimesheet_cct.Rebind()
            Else
                ClearControlValue(rdtungay, rdDenngay)
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
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim repS As New AttendanceStoreProcedure
        Try
            If cboPeriod.SelectedValue <> "" Then
                Dim p = (From o In Me.PERIOD Where o.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)).FirstOrDefault
                If p IsNot Nothing Then
                    rdtungay.SelectedDate = p.START_DATE
                    rdDenngay.SelectedDate = p.END_DATE
                Else
                    Dim dtData As List(Of AT_PERIODDTO)
                    Dim rep As New AttendanceRepository
                    Dim period As New AT_PERIODDTO
                    period.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
                    period.YEAR = Decimal.Parse(cboYear.SelectedValue)
                    dtData = rep.LOAD_PERIODBylinq(period)

                    Dim pnot = (From o In dtData Where o.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)).FirstOrDefault
                    If pnot IsNot Nothing Then
                        rdtungay.SelectedDate = pnot.START_DATE
                        rdDenngay.SelectedDate = pnot.END_DATE
                    End If
                End If
            End If

            If Not String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
                Dim strOrgs As String = String.Join(",", (From row In ctrlOrganization.GetAllChild(ctrlOrganization.CurrentValue) Select row).ToArray)
                Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                                .S_ORG_ID = strOrgs,
                                                .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
                                                .IS_DISSOLVE = ctrlOrganization.IsDissolve}

                Dim btnEnable As Boolean = False
                btnEnable = repS.IS_PERIODSTATUS(_param.S_ORG_ID, _param.PERIOD_ID)
                CType(_toolbar.Items(0), RadToolBarButton).Enabled = btnEnable
                CType(_toolbar.Items(3), RadToolBarButton).Enabled = btnEnable
            End If

            'rgTimeTimesheet_cct.Rebind()
            ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Reload datasource for grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgTimeTimesheet_cct.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If IsPostBack Then
                CreateDataFilter(False)
            Else
                Dim dt As DataTable = New DataTable("data")
                dt.Columns.Add("EMPLOYEE_ID", GetType(Integer))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("FULLNAME_VN", GetType(String))
                dt.Columns.Add("TITLE_NAME", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("OBJECT_ATTENDANCE_NAME", GetType(String))
                'dt.Columns.Add("TOTAL_DAY_SAL", GetType(String))
                'dt.Columns.Add("TOTAL_DAY_NON_SAL", GetType(String))
                dt.Columns.Add("D1", GetType(String))
                dt.Columns.Add("D2", GetType(String))
                dt.Columns.Add("D3", GetType(String))
                dt.Columns.Add("D4", GetType(String))
                dt.Columns.Add("D5", GetType(String))
                dt.Columns.Add("D6", GetType(String))
                dt.Columns.Add("D7", GetType(String))
                dt.Columns.Add("D8", GetType(String))
                dt.Columns.Add("D9", GetType(String))
                dt.Columns.Add("D10", GetType(String))
                dt.Columns.Add("D11", GetType(String))
                dt.Columns.Add("D12", GetType(String))
                dt.Columns.Add("D13", GetType(String))
                dt.Columns.Add("D14", GetType(String))
                dt.Columns.Add("D15", GetType(String))
                dt.Columns.Add("D16", GetType(String))
                dt.Columns.Add("D17", GetType(String))
                dt.Columns.Add("D18", GetType(String))
                dt.Columns.Add("D19", GetType(String))
                dt.Columns.Add("D20", GetType(String))
                dt.Columns.Add("D21", GetType(String))
                dt.Columns.Add("D22", GetType(String))
                dt.Columns.Add("D23", GetType(String))
                dt.Columns.Add("D24", GetType(String))
                dt.Columns.Add("D26", GetType(String))
                dt.Columns.Add("D27", GetType(String))
                dt.Columns.Add("D28", GetType(String))
                dt.Columns.Add("D29", GetType(String))
                dt.Columns.Add("D30", GetType(String))
                dt.Columns.Add("D31", GetType(String))

                rgTimeTimesheet_cct.DataSource = dt
                rgTimeTimesheet_cct.VirtualItemCount = 0
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Set color for column grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgTimeTimesheet_cct_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgTimeTimesheet_cct.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            'If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            '    Dim rowView = CType(e.Item.DataItem, DataRowView)
            '    Dim dataItem As GridDataItem = e.Item
            '    Dim i As Integer = 1
            '    While True
            '        Dim str = "D" & i
            '        If rowView.Row.Table.Columns.Contains(str) Then
            '            If rowView.Row(str & "_COLOR") Is DBNull.Value Then
            '                i = i + 1
            '                Continue While
            '            End If
            '            If String.IsNullOrEmpty(rowView.Row(str & "_COLOR")) Then
            '                i = i + 1
            '                Continue While
            '            End If
            '            Select Case rowView.Row(str & "_COLOR")
            '                Case 1
            '                    dataItem(str).BackColor = Drawing.Color.LightBlue
            '                Case 2
            '                    dataItem(str).BackColor = Drawing.Color.DarkGreen
            '                Case 3
            '                    dataItem(str).BackColor = Drawing.Color.Yellow
            '                Case 4
            '                    dataItem(str).BackColor = Drawing.Color.Red
            '            End Select
            '        Else
            '            Exit While
            '        End If
            '        i = i + 1
            '    End While
            'End If
            'If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            '    Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            '    datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
            'End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Event Request ajaxmanager
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
                rgTimeTimesheet_cct.CurrentPageIndex = 0
                rgTimeTimesheet_cct.Rebind()
                If rgTimeTimesheet_cct.Items IsNot Nothing AndAlso rgTimeTimesheet_cct.Items.Count > 0 Then
                    rgTimeTimesheet_cct.Items(0).Selected = True
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Event click cancle popup import
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrgPopup.CancelClicked
        Try
            isLoadPopup = 0
        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region "CUSTOM"
    ''' <summary>
    ''' Save data from grid to DB when import
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function saveGrid() As Boolean
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            dtData.TableName = "Data"
            Dim dtImport = CreateDataImport()
            For Each row As DataRow In dtData.Rows
                dtImport.ImportRow(row)
            Next
            dtImport.TableName = "DATA"
            rep.InsertLeaveSheetDaily(dtImport, cboPeriod.SelectedValue)
            Return True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
    ''' <summary>
    ''' Load data from file import to grid
    ''' </summary>
    ''' <param name="dtDataHeader"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function loadToGrid(ByVal dtDataHeader As DataTable) As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim dtDatas As AT_PERIODDTO
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO
        Dim rowError As DataRow
        Dim isError As Boolean = False
        Dim sError As String = String.Empty
        Dim dtDataImportEmployee As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim irow = 6
        Try
            If dtData.Rows.Count = 0 Then
                ShowMessage("File mẫu không đúng định dạng", NotifyType.Warning)
                Return False
            End If
            dtDataImportEmployee = dtData.Clone
            dtError = dtData.Clone
            dtError.Columns.Add("STT")
            period.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)
            dtDatas = rep.LOAD_PERIODByID(period)
            Dim dtManual = rep.GetAT_TIME_MANUAL(New AT_TIME_MANUALDTO)
            For Each row As DataRow In dtData.Rows
                Dim isRow = ImportValidate.TrimRow(row)
                If Not isRow Then
                    irow += 1
                    Continue For
                End If
                isError = False
                ImportValidate.TrimRow(row)
                rowError = dtError.NewRow
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)

                sError = "Mã nhân viên không phải hệ thống chiết xuất"
                ImportValidate.IsValidNumber("EMPLOYEE_ID", row, rowError, isError, sError)
                If rowError("EMPLOYEE_ID").ToString <> "" Then
                    rowError("EMPLOYEE_CODE") = sError
                End If

                For index = 1 To (dtDatas.END_DATE - dtDatas.START_DATE).Value.TotalDays + 1
                    If row("D" & index).ToString <> "" Then
                        Dim r = row("D" & index).ToString
                        Dim exists = (From p In dtManual Where p.CODE.ToUpper = r.ToUpper).FirstOrDefault

                        If exists Is Nothing Then
                            rowError("D" & index) = row("D" & index).ToString & " không tồn tại"
                            isError = True
                        Else
                            row("D" & index) = exists.ID
                        End If
                    End If
                Next
                If isError Then
                    rowError("STT") = irow
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    rowError("VN_FULLNAME") = row("VN_FULLNAME").ToString
                    rowError("ORG_NAME") = row("ORG_NAME").ToString
                    rowError("ORG_PATH") = row("ORG_PATH").ToString
                    rowError("TITLE_NAME") = row("TITLE_NAME").ToString
                    dtError.Rows.Add(rowError)
                End If
                irow += 1
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Dim dsData As New DataSet
                dsData.Tables.Add(dtError)
                Session("EXPORTREPORT") = dsData
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_importTimesheet_CTT_Error&PERIOD_ID=" & cboPeriod.SelectedValue & "&orgid=" & ctrlOrganization.CurrentValue & "&IS_DISSOLVE=" & IIf(ctrlOrganization.IsDissolve, "1", "0") & "')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Return False
            End If
            Return True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
    ''' <summary>
    ''' Load, khởi tạo popup sơ đồ tổ chức
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_APPROVE
                    Dim lsEmployee As New List(Of Decimal?)
                    Dim employee_id As Decimal?
                    For Each items As GridDataItem In rgTimeTimesheet_cct.MasterTableView.GetSelectedItems()
                        Dim item = Decimal.Parse(items.GetDataKeyValue("EMPLOYEE_ID"))
                        employee_id = Decimal.Parse(item)
                        lsEmployee.Add(employee_id)
                    Next
                    Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                                    .PERIOD_ID = Decimal.Parse(IIf(cboPeriod.SelectedValue = "", -1, cboPeriod.SelectedValue)),
                                                    .IS_DISSOLVE = ctrlOrganization.IsDissolve}
                    Dim is_delete As Decimal = 0

                    If chkSummary.Checked Then
                        is_delete = 1
                    Else
                        is_delete = 0
                    End If
                    If is_delete = 1 Then
                        ctrlMessageBox.MessageText = Translate(CommonMessage.CM_CTRLPA_FORMULA_MESSAGE_BOX_CONFIRM)
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARTIEM_CALCULATE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    End If
                    If getSE_CASE_CONFIG("ctrlTimeTimesheet_machine_case1") > 0 Then
                        rep.Init_TimeTImesheetMachines(_param, rdtungay.SelectedDate, rdDenngay.SelectedDate,
                                                   Decimal.Parse(ctrlOrganization.CurrentValue), lsEmployee, is_delete, "ctrlTimeTimesheet_machine_case1")
                        Refresh("UpdateView")
                    Else
                        rep.Init_TimeTImesheetMachines(_param, rdtungay.SelectedDate, rdDenngay.SelectedDate,
                                                   Decimal.Parse(ctrlOrganization.CurrentValue), lsEmployee, 0, "")
                        Refresh("UpdateView")
                    End If
                    Return
            End Select
            phPopup.Controls.Clear()
            Select Case isLoadPopup
                Case 1
                    ctrlOrgPopup = Me.Register("ctrlOrgPopup", "Common", "ctrlFindOrgPopup")
                    phPopup.Controls.Add(ctrlOrgPopup)
            End Select
            UpdateToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Update trạng thái toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Function CreateDataImport() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("EMPLOYEE_ID", GetType(String))
        dt.Columns.Add("D1", GetType(String))
        dt.Columns.Add("D2", GetType(String))
        dt.Columns.Add("D3", GetType(String))
        dt.Columns.Add("D4", GetType(String))
        dt.Columns.Add("D5", GetType(String))
        dt.Columns.Add("D6", GetType(String))
        dt.Columns.Add("D7", GetType(String))
        dt.Columns.Add("D8", GetType(String))
        dt.Columns.Add("D9", GetType(String))
        dt.Columns.Add("D10", GetType(String))
        dt.Columns.Add("D11", GetType(String))
        dt.Columns.Add("D12", GetType(String))
        dt.Columns.Add("D13", GetType(String))
        dt.Columns.Add("D14", GetType(String))
        dt.Columns.Add("D15", GetType(String))
        dt.Columns.Add("D16", GetType(String))
        dt.Columns.Add("D17", GetType(String))
        dt.Columns.Add("D18", GetType(String))
        dt.Columns.Add("D19", GetType(String))
        dt.Columns.Add("D20", GetType(String))
        dt.Columns.Add("D21", GetType(String))
        dt.Columns.Add("D22", GetType(String))
        dt.Columns.Add("D23", GetType(String))
        dt.Columns.Add("D24", GetType(String))
        dt.Columns.Add("D25", GetType(String))
        dt.Columns.Add("D26", GetType(String))
        dt.Columns.Add("D27", GetType(String))
        dt.Columns.Add("D28", GetType(String))
        dt.Columns.Add("D29", GetType(String))
        dt.Columns.Add("D30", GetType(String))
        dt.Columns.Add("D31", GetType(String))

        Return dt
    End Function
#End Region

End Class
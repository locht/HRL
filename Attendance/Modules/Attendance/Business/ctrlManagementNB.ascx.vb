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

Public Class ctrlManagementNB
    Inherits Common.CommonView
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()
#Region "Properties"
    ''' <summary>
    ''' p_End_date allow null
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property p_End_date As Date?
    ''' <summary>
    ''' ManagementCP
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ManagementCP As List(Of AT_COMPENSATORYDTO)
        Get
            Return ViewState(Me.ID & "_ManagementCP")
        End Get
        Set(ByVal value As List(Of AT_COMPENSATORYDTO))
            ViewState(Me.ID & "_ManagementCP") = value
        End Set
    End Property
    ''' <summary>
    ''' danh sách Period
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
    ''' Hiển thị thông tin control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Try
                Dim startTime As DateTime = DateTime.UtcNow
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
    ''' Ghi đè phương thức khởi tạo các thiết lập cho các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SetGridFilter(rgManagementNB)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgManagementNB.AllowCustomPaging = True
            rgManagementNB.ClientSettings.EnablePostBackOnRowClick = False
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Phương thức khởi tạo các thiết lập cho các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Calculate,
                                       ToolbarItem.Export)


            'ToolbarItem.Next,
            'ToolbarItem.Import,
            'ToolbarItem.Print,
            'ToolbarItem.Active

            MainToolBar.Items(0).Text = Translate("Tổng hợp")
            'MainToolBar.Items(2).Text = Translate("Xuất file mẫu điều chỉnh phép năm trước")
            'CType(MainToolBar.Items(2), RadToolBarButton).ImageUrl = CType(MainToolBar.Items(1), RadToolBarButton).ImageUrl
            'MainToolBar.Items(3).Text = Translate("Nhập file mẫu điều chỉnh phép năm trước")
            'MainToolBar.Items(4).Text = Translate("Xuất file mẫu")
            'CType(MainToolBar.Items(4), RadToolBarButton).ImageUrl = CType(MainToolBar.Items(1), RadToolBarButton).ImageUrl
            'MainToolBar.Items(5).Text = Translate("Nhập file mẫu")
            'CType(MainToolBar.Items(5), RadToolBarButton).ImageUrl = CType(MainToolBar.Items(3), RadToolBarButton).ImageUrl
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
    ''' Bind dữ liệu cho các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
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
            FillRadCombobox(cboPeriodId, lsData, "PERIOD_NAME", "PERIOD_ID", True)
            If lsData.Count > 0 Then
                Dim periodid = (From d In lsData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriodId.SelectedValue = periodid.PERIOD_ID.ToString()
                Else
                    cboPeriodId.SelectedIndex = 0
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
    ''' Xử lý sự kiện SelectedNodeChanged khi changed value item of cboYear
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlYear_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dtData As List(Of AT_PERIODDTO)
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            period.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            dtData = rep.LOAD_PERIODBylinq(period)
            cboPeriodId.ClearSelection()

            FillRadCombobox(cboPeriodId, dtData, "PERIOD_NAME", "PERIOD_ID", True)
            If dtData.Count > 0 Then
                Dim periodid = (From d In dtData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriodId.SelectedValue = periodid.PERIOD_ID.ToString()
                Else
                    cboPeriodId.SelectedIndex = 0
                End If
                rgManagementNB.Rebind()
            Else
                rgManagementNB.VirtualItemCount = 0
                rgManagementNB.DataSource = New List(Of AT_COMPENSATORYDTO)
                rgManagementNB.DataBind()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Phương thức làm mới thông tin trên trang
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ManagementCP Is Nothing Or Message = CommonMessage.ACTION_UPDATED Then
                rgManagementNB.Rebind()
            End If
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
    ''' Xử lý sự kiện Click khi click button btnSearch
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If String.IsNullOrEmpty(cboPeriodId.SelectedValue) Then
                ShowMessage(Translate("Kỳ công chưa được chọn"), NotifyType.Error)
                Exit Sub
            End If
            rgManagementNB.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Xử lý sự kiện SelectedNodeChanged của ctrlorganization
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'If Not IsPostBack Then
            '    ctrlOrganization.SetColorPeriod(cboPeriodId.SelectedValue, PeriodType.AT)
            'End If
            rgManagementNB.CurrentPageIndex = 0
            rgManagementNB.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Xử lý sự kiện Command khi click item trên toolbar 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgManagementNB.ExportExcel(Server, Response, dtDatas, "DataManagementCP")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using
                Case TOOLBARTIEM_CALCULATE
                    Dim rep As New AttendanceRepository
                    If (cboPeriodId.SelectedValue Is Nothing) Then
                        ShowMessage(Translate("Bạn chưa chọn kỳ công"), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim _param = New ParamDTO With {.ORG_ID = ctrlOrganization.CurrentValue, _
                                                    .PERIOD_ID = cboPeriodId.SelectedValue, _
                                                    .IS_DISSOLVE = ctrlOrganization.IsDissolve}
                    Dim lsEmployee As New List(Of Decimal?)
                    Dim employee_id As Decimal?
                    For Each items As GridDataItem In rgManagementNB.MasterTableView.GetSelectedItems()
                        Dim item = Decimal.Parse(items.GetDataKeyValue("EMPLOYEE_ID"))
                        employee_id = Decimal.Parse(item)
                        lsEmployee.Add(employee_id)
                    Next
                    If rep.CALCULATE_ENTITLEMENT_NB(_param, lsEmployee) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        Refresh(ACTION_UPDATED)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        Exit Sub
                    End If

                Case TOOLBARITEM_NEXT
                    Using xls As New ExcelCommon
                        Dim dataSet As New DataSet
                        Dim dtVariable As New DataTable
                        Dim tempPath = "~/ReportTemplates//Attendance//Import//TEMPLATE_IMPORT_NB.xls"
                        Dim bCheck = xls.ExportExcelTemplate(
                          System.IO.Path.Combine(Server.MapPath(tempPath)), "IMPORT_NB", dataSet, Nothing, Response)
                    End Using
                Case TOOLBARITEM_PRINT
                    Using xls As New ExcelCommon
                        Dim dataSet As New DataSet
                        Dim dtVariable As New DataTable
                        Dim tempPath = "~/ReportTemplates//Attendance//Import//TEMPLATE_IMPORT_NB_PREV.xls"
                        Dim bCheck = xls.ExportExcelTemplate(
                              System.IO.Path.Combine(Server.MapPath(tempPath)), "IMPORT_NB_NamTruoc", dataSet, Nothing, Response)
                    End Using
                Case TOOLBARITEM_IMPORT
                    ctrlUpload.Show()
                Case TOOLBARITEM_ACTIVE
                    ctrlUpload1.Show()
            End Select
            ' UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Xử lý sự kiện Button Command của ctrlmessageBox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim strError As String = ""
                If strError = "" Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                Else
                    ShowMessage(Translate("Nhân viên đã có hợp đồng " & strError.Substring(1, strError.Length - 1) & ". Hãy xóa hợp đồng trước khi xóa nhân viên."), Utilities.NotifyType.Error)
                End If
                Refresh(ACTION_UPDATED)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho button ctrUpload
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Import_Data()
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub


    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho button ctrUpload1
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Import_Data1()
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    Private Sub Import_Data()
        Try
            Dim rep As New AttendanceRepository
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
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
                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    If rep.INPORT_NB(DocXml, cboPeriodId.SelectedValue) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        'rgData.Rebind()
                    Else
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Warning)
                    End If
                End If
            Else
                Session("EXPORTREPORT") = dtLogs
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('HU_ANNUALLEAVE_PLANS_ERROR')", True)
                ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Import_Data1()
        Try
            Dim rep As New AttendanceRepository
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload1.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim ds As New DataSet
            If countFile > 0 Then
                Dim file As UploadedFile = ctrlUpload1.UploadedFiles(countFile - 1)
                fileName = System.IO.Path.Combine(savepath, file.FileName)
                file.SaveAs(fileName, True)
                Using ep As New ExcelPackage
                    ds = ep.ReadExcelToDataSet(fileName, False)
                End Using
            End If
            If ds Is Nothing Then
                Exit Sub
            End If
            TableMapping1(ds.Tables(0))

            If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then
                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    If rep.INPORT_NB_PREV(DocXml, cboYear.SelectedValue) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        'rgData.Rebind()
                    Else
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Warning)
                    End If
                End If
            Else
                Session("EXPORTREPORT") = dtLogs
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('HU_ANNUALLEAVE_PLANS_ERROR')", True)
                ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        Dim rep As New AttendanceRepository
        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
        dtTemp.Columns(0).ColumnName = "EMPLOYEE_CODE"
        dtTemp.Columns(5).ColumnName = "COMPENSATORY_EDIT"

        'XOA DONG TIEU DE VA HEADER
        dtTemp.Rows(0).Delete()

        ' add Log
        Dim _error As Boolean = True
        Dim count As Integer
        Dim newRow As DataRow
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

            ' Nhân viên k có trong hệ thống
            If rep.CHECK_EMPLOYEE(rows("EMPLOYEE_CODE")) = 0 Then
                newRow("DISCIPTION") = "Mã nhân viên - Không tồn tại,"
                _error = False
            End If

            If Not (IsNumeric(rows("COMPENSATORY_EDIT"))) Then
                rows("COMPENSATORY_EDIT") = 0
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Số ngày phép bù điều chỉnh - Không đúng định dạng,"
                _error = False
            End If

            If _error = False Then
                dtLogs.Rows.Add(newRow)
                count = count + 1
                _error = True
            End If
        Next
        dtTemp.AcceptChanges()
    End Sub

    Private Sub TableMapping1(ByVal dtTemp As System.Data.DataTable)
        Dim rep As New AttendanceRepository
        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
        dtTemp.Columns(0).ColumnName = "EMPLOYEE_CODE"
        dtTemp.Columns(5).ColumnName = "PREV_HAVE"

        'XOA DONG TIEU DE VA HEADER
        dtTemp.Rows(0).Delete()

        ' add Log
        Dim _error As Boolean = True
        Dim count As Integer
        Dim newRow As DataRow
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

            ' Nhân viên k có trong hệ thống
            If rep.CHECK_EMPLOYEE(rows("EMPLOYEE_CODE")) = 0 Then
                newRow("DISCIPTION") = "Mã nhân viên - Không tồn tại,"
                _error = False
            End If

            If Not (IsNumeric(rows("PREV_HAVE"))) Then
                rows("PREV_HAVE") = 0
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Số phép bù năm trước chuyển sang - Không đúng định dạng,"
                _error = False
            End If

            If _error = False Then
                dtLogs.Rows.Add(newRow)
                count = count + 1
                _error = True
            End If
        Next
        dtTemp.AcceptChanges()
    End Sub



    ''' <summary>
    ''' Xử lý sự kiện Tạo dữ liệu filter
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        If cboPeriodId.SelectedValue Is Nothing Then
            rgManagementNB.VirtualItemCount = 0
            rgManagementNB.DataSource = New List(Of AT_COMPENSATORYDTO)
            rgManagementNB.DataBind()
            Return rgManagementNB.DataSource
        End If
        Dim rep As New AttendanceRepository
        Dim obj As New AT_COMPENSATORYDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgManagementNB, obj)
            Dim _param = New ParamDTO With {.ORG_ID = ctrlOrganization.CurrentValue, _
                                           .IS_DISSOLVE = ctrlOrganization.IsDissolve,
                                           .IS_FULL = True}
            Dim Sorts As String = rgManagementNB.MasterTableView.SortExpressions.GetSortString()

            obj.YEAR = cboYear.SelectedValue
            obj.PERIOD_ID = Decimal.Parse(cboPeriodId.SelectedValue)
            'obj.END_DATE = p_End_date
            obj.ISTEMINAL = chkChecknghiViec.Checked
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.ManagementCP = rep.GetNB(obj, _param, MaximumRows, rgManagementNB.CurrentPageIndex, rgManagementNB.PageSize, "CREATED_DATE desc")
                Else
                    Me.ManagementCP = rep.GetNB(obj, _param, MaximumRows, rgManagementNB.CurrentPageIndex, rgManagementNB.PageSize)
                End If
            Else
                Return rep.GetNB(obj, _param).ToTable
            End If
            rgManagementNB.VirtualItemCount = MaximumRows
            rgManagementNB.DataSource = Me.ManagementCP
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Xử lý sự kiện NeedDataSource cho rad grid rgManagementNB
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgManagementNB.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter(False)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Xử lý sự kiện PageIndexChanged cho rad grid rgManagementNB
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgManagementNB.PageIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Xử lý sự kiện SelectedNodeChanged của control cboPeriod
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboPeriod_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriodId.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgManagementNB.Rebind()
            'Dim p = (From o In Me.PERIOD Where o.PERIOD_ID = Decimal.Parse(cboPeriodId.SelectedValue)).FirstOrDefault
            'p_End_date = p.END_DATE
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Xử lý sự kiện AjaxRequest
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim url = e.Argument
            If (url.Contains("reload=1")) Then
                rgManagementNB.CurrentPageIndex = 0
                rgManagementNB.Rebind()
                If rgManagementNB.Items IsNot Nothing AndAlso rgManagementNB.Items.Count > 0 Then
                    rgManagementNB.Items(0).Selected = True
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
    ''' Xử lý sự kiện ItemDataBound của rad grid rgManagementNB
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgManagementNB_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgManagementNB.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
#End Region

End Class
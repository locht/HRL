Imports Common
Imports Attendance.AttendanceBusiness
Imports Telerik.Web.UI
Imports Framework.UI.Utilities
Imports Telerik.Web
Imports Framework.UI
Imports WebAppLog

Public Class ctrlSwipeDataDownload
    Inherits Common.CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()
#Region "Property"
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' AT_Terminal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AT_Terminal As List(Of AT_TERMINALSDTO)
        Get
            Return ViewState(Me.ID & "_Termidal")
        End Get
        Set(ByVal value As List(Of AT_TERMINALSDTO))
            ViewState(Me.ID & "_Termidal") = value
        End Set
    End Property
    ''' <summary>
    ''' danh sach du lieu cham cong
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SWIPE_DATA As List(Of AT_SWIPE_DATADTO)
        Get
            Return ViewState(Me.ID & "_AT_SWIPE_DATADTO")
        End Get
        Set(ByVal value As List(Of AT_SWIPE_DATADTO))
            ViewState(Me.ID & "_AT_SWIPE_DATADTO") = value
        End Set
    End Property
    ''' <summary>
    ''' dtDataImportEmployee
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property dtDataImportEmployee As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataImportEmployee")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataImportEmployee") = value
        End Set

    End Property

    Dim dsDataComper As New DataTable
    ''' <summary>
    ''' Danh sach du lieu cham cong
    ''' </summary>
    ''' <remarks></remarks>
    Dim ls_AT_SWIPE_DATADTO As New List(Of AT_SWIPE_DATADTO)
    Dim mv_IP As String = ""
    ''' <summary>
    ''' Tao DataTable luu tru du lieu cham cong
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                'dt.Columns.Add("ID", GetType(String))
                dt.Columns.Add("ITIME_ID", GetType(String))
                'dt.Columns.Add("TERMINAL_ID", GetType(String))
                'dt.Columns.Add("TERMINAL_NAME", GetType(String))
                dt.Columns.Add("VALTIME", GetType(String))
                dt.Columns.Add("WORKINGDAY", GetType(String))

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
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi de phuong thuc ViewLoad
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rglSwipeDataDownload.SetFilter()
            rglSwipeDataDownload.AllowCustomPaging = True

            ctrlUpload.isMultiple = AsyncUpload.MultipleFileSelection.Disabled
            ctrlUpload.MaxFileInput = 1
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi de phuong thuc khoi tao cac control tren trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            InitControl()
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
    ''' Phuong thuc khoi tao, thiet lap cac control tren trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = rtbMain
            Common.Common.BuildToolbar(Me.MainToolBar,
                                        ToolbarItem.Export)
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_TEMP",
                                                                    ToolbarIcons.Export,
                                                                    ToolbarAuthorize.Export,
                                                                    Translate("Xuất file mẫu")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP",
                                                                     ToolbarIcons.Import,
                                                                     ToolbarAuthorize.Import,
                                                                     Translate("Nhập file mẫu")))

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT",
                                                                    ToolbarIcons.Import,
                                                                    ToolbarAuthorize.Import,
                                                                    Translate("Nhập file")))

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
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
    ''' Cap nhat trang thai cac control tren trang
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    'EnabledGridNotPostback(rglSwipeDataDownload, True)
            End Select
            UpdateToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim dtdata As DataTable
        Try
            Using rep As New AttendanceRepository
                dtdata = rep.GetOtherList("TIME_RECORDER", True)
                If dtdata IsNot Nothing AndAlso dtdata.Rows.Count Then
                    FillRadCombobox(cbMachine_Type, dtdata, "NAME", "ID")
                End If
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        'Dim dic As New Dictionary(Of String, Control)
        'Utilities.OnClientRowSelectedChanged(rglSwipeDataDownload, dic)
        'Dim rep1 As New AttendanceRepository
        'Dim obj As New AT_TERMINALSDTO
        'Dim table = rep1.GetTerminal(obj)
        'FillRadCombobox(cboMachine, table, "TERMINAL_NAME", "TERMINAL_ID")
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Lam moi trang theo tung trang thai
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rglSwipeDataDownload.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rglSwipeDataDownload.CurrentPageIndex = 0
                        rglSwipeDataDownload.MasterTableView.SortExpressions.Clear()
                        rglSwipeDataDownload.Rebind()
                    Case "Cancel"
                        rglSwipeDataDownload.MasterTableView.ClearSelectedItems()
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
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Tao du lieu filter cho rad grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_SWIPE_DATADTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rglSwipeDataDownload, obj)
            obj.FROM_DATE = rdStartDate.SelectedDate
            obj.END_DATE = rdEndDate.SelectedDate
            Dim Sorts As String = rglSwipeDataDownload.MasterTableView.SortExpressions.GetSortString()
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.SWIPE_DATA = New List(Of AT_SWIPE_DATADTO) 'rep.GetSwipeData(obj, rglSwipeDataDownload.CurrentPageIndex, rglSwipeDataDownload.PageSize, MaximumRows, Sorts)
                Else
                    Me.SWIPE_DATA = New List(Of AT_SWIPE_DATADTO) 'rep.GetSwipeData(obj, rglSwipeDataDownload.CurrentPageIndex, rglSwipeDataDownload.PageSize, MaximumRows)
                End If
            Else
                Return rep.GetSwipeData(obj).ToTable()
            End If
            rglSwipeDataDownload.VirtualItemCount = MaximumRows
            rglSwipeDataDownload.DataSource = Me.SWIPE_DATA
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cho rad grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rglSwipeDataDownload_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rglSwipeDataDownload.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

  
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Command khi click item tren toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rglSwipeDataDownload.ExportExcel(Server, Response, dtData, "SwipeDataDownload")
                        End If
                    End Using
                Case "EXPORT_TEMP"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_ImportSwipeData&orgid=46&IS_DISSOLVE=0')", True)
                Case "IMPORT_TEMP"
                    ctrlUpload.Show()
                Case "IMPORT"
                    ctrlUpload1.Show()

            End Select
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
    ''' Bind du lieu cho rad grid
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function loadToGrid() As Boolean
        'LAY THONG TIN CONFIG TMPLATE => @PAR = MACHINE_TYPE 
        Dim IAttenDance As IAttendanceBusiness = New AttendanceBusinessClient()
        Dim dtConfig As DataTable = IAttenDance.GET_CONFIG_TEMPLATE(cbMachine_Type.SelectedValue)
        Dim DATA_IN As New DataTable("DATA_IN")
        'Create struct DATA IN with table config
        For Each row In dtConfig.Rows
            DATA_IN.Columns.Add(row("COLUMN_CODE"), GetType(String))
        Next
        'end create struct DATA IN
        'GET DATA 
        For Each rowData In dsDataComper.Rows
            Dim newRow As DataRow = DATA_IN.NewRow()
            For Each rowConfig In dtConfig.Rows
                newRow(rowConfig("COLUMN_CODE")) = rowData(CType(rowConfig("ORDER_COLUMN"), Integer))
            Next
            DATA_IN.Rows.Add(newRow)
        Next
        'END GET DATA


        Dim dtError As New DataTable("ERROR")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            Dim _validate As New AT_SWIPE_DATADTO
            Dim rep As New AttendanceRepository
            Dim lstEmp As New List(Of String)
            dtDataImportEmployee = dtData.Clone
            dtError = dtData.Clone
            dtError.Columns.Add("ID", GetType(String))
            Dim irow = 5
            Dim irowEm = 5
            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                sError = "Mã chấm công nhân viên không được để trống"
                ImportValidate.EmptyValue("ITIME_ID", row, rowError, isError, sError)

                sError = "Giờ không được để trống"
                ImportValidate.EmptyValue("VALTIME", row, rowError, isError, sError)

                sError = "Ngày không được để trống"
                ImportValidate.IsValidDate("WORKINGDAY", row, rowError, isError, sError)

                sError = "Giờ sai định dạng"
                If Not IsDate(row("WORKINGDAY") + " " + row("VALTIME")) Then
                    rowError("VALTIME") = sError
                    isError = True
                End If

                If isError Then
                    rowError("ID") = irow
                    dtError.Rows.Add(rowError)
                Else
                    dtDataImportEmployee.ImportRow(row)
                End If
                irow = irow + 1
                isError = False
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("EXPORTREPORT") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Import_SwipeData_Error')", True)
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
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien OkClicked khi click OK tren ctrlUpload
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked

        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim rep As New AttendanceRepository
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ctrlUpload.UploadedFiles.Count = 0 Then
                ShowMessage(Translate("Bạn chưa chọn biễu mẫu import"), NotifyType.Warning)
                Exit Sub
            End If
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            For Each file As UploadedFile In ctrlUpload.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                If workbook.Worksheets.GetSheetByCodeName("Import Data") Is Nothing Then
                    ShowMessage(Translate("File mẫu không đúng định dạng"), NotifyType.Warning)
                    Exit Sub
                End If
                worksheet = workbook.Worksheets(0)
                Dim lastRow = worksheet.Cells.GetLastDataRow(2)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(3, 1, lastRow, worksheet.Cells.MaxColumn, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            dsDataComper = dsDataPrepare.Tables(0).Clone()
            For Each dt As DataTable In dsDataPrepare.Tables
                For Each row In dt.Rows
                    Dim isRow = ImportValidate.TrimRow(row)
                    If Not isRow Then
                        Continue For
                    End If
                    dtData.ImportRow(row)
                    dsDataComper.ImportRow(row)
                Next
            Next
            If loadToGrid() Then
                dtData.TableName = "DATA"
                rep.ImportSwipeData(dtData)
                CurrentState = CommonMessage.STATE_NORMAL
                Refresh("InsertView")
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    'https://www.code-sample.net/CSharp/Format-DateTime
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim rep As New AttendanceRepository
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'check validate 
            If ctrlUpload1.UploadedFiles.Count = 0 Then
                ShowMessage(Translate(CommonMessage.CM_CTRLPROGRAMS_NOT_CHOOSE_FILE), NotifyType.Warning)
                Exit Sub
            End If
            If Not IsNumeric(cbMachine_Type.SelectedValue) Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_CHOOSE_MACHINE_TYPE), NotifyType.Warning)
                Exit Sub
            End If
            'end check validate

            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                Dim lastRow = worksheet.Cells.GetLastDataRow(2)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(2, 0, lastRow, worksheet.Cells.MaxColumn, True))

                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            dsDataComper = dsDataPrepare.Tables(0).Clone()
            For Each dt As DataTable In dsDataPrepare.Tables
                For Each row In dt.Rows
                    Dim isRow = ImportValidate.TrimRow(row)
                    If Not isRow Then
                        Continue For
                    End If
                    dtData.ImportRow(row)
                    dsDataComper.ImportRow(row)
                Next
            Next
            If loadToGrid() Then
                dtData.TableName = "DATA"
                rep.ImportSwipeData(dtData)
                CurrentState = CommonMessage.STATE_NORMAL
                Refresh("InsertView")
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub


    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Cap nhat trang thai toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Click khi click btnSearchEmp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearchEmp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchEmp.Click
        Try
            rglSwipeDataDownload.Rebind()
        Catch ex As Exception
        End Try

    End Sub

#End Region

End Class
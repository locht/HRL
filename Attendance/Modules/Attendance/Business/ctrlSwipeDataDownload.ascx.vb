Imports Common
Imports Attendance.AttendanceBusiness
Imports Telerik.Web.UI
Imports Framework.UI.Utilities
Imports Telerik.Web
Imports Framework.UI
Imports WebAppLog
Imports System.Web.Script.Serialization
Imports System.IO

Public Class ctrlSwipeDataDownload
    Inherits Common.CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()
    Const TOTAL_ROW_IMPORT As Integer = 100
    Dim log As New CommonBusiness.UserLog
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
    Private JSONDATA As List(Of DATA_IN)
    Private DATA_IN As DataTable
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
            log = LogHelper.GetUserLog
            InitControl()
            If Not IsPostBack Then
                ViewConfig(RadPane1)
                GirdConfig(rglSwipeDataDownload)
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

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("DOWNLOADDATA",
                                                                     ToolbarIcons.Calculator,
                                                                     ToolbarAuthorize.Special1,
                                                                     Translate("Tải dữ liệu")))

            'Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT",
            '                                                        ToolbarIcons.Import,
            '                                                        ToolbarAuthorize.Import,
            '                                                        Translate("Nhập file")))

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
            If IsNumeric(cbMachine_Type.SelectedValue) Then
                obj.MACHINE_TYPE = cbMachine_Type.SelectedValue
            End If
            Dim Sorts As String = rglSwipeDataDownload.MasterTableView.SortExpressions.GetSortString()
            If Not IsDate(rdStartDate.SelectedDate) And Not IsDate(rdEndDate.SelectedDate) Then
                Me.SWIPE_DATA = New List(Of AT_SWIPE_DATADTO)
            Else
                If Not isFull Then
                    If Sorts IsNot Nothing Then
                        Me.SWIPE_DATA = rep.GetSwipeData(obj, rglSwipeDataDownload.CurrentPageIndex, rglSwipeDataDownload.PageSize, MaximumRows, Sorts)
                    Else
                        Me.SWIPE_DATA = rep.GetSwipeData(obj, rglSwipeDataDownload.CurrentPageIndex, rglSwipeDataDownload.PageSize, MaximumRows)
                    End If
                Else
                    Return rep.GetSwipeData(obj).ToTable()
                End If
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
                    If IsNumeric(cbMachine_Type.SelectedValue) Then
                        ctrlUpload1.Show()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_CHOOSE_MACHINE_TYPE), NotifyType.Warning)
                        Exit Sub
                    End If
                Case "DOWNLOADDATA"

            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Function loadToGrid() As Boolean
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
            'Dim dtEmpID As DataTable
            'Dim is_Validate As Boolean
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

                'If row("ITIME_ID") IsNot DBNull.Value Then
                '    dtEmpID = New DataTable
                '    dtEmpID = rep.GetEmployeeByTimeID(row("ITIME_ID"))
                '    If dtEmpID Is Nothing Or dtEmpID.Rows.Count <= 0 Then
                '        rowError("ITIME_ID") = "Mã chấm công không tồn tại trên hệ thống."
                '        isError = True
                '    End If
                'End If

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
    ''' Bind du lieu cho rad grid
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Function loadToGridByConfig(ByVal dtConfig As DataTable) As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim dtdata As DataTable
            Using rep As New AttendanceRepository
                dtdata = rep.GetOtherList("TIME_RECORDER", False)
            End Using
            Dim MACHINE_TYPE_CODE As String = String.Empty
            If dtdata IsNot Nothing AndAlso dtdata.Rows.Count > 0 Then
                MACHINE_TYPE_CODE = (From P In dtdata.AsEnumerable Where P("ID") = cbMachine_Type.SelectedValue Select P("CODE")).FirstOrDefault
            End If
            DATA_IN = New DataTable("DATA_IN")
            'Create struct DATA IN with table config
            For Each row In dtConfig.Rows
                DATA_IN.Columns.Add(row("COLUMN_CODE").ToString.Trim, GetType(String))
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
            If DATA_IN.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            Else
                Return True
            End If

            'TAM THOI NGAT XU LY DEN LINE NAY
            JSONDATA = New List(Of DATA_IN)
            Dim ArrData As New ArrayList()
            For Each row As DataRow In (From P In DATA_IN.AsEnumerable Where P("USER_ID") <> String.Empty OrElse P("USER_ID") <> "" Select P)
                If Not IsNumeric(row("USER_ID")) OrElse row("USER_ID").ToString.Trim = String.Empty Then Continue For
                Dim objJSON As New DATA_IN
                Dim strCultureInfo As String = String.Empty
                objJSON.USER_ID = row("USER_ID").ToString()
                objJSON.MACHINE_TYPE = cbMachine_Type.SelectedValue
                If MACHINE_TYPE_CODE.ToUpper = "CP" Then 'CAR PARKING
                    objJSON.TERMINAL_ID = row("TERMINAL_ID")
                    If row("TERMINAL_ID") = 1 Then 'MAY IN
                        strCultureInfo = (From p In dtConfig.AsEnumerable Where p("COLUMN_CODE") = "TIME_IN" Select p("FORMAT")).FirstOrDefault
                        objJSON.WORKING_DAY = ConvertDateTo24H(row("TIME_IN"), strCultureInfo)
                    ElseIf row("TERMINAL_ID") = 2 Then 'MAY OUT
                        strCultureInfo = (From p In dtConfig.AsEnumerable Where p("COLUMN_CODE") = "TIME_OUT" Select p("FORMAT")).FirstOrDefault
                        objJSON.WORKING_DAY = ConvertDateTo24H(row("TIME_OUT"), strCultureInfo)
                    End If
                ElseIf MACHINE_TYPE_CODE = "VT" Then 'TOUCH ID
                    objJSON.TERMINAL_ID = row("TERMINAL_ID")
                    strCultureInfo = (From p In dtConfig.AsEnumerable Where p("COLUMN_CODE") = "WORKING_DAY" Select p("FORMAT")).FirstOrDefault
                    objJSON.WORKING_DAY = ConvertDateTo24H(row("WORKING_DAY"), strCultureInfo)
                ElseIf MACHINE_TYPE_CODE = "AC" Then ' ACCESS CONTROL
                    objJSON.TERMINAL_ID = ""
                    strCultureInfo = (From p In dtConfig.AsEnumerable Where p("COLUMN_CODE") = "WORKING_DAY" Select p("FORMAT")).FirstOrDefault
                    objJSON.WORKING_DAY = ConvertDateTo24H(row("WORKING_DAY"), strCultureInfo)
                End If
                JSONDATA.Add(objJSON)
            Next
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
            'LAY THONG TIN CONFIG TMPLATE => @PAR = MACHINE_TYPE 
            Dim fistRow As Integer = 0
            Dim fistCol As Integer = 0
            Dim file_type As String = String.Empty
            Dim IAttenDance As IAttendanceBusiness = New AttendanceBusinessClient()
            Dim dsConfig As DataSet = IAttenDance.GET_CONFIG_TEMPLATE(cbMachine_Type.SelectedValue)
            If dsConfig IsNot Nothing AndAlso dsConfig.Tables.Count = 2 AndAlso dsConfig.Tables(1) IsNot Nothing AndAlso dsConfig.Tables(1).Rows.Count = 1 Then
                fistRow = dsConfig.Tables(1)(0)("FIST_ROW")
                fistCol = dsConfig.Tables(1)(0)("FIST_COL")
                file_type = dsConfig.Tables(1)(0)("FILE_TYPE")
            End If
            'end get config
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & "." & file_type)
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                Dim lastRow = worksheet.Cells.GetLastDataRow(2) + 1
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(fistRow, fistCol, lastRow, worksheet.Cells.MaxColumn, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dsDataComper = dsDataPrepare.Tables(0).Clone()
            For Each dt As DataTable In dsDataPrepare.Tables
                For Each row In dt.Rows
                    Dim isRow = ImportValidate.TrimRow(row)
                    If Not isRow Then
                        Continue For
                    End If
                    dsDataComper.ImportRow(row)
                Next
            Next
            Dim thrIMPORT_DATA As Threading.Thread
            If loadToGridByConfig(dsConfig.Tables(0)) Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                DATA_IN.WriteXml(sw, False)
                DocXml = sw.ToString.Replace(vbCr, "").Replace(vbCrLf, "").Replace(vbLf, "").Trim
                IAttenDance.IMPORT_AT_SWIPE_DATA_V1(log, DocXml, cbMachine_Type.SelectedValue)
                CurrentState = CommonMessage.STATE_NORMAL
                Refresh("InsertView")

                Exit Sub 'NGAT LUONG LAM VIEC O DAY LAM THEO HUONG MOI
                Dim jsonSerialiser = New JavaScriptSerializer()
                Dim Index As Integer = 0
                Dim TotalRow As Integer = JSONDATA.Count
                Dim Sum As Integer = 0
                Dim subArray As DATA_IN()
                While (True)
                    If TotalRow <= TOTAL_ROW_IMPORT Then
                        subArray = New DATA_IN(TotalRow - 1) {}
                        JSONDATA.CopyTo(Index, subArray, 0, TotalRow)
                        Dim strJson = jsonSerialiser.Serialize(subArray)
                        'CAL FUNCTION IMPORT
                        IAttenDance.IMPORT_AT_SWIPE_DATA(log, strJson)
                        Sum += subArray.Length
                        Exit While
                    Else
                        Try
                            subArray = New DATA_IN(TOTAL_ROW_IMPORT - 1) {}
                            JSONDATA.CopyTo(Index, subArray, 0, TOTAL_ROW_IMPORT)
                            Dim strJson = jsonSerialiser.Serialize(subArray)
                            'CAL FUNCTION IMPORT
                            thrIMPORT_DATA = New Threading.Thread(New Threading.ThreadStart(Function()
                                                                                                Return IAttenDance.IMPORT_AT_SWIPE_DATA(log, strJson)
                                                                                            End Function))
                            thrIMPORT_DATA.IsBackground = True
                            thrIMPORT_DATA.Start()
                            'END CALL FUNCTION IMPORT
                            Index = Index + TOTAL_ROW_IMPORT
                            TotalRow = TotalRow - TOTAL_ROW_IMPORT
                            Sum += subArray.Length
                        Catch ex As Exception
                        End Try
                    End If
                End While
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

    Private Function ConvertDateTo24H(ByVal strDate As String, ByVal strCulture As String) As String
        Dim outDate As Date
        Dim StrOut As String = String.Empty
        Try
            If strDate = String.Empty Then Return String.Empty
            Dim culture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo(strCulture)
            outDate = Convert.ToDateTime(strDate, culture)
            StrOut = String.Format("{0}/{1}/{2} {3}:{4}", outDate.Day, outDate.Month, outDate.Year, outDate.Hour, outDate.Minute)
            Return StrOut
        Catch ex As Exception
            ShowMessage(strDate + " Loi format date:" + ex.ToString, NotifyType.Warning)
            'Return String.Empty
        End Try
    End Function
#End Region
End Class
Public Structure DATA_IN
    Public USER_ID As String
    Public TERMINAL_ID As String
    Public WORKING_DAY As String
    Public MACHINE_TYPE As String
End Structure
Imports Framework.UI
Imports Common
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI
Imports Framework.UI.Utilities
Imports Common.CommonMessage
Imports System.IO
Imports Aspose.Cells
Imports System.Globalization
Imports HistaffFrameworkPublic

Class ctrlPE_KPI_Evaluate
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Properties"

    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Private Property dtLogs As DataTable
        Get
            Return PageViewState(Me.ID & "_dtLogs")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtLogs") = value
        End Set
    End Property
    Property Upload4Emp As Decimal
        Get
            Return ViewState(Me.ID & "_Upload4Emp")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Upload4Emp") = value
        End Set
    End Property
    Property isRight As Decimal
        Get
            Return ViewState(Me.ID & "_isRight")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_isRight") = value
        End Set
    End Property
    Property EmployeeList As List(Of KPI_EVALUATEDTO)
        Get
            Return ViewState(Me.ID & "_EmployeeList")
        End Get
        Set(ByVal value As List(Of KPI_EVALUATEDTO))
            ViewState(Me.ID & "_EmployeeList") = value
        End Set
    End Property
#End Region
#Region "Page"
    ''' <summary>
    ''' Hiển thị thông tin trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        'Edit by: ChienNV 
        'Trước khi Load thì kiểm tra PostBack
        If Not IsPostBack Then
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Try
                Dim startTime As DateTime = DateTime.UtcNow
                ctrlOrganization.AutoPostBack = True
                ctrlOrganization.LoadDataAfterLoaded = True
                ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
                ctrlOrganization.CheckBoxes = TreeNodeTypes.None
                rgEmployeeList.SetFilter()
                Refresh()
                'For Each item As GridDataItem In rgEmployeeList.MasterTableView.Items
                '    item.Edit = False
                'Next
                '  _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                DisplayException(Me.ViewName, Me.ID, ex)
                '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            End Try
        Else
            ctrlOrganization.Enabled = True
            Exit Sub
        End If

    End Sub
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo các thiết lập, giá trị các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID

            rgEmployeeList.SetFilter()
            rgEmployeeList.AllowCustomPaging = True
            rgEmployeeList.PageSize = Common.Common.DefaultPageSize
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            ' _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' khởi tạo các thành phần trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Export,
                                       ToolbarItem.Next,
                                       ToolbarItem.Import
                                       )
            CType(Me.MainToolBar.Items(1), RadToolBarButton).Text = Translate("Xuất file mẫu")
            CType(Me.MainToolBar.Items(1), RadToolBarButton).ImageUrl = CType(Me.MainToolBar.Items(0), RadToolBarButton).ImageUrl
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Text = Translate("Nhập file mẫu")
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    ''' <summary>
    ''' Edit by: ChienNV;
    ''' Fill data in control cboPrintSupport
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Try
            GetDataCombo()
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Message = CommonMessage.ACTION_UPDATED Then
                rgEmployeeList.Rebind()
            End If
            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region
#Region "Event"


    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgEmployeeList.CurrentPageIndex = 0
            rgEmployeeList.Rebind()
            ' EnableOngrid()
            ' _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ' _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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
            '  Dim objdata As PE_EVALUATE_PERIODDTO
            Dim objEmployee As New KPI_EVALUATEDTO
            Dim rep As New PerformanceBusinessClient
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_EDIT
                    isRight = 1
                    ctrlOrganization.Enabled = False
                    For Each item As GridItem In rgEmployeeList.MasterTableView.Items
                        item.Edit = True
                    Next
                    CurrentState = CommonMessage.STATE_EDIT
                Case CommonMessage.TOOLBARITEM_SAVE
                  
                Case TOOLBARITEM_DELETE
                    'Kiểm tra các điều kiện để xóa.
                    If rgEmployeeList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi xóa"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgEmployeeList.ExportExcel(Server, Response, dtData, "ResultMBO")
                        Else
                            ShowMessage(Translate(MESSAGE_WARNING_EXPORT_EMPTY), Utilities.NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_NEXT
                    '  ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_ImportMBO');", True)
                    Template_ImportMBO()
                Case CommonMessage.TOOLBARITEM_IMPORT
                    If cboPeriodEvaluate.SelectedValue = "" Then
                        ShowMessage(Translate("Vui lòng chọn kỳ đánh giá để import"), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlUpload1.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    'isRight = 1
                    rgEmployeeList.Rebind()
                    '   EnableOngrid()
                    '
                    'EnabledGrid(rgEmployeeList, False)
                    '   rgEmployeeList.Rebind()
            End Select
            'rep.Dispose()
            UpdateControlState()
            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgEmployeeList.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Not IsPostBack Then
                rgEmployeeList.VirtualItemCount = 0
                rgEmployeeList.DataSource = New List(Of KPI_EVALUATEDTO)
            Else
                CreateDataFilter()
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            ' _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Dim MaximumRows As Integer
        Dim Sorts As String
        Dim _filter As New KPI_EVALUATEDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Using rep As New PerformanceRepository

                If ctrlOrganization.CurrentValue IsNot Nothing Then
                    _filter.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
                Else
                    rgEmployeeList.DataSource = New List(Of KPI_EVALUATEDTO)
                    Exit Function
                End If
                If cboPeriodEvaluate.SelectedValue <> "" Then
                    _filter.KPI_EVALUATE = cboPeriodEvaluate.SelectedValue
                End If

                Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                                .IS_DISSOLVE = ctrlOrganization.IsDissolve}

                SetValueObjectByRadGrid(rgEmployeeList, _filter)
                _filter.IS_TER = chkTerminate.Checked
                Sorts = rgEmployeeList.MasterTableView.SortExpressions.GetSortString()
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetListEmployeePaging(_filter, _param, Sorts).ToTable()
                    Else
                        Return rep.GetListEmployeePaging(_filter, _param).ToTable()
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        EmployeeList = rep.GetListEmployeePaging(_filter, rgEmployeeList.CurrentPageIndex, rgEmployeeList.PageSize, MaximumRows, _param, Sorts)
                    Else
                        EmployeeList = rep.GetListEmployeePaging(_filter, rgEmployeeList.CurrentPageIndex, rgEmployeeList.PageSize, MaximumRows, _param)
                    End If

                    rgEmployeeList.VirtualItemCount = MaximumRows
                    rgEmployeeList.DataSource = EmployeeList
                End If


            End Using
            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
    Private Sub GetDataCombo()
        Dim dtData As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Using rep As New PerformanceBusinessClient

                dtData = rep.GetlistYear()
                FillRadCombobox(cboYear, dtData, "YEAR", "ID")
            End Using
            ' _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgEmployeeList.Rebind()
            isRight = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Select Case CurrentState
            Case CommonMessage.STATE_EDIT
                ' EnabledGrid(rgEmployeeList, True, False)
                For Each item As GridDataItem In rgEmployeeList.MasterTableView.Items
                    Dim txtmark_offical As RadNumericTextBox = DirectCast(item("MARK_MBO_OFFICAL").FindControl("rnMBOGoc"), RadNumericTextBox)
                    txtmark_offical.Enabled = True
                    Dim txtmark_dc As RadNumericTextBox = DirectCast(item("MARK_MBO_EDIT").FindControl("nnMBODC"), RadNumericTextBox)
                    txtmark_dc.Enabled = True
                    Dim txtATTACH_FILE As LinkButton = DirectCast(item("UPLOAD_FILE").FindControl("lbtnUpload"), LinkButton)
                    txtATTACH_FILE.Enabled = True
                    Dim txtupload As RadButton = DirectCast(item("ID").FindControl("btnUpload"), RadButton)
                    txtupload.Enabled = True
                Next
                rgEmployeeList.Rebind()
            Case CommonMessage.STATE_NORMAL

        End Select
        ChangeToolbarState()
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
                        If InStr(",EVALUATE_PERIOD_NAME,MARK_MBO_OFFICAL,MARK_MBO_EDIT,UPLOAD_FILE,", "," + col.UniqueName + ",") > 0 Then
                            Select Case Item(col.UniqueName).Controls(1).ID.ToString.Substring(0, 2)
                                Case "cb"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadComboBox).SelectedValue
                                Case "rn"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox).Value
                                Case "nn"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox).Value
                                Case "lb"
                                    Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), LinkButton).Text
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
    Private Sub rgEmployee_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgEmployeeList.ItemCommand
        If e.CommandName = "UploadFile" Then
            Upload4Emp = e.CommandArgument
            ctrlUpload.Show()
        ElseIf e.CommandName = "DownloadFile" Then
            Dim url As String = "Download.aspx?" & "CtrlPE_Period_Evaluate," & e.CommandArgument
            Dim str As String = "window.open('" & url + "');"
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
        End If
    End Sub
    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked
        Dim fileName As String
        Try
            If ctrlUpload.UploadedFiles.Count > 1 Then
                ShowMessage("Chỉ được chọn 1 file để upload", NotifyType.Error)
                Exit Sub
            Else
                If ctrlUpload.UploadedFiles.Count > 0 Then
                    Dim file As UploadedFile = ctrlUpload.UploadedFiles(0)
                    fileName = Server.MapPath("~/ReportTemplates/Performance/LinkFile")
                    If Not Directory.Exists(fileName) Then
                        Directory.CreateDirectory(fileName)
                    End If
                    fileName = System.IO.Path.Combine(fileName, file.FileName)
                    file.SaveAs(fileName, True)

                    For Each abc As GridDataItem In rgEmployeeList.MasterTableView.Items
                        If abc.GetDataKeyValue("ID").ToString = Upload4Emp Then
                            Dim txtATTACH_FILE As LinkButton = DirectCast(abc("UPLOAD_FILE").FindControl("lbtnUpload"), LinkButton)
                            txtATTACH_FILE.Text = file.FileName
                            Exit For
                        End If
                    Next

                Else
                    ShowMessage(Translate("Chưa upload được file"), NotifyType.Error)
                End If

            End If
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi"), NotifyType.Error)
        End Try
    End Sub
#End Region

    Private Sub Template_ImportMBO()
        Try
            If cboPeriodEvaluate.SelectedValue = "" Then
                ShowMessage(Translate("Phải chọn kỳ đánh giá"), NotifyType.Warning)
                Exit Sub
            End If

            Using REP As New PerformanceRepository
                Dim dsData As New DataSet
                dsData = REP.GetExportKPI(cboPeriodEvaluate.SelectedValue)
                dsData.Tables(0).TableName = "Table"
                dsData.Tables(1).TableName = "Table1"
                dsData.Tables(2).TableName = "Table2"
                REP.Dispose()
                ExportTemplate("Performance\KPI\DANHGIAKPI.xls",
                                   dsData, Nothing, "DANHGIAKPI" & Format(Date.Now, "yyyyMMdd"))

            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dsData As DataSet,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dsData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim countFile As Integer = ctrlUpload1.UploadedFiles.Count
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
            TableMapping(ds.Tables(0))
            dtLogs.TableName = "DATA_ERROR"
            If dtLogs.Rows.Count > 0 Then
                ShowMessage(Translate("Đã có lỗi xảy ra,Xin kiểm tra lại"), NotifyType.Warning)
                HttpContext.Current.Session("KHDT_ERROR") = dtLogs
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('ACV_DANHGIAKPI_error');", True)
                Exit Sub
                'ExportTemplate("Training/QLKH/VNM_Kehoachdt_error.xls",
                '                      dtLogs.DataSet, Nothing, "Template_ERROR_" & Format(Date.Now, "yyyyMMdd"))
            End If
            Dim sw As New StringWriter()
            Dim DocXml As String = String.Empty
            ds.Tables(0).WriteXml(sw, False)
            DocXml = sw.ToString
            Dim callst As New PerformaceStoreProcedure
            If callst.Import_KPI(LogHelper.GetUserLog.Username.ToUpper, DocXml) Then
                ShowMessage(Translate("Import thành công"), NotifyType.Success)
                rgEmployeeList.Rebind()
            Else
                ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            End If
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub
    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
        dtTemp.Columns(0).ColumnName = "STT"
        dtTemp.Columns(1).ColumnName = "EMPLOYEE_CODE"
        dtTemp.Columns(2).ColumnName = "FULLNAME"
        dtTemp.Columns(3).ColumnName = "ORG_NAME"
        dtTemp.Columns(4).ColumnName = "ORG_NAME2"
        dtTemp.Columns(5).ColumnName = "TITLE_NAME"
        dtTemp.Columns(6).ColumnName = "SALARY_LEVEL"
        dtTemp.Columns(7).ColumnName = "SALARY_LEVEL_ID"
        dtTemp.Columns(8).ColumnName = "JOIN_DATE"
        dtTemp.Columns(9).ColumnName = "END_DATE"
        dtTemp.Columns(10).ColumnName = "FINANCE_TT"
        dtTemp.Columns(11).ColumnName = "FINANCE_TTX"
        dtTemp.Columns(12).ColumnName = "CUSTOMER_TT"
        dtTemp.Columns(13).ColumnName = "CUSTOMER_TTX"
        dtTemp.Columns(14).ColumnName = "PROCESS_TT"
        dtTemp.Columns(15).ColumnName = "PROCESS_TTX"
        dtTemp.Columns(16).ColumnName = "LEARN_TT"
        dtTemp.Columns(17).ColumnName = "LEARN_TTX"
        dtTemp.Columns(18).ColumnName = "SUM_TT"
        dtTemp.Columns(19).ColumnName = "SUM_TTX"
        dtTemp.Columns(20).ColumnName = "SUM_RATE_KPI"
        dtTemp.Columns(21).ColumnName = "CLASSFICATION"
        dtTemp.Columns(22).ColumnName = "CLASSFICATION_ID"
        dtTemp.Columns(23).ColumnName = "COMMENTS"
        dtTemp.Columns(24).ColumnName = "REMARK"
        dtTemp.Columns(25).ColumnName = "KPI_ID"
        'XOA DONG TIEU DE VA HEADER
        dtTemp.Rows(0).Delete()
        dtTemp.Rows(0).Delete()
        dtTemp.Rows(0).Delete()
        dtTemp.Rows(0).Delete()
        dtTemp.Rows(0).Delete()
        dtTemp.Rows(0).Delete()
        dtTemp.Rows(0).Delete()
        dtTemp.Rows(0).Delete()
        dtTemp.Rows(0).Delete()

        dtTemp.Rows(1).Delete()
        dtTemp.Rows(2).Delete()
        dtTemp.Rows(3).Delete()
        dtTemp.Rows(4).Delete()
        dtTemp.Rows(5).Delete()
        dtTemp.Rows(6).Delete()
        dtTemp.Rows(7).Delete()
        ' add Log
        Dim _error As Boolean = True
        Dim count As Integer
        Dim newRow As DataRow
        Dim dsEMP As DataTable
        If dtLogs Is Nothing Then
            dtLogs = New DataTable("data")
            dtLogs.Columns.Add("STT", GetType(Integer))
            dtLogs.Columns.Add("DISCIPTION", GetType(String))
        End If
        dtLogs.Clear()
        'XOA NHUNG DONG DU LIEU NULL STT
        Dim rowDel As DataRow
        For i As Integer = 0 To dtTemp.Rows.Count - 1
            If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
            rowDel = dtTemp.Rows(i)
            If rowDel("STT").ToString.Trim = "" Then
                dtTemp.Rows(i).Delete()
            End If
        Next
        For Each rows As DataRow In dtTemp.Rows
            If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For
            newRow = dtLogs.NewRow
            newRow("STT") = count + 1
            If IsDBNull(rows("STT")) Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "STT - Phải nhập STT"
                _error = False
            End If
            If IsDBNull(rows("EMPLOYEE_CODE")) Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "MÃ NV - Phải nhập MÃ NV"
                _error = False
            End If
            If IsDBNull(rows("SALARY_LEVEL")) Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "NGẠCH - Phải nhập NGẠCH"
                _error = False
            End If
            If IsDBNull(rows("SUM_TT")) Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Tỷ trọng - Phải nhập Tỷ trọng"
                _error = False
            End If
            If IsDBNull(rows("SUM_TTX")) Or rows("SUM_TTX") = 0.0 Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Tỷ trọng x Điểm - Phải nhập Tỷ trọng x Điểm"
                _error = False
            End If
            If IsDBNull(rows("SUM_RATE_KPI")) Or rows("SUM_RATE_KPI") = 0.0 Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Tỷ lệ đạt KPI tương ứngKPI  - Phải nhập Tỷ lệ đạt KPI tương ứngKPI "
                _error = False
            End If
            If IsDBNull(rows("CLASSFICATION")) Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Xếp loại  - Phải nhập Xếp loại"
                _error = False
            End If
            rows("KPI_ID") = cboPeriodEvaluate.SelectedValue
            If _error = False Then
                dtLogs.Rows.Add(newRow)
                _error = True
            End If
            count += 1
        Next
        dtTemp.AcceptChanges()
    End Sub

    Private Sub cboYear_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
        Try
            Using rep As New PerformanceBusinessClient
                If cboYear.Text <> "" Then
                    Dim dtData As New DataTable
                    dtData = rep.GetLstPeriod(Decimal.Parse(cboYear.Text))
                    FillRadCombobox(cboPeriodEvaluate, dtData, "NAME", "ID")
                Else
                    ClearControlValue(cboPeriodEvaluate, rdFrom, rdTo)
                End If
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    'Private Sub EnableOngrid()
    '    Try
    '        For Each item As GridDataItem In rgEmployeeList.MasterTableView.Items
    '            Dim txtmark_offical As RadNumericTextBox = DirectCast(item("MARK_MBO_OFFICAL").FindControl("rnMBOGoc"), RadNumericTextBox)
    '            txtmark_offical.Enabled = False
    '            Dim txtmark_dc As RadNumericTextBox = DirectCast(item("MARK_MBO_EDIT").FindControl("nnMBODC"), RadNumericTextBox)
    '            txtmark_dc.Enabled = False
    '            Dim txtATTACH_FILE As LinkButton = DirectCast(item("UPLOAD_FILE").FindControl("lbtnUpload"), LinkButton)
    '            txtATTACH_FILE.Enabled = True
    '            Dim txtupload As RadButton = DirectCast(item("ID").FindControl("btnUpload"), RadButton)
    '            txtupload.Enabled = False
    '        Next
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    'Private Sub rgEmployeeList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgEmployeeList.ItemDataBound
    '    If isRight = 0 Then
    '        If rgEmployeeList.Items.Count = 0 Then
    '            Exit Sub
    '        End If
    '        EnableOngrid()
    '    End If
    'End Sub

    Private Sub cboPeriodEvaluate_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPeriodEvaluate.SelectedIndexChanged
        Try
            Using rep As New PerformanceBusinessClient
                If cboPeriodEvaluate.SelectedValue <> "" Then
                    Dim dtData As PeriodDTO
                    dtData = rep.GetPeriodDate(cboPeriodEvaluate.SelectedValue)
                    rdFrom.SelectedDate = dtData.START_DATE
                    rdTo.SelectedDate = dtData.END_DATE
                Else
                    ClearControlValue(rdFrom, rdTo)
                End If
            End Using
        Catch ex As Exception

        End Try
    End Sub
End Class
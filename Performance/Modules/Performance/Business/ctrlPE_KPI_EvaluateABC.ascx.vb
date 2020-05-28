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
Imports Common.CommonBusiness

Class ctrlPE_KPI_EvaluateABC
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Properties"

    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim log As New UserLog
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
    Property EmployeeList As List(Of PE_EVALUATE_PERIODDTO)
        Get
            Return ViewState(Me.ID & "_EmployeeList")
        End Get
        Set(ByVal value As List(Of PE_EVALUATE_PERIODDTO))
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
            'EnableOngrid()
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
            log = LogHelper.GetUserLog
            '  Dim objdata As PE_EVALUATE_PERIODDTO
            Dim objEmployee As New Performance.PerformanceBusiness.EmployeeDTO
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
                    If cboYear.SelectedValue Is Nothing Or cboYear.SelectedValue = "" Then
                        ShowMessage(Translate("Năm bắt buộc chọn"), NotifyType.Warning)
                        Exit Sub
                    End If

                    If cboPeriodEvaluate.SelectedValue Is Nothing Or cboPeriodEvaluate.SelectedValue = "" Then
                        ShowMessage(Translate("Kỳ đánh giá bắt buộc chọn"), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlUpload1.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    'isRight = 1
                    rgEmployeeList.Rebind()
                    'EnableOngrid()

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
                rgEmployeeList.DataSource = New List(Of Performance.PerformanceBusiness.PE_EVALUATE_PERIODDTO)
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
        Dim _filter As New PE_EVALUATE_PERIODDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Using rep As New PerformanceRepository

                If cboPeriodEvaluate.SelectedValue <> "" Then
                    _filter.PERIOD_ID = cboPeriodEvaluate.SelectedValue
                End If

                Dim _param = New Performance.PerformanceBusiness.ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                                .IS_DISSOLVE = ctrlOrganization.IsDissolve}

                SetValueObjectByRadGrid(rgEmployeeList, _filter)

                Sorts = rgEmployeeList.MasterTableView.SortExpressions.GetSortString()
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetPeEvaluatePeriod(_filter, _param, Sorts).ToTable()
                    Else
                        Return rep.GetPeEvaluatePeriod(_filter, _param).ToTable()
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        EmployeeList = rep.GetPeEvaluatePeriod(_filter, _param, rgEmployeeList.CurrentPageIndex, rgEmployeeList.PageSize, MaximumRows)
                    Else
                        EmployeeList = rep.GetPeEvaluatePeriod(_filter, _param, rgEmployeeList.CurrentPageIndex, rgEmployeeList.PageSize, MaximumRows)
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
                dtData = rep.GET_LIST_YEAR()
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
            'Case CommonMessage.STATE_EDIT
            '    ' EnabledGrid(rgEmployeeList, True, False)
            '    For Each item As GridDataItem In rgEmployeeList.MasterTableView.Items
            '        Dim txtmark_offical As RadNumericTextBox = DirectCast(item("MARK_MBO_OFFICAL").FindControl("rnMBOGoc"), RadNumericTextBox)
            '        txtmark_offical.Enabled = True
            '        Dim txtmark_dc As RadNumericTextBox = DirectCast(item("MARK_MBO_EDIT").FindControl("nnMBODC"), RadNumericTextBox)
            '        txtmark_dc.Enabled = True
            '        Dim txtATTACH_FILE As LinkButton = DirectCast(item("UPLOAD_FILE").FindControl("lbtnUpload"), LinkButton)
            '        txtATTACH_FILE.Enabled = True
            '        Dim txtupload As RadButton = DirectCast(item("ID").FindControl("btnUpload"), RadButton)
            '        txtupload.Enabled = True
            '    Next
            '    rgEmployeeList.Rebind()
            Case CommonMessage.STATE_NORMAL

        End Select
        ChangeToolbarState()
    End Sub
    'Private Function GetDataFromGrid(ByVal gr As RadGrid) As DataTable
    '    Dim bsSource As DataTable
    '    Try
    '        bsSource = New DataTable()
    '        For Each Col As GridColumn In gr.Columns
    '            Dim DataColumn As DataColumn = New DataColumn(Col.UniqueName)
    '            bsSource.Columns.Add(DataColumn)
    '        Next
    '        'coppy data to grid
    '        For Each Item As GridDataItem In gr.EditItems
    '            If Item.Display = False Then Continue For
    '            Dim Dr As DataRow = bsSource.NewRow()
    '            For Each col As GridColumn In gr.Columns
    '                Try
    '                    If col.UniqueName = "cbStatus" Then
    '                        If Item.Selected = True Then
    '                            Dr(col.UniqueName) = 1
    '                        Else
    '                            Dr(col.UniqueName) = 0
    '                        End If
    '                        Continue For
    '                    End If
    '                    If InStr(",EVALUATE_PERIOD_NAME,MARK_MBO_OFFICAL,MARK_MBO_EDIT,UPLOAD_FILE,", "," + col.UniqueName + ",") > 0 Then
    '                        Select Case Item(col.UniqueName).Controls(1).ID.ToString.Substring(0, 2)
    '                            Case "cb"
    '                                Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadComboBox).SelectedValue
    '                            Case "rn"
    '                                Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox).Value
    '                            Case "nn"
    '                                Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), RadNumericTextBox).Value
    '                            Case "lb"
    '                                Dr(col.UniqueName) = CType(Item.FindControl(Item(col.UniqueName).Controls(1).ID.ToString), LinkButton).Text
    '                        End Select
    '                    Else
    '                        Dr(col.UniqueName) = Item.GetDataKeyValue(col.UniqueName)
    '                    End If
    '                Catch ex As Exception
    '                    Continue For
    '                End Try
    '            Next
    '            bsSource.Rows.Add(Dr)
    '        Next
    '        bsSource.AcceptChanges()
    '        Return bsSource
    '    Catch ex As Exception
    '    End Try
    'End Function
    'Private Sub rgEmployee_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgEmployeeList.ItemCommand
    '    If e.CommandName = "UploadFile" Then
    '        Upload4Emp = e.CommandArgument
    '        ctrlUpload.Show()
    '    ElseIf e.CommandName = "DownloadFile" Then
    '        Dim url As String = "Download.aspx?" & "CtrlPE_Period_Evaluate," & e.CommandArgument
    '        Dim str As String = "window.open('" & url + "');"
    '        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
    '    End If
    'End Sub
    'Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked
    '    Dim fileName As String
    '    Try
    '        If ctrlUpload.UploadedFiles.Count > 1 Then
    '            ShowMessage("Chỉ được chọn 1 file để upload", NotifyType.Error)
    '            Exit Sub
    '        Else
    '            If ctrlUpload.UploadedFiles.Count > 0 Then
    '                Dim file As UploadedFile = ctrlUpload.UploadedFiles(0)
    '                fileName = Server.MapPath("~/ReportTemplates/Performance/LinkFile")
    '                If Not Directory.Exists(fileName) Then
    '                    Directory.CreateDirectory(fileName)
    '                End If
    '                fileName = System.IO.Path.Combine(fileName, file.FileName)
    '                file.SaveAs(fileName, True)

    '                For Each abc As GridDataItem In rgEmployeeList.MasterTableView.Items
    '                    If abc.GetDataKeyValue("ID").ToString = Upload4Emp Then
    '                        Dim txtATTACH_FILE As LinkButton = DirectCast(abc("UPLOAD_FILE").FindControl("lbtnUpload"), LinkButton)
    '                        txtATTACH_FILE.Text = file.FileName
    '                        Exit For
    '                    End If
    '                Next

    '            Else
    '                ShowMessage(Translate("Chưa upload được file"), NotifyType.Error)
    '            End If

    '        End If
    '    Catch ex As Exception
    '        ShowMessage(Translate("Import bị lỗi"), NotifyType.Error)
    '    End Try
    'End Sub
#End Region

    Private Sub Template_ImportMBO()
        Try
            If cboPeriodEvaluate.SelectedValue = "" Then
                ShowMessage(Translate("Phải chọn kỳ đánh giá"), NotifyType.Warning)
                Exit Sub
            End If

            Using REP As New PerformanceRepository
                Dim dsData As New DataSet
                dsData = REP.EXPORT_EVALUATE_ABC(cboPeriodEvaluate.SelectedValue)
                ExportTemplate("Performance\Report\Template_import_ABC.xls",
                                   dsData, Nothing, "Import_DanhGiaABC" & Format(Date.Now, "yyyyMMdd"))

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
        Dim rep As New PerformanceRepository
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
            If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then

                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    If rep.INPORT_EVALUATE_ABC(DocXml, cboPeriodEvaluate.SelectedValue, log.Username) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgEmployeeList.Rebind()
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
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub
    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
        dtTemp.Columns(0).ColumnName = "STT"
        dtTemp.Columns(1).ColumnName = "EMPLOYEE_ID"
        dtTemp.Columns(6).ColumnName = "SAL_LEVEL"
        dtTemp.Columns(9).ColumnName = "RESPONSIBILITY"
        dtTemp.Columns(10).ColumnName = "COOPERATION"
        dtTemp.Columns(11).ColumnName = "DILIGENCE"
        dtTemp.Columns(12).ColumnName = "TOTAL"
        dtTemp.Columns(13).ColumnName = "CLASSIFICATION"
        dtTemp.Columns(14).ColumnName = "COMMENT1"
        dtTemp.Columns(15).ColumnName = "NOTE"
        'XOA DONG TIEU DE VA HEADER
        dtTemp.Rows(0).Delete()
        dtTemp.Rows(1).Delete()
        dtTemp.Rows(2).Delete()
        dtTemp.Rows(3).Delete()
        dtTemp.Rows(4).Delete()
        dtTemp.Rows(5).Delete()
        dtTemp.Rows(6).Delete()
        dtTemp.Rows(7).Delete()
        dtTemp.Rows(8).Delete()
        dtTemp.Rows(9).Delete()

        ' add Log
        Dim _error As Boolean = True
        Dim count As Integer
        Dim newRow As DataRow
        Dim dsEMP As DataTable
        Dim empId As Integer
        Dim rep As New PerformanceRepository
        If dtLogs Is Nothing Then
            dtLogs = New DataTable("data")
            dtLogs.Columns.Add("STT", GetType(Integer))
            dtLogs.Columns.Add("EMPLOYEE_CODE", GetType(String))
            dtLogs.Columns.Add("DISCIPTION", GetType(String))
        End If
        dtLogs.Clear()
        'XOA NHUNG DONG DU LIEU NULL STT
        Dim rowDel As DataRow
        For i As Integer = 0 To dtTemp.Rows.Count - 1
            If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
            rowDel = dtTemp.Rows(i)
            If rowDel("EMPLOYEE_ID").ToString.Trim = "" Then
                dtTemp.Rows(i).Delete()
            End If
        Next
        For Each rows As DataRow In dtTemp.Rows
            If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For
            newRow = dtLogs.NewRow
            newRow("STT") = count + 1
            newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_ID")

            empId = rep.CheckEmployee_Exits(rows("EMPLOYEE_ID"))

            If empId = 0 Then
                newRow("DISCIPTION") = "Mã nhân viên - Không tồn tại,"
                _error = False
            End If

            If IsDBNull(rows("SAL_LEVEL")) OrElse rows("SAL_LEVEL") = "" Then
                rows("SAL_LEVEL") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngạch lương - bắt buộc nhập,"
                _error = False
            End If

            If IsDBNull(rows("RESPONSIBILITY")) Then
            Else
                If IsNumeric(rows("RESPONSIBILITY")) = False Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Tinh thần trách nhiệm - Không đúng định dạng,"
                    _error = False
                End If
            End If

            If IsDBNull(rows("COOPERATION")) Then
            Else
                If IsNumeric(rows("COOPERATION")) = False Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Tính hợp tác - Không đúng định dạng,"
                    _error = False
                End If
            End If

            If IsDBNull(rows("DILIGENCE")) Then
            Else
                If IsNumeric(rows("DILIGENCE")) = False Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Tính chuyên cần - Không đúng định dạng,"
                    _error = False
                End If
            End If

            If IsNumeric(rows("TOTAL")) = False Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "TỔNG CỘNG - Không đúng định dạng,"
                _error = False
            End If

            If IsDBNull(rows("CLASSIFICATION")) OrElse rows("CLASSIFICATION") = "" Then
                rows("CLASSIFICATION") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "XẾP LOẠI - bắt buộc nhập,"
                _error = False
            End If

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
                Dim dtData As New DataTable
                dtData = rep.GET_PERIOD_BY_YEAR(cboYear.SelectedValue)
                FillRadCombobox(cboPeriodEvaluate, dtData, "NAME", "ID")
                cboPeriodEvaluate.SelectedValue = Nothing
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cboPeriodEvaluate_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPeriodEvaluate.SelectedIndexChanged
        Try
            If cboPeriodEvaluate.SelectedValue <> "" Then
                Using rep As New PerformanceBusinessClient
                    Dim dtData As New DataTable
                    dtData = rep.GET_DATE_BY_PERIOD(cboPeriodEvaluate.SelectedValue)
                    rdFrom.SelectedDate = dtData.Rows(0)("START_DATE")
                    rdTo.SelectedDate = dtData.Rows(0)("END_DATE")
                End Using
            Else
                rdFrom.SelectedDate = Nothing
                rdTo.SelectedDate = Nothing
            End If
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
End Class
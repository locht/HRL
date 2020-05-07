Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Telerik.Web.UI
Imports Aspose.Cells
Imports System.IO
Imports HistaffFrameworkPublic
Imports Common.CommonBusiness

Public Class ctrlInsInformationsMng
    Inherits Common.CommonView
    'Private WithEvents viewRegister As ctrlShiftPlanningRegister

    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlOrgSULPPopup As ctrlFindOrgPopup

    Public Property popup As RadWindow
    Public Property popupId As String
    Dim log As New UserLog
    Public Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
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

#Region "Property & Variable"

    Private Property LastStartDate As Nullable(Of Date)
        Get
            Return PageViewState("LastStartDate")
        End Get
        Set(ByVal value As Nullable(Of Date))
            PageViewState("LastStartDate") = value
        End Set
    End Property

    Private Property LastEndDate As Nullable(Of Date)
        Get
            Return PageViewState("LastEndDate")
        End Get
        Set(ByVal value As Nullable(Of Date))
            PageViewState("LastEndDate") = value
        End Set
    End Property


    Public ReadOnly Property PageId As String
        Get
            Return Me.ID
        End Get
    End Property

    Public Property InsCompList As DataTable
        Get
            Return ViewState(Me.ID & "_InsCompList")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_InsCompList") = value
        End Set
    End Property

    Public Property SelectedItemList As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_SelectedItemList")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_SelectedItemList") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try

            rgGridData.AllowCustomPaging = True
            rgGridData.PageSize = Common.Common.DefaultPageSize
            popup = CType(Me.Page, AjaxPage).PopupWindow
            popup.Title = "Thông tin Bảo hiểm chi tiết"
            popupId = popup.ClientID

            Me.MainToolBar = rtbMain
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Export,
                                       ToolbarItem.Next,
                                       ToolbarItem.Import)

            CType(Me.MainToolBar.Items(4), RadToolBarButton).Text = Translate("Xuất file mẫu")
            CType(Me.MainToolBar.Items(4), RadToolBarButton).ImageUrl = CType(Me.MainToolBar.Items(3), RadToolBarButton).ImageUrl
            CType(Me.MainToolBar.Items(5), RadToolBarButton).Text = Translate("Nhập file mẫu")
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            'rgGridData.SetFilter()
            If Not IsPostBack Then
                txtID.Text = "0"
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            Call LoadDataGrid()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        log = LogHelper.GetUserLog
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                Case CommonMessage.TOOLBARITEM_EDIT
                    If txtID.Text = "0" Then
                        ShowMessage(Translate("Bạn chưa chọn nội dung cần sửa."), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgGridData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn nội dung cần xóa."), NotifyType.Warning)
                        Exit Sub
                    End If
                    If CheckData_Delete() Then
                        ctrlMessageBox.MessageText = Translate("Bạn có muốn xóa dữ liệu ?")
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else
                        ShowMessage(Translate("Dữ liệu đang dùng không được xóa"), NotifyType.Warning)
                    End If

                Case CommonMessage.TOOLBARITEM_HIEXT
                    Response.Redirect("Default.aspx?mid=Insurance&fid=ctrlInsHealthExt&group=Business")

                Case CommonMessage.TOOLBARITEM_HIUPDATEINFO
                    Response.Redirect("Default.aspx?mid=Insurance&fid=ctrlInsHealthImport&group=Business")

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDataExport As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsInfomation(Common.Common.GetUsername(), InsCommon.getNumber(0) _
                                                                   , txtEMPLOYEEID_SEARCH.Text _
                                                                   , InsCommon.getNumber(ctrlOrg.CurrentValue) _
                                                                   , InsCommon.getNumber(IIf(chkSTATUS.Checked, 1, 0)) _
                                                                   , InsCommon.getNumber(IIf(ctrlOrg.IsDissolve, 1, 0))
                                                                   )
                        If dtDataExport.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtDataExport.Rows.Count > 0 Then
                            rgGridData.ExportExcel(Server, Response, dtDataExport, "DsBaoHiem")
                            Exit Sub
                        End If
                    End Using

                Case CommonMessage.TOOLBARITEM_NEXT
                    Dim rep1 As New InsuranceRepository
                    Dim dtDATA As DataSet = rep.EXPORT_INS_INFORMATION(log.Username.ToUpper, Decimal.Parse(ctrlOrg.CurrentValue), ctrlOrg.IsDissolve)

                    ExportTemplate("Insurance\Import\Import thong tin bao hiem.xls",
                                   dtDATA, Nothing, "Import thong tin bao hiem" & Format(Date.Now, "yyyyMMdd"))
                    rep1.Dispose()
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly upload file khi click button [OK] o popup ctrlUpload
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim fileName As String
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim rep As New InsuranceRepository
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
            If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then

                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    If rep.INPORT_INS_INFORMATION(DocXml, log.Username) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgGridData.Rebind()
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
        dtTemp.Columns(0).ColumnName = "EMPLOYEE_CODE"
        dtTemp.Columns(5).ColumnName = "SOCIAL_NUMBER"
        dtTemp.Columns(6).ColumnName = "HEALTH_NUMBER"
        dtTemp.Columns(7).ColumnName = "PROVINCE_NAME"
        dtTemp.Columns(8).ColumnName = "HEALTH_AREA_NAME"
        
        'XOA DONG TIEU DE VA HEADER
        dtTemp.Rows(0).Delete()

        ' add Log
        Dim _error As Boolean = True
        Dim count As Integer
        Dim newRow As DataRow
        Dim rep As New InsuranceRepository
        Dim empId As Integer
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
            If rowDel("EMPLOYEE_CODE").ToString.Trim = "" Then
                dtTemp.Rows(i).Delete()
            End If
        Next
        For Each rows As DataRow In dtTemp.Rows
            If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For
            newRow = dtLogs.NewRow
            newRow("STT") = count + 1
            newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")

            empId = rep.CheckEmployee_Exits(rows("EMPLOYEE_CODE"))

            If empId = 0 Then
                newRow("DISCIPTION") = "Mã nhân viên - Không tồn tại,"
                _error = False
            Else
                rows("EMPLOYEE_CODE") = empId
            End If

            If _error = False Then
                dtLogs.Rows.Add(newRow)
                _error = True
            End If
            count += 1
        Next
        dtTemp.AcceptChanges()
    End Sub

    Private Sub rgGridData_ExcelExportCellFormatting(ByVal sender As Object, ByVal e As ExportCellFormattingEventArgs) Handles rgGridData.ExportCellFormatting
        Try
            e.Cell.Style("mso-number-format") = "\@"
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rgGridData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGridData.NeedDataSource
        Call LoadDataGrid(False)
    End Sub

    Protected Function RepareDataForAction() As List(Of Decimal)
        Dim lst As New List(Of Decimal)
        For Each dr As Telerik.Web.UI.GridDataItem In rgGridData.SelectedItems
            lst.Add(dr.GetDataKeyValue("ID"))
        Next
        Return lst
    End Function

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Call DeleteData()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Function CheckData_Delete() As Boolean
        Try
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub btnFIND_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFind.Click
        Call LoadDataGrid()
    End Sub

#End Region

#Region "Function & Sub"

    Private Sub Export()
        Try
            Dim _error As String = ""

            Dim dtVariable = New DataTable
            dtVariable.Columns.Add(New DataColumn("FROM_DATE"))
            dtVariable.Columns.Add(New DataColumn("TO_DATE"))
            Dim drVariable = dtVariable.NewRow()
            drVariable("FROM_DATE") = Now.ToString("dd/MM/yyyy")
            drVariable("TO_DATE") = Now.ToString("dd/MM/yyyy")
            dtVariable.Rows.Add(drVariable)

            Using xls As New ExcelCommon

                Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsInfomation(Common.Common.GetUsername(), 0 _
                                                                                        , txtEMPLOYEEID_SEARCH.Text _
                                                                                        , InsCommon.getNumber(ctrlOrg.CurrentValue) _
                                                                                        , InsCommon.getNumber(IIf(chkSTATUS.Checked, 1, 0)) _
                                                                                        , InsCommon.getNumber(ctrlOrg.IsDissolve)
                                                                                        )
                lstSource.TableName = "TABLE"

                Dim bCheck = xls.ExportExcelTemplate(Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/" & Request.Params("group") & "/InsInfomation.xlsx"),
                    "ThongTinBH", lstSource, dtVariable, Response, _error, ExcelCommon.ExportType.Excel)
                If Not bCheck Then
                    Select Case _error
                        Case 1
                            ShowMessage(Translate("Mẫu báo cáo không tồn tại"), NotifyType.Warning)
                        Case 2
                            ShowMessage(Translate("Dữ liệu không tồn tại"), NotifyType.Warning)
                    End Select
                    Exit Sub
                End If
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadDataGrid(Optional ByVal IsDataBind As Boolean = True)
        Try
            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsInfomation(Common.Common.GetUsername(), InsCommon.getNumber(0) _
                                                                    , txtEMPLOYEEID_SEARCH.Text _
                                                                    , InsCommon.getNumber(ctrlOrg.CurrentValue) _
                                                                    , InsCommon.getNumber(IIf(chkSTATUS.Checked, 1, 0)) _
                                                                    , InsCommon.getNumber(IIf(ctrlOrg.IsDissolve, 1, 0))
                                                                    )
            Dim maximumRows As Double = 0
            Dim startRowIndex As Double = 0
            Dim dtb As New DataTable
            If lstSource.Rows.Count = 0 Then
                rgGridData.DataSource = lstSource
                rgGridData.MasterTableView.VirtualItemCount = 0
                rgGridData.CurrentPageIndex = rgGridData.MasterTableView.CurrentPageIndex
                If IsDataBind Then
                    rgGridData.DataBind()
                End If
                Return
            Else
                Dim filterExp = rgGridData.MasterTableView.FilterExpression
                If lstSource.Select(filterExp).AsEnumerable.Count = 0 Then
                    rgGridData.DataSource = dtb
                    rgGridData.MasterTableView.GroupsDefaultExpanded = True
                    rgGridData.MasterTableView.VirtualItemCount = lstSource.Rows.Count
                    rgGridData.CurrentPageIndex = rgGridData.MasterTableView.CurrentPageIndex
                    If IsDataBind Then
                        rgGridData.DataBind()
                    End If
                    Return
                Else
                    lstSource = lstSource.Select(filterExp).AsEnumerable.CopyToDataTable
                End If
                startRowIndex = IIf(lstSource.Rows.Count <= rgGridData.PageSize, 0, rgGridData.CurrentPageIndex * rgGridData.PageSize)
                maximumRows = IIf(lstSource.Rows.Count <= rgGridData.PageSize, lstSource.Rows.Count, Math.Min(startRowIndex + rgGridData.PageSize, lstSource.Rows.Count))
                dtb = lstSource.Clone()
                For i As Integer = startRowIndex To maximumRows - 1
                    dtb.ImportRow(lstSource.Rows(i))
                Next
            End If
            rgGridData.DataSource = dtb
            rgGridData.MasterTableView.VirtualItemCount = lstSource.Rows.Count
            rgGridData.CurrentPageIndex = rgGridData.MasterTableView.CurrentPageIndex
            If IsDataBind Then
                rgGridData.DataBind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub DeleteData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            Dim isFail As Boolean
            Dim isResult As Boolean
            Dim lstID As String = ""
            For i As Integer = 0 To rgGridData.SelectedItems.Count - 1
                Dim item As GridDataItem = rgGridData.SelectedItems(i)
                If i = rgGridData.SelectedItems.Count - 1 Then
                    lstID = lstID & item("ID").Text.ToString
                Else
                    lstID = lstID & item("ID").Text.ToString & ","
                End If
                isResult = rep.DeleteInsInfomation(Common.Common.GetUsername(), InsCommon.getString(lstID))
                If isResult = False Then
                    isFail = True
                End If
            Next
            If isFail = False Then
                Refresh("UpdateView")
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Try
            LoadDataGrid()
        Catch ex As Exception

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
End Class
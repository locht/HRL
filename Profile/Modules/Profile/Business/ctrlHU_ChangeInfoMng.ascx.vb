Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports System.IO
Imports WebAppLog
Imports HistaffWebAppResources.My.Resources
Public Class ctrlHU_ChangeInfoMng
    Inherits Common.CommonView

    ''' <summary>
    ''' ctrl FindOrgPopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary>ListComboData</summary>
    ''' <remarks></remarks>
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary>IsLoad</summary>
    ''' <remarks></remarks>
    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsUpdate")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsUpdate") = value
        End Set
    End Property

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary>isLoadPopup</summary>
    ''' <remarks></remarks>
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    ''' <summary>
    ''' bExist
    ''' </summary>
    ''' <remarks></remarks>
    Dim bExist As Boolean = False

#End Region

#Region "Page"

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Refresh()
            UpdateControlState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rgWorking.SetFilter()
            rgWorking.AllowCustomPaging = True

            ctrlUpload1.isMultiple = AsyncUpload.MultipleFileSelection.Disabled
            ctrlUpload1.MaxFileInput = 1
            If Not IsPostBack Then
                ViewConfig(LeftPane)
                GirdConfig(rgWorking)
            End If
            InitControl()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary>Fill du lieu len combobox</summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            GetDataCombo()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.MainToolBar = tbarWorkings

            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Delete,
                                       ToolbarItem.Export, ToolbarItem.Print)
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_CREATE_BATCH,
                                                                  ToolbarIcons.Add,
                                                                  ToolbarAuthorize.None,
                                                                  "Phê duyệt hàng loạt"))
            'Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_TEMP", ToolbarIcons.Export,
            '                                                         ToolbarAuthorize.Export, Translate("Xuất file mẫu")))
            'Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP", ToolbarIcons.Import,
            '                                                         ToolbarAuthorize.Import, Translate("Nhập file mẫu")))
            'Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPIRE_TEMP", ToolbarIcons.DeActive,
            '                                                         ToolbarAuthorize.Create, Translate("Cắt phụ cấp")))

            CType(MainToolBar.Items(4), RadToolBarButton).Text = "In"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            rgWorking.MasterTableView.GetColumn("DECISION_TYPE_NAME").HeaderText = UI.DecisionType

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation

            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgWorking.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CreateDataFilter()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary> 
    ''' Event click item tren menu toolbar
    ''' Set trang thai control sau khi process xong 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objOrgFunction As New OrganizationDTO
        Dim sError As String = ""
        Dim tempPath As String = ConfigurationManager.AppSettings("WordFileFolder")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgWorking.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgWorking.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgWorking.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Bản ghi đã phê duyệt. Thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.NOT_APPROVE_ID Then
                            ShowMessage(Translate("Bản ghi không phê duyệt. Thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case "EXPORT_TEMP"
                    isLoadPopup = 1 'Chọn Org
                    UpdateControlState()
                    ctrlOrgPopup.Show()
                    Exit Sub
                Case "IMPORT_TEMP"
                    ctrlUpload1.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable

                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgWorking.ExportExcel(Server, Response, dtData, "ChangeInfo")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_PRINT
                    Dim dtData As DataTable
                    Dim folderName As String = ""
                    Dim filePath As String = ""
                    Dim extension As String = ""
                    Dim sourcePath = Server.MapPath("~/ReportTemplates/Profile/LocationInfo/")
                    Dim iError As Integer = 0
                    Dim item As GridDataItem = rgWorking.SelectedItems(0)

                    ' Kiểm tra + lấy thông tin trong database
                    Using rep As New ProfileRepository
                        dtData = rep.GetHU_DataDynamic(item.GetDataKeyValue("ID"),
                                                       ProfileCommon.HU_TEMPLATE_TYPE.DECISION_ID,
                                                       folderName)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage("Dữ liệu không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                        If folderName = "" Then
                            ShowMessage("Thư mục không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

                    ' Kiểm tra file theo thông tin trong database
                    If Not Utilities.GetTemplateLinkFile(item.GetDataKeyValue("CODE"),
                                                     folderName,
                                                     filePath,
                                                     extension,
                                                     iError) Then
                        Select Case iError
                            Case 1
                                ShowMessage("Biểu mẫu không tồn tại", NotifyType.Warning)
                                Exit Sub
                        End Select
                    End If

                    ' Export file mẫu
                    Using word As New WordCommon
                        word.ExportMailMerge(filePath,
                                             item.GetDataKeyValue("EMPLOYEE_CODE") & "_TDTTNS_" & _
                                             Format(Date.Now, "yyyyMMddHHmmss"),
                                             dtData,
                                             sourcePath,
                                             Response)
                    End Using
                Case CommonMessage.TOOLBARITEM_CREATE_BATCH
                    BatchApproveChangeInfoMng()
            End Select

            UpdateControlState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary>Reload grid sau khi click node treeview </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rgWorking.Rebind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary>Event button tim kiem theo dieu kien tu ngay den ngay</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rgWorking.Rebind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary> 
    ''' Event Yes/No messagebox hoi Xoa du lieu
    ''' Update lai trang thai control sau khi process Xoa
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
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    'Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgWorking.ItemDataBound
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim startTime As DateTime = DateTime.UtcNow

    '    Try
    '        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
    '            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
    '            datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow("ORG_DESC").Text)
    '        End If

    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary>Event OK tren form popup so do to chuc khi click "Xuat File Mau"</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('HU_ChangeInfo&ORGID=" & e.CurrentValue & "&IS_DISSOLVE=" & IIf(ctrlOrgPopup.IsDissolve, "1", "0") & "')", True)
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary> Event CANCEL tren form popup so do to chuc khi click "Xuat File Mau" </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrgPopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary> Event Ok tren form popup chon file mau khi click "Nhap file mau" </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Dim dtData As DataTable
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If ctrlUpload1.UploadedFiles.Count = 0 Then
                ShowMessage(Translate("Bạn chưa chọn biễu mẫu import"), NotifyType.Warning)
                Exit Sub
            End If

            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                ' Kiểm tra file mẫu đúng định dạng hay không
                If workbook.Worksheets.GetSheetByCodeName("DataChangeInfo") Is Nothing Then
                    ShowMessage(Translate("File mẫu không đúng định dạng"), NotifyType.Warning)
                    Exit Sub
                End If
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(3, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next

            dtData = dsDataPrepare.Tables(0).Clone
            For Each dt As DataTable In dsDataPrepare.Tables
                For Each row In dt.Rows
                    dtData.ImportRow(row)
                Next
            Next

            dtData.Columns.Add("STT")

            ProcessData(dtData)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary> Xu ly file mau import </summary>
    ''' <param name="dtData"></param>
    ''' <remarks></remarks>
    Private Sub ProcessData(ByVal dtData As DataTable)
        Dim dtError As DataTable
        Dim iRow As Integer = 4
        Dim rowError As DataRow
        Dim sError As String
        Dim isError As Boolean = False
        Dim lstWorking As New List(Of WorkingDTO)
        Dim objWorking As WorkingDTO
        Dim lstEmp As New List(Of String)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            dtError = dtData.Clone
            dtError.TableName = "DATA"

            For Each row In dtData.Rows
                iRow += 1
                If Not ImportValidate.TrimRow(row) Then
                    Continue For
                End If
                rowError = dtError.NewRow
                rowError("STT") = iRow
                rowError("EMPLOYEE_NAME") = row("EMPLOYEE_NAME").ToString

                sError = "Mã nhân viên chưa nhập"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)
                If row("EMPLOYEE_CODE").ToString <> "" And rowError("EMPLOYEE_CODE").ToString = "" Then
                    Dim empCode = row("EMPLOYEE_CODE").ToString
                    If Not lstEmp.Contains(empCode) Then
                        lstEmp.Add(empCode)
                    Else
                        isError = True
                        rowError("EMPLOYEE_CODE") = "Mã nhân viên đã tồn tại trong file import"
                    End If
                End If

                sError = "Cấp nhân sự"
                ImportValidate.IsValidList("STAFF_RANK_NAME", "STAFF_RANK_ID",
                                           row, rowError, isError, sError)

                sError = "Đơn vị"
                ImportValidate.IsValidList("ORG_NAME", "ORG_ID",
                                           row, rowError, isError, sError)

                sError = "Chức danh"
                ImportValidate.IsValidList("TITLE_NAME", "TITLE_ID",
                                           row, rowError, isError, sError)

                sError = "Loại tờ trình"
                ImportValidate.IsValidList("DECISION_TYPE_NAME", "DECISION_TYPE_ID",
                                           row, rowError, isError, sError)

                sError = "Ngày hiệu lực chưa nhập"
                ImportValidate.IsValidDate("EFFECT_DATE", row, rowError, isError, sError)

                If row("EXPIRE_DATE").ToString <> "" Then
                    ImportValidate.IsValidDate("EXPIRE_DATE", row, rowError, isError, sError)
                End If

                If rowError("EFFECT_DATE").ToString = "" And _
                    rowError("EXPIRE_DATE").ToString = "" And _
                    row("EFFECT_DATE").ToString <> "" And _
                    row("EXPIRE_DATE").ToString <> "" Then
                    Dim startdate = Date.Parse(row("EFFECT_DATE"))
                    Dim enddate = Date.Parse(row("EXPIRE_DATE"))
                    If startdate > enddate Then
                        rowError("EXPIRE_DATE") = "Ngày hết hiệu lực phải lớn hơn Ngày hiệu lực"
                        isError = True
                    End If
                End If

                sError = "Thang lương"
                ImportValidate.IsValidList("SAL_GROUP_NAME", "SAL_GROUP_ID",
                                           row, rowError, isError, sError)

                sError = "Nhóm lương"
                ImportValidate.IsValidList("SAL_LEVEL_NAME", "SAL_LEVEL_ID",
                                           row, rowError, isError, sError)

                sError = "Bậc lương"
                ImportValidate.IsValidList("SAL_RANK_NAME", "SAL_RANK_ID",
                                           row, rowError, isError, sError)

                sError = "Số tiền sai định dạng"
                ImportValidate.IsValidNumber("SAL_BASIC", row, rowError, isError, sError)


                sError = "% hưởng lương sai định dạng"
                ImportValidate.IsValidNumber("PERCENT_SALARY", row, rowError, isError, sError)
                If rowError("PERCENT_SALARY").ToString = "" Then
                    Dim value = Decimal.Parse(row("PERCENT_SALARY").ToString)
                    If value < 0 Or value > 100 Then
                        isError = True
                        rowError("PERCENT_SALARY") = "% hưởng lương không được nhỏ hơn 0 hoặc lớn hơn 100"
                    End If
                End If

                If row("COST_SUPPORT").ToString <> "" Then
                    sError = "Chi phí hỗ trợ sai định dạng"
                    ImportValidate.IsValidNumber("COST_SUPPORT", row, rowError, isError, sError)
                Else
                    ImportValidate.CheckNumber("COST_SUPPORT", row, 0)
                End If

                If row("SIGN_DATE").ToString <> "" Then
                    ImportValidate.IsValidDate("SIGN_DATE", row, rowError, isError, sError)
                End If

                If isError Then
                    dtError.Rows.Add(rowError)
                Else
                    objWorking = New WorkingDTO
                    With objWorking
                        .EMPLOYEE_CODE = row("EMPLOYEE_CODE").ToString
                        .TITLE_ID = row("TITLE_ID").ToString
                        .ORG_ID = row("ORG_ID").ToString
                        .STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID
                        .EFFECT_DATE = Date.Parse(row("EFFECT_DATE").ToString)
                        If row("EXPIRE_DATE").ToString <> "" Then
                            .EXPIRE_DATE = Date.Parse(row("EXPIRE_DATE").ToString)
                        End If
                        .DECISION_TYPE_ID = row("DECISION_TYPE_ID").ToString
                        .DECISION_NO = row("DECISION_NO").ToString
                        .SAL_GROUP_ID = row("SAL_GROUP_ID").ToString
                        .SAL_LEVEL_ID = row("SAL_LEVEL_ID").ToString
                        .SAL_RANK_ID = row("SAL_RANK_ID").ToString
                        .STAFF_RANK_ID = row("STAFF_RANK_ID").ToString
                        .SAL_BASIC = Decimal.Parse(row("SAL_BASIC").ToString)
                        .COST_SUPPORT = Decimal.Parse(row("COST_SUPPORT").ToString)
                        If row("SIGN_DATE").ToString <> "" Then
                            .SIGN_DATE = Date.Parse(row("SIGN_DATE").ToString)
                        End If
                        If row("SIGN_CODE").ToString <> "" Then
                            .SIGN_CODE = row("SIGN_CODE").ToString
                        End If
                        .REMARK = row("REMARK").ToString
                        .IS_PROCESS = True
                        .IS_MISSION = True
                        .IS_3B = False
                        .IS_WAGE = False
                        .PERCENT_SALARY = Decimal.Parse(row("PERCENT_SALARY").ToString)

                        .lstAllowance = New List(Of WorkingAllowanceDTO)
                    End With
                    lstWorking.Add(objWorking)
                End If
            Next

            If dtError.Rows.Count > 0 Then
                Session("EXPORTREPORT") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('HU_ChangeInfo_Error')", True)
                ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)
            Else
                Using rep As New ProfileBusinessRepository
                    If rep.ImportChangeInfo(lstWorking, dtError) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgWorking.Rebind()
                    Else
                        Session("EXPORTREPORT") = dtError
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('HU_ChangeInfo_Error')", True)
                        ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)
                    End If
                End Using
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary> Event Click nut "Ho tro in" </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrintSupport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintSupport.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtData As DataTable
        Dim folderName As String = ""
        Dim filePath As String = ""
        Dim extension As String = ""
        Dim iError As Integer = 0

        Try
            If bExist Then
                Exit Sub
            Else
                bExist = False
            End If

            If cboPrintSupport.SelectedValue = "" Then
                ShowMessage(Translate("Bạn chưa chọn biểu mẫu"), NotifyType.Warning)
                Exit Sub
            End If

            Dim item As GridDataItem = rgWorking.SelectedItems(0)

            ' Kiểm tra + lấy thông tin trong database
            Using rep As New ProfileRepository
                dtData = rep.GetHU_DataDynamic(item.GetDataKeyValue("ID"),
                                               ProfileCommon.HU_TEMPLATE_TYPE.DECISION_SUPPORT_ID,
                                               folderName)
                If dtData.Rows.Count = 0 Then
                    ShowMessage("Dữ liệu không tồn tại", NotifyType.Warning)
                    Exit Sub
                End If
                If folderName = "" Then
                    ShowMessage("Thư mục không tồn tại", NotifyType.Warning)
                    Exit Sub
                End If
            End Using

            ' Kiểm tra file theo thông tin trong database
            If Not Utilities.GetTemplateLinkFile(cboPrintSupport.SelectedValue,
                                             folderName,
                                             filePath,
                                             extension,
                                             iError) Then
                Select Case iError
                    Case 1
                        ShowMessage("Biểu mẫu không tồn tại", NotifyType.Warning)
                        Exit Sub
                End Select
            End If

            ' Export file mẫu
            Using word As New WordCommon
                word.ExportMailMerge(filePath,
                                     item.GetDataKeyValue("EMPLOYEE_CODE") & "_TDTTNS_" & _
                                     Format(Date.Now, "yyyyMMddHHmmss"),
                                     dtData,
                                     Response)
            End Using

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>07/07/2017</lastupdate>
    ''' <summary>
    ''' Validate combobox biểu mẫu hỗ trợ
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalPrintSupport_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalPrintSupport.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New OtherListDTO
        Dim dtData As DataTable = Nothing
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            validate.ID = cboPrintSupport.SelectedValue
            validate.ACTFLG = "A"
            validate.CODE = "DECISION_SUPPORT"
            args.IsValid = rep.ValidateOtherList(validate)
            If Not args.IsValid Then
                dtData = rep.GetOtherList("DECISION_SUPPORT")
                FillRadCombobox(cboPrintSupport, dtData, "NAME", "ID")
                cboPrintSupport.Items.Insert(0, New RadComboBoxItem("", ""))

                CurrentState = CommonMessage.STATE_NORMAL

                cboPrintSupport.ClearSelection()
                cboPrintSupport.SelectedIndex = 0

                bExist = True
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary> Khoi tao form popup, set trang thai control, page </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            phPopup.Controls.Clear()

            Select Case isLoadPopup
                Case 1
                    ctrlOrgPopup = Me.Register("ctrlOrgPopup", "Common", "ctrlFindOrgPopup")
                    ctrlOrgPopup.ShowCheckBoxes = False
                    phPopup.Controls.Add(ctrlOrgPopup)
            End Select

            tbarWorkings.Enabled = True
            rgWorking.Enabled = True
            ctrlOrg.Enabled = True

            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                Case CommonMessage.STATE_EDIT
                Case CommonMessage.STATE_DELETE
                    If rep.DeleteWorking(New WorkingDTO With {.ID = rgWorking.SelectedValue}) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgWorking.CurrentPageIndex = 0
                        rgWorking.MasterTableView.SortExpressions.Clear()
                        rgWorking.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary>Load data len combobox </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim dtData As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Using rep As New ProfileRepository
                dtData = rep.GetOtherList("DECISION_SUPPORT")
                FillRadCombobox(cboPrintSupport, dtData, "NAME", "ID")
            End Using

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>06/07/2017</lastupdate>
    ''' <summary>  Load data len grid </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileBusinessRepository
        Dim _filter As New WorkingDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                rgWorking.DataSource = New List(Of WorkingDTO)
                Exit Function
            End If

            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}

            SetValueObjectByRadGrid(rgWorking, _filter)

            _filter.FROM_DATE = rdEffectDate.SelectedDate
            _filter.TO_DATE = rdExpireDate.SelectedDate
            _filter.IS_TER = chkTerminate.Checked
            _filter.IS_MISSION = True
            Dim MaximumRows As Integer
            Dim Sorts As String = rgWorking.MasterTableView.SortExpressions.GetSortString()

            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetWorking(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetWorking(_filter, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    rgWorking.DataSource = rep.GetWorking(_filter, rgWorking.CurrentPageIndex, rgWorking.PageSize, MaximumRows, _param, Sorts)
                Else
                    rgWorking.DataSource = rep.GetWorking(_filter, rgWorking.CurrentPageIndex, rgWorking.PageSize, MaximumRows, _param)
                End If

                rgWorking.VirtualItemCount = MaximumRows
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý phê duyệt quyet dinh</summary>
    ''' <remarks></remarks>
    Private Sub BatchApproveChangeInfoMng()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim lstID As New List(Of Decimal)

        Try
            '1. Check có rows nào được select hay không
            If rgWorking Is Nothing OrElse rgWorking.SelectedItems.Count <= 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            End If

            Dim listCon As New List(Of ContractDTO)

            For Each dr As Telerik.Web.UI.GridDataItem In rgWorking.SelectedItems
                Dim ID As New Decimal
                If Not dr.GetDataKeyValue("STATUS_ID").Equals("447") Then
                    ID = dr.GetDataKeyValue("ID")
                    lstID.Add(ID)
                End If
            Next

            If lstID.Count > 0 Then
                Dim bCheckHasfile = rep.CheckHasFile(lstID)
                For Each item As GridDataItem In rgWorking.SelectedItems
                    If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                        ShowMessage(Translate("Bản ghi đã phê duyệt."), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                If bCheckHasfile = 1 Then
                    ShowMessage(Translate("Duyệt khi tất cả các record đã có tập tin đính kèm,bạn kiểm tra lại"), NotifyType.Warning)
                    Exit Sub
                End If
                If rep.ApproveListChangeInfoMng(lstID) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgWorking.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            Else
                ShowMessage("Các quyết định được chọn đã được phê duyệt", NotifyType.Information)
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

End Class
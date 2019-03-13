Imports Framework.UI
Imports Framework.UI.Utilities
Imports Insurance.InsuranceBusiness
Imports Common
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlInsManagerSunCare
    Inherits Common.CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup

    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim dtDataImp As DataTable

    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Insurance/Module/Insurance/Business/" + Me.GetType().Name.ToString()

#Region "Property"

    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
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

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("EMPLOYEE_ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("VN_FULLNAME", GetType(String))
                dt.Columns.Add("THOIDIEMHUONG", GetType(String))
                dt.Columns.Add("START_DATE", GetType(String))
                dt.Columns.Add("END_DATE", GetType(String))
                dt.Columns.Add("LEVEL_NAME", GetType(String))
                dt.Columns.Add("LEVEL_ID", GetType(String))
                dt.Columns.Add("COST", GetType(String))
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

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ctrlOrg.AutoPostBack = True
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrg.CheckBoxes = TreeNodeTypes.None

            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SetGridFilter(rgData)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgData.AllowCustomPaging = True
            rgData.ClientSettings.EnablePostBackOnRowClick = False
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load cac control, menubar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Export)
            'Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_TEMP",
            '                                                       ToolbarIcons.Export,
            '                                                       ToolbarAuthorize.Export,
            '                                                       Translate("Xuất file mẫu")))
            'Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP",
            '                                                         ToolbarIcons.Import,
            '                                                         ToolbarAuthorize.Import,
            '                                                         Translate("Nhập file mẫu")))
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
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
                        rgData.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                               CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGridData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            LoadDataGrid()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        'Dim gID As Decimal
        Dim objData As New INS_SUN_CARE_DTO
        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = LoadDataGrid(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtDatas, "Infomation_Ins")
                        End If
                    End Using
                Case "EXPORT_TEMP"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Import_INS_SUNCARE&orgid=" & ctrlOrg.CurrentValue & "');", True)
                Case "IMPORT_TEMP"
                    ctrlUpload.Show()
            End Select
            ChangeToolbarState()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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

        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Dim objShift As New INS_INFORMATIONDTO
        Dim rep As New InsuranceRepository
        dtDataImp = New DataTable
        'Dim gID As Decimal
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
                If workbook.Worksheets.GetSheetByCodeName("ImportInsSunCare") Is Nothing Then
                    ShowMessage(Translate("File mẫu không đúng định dạng"), NotifyType.Warning)
                    Exit Sub
                End If
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(5, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            dtDataImp = dsDataPrepare.Tables(0).Clone()
            For Each dt As DataTable In dsDataPrepare.Tables
                For Each row As DataRow In dt.Rows
                    Dim isRow = ImportValidate.TrimRow(row)
                    If Not isRow Then
                        Continue For
                    End If
                    dtData.ImportRow(row)
                    dtDataImp.ImportRow(row)
                Next
            Next
            If loadToGrid() Then
                If SaveData() Then
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("InsertView")
                    UpdateControlState()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <remarks></remarks>
    Function loadToGrid() As Boolean
        Dim dtError As New DataTable("ERROR")

        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dtData.Rows.Count = 0 Then
                ShowMessage("File mẫu không đúng định dạng", NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            Dim rep As New InsuranceRepository
            Dim lstEmp As New List(Of String)
            dtError = dtData.Clone
            Dim irow = 7
            For Each row As DataRow In dtData.Rows
                isError = False
                rowError = dtError.NewRow
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)

                If row("START_DATE") <> "" Then
                    ImportValidate.IsValidDate("START_DATE", row, rowError, isError, sError)
                End If
                If row("END_DATE") <> "" Then
                    ImportValidate.IsValidDate("END_DATE", row, rowError, isError, sError)
                End If

                sError = "Nhóm hưởng bảo hiểm"
                ImportValidate.IsValidList("LEVEL_NAME", "LEVEL_ID", row, rowError, isError, sError)

                If row("COST") <> "" Then
                    sError = "Mức chi phí"
                    ImportValidate.IsValidNumber("COST", row, rowError, isError, sError)
                End If

                If isError Then
                    rowError("STT") = irow
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    rowError("ORG_NAME") = row("ORG_NAME").ToString
                    rowError("TITLE_NAME") = row("TITLE_NAME").ToString
                    rowError("STAFF_RANK_NAME") = row("STAFF_RANK_NAME").ToString
                    dtError.Rows.Add(rowError)
                End If
                irow = irow + 1
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("EXPORTREPORT") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_ImportInfoIns_error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Return False
            Else
                Return True
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Insert data vao database
    ''' </summary>
    ''' <remarks></remarks>
    Function SaveData() As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New InsuranceRepository
            Dim obj As INS_SUN_CARE_DTO
            Dim gId As New Decimal?
            gId = 0
            For Each row As DataRow In dtDataImp.Rows
                obj = New INS_SUN_CARE_DTO
                obj.EMPLOYEE_ID = CDec(row("EMPLOYEE_ID"))
                obj.THOIDIEMHUONG = ToDate(row("THOIDIEMHUONG"))
                obj.START_DATE = ToDate(row("START_DATE"))
                obj.END_DATE = ToDate(row("END_DATE"))
                obj.LEVEL_ID = CDec(row("LEVEL_ID"))
                obj.COST = CDec(row("COST"))
                obj.NOTE = row("NOTE")
                rep.InsertSunCare(obj, gId)
            Next
            Return True
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
        Return False
    End Function

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Event Yes, No tren popup message khi click nut: xoa, ap dung, ngung ap dung
    ''' va Set trang thai cho form va update trang thai control 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien SelectedNodeChanged cua control ctrlOrg
    ''' Xet lai cac thiet lap trang thai cho grid rgRegisterLeave
    ''' Bind lai du lieu cho rgRegisterLeave
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgData.CurrentPageIndex = 0
            rgData.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho button btnSearch
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgData.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien ItemDataBound cua grid rgAgrising
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Function & Sub"

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Private Function LoadDataGrid(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New InsuranceRepository
        Dim obj As New INS_SUN_CARE_DTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer = 0
            SetValueObjectByRadGrid(rgData, obj)
            Dim lstSource As List(Of INS_SUN_CARE_DTO)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()

            If rdFrom.SelectedDate.HasValue Then
                obj.FROM_DATE_SEARCH = rdFrom.SelectedDate
            End If
            If rdTo.SelectedDate.HasValue Then
                obj.TO_DATE_SEARCH = rdTo.SelectedDate
            End If
            obj.IS_TERMINATE = chkChecknghiViec.Checked

            If Not isFull Then
                If Sorts IsNot Nothing Then
                    lstSource = rep.GetSunCare(obj, Utilities.ObjToInt(ctrlOrg.CurrentValue), "", rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                Else
                    lstSource = rep.GetSunCare(obj, Utilities.ObjToInt(ctrlOrg.CurrentValue), "", rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                End If
                rgData.DataSource = lstSource
                rgData.MasterTableView.VirtualItemCount = MaximumRows
            Else
                If Sorts IsNot Nothing Then
                    Return rep.GetSunCare(obj, Utilities.ObjToInt(ctrlOrg.CurrentValue), "", , , , Sorts).ToTable
                Else
                    Return rep.GetSunCare(obj, Utilities.ObjToInt(ctrlOrg.CurrentValue), "").ToTable
                End If

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Update trang thai cua cac control theo state
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New InsuranceRepository
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteSunCare(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

End Class
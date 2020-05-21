Imports System.Globalization
Imports System.IO
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_Commend
    Inherits Common.CommonView

    ''' <summary>
    ''' psp
    ''' </summary>
    ''' <remarks></remarks>
    Dim psp As New ProfileStoreProcedure

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Commends As List(Of CommendDTO)
        Get
            Return ViewState(Me.ID & "_Commends")
        End Get

        Set(ByVal value As List(Of CommendDTO))
            ViewState(Me.ID & "_Commends") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DeleteCommend As CommendDTO
        Get
            Return ViewState(Me.ID & "_DeleteCommend")
        End Get

        Set(ByVal value As CommendDTO)
            ViewState(Me.ID & "_DeleteCommend") = value
        End Set
    End Property

    ''' <summary>
    ''' ListComboData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get

        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
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

    ''' <summary>
    ''' IDSelect
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get

        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    ''' <summary>
    ''' IsLoad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get

        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property

    ''' <summary>
    ''' ApproveCommend
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ApproveCommend As CommendDTO
        Get
            Return ViewState(Me.ID & "_ApproveCommend")
        End Get

        Set(ByVal value As CommendDTO)
            ViewState(Me.ID & "_ApproveCommend") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If Not IsPostBack Then
                Refresh()
            End If

            UpdateControlState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            rgCommend.AllowCustomPaging = True
            rgCommend.PageSize = Common.Common.DefaultPageSize
            rgCommend.SetFilter()
            btnSearch.CausesValidation = False
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            InitControl()
            If Not IsPostBack Then
                ViewConfig(RadPane1)
                'GirdConfig(rgCommend)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarCommends

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Export,
                                       ToolbarItem.Delete, ToolbarItem.Print, ToolbarItem.ExportTemplate, ToolbarItem.Import)
            MainToolBar.Items(5).Text = Translate("Xuất file mẫu")
            CType(Me.MainToolBar.Items(6), RadToolBarButton).ImageUrl = CType(Me.MainToolBar.Items(0), RadToolBarButton).ImageUrl
            MainToolBar.Items(6).Text = Translate("Nhập file mẫu")
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_CREATE_BATCH,
                                                                  ToolbarIcons.Add,
                                                                  ToolbarAuthorize.Special1,
                                                                  "Phê duyệt hàng loạt"))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_APPROVE_OPEN,
                                                                  ToolbarIcons.Unlock,
                                                                  ToolbarAuthorize.Special1,
                                                                  "Mở phê duyệt"))

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Fill du lieu len combobox</summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            GetDataCombo()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Cập nhật trạng thái control</summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            UpdateControlEnabled(False)
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    If rep.DeleteCommend(DeleteCommend) Then
                        DeleteCommend = Nothing
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If

                Case CommonMessage.STATE_APPROVE
                    If rep.ApproveCommend(ApproveCommend) Then
                        ApproveCommend = Nothing
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                Case CommonMessage.STATE_DEACTIVE
                    If rep.UnApproveCommend(ApproveCommend) Then
                        ApproveCommend = Nothing
                        Refresh("UpdateView")
                        UpdateControlState()
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

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Cập nhật lại dữ liệu của grid</summary>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If Not IsPostBack Then
                rgCommend.Rebind()
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgCommend.Rebind()

                        SelectedItemDataGridByKey(rgCommend, IDSelect, , rgCommend.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgCommend.CurrentPageIndex = 0
                        rgCommend.MasterTableView.SortExpressions.Clear()
                        rgCommend.Rebind()
                        SelectedItemDataGridByKey(rgCommend, IDSelect, )
                    Case "Cancel"
                        rgCommend.MasterTableView.ClearSelectedItems()
                End Select
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary> 
    ''' Event click item tren menu toolbar
    ''' Set trang thai control sau khi process xong 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_EDIT
                    'Dim rep As New ProfileBusinessRepository
                    Dim gId = Utilities.ObjToDecima(rgCommend.SelectedValue)
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_DELETE
                    Dim objCommend As CommendDTO
                    objCommend = (From p In Commends Where p.ID = rgCommend.SelectedValue).FirstOrDefault

                    Using rep As New ProfileBusinessRepository
                        If objCommend.STATUS_ID = ProfileCommon.COMMEND_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Khen thưởng đã phê duyệt, không được phép khen thưởng"), NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

                    If objCommend.STATUS_ID = ProfileCommon.HU_COMMEND_STATUS.APPROVE Then
                        ShowMessage(Translate("Bạn không thể xóa bản ghi này"), NotifyType.Warning)
                        Exit Sub
                    End If

                    If objCommend IsNot Nothing Then
                        DeleteCommend = New CommendDTO With {.ID = objCommend.ID,
                                                                   .DECISION_ID = objCommend.DECISION_ID}
                    End If

                    If DeleteCommend IsNot Nothing Then
                        ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    End If

                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_PRINT
                    'Dim repval As New ProfileBusinessRepository
                    'Dim listID As New List(Of Decimal)
                    'listID.Add(rgCommend.SelectedValue)
                    'If repval.ValidateBusiness("HU_COMMEND", "ID", listID) Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                    '    Exit Sub
                    'End If

                    'If rgCommend.SelectedItems.Count = 0 Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    '    Exit Sub
                    'End If

                    'If rgCommend.SelectedItems.Count > 1 Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                    '    Exit Sub
                    'End If

                    'Dim dtData As DataTable
                    'Dim folderName As String = ""
                    'Dim filePath As String = ""
                    'Dim extension As String = ""
                    'Dim iError As Integer = 0
                    'Dim item As GridDataItem = rgCommend.SelectedItems(0)

                    '' Kiểm tra + lấy thông tin trong database
                    'Using rep As New ProfileRepository
                    '    dtData = rep.GetHU_DataDynamic(item.GetDataKeyValue("ID"),
                    '                                   ProfileCommon.HU_TEMPLATE_TYPE.TERMINATE_ID,
                    '                                   folderName)
                    '    If dtData.Rows.Count = 0 Then
                    '        ShowMessage("Dữ liệu không tồn tại", NotifyType.Warning)
                    '        Exit Sub
                    '    End If
                    '    If folderName = "" Then
                    '        ShowMessage("Thư mục không tồn tại", NotifyType.Warning)
                    '        Exit Sub
                    '    End If
                    'End Using

                    '' Kiểm tra file theo thông tin trong database
                    'If Not Utilities.GetTemplateLinkFile(2,
                    '                                     folderName,
                    '                                     filePath,
                    '                                     extension,
                    '                                     iError) Then
                    '    Select Case iError
                    '        Case 1
                    '            ShowMessage("Biểu mẫu không tồn tại", NotifyType.Warning)
                    '            Exit Sub
                    '    End Select
                    'End If

                    '' Export file mẫu
                    'Using word As New WordCommon
                    '    word.ExportMailMerge(filePath,
                    '                         item.GetDataKeyValue("ID") & "_" & _
                    '                         Format(Date.Now, "yyyyMMddHHmmss"),
                    '                         dtData,
                    '                         Response)
                    'End Using

                Case CommonMessage.TOOLBARITEM_APPROVE
                    Dim objCommend As CommendDTO
                    'Dim rep As New ProfileBusinessRepository
                    objCommend = (From p In Commends Where p.ID = rgCommend.SelectedValue).FirstOrDefault

                    If objCommend.STATUS_ID = ProfileCommon.COMMEND_STATUS.APPROVE_ID Then
                        ShowMessage(Translate("Khen thưởng đã phê duyệt. Vui lòng kiểm tra lại."), NotifyType.Warning)
                        Exit Sub
                    End If

                    If objCommend IsNot Nothing Then
                        ApproveCommend = New CommendDTO With {.ID = objCommend.ID,
                                                                    .EMPLOYEE_ID = objCommend.EMPLOYEE_ID,
                                                              .EFFECT_DATE = objCommend.EFFECT_DATE}
                    End If
                    If ApproveCommend IsNot Nothing Then
                        ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_APPROVE)
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    End If
                Case CommonMessage.TOOLBARITEM_APPROVE_OPEN
                    Dim objCommend As CommendDTO
                    'Dim rep As New ProfileBusinessRepository
                    objCommend = (From p In Commends Where p.ID = rgCommend.SelectedValue).FirstOrDefault

                    If objCommend.STATUS_ID <> ProfileCommon.COMMEND_STATUS.APPROVE_ID Then
                        ShowMessage(Translate("Khen thưởng chưa phê duyệt, không thể mở phê duyệt. Vui lòng kiểm tra lại."), NotifyType.Warning)
                        Exit Sub
                    End If

                    If objCommend IsNot Nothing Then
                        ApproveCommend = New CommendDTO With {.ID = objCommend.ID,
                                                                    .EMPLOYEE_ID = objCommend.EMPLOYEE_ID,
                                                              .EFFECT_DATE = objCommend.EFFECT_DATE}
                    End If
                    If ApproveCommend IsNot Nothing Then
                        ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_UNAPPROVE)
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE_OPEN
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgCommend.ExportExcel(Server, Response, dtData, "Commend")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using

                Case CommonMessage.TOOLBARITEM_APPROVE
                    Dim objCommend As CommendDTO
                    'Dim rep As New ProfileBusinessRepository
                    objCommend = (From p In Commends Where p.ID = rgCommend.SelectedValue).FirstOrDefault

                    If objCommend.STATUS_ID = ProfileCommon.COMMEND_STATUS.APPROVE_ID Then
                        ShowMessage(Translate("Khen thưởng đã phê duyệt. Vui lòng kiểm tra lại."), NotifyType.Warning)
                        Exit Sub
                    End If

                    If objCommend IsNot Nothing Then
                        ApproveCommend = New CommendDTO With {.ID = objCommend.ID,
                                                                    .EMPLOYEE_ID = objCommend.EMPLOYEE_ID,
                                                              .EFFECT_DATE = objCommend.EFFECT_DATE}
                    End If
                    If ApproveCommend IsNot Nothing Then
                        ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_APPROVE)
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    End If

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgCommend.ExportExcel(Server, Response, dtData, "Commend")
                        End If
                    End Using

                Case CommonMessage.TOOLBARITEM_CREATE_BATCH
                    BatchApproveCommend()

                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Template_Import_Commend()
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
            End Select

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#Region "Import and Export Commend"

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Try
            Import_Data()
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi"), NotifyType.Error)
        End Try

    End Sub

    Private Sub Import_Data()
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload1.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim rep As New ProfileBusinessRepository
            '_mylog = LogHelper.GetUserLog
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
                    If rep.INPORT_QLKT(DocXml) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgCommend.Rebind()
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
        Dim rep As New ProfileBusinessClient
        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
        dtTemp.Columns(0).ColumnName = "EMPLOYEE_CODE"
        dtTemp.Columns(4).ColumnName = "EFFECT_DATE"
        dtTemp.Columns(5).ColumnName = "NO"
        dtTemp.Columns(6).ColumnName = "SIGN_DATE"
        dtTemp.Columns(7).ColumnName = "SIGN_NAME"
        dtTemp.Columns(12).ColumnName = "MONEY"
        dtTemp.Columns(15).ColumnName = "REMARK"
        dtTemp.Columns(19).ColumnName = "YEAR_COMMEND"
        dtTemp.Columns(21).ColumnName = "NOTE"
        dtTemp.Columns(22).ColumnName = "SIGN_ID"
        dtTemp.Columns(23).ColumnName = "POWER_PAY_ID"
        dtTemp.Columns(24).ColumnName = "COMMEND_PAY"
        dtTemp.Columns(25).ColumnName = "COMMEND_TYPE"
        dtTemp.Columns(26).ColumnName = "COMMEND_LIST"
        dtTemp.Columns(27).ColumnName = "TITLE_ID"
        dtTemp.Columns(28).ColumnName = "COMMEND_LEVEL"
        dtTemp.Columns(30).ColumnName = "PERIOD_ID" 'KY LUONG CHI TRA
        dtTemp.Columns(32).ColumnName = "PERIOD_TAX"
        dtTemp.Columns(33).ColumnName = "IS_TAX"

        'XOA DONG TIEU DE VA HEADER
        dtTemp.Rows(0).Delete()
        dtTemp.Rows(1).Delete()
        ' add Log
        Dim _error As Boolean = True
        Dim count As Integer
        Dim newRow As DataRow
        Dim startDate As Date
        Dim dsEMP As DataTable
        If dtLogs Is Nothing Then
            dtLogs = New DataTable("data")
            dtLogs.Columns.Add("ID", GetType(Integer))
            dtLogs.Columns.Add("EMPLOYEE_CODE", GetType(String))
            dtLogs.Columns.Add("DISCIPTION", GetType(String))
        End If
        dtLogs.Clear()
        Dim strEmpCode As String


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
            Else
                If strEmpCode <> "" Then
                    If strEmpCode.Contains(rows("EMPLOYEE_CODE")) Then
                        newRow("DISCIPTION") = "Mã nhân viên - nhiều hơn 2 dòng trong file,"
                        _error = False
                    End If
                    strEmpCode = strEmpCode + rows("EMPLOYEE_CODE") + ","
                End If
            End If

            'If Not (IsNumeric(rows("COMMEND_PAY"))) Then
            '    rows("COMMEND_PAY") = 0
            '    newRow("DISCIPTION") = newRow("DISCIPTION") + "Hình thức trả thưởng - Không đúng định dạng,"
            '    _error = False
            'End If
            If IsDBNull(rows("EFFECT_DATE")) OrElse rows("EFFECT_DATE") = "" OrElse CheckDate(rows("EFFECT_DATE"), startDate) = False Then
                rows("EFFECT_DATE") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày hiệu lực - Không đúng định dạng,"
                _error = False
            End If

            If IsDBNull(rows("NO")) OrElse rows("NO") = "" Then
                rows("NO") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Số quyết định - bắt buộc nhập,"
                _error = False
            End If

            If IsDBNull(rows("SIGN_DATE")) OrElse rows("SIGN_DATE") = "" OrElse CheckDate(rows("SIGN_DATE"), startDate) = False Then
                rows("SIGN_DATE") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày ký - Không đúng định dạng"
                _error = False
            End If

            If IsDBNull(rows("SIGN_NAME")) OrElse rows("SIGN_NAME") = "" Then
                rows("SIGN_NAME") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Người ký - bắt buộc nhập"
                _error = False
            End If

            If Not (IsNumeric(rows("TITLE_ID"))) Then
                rows("TITLE_ID") = 0
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Danh hiệu khen thưởng - bắt buộc nhập"
                _error = False
            End If

            If Not (IsNumeric(rows("COMMEND_TYPE"))) Then
                rows("COMMEND_TYPE") = 0
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Hình thức khen thưởng - bắt buộc nhập"
                _error = False
            End If

            If Not (IsNumeric(rows("COMMEND_LEVEL"))) Then
                rows("COMMEND_LEVEL") = 0
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Cấp khen thưởng - bắt buộc nhập"
                _error = False
            End If

            'If IsDBNull(rows("IS_TAX")) Then

            'Else
            '    If Not rows("IS_TAX") = "Có" AndAlso rows("NO") = "Không" Then
            '        rows("IS_TAX") = "NULL"
            '        newRow("DISCIPTION") = newRow("DISCIPTION") + "Tính thuế - Không đúng định dạng,"
            '        _error = False
            '    End If
            'End If

            If Not (IsNumeric(rows("MONEY"))) Then
                rows("MONEY") = 0
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Mức thưởng - Không đúng định dạng,"
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

    Private Sub Template_Import_Commend()
        Dim rep As New Profile.ProfileBusinessRepository
        Try

            Dim tempPath = "~/ReportTemplates//Profile//Import//import_khenthuong1.xls"
            Dim dsData As DataSet = rep.EXPORT_QLKT()
            If File.Exists(System.IO.Path.Combine(Server.MapPath(tempPath))) Then
                Using xls As New AsposeExcelCommon
                    Dim bCheck = xls.ExportExcelTemplate(
                      System.IO.Path.Combine(Server.MapPath(tempPath)), "IMPORT_KhenThuong" & Format(Date.Now, "yyyyMMdd"), dsData, Nothing, Response)

                End Using
            Else
                ShowMessage(Translate("Template không tồn tại"), Utilities.NotifyType.Error)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function CheckDate(ByVal value As String, ByRef result As Date) As Boolean
        Dim dateCheck As Boolean
        If value = "" Or value = "&nbsp;" Then
            value = ""
            Return True
        End If

        Try
            dateCheck = DateTime.TryParseExact(value, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, result)
            Return dateCheck
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function CheckYear(ByVal value As String, ByRef result As Date) As Boolean
        Dim dateCheck As Boolean
        If value = "" Or value = "&nbsp;" Then
            value = ""
            Return True
        End If

        Try
            dateCheck = DateTime.TryParseExact(value, "yyyy", New CultureInfo("en-US"), DateTimeStyles.None, result)
            Return dateCheck
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function CheckMonth(ByVal value As String, ByRef result As Date) As Boolean
        Dim dateCheck As Boolean
        If value = "" Or value = "&nbsp;" Then
            value = ""
            Return True
        End If

        Try
            dateCheck = DateTime.TryParseExact(value, "MM", New CultureInfo("en-US"), DateTimeStyles.None, result)
            Return dateCheck
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary> 
    ''' Event Yes/No messagebox hoi Xoa du lieu
    ''' Update lai trang thai control sau khi process Xoa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_APPROVE
                UpdateControlState()
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE_OPEN And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
                UpdateControlState()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Event button tim kiem theo dieu kien tu ngay den ngay</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            rgCommend.CurrentPageIndex = 0
            rgCommend.MasterTableView.SortExpressions.Clear()
            rgCommend.Rebind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            CreateDataFilter()
            Refresh()
            rgCommend.CurrentPageIndex = 0
            rgCommend.MasterTableView.SortExpressions.Clear()
            rgCommend.Rebind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgCommend.NeedDataSource
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            CreateDataFilter()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    'Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgCommend.ItemDataBound
    '    Dim startTime As DateTime = DateTime.UtcNow 
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() 
    '    Try
    '        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
    '            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
    '            datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow("ORG_DESC").Text)
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "") 
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "") 
    '    End Try
    'End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Cập nhật trạng thái các control</summary>
    ''' <param name="bCheck"></param>
    ''' <remarks></remarks>
    Private Sub UpdateControlEnabled(ByVal bCheck As Boolean)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            ctrlOrg.Enabled = Not bCheck
            Utilities.EnabledGrid(rgCommend, Not bCheck, False)
            rdFromDate.Enabled = Not bCheck
            rdToDate.Enabled = Not bCheck
            btnSearch.Enabled = Not bCheck

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý phê duyệt khen thưởng</summary>
    ''' <remarks></remarks>
    Private Sub BatchApproveCommend()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim lstID As New List(Of Decimal)

        Try
            '1. Check có rows nào được select hay không
            If rgCommend Is Nothing OrElse rgCommend.SelectedItems.Count <= 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            End If

            Dim listCon As New List(Of ContractDTO)

            For Each dr As Telerik.Web.UI.GridDataItem In rgCommend.SelectedItems
                Dim ID As New Decimal
                If Not dr("STATUS_ID").Text.Equals("447") Then
                    ID = dr.GetDataKeyValue("ID")
                    lstID.Add(ID)
                End If
            Next

            If lstID.Count > 0 Then
                Dim bCheckHasfile = rep.CheckHasFileComend(lstID)
                For Each item As GridDataItem In rgCommend.SelectedItems
                    If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                        ShowMessage(Translate("Bản ghi đã phê duyệt."), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                'If bCheckHasfile = 1 Then
                '    ShowMessage(Translate("Duyệt khi tất cả các record đã có tập tin đính kèm,bạn kiểm tra lại"), NotifyType.Warning)
                '    Exit Sub
                'End If
                If rep.ApproveListCommend(lstID) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgCommend.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            Else
                ShowMessage("Các khen thưởng được chọn đã được phê duyệt", NotifyType.Information)
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Load dữ liệu cho combobox</summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_COMMEND_OBJ = True
                ListComboData.GET_COMMEND_STATUS = True
                rep.GetComboList(ListComboData)
            End If

            FillDropDownList(cboStatus, ListComboData.LIST_COMMEND_STATUS, "NAME_VN", "ID", Common.Common.SystemLanguage, False)
            FillDropDownList(cboCommendObj, ListComboData.LIST_COMMEND_OBJ, "NAME_VN", "ID", Common.Common.SystemLanguage, True)

            cboCommendObj.SelectedIndex = 1 'default: khen thuong ca nhan 
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>10/07/2017</lastupdate>
    ''' <summary>Load data len grid</summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        CreateDataFilter = Nothing
        Dim _filter As New CommendDTO
        _filter.param = New ParamDTO
        Dim rep As New ProfileBusinessRepository
        Dim bCheck As Boolean = False
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.param.ORG_ID = Utilities.ObjToDecima(ctrlOrg.CurrentValue)
            Else
                rgCommend.DataSource = New List(Of CommendDTO)
                '_filter.param.ORG_ID = psp.GET_ID_ORG()
                Exit Function
            End If

            _filter.param.IS_DISSOLVE = ctrlOrg.IsDissolve

            If txtEmployee.Text <> "" Then
                _filter.EMPLOYEE_CODE = txtEmployee.Text
            End If

            If rdFromDate.SelectedDate IsNot Nothing Then
                _filter.EFFECT_DATE = rdFromDate.SelectedDate
            End If

            If rdToDate.SelectedDate IsNot Nothing Then
                _filter.EXPIRE_DATE = rdToDate.SelectedDate
            End If

            If cboCommendObj.SelectedValue <> "" Then
                _filter.COMMEND_OBJ = cboCommendObj.SelectedValue
            End If

            If cboStatus.SelectedValue <> "" Then
                _filter.STATUS_ID = cboStatus.SelectedValue
            End If

            _filter.IS_TERMINATE = chkChecknghiViec.Checked

            SetValueObjectByRadGrid(rgCommend, _filter)

            Dim MaximumRows As Integer
            Dim Sorts As String = rgCommend.MasterTableView.SortExpressions.GetSortString()

            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetCommend(_filter, Sorts).ToTable()
                Else
                    Return rep.GetCommend(_filter).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.Commends = rep.GetCommend(_filter, rgCommend.CurrentPageIndex, rgCommend.PageSize, MaximumRows, Sorts)
                Else
                    Me.Commends = rep.GetCommend(_filter, rgCommend.CurrentPageIndex, rgCommend.PageSize, MaximumRows)
                End If
                rep.Dispose()
                rgCommend.VirtualItemCount = MaximumRows
                rgCommend.DataSource = Me.Commends
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

#End Region

End Class
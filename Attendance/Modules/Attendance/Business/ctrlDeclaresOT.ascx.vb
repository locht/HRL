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
Imports System.Globalization
Imports Profile.ProfileBusiness

Public Class ctrlDeclaresOT
    Inherits Common.CommonView
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()
#Region "Properties"
    Private Property REGISTER_OT As List(Of AT_OT_REGISTRATIONDTO)
        Get
            Return ViewState(Me.ID & "_REGISTER_OT")
        End Get
        Set(ByVal value As List(Of AT_OT_REGISTRATIONDTO))
            ViewState(Me.ID & "_REGISTER_OT") = value
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
    Property dtDataImportEmployee As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataImportEmployee")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataImportEmployee") = value
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
    ''' Load, khởi tạo page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ctrlOrganization.AutoPostBack = True
            ctrlOrganization.LoadDataAfterLoaded = True
            ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrganization.CheckBoxes = TreeNodeTypes.None

            Refresh()
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Khởi tạo page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            SetGridFilter(rgDeclaresOT)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgDeclaresOT.AllowCustomPaging = True

            rgDeclaresOT.ClientSettings.EnablePostBackOnRowClick = False
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
    ''' Khởi tạo control, menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Export,
                                       ToolbarItem.Delete)
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_TEMP",
                                                                     ToolbarIcons.Export,
                                                                     ToolbarAuthorize.Export,
                                                                     Translate("Xuất file mẫu")))

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP",
                                                                     ToolbarIcons.Import,
                                                                     ToolbarAuthorize.Import,
                                                                     Translate("Nhập file mẫu")))
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
                    cboPeriod.SelectedIndex = lsData.Count - 1
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
    ''' Update trạng thái của control theo trạng thái page
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim rep As New AttendanceRepository
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDeclaresOT.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDeclaresOT.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteRegisterOT(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
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
    ''' Event cboYear SelectedIndexChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtData As List(Of AT_PERIODDTO)
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO
        Try
            period.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            dtData = rep.LOAD_PERIODBylinq(period)
            cboPeriod.ClearSelection()
            Me.PERIOD = dtData
            FillRadCombobox(cboPeriod, dtData, "PERIOD_NAME", "PERIOD_ID", True)
            If dtData.Count > 0 Then
                Dim periodid = (From d In dtData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                    rdtungay.SelectedDate = periodid.START_DATE
                    rdDenngay.SelectedDate = periodid.END_DATE
                    rgDeclaresOT.Rebind()
                Else
                    cboPeriod.SelectedIndex = dtData.Count - 1
                    Dim per = (From c In dtData Where c.PERIOD_ID = cboPeriod.SelectedValue).FirstOrDefault
                    rdtungay.SelectedDate = per.START_DATE
                    rdDenngay.SelectedDate = per.END_DATE
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
    ''' Event cboPeriod SelectedIndexChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriod.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO
        Try
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            period.ORG_ID = 46
            If cboPeriod.SelectedValue <> "" Then
                Dim Lstperiod = rep.LOAD_PERIODBylinq(period)

                Dim p = (From o In Lstperiod Where o.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)).FirstOrDefault
                rdtungay.SelectedDate = p.START_DATE
                rdDenngay.SelectedDate = p.END_DATE

            End If
            rgDeclaresOT.Rebind()
            ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Reload page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New AttendanceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDeclaresOT.Rebind()
                        'SelectedItemDataGridByKey(rgSignWork, IDSelect, , rgSignWork.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDeclaresOT.CurrentPageIndex = 0
                        rgDeclaresOT.MasterTableView.SortExpressions.Clear()
                        rgDeclaresOT.Rebind()
                        'SelectedItemDataGridByKey(rgSignWork, IDSelect, )
                    Case "Cancel"
                        rgDeclaresOT.MasterTableView.ClearSelectedItems()
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
    ''' <summary>
    ''' Event ctrlOrganization SelectedNodeChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If Not IsPostBack Then
                ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
            End If
            rgDeclaresOT.CurrentPageIndex = 0
            rgDeclaresOT.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Event click item menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim rep As New AttendanceRepository
            Dim _param = New Attendance.AttendanceBusiness.ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue), _
                                            .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
                                            .IS_DISSOLVE = ctrlOrganization.IsDissolve}

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_DELETE
                    ' kiem tra ky cong da dong chua?
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    'Kiểm tra các điều kiện để xóa.
                    If rgDeclaresOT.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi xóa"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case "EXPORT_TEMP"
                    ' kiem tra ky cong da dong chua?
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    isLoadPopup = 0 'Chọn Org
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_DeclareOT')", True)
                    'UpdateControlState()
                    ' ctrlOrgPopup.Show()
                    'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_DeclareOT&PERIOD_ID=" & cboPeriod.SelectedValue & "&orgid=" & e.CurrentValue & "&IS_DISSOLVE=" & IIf(ctrlOrgPopup.IsDissolve, "1", "0") & "')", True)

                Case "IMPORT_TEMP"
                    ' kiem tra ky cong da dong chua?
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    ctrlUpload1.AllowedExtensions = "xls,xlsx"
                    ctrlUpload1.Show()
                Case TOOLBARITEM_SAVE
                    ' kiem tra ky cong da dong chua?
                    'If rep.IS_PERIODSTATUS(_param) = False Then
                    '    ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                    '    Exit Sub
                    'End If
                    SaveOT()
                    Refresh("InsertView")
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                Case TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                    rgDeclaresOT.Rebind()
                Case TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgDeclaresOT.ExportExcel(Server, Response, dtDatas, "DataEntitlement")
                        End If
                    End Using
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Function Save to database
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveOT() As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim rep As New AttendanceRepository
            Dim obj As New AT_REGISTER_OTDTO
            Dim gId As New Decimal?
            gId = 0
            For Each row As DataRow In dtData.Rows
                obj.EMPLOYEE_CODE = row("EMPLOYEE_CODE")
                obj.WORKINGDAY = ToDate(row("WORKINGDAY"))
                obj.FROM_HOUR = obj.WORKINGDAY.Value.AddHours(TimeSpan.Parse(row("FROM_HOUR")).TotalHours)
                obj.TO_HOUR = obj.WORKINGDAY.Value.AddHours(TimeSpan.Parse(row("TO_HOUR")).TotalHours)
                If obj.TO_HOUR < obj.FROM_HOUR Then
                    obj.TO_HOUR = obj.WORKINGDAY.Value.AddDays(1).AddHours(TimeSpan.Parse(row("TO_HOUR")).TotalHours)
                Else
                    obj.TO_HOUR = obj.WORKINGDAY.Value.AddHours(TimeSpan.Parse(row("TO_HOUR")).TotalHours)
                End If
                Dim breakHour = 0
                If Not String.IsNullOrWhiteSpace(row(AT_RegisterOT_OrderBy.Break_Out).ToString) Then
                    breakHour = Convert.ToDecimal(row(AT_RegisterOT_OrderBy.Break_Out).ToString)
                    obj.BREAK_HOUR = breakHour
                End If

                obj.HOUR = (TimeSpan.Parse(row("TO_HOUR")).TotalHours - TimeSpan.Parse(row("FROM_HOUR")).TotalHours)
                'trừ số giờ nghỉ trưa
                If obj.HOUR >= breakHour Then
                    obj.HOUR = obj.HOUR - breakHour
                Else
                    obj.HOUR = 0
                End If


                obj.NOTE = row("NOTE")
                If row("IS_NB").ToString.ToUpper() = "X" Then
                    obj.IS_NB = True
                Else
                    obj.IS_NB = False
                End If
                If row("HS_OT_NAME") = "1.0" Then
                    obj.HS_OT = 4236
                End If
                If row("HS_OT_NAME") = "1.5" Then
                    obj.HS_OT = 4237
                End If
                If row("HS_OT_NAME") = "2.0" Then
                    obj.HS_OT = 4238
                End If
                If row("HS_OT_NAME") = "2.7" Then
                    obj.HS_OT = 4239
                End If
                If row("HS_OT_NAME") = "3.0" Then
                    obj.HS_OT = 4240
                End If
                If row("HS_OT_NAME") = "3.9" Then
                    obj.HS_OT = 4241
                End If
                obj.TYPE_INPUT = False


                rep.InsertRegisterOT(obj, gId)
            Next
            Return True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
        Return False
    End Function
    ''' <summary>
    ''' Load data from file import to Grid
    ''' </summary>
    ''' <param name="dtDataHeader"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function loadToGrid(ByVal dtDataHeader As DataTable) As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If dtDataHeader.Rows.Count = 0 Then
                ShowMessage("File mẫu không đúng định dạng", NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            dtError = dtDataHeader.Clone
            Dim countErr As New Integer
            Dim irow = 5
            Dim objData As New AT_REGISTER_OTDTO
            Dim rep As New AttendanceRepository
            For Each row As DataRow In dtDataHeader.Rows
                isError = False
                rowError = dtError.NewRow
                countErr = 0
                sError = Translate("Chưa nhập mã nhân viên")
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)
                sError = Translate("Từ giờ đang để trống hoặc nhập sai định dạng")
                ImportValidate.IsValidTime("FROM_HOUR", row, rowError, isError, sError)
                sError = Translate("Đến giờ đang để trống hoặc nhập sai định dạng")
                ImportValidate.IsValidTime("TO_HOUR", row, rowError, isError, sError)
                sError = Translate("Ngày đăng ký không được để trống")
                ImportValidate.IsValidDate("WORKINGDAY", row, rowError, isError, sError)

                sError = Translate("Số giờ nghỉ trưa không hợp lệ")
                If Not String.IsNullOrWhiteSpace(row(AT_RegisterOT_OrderBy.Break_Out).ToString) Then
                    ImportValidate.IsValidNumber(AT_RegisterOT_OrderBy.Break_Out, row, rowError, isError, sError)
                End If

                For column As Integer = 13 To 18
                    If row(column) <> "" Then
                        countErr = countErr + 1
                    End If
                    'column = column + 1
                Next
                If countErr = 0 Then
                    isError = True
                    rowError("HS_OT10") = "Bạn chưa nhập hệ số làm thêm"
                ElseIf countErr > 1 Then
                    isError = True
                    rowError("HS_OT10") = "Bạn chỉ được chọn 1 hệ số làm thêm"
                End If

                If row("HS_OT10") <> "" AndAlso row("HS_OT10").ToString().ToUpper() <> "X" Then
                    isError = True
                    rowError("HS_OT10") = "Bạn nhập sai định dạng"
                End If
                If row("HS_OT15") <> "" AndAlso row("HS_OT15").ToString().ToUpper() <> "X" Then
                    isError = True
                    rowError("HS_OT15") = "Bạn nhập sai định dạng"
                End If
                If row("HS_OT20") <> "" AndAlso row("HS_OT20").ToString().ToUpper() <> "X" Then
                    isError = True
                    rowError("HS_OT20") = "Bạn nhập sai định dạng"
                End If
                If row("HS_OT27") <> "" AndAlso row("HS_OT27").ToString().ToUpper() <> "X" Then
                    isError = True
                    rowError("HS_OT27") = "Bạn nhập sai định dạng"
                End If
                If row("HS_OT30") <> "" AndAlso row("HS_OT30").ToString().ToUpper() <> "X" Then
                    isError = True
                    rowError("HS_OT30") = "Bạn nhập sai định dạng"
                End If
                If row("HS_OT39") <> "" AndAlso row("HS_OT39").ToString().ToUpper() <> "X" Then
                    isError = True
                    rowError("HS_OT39") = "Bạn nhập sai định dạng"
                End If
                If row("IS_NB") <> "" AndAlso row("IS_NB").ToString().ToUpper() <> "X" Then
                    isError = True
                    rowError("IS_NB") = "Bạn nhập sai định dạng"
                End If
                If isError = False Then
                    objData.EMPLOYEE_CODE = row("EMPLOYEE_CODE")
                    objData.WORKINGDAY = ToDate(row("WORKINGDAY"))
                    objData.FROM_HOUR = objData.WORKINGDAY.Value.AddHours(TimeSpan.Parse(row("FROM_HOUR")).TotalHours)
                    objData.TO_HOUR = objData.WORKINGDAY.Value.AddHours(TimeSpan.Parse(row("TO_HOUR")).TotalHours)
                    If objData.TO_HOUR < objData.FROM_HOUR Then
                        objData.TO_HOUR = objData.WORKINGDAY.Value.AddDays(1).AddHours(TimeSpan.Parse(row("TO_HOUR")).TotalHours)
                    Else
                        objData.TO_HOUR = objData.WORKINGDAY.Value.AddHours(TimeSpan.Parse(row("TO_HOUR")).TotalHours)
                    End If
                    objData.TYPE_INPUT = False
                    If Not rep.CheckImporAddNewtOT(objData) Then
                        isError = True
                        rowError("FROM_HOUR") = "Thời gian đăng ký bị trùng"
                    End If
                End If

                If isError Then
                    rowError("ID") = irow
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    rowError("VN_FULLNAME") = row("VN_FULLNAME").ToString
                    rowError("ORG_NAME") = row("ORG_NAME").ToString
                    rowError("ORG_PATH") = row("ORG_PATH").ToString
                    rowError("TITLE_NAME") = row("TITLE_NAME").ToString
                    dtError.Rows.Add(rowError)
                End If
                irow = irow + 1
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                Session("EXPORTREPORT") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_ImportOT_error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            Else
                Return True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("VN_FULLNAME", GetType(String))
                dt.Columns.Add("TITLE_NAME", GetType(String))
                dt.Columns.Add("ORG_ID", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("ORG_DESC", GetType(String))
                dt.Columns.Add("WORKINGDAY", GetType(String))
                dt.Columns.Add("FROM_HOUR", GetType(String))
                dt.Columns.Add("TO_HOUR", GetType(String))
                dt.Columns.Add(AT_RegisterOT_OrderBy.Break_Out, GetType(String))
                dt.Columns.Add("NOTE", GetType(String))
                dt.Columns.Add("HS_OT10", GetType(String))
                dt.Columns.Add("HS_OT15", GetType(String))
                dt.Columns.Add("HS_OT20", GetType(String))
                dt.Columns.Add("HS_OT27", GetType(String))
                dt.Columns.Add("HS_OT30", GetType(String))
                dt.Columns.Add("HS_OT39", GetType(String))
                dt.Columns.Add("HS_OT_NAME", GetType(String))
                dt.Columns.Add("IS_NB_NAME", GetType(String))
                dt.Columns.Add("IS_NB", GetType(String))
                dt.Columns.Add("ORG_PATH", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property
    ''' <summary>
    ''' Event OK popup import file mẫu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Try
            Import_Data()
            'Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            'Dim startTime As DateTime = DateTime.UtcNow
            'Dim fileName As String
            'Dim dsDataPrepare As New DataSet
            'Dim workbook As Aspose.Cells.Workbook
            'Dim worksheet As Aspose.Cells.Worksheet
            'Dim dtDataHeard As New DataTable
            'Try
            '    Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            '    Dim savepath = Context.Server.MapPath(tempPath)
            '    For Each file As UploadedFile In ctrlUpload1.UploadedFiles
            '        fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
            '        file.SaveAs(fileName, True)
            '        workbook = New Aspose.Cells.Workbook(fileName)
            '        If workbook.Worksheets.GetSheetByCodeName("ImportOT") Is Nothing Then
            '            ShowMessage(Translate("File mẫu không đúng định dạng"), NotifyType.Warning)
            '            Exit Sub
            '        End If
            '        worksheet = workbook.Worksheets(0)
            '        dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(3, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
            '        If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            '    Next
            '    dtData = dtData.Clone()
            '    dtDataHeard = dtData.Clone()
            '    For Each dt As DataTable In dsDataPrepare.Tables
            '        For Each row In dt.Rows
            '            Dim isRow = ImportValidate.TrimRow(row)
            '            If Not isRow Then
            '                Continue For
            '            End If
            '            dtData.ImportRow(row)
            '            dtDataHeard.ImportRow(row)
            '        Next
            '    Next
            '    If loadToGrid(dtDataHeard) Then
            '        For Each row As DataRow In dtData.Rows
            '            If row("HS_OT10") <> "" Then
            '                row("HS_OT_NAME") = "1.0"
            '            End If
            '            If row("HS_OT15") <> "" Then
            '                row("HS_OT_NAME") = "1.5"
            '            End If
            '            If row("HS_OT20") <> "" Then
            '                row("HS_OT_NAME") = "2.0"
            '            End If
            '            If row("HS_OT27") <> "" Then
            '                row("HS_OT_NAME") = "2.7"
            '            End If
            '            If row("HS_OT30") <> "" Then
            '                row("HS_OT_NAME") = "3.0"
            '            End If
            '            If row("HS_OT39") <> "" Then
            '                row("HS_OT_NAME") = "3.9"
            '            End If
            '        Next
            '        'rgDeclaresOT.VirtualItemCount = dtData.Rows.Count
            '        'rgDeclaresOT.DataSource = dtData
            '        'rgDeclaresOT.DataBind()
            '        'CurrentState = CommonMessage.STATE_NEW
            '        'UpdateControlState()
            '        SaveOT()
            '        Refresh("InsertView")
            '        CurrentState = CommonMessage.STATE_NORMAL
            '        UpdateControlState()
            'End If
            '_myLog.WriteLog(_myLog._info, _classPath, method,
            '                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            '_myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub
    ''' <summary>
    ''' Event OK popup sơ đồ tổ chức
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_DeclareOT&PERIOD_ID=" & cboPeriod.SelectedValue & "&orgid=" & e.CurrentValue & "&IS_DISSOLVE=" & IIf(ctrlOrgPopup.IsDissolve, "1", "0") & "')", True)
            isLoadPopup = 0
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event Cancel popup sơ đồ tổ chức
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrgPopup.CancelClicked
        isLoadPopup = 0
    End Sub
    ''' <summary>
    ''' Event Yes.No Messager hỏi xóa
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
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Load DB to grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_OT_REGISTRATIONDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgDeclaresOT, obj)
            Dim _param = New Attendance.AttendanceBusiness.ParamDTO With {.ORG_ID = ctrlOrganization.CurrentValue, _
                                           .IS_DISSOLVE = ctrlOrganization.IsDissolve,
                                           .IS_FULL = True}
            Dim Sorts As String = rgDeclaresOT.MasterTableView.SortExpressions.GetSortString()

            If rdtungay.SelectedDate Is Nothing Then
                rdtungay.SelectedDate = CType("01/01/" & cboYear.SelectedValue.Trim.ToString, Date)
            End If
            obj.REGIST_DATE_FROM = rdtungay.SelectedDate
            If rdDenngay.SelectedDate.HasValue Then
                obj.REGIST_DATE_TO = rdDenngay.SelectedDate
            Else
                If (cboYear.SelectedValue IsNot Nothing) Then
                    If (Common.Common.GetShortDatePattern().Trim.ToUpper.Contains("DD/MM/YYYY")) Then
                        obj.REGIST_DATE_TO = CType("31/01/" & cboYear.SelectedValue.Trim.ToString, Date)
                    Else
                        obj.REGIST_DATE_TO = CType("01/31/" & cboYear.SelectedValue.Trim.ToString, Date)
                    End If
                    rdDenngay.SelectedDate = obj.REGIST_DATE_TO
                End If
                rdtungay.SelectedDate = CType("01/01/" & cboYear.SelectedValue.Trim.ToString, Date)
            End If
            'If chkChecknghiViec.Checked Then
            '    obj.IS_TERMINATE = True
            'Else
            '    obj.IS_TERMINATE = False
            'End If
            'obj.TYPE_INPUT = False
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.REGISTER_OT = rep.GetRegisterOT(obj, _param, MaximumRows, rgDeclaresOT.CurrentPageIndex, rgDeclaresOT.PageSize, "CREATED_DATE desc")
                Else
                    Me.REGISTER_OT = rep.GetRegisterOT(obj, _param, MaximumRows, rgDeclaresOT.CurrentPageIndex, rgDeclaresOT.PageSize)
                End If
            Else
                Return rep.GetRegisterOT(obj, _param).ToTable
            End If

            rgDeclaresOT.VirtualItemCount = MaximumRows
            rgDeclaresOT.DataSource = Me.REGISTER_OT
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
    ''' <summary>
    ''' Reload, Refresh grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDeclaresOT.NeedDataSource
        Try
            CreateDataFilter(False)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Event Click button tìm kiếm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            'If String.IsNullOrEmpty(cboPeriod.SelectedValue) And rdTungay.SelectedDate.HasValue = False And rdDenngay.SelectedDate.HasValue = False Then
            '    ShowMessage(Translate("Kỳ công chưa được chọn"), NotifyType.Error)
            '    Exit Sub
            'End If
            rgDeclaresOT.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub rgDeclaresOT_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgDeclaresOT.PageIndexChanged
        Try
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim url = e.Argument
            If (url.Contains("reload=1")) Then
                rgDeclaresOT.CurrentPageIndex = 0
                rgDeclaresOT.Rebind()
                If rgDeclaresOT.Items IsNot Nothing AndAlso rgDeclaresOT.Items.Count > 0 Then
                    rgDeclaresOT.Items(0).Selected = True
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub Import_Data()
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
            TableMapping(ds.Tables(0))

            If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then
                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    If rep.INPORT_AT_OT_REGISTRATION(DocXml) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgDeclaresOT.Rebind()
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
        Dim rep1 As New ProfileBusinessClient
        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
        dtTemp.Columns(1).ColumnName = "EMPLOYEE_CODE"
        dtTemp.Columns(5).ColumnName = "REGIST_DATE"
        dtTemp.Columns(6).ColumnName = "FROM_AM"
        dtTemp.Columns(7).ColumnName = "FROM_AM_NN"
        dtTemp.Columns(8).ColumnName = "TO_AM"
        dtTemp.Columns(9).ColumnName = "TO_AM_NN"
        dtTemp.Columns(10).ColumnName = "FROM_PM"
        dtTemp.Columns(11).ColumnName = "FROM_PM_NN"
        dtTemp.Columns(12).ColumnName = "TO_PM"
        dtTemp.Columns(13).ColumnName = "TO_PM_NN"
        dtTemp.Columns(15).ColumnName = "OT_TYPE_ID"
        dtTemp.Columns(17).ColumnName = "OT"
        dtTemp.Columns(18).ColumnName = "NOTE"

        dtTemp.Columns(16).ColumnName = "TOTAL_OT"

        'XOA DONG TIEU DE VA HEADER
        dtTemp.Rows(0).Delete()
        dtTemp.Rows(1).Delete()
        dtTemp.Rows(2).Delete()
        dtTemp.Rows(3).Delete()
        dtTemp.Rows(4).Delete()

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

            Dim totalHour As Decimal = 0.0
            Dim fromAM As Decimal = 0.0
            Dim fromMNAM As Decimal = 0.0
            Dim toAM As Decimal = 0.0
            Dim toMMAM As Decimal = 0.0
            Dim fromPM As Decimal = 0.0
            Dim fromMNPM As Decimal = 0.0
            Dim toPM As Decimal = 0.0
            Dim toMNPM As Decimal = 0.0

            Dim totalFromAM As Decimal = 0.0
            Dim totalToAM As Decimal = 0.0
            Dim totalFromPM As Decimal = 0.0
            Dim totalToPM As Decimal = 0.0
            Dim AM As Decimal = 0.0 'tong tgian OT tu 0am - 6am
            Dim PM As Decimal = 0.0 'tong tgian OT tu 10pm - 11h59pm

            newRow = dtLogs.NewRow
            newRow("ID") = count + 1
            newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")

            ' Nhân viên k có trong hệ thống
            If rep1.CHECK_EMPLOYEE(rows("EMPLOYEE_CODE")) = 0 Then
                newRow("DISCIPTION") = "Mã nhân viên - Không tồn tại,"
                _error = False
            End If

            If IsDBNull(rows("REGIST_DATE")) OrElse rows("REGIST_DATE") = "" OrElse CheckDate(rows("REGIST_DATE")) = False Then
                rows("REGIST_DATE") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày làm thêm - Không đúng định dạng,"
                _error = False
            End If

            If IsNumeric(rows("FROM_AM")) Or IsNumeric(rows("FROM_AM_NN")) Or IsNumeric(rows("TO_AM")) Or IsNumeric(rows("TO_AM_NN")) Then
                If Not (IsNumeric(rows("FROM_AM"))) Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Từ giờ buổi sáng - Không đúng định dạng,"
                    _error = False
                End If

                If Not (IsNumeric(rows("FROM_AM_NN"))) Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Từ phút buổi sáng - Không đúng định dạng,"
                    _error = False
                End If

                If Not (IsNumeric(rows("TO_AM"))) Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Đến giờ buổi sáng - Không đúng định dạng,"
                    _error = False
                End If

                If Not (IsNumeric(rows("TO_AM_NN"))) Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Đến phút buổi sáng - Không đúng định dạng,"
                    _error = False
                End If
            End If

            If IsNumeric(rows("FROM_PM")) Or IsNumeric(rows("FROM_PM_NN")) Or IsNumeric(rows("TO_PM")) Or IsNumeric(rows("TO_PM_NN")) Then
                If Not (IsNumeric(rows("FROM_PM"))) Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Từ giờ buổi chiều - Không đúng định dạng,"
                    _error = False
                End If

                If Not (IsNumeric(rows("FROM_PM_NN"))) Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Từ phút buổi chiều - Không đúng định dạng,"
                    _error = False
                End If

                If Not (IsNumeric(rows("TO_PM"))) Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Đến giờ buổi chiều - Không đúng định dạng,"
                    _error = False
                End If

                If Not (IsNumeric(rows("TO_PM_NN"))) Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Đến phút buổi chiều - Không đúng định dạng,"
                    _error = False
                End If
            End If

            If Not (IsNumeric(rows("OT_TYPE_ID"))) Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Loại làm thêm - Không đúng định dạng,"
                _error = False
            End If

            If Not (IsNumeric(rows("OT"))) Then
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Hệ số - Không đúng định dạng,"
                _error = False
            End If

            AM = 0
            If IsNumeric(rows("FROM_AM")) AndAlso IsNumeric(rows("FROM_AM_NN")) AndAlso IsNumeric(rows("TO_AM")) AndAlso IsNumeric(rows("TO_AM_NN")) Then
                fromAM = rows("FROM_AM")
                fromMNAM = Decimal.Parse(If(rows("FROM_AM_NN") = 30, 0.5, 0))
                toAM = rows("TO_AM")
                toMMAM = Decimal.Parse(If(rows("TO_AM_NN") = 30, 0.5, 0))
                totalFromAM = fromAM + fromMNAM
                totalToAM = toAM + toMMAM
                If totalFromAM > totalToAM Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Giờ làm thêm AM - không hợp lệ,"
                    _error = False
                End If
                AM = totalToAM - totalFromAM
            End If

            PM = 0
            If IsNumeric(rows("FROM_PM")) AndAlso IsNumeric(rows("FROM_PM_NN")) AndAlso IsNumeric(rows("TO_PM")) AndAlso IsNumeric(rows("TO_PM_NN")) Then
                fromPM = rows("FROM_PM")
                fromMNPM = Decimal.Parse(If(rows("FROM_PM_NN") = 30, 0.5, 0))
                toPM = rows("TO_PM")
                toMNPM = Decimal.Parse(If(rows("TO_PM_NN") = 30, 0.5, 0))
                totalFromPM = fromPM + fromMNPM
                totalToPM = toPM + toMNPM
                If totalFromPM > totalToPM Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Giờ làm thêm PM - không hợp lệ,"
                    _error = False
                End If
                PM = totalToPM - totalFromPM
            End If

            rows("TOTAL_OT") = (AM + PM).ToString().Replace(",", ".")

            If CheckDate(rows("REGIST_DATE")) AndAlso IsNumeric(rows("OT")) Then
                If rep.CHECK_OT_REGISTRATION_EXIT(rows("EMPLOYEE_CODE"), rows("REGIST_DATE"), rows("OT")) > 0 Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày OT đã được đăng ký, vui lòng chọn ngày khác,"
                    _error = False
                End If
            End If

            If _error = False Then
                dtLogs.Rows.Add(newRow)
                count = count + 1
                _error = True
            End If
        Next
        dtTemp.AcceptChanges()
    End Sub

    Private Function CheckDate(ByVal value As String) As Boolean
        Dim dateCheck As Boolean
        Dim result As Date

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

    'Private Sub rgDeclaresOT_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgDeclaresOT.ItemDataBound
    '    If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
    '        Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
    '        datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
    '    End If
    'End Sub
#End Region

#Region "Custom"
    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region
End Class
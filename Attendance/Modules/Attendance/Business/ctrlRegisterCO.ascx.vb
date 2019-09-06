Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Telerik.Web.UI
Imports Common.Common
Imports Common.CommonMessage
Imports Attendance.AttendanceRepository
Imports Attendance.AttendanceBusiness
Imports WebAppLog

Public Class ctrlRegisterCO
    Inherits Common.CommonView
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim dsDataComper As New DataTable

    Dim _myLog As New MyLog()
    Dim pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()

#Region "Properties"

    ''' <summary>
    ''' Obj LEAVESHEET
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property LEAVESHEET As List(Of AT_LEAVESHEETDTO)
        Get
            Return ViewState(Me.ID & "_LEAVESHEET")
        End Get
        Set(ByVal value As List(Of AT_LEAVESHEETDTO))
            ViewState(Me.ID & "_LEAVESHEET") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj isLoadPopup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
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
    ''' Obj PERIOD
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

    ''' <summary>
    ''' Obj dtData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("EMPLOYEE_ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("VN_FULLNAME", GetType(String))
                dt.Columns.Add("TITLE_NAME", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("ORG_PATH", GetType(String))
                dt.Columns.Add("STAFF_RANK_NAME", GetType(String))
                dt.Columns.Add("BALANCE_NOW", GetType(String))
                dt.Columns.Add("LEAVE_FROM", GetType(String))
                dt.Columns.Add("LEAVE_TO", GetType(String))
                dt.Columns.Add("MANUAL_NAME", GetType(String))
                dt.Columns.Add("MANUAL_ID", GetType(String))
                dt.Columns.Add("MORNING_NAME", GetType(String))
                dt.Columns.Add("MORNING_ID", GetType(String))
                dt.Columns.Add("AFTERNOON_NAME", GetType(String))
                dt.Columns.Add("AFTERNOON_ID", GetType(String))
                dt.Columns.Add("NOTE", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj dtDataImportEmployee
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
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 15/08/2017 10:00
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
            'If Not IsPostBack Then
            '    chkOnlyOrgSelected.Checked = False
            'End If
            ctrlOrganization.AutoPostBack = True
            ctrlOrganization.LoadDataAfterLoaded = True
            ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrganization.CheckBoxes = TreeNodeTypes.None
            Refresh()
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Xét các trạng thái của grid rgRegisterLeave
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SetGridFilter(rgRegisterLeave)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgRegisterLeave.AllowCustomPaging = True
            rgRegisterLeave.ClientSettings.EnablePostBackOnRowClick = False
            ctrlUpload.isMultiple = AsyncUpload.MultipleFileSelection.Disabled
            ctrlUpload.MaxFileInput = 1
            InitControl()
            If Not IsPostBack Then '
                GirdConfig(rgRegisterLeave)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham khoi taso toolbar voi cac button them moi, sua, xuat file, xoa, in hop dong
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
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
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức BindData
    ''' Gọi phương thức load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'Dim lsData As List(Of AT_PERIODDTO)
            Dim rep As New AttendanceRepository
            'Dim table As New DataTable
            'table.Columns.Add("YEAR", GetType(Integer))
            'table.Columns.Add("ID", GetType(Integer))
            'Dim row As DataRow
            'For index = 2015 To Date.Now.Year + 1
            '    row = table.NewRow
            '    row("ID") = index
            '    row("YEAR") = index
            '    table.Rows.Add(row)
            'Next
            'FillRadCombobox(cboYear, table, "YEAR", "ID")
            'cboYear.SelectedValue = Date.Now.Year
            'Dim period As New AT_PERIODDTO
            'period.ORG_ID = 1
            'period.YEAR = Date.Now.Year
            'lsData = rep.LOAD_PERIODBylinq(period)
            'Me.PERIOD = lsData
            'FillRadCombobox(cboPeriod, lsData, "PERIOD_NAME", "PERIOD_ID", True)

            Dim dtData As New DataTable
            dtData = rep.GetOtherList("PROCESS_STATUS", True)
            FillRadCombobox(cbStatus, dtData, "NAME", "ID", True)
            rdtungay.SelectedDate = New DateTime(DateTime.Now.Year, 1, 1)
            rdDenngay.SelectedDate = New DateTime(DateTime.Now.Year, 12, 31)
            'If lsData.Count > 0 Then
            '    'Dim periodid = (From d In lsData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
            '    If periodid IsNot Nothing Then
            '        'cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
            '        rdtungay.SelectedDate = periodid.START_DATE
            '        rdDenngay.SelectedDate = periodid.END_DATE
            '    Else
            '        'cboPeriod.SelectedIndex = 0
            '        'Dim periodid1 = (From d In lsData Where d.PERIOD_ID.ToString.Contains(cboPeriod.SelectedValue.ToString) Select d).FirstOrDefault

            '        If periodid1 IsNot Nothing Then
            '            rdtungay.SelectedDate = periodid1.START_DATE
            '            rdDenngay.SelectedDate = periodid1.END_DATE
            '        End If
            '        'If (Common.Common.GetShortDatePattern().Trim.ToUpper.Contains("DD/MM/YYYY")) Then
            '        '    rdtungay.SelectedDate = CType("01/01/" & cboYear.SelectedValue.Trim.ToString, Date)
            '        '    rdDenngay.SelectedDate = CType("31/01/" & cboYear.SelectedValue.Trim.ToString, Date)
            '        'Else
            '        '    rdtungay.SelectedDate = CType("01/01/" & cboYear.SelectedValue.Trim.ToString, Date)
            '        '    rdDenngay.SelectedDate = CType("01/31/" & cboYear.SelectedValue.Trim.ToString, Date)
            '        'End If

            '        End If
            'End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức update trạng thái của các control theo state
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case isLoadPopup
                Case 1
                    phPopup.Controls.Clear()
                    ctrlOrgPopup = Me.Register("ctrlOrgPopup", "Common", "ctrlFindOrgPopup")
                    phPopup.Controls.Add(ctrlOrgPopup)
                    Exit Sub
            End Select

            Dim rep As New AttendanceRepository
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of AT_LEAVESHEETDTO)
                    For idx = 0 To rgRegisterLeave.SelectedItems.Count - 1
                        Dim lst = New AT_LEAVESHEETDTO
                        Dim item As GridDataItem = rgRegisterLeave.SelectedItems(idx)
                        lst.ID = item.GetDataKeyValue("ID")
                        lstDeletes.Add(lst)
                    Next
                    If rep.DeleteLeaveSheet(lstDeletes) Then
                        Refresh("UpdateView")
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức selectedindexchange của control cboYear
    ''' </summary>
    ''' <remarks></remarks>
    'Private Sub ctrlYear_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        Dim dtData As List(Of AT_PERIODDTO)
    '        Dim rep As New AttendanceRepository
    '        Dim period As New AT_PERIODDTO
    '        If String.IsNullOrEmpty(cboYear.SelectedValue) Then
    '            Exit Sub
    '        End If
    '        period.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
    '        period.YEAR = Decimal.Parse(cboYear.SelectedValue)
    '        dtData = rep.LOAD_PERIODBylinq(period)
    '        cboPeriod.ClearSelection()
    '        FillRadCombobox(cboPeriod, dtData, "PERIOD_NAME", "PERIOD_ID", True)
    '        If dtData.Count > 0 Then
    '            Dim periodid = (From d In dtData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
    '            If periodid IsNot Nothing Then
    '                cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
    '                rdtungay.SelectedDate = periodid.START_DATE
    '                rdDenngay.SelectedDate = periodid.END_DATE
    '            Else
    '                cboPeriod.SelectedIndex = 0
    '                Dim per = (From c In dtData Where c.PERIOD_ID = cboPeriod.SelectedValue).FirstOrDefault
    '                rdtungay.SelectedDate = per.START_DATE
    '                rdDenngay.SelectedDate = per.END_DATE
    '            End If
    '        End If
    '        _myLog.WriteLog(_myLog._info, _classPath, method,
    '                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Xet cac tuy chon gia tri cua Message la UpdateView, InsertView
    ''' Bind lai du lieu cho grid rgRegisterLeave
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
                        rgRegisterLeave.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgRegisterLeave.CurrentPageIndex = 0
                        rgRegisterLeave.MasterTableView.SortExpressions.Clear()
                        rgRegisterLeave.Rebind()
                    Case "Cancel"
                        rgRegisterLeave.MasterTableView.ClearSelectedItems()
                End Select
            End If
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien SelectedNodeChanged cua control ctrlOrg
    ''' Xet lai cac thiet lap trang thai cho grid rgRegisterLeave
    ''' Bind lai du lieu cho rgRegisterLeave
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                'ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
            End If
            rgRegisterLeave.CurrentPageIndex = 0
            rgRegisterLeave.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command khi click item tren toolbar
    ''' Command xuat excel, xuat file mau, nhap file mau
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New AttendanceRepository
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue), _
                                            .IS_DISSOLVE = ctrlOrganization.IsDissolve}

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_DELETE
                    ' kiem tra ky cong da dong chua?
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    'Kiểm tra các điều kiện để xóa.
                    If rgRegisterLeave.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgRegisterLeave.ExportExcel(Server, Response, dtDatas, "LEAVESHEET")
                        End If
                    End Using
                Case "EXPORT_TEMP"
                    ' kiem tra ky cong da dong chua?
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    isLoadPopup = 1 'Chọn Org
                    UpdateControlState()
                    ctrlOrgPopup.Show()
                Case "IMPORT_TEMP"
                    ' kiem tra ky cong da dong chua?
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    ctrlUpload.AllowedExtensions = "xls,xlsx"
                    ctrlUpload.Show()
                    _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command button cua control ctrkMessageBox
    ''' Cap nhat trang thai cua cac control
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
                _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức lấy data cho rgRegisterLeave
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_LEAVESHEETDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgRegisterLeave, obj)
            Dim _param = New ParamDTO With {.ORG_ID = ctrlOrganization.CurrentValue, _
                                           .IS_DISSOLVE = ctrlOrganization.IsDissolve,
                                           .IS_FULL = True}
            Dim Sorts As String = rgRegisterLeave.MasterTableView.SortExpressions.GetSortString()
            If IsNumeric(cbStatus.SelectedValue) Then
                obj.STATUS = cbStatus.SelectedValue
            End If
            If rdtungay.SelectedDate.HasValue Then
                obj.FROM_DATE = rdtungay.SelectedDate
            End If
            If rdDenngay.SelectedDate.HasValue Then
                obj.END_DATE = rdDenngay.SelectedDate
            End If
            If chkChecknghiViec.Checked Then
                obj.ISTEMINAL = True
            End If
            obj.IS_APP = -1
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.LEAVESHEET = rep.GetLeaveSheet(obj, _param, MaximumRows, rgRegisterLeave.CurrentPageIndex, rgRegisterLeave.PageSize, "CREATED_DATE desc")
                Else
                    Me.LEAVESHEET = rep.GetLeaveSheet(obj, _param, MaximumRows, rgRegisterLeave.CurrentPageIndex, rgRegisterLeave.PageSize)
                End If
            Else
                Return rep.GetLeaveSheet(obj, _param).ToTable
            End If

            rgRegisterLeave.VirtualItemCount = MaximumRows
            rgRegisterLeave.DataSource = Me.LEAVESHEET
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cho rgRegisterLeave
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgRegisterLeave.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter(False)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho button btnSearch
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'If String.IsNullOrEmpty(cboPeriod.SelectedValue) And rdtungay.SelectedDate.HasValue = False And rdDenngay.SelectedDate.HasValue = False Then
            '    ShowMessage(Translate("Kỳ công chưa được chọn"), NotifyType.Error)
            '    Exit Sub
            'End If
            rgRegisterLeave.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien PageIndexChanged cho rgRegisterLeave
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgRegisterLeave.PageIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien OrganizationSelected cua control ctrlOrgPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New AttendanceRepository
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(e.CurrentValue),
                                           .IS_DISSOLVE = ctrlOrgPopup.IsDissolve}
            If rep.IS_PERIODSTATUS(_param) = False Then
                ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                Exit Sub
            End If

            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Register&&orgid=" & e.CurrentValue & "&IS_DISSOLVE=" & IIf(ctrlOrgPopup.IsDissolve, "1", "0") & "')", True)
            isLoadPopup = 0
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien click cua buton ctrlUpload
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
                ShowMessage(Translate("Bảng dữ liệu chưa có cột hoặc file không đúng định dạng"), NotifyType.Warning)
                Exit Sub
            End If
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            For Each file As UploadedFile In ctrlUpload.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                Try
                    workbook = New Aspose.Cells.Workbook(fileName)
                Catch ex As Exception
                    If ex.ToString.Contains("This file's format is not supported or you don't specify a correct format") Then
                        ShowMessage(Translate("Bảng dữ liệu chưa có cột hoặc file không đúng định dạng"), NotifyType.Warning)
                        Exit Sub
                    End If
                End Try
                If workbook.Worksheets.GetSheetByCodeName("IMPORT_REGISTER_CO") Is Nothing Then
                    ShowMessage(Translate("File mẫu không đúng định dạng"), NotifyType.Warning)
                    Exit Sub
                End If
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(6, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
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
                Dim objSIGN As AT_LEAVESHEETDTO
                Dim gID As Decimal
                Dim dtDataImp As DataTable = dsDataPrepare.Tables(0)
                For Each dr In dsDataComper.Rows
                    objSIGN = New AT_LEAVESHEETDTO
                    objSIGN.EMPLOYEE_ID = CDec(dr("EMPLOYEE_ID"))
                    objSIGN.EMPLOYEE_CODE = dr("EMPLOYEE_CODE")
                    objSIGN.BALANCE_NOW = CDec(If(dr("BALANCE_NOW") = "", Nothing, dr("BALANCE_NOW")))
                    objSIGN.LEAVE_FROM = ToDate(dr("LEAVE_FROM"))
                    objSIGN.LEAVE_TO = ToDate(dr("LEAVE_TO"))
                    objSIGN.MANUAL_ID = CDec(dr("MANUAL_ID"))
                    'objSIGN.ORG_ID = CDec(dr("ORG_ID"))
                    objSIGN.NOTE = dr("NOTE")
                    rep.InsertLeaveSheet(objSIGN, gID)
                Next
                CurrentState = CommonMessage.STATE_NORMAL
                Refresh("InsertView")
                _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            End If
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức validate cho file được upload
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
            Dim dtEmpID As DataTable
            'Dim is_Validate As Boolean
            Dim _validate As New AT_LEAVESHEETDTO
            Dim rep As New AttendanceRepository
            dtData.TableName = "DATA"
            dtDataImportEmployee = dtData.Clone
            dtError = dtData.Clone
            Dim irow = 8
            Dim irowEm = 8

            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)

                sError = "Nghỉ từ ngày không được để trống"
                ImportValidate.IsValidDate("LEAVE_FROM", row, rowError, isError, sError)
                sError = "Nghỉ đến ngày không được để trống"
                ImportValidate.IsValidDate("LEAVE_TO", row, rowError, isError, sError)
                If rowError("LEAVE_FROM").ToString = "" And _
                   rowError("LEAVE_TO").ToString = "" And _
                    row("LEAVE_FROM").ToString <> "" And _
                   row("LEAVE_TO").ToString <> "" Then
                    Dim startdate = Date.Parse(row("LEAVE_FROM"))
                    Dim enddate = Date.Parse(row("LEAVE_TO"))
                    If startdate > enddate Then
                        rowError("LEAVE_FROM") = "Nghỉ từ ngày phải nhỏ hơn nghỉ tới ngày"
                        isError = True
                    End If
                End If
                sError = "Kiểu công"
                ImportValidate.IsValidList("MANUAL_NAME", "MANUAL_ID", row, rowError, isError, sError)
                sError = "Kiểu công buổi sáng"
                ImportValidate.IsValidList("MORNING_NAME", "MORNING_ID", row, rowError, isError, sError)
                sError = "Kiểu công buổi chiều"
                ImportValidate.IsValidList("AFTERNOON_NAME", "AFTERNOON_ID", row, rowError, isError, sError)

                If isError Then
                    rowError("STT") = irow
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    rowError("VN_FULLNAME") = row("VN_FULLNAME").ToString
                    rowError("ORG_NAME") = row("ORG_NAME").ToString
                    rowError("ORG_PATH") = row("ORG_PATH").ToString
                    rowError("TITLE_NAME") = row("TITLE_NAME").ToString
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
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Register_Error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
            If isError Then
                Return False
            Else
                ' check nv them vao file import có nằm trong hệ thống không.
                For j As Integer = 0 To dsDataComper.Rows.Count - 1
                    If dsDataComper(j)("EMPLOYEE_ID") = "" Then
                        dtEmpID = New DataTable
                        dtEmpID = rep.GetEmployeeID(dsDataComper(j)("EMPLOYEE_CODE"), rdDenngay.SelectedDate)
                        rowError = dtError.NewRow
                        If dtEmpID Is Nothing Or dtEmpID.Rows.Count <= 0 Then
                            rowError("EMPLOYEE_CODE") = "Mã nhân viên " & dsDataComper(j)("EMPLOYEE_CODE") & " không tồn tại trên hệ thống."
                            isError = True
                        Else
                            dsDataComper(j)("EMPLOYEE_ID") = dtEmpID.Rows(0)("ID")
                        End If
                    End If
                    If isError Then
                        rowError("ID") = irowEm
                        dtError.Rows.Add(rowError)
                    End If
                    irowEm = irowEm + 1
                    isError = False
                Next
                If dtError.Rows.Count > 0 Then
                    dtError.TableName = "DATA"
                    Session("EXPORTREPORT") = dtError
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Register_Error')", True)
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Else
                    'check phép < -3
                    dtError = rep.checkLeaveImport(dtData)
                    If dtError IsNot Nothing AndAlso dtError.Rows.Count > 0 Then
                        dtError.TableName = "DATA"
                        Session("EXPORTREPORT") = dtError
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Register_Error')", True)
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    Else
                        Return True
                    End If
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien ItemDataBound cua control rgRegisterLeave
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgRegisterLeave_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgRegisterLeave.ItemDataBound
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

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien SelectedIndexChanged cua control cboPeriod
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub cboPeriod_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriod.SelectedIndexChanged
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        Dim rep As New AttendanceRepository
    '        Dim period As New AT_PERIODDTO
    '        period.YEAR = Decimal.Parse(cboYear.SelectedValue)
    '        period.ORG_ID = 46
    '        If cboPeriod.SelectedValue <> "" Then
    '            Dim Lstperiod = rep.LOAD_PERIODBylinq(period)
    '            Dim p = (From o In Lstperiod Where o.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)).FirstOrDefault
    '            If p IsNot Nothing Then
    '                rdtungay.SelectedDate = p.START_DATE
    '                rdDenngay.SelectedDate = p.END_DATE
    '            End If
    '        End If
    '        rgRegisterLeave.Rebind()
    '        ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
    '        _myLog.WriteLog(_myLog._info, _classPath, method,
    '                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien AjaxRequest cua control AjaxManager
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
                rgRegisterLeave.CurrentPageIndex = 0
                rgRegisterLeave.Rebind()
                If rgRegisterLeave.Items IsNot Nothing AndAlso rgRegisterLeave.Items.Count > 0 Then
                    rgRegisterLeave.Items(0).Selected = True
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

End Class
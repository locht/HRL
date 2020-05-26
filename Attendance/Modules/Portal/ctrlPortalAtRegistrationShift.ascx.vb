Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Common.Common
Imports Attendance.AttendanceBusiness
Imports Telerik.Web.UI
Imports HistaffFrameworkPublic.HistaffFrameworkEnum
Imports WebAppLog

Public Class ctrlPortalAtRegistrationShift
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Portal/" + Me.GetType().Name.ToString()
#Region "Property"

    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
    Public Property unit As String

    Property RegistrationList As List(Of AtPortalRegistrationShiftDTO)
        Get
            Return ViewState(Me.ID & "_OT_REGISTRATIONDTOS")
        End Get
        Set(ByVal value As List(Of AtPortalRegistrationShiftDTO))
            ViewState(Me.ID & "_OT_REGISTRATIONDTOS") = value
        End Set
    End Property

    Property OtRegistrationTotal As Int32
        Get
            Return ViewState(Me.ID & "_OtRegistrationTotal")
        End Get
        Set(ByVal value As Int32)
            ViewState(Me.ID & "_OtRegistrationTotal") = value
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

#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New AttendanceRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteOtRegistration(lstDeletes) Then
                        Refresh("UpdateView")
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case CommonMessage.STATE_APPROVE
                    Dim lstApp As New List(Of AT_OT_REGISTRATIONDTO)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        Dim dto As New AT_OT_REGISTRATIONDTO
                        dto.ID = item.GetDataKeyValue("ID")
                        dto.REGIST_DATE = item.GetDataKeyValue("REGIST_DATE")
                        'Kiem tra ky cong da dong hay chua
                        Dim periodid = rep.GetperiodID(EmployeeID, dto.REGIST_DATE, dto.REGIST_DATE)
                        If periodid = 0 Then
                            ShowMessage(Translate("Kiểm tra lại kì công"), NotifyType.Warning)
                            Exit Sub
                        End If
                        Dim checkKicong = rep.CHECK_PERIOD_CLOSE(periodid)
                        If checkKicong = 0 Then
                            ShowMessage(Translate("Kì công đã đóng. Vui lòng kiểm tra lại!"), NotifyType.Warning)
                            Exit Sub
                        End If

                        dto.STATUS = PortalStatus.WaitingForApproval
                        dto.EMPLOYEE_ID = LogHelper.CurrentUser.EMPLOYEE_ID
                        dto.REASON = ""
                        lstApp.Add(dto)
                    Next
                    If Not rep.SendApproveOtRegistration(lstApp) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                    End If
            End Select
            rep.Dispose()
        Catch ex As Exception

        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        'rgMain.SetFilter()
        SetFilter(rgMain)
        rgMain.AllowCustomPaging = True
        rgMain.PageSize = Common.Common.DefaultPageSize
        CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Seperator, _
                          ToolbarItem.Export, ToolbarItem.Seperator, ToolbarItem.Delete)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim dtData As DataTable
        Using rep As New AttendanceRepository
            'dtData = rep.GetOtherList("PROCESS_STATUS", True)
            'If dtData IsNot Nothing Then
            '    Dim data = dtData.AsEnumerable().Where(Function(f) Not f.Field(Of Decimal?)("ID").HasValue _
            '                                               Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.Saved).ToString() _
            '                                               Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.WaitingForApproval).ToString() _
            '                                               Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.ApprovedByLM).ToString() _
            '                                               Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.UnApprovedByLM).ToString()).CopyToDataTable()
            '    'FillRadCombobox(cboStatus, data, "NAME", "ID")

            'End If

            Dim table As New DataTable
            Dim lsData As List(Of AT_PERIODDTO)
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
            cboYear.SelectedValue = Year(DateTime.Now)
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
                    rdRegDateFrom.SelectedDate = periodid.START_DATE
                    rdRegDateTo.SelectedDate = periodid.END_DATE
                Else
                    cboPeriod.SelectedValue = lsData.Item(0).PERIOD_ID.ToString()
                    rdRegDateFrom.SelectedDate = lsData.Item(0).START_DATE
                    rdRegDateTo.SelectedDate = lsData.Item(0).END_DATE
                End If
            End If
        End Using
    End Sub

#End Region

#Region "Event"

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            rgMain.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        If item.GetDataKeyValue("STATUS") <> PortalStatus.Saved And item.GetDataKeyValue("STATUS") <> PortalStatus.UnApprovedByLM Then
                            ShowMessage(Translate("Thao tác này chỉ áp dụng đối với trạng thái Chưa gửi duyệt, Không phê duyệt. Vui lòng chọn dòng khác."), NotifyType.Error)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtDatas, "Overtime Record")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SUBMIT
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim listDataCheck As New List(Of AT_PROCESS_DTO)
                    Dim datacheck As AT_PROCESS_DTO
                    'Kiểm tra các điều kiện trước khi xóa
                    For Each dr As Telerik.Web.UI.GridDataItem In rgMain.SelectedItems
                        If dr.GetDataKeyValue("STATUS") <> PortalStatus.Saved And dr.GetDataKeyValue("STATUS") <> PortalStatus.UnApprovedByLM Then
                            ShowMessage(String.Format(Translate("Trạng thái {0} không thể gửi duyệt. Vui lòng chọn dòng khác."), dr.GetDataKeyValue("STATUS_NAME")), NotifyType.Warning)
                            Exit Sub
                        End If
                        datacheck = New AT_PROCESS_DTO With {
                            .EMPLOYEE_ID = dr.GetDataKeyValue("EMPLOYEE_ID"),
                            .FROM_DATE = dr.GetDataKeyValue("REGIST_DATE"),
                            .FULL_NAME = dr.GetDataKeyValue("FULLNAME")
                        }
                        listDataCheck.Add(datacheck)
                    Next

                    Dim itemError As New AT_PROCESS_DTO
                    Using rep As New AttendanceRepository
                        Dim checkResult = rep.CheckTimeSheetApproveVerify(listDataCheck, "LEAVE", itemError)
                        If Not checkResult Then
                            ShowMessage(String.Format(Translate("Thời gian biểu của {0} trong tháng {1} đã được phê duyệt."), itemError.FULL_NAME, itemError.FROM_DATE.Value.Month & "/" & itemError.FROM_DATE.Value.Year), NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

                    ctrlMessageBox.MessageText = Translate("Gửi duyệt. Bạn có chắc chắn gửi duyệt?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_SUBMIT
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_SUBMIT And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_APPROVE
            UpdateControlState()
        End If

        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
        rgMain.Rebind()
    End Sub

    ''' <summary>
    ''' Event SelectedIndexChange combobox năm
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
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            dtData = rep.LOAD_PERIODBylinq(period)
            cboPeriod.ClearSelection()
            FillRadCombobox(cboPeriod, dtData, "PERIOD_NAME", "PERIOD_ID", True)
            If dtData.Count > 0 Then
                Dim periodid = (From d In dtData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                    rdRegDateFrom.SelectedDate = periodid.START_DATE
                    rdRegDateTo.SelectedDate = periodid.END_DATE
                    'rgMain.Rebind()
                Else
                    cboPeriod.SelectedIndex = 0
                    Dim per = (From c In dtData Where c.PERIOD_ID = cboPeriod.SelectedValue).FirstOrDefault
                    rdRegDateFrom.SelectedDate = per.START_DATE
                    rdRegDateTo.SelectedDate = per.END_DATE
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
    ''' Event SelectedIndexChanged combobox kỳ công
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriod.SelectedIndexChanged
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            rdRegDateFrom.Clear()
            rdRegDateTo.Clear()
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            period.ORG_ID = 46
            If cboPeriod.SelectedValue <> "" Then
                Dim Lstperiod = rep.LOAD_PERIODBylinq(period)

                Dim p = (From o In Lstperiod Where o.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)).FirstOrDefault
                rdRegDateFrom.SelectedDate = p.START_DATE
                rdRegDateTo.SelectedDate = p.END_DATE
            End If
            'rgTimeTimesheet_machine.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim _filter As New AtPortalRegistrationShiftDTO
        Try
            _filter.EMPLOYEE_ID = EmployeeID
            _filter.CREATED_BY = LogHelper.CurrentUser.EMPLOYEE_ID
            If rdRegDateFrom.SelectedDate.HasValue Then
                _filter.DATE_FROM_SEARCH = rdRegDateFrom.SelectedDate
            End If
            If rdRegDateTo.SelectedDate.HasValue Then
                _filter.DATE_TO_SEARCH = rdRegDateTo.SelectedDate
            End If
            'If Not String.IsNullOrEmpty(cboStatus.SelectedValue) Then
            '    _filter.STATUS = cboStatus.SelectedValue
            'End If
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            'If isFull Then
            '    If Sorts IsNot Nothing Then
            '        Return rep.GetAtRegShift(_filter, Integer.MaxValue, 0, Integer.MaxValue, Sorts).ToTable()
            '    Else
            '        Return rep.GetAtRegShift(_filter, Integer.MaxValue, 0, Integer.MaxValue).ToTable
            '    End If
            'Else
            '    If Sorts IsNot Nothing Then
            '        Me.RegistrationList = rep.GetAtRegShift(_filter, Me.OtRegistrationTotal, rgMain.CurrentPageIndex, rgMain.PageSize, Sorts)
            '    Else
            '        Me.RegistrationList = rep.GetAtRegShift(_filter, Me.OtRegistrationTotal, rgMain.CurrentPageIndex, rgMain.PageSize)
            '    End If
            'End If
            If Sorts IsNot Nothing Then
                Me.RegistrationList = rep.GetAtRegShift(_filter, Me.OtRegistrationTotal, rgMain.CurrentPageIndex, rgMain.PageSize, Sorts)
            Else
                Me.RegistrationList = rep.GetAtRegShift(_filter, Me.OtRegistrationTotal, rgMain.CurrentPageIndex, rgMain.PageSize)
            End If
            rgMain.VirtualItemCount = Me.OtRegistrationTotal
            rgMain.DataSource = Me.RegistrationList

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
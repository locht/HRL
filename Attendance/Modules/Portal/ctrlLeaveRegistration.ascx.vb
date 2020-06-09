Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Common.Common
Imports Attendance.AttendanceBusiness
Imports Attendance.AttendanceRepository
Imports Telerik.Web.UI
Imports System.Web.Configuration
Imports HistaffFrameworkPublic.HistaffFrameworkEnum
Imports WebAppLog

Public Class ctrlLeaveRegistration
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Portal/" + Me.GetType().Name.ToString()
    Dim psp As New AttendanceStoreProcedure
#Region "Property"

    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
    Public Property unit As String

    ''' <summary>
    ''' Obj LEAVESHEET
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property LEAVESHEET As DataTable
        Get
            Return ViewState(Me.ID & "_LEAVESHEET")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_LEAVESHEET") = value
        End Set
    End Property


    Property LeaveMasters As List(Of AT_PORTAL_REG_DTO)
        Get
            Return ViewState(Me.ID & "_LeaveMasters")
        End Get
        Set(ByVal value As List(Of AT_PORTAL_REG_DTO))
            ViewState(Me.ID & "_LeaveMasters") = value
        End Set
    End Property

    Property LeaveMasterTotal As Int32
        Get
            Return ViewState(Me.ID & "_LeaveMasterTotal")
        End Get
        Set(ByVal value As Int32)
            ViewState(Me.ID & "_LeaveMasterTotal") = value
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
                    Dim lstDeletes As New List(Of AT_LEAVESHEETDTO)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        Dim obj As New AT_LEAVESHEETDTO
                        obj.ID = item.GetDataKeyValue("ID")
                        lstDeletes.Add(obj)
                    Next
                    If rep.DeleteLeaveSheet(lstDeletes) Then
                        Refresh("UpdateView")
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                    rgMain.Rebind()
            End Select
        Catch ex As Exception

        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then

                'Dim table As New DataTable
                'table.Columns.Add("YEAR", GetType(Integer))
                'table.Columns.Add("ID", GetType(Integer))
                'Dim row As DataRow
                'row = table.NewRow
                'row("ID") = 0
                'row("YEAR") = DBNull.Value
                'table.Rows.Add(row)
                'For index = 2010 To Date.Now.Year + 1
                '    row = table.NewRow
                '    row("ID") = index
                '    row("YEAR") = index
                '    table.Rows.Add(row)
                'Next
                'FillRadCombobox(cboYear, table, "YEAR", "ID")
                'cboYear.SelectedValue = Date.Now.Year
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        'rgMain.SetFilter()
        rgMain.AllowCustomPaging = True
        rgMain.PageSize = Common.Common.DefaultPageSize
        SetFilter(rgMain)
        CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Seperator, ToolbarItem.Export, ToolbarItem.Seperator, ToolbarItem.Delete)
            'BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Seperator, ToolbarItem.Submit, ToolbarItem.Export, ToolbarItem.Print, ToolbarItem.Seperator, ToolbarItem.Delete)
            'CType(MainToolBar.Items(5), RadToolBarButton).Text = "In đơn phép"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim dtData As DataTable
        Dim store As New AttendanceStoreProcedure
        Dim arr As New ArrayList()
        Dim dtLeaveType As New DataTable
        Try
            dtLeaveType = store.GET_LEAVE_TYPE(True, True)
            FillRadCombobox(cboManual, dtLeaveType, "NAME", "ID")
            Using rep As New AttendanceRepository
                dtData = rep.GetOtherList("PROCESS_STATUS", True)
                If dtData IsNot Nothing Then
                    Dim data = dtData.AsEnumerable().Where(Function(f) Not f.Field(Of Decimal?)("ID").HasValue _
                                                               Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.WaitingForApproval).ToString() _
                                                               Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.UnApprovedByLM).ToString() _
                                                               Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.RestoreApproveByLM).ToString() _
                                                               Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.Saved).ToString()).CopyToDataTable()
                    FillRadCombobox(cboStatus, data, "NAME", "ID")

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
                            rdtungay.SelectedDate = periodid.START_DATE
                            rdDenngay.SelectedDate = periodid.END_DATE
                        Else
                            cboPeriod.SelectedValue = lsData.Item(0).PERIOD_ID.ToString()
                            rdtungay.SelectedDate = lsData.Item(0).START_DATE
                            rdDenngay.SelectedDate = lsData.Item(0).END_DATE
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception

        End Try
        
        'txtYear.Value = Date.Now.Year
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
                        If item.GetDataKeyValue("STATUS") <> PortalStatus.Saved And item.GetDataKeyValue("STATUS") <> PortalStatus.WaitingForApproval Then
                            ShowMessage(Translate("Chỉ được xóa ở trạng thái chưa gửi duyệt hoặc đang chờ duyệt. Vui lòng kiểm tra lại !"), NotifyType.Error)
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
                            rgMain.ExportExcel(Server, Response, dtDatas, "Leave Record")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_PRINT
                    Dim filePath As String = String.Empty
                    Dim extension As String = String.Empty
                    Dim path As String = String.Empty
                    Dim iError As Integer = 0
                    Dim _IdPhep As Decimal?
                    Dim _IdEmp As Decimal?
                    Dim _ToDate As Date?

                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage("Bạn chưa chọn bản ghi nào để in", NotifyType.Warning)
                        Exit Sub
                    ElseIf rgMain.SelectedItems.Count > 1 Then
                        ShowMessage("Bạn chỉ được chọn 1 bản ghi để in", NotifyType.Warning)
                        Exit Sub
                    Else
                        For Each dr As GridDataItem In rgMain.SelectedItems
                            _IdPhep = Decimal.Parse(dr.GetDataKeyValue("ID").ToString)
                            _IdEmp = Decimal.Parse(dr.GetDataKeyValue("ID_EMPLOYEE").ToString)
                            _ToDate = Date.Parse(dr.GetDataKeyValue("TO_DATE").ToString)
                        Next
                    End If

                    ' Get Data print DK nghi
                    Dim table As DataTable
                    Dim repS As New AttendanceStoreProcedure
                    table = repS.PRINT_DONPHEP(_IdPhep, _IdEmp, _ToDate)

                    If Not System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory & "Files\") Then
                        System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory & "Files\")
                    Else
                        Dim dir2 As String() = System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory & "Files\")
                        For Each f2 As String In dir2
                            Try
                                System.IO.File.Delete(f2)
                            Catch ex As Exception
                            End Try
                        Next
                    End If

                    If Not Utilities.GetTemplateLinkFile("DonPhep", "Attendance", filePath, extension, iError) Then
                        If (iError = 1) Then
                            ShowMessage("Biểu mẫu không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                    End If

                    Dim fileNameOutput As String = "_DKN_" & Date.Now.ToString("yyyyMMddHHmmss")
                    If table.Rows.Count = 1 Then   ' Export file 1 page từ 1 file mẫu
                        fileNameOutput = table.Rows(0)("MASO_NV") & fileNameOutput
                        Using word As New WordCommon
                            word.ExportMailMerge(filePath, fileNameOutput, table, Response)
                        End Using
                    Else ' Export file nhiều page từ 1 file mẫu
                        Dim docMutilePage As New Aspose.Words.Document()
                        docMutilePage.RemoveAllChildren()
                        fileNameOutput = "ALL" & fileNameOutput

                        For i = 0 To table.Rows.Count - 1
                            Dim doc As New Aspose.Words.Document(filePath)
                            doc.MailMerge.Execute(table.Rows(i))
                            docMutilePage.AppendDocument(doc, Aspose.Words.ImportFormatMode.KeepSourceFormatting)
                        Next

                        docMutilePage.Save(Response, fileNameOutput & extension,
                                     Aspose.Words.ContentDisposition.Attachment,
                                     Aspose.Words.Saving.SaveOptions.CreateSaveOptions(Aspose.Words.SaveFormat.Doc))
                    End If
                    'Dim table = GetDataPrintFromGrid(rgRegisterLeave)
                    'If table.Rows.Count = 0 Then
                    '    ShowMessage("Bạn chưa chọn bản ghi nào để in", NotifyType.Warning)
                    '    Exit Sub
                    'End If

                Case CommonMessage.TOOLBARITEM_SUBMIT
                    'Dim listDataCheck As New List(Of AT_PROCESS_DTO)
                    'Dim datacheck As AT_PROCESS_DTO
                    'For idx = 0 To rgMain.SelectedItems.Count - 1
                    '    Dim item As GridDataItem = rgMain.SelectedItems(idx)
                    '    If item.GetDataKeyValue("STATUS") = PortalStatus.ApprovedByLM OrElse item.GetDataKeyValue("STATUS") = PortalStatus.WaitingForApproval OrElse item.GetDataKeyValue("STATUS") = PortalStatus.UnApprovedByLM Then
                    '        ShowMessage(Translate("Đang ở trạng thái chờ phê duyệt hoặc đã phê duyệt,không thể chỉnh sửa"), NotifyType.Error)
                    '        Exit Sub
                    '    End If
                    'Next
                    'Dim itemError As New AT_PROCESS_DTO
                    'Using rep As New AttendanceRepository
                    '    Dim checkResult = rep.CheckTimeSheetApproveVerify(listDataCheck, "LEAVE", itemError)
                    '    If Not checkResult Then
                    '        If itemError.FROM_DATE IsNot Nothing Then
                    '            ShowMessage(String.Format(Translate("TimeSheet of {0} in {1} has been approved"), itemError.FULL_NAME, itemError.FROM_DATE.Value.Month & "/" & itemError.FROM_DATE.Value.Year), NotifyType.Warning)
                    '            Exit Sub
                    '        Else
                    '            ShowMessage(String.Format(Translate("TimeSheet of {0} in {1} has been approved"), itemError.FULL_NAME, itemError.TO_DATE.Value.Month & "/" & itemError.TO_DATE.Value.Year), NotifyType.Warning)
                    '            Exit Sub
                    '        End If
                    '    End If
                    'End Using
                    'Dim strId As String
                    'For Each dr As GridDataItem In rgMain.SelectedItems
                    '    strId += dr.GetDataKeyValue("ID").ToString + ","
                    'Next
                    'strId = strId.Remove(strId.LastIndexOf(",")).ToString
                    'If strId.Contains(",") Then
                    '    ShowMessage(Translate("Chỉ gửi được từng đơn một"), NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    Dim _count As Integer = 0
                    Dim _item As GridDataItem
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        _item = rgMain.SelectedItems(idx)
                        _count += 1
                    Next

                    If _count > 1 Then 'KIỂM TRA SỐ LƯỢNG ĐƠN GỬI
                        ShowMessage(Translate("Chỉ gửi được từng đơn một. Vui lòng thử lại !"), NotifyType.Warning)
                        Exit Sub
                    ElseIf _item.GetDataKeyValue("STATUS") <> PortalStatus.Saved Then 'KIỂM TRA TRẠNG THÁI ĐƠN
                        ShowMessage(Translate("Chỉ gửi đơn ở trạng thái Chưa gửi duyệt. Vui lòng thử lại !"), NotifyType.Warning)
                        Exit Sub
                    Else
                        ctrlMessageBox.MessageText = Translate("Bạn có muốn gửi phê duyệt?")
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_SUBMIT
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    End If

            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_SUBMIT And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            Dim lstApp As New List(Of AT_PORTAL_REG_DTO)
            Dim strId As String
            Dim sign_id As Integer
            Dim period_id As Integer
            Dim id_group As Integer
            Dim sumday As Integer
            For Each dr As GridDataItem In rgMain.SelectedItems
                strId += dr.GetDataKeyValue("ID").ToString + ","
            Next
            strId = strId.Remove(strId.LastIndexOf(",")).ToString
            Dim dtCheckSendApprove As DataTable = psp.CHECK_APPROVAL(strId)
            If dtCheckSendApprove.Rows.Count > 0 Then
                If dtCheckSendApprove(0)("MESSAGE") > 1 Then
                    ShowMessage(Translate("Không thể gửi phê duyệt các loại nghỉ khác nhau cùng lúc"), NotifyType.Warning)
                    Exit Sub
                End If
            End If
            period_id = Utilities.ObjToDecima(dtCheckSendApprove.Rows(0)("PERIOD_ID"))
            sign_id = Utilities.ObjToDecima(dtCheckSendApprove.Rows(0)("SIGN_ID"))
            id_group = Utilities.ObjToDecima(dtCheckSendApprove.Rows(0)("ID_REGGROUP"))
            sumday = Utilities.ObjToDecima(dtCheckSendApprove.Rows(0)("SUMDAY"))
            Using rep As New AttendanceRepository
                Dim check = rep.CHECK_PERIOD_CLOSE(period_id)

                If check = 0 Then
                    ShowMessage(Translate("Kì công đã đóng,Xin kiểm tra lại"), NotifyType.Warning)
                    Exit Sub
                End If
            End Using
            Dim outNumber As Decimal
            Try
                Dim IAttendance As IAttendanceBusiness = New AttendanceBusinessClient()
                outNumber = IAttendance.PRI_PROCESS_APP(EmployeeID, period_id, "LEAVE", 0, sumday, sign_id, id_group, LogHelper.CurrentUser.EMPLOYEE_ID)
            Catch ex As Exception
                ShowMessage(ex.ToString, NotifyType.Error)
            End Try

            If outNumber = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
            ElseIf outNumber = 1 Then
                ShowMessage(Translate("Quy trình phê duyệt chưa được thiết lập"), NotifyType.Success)
            ElseIf outNumber = 2 Then
                ShowMessage(Translate("Thao tác xảy ra lỗi,bạn kiểm tra lại quy trình"), NotifyType.Error)
            ElseIf outNumber = 3 Then
                ShowMessage(Translate("Nhân viên chưa có thiết lập nhóm chức danh"), NotifyType.Error)
            End If
            rgMain.Rebind()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
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
                    rdtungay.SelectedDate = periodid.START_DATE
                    rdDenngay.SelectedDate = periodid.END_DATE
                    'rgMain.Rebind()
                Else
                    cboPeriod.SelectedIndex = 0
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
            rdtungay.Clear()
            rdDenngay.Clear()
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            period.ORG_ID = 46
            If cboPeriod.SelectedValue <> "" Then
                Dim Lstperiod = rep.LOAD_PERIODBylinq(period)

                Dim p = (From o In Lstperiod Where o.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)).FirstOrDefault
                rdtungay.SelectedDate = p.START_DATE
                rdDenngay.SelectedDate = p.END_DATE
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
        Dim _filter As New AT_LEAVESHEETDTO
        Dim lstLeaveSheet As New List(Of AT_LEAVESHEETDTO)
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            _filter.EMPLOYEE_ID = EmployeeID
            _filter.IS_APP = -1
            _filter.CREATED_BY_EMP = LogHelper.CurrentUser.EMPLOYEE_ID
            If IsNumeric(cboStatus.SelectedValue) Then
                _filter.STATUS = cboStatus.SelectedValue
            End If
            If rdtungay.SelectedDate.HasValue Then
                _filter.FROM_DATE = rdtungay.SelectedDate
            End If
            If IsNumeric(cboManual.SelectedValue) Then
                _filter.MANUAL_ID = cboManual.SelectedValue
            End If
            If rdDenngay.SelectedDate.HasValue Then
                _filter.END_DATE = rdDenngay.SelectedDate
            End If
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    lstLeaveSheet = rep.GetLeaveSheet_Portal(_filter, MaximumRows, rgMain.CurrentPageIndex, rgMain.PageSize, "CREATED_DATE desc")
                Else
                    lstLeaveSheet = rep.GetLeaveSheet_Portal(_filter, MaximumRows, rgMain.CurrentPageIndex, rgMain.PageSize)
                End If
            Else
                Return rep.GetLeaveSheet_Portal(_filter).ToTable
            End If

            rgMain.VirtualItemCount = MaximumRows
            rgMain.DataSource = lstLeaveSheet

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Common.Common
Imports Attendance.AttendanceBusiness
Imports Telerik.Web.UI
Imports System.Web.Configuration
Imports HistaffFrameworkPublic.HistaffFrameworkEnum

Public Class ctrlLeaveRegistration
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Public Overrides Property MustAuthorize As Boolean = False
    Dim psp As New AttendanceStoreProcedure
#Region "Property"

    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
    Public Property unit As String

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
                    If rep.DeletePortalReg(lstDeletes) Then
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
            BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Seperator, ToolbarItem.Submit, ToolbarItem.Export, ToolbarItem.Seperator, ToolbarItem.Delete)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim dtData As DataTable
        Using rep As New AttendanceRepository
            dtData = rep.GetOtherList("PROCESS_STATUS", True)
            If dtData IsNot Nothing Then
                Dim data = dtData.AsEnumerable().Where(Function(f) Not f.Field(Of Decimal?)("ID").HasValue _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.WaitingForApproval).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.ApprovedByLM).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.UnApprovedByLM).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.Saved).ToString()).CopyToDataTable()
                FillRadCombobox(cboStatus, data, "NAME", "ID")
            End If
        End Using
        txtYear.Value = Date.Now.Year
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
                            ShowMessage(Translate("Chỉ được xóa ở trạng thái chưa gửi duyệt và không phê duyệt"), NotifyType.Error)
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
                Case CommonMessage.TOOLBARITEM_SUBMIT
                    Dim listDataCheck As New List(Of AT_PROCESS_DTO)

                    Dim datacheck As AT_PROCESS_DTO
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        If item.GetDataKeyValue("STATUS") = PortalStatus.ApprovedByLM OrElse item.GetDataKeyValue("STATUS") = PortalStatus.WaitingForApproval Then
                            ShowMessage(Translate("Đang ở trạng thái chờ phê duyệt hoặc đã phê duyệt,không thể chỉnh sửa"), NotifyType.Error)
                            Exit Sub
                        End If
                    Next

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


                    ctrlMessageBox.MessageText = Translate("Bạn có muốn gửi phê duyệt?")
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
            Dim lstApp As New List(Of AT_PORTAL_REG_DTO)
            Dim strId As String
            Dim sign_id As Integer
            Dim period_id As Integer
            Dim id_group As Integer
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
                If dtCheckSendApprove(0)("SIGN_ID").ToString <> "" Then
                    sign_id = dtCheckSendApprove(0)("SIGN_ID")
                End If
                If dtCheckSendApprove(0)("PERIOD_ID").ToString <> "" Then
                    period_id = dtCheckSendApprove(0)("PERIOD_ID")
                End If
                If dtCheckSendApprove(0)("ID_REGGROUP").ToString <> "" Then
                    id_group = dtCheckSendApprove(0)("ID_REGGROUP")
                End If
            End If

            Using rep As New AttendanceRepository
                Dim check = rep.CHECK_PERIOD_CLOSE(period_id)

                If check = 0 Then
                    ShowMessage(Translate("Kì công đã đóng,Xin kiểm tra lại"), NotifyType.Warning)
                    Exit Sub
                End If
            End Using


            Dim outNumber As Decimal = AttendanceRepositoryStatic.Instance.PRI_PROCESS_APP(EmployeeID, period_id, "LEAVE", 0, 0, sign_id, id_group)
            If outNumber = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
            ElseIf outNumber = 1 Then
                ShowMessage(Translate("CHƯA CÓ TEMPLATE"), NotifyType.Success)
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
            End If

            rgMain.Rebind()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
    End Sub
#End Region

#Region "Custom"
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim _filter As New AT_PORTAL_REG_DTO
        Try
            _filter.ID_EMPLOYEE = EmployeeID
            '_filter.YEAR = 0
            If txtYear.Value.HasValue Then
                _filter.YEAR = txtYear.Value
            End If
            If Not String.IsNullOrEmpty(cboStatus.SelectedValue) Then
                _filter.STATUS = cboStatus.SelectedValue
            End If
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()

            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetLeaveRegistrationList(_filter, Integer.MaxValue, 0, Integer.MaxValue, Sorts).ToTable()
                Else
                    Return rep.GetLeaveRegistrationList(_filter, Integer.MaxValue, 0, Integer.MaxValue).ToTable
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.LeaveMasters = rep.GetLeaveRegistrationList(_filter, Me.LeaveMasterTotal, rgMain.CurrentPageIndex, rgMain.PageSize, Sorts)
                Else
                    Me.LeaveMasters = rep.GetLeaveRegistrationList(_filter, Me.LeaveMasterTotal, rgMain.CurrentPageIndex, rgMain.PageSize)
                End If
            End If

            rgMain.VirtualItemCount = Me.LeaveMasterTotal
            rgMain.DataSource = Me.LeaveMasters

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
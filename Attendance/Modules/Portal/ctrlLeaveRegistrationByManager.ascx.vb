Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Common.Common
Imports Attendance.AttendanceBusiness
Imports Telerik.Web.UI
Imports HistaffFrameworkPublic.HistaffFrameworkEnum

Public Class ctrlLeaveRegistrationByManager
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"

    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
    Public Property unit As String

    Property LeaveMasters As DataTable
        Get
            Return ViewState(Me.ID & "_LeaveMasters")
        End Get
        Set(ByVal value As DataTable)
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
                Case CommonMessage.STATE_APPROVE
                    Dim lstApprove As New List(Of AT_PORTAL_REG_DTO)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        Dim dto As New AT_PORTAL_REG_DTO
                        dto.ID = item.GetDataKeyValue("ID")
                        dto.YEAR = item.GetDataKeyValue("YEAR")
                        dto.STATUS = PortalStatus.ApprovedByLM
                        lstApprove.Add(dto)
                    Next
                    'If rep.ApprovePortalRegList(lstApprove) Then
                    '    CurrentState = CommonMessage.STATE_NORMAL
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    '    Refresh("UpdateView")
                    'Else
                    '    CurrentState = CommonMessage.STATE_NORMAL
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    'End If
                    rgMain.Rebind()
            End Select
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
        rgMain.AllowCustomPaging = True
        rgMain.PageSize = Common.Common.DefaultPageSize
        SetFilter(rgMain)
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Approve, ToolbarItem.Reject, ToolbarItem.Export)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).Text = Translate("Phê duyệt (QLTT)")
            CType(Me.MainToolBar.Items(1), RadToolBarButton).Text = Translate("Không phê duyệt (QLTT)")
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
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
                cboStatus.SelectedValue = PortalStatus.WaitingForApproval
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
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtDatas, "Approve Leave Request (LM)")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_APPROVE
                    Dim listDataCheck As New List(Of AT_PROCESS_DTO)
                   

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_APPROVE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_REJECT
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlCommon_Reject.Show()
            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            Dim ID_EMPLOYEE As Integer
            Dim NOTE As String
            Dim ID_REGGROUP As Integer
            Dim fromdate As Date
            Dim todate As Date
            For Each dr As GridDataItem In rgMain.SelectedItems
                ID_EMPLOYEE = dr.GetDataKeyValue("ID_EMPLOYEE")
                NOTE = dr.GetDataKeyValue("NOTE")
                ID_REGGROUP = dr.GetDataKeyValue("ID_REGGROUP")
                fromdate = dr.GetDataKeyValue("FROM_DATE")
                todate = dr.GetDataKeyValue("TO_DATE")
                Using rep As New AttendanceRepository
                    Dim periodid = rep.GetperiodID(ID_EMPLOYEE, fromdate, todate)
                    Dim result = rep.PRI_PROCESS(EmployeeID, ID_EMPLOYEE, periodid, 1, "LEAVE", NOTE, ID_REGGROUP)
                    If result = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        Refresh("UpdateView")
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                End Using
            Next
            rgMain.Rebind()
            UpdateControlState()
        End If
    End Sub

    Protected WithEvents ctrlCommon_Reject As ctrlCommon_Reject
    Private Sub ctrlCommon_Reject_ButtonCommand(ByVal sender As Object, ByVal e As CommandSaveEventArgs) Handles ctrlCommon_Reject.ButtonCommand
        Dim strComment As String = e.Comment
        Dim ID_EMPLOYEE As Integer

        Dim ID_REGGROUP As Integer
        Dim fromdate As Date
        Dim todate As Date
        For Each dr As GridDataItem In rgMain.SelectedItems
            ID_EMPLOYEE = dr.GetDataKeyValue("ID_EMPLOYEE")
            ID_REGGROUP = dr.GetDataKeyValue("ID_REGGROUP")
            fromdate = dr.GetDataKeyValue("FROM_DATE")
            todate = dr.GetDataKeyValue("TO_DATE")
            Using rep As New AttendanceRepository
                Dim periodid = rep.GetperiodID(ID_EMPLOYEE, fromdate, todate)
                Dim result = rep.PRI_PROCESS(EmployeeID, ID_EMPLOYEE, periodid, 2, "LEAVE", strComment, ID_REGGROUP)
                If result = 0 Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Refresh("UpdateView")
                Else
                    CurrentState = CommonMessage.STATE_NORMAL
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            End Using
        Next
        Using rep As New AttendanceRepository
            'If rep.ApprovePortalRegList(lstReject) Then
            '    CurrentState = CommonMessage.STATE_NORMAL
            '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
            '    Refresh("UpdateView")
            'Else
            '    CurrentState = CommonMessage.STATE_NORMAL
            '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
            'End If
        End Using
        rgMain.Rebind()
        UpdateControlState()
    End Sub


#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim _filter As New AT_PORTAL_REG_DTO
        Try
            _filter.ID_EMPLOYEE = EmployeeID
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
                    Return rep.PRS_GETLEAVE_BY_APPROVE(EmployeeID, cboStatus.SelectedValue, txtYear.Text)
                Else
                    Return rep.PRS_GETLEAVE_BY_APPROVE(EmployeeID, cboStatus.SelectedValue, txtYear.Text)
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.LeaveMasters = rep.PRS_GETLEAVE_BY_APPROVE(EmployeeID, cboStatus.SelectedValue, txtYear.Text)
                Else
                    Me.LeaveMasters = rep.PRS_GETLEAVE_BY_APPROVE(EmployeeID, cboStatus.SelectedValue, txtYear.Text)
                End If
            End If
            rgMain.MasterTableView.FilterExpression = String.Empty
            rgMain.VirtualItemCount = Me.LeaveMasterTotal
            rgMain.DataSource = Me.LeaveMasters

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
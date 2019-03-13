Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Common
Imports Telerik.Web.UI
Imports HistaffFrameworkPublic
Imports System.IO

Public Class ctrlRC_Request
    Inherits Common.CommonView
    Protected WithEvents RequestView As ViewBase
    Private store As New RecruitmentStoreProcedure()
    'Dim sListRejectID As String = ""

#Region "Property"

    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property

    Property sListRejectID As String
        Get
            Return ViewState(Me.ID & "_ListRejectID")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_ListRejectID") = value
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

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            Dim dtData As DataTable
            Using rep As New RecruitmentRepository
                dtData = rep.GetOtherList("RC_REQUEST_STATUS", True)
                FillRadCombobox(cboStatus, dtData, "NAME", "ID")
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            ''TODO: DAIDM comment
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Delete,
                                       ToolbarItem.Approve, ToolbarItem.Reject) ', ToolbarItem.Seperator, ToolbarItem.Export)
            CType(MainToolBar.Items(3), RadToolBarButton).Text = "Phê duyệt"
            CType(MainToolBar.Items(4), RadToolBarButton).Text = "Không phê duyệt"
            'CType(MainToolBar.Items(6), RadToolBarButton).Text = "Xuất tờ trình"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New RecruitmentRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteRequest(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case CommonMessage.STATE_APPROVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.UpdateStatusRequest(lstDeletes, RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_REJECT
                    'Dim lstDeletes As New List(Of Decimal)
                    'For idx = 0 To rgData.SelectedItems.Count - 1
                    '    Dim item As GridDataItem = rgData.SelectedItems(idx)
                    '    lstDeletes.Add(item.GetDataKeyValue("ID"))
                    'Next
                    'If rep.UpdateStatusRequest(lstDeletes, RecruitmentCommon.RC_REQUEST_STATUS.NOT_APPROVE_ID) Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    '    CurrentState = CommonMessage.STATE_NORMAL
                    '    rgData.Rebind()
                    'Else
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    'End If

                    CurrentState = CommonMessage.STATE_NORMAL
                    rgData.Rebind()
            End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") = RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng thái phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If item.GetDataKeyValue("STATUS_ID") = RecruitmentCommon.RC_REQUEST_STATUS.NOT_APPROVE_ID Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng thái không phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_APPROVE
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") = RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng thái phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_APPROVE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_REJECT
                    Dim bStatus As Boolean = True
                    sListRejectID = ""
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") = RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng thái phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            bStatus = False
                            Exit Sub
                        Else
                            sListRejectID = sListRejectID & item.GetDataKeyValue("ID") & ","
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_REJECT)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_REJECT
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                    'If bStatus Then

                    '    'rwPopup.NavigateUrl = ConfigurationManager.AppSettings("HostPath").ToString() & hddLinkPopup.Value & "&RejectID=" & sListRejectID
                    '    rwPopup.NavigateUrl = "http://" & Request.Url.Host & ":" & Request.Url.Port & "/" & hddLinkPopup.Value & "&RejectID=" & sListRejectID
                    '    rwPopup.Width = "500"
                    '    rwPopup.Height = "250"
                    '    rwPopup.VisibleOnPageLoad = True
                    'End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Try
                        If rgData.SelectedItems.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                            Exit Sub
                        End If

                        Dim sIDList As String = ""
                        Dim hfr As New HistaffFrameworkRepository
                        Dim tempPath As String = "~/Word/Recruitment/BM04_TT_Ke_hoach_TD.doc"
                        For Each dr As Telerik.Web.UI.GridDataItem In rgData.SelectedItems
                            sIDList &= IIf(sIDList = vbNullString, Decimal.Parse(dr("ID").Text).ToString, "," & Decimal.Parse(dr("ID").Text).ToString)
                        Next
                        If sIDList = "," Then
                            sIDList = ""
                        End If
                        If sIDList <> "" Then

                            'If store.CHECK_REQUEST_NOT_APPROVE(sIDList) > 0 Then
                            '    ShowMessage(Translate("Tồn tại thông tin chưa được phê duyệt.<br /> Vui lòng kiểm tra lại!"), NotifyType.Warning)
                            '    Exit Sub
                            'End If

                            Dim dsData As DataSet = New DataSet
                            Dim arr() As String
                            arr = sIDList.Split(",")
                            For Each val As String In arr
                                Dim ds = hfr.ExecuteToDataSet("PKG_RECRUITMENT.EXPORT_RC_NEEDS_BYLISTID", New List(Of Object)({val}))
                                dsData.Merge(ds, True, MissingSchemaAction.Add)
                            Next

                            If (dsData Is Nothing OrElse dsData.Tables(0).Rows.Count = 0) Then
                                ShowMessage(Translate("Không có dữ liệu để in báo cáo"), NotifyType.Warning)
                                Exit Sub
                            End If

                            Dim tbl1 As New DataTable
                            Dim col_NAM As New DataColumn("NAM", GetType(String))
                            Dim col_THANG As New DataColumn("THANG", GetType(String))
                            Dim col_NGAY As New DataColumn("NGAY", GetType(String))

                            tbl1.Columns.Add(col_NAM)
                            tbl1.Columns.Add(col_THANG)
                            tbl1.Columns.Add(col_NGAY)

                            Dim row = tbl1.NewRow
                            row(0) = DateTime.Now.Year.ToString()
                            row(1) = DateTime.Now.Month.ToString()
                            row(2) = DateTime.Now.Month.ToString()
                            tbl1.Rows.Add(row)

                            dsData.Tables.Add(tbl1)
                            dsData.Tables(0).TableName = "DT1"
                            dsData.Tables(1).TableName = "DT"

                            For i As Int32 = 0 To dsData.Tables(0).Rows.Count - 1
                                dsData.Tables(0).Rows(i)("STT") = i + 1
                            Next

                            If Not System.IO.File.Exists(MapPath(tempPath)) Then
                                ShowMessage(Translate("Mẫu báo cáo không tồn tại"), NotifyType.Warning)
                                Exit Sub
                            End If

                            ExportWordMailMergeDS(System.IO.Path.Combine(Server.MapPath(tempPath)),
                                                "TT_KeHoach_TD_" + DateTime.Now.ToString("HHmmssddMMyyyy") + ".doc",
                                                dsData, Response)
                        End If
                    Catch ex As Exception

                    End Try


            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_APPROVE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_REJECT And e.ButtonID = MessageBoxButtonType.ButtonYes Then

                'rwPopup.NavigateUrl = ConfigurationManager.AppSettings("HostPath").ToString() & hddLinkPopup.Value & "&RejectID=" & sListRejectID
                'rwPopup.NavigateUrl = "http://" & Request.Url.Host & ":" & Request.Url.Port & "/" & hddLinkPopup.Value & "&RejectID=" & sListRejectID
                rwPopup.NavigateUrl = "~/" & hddLinkPopup.Value & "&RejectID=" & sListRejectID
                rwPopup.Width = "500"
                rwPopup.Height = "250"
                rwPopup.VisibleOnPageLoad = True

                'CurrentState = CommonMessage.STATE_REJECT
                'UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged

        Try
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()
            rwPopup.VisibleOnPageLoad = False
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow("ORG_DESC").Text)
        End If
    End Sub

#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New RequestDTO
        Dim rep As New RecruitmentRepository
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                rgData.DataSource = New List(Of RequestDTO)
                Exit Function
            End If
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue), _
                                               .IS_DISSOLVE = ctrlOrg.IsDissolve}
            _filter.FROM_DATE = rdFromDate.SelectedDate
            _filter.TO_DATE = rdToDate.SelectedDate
            If cboStatus.SelectedValue <> "" Then
                _filter.STATUS_ID = Decimal.Parse(cboStatus.SelectedValue)
            End If
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of RequestDTO)
            If Sorts IsNot Nothing Then
                lstData = rep.GetRequest(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
            Else
                lstData = rep.GetRequest(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
            End If

            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = lstData
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        rgData.CurrentPageIndex = 0
        rgData.MasterTableView.SortExpressions.Clear()
        rgData.Rebind()
    End Sub
End Class
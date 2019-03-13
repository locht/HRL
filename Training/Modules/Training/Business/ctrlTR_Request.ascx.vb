Imports Framework.UI
Imports Framework.UI.Utilities
Imports Training.TrainingBusiness
Imports Common
Imports Telerik.Web.UI

Public Class ctrlTR_Request
    Inherits Common.CommonView
    Protected WithEvents RequestView As ViewBase

    Dim rep As New TrainingRepository

#Region "Property"

    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property

#End Region

#Region "Page"

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
    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Delete,
                                       ToolbarItem.Print, ToolbarItem.Approve, ToolbarItem.Reject)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New TrainingRepository
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
    Public Overrides Sub UpdateControlState()
        Dim rep As New TrainingRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            MyBase.BindData()
            Dim dtData As DataTable
            dtData = rep.GetOtherList("DECISION_STATUS", True)
            FillRadCombobox(cboStatus, dtData, "NAME", "ID")

            rntYear.Value = Today.Date.Year
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
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_DELETE
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") = TrainingCommon.TR_REQUEST_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng thái phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        ElseIf item.GetDataKeyValue("STATUS_ID") = TrainingCommon.TR_REQUEST_STATUS.NOT_APPROVE_ID Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng thái không phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_PRINT

                Case CommonMessage.TOOLBARITEM_APPROVE
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") = TrainingCommon.TR_REQUEST_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng thái phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        ElseIf item.GetDataKeyValue("STATUS_ID") = TrainingCommon.TR_REQUEST_STATUS.NOT_APPROVE_ID Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng thái không phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_APPROVE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_REJECT
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") = TrainingCommon.TR_REQUEST_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng thái phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        ElseIf item.GetDataKeyValue("STATUS_ID") = TrainingCommon.TR_REQUEST_STATUS.NOT_APPROVE_ID Then
                            ShowMessage(Translate("Tồn tại bản ghi đã ở trạng thái không phê duyệt, thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_REJECT)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_REJECT
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Select Case e.ActionName
                    Case CommonMessage.TOOLBARITEM_DELETE
                        Dim lstDeletes As New List(Of Decimal)
                        For idx = 0 To rgData.SelectedItems.Count - 1
                            Dim item As GridDataItem = rgData.SelectedItems(idx)
                            lstDeletes.Add(item.GetDataKeyValue("ID"))
                        Next
                        If rep.DeleteTrainingRequests(lstDeletes) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If

                    Case CommonMessage.TOOLBARITEM_APPROVE
                        Dim lstDeletes As New List(Of Decimal)
                        For idx = 0 To rgData.SelectedItems.Count - 1
                            Dim item As GridDataItem = rgData.SelectedItems(idx)
                            lstDeletes.Add(item.GetDataKeyValue("ID"))
                        Next
                        If rep.UpdateStatusTrainingRequests(lstDeletes, TrainingCommon.TR_REQUEST_STATUS.APPROVE_ID) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If

                    Case CommonMessage.TOOLBARITEM_REJECT
                        Dim sListRejectID As String = ""
                        For idx = 0 To rgData.SelectedItems.Count - 1
                            Dim item As GridDataItem = rgData.SelectedItems(idx)
                            sListRejectID &= item.GetDataKeyValue("ID").ToString & ","
                        Next
                        rwPopup.NavigateUrl = "~/Dialog.aspx?mid=Training&fid=ctrlTR_RequestReject&group=Business&noscroll=1&RejectID=" & sListRejectID
                        rwPopup.Width = "500"
                        rwPopup.Height = "250"
                        rwPopup.VisibleOnPageLoad = True

                        'Dim lstDeletes As New List(Of Decimal)
                        'For idx = 0 To rgData.SelectedItems.Count - 1
                        '    Dim item As GridDataItem = rgData.SelectedItems(idx)
                        '    lstDeletes.Add(item.GetDataKeyValue("ID"))
                        'Next
                        'If rep.UpdateStatusTrainingRequests(lstDeletes, TrainingCommon.TR_REQUEST_STATUS.NOT_APPROVE_ID) Then
                        '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        '    CurrentState = CommonMessage.STATE_NORMAL
                        '    rgData.Rebind()
                        'Else
                        '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        'End If
                End Select
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
    Protected Sub CreateDataFilter(Optional ByVal isFull As Boolean = False)
        Dim _filter As New RequestDTO
        Dim rep As New TrainingRepository
        Dim total As Integer = 0
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                rgData.DataSource = New List(Of RequestDTO)
                Exit Sub
            End If

            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue), _
                                               .IS_DISSOLVE = ctrlOrg.IsDissolve}

            If cboStatus.SelectedValue <> String.Empty Then
                _filter.STATUS_ID = cboStatus.SelectedValue
            End If
            _filter.YEAR = rntYear.Value
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of RequestDTO)
            If Sorts IsNot Nothing Then
                lstData = rep.GetTrainingRequests(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
            Else
                lstData = rep.GetTrainingRequests(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
            End If
            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = lstData
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                'datarow("COM_NAME").ToolTip = Utilities.DrawTreeByString(datarow("COM_DESC").Text)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow("ORG_DESC").Text)
            End If

            'If TypeOf (e.Item) Is GridPagerItem Then
            '    Dim myPageSizeCombo As RadComboBox = e.Item.FindControl("PageSizeComboBox")
            '    myPageSizeCombo.Items.Clear()
            '    Dim arrPageSizes() As String = {"10", "20", "50", "100", "200", "500", "1000"}
            '    For x As Integer = 0 To UBound(arrPageSizes)
            '        Dim myRadComboBoxItem As New RadComboBoxItem(arrPageSizes(x))
            '        myPageSizeCombo.Items.Add(myRadComboBoxItem)
            '        'add the following line
            '        myRadComboBoxItem.Attributes.Add("ownerTableViewId", rgWorking.MasterTableView.ClientID)
            '    Next
            '    myPageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = True
            'End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        rgData.Rebind()
    End Sub

#End Region

#Region "Custom"
#End Region

End Class
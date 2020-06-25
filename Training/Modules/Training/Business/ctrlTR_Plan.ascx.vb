Imports Framework.UI
Imports Framework.UI.Utilities
Imports Training.TrainingBusiness
Imports HistaffFrameworkPublic
Imports Common
Imports Telerik.Web.UI

Public Class ctrlTR_Plan
    Inherits Common.CommonView
    Protected WithEvents RequestView As ViewBase

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

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Delete, ToolbarItem.Approve, ToolbarItem.Reject)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New TrainingRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    CurrentState = CommonMessage.STATE_NORMAL
                    If rep.DeletePlans(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                Case CommonMessage.STATE_APPROVE
                    Dim lstId As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstId.Add(item.GetDataKeyValue("ID"))
                    Next
                    CurrentState = CommonMessage.STATE_NORMAL
                    If rep.ApproveListPlan(lstId, 1) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If

                Case CommonMessage.STATE_REJECT
                    Dim lstId As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstId.Add(item.GetDataKeyValue("ID"))
                    Next
                    CurrentState = CommonMessage.STATE_NORMAL
                    If rep.ApproveListPlan(lstId, 0) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New TrainingRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                rntYear.Value = Date.Now.Year
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

    Public Overrides Sub BindData()
        Try

            GetDataCombo()
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
                    
                    Dim repHF = New HistaffFrameworkRepository
                    Dim dtData1 As New DataTable
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        dtData1 = repHF.ExecuteToDataSet("PKG_TRAINING.PLAN_CHECK_REQUEST", New List(Of Object)({item.GetDataKeyValue("ID")})).Tables(0)
                        If dtData1 IsNot Nothing Then
                            If dtData1.Rows.Count >= 1 Then
                                ShowMessage(Translate("Kế hoạch thuộc Yêu cầu đào tạo đã được phê duyệt, xin thử lại."), NotifyType.Warning)
                                Exit Sub
                            End If
                        End If
                    Next
                    Dim item1 As GridDataItem = rgData.SelectedItems(0)
                    If item1.GetDataKeyValue("STATUS_ID") = 4001 Then
                        ShowMessage(Translate("Bản ghi đã phê duyệt. Thao tác thực hiện không thành công"), NotifyType.Warning)
                        Exit Sub
                    End If
                   
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_APPROVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each dr As Telerik.Web.UI.GridDataItem In rgData.SelectedItems
                        If dr.GetDataKeyValue("STATUS_ID") = 4001 Or dr.GetDataKeyValue("STATUS_ID") = 4002 Then
                            ShowMessage(Translate("Thao tác này chỉ áp dụng ở trạng thái Chờ phê duyệt"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate("Bạn có muốn phê duyệt?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_REJECT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    For Each dr As Telerik.Web.UI.GridDataItem In rgData.SelectedItems
                        If dr.GetDataKeyValue("STATUS_ID") = 4001 Or dr.GetDataKeyValue("STATUS_ID") = 4002 Then
                            ShowMessage(Translate("Thao tác này chỉ áp dụng ở trạng thái Chờ phê duyệt"), NotifyType.Warning)
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
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_APPROVE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_REJECT And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_REJECT
                UpdateControlState()
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
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        rgData.Rebind()
    End Sub

    Private Sub cboGroup_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboGroup.SelectedIndexChanged
        Try
            Dim store As New TrainingStoreProcedure
            Dim dtData As New DataTable
            cboCourse.ClearSelection()
            If cboGroup.SelectedValue = "" Then
                dtData = store.GET_COURSE_BY_PROGRAM_GROUP(True)
            Else
                dtData = store.GET_COURSE_BY_PROGRAM_GROUP(True, cboGroup.SelectedValue)
            End If
            FillRadCombobox(cboCourse, dtData, "NAME", "ID")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Custom"

    Protected Sub CreateDataFilter(Optional ByVal isFull As Boolean = False)
        Dim _filter As New PlanDTO
        Dim rep As New TrainingRepository
        Dim total As Integer = 0
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                rgData.DataSource = New List(Of PlanDTO)
                Exit Sub
            End If

            If cboGroup.SelectedValue <> "" Then
                _filter.GR_PROGRAM_ID = cboGroup.SelectedValue
            End If

            If cboCourse.SelectedValue <> "" Then
                _filter.TR_COURSE_ID = cboCourse.SelectedValue
            End If

            If cboStatus.SelectedValue <> "" Then
                _filter.STATUS_ID = cboStatus.SelectedValue
            End If

            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue), _
                                               .IS_DISSOLVE = ctrlOrg.IsDissolve}
            _filter.YEAR = rntYear.Value
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of PlanDTO)
            If Sorts IsNot Nothing Then
                lstData = rep.GetPlans(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
            Else
                lstData = rep.GetPlans(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
            End If
            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = lstData
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetDataCombo()
        Dim store As New TrainingStoreProcedure
        Dim rep As New TrainingRepository
        Dim dtData As DataTable
        Try
            dtData = store.GET_PROGRAM_GROUP(True)
            FillRadCombobox(cboGroup, dtData, "NAME", "ID")
            dtData = store.GET_COURSE_BY_PROGRAM_GROUP(True)
            FillRadCombobox(cboCourse, dtData, "NAME", "ID")
            dtData = rep.GetOtherList("TR_REQUEST_STATUS", True)
            FillRadCombobox(cboStatus, dtData, "NAME", "ID", True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region


End Class
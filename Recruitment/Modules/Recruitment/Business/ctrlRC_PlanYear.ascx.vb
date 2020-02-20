Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Common
Imports Telerik.Web.UI
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports System.IO
Imports Aspose.Cells

Public Class ctrlRC_PlanYear
    Inherits Common.CommonView
    Protected WithEvents PlanYearView As ViewBase
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Private rep As New HistaffFrameworkRepository
    Dim repStore As New RecruitmentStoreProcedure

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
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
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
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.ExportTemplate)
            CType(MainToolBar.Items(0), RadToolBarButton).Text = "Tổng hợp"
            CType(MainToolBar.Items(1), RadToolBarButton).Text = "Xuất excel"
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
                    If rep.DeletePlanReg(lstDeletes) Then
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
                    If rep.UpdateStatusPlanReg(lstDeletes, RecruitmentCommon.RC_PLAN_REG_STATUS.APPROVE_ID) Then
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
                    'If rep.UpdateStatusPlanReg(lstDeletes, RecruitmentCommon.RC_PLAN_REG_STATUS.NOT_APPROVE_ID) Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    '    CurrentState = CommonMessage.STATE_NORMAL
                    '    rgData.Rebind()
                    'Else
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    'End If
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

    Public Overrides Sub BindData()
        Try
            Dim lstData As New ComboBoxDataDTO
            Using rep As New RecruitmentRepository
                'lstData.GET_RC_PLAN_YEAR = True
                'rep.GetComboList(lstData)
                'FillRadCombobox(cboYear, lstData.LIST_RC_PLAN_YEAR, "YEAR", "YEAR")
                'cboYear.SelectedValue = lstData.LIST_RC_PLAN_YEAR.Item(0).YEAR
                Dim table As New DataTable
                table.Columns.Add("YEAR", GetType(Integer))
                Dim row As DataRow
                For index = 2015 To Date.Now.Year
                    row = table.NewRow
                    row("YEAR") = index
                    table.Rows.Add(row)
                Next
                FillRadCombobox(cboYear, table, "YEAR", "YEAR")
                cboYear.SelectedValue = Date.Now.Year

            End Using
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
                    If cboYear.SelectedValue <> "" Then
                        If (repStore.SYNTHESIS_RC_YEAR_PLAN(LogHelper.GetUserLog.Username, ctrlOrg.CurrentValue, ctrlOrg.IsDissolve, cboYear.SelectedValue)) Then
                            ShowMessage(Translate("Tổng hợp thành công"), NotifyType.Success)
                        Else
                            ShowMessage(Translate("Tổng hợp thất bại"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Else
                        ShowMessage(Translate("Bạn phải chọn năm tổng hợp"), NotifyType.Warning)
                        Exit Sub
                    End If
                    rgData.Rebind()
                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData IsNot Nothing And dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "Terminate")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using
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

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False, Optional ByVal isSearch As Boolean = False) As DataTable
        Dim _filter As New PlanYearDTO
        Dim rep As New RecruitmentRepository
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                rgData.DataSource = New List(Of PlanYearDTO)
                Exit Function
            End If
            Dim iYear As Decimal = 0


            iYear = If(cboYear.SelectedValue = [String].Empty, 0, Int32.Parse(cboYear.SelectedValue))


            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue), _
                                               .IS_DISSOLVE = ctrlOrg.IsDissolve, .YEAR = iYear}
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetPlanYear(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetPlanYear(_filter, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    rgData.DataSource = rep.GetPlanYear(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts, isSearch)
                Else
                    rgData.DataSource = rep.GetPlanYear(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, , isSearch)
                End If
                rgData.VirtualItemCount = MaximumRows
            End If
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        CreateDataFilter(, True)
        rgData.DataBind()
    End Sub
End Class
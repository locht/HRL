﻿Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Common
Imports Telerik.Web.UI

Public Class ctrlPA_WorkStandard
    Inherits Common.CommonView

#Region "Property"

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarSalaryTypes
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Active, ToolbarItem.Deactive,
                                       ToolbarItem.Delete, ToolbarItem.Export)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"


        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")

        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        SelectedItemDataGridByKey(rgData, IDSelect, , rgData.CurrentPageIndex)

                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                        SelectedItemDataGridByKey(rgData, IDSelect)
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable

        Dim obj As New Work_StandardDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgData, obj)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()

            Using rep As New PayrollRepository
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetWorkStandard(rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts).ToTable()
                    Else
                        Return rep.GetWorkStandard(rgData.CurrentPageIndex, rgData.PageSize, MaximumRows).ToTable()
                    End If
                Else
                    Dim WorkStandard As New List(Of Work_StandardDTO)
                    If Sorts IsNot Nothing Then
                        WorkStandard = rep.GetWorkStandard(rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                    Else
                        WorkStandard = rep.GetWorkStandard(rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                    End If
                    rgData.VirtualItemCount = MaximumRows
                    rgData.DataSource = WorkStandard
                End If


            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Overrides Sub UpdateControlState()

        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgData, False)
                    EnableControlAll(True, rntxtYEAR, cboLabor, cboPeriod, txtRemark, txtWordStandard)
                    rntxtYEAR.Focus()
                    ctrlOrg.Enabled = True
                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgData, True)
                    EnableControlAll(False, rntxtYEAR, cboLabor, cboPeriod, txtRemark, txtWordStandard)
                    cboPeriod.Focus()
                    ctrlOrg.Enabled = False
                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgData, False)
                    'rgData.Enabled = False
                    EnableControlAll(True, rntxtYEAR, cboLabor, cboPeriod, txtRemark, txtWordStandard)
                    cboPeriod.Focus()
                    ctrlOrg.Enabled = True
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.DeleteWorkStandard(lstDeletes) Then
                            Refresh("UpdateView")
                            UpdateControlState()
                        Else
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                        End If
                    End Using

                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.ActiveWorkStandard(lstDeletes, "A") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If

                    End Using

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.ActiveWorkStandard(lstDeletes, "I") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If

                    End Using
            End Select
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub BindData()
        Dim dic As New Dictionary(Of String, Control)

        dic.Add("YEAR", rntxtYEAR)
        dic.Add("OBJECT_ID", cboLabor)
        dic.Add("PERIOD_ID", cboPeriod)
        dic.Add("REMARK", txtRemark)
        dic.Add("PERIOD_STANDARD", txtWordStandard)

        Using rep As New PayrollRepository
            FillRadCombobox(cboLabor, rep.GetOtherList("OBJECT_LABOR"), "NAME", "ID", False)

        End Using
        Utilities.OnClientRowSelectedChanged(rgData, dic)
    End Sub

#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objWorkStandardDTO As New Work_StandardDTO
        Dim gID As Decimal
        Dim orgID As String = String.Empty
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(rntxtYEAR, cboPeriod, txtRemark, txtWordStandard, cboLabor)
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
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstID As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "WorkStandard")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objWorkStandardDTO.YEAR = rntxtYEAR.Value
                        objWorkStandardDTO.OBJECT_ID = cboLabor.SelectedValue
                        objWorkStandardDTO.REMARK = txtRemark.Text
                        objWorkStandardDTO.PERIOD_ID = cboPeriod.SelectedValue
                        objWorkStandardDTO.Period_standard = Decimal.Parse(txtWordStandard.Text)
                        objWorkStandardDTO.ORG_ID = ctrlOrg.CurrentValue
                        Using rep As New PayrollRepository
                            Select Case CurrentState

                                Case CommonMessage.STATE_NEW
                                    objWorkStandardDTO.ACTFLG = "A"
                                    If rep.ValidateWorkStandard(objWorkStandardDTO) Then
                                        If rep.InsertWorkStandard(objWorkStandardDTO, gID) Then
                                            CurrentState = CommonMessage.STATE_NORMAL
                                            IDSelect = gID
                                            Refresh("InsertView")
                                            UpdateControlState()
                                        Else
                                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                        End If
                                    Else
                                        ShowMessage(Translate("Kỳ lương đã được thiết lập ngày công chế độ"), Utilities.NotifyType.Information)
                                    End If

                                Case CommonMessage.STATE_EDIT
                                    objWorkStandardDTO.ID = rgData.SelectedValue
                                    If rep.ModifyWorkStandard(objWorkStandardDTO, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = objWorkStandardDTO.ID
                                        Refresh("UpdateView")
                                        UpdateControlState()
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                            End Select
                        End Using
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If

                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    ClearControlValue(rntxtYEAR, cboLabor, cboPeriod, txtRemark, txtWordStandard)
            End Select
            UpdateControlState()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ButtonID = MessageBoxButtonType.ButtonYes AndAlso e.ActionName = CommonMessage.TOOLBARITEM_DELETE Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_ACTIVE
            UpdateControlState()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DEACTIVE
            UpdateControlState()
        End If
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub


#End Region

#Region "Custom"
    Public Function ParseBoolean(ByVal dValue As String) As Boolean
        If String.IsNullOrEmpty(dValue) Then
            Return False
        Else
            Return If(dValue = "1", True, False)
        End If
    End Function
#End Region


    Protected Sub rntxtYEAR_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rntxtYEAR.TextChanged

        Using rep As New PayrollRepository
            FillRadCombobox(cboPeriod, rep.GetPeriodbyYear(rntxtYEAR.Value), "PERIOD_NAME", "ID", False)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim WorkStandard As New List(Of Work_StandardDTO)
            WorkStandard = rep.GetWorkStandardByYear(rntxtYEAR.Value)
            rgData.DataSource = WorkStandard
        End Using


        
    End Sub
End Class
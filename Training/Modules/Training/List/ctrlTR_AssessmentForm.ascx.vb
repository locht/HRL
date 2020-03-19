﻿Imports Framework.UI
Imports Framework.UI.Utilities
Imports Training.TrainingBusiness
Imports Common
Imports Telerik.Web.UI

Public Class ctrlTR_AssessmentForm
    Inherits Common.CommonView

#Region "Property"

    Public Property AssessmentForms As List(Of AssessmentFormDTO)
        Get
            Return ViewState(Me.ID & "_AssessmentForms")
        End Get
        Set(ByVal value As List(Of AssessmentFormDTO))
            ViewState(Me.ID & "_AssessmentForms") = value
        End Set
    End Property

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

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
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        SetGridFilter(rgMain)
        rgMain.AllowCustomPaging = True
        rgMain.PageSize = Common.Common.DefaultPageSize
        'rgMain.ClientSettings.EnablePostBackOnRowClick = True
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Seperator,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete, ToolbarItem.Export)
            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
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
                        rgMain.Rebind()

                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                    Case "Cancel"
                        rgMain.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New TrainingRepository
        Dim _filter As New AssessmentFormDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetAssessmentForm(_filter, Sorts).ToTable()
                Else
                    Return rep.GetAssessmentForm(_filter).ToTable()
                End If
            Else

                If Sorts IsNot Nothing Then
                    Me.AssessmentForms = rep.GetAssessmentForm(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
                Else
                    Me.AssessmentForms = rep.GetAssessmentForm(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
                End If

                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = Me.AssessmentForms
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Overrides Sub UpdateControlState()
        Dim rep As New TrainingRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgMain, False)
                    Utilities.EnableRadCombo(cboRateType, True)
                    txtName.ReadOnly = False
                    txtRemark.ReadOnly = False

                    cboRateType.SelectedValue = "7703"

                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgMain, True)
                    Utilities.EnableRadCombo(cboRateType, False)
                    txtName.ReadOnly = True
                    txtRemark.ReadOnly = True

                    txtName.Text = ""
                    txtRemark.Text = ""

                Case CommonMessage.STATE_EDIT

                    EnabledGridNotPostback(rgMain, False)
                    Utilities.EnableRadCombo(cboRateType, True)
                    txtName.ReadOnly = False
                    txtRemark.ReadOnly = False

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of AssessmentFormDTO)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(New AssessmentFormDTO With {.ID = item.GetDataKeyValue("ID")})
                    Next
                    If rep.DeleteAssessmentForm(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            txtName.Focus()
            UpdateToolbarState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            Using rep As New TrainingRepository
                Dim dtData As New DataTable
                dtData = rep.GetTrRateCombo(False)
                FillRadCombobox(cboRateType, dtData, "NAME_VN", "ID", False)

            End Using
        Catch ex As Exception

        End Try

        Dim dic As New Dictionary(Of String, Control)
        dic.Add("NAME", txtName)
        dic.Add("REMARK", txtRemark)
        dic.Add("RATE_TYPE", cboRateType)
        Utilities.OnClientRowSelectedChanged(rgMain, dic)
    End Sub

#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objAssessmentForm As New AssessmentFormDTO
        Dim rep As New TrainingRepository
        Dim gID As Decimal
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    Utilities.EnableRadCombo(cboRateType, True)
                    txtName.Text = ""
                    txtRemark.Text = ""
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    Utilities.EnableRadCombo(cboRateType, True)
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgMain.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT

                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_DELETE
                    Utilities.EnableRadCombo(cboRateType, False)
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtData, "AssessmentForm")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        With objAssessmentForm
                            objAssessmentForm.NAME = txtName.Text
                            objAssessmentForm.REMARK = txtRemark.Text
                            objAssessmentForm.RATE_TYPE = cboRateType.SelectedValue
                        End With
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertAssessmentForm(objAssessmentForm, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                    ClearControlValue(cboRateType, txtName, txtRemark)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objAssessmentForm.ID = rgMain.SelectedValue
                                If rep.ModifyAssessmentForm(objAssessmentForm, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objAssessmentForm.ID
                                    Refresh("UpdateView")
                                    ClearControlValue(cboRateType, txtName, txtRemark)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_ACTIVE
            UpdateControlState()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DEACTIVE
            UpdateControlState()
        End If
        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

#End Region

#Region "Custom"
    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

End Class
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Common
Imports Telerik.Web.UI

Public Class ctrlPA_SalaryRank
    Inherits Common.CommonView

#Region "Property"

    Public Property SalaryRanks As List(Of SalaryRankDTO)
        Get
            Return ViewState(Me.ID & "_SalaryRanks")
        End Get
        Set(ByVal value As List(Of SalaryRankDTO))
            ViewState(Me.ID & "_SalaryRanks") = value
        End Set
    End Property

    Property DeleteSalaryRanks As List(Of SalaryRankDTO)
        Get
            Return ViewState(Me.ID & "_DeleteSalaryRanks")
        End Get
        Set(ByVal value As List(Of SalaryRankDTO))
            ViewState(Me.ID & "_DeleteSalaryRanks") = value
        End Set
    End Property

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
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgData.SetFilter()
            rgData.AllowCustomPaging = True

            'rgData.ClientSettings.EnablePostBackOnRowClick = True
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarSalaryRanks
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
                        SelectedItemDataGridByKey(rgData, IDSelect, )
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable

        Dim obj As New SalaryRankDTO
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgData, obj)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            If cboSalaryGroup.SelectedValue <> "" Then
                obj.SAL_GROUP_ID = cboSalaryGroup.SelectedValue
            End If
            Using rep As New PayrollRepository
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetSalaryRank(obj, Sorts).ToTable
                    Else
                        Return rep.GetSalaryRank(obj).ToTable
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        Me.SalaryRanks = rep.GetSalaryRank(obj, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                    Else
                        Me.SalaryRanks = rep.GetSalaryRank(obj, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                    End If
                    rgData.VirtualItemCount = MaximumRows
                    rgData.DataSource = Me.SalaryRanks
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

                    'cboSalaryLevel.SelectedValue = Nothing
                    txtRank.Text = 0
                    rntxtSalaryBasic.Value = 0
                    txtRemark.Text = ""

                    rntxtSalaryBasic.ReadOnly = False
                    txtRank.ReadOnly = False
                    txtRemark.ReadOnly = False
                    Utilities.EnableRadCombo(cboSalaryLevel, True)

                Case CommonMessage.STATE_NORMAL

                    txtRank.Text = 0
                    rntxtSalaryBasic.Value = 0
                    txtRemark.Text = ""

                    EnabledGridNotPostback(rgData, True)
                    rntxtSalaryBasic.ReadOnly = True
                    txtRank.ReadOnly = True
                    txtRemark.ReadOnly = True
                    Utilities.EnableRadCombo(cboSalaryLevel, False)
                Case CommonMessage.STATE_EDIT

                    EnabledGridNotPostback(rgData, False)

                    rntxtSalaryBasic.ReadOnly = False
                    txtRank.ReadOnly = False
                    txtRemark.ReadOnly = False
                    Utilities.EnableRadCombo(cboSalaryLevel, True)

                Case CommonMessage.STATE_DELETE
                    Using rep As New PayrollRepository
                        If rep.DeleteSalaryRank(DeleteSalaryRanks) Then
                            DeleteSalaryRanks = Nothing
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
                        If rep.ActiveSalaryRank(lstDeletes, "A", cboSalaryLevel.SelectedValue) Then
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
                        If rep.ActiveSalaryRank(lstDeletes, "I", cboSalaryLevel.SelectedValue) Then
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

        dic.Add("SAL_LEVEL_ID", cboSalaryLevel)
        dic.Add("RANK", txtRank)
        dic.Add("SALARY_BASIC", rntxtSalaryBasic)
        dic.Add("REMARK", txtRemark)
        Utilities.OnClientRowSelectedChanged(rgData, dic)

        Try
            Dim rep As New PayrollRepository
            Dim comboBoxDataDTO As New ComboBoxDataDTO

            comboBoxDataDTO.GET_SALARY_GROUP = True
            rep.GetComboboxData(comboBoxDataDTO)

            FillDropDownList(cboSalaryGroup, comboBoxDataDTO.LIST_SALARY_GROUP, "NAME", "ID", Common.Common.SystemLanguage, False)
            Dim SalaryGroupEffect As SalaryGroupDTO = rep.GetEffectSalaryGroup()
            If cboSalaryGroup.Items.Count > 0 And SalaryGroupEffect IsNot Nothing Then
                cboSalaryGroup.SelectedValue = SalaryGroupEffect.ID
                cboSalaryGroup_SelectedIndexChanged(Nothing, Nothing)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objSalaryRank As New SalaryRankDTO
        Dim gID As Decimal

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

                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lstDeletes As New List(Of SalaryRankDTO)
                    Dim lstID As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(New SalaryRankDTO With {.ID = Decimal.Parse(item("ID").Text),
                                                               .SAL_LEVEL_ID = cboSalaryLevel.SelectedValue})
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next

                    Using rep As New PayrollRepository
                        If Not rep.CheckExistInDatabase(lstID, TABLE_NAME.PA_SALARY_RANK) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                            Return
                        End If
                    End Using

                    DeleteSalaryRanks = lstDeletes
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "SalaryRank")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If cboSalaryLevel.SelectedValue = "" Or cboSalaryLevel.SelectedValue Is Nothing Then
                            ShowMessage("Bạn chưa chọn ngạch công chức", NotifyType.Warning)
                            Exit Sub
                        End If
                        objSalaryRank.SAL_LEVEL_ID = cboSalaryLevel.SelectedValue
                        objSalaryRank.RANK = txtRank.Text
                        objSalaryRank.SALARY_BASIC = rntxtSalaryBasic.Value
                        objSalaryRank.REMARK = txtRemark.Text
                        Using rep As New PayrollRepository
                            Select Case CurrentState
                                Case CommonMessage.STATE_NEW
                                    objSalaryRank.ACTFLG = "A"
                                    If rep.InsertSalaryRank(objSalaryRank, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = gID
                                        Refresh("InsertView")
                                        UpdateControlState()
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                                Case CommonMessage.STATE_EDIT
                                    objSalaryRank.ID = rgData.SelectedValue
                                    If rep.ModifySalaryRank(objSalaryRank, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = objSalaryRank.ID
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
                    Refresh("Cancel")
                    UpdateControlState()
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ButtonID = MessageBoxButtonType.ButtonYes And e.ActionName = CommonMessage.TOOLBARITEM_DELETE Then
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

    Private Sub cboSalaryGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboSalaryGroup.SelectedIndexChanged
        Try
            If cboSalaryGroup.SelectedValue = "" Then
                ShowMessage(Translate("Bạn chưa chọn thang bảng lương"), Utilities.NotifyType.Warning)
                cboSalaryLevel.Items.Clear()
                cboSalaryLevel.ClearCheckedItems()
                cboSalaryLevel.ClearSelection()
                cboSalaryLevel.Text = ""
                Return
            End If
            Dim obj As New SalaryLevelDTO
            Dim MaximumRows As Integer
            obj.SAL_GROUP_ID = cboSalaryGroup.SelectedValue
            obj.ACTFLG_DB = "A"
            Using rep As New PayrollRepository
                Dim SalaryLevelByGroup = rep.GetSalaryLevel(obj, 0, Integer.MaxValue, MaximumRows)
                FillDropDownList(cboSalaryLevel, SalaryLevelByGroup, "NAME", "ID", Common.Common.SystemLanguage, False)
                rgData.Rebind()
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

#End Region

End Class
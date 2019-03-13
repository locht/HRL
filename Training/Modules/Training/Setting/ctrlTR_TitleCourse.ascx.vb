Imports Framework.UI
Imports Framework.UI.Utilities
Imports Training.TrainingBusiness
Imports Common
Imports Telerik.Web.UI

Public Class ctrlTR_TitleCourse
    Inherits Common.CommonView
    Protected WithEvents ExamsDtlView As ViewBase

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

    Public Overrides Sub BindData()
        Try
            GetDataCombo()
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("ID", hidID)
            dic.Add("TR_COURSE_CODE", cboCourse)
            dic.Add("TR_COURSE_NAME", cboCourse)
            dic.Add("TR_COURSE_REMARK", cboCourse)
            Utilities.OnClientRowSelectedChanged(rgData, dic)
            EnableRadCombo(cboCourse, False)
            rgData.DataSource = Nothing
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Seperator,
                                       ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Delete)
            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New TrainingRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    EnableRadCombo(cboTitle, True)
                    EnableRadCombo(cboCourse, True)
                    EnabledGridNotPostback(rgData, True)
                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    EnableRadCombo(cboTitle, False)
                    EnableRadCombo(cboCourse, True)
                    EnabledGridNotPostback(rgData, False)
                Case CommonMessage.STATE_DELETE
                    Dim objDelete As TitleCourseDTO
                    Dim item As GridDataItem = rgData.SelectedItems(0)
                    objDelete = New TitleCourseDTO With {.ID = item.GetDataKeyValue("ID")}
                    If rep.DeleteTitleCourse(objDelete) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
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

    Private Sub GetDataCombo()
        Dim rep As New TrainingRepository
        Dim dtData As DataTable
        Dim dtCourse As DataTable
        Try
            dtData = rep.GetTitleByList()
            FillRadCombobox(cboTitle, dtData, "NAME", "ID")

            dtCourse = rep.GetCourseByList()
            FillRadCombobox(cboCourse, dtCourse, "NAME", "ID")
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New TrainingRepository

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    If cboTitle.SelectedValue = "" Then
                        ShowMessage(Translate("Bạn phải chọn Chức danh"), Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_NEW
                    cboCourse.SelectedValue = Nothing
                    hidID.Value = ""
                Case CommonMessage.TOOLBARITEM_EDIT
                    CurrentState = CommonMessage.STATE_EDIT
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim obj As New TitleCourseDTO
                        If hidID.Value <> "" Then
                            obj.ID = Decimal.Parse(hidID.Value)
                        End If
                        obj.TR_COURSE_ID = cboCourse.SelectedValue
                        obj.HU_TITLE_ID = cboTitle.SelectedValue
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW

                            Case CommonMessage.STATE_EDIT
                                obj.ID = rgData.SelectedValue
                                obj.ACTFLG = "A"
                        End Select
                        If rep.UpdateTitleCourse(obj) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            Refresh("UpdateView")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                    Else
                        'ExcuteScript("Resize", "ResizeSplitter()")
                    End If
                Case CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    cboCourse.SelectedValue = Nothing
            End Select
            rep.Dispose()
            UpdateControlState()
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
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    'Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged

    '    Dim dtData As DataTable
    '    Try
    '        'Using rep As New TrainingRepository
    '        '    hidOrgID.Value = ""
    '        '    lblOrgName.Text = ""
    '        '    cboTitle.Items.Clear()
    '        '    cboTitle.ClearSelection()
    '        '    cboTitle.Text = ""
    '        '    If ctrlOrg.CurrentValue <> "" Then
    '        '        hidOrgID.Value = ctrlOrg.CurrentValue
    '        '        lblOrgName.Text = ctrlOrg.CurrentText
    '        '        dtData = rep.GetTitleByOrgList(Decimal.Parse(ctrlOrg.CurrentValue), False)
    '        '        FillRadCombobox(cboTitle, dtData, "NAME", "ID")
    '        '    End If
    '        '    rgData.Rebind()
    '        'End Using
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub cboTitle_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTitle.SelectedIndexChanged
        Try
            'ClearControlValue(cboCourse)
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub

#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New TitleCourseDTO
        Dim rep As New TrainingRepository
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of TitleCourseDTO)
            If cboTitle.SelectedValue <> "" Then
                _filter.HU_TITLE_ID = cboTitle.SelectedValue
            Else
                _filter.HU_TITLE_ID = 0
            End If
            If Sorts IsNot Nothing Then
                lstData = rep.GetTitleCourse(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
            Else
                lstData = rep.GetTitleCourse(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
            End If

            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = lstData
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    Protected Sub rgData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgData.SelectedIndexChanged
        Try
            'If rgData.SelectedItems.Count = 0 Then Exit Sub

            'Dim item As GridDataItem = CType(rgData.SelectedItems(0), GridDataItem)
            'If item.GetDataKeyValue("TR_TRAIN_FIELD_ID") IsNot Nothing Then
            '    cboCourse.SelectedValue = CDec(item.GetDataKeyValue("TR_COURSE_NAME"))
            'Else
            '    cboCourse.SelectedValue = Nothing
            '    cboCourse.ClearSelection()
            '    cboCourse.Items.Clear()
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
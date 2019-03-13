Imports Framework.UI
Imports Framework.UI.Utilities
Imports Training.TrainingBusiness
Imports Common
Imports Telerik.Web.UI
Imports System.IO

Public Class ctrlTR_ProgramClassStudent
    Inherits CommonView

    Public Overrides Property MustAuthorize As Boolean = False
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup

#Region "Property"

    Public Property SelectedItemCanNotSchedule As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemCanNotSchedule") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemCanNotSchedule") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemCanNotSchedule")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemCanNotSchedule") = value
        End Set
    End Property

    Public Property SelectedItemCanSchedule As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemCanSchedule") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemCanSchedule") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemCanSchedule")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemCanSchedule") = value
        End Set
    End Property


    Public Property lstDataCanNotSchedule As List(Of ProgramClassStudentDTO)
        Get
            If ViewState(Me.ID & "_lstDataCanNotSchedule") Is Nothing Then
                ViewState(Me.ID & "_lstDataCanNotSchedule") = New List(Of ProgramClassStudentDTO)
            End If
            Return ViewState(Me.ID & "_lstDataCanNotSchedule")
        End Get
        Set(ByVal value As List(Of ProgramClassStudentDTO))
            ViewState(Me.ID & "_lstDataCanNotSchedule") = value
        End Set
    End Property
    '0 - normal
    '1 - Employee
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            'rgCanNotSchedule.PageSize = 50
            rgCanNotSchedule.AllowCustomPaging = True
            ' rgCanSchedule.PageSize = 50
            rgCanSchedule.AllowCustomPaging = True
            hidClassID.Value = Request.Params("CLASS_ID")
            UpdateControlState()
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgCanNotSchedule.AllowCustomPaging = True
            rgCanNotSchedule.SetFilter()
            rgCanSchedule.AllowCustomPaging = True
            rgCanSchedule.SetFilter()
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            'Dim dtData
            'Using rep As New TrainingRepository
            '    dtData = rep.GetHuContractTypeList(False)
            '    FillCheckBoxList(lstContractType, dtData, "NAME", "ID")
            'End Using
            chkIsPlan.Checked = True
            chkIsPlan_CheckedChanged(Nothing, Nothing)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Cancel)
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try

            If Not IsPostBack Then
                Using rep As New TrainingRepository
                    Dim obj = rep.GetClassByID(New ProgramClassDTO With {.ID = Decimal.Parse(hidClassID.Value)})
                    txtName.Text = obj.NAME
                    rdStartDate.SelectedDate = obj.START_DATE
                    rdEndDate.SelectedDate = obj.END_DATE
                End Using
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        phFindOrg.Controls.Clear()
        Select Case isLoadPopup
            Case 1
                ctrlOrgPopup = Me.Register("ctrlOrgPopup", "Common", "ctrlFindOrgPopup")
                phFindOrg.Controls.Add(ctrlOrgPopup)

        End Select

        Using rep As New TrainingRepository
            Dim lst As New List(Of ProgramClassStudentDTO)

            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    For Each studentID In SelectedItemCanNotSchedule
                        Dim obj As New ProgramClassStudentDTO
                        obj.EMPLOYEE_ID = studentID
                        obj.TR_CLASS_ID = Decimal.Parse(hidClassID.Value)
                        lst.Add(obj)
                    Next

                    If rep.InsertClassStudent(lst) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        SelectedItemCanNotSchedule = Nothing
                        SelectedItemCanSchedule = Nothing
                        rgCanNotSchedule.Rebind()
                        rgCanSchedule.Rebind()
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If

                Case CommonMessage.STATE_DELETE
                    For Each studentID In SelectedItemCanSchedule
                        Dim obj As New ProgramClassStudentDTO
                        obj.EMPLOYEE_ID = studentID
                        obj.TR_CLASS_ID = Decimal.Parse(hidClassID.Value)
                        lst.Add(obj)
                    Next

                    If rep.DeleteClassStudent(lst) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        SelectedItemCanNotSchedule = Nothing
                        SelectedItemCanSchedule = Nothing
                        rgCanNotSchedule.Rebind()
                        rgCanSchedule.Rebind()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select

        End Using
    End Sub

#End Region

#Region "Event"

    Private Sub tbarMain_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles tbarMain.ButtonClick
        Dim rep As New TrainingRepository
        Dim _error As Integer = 0
        Dim sType As Object = Nothing
        Try

        Catch ex As Exception
            ShowMessage(ex.Message, Framework.UI.Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub rgCanNotSchedule_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgCanNotSchedule.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim id = datarow.GetDataKeyValue("EMPLOYEE_ID")
            If SelectedItemCanNotSchedule IsNot Nothing AndAlso SelectedItemCanNotSchedule.Contains(id) Then
                datarow.Selected = True
            End If
        End If
    End Sub

    Private Sub rgCanNotSchedule_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgCanNotSchedule.PageIndexChanged
        GetCanNotScheduleSelected()
    End Sub

    Private Sub rgCanNotSchedule_PageSizeChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles rgCanNotSchedule.PageSizeChanged
        GetCanNotScheduleSelected()
    End Sub

    Private Sub rgCanNotSchedule_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles rgCanNotSchedule.SortCommand
        GetCanNotScheduleSelected()
    End Sub

    Private Sub rgCanSchedule_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgCanSchedule.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim id = datarow.GetDataKeyValue("EMPLOYEE_ID")
            If SelectedItemCanSchedule IsNot Nothing AndAlso SelectedItemCanSchedule.Contains(id) Then
                datarow.Selected = True
            End If
        End If
    End Sub

    Private Sub rgCanSchedule_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgCanSchedule.PageIndexChanged
        GetCanScheduleSelected()
    End Sub

    Private Sub rgCanSchedule_PageSizeChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles rgCanSchedule.PageSizeChanged
        GetCanScheduleSelected()
    End Sub

    Private Sub rgCanSchedule_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles rgCanSchedule.SortCommand
        GetCanScheduleSelected()
    End Sub

    Private Sub rgCanNotSchedule_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCanNotSchedule.NeedDataSource
        CreateDataFilterCanNotSchedule()
    End Sub

    Private Sub rgCanSchedule_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCanSchedule.NeedDataSource
        CreateDataFilterStudent()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        GetCanScheduleSelected()
        ctrlMessageBox.MessageText = "Bạn có chắc chắn xóa " & SelectedItemCanSchedule.Count & " học viên?"
        ctrlMessageBox.ActionName = "DELETE"
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()

    End Sub

    Private Sub btnInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        If Page.IsValid Then
            GetCanNotScheduleSelected()
            ctrlMessageBox.MessageText = "Bạn có chắc chắn chuyển " & SelectedItemCanNotSchedule.Count & " nhân viên thành học viên?"
            ctrlMessageBox.ActionName = "INSERT"
            ctrlMessageBox.DataBind()
            ctrlMessageBox.Show()
        End If
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = "INSERT" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_NEW
                UpdateControlState()
            End If
            If e.ActionName = "DELETE" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub btnChoose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChoose.Click
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlOrgPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlOrgPopup.OrganizationSelected
        Try
            Select Case isLoadPopup
                Case 1
                    Dim orgItem = ctrlOrgPopup.CurrentItemDataObject
                    If orgItem IsNot Nothing Then
                        hidOrg.Value = e.CurrentValue
                        txtOrgName.Text = orgItem.name_vn
                        txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.description_path)
                    End If
                    PopulatingListTitle()
            End Select
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrgPopup.CancelClicked
        isLoadPopup = 0
    End Sub


    Private Sub chkIsPlan_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIsPlan.CheckedChanged
        Try
            btnChoose.Enabled = Not chkIsPlan.Checked
            'lstContractType.Enabled = Not chkIsPlan.Checked
            'lstPositions.Enabled = Not chkIsPlan.Checked
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            rgCanNotSchedule.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)

        End Try
    End Sub

#End Region

#Region "Custom"

    Private Sub CreateDataFilterStudent()

        Try
            Dim rep As New TrainingRepository
            rgCanSchedule.VirtualItemCount = 0
            rgCanSchedule.DataSource = New List(Of ProgramClassStudentDTO)

            Dim _filter As New ProgramClassStudentDTO
            _filter.TR_CLASS_ID = Decimal.Parse(hidClassID.Value)
            SetValueObjectByRadGrid(rgCanSchedule, _filter)
            Dim Sorts As String = rgCanSchedule.MasterTableView.SortExpressions.GetSortString()
            Dim MaximumRows As Decimal = 0
            Dim lstData As List(Of ProgramClassStudentDTO)
            If Sorts IsNot Nothing Then
                lstData = rep.GetEmployeeByClassID(_filter,
                                                       rgCanSchedule.CurrentPageIndex, _
                                                       rgCanSchedule.PageSize, _
                                                       MaximumRows, _
                                                       Sorts)
            Else

                lstData = rep.GetEmployeeByClassID(_filter,
                                                       rgCanSchedule.CurrentPageIndex, _
                                                       rgCanSchedule.PageSize, _
                                                       MaximumRows)
            End If
            rgCanSchedule.VirtualItemCount = MaximumRows
            rgCanSchedule.DataSource = lstData
            'If Sorts IsNot Nothing Then
            '    rgCanSchedule.DataSource = rep.GetEmployeeByClassID(_filter, rgCanSchedule.CurrentPageIndex, _
            '                                                    rgCanSchedule.PageSize, _
            '                                                    MaximumRows, _
            '                                                    Sorts)
            'Else

            '    rgCanSchedule.DataSource = rep.GetEmployeeByClassID(_filter, rgCanSchedule.CurrentPageIndex, _
            '                                                    rgCanSchedule.PageSize, _
            '                                                    MaximumRows)
            'End If
            ''rgCanNotSchedule.VirtualItemCount = MaximumRows
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub CreateDataFilterCanNotSchedule()

        Try
            Dim rep As New TrainingRepository
            Dim _filter As New ProgramClassStudentDTO
            rgCanNotSchedule.VirtualItemCount = 0
            rgCanNotSchedule.DataSource = New List(Of ProgramClassStudentDTO)
            SetValueObjectByRadGrid(rgCanNotSchedule, _filter)
            If Not chkIsPlan.Checked Then
                If hidOrg.Value <> "" Then
                    _filter.ORG_ID = Decimal.Parse(hidOrg.Value)
                End If
                'If lstContractType.CheckedItems.Count > 0 Then
                '    _filter.lstContractType = New List(Of Decimal)
                '    For Each item In lstContractType.CheckedItems
                '        _filter.lstContractType.Add(Decimal.Parse(item.Value))
                '    Next
                'End If

                'If lstPositions.CheckedItems.Count > 0 Then
                '    _filter.lstTitle = New List(Of Decimal)
                '    For Each item In lstPositions.CheckedItems
                '        _filter.lstTitle.Add(Decimal.Parse(item.Value))
                '    Next
                'End If
                _filter.IS_PLAN = chkIsPlan.Checked
            Else
                _filter.IS_PLAN = chkIsPlan.Checked
            End If

            _filter.TR_CLASS_ID = Decimal.Parse(hidClassID.Value)
            Dim Sorts As String = rgCanNotSchedule.MasterTableView.SortExpressions.GetSortString()
            Dim MaximumRows As Decimal = 0

            If Sorts IsNot Nothing Then
                lstDataCanNotSchedule = rep.GetEmployeeNotByClassID(_filter, rgCanNotSchedule.CurrentPageIndex, _
                                                            rgCanNotSchedule.PageSize, _
                                                            MaximumRows, _
                                                            Sorts)
            Else
                lstDataCanNotSchedule = rep.GetEmployeeNotByClassID(_filter, rgCanNotSchedule.CurrentPageIndex, _
                                                            rgCanNotSchedule.PageSize, _
                                                            MaximumRows)
            End If
            rgCanNotSchedule.VirtualItemCount = MaximumRows
            rgCanNotSchedule.DataSource = lstDataCanNotSchedule
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub GetCanNotScheduleSelected()
        For Each dr As Telerik.Web.UI.GridDataItem In rgCanNotSchedule.Items
            Dim id As String = dr.GetDataKeyValue("EMPLOYEE_ID")
            If dr.Selected Then
                If Not SelectedItemCanNotSchedule.Contains(id) Then SelectedItemCanNotSchedule.Add(id)
            Else
                If SelectedItemCanNotSchedule.Contains(id) Then SelectedItemCanNotSchedule.Remove(id)
            End If
        Next
    End Sub

    Private Sub GetCanScheduleSelected()
        For Each dr As Telerik.Web.UI.GridDataItem In rgCanSchedule.Items
            Dim id As String = dr.GetDataKeyValue("EMPLOYEE_ID")
            If dr.Selected Then
                If Not SelectedItemCanSchedule.Contains(id) Then SelectedItemCanSchedule.Add(id)
            Else
                If SelectedItemCanSchedule.Contains(id) Then SelectedItemCanSchedule.Remove(id)
            End If
        Next
    End Sub

    Private Sub PopulatingListTitle()
        Dim lstOrgIds = New List(Of Decimal)
        If hidOrg.Value <> "" Then
            lstOrgIds.Add(hidOrg.Value)
        End If
        'Using rep As New TrainingBusinessClient
        '    lstPositions.Items.Clear()
        '    Dim titles = rep.GetTitlesByOrgs(lstOrgIds, Common.Common.SystemLanguage.Name)
        '    For Each item In titles
        '        lstPositions.Items.Add(New RadListBoxItem(item.NAME, item.ID))
        '    Next
        'End Using
    End Sub


#End Region

End Class
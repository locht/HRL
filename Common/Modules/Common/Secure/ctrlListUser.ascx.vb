Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common.CommonBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Imports System.IO



Public Class ctrlListUser
    Inherits CommonView
    Protected WithEvents UserView As ViewBase
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Common/Secure/" + Me.GetType().Name.ToString()


#Region "Property"
    Property User As UserDTO
        Get
            Return PageViewState(Me.ID & "_User")
        End Get
        Set(ByVal value As UserDTO)
            PageViewState(Me.ID & "_User") = value
        End Set
    End Property

    Public Property ListUsers As List(Of UserDTO)
        Get
            Return PageViewState(Me.ID & "_ListUsers")
        End Get
        Set(ByVal value As List(Of UserDTO))
            PageViewState(Me.ID & "_ListUsers") = value
        End Set
    End Property

    Public Property ListGroupCombo As List(Of GroupDTO)
        Get
            Return PageViewState(Me.ID & "_ListGroupCombo")
        End Get
        Set(ByVal value As List(Of GroupDTO))
            PageViewState(Me.ID & "_ListGroupCombo") = value
        End Set
    End Property

    Public Property DeleteItemList As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_DeleteItemList")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_DeleteItemList") = value
        End Set
    End Property


    Public Property SelectedID As Decimal
        Get
            Return PageViewState(Me.ID & "_SelectedID")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_SelectedID") = value
        End Set
    End Property
#End Region

#Region "Page"
    ''' <summary>
    ''' Khởi tạo, load page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgGrid.SetFilter()
            rgGrid.AllowCustomPaging = True
            rgGrid.ClientSettings.EnablePostBackOnRowClick = True
            UpdatePageViewState()
            
            _mylog.WriteLog(_mylog._info, _classPath, method,
                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Khởi tạo, load menu
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = rtbMain
            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Seperator, _
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Seperator,
                                       ToolbarItem.Lock,
                                       ToolbarItem.Unlock,
                                       ToolbarItem.Seperator,
                                       ToolbarItem.Export)
            CType(rtbMain.Items(3), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"

            UserView = Me.Register("ctrlListUserNewEdit", "Common", "ctrlListUserNewEdit", "Secure")
            ViewPlaceHolder.Controls.Add(UserView)
            UserView.DataBind()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' ResizeSpliter về trạng thái ban đầu
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub ReSize()
        ExcuteScript("Resize", "ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize)")
    End Sub
    ''' <summary>
    ''' ResizeSpliter khi show validate
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub ReSizeSpliter()
        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgGrid')")
    End Sub

    ''' <summary>
    ''' Load, Reload grid
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim MaximumRows As Integer
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim rep As New CommonRepository
            If Message = CommonMessage.ACTION_UPDATED Then
                Dim Sorts As String = rgGrid.MasterTableView.SortExpressions.GetSortString()
                Dim _filter As New UserDTO
                SetValueObjectByRadGrid(rgGrid, _filter)
                If Sorts IsNot Nothing Then
                    Me.ListUsers = rep.GetUserList(_filter, rgGrid.CurrentPageIndex, rgGrid.PageSize, MaximumRows, Sorts)
                Else
                    Me.ListUsers = rep.GetUserList(_filter, rgGrid.CurrentPageIndex, rgGrid.PageSize, MaximumRows)
                End If
                'Đưa dữ liệu vào Grid
                rgGrid.VirtualItemCount = MaximumRows
                rgGrid.DataSource = Me.ListUsers
                rgGrid.MasterTableView.DataSource = Me.ListUsers

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Set trạng thái control theo trạng thái page
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    'Utilities.EnabledGrid(rgGrid, False)
                    rgGrid.Enabled = False
                    'rgGrid.ClientSettings.Selecting.AllowRowSelect = False
                Case CommonMessage.STATE_NORMAL, ""
                    Utilities.EnabledGrid(rgGrid, True)
            End Select
            ChangeToolbarState()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' set trạng thái page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Protected Sub UpdatePageViewState(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If
            UpdateControlState()
            UserView.CurrentState = CurrentState
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    UserView.SetProperty("User", User)
                Case CommonMessage.STATE_EDIT
                    UserView.SetProperty("User", User)
            End Select
            'If Message = CommonMessage.ACTION_UPDATED Then
            '    UserView.UpdateControlState()
            '    UserView.Refresh()
            'End If
            'If Message = CommonMessage.ACTION_CANCEL Then
            '    User = Nothing
            '    UserView.Refresh()
            '    rgGrid.SelectedIndexes.Clear()
            'End If
            If Message = CommonMessage.ACTION_UPDATED Or Message = CommonMessage.ACTION_SAVED Then
                UserView.UpdateControlState()
                UserView.Refresh(Message)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <summary>
    ''' Event click item trên menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdatePageViewState(CommonMessage.ACTION_UPDATED)
                    rgGrid.SelectedIndexes.Clear()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgGrid.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    'User = (From p In ListUsers Where p.ID = rgGrid.SelectedValue).SingleOrDefault
                    UpdatePageViewState(CommonMessage.ACTION_UPDATED)
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    DeleteItemList = RepareDataForDelete()
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.MessageTitle = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE_TITLE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DELETED
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_LOCK
                    If rgGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    DeleteItemList = RepareDataForDelete()
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_LOCK)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_LOCKED
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_UNLOCK
                    If rgGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    DeleteItemList = RepareDataForDelete()
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_UNLOCK)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_UNLOCKED
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_RESET
                    If rgGrid.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    DeleteItemList = RepareDataForDelete()
                    ctrlMessageBox.MessageText = Translate("Bạn có muốn Reset lại mật khẩu?")
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_RESET
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_SYNC
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_SYNC)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_SYNC
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    UpdatePageViewState(CommonMessage.ACTION_CANCEL)
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim rep As New CommonRepository
                    Dim dtData As DataTable
                    Dim Sorts As String = rgGrid.MasterTableView.SortExpressions.GetSortString()
                    Dim _filter As New UserDTO
                    SetValueObjectByRadGrid(rgGrid, _filter)
                    If Sorts IsNot Nothing Then
                        dtData = rep.GetUserList(_filter, Sorts).ToTable()
                    Else
                        dtData = rep.GetUserList(_filter).ToTable()
                    End If

                    Using xls As New ExcelCommon
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgGrid.ExportExcel(Server, Response, dtData, "Title")
                        End If
                    End Using
            End Select
            CType(UserView, ctrlListUserNewEdit).OnToolbar_Command(sender, e)
            _mylog.WriteLog(_mylog._info, _classPath, method,
                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load grid theo trạng thái page
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub ctrlUser_OnReceiveData(ByVal sender As IViewListener(Of ViewBase), ByVal e As ViewCommunicationEventArgs) Handles Me.OnReceiveData
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.FromViewID = "ctrlListUserNewEdit" Then
                If CurrentState = CommonMessage.STATE_NEW Then
                    SelectedID = 0
                    rgGrid.MasterTableView.SortExpressions.AddSortExpression("CREATED_DATE DESC")
                End If
                CurrentState = CommonMessage.STATE_NORMAL
                If e.EventData = CommonMessage.ACTION_SUCCESS Then
                    rgGrid.Rebind()
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    rgGrid_SelectedIndexChanged(Nothing, Nothing)
                    'Exit Sub
                ElseIf e.EventData = CommonMessage.ACTION_UNSUCCESS Then
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                ElseIf e.EventData = CommonMessage.MESSAGE_WARNING_EXIST_DATABASE Then
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                End If
                UpdatePageViewState(CommonMessage.ACTION_UPDATED)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event Yes/No hỏi xóa, khóa tài khoản, mở khóa tài khoản, Reset
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New CommonRepository
        Dim _error As String = ""
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Select Case e.ActionName
                    Case CommonMessage.ACTION_DELETED
                        If rep.DeleteUser(DeleteItemList, _error) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgGrid.Rebind()
                            ClearControlValue(txtResult)
                            
                            UserView.SetProperty("User", Nothing)
                            UserView.Refresh()
                            rgGrid.SelectedIndexes.Clear()
                        Else
                            ShowMessage(Translate(_error), NotifyType.Warning)
                        End If
                    Case CommonMessage.ACTION_LOCKED
                        If rep.UpdateUserListStatus(DeleteItemList, "I") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgGrid.Rebind()
                            UserView.SetProperty("User", Nothing)
                            UserView.Refresh()
                            rgGrid.SelectedIndexes.Clear()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If
                    Case CommonMessage.ACTION_UNLOCKED
                        If rep.UpdateUserListStatus(DeleteItemList, "A") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgGrid.Rebind()
                            UserView.SetProperty("User", Nothing)
                            UserView.Refresh()
                            rgGrid.SelectedIndexes.Clear()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If
                    Case CommonMessage.ACTION_RESET
                        If rep.ResetUserPassword(DeleteItemList,
                                                 CommonConfig.PasswordLength,
                                                 CommonConfig.PasswordLowerChar,
                                                 CommonConfig.PasswordUpperChar,
                                                 CommonConfig.PasswordNumberChar,
                                                 CommonConfig.PasswordSpecialChar) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgGrid.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If
                    Case CommonMessage.ACTION_SYNC
                        Dim _lstNew As String = ""
                        Dim _lstModify As String = ""
                        Dim _lstDelete As String = ""
                        If rep.SyncUserList(_lstNew, _lstModify, _lstDelete) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            txtResult.Text = "Các tài khoản thêm mới: " & vbNewLine
                            txtResult.Text &= _lstNew
                            txtResult.Text &= vbNewLine & vbNewLine & "Các tài khoản thay đổi thông tin: " & vbNewLine
                            txtResult.Text &= _lstModify
                            txtResult.Text &= vbNewLine & vbNewLine & "Các tài khoản bị khóa vì không tồn tại: " & vbNewLine
                            txtResult.Text &= _lstDelete
                            rwMessage.VisibleOnPageLoad = True
                            rwMessage.Visible = True

                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If
                End Select
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgGrid.DataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If SelectedID = 0 Then
                If (rgGrid.MasterTableView.Items.Count > 0) Then
                    'rgGrid.MasterTableView.Items(0).Selected = True
                    rgGrid_SelectedIndexChanged(Nothing, Nothing)
                End If
            End If
            Dim gdiItem As GridDataItem = rgGrid.MasterTableView.FindItemByKeyValue("ID", SelectedID)
            If gdiItem IsNot Nothing Then
                gdiItem.Selected = True
                rgGrid_SelectedIndexChanged(Nothing, Nothing)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load datasource cho grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGrid.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh(CommonMessage.ACTION_UPDATED)
            _mylog.WriteLog(_mylog._info, _classPath, method,
                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Event thay đổi trang hiển thị, set lại dòng được chọn là dòng đầu trang
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub rgGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgGrid.PageIndexChanged
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        SelectedID = 0
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
    ''' <summary>
    ''' Event chọn vào row trên grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgGrid.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim item As GridDataItem
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_NORMAL Then
                If rgGrid.SelectedItems.Count > 0 Then
                    item = CType(rgGrid.SelectedItems(0), GridDataItem)
                    SelectedID = Decimal.Parse(item.GetDataKeyValue("ID").ToString)
                    User = (From p In Me.ListUsers Where p.ID = SelectedID).SingleOrDefault
                    UpdatePageViewState(CommonMessage.ACTION_UPDATED)
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"
    ''' <summary>
    ''' Phục hồi dữ liệu đã xóa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function RepareDataForDelete() As List(Of Decimal)
        Dim lst As New List(Of Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            For Each dr As Telerik.Web.UI.GridDataItem In rgGrid.SelectedItems
                lst.Add(Decimal.Parse(dr("ID").Text))
            Next
            _mylog.WriteLog(_mylog._info, _classPath, method,
                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
        Return lst
    End Function

    Protected Sub rgGrid_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        'If Not IsPostBack Then
        '    rgGrid.SelectedIndexes.Clear()
        '    SelectedID = Nothing
        'End If
        If rgGrid.SelectedItems.Count = 0 And CurrentState = CommonMessage.STATE_NORMAL Then
            UserView.SetProperty("User", Nothing)
            UserView.Refresh()
            rgGrid.SelectedIndexes.Clear()
            'UpdatePageViewState(CommonMessage.ACTION_UPDATED)
        End If
    End Sub
#End Region

End Class
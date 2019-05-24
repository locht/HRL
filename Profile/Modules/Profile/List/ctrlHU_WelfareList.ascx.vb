Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Common
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_WelfareList
    Inherits Common.CommonView

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/List/" + Me.GetType().Name.ToString()

#Region "Property"
 
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Goi ham khoi tao gia tri ban dau cho cac control tren page
    ''' khoi tao trang thai cho grid rgAssets voi cac thiet lap filter
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgWelfareList.SetFilter()
            rgWelfareList.AllowCustomPaging = True
            InitControl()
            If Not IsPostBack Then
                ViewConfig(RadPane1)
                GirdConfig(rgWelfareList)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ''' <lastupdate>
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Khoi tao gia tri cho cac control tren page
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarWelfareLists
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete, ToolbarItem.Active, ToolbarItem.Deactive,
                                       ToolbarItem.Export)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            Refresh("")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Rebind du lieu cho rgAssets theo case message
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        'Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgWelfareList.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgWelfareList.CurrentPageIndex = 0
                        rgWelfareList.MasterTableView.SortExpressions.Clear()
                        rgWelfareList.Rebind()
                    Case "Cancel"
                        rgWelfareList.MasterTableView.ClearSelectedItems()
                End Select
            End If
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    '''<lastupdate>
    ''' 30/06/2017 15:54
    ''' </lastupdate>
    ''' <summary>
    ''' Ham tao du lieu filter theo cac tham so dau vao
    ''' mac dinh isFull  load full man hinh hay ko?
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileRepository
        Dim _filter As New WelfareListDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgWelfareList, _filter)
            Dim Sorts As String = rgWelfareList.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetWelfareList(_filter, Sorts).ToTable()
                Else
                    Return rep.GetWelfareList(_filter).ToTable()
                End If
            Else
                Dim WelfareLists As List(Of WelfareListDTO)
                If Sorts IsNot Nothing Then
                    WelfareLists = rep.GetWelfareList(_filter, rgWelfareList.CurrentPageIndex, rgWelfareList.PageSize, MaximumRows, Sorts)
                Else
                    WelfareLists = rep.GetWelfareList(_filter, rgWelfareList.CurrentPageIndex, rgWelfareList.PageSize, MaximumRows)
                End If

                rgWelfareList.VirtualItemCount = MaximumRows
                rgWelfareList.DataSource = WelfareLists
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function

    ''' <lastupdate>
    ''' 30/06/2017 15:52
    ''' </lastupdate>
    ''' <summary>
    ''' Cap nhat trang thai control tren page theo trang thai them moi, sua hay default, kich hoat
    ''' huy kich hoat, update trang thai cac item tren toolbar theo tung truong hop 
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgWelfareList, False)
                    EnableControlAll(True, txtCode, txtName, nmSENIORITY, nmCHILD_OLD_FROM,
                                     nmCHILD_OLD_TO, nmMONEY, lstbGender, lstCONTRACT_TYPE,
                                     dpSTART_DATE, dpEND_DATE, chkIS_AUTO)
                    txtCode.ReadOnly = True
                    txtCode.Text = rep.AutoGenCode("PL", "HU_WELFARE_LIST", "CODE")
                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgWelfareList, True)
                    EnableControlAll(False, txtCode, txtName, nmSENIORITY, nmCHILD_OLD_FROM,
                                     nmCHILD_OLD_TO, nmMONEY, lstbGender, lstCONTRACT_TYPE,
                                     dpSTART_DATE, dpEND_DATE, chkIS_AUTO)
                    txtCode.ReadOnly = True
                Case CommonMessage.STATE_EDIT

                    EnabledGridNotPostback(rgWelfareList, False)
                    EnableControlAll(True, txtCode, txtName, nmSENIORITY, nmCHILD_OLD_FROM,
                                     nmCHILD_OLD_TO, nmMONEY, lstbGender, lstCONTRACT_TYPE,
                                     dpSTART_DATE, dpEND_DATE, chkIS_AUTO)

                    txtCode.ReadOnly = True
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgWelfareList.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgWelfareList.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveWelfareList(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgWelfareList.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgWelfareList.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgWelfareList.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveWelfareList(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgWelfareList.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgWelfareList.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgWelfareList.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteWelfareList(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            UpdateToolbarState()
            rep.Dispose()
            txtCode.Focus()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try

    End Sub

    ''' <lastupdate>
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Fill du lieu cho control combobox, thiet lap ngon ngu hien thi cho cac control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("CODE", txtCode)
            dic.Add("NAME", txtName)
            dic.Add("GENDER", lstbGender)
            dic.Add("CONTRACT_TYPE", lstCONTRACT_TYPE)
            dic.Add("SENIORITY", nmSENIORITY)
            dic.Add("MONEY", nmMONEY)
            dic.Add("CHILD_OLD_FROM", nmCHILD_OLD_FROM)
            dic.Add("CHILD_OLD_TO", nmCHILD_OLD_TO)
            dic.Add("IS_AUTO", chkIS_AUTO)
            dic.Add("START_DATE", dpSTART_DATE)
            dic.Add("END_DATE", dpEND_DATE)
            'dic.Add("IS_AUTO", chkIS_AUTO)
            'dic.Add("CHILD_OLD_FROM", nmCHILD_OLD_FROM)
            'dic.Add("CHILD_OLD_TO", nmCHILD_OLD_TO)
            Utilities.OnClientRowSelectedChanged(rgWelfareList, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 30/06/2017 15:33
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command cho control OnMainToolbar
    ''' Cac command la them moi, sua, kich hoat, huy kich hoat, xoa, xuat file, luu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objWelfareList As New WelfareListDTO
        Dim gID As Decimal
        Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(txtCode, txtName, nmSENIORITY,
                                      nmMONEY, lstbGender, lstCONTRACT_TYPE,
                                      dpSTART_DATE, dpEND_DATE, chkIS_AUTO)
                    chkIS_AUTO.Checked = Nothing
                    rgWelfareList.Rebind()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgWelfareList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgWelfareList.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgWelfareList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgWelfareList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgWelfareList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgWelfareList.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgWelfareList.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If Not rep.CheckExistInDatabase(lstDeletes, ProfileCommonTABLE_NAME.HU_WELFARE_LIST) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                        Return
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
                            rgWelfareList.ExportExcel(Server, Response, dtData, "WelfareList")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim strGenderID As New List(Of String)
                        Dim strGenderName As New List(Of String)
                        Dim strWorkStatusID As New List(Of String)
                        Dim strWorkStatusName As New List(Of String)
                        For Each i As RadListBoxItem In lstbGender.CheckedItems
                            strGenderID.Add(i.Value.ToString())
                            strGenderName.Add(i.Text.ToString())
                        Next
                        Try
                            For Each t As RadListBoxItem In lstCONTRACT_TYPE.CheckedItems
                                strWorkStatusID.Add(t.Value.ToString())
                                strWorkStatusName.Add(t.Text.ToString())
                            Next
                        Catch ex As Exception
                            Throw ex
                        End Try

                        objWelfareList.CODE = txtCode.Text
                        objWelfareList.NAME = txtName.Text
                        If strGenderID.Count > 0 Then
                            objWelfareList.GENDER = strGenderID.Aggregate(Function(cur, [next]) cur & "," & [next])
                            objWelfareList.GENDER_NAME = strGenderName.Aggregate(Function(cur, [next]) cur & "," & [next])
                        End If
                        If strWorkStatusID IsNot Nothing And strWorkStatusID.Count > 0 Then
                            objWelfareList.CONTRACT_TYPE = strWorkStatusID.Aggregate(Function(cur, [next]) cur & "," & [next])
                            objWelfareList.CONTRACT_TYPE_NAME = strWorkStatusName.Aggregate(Function(cur, [next]) cur & "," & [next])
                        End If
                        objWelfareList.CHILD_OLD_FROM = nmCHILD_OLD_FROM.Value
                        objWelfareList.CHILD_OLD_TO = nmCHILD_OLD_TO.Value
                        If (objWelfareList.CHILD_OLD_FROM > objWelfareList.CHILD_OLD_TO) Then
                            ShowMessage("Tuổi từ phải nhỏ hơn tuổi đến", NotifyType.Error)
                            Exit Sub
                        End If
                        objWelfareList.SENIORITY = nmSENIORITY.Value
                        objWelfareList.MONEY = nmMONEY.Value
                        objWelfareList.START_DATE = dpSTART_DATE.SelectedDate
                        objWelfareList.END_DATE = dpEND_DATE.SelectedDate
                        objWelfareList.IS_AUTO = chkIS_AUTO.Checked
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objWelfareList.ACTFLG = "A"
                                If rep.InsertWelfareList(objWelfareList, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    Refresh("InsertView")
                                    'SelectedItemDataGridByKey(rgWelfareList, gID)
                                    ClearControlValue(txtCode, txtName, nmSENIORITY,
                                       nmMONEY, lstbGender, lstCONTRACT_TYPE,
                                       dpSTART_DATE, dpEND_DATE, chkIS_AUTO)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objWelfareList.ID = rgWelfareList.SelectedValue
                                If rep.ModifyWelfareList(objWelfareList, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    Refresh("UpdateView")
                                    'SelectedItemDataGridByKey(rgWelfareList, gID, , rgWelfareList.CurrentPageIndex)
                                    ClearControlValue(txtCode, txtName, nmSENIORITY,
                                      nmMONEY, lstbGender, lstCONTRACT_TYPE,
                                      dpSTART_DATE, dpEND_DATE, chkIS_AUTO)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWelfareList")
                    End If

                    chkIS_AUTO.Checked = Nothing
                    rgWelfareList.Rebind()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    ClearControlValue(txtCode, txtName, nmSENIORITY, nmMONEY,
                                      lstbGender, lstCONTRACT_TYPE, dpSTART_DATE,
                                      dpEND_DATE, chkIS_AUTO)
                    chkIS_AUTO.Checked = Nothing
                    Refresh("Cancel")
                    rgWelfareList.Rebind()
            End Select
            rep.Dispose()
            UpdateControlState()

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 30/06/2017 15:37
    ''' </lastupdate>
    ''' <summary>
    ''' Xu lu su kien button command cho control ctrlMessageBox
    ''' Xu ly cho cac command kich hoat, huy kich hoat, xoa
    ''' Cap nhat lai trang thai cac control theo tung command
    ''' Rebind lai du lieu tren grid rgAssets
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
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
            rgWelfareList.Rebind()
            ClearControlValue(txtCode, txtName, nmSENIORITY,
                                      nmMONEY, lstbGender, lstCONTRACT_TYPE,
                                      dpSTART_DATE, dpEND_DATE, chkIS_AUTO)
            chkIS_AUTO.Checked = Nothing
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    ''' <lastupdate>
    ''' 30/06/2017 15:44
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc dung de ket noi du lieu truoc khi bind du lieu va grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgWelfareList.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ''' <lastupdate>
    ''' 30/06/2017 15:46
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly server validate cho control cvalCode
    ''' Call ham AutogenCode(firstChar As String, tableName As String, colName As String)
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New ProfileRepository
        Dim _validate As New WelfareListDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.ID = rgWelfareList.SelectedValue
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateWelfareList(_validate)
            Else
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateWelfareList(_validate)
            End If

            If Not args.IsValid Then
                txtCode.Text = rep.AutoGenCode("PL", "HU_WELFARE_LIST", "CODE")
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Server Validate cua control cvalDisciplineLevel
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Sub cvalContractType_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalContractType.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New ProfileRepository
            Dim validate As New ContractTypeDTO
            Dim lstID As New List(Of Decimal)
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                For Each t As RadListBoxItem In lstCONTRACT_TYPE.CheckedItems
                    lstID.Add(t.Value)
                Next
                If (lstID.Count > 0) Then
                    For Each i In lstID
                        validate.ID = i
                        validate.ACTFLG = "A"
                        args.IsValid = rep.ValidateContractType(validate)
                        If Not args.IsValid Then
                            Exit For
                        End If
                    Next
                End If
            End If
            If Not args.IsValid Then
                Dim ListComboData As New ComboBoxDataDTO
                ListComboData.GET_CONTRACTTYPE = True
                rep.GetComboList(ListComboData)
                FillCheckBoxList(lstCONTRACT_TYPE, ListComboData.LIST_CONTRACTTYPE, "NAME", "ID")
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"

    '''<lastupdate>
    ''' 30/06/2017 15:54
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc cap nhat trang thai cho control toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    ''' <lastupdate>
    ''' 30/06/2017 16:48
    ''' </lastupdate>
    ''' <summary>
    ''' Lay du lieu cho control combobox ListComboData gioi tinh, loai hop dong
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim rep As New ProfileRepository
        Dim ListComboData As New ComboBoxDataDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ListComboData.GET_GENDER = True
            ListComboData.GET_CONTRACTTYPE = True
            rep.GetComboList(ListComboData)
            rep.Dispose()
            FillCheckBoxList(lstbGender, ListComboData.LIST_GENDER, "NAME_VN", "ID")
            FillCheckBoxList(lstCONTRACT_TYPE, ListComboData.LIST_CONTRACTTYPE, "NAME", "ID")
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

#End Region

End Class
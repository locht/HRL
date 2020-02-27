﻿Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Common
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_AllowanceList
    Inherits Common.CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/List/" + Me.GetType().Name.ToString()

#Region "Property"

    Public Property AllowanceLists As List(Of AllowanceListDTO)
        Get
            Return ViewState(Me.ID & "_AllowanceLists")
        End Get
        Set(ByVal value As List(Of AllowanceListDTO))
            ViewState(Me.ID & "_AllowanceLists") = value
        End Set
    End Property

    Property ActiveAllowanceLists As List(Of AllowanceListDTO)
        Get
            Return ViewState(Me.ID & "_ActiveAllowanceLists")
        End Get
        Set(ByVal value As List(Of AllowanceListDTO))
            ViewState(Me.ID & "_ActiveAllowanceLists") = value
        End Set
    End Property

    Property DeleteAllowanceLists As List(Of AllowanceListDTO)
        Get
            Return ViewState(Me.ID & "_DeleteAllowanceLists")
        End Get
        Set(ByVal value As List(Of AllowanceListDTO))
            ViewState(Me.ID & "_DeleteAllowanceLists") = value
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

    Property dataTable As DataTable
        Get
            Return ViewState(Me.ID & "_dataTable")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dataTable") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 29/06/2017 15:51
    ''' </lastupdate>
    ''' <summary>
    ''' Hien thi thong tin danh muc phu cap
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SetGridFilter(rgMain)
            Refresh()
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 29/06/2017 15:54
    ''' </lastupdate>
    ''' <summary>
    ''' Ham goi khoi tao cho cac control tren page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgMain.AllowCustomPaging = True
            'rgMain.ClientSettings.EnablePostBackOnRowClick = True
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ''' <lastupdate>
    ''' 29/06/2017 15:54
    ''' </lastupdate>
    ''' <summary>
    ''' Ham khoi tao gia tri cho cac control tren page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarAllowanceLists
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Seperator,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete, ToolbarItem.Seperator,
                                       ToolbarItem.Active,
                                       ToolbarItem.Deactive,
                                       ToolbarItem.Export)
            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
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
    ''' 29/06/2017 15:56
    ''' </lastupdate>
    ''' <summary>
    ''' Gan lai gia tri CurrentState ve gia tri trang thai default
    ''' Rebind du lieu va cac trang thai sort cho grid rgMain
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
                        rgMain.Rebind()
                        ClearControlValue(txtCode, txtName, cbDMAlow, cboNAlow, ntxtOrder, chkIsInsurrance, chkIsContract)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                        ClearControlValue(txtCode, txtName, cbDMAlow, cboNAlow, ntxtOrder, chkIsInsurrance, chkIsContract)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgMain.MasterTableView.ClearSelectedItems()
                End Select
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 29/06/2017 16:09
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc tao du lieu khi filter, default isFull = false
    ''' Bind du lieu vao grid voi ket qua co duoc sau filter
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileRepository
        Dim _filter As New AllowanceListDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            SetValueObjectByRadGrid(rgMain, _filter)
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetAllowanceList(_filter, Sorts).ToTable()
                Else
                    Return rep.GetAllowanceList(_filter).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.AllowanceLists = rep.GetAllowanceList(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
                Else
                    Me.AllowanceLists = rep.GetAllowanceList(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
                End If
                rep.Dispose()
                rgMain.MasterTableView.FilterExpression = ""
                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = Me.AllowanceLists
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function

    ''' <lastupdate>
    ''' 29/06/2017 16:09
    ''' </lastupdate>
    ''' <summary>
    ''' Cap nhat trang thai cac control tren page
    ''' Xu ly theo cac su kien them moi, mac dinh, sua, kick hoat, huy kich hoat, xoa
    ''' Cap nhat trang thai toolbar sau cac action
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NEW

                    EnabledGridNotPostback(rgMain, False)
                    txtCode.Focus()
                    EnableControlAll(True, txtCode, txtName, chkIsInsurrance, cbDMAlow, cboNAlow, ntxtOrder, chkIsContract)
                Case CommonMessage.STATE_NORMAL
                    If rgMain.SelectedValue IsNot Nothing Then
                        Dim item = (From p In AllowanceLists Where p.ID = rgMain.SelectedValue).SingleOrDefault
                        txtName.Text = item.NAME
                        txtCode.Text = item.CODE

                    End If
                    EnabledGridNotPostback(rgMain, True)
                    EnableControlAll(False, txtCode, txtName, chkIsInsurrance, cbDMAlow, cboNAlow, ntxtOrder, chkIsContract)
                Case CommonMessage.STATE_EDIT

                    EnabledGridNotPostback(rgMain, False)
                    EnableControlAll(True, txtCode, txtName, chkIsInsurrance, cbDMAlow, cboNAlow, ntxtOrder, chkIsContract)

                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAllowanceList(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAllowanceList(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
                Case CommonMessage.STATE_DELETE
                    If rep.DeleteAllowanceList(DeleteAllowanceLists) Then
                        DeleteAllowanceLists = Nothing
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        UpdateControlState()
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                    End If
            End Select
            rep.Dispose()
            UpdateToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try

    End Sub

    ''' <lastupdate>
    ''' 29/06/2017 16:09
    ''' </lastupdate>
    ''' <summary>
    ''' Fill du lieu cho combobox cboAllowType
    ''' Thiet lap ngon ngu cho cac controll
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim dtData As DataTable
            Using rep As New ProfileRepository
                dataTable = rep.GetOtherList("ALLOWANCE_TYPE", True)
                FillRadCombobox(cbDMAlow, dataTable, "NAME", "ID")
                dtData = rep.GetOtherList("ALLOWANCE_GROUP", True)
                FillRadCombobox(cboNAlow, dtData, "NAME", "ID")
            End Using

            Dim startTime As DateTime = DateTime.UtcNow
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("CODE", txtCode)
            dic.Add("NAME", txtName)
            dic.Add("IS_INSURANCE", chkIsInsurrance)
            dic.Add("IS_CONTRACT", chkIsContract)
            dic.Add("ALLOWANCE_TYPE", cbDMAlow)
            dic.Add("ALLOWANCE_GROUP", cboNAlow)
            dic.Add("ORDERS", ntxtOrder)
            Utilities.OnClientRowSelectedChanged(rgMain, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 29/06/2017 16:09
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command cua OnMainToolbar
    ''' Cap nhat trang thai cac control tren page tuy theo cac command 
    ''' la them moi, sua, kick hoat, huy kich hoat, xoa, xuat file, luu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objAllowanceList As New AllowanceListDTO
        Dim gID As Decimal
        Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(txtCode, txtName)
                    ' txtCode.Text = rep.AutoGenCode("PC", "HU_ALLOWANCE_LIST", "CODE")
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
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
                Case CommonMessage.TOOLBARITEM_DELETE

                    Dim lstDeletes As New List(Of AllowanceListDTO)
                    Dim lstID As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(New AllowanceListDTO With {.ID = item.GetDataKeyValue("ID")})
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next

                    If Not rep.CheckExistInDatabase(lstID, ProfileCommonTABLE_NAME.HU_ALLOWANCE_LIST) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                        Return
                    End If
                    DeleteAllowanceLists = lstDeletes
                    If DeleteAllowanceLists.Count > 0 Then
                        ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    End If

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtData, "AllowanceList")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objAllowanceList.CODE = txtCode.Text.ToString.Trim
                        objAllowanceList.NAME = txtName.Text.ToString.Trim
                        objAllowanceList.IS_INSURANCE = chkIsInsurrance.Checked
                        objAllowanceList.IS_CONTRACT = chkIsContract.Checked
                        objAllowanceList.ALLOWANCE_TYPE = cbDMAlow.SelectedValue
                        objAllowanceList.ALLOWANCE_GROUP = cboNAlow.SelectedValue
                        objAllowanceList.ORDERS = ntxtOrder.Value
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objAllowanceList.ACTFLG = "A"
                                If rep.InsertAllowanceList(objAllowanceList, gID) Then
                                    CurrentState = CommonMessage.STATE_NEW
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objAllowanceList.ID = rgMain.SelectedValue
                                'check exist item allowanceList
                                Dim _validate As New AllowanceListDTO
                                _validate.ID = rgMain.SelectedValue
                                If rep.ValidateAllowanceList(_validate) Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                    ClearControlValue(txtCode, txtName, cbDMAlow, cboNAlow, ntxtOrder, chkIsInsurrance, chkIsContract)
                                    rgMain.Rebind()
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    UpdateControlState()
                                    Exit Sub
                                End If
                                If rep.ModifyAllowanceList(objAllowanceList, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objAllowanceList.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgMain')")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ClearControlValue(txtCode, txtName, cbDMAlow, cboNAlow, ntxtOrder, chkIsInsurrance, chkIsContract)
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
                    rgMain.Rebind()
            End Select
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 29/06/2017 16:37
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command cho control ctrlMessageBox khi click Yes/No sau confirm
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
            ClearControlValue(txtCode, txtName, cbDMAlow, cboNAlow, ntxtOrder, chkIsInsurrance, chkIsContract)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    ''' <lastupdate>
    ''' 29/06/2017 16:39
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien connect du lieu truoc khi bind du lieu vao data grid 
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
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
    ''' Xu ly su kien server validate cho control cvalCode
    ''' </lastupdate>
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New ProfileRepository
        Dim _validate As New AllowanceListDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.ID = rgMain.SelectedValue
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateAllowanceList(_validate)
            Else
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateAllowanceList(_validate)
            End If

            If Not args.IsValid Then
                txtCode.Text = rep.AutoGenCode("PC", "HU_ALLOWANCE_LIST", "CODE")
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 29/06/2017 16:42
    ''' </lastupdate>
    ''' <summary>
    ''' Cap nhat trang thai cho control toolbar
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

#End Region

End Class
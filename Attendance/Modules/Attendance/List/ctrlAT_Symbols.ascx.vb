Imports Framework.UI
Imports Framework.UI.Utilities
Imports Attendance.AttendanceBusiness
Imports Common
Imports Telerik.Web.UI
Imports WebAppLog
Imports Profile

Public Class ctrlAT_Symbols
    Inherits Common.CommonView

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _shift_Day_ID As Decimal = 0
    Dim _classPath As String = "Attendance/Module/Attendance/List/" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' IDSelect
    ''' </summary>
    ''' <remarks></remarks>
    Public IDSelect As Integer

    ''' <summary>
    ''' AT_SHIFT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AT_SHIFT As List(Of AT_SHIFTDTO)
        Get
            Return ViewState(Me.ID & "_AT_SHIFT")
        End Get
        Set(ByVal value As List(Of AT_SHIFTDTO))
            ViewState(Me.ID & "_AT_SHIFT") = value
        End Set
    End Property

    ''' <summary>
    ''' ListComboData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property


#End Region

#Region "Page"

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Ke thua ViewLoad tu CommnView, la ham load du lieu, control states cua usercontrol
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            SetGridFilter(rgDanhMuc)
            rgDanhMuc.AllowCustomPaging = True
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Load cac control, menubar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarCostCenters

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                        ToolbarItem.Delete,
                                       ToolbarItem.Active, ToolbarItem.Deactive,
                                        ToolbarItem.Export)

            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            Refresh("")

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New AttendanceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDanhMuc.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDanhMuc.CurrentPageIndex = 0
                        rgDanhMuc.MasterTableView.SortExpressions.Clear()
                        rgDanhMuc.Rebind()
                    Case "Cancel"
                        rgDanhMuc.MasterTableView.ClearSelectedItems()
                End Select

            End If

            UpdateControlState()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_SHIFTDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgDanhMuc, obj)
            Dim Sorts As String = rgDanhMuc.MasterTableView.SortExpressions.GetSortString()
            'hoaivv add 2 rc
            Dim user = LogHelper.CurrentUser
            obj.SHIFT_DAY = user.ID
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.AT_SHIFT = rep.GetAT_SHIFT(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows, "CREATED_DATE desc")
                Else
                    Me.AT_SHIFT = rep.GetAT_SHIFT(obj, rgDanhMuc.CurrentPageIndex, rgDanhMuc.PageSize, MaximumRows)
                End If
                rgDanhMuc.VirtualItemCount = MaximumRows
                rgDanhMuc.DataSource = Me.AT_SHIFT
            Else
                If Sorts IsNot Nothing Then
                    Return rep.GetAT_SHIFT(obj, 0, Integer.MaxValue, 0, Sorts).ToTable
                Else
                    Return rep.GetAT_SHIFT(obj).ToTable
                End If
                'Return rep.GetAT_SHIFT(obj).ToTable
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    rtWCODE.Text = ""
                    rtWNAME.Text = ""
                    txtNote.Text = ""
                    rcWDATAMODEID.SelectedIndex = Nothing
                    rcWDATATYEID.SelectedIndex = Nothing
                    rcWGROUPID.SelectedIndex = Nothing
                    rtWCODE.Enabled = True
                    rtWNAME.Enabled = True
                    rcWDATAMODEID.Enabled = True
                    rcWDATATYEID.Enabled = True
                    rcWGROUPID.Enabled = True
                    txtNote.Enabled = True
                    rnWINDEX.ClearValue()
                    rnWINDEX.Enabled = True
                    rdEFFECT_DATE.ClearValue()
                    rdEFFECT_DATE.Enabled = True
                    rdEXPIRE_DATE.ClearValue()
                    rdEXPIRE_DATE.Enabled = True
                    'rcSYMBOL_FUN_ID.ClearCheckedItems()
                    'rcSYMBOL_FUN_ID.Enabled = True
                    ckIS_DATAFROMEXCEL.Checked = False
                    ckIS_DATAFROMEXCEL.Enabled = True
                    ckIS_DAY_HALF.Checked = False
                    ckIS_DAY_HALF.Enabled = True
                    ckIS_DISPLAY.Checked = False
                    ckIS_DISPLAY.Enabled = True
                    ckIS_DISPLAY_PORTAL.Checked = False
                    ckIS_DISPLAY_PORTAL.Enabled = True
                    ckIS_LAVE_HOLIDAY.Checked = False
                    ckIS_LAVE_HOLIDAY.Enabled = True
                    ckIS_LEAVE.Checked = False
                    ckIS_LEAVE.Enabled = True
                    ckIS_LEAVE_WEEKLY.Checked = False
                    ckIS_LEAVE_WEEKLY.Enabled = True

                    rgDanhMuc.Rebind()
                    EnabledGridNotPostback(rgDanhMuc, False)
                Case CommonMessage.STATE_NORMAL
                    rtWNAME.Text = ""
                    rcWDATAMODEID.SelectedIndex = Nothing
                    rcWDATATYEID.SelectedIndex = Nothing
                    rcWGROUPID.SelectedIndex = Nothing
                    txtNote.Text = ""
                    rtWCODE.Text = ""
                    rtWCODE.Enabled = False
                    rtWNAME.Enabled = False
                    rcWDATAMODEID.Enabled = False
                    rcWDATATYEID.Enabled = False
                    rcWGROUPID.Enabled = False
                    txtNote.Enabled = False
                    rnWINDEX.ClearValue()
                    rnWINDEX.Enabled = False
                    rdEFFECT_DATE.ClearValue()
                    rdEFFECT_DATE.Enabled = False
                    rdEXPIRE_DATE.ClearValue()
                    rdEXPIRE_DATE.Enabled = False
                    'rcSYMBOL_FUN_ID.ClearCheckedItems()
                    'rcSYMBOL_FUN_ID.Enabled = False
                    ckIS_DATAFROMEXCEL.Checked = False
                    ckIS_DATAFROMEXCEL.Enabled = False
                    ckIS_DAY_HALF.Checked = False
                    ckIS_DAY_HALF.Enabled = False
                    ckIS_DISPLAY.Checked = False
                    ckIS_DISPLAY.Enabled = False
                    ckIS_DISPLAY_PORTAL.Checked = False
                    ckIS_DISPLAY_PORTAL.Enabled = False
                    ckIS_LAVE_HOLIDAY.Checked = False
                    ckIS_LAVE_HOLIDAY.Enabled = False
                    ckIS_LEAVE.Checked = False
                    ckIS_LEAVE.Enabled = False
                    ckIS_LEAVE_WEEKLY.Checked = False
                    ckIS_LEAVE_WEEKLY.Enabled = False
                    EnabledGridNotPostback(rgDanhMuc, True)
                Case CommonMessage.STATE_EDIT
                    rtWCODE.Enabled = True
                    rtWNAME.Enabled = True
                    rcWDATAMODEID.Enabled = True
                    rcWDATATYEID.Enabled = True
                    rcWGROUPID.Enabled = True
                    txtNote.Enabled = True
                    rnWINDEX.Enabled = True
                    rdEFFECT_DATE.Enabled = True
                    rdEXPIRE_DATE.Enabled = True
                    'rcSYMBOL_FUN_ID.Enabled = True
                    ckIS_DATAFROMEXCEL.Enabled = True
                    ckIS_DAY_HALF.Enabled = True
                    ckIS_DISPLAY.Enabled = True
                    ckIS_DISPLAY_PORTAL.Enabled = True
                    ckIS_LAVE_HOLIDAY.Enabled = True
                    ckIS_LEAVE.Enabled = True
                    ckIS_LEAVE_WEEKLY.Enabled = True
                    EnabledGridNotPostback(rgDanhMuc, False)
                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAT_SHIFT(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgDanhMuc.Rebind()
                        ClearControlValue(rtWCODE, rtWNAME, rcWDATAMODEID, rcWDATATYEID, rcWGROUPID, txtNote)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveAT_SHIFT(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgDanhMuc.Rebind()
                        ClearControlValue(rtWCODE, rtWNAME, rcWDATAMODEID, rcWDATATYEID, rcWGROUPID, txtNote)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteAT_SHIFT(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select

            rtWCODE.Focus()
            UpdateToolbarState()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            GetDataCombo()
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("CODE", rtWCODE)
            dic.Add("NAME_VN", rtWNAME)
            dic.Add("NOTE", txtNote)
            Utilities.OnClientRowSelectedChanged(rgDanhMuc, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Set ngon ngu cho cac lable control va cac column name tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtdata As New DataTable()
        Try
            Using rep As New ProfileRepository
                dtdata = rep.GetOtherList("SYMBOLGROUP", True)
                FillRadCombobox(rcWGROUPID, dtdata, "NAME", "ID")
                dtdata = rep.GetOtherList("ATDATATYPE", True)
                FillRadCombobox(rcWDATATYEID, dtdata, "NAME", "ID")
                dtdata = rep.GetOtherList("ATTYPE", True)
                FillRadCombobox(rcWDATAMODEID, dtdata, "NAME", "ID")
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        Finally
            dtdata.Dispose()
        End Try
    End Sub

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Set lai ngon ngu cho cac lable control va cac column name tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReloadCombobox()
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ListComboData = New ComboBoxDataDTO
            ListComboData.GET_LIST_SHIFT_SUNDAY = True
            rep.GetComboboxData(ListComboData)
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objsYMBOLS As New AT_SymbolsDTO
        Dim rep As New AttendanceRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgDanhMuc.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgDanhMuc.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lstID As New List(Of Decimal)
                    For idx = 0 To rgDanhMuc.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDanhMuc.SelectedItems(idx)
                        lstID.Add(Decimal.Parse(item("ID").Text))
                    Next
                    If Not rep.CheckExistInDatabase(lstID, AttendanceCommonTABLE_NAME.AT_SHIFT) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                        Return
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objsYMBOLS.STATUS = -1
                        objsYMBOLS.WCODE = rtWCODE.Text.Trim
                        objsYMBOLS.WNAME = rtWNAME.Text.Trim
                        If IsNumeric(rcWGROUPID.SelectedValue) Then
                            objsYMBOLS.WGROUPID = rcWGROUPID.SelectedValue
                        End If
                        If IsNumeric(rnWINDEX.Value) Then
                            objsYMBOLS.WINDEX = rnWINDEX.Value
                        End If
                        If IsNumeric(rcWDATATYEID.SelectedValue) Then
                            objsYMBOLS.WDATATYEID = rcWDATATYEID.SelectedValue
                        End If
                        If IsNumeric(rcWDATAMODEID.SelectedValue) Then
                            objsYMBOLS.WDATAMODEID = rcWDATAMODEID.SelectedValue
                        End If
                        If IsDate(rdEFFECT_DATE.SelectedDate) Then
                            objsYMBOLS.EFFECT_DATE = rdEFFECT_DATE.SelectedDate
                        End If
                        If IsDate(rdEXPIRE_DATE.SelectedDate) Then
                            objsYMBOLS.EXPIRE_DATE = rdEXPIRE_DATE.SelectedDate
                        End If
                        objsYMBOLS.IS_DISPLAY = ckIS_DISPLAY.Checked
                        objsYMBOLS.IS_DATAFROMEXCEL = ckIS_DATAFROMEXCEL.Checked
                        objsYMBOLS.IS_DISPLAY_PORTAL = ckIS_DISPLAY_PORTAL.Checked
                        objsYMBOLS.IS_LEAVE = ckIS_LEAVE.Checked
                        objsYMBOLS.IS_LEAVE_WEEKLY = ckIS_LEAVE_WEEKLY.Checked
                        objsYMBOLS.IS_LAVE_HOLIDAY = ckIS_LAVE_HOLIDAY.Checked
                        objsYMBOLS.IS_DAY_HALF = ckIS_DAY_HALF.Checked
                        objsYMBOLS.NOTE = txtNote.Text.Trim
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.SaveAT_Symnols(objsYMBOLS, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    CreateDataFilter()
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                    ClearControlValue(rtWCODE, rtWNAME, txtNote, rdEFFECT_DATE, rdEXPIRE_DATE, rnWINDEX, rcWDATATYEID, rcWDATAMODEID, ckIS_DISPLAY, ckIS_DATAFROMEXCEL, ckIS_DISPLAY_PORTAL, ckIS_LEAVE, ckIS_LEAVE_WEEKLY, ckIS_LAVE_HOLIDAY, ckIS_DAY_HALF)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)

                                End If
                            Case CommonMessage.STATE_EDIT
                                objsYMBOLS.ID = rgDanhMuc.SelectedValue
                                If rep.SaveAT_Symnols(objsYMBOLS, rgDanhMuc.SelectedValue) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    CreateDataFilter()
                                    IDSelect = objsYMBOLS.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                    ClearControlValue(rtWCODE, rtWNAME, txtNote, rdEFFECT_DATE, rdEXPIRE_DATE, rnWINDEX, rcWDATATYEID, rcWDATAMODEID, ckIS_DISPLAY, ckIS_DATAFROMEXCEL, ckIS_DISPLAY_PORTAL, ckIS_LEAVE, ckIS_LEAVE_WEEKLY, ckIS_LAVE_HOLIDAY, ckIS_DAY_HALF)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Script", "setDefaultSize()")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgDanhMuc.ExportExcel(Server, Response, dtDatas, "CaLamViec")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
            End Select
            CreateDataFilter()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Event Yes, No tren popup message khi click nut: xoa, ap dung, ngung ap dung
    ''' va Set trang thai cho form va update trang thai control 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
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

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDanhMuc.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CreateDataFilter()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly viec check gia tri ID, Code
    ''' Tu dong gen code khi them moi
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvaWCODE_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvaWCODE.ServerValidate
        Dim rep As New AttendanceRepository
        Dim _validate As New AT_SHIFTDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.ID = rgDanhMuc.SelectedValue
                _validate.CODE = rtWCODE.Text.Trim
                args.IsValid = rep.ValidateAT_SHIFT(_validate)
            Else
                _validate.CODE = rtWCODE.Text.Trim
                args.IsValid = rep.ValidateAT_SHIFT(_validate)
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Update trang thai menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region
End Class
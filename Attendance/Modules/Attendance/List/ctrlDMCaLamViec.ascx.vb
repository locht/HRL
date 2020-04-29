﻿Imports Framework.UI
Imports Framework.UI.Utilities
Imports Attendance.AttendanceBusiness
Imports Common
Imports Telerik.Web.UI
Imports WebAppLog
Imports Profile

Public Class ctrlDMCaLamViec
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

    ''' <summary>
    ''' ValueMaCong
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueMaCong As Decimal
        Get
            Return ViewState(Me.ID & "_ValueMaCong")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ValueMaCong") = value
        End Set
    End Property

    ''' <summary>
    ''' ValueCaThu7
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueCaThu7 As Decimal
        Get
            Return ViewState(Me.ID & "_ValueCaThu7")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ValueCaThu7") = value
        End Set
    End Property

    ''' <summary>
    ''' ValueKieuCongCN
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueKieuCongCN As Decimal
        Get
            Return ViewState(Me.ID & "_ValueKieuCongCN")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_ValueKieuCongCN") = value
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
            'DisplayException(Me.ViewName, Me.ID, ex)
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
                        'SelectedItemDataGridByKey(rgDanhMuc, IDSelect, , rgDanhMuc.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL

                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDanhMuc.CurrentPageIndex = 0
                        rgDanhMuc.MasterTableView.SortExpressions.Clear()
                        rgDanhMuc.Rebind()
                        'SelectedItemDataGridByKey(rgDanhMuc, IDSelect, )

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

                    txtCode.Text = ""
                    txtNameVN.Text = ""
                    txtNote.Text = ""
                    'txtEnglishName.Text = ""
                    cboManualType.Text = ""
                    rtxtLATE_MINUTES.Value = 0
                    rtxtSOON_MINUTES.Value = 0
                    rtxtValue_Late.Value = 0
                    rtxtValue_Soon.Value = 0
                    rtxtSTT.Text = ""
                    chkIS_Shift_Night.Checked = False
                    chkIS_Show_Iportal.Checked = False


                    rdHours_Start.SelectedDate = Nothing
                    rdHours_Stop.SelectedDate = Nothing
                    rdEND_MID_HOURS.SelectedDate = Nothing
                    rdSTART_MID_HOURS.SelectedDate = Nothing
                    rdStart_Cal_Soon.SelectedDate = Nothing
                    rdStart_Cal_late.SelectedDate = Nothing

                    txtCode.Enabled = True
                    txtNameVN.Enabled = True
                    'txtEnglishName.Enabled = True

                    cboManualType.Enabled = True

                    rdHours_Start.Enabled = True
                    rdHours_Stop.Enabled = True
                    rdEND_MID_HOURS.Enabled = True
                    rdSTART_MID_HOURS.Enabled = True
                    rdStart_Cal_Soon.Enabled = True
                    rdStart_Cal_late.Enabled = True

                    rtxtLATE_MINUTES.Enabled = True
                    rtxtSOON_MINUTES.Enabled = True
                    rtxtValue_Late.Enabled = True
                    rtxtValue_Soon.Enabled = True

                    chkIS_Shift_Night.Enabled = True
                    chkIS_Show_Iportal.Enabled = True

                    txtNote.Enabled = True
                    rtxtSTT.Enabled = True

                    rgDanhMuc.Rebind()
                    EnabledGridNotPostback(rgDanhMuc, False)
                Case CommonMessage.STATE_NORMAL

                    txtCode.Text = ""
                    txtNameVN.Text = ""
                    txtNote.Text = ""
                    'txtEnglishName.Text = ""
                    cboManualType.Text = ""
                    rtxtLATE_MINUTES.Value = 0
                    rtxtSOON_MINUTES.Value = 0
                    rtxtValue_Late.Value = 0
                    rtxtValue_Soon.Value = 0
                    rtxtSTT.Text = ""

                    rdHours_Start.SelectedDate = Nothing
                    rdHours_Stop.SelectedDate = Nothing
                    rdEND_MID_HOURS.SelectedDate = Nothing
                    rdSTART_MID_HOURS.SelectedDate = Nothing
                    rdStart_Cal_Soon.SelectedDate = Nothing
                    rdStart_Cal_late.SelectedDate = Nothing

                    txtCode.Enabled = False
                    txtNameVN.Enabled = False
                    'txtEnglishName.Enabled = False

                    cboManualType.Enabled = False

                    rdHours_Start.Enabled = False
                    rdHours_Stop.Enabled = False
                    rdEND_MID_HOURS.Enabled = False
                    rdSTART_MID_HOURS.Enabled = False
                    rdStart_Cal_Soon.Enabled = False
                    rdStart_Cal_late.Enabled = False

                    rtxtLATE_MINUTES.Enabled = False
                    rtxtSOON_MINUTES.Enabled = False
                    rtxtValue_Late.Enabled = False
                    rtxtValue_Soon.Enabled = False

                    chkIS_Shift_Night.Enabled = False
                    chkIS_Show_Iportal.Enabled = False

                    txtNote.Enabled = False
                    rtxtSTT.Enabled = False
                    ClearControlValue(cboManualType, chkIS_Shift_Night, chkIS_Show_Iportal)
                    EnabledGridNotPostback(rgDanhMuc, True)
                Case CommonMessage.STATE_EDIT

                    txtCode.Enabled = True
                    txtNameVN.Enabled = True
                    'txtEnglishName.Enabled = True

                    cboManualType.Enabled = True

                    rdHours_Start.Enabled = True
                    rdHours_Stop.Enabled = True
                    rdEND_MID_HOURS.Enabled = True
                    rdSTART_MID_HOURS.Enabled = True
                    rdStart_Cal_Soon.Enabled = True
                    rdStart_Cal_late.Enabled = True

                    rtxtLATE_MINUTES.Enabled = True
                    rtxtSOON_MINUTES.Enabled = True
                    rtxtValue_Late.Enabled = True
                    rtxtValue_Soon.Enabled = True

                    chkIS_Shift_Night.Enabled = True
                    chkIS_Show_Iportal.Enabled = True

                    txtNote.Enabled = True
                    rtxtSTT.Enabled = True

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
                        ClearControlValue(txtCode, txtNameVN, rdHours_Start, rdHours_Stop, txtNote, rdEND_MID_HOURS, rdSTART_MID_HOURS)
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
                        ClearControlValue(txtCode, txtNameVN, rdHours_Start, rdHours_Stop, txtNote, rdEND_MID_HOURS, rdSTART_MID_HOURS)
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

                        'ListComboData = New ComboBoxDataDTO
                        'ListComboData.GET_LIST_SHIFT = True

                        'cboSaturday.ClearSelection()
                        'rep.GetComboboxData(ListComboData)
                        'FillDropDownList(cboSaturday, ListComboData.LIST_LIST_SHIFT, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboSaturday.SelectedValue)
                        'cboSaturday.SelectedIndex = 0
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select

            txtCode.Focus()
            UpdateToolbarState()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
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
            dic.Add("CODE", txtCode)
            dic.Add("NAME_VN", txtNameVN)

            dic.Add("MANUAL_TYPE", cboManualType)
            dic.Add("START_CAL_SOON", rdStart_Cal_Soon)
            dic.Add("START_CAL_LATE", rdStart_Cal_late)
            dic.Add("LATE_MINUTES", rtxtLATE_MINUTES)
            dic.Add("SOON_MINUTES", rtxtSOON_MINUTES)
            dic.Add("VALUE_LATE", rtxtValue_Late)
            dic.Add("VALUE_SOON", rtxtValue_Soon)
            dic.Add("IS_SHIFT_NIGHT", chkIS_Shift_Night)
            dic.Add("IS_SHOW_IPORTAL", chkIS_Show_Iportal)
            dic.Add("NOTE", txtNote)
            dic.Add("STT", rtxtSTT)

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
        Dim rep As New AttendanceRepository
        Dim repS As New ProfileStoreProcedure
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        '  Dim lstDate As List(Of String)

        Try
            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                'ListComboData.GET_LIST_SHIFT = True
                'ListComboData.GET_LIST_APPLY_LAW = True
                ListComboData.GET_LIST_SHIFT_SUNDAY = True
                rep.GetComboboxData(ListComboData)
            End If
            'lstDate.Add("Ngày làm việc bình thường (X) ")
            'lstDate.Add("Ngày nghĩ hàng tuần (CN) ")
            'FillRadCombobox(cboManualType, lstDate, "NAME", "ID")

            'FillDropDownList(cboSaturday, ListComboData.LIST_LIST_SHIFT_SUNDAY, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboSaturday.SelectedValue)
            'FillDropDownList(cboSunDay, ListComboData.LIST_LIST_SHIFT_SUNDAY, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboSunDay.SelectedValue)

            'hoaivv
            Dim dtOrgLevel As DataTable
            'Dim US As New USERDTO
            Dim user_id As Decimal
            Dim user = LogHelper.CurrentUser
            user_id = user.ID
            dtOrgLevel = repS.GET_ORGID_COMPANY_LEVEL_USER_ID(user_id)
            Dim dr As DataRow = dtOrgLevel.NewRow
            dr("ORG_ID") = "-1"
            dr("ORG_NAME_VN") = "Dùng  Chung"
            dtOrgLevel.Rows.Add(dr)

            dtOrgLevel.DefaultView.Sort = "ORG_ID ASC"
            ' FillRadCombobox(cboCongTy, dtOrgLevel, "ORG_NAME_VN", "ORG_ID", True)


            Dim item1 As New RadComboBoxItem()
            item1.Text = "Ngày làm việc bình thường (X)"
            item1.Value = 0
            cboManualType.Items.Add(item1)
            Dim item2 As New RadComboBoxItem()
            item2.Text = "Ngày nghĩ hàng tuần (CN)"
            item2.Value = 1
            cboManualType.Items.Add(item2)




            Dim table As New DataTable

            ' Create four typed columns in the DataTable.
            table.Columns.Add("ID", GetType(Double))
            table.Columns.Add("NAME", GetType(String))


            ' Add five rows with those columns filled in the DataTable.
            table.Rows.Add(0, "0")
            table.Rows.Add(0.5, "0.5")
            table.Rows.Add(1, "1")
            table.Rows.Add(1.5, "1.5")
            'FillRadCombobox(cboNgayCongCa, table, "NAME", "ID")
            'end
            Dim dtData As DataTable
            dtData = rep.GetAT_TIME_MANUALBINCOMBO()
            'illRadCombobox(cboMaCong, dtData, "NAME", "ID")

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
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
            'ListComboData.GET_LIST_SHIFT = True
            'ListComboData.GET_LIST_APPLY_LAW = True
            ListComboData.GET_LIST_SHIFT_SUNDAY = True
            rep.GetComboboxData(ListComboData)

            'FillDropDownList(cboSaturday, ListComboData.LIST_LIST_SHIFT_SUNDAY, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboSaturday.SelectedValue)
            'FillDropDownList(cboSunDay, ListComboData.LIST_LIST_SHIFT_SUNDAY, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboSunDay.SelectedValue)

            Dim dtData As DataTable
            dtData = rep.GetAT_TIME_MANUALBINCOMBO()
            'FillRadCombobox(cboMaCong, dtData, "NAME", "ID")

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
        Dim objShift As New AT_SHIFTDTO
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

                    If rep.ValidateCheckExistWorkingShift(lstID) = False Then
                        ShowMessage("Không được xóa. Ca làm việc đã được thiết lập cho phòng ban", NotifyType.Warning)
                        Exit Sub
                    End If

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
                        objShift.CODE = txtCode.Text.Trim
                        objShift.NAME_VN = txtNameVN.Text.Trim
                        objShift.HOURS_START = rdHours_Start.SelectedDate
                        objShift.HOURS_STOP = rdHours_Stop.SelectedDate
                        objShift.START_MID_HOURS = rdSTART_MID_HOURS.SelectedDate
                        objShift.END_MID_HOURS = rdEND_MID_HOURS.SelectedDate

                        objShift.MANUAL_TYPE = cboManualType.Text
                        objShift.IS_SHIFT_NIGHT = chkIS_Shift_Night.Checked
                        objShift.IS_SHOW_IPORTAL = chkIS_Show_Iportal.Checked
                        objShift.START_CAL_SOON = rdStart_Cal_Soon.SelectedDate
                        objShift.START_CAL_LATE = rdStart_Cal_late.SelectedDate
                        objShift.LATE_MINUTES = rtxtLATE_MINUTES.Value
                        objShift.SOON_MINUTES = rtxtSOON_MINUTES.Value
                        objShift.VALUE_LATE = rtxtValue_Late.Value
                        objShift.VALUE_SOON = rtxtValue_Soon.Value
                        objShift.STT = rtxtSTT.Value

                        objShift.NOTE = txtNote.Text.Trim
                        'hoaivv
                        'objShift.ORG_ID = cboCongTy.SelectedValue
                        'objShift.SHIFT_DAY = Convert.ToDecimal(cboNgayCongCa.SelectedValue)

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objShift.ACTFLG = "A"
                                If rep.InsertAT_SHIFT(objShift, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    CreateDataFilter()
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                    'ReloadCombobox()
                                    ClearControlValue(txtCode, txtNameVN, rdHours_Start, rdHours_Stop, txtNote)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)

                                End If

                            Case CommonMessage.STATE_EDIT

                                Dim cmRep As New CommonRepository
                                Dim lstID As New List(Of Decimal)

                                lstID.Add(Convert.ToDecimal(rgDanhMuc.SelectedValue))

                                If cmRep.CheckExistIDTable(lstID, "AT_SHIFT", "ID") Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    Refresh("Cancel")
                                    UpdateControlState()
                                    Exit Sub
                                End If

                                objShift.ID = rgDanhMuc.SelectedValue

                                For Each re As AT_SHIFTDTO In Me.AT_SHIFT
                                    If re.ID = objShift.ID Then
                                        objShift.CREATED_DATE = re.CREATED_DATE
                                        Exit For
                                    End If
                                Next

                                If rep.ModifyAT_SHIFT(objShift, rgDanhMuc.SelectedValue) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    CreateDataFilter()
                                    IDSelect = objShift.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                    ClearControlValue(txtCode, txtNameVN, rdHours_Start, rdHours_Stop, txtNote)
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
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New AttendanceRepository
        Dim _validate As New AT_SHIFTDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.ID = rgDanhMuc.SelectedValue
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateAT_SHIFT(_validate)
            Else
                _validate.CODE = txtCode.Text.Trim
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

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Kiem tra gia tri dang đươc chọn cua combox MÃ CÔNG có tồn tại hoặc bị ngừng ap dụng hay không?
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Private Sub cvalMaCong_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalMaCong.ServerValidate
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim rep As New AttendanceRepository

    '    Try
    '        If String.IsNullOrEmpty(cboMaCong.SelectedValue) Then
    '            Return
    '        End If

    '        ValueMaCong = cboMaCong.SelectedValue
    '        Dim result() As DataRow = rep.GetAT_TIME_MANUALBINCOMBO().Select("ID = " & cboMaCong.SelectedValue)
    '        If result.Length = 0 Then
    '            args.IsValid = False

    '            cboMaCong.ClearSelection()
    '            Dim dtData As DataTable
    '            dtData = rep.GetAT_TIME_MANUALBINCOMBO()
    '            FillRadCombobox(cboMaCong, dtData, "NAME", "ID")
    '            cboMaCong.SelectedIndex = 0
    '        Else
    '            args.IsValid = True
    '        End If

    '        _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    ''' <lastupdate>14/08/2017</lastupdate>
    ''' <summary>
    ''' Kiem tra gia tri dang đươc chọn cua combox KIỂU CÔNG CHỦ NHẬT có tồn tại hoặc bị ngừng áp dụng hay không?
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>

    Protected Sub rgDanhMuc_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgDanhMuc.SelectedIndexChanged
        Dim item As GridDataItem

        Try
            item = CType(rgDanhMuc.SelectedItems(0), GridDataItem)
            IDSelect = item.GetDataKeyValue("ID").ToString
            Dim AT_SHIFT1 = (From p In AT_SHIFT Where p.ID = Decimal.Parse(IDSelect)).SingleOrDefault
            Dim value As String = Replace(AT_SHIFT1.SHIFT_DAY.ToString(), ",", ".")
            ' cboNgayCongCa.SelectedValue = value
            rdHours_Start.SelectedDate = AT_SHIFT1.HOURS_START
            rdHours_Stop.SelectedDate = AT_SHIFT1.HOURS_STOP
            rdSTART_MID_HOURS.SelectedDate = AT_SHIFT1.START_MID_HOURS
            rdEND_MID_HOURS.SelectedDate = AT_SHIFT1.END_MID_HOURS
            'rdHOURS_STAR_CHECKIN.SelectedDate = AT_SHIFT1.HOURS_STAR_CHECKIN
            'rdHOURS_STAR_CHECKOUT.SelectedDate = AT_SHIFT1.HOURS_STAR_CHECKOUT

            If (IDSelect = 1 Or IDSelect = 2) Then
                Me.MainToolBar = tbarCostCenters
                MainToolBar.Items(1).Enabled = False
                MainToolBar.Items(4).Enabled = False
            Else
                MainToolBar.Items(1).Enabled = True
                MainToolBar.Items(4).Enabled = True
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

End Class
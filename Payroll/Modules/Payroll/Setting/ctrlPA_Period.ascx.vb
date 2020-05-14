﻿Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Common
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_Period
    Inherits Common.CommonView

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Payroll/Module/Payroll/Setting/" + Me.GetType().Name.ToString()

#Region "Property"

    Public Property vPeriod As List(Of ATPeriodDTO)
        Get
            Return ViewState(Me.ID & "_Period")
        End Get
        Set(ByVal value As List(Of ATPeriodDTO))
            ViewState(Me.ID & "_Period") = value
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

    'Property IsLoad As Boolean
    '    Get
    '        Return ViewState(Me.ID & "_IsLoad")
    '    End Get
    '    Set(ByVal value As Boolean)
    '        ViewState(Me.ID & "_IsLoad") = value
    '    End Set
    'End Property

#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
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
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            rgData.ClientSettings.EnablePostBackOnRowClick = True
            SetGridFilter(rgData)
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load cac control, menubar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMenu
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Export,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel,
                                       ToolbarItem.Active,
                                       ToolbarItem.Deactive,
                                       ToolbarItem.Delete)

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim dic As New Dictionary(Of String, Control)
            
            Utilities.OnClientRowSelectedChanged(rgData, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        'Dim rep As New ProfileBusinessRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            UpdateCotrolEnabled(False)
            EnabledGridNotPostback(rgData, True)
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    UpdateCotrolEnabled(False)
                    ClearControlValue(nmrYear, dpBonusDate, txtPeriodStanDard, txtPeriodName, txtRemark, dpStartDate, dpEndDate)
                Case CommonMessage.STATE_NEW
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    UpdateCotrolEnabled(True)
                    ClearControlValue(nmrYear, dpBonusDate, txtPeriodStanDard, txtPeriodName, txtRemark, dpStartDate, dpEndDate)
                    'txtPeriodStanDard.Value = 26
                    EnabledGridNotPostback(rgData, False)
                Case CommonMessage.STATE_EDIT
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    UpdateCotrolEnabled(True)
                    EnabledGridNotPostback(rgData, False)
                Case CommonMessage.STATE_DELETE
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    UpdateCotrolEnabled(False)
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.ActivePeriod(lstDeletes, "A") Then
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
                        If rep.ActivePeriod(lstDeletes, "I") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgData.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                        End If
                    End Using
            End Select
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Update trang thai enable cua cac control tren man hinh theo tham so truyen vao
    ''' </summary>
    ''' <param name="bCheck"></param>
    ''' <remarks></remarks>
    Private Sub UpdateCotrolEnabled(ByVal bCheck As Boolean)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ctrlOrg.Enabled = bCheck
            nmrYear.Enabled = bCheck
            txtPeriodName.Enabled = bCheck
            EnableRadDatePicker(dpStartDate, bCheck)
            EnableRadDatePicker(dpEndDate, bCheck)
            EnableRadDatePicker(dpBonusDate, bCheck)
            txtRemark.Enabled = bCheck
            txtPeriodStanDard.Enabled = bCheck
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                rgData.Rebind()
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "U"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "N"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                    Case "C"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objPeriod As New ATPeriodDTO
        Dim objOrgPeriod As New List(Of AT_ORG_PERIOD)
        Dim rep As New PayrollRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    rgData.MasterTableView.ClearChildSelectedItems()
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
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    rgData.MasterTableView.ClearChildSelectedItems()
                    UpdateControlState()
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
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = Me.vPeriod.ToTable()
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "SetUpSalary")
                            Exit Sub
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objPeriod.YEAR = nmrYear.Value
                        objPeriod.MONTH = dpEndDate.SelectedDate.Value.Month
                        objPeriod.PERIOD_NAME = txtPeriodName.Text
                        objPeriod.PERIOD_STANDARD = txtPeriodStanDard.Value
                        objPeriod.START_DATE = dpStartDate.SelectedDate
                        objPeriod.END_DATE = dpEndDate.SelectedDate
                        objPeriod.BONUS_DATE = dpBonusDate.SelectedDate
                        objPeriod.REMARK = txtRemark.Text
                        For Each item As Decimal In ctrlOrg.CheckedValueKeys
                            Dim objOrg As New AT_ORG_PERIOD
                            objOrg.ORG_ID = item
                            objOrgPeriod.Add(objOrg)
                        Next
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objPeriod.ACTFLG = "A"
                                If rep.ValidateATPeriodDay(objPeriod) Then
                                    If rep.InsertPeriod(objPeriod, objOrgPeriod, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = gID
                                        Refresh("N")
                                        UpdateControlState()
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                                Else
                                    ShowMessage(Translate("Không được phép tạo 2 kỳ lương 1 tháng"), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objPeriod.ID = rgData.SelectedValue

                                If rep.ModifyPeriod(objPeriod, objOrgPeriod, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objPeriod.ID
                                    Refresh("U")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Event Yes, No tren popup message khi click nut: xoa, ap dung, ngung ap dung
    ''' va Set trang thai cho form va update trang thai control 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New PayrollRepository
        Dim objDelete As ATPeriodDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                objDelete = New ATPeriodDTO
                objDelete.ID = Utilities.ObjToDecima(rgData.SelectedValue)
                If Not rep.ValidateATPeriod(objDelete) Then
                    ShowMessage("Kỳ lương đã được sử dụng ở các phòng ban. Bạn không thể xóa!", NotifyType.Warning)
                    Exit Sub
                Else
                    If rep.DeletePeriod(objDelete) Then
                        objDelete = Nothing
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                End If
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
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Tao du lieu mau cho grid
    ''' </summary>
    ''' <remarks></remarks>
    Sub CreatDataTemp()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim dt As New DataTable()
            dt.Columns.Add("ID")
            dt.Columns.Add("YEAR")
            dt.Columns.Add("MONTH")
            dt.Columns.Add("PERIOD_NAME")
            dt.Columns.Add("PERIOD_STANDARD")
            dt.Columns.Add("START_DATE", GetType(Date))
            dt.Columns.Add("START_DATE", GetType(Date))
            dt.Columns.Add("BONUS_DATE", GetType(Date))
            dt.Columns.Add("REMARK")

            dt.Rows.Add(1, 2016, 1, "Tháng 1 - 2016", 26, New Date(2016, 1, 1), New Date(2016, 1, 31), New Date(2016, 1, 31), "")
            dt.Rows.Add(1, 2016, 1, "Tháng 2 - 2016", 24, New Date(2016, 2, 1), New Date(2016, 2, 29), New Date(2016, 2, 29), "")
            dt.Rows.Add(1, 2016, 1, "Tháng 3 - 2016", 26, New Date(2016, 3, 1), New Date(2016, 3, 31), New Date(2016, 3, 31), "")

            rgData.VirtualItemCount = 3
            rgData.DataSource = dt
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter()
            'CreatDataTemp()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgData_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rgData.SelectedIndexChanged
        Dim lstObject As New List(Of AT_ORG_PERIOD)
        Dim lstId As New List(Of Decimal)
        Try
            Dim item1 = CType(rgData.SelectedItems(0), GridDataItem)
            Dim ID = item1.GetDataKeyValue("ID").ToString
            Dim At_Period = (From p In vPeriod Where p.ID = Decimal.Parse(ID)).SingleOrDefault
            nmrYear.Value = At_Period.YEAR
            txtPeriodName.Text = At_Period.PERIOD_NAME
            txtPeriodStanDard.Value = At_Period.PERIOD_STANDARD
            txtRemark.Text = At_Period.REMARK
            dpStartDate.SelectedDate = At_Period.START_DATE
            dpEndDate.SelectedDate = At_Period.END_DATE
            dpBonusDate.SelectedDate = At_Period.BONUS_DATE

            For Each LINE As GridDataItem In rgData.SelectedItems
                Using rep As New PayrollRepository
                    lstObject = rep.GetListOrgPeriod(Decimal.Parse(LINE.GetDataKeyValue("ID")))
                    For Each item In lstObject
                        lstId.Add(item.ORG_ID)
                    Next
                    ctrlOrg.CheckedValueKeys = lstId
                End Using
            Next
        Catch ex As Exception

        End Try
    End Sub

    ''' <lastupdate>
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien custom validate cua cvalEffedate
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalEffedate_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalEffedate.ServerValidate
        Dim rep As New PayrollRepository
        Dim _validate As New ATPeriodDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            _validate.ID = rgData.SelectedValue
            _validate.YEAR = nmrYear.Value
            _validate.START_DATE = dpStartDate.SelectedDate
            _validate.END_DATE = dpEndDate.SelectedDate
            If CurrentState = CommonMessage.STATE_EDIT Then
                args.IsValid = rep.ValidateATPeriodDay(_validate)
            Else
                args.IsValid = rep.ValidateATPeriodDay(_validate)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien PreRender cho rad grid - click row
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rgData_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If (CurrentState <> CommonMessage.STATE_NEW And (rgData.SelectedItems.Count = 0 Or rgData.SelectedItems.Count > 1)) Then
                ClearControlValue(nmrYear, dpBonusDate, txtPeriodStanDard, txtPeriodName, txtRemark, dpStartDate, dpEndDate)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CreateDataFilter()
        Dim _filter As New ATPeriodDTO
        Dim rep As New PayrollRepository
        Dim bCheck As Boolean = False
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SetValueObjectByRadGrid(rgData, _filter)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                Me.vPeriod = rep.GetPeriodList(rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
            Else
                Me.vPeriod = rep.GetPeriodList(rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
            End If

            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = Me.vPeriod
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly goi ham thay doi state cua toolbar
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
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    'TỰ TÍNH NGÀY CÔNG CHUẨN

#End Region

    Private Sub dpStartDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles dpStartDate.SelectedDateChanged
        Try
            If dpStartDate.SelectedDate IsNot Nothing And dpEndDate.SelectedDate IsNot Nothing Then
                CalDayStanrd()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dpEndDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles dpEndDate.SelectedDateChanged
        Try
            If dpStartDate.SelectedDate IsNot Nothing And dpEndDate.SelectedDate IsNot Nothing Then
                CalDayStanrd()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CalDayStanrd()
        Try
            Dim countHoliday As Decimal = 0
            Dim countSunday As Decimal = 0
            Dim sumDay As Decimal = 0
            Dim selectedFromDate = dpStartDate.SelectedDate
            Dim selectedToDate = dpEndDate.SelectedDate
            Dim rep As New PayrollRepository
            countHoliday = rep.CountHoliday(dpStartDate.SelectedDate, dpEndDate.SelectedDate)
            While selectedFromDate.Value.Date <= selectedToDate.Value.Date
                Dim d As Date = selectedFromDate
                If d.DayOfWeek = DayOfWeek.Sunday Then
                    countSunday += 1
                End If
                sumDay += 1
                selectedFromDate = selectedFromDate.Value.AddDays(1)
            End While
            txtPeriodStanDard.Value = sumDay - countHoliday - countSunday
        Catch ex As Exception

        End Try
    End Sub

End Class
Imports WebAppLog
Imports Common
Imports Telerik.Web.UI
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness

Public Class ctrlPA_TaxFinalization
    Inherits Common.CommonView

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Payroll/Module/Payroll/Setting/" + Me.GetType().Name.ToString()


#Region "Property"

    Public Property vPeriod As List(Of PATaxFinalizationDTO)
        Get
            Return ViewState(Me.ID & "_Period")
        End Get
        Set(ByVal value As List(Of PATaxFinalizationDTO))
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

#End Region

#Region "Page"

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
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel,
                                       ToolbarItem.Active,
                                       ToolbarItem.Deactive,
                                       ToolbarItem.Delete)
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("YEAR", rdnYear)
            dic.Add("TAX_FINALIZATION_NAME", txtTaxName)
            Utilities.OnClientRowSelectedChanged(rgData, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

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
                    ClearControlValue(rdnYear, txtTaxName, rdFromMonth, txtRemark, rdEndMonth, rdnYearperiod, cboPeriod)
                Case CommonMessage.STATE_NEW
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    UpdateCotrolEnabled(True)
                    ClearControlValue(rdnYear, txtTaxName, rdFromMonth, txtRemark, rdEndMonth, rdnYearperiod, cboPeriod)
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
                        If rep.ActiveTaxFinalization(lstDeletes, "A") Then
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
                        If rep.ActiveTaxFinalization(lstDeletes, "I") Then
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

    ''' <summary>
    ''' Update trang thai enable cua cac control tren man hinh theo tham so truyen vao
    ''' </summary>
    ''' <param name="bCheck"></param>
    ''' <remarks></remarks>
    Private Sub UpdateCotrolEnabled(ByVal bCheck As Boolean)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rdnYear.Enabled = bCheck
            rdnYearperiod.Enabled = bCheck
            txtTaxName.Enabled = bCheck
            EnableRadMonthYearPicker(rdFromMonth, bCheck)
            EnableRadMonthYearPicker(rdEndMonth, bCheck)
            txtRemark.Enabled = bCheck
            rdnYearperiod.Enabled = bCheck
            cboPeriod.Enabled = bCheck
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

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

    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objPeriod As New PATaxFinalizationDTO
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
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objPeriod.YEAR = rdnYear.Value
                        objPeriod.TAX_FINALIZATION_NAME = txtTaxName.Text
                        objPeriod.START_DATE = rdFromMonth.SelectedDate
                        objPeriod.START_DATE_MONTH = rdFromMonth.SelectedDate.Value.Month
                        objPeriod.START_DATE_YEAR = rdFromMonth.SelectedDate.Value.Year
                        objPeriod.END_DATE = rdEndMonth.SelectedDate
                        objPeriod.END_DATE_MONTH = rdEndMonth.SelectedDate.Value.Month
                        objPeriod.END_DATE_YEAR = rdEndMonth.SelectedDate.Value.Year
                        objPeriod.YEAR_PERIOD = rdnYearperiod.Value
                        objPeriod.PERIOD_ID = cboPeriod.SelectedValue
                        objPeriod.REMARK = txtRemark.Text
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objPeriod.ACTFLG = "A"
                                If rep.InsertTaxFinalization(objPeriod, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("N")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objPeriod.ID = rgData.SelectedValue

                                If rep.ModifyTaxFinalization(objPeriod, gID) Then
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

    ''' <summary>
    ''' Event Yes, No tren popup message khi click nut: xoa, ap dung, ngung ap dung
    ''' va Set trang thai cho form va update trang thai control 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New PayrollRepository
        Dim objDelete As PATaxFinalizationDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                objDelete = New PATaxFinalizationDTO
                objDelete.ID = Utilities.ObjToDecima(rgData.SelectedValue)

                If rep.DeleteTaxFinalization(objDelete) Then
                    objDelete = Nothing
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgData.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            End If
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
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
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

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
                ClearControlValue(rdnYear, txtTaxName, rdFromMonth, txtRemark, rdEndMonth, rdnYearperiod, cboPeriod)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CreateDataFilter()
        Dim _filter As New PATaxFinalizationDTO
        Dim rep As New PayrollRepository
        Dim bCheck As Boolean = False
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SetValueObjectByRadGrid(rgData, _filter)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                Me.vPeriod = rep.GetTaxFinalizationList(rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
            Else
                Me.vPeriod = rep.GetTaxFinalizationList(rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
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

    Protected Sub rgData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgData.SelectedIndexChanged

        Dim item = TryCast(rgData.SelectedItems(0), GridDataItem)
        If item IsNot Nothing Then
            Dim ID = item.GetDataKeyValue("ID").ToString
            Dim obj = (From p In vPeriod Where p.ID = Decimal.Parse(ID)).SingleOrDefault
            rdnYearperiod.Text = obj.YEAR_PERIOD
            Using rep As New PayrollRepository
                FillDropDownList(cboPeriod, rep.GetPeriodbyYear(obj.YEAR_PERIOD), "PERIOD_NAME", "ID", Common.Common.SystemLanguage, False, cboPeriod.SelectedValue)
            End Using
            cboPeriod.SelectedValue = obj.PERIOD_ID
            rdFromMonth.SelectedDate = obj.START_DATE
            rdEndMonth.SelectedDate = obj.END_DATE
            txtRemark.Text = obj.REMARK
            rdnYear.Value = obj.YEAR
            txtTaxName.Text = obj.TAX_FINALIZATION_NAME
        End If
    End Sub

    Protected Sub rdnYearperiod_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rdnYearperiod.TextChanged
        If rdnYearperiod.Value IsNot Nothing Then
            Using rep As New PayrollRepository
                FillDropDownList(cboPeriod, rep.GetPeriodbyYear(rdnYearperiod.Value), "PERIOD_NAME", "ID", Common.Common.SystemLanguage, False, cboPeriod.SelectedValue)
            End Using
        End If
    End Sub

    Private Sub EnableRadMonthYearPicker(ByVal _radDatePicker As RadMonthYearPicker, ByVal _enable As Boolean)
        _radDatePicker.Enabled = _enable
        _radDatePicker.DateInput.ReadOnly = Not _enable
        _radDatePicker.Enabled = _enable
    End Sub

#End Region

End Class
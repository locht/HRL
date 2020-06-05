Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Telerik.Web.UI
Imports Common.Common
Imports Common.CommonMessage
Imports Attendance.AttendanceRepository
Imports Attendance.AttendanceBusiness
Imports WebAppLog
Imports System.IO
Imports HistaffFrameworkPublic
Imports System.Globalization
Imports Profile.ProfileBusiness

Public Class ctrlShiftRegMng
    Inherits Common.CommonView
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()
#Region "Properties"

    Private Property dtLogs As DataTable
        Get
            Return PageViewState(Me.ID & "_dtLogs")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtLogs") = value
        End Set
    End Property

    Private Property lst_ShiftReg As List(Of AtShiftRegMngDTO)
        Get
            Return PageViewState(Me.ID & "_lst_ShiftReg")
        End Get
        Set(value As List(Of AtShiftRegMngDTO))
            PageViewState(Me.ID & "_lst_ShiftReg") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ctrlOrganization.AutoPostBack = True
            ctrlOrganization.LoadDataAfterLoaded = True
            ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrganization.CheckBoxes = TreeNodeTypes.None

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
    ''' Khởi tạo page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            SetGridFilter(rgShiftReg)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgShiftReg.AllowCustomPaging = True

            rgShiftReg.ClientSettings.EnablePostBackOnRowClick = False
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Khởi tạo control, menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Calculate, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Export,
                                       ToolbarItem.Delete)
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load data to combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim lsData As List(Of AT_PERIODDTO)
            Dim rep As New AttendanceRepository
            Dim table As New DataTable
            table.Columns.Add("YEAR", GetType(Integer))
            table.Columns.Add("ID", GetType(Integer))
            Dim row As DataRow
            For index = 2015 To Date.Now.Year + 1
                row = table.NewRow
                row("ID") = index
                row("YEAR") = index
                table.Rows.Add(row)
            Next
            FillRadCombobox(cboYear, table, "YEAR", "ID")
            cboYear.SelectedValue = Date.Now.Year
            Dim period As New AT_PERIODDTO
            period.ORG_ID = 1
            period.YEAR = Date.Now.Year
            lsData = rep.LOAD_PERIODBylinq(period)
            FillRadCombobox(cboPeriod, lsData, "PERIOD_NAME", "PERIOD_ID", True)
            If lsData.Count > 0 Then
                Dim periodid = (From d In lsData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                    rdtungay.SelectedDate = periodid.START_DATE
                    rdDenngay.SelectedDate = periodid.END_DATE
                Else
                    cboPeriod.SelectedIndex = lsData.Count - 1
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Update trạng thái của control theo trạng thái page
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim rep As New AttendanceRepository
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgShiftReg.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgShiftReg.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteShiftReg(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            UpdateToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Event cboYear SelectedIndexChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtData As List(Of AT_PERIODDTO)
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO
        Try
            period.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            dtData = rep.LOAD_PERIODBylinq(period)
            cboPeriod.ClearSelection()
            FillRadCombobox(cboPeriod, dtData, "PERIOD_NAME", "PERIOD_ID", True)
            If dtData.Count > 0 Then
                Dim periodid = (From d In dtData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                    rdtungay.SelectedDate = periodid.START_DATE
                    rdDenngay.SelectedDate = periodid.END_DATE
                    rgShiftReg.Rebind()
                Else
                    cboPeriod.SelectedIndex = dtData.Count - 1
                    Dim per = (From c In dtData Where c.PERIOD_ID = cboPeriod.SelectedValue).FirstOrDefault
                    rdtungay.SelectedDate = per.START_DATE
                    rdDenngay.SelectedDate = per.END_DATE
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Event cboPeriod SelectedIndexChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriod.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO
        Try
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            period.ORG_ID = 46
            If cboPeriod.SelectedValue <> "" Then
                Dim Lstperiod = rep.LOAD_PERIODBylinq(period)

                Dim p = (From o In Lstperiod Where o.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)).FirstOrDefault
                rdtungay.SelectedDate = p.START_DATE
                rdDenngay.SelectedDate = p.END_DATE

            End If
            rgShiftReg.Rebind()
            ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Reload page
    ''' </summary>
    ''' <param name="Message"></param>
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
                        rgShiftReg.Rebind()
                        'SelectedItemDataGridByKey(rgSignWork, IDSelect, , rgSignWork.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgShiftReg.CurrentPageIndex = 0
                        rgShiftReg.MasterTableView.SortExpressions.Clear()
                        rgShiftReg.Rebind()
                        'SelectedItemDataGridByKey(rgSignWork, IDSelect, )
                    Case "Cancel"
                        rgShiftReg.MasterTableView.ClearSelectedItems()
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
#End Region

#Region "Event"
    ''' <summary>
    ''' Event ctrlOrganization SelectedNodeChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If Not IsPostBack Then
                ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
            End If
            rgShiftReg.CurrentPageIndex = 0
            rgShiftReg.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Event click item menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim rep As New AttendanceRepository
            Dim _param = New Attendance.AttendanceBusiness.ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue), _
                                            .PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue),
                                            .IS_DISSOLVE = ctrlOrganization.IsDissolve}

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARTIEM_CALCULATE
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thêm"), NotifyType.Error)
                        Exit Sub
                    End If
                    Dim lsEmployee As New List(Of Decimal?)
                    If rep.Calculate_Shift_Reg(_param, Decimal.Parse(cboPeriod.SelectedValue),
                                                   Decimal.Parse(ctrlOrganization.CurrentValue), lsEmployee) Then
                        Refresh("UpdateView")
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Success)
                        Exit Sub
                    End If
                Case TOOLBARITEM_DELETE
                    ' kiem tra ky cong da dong chua?
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    'Kiểm tra các điều kiện để xóa.
                    If rgShiftReg.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi xóa"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgShiftReg.ExportExcel(Server, Response, dtDatas, "ShiftRegMng")
                        End If
                    End Using
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
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
    ''' Load DB to grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim _filter As New AtShiftRegMngDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgShiftReg, _filter)
            Dim _param = New Attendance.AttendanceBusiness.ParamDTO With {.ORG_ID = ctrlOrganization.CurrentValue, _
                                           .IS_DISSOLVE = ctrlOrganization.IsDissolve,
                                           .IS_FULL = True}
            Dim Sorts As String = rgShiftReg.MasterTableView.SortExpressions.GetSortString()

            If rdtungay.SelectedDate IsNot Nothing Then
                _filter.FROM_DATE = rdtungay.SelectedDate
            End If
            If rdDenngay.SelectedDate IsNot Nothing Then
                _filter.TO_DATE = rdDenngay.SelectedDate
            End If
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.lst_ShiftReg = rep.GetAtShiftRegMng(_filter, _param, MaximumRows, rgShiftReg.CurrentPageIndex, rgShiftReg.PageSize, "EMPLOYEE_CODE desc")
                Else
                    Me.lst_ShiftReg = rep.GetAtShiftRegMng(_filter, _param, MaximumRows, rgShiftReg.CurrentPageIndex, rgShiftReg.PageSize)
                End If
            Else
                Return rep.GetAtShiftRegMng(_filter, _param).ToTable
            End If

            rgShiftReg.VirtualItemCount = MaximumRows
            rgShiftReg.DataSource = Me.lst_ShiftReg
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
    ''' <summary>
    ''' Reload, Refresh grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgShiftReg.NeedDataSource
        Try
            CreateDataFilter(False)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Event Click button tìm kiếm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            'If String.IsNullOrEmpty(cboPeriod.SelectedValue) And rdTungay.SelectedDate.HasValue = False And rdDenngay.SelectedDate.HasValue = False Then
            '    ShowMessage(Translate("Kỳ công chưa được chọn"), NotifyType.Error)
            '    Exit Sub
            'End If
            rgShiftReg.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub rgShiftReg_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgShiftReg.PageIndexChanged
        Try
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim url = e.Argument
            If (url.Contains("reload=1")) Then
                rgShiftReg.CurrentPageIndex = 0
                rgShiftReg.Rebind()
                If rgShiftReg.Items IsNot Nothing AndAlso rgShiftReg.Items.Count > 0 Then
                    rgShiftReg.Items(0).Selected = True
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    


    
#End Region

#Region "Custom"
    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region
End Class
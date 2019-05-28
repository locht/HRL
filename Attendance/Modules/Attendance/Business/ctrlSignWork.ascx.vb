﻿Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Telerik.Web.UI
Imports Common.Common
Imports Common.CommonMessage
Imports Attendance.AttendanceBusiness
Imports Attendance.AttendanceRepository
Imports WebAppLog
Imports System.ComponentModel
Imports System.Reflection


Public Class ctrlSignWork
    Inherits Common.CommonView
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String

    Dim psp As New AttendanceStoreProcedure
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()

#Region "Properties"
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Danh sách tham số 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PARAM_ATDTO As List(Of AT_WORKSIGNDTO)
        Get
            Return ViewState(Me.ID & "_PARAM_ATDTO")
        End Get
        Set(ByVal value As List(Of AT_WORKSIGNDTO))
            ViewState(Me.ID & "_PARAM_ATDTO") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' trạng thái load popup cho biết popup nào sẽ được load lên
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Tạo DataTable lưu trữ thông tin ca làm việc của nhân viên 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("VN_FULLNAME", GetType(String))
                dt.Columns.Add("TITLE_NAME", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("ORG_PATH", GetType(String))
                dt.Columns.Add("D1", GetType(String))
                dt.Columns.Add("D2", GetType(String))
                dt.Columns.Add("D3", GetType(String))
                dt.Columns.Add("D4", GetType(String))
                dt.Columns.Add("D5", GetType(String))
                dt.Columns.Add("D6", GetType(String))
                dt.Columns.Add("D7", GetType(String))
                dt.Columns.Add("D8", GetType(String))
                dt.Columns.Add("D9", GetType(String))
                dt.Columns.Add("D10", GetType(String))

                dt.Columns.Add("D11", GetType(String))
                dt.Columns.Add("D12", GetType(String))
                dt.Columns.Add("D13", GetType(String))
                dt.Columns.Add("D14", GetType(String))
                dt.Columns.Add("D15", GetType(String))
                dt.Columns.Add("D16", GetType(String))
                dt.Columns.Add("D17", GetType(String))
                dt.Columns.Add("D18", GetType(String))
                dt.Columns.Add("D19", GetType(String))
                dt.Columns.Add("D20", GetType(String))

                dt.Columns.Add("D21", GetType(String))
                dt.Columns.Add("D22", GetType(String))
                dt.Columns.Add("D23", GetType(String))
                dt.Columns.Add("D24", GetType(String))
                dt.Columns.Add("D25", GetType(String))
                dt.Columns.Add("D26", GetType(String))
                dt.Columns.Add("D27", GetType(String))
                dt.Columns.Add("D28", GetType(String))
                dt.Columns.Add("D29", GetType(String))
                dt.Columns.Add("D30", GetType(String))
                dt.Columns.Add("D31", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property
#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức load dữ liệu cho trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh()
            ctrlOrganization.AutoPostBack = True
            ctrlOrganization.LoadDataAfterLoaded = True
            ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrganization.CheckBoxes = TreeNodeTypes.None
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo các thiết lập, control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            SetGridFilter(rgSignWork)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgSignWork.AllowCustomPaging = True

            rgSignWork.ClientSettings.EnablePostBackOnRowClick = False
            ctrlUpload1.isMultiple = AsyncUpload.MultipleFileSelection.Disabled
            ctrlUpload1.MaxFileInput = 1
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức thiết lập toolbar, popup message
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       -99,
                                       ToolbarItem.Export,
                                       ToolbarItem.Delete)
            MainToolBar.Items(0).Text = Translate("Cập nhật")
            MainToolBar.Items(1).Text = Translate("Lấy ca mặc định")

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("EXPORT_TEMP",
                                                                     ToolbarIcons.Export,
                                                                     ToolbarAuthorize.Export,
                                                                     Translate("Xuất file mẫu")))

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("IMPORT_TEMP",
                                                                     ToolbarIcons.Import,
                                                                     ToolbarAuthorize.Import,
                                                                     Translate("Nhập file mẫu")))

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
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức bind dữ liệu lên form thêm mới, sửa
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
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
            FillRadCombobox(cboPeriodId, lsData, "PERIOD_NAME", "PERIOD_ID", True)
            If lsData.Count > 0 Then
                Dim periodid = (From d In lsData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriodId.SelectedValue = periodid.PERIOD_ID.ToString()
                Else
                    cboPeriodId.SelectedIndex = 0
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Làm mới các thiết lập cho các control trên trang
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New AttendanceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgSignWork.Rebind()
                        'SelectedItemDataGridByKey(rgSignWork, IDSelect, , rgSignWork.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgSignWork.CurrentPageIndex = 0
                        rgSignWork.MasterTableView.SortExpressions.Clear()
                        rgSignWork.Rebind()
                        'SelectedItemDataGridByKey(rgSignWork, IDSelect, )
                    Case "Cancel"
                        rgSignWork.MasterTableView.ClearSelectedItems()
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
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật trạng thái cho các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_CHECK

                    Dim _param = New ParamDTO With {.ORG_ID = ctrlOrganization.CurrentValue, _
                                                    .IS_DISSOLVE = ctrlOrganization.IsDissolve, _
                                                    .FROMDATE = Date.Now,
                                                    .ENDDATE = Date.Now,
                                                    .PERIOD_ID = Decimal.Parse(cboPeriodId.SelectedValue)}
                    rep.GETSIGNDEFAULT(_param)
                    Refresh("UpdateView")

            End Select
            phPopup.Controls.Clear()
            Select Case isLoadPopup
                Case 1
                    ctrlOrgPopup = Me.Register("ctrlOrgPopup", "Common", "ctrlFindOrgPopup")
                    phPopup.Controls.Add(ctrlOrgPopup)
            End Select
            UpdateToolbarState()
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
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedNodeChanged cho control ctrlOrganization
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                ctrlOrganization.SetColorPeriod(cboPeriodId.SelectedValue, PeriodType.AT)
            End If
            rgSignWork.CurrentPageIndex = 0
            rgSignWork.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện Command khi click main toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objSign As New AT_WORKSIGNDTO
        Dim rep As New AttendanceRepository

        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue), _
                                            .PERIOD_ID = Decimal.Parse(cboPeriodId.SelectedValue),
                                            .IS_DISSOLVE = ctrlOrganization.IsDissolve}

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    If rgSignWork.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgSignWork.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case "EXPORT_TEMP"
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    isLoadPopup = 1 'Chọn Org
                    UpdateControlState()
                    ctrlOrgPopup.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As New DataTable
                        dtDatas = CreateDataFilter(True)
                        If (dtDatas IsNot Nothing) Then
                            If (dtDatas.Rows IsNot Nothing) Then
                                If dtDatas.Rows.Count > 0 Then
                                    rgSignWork.ExportExcel(Server, Response, dtDatas, "SignWork")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                                    Exit Sub
                                End If
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                                Exit Sub
                            End If
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        End If
                       
                    End Using
                Case "IMPORT_TEMP"
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    ctrlUpload1.Show()
                Case CommonMessage.TOOLBARITEM_RESET
                    If cboPeriodId.SelectedValue Is Nothing Then
                        ShowMessage(Translate("Kỳ công chưa được chọn"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate("Bạn có chắc chắn cập nhật lại ca mặc định?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_CHECK
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
                Case TOOLBARITEM_SAVE
                    If rep.IS_PERIODSTATUS(_param) = False Then
                        ShowMessage(Translate("Kỳ công đã đóng, bạn không thể thực hiện thao tác này"), NotifyType.Error)
                        Exit Sub
                    End If
                    saveGrid()
                    Refresh("InsertView")
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                Case TOOLBARITEM_DELETE
                    If rgSignWork.SelectedItems.Count > 0 Then
                        For idx = 0 To rgSignWork.SelectedItems.Count - 1
                            Dim item As GridDataItem = rgSignWork.SelectedItems(idx)
                            Dim slItem As GridDataItem
                            Dim id As String
                            slItem = rgSignWork.SelectedItems(idx)
                            id = slItem("ID").Text
                            psp.CHECK_SEND_APPROVE(id)
                        Next
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
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
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Lưu dữ liệu vào db
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function saveGrid() As Boolean
        Dim rep As New AttendanceRepository
        Dim lstData As New List(Of AT_WORKSIGNDTO)
        Dim gId As New Decimal
        gId = 0
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dtData.TableName = "Data"
            rep.InsertWORKSIGNByImport(dtData, cboPeriodId.SelectedValue)
            Return True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Xử lý sự kiện command khi click button yes/no
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_CHECK And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_CHECK
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
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện NeedDataSource cho rad grid rgSignWork
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgSignWork.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter(False)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện click khi click btnSearch
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearchEmp.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgSignWork.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                          CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Hàm tạo dữ liệu filter cho grid
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim startdate As Date
            Dim enddate As Date
            Dim rep = New AttendanceRepository

            Dim _filter As New AT_WORKSIGNDTO
            If Not String.IsNullOrEmpty(cboPeriodId.SelectedValue) Then
                Dim obj As New AT_PERIODDTO
                obj.PERIOD_ID = Decimal.Parse(cboPeriodId.SelectedValue)
                _filter.PERIOD_ID = obj.PERIOD_ID
                Dim ddate = rep.LOAD_PERIODByID(obj)
                startdate = ddate.START_DATE
                enddate = ddate.END_DATE
            End If
            For i = 1 To 31
                If startdate <= enddate Then
                    rgSignWork.MasterTableView.GetColumn("D" & i).HeaderText = startdate.ToString("dd/MM/yyyy")
                    rgSignWork.MasterTableView.GetColumn("D" & i).Visible = True
                    startdate = startdate.AddDays(1)
                Else
                    rgSignWork.MasterTableView.GetColumn("D" & i).Visible = False
                End If
            Next
            SetValueObjectByRadGrid(rgSignWork, _filter)
            _filter.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
            _filter.IS_DISSOLVE = ctrlOrganization.IsDissolve
            Dim Sorts As String = rgSignWork.MasterTableView.SortExpressions.GetSortString()
            If Not isFull Then
                Dim ds = rep.GET_WORKSIGN(_filter)
                If ds IsNot Nothing Then
                    Dim tableSignWork = ds.Tables(0)
                    rgSignWork.VirtualItemCount = Decimal.Parse(ds.Tables(1).Rows(0)("TOTAL"))
                    rgSignWork.DataSource = tableSignWork
                Else
                    rgSignWork.VirtualItemCount = 0
                    rgSignWork.DataSource = New DataTable
                End If
            Else
                _filter.PAGE_INDEX = 1
                _filter.PAGE_SIZE = Integer.MaxValue
                Return rep.GET_WORKSIGN(_filter).Tables(0)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện AjaxRequest
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim url = e.Argument
            If (url.Contains("reload=1")) Then
                rgSignWork.CurrentPageIndex = 0
                rgSignWork.Rebind()
                If rgSignWork.Items IsNot Nothing AndAlso rgSignWork.Items.Count > 0 Then
                    rgSignWork.Items(0).Selected = True
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedNodeChanged cho ctrlYear
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlYear_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim dtData As List(Of AT_PERIODDTO)
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            period.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            dtData = rep.LOAD_PERIODBylinq(period)
            cboPeriodId.ClearSelection()

            FillRadCombobox(cboPeriodId, dtData, "PERIOD_NAME", "PERIOD_ID", True)
            If dtData.Count > 0 Then
                Dim periodid = (From d In dtData Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault
                If periodid IsNot Nothing Then
                    cboPeriodId.SelectedValue = periodid.PERIOD_ID.ToString()
                Else
                    cboPeriodId.SelectedIndex = 0
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedIndexChanged cho combobox cboPeriodId
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboPeriodId_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriodId.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgSignWork.Rebind()
            ctrlOrganization.SetColorPeriod(cboPeriodId.SelectedValue, PeriodType.AT)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Hàm load grid với header định trước
    ''' </summary>
    ''' <param name="dtDataHeader"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function loadToGrid(dtDataHeader As DataTable) As Boolean
        Dim dtError As New DataTable("ERROR")
        Dim dtDatas As AT_PERIODDTO
        Dim rep As New AttendanceRepository
        Dim period As New AT_PERIODDTO
        Dim rowError As DataRow
        Dim isError As Boolean = False
        Dim sError As String = String.Empty
        Dim irow = 6
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If dtData.Rows.Count = 0 Then
                ShowMessage("File mẫu không đúng định dạng", NotifyType.Warning)
                Return False
            End If
            dtError = dtData.Clone
            dtError.Columns.Add("STT")
            period.PERIOD_ID = Decimal.Parse(cboPeriodId.SelectedValue)
            dtDatas = rep.LOAD_PERIODByID(period)
            Dim dtShift = rep.GetAT_SHIFT(New AT_SHIFTDTO)
            For Each row As DataRow In dtData.Rows
                Dim isRow = ImportValidate.TrimRow(row)
                If Not isRow Then
                    irow += 1
                    Continue For
                End If
                isError = False
                ImportValidate.TrimRow(row)
                rowError = dtError.NewRow
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)

                sError = "Mã nhân viên không phải hệ thống chiết xuất"
                ImportValidate.IsValidNumber("EMPLOYEE_ID", row, rowError, isError, sError)
                If rowError("EMPLOYEE_ID").ToString <> "" Then
                    rowError("EMPLOYEE_CODE") = sError
                End If
                Dim index As Integer = 0
                For index = 1 To (dtDatas.END_DATE - dtDatas.START_DATE).Value.TotalDays + 1
                    If row("D" & index).ToString <> "" Then
                        Dim r = row("D" & index).ToString
                        Dim exists = (From p In dtShift Where p.CODE.ToUpper = r.ToUpper).FirstOrDefault

                        If exists Is Nothing Then
                            rowError("D" & index) = row("D" & index).ToString & " không tồn tại"
                            isError = True
                        Else
                            row("D" & index) = ""
                            Dim workingDate As Date = dtDatas.START_DATE.Value.AddDays(index - 1)
                            If IsNumeric(exists.ID) Then
                                row("D" & index) = exists.ID
                            Else
                                If workingDate.DayOfWeek = DayOfWeek.Sunday And exists.SUNDAY.HasValue Then
                                    row("D" & index) = 81 ' OFF
                                Else
                                    row("D" & index) = exists.ID
                                End If
                            End If
                            
                        End If
                    Else

                    End If
                Next
                If index = 28 Then
                    row("D29") = ""
                    row("D30") = ""
                    row("D31") = ""
                End If
                If index = 29 Then
                    row("D30") = ""
                    row("D31") = ""
                End If
                If index = 30 Then
                    row("D31") = ""
                End If

                If isError Then
                    rowError("STT") = irow
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    rowError("VN_FULLNAME") = row("VN_FULLNAME").ToString
                    rowError("ORG_NAME") = row("ORG_NAME").ToString
                    rowError("ORG_PATH") = row("ORG_PATH").ToString
                    rowError("TITLE_NAME") = row("TITLE_NAME").ToString
                    dtError.Rows.Add(rowError)
                End If
                irow += 1
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                dtDataHeader.TableName = "DATA_HEADER"
                Dim dsData As New DataSet
                dsData.Tables.Add(dtError)
                dsData.Tables.Add(dtDataHeader)
                Session("EXPORTREPORT") = dsData
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_ImportShift_error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                Return False
            End If
            Return True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện OrganizationSelected cho control ctrlOrgPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If String.IsNullOrEmpty(cboPeriodId.SelectedValue) Then
                ShowMessage(Translate("Kỳ công chưa được chọn"), NotifyType.Error)
                Exit Sub
            End If
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('WorkShiftImport&PERIOD_ID=" & cboPeriodId.SelectedValue & "&orgid=" & e.CurrentValue & "&IS_DISSOLVE=" & IIf(ctrlOrgPopup.IsDissolve, "1", "0") & "')", True)
            isLoadPopup = 0
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện CancelClicked của ctrlOrgPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrgPopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 0
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện OkClicked của ctrlUpload1
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked

        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Dim dtDataHeader As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                If workbook.Worksheets.GetSheetByCodeName("IMPORTSHIFT") Is Nothing Then
                    ShowMessage(Translate("File mẫu không đúng định dạng"), NotifyType.Warning)
                    Exit Sub
                End If
                worksheet = workbook.Worksheets(0)
                dtDataHeader = worksheet.Cells.ExportDataTableAsString(0, 0, 5, worksheet.Cells.MaxColumn + 1, True)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(4, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            dtDataHeader.Rows.RemoveAt(0)
            dtDataHeader.Rows.RemoveAt(0)
            For col As Integer = 0 To dtDataHeader.Columns.Count - 1
                Dim colName = dtDataHeader.Rows(1)(col)
                If colName IsNot DBNull.Value Then
                    dtDataHeader.Columns(col).ColumnName = colName
                End If
            Next
            dtDataHeader.Rows.RemoveAt(1)
            For Each dt As DataTable In dsDataPrepare.Tables
                For Each row In dt.Rows
                    Dim isRow = ImportValidate.TrimRow(row)
                    If isRow Then
                        dtData.ImportRow(row)
                    End If
                Next
            Next
            If loadToGrid(dtDataHeader) = False Then
            Else
                If saveGrid() Then
                    Refresh("InsertView")
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                                         CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện ItemDataBound của rad grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgSignWork_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgSignWork.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
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
    ''' 17/08/2017 08:40
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật trạng thái toolbar
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

    Public Shared Function ConvertToDataTable(Of T)(ByVal list As IList(Of T)) As DataTable
        Dim table As New DataTable()
        Dim fields() As FieldInfo = GetType(T).GetFields()
        For Each field As FieldInfo In fields
            table.Columns.Add(field.Name, field.FieldType)
        Next
        For Each item As T In list
            Dim row As DataRow = table.NewRow()
            For Each field As FieldInfo In fields
                row(field.Name) = field.GetValue(item)
            Next
            table.Rows.Add(row)
        Next
        Return table
    End Function

#End Region

End Class
﻿Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Telerik.Web.UI
Imports Common.Common
Imports Common.CommonMessage
Imports Attendance.AttendanceRepository
Imports Attendance.AttendanceBusiness
Imports WebAppLog

Public Class ctrlMngIO
    Inherits Common.CommonView

    ''' <summary>
    ''' ctrlOrgPopup
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents ctrlOrgPopup As ctrlFindOrgPopup

    ''' <summary>
    ''' AjaxManager
    ''' </summary>
    ''' <remarks></remarks>
    Public WithEvents AjaxManager As RadAjaxManager

    ''' <summary>
    ''' AjaxManagerId
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AjaxManagerId As String

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()

#Region "Properties"

    ''' <summary>
    ''' DATAINOUT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property DATAINOUT As List(Of AT_DATAINOUTDTO)
        Get
            Return ViewState(Me.ID & "_DATAINOUT")
        End Get
        Set(ByVal value As List(Of AT_DATAINOUTDTO))
            ViewState(Me.ID & "_DATAINOUT") = value
        End Set
    End Property

    ''' <summary>
    ''' isLoadPopup
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

    ''' <summary>
    ''' dtDataImportEmployee
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property dtDataImportEmployee As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataImportEmployee")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataImportEmployee") = value
        End Set
    End Property

    ''' <summary>
    ''' PERIOD
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property PERIOD As List(Of AT_PERIODDTO)
        Get
            Return ViewState(Me.ID & "_PERIOD")
        End Get
        Set(ByVal value As List(Of AT_PERIODDTO))
            ViewState(Me.ID & "_PERIOD") = value
        End Set
    End Property

    ''' <summary>
    ''' dtData
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
                dt.Columns.Add("WORKINGDAY", GetType(Date))
                dt.Columns.Add("VALIN1", GetType(String))
                dt.Columns.Add("VALIN2", GetType(String))
                dt.Columns.Add("VALIN3", GetType(String))
                dt.Columns.Add("VALIN4", GetType(String))
                dt.Columns.Add("VALIN5", GetType(String))
                dt.Columns.Add("VALIN6", GetType(String))
                dt.Columns.Add("VALIN7", GetType(String))
                dt.Columns.Add("VALIN8", GetType(String))
                dt.Columns.Add("VALIN9", GetType(String))
                dt.Columns.Add("VALIN10", GetType(String))
                dt.Columns.Add("VALIN11", GetType(String))
                dt.Columns.Add("VALIN12", GetType(String))
                dt.Columns.Add("VALIN13", GetType(String))
                dt.Columns.Add("VALIN14", GetType(String))
                dt.Columns.Add("VALIN15", GetType(String))
                dt.Columns.Add("VALIN16", GetType(String))
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

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Ke thua ViewLoad tu CommnView, la ham load du lieu, control states cua usercontrol
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            'If Not IsPostBack Then
            '    chkOnlyOrgSelected.Checked = False
            'End If
            ctrlOrganization.AutoPostBack = True
            ctrlOrganization.LoadDataAfterLoaded = True
            ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
            ctrlOrganization.CheckBoxes = TreeNodeTypes.None

            Refresh()
            UpdateControlState()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            SetGridFilter(rgMngIO)
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgMngIO.AllowCustomPaging = True
            rgMngIO.ClientSettings.EnablePostBackOnRowClick = False
            ctrlUpload1.isMultiple = False
            InitControl()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Load cac control, menubar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Export)

            'CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
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
            period.ORG_ID = 46
            period.YEAR = Date.Now.Year

            lsData = rep.LOAD_PERIODBylinq(period)
            Me.PERIOD = lsData
            FillRadCombobox(cboPeriod, lsData, "PERIOD_NAME", "PERIOD_ID", True)

            If lsData.Count > 0 Then
                Dim periodid = (From d In lsData
                                Where d.START_DATE.Value.ToString("yyyyMM").Equals(Date.Now.ToString("yyyyMM")) Select d).FirstOrDefault

                If periodid IsNot Nothing Then
                    cboPeriod.SelectedValue = periodid.PERIOD_ID.ToString()
                    rdtungay.SelectedDate = periodid.START_DATE
                    rdDenngay.SelectedDate = periodid.END_DATE
                Else
                    cboPeriod.SelectedIndex = 0
                    Dim periodid1 = (From d In lsData Where d.PERIOD_ID.ToString.Contains(cboPeriod.SelectedValue.ToString) Select d).FirstOrDefault

                    If periodid1 IsNot Nothing Then
                        rdtungay.SelectedDate = periodid1.START_DATE
                        rdDenngay.SelectedDate = periodid1.END_DATE
                    End If
                    'If (Common.Common.GetShortDatePattern().Trim.ToUpper.Contains("DD/MM/YYYY")) Then
                    '    rdtungay.SelectedDate = CType("01/01/" & cboYear.SelectedValue.Trim.ToString, Date)
                    '    rdDenngay.SelectedDate = CType("31/01/" & cboYear.SelectedValue.Trim.ToString, Date)
                    'Else
                    '    rdtungay.SelectedDate = CType("01/01/" & cboYear.SelectedValue.Trim.ToString, Date)
                    '    rdDenngay.SelectedDate = CType("01/31/" & cboYear.SelectedValue.Trim.ToString, Date)
                    'End If
                End If
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim rep As New AttendanceRepository
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of AT_DATAINOUTDTO)

                    For idx = 0 To rgMngIO.SelectedItems.Count - 1
                        Dim lst = New AT_DATAINOUTDTO
                        Dim item As GridDataItem = rgMngIO.SelectedItems(idx)
                        lst.ID = item.GetDataKeyValue("ID")
                        lstDeletes.Add(lst)
                    Next

                    If rep.DeleteDataInout(lstDeletes) Then
                        Refresh("UpdateView")
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
            End Select

            phPopup.Controls.Clear()

            Select Case isLoadPopup
                Case 1
                    ctrlOrgPopup = Me.Register("ctrlOrgPopup", "Common", "ctrlFindOrgPopup")
                    phPopup.Controls.Add(ctrlOrgPopup)
            End Select

            UpdateToolbarState()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Event thay doi giá trị cua combobox NĂM 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlYear_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim dtData As List(Of AT_PERIODDTO)
            Dim rep As New AttendanceRepository
            Dim period As New AT_PERIODDTO

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
                    rgMngIO.Rebind()
                Else
                    cboPeriod.SelectedIndex = 0
                    Dim per = (From c In dtData Where c.PERIOD_ID = cboPeriod.SelectedValue).FirstOrDefault
                    rdtungay.SelectedDate = per.START_DATE
                    rdDenngay.SelectedDate = per.END_DATE
                    rgMngIO.Rebind()
                End If
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Event thay doi giá trị cua combobox KỲ CÔNG
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboPeriod_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriod.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim rep As New AttendanceRepository
            Dim period As New AT_PERIODDTO

            period.YEAR = Decimal.Parse(cboYear.SelectedValue)
            period.ORG_ID = 46

            If cboPeriod.SelectedValue <> "" Then
                Dim Lstperiod = rep.LOAD_PERIODBylinq(period)
                Dim p = (From o In Lstperiod Where o.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)).FirstOrDefault

                rdtungay.SelectedDate = p.START_DATE
                rdDenngay.SelectedDate = p.END_DATE
            End If
            rgMngIO.Rebind()
            'ctrlOrganization.SetColorPeriod(cboPeriod.SelectedValue, PeriodType.AT)
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Load lai du lieu tren grid khi thuc hien update, insert, delete
    ''' (neu co tham so truyen vao thi dua ra messager)
    ''' </summary>
    ''' <param name="Message">tham so truyen vao de phan biet trang thai Insert, Update, Cancel</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceBusinessClient

        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMngIO.Rebind()
                        'SelectedItemDataGridByKey(rgSignWork, IDSelect, , rgSignWork.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL

                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMngIO.CurrentPageIndex = 0
                        rgMngIO.MasterTableView.SortExpressions.Clear()
                        rgMngIO.Rebind()
                        'SelectedItemDataGridByKey(rgSignWork, IDSelect, )

                    Case "Cancel"
                        rgMngIO.MasterTableView.ClearSelectedItems()
                End Select
            End If

            UpdateControlState()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Event lưa chọn to chức thay doi tren popup ctrlOrganization
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rgMngIO.CurrentPageIndex = 0
            rgMngIO.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try        
    End Sub

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_DELETE
                    'Kiểm tra các điều kiện để xóa.
                    If rgMngIO.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi xóa"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case TOOLBARITEM_ATTACH
                    isLoadPopup = 1
                    UpdateControlState()
                    ctrlOrgPopup.Show()

                Case TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgMngIO.ExportExcel(Server, Response, dtDatas, "DataInout")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using

                Case TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
                    'Case TOOLBARITEM_SAVE
                    '    SaveInOut()
                    '    Refresh("InsertView")
                    '    CurrentState = CommonMessage.STATE_NORMAL
                    '    UpdateControlState()
            End Select

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Insert data dữ liệu vào ra
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveInOut() As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim rep As New AttendanceRepository
            Dim obj As New AT_DATAINOUTDTO
            Dim gId As New Decimal?
            gId = 0

            For Each row As DataRow In dtDataImportEmployee.Rows
                obj.EMPLOYEE_CODE = row("EMPLOYEE_CODE")
                obj.WORKINGDAY = Date.Parse(row("WORKINGDAY"))
                If row("VALIN1") IsNot DBNull.Value AndAlso row("VALIN1") <> "" Then
                    obj.VALIN1 = Date.Parse(row("WORKINGDAY")).AddHours(TimeSpan.Parse(row("VALIN1")).TotalHours)
                End If
                If row("VALIN2") IsNot DBNull.Value AndAlso row("VALIN2") <> "" Then
                    obj.VALIN2 = Date.Parse(row("WORKINGDAY")).AddHours(TimeSpan.Parse(row("VALIN2")).TotalHours)
                End If
                If row("VALIN3") IsNot DBNull.Value AndAlso row("VALIN3") <> "" Then
                    obj.VALIN3 = Date.Parse(row("WORKINGDAY")).AddHours(TimeSpan.Parse(row("VALIN3")).TotalHours)
                End If
                If row("VALIN4") IsNot DBNull.Value AndAlso row("VALIN4") <> "" Then
                    obj.VALIN4 = Date.Parse(row("WORKINGDAY")).AddHours(TimeSpan.Parse(row("VALIN4")).TotalHours)
                End If
                If row("VALIN5") IsNot DBNull.Value AndAlso row("VALIN5") <> "" Then
                    obj.VALIN5 = Date.Parse(row("WORKINGDAY")).AddHours(TimeSpan.Parse(row("VALIN5")).TotalHours)
                End If
                If row("VALIN6") IsNot DBNull.Value AndAlso row("VALIN6") <> "" Then
                    obj.VALIN6 = Date.Parse(row("WORKINGDAY")).AddHours(TimeSpan.Parse(row("VALIN6")).TotalHours)
                End If
                obj.PERIOD_ID = Decimal.Parse(cboPeriod.SelectedValue)
                'rep.InsertDataInout(obj, gId)
            Next

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Return True
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
        Return False
    End Function

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Check data khi upload
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function loadToGrid() As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtError As New DataTable("ERROR")

        Try
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            End If

            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            dtDataImportEmployee = dtData.Clone
            dtError = dtData.Clone
            Dim iRow = 1

            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                isError = False
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)

                If row("VALIN1") IsNot DBNull.Value AndAlso row("VALIN1") <> "" Then
                    sError = "Sai định dạng giờ quẹt lần 1"
                    ImportValidate.IsValidTime("VALIN1", row, rowError, isError, sError)
                End If

                If row("VALIN2") IsNot DBNull.Value AndAlso row("VALIN2") <> "" Then
                    sError = "Sai định dạng giờ quẹt lần 2"
                    ImportValidate.IsValidTime("VALIN2", row, rowError, isError, sError)
                End If

                If row("VALIN3") IsNot DBNull.Value AndAlso row("VALIN3") <> "" Then
                    sError = "Sai định dạng giờ quẹt lần 3"
                    ImportValidate.IsValidTime("VALIN3", row, rowError, isError, sError)
                End If

                If row("VALIN4") IsNot DBNull.Value AndAlso row("VALIN4") <> "" Then
                    sError = "Sai định dạng giờ quẹt lần 4"
                    ImportValidate.IsValidTime("VALIN4", row, rowError, isError, sError)
                End If

                If row("VALIN5") IsNot DBNull.Value AndAlso row("VALIN5") <> "" Then
                    sError = "Sai định dạng giờ quẹt lần 5"
                    ImportValidate.IsValidTime("VALIN5", row, rowError, isError, sError)
                End If

                If row("VALIN6") IsNot DBNull.Value AndAlso row("VALIN6") <> "" Then
                    sError = "Sai định dạng giờ quẹt lần 6"
                    ImportValidate.IsValidTime("VALIN6", row, rowError, isError, sError)
                End If

                If isError Then
                    rowError("ID") = iRow

                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If

                    rowError("VN_FULLNAME") = row("VN_FULLNAME").ToString
                    rowError("ORG_NAME") = row("ORG_NAME").ToString
                    rowError("ORG_PATH") = row("ORG_PATH").ToString
                    rowError("TITLE_NAME") = row("TITLE_NAME").ToString
                    dtError.Rows.Add(rowError)
                Else
                    dtDataImportEmployee.ImportRow(row)
                End If
                iRow = iRow + 1
            Next

            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                ' gộp các lỗi vào 1 cột ghi chú 
                Dim dtErrorGroup As New DataTable
                Dim RowErrorGroup As DataRow
                dtErrorGroup.Columns.Add("STT")
                dtErrorGroup.Columns.Add("NOTE")

                For j As Integer = 0 To dtError.Rows.Count - 1
                    Dim strNote As String = String.Empty
                    RowErrorGroup = dtErrorGroup.NewRow

                    For k As Integer = 1 To dtError.Columns.Count - 1
                        If Not dtError.Rows(j)(k) Is DBNull.Value Then
                            strNote &= dtError.Rows(j)(k) & "\"
                        End If
                    Next

                    RowErrorGroup("STT") = dtError.Rows(j)("ID")
                    RowErrorGroup("NOTE") = strNote
                    dtErrorGroup.Rows.Add(RowErrorGroup)
                Next

                dtErrorGroup.TableName = "DATA"

                Session("EXPORTREPORT") = dtErrorGroup
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_importIO_error')", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If

            If isError Then
                Return False
            Else
                Return True
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly upload file khi click button [OK] o popup ctrlUpload
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet

        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(1, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next

            dtData = dtData.Clone()

            For Each dt As DataTable In dsDataPrepare.Tables
                For Each row In dt.Rows
                    Dim isRow = ImportValidate.TrimRow(row)
                    If Not isRow Then
                        Continue For
                    End If
                    dtData.ImportRow(row)
                Next
            Next

            If loadToGrid() Then
                'CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                'CType(MainToolBar.Items(1), RadToolBarButton).Enabled = True
                'CType(MainToolBar.Items(2), RadToolBarButton).Enabled = True
                rgMngIO.VirtualItemCount = dtData.Rows.Count
                rgMngIO.DataSource = dtData
                rgMngIO.DataBind()
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly load tat ca thong tin ve to chức khi click button [chọn] o popup ctrlOrgPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            'If String.IsNullOrEmpty(cboPeriod.SelectedValue) Then
            '    ShowMessage(Translate("Kỳ công chưa được chọn"), NotifyType.Error)
            '    Exit Sub
            'End If
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('ManIOImport&PERIOD_ID=" & cboPeriod.SelectedValue & "&orgid=" & e.CurrentValue & "&IS_DISSOLVE=" & IIf(ctrlOrgPopup.IsDissolve, "1", "0") & "')", True)
            isLoadPopup = 0

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly load tat ca thong tin ve to chức khi click button [hủy] o popup ctrlOrgPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrgPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrgPopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            isLoadPopup = 0
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/08/2017</lastupdate>
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
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New AttendanceRepository
        Dim obj As New AT_DATAINOUTDTO

        Try
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgMngIO, obj)

            Dim _param = New ParamDTO With {.ORG_ID = ctrlOrganization.CurrentValue, _
                                           .IS_DISSOLVE = ctrlOrganization.IsDissolve,
                                           .IS_FULL = True}

            Dim Sorts As String = rgMngIO.MasterTableView.SortExpressions.GetSortString()

            If rdtungay.SelectedDate.HasValue Then
                obj.FROM_DATE = rdtungay.SelectedDate
            End If

            If rdDenngay.SelectedDate.HasValue Then
                obj.END_DATE = rdDenngay.SelectedDate
            End If

            If chkChecknghiViec.Checked Then
                obj.IS_TERMINATE = True
            End If

            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.DATAINOUT = rep.GetDataInout(obj, _param, MaximumRows, rgMngIO.CurrentPageIndex, rgMngIO.PageSize, "ID")
                Else
                    Me.DATAINOUT = rep.GetDataInout(obj, _param, MaximumRows, rgMngIO.CurrentPageIndex, rgMngIO.PageSize)
                End If
            Else
                Return rep.GetDataInout(obj, _param).ToTable
            End If

            rgMngIO.VirtualItemCount = MaximumRows
            rgMngIO.DataSource = Me.DATAINOUT

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Load data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMngIO.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CreateDataFilter(False)
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Event click của button Tìm kiếm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            'If String.IsNullOrEmpty(cboPeriod.SelectedValue) And rdTungay.SelectedDate.HasValue = False And rdDenngay.SelectedDate.HasValue = False Then
            '    ShowMessage(Translate("Kỳ công chưa được chọn"), NotifyType.Error)
            '    Exit Sub
            'End If
            rgMngIO.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Event thay đổi index pager cua grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgMngIO.PageIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' AjaxManager_AjaxRequest
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim url = e.Argument

            If (url.Contains("reload=1")) Then
                rgMngIO.CurrentPageIndex = 0
                rgMngIO.Rebind()

                If rgMngIO.Items IsNot Nothing AndAlso rgMngIO.Items.Count > 0 Then
                    rgMngIO.Items(0).Selected = True
                End If
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    'Private Sub rgMngIO_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgMngIO.ItemDataBound
    '    If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
    '        Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
    '        datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
    '    End If
    'End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>17/08/2017</lastupdate>
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
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

End Class
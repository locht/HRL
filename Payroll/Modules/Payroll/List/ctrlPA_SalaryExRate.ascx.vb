﻿Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Common
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPA_SalaryExRate
    Inherits Common.CommonView

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Payroll/Module/Payroll/List/" + Me.GetType().Name.ToString()

#Region "Property"

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
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
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
            InitControl()
            'If Not IsPostBack Then
            '    ViewConfig(RadPane1)
            '    GirdConfig(rgData)
            'End If
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
            Me.MainToolBar = tbarSalaryGroups
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Active, ToolbarItem.Deactive,
                                       ToolbarItem.Delete, ToolbarItem.Export)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
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
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                    Case "Cancel"
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

    ''' <lastupdate>
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu tu DB len grid va filter
    ''' </summary>
    ''' <param name="isFull">Tham so xac dinh viec load full du lieu neu = false hoac Nothing
    ''' Load co phan trang neu isFull = true</param>
    ''' <returns>tra ve datatable</returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim obj As New SalaryExRateDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgData, obj)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()

            Using rep As New PayrollRepository
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetSalaryExRate(obj, Sorts).ToTable
                    Else
                        Return rep.GetSalaryExRate(obj).ToTable
                    End If
                Else
                    Dim SalaryGroups As New List(Of SalaryExRateDTO)
                    If Sorts IsNot Nothing Then
                        SalaryGroups = rep.GetSalaryExRate(obj, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
                    Else
                        SalaryGroups = rep.GetSalaryExRate(obj, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
                    End If
                    rgData.VirtualItemCount = MaximumRows
                    rgData.DataSource = SalaryGroups
                End If
            End Using
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    ''' <lastupdate>
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Update trang thai control khi an nut them moi, sua, xoa, luu, ap dung, ngung ap dung
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgData, False)

                    Utilities.EnableRadCombo(cboFrCuType, True)
                    ntxtExRate.ReadOnly = False
                    txtRemark.ReadOnly = False
                    rdEffectDate.Enabled = True

                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgData, True)

                    Utilities.EnableRadCombo(cboFrCuType, False)
                    ntxtExRate.ReadOnly = True
                    txtRemark.ReadOnly = True
                    rdEffectDate.Enabled = False

                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgData, False)

                    Utilities.EnableRadCombo(cboFrCuType, True)
                    ntxtExRate.ReadOnly = False
                    txtRemark.ReadOnly = False
                    rdEffectDate.Enabled = True

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.DeleteSalaryExRate(lstDeletes) Then
                            Refresh("UpdateView")
                            UpdateControlState()
                            rgData.Rebind()
                            ClearControlValue(cboFrCuType, ntxtExRate, txtRemark, rdEffectDate)
                        Else
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                        End If
                    End Using
                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    Using rep As New PayrollRepository
                        If rep.ActiveSalaryExRate(lstDeletes, "A") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgData.Rebind()
                            ClearControlValue(cboFrCuType, ntxtExRate, txtRemark, rdEffectDate)
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
                        If rep.ActiveSalaryExRate(lstDeletes, "I") Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgData.Rebind()
                            ClearControlValue(cboFrCuType, ntxtExRate, txtRemark, rdEffectDate)
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
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()

        Dim dic As New Dictionary(Of String, Control)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Using rep As New PayrollRepository
                'FillDropDownList(cboFrCuType, rep.GetSalaryExRateCombo(True), "NAME_VN", "ID", Common.Common.SystemLanguage, False)
                FillRadCombobox(cboFrCuType, rep.GetSalaryExRateCombo(False), "NAME_VN", "ID", False)
            End Using
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            dic.Add("NAME", ntxtExRate)
            dic.Add("EFFECT_DATE", rdEffectDate)
            dic.Add("REMARK", txtRemark)
            dic.Add("CODE", cboFrCuType)
            Utilities.OnClientRowSelectedChanged(rgData, dic)
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
        Dim objSalaryExRate As New SalaryExRateDTO
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(cboFrCuType, ntxtExRate, txtRemark, rdEffectDate)
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
                    Dim lstID As New List(Of Decimal)
                    For idx = 0 To rgData.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgData.SelectedItems(idx)
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next
                    'Using rep As New PayrollRepository
                    '    If Not rep.CheckExistInDatabase(lstID, TABLE_NAME.PA_SALARY_EXCHANGE_RATE) Then
                    '        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                    '        Return
                    '    End If
                    'End Using
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "SalaryGroup")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        'objSalaryExRate.ID = cboFrCuType.SelectedValue
                        objSalaryExRate.NAME = ntxtExRate.Text.Trim
                        objSalaryExRate.EFFECT_DATE = rdEffectDate.SelectedDate
                        objSalaryExRate.REMARK = txtRemark.Text.Trim
                        objSalaryExRate.CODE = cboFrCuType.SelectedValue

                        Using rep As New PayrollRepository
                            Select Case CurrentState
                                Case CommonMessage.STATE_NEW
                                    objSalaryExRate.ACTFLG = "A"
                                    objSalaryExRate.IS_DELETED = 0
                                    If rep.InsertSalaryExRate(objSalaryExRate, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = gID
                                        Refresh("InsertView")
                                        UpdateControlState()
                                        rgData.Rebind()
                                        ClearControlValue(cboFrCuType, ntxtExRate, txtRemark, rdEffectDate)
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                                Case CommonMessage.STATE_EDIT
                                    objSalaryExRate.ID = rgData.SelectedValue
                                    Dim repCheck As New CommonRepository
                                    Dim lstCheck As New List(Of Decimal)
                                    lstCheck.Add(objSalaryExRate.ID)
                                    If repCheck.CheckExistIDTable(lstCheck, "PA_SALARY_EXCHANGE_RATE", "ID") Then
                                        ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        ClearControlValue(cboFrCuType, ntxtExRate, txtRemark, rdEffectDate)
                                        UpdateControlState()
                                        rgData.Rebind()
                                        Exit Sub
                                    End If
                                    If rep.ModifySalaryExRate(objSalaryExRate, gID) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        IDSelect = objSalaryExRate.ID
                                        Refresh("UpdateView")
                                        UpdateControlState()
                                        rgData.Rebind()
                                        ClearControlValue(cboFrCuType, ntxtExRate, txtRemark, rdEffectDate)
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                    End If
                            End Select
                        End Using
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData')")
                    End If
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
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    ClearControlValue(cboFrCuType, ntxtExRate, txtRemark, rdEffectDate)
            End Select
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
    ''' Event Yes, No tren popup message khi click nut: xoa, ap dung, ngung ap dung
    ''' va Set trang thai cho form va update trang thai control 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                If e.ActionName = CommonMessage.TOOLBARITEM_DELETE Then
                    CurrentState = CommonMessage.STATE_DELETE
                End If
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

    ''' <lastupdate>
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien custom validate cua cvalCode
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim _validate As New SalaryExRateDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Using rep As New PayrollRepository
                If CurrentState = CommonMessage.STATE_EDIT Then
                    _validate.CODE = cboFrCuType.SelectedValue
                    _validate.ID = rgData.SelectedValue
                    args.IsValid = rep.ValidateSalaryExRate(_validate)

                Else
                    _validate.CODE = cboFrCuType.SelectedValue
                    args.IsValid = rep.ValidateSalaryExRate(_validate)
                End If
            End Using
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
    ''' 23/08/2017 09:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly viec parse tu number sang boolean
    ''' </summary>
    ''' <param name="dValue"></param>
    ''' <remarks></remarks>
    Public Function ParseBoolean(ByVal dValue As String) As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If String.IsNullOrEmpty(dValue) Then
                Return False
            Else
                Return If(dValue = "1", True, False)
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

#End Region

End Class
Imports Framework.UI
Imports Common
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI
Imports Framework.UI.Utilities
Imports Common.CommonMessage
Imports System.IO
Imports Aspose.Cells
Imports System.Globalization
Imports HistaffFrameworkPublic

Class ctrlPE_Result_Evaluate
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Properties"

    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    Private Property dtLogs As DataTable
        Get
            Return PageViewState(Me.ID & "_dtLogs")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtLogs") = value
        End Set
    End Property
    Property Upload4Emp As Decimal
        Get
            Return ViewState(Me.ID & "_Upload4Emp")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Upload4Emp") = value
        End Set
    End Property
    Property isRight As Decimal
        Get
            Return ViewState(Me.ID & "_isRight")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_isRight") = value
        End Set
    End Property
    Property EmployeeList As List(Of KPI_EVALUATEDTO)
        Get
            Return ViewState(Me.ID & "_EmployeeList")
        End Get
        Set(ByVal value As List(Of KPI_EVALUATEDTO))
            ViewState(Me.ID & "_EmployeeList") = value
        End Set
    End Property
#End Region
#Region "Page"
    ''' <summary>
    ''' Hiển thị thông tin trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        'Edit by: ChienNV 
        'Trước khi Load thì kiểm tra PostBack
        If Not IsPostBack Then
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Try
                Dim startTime As DateTime = DateTime.UtcNow
                ctrlOrganization.AutoPostBack = True
                ctrlOrganization.LoadDataAfterLoaded = True
                ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
                ctrlOrganization.CheckBoxes = TreeNodeTypes.None
                rgEmployeeList.SetFilter()
                Refresh()
                'For Each item As GridDataItem In rgEmployeeList.MasterTableView.Items
                '    item.Edit = False
                'Next
                '  _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                DisplayException(Me.ViewName, Me.ID, ex)
                '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            End Try
        Else
            ctrlOrganization.Enabled = True
            Exit Sub
        End If

    End Sub
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo các thiết lập, giá trị các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID

            rgEmployeeList.SetFilter()
            rgEmployeeList.AllowCustomPaging = True
            rgEmployeeList.PageSize = Common.Common.DefaultPageSize
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            ' _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' khởi tạo các thành phần trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Export,
                                       ToolbarItem.Next,
                                       ToolbarItem.Import
                                       )
            CType(Me.MainToolBar.Items(1), RadToolBarButton).Text = Translate("Xuất file mẫu")
            CType(Me.MainToolBar.Items(1), RadToolBarButton).ImageUrl = CType(Me.MainToolBar.Items(0), RadToolBarButton).ImageUrl
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Text = Translate("Nhập file mẫu")
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    ''' <summary>
    ''' Edit by: ChienNV;
    ''' Fill data in control cboPrintSupport
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Try
            GetDataCombo()
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Message = CommonMessage.ACTION_UPDATED Then
                rgEmployeeList.Rebind()
            End If
            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region
#Region "Event"


    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgEmployeeList.CurrentPageIndex = 0
            rgEmployeeList.Rebind()
            ' EnableOngrid()
            ' _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ' _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Xử lý sự kiện Command khi click item trên toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            '  Dim objdata As PE_EVALUATE_PERIODDTO
            Dim objEmployee As New KPI_EVALUATEDTO
            Dim rep As New PerformanceBusinessClient
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_EDIT
                    isRight = 1
                    ctrlOrganization.Enabled = False
                    For Each item As GridItem In rgEmployeeList.MasterTableView.Items
                        item.Edit = True
                    Next
                    CurrentState = CommonMessage.STATE_EDIT
                Case CommonMessage.TOOLBARITEM_SAVE

                Case TOOLBARITEM_DELETE
                    'Kiểm tra các điều kiện để xóa.
                    If rgEmployeeList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi xóa"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgEmployeeList.ExportExcel(Server, Response, dtData, "ResultMBO")
                        Else
                            ShowMessage(Translate(MESSAGE_WARNING_EXPORT_EMPTY), Utilities.NotifyType.Warning)
                        End If
                    End Using
                    'exportKPI()
               
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    rgEmployeeList.Rebind()
            End Select
            'rep.Dispose()
            UpdateControlState()
            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgEmployeeList.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If Not IsPostBack Then
                rgEmployeeList.VirtualItemCount = 0
                rgEmployeeList.DataSource = New List(Of KPI_EVALUATEDTO)
            Else
                CreateDataFilter()
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            ' _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Dim MaximumRows As Integer
        Dim Sorts As String
        Dim _filter As New KPI_EVALUATEDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Using rep As New PerformanceRepository

                If ctrlOrganization.CurrentValue IsNot Nothing Then
                    _filter.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
                Else
                    rgEmployeeList.DataSource = New List(Of KPI_EVALUATEDTO)
                    Exit Function
                End If
                If cboPeriodEvaluate.SelectedValue <> "" Then
                    _filter.KPI_EVALUATE = cboPeriodEvaluate.SelectedValue
                End If

                Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                                .IS_DISSOLVE = ctrlOrganization.IsDissolve}

                SetValueObjectByRadGrid(rgEmployeeList, _filter)
                _filter.IS_TER = chkTerminate.Checked
                Sorts = rgEmployeeList.MasterTableView.SortExpressions.GetSortString()
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetListEmployeePaging1(_filter, _param, Sorts).ToTable()
                    Else
                        Return rep.GetListEmployeePaging1(_filter, _param).ToTable()
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        EmployeeList = rep.GetListEmployeePaging1(_filter, rgEmployeeList.CurrentPageIndex, rgEmployeeList.PageSize, MaximumRows, _param, Sorts)
                    Else
                        EmployeeList = rep.GetListEmployeePaging1(_filter, rgEmployeeList.CurrentPageIndex, rgEmployeeList.PageSize, MaximumRows, _param)
                    End If

                    rgEmployeeList.VirtualItemCount = MaximumRows
                    rgEmployeeList.DataSource = EmployeeList
                End If


            End Using
            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
    Private Sub GetDataCombo()
        Dim dtData As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Using rep As New PerformanceBusinessClient

                dtData = rep.GetlistYear()
                FillRadCombobox(cboYear, dtData, "YEAR", "ID")
            End Using
            ' _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgEmployeeList.Rebind()
            isRight = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Select Case CurrentState
            Case CommonMessage.STATE_EDIT
                ' EnabledGrid(rgEmployeeList, True, False)
                For Each item As GridDataItem In rgEmployeeList.MasterTableView.Items
                    Dim txtmark_offical As RadNumericTextBox = DirectCast(item("MARK_MBO_OFFICAL").FindControl("rnMBOGoc"), RadNumericTextBox)
                    txtmark_offical.Enabled = True
                    Dim txtmark_dc As RadNumericTextBox = DirectCast(item("MARK_MBO_EDIT").FindControl("nnMBODC"), RadNumericTextBox)
                    txtmark_dc.Enabled = True
                    Dim txtATTACH_FILE As LinkButton = DirectCast(item("UPLOAD_FILE").FindControl("lbtnUpload"), LinkButton)
                    txtATTACH_FILE.Enabled = True
                    Dim txtupload As RadButton = DirectCast(item("ID").FindControl("btnUpload"), RadButton)
                    txtupload.Enabled = True
                Next
                rgEmployeeList.Rebind()
            Case CommonMessage.STATE_NORMAL

        End Select
        ChangeToolbarState()
    End Sub
    
#End Region

    Private Sub cboYear_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboYear.SelectedIndexChanged
        Try
            Using rep As New PerformanceBusinessClient
                If cboYear.Text <> "" Then
                    Dim dtData As New DataTable
                    dtData = rep.GetLstPeriod1(Decimal.Parse(cboYear.Text))
                    FillRadCombobox(cboPeriodEvaluate, dtData, "NAME", "ID")
                Else
                    ClearControlValue(cboPeriodEvaluate, rdFrom, rdTo)
                End If
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub cboPeriodEvaluate_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPeriodEvaluate.SelectedIndexChanged
        Try
            Using rep As New PerformanceBusinessClient
                If cboPeriodEvaluate.SelectedValue <> "" Then
                    Dim dtData As PeriodDTO
                    dtData = rep.GetPeriodDate(cboPeriodEvaluate.SelectedValue)
                    rdFrom.SelectedDate = dtData.START_DATE
                    rdTo.SelectedDate = dtData.END_DATE
                Else
                    ClearControlValue(rdFrom, rdTo)
                End If
            End Using
        Catch ex As Exception

        End Try
    End Sub

    

End Class
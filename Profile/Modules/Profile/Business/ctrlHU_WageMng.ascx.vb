Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Common
Imports Telerik.Web.UI
Imports System.IO
Imports HistaffWebAppResources.My.Resources
Imports WebAppLog
Public Class ctrlHU_WageMng
    Inherits Common.CommonView

    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
    'Private ReadOnly RestClient As IServerDataRestClient = New ServerDataRestClient()
#Region "Property"

    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsUpdate")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsUpdate") = value
        End Set
    End Property

#End Region

#Region "Page"
    ''' <summary>
    ''' Load page, set trang thai cua page va control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Khoi tao page, set thuoc tinh cho grid
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgWorking.SetFilter()
            rgWorking.AllowCustomPaging = True
            InitControl()
            If Not IsPostBack Then
                ViewConfig(RadPane1)
                GirdConfig(rgWorking)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' khoi tao page, menu toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarWorkings
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Create, ToolbarItem.Edit,
                                       ToolbarItem.Export, ToolbarItem.ApproveBatch,
                                       ToolbarItem.Next, ToolbarItem.Import,
                                       ToolbarItem.Delete)
            CType(MainToolBar.Items(3), RadToolBarButton).Text = UI.Approve
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Text = Translate("Xuất file mẫu")
            CType(Me.MainToolBar.Items(4), RadToolBarButton).ImageUrl = CType(Me.MainToolBar.Items(2), RadToolBarButton).ImageUrl
            CType(Me.MainToolBar.Items(5), RadToolBarButton).Text = Translate("Nhập file mẫu")
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            'rgWorking.MasterTableView.GetColumn("SAL_TYPE_NAME").HeaderText = UI.Wage_WageGRoup
            'rgWorking.MasterTableView.GetColumn("SAL_BASIC").HeaderText = UI.Wage_BasicSalary
            'rgWorking.MasterTableView.GetColumn("TAX_TABLE_Name").HeaderText = UI.Wage_TaxTable
            'rgWorking.MasterTableView.GetColumn("ResponsibilityAllowances").HeaderText = UI.Wage_ResponsibilityAllowances
            'rgWorking.MasterTableView.GetColumn("WorkAllowances").HeaderText = UI.WorkAllowances
            'rgWorking.MasterTableView.GetColumn("AttendanceAllowances").HeaderText = UI.AttendanceAllowances
            'rgWorking.MasterTableView.GetColumn("HousingAllowances").HeaderText = UI.HousingAllowances
            'rgWorking.MasterTableView.GetColumn("CarRentalAllowances").HeaderText = UI.CarRentalAllowances
            'rgWorking.MasterTableView.GetColumn("SAL_INS").HeaderText = UI.Wage_Sal_Ins
            'rgWorking.MasterTableView.GetColumn("SAL_TOTAL").HeaderText = UI.Wage_Salary_Total

        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' reset trang thai page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileBusinessRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation

            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' get data len grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgWorking.NeedDataSource

        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter()
#If DEBUG Then
            ' Dim test = RestClient.GetAll
#End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub


#End Region

#Region "Event"
    ''' <summary>
    ''' event click item menu toolbar
    ''' update lai trang thai control, trang thai page sau khi process event xong
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objOrgFunction As New OrganizationDTO
        Dim sError As String = ""
        Dim tempPath As String = ConfigurationManager.AppSettings("WordFileFolder")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgWorking.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgWorking.ExportExcel(Server, Response, dtData, "Wage")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgWorking.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgWorking.SelectedItems
                        If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Bản ghi đã phê duyệt. Thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.NOT_APPROVE_ID Then
                            ShowMessage(Translate("Bản ghi không phê duyệt. Thao tác thực hiện không thành công"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_PRINT
                    If rgWorking.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgWorking.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim dtData As DataTable
                    Dim folderName As String = ""
                    Dim filePath As String = ""
                    Dim extension As String = ""
                    Dim iError As Integer = 0
                    Dim item As GridDataItem = rgWorking.SelectedItems(0)
                    Dim repValidate As New ProfileBusinessRepository
                    Dim validate As New WorkingDTO
                    validate.ID = item.GetDataKeyValue("ID")
                    If repValidate.ValidateWorking("EXIST_ID", validate) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                        Exit Sub
                    End If
                    ' Kiểm tra + lấy thông tin trong database
                    Using rep As New ProfileRepository
                        dtData = rep.GetHU_DataDynamic(item.GetDataKeyValue("ID"),
                                                       ProfileCommon.HU_TEMPLATE_TYPE.DECISION_ID,
                                                       folderName)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage("Dữ liệu không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                        If folderName = "" Then
                            ShowMessage("Thư mục không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

                    ' Kiểm tra file theo thông tin trong database
                    If Not Utilities.GetTemplateLinkFile(item.GetDataKeyValue("DECISION_TYPE_ID"),
                                                     folderName,
                                                     filePath,
                                                     extension,
                                                     iError) Then
                        Select Case iError
                            Case 1
                                ShowMessage("Biểu mẫu không tồn tại", NotifyType.Warning)
                                Exit Sub
                        End Select
                    End If
                    ' Export file mẫu
                    Using word As New WordCommon
                        word.ExportMailMerge(filePath,
                                             item.GetDataKeyValue("EMPLOYEE_CODE") & "_HSL_" &
                                             Format(Date.Now, "yyyyMMddHHmmss"),
                                             dtData,
                                             Response)
                    End Using
                Case CommonMessage.TOOLBARITEM_APPROVE_BATCH
                    If rgWorking.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Error)
                        Exit Sub
                    End If
                    Dim workingIds = New List(Of Decimal)
                    For Each selectedItem As GridDataItem In rgWorking.SelectedItems
                        workingIds.Add(selectedItem.GetDataKeyValue("ID"))
                    Next
                    Dim validateMsg = ValidateApprove(workingIds)
                    If validateMsg.Count > 0 Then
                        Translate(validateMsg.ToString(), NotifyType.Warning)
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_APPROVE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE_BATCH
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select

            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Function ValidateApprove(ByRef workingIds As List(Of Decimal)) As List(Of String)
        Dim result = New List(Of String)
        Using repo As New ProfileBusinessRepository
            Dim param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                    .IS_DISSOLVE = ctrlOrg.IsDissolve}

            Dim workings = repo.GetWorking(New WorkingDTO With {.Ids = workingIds}, param)
            For Each workingDto As WorkingDTO In workings
                If Not workingDto.STATUS_ID.HasValue Or workingDto.STATUS_ID.Value <> ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID Then
                    result.Add(String.Format("{0} ", workingDto.ID))
                End If
            Next
        End Using
        Return result
    End Function
    ''' <summary>
    ''' Load lai grid khi click node in treeview
    ''' Rebind=> reload lai ham NeedDataSource
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgWorking.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' event click nut tim kiem theo thang bien dong
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgWorking.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' event Yes/No cua message popup
    ''' update lai trang thai page, trang thai control sau khi process xong event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE_BATCH And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.TOOLBARITEM_APPROVE_BATCH
                UpdateControlState()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' add tooltip (so do to chuc) len tung row trong grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgWorking.ItemDataBound
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
    '            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
    '            datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow("ORG_DESC").Text)
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '    End Try

    'End Sub

#End Region

#Region "Custom"
    ''' <summary>
    ''' Thiet lap lai trang thai control
    ''' process event xoa du lieu
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            tbarWorkings.Enabled = True
            rgWorking.Enabled = True
            ctrlOrg.Enabled = True
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                Case CommonMessage.STATE_EDIT
                Case CommonMessage.STATE_DELETE
                    Using rep As New ProfileBusinessRepository
                        If rep.DeleteWorking(New WorkingDTO With {.ID = rgWorking.SelectedValue}) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgWorking.CurrentPageIndex = 0
                            rgWorking.MasterTableView.SortExpressions.Clear()

                            rgWorking.Rebind()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                    End Using

                Case CommonMessage.TOOLBARITEM_APPROVE_BATCH
                    Using rep As New ProfileBusinessRepository
                        Dim workingIds = New List(Of Decimal)
                        For Each selectedItem As GridDataItem In rgWorking.SelectedItems
                            workingIds.Add(selectedItem.GetDataKeyValue("ID"))
                        Next
                        If rep.ApproveWorkings(workingIds).Status = 1 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            rgWorking.CurrentPageIndex = 0
                            rgWorking.MasterTableView.SortExpressions.Clear()
                            rgWorking.Rebind()
                        End If
                    End Using
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Get data from database to grid
    ''' </summary>
    ''' <param name="isFull">If = true thi full data, =false load filter or phan trang</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim _filter As New WorkingDTO
        Try
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            Else
                rgWorking.DataSource = New List(Of EmployeeDTO)
                Exit Function
            End If
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}

            SetValueObjectByRadGrid(rgWorking, _filter)

            _filter.FROM_DATE = rdEffectDate.SelectedDate
            _filter.TO_DATE = rdExpireDate.SelectedDate
            _filter.IS_WAGE = True
            ' _filter.IS_MISSION = False
            _filter.IS_TER = chkTerminate.Checked

            Dim MaximumRows As Integer
            Dim Sorts As String = rgWorking.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetWorking(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetWorking(_filter, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    rgWorking.DataSource = rep.GetWorking(_filter, rgWorking.CurrentPageIndex, rgWorking.PageSize, MaximumRows, _param, Sorts)
                Else
                    rgWorking.DataSource = rep.GetWorking(_filter, rgWorking.CurrentPageIndex, rgWorking.PageSize, MaximumRows, _param)
                End If

                rgWorking.VirtualItemCount = MaximumRows
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

#End Region
#Region "Utility"
    Public Function GetUiResx(ByVal input) As String
        Return UI.ResourceManager.GetString(input)
    End Function
    Public Function GetErrorsResx(ByVal input) As String
        Return Errors.ResourceManager.GetString(input)
    End Function
#End Region
End Class
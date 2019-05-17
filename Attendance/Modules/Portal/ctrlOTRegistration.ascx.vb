Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Common.Common
Imports Attendance.AttendanceBusiness
Imports Telerik.Web.UI
Imports HistaffFrameworkPublic.HistaffFrameworkEnum
Imports HistaffFrameworkPublic

Public Class ctrlOTRegistration
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Public Overrides Property MustAuthorize As Boolean = False
    Dim psp As New AttendanceStoreProcedure

#Region "Property"

    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
    Public Property unit As String

    Property RegistrationList As List(Of AT_PORTAL_REG_DTO)
        Get
            Return ViewState(Me.ID & "_OT_REGISTRATIONDTOS")
        End Get
        Set(ByVal value As List(Of AT_PORTAL_REG_DTO))
            ViewState(Me.ID & "_OT_REGISTRATIONDTOS") = value
        End Set
    End Property

    Property OtRegistrationTotal As Int32
        Get
            Return ViewState(Me.ID & "_OtRegistrationTotal")
        End Get
        Set(ByVal value As Int32)
            ViewState(Me.ID & "_OtRegistrationTotal") = value
        End Set
    End Property

#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New AttendanceRepository
        Dim strId As String
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeletePortalReg(lstDeletes) Then
                        Refresh("UpdateView")
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                    rgMain.Rebind()
                Case CommonMessage.STATE_APPROVE
                    Dim lstApp As New List(Of AT_PORTAL_REG_DTO)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        Dim id As String = item.GetDataKeyValue("ID_REGGROUP").ToString
                        strId &= IIf(strId = vbNullString, id, "," & id)
                    Next
                    'Dim strGroup As String =String.Join (
                    Dim dtCheckSendApprove As DataTable = psp.CHECK_SEND_APPROVE(strId)
                    Dim repPublic As New EmailCommon
                    ' Lấy danh sách ID portal register
                    Dim sUser As String = LogHelper.CurrentUser.EMPLOYEE_CODE
                    Dim periodId As Integer = Int32.Parse(dtCheckSendApprove.Rows(0)("PERIOD_ID").ToString())
                    Dim signId As Integer = Int32.Parse(dtCheckSendApprove.Rows(0)("SIGN_ID").ToString())
                    Dim totalHours As Decimal = Decimal.Parse(dtCheckSendApprove.Rows(0)("SUMVAL").ToString())
                    Dim IdGroup1 As Decimal = Decimal.Parse(dtCheckSendApprove.Rows(0)("ID_REGGROUP").ToString())
                    Dim outNumber As Decimal = AttendanceRepositoryStatic.Instance.PRI_PROCESS_APP(EmployeeID, periodId, "OVERTIME", totalHours, 0, signId, IdGroup1)

                    If outNumber = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
            End Select
        Catch ex As Exception

        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        'rgMain.SetFilter()
        SetFilter(rgMain)
        rgMain.AllowCustomPaging = True
        rgMain.PageSize = Common.Common.DefaultPageSize
        CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Seperator, _
                         ToolbarItem.Submit, ToolbarItem.Export, ToolbarItem.Seperator, ToolbarItem.Delete)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim dtData As DataTable
        Using rep As New AttendanceRepository
            dtData = rep.GetOtherList("PORTAL_STATUS", True)
            If dtData IsNot Nothing Then
                Dim data = dtData.AsEnumerable().Where(Function(f) Not f.Field(Of Decimal?)("ID").HasValue _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.unsent).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.waitsend).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.aprrove).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.unaprrove).ToString() _
                                                           Or f.Field(Of Decimal?)("ID") = Int16.Parse(PortalStatus.unCBNS).ToString()).CopyToDataTable()
                FillRadCombobox(cboStatus, data, "NAME", "ID")

            End If

            rdRegDateFrom.SelectedDate = New DateTime(DateTime.Now.Year, 1, 1)
            rdRegDateTo.SelectedDate = New DateTime(DateTime.Now.Year, 1, 1).AddMonths(12).AddDays(-1)
        End Using
    End Sub

#End Region

#Region "Event"

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            rgMain.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        If item.GetDataKeyValue("STATUS") <> 0 And item.GetDataKeyValue("STATUS") <> PortalStatus.unsent And item.GetDataKeyValue("STATUS") <> PortalStatus.unaprrove And item.GetDataKeyValue("STATUS") <> PortalStatus.unCBNS Then
                            ShowMessage(Translate("Hành động này chỉ áp dụng đối với trạng thái đăng ký, chờ phê duyệt. Vui lòng chọn dòng khác."), NotifyType.Error)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        Dim dtDatas As DataTable
                        dtDatas = CreateDataFilter(True)
                        If dtDatas.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtDatas, "Overtime Record")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_SUBMIT
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim listDataCheck As New List(Of AT_PROCESS_DTO)
                    Dim datacheck As AT_PROCESS_DTO
                    'Kiểm tra các điều kiện trước khi xóa
                    For Each dr As Telerik.Web.UI.GridDataItem In rgMain.SelectedItems
                        If dr.GetDataKeyValue("STATUS") <> PortalStatus.unsent Then
                            ShowMessage(Translate("Trạng thái làm thêm này không thể gửi duyệt. Vui lòng chọn dòng khác."), NotifyType.Warning)
                            Exit Sub
                        End If
                        datacheck = New AT_PROCESS_DTO With {
                            .EMPLOYEE_ID = dr.GetDataKeyValue("ID_EMPLOYEE"),
                            .FROM_DATE = dr.GetDataKeyValue("REGIST_DATE"),
                            .FULL_NAME = dr.GetDataKeyValue("FULLNAME")
                        }
                        listDataCheck.Add(datacheck)
                    Next

                    Dim itemError As New AT_PROCESS_DTO
                    Using rep As New AttendanceRepository
                        Dim checkResult = rep.CheckTimeSheetApproveVerify(listDataCheck, "LEAVE", itemError)
                        If Not checkResult Then
                            ShowMessage(String.Format(Translate("TimeSheet of {0} in {1} has been approved"), itemError.FULL_NAME, itemError.FROM_DATE.Value.Month & "/" & itemError.FROM_DATE.Value.Year), NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

                    ctrlMessageBox.MessageText = Translate("Gửi duyệt. Bạn có chắc chắn?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_SUBMIT
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_SUBMIT And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_APPROVE
            UpdateControlState()
        End If

        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
        End If
        rgMain.Rebind()
    End Sub

#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim _filter As New AT_PORTAL_REG_DTO
        Try
            _filter.ID_EMPLOYEE = EmployeeID
            If Not String.IsNullOrEmpty(cboStatus.SelectedValue) Then
                _filter.STATUS = cboStatus.SelectedValue
            End If
            SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                'If Sorts IsNot Nothing Then
                '    Return rep.GetOtRegistration(_filter, Integer.MaxValue, 0, Integer.MaxValue, Sorts).ToTable()
                'Else
                '    Return rep.GetOtRegistration(_filter, Integer.MaxValue, 0, Integer.MaxValue).ToTable
                'End If
            Else
                'If Sorts IsNot Nothing Then
                '    Me.RegistrationList = rep.GetOtRegistration(_filter, Me.OtRegistrationTotal, rgMain.CurrentPageIndex, rgMain.PageSize, Sorts)
                'Else
                '    Me.RegistrationList = rep.GetOtRegistration(_filter, Me.OtRegistrationTotal, rgMain.CurrentPageIndex, rgMain.PageSize)
                'End If
            End If

            rgMain.VirtualItemCount = Me.OtRegistrationTotal
            rgMain.DataSource = Me.RegistrationList

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
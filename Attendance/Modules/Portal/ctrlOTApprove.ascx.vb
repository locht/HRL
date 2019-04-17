Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Telerik.Web.UI
Imports Profile
Imports Common.CommonBusiness
Imports Attendance.AttendanceBusiness

Public Class ctrlOTApprove
    Inherits Common.CommonView
    Dim log As New UserLog
    Dim psp As New AttendanceStoreProcedure
    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
    Public Overrides Property MustAuthorize As Boolean = False

    Public ReadOnly Property ProcessApprove As String
        Get
            Return ATConstant.GSIGNCODE_OVERTIME
        End Get
    End Property

    Public ReadOnly Property PageId As String
        Get
            ' Lấy Page ID của form đăng ký
            Return "ctrlOTRegister"
        End Get
    End Property
    Protected Property ListManual As List(Of AT_TIME_MANUALDTO)
        Get
            Return PageViewState(Me.ID & "_ListManual")
        End Get
        Set(ByVal value As List(Of AT_TIME_MANUALDTO))
            PageViewState(Me.ID & "_ListManual") = value
        End Set
    End Property

    Property ListComboData As Attendance.AttendanceBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Attendance.AttendanceBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
#Region "View"

    Property GridData As List(Of AttendanceBusiness.AT_PORTAL_REG_DTO)
        Get
            Return ViewState(Me.ID & "_GridData")
        End Get
        Set(value As List(Of AttendanceBusiness.AT_PORTAL_REG_DTO))
            ViewState(Me.ID & "_GridData") = value
        End Set
    End Property

    Public Overrides Sub ViewInit(e As System.EventArgs)
        grvWaiting.SetFilter()
        grvWaiting.AllowCustomPaging = True
    End Sub

    Public Overrides Sub ViewLoad(e As System.EventArgs)
        If Not IsPostBack Then
            Refresh()
        End If
    End Sub

    Public Overrides Sub Refresh(Optional Message As String = "")
        Try
            Dim db As New AttendanceRepository

            Dim filter As New AttendanceBusiness.ATRegSearchDTO With {
                .EmployeeIdName = txtEmployee.Text,
                .FromDate = rdpFromDateSearch.SelectedDate,
                .ToDate = rdpToDateSearch.SelectedDate,
                .Status = 0
            }

            If cboLeaveTypeSearch.SelectedIndex <> -1 Then
                filter.SignId = Utilities.ObjToInt(cboLeaveTypeSearch.SelectedValue)
            End If

            Dim listApprove = db.GetListWaitingForApproveOT(EmployeeID, ProcessApprove, filter)

            GridData = listApprove

            grvApproved.Rebind()
            grvDenied.Rebind()
            grvWaiting.Rebind()
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
        End Try
    End Sub

    Public Overrides Sub BindData()
        LoadComboData()

        rdpFromDateSearch.SelectedDate = Date.Now.FirstDateOfMonth()
        rdpToDateSearch.SelectedDate = Date.Now.LastDateOfMonth()
    End Sub

#End Region

#Region "Events"
    Private Sub grvApproved_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grvApproved.NeedDataSource
        Try
            Dim listApproved = GridData.Where(Function(p) p.STATUS = 2).ToList

            grvApproved.DataSource = listApproved
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub grvDenied_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grvDenied.NeedDataSource
        Try
            Dim listDenied = GridData.Where(Function(p) p.STATUS = 3).ToList

            grvDenied.DataSource = listDenied
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub grvWaiting_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grvWaiting.NeedDataSource
        Try
            Dim listWaiting = GridData.Where(Function(p) p.STATUS = 1 OrElse p.STATUS = -1).ToList

            grvWaiting.DataSource = listWaiting
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Refresh()
    End Sub

    Private Sub btnApprove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        Dim sAction As String = "OVERTIME"
        log = LogHelper.GetUserLog
        Try

            If grvWaiting.MasterTableView.GetSelectedItems().Length = 0 Then
                ShowMessage("Bạn chưa chọn bản đăng ký cần phê duyệt", NotifyType.Error)
                Exit Sub
            End If
            Dim sUser As String = LogHelper.CurrentUser.EMPLOYEE_CODE
            Dim result As DataTable
            Dim period_id As Decimal?
            Dim outNumber As Decimal?
            For idx = 0 To grvWaiting.SelectedItems.Count - 1
                Dim item As GridDataItem = grvWaiting.SelectedItems(idx)
                Dim ID = Decimal.Parse(item.GetDataKeyValue("ID"))
                Dim NOTE As String = txtNote.Text
                result = psp.GET_APPROVE_STATUS(ID, "OVERTIME")
                If result.Rows.Count > 0 Then

                    Dim employee_id As Integer = Int32.Parse(result(0)("EMPLOYEE_ID").ToString)
                    Dim employee_app As Integer = Int32.Parse(result(0)("EMPLOYEE_APPROVED").ToString)
                    period_id = result(0)("PE_PERIOD_ID").ToString

                    outNumber = psp.APPROVE_REG(employee_id, employee_app, period_id, 1, sAction, NOTE, ID)
                End If
            Next
            Refresh()
            grvWaiting.Dispose()

        Catch ex As Exception
            ShowMessage(Translate(ex.Message), NotifyType.Error)
        End Try
    End Sub

    Private Sub btnDeny_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeny.Click
        Dim sAction = "OVERTIME"
        log = LogHelper.GetUserLog
        Dim sUser As String = LogHelper.CurrentUser.EMPLOYEE_CODE
        Try

            If grvWaiting.MasterTableView.GetSelectedItems().Length = 0 Then
                ShowMessage("Bạn chưa chọn dòng dữ liệu để từ chối phê duyệt ", NotifyType.Error)
                Exit Sub
            End If
            Dim result As DataTable
            Dim period_id As Decimal?
            Dim outNumber As Decimal?
            For idx = 0 To grvWaiting.SelectedItems.Count - 1
                Dim item As GridDataItem = grvWaiting.SelectedItems(idx)
                Dim ID = Decimal.Parse(item.GetDataKeyValue("ID"))
                Dim NOTE As String = txtNote.Text
                result = psp.GET_APPROVE_STATUS(ID, "OVERTIME")
                If result.Rows.Count > 0 Then

                    Dim employee_id As Integer = Int32.Parse(result(0)("EMPLOYEE_ID").ToString)
                    Dim employee_app As Integer = Int32.Parse(result(0)("EMPLOYEE_APPROVED").ToString)
                    period_id = result(0)("PE_PERIOD_ID").ToString

                    outNumber = psp.APPROVE_REG(employee_id, employee_app, period_id, 2, sAction, NOTE, ID)
                End If
            Next
            Refresh()
            grvWaiting.Dispose()
        Catch ex As Exception
            ShowMessage(Translate(ex.Message), NotifyType.Error)
        End Try
    End Sub
#End Region

#Region "Custom"
    Private Sub LoadComboData()
       Dim rep As New AttendanceRepository
        Try
            If ListComboData Is Nothing Then
                ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO
                ListComboData.GET_LIST_HS_OT = True
                rep.GetComboboxData(ListComboData)
            End If
            FillRadCombobox(cboLeaveTypeSearch, ListComboData.LIST_LIST_HS_OT, "CODE", "ID", True)
            If ListComboData.LIST_LIST_HS_OT.Count > 0 Then
                cboLeaveTypeSearch.SelectedIndex = 0
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, NotifyType.Error)
        End Try
    End Sub
#End Region

End Class
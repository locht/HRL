Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Common.Common
Imports Attendance.AttendanceBusiness
Imports Telerik.Web.UI
Imports HistaffFrameworkPublic.HistaffFrameworkEnum

Public Class ctrlLeaveRegistrationNewEdit
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"

    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
    Protected Property EmployeeDto As DataTable
        Get
            Return PageViewState(Me.ID & "_EmployeeDto")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_EmployeeDto") = value
        End Set
    End Property
    Protected Property ListManual As List(Of AT_TIME_MANUALDTO)
        Get
            Return PageViewState(Me.ID & "_ListFML")
        End Get
        Set(ByVal value As List(Of AT_TIME_MANUALDTO))
            PageViewState(Me.ID & "_ListFML") = value
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
    Property leaveMaster As AT_PORTAL_REG_LIST_DTO
        Get
            Return ViewState(Me.ID & "_leaveMaster")
        End Get
        Set(ByVal value As AT_PORTAL_REG_LIST_DTO)
            ViewState(Me.ID & "_leaveMaster") = value
        End Set
    End Property
    Property leaveDetails As List(Of AT_PORTAL_REG_DTO)
        Get
            Return ViewState(Me.ID & "_leaveDetails")
        End Get
        Set(ByVal value As List(Of AT_PORTAL_REG_DTO))
            ViewState(Me.ID & "_leaveDetails") = value
        End Set
    End Property
    Property leaveEmpDetails As List(Of LEAVE_DETAIL_EMP_DTO)
        Get
            Return ViewState(Me.ID & "_leaveEmpDetails")
        End Get
        Set(ByVal value As List(Of LEAVE_DETAIL_EMP_DTO))
            ViewState(Me.ID & "_leaveEmpDetails") = value
        End Set
    End Property

    Property userType As String
        Get
            Return ViewState(Me.ID & "_userType")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_userType") = value
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

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub
    Public Overrides Sub BindData()
        Dim rep As New AttendanceRepository
        Try
            If ListComboData Is Nothing Then
                ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO
                ListComboData.GET_LIST_TYPE_MANUAL_LEAVE = True
                rep.GetComboboxData(ListComboData)
            End If
            FillRadCombobox(cboleaveType, ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE, "NAME_VN", "ID", True)
            ListManual = ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE
            If ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE.Count > 0 Then
                cboleaveType.SelectedIndex = 0
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                Dim id As Decimal = 0
                Dim empId As Decimal = 0
                Decimal.TryParse(Request.QueryString("id"), id)
                Decimal.TryParse(Request.QueryString("empId"), empId)
                hidID.Value = id
                hidValid.Value = 0
                Dim state = Request.QueryString("id")
                userType = Request.QueryString("typeUser")
                CurrentState = CommonMessage.STATE_NORMAL
                Dim dto As New AT_PORTAL_REG_LIST_DTO
                dto.ID = hidID.Value
                If hidID.Value = 0 Then
                    EmployeeID = LogHelper.CurrentUser.EMPLOYEE_ID
                    dto.EMPLOYEE_ID = EmployeeID
                Else
                    EmployeeID = If(empId = 0, LogHelper.CurrentUser.EMPLOYEE_ID, empId)
                    dto.EMPLOYEE_ID = If(empId = 0, LogHelper.CurrentUser.EMPLOYEE_ID, empId)
                End If

                leaveMaster = New AT_PORTAL_REG_LIST_DTO
                leaveDetails = New List(Of AT_PORTAL_REG_DTO)
                EmployeeDto = New DataTable

                Using rep As New AttendanceRepository
                    EmployeeDto = rep.GetEmployeeInfor(EmployeeID, Nothing, rdFromDate.SelectedDate)
                    If dto.ID > 0 Then
                        leaveMaster = rep.GetLeaveRegistrationById(dto)
                        If leaveMaster.ID > 0 Then
                            leaveDetails = rep.GetLeaveRegistrationDetailById(leaveMaster.ID)
                        End If
                    End If
                End Using
                If EmployeeDto IsNot Nothing AndAlso EmployeeDto.Rows.Count > 0 Then
                    txtFullName.Text = EmployeeDto.Rows(0)("FULLNAME_VN")
                    txtEmpCode.Text = EmployeeDto.Rows(0)("EMPLOYEE_CODE")
                    txtDepartment.Text = EmployeeDto.Rows(0)("ORG_NAME")
                    txtTitle.Text = EmployeeDto.Rows(0)("TITLE_NAME_EN")
                    'rntEntitlement.Value = If(EmployeeDto.Rows(0)("DAY_LEAVE_NUMBER_ADJUST") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("DAY_LEAVE_NUMBER_ADJUST").ToString()))
                    'rntSeniority.Value = If(EmployeeDto.Rows(0)("WORKING_TIME_HAVE_ADJUST") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("WORKING_TIME_HAVE_ADJUST").ToString()))
                    'rntBrought.Value = If(EmployeeDto.Rows(0)("PREV_HAVE") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("PREV_HAVE").ToString()))
                    'rntTotal.Value = If(EmployeeDto.Rows(0)("TOTAL_HAVE") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("TOTAL_HAVE").ToString()))
                    'rntTotalTaken.Value = If(EmployeeDto.Rows(0)("CUR_USED") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("CUR_USED").ToString()))
                    'rntBalance.Value = If(EmployeeDto.Rows(0)("CUR_HAVE") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("CUR_HAVE").ToString()))
                End If
                If leaveMaster IsNot Nothing Then
                    hidStatus.Value = If(leaveMaster.STATUS.HasValue, leaveMaster.STATUS, 0)
                    If leaveMaster.ID_SIGN.HasValue Then
                        cboleaveType.SelectedValue = leaveMaster.ID_SIGN
                    End If
                    If leaveMaster.TOTAL_LEAVE.HasValue Then
                        txtDayRegist.Text = leaveMaster.TOTAL_LEAVE
                        rntxDayRegist.Value = leaveMaster.TOTAL_LEAVE
                    End If
                    If leaveMaster.FROM_DATE.HasValue Then
                        rdFromDate.SelectedDate = leaveMaster.FROM_DATE
                    End If
                    If leaveMaster.TO_DATE.HasValue Then
                        rdToDate.SelectedDate = leaveMaster.TO_DATE
                    End If
                    txtNote.Text = leaveMaster.NOTE
                    rgData.Rebind()
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Try
            If IsPostBack Then
                Exit Sub
            End If
            tbarMainToolBar.Items(1).Enabled = True
            tbarMainToolBar.Items(0).Enabled = False

            Select Case hidStatus.Value
                '0 Khai báo thông tin 
                '16 Da luu
                '19 Khong duyet qltt
                '20 Khong xac nhan nhan su
                '22 Khong duyet GM
                Case 0, PortalStatus.unsent
                    ', PortalStatus.Saved, PortalStatus.UnApprovedByLM, PortalStatus.UnVerifiedByHr
                    If userType = "User" Then
                        tbarMainToolBar.Items(0).Enabled = True
                        EnableControlAll(True, cboleaveType, rdFromDate, rdToDate, txtNote)
                    Else
                        tbarMainToolBar.Items(0).Enabled = False
                        EnableControlAll(False, cboleaveType, rdFromDate, rdToDate, txtNote)
                    End If
                Case Else
                    EnableControlAll(False, cboleaveType, rdFromDate, rdToDate, txtNote)
            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(cboleaveType, rdFromDate, rdToDate, txtNote, hidID)

                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If rntxDayRegist.Value.HasValue AndAlso rntxDayRegist.Value <= 0 Then
                            ShowMessage(Translate("Số ngày nghỉ phải > 0."), NotifyType.Warning)
                            UpdateControlState()
                            Exit Sub
                        End If

                        Dim isInsert As Boolean = True
                        Dim obj As New AT_PORTAL_REG_LIST_DTO
                        obj.EMPLOYEE_ID = EmployeeID

                        obj.ID_SIGN = Decimal.Parse(cboleaveType.SelectedValue)
                        obj.FROM_DATE = rdFromDate.SelectedDate
                        obj.TO_DATE = rdToDate.SelectedDate
                        obj.TOTAL_LEAVE = rntxDayRegist.Value 'txtDayRegist.Text
                        obj.NOTE = txtNote.Text
                        obj.STATUS = 6860
                        hidStatus.Value = 6860
                        'lay thong tin detail
                        Dim details As New List(Of AT_PORTAL_REG_DTO)
                        For Each item As GridDataItem In rgData.Items
                            If item.Edit = True Then
                                Dim edit = CType(item, GridEditableItem)
                                Dim cbo As RadComboBox = CType(edit.FindControl("cboLeaveValue"), RadComboBox)
                                Dim leave_value As Decimal = If(cbo.SelectedValue = "0", 0, If(cbo.SelectedValue = "0.5", 0.5, 1))
                                If leave_value > 0 Then
                                    Dim objDetail As New AT_PORTAL_REG_DTO
                                    With objDetail
                                        .ID_EMPLOYEE = EmployeeID
                                        .ID_SIGN = obj.ID_SIGN
                                        .REGDATE = item.GetDataKeyValue("EFFECTIVEDATE")
                                        .NVALUE = leave_value
                                        .SVALUE = "LEAVE"
                                        .NOTE = txtNote.Text
                                    End With
                                    details.Add(objDetail)
                                End If
                            End If
                        Next
                        If Not details.Count > 0 And hidID.Value = 0 Then
                            ShowMessage(Translate("Không tìm thấy chi tiết ngày nghỉ."), NotifyType.Warning)
                            UpdateControlState()
                            Return
                        End If
                        Using rep As New AttendanceRepository
                            If hidID.Value <> 0 Then
                                isInsert = False
                            End If
                            Dim itemExist = New AT_PORTAL_REG_DTO
                            Dim isOverAnnualLeave As Boolean = False
                            If isInsert Then
                                rep.InsertPortalRegList(obj, details, hidID.Value, itemExist, isOverAnnualLeave)
                                obj.ID = hidID.Value
                            Else
                                obj.ID = hidID.Value
                                rep.ModifyPortalRegList(obj, details, itemExist, isOverAnnualLeave)
                            End If

                            If isOverAnnualLeave Then
                                ShowMessage(Translate("Không được đăng ký nghỉ vượt quá số ngày phép con lại trong năm"), NotifyType.Warning)
                                Return
                            End If

                            If itemExist IsNot Nothing Then
                                ShowMessage(String.Format(Translate("Leave Request has been exist in {0}"), itemExist.REGDATE.Value.ToString("dd/MM/yyyy")), NotifyType.Warning)
                                Return
                            End If

                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlLeaveRegistration")
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                        End Using
                    End If
                Case CommonMessage.TOOLBARITEM_SUBMIT
                    ctrlMessageBox.MessageText = Translate("Thông tin đã gửi duyệt sẽ không được chỉnh sửa. Bạn chắc chắn muốn gửi duyệt?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    If userType = "User" Then
                        Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlLeaveRegistration")
                    ElseIf userType = "LM" Then
                        Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlLeaveRegistrationByManager")

                    ElseIf userType = "HR" Then
                        Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlLeaveRegistrationByHR")
                    End If


            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                'Using rep As New ProfileBusinessRepository
                '    Dim lstID As New List(Of Decimal)
                '    lstID.Add(hidID.Value)
                '    If lstID.Count > 0 Then
                '        rep.SendEmployeeEdit(lstID)
                '        hidStatus.Value = 1
                '        lbStatus.Text = "Thông tin chỉnh sửa đang ở trạng thái [ Chờ phê duyệt ], Bạn không thể chỉnh sửa"
                '        tbarMainToolBar.Items(0).Enabled = False
                '        tbarMainToolBar.Items(3).Enabled = False
                '        EnableControlAll(False, cboFamilyStatus, cboNav_District, cboNav_Province, cboNav_Ward,
                '                    cboPer_District, cboPer_Province, cboPer_Ward, txtID_NO,
                '                    txtIDPlace, txtNavAddress, txtPerAddress, rdIDDate)

                '    End If
                '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                'End Using
            End If


        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    'Protected Sub btnDetail_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDetail.Click
    '    Try
    '        'Hien thi thong tin grid
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub
    Protected Sub rdFromDate_Select(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdFromDate.SelectedDateChanged
        Try
            leaveMaster = New AT_PORTAL_REG_LIST_DTO
            Using rep As New AttendanceRepository
                EmployeeDto = rep.GetEmployeeInfor(EmployeeID, Nothing, rdFromDate.SelectedDate)
                If EmployeeDto IsNot Nothing AndAlso EmployeeDto.Rows.Count > 0 Then
                    txtFullName.Text = EmployeeDto.Rows(0)("FULLNAME_VN")
                    txtEmpCode.Text = EmployeeDto.Rows(0)("EMPLOYEE_CODE")
                    txtDepartment.Text = EmployeeDto.Rows(0)("ORG_NAME")
                    txtTitle.Text = EmployeeDto.Rows(0)("TITLE_NAME_EN")
                    'rntEntitlement.Value = If(EmployeeDto.Rows(0)("DAY_LEAVE_NUMBER_ADJUST") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("DAY_LEAVE_NUMBER_ADJUST").ToString()))
                    'rntSeniority.Value = If(EmployeeDto.Rows(0)("WORKING_TIME_HAVE_ADJUST") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("WORKING_TIME_HAVE_ADJUST").ToString()))
                    'rntBrought.Value = If(EmployeeDto.Rows(0)("PREV_HAVE") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("PREV_HAVE").ToString()))
                    'rntTotal.Value = If(EmployeeDto.Rows(0)("TOTAL_HAVE") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("TOTAL_HAVE").ToString()))
                    'rntTotalTaken.Value = If(EmployeeDto.Rows(0)("CUR_USED") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("CUR_USED").ToString()))
                    'rntBalance.Value = If(EmployeeDto.Rows(0)("CUR_HAVE") Is Nothing, Nothing, Decimal.Parse(EmployeeDto.Rows(0)("CUR_HAVE").ToString()))
                End If
            End Using

            If rdFromDate.SelectedDate IsNot Nothing And rdToDate.SelectedDate IsNot Nothing Then
                rgData.Rebind()
                For Each item As GridDataItem In rgData.MasterTableView.Items
                    item.Edit = True
                Next
                rgData.MasterTableView.Rebind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub rdToDate_Select(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdToDate.SelectedDateChanged
        Try
            If rdFromDate.SelectedDate IsNot Nothing And rdToDate.SelectedDate IsNot Nothing Then
                rgData.Rebind()
                For Each item As GridDataItem In rgData.MasterTableView.Items
                    item.Edit = True
                Next
                rgData.MasterTableView.Rebind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub rgData_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Protected Sub rgData_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemCreated
        If TypeOf e.Item Is GridDataItem Then
            Dim control As GridDataItem = CType(e.Item, GridDataItem)
            Dim combo As RadComboBox = CType(control.FindControl("cboLeaveValue"), RadComboBox) ' CType(insertItem("cboLeaveValue").FindControl("radComboBoxOccCode"), RadComboBox)
            If combo IsNot Nothing Then
                combo.AutoPostBack = True
                AddHandler combo.SelectedIndexChanged, AddressOf combo_SelectedIndexChanged
            End If
        End If
        If Not String.IsNullOrEmpty(hidID.Value) AndAlso hidID.Value > 0 Then
            Dim cmdItem As GridItem = rgData.MasterTableView.GetItems(GridItemType.CommandItem)(0)
            Dim editDetail As RadButton = CType(cmdItem.FindControl("btnEditDetail"), RadButton)
            editDetail.Visible = True
        End If
    End Sub
    Protected Sub combo_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            For Each item As GridDataItem In rgData.EditItems
                Dim edit = CType(item, GridEditableItem)
                Dim effect As Date = item.GetDataKeyValue("EFFECTIVEDATE")
                Dim detail = leaveEmpDetails.First(Function(f) f.EFFECTIVEDATE = effect)
                Dim index = leaveEmpDetails.IndexOf(detail)
                Dim cbo As RadComboBox = CType(edit.FindControl("cboLeaveValue"), RadComboBox)
                If cbo IsNot Nothing Then
                    If cbo.SelectedValue = "0.5" Then
                        detail.LEAVE_VALUE = 0.5
                    ElseIf cbo.SelectedValue = "1" Then
                        detail.LEAVE_VALUE = 1
                    Else
                        detail.LEAVE_VALUE = 0
                    End If
                    leaveEmpDetails(index) = detail
                End If
            Next
            txtDayRegist.Text = leaveEmpDetails.Select(Function(f) f.LEAVE_VALUE).Sum
            rntxDayRegist.Value = leaveEmpDetails.Select(Function(f) f.LEAVE_VALUE).Sum
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", "showDetail('block');", True)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub rgData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        If e.Item.Edit Then
            Dim edit = CType(e.Item, GridEditableItem)
            Dim cbo As RadComboBox
            cbo = CType(edit.FindControl("cboLeaveValue"), RadComboBox)
            If cbo IsNot Nothing Then
                Dim table As DataTable = LoadComboLeaveValue()
                FillRadCombobox(cbo, table, "VALUE", "ID")
                'cbo.SelectedValue = edit.GetDataKeyValue("LEAVE_VALUE").ToString
                If edit.GetDataKeyValue("LEAVE_VALUE").ToString = "0" Then
                    If edit.GetDataKeyValue("IS_OFF") = "1" Then
                        EnableControlAll(False, cbo)
                    End If
                    cbo.SelectedValue = "0"
                ElseIf edit.GetDataKeyValue("LEAVE_VALUE").ToString = "1" Then
                    cbo.SelectedValue = "1"
                Else
                    cbo.SelectedValue = "0.5"
                End If
                cbo.Width = Unit.Percentage(100)
            End If
        End If
    End Sub


    Private Sub rgData_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgData.ItemCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case e.CommandName
                Case "EditDetail"
                    'rdToDate_Select(sender, Nothing)
                    'rgData.Rebind()
                    For Each item As GridDataItem In rgData.MasterTableView.Items
                        item.Edit = True
                    Next
                    rgData.MasterTableView.Rebind()
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", "showDetail('');", True)

            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"
    Private Function Valid() As Boolean
        Dim value As Double
        'value = IIf(String.IsNullOrEmpty(txtLeaveReUse.Text), 0, Double.Parse(txtLeaveReUse.Text)
    End Function
    Protected Function CreateDataFilter(Optional ByVal fromDate As Date? = Nothing, Optional ByVal toDate As Date? = Nothing, Optional ByVal isFull As Boolean = False) As DataTable
        Try
            Using rep As New AttendanceRepository
                If fromDate IsNot Nothing Or toDate IsNot Nothing Then
                    leaveEmpDetails = rep.GetLeaveEmpDetail(EmployeeID, fromDate.Value, toDate.Value, If(IsNumeric(hidID.Value), hidID.Value, 0) <> 0)
                ElseIf rdFromDate.SelectedDate IsNot Nothing And rdToDate.SelectedDate IsNot Nothing Then
                    leaveEmpDetails = rep.GetLeaveEmpDetail(EmployeeID, rdFromDate.SelectedDate, rdToDate.SelectedDate, If(IsNumeric(hidID.Value), hidID.Value, 0) <> 0)
                    'Truog hop k co phan ca thi generate ca default

                    If leaveEmpDetails IsNot Nothing AndAlso leaveEmpDetails.Count = 0 Then
                        leaveEmpDetails = New List(Of LEAVE_DETAIL_EMP_DTO)
                        'Initial default list 
                        Dim selectedFromDate = rdFromDate.SelectedDate
                        Dim selectedToDate = rdToDate.SelectedDate
                        While selectedFromDate.Value.Date <= selectedToDate.Value.Date
                            Dim leaveEmpDetail = New LEAVE_DETAIL_EMP_DTO
                            leaveEmpDetail.EFFECTIVEDATE = selectedFromDate
                            leaveEmpDetail.LEAVE_VALUE = 1
                            leaveEmpDetail.IS_UPDATE = 0
                            leaveEmpDetails.Add(leaveEmpDetail)
                            selectedFromDate = selectedFromDate.Value.AddDays(1)
                        End While
                    End If
                Else
                    leaveEmpDetails = New List(Of LEAVE_DETAIL_EMP_DTO)
                End If
                If leaveDetails IsNot Nothing Then
                    For Each item In leaveDetails
                        If leaveEmpDetails IsNot Nothing AndAlso leaveEmpDetails.Count > 0 Then
                            Dim detail = leaveEmpDetails.FirstOrDefault(Function(f) f.EFFECTIVEDATE = item.REGDATE)
                            If detail IsNot Nothing Then
                                Dim index = leaveEmpDetails.IndexOf(detail)
                                detail.LEAVE_VALUE = item.NVALUE
                                leaveEmpDetails(index) = detail
                            End If
                        End If
                    Next
                End If
            End Using
            If leaveEmpDetails IsNot Nothing Then
                txtDayRegist.Text = 0
                rntxDayRegist.Value = 0
                If leaveEmpDetails IsNot Nothing Then
                    txtDayRegist.Text = leaveEmpDetails.Select(Function(f) f.LEAVE_VALUE).Sum
                    rntxDayRegist.Value = leaveEmpDetails.Select(Function(f) f.LEAVE_VALUE).Sum
                End If
                rgData.DataSource = leaveEmpDetails.OrderBy(Function(f) f.EFFECTIVEDATE)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function LoadComboLeaveValue() As DataTable
        Try
            Dim table As New DataTable
            table.Columns.Add("VALUE", GetType(String))
            table.Columns.Add("ID", GetType(String))
            Dim row As DataRow
            row = table.NewRow
            row("ID") = "0"
            row("VALUE") = ""
            table.Rows.Add(row)

            row = table.NewRow
            row("ID") = "0.5"
            row("VALUE") = "0.5"
            table.Rows.Add(row)

            row = table.NewRow
            row("ID") = "1"
            row("VALUE") = "1"
            table.Rows.Add(row)
            Return table
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function
#End Region

End Class
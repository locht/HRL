Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Common.Common
Imports Common.CommonBusiness
Imports Attendance.AttendanceBusiness
Imports Telerik.Web.UI
Imports HistaffFrameworkPublic.HistaffFrameworkEnum
Imports System.IO
Imports Telerik.Web.UI.Calendar
Imports Profile.ProfileBusiness

Public Class ctrlLeaveRegistrationNewEdit
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"
    Public ReadOnly Property CurrentUser As UserDTO
        Get
            Return LogHelper.CurrentUser
        End Get
    End Property
    Public ReadOnly Property ApproveProcess As String
        Get
            Return ATConstant.GSIGNCODE_LEAVE
        End Get
    End Property

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
    'EmployeeID
    Property EmployeeID As Decimal
        Get
            Return ViewState(Me.ID & "_EmployeeID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_EmployeeID") = value
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

    Property dtDetail As DataTable
        Get
            Return ViewState(Me.ID & "_dtDetail")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDetail") = value
        End Set
    End Property
    Property rPH As DataRow
        Get
            Return ViewState(Me.ID & "_rPH")
        End Get
        Set(ByVal value As DataRow)
            ViewState(Me.ID & "_rPH") = value
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
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Submit, ToolbarItem.Cancel)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dsLeaveSheet As New DataSet()
        Try
            'If IsPostBack Then
            '    rtEmployee_id.Text = LogHelper.CurrentUser.EMPLOYEE_ID
            '    Using rep As New AttendanceRepository
            '        EmployeeDto = rep.GetEmployeeInfor(EmployeeID, Nothing)
            '    End Using
            'End If
            rtEmployee_id.Text = LogHelper.CurrentUser.EMPLOYEE_ID
            Dim startTime As DateTime = DateTime.UtcNow
            Message = Request.Params("VIEW")
            Dim Struct As Decimal = 1
            Dim ID_PH As Decimal = 0
            If IsNumeric(Request.Params("ID")) Then
                Struct = 0
                ID_PH = Decimal.Parse(Request.Params("ID"))
            End If
            Using rep As New AttendanceRepository
                dsLeaveSheet = rep.GetLeaveSheet_ById(ID_PH, Struct)
            End Using
            If dsLeaveSheet IsNot Nothing Then
                If dsLeaveSheet.Tables(0) IsNot Nothing Then
                    rPH = dsLeaveSheet.Tables(0).NewRow
                    If dsLeaveSheet.Tables(0).Rows.Count > 0 Then
                        rPH = dsLeaveSheet.Tables(0).Rows(0)
                    End If
                End If
                If dsLeaveSheet.Tables(1) IsNot Nothing AndAlso dtDetail Is Nothing Then
                    dtDetail = dsLeaveSheet.Tables(1).Clone()
                    dtDetail = dsLeaveSheet.Tables(1)
                End If
            End If
            FillData(Decimal.Parse(rtEmployee_id.Text.Trim))
            Select Case Message
                Case "TRUE"
                    CreateDataBinDing(1)
            End Select
        Catch ex As Exception
            Throw ex
        Finally
            dsLeaveSheet.Dispose()
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Try

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    Protected Sub cbSTATUS_SHIFT_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            Dim edit = CType(sender, RadComboBox)
            If edit.Enabled = False Then
                Exit Sub
            End If
            Dim item = CType(edit.NamingContainer, GridEditableItem)
            ' If Not IsNumeric(edit.SelectedValue) Then Exit Sub
            Dim EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")
            Dim LEAVE_DAY = item.GetDataKeyValue("LEAVE_DAY")
            For Each rows In dtDetail.Rows
                If rows("LEAVE_DAY") = LEAVE_DAY Then
                    rows("STATUS_SHIFT") = If(IsNumeric(edit.SelectedValue), edit.SelectedValue, 0)
                    If edit.SelectedValue IsNot Nothing AndAlso edit.SelectedValue <> "" Then
                        'rows("DAY_NUM") = 0.5
                        rows("DAY_NUM") = Decimal.Parse(rows("SHIFT_DAY")) / 2
                    Else
                        'rows("DAY_NUM") = 1
                        rows("DAY_NUM") = Decimal.Parse(rows("SHIFT_DAY"))
                    End If
                    Exit For
                End If
            Next
            rgData.Rebind()
            For Each items As GridDataItem In rgData.MasterTableView.Items
                items.Edit = True
            Next
            rgData.MasterTableView.Rebind()
            Cal_DayLeaveSheet()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim objValidate As New AT_LEAVESHEETDTO
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        'check so ngay dang ky nghi
                        If Not IsNumeric(rnDAY_NUM.Value) OrElse rnDAY_NUM.Value < 1 Then
                            ShowMessage(Translate("Số ngày đăng ký nghỉ phải lơn hơn 0"), NotifyType.Warning)
                            Exit Sub
                        End If
                        CreateDataBinDing(0)
                        objValidate.LEAVE_FROM = rdLEAVE_FROM.SelectedDate
                        objValidate.LEAVE_TO = rdLEAVE_TO.SelectedDate
                        objValidate.ID = Utilities.ObjToDecima(rPH("ID"))
                        objValidate.EMPLOYEE_ID = CDec(rtEmployee_id.Text)
                        If (New AttendanceBusinessClient).ValidateLeaveSheetDetail(objValidate) = False Then
                            ShowMessage(Translate("Ngày đăng ký nghỉ đã bị trùng"), NotifyType.Warning)
                            Exit Sub
                        End If
                        'If valSum.Page.IsValid Then
                        If Utilities.ObjToString(rPH("S_CODE")) = "A" Then 'TRANG THAI approve
                            ShowMessage(Translate("Đơn đã Phê duyệt. Không thể chỉnh sửa !"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If SaveDB() Then
                            Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlLeaveRegistration")
                        Else
                            ShowMessage(Translate("Xảy ra lỗi"), NotifyType.Error)
                        End If
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule')")
                    End If
                Case CommonMessage.TOOLBARITEM_SUBMIT
                    Select Case Utilities.ObjToString(rPH("S_CODE"))
                        Case "W"
                            ShowMessage(Translate("Đơn đang Chờ phê duyệt. Vui lòng thử lại !"), NotifyType.Warning)
                            Exit Sub
                        Case "A"
                            ShowMessage(Translate("Đơn đã Phê duyệt. Vui lòng thử lại !"), NotifyType.Warning)
                            Exit Sub
                    End Select
                    ctrlMessageBox.MessageText = Translate("Bạn có muốn gửi phê duyệt?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_SUBMIT
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlLeaveRegistration")
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim objValidate As New AT_LEAVESHEETDTO
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_SUBMIT And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                SetData_Controls(objValidate, 0)
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
                combo.Enabled = False
            End If
        End If
        If Not String.IsNullOrEmpty(hidID.Value) AndAlso hidID.Value > 0 Then
            Dim cmdItem As GridItem = rgData.MasterTableView.GetItems(GridItemType.CommandItem)(0)
            Dim editDetail As RadButton = CType(cmdItem.FindControl("btnEditDetail"), RadButton)
            editDetail.Enabled = False
        End If
    End Sub

    Protected Sub combo_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs)
        Try

            ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", "showDetail('block');", True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rgData_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles rgData.ItemDataBound
        Dim cbo As New RadComboBox
        Dim arr As New ArrayList()
        Try
            If e.Item.Edit Then
                Dim edit = CType(e.Item, GridEditableItem)
                Dim item As GridDataItem = CType(e.Item, GridDataItem)
                cbo = CType(edit.FindControl("cbSTATUS_SHIFT"), RadComboBox)
                arr.Add(New DictionaryEntry("", Nothing))
                arr.Add(New DictionaryEntry("Đầu ca", 1))
                arr.Add(New DictionaryEntry("Cuối ca", 2))
                With cbo
                    .DataSource = arr
                    .DataValueField = "Value"
                    .DataTextField = "Key"
                    cbo.DataBind()
                    .SelectedIndex = 0
                End With
                SetDataToGrid(edit)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            cbo.Dispose()
            arr = Nothing
        End Try
    End Sub

    Private Sub rgData_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgData.ItemCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case e.CommandName
                Case "EditDetail"
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

    Private Sub rdLEAVE_FROM_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdLEAVE_FROM.SelectedDateChanged, rdLEAVE_TO.SelectedDateChanged
        Try
            If (Not IsDate(rdLEAVE_FROM.SelectedDate) OrElse Not IsDate(rdLEAVE_TO.SelectedDate) OrElse dtDetail Is Nothing OrElse Not IsNumeric(rtEmployee_id.Text)) Then Exit Sub
            GetLeaveSheet_Detail()
            Cal_DayLeaveSheet()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cbMANUAL_ID_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cbMANUAL_ID.SelectedIndexChanged
        Try
            If (Not IsDate(rdLEAVE_FROM.SelectedDate) OrElse Not IsDate(rdLEAVE_TO.SelectedDate) OrElse dtDetail Is Nothing OrElse Not IsNumeric(rtEmployee_id.Text)) Then Exit Sub
            GetLeaveSheet_Detail()
            Cal_DayLeaveSheet()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal fromDate As Date? = Nothing, Optional ByVal toDate As Date? = Nothing, Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_LEAVESHEETDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgData.VirtualItemCount = dtDetail.Rows.Count
            rgData.DataSource = dtDetail
            Cal_DayLeaveSheet()
        Catch ex As Exception
        End Try
        Return New DataTable()
    End Function

    Private Sub Cal_DayLeaveSheet()
        Try
            Dim sumDay As Decimal = dtDetail.Compute("SUM(DAY_NUM)", "1=1")
            rnDAY_NUM.NumberFormat.AllowRounding = False
            rnDAY_NUM.NumberFormat.DecimalDigits = 2
            rnDAY_NUM.Value = sumDay

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub CreateDataBinDing(ByVal Mode As Decimal)
        '1 set data in list control
        '0 get data to list control
        Select Case Mode
            Case 1
                For Each ctrs As Control In RadPane1.Controls
                    Try
                        Select Case ctrs.ID.ToString.ToUpper.Substring(0, 2)
                            Case "cb".ToUpper
                                CType(ctrs, RadComboBox).SelectedValue = rPH(ctrs.ID.ToString.ToUpper.Substring(2))
                            Case "rt".ToUpper
                                CType(ctrs, RadTextBox).Text = rPH(ctrs.ID.ToString.ToUpper.Substring(2))
                            Case "rn".ToUpper
                                CType(ctrs, RadNumericTextBox).Value = rPH(ctrs.ID.ToString.ToUpper.Substring(2))
                            Case "rd".ToUpper
                                CType(ctrs, RadDatePicker).SelectedDate = rPH(ctrs.ID.ToString.ToUpper.Substring(2))
                            Case Else
                                Continue For
                        End Select
                    Catch ex As Exception
                        Continue For
                    End Try
                Next
            Case 0
                For Each ctrs As Control In RadPane1.Controls
                    Try
                        Select Case ctrs.ID.ToString.ToUpper.Substring(0, 2)
                            Case "cb".ToUpper
                                rPH(ctrs.ID.ToString.ToUpper.Substring(2)) = CType(ctrs, RadComboBox).SelectedValue
                            Case "rt".ToUpper
                                rPH(ctrs.ID.ToString.ToUpper.Substring(2)) = CType(ctrs, RadTextBox).Text
                            Case "rn".ToUpper
                                rPH(ctrs.ID.ToString.ToUpper.Substring(2)) = CType(ctrs, RadNumericTextBox).Value
                            Case "rd".ToUpper
                                rPH(ctrs.ID.ToString.ToUpper.Substring(2)) = CType(ctrs, RadDatePicker).SelectedDate
                            Case Else
                                Continue For
                        End Select
                    Catch ex As Exception
                        Continue For
                    End Try
                Next
        End Select

    End Sub

    Private Sub SetDataToGrid(ByVal EditItem As GridEditableItem)
        Dim cbo As New RadComboBox
        Dim arr As New ArrayList()
        Try
            Dim LEAVE_DAY = EditItem.GetDataKeyValue("LEAVE_DAY")
            Dim STATUS_SHIFT As New Object
            Dim MANUAL_ID As New Object
            Dim IS_DEDUCT_SHIFT As New Object
            Dim SHIFT_DAY As New Object
            For Each rows As DataRow In dtDetail.Rows
                If rows("LEAVE_DAY") = LEAVE_DAY Then
                    STATUS_SHIFT = rows("STATUS_SHIFT")
                    MANUAL_ID = rows("MANUAL_ID")
                    IS_DEDUCT_SHIFT = rows("IS_DEDUCT_SHIFT")
                    SHIFT_DAY = rows("SHIFT_DAY")
                    Exit For
                End If
            Next
            cbo = CType(EditItem.FindControl("cbSTATUS_SHIFT"), RadComboBox)
            arr.Add(New DictionaryEntry("", Nothing))
            arr.Add(New DictionaryEntry("Đầu ca", 1))
            arr.Add(New DictionaryEntry("Cuối ca", 2))
            With cbo
                .DataSource = arr
                .DataValueField = "Value"
                .DataTextField = "Key"
                cbo.DataBind()
                .SelectedIndex = 0
            End With
            If IsNumeric(STATUS_SHIFT) Then
                cbo.SelectedValue = STATUS_SHIFT
                cbo.Enabled = If(Not IsNumeric(MANUAL_ID), False, True)
            End If
            If IS_DEDUCT_SHIFT = 0 Then
                cbo.Enabled = False
            Else
                If IsDBNull(SHIFT_DAY) = True Then
                    cbo.Enabled = False
                Else
                    If SHIFT_DAY <= 0.5 Then
                        cbo.Enabled = False
                    Else
                        cbo.Enabled = True
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function SaveDB() As Boolean
        Dim rep As New AttendanceRepository
        Dim PH As DataTable = New DataTable()
        Dim dr As DataRow() = New DataRow() {rPH}
        PH = dr.CopyToDataTable()
        PH.TableName = "PH"
        Dim dsLeaveSheet As New DataSet("DATA")
        Dim CT As New DataTable()
        dsLeaveSheet.Tables.Add(PH)
        CT = dtDetail
        CT.TableName = "CT"
        'dsLeaveSheet.Tables.Remove("CT")
        dsLeaveSheet.Tables.Add(CT.Copy())
        Try
            Return rep.SaveLeaveSheet(dsLeaveSheet)
        Catch ex As Exception
            Return False
        Finally
            rep.Dispose()
            CT.Dispose()
            PH.Dispose()
        End Try
    End Function

    Private Sub GetDataCombo()
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ListComboData Is Nothing Then
                ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO

                'Điều chỉnh Loại nghỉ (thêm điều kiện Loại xử lý Kiểu công: Đăng ký)
                ListComboData.GET_LIST_TYPE_MANUAL_LEAVE = True
                rep.GetComboboxData(ListComboData)
                FillRadCombobox(cbMANUAL_ID, ListComboData.LIST_LIST_TYPE_MANUAL_LEAVE, "NAME_VN", "ID", True)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub GetLeaveSheet_Detail()
        Dim dtSource As New DataTable()
        Try
            Using rep As New AttendanceRepository
                Dim employee_id As Decimal = Decimal.Parse(rtEmployee_id.Text.Trim)
                Dim manualID As Decimal = Decimal.Parse(cbMANUAL_ID.SelectedValue)
                dtSource = rep.GetLeaveSheet_Detail_ByDate(employee_id, rdLEAVE_FROM.SelectedDate, rdLEAVE_TO.SelectedDate, manualID)
            End Using
            dtDetail = dtSource
            rgData.Rebind()
            For Each item As GridDataItem In rgData.MasterTableView.Items
                item.Edit = True
            Next
            rgData.MasterTableView.Rebind()
        Catch ex As Exception
            Throw ex
        Finally
            dtSource.Dispose()
        End Try
    End Sub

    ''' <summary>
    ''' Fill data len control theo ID
    ''' </summary>
    ''' <param name="empid">Ma nhan vien</param>
    ''' <remarks></remarks>
    Private Sub FillData(ByVal empid As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dtData As New DataTable()
        Dim dateValue = Date.Now
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Using rep As New Profile.ProfileBusinessRepository
                Dim obj = rep.GetEmployeCurrentByID(New WorkingDTO With {.EMPLOYEE_ID = empid})
                If IsNumeric(obj.ID) Then
                    rtEmployee_id.Text = obj.EMPLOYEE_ID.ToString()
                End If
                rtEmployee_Name.Text = obj.EMPLOYEE_NAME
                rtEmployee_Code.Text = obj.EMPLOYEE_CODE
                rtOrg_Name.Text = obj.ORG_NAME
                If IsNumeric(obj.ORG_ID) Then
                    rtOrg_id.Text = obj.ORG_ID.ToString()
                End If
                rtTitle_Name.Text = obj.TITLE_NAME
                If IsNumeric(obj.TITLE_ID) Then
                    rtTitle_Id.Text = obj.TITLE_ID.ToString()
                End If

            End Using

        Catch ex As Exception
        End Try
    End Sub

    Private Sub SetData_Controls(ByVal atLeave As AT_LEAVESHEETDTO, ByVal id_state As Decimal)
        Try

            'check so ngay dang ky nghi
            If Not IsNumeric(rnDAY_NUM.Value) OrElse rnDAY_NUM.Value < 1 Then
                ShowMessage(Translate("Số ngày đăng ký nghỉ phải lơn hơn 0"), NotifyType.Warning)
                Exit Sub
            End If
            rnSTATUS.Text = id_state.ToString
            CreateDataBinDing(0)
            atLeave.LEAVE_FROM = rdLEAVE_FROM.SelectedDate
            atLeave.LEAVE_TO = rdLEAVE_TO.SelectedDate
            atLeave.ID = Utilities.ObjToDecima(rPH("ID"))
            atLeave.EMPLOYEE_ID = CDec(rtEmployee_id.Text)
            If (New AttendanceBusinessClient).ValidateLeaveSheetDetail(atLeave) = False Then
                ShowMessage(Translate("Ngày đăng ký nghỉ đã bị trùng"), NotifyType.Warning)
                Exit Sub
            End If

            If SaveDB() Then
                Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlLeaveRegistration")
            Else
                ShowMessage(Translate("Xảy ra lỗi"), NotifyType.Error)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

End Class
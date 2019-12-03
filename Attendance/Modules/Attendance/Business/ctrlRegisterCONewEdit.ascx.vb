Imports Framework.UI
Imports Common
Imports Attendance.ctrlNewInOut
Imports Telerik.Web.UI
Imports Framework.UI.Utilities
Imports Attendance.AttendanceRepository
Imports Profile.ProfileBusiness
Imports Attendance.AttendanceBusiness
Imports Common.CommonBusiness
Imports WebAppLog
Imports Telerik.Web.UI.Calendar

Public Class ctrlRegisterCONewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String

    Dim _myLog As New MyLog()
    Dim pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Attendance/Module/Attendance/Business/" + Me.GetType().Name.ToString()

#Region "Property"
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    Property isFlag As Boolean
        Get
            Return ViewState(Me.ID & "_isFlag")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isFlag") = value
        End Set
    End Property
    Property isEdit As Boolean
        Get
            Return ViewState(Me.ID & "_isEdit")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isEdit") = value
        End Set
    End Property

    Property Employee_id As Integer
        Get
            Return ViewState(Me.ID & "_Employee_id")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_Employee_id") = value
        End Set
    End Property

    Property _Value As Decimal?
        Get
            Return ViewState(Me.ID & "_Value")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_Value") = value
        End Set
    End Property

    Property RegisterLeave As AT_LEAVESHEETDTO
        Get
            Return ViewState(Me.ID & "_AT_LEAVESHEETDTO")
        End Get
        Set(ByVal value As AT_LEAVESHEETDTO)
            ViewState(Me.ID & "_AT_LEAVESHEETDTO") = value
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

    Public ReadOnly Property CurrentUser As UserDTO
        Get
            Return LogHelper.CurrentUser
        End Get
    End Property

    Property totalDayResOld As Integer
        Get
            Return ViewState(Me.ID & "_totalDayResOld")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_totalDayResOld") = value
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

    ''' <lastupdate>
    ''' 15/08/2017 10:00
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
            CurrentState = CommonMessage.STATE_EDIT
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Xét các trạng thái của grid rgWorkschedule
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Bind lai du lieu cho grid rgWorkschedule
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dsLeaveSheet As New DataSet()
        Try
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
                If dsLeaveSheet.Tables(1) IsNot Nothing Then
                    dtDetail = dsLeaveSheet.Tables(1).Clone()
                    dtDetail = dsLeaveSheet.Tables(1)
                End If
            End If
            Select Case Message
                Case "TRUE"
                    CreateDataBinDing(1)
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        Finally
            dsLeaveSheet.Dispose()
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức BindData
    ''' Gọi phương thức load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()
            Refresh()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham khoi taso toolbar voi cac button them moi, sua, xuat file, xoa, in hop dong
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.MainToolBar = tbarOT
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                       ToolbarItem.Cancel)

            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cho rgRegisterLeave
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter(False)
            'GetLeaveSheet_Detail()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Event"

    'Protected Sub ckNON_LEAVE_OnCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
    '    Try
    '        Dim edit = CType(sender, CheckBox)
    '        If edit.Enabled = False Then
    '            Exit Sub
    '        End If
    '        Dim item = CType(edit.NamingContainer, GridEditableItem)
    '        Dim LEAVE_DAY = item.GetDataKeyValue("LEAVE_DAY")
    '        For Each rows In dtDetail.Rows
    '            If rows("LEAVE_DAY") = LEAVE_DAY Then

    '            End If
    '        Next
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

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

            ScriptManager.RegisterStartupScript(Me.Page, Page.GetType(), "text", "IsBlock()", True)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rdFROM_LEAVE_SelectedDateChanged(ByVal sender As Object, ByVal e As SelectedDateChangedEventArgs) Handles rdLEAVE_FROM.SelectedDateChanged, rdLEAVE_TO.SelectedDateChanged
        If (Not IsDate(rdLEAVE_FROM.SelectedDate) OrElse Not IsDate(rdLEAVE_TO.SelectedDate) OrElse dtDetail Is Nothing OrElse Not IsNumeric(rtEmployee_id.Text)) Then Exit Sub
        Try
            GetLeaveSheet_Detail()
            Cal_DayLeaveSheet()
            Cal_DayEntitlement()

            Dim store As New AttendanceStoreProcedure
            Dim dtSourceNB = store.GET_INFO_NGHIBU(Decimal.Parse(rtEmployee_id.Text.Trim), rdLEAVE_FROM.SelectedDate)
            If dtSourceNB.Rows.Count > 0 Then
                txtCUR_HAVE.Text = If(dtSourceNB.Rows(0)("CUR_HAVE") Is Nothing, 0, CDec(dtSourceNB.Rows(0)("CUR_HAVE").ToString()))
            Else
                txtCUR_HAVE.Text = 0
            End If
        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Sub

    Private Sub cbMANUAL_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs) Handles cbMANUAL_ID.SelectedIndexChanged
        Try
            GetLeaveSheet_Detail()
            Cal_DayLeaveSheet()
            Cal_DayEntitlement()
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Event click button search ma nhan vien
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFindCommon_Click(ByVal sender As Object,
                                    ByVal e As EventArgs) Handles _
                                btnFindEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case sender.ID
                Case btnFindEmployee.ID
                    isLoadPopup = 1
            End Select
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click nut cancel cua popup ctrlFindPopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 0
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command khi click item tren toolbar
    ''' Command xuat luu, huy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim objValidate As New AT_LEAVESHEETDTO
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        'check so ngay dang ky nghi
                        If Not IsNumeric(rnDAY_NUM.Value) OrElse rnDAY_NUM.Value <= 0 Then
                            ShowMessage(Translate("Số ngày đăng ký nghỉ phải lơn hơn 0"), NotifyType.Warning)
                            Exit Sub
                        End If

                        If (isFlag = True) Then
                            Dim intBalance As Double = If(rnBALANCE.Text.Trim = "", 0, Double.Parse(rnBALANCE.Text.Trim))
                            If (rnDAY_NUM.Value > intBalance) Then
                                ShowMessage(Translate("Đã vượt quá số phép qui định, vui lòng điều chỉnh lại dữ liệu"), NotifyType.Warning)
                                Exit Sub
                            End If
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

                        If SaveDB() Then
                            Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlRegisterCO&group=Business")
                        Else
                            ShowMessage(Translate("Xảy ra lỗi"), NotifyType.Error)
                        End If
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule')")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Attendance&fid=ctrlRegisterCO&group=Business")
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New AttendanceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim empID = ctrlFindEmployeePopup.SelectedEmployeeID(0)
            FillData(empID)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub rgData_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles rgData.ItemDataBound
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
#End Region

#Region "Custom"
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
    Private Sub Cal_DayEntitlement()
        Try
            If rtEmployee_id.Text = "" Or rdLEAVE_FROM.SelectedDate Is Nothing Then
                Exit Sub
            End If

            Dim dtSourceEntitlement As New DataTable()
            Dim dtManual As New DataTable()
            Dim employee_id As Decimal = Decimal.Parse(rtEmployee_id.Text.Trim)

            Try
                Using rep As New AttendanceRepository
                    Dim manualID As Decimal = Decimal.Parse(cbMANUAL_ID.SelectedValue)
                    dtManual = rep.GET_MANUAL_BY_ID(manualID)
                End Using

                If dtManual.Rows.Count > 0 Then
                    Dim strCode As String = dtManual.Rows(0)("CODE").ToString()
                    Dim strMorning As String = If(dtManual.Rows(0)("MORNING_ID") IsNot Nothing, dtManual.Rows(0)("MORNING_ID").ToString(), Nothing)
                    Dim strAfternoon As String = If(dtManual.Rows(0)("AFTERNOON_ID") IsNot Nothing, dtManual.Rows(0)("AFTERNOON_ID").ToString(), Nothing)

                    If (strCode.ToUpper() = "P" And (strMorning = "251" Or strAfternoon = "251")) Then
                        Using rep As New AttendanceRepository
                            dtSourceEntitlement = rep.GET_INFO_PHEPNAM(employee_id, rdLEAVE_FROM.SelectedDate)
                            If dtSourceEntitlement.Rows.Count > 0 Then
                                isFlag = True
                                rnCUR_HAVE.Text = If(dtSourceEntitlement.Rows(0)("PHEP_CHE_DO") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CHE_DO").ToString())
                                rnSENIORITYHAVE.Text = If(dtSourceEntitlement.Rows(0)("PHEP_THAM_NIEN") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_THAM_NIEN").ToString())
                                rnPREV_HAVE.Text = If(dtSourceEntitlement.Rows(0)("PHEP_CU_CHUYEN_QUA") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CU_CHUYEN_QUA").ToString())
                                rnCUR_USED.Text = If(dtSourceEntitlement.Rows(0)("PHEP_CU_DA_NGHI") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CU_DA_NGHI").ToString())
                                rnCUR_HAVE_INMONTH.Text = If(dtSourceEntitlement.Rows(0)("PHEP_THANG_LVIEC") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_THANG_LVIEC").ToString())
                                rnPREVTOTAL_HAVE.Text = If(dtSourceEntitlement.Rows(0)("PHEP_CU_CON_HLUC") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CU_CON_HLUC").ToString())
                                rtCUR_USED_INMONTH.Text = If(dtSourceEntitlement.Rows(0)("PHEP_DA_NGHI") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_DA_NGHI").ToString())
                                rnCUR_DANGKY.Text = If(dtSourceEntitlement.Rows(0)("PHEP_DA_DANGKY") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_DA_DANGKY").ToString())
                                rnBALANCE.Text = If(dtSourceEntitlement.Rows(0)("PHEP_CONLAI") Is Nothing, "0", dtSourceEntitlement.Rows(0)("PHEP_CONLAI").ToString())
                            Else
                                Clearn__DayEntitlement()
                            End If
                        End Using
                    Else
                        Clearn__DayEntitlement()
                    End If
                Else
                    Clearn__DayEntitlement()
                End If
            Catch ex As Exception
                Throw ex
            Finally
                dtSourceEntitlement.Dispose()
                dtManual.Dispose()
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Clearn__DayEntitlement()
        isFlag = False
        rnCUR_HAVE.Text = ""
        rnSENIORITYHAVE.Text = ""
        rnPREV_HAVE.Text = ""
        rnCUR_USED.Text = ""
        rnCUR_HAVE_INMONTH.Text = ""
        rnPREVTOTAL_HAVE.Text = ""
        rtCUR_USED_INMONTH.Text = ""
        rnCUR_DANGKY.Text = ""
        rnBALANCE.Text = ""
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức update trạng thái của các control theo state
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case isLoadPopup
                Case 1
                    If Not phFindEmp.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        phFindEmp.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.LoadAllOrganization = False
                        ctrlFindEmployeePopup.MustHaveContract = False
                    End If
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien load data cho cac combobox
    ''' </summary>
    ''' <remarks></remarks>
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

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 15/08/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức lấy data cho rgRegisterLeave
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New AttendanceRepository
        Dim obj As New AT_LEAVESHEETDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgData.VirtualItemCount = dtDetail.Rows.Count
            rgData.DataSource = dtDetail
            'rgData.Rebind()
            'For Each item As GridDataItem In rgData.MasterTableView.Items
            '    item.Edit = True
            'Next
            'rgData.MasterTableView.Rebind()
            Cal_DayLeaveSheet()
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
        Return New DataTable()
    End Function

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
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub SetDataToGrid(ByVal EditItem As GridEditableItem)
        Dim cbo As New RadComboBox
        Dim arr As New ArrayList()
        Try
            Dim LEAVE_DAY = EditItem.GetDataKeyValue("LEAVE_DAY")
            Dim STATUS_SHIFT
            Dim MANUAL_ID
            Dim IS_DEDUCT_SHIFT
            Dim SHIFT_DAY
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
                    If SHIFT_DAY <= 0.5 Or SHIFT_DAY > 1 Then
                        cbo.Enabled = False
                    Else
                        cbo.Enabled = True
                    End If
                End If
            End If
        Catch ex As Exception
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
#End Region
End Class


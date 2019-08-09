Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports Common.CommonBusiness
Public Class ctrlDashboardHome
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False
    Dim log As New UserLog
    Dim psp As New ProfileStoreProcedure
    Dim com As New CommonProcedureNew
    Dim cons As New Contant_OtherList_Iprofile
    Dim cons_com As New Contant_Common
#Region "Property"
    Public Property dtReminder As DataTable
        Get
            Return ViewState(Me.ID & "_dtReminder")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtReminder") = value
        End Set
    End Property


    Public Property InfoList As List(Of StatisticDTO)
        Get
            Return ViewState(Me.ID & "_InfoList")
        End Get
        Set(ByVal value As List(Of StatisticDTO))
            ViewState(Me.ID & "_InfoList") = value
        End Set
    End Property

    Public Property RemindList As List(Of ReminderLogDTO)
        Get
            Return PageViewState(Me.ID & "_RemindContractList")
        End Get
        Set(ByVal value As List(Of ReminderLogDTO))
            PageViewState(Me.ID & "_RemindContractList") = value
        End Set
    End Property

    Public Property _filter As ReminderLogDTO
        Get
            If PageViewState(Me.ID & "_filter") Is Nothing Then
                PageViewState(Me.ID & "_filter") = New ReminderLogDTO
            End If
            Return PageViewState(Me.ID & "_filter")
        End Get
        Set(ByVal value As ReminderLogDTO)
            PageViewState(Me.ID & "_filter") = value
        End Set
    End Property

    ' Nhac nho ngay sinh sinh
    Dim birthdayRemind As Integer
    ' Nhac nho het han hop dong
    Dim contractRemind As Integer
    ' Nhac nho het han thu viec
    Dim probationRemind As Integer
    ' Nhac nho het han visa
    Dim visaRemind As Integer
    ' Nhac nho het han bo nhiem
    Dim appointRemind As Integer
    ' Nhac nho het han dieu dong bo nhiem
    Dim transferRemind As Integer
    ' Nhac nho het han nghi thai san
    Dim maternityRemind As Integer
    ' Nhac nho  nang luong
    Dim heavysalaryRemind As Integer

#End Region

#Region "Page"
    Private width As Integer
    Private height As Integer

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Dim resize As Integer = 0
            If Request("resize") IsNot Nothing Then
                resize = CInt(Request("resize"))
            End If
            LoadConfig()
            If Not IsPostBack Or resize = 0 Then
                Refresh(CommonMessage.ACTION_UPDATED)
            Else
                Refresh()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileDashboardRepository
        log = LogHelper.GetUserLog
        Dim dt As New DataTable
        Dim dr() As DataRow
        Try
            If InfoList Is Nothing Or Message = CommonMessage.ACTION_UPDATED Then
                'dt = psp.GET_COMPANY_NEW_INFO(log.Username.ToUpper)
            End If
            'If dt.Rows.Count > 0 Then
            '    dr = dt.Select("NAME = '" + cons_com.EMP_COUNT + "'")
            '    lbtnEmpCount.Text = Decimal.Parse(dr(0)("VALUE").ToString)
            '    dr = dt.Select("NAME = '" + cons_com.EMP_NEW + "'")
            '    lbtnEmpNew.Text = Decimal.Parse(dr(0)("VALUE").ToString)
            '    dr = dt.Select("NAME = '" + cons_com.EMP_TER + "'")
            '    lbtnEmpTer.Text = Decimal.Parse(dr(0)("VALUE").ToString)
            '    dr = dt.Select("NAME = '" + cons_com.CONTRACT_NEW + "'")
            '    lbtnContractNew.Text = Decimal.Parse(dr(0)("VALUE").ToString)
            '    dr = dt.Select("NAME = '" + cons_com.AGE_AVG + "'")
            '    lbtnAgeAvg.Text = Decimal.Parse(dr(0)("VALUE").ToString)
            '    dr = dt.Select("NAME = '" + cons_com.SENIORITY_AVG + "'")
            '    lbtnSeniority.Text = Decimal.Parse(dr(0)("VALUE").ToString)
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgContract.SetFilter()
            rgContract.PageSize = 100
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


#End Region

#Region "Event"

    Private Sub rgContract_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgContract.ItemCommand
        Dim sv_sID As String = String.Empty
        Dim sv_ID As String = String.Empty
        If e.CommandName = Telerik.Web.UI.RadGrid.ExportToExcelCommandName Then
            ExportToExcel(rgContract)
            e.Canceled = True
        End If
        If e.CommandName = Telerik.Web.UI.RadGrid.ExportToCsvCommandName Then
            For Each dr As Telerik.Web.UI.GridDataItem In rgContract.SelectedItems
                If dr("REMIND_TYPE").Text.ToString = "011" Then
                    sv_sID &= IIf(sv_sID = vbNullString, dr.GetDataKeyValue("ID_TYPE").ToString, "," & dr.GetDataKeyValue("ID_TYPE").ToString)
                    sv_ID &= IIf(sv_ID = vbNullString, dr.GetDataKeyValue("ID").ToString, "," & dr.GetDataKeyValue("ID").ToString)
                End If
            Next
            If sv_sID = "" Then
                ShowMessage("Vui lòng chọn loại nhắc nhở hết hạn điều động tạm thời, để thực hiện chức năng này", NotifyType.Warning)
                e.Canceled = True
            Else
                If psp.INSERT_WORKING_BY_REMINDER(sv_sID) = 1 Then

                    'psp.DELETE_INFO_REMINDER(sv_ID)
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    rgContract.Rebind()
                    e.Canceled = True
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                End If
            End If
        End If

        If e.CommandName = Telerik.Web.UI.RadGrid.ExportToPdfCommandName Then

            For Each dr As Telerik.Web.UI.GridDataItem In rgContract.SelectedItems
                If dr("REMIND_TYPE").Text.ToString = "015" Then
                    sv_sID &= IIf(sv_sID = vbNullString, dr.GetDataKeyValue("ID_TYPE").ToString, "," & dr.GetDataKeyValue("ID_TYPE").ToString)
                End If
            Next
            If sv_sID = "" Then
                ShowMessage("Vui lòng chọn loại nhắc nhở hết hạn nghỉ thai sản, để thực hiện chức năng này", NotifyType.Warning)
                e.Canceled = True
            Else
                For Each dr As Telerik.Web.UI.GridDataItem In rgContract.SelectedItems
                    'Dim tab As DataTable = com.GetByID("Ins_Maternity_Mng", String.Empty, Int32.Parse(dr.GetDataKeyValue("ID_TYPE").ToString()))
                    'If tab IsNot Nothing And tab.Rows.Count > 0 Then
                    '    For Each row As DataRow In tab.Rows
                    '        If psp.PRI_INS_ARISING_MATERNITY(Int32.Parse(row("ID").ToString()), Int32.Parse(row("EMPLOYEE_ID").ToString()), DateTime.Parse(row("FROM_DATE").ToString()), 9, log.Username) = 1 Then
                    '            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    '        Else
                    '            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    '        End If
                    '    Next
                    'End If
                Next

                rgContract.Rebind()
                e.Canceled = True
            End If
        End If
    End Sub

    Private Sub rgContract_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgContract.ItemDataBound
        Try
            If TypeOf e.Item Is GridDataItem Then
                Dim item = CType(e.Item, GridDataItem)
                Dim link As LinkButton
                link = CType(item("LINK_POPUP").FindControl("lbtnLink"), LinkButton)
                If item.GetDataKeyValue("LINK_POPUP") Is Nothing Then
                    link.Visible = False
                Else
                    link.OnClientClick = item.GetDataKeyValue("LINK_POPUP")
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgContract.NeedDataSource
        log = LogHelper.GetUserLog
        'Dim dr() As DataRow
        Using rep As New ProfileDashboardRepository
            Try
                'dtReminder = psp.GET_INFO_REMINDER(log.Username.ToUpper)
                'dtReminder = psp.GET_LIST_INFO_REMINDER(log.Username.ToUpper)
                'If dtReminder.Rows.Count > 0 Then
                '    'lbbirthdayRemind.Text = dtReminder.Select("REMIND_TYPE = '" + cons_com.REMINDER12 + "'").Count
                '    'lbcontractRemind.Text = dtReminder.Select("REMIND_TYPE = '" + cons_com.REMINDER2 + "'").Count
                '    'lbprobationRemind.Text = dtReminder.Select("REMIND_TYPE = '" + cons_com.REMINDER1 + "'").Count
                '    'lbvisaRemind.Text = dtReminder.Select("REMIND_TYPE = '" + cons_com.REMINDER4 + "'").Count
                '    'lbappointRemind.Text = dtReminder.Select("REMIND_TYPE = '" + cons_com.REMINDER8 + "'").Count
                '    'lbtransferRemind.Text = dtReminder.Select("REMIND_TYPE = '" + cons_com.REMINDER11 + "'").Count
                '    'lbmaternityRemind.Text = dtReminder.Select("REMIND_TYPE = '" + cons_com.REMINDER15 + "'").Count
                '    'lbheavysalaryRemind.Text = dtReminder.Select("REMIND_TYPE = '" + cons_com.REMINDER6 + "'").Count
                '    'lbDenTuoiVeHuu.Text = dtReminder.Select("REMIND_TYPE = '" + cons_com.REMINDER7 + "'").Count

                'End If
                rgContract.DataSource = dtReminder
            Catch ex As Exception
                rep.Dispose()
                DisplayException(Me.ViewName, Me.ID, ex)
            End Try
        End Using
    End Sub

    Protected Sub ibtnReLoad_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ibtnReLoad.Click
        Try
            log = LogHelper.GetUserLog
            'psp.INSERT_INFO_REMINDER_DETAIL(log.Username.ToUpper)
            rgContract.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


#End Region

#Region "Custom"


    Private Sub LoadConfig()
        Try
            'Dim dr() As DataRow
            'Dim dt = psp.GET_LIST_REMINDER()
            'If dt.Rows.Count > 0 Then
            '    dr = dt.Select("CODE = '" + cons_com.REMINDER3 + "'")
            '    birthdayRemind = Decimal.Parse(dr(0)("VALUE").ToString)

            '    dr = dt.Select("CODE = '" + cons_com.REMINDER2 + "'")
            '    contractRemind = Decimal.Parse(dr(0)("VALUE").ToString)

            '    dr = dt.Select("CODE = '" + cons_com.REMINDER1 + "'")
            '    probationRemind = Decimal.Parse(dr(0)("VALUE").ToString)

            '    dr = dt.Select("CODE = '" + cons_com.REMINDER4 + "'")
            '    visaRemind = Decimal.Parse(dr(0)("VALUE").ToString)

            '    dr = dt.Select("CODE = '" + cons_com.REMINDER8 + "'")
            '    appointRemind = Decimal.Parse(dr(0)("VALUE").ToString)

            '    dr = dt.Select("CODE = '" + cons_com.REMINDER11 + "'")
            '    transferRemind = Decimal.Parse(dr(0)("VALUE").ToString)

            '    dr = dt.Select("CODE = '" + cons_com.REMINDER15 + "'")
            '    maternityRemind = Decimal.Parse(dr(0)("VALUE").ToString)

            '    dr = dt.Select("CODE = '" + cons_com.REMINDER6 + "'")
            '    heavysalaryRemind = Decimal.Parse(dr(0)("VALUE").ToString)
            'End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ExportToExcel(ByVal grid As RadGrid)
        ' Dim lstData As List(Of ReminderLogDTO)

        Dim _error As Integer = 0
        Using xls As New ExcelCommon


            If dtReminder.Rows.Count > 0 Then
                Dim bCheck = xls.ExportExcelTemplate(
                Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/" & Request.Params("fid") & ".xls"),
                "DanhSachNhacNho", dtReminder, Response, _error)
                If Not bCheck Then
                    Select Case _error
                        Case 1
                            ShowMessage(Translate("Mẫu báo cáo không tồn tại"), NotifyType.Warning)
                        Case 2
                            ShowMessage(Translate("Dữ liệu không tồn tại"), NotifyType.Warning)
                    End Select
                    Exit Sub
                End If
            Else
                ShowMessage(Translate("Dữ liệu không tồn tại"), NotifyType.Warning)
            End If

        End Using
    End Sub

#End Region


End Class
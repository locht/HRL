﻿Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Telerik.Web.UI
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports Insurance.InsuranceBusiness
Imports Common.CommonBusiness

Public Class ctrlInsMaternityDetail
    Inherits Common.CommonView
    Private store_list As New Store_Insurance_List()
    Private store_business As New Store_Insurance_Business()
    Private userlog As UserLog
    'Private WithEvents viewRegister As ctrlShiftPlanningRegister
    Public Overrides Property MustAuthorize As Boolean = False

    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlOrgSULPPopup As ctrlFindOrgPopup

#Region "Property & Variable"

    Public Property popup As RadWindow
    Public Property popupId As String
    Public Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    Private Property LastStartDate As Nullable(Of Date)
        Get
            Return PageViewState("LastStartDate")
        End Get
        Set(ByVal value As Nullable(Of Date))
            PageViewState("LastStartDate") = value
        End Set
    End Property

    Private Property LastEndDate As Nullable(Of Date)
        Get
            Return PageViewState("LastEndDate")
        End Get
        Set(ByVal value As Nullable(Of Date))
            PageViewState("LastEndDate") = value
        End Set
    End Property

    Private Property Status As Decimal
        Get
            Return ViewState(Me.ID & "_Status")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Status") = value
        End Set
    End Property

    Public ReadOnly Property PageId As String
        Get
            Return Me.ID
        End Get
    End Property

    'Private Property ListPeriod As List(Of PERIOD_DTO)
    '    Get
    '        Return ViewState(Me.ID & "_ListPeriod")
    '    End Get
    '    Set(ByVal value As List(Of PERIOD_DTO))
    '        ViewState(Me.ID & "_ListPeriod") = value
    '    End Set
    'End Property

    Public Property InsCompList As DataTable
        Get
            Return ViewState(Me.ID & "_InsCompList")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_InsCompList") = value
        End Set
    End Property

    Public Property SelectedItemList As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_SelectedItemList")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_SelectedItemList") = value
        End Set
    End Property

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

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            Me.MainToolBar = rtbMain
            Common.Common.BuildToolbar(Me.MainToolBar, _
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).CausesValidation = True

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ShowPopupEmployee()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            IDSelect = 0 'Khoi tao cho IdSelect = 0
            'GetDataCombo()
            GetParams() 'ThanhNT added 02/03/2016            

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub UpdateControlState(ByVal sState As String)
        Try
            Select Case sState
                Case CommonMessage.STATE_NORMAL
                    btnSearchEmp.Enabled = False
                    SetEnabled(False)
                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    btnSearchEmp.Enabled = True
                    SetEnabled(True)
                Case CommonMessage.STATE_DELETE

                Case "Nothing"
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        'Dim gID As Decimal
        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Call ResetForm()
                Case CommonMessage.TOOLBARITEM_SAVE
                    Call SaveData()

                    UpdateControlState(CommonMessage.STATE_NORMAL)
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Function & Sub"

    Protected Sub SetEnabled(ByVal val As Boolean)
        Try
            btnSearchEmp.Enabled = val
            dateNgayDuSinh.Enabled = val
            cbNghiThaiSan1.Enabled = val
            cbNormal_Birth1.Enabled = val
            cbNotNormal_Birth1.Enabled = val
            dateFrom.Enabled = val
            dateTo.Enabled = val
            dateNgaySinh.Enabled = val
            'dateNgayDiLamSom.Enabled = val
            txtSoCon.Enabled = val
            txtTamUng.Enabled = val
            txtINS_PAY_UNIT.Enabled = val
            txtDiffMoney1.Enabled = val
            txtRemark.Enabled = val
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ResetForm()
        Try
            txtID.Text = "0"
            txtEMPLOYEE_ID.Text = ""
            txtFULLNAME.Text = ""
            txtDEP.Text = ""
            txtEMPID.Text = "0"
            'ThanhNT added 05/01/2016
            txtPOSITION.Text = ""
            txtINS_PAY_UNIT.Text = ""
            dateNgayDuSinh.SelectedDate = Nothing
            cbNghiThaiSan1.Checked = True
            cbNormal_Birth1.Checked = True
            cbNotNormal_Birth1.Checked = False
            dateFrom.SelectedDate = Nothing
            dateTo.SelectedDate = Nothing
            'dataFromEnjoy.SelectedDate = Nothing
            'dataToEnjoy.SelectedDate = Nothing
            dateNgaySinh.SelectedDate = Nothing
            'dateNgayDiLamSom.SelectedDate = Nothing
            txtSoCon.Text = 1
            txtTamUng.Text = 0
            txtInsPay.Text = 0
            txtDiffMoney1.Text = 0
            txtRemark.Text = String.Empty
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SaveData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            userlog = New UserLog
            userlog = LogHelper.GetUserLog

            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    'dataFromEnjoy.SelectedDate, dataToEnjoy.SelectedDate,dateNgayDiLamSom.SelectedDate
                    Dim id As Int32 = store_business.SAVE_INS_MATERNITY_MNG(0,
                                                                            InsCommon.getNumber(txtEMPID.Text),
                                                                            dateNgayDuSinh.SelectedDate,
                                                                            InsCommon.getNumber(IIf(cbNghiThaiSan1.Checked, 1, 0)),
                                                                            InsCommon.getNumber(IIf(cbNormal_Birth1.Checked, 1, 0)),
                                                                            InsCommon.getNumber(IIf(cbNotNormal_Birth1.Checked, 1, 0)),
                                                                            dateFrom.SelectedDate,
                                                                            dateTo.SelectedDate,
                                                                            Nothing, Nothing,
                                                                            dateNgaySinh.SelectedDate,
                                                                            InsCommon.getNumber(txtSoCon.Text),
                                                                            InsCommon.getNumber(txtTamUng.Text),
                                                                            InsCommon.getNumber(txtInsPay.Text),
                                                                            InsCommon.getNumber(txtDiffMoney1.Text),
                                                                            Nothing,
                                                                            txtRemark.Text,
                                                                            userlog.Username,
                                                                            String.Format("{0}-{1}", userlog.ComputerName, userlog.Ip))

                    'P_INS_ARISING_TYPE = 3: Cập nhật biến động BH giảm do nghỉ thai sản
                    If cbNghiThaiSan1.Checked = True Then
                        store_business.PRI_INS_ARISING_MATERNITY(id, InsCommon.getNumber(txtEMPID.Text), dateFrom.SelectedDate, 3, userlog.Username)
                        'Dim ed = DateAdd("d", 1, dateTo.SelectedDate)
                        'store_business.INSERT_INC_VOLATILITY(0,
                        '                                     InsCommon.getNumber(txtEMPID.Text),
                        '                                     ed,
                        '                                     userlog.Username)
                    End If

                    'If dateNgayDiLamSom.SelectedDate IsNot Nothing Then
                    'Dim result = store_business.DELETE_INS_THAISAN(txtEMPLOYEE_ID.Text, dateNgayDiLamSom.SelectedDate, dateTo.SelectedDate)
                    'End If

                    If id > 0 Then
                        Refresh("InsertView")
                        'Common.Common.OrganizationLocationDataSession = Nothing
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
                    'Common.Common.OrganizationLocationDataSession = Nothing
                Case CommonMessage.STATE_EDIT
                    'dataFromEnjoy.SelectedDate, dataToEnjoy.SelectedDate dateNgayDiLamSom.SelectedDate
                    If store_business.SAVE_INS_MATERNITY_MNG(InsCommon.getNumber(txtID.Text),
                                                             InsCommon.getNumber(txtEMPID.Text),
                                                             dateNgayDuSinh.SelectedDate,
                                                             InsCommon.getNumber(IIf(cbNghiThaiSan1.Checked, 1, 0)),
                                                             InsCommon.getNumber(IIf(cbNormal_Birth1.Checked, 1, 0)),
                                                             InsCommon.getNumber(IIf(cbNotNormal_Birth1.Checked, 1, 0)),
                                                             dateFrom.SelectedDate, dateTo.SelectedDate,
                                                             Nothing, Nothing, dateNgaySinh.SelectedDate,
                                                             InsCommon.getNumber(txtSoCon.Text),
                                                             InsCommon.getNumber(txtTamUng.Text),
                                                             InsCommon.getNumber(txtInsPay.Text),
                                                             InsCommon.getNumber(txtDiffMoney1.Text),
                                                             Nothing,
                                                             txtRemark.Text, userlog.Username,
                                                             String.Format("{0}-{1}", userlog.ComputerName, userlog.Ip)) Then

                        'P_INS_ARISING_TYPE = 3: Cập nhật biến động BH giảm do nghỉ thai sản
                        If cbNghiThaiSan1.Checked = True Then
                            store_business.PRI_INS_ARISING_MATERNITY(InsCommon.getNumber(txtID.Text), InsCommon.getNumber(txtEMPID.Text), dateFrom.SelectedDate, 3, userlog.Username)
                        End If

                        'If dateNgayDiLamSom.SelectedDate IsNot Nothing Then
                        'Dim result = store_business.DELETE_INS_THAISAN(txtEMPLOYEE_ID.Text, dateNgayDiLamSom.SelectedDate, dateTo.SelectedDate)
                        'End If
                        Refresh("UpdateView")
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
            End Select
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub GetDataCombo()
    '    Try

    '        'Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsListArisingType(Common.Common.GetUserName(), 0, "0", "", 0, 1)
    '        'FillRadCombobox(ddlINS_ARISING_TYPE_ID, lstSource, "NAME", "ID", False)

    '        'lstSource = (New InsuranceBusiness.InsuranceBusinessClient).GetInsListInsurance(Common.Common.GetUserName(), 0, "", "", "", "", "", 1)
    '        'FillRadCombobox(ddlINS_ORG_ID, lstSource, "NAME", "ID", False)

    '        'Đơn vị BH
    '        ddlINS_ORG_ID.DataSource = New CommonProcedureNew().GET_COMBOBOX(Common.Contant_OtherList_Code_Insurance.table_INS_LIST_INSURANCE, "ID", "NAME", String.Empty, "NAME", False)
    '        ddlINS_ORG_ID.DataValueField = "ID"
    '        ddlINS_ORG_ID.DataTextField = "NAME"
    '        ddlINS_ORG_ID.DataBind()

    '        'Loại biến động
    '        ddlINS_ARISING_TYPE_ID.DataSource = New CommonProcedureNew().GET_COMBOBOX(Common.Contant_OtherList_Code_Insurance.table_INS_LIST_ARISING_TYPE, "ID", "NAME", String.Empty, "NAME", False)
    '        ddlINS_ARISING_TYPE_ID.DataValueField = "ID"
    '        ddlINS_ARISING_TYPE_ID.DataTextField = "NAME"
    '        ddlINS_ARISING_TYPE_ID.DataBind()

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub FillData(ByVal idSelected As Decimal)
        Try
            'Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsArisingManual(Common.Common.GetUserName(), InsCommon.getNumber(txtID.Text) _
            Dim lstSource As DataTable = store_business.GET_INS_MATERNITY_MNG_BYID(InsCommon.getNumber(txtID.Text))
            If lstSource IsNot Nothing AndAlso lstSource.Rows.Count > 0 Then

                InsCommon.SetString(txtEMPID, lstSource.Rows(0)("EMPLOYEE_ID"))

                InsCommon.SetString(txtEMPLOYEE_ID, lstSource.Rows(0)("EMPLOYEE_CODE"))
                InsCommon.SetString(txtFULLNAME, lstSource.Rows(0)("FULL_NAME"))
                InsCommon.SetString(txtDEP, lstSource.Rows(0)("DEP_NAME"))
                InsCommon.SetString(txtPOSITION, lstSource.Rows(0)("POSITION_NAME"))
                InsCommon.SetString(txtINS_PAY_UNIT, lstSource.Rows(0)("INS_PAY_UNIT"))
                InsCommon.SetDate(dateNgayDuSinh, lstSource.Rows(0)("NGAY_DU_SINH"))
                'InsCommon.SetNumber(cbNghiThaiSan1, lstSource.Rows(0)("NGHI_THAI_SAN"))
                'InsCommon.SetNumber(cbNormal_Birth1, lstSource.Rows(0)("IS_NORMAL_BIRTH"))
                Dim q = lstSource.Rows(0)("IS_NORMAL_BIRTH").ToString
                ''If lstSource.Rows(0)("NGAY_DU_SINH"))

                If lstSource.Rows(0)("NGHI_THAI_SAN") Is Nothing OrElse CDec(Val(lstSource.Rows(0)("NGHI_THAI_SAN"))) = 0 Then
                    cbNghiThaiSan1.Checked = False
                Else
                    cbNghiThaiSan1.Checked = True
                End If

                If lstSource.Rows(0)("IS_NORMAL_BIRTH").ToString = "" OrElse CDec(Val(lstSource.Rows(0)("IS_NORMAL_BIRTH"))) = 0 Then
                    cbNormal_Birth1.Checked = False
                Else
                    cbNormal_Birth1.Checked = True
                End If
                If lstSource.Rows(0)("IS_NOT_NORMAL_BIRTH").ToString = "" OrElse CDec(Val(lstSource.Rows(0)("IS_NOT_NORMAL_BIRTH"))) = 0 Then
                    cbNotNormal_Birth1.Checked = False
                Else
                    cbNotNormal_Birth1.Checked = True
                End If
                'InsCommon.SetNumber(cbNotNormal_Birth1, lstSource.Rows(0)("IS_NOT_NORMAL_BIRTH"))
                InsCommon.SetDate(dateFrom, lstSource.Rows(0)("FROM_DATE"))
                InsCommon.SetDate(dateTo, lstSource.Rows(0)("TO_DATE"))
                'InsCommon.SetDate(dataFromEnjoy, lstSource.Rows(0)("FROM_DATE_ENJOY"))
                'InsCommon.SetDate(dataToEnjoy, lstSource.Rows(0)("TO_DATE_ENJOY"))
                InsCommon.SetDate(dateNgaySinh, lstSource.Rows(0)("NGAY_SINH"))
                'InsCommon.SetDate(dateNgayDiLamSom, lstSource.Rows(0)("NGAY_DI_LAM_SOM"))
                InsCommon.SetNumber(txtSoCon, lstSource.Rows(0)("SO_CON"))
                InsCommon.SetNumber(txtTamUng, lstSource.Rows(0)("TIEN_TAM_UNG"))
                InsCommon.SetNumber(txtInsPay, lstSource.Rows(0)("INSURANCE_FEE_PAY"))
                InsCommon.SetString(txtDiffMoney1, lstSource.Rows(0)("DIFFERENCE_FEE"))
                InsCommon.SetString(txtRemark, lstSource.Rows(0)("REMARK"))
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub GetParams()
        Try
            If CurrentState Is Nothing Then

                If Request.Params("Status") IsNot Nothing Then
                    Status = Decimal.Parse(Request.Params("Status"))
                    If Status = 1 Then 'Edit -> FillData
                        If Request.Params("IDSelect") IsNot Nothing Then
                            txtID.Text = Decimal.Parse(Request.Params("IDSelect"))
                            txtEMPID.Text = Decimal.Parse(Request.Params("EmployeeID"))
                            CurrentState = CommonMessage.STATE_EDIT
                            FillData(txtID.Text)
                        End If
                    Else 'New
                        txtID.Text = "0"
                        txtEMPID.Text = "0"
                        CurrentState = CommonMessage.STATE_NEW
                        ResetForm()
                    End If
                End If
                UpdateControlState(CurrentState)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region
#Region "FindEmployeeButton"

    Private Sub ctrlFindEmployeePopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0

        Dim script As String = "resize(1024,400);"
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Resize", script, True)

    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of Common.CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New AttendanceRepository
        Try
            Dim a As Object = ctrlFindEmployeePopup.SelectedEmployee
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of Common.CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                'Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetEmpInfo(item.EMPLOYEE_ID, 0)
                'Dim a1 = Convert.ToInt32(dateFrom.SelectedDate.ToString("yyyy"))
                'Dim a2 = Convert.ToInt32(dateFrom.SelectedDate.ToString("mm"))

                Dim lstSource As DataTable = store_list.GET_INS_EMPINFO(item.EMPLOYEE_ID, String.Empty)
                'Dim lstSource1 As DataTable = store_list.GET_TIEN_TAM_UNG(item.EMPLOYEE_ID, a1, a2)
                'If (Not (lstSource1 Is Nothing) AndAlso lstSource1.Rows.Count > 0) Then
                '    Dim count = 0
                '    Dim sum As New Decimal
                '    For Each item1 In lstSource1.Rows
                '        sum = sum + item1("new_sal")
                '        count = count + 1
                '    Next
                '    txtTamUng.Text = sum / count
                'End If
                If (Not (lstSource Is Nothing) AndAlso lstSource.Rows.Count > 0) Then
                    txtEMPLOYEE_ID.Text = lstSource.Rows(0)("EMPLOYEE_ID")
                    hdORG_ID.Value = lstSource.Rows(0)("ORG_ID").ToString()
                    hdTITLE_ID.Value = lstSource.Rows(0)("TITLE_ID").ToString()
                    InsCommon.SetString(txtFULLNAME, lstSource.Rows(0)("VN_FULLNAME"))
                    InsCommon.SetString(txtDEP, lstSource.Rows(0)("ORG_NAME"))
                    InsCommon.SetString(txtPOSITION, lstSource.Rows(0)("POSITION_NAME"))
                    InsCommon.SetString(txtINS_PAY_UNIT, lstSource.Rows(0)("INS_PAY_UNIT"))
                    txtEMPID.Text = lstSource.Rows(0)("EMPID")
                Else
                    ResetForm()
                    ShowMessage(Translate("Nhân viên chưa có thông tin bảo hiểm. Vui lòng kiểm tra lại"), Utilities.NotifyType.Warning)
                    Exit Sub
                End If

            End If
            isLoadPopup = 0

            Dim script As String = "resize(1024,400);"
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Resize", script, True)

        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnSearchEmp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchEmp.Click
        Try
            isLoadPopup = 1

            ShowPopupEmployee()

            Dim script As String = "resize(1024,600);"
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Resize", script, True)

            ctrlFindEmployeePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ShowPopupEmployee()
        Try
            If FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                FindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
            End If

            If isLoadPopup = 1 Then
                ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                'ctrlFindEmployeePopup.MustHaveTerminate = True
                ctrlFindEmployeePopup.IsHideTerminate = False
                ctrlFindEmployeePopup.MultiSelect = False
                ctrlFindEmployeePopup.MustHaveContract = False
                ctrlFindEmployeePopup.LoadAllOrganization = False
                FindEmployee.Controls.Add(ctrlFindEmployeePopup)

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region


    Protected Sub RadAjaxPanel1_AjaxRequest(ByVal sender As Object, ByVal e As AjaxRequestEventArgs)

        If CurrentState <> CommonMessage.STATE_NORMAL Then
            Dim str As String
            Dim arr() As String
            arr = e.Argument.Split("_")
            str = arr(arr.Count - 1)

            Select Case str
                Case "dateNgaySinh"
                    If dateNgaySinh.SelectedDate IsNot Nothing Then
                        'dataFromEnjoy.SelectedDate = dateNgaySinh.SelectedDate
                        'dataToEnjoy.SelectedDate = dateNgaySinh.SelectedDate.Value.AddMonths(12)
                    Else
                        'dataFromEnjoy.SelectedDate = Nothing
                        'dataToEnjoy.SelectedDate = Nothing
                    End If
                Case "dateFrom"
                    If dateFrom.SelectedDate IsNot Nothing Then
                        dateTo.SelectedDate = dateFrom.SelectedDate.Value.AddMonths(6)
                    Else
                        dateTo.SelectedDate = Nothing
                    End If
                Case "dataFromEnjoy"
                    'If dataFromEnjoy.SelectedDate IsNot Nothing Then
                    '    dataToEnjoy.SelectedDate = dataFromEnjoy.SelectedDate.Value.AddMonths(12)
                    'Else
                    '    dataToEnjoy.SelectedDate = Nothing
                    'End If
            End Select
        End If

    End Sub

    Private Sub dateFrom_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs) Handles dateFrom.SelectedDateChanged
        'Dim a = dateFrom.SelectedDate

        If dateFrom.SelectedDate IsNot Nothing Then
            dateTo.SelectedDate = DateAdd("m", 6, dateFrom.SelectedDate)
        End If
        Dim a0 As Date
        a0 = dateFrom.SelectedDate
        Dim a1 = a0.Year
        Dim a2 = a0.Month
        Dim empid = CDec(Val(txtEMPID.Text))
        Dim lstSource1 As DataTable = store_list.GET_TIEN_TAM_UNG(empid, a1, a2)
        If (Not (lstSource1 Is Nothing) AndAlso lstSource1.Rows.Count > 0) Then
            Dim count = 0
            Dim sum As New Decimal
            For Each item1 In lstSource1.Rows
                sum = sum + item1("new_sal")
                count = count + 1
            Next
            txtTamUng.Text = sum / count
        End If
    End Sub

    Private Sub cbNormal_Birth1_CheckedChanged(sender As Object, e As EventArgs) Handles cbNormal_Birth1.CheckedChanged
        If cbNormal_Birth1.Checked = True Then
            cbNotNormal_Birth1.Checked = False
        End If
    End Sub

    Private Sub cbNotNormal_Birth1_CheckedChanged(sender As Object, e As EventArgs) Handles cbNotNormal_Birth1.CheckedChanged
        If cbNotNormal_Birth1.Checked = True Then
            cbNormal_Birth1.Checked = False
        End If
    End Sub

    Private Sub txtInsPay_TextChanged(sender As Object, e As EventArgs) Handles txtInsPay.TextChanged
        If txtTamUng.Text <> 0 Then
            txtDiffMoney1.Text = CDec(Val(txtTamUng.Text)) - CDec(Val(txtInsPay.Text))
        Else
            txtDiffMoney1.Text = ""
        End If

    End Sub

    Private Sub txtTamUng_TextChanged(sender As Object, e As EventArgs) Handles txtTamUng.TextChanged
        If txtTamUng.Text <> 0 And txtInsPay.Text <> 0 Then
            txtDiffMoney1.Text = CDec(Val(txtTamUng.Text)) - CDec(Val(txtInsPay.Text))
        Else
            txtDiffMoney1.Text = ""
        End If
    End Sub
End Class
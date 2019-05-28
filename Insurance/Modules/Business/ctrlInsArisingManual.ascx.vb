Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Telerik.Web.UI
Imports Insurance.InsuranceBusiness

Public Class ctrlInsArisingManual
    Inherits Common.CommonView
    'Private WithEvents viewRegister As ctrlShiftPlanningRegister

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

    Private Property Arising_Type As Decimal
        Get
            Return ViewState(Me.ID & "_Arising_Type")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Arising_Type") = value
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
            If Not IsPostBack Then
                ViewConfig(RadPane1)
                'GirdConfig(rgGrid)
            End If
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
            GetDataCombo()
            GetParams() 'ThanhNT added 02/03/2016            

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub UpdateControlState(ByVal sState As String)
        Try
            Select Case sState
                Case CommonMessage.STATE_NORMAL
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                    btnSearchEmp.Enabled = False

                    ddlINS_ORG_ID.Enabled = False
                    ddlINS_ARISING_TYPE_ID.Enabled = False
                    txtSALARY_PRE_PERIOD.Enabled = False
                    txtSALARY_NOW_PERIOD.Enabled = False
                    txtEFFECTIVE_DATE.Enabled = False
                    txtEXPRIE_DATE.Enabled = False
                    txtDECLARE_DATE.Enabled = False
                    txtARISING_FROM_MONTH.Enabled = False
                    txtARISING_TO_MONTH.Enabled = False
                    txtNOTE.Enabled = False

                    txtHEALTH_RETURN_DATE.Enabled = False

                    chkSI.Enabled = False
                    chkHI.Enabled = False
                    chkUI.Enabled = False
                    chkTNLD_BNN.Enabled = False
                    txtDateIssue.Enabled = False
                    txtDoB.Enabled = False

                    txtR_FROM.Enabled = False
                    txtR_TO.Enabled = False
                    txtR_SI.Enabled = False
                    txtR_HI.Enabled = False
                    txtR_UI.Enabled = False

                    txtO_FROM.Enabled = False
                    txtO_TO.Enabled = False
                    txtO_HI.Enabled = False
                    txtO_UI.Enabled = False

                    txtA_FROM.Enabled = False
                    txtA_TO.Enabled = False
                    txtA_SI.Enabled = False
                    txtA_HI.Enabled = False
                    txtA_UI.Enabled = False


                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
                    btnSearchEmp.Enabled = True

                    ddlINS_ORG_ID.Enabled = True
                    ddlINS_ARISING_TYPE_ID.Enabled = True
                    txtSALARY_PRE_PERIOD.Enabled = True
                    txtSALARY_NOW_PERIOD.Enabled = True
                    txtEFFECTIVE_DATE.Enabled = True
                    txtEXPRIE_DATE.Enabled = True
                    txtDECLARE_DATE.Enabled = True
                    txtARISING_FROM_MONTH.Enabled = True
                    txtARISING_TO_MONTH.Enabled = True
                    txtNOTE.Enabled = True
                    chkSI.Enabled = True
                    chkHI.Enabled = True
                    chkUI.Enabled = True
                    chkTNLD_BNN.Enabled = True
                    txtHEALTH_RETURN_DATE.Enabled = True

                    txtDateIssue.Enabled = False
                    txtDoB.Enabled = False

                    txtR_FROM.Enabled = True
                    txtR_TO.Enabled = True
                    txtR_SI.Enabled = True
                    txtR_HI.Enabled = True
                    txtR_UI.Enabled = True

                    txtO_FROM.Enabled = True
                    txtO_TO.Enabled = True
                    txtO_HI.Enabled = True
                    txtO_UI.Enabled = True

                    txtA_FROM.Enabled = True
                    txtA_TO.Enabled = True
                    txtA_SI.Enabled = True
                    txtA_HI.Enabled = True
                    txtA_UI.Enabled = True



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
        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Dim str As String = "getRadWindow().close('1');"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                Case CommonMessage.TOOLBARITEM_SAVE
                    Call SaveData()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Function & Sub"

    Private Sub ResetForm()
        Try
            txtID.Text = "0"
            txtEMPLOYEE_ID.Text = ""
            txtFULLNAME.Text = ""
            txtDEP.Text = ""
            txtEMPID.Text = "0"
            'ThanhNT added 05/01/2016
            txtPOSITION.Text = ""
            txtCMND.Text = ""
            txtDateIssue.SelectedDate = Nothing
            txtDoB.SelectedDate = Nothing
            txtBirthPlace.Text = ""
            ddlINS_ORG_ID.Text = ""
            ddlINS_ARISING_TYPE_ID.Text = ""
            ddlINS_ARISING_TYPE_ID.SelectedIndex = -1
            'ThanhNT added 05/01/2016  --end
            ddlINS_ORG_ID.Text = ""
            ddlINS_ORG_ID.SelectedIndex = -1
            txtSALARY_PRE_PERIOD.Text = ""
            txtSALARY_NOW_PERIOD.Text = ""
            txtEFFECTIVE_DATE.SelectedDate = Nothing
            txtEXPRIE_DATE.SelectedDate = Nothing
            txtDECLARE_DATE.SelectedDate = Nothing
            txtARISING_FROM_MONTH.SelectedDate = Nothing
            txtARISING_TO_MONTH.SelectedDate = Nothing
            txtNOTE.Text = ""

            txtHEALTH_RETURN_DATE.SelectedDate = Nothing

            txtR_FROM.SelectedDate = Nothing
            txtR_TO.SelectedDate = Nothing
            txtR_SI.Text = "0"
            txtR_HI.Text = "0"
            txtR_UI.Text = "0"

            txtO_FROM.SelectedDate = Nothing
            txtO_TO.SelectedDate = Nothing
            txtO_HI.Text = "0"
            txtO_UI.Text = "0"

            txtA_FROM.SelectedDate = Nothing
            txtA_TO.SelectedDate = Nothing
            txtA_SI.Text = "0"
            txtA_HI.Text = "0"
            txtA_UI.Text = "0"

        Catch ex As Exception
        End Try
    End Sub

    Private Sub SaveData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    'Nếu là điều chỉnh thì 2 mức lương phải khác nhau
                    If Arising_Type = 3 AndAlso InsCommon.getNumber(txtSALARY_PRE_PERIOD.Text) = InsCommon.getNumber(txtSALARY_NOW_PERIOD.Text) Then
                        ShowMessage("Vui lòng nhập mức lương kì này khác mức lương kì trước!", NotifyType.Warning)
                        UpdateControlState(CommonMessage.STATE_EDIT)
                        Exit Sub
                    End If
                    If rep.UpdateInsArisingManual(Common.Common.GetUsername(), InsCommon.getNumber(0) _
                                                    , InsCommon.getNumber(txtEMPID.Text) _
                                                    , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue) _
                                                    , InsCommon.getNumber(ddlINS_ARISING_TYPE_ID.SelectedValue) _
                                                    , InsCommon.getNumber(txtSALARY_PRE_PERIOD.Text) _
                                                    , InsCommon.getNumber(txtSALARY_NOW_PERIOD.Text) _
                                                    , InsCommon.getNumber(0) _
                                                    , (txtEFFECTIVE_DATE.SelectedDate) _
                                                    , txtEXPRIE_DATE.SelectedDate _
                                                    , (txtDECLARE_DATE.SelectedDate) _
                                                    , (txtARISING_FROM_MONTH.SelectedDate) _
                                                    , (txtARISING_TO_MONTH.SelectedDate) _
                                                    , txtNOTE.Text _
                                                    , "" _
                                                    , "" _
                                                    , InsCommon.getNumber(0) _
                                                    , Nothing _
                                                    , Nothing _
                                                    , InsCommon.getNumber(0) _
                                                    , (txtR_FROM.SelectedDate) _
                                                    , "" _
                                                    , txtHEALTH_RETURN_DATE.SelectedDate _
                                                    , (txtR_FROM.SelectedDate) _
                                                    , (txtR_FROM.SelectedDate) _
                                                    , (txtR_FROM.SelectedDate) _
                                                    , (txtR_FROM.SelectedDate) _
                                                    , (txtO_FROM.SelectedDate) _
                                                    , (txtR_TO.SelectedDate) _
                                                    , (txtO_TO.SelectedDate) _
                                                    , InsCommon.getNumber(txtR_SI.Text) _
                                                    , InsCommon.getNumber(0) _
                                                    , InsCommon.getNumber(txtR_HI.Text) _
                                                    , InsCommon.getNumber(txtO_HI.Text) _
                                                    , InsCommon.getNumber(txtR_UI.Text) _
                                                    , InsCommon.getNumber(txtO_UI.Text) _
                                                    , (txtA_FROM.SelectedDate) _
                                                    , (txtA_TO.SelectedDate) _
                                                    , InsCommon.getNumber(txtA_SI.Text) _
                                                    , InsCommon.getNumber(txtA_HI.Text) _
                                                    , InsCommon.getNumber(txtA_UI.Text) _
                                                    , InsCommon.getNumber(IIf(chkSI.Checked, -1, 0)) _
                                                    , InsCommon.getNumber(IIf(chkHI.Checked, -1, 0)) _
                                                    , InsCommon.getNumber(IIf(chkUI.Checked, -1, 0)) _
                                                    , InsCommon.getNumber(IIf(chkTNLD_BNN.Checked, -1, 0))
                                                    ) Then

                        Refresh("InsertView")
                        'Common.Common.OrganizationLocationDataSession = Nothing
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        Dim str As String = "getRadWindow().close('1');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        UpdateControlState(CommonMessage.STATE_EDIT)
                    End If
                    UpdateControlState(CommonMessage.STATE_NORMAL)
                    'Common.Common.OrganizationLocationDataSession = Nothing
                Case CommonMessage.STATE_EDIT
                    'Nếu là điều chỉnh thì 2 mức lương phải khác nhau
                    If Arising_Type = 3 AndAlso InsCommon.getNumber(txtSALARY_PRE_PERIOD.Text) = InsCommon.getNumber(txtSALARY_NOW_PERIOD.Text) Then
                        ShowMessage("Vui lòng nhập mức lương kì này khác mức lương kì trước!", NotifyType.Warning)
                        UpdateControlState(CommonMessage.STATE_EDIT)
                        Exit Sub
                    End If
                    'objOrgFunction.ID = Decimal.Parse(hidID.Value)
                    If rep.UpdateInsArisingManual(Common.Common.GetUsername(), InsCommon.getNumber(txtID.Text) _
                                                                    , InsCommon.getNumber(txtEMPID.Text) _
                                                                    , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue) _
                                                                    , InsCommon.getNumber(ddlINS_ARISING_TYPE_ID.SelectedValue) _
                                                                    , InsCommon.getNumber(txtSALARY_PRE_PERIOD.Text) _
                                                                    , InsCommon.getNumber(txtSALARY_NOW_PERIOD.Text) _
                                                                    , InsCommon.getNumber(0) _
                                                                    , (txtEFFECTIVE_DATE.SelectedDate) _
                                                                    , txtEXPRIE_DATE.SelectedDate _
                                                                    , (txtDECLARE_DATE.SelectedDate) _
                                                                    , (txtARISING_FROM_MONTH.SelectedDate) _
                                                                    , (txtARISING_TO_MONTH.SelectedDate) _
                                                                    , txtNOTE.Text _
                                                                    , "" _
                                                                    , "" _
                                                                    , InsCommon.getNumber(0) _
                                                                    , Nothing _
                                                                    , Nothing _
                                                                    , InsCommon.getNumber(0) _
                                                                    , (txtR_FROM.SelectedDate) _
                                                                    , "" _
                                                                    , txtHEALTH_RETURN_DATE.SelectedDate _
                                                                    , (txtR_FROM.SelectedDate) _
                                                                    , (txtR_FROM.SelectedDate) _
                                                                    , (txtR_FROM.SelectedDate) _
                                                                    , (txtR_FROM.SelectedDate) _
                                                                    , (txtO_FROM.SelectedDate) _
                                                                    , (txtR_TO.SelectedDate) _
                                                                    , (txtO_TO.SelectedDate) _
                                                                    , InsCommon.getNumber(txtR_SI.Text) _
                                                                    , InsCommon.getNumber(0) _
                                                                    , InsCommon.getNumber(txtR_HI.Text) _
                                                                    , InsCommon.getNumber(txtO_HI.Text) _
                                                                    , InsCommon.getNumber(txtR_UI.Text) _
                                                                    , InsCommon.getNumber(txtO_UI.Text) _
                                                                    , (txtA_FROM.SelectedDate) _
                                                                    , (txtA_TO.SelectedDate) _
                                                                    , InsCommon.getNumber(txtA_SI.Text) _
                                                                    , InsCommon.getNumber(txtA_HI.Text) _
                                                                    , InsCommon.getNumber(txtA_UI.Text) _
                                                                    , InsCommon.getNumber(IIf(chkSI.Checked, -1, 0)) _
                                                                    , InsCommon.getNumber(IIf(chkHI.Checked, -1, 0)) _
                                                                    , InsCommon.getNumber(IIf(chkUI.Checked, -1, 0)) _
                                                                    , InsCommon.getNumber(IIf(chkTNLD_BNN.Checked, -1, 0))
                                                                    ) Then
                        Refresh("UpdateView")
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        Dim str As String = "getRadWindow().close('1');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        'UpdateControlState(CommonMessage.STATE_NORMAL)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        UpdateControlState(CommonMessage.STATE_EDIT)
                    End If
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetDataCombo()
        Try
            Dim rep As New InsuranceBusinessClient
            Dim dtData = rep.GetInsListChangeType() 'Loai bien dong
            FillRadCombobox(ddlINS_ARISING_TYPE_ID, dtData, "ARISING_NAME", "ID", False)

            dtData = rep.GetInsListInsurance(False) 'Don vi bao hiem
            FillRadCombobox(ddlINS_ORG_ID, dtData, "NAME", "ID", False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtEXPRIE_DATE_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtEXPRIE_DATE.SelectedDateChanged
        Try
            If txtEXPRIE_DATE.SelectedDate.Value.Day <= 15 Then
                txtARISING_TO_MONTH.SelectedDate = txtEXPRIE_DATE.SelectedDate
            Else
                txtARISING_TO_MONTH.SelectedDate = txtEXPRIE_DATE.SelectedDate.Value.AddMonths(1)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtEFFECTIVE_DATE_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtEFFECTIVE_DATE.SelectedDateChanged
        Try
            If txtEFFECTIVE_DATE.SelectedDate IsNot Nothing Then
                If txtEFFECTIVE_DATE.SelectedDate.Value.Day <= 15 Then
                    txtARISING_FROM_MONTH.SelectedDate = txtEFFECTIVE_DATE.SelectedDate
                Else
                    txtARISING_FROM_MONTH.SelectedDate = txtEFFECTIVE_DATE.SelectedDate.Value.AddMonths(1)
                End If
                Call LoadArisingManual()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtDECLARE_DATE_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtDECLARE_DATE.SelectedDateChanged
        Try
            Call LoadArisingManual()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtHEALTH_RETURN_DATE_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtHEALTH_RETURN_DATE.SelectedDateChanged
        Try
            Call LoadArisingManual()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkSI_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSI.CheckedChanged
        Try
            Call LoadArisingManual()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkHI_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkHI.CheckedChanged
        Try
            Call LoadArisingManual()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkUI_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkUI.CheckedChanged
        Try
            Call LoadArisingManual()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkTNLD_BNN_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTNLD_BNN.CheckedChanged
        Try
            Call LoadArisingManual()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlINS_ARISING_TYPE_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlINS_ARISING_TYPE_ID.SelectedIndexChanged
        Call LoadArisingManual()
    End Sub

    Private Sub LoadArisingManual()
        Try
            'THANHNT ADDED 05/01/2016
            If ddlINS_ARISING_TYPE_ID.SelectedValue IsNot Nothing AndAlso CheckArisingType(ddlINS_ARISING_TYPE_ID.SelectedValue) AndAlso txtO_TO.SelectedDate IsNot Nothing AndAlso txtO_FROM.SelectedDate IsNot Nothing AndAlso txtSALARY_PRE_PERIOD.Value IsNot Nothing AndAlso txtO_FROM.SelectedDate < txtO_TO.SelectedDate Then
                'thực hiện tính toán số tiền cho truy thu bảo hiểm y tế
                Dim value = txtSALARY_PRE_PERIOD.Value * 0.045 * (((txtO_TO.SelectedDate.Value.Year - txtO_FROM.SelectedDate.Value.Year) * 12) + (txtO_TO.SelectedDate.Value.Month - txtO_FROM.SelectedDate.Value.Month + 1))
                InsCommon.SetNumber(txtO_HI, value)
            End If 'THANHNT ADDED 05/01/2016   
            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).InsAraisingAuto(Common.Common.GetUsername(), InsCommon.getNumber(txtEMPID.Text), InsCommon.getNumber(ddlINS_ARISING_TYPE_ID.SelectedValue), txtARISING_FROM_MONTH.SelectedDate, txtDECLARE_DATE.SelectedDate, txtHEALTH_RETURN_DATE.SelectedDate)
            Arising_Type = lstSource.Rows(0)("ARISING_TYPE")
            InsCommon.SetNumber(txtSALARY_PRE_PERIOD, lstSource.Rows(0)("SALARY_PRE_PERIOD"))
            InsCommon.SetNumber(txtSALARY_NOW_PERIOD, lstSource.Rows(0)("SALARY_NOW_PERIOD"))

            'ThanhNT added 26/07/2016
            '[2:39:36 PM] Luyen Nguyen Pham Bao: giảm => không cho nhập mức lương kỳ này
            '[2:39:47 PM] Luyen Nguyen Pham Bao: Tăng => không cho nhập mức lương kỳ trước
            '[2:39:57 PM] Luyen Nguyen Pham Bao: điều chỉnh: 2 mức lương phải khác nhau
            If Arising_Type = 1 Then 'TANG
                txtSALARY_PRE_PERIOD.ReadOnly = True
                txtSALARY_NOW_PERIOD.ReadOnly = False
            ElseIf lstSource.Rows(0)("ARISING_TYPE") = 2 Then 'giam
                txtSALARY_NOW_PERIOD.ReadOnly = True
                txtSALARY_PRE_PERIOD.ReadOnly = False
            Else
                txtSALARY_PRE_PERIOD.ReadOnly = False
                txtSALARY_NOW_PERIOD.ReadOnly = False
            End If
            InsCommon.SetDate(txtA_FROM, lstSource.Rows(0)("A_FROM"))
            InsCommon.SetDate(txtA_TO, lstSource.Rows(0)("A_TO"))

            InsCommon.SetDate(txtR_FROM, lstSource.Rows(0)("R_FROM"))
            InsCommon.SetDate(txtR_TO, lstSource.Rows(0)("R_TO"))

            'InsCommon.SetDate(txtO_FROM, lstSource.Rows(0)("O_FROM"))   //phuongnt created then ThanhNT edit
            'ThanhNT added 22/12/2015
            InsCommon.SetDate(txtO_FROM, txtDECLARE_DATE.SelectedDate)

            InsCommon.SetDate(txtO_TO, lstSource.Rows(0)("O_TO"))

            If chkSI.Checked Then
                InsCommon.SetNumber(txtA_SI, lstSource.Rows(0)("A_SI"))
                InsCommon.SetNumber(txtR_SI, lstSource.Rows(0)("R_SI"))
            Else
                txtA_SI.Value = Nothing
                txtR_SI.Value = Nothing
            End If

            If chkHI.Checked Then
                InsCommon.SetNumber(txtA_HI, lstSource.Rows(0)("A_HI"))
                ' Trường hợp giảm do nghỉ thai sản thì BHYT = 0
                'If ddlINS_ARISING_TYPE_ID.SelectedIndex <> -1 AndAlso ddlINS_ARISING_TYPE_ID.SelectedValue <> 13 Then
                '    InsCommon.SetNumber(txtR_HI, 0)
                'Else
                InsCommon.SetNumber(txtR_HI, lstSource.Rows(0)("R_HI"))
                'End If
                'ThanhNT added 22/12/2015

                InsCommon.SetNumber(txtO_HI, lstSource.Rows(0)("O_HI"))
            Else
                txtA_HI.Value = Nothing
                txtR_HI.Value = Nothing
                txtO_HI.Value = Nothing
            End If

            If chkUI.Checked Then
                InsCommon.SetNumber(txtA_UI, lstSource.Rows(0)("A_UI"))
                InsCommon.SetNumber(txtR_UI, lstSource.Rows(0)("R_UI"))
            Else
                txtA_UI.Value = Nothing
                txtR_UI.Value = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillData(ByVal idSelected As Decimal)
        Try
            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsArisingManual(Common.Common.GetUsername(), InsCommon.getNumber(txtID.Text) _
                                                                                                                                        , 0, 0 _
                                                                                                                                        , Nothing _
                                                                                                                                        , Nothing _
                                                                                                                                        , 0, 0)

            If lstSource IsNot Nothing AndAlso lstSource.Rows.Count > 0 Then
                Arising_Type = lstSource.Rows(0)("ARISING_TYPE")
                If Arising_Type = 1 Then 'TANG
                    txtSALARY_PRE_PERIOD.ReadOnly = True
                    txtSALARY_NOW_PERIOD.ReadOnly = False
                ElseIf lstSource.Rows(0)("ARISING_TYPE") = 2 Then 'giam
                    txtSALARY_NOW_PERIOD.ReadOnly = True
                    txtSALARY_PRE_PERIOD.ReadOnly = False
                Else
                    txtSALARY_PRE_PERIOD.ReadOnly = False
                    txtSALARY_NOW_PERIOD.ReadOnly = False
                End If
                InsCommon.SetString(txtEMPID, lstSource.Rows(0)("EMPID"))

                InsCommon.SetString(txtEMPLOYEE_ID, lstSource.Rows(0)("EMPLOYEE_CODE"))
                InsCommon.SetString(txtFULLNAME, lstSource.Rows(0)("FULL_NAME"))
                InsCommon.SetString(txtDEP, lstSource.Rows(0)("DEP_NAME"))
                InsCommon.SetDate(txtDoB, lstSource.Rows(0)("BIRTH_DATE"))
                InsCommon.SetString(txtBirthPlace, lstSource.Rows(0)("PLACE_OF_BIRTH_NAME"))
                InsCommon.SetString(txtCMND, lstSource.Rows(0)("ID_NO"))
                InsCommon.SetDate(txtDateIssue, lstSource.Rows(0)("ID_DATE"))
                InsCommon.SetString(txtPOSITION, lstSource.Rows(0)("POSITION_NAME"))

                InsCommon.SetNumber(chkSI, lstSource.Rows(0)("SI"))
                InsCommon.SetNumber(chkHI, lstSource.Rows(0)("HI"))
                InsCommon.SetNumber(chkUI, lstSource.Rows(0)("UI"))
                InsCommon.SetNumber(chkTNLD_BNN, lstSource.Rows(0)("BHTNLD_BNN"))

                InsCommon.SetNumber(ddlINS_ORG_ID, lstSource.Rows(0)("INS_ORG_ID"))
                InsCommon.SetNumber(ddlINS_ARISING_TYPE_ID, lstSource.Rows(0)("INS_ARISING_TYPE_ID"))
                InsCommon.SetNumber(txtSALARY_PRE_PERIOD, lstSource.Rows(0)("SALARY_PRE_PERIOD"))
                InsCommon.SetNumber(txtSALARY_NOW_PERIOD, lstSource.Rows(0)("SALARY_NOW_PERIOD"))


                InsCommon.SetDate(txtEFFECTIVE_DATE, lstSource.Rows(0)("EFFECTIVE_DATE"))
                InsCommon.SetDate(txtEXPRIE_DATE, lstSource.Rows(0)("EXPRIE_DATE"))
                InsCommon.SetDate(txtDECLARE_DATE, lstSource.Rows(0)("DECLARE_DATE"))
                InsCommon.SetDate(txtARISING_FROM_MONTH, lstSource.Rows(0)("ARISING_FROM_MONTH"))
                InsCommon.SetDate(txtARISING_TO_MONTH, lstSource.Rows(0)("ARISING_TO_MONTH"))
                InsCommon.SetDate(txtHEALTH_RETURN_DATE, lstSource.Rows(0)("HEALTH_RETURN_DATE"))

                InsCommon.SetString(txtNOTE, lstSource.Rows(0)("NOTE"))

                InsCommon.SetDate(txtR_FROM, lstSource.Rows(0)("R_FROM"))
                InsCommon.SetDate(txtR_TO, lstSource.Rows(0)("R_TO"))
                InsCommon.SetNumber(txtR_SI, lstSource.Rows(0)("R_SI"))
                InsCommon.SetNumber(txtR_HI, lstSource.Rows(0)("R_HI"))
                InsCommon.SetNumber(txtR_UI, lstSource.Rows(0)("R_UI"))

                InsCommon.SetDate(txtA_FROM, lstSource.Rows(0)("A_FROM"))
                InsCommon.SetDate(txtA_TO, lstSource.Rows(0)("A_TO"))
                InsCommon.SetNumber(txtA_SI, lstSource.Rows(0)("A_SI"))
                InsCommon.SetNumber(txtA_HI, lstSource.Rows(0)("A_HI"))
                InsCommon.SetNumber(txtA_UI, lstSource.Rows(0)("A_UI"))

                InsCommon.SetDate(txtO_FROM, lstSource.Rows(0)("O_FROM"))
                InsCommon.SetDate(txtO_TO, lstSource.Rows(0)("O_TO"))
                'InsCommon.SetNumber(txtO_SI, lstSource.Rows(0)("O_SI"))
                InsCommon.SetNumber(txtO_HI, lstSource.Rows(0)("O_HI"))
                InsCommon.SetNumber(txtO_UI, lstSource.Rows(0)("O_UI"))

            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub txtR_FROM_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtR_FROM.SelectedDateChanged
        Call LoadArisingManual2("0")
    End Sub

    Private Sub txtR_TO_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtR_TO.SelectedDateChanged
        Call LoadArisingManual2("0")
    End Sub

    Private Sub txtA_FROM_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtA_FROM.SelectedDateChanged
        Call LoadArisingManual2("1")
    End Sub

    Private Sub txtA_TO_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtA_TO.SelectedDateChanged
        Call LoadArisingManual2("1")
    End Sub

    'ThanhNT added 05/01/2016
    Private Sub txtO_FROM_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtO_FROM.SelectedDateChanged
        If ddlINS_ARISING_TYPE_ID.SelectedValue IsNot Nothing AndAlso CheckArisingType(ddlINS_ARISING_TYPE_ID.SelectedValue) AndAlso txtO_TO.SelectedDate IsNot Nothing AndAlso txtSALARY_PRE_PERIOD.Value IsNot Nothing AndAlso txtO_FROM.SelectedDate < txtO_TO.SelectedDate Then
            'thực hiện tính toán số tiền cho truy thu bảo hiểm y tế
            Dim value = txtSALARY_PRE_PERIOD.Value * 0.045 * (((txtO_TO.SelectedDate.Value.Year - txtO_FROM.SelectedDate.Value.Year) * 12) + (txtO_TO.SelectedDate.Value.Month - txtO_FROM.SelectedDate.Value.Month + 1))
            InsCommon.SetNumber(txtO_HI, value)
        End If
    End Sub

    'ThanhNT added 05/01/2016
    Private Sub txtO_TO_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtO_TO.SelectedDateChanged
        If ddlINS_ARISING_TYPE_ID.SelectedValue IsNot Nothing AndAlso CheckArisingType(ddlINS_ARISING_TYPE_ID.SelectedValue) AndAlso txtO_FROM.SelectedDate IsNot Nothing AndAlso txtSALARY_PRE_PERIOD.Value IsNot Nothing AndAlso txtO_FROM.SelectedDate < txtO_TO.SelectedDate Then
            'thực hiện tính toán số tiền cho truy thu bảo hiểm y tế
            Dim value = txtSALARY_PRE_PERIOD.Value * 0.045 * ((txtO_TO.SelectedDate.Value.Year - txtO_FROM.SelectedDate.Value.Year) * 12 + (txtO_TO.SelectedDate.Value.Month - txtO_FROM.SelectedDate.Value.Month) + 1)
            InsCommon.SetNumber(txtO_HI, value)
        End If
    End Sub

    'ThanhNT added 05/01/2016
    Private Function CheckArisingType(ByVal arisingTypeID As Decimal) As Boolean
        Try
            Dim rs = (New InsuranceBusiness.InsuranceBusinessClient).Check_Arising_Type(arisingTypeID)
            If rs Is Nothing Then
                ShowMessage("Đã xảy ra lỗi trong quá trình kiểm tra loại biến động", NotifyType.Warning)
                Exit Function
            Else
                If Decimal.Parse(rs(0)(0).ToString()) = 0 Then 'giam
                    Return True
                Else 'tang
                    Return False
                End If
            End If
        Catch ex As Exception

        End Try

    End Function

    Private Sub LoadArisingManual2(Optional ByVal truythu As String = "1")
        If CurrentState = CommonMessage.STATE_NEW Or CurrentState = CommonMessage.STATE_EDIT Then
            Try
                Dim lstSource As DataTable
                If truythu = "1" Then
                    lstSource = (New InsuranceBusiness.InsuranceBusinessClient).InsAraisingAuto2(Common.Common.GetUsername(), InsCommon.getNumber(txtEMPID.Text), InsCommon.getNumber(ddlINS_ARISING_TYPE_ID.SelectedValue), txtA_FROM.SelectedDate, txtA_TO.SelectedDate, txtHEALTH_RETURN_DATE.SelectedDate, truythu)
                    If chkSI.Checked Then
                        InsCommon.SetNumber(txtA_SI, lstSource.Rows(0)("A_SI"))
                        'InsCommon.SetNumber(txtR_SI, lstSource.Rows(0)("R_SI"))
                    Else
                        txtA_SI.Value = Nothing
                        'txtR_SI.Value = Nothing
                    End If

                    If chkHI.Checked Then
                        InsCommon.SetNumber(txtA_HI, lstSource.Rows(0)("A_HI"))
                        'InsCommon.SetNumber(txtR_HI, lstSource.Rows(0)("R_HI"))
                    Else
                        txtA_HI.Value = Nothing
                        'txtR_HI.Value = Nothing
                    End If

                    If chkUI.Checked Then
                        InsCommon.SetNumber(txtA_UI, lstSource.Rows(0)("A_UI"))
                        'InsCommon.SetNumber(txtR_UI, lstSource.Rows(0)("R_UI"))
                    Else
                        txtA_UI.Value = Nothing
                        'txtR_UI.Value = Nothing
                    End If

                Else
                    lstSource = (New InsuranceBusiness.InsuranceBusinessClient).InsAraisingAuto2(Common.Common.GetUsername(), InsCommon.getNumber(txtEMPID.Text), InsCommon.getNumber(ddlINS_ARISING_TYPE_ID.SelectedValue), txtR_FROM.SelectedDate, txtR_TO.SelectedDate, txtHEALTH_RETURN_DATE.SelectedDate, truythu)

                    If chkSI.Checked Then
                        'InsCommon.SetNumber(txtA_SI, lstSource.Rows(0)("A_SI"))
                        InsCommon.SetNumber(txtR_SI, lstSource.Rows(0)("R_SI"))
                    Else
                        'txtA_SI.Value = Nothing
                        txtR_SI.Value = Nothing
                    End If

                    If chkHI.Checked Then
                        'InsCommon.SetNumber(txtA_HI, lstSource.Rows(0)("A_HI"))
                        InsCommon.SetNumber(txtR_HI, lstSource.Rows(0)("R_HI"))
                    Else
                        'txtA_HI.Value = Nothing
                        txtR_HI.Value = Nothing
                    End If

                    If chkUI.Checked Then
                        'InsCommon.SetNumber(txtA_UI, lstSource.Rows(0)("A_UI"))
                        InsCommon.SetNumber(txtR_UI, lstSource.Rows(0)("R_UI"))
                    Else
                        'txtA_UI.Value = Nothing
                        txtR_UI.Value = Nothing
                    End If
                End If

                'InsCommon.SetNumber(txtSALARY_PRE_PERIOD, lstSource.Rows(0)("SALARY_PRE_PERIOD"))
                'InsCommon.SetNumber(txtSALARY_NOW_PERIOD, lstSource.Rows(0)("SALARY_NOW_PERIOD"))

                'InsCommon.SetDate(txtA_FROM, lstSource.Rows(0)("A_FROM"))
                'InsCommon.SetDate(txtA_TO, lstSource.Rows(0)("A_TO"))

                'InsCommon.SetDate(txtR_FROM, lstSource.Rows(0)("R_FROM"))
                'InsCommon.SetDate(txtR_TO, lstSource.Rows(0)("R_TO"))




            Catch ex As Exception

            End Try
        End If

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
                    ElseIf Status = 0 Then 'New
                        txtID.Text = "0"
                        txtEMPID.Text = "0"
                        CurrentState = CommonMessage.STATE_NEW
                        ResetForm()
                        If Request.Params("EmployeeID") IsNot Nothing Then
                            FillDataEmp(Decimal.Parse(Request.Params("EmployeeID")))
                        End If
                    Else 'View
                        If Request.Params("IDSelect") IsNot Nothing Then
                            txtID.Text = Decimal.Parse(Request.Params("IDSelect"))
                            txtEMPID.Text = Decimal.Parse(Request.Params("EmployeeID"))
                            CurrentState = CommonMessage.STATE_NORMAL
                            FillData(txtID.Text)
                        End If
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
    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of Common.CommonBusiness.EmployeePopupFindDTO)
        Try
            Dim a As Object = ctrlFindEmployeePopup.SelectedEmployee
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of Common.CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetEmpInfo(item.EMPLOYEE_ID, 0)
                If (Not (lstSource Is Nothing) AndAlso lstSource.Rows.Count > 0) Then
                    If getSE_CASE_CONFIG("ctrlInsArisingManual_case1") > 0 Then

                    Else
                        If lstSource.Rows(0)("CHECK_EMP_INFOR").ToString() = "" Then 'nhân viên đó không tham gia bảo hiểm
                            btnSearchEmp.Focus()
                            ShowMessage("Nhân viên đã chọn không có thông tin bảo hiểm. Vui lòng chọn khác", NotifyType.Warning, 10)
                            Exit Sub
                        End If
                    End If
                    
                    txtEMPLOYEE_ID.Text = lstSource.Rows(0)("EMPLOYEE_ID")
                    InsCommon.SetString(txtFULLNAME, lstSource.Rows(0)("VN_FULLNAME"))
                    InsCommon.SetString(txtDEP, lstSource.Rows(0)("ORG_NAME"))
                    InsCommon.SetDate(txtDoB, lstSource.Rows(0)("BIRTH_DATE"))

                    InsCommon.SetString(txtBirthPlace, lstSource.Rows(0)("PLACE_OF_BIRTH_NAME"))
                    InsCommon.SetString(txtCMND, lstSource.Rows(0)("ID_NO"))

                    InsCommon.SetDate(txtDateIssue, lstSource.Rows(0)("ID_DATE"))
                    InsCommon.SetString(txtPOSITION, lstSource.Rows(0)("POSITION_NAME"))
                    'txtINSORG.Text = lstSource.Rows(0)("INS_ORG_NAME")

                    txtEMPID.Text = lstSource.Rows(0)("EMPID")

                    InsCommon.SetNumber(ddlINS_ORG_ID, lstSource.Rows(0)("INS_ORG_ID"))

                    chkSI.Checked = True
                    chkHI.Checked = True
                    chkUI.Checked = True
                    chkTNLD_BNN.Checked = True
                End If

                'hidEmployeeID.Value = item.ID.ToString
                'txtEmplloyeeCode.Text = item.EMPLOYEE_ID
                'txtEmployeeName.Text = item.VN_FULLNAME
            End If
            isLoadPopup = 0
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnSearchEmp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchEmp.Click
        Try
            isLoadPopup = 1

            ShowPopupEmployee()
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
                ctrlFindEmployeePopup.MustHaveContract = False
                ctrlFindEmployeePopup.MultiSelect = False
                FindEmployee.Controls.Add(ctrlFindEmployeePopup)

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Sub FillDataEmp(ByVal empID As Decimal)
        Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetEmpInfo(empID, 0)
        If (Not (lstSource Is Nothing) AndAlso lstSource.Rows.Count > 0) Then
            If lstSource.Rows(0)("CHECK_EMP_INFOR").ToString() = "" Then 'nhân viên đó không tham gia bảo hiểm
                btnSearchEmp.Focus()
                ShowMessage("Nhân viên đã chọn không có thông tin bảo hiểm. Vui lòng chọn khác", NotifyType.Warning, 10)
                Exit Sub
            End If
            txtEMPLOYEE_ID.Text = lstSource.Rows(0)("EMPLOYEE_ID")
            InsCommon.SetString(txtFULLNAME, lstSource.Rows(0)("VN_FULLNAME"))
            InsCommon.SetString(txtDEP, lstSource.Rows(0)("ORG_NAME"))
            InsCommon.SetDate(txtDoB, lstSource.Rows(0)("BIRTH_DATE"))

            InsCommon.SetString(txtBirthPlace, lstSource.Rows(0)("PLACE_OF_BIRTH_NAME"))
            InsCommon.SetString(txtCMND, lstSource.Rows(0)("ID_NO"))

            InsCommon.SetDate(txtDateIssue, lstSource.Rows(0)("ID_DATE"))
            InsCommon.SetString(txtPOSITION, lstSource.Rows(0)("POSITION_NAME"))
            'txtINSORG.Text = lstSource.Rows(0)("INS_ORG_NAME")

            txtEMPID.Text = lstSource.Rows(0)("EMPID")

            InsCommon.SetNumber(ddlINS_ORG_ID, lstSource.Rows(0)("INS_ORG_ID"))

            chkSI.Checked = True
            chkHI.Checked = True
            chkUI.Checked = True
            chkTNLD_BNN.Checked = True
        End If
    End Sub
#End Region

End Class
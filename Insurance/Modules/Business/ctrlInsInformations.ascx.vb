﻿Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Telerik.Web.UI
Imports Insurance.InsuranceBusiness

Public Class ctrlInsInformations
    Inherits Common.CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlOrgSULPPopup As ctrlFindOrgPopup
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

#Region "Property & Variable"

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

#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            Me.MainToolBar = rtbMain
            Common.Common.BuildToolbar(Me.MainToolBar,
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
            GetDataCombo()
            GetParams()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Sub UpdateControlState(ByVal sState As String)
        Try
            Select Case sState
                Case CommonMessage.STATE_NORMAL
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
                    btnSearchEmp.Enabled = False

                    txtDateIssue.Enabled = False
                    txtDoB.Enabled = False
                    'txtSENIORITY_INSURANCE.Enabled = False
                    'txtSENIORITY_INSURANCE_COMPANY.Enabled = False
                    txtSOCIAL_NUMBER.Enabled = False
                    ddlSOCIAL_STATUS.Enabled = False
                    txtSOCIAL_SUBMIT_DATE.Enabled = False
                    txtSOCIAL_SUBMIT.Enabled = False
                    txtSOCIAL_GRANT_DATE.Enabled = False
                    txtSOCIAL_SAVE_NUMBER.Enabled = False
                    txtSOCIAL_RETURN_DATE.Enabled = False
                    txtSOCIAL_RECEIVER.Enabled = False
                    txtSOCIAL_NOTE.Enabled = False
                    txtHEALTH_NUMBER.Enabled = False
                    ddlHEALTH_STATUS.Enabled = False
                    txtHEALTH_EFFECT_FROM_DATE.Enabled = False
                    txtHEALTH_EFFECT_TO_DATE.Enabled = False
                    ddlHEALTH_AREA_INS_ID.Enabled = False
                    txtHEALTH_RECEIVE_DATE.Enabled = False

                    txtSI_FROM_MONTH.Enabled = False
                    txtSI_TO_MONTH.Enabled = False
                    txtHI_FROM_MONTH.Enabled = False
                    txtHI_TO_MONTH.Enabled = False

                    chkHI.Enabled = False
                    chkSI.Enabled = False
                    chkUI.Enabled = False
                    txtSALARY.Enabled = False

                    chkBHTNLD_BNN.Enabled = False
                    chkIS_HI_FIVE_YEAR.Enabled = False
                    txtBHTNLD_BNN_FROM_MONTH.Enabled = False
                    txtBHTNLD_BNN_TO_MONTH.Enabled = False
                    rtxtSeniortyMonth.Enabled = False
                    rtxtSeniortyMonthCT.Enabled = False
                    rtxtSeniortyYear.Enabled = False
                    rtxtSeniortyYearCT.Enabled = False
                    cboPlaceBHYT.Enabled = False

                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
                    btnSearchEmp.Enabled = True

                    txtDateIssue.Enabled = False
                    txtDoB.Enabled = False

                    txtSOCIAL_NUMBER.Enabled = True
                    ddlSOCIAL_STATUS.Enabled = True
                    txtSOCIAL_SUBMIT_DATE.Enabled = True
                    txtSOCIAL_SUBMIT.Enabled = True
                    txtSOCIAL_GRANT_DATE.Enabled = True
                    txtSOCIAL_SAVE_NUMBER.Enabled = True
                    txtSOCIAL_RETURN_DATE.Enabled = True
                    txtSOCIAL_RECEIVER.Enabled = True
                    txtSOCIAL_NOTE.Enabled = True
                    txtHEALTH_NUMBER.Enabled = True
                    ddlHEALTH_STATUS.Enabled = True
                    txtHEALTH_EFFECT_FROM_DATE.Enabled = True
                    txtHEALTH_EFFECT_TO_DATE.Enabled = True
                    ddlHEALTH_AREA_INS_ID.Enabled = True
                    txtHEALTH_RECEIVE_DATE.Enabled = True
                    txtHEALTH_RETURN_DATE.Enabled = True
                    txtUNEMP_FROM_MONTH.Enabled = True
                    txtUNEMP_TO_MONTH.Enabled = True

                    txtSI_FROM_MONTH.Enabled = True
                    txtSI_TO_MONTH.Enabled = True
                    txtHI_FROM_MONTH.Enabled = True
                    txtHI_TO_MONTH.Enabled = True

                    chkHI.Enabled = True
                    chkSI.Enabled = True
                    chkUI.Enabled = True
                    txtSALARY.Enabled = False

                    chkBHTNLD_BNN.Enabled = True
                    chkIS_HI_FIVE_YEAR.Enabled = True
                    txtBHTNLD_BNN_FROM_MONTH.Enabled = True
                    txtBHTNLD_BNN_TO_MONTH.Enabled = True

                    rtxtSeniortyMonth.Enabled = True
                    rtxtSeniortyMonthCT.Enabled = True
                    rtxtSeniortyYear.Enabled = True
                    rtxtSeniortyYearCT.Enabled = True
                    cboPlaceBHYT.Enabled = True

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
                Case CommonMessage.TOOLBARITEM_SAVE
                    Call SaveData()
                    UpdateControlState(CommonMessage.STATE_NORMAL)
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Dim str As String = "getRadWindow().close('1');"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                Case CommonMessage.TOOLBARITEM_CANCEL
                    UpdateControlState(CommonMessage.STATE_NORMAL)
                Case CommonMessage.TOOLBARITEM_HIEXT
                    Response.Redirect("Default.aspx?mid=Insurance&fid=ctrlInsHealthExt&group=Business")
                Case CommonMessage.TOOLBARITEM_HIUPDATEINFO
                    Response.Redirect("Default.aspx?mid=Insurance&fid=ctrlInsHealthImport&group=Business")
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
            txtDoB.SelectedDate = Nothing
            txtBirthPlace.Text = ""
            txtCMND.Text = ""
            txtDateIssue.SelectedDate = Nothing
            txtPOSITION.Text = ""
            txtINSORG.Text = ""
            txtSALARY.Text = 0
            txtEMPID.Text = "0"
            txtSOCIAL_NUMBER.Text = ""
            ddlSOCIAL_STATUS.SelectedIndex = 0
            txtSOCIAL_SUBMIT_DATE.SelectedDate = Nothing
            txtSOCIAL_GRANT_DATE.SelectedDate = Nothing
            txtSOCIAL_SAVE_NUMBER.Enabled = True
            txtSOCIAL_RETURN_DATE.SelectedDate = Nothing
            txtSOCIAL_RECEIVER.Text = ""
            txtSOCIAL_NOTE.Text = ""
            txtHEALTH_NUMBER.Text = ""
            ddlHEALTH_STATUS.SelectedIndex = 0
            txtHEALTH_EFFECT_FROM_DATE.SelectedDate = Nothing
            txtHEALTH_EFFECT_TO_DATE.SelectedDate = Nothing
            ddlHEALTH_AREA_INS_ID.SelectedIndex = 0
            txtHEALTH_RECEIVE_DATE.SelectedDate = Nothing
            txtHEALTH_RETURN_DATE.SelectedDate = Nothing
            txtUNEMP_FROM_MONTH.SelectedDate = Nothing
            txtUNEMP_TO_MONTH.SelectedDate = Nothing
            txtSI_FROM_MONTH.SelectedDate = Nothing
            txtSI_TO_MONTH.SelectedDate = Nothing
            txtHI_FROM_MONTH.SelectedDate = Nothing
            txtHI_TO_MONTH.SelectedDate = Nothing
            chkSI.Checked = True
            chkHI.Checked = True
            chkUI.Checked = True
            chkBHTNLD_BNN.Checked = True
            chkIS_HI_FIVE_YEAR.Checked = False
            txtBHTNLD_BNN_FROM_MONTH.SelectedDate = Nothing
            txtBHTNLD_BNN_TO_MONTH.SelectedDate = Nothing
            rtxtSeniortyMonth.Text = Nothing
            rtxtSeniortyMonthCT.Text = Nothing
            rtxtSeniortyYear.Text = Nothing
            rtxtSeniortyYearCT.Text = Nothing
            cboPlaceBHYT.SelectedIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SaveData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    'ShowMessage("Đã tồn tại phòng ban cao nhất?", NotifyType.Warning)

                    If rep.UpdateInsInfomation(Common.Common.GetUsername(), txtID.Text _
                                                    , InsCommon.getNumber(txtEMPID.Text) _
                                                    , InsCommon.getString(txtINSORG.Text) _
                                                    , Nothing _
                                                    , Nothing _
                                                    , txtSOCIAL_NUMBER.Text _
                                                    , InsCommon.getNumber(ddlSOCIAL_STATUS.SelectedValue) _
                                                    , txtSOCIAL_SUBMIT_DATE.SelectedDate _
                                                    , txtSOCIAL_SUBMIT.Text _
                                                    , txtSOCIAL_GRANT_DATE.SelectedDate _
                                                    , InsCommon.getString(txtSOCIAL_SAVE_NUMBER.Text) _
                                                    , DateTime.Now() _
                                                    , txtSOCIAL_RETURN_DATE.SelectedDate _
                                                    , txtSOCIAL_RECEIVER.Text _
                                                    , txtSOCIAL_NOTE.Text _
                                                    , txtHEALTH_NUMBER.Text _
                                                    , InsCommon.getNumber(ddlHEALTH_STATUS.SelectedValue) _
                                                    , txtHEALTH_EFFECT_FROM_DATE.SelectedDate _
                                                    , txtHEALTH_EFFECT_TO_DATE.SelectedDate _
                                                    , InsCommon.getNumber(ddlHEALTH_AREA_INS_ID.SelectedValue) _
                                                    , txtHEALTH_RECEIVE_DATE.SelectedDate _
                                                    , Nothing _
                                                    , txtHEALTH_RETURN_DATE.SelectedDate _
                                                    , txtUNEMP_FROM_MONTH.SelectedDate _
                                                    , txtUNEMP_TO_MONTH.SelectedDate _
                                                    , DateTime.Now() _
                                                    , txtSI_FROM_MONTH.SelectedDate _
                                                    , txtSI_TO_MONTH.SelectedDate _
                                                    , txtHI_FROM_MONTH.SelectedDate _
                                                    , txtHI_TO_MONTH.SelectedDate _
                                                    , InsCommon.getNumber(IIf(chkSI.Checked, 1, 0)) _
                                                    , InsCommon.getNumber(IIf(chkHI.Checked, 1, 0)) _
                                                    , InsCommon.getNumber(IIf(chkUI.Checked, 1, 0)) _
                                                    , InsCommon.getNumber(IIf(chkBHTNLD_BNN.Checked, 1, 0)) _
                                                    , InsCommon.getNumber(IIf(chkIS_HI_FIVE_YEAR.Checked, 1, 0)) _
                                                    , txtBHTNLD_BNN_FROM_MONTH.SelectedDate _
                                                    , txtBHTNLD_BNN_TO_MONTH.SelectedDate _
                                                    , rtxtSeniortyYear.Value _
                                                    , rtxtSeniortyMonth.Value _
                                                    , rtxtSeniortyYearCT.Value _
                                                    , rtxtSeniortyMonthCT.Value _
                                                    , txtSeniorityTotal.Text _
                                                    , InsCommon.getNumber(cboPlaceBHYT.SelectedValue)
                                                    ) Then
                        Refresh("InsertView")
                        'Common.Common.OrganizationLocationDataSession = Nothing
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        Dim str As String = "getRadWindow().close('1');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
                    'Common.Common.OrganizationLocationDataSession = Nothing
                Case CommonMessage.STATE_EDIT
                    'objOrgFunction.ID = Decimal.Parse(hidID.Value)
                    If rep.UpdateInsInfomation(Common.Common.GetUsername(), txtID.Text _
                                                    , InsCommon.getNumber(txtEMPID.Text) _
                                                    , InsCommon.getString(txtINSORG.Text) _
                                                    , Nothing _
                                                    , Nothing _
                                                    , txtSOCIAL_NUMBER.Text _
                                                    , InsCommon.getNumber(ddlSOCIAL_STATUS.SelectedValue) _
                                                    , txtSOCIAL_SUBMIT_DATE.SelectedDate _
                                                    , txtSOCIAL_SUBMIT.Text _
                                                    , txtSOCIAL_GRANT_DATE.SelectedDate _
                                                    , InsCommon.getString(txtSOCIAL_SAVE_NUMBER.Text) _
                                                    , DateTime.Now() _
                                                    , txtSOCIAL_RETURN_DATE.SelectedDate _
                                                    , txtSOCIAL_RECEIVER.Text _
                                                    , txtSOCIAL_NOTE.Text _
                                                    , txtHEALTH_NUMBER.Text _
                                                    , InsCommon.getNumber(ddlHEALTH_STATUS.SelectedValue) _
                                                    , txtHEALTH_EFFECT_FROM_DATE.SelectedDate _
                                                    , txtHEALTH_EFFECT_TO_DATE.SelectedDate _
                                                    , InsCommon.getNumber(ddlHEALTH_AREA_INS_ID.SelectedValue) _
                                                    , txtHEALTH_RECEIVE_DATE.SelectedDate _
                                                    , Nothing _
                                                    , txtHEALTH_RETURN_DATE.SelectedDate _
                                                    , txtUNEMP_FROM_MONTH.SelectedDate _
                                                    , txtUNEMP_TO_MONTH.SelectedDate _
                                                    , DateTime.Now() _
                                                    , txtSI_FROM_MONTH.SelectedDate _
                                                    , txtSI_TO_MONTH.SelectedDate _
                                                    , txtHI_FROM_MONTH.SelectedDate _
                                                    , txtHI_TO_MONTH.SelectedDate _
                                                    , InsCommon.getNumber(IIf(chkSI.Checked, 1, 0)) _
                                                    , InsCommon.getNumber(IIf(chkHI.Checked, 1, 0)) _
                                                    , InsCommon.getNumber(IIf(chkUI.Checked, 1, 0)) _
                                                    , InsCommon.getNumber(IIf(chkBHTNLD_BNN.Checked, 1, 0)) _
                                                    , InsCommon.getNumber(IIf(chkIS_HI_FIVE_YEAR.Checked, 1, 0)) _
                                                    , txtBHTNLD_BNN_FROM_MONTH.SelectedDate _
                                                    , txtBHTNLD_BNN_TO_MONTH.SelectedDate _
                                                    , rtxtSeniortyYear.Value _
                                                    , rtxtSeniortyMonth.Value _
                                                    , rtxtSeniortyYearCT.Value _
                                                    , rtxtSeniortyMonthCT.Value _
                                                    , txtSeniorityTotal.Text _
                                                    , InsCommon.getNumber(cboPlaceBHYT.SelectedValue)
                                                    ) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        Dim str As String = "getRadWindow().close('1');"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                    End If
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetDataCombo()
        Try
            Dim rep As New InsuranceBusinessClient
            Dim dtData = rep.GetOtherList("STATUS_NOBOOK", Common.Common.SystemLanguage.Name, False) 'Trang thai sổ BH
            FillRadCombobox(ddlSOCIAL_STATUS, dtData, "NAME", "ID", False)

            dtData = rep.GetOtherList("STATUS_CARD", Common.Common.SystemLanguage.Name, False) 'Trang thai thẻ
            FillRadCombobox(ddlHEALTH_STATUS, dtData, "NAME", "ID", False)

            dtData = rep.GetInsListWhereHealth()
            FillRadCombobox(ddlHEALTH_AREA_INS_ID, dtData, "NAME_VN", "ID", False)

            dtData = rep.GetOtherList("PLACE_BHYT", Common.Common.SystemLanguage.Name, False) 'Trang thai thẻ
            FillRadCombobox(cboPlaceBHYT, dtData, "NAME", "ID", False)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function CheckExist(ByVal strEmpId As String) As Boolean
        Try
            Dim tblSource As DataTable
            tblSource = (New InsuranceBusiness.InsuranceBusinessClient).Check_Exist_Emp_Ins(strEmpId)

            If tblSource IsNot Nothing AndAlso tblSource.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

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

    Private Sub FillData(ByVal idSelected As Decimal)
        Try
            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsInfomation(Common.Common.GetUsername(), idSelected, 0, 0, 0, 0)

            If lstSource IsNot Nothing AndAlso lstSource.Rows.Count > 0 Then

                InsCommon.SetString(txtEMPID, lstSource.Rows(0)("EMPID"))
                InsCommon.SetString(txtEMPLOYEE_ID, lstSource.Rows(0)("EMPLOYEE_CODE"))
                InsCommon.SetString(txtFULLNAME, lstSource.Rows(0)("FULL_NAME"))
                InsCommon.SetString(txtDEP, lstSource.Rows(0)("DEP_NAME"))
                InsCommon.SetDate(txtDoB, lstSource.Rows(0)("BIRTH_DATE"))
                InsCommon.SetString(txtBirthPlace, lstSource.Rows(0)("PLACE_OF_BIRTH_NAME"))
                InsCommon.SetString(txtBirthPlaceCmnd, lstSource.Rows(0)("PLACE_OF_BIRTH_NAME"))
                InsCommon.SetString(txtCMND, lstSource.Rows(0)("ID_NO"))
                InsCommon.SetDate(txtDateIssue, lstSource.Rows(0)("ID_DATE"))
                InsCommon.SetString(txtINSORG, lstSource.Rows(0)("INS_ORG_NAME"))
                InsCommon.SetString(txtPOSITION, lstSource.Rows(0)("POSITION_NAME"))
                InsCommon.SetNumber(txtSALARY, lstSource.Rows(0)("SALARY"))
                InsCommon.SetNumber(rtxtSeniortyMonth, lstSource.Rows(0)("SENIORITY_MONTH"))
                InsCommon.SetNumber(rtxtSeniortyMonthCT, lstSource.Rows(0)("SENIORITY_MONTH_CP"))
                InsCommon.SetNumber(rtxtSeniortyYear, lstSource.Rows(0)("SENIORITY_YEAR"))
                InsCommon.SetNumber(rtxtSeniortyYearCT, lstSource.Rows(0)("SENIORITY_YEAR_CP"))
                InsCommon.SetNumber(cboPlaceBHYT, lstSource.Rows(0)("PLACE_BHYT_ID"))
                InsCommon.SetNumber(chkSI, lstSource.Rows(0)("SI"))
                InsCommon.SetNumber(chkHI, lstSource.Rows(0)("HI"))
                InsCommon.SetNumber(chkUI, lstSource.Rows(0)("UI"))
                InsCommon.SetString(txtSOCIAL_NUMBER, lstSource.Rows(0)("SOCIAL_NUMBER"))
                InsCommon.SetNumber(ddlSOCIAL_STATUS, lstSource.Rows(0)("SOCIAL_STATUS"))
                InsCommon.SetDate(txtSOCIAL_SUBMIT_DATE, lstSource.Rows(0)("SOCIAL_SUBMIT_DATE"))
                InsCommon.SetDate(txtSOCIAL_GRANT_DATE, lstSource.Rows(0)("SOCIAL_GRANT_DATE"))
                InsCommon.SetString(txtSOCIAL_SAVE_NUMBER, lstSource.Rows(0)("SOCIAL_SAVE_NUMBER"))
                InsCommon.SetDate(txtSOCIAL_RETURN_DATE, lstSource.Rows(0)("SOCIAL_RETURN_DATE"))
                InsCommon.SetString(txtSOCIAL_RECEIVER, lstSource.Rows(0)("SOCIAL_RECEIVER"))
                InsCommon.SetString(txtSOCIAL_NOTE, lstSource.Rows(0)("SOCIAL_NOTE"))
                InsCommon.SetString(txtHEALTH_NUMBER, lstSource.Rows(0)("HEALTH_NUMBER"))
                InsCommon.SetNumber(ddlHEALTH_STATUS, lstSource.Rows(0)("HEALTH_STATUS"))
                InsCommon.SetDate(txtHEALTH_EFFECT_FROM_DATE, lstSource.Rows(0)("HEALTH_EFFECT_FROM_DATE"))
                InsCommon.SetDate(txtHEALTH_EFFECT_TO_DATE, lstSource.Rows(0)("HEALTH_EFFECT_TO_DATE"))
                InsCommon.SetNumber(ddlHEALTH_AREA_INS_ID, lstSource.Rows(0)("HEALTH_AREA_INS_ID"))
                InsCommon.SetDate(txtHEALTH_RECEIVE_DATE, lstSource.Rows(0)("HEALTH_RECEIVE_DATE"))
                InsCommon.SetDate(txtHEALTH_RETURN_DATE, lstSource.Rows(0)("HEALTH_RETURN_DATE"))
                InsCommon.SetDate(txtUNEMP_FROM_MONTH, lstSource.Rows(0)("UNEMP_FROM_MONTH"))
                InsCommon.SetDate(txtUNEMP_TO_MONTH, lstSource.Rows(0)("UNEMP_TO_MONTH"))
                InsCommon.SetString(txtSOCIAL_SUBMIT, lstSource.Rows(0)("SOCIAL_SUBMIT"))
                InsCommon.SetDate(txtSI_FROM_MONTH, lstSource.Rows(0)("SI_FROM_MONTH"))
                InsCommon.SetDate(txtSI_TO_MONTH, lstSource.Rows(0)("SI_TO_MONTH"))
                InsCommon.SetDate(txtHI_FROM_MONTH, lstSource.Rows(0)("HI_FROM_MONTH"))
                InsCommon.SetDate(txtHI_TO_MONTH, lstSource.Rows(0)("HI_TO_MONTH"))
                InsCommon.SetNumber(chkBHTNLD_BNN, lstSource.Rows(0)("BHTNLD_BNN"))
                InsCommon.SetDate(txtBHTNLD_BNN_FROM_MONTH, lstSource.Rows(0)("BHTNLD_BNN_FROM_MONTH"))
                InsCommon.SetDate(txtBHTNLD_BNN_TO_MONTH, lstSource.Rows(0)("BHTNLD_BNN_TO_MONTH"))
                InsCommon.SetNumber(chkIS_HI_FIVE_YEAR, lstSource.Rows(0)("IS_HI_FIVE_YEAR"))

                InsCommon.SetNumber(rtxtSeniortyMonth, lstSource.Rows(0)("SENIORITY_MONTH"))
                InsCommon.SetNumber(rtxtSeniortyMonthCT, lstSource.Rows(0)("SENIORITY_MONTH_CP"))
                InsCommon.SetNumber(rtxtSeniortyYear, lstSource.Rows(0)("SENIORITY_YEAR"))
                InsCommon.SetNumber(rtxtSeniortyYearCT, lstSource.Rows(0)("SENIORITY_YEAR_CP"))
                InsCommon.SetString(txtSeniorityTotal, lstSource.Rows(0)("SENIORITY_TOTAL"))
                InsCommon.SetNumber(cboPlaceBHYT, lstSource.Rows(0)("PLACE_BHYT_ID"))

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "FindEmployeeButton & Org"

    Private Sub ctrlFindEmployeePopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of Common.CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New InsuranceRepository
        Try
            Dim a As Object = ctrlFindEmployeePopup.SelectedEmployee
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of Common.CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetEmpInfo(item.EMPLOYEE_ID, 0)
                If (Not (lstSource Is Nothing) AndAlso lstSource.Rows.Count > 0) Then
                    If CheckExist(lstSource.Rows(0)("EMPLOYEE_ID")) = False Then
                        txtEMPLOYEE_ID.Text = lstSource.Rows(0)("EMPLOYEE_ID")
                        InsCommon.SetString(txtFULLNAME, lstSource.Rows(0)("VN_FULLNAME"))
                        InsCommon.SetString(txtDEP, lstSource.Rows(0)("ORG_NAME"))
                        InsCommon.SetDate(txtDoB, lstSource.Rows(0)("BIRTH_DATE"))
                        InsCommon.SetString(txtBirthPlace, lstSource.Rows(0)("PLACE_OF_BIRTH_NAME"))
                        InsCommon.SetString(txtCMND, lstSource.Rows(0)("ID_NO"))
                        InsCommon.SetDate(txtDateIssue, lstSource.Rows(0)("ID_DATE"))
                        InsCommon.SetString(txtPOSITION, lstSource.Rows(0)("POSITION_NAME"))
                        InsCommon.SetString(txtINSORG, lstSource.Rows(0)("INS_ORG_NAME"))
                        InsCommon.SetNumber(txtSALARY, lstSource.Rows(0)("SALARY"))
                        txtEMPID.Text = lstSource.Rows(0)("EMPID")
                        Dim dtData As DataTable = rep.GET_SENIORITY(lstSource.Rows(0)("EMPLOYEE_ID"))
                        rtxtSeniortyYearCT.Value = If(IsDBNull(dtData.Rows(0)("SENIORITY_YEAR_CP")), Nothing, dtData.Rows(0)("SENIORITY_YEAR_CP"))
                        rtxtSeniortyMonthCT.Value = If(IsDBNull(dtData.Rows(0)("SENIORITY_MONTH_CP")), Nothing, dtData.Rows(0)("SENIORITY_MONTH_CP"))
                        txtSeniorityTotal.Text = dtData.Rows(0)("SENIORITY_TOTAL")
                    Else
                        ShowMessage(Translate("Nhân viên này đã tồn tại, vui lòng chọn lại"), NotifyType.Warning)
                    End If

                End If
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
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

    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrgSULPPopup.CancelClicked
        isLoadPopup = 0
    End Sub

    Protected Sub btnOrgSULP_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOrgSULP.Click
        Try
            isLoadPopup = 4
            If Not FindOrgSULP.Controls.Contains(ctrlOrgSULPPopup) Then
                ctrlOrgSULPPopup = Me.Register("ctrlOrgSULPPopup", "Common", "ctrlFindOrgPopup")
                ctrlOrgSULPPopup.LoadAllOrganization = True
                FindOrgSULP.Controls.Add(ctrlOrgSULPPopup)
            End If
            ctrlOrgSULPPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlOrgSULPPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlOrgSULPPopup.OrganizationSelected
        Try
            Dim orgItem = ctrlOrgSULPPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rtxtSeniorty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtxtSeniortyYear.TextChanged, rtxtSeniortyMonth.TextChanged
        Dim year As Integer
        Dim month As Integer

        year = If(rtxtSeniortyYear.Value Is Nothing, 0, rtxtSeniortyYear.Value) + If(rtxtSeniortyYearCT.Value Is Nothing, 0, rtxtSeniortyYearCT.Value)
        month = If(rtxtSeniortyMonth.Value Is Nothing, 0, rtxtSeniortyMonth.Value) + If(rtxtSeniortyMonthCT.Value Is Nothing, 0, rtxtSeniortyMonthCT.Value)

        If year <> 0 And month <> 0 Then
            txtSeniorityTotal.Text = String.Format("{0} năm {1} tháng", year, month)
        End If

        If year <> 0 And month = 0 Then
            txtSeniorityTotal.Text = String.Format("{0} năm", year)
        End If

        If year = 0 And month <> 0 Then
            txtSeniorityTotal.Text = String.Format("{0} tháng", month)
        End If
    End Sub

#End Region

End Class
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Telerik.Web.UI

Public Class ctrlInsTotalSalary
    Inherits Common.CommonView


#Region "Property"

    Public Property popupId As String

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

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Me.MainToolBar = tbarOtherLists
            Me.ctrlMessageBox.Listener = Me
            Me.rgGridData.SetFilter()
            If Not IsPostBack Then
                UpdateControlState(CommonMessage.STATE_NORMAL)
                UpdateToolbarState(CommonMessage.STATE_NORMAL)
                txtID.Text = "0"
                ' Refresh()
                ' UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub

    Public Overrides Sub BindData()
        Try
            GetDataCombo()

            Dim dic As New Dictionary(Of String, Control)
            InitControl()

            dic.Add("ID", txtID)

            Utilities.OnClientRowSelectedChanged(rgGridData, dic)

            txtYear.Text = DateTime.Now.Year
            txtMonth.Text = DateTime.Now.Month
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarOtherLists
            Common.Common.BuildToolbar(Me.MainToolBar,
                                        ToolbarItem.Calculate,
                                        ToolbarItem.Export,
                                        ToolbarItem.Seperator,
                                        ToolbarItem.Create)
            tbarOtherLists.Items(3).Text = Translate("Tổng hợp hàng loạt")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            Dim popup As RadWindow
            popup = CType(Me.Page, AjaxPage).PopupWindow
            popup.Title = "Tổng quỹ lương hàng loạt"
            popupId = popup.ClientID

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
        LoadData(True)
    End Sub

    Private Sub UpdateControlState(ByVal sState As String)
        Try
            Select Case sState
                Case CommonMessage.STATE_NORMAL
                    Utilities.EnabledGridNotPostback(rgGridData, True)
                    divCal.Visible = True
                    divCalBatch.Visible = False
                Case CommonMessage.STATE_NEW
                    divCal.Visible = False
                    divCalBatch.Visible = True
                Case "Nothing"
                    Utilities.EnabledGridNotPostback(rgGridData, True)
            End Select

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub UpdateToolbarState(ByVal sState As String)
        Try
            Select Case sState
                Case CommonMessage.STATE_NORMAL
                    Utilities.EnabledGridNotPostback(rgGridData, True)
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True

                Case CommonMessage.STATE_NEW
                    Utilities.EnabledGridNotPostback(rgGridData, False)
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True


                Case CommonMessage.STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgGridData, False)
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
                    CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True

                Case CommonMessage.STATE_DELETE

                Case "Nothing"
                    Utilities.EnabledGridNotPostback(rgGridData, True)

            End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        'Dim objOrgFunction As New OrganizationDTO
        'Dim gID As Decimal
        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                Case CommonMessage.TOOLBARITEM_EDIT
                    CurrentState = CommonMessage.STATE_EDIT
                    'txtCODE.Focus()
                    'Case CommonMessage.TOOLBARITEM_ACTIVE, CommonMessage.TOOLBARITEM_DEACTIVE

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    'FillDropDownList(cboLevel_ID, ListComboData.LIST_ORG_LEVEL, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboLevel_ID.SelectedValue)
                    'FillDataByTree()
                Case CommonMessage.TOOLBARITEM_SAVE
                    Call SaveData()
                    CurrentState = CommonMessage.STATE_NORMAL

                Case CommonMessage.TOOLBARITEM_DELETE
                    Call DeleteData()
                    CurrentState = CommonMessage.STATE_NORMAL

                Case CommonMessage.TOOLBARTIEM_CALCULATE
                    CurrentState = CommonMessage.STATE_NORMAL
                    If divCal.Visible = False Then
                        ddlPeriod.Text = ""
                        ddlPeriod.SelectedIndex = -1
                        divCal.Visible = True
                        divCalBatch.Visible = False
                    End If
                    If ddlINS_ORG_ID.SelectedValue = "" Then
                        ShowMessage("Bạn phải chọn đơn vị bảo hiểm?", NotifyType.Warning)
                        Exit Sub
                    End If
                    If InsCommon.getString(ddlPeriod.Text) = "" Then
                        ShowMessage("Bạn phải chọn đợt khai báo?", NotifyType.Warning)
                        Exit Sub
                    End If
                    If IsExistDataCal() Then
                        ctrlMessageBox.MessageText = Translate("Đã có dữ liệu, bạn có thực sự muốn tổng hợp lại quỹ lương không?")
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARTIEM_CALCULATE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else
                        CalData()
                    End If
                Case CommonMessage.TOOLBARITEM_RESET
                        If ddlINS_ORG_ID.SelectedValue = "" Or ddlINS_ORG_ID.SelectedValue Is Nothing Then
                            ShowMessage("Vui lòng chọn đơn vị bảo hiểm để kiểm tra!", NotifyType.Warning)
                            Exit Sub
                        End If
                    Dim obj1 As Object = (New InsuranceBusiness.InsuranceBusinessClient).CheckInvalidArising(ddlINS_ORG_ID.SelectedValue)
                        If obj1 Is Nothing Then
                            ShowMessage("Quá trình kiểm tra đã xảy ra lỗi!", NotifyType.Warning)
                            Exit Sub
                        Else
                            Dim rs As String = obj1(0).ToString()
                            ShowMessage("Danh sách nhân viên không đúng quy trình: " & rs, NotifyType.Warning, 60)
                            Exit Sub
                        End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Call Export()
            End Select
            UpdateControlState(CurrentState)
            UpdateToolbarState(CurrentState)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgGridData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGridData.NeedDataSource
        Try
            Call LoadData()
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Function RepareDataForAction() As List(Of Decimal)
        Dim lst As New List(Of Decimal)
        For Each dr As Telerik.Web.UI.GridDataItem In rgGridData.SelectedItems
            lst.Add(dr.GetDataKeyValue("ID"))
        Next
        Return lst
    End Function

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARTIEM_CALCULATE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Call CalData()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_CREATE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Call CalDataBatch()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub btnCalBatch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalBatch.Click
        If ddlINS_ORG_ID.SelectedValue = "" Then
            ShowMessage("Bạn phải chọn đơn vị bảo hiểm?", NotifyType.Warning)
            Exit Sub
        End If
        ctrlMessageBox.MessageText = Translate("Vui lòng xác nhận để tổng quỹ lương hàng loạt !")
        ctrlMessageBox.MessageTitle = Translate("Tổng quỹ lương hàng loạt")
        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_CREATE
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub

#End Region

#Region "Function & Sub"

    Private Sub ResetForm()
        Try
            txtID.Text = "0"
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SaveData()
        Try
            
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DeleteData()
        Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetDataCombo()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            Dim lstSource As DataTable = rep.GetInsListInsuranceByUsername(Common.Common.GetUsername(), False)
            FillRadCombobox(ddlINS_ORG_ID, lstSource, "NAME", "ID", True)
            If ddlINS_ORG_ID.Items.Count > 0 Then
                ddlINS_ORG_ID.SelectedValue = lstSource.Rows(0)("ID")
                LoadComboboxPeriod()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function IsExistDataCal() As Boolean
        Try
            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsTotalSalary(Common.Common.GetUserName(), InsCommon.getNumber(txtYear.Text) _
                                              , InsCommon.getNumber(txtMonth.Text) _
                                              , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue) _
                                               , InsCommon.getString(ddlPeriod.SelectedValue) _
                                              )
            If (lstSource Is Nothing) OrElse (lstSource IsNot Nothing AndAlso lstSource.Rows.Count > 0) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return True
        End Try

    End Function

    Private Sub LoadData(Optional ByVal isBind As Boolean = False)
        Try
            If txtYear.Text.Equals("") Then
                txtYear.Text = Now.Year
                txtMonth.Text = Now.Month
            End If
            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsTotalSalary(Common.Common.GetUsername(), InsCommon.getNumber(txtYear.Text) _
                                        , InsCommon.getNumber(txtMonth.Text) _
                                        , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue) _
                                         , InsCommon.getString(ddlPeriod.SelectedValue) _
                                        )
            rgGridData.DataSource = lstSource
            If isBind Then
                rgGridData.DataBind()
            End If

            Dim lstDataPre As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsTotalSalary_Summary(Common.Common.GetUsername(), InsCommon.getNumber(txtYear.Text) _
                                        , InsCommon.getNumber(txtMonth.Text) _
                                        , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue) _
                                         , InsCommon.getString(ddlPeriod.SelectedValue) _
                                         , "1"
                                        )
            Dim lstDataCur As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsTotalSalary_Summary(Common.Common.GetUsername(), InsCommon.getNumber(txtYear.Text) _
                                      , InsCommon.getNumber(txtMonth.Text) _
                                      , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue) _
                                      , InsCommon.getString(ddlPeriod.SelectedValue) _
                                       , "0"
                                      )
            If Not (lstDataCur Is Nothing) AndAlso lstDataCur.Rows.Count > 0 Then
                '<%# Translate("1. Số lao động")%>
                InsCommon.SetNumber(txtHC_1_SI_T, lstDataCur.Rows(0)("HC_1_SI_T"))
                InsCommon.SetNumber(txtHC_1_SI_G, lstDataCur.Rows(0)("HC_1_SI_G"))
                InsCommon.SetNumber(txtHC_1_HI_T, lstDataCur.Rows(0)("HC_1_HI_T"))
                InsCommon.SetNumber(txtHC_1_HI_G, lstDataCur.Rows(0)("HC_1_HI_G"))
                InsCommon.SetNumber(txtHC_1_UI_T, lstDataCur.Rows(0)("HC_1_UI_T"))
                InsCommon.SetNumber(txtHC_1_UI_G, lstDataCur.Rows(0)("HC_1_UI_G"))
                ' THEM MOI
                InsCommon.SetNumber(txtHC_1_BHTNLD_T, lstDataCur.Rows(0)("HC_1_TNLD_T"))
                InsCommon.SetNumber(txtHC_1_BHTNLD_G, lstDataCur.Rows(0)("HC_1_TNLD_G"))


                '<%# Translate("2. Tổng quỹ lương")%>
                InsCommon.SetNumber(txtTotalSal_1_SI_T, lstDataCur.Rows(0)("TotalSal_1_SI_T"))
                InsCommon.SetNumber(txtTotalSal_1_SI_G, lstDataCur.Rows(0)("TotalSal_1_SI_G"))
                InsCommon.SetNumber(txtTotalSal_1_HI_T, lstDataCur.Rows(0)("TotalSal_1_HI_T"))
                InsCommon.SetNumber(txtTotalSal_1_HI_G, lstDataCur.Rows(0)("TotalSal_1_HI_G"))
                InsCommon.SetNumber(txtTotalSal_1_UI_T, lstDataCur.Rows(0)("TotalSal_1_UI_T"))
                InsCommon.SetNumber(txtTotalSal_1_UI_G, lstDataCur.Rows(0)("TotalSal_1_UI_G"))
                ' THEM MOI
                InsCommon.SetNumber(txtTotalSal_1_BHTNLD_T, lstDataCur.Rows(0)("TOTALSAL_1_TNLD_T"))
                InsCommon.SetNumber(txtTotalSal_1_BHTNLD_G, lstDataCur.Rows(0)("TOTALSAL_1_TNLD_G"))

                '<%# Translate("3. Số phải đóng")%>
                InsCommon.SetNumber(txtSumit_1_SI_T, lstDataCur.Rows(0)("Sumit_1_SI_T"))
                InsCommon.SetNumber(txtSumit_1_SI_G, lstDataCur.Rows(0)("Sumit_1_SI_G"))
                InsCommon.SetNumber(txtSumit_1_HI_T, lstDataCur.Rows(0)("Sumit_1_HI_T"))
                InsCommon.SetNumber(txtSumit_1_HI_G, lstDataCur.Rows(0)("Sumit_1_HI_G"))
                InsCommon.SetNumber(txtSumit_1_UI_T, lstDataCur.Rows(0)("Sumit_1_UI_T"))
                InsCommon.SetNumber(txtSumit_1_UI_G, lstDataCur.Rows(0)("Sumit_1_UI_G"))
                ' THEM MOI
                InsCommon.SetNumber(txtSumit_1_BHTNLD_T, lstDataCur.Rows(0)("SUMIT_1_TNLD_T"))
                InsCommon.SetNumber(txtSumit_1_BHTNLD_G, lstDataCur.Rows(0)("SUMIT_1_TNLD_G"))

                '<%# Translate("4. Điều chỉnh")%>
                InsCommon.SetNumber(txtAdjust_1_SI_T, lstDataCur.Rows(0)("Adjust_1_SI_T"))
                InsCommon.SetNumber(txtAdjust_1_SI_G, lstDataCur.Rows(0)("Adjust_1_SI_G"))
                InsCommon.SetNumber(txtAdjust_1_HI_T, lstDataCur.Rows(0)("Adjust_1_HI_T"))
                InsCommon.SetNumber(txtAdjust_1_HI_G, lstDataCur.Rows(0)("Adjust_1_HI_G"))
                InsCommon.SetNumber(txtAdjust_1_UI_T, lstDataCur.Rows(0)("Adjust_1_UI_T"))
                InsCommon.SetNumber(txtAdjust_1_UI_G, lstDataCur.Rows(0)("Adjust_1_UI_G"))
                'THEM MOI
                InsCommon.SetNumber(txtAdjust_1_BHTNLD_T, lstDataCur.Rows(0)("ADJUST_1_TNLD_T"))
                InsCommon.SetNumber(txtAdjust_1_BHTNLD_G, lstDataCur.Rows(0)("ADJUST_1_TNLD_G"))
            End If


            If Not (lstDataPre Is Nothing) AndAlso lstDataPre.Rows.Count > 0 Then
                '<%# Translate("1. Số lao động")%>
                InsCommon.SetNumber(txtHC_2_SI_T, lstDataPre.Rows(0)("HC_1_SI_T"))
                InsCommon.SetNumber(txtHC_2_SI_G, lstDataPre.Rows(0)("HC_1_SI_G"))
                InsCommon.SetNumber(txtHC_2_HI_T, lstDataPre.Rows(0)("HC_1_HI_T"))
                InsCommon.SetNumber(txtHC_2_HI_G, lstDataPre.Rows(0)("HC_1_HI_G"))
                InsCommon.SetNumber(txtHC_2_UI_T, lstDataPre.Rows(0)("HC_1_UI_T"))
                InsCommon.SetNumber(txtHC_2_UI_G, lstDataPre.Rows(0)("HC_1_UI_G"))
                ' THEM MOI
                InsCommon.SetNumber(txtHC_2_BHTNLD_T, lstDataPre.Rows(0)("HC_1_TNLD_T"))
                InsCommon.SetNumber(txtHC_2_BHTNLD_G, lstDataPre.Rows(0)("HC_1_TNLD_G"))

                '<%# Translate("2. Tổng quỹ lương")%>
                InsCommon.SetNumber(txtTotalSal_2_SI_T, lstDataPre.Rows(0)("TotalSal_1_SI_T"))
                InsCommon.SetNumber(txtTotalSal_2_SI_G, lstDataPre.Rows(0)("TotalSal_1_SI_G"))
                InsCommon.SetNumber(txtTotalSal_2_HI_T, lstDataPre.Rows(0)("TotalSal_1_HI_T"))
                InsCommon.SetNumber(txtTotalSal_2_HI_G, lstDataPre.Rows(0)("TotalSal_1_HI_G"))
                InsCommon.SetNumber(txtTotalSal_2_UI_T, lstDataPre.Rows(0)("TotalSal_1_UI_T"))
                InsCommon.SetNumber(txtTotalSal_2_UI_G, lstDataPre.Rows(0)("TotalSal_1_UI_G"))
                ' THEM MOI
                InsCommon.SetNumber(txtTotalSal_2_BHTNLD_T, lstDataPre.Rows(0)("TOTALSAL_1_TNLD_T"))
                InsCommon.SetNumber(txtTotalSal_2_BHTNLD_G, lstDataPre.Rows(0)("TOTALSAL_1_TNLD_G"))

                '<%# Translate("3. Số phải đóng")%>
                InsCommon.SetNumber(txtSumit_2_SI_T, lstDataPre.Rows(0)("Sumit_1_SI_T"))
                InsCommon.SetNumber(txtSumit_2_SI_G, lstDataPre.Rows(0)("Sumit_1_SI_G"))
                InsCommon.SetNumber(txtSumit_2_HI_T, lstDataPre.Rows(0)("Sumit_1_HI_T"))
                InsCommon.SetNumber(txtSumit_2_HI_G, lstDataPre.Rows(0)("Sumit_1_HI_G"))
                InsCommon.SetNumber(txtSumit_2_UI_T, lstDataPre.Rows(0)("Sumit_1_UI_T"))
                InsCommon.SetNumber(txtSumit_2_UI_G, lstDataPre.Rows(0)("Sumit_1_UI_G"))
                ' THEM MOI
                InsCommon.SetNumber(txtSumit_2_BHTNLD_T, lstDataPre.Rows(0)("SUMIT_1_TNLD_T"))
                InsCommon.SetNumber(txtSumit_2_BHTNLD_G, lstDataPre.Rows(0)("SUMIT_1_TNLD_G"))
                '<%# Translate("4. Điều chỉnh")%>
                'txtAdjust_2_SI_T.Text = lstDataPre.Rows(0)("Adjust_1_SI_T")
                'txtAdjust_2_SI_G.Text = lstDataPre.Rows(0)("Adjust_1_SI_G")
                'txtAdjust_2_HI_T.Text = lstDataPre.Rows(0)("Adjust_1_HI_T")
                'txtAdjust_2_HI_G.Text = lstDataPre.Rows(0)("Adjust_1_HI_G")
                'txtAdjust_2_UI_T.Text = lstDataPre.Rows(0)("Adjust_1_UI_T")
                'txtAdjust_2_UI_G.Text = lstDataPre.Rows(0)("Adjust_1_UI_G")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadComboboxPeriod()
        Try
            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsTotalSalaryPeriod(Common.Common.GetUsername(), If(txtYear.Text = "", Now.Year, InsCommon.getNumber(txtYear.Text)) _
                                        , If(txtMonth.Text = "", Now.Month, InsCommon.getNumber(txtMonth.Text)) _
                                        , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue) _
                                         , InsCommon.getString(ddlPeriod.SelectedValue) _
                                        )
            FillRadCombobox(ddlPeriod, lstSource, "PERIOD_NM", "PERIOD_VAL", True)
            If ddlPeriod.Items.Count > 0 Then
                ddlPeriod.SelectedValue = lstSource.Rows(0)("PERIOD_VAL")
            End If
            LoadData(True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlINS_ORG_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlINS_ORG_ID.SelectedIndexChanged
        Call LoadComboboxPeriod()
        'Call LoadData()
    End Sub

    Private Sub txtMonth_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMonth.TextChanged
        ddlPeriod.Text = ""
        Call LoadComboboxPeriod()
        'Call LoadData()
    End Sub

    Private Sub txtYear_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtYear.TextChanged
        Call LoadComboboxPeriod()
        'Call LoadData()
    End Sub

    Private Sub ddlPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlPeriod.SelectedIndexChanged
        Call LoadData(True)
    End Sub
    '----------------------------------------------------------------------
    Private Sub btnCAL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCAL.Click
        Try
            If ddlINS_ORG_ID.SelectedValue = "" Then
                ShowMessage("Bạn phải chọn đơn vị bảo hiểm?", NotifyType.Warning)
                Exit Sub
            End If
            If InsCommon.getString(ddlINS_ORG_ID.SelectedValue) = "" Then
                ShowMessage("Bạn phải chọn đợt khai báo?", NotifyType.Warning)
                Exit Sub
            End If
            Call CalData()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnLock_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLock.Click
        If ddlINS_ORG_ID.SelectedValue = "" Then
            ShowMessage("Bạn phải chọn đơn vị bảo hiểm?", NotifyType.Warning)
            Exit Sub
        End If
        If InsCommon.getString(ddlINS_ORG_ID.SelectedValue) = "" Then
            ShowMessage("Bạn phải chọn đợt khai báo?", NotifyType.Warning)
            Exit Sub
        End If
        Call LockData()
    End Sub
    '----------------------------------------------------------------------
    Private Sub CalData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            If rep.CalInsTotalSalary(Common.Common.GetUserName(), InsCommon.getNumber(txtYear.Text) _
                                       , InsCommon.getNumber(txtMonth.Text) _
                                       , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue) _
                                       , InsCommon.getString(ddlPeriod.SelectedValue)
                                       ) Then
                Refresh("")
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub CalDataBatch()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            If rep.CalInsTotalSalaryBatch(Common.Common.GetUserName(), InsCommon.getDate(txtFROMDATE.SelectedDate) _
                                                                     , InsCommon.getDate(txtTODATE.SelectedDate) _
                                                                     , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue)) Then
                CurrentState = CommonMessage.STATE_NORMAL
                UpdateControlState(CurrentState)
                UpdateToolbarState(CurrentState)
                Refresh("")
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LockData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            If rep.LockInsTotalSalary(Common.Common.GetUserName(), InsCommon.getNumber(txtYear.Text) _
                                       , InsCommon.getNumber(txtMonth.Text) _
                                       , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue) _
                                       , InsCommon.getString(ddlPeriod.SelectedValue)
                                       ) Then
                Refresh("")
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Call Export()
    End Sub

    Private Sub Export()
        Try
            Dim _error As String = ""

            Dim dtVariable = New DataTable
            dtVariable.Columns.Add(New DataColumn("FROM_DATE"))
            dtVariable.Columns.Add(New DataColumn("TO_DATE"))
            Dim drVariable = dtVariable.NewRow()
            drVariable("FROM_DATE") = Now.ToString("dd/MM/yyyy")
            drVariable("TO_DATE") = Now.ToString("dd/MM/yyyy")
            dtVariable.Rows.Add(drVariable)

            Using xls As New ExcelCommon

                Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsTotalSalary(Common.Common.GetUserName(), InsCommon.getNumber(txtYear.Text) _
                                    , InsCommon.getNumber(txtMonth.Text) _
                                    , InsCommon.getNumber(ddlINS_ORG_ID.SelectedValue) _
                                     , InsCommon.getString(ddlPeriod.SelectedValue) _
                                    )
                lstSource.TableName = "TABLE"

                Dim bCheck = xls.ExportExcelTemplate(Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/" & Request.Params("group") & "/InsTotalSalary.xlsx"),
                    "TongQuyLuong", lstSource, dtVariable, Me.Response, _error, ExcelCommon.ExportType.Excel)
                If Not bCheck Then
                    Select Case _error
                        Case 1
                            ShowMessage(Translate("Mẫu báo cáo không tồn tại"), NotifyType.Warning)
                        Case 2
                            ShowMessage(Translate("Dữ liệu không tồn tại"), NotifyType.Warning)
                    End Select
                    Exit Sub
                End If
            End Using
        Catch ex As Exception

        End Try
    End Sub

#End Region


End Class
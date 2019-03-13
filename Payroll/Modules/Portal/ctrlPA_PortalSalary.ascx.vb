Imports Framework.UI
Imports Framework.UI.Utilities
Imports Payroll.PayrollBusiness
Imports Common
Imports Telerik.Web.UI

Public Class ctrlPA_PortalSalary
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"
    Public IDSelect As Integer
    Public Property vPAHoldSalary As List(Of PAHoldSalaryDTO)
        Get
            Return ViewState(Me.ID & "_HoldSalary")
        End Get
        Set(ByVal value As List(Of PAHoldSalaryDTO))
            ViewState(Me.ID & "_HoldSalary") = value
        End Set
    End Property
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    Public Property EmployeeID As Decimal
#End Region

#Region "Page"
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()


    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me

            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Public Overrides Sub BindData()
        Try

            Dim rep As New PayrollRepository
            Dim table As New DataTable
            table.Columns.Add("YEAR", GetType(Integer))
            table.Columns.Add("ID", GetType(Integer))
            Dim row As DataRow
            For index = 2015 To Date.Now.Year + 1
                row = table.NewRow
                row("ID") = index
                row("YEAR") = index
                table.Rows.Add(row)
            Next
            FillRadCombobox(cboYear, table, "YEAR", "ID")
            cboYear.SelectedValue = Date.Now.Year

            If cboYear.Text.Length = 4 Then
                rcboPeriod.ClearSelection()
                rcboPeriod.DataSource = rep.GetPeriodbyYear(cboYear.SelectedValue)
                rcboPeriod.DataValueField = "ID"
                rcboPeriod.DataTextField = "PERIOD_NAME"
                rcboPeriod.DataBind()
                rcboPeriod.SelectedIndex = 0
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

#End Region

#Region "Event"


    Private Sub ctrlYear_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim rep As New PayrollRepository
        Try
            If cboYear.Text.Length = 4 Then
                rcboPeriod.ClearSelection()
                rcboPeriod.DataSource = rep.GetPeriodbyYear(cboYear.SelectedValue)
                rcboPeriod.DataValueField = "ID"
                rcboPeriod.DataTextField = "PERIOD_NAME"
                rcboPeriod.DataBind()
                rcboPeriod.SelectedIndex = 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            Dim dtable As DataTable
            Dim dtableCheck As DataTable
            Dim rp As New PayrollRepository

            If rcboPeriod.SelectedValue = "" Then
                ShowMessage(Translate("Chưa chọn kỳ lương ."), NotifyType.Warning)
                Exit Sub
            End If
            'kiem tra ky luong phai dong moi xem duoc

            dtableCheck = rp.CHECK_OPEN_CLOSE(rcboPeriod.SelectedValue, EmployeeID)

            If dtableCheck.Rows(0)("ID").ToString() = "0" Then
                ShowMessage(Translate("Kỳ lương đang mở, không xem được."), NotifyType.Warning)
                Exit Sub
            End If

            dtable = rp.GetPayrollSheetSumSheet(rcboPeriod.SelectedValue, EmployeeID)
            reset()
            If dtable.Rows.Count > 0 Then
                If dtable.Columns.Contains("FULLNAME_VN") AndAlso dtable.Rows(0)("FULLNAME_VN").ToString() IsNot "" Then
                    lblFULLNAME_VN.Text = dtable.Rows(0)("FULLNAME_VN").ToString()
                End If
                If dtable.Columns.Contains("ORG_NAME") AndAlso dtable.Rows(0)("ORG_NAME").ToString() IsNot "" Then
                    lblORG_NAME.Text = dtable.Rows(0)("ORG_NAME").ToString()
                End If
                If dtable.Columns.Contains("BL_3") AndAlso dtable.Rows(0)("BL_3") IsNot Nothing AndAlso dtable.Rows(0)("BL_3").ToString() IsNot "" Then
                    lblBL_3.Text = Utilities.ObjToDecima(dtable.Rows(0)("BL_3")).ToString("#,#.##")
                End If

                If dtable.Columns.Contains("BL_4") AndAlso dtable.Rows(0)("BL_4") IsNot Nothing AndAlso dtable.Rows(0)("BL_4").ToString() IsNot "" Then
                    lblBL_4.Text = Utilities.ObjToDecima(dtable.Rows(0)("BL_4")).ToString("#,#.##")
                End If

                If dtable.Columns.Contains("BL_5") AndAlso dtable.Rows(0)("BL_5") IsNot Nothing AndAlso dtable.Rows(0)("BL_5").ToString() IsNot "" Then
                    lblBL_5.Text = Utilities.ObjToDecima(dtable.Rows(0)("BL_5")).ToString("#,#.##")
                End If

                If dtable.Columns.Contains("ADD1") AndAlso dtable.Rows(0)("ADD1") IsNot Nothing AndAlso dtable.Rows(0)("ADD1").ToString() IsNot "" Then
                    lblADD1.Text = Utilities.ObjToDecima(dtable.Rows(0)("ADD1")).ToString("#,#.##")
                End If

                If dtable.Columns.Contains("BL_2") AndAlso dtable.Rows(0)("BL_2") IsNot Nothing AndAlso dtable.Rows(0)("BL_2").ToString() IsNot "" Then
                    lblBL_2.Text = Utilities.ObjToDecima(dtable.Rows(0)("BL_2")).ToString("#,#.##")
                End If

                If dtable.Columns.Contains("BL_14") AndAlso dtable.Rows(0)("BL_14") IsNot Nothing AndAlso dtable.Rows(0)("BL_14").ToString() IsNot "" Then
                    lblBL_14.Text = Utilities.ObjToDecima(dtable.Rows(0)("BL_14")).ToString("#,#.##")
                End If

                If dtable.Columns.Contains("BL_17") AndAlso dtable.Rows(0)("BL_17") IsNot Nothing AndAlso dtable.Rows(0)("BL_17").ToString() IsNot "" Then
                    lblBL_17.Text = Utilities.ObjToDecima(dtable.Rows(0)("BL_17")).ToString("#,#.##")
                End If

                If dtable.Columns.Contains("BL_15 ") AndAlso dtable.Rows(0)("BL_15") IsNot Nothing AndAlso dtable.Rows(0)("BL_15").ToString() IsNot "" Then
                    lblBL_15_BL_16.Text = Utilities.ObjToDecima(dtable.Rows(0)("BL_15")).ToString("#,#.##")
                End If

                If dtable.Columns.Contains("BL_16") AndAlso dtable.Rows(0)("BL_16") IsNot Nothing AndAlso dtable.Rows(0)("BL_16").ToString() IsNot "" Then
                    lblBL_15_BL_16.Text += Utilities.ObjToDecima(dtable.Rows(0)("BL_16")).ToString("#,#.##")
                End If

                If dtable.Columns.Contains("BL_18") AndAlso dtable.Rows(0)("BL_18") IsNot Nothing AndAlso dtable.Rows(0)("BL_18").ToString() IsNot "" Then
                    lblBL_18.Text = Utilities.ObjToDecima(dtable.Rows(0)("BL_18")).ToString("#,#.##")
                End If

                If dtable.Columns.Contains("BL_19") AndAlso dtable.Rows(0)("BL_19") IsNot Nothing AndAlso dtable.Rows(0)("BL_19").ToString() IsNot "" Then
                    lblBL_19.Text = Utilities.ObjToDecima(dtable.Rows(0)("BL_19")).ToString("#,#.##")
                End If

                If dtable.Columns.Contains("BL_20") AndAlso dtable.Rows(0)("BL_20") IsNot Nothing AndAlso dtable.Rows(0)("BL_20").ToString() IsNot "" Then
                    lblBL_20.Text = Utilities.ObjToDecima(dtable.Rows(0)("BL_20")).ToString("#,#.##")
                End If

                If dtable.Columns.Contains("TT_BHXH_NV") AndAlso dtable.Rows(0)("TT_BHXH_NV") IsNot Nothing AndAlso dtable.Rows(0)("TT_BHXH_NV").ToString() IsNot "" Then
                    lblTT_BHXH_NV.Text = Utilities.ObjToDecima(dtable.Rows(0)("TT_BHXH_NV")).ToString("#,#.##")
                End If

                If dtable.Columns.Contains("TT_BHYT_NV") AndAlso dtable.Rows(0)("TT_BHYT_NV") IsNot Nothing AndAlso dtable.Rows(0)("TT_BHYT_NV").ToString() IsNot "" Then
                    lblTT_BHYT_NV.Text = Utilities.ObjToDecima(dtable.Rows(0)("TT_BHYT_NV")).ToString("#,#.##")
                End If

                If dtable.Columns.Contains("TT_BHTN_NV") AndAlso dtable.Rows(0)("TT_BHTN_NV") IsNot Nothing AndAlso dtable.Rows(0)("TT_BHTN_NV").ToString() IsNot "" Then
                    lblTT_BHTN_NV.Text = Utilities.ObjToDecima(dtable.Rows(0)("TT_BHTN_NV")).ToString("#,#.##")
                End If

                If dtable.Columns.Contains("BL_25") AndAlso dtable.Rows(0)("BL_25") IsNot Nothing AndAlso dtable.Rows(0)("BL_25").ToString() IsNot "" Then
                    lblBL_25.Text = Utilities.ObjToDecima(dtable.Rows(0)("BL_25")).ToString("#,#.##")
                End If

                If dtable.Columns.Contains("BL_27") AndAlso dtable.Rows(0)("BL_27") IsNot Nothing AndAlso dtable.Rows(0)("BL_27").ToString() IsNot "" Then
                    lblBL_27.Text = Utilities.ObjToDecima(dtable.Rows(0)("BL_27")).ToString("#,#.##")
                End If

                If dtable.Columns.Contains("BL_29") AndAlso dtable.Rows(0)("BL_29") IsNot Nothing AndAlso dtable.Rows(0)("BL_29").ToString() IsNot "" Then
                    lblBL_29.Text = Utilities.ObjToDecima(dtable.Rows(0)("BL_29")).ToString("#,#.##")
                End If

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        'If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
        '    'CurrentState = CommonMessage.STATE_DELETE
        '    'UpdateControlState()
        'End If
    End Sub


#End Region

#Region "Custom"
    Private Sub reset()
        lblBL_3.Text = String.Empty
        lblBL_4.Text = String.Empty
        lblORG_NAME.Text = String.Empty
        lblFULLNAME_VN.Text = String.Empty
        lblBL_5.Text = String.Empty
        lblBL_2.Text = String.Empty
        lblADD1.Text = String.Empty
        lblBL_14.Text = String.Empty
        lblBL_17.Text = String.Empty
        lblBL_15_BL_16.Text = String.Empty
        lblBL_18.Text = String.Empty
        lblBL_19.Text = String.Empty
        lblBL_19.Text = String.Empty
        lblBL_20.Text = String.Empty
        lblTT_BHXH_NV.Text = String.Empty
        lblTT_BHYT_NV.Text = String.Empty
        lblTT_BHTN_NV.Text = String.Empty
        lblBL_25.Text = String.Empty
        lblBL_27.Text = String.Empty
        lblBL_29.Text = String.Empty

    End Sub
#End Region

    Private Function lblCL2() As Object
        Throw New NotImplementedException
    End Function

End Class
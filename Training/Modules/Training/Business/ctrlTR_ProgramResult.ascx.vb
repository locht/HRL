﻿Imports Framework.UI
Imports Framework.UI.Utilities
Imports Training.TrainingBusiness
Imports Common
Imports Telerik.Web.UI
Imports System.IO

Public Class ctrlTR_ProgramResult
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"
    Property Upload4Emp As Decimal
        Get
            Return ViewState(Me.ID & "_Upload4Emp")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_Upload4Emp") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            'Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel,
            '                           ToolbarItem.Import, ToolbarItem.Export, ToolbarItem.Print, ToolbarItem.Next)
            'CType(MainToolBar.Items(1), RadToolBarButton).CausesValidation = True
            'CType(MainToolBar.Items(0), RadToolBarButton).Text = "Cập nhật kết quả"
            'CType(MainToolBar.Items(3), RadToolBarButton).Text = "Import kết quả"
            'CType(MainToolBar.Items(4), RadToolBarButton).Text = "Xuất Template"
            'CType(MainToolBar.Items(5), RadToolBarButton).Text = "In kết quả"
            'CType(MainToolBar.Items(6), RadToolBarButton).Text = "In giấy cam kết nộp chứng chỉ Toiec"
            'CType(MainToolBar.Items(6), RadToolBarButton).ImageUrl = CType(MainToolBar.Items(5), RadToolBarButton).ImageUrl
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(MainToolBar.Items(1), RadToolBarButton).CausesValidation = True
            CType(MainToolBar.Items(0), RadToolBarButton).Text = "Cập nhật kết quả"
            'CType(MainToolBar.Items(6), RadToolBarButton).ImageUrl = CType(MainToolBar.Items(5), RadToolBarButton).ImageUrl
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            hidProgramID.Value = Request.Params("PROGRAM_ID")
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL

                Dim objProgram As ProgramDTO
                Using rep As New TrainingRepository
                    objProgram = rep.GetProgramById(hidProgramID.Value)
                End Using

                If objProgram IsNot Nothing Then
                    txtProgramName.Text = objProgram.NAME
                    txtHinhThuc.Text = objProgram.TRAIN_FORM_NAME
                    rdFromDate.SelectedDate = objProgram.START_DATE
                    rdToDate.SelectedDate = objProgram.END_DATE

                    txtProgramName.ReadOnly = True
                    txtHinhThuc.ReadOnly = True
                    rdFromDate.Enabled = False
                    rdToDate.Enabled = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Overrides Sub UpdateControlState()
        Select Case CurrentState
            Case CommonMessage.STATE_EDIT

            Case CommonMessage.STATE_NORMAL

        End Select
        ChangeToolbarState()
    End Sub

    Public Overrides Sub BindData()
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Dim rep As New TrainingRepository
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EDIT
                    Dim isSchedule As Boolean = False
                    For Each item As GridDataItem In rgData.MasterTableView.Items
                        'If item.GetDataKeyValue("ID") IsNot Nothing Then
                        '    isSchedule = True
                        '    item.Edit = True
                        'End If
                        If item.Selected Then
                            isSchedule = True
                            item.Edit = True
                        End If
                    Next
                    If Not isSchedule Then
                        'ShowMessage(Translate("Ứng viên chưa được lên lịch thi"), NotifyType.Warning)
                        ShowMessage(Translate("Chưa chọn nhân viên để cập nhật kết quả"), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    rgData.MasterTableView.Rebind()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    For Each item As GridDataItem In rgData.MasterTableView.Items
                        item.Edit = False
                    Next
                    rgData.MasterTableView.Rebind()
                Case CommonMessage.TOOLBARITEM_SAVE
                    Dim lst As New List(Of ProgramResultDTO)
                    For Each item As GridDataItem In rgData.EditItems
                        If item.Edit = True Then

                            Dim edit = CType(item, GridEditableItem)
                            Dim rntxtDuration As RadNumericTextBox = CType(edit("DURATION").Controls(0), RadNumericTextBox)
                            Dim cbIsExam As CheckBox = CType(edit("IS_EXAMS").Controls(0), CheckBox)
                            Dim cbIsEnd As CheckBox = CType(edit("IS_END").Controls(0), CheckBox)
                            Dim cbIsReach As CheckBox = CType(edit("IS_REACH").Controls(0), CheckBox)
                            Dim rntxtTOIEC_BENCHMARK As RadNumericTextBox = CType(edit("TOIEC_BENCHMARK").Controls(0), RadNumericTextBox)
                            Dim rntxtTOIEC_SCORE_IN As RadNumericTextBox = CType(edit("TOIEC_SCORE_IN").Controls(0), RadNumericTextBox)
                            Dim rntxtTOIEC_SCORE_OUT As RadNumericTextBox = CType(edit("TOIEC_SCORE_OUT").Controls(0), RadNumericTextBox)
                            Dim rntxtINCREMENT_SCORE As RadNumericTextBox = CType(edit("INCREMENT_SCORE").Controls(0), RadNumericTextBox)
                            Dim cboRank As RadComboBox = CType(edit.FindControl("cboRank"), RadComboBox)
                            Dim rntxtRETEST_SCORE As RadNumericTextBox = CType(edit("RETEST_SCORE").Controls(0), RadNumericTextBox)
                            Dim cboRank2 As RadComboBox = CType(edit.FindControl("cboRank2"), RadComboBox)
                            Dim txtRETEST_REMARK As TextBox = CType(edit("RETEST_REMARK").Controls(0), TextBox)
                            Dim rntxtFINAL_SCORE As RadNumericTextBox = CType(edit("FINAL_SCORE").Controls(0), RadNumericTextBox)

                            Dim cbABSENT_REASON As CheckBox = CType(edit("ABSENT_REASON").Controls(0), CheckBox)
                            Dim cbABSENT_UNREASON As CheckBox = CType(edit("ABSENT_UNREASON").Controls(0), CheckBox)
                            Dim cbIsCer As CheckBox = CType(edit("IS_CERTIFICATE").Controls(0), CheckBox)
                            Dim rdCerDate As RadDatePicker = CType(edit("CERTIFICATE_DATE").Controls(0), RadDatePicker)
                            Dim txtCerNo As TextBox = CType(edit("CERTIFICATE_NO").Controls(0), TextBox)
                            Dim rdCER_RECEIVE_DATE As RadDatePicker = CType(edit("CER_RECEIVE_DATE").Controls(0), RadDatePicker)
                            Dim rdCOMMIT_STARTDATE As RadDatePicker = CType(edit("COMMIT_STARTDATE").Controls(0), RadDatePicker)
                            Dim rdCOMMIT_ENDDATE As RadDatePicker = CType(edit("COMMIT_ENDDATE").Controls(0), RadDatePicker)
                            Dim rntxtCOMMIT_WORKMONTH As RadNumericTextBox = CType(edit("COMMIT_WORKMONTH").Controls(0), RadNumericTextBox)
                            Dim cbIS_REFUND_FEE As CheckBox = CType(edit("IS_REFUND_FEE").Controls(0), CheckBox)
                            Dim cbIS_RESERVES As CheckBox = CType(edit("IS_RESERVES").Controls(0), CheckBox)
                            Dim txtREMARK As TextBox = CType(edit("REMARK").Controls(0), TextBox)
                            Dim txtATTACH_FILE As RadTextBox = CType(edit.FindControl("_FileName"), RadTextBox)

                            Dim obj As New ProgramResultDTO
                            With obj
                                .ID = item.GetDataKeyValue("ID")
                                .TR_PROGRAM_ID = hidProgramID.Value
                                .EMPLOYEE_ID = item.GetDataKeyValue("EMPLOYEE_ID")
                                .DURATION = rntxtDuration.Value
                                .IS_EXAMS = cbIsExam.Checked
                                .IS_END = cbIsEnd.Checked
                                .IS_REACH = cbIsReach.Checked
                                .TOIEC_BENCHMARK = rntxtTOIEC_BENCHMARK.Value
                                .TOIEC_SCORE_IN = rntxtTOIEC_SCORE_IN.Value
                                .TOIEC_SCORE_OUT = rntxtTOIEC_SCORE_OUT.Value
                                .INCREMENT_SCORE = rntxtINCREMENT_SCORE.Value
                                If cboRank.SelectedValue <> "" Then
                                    .TR_RANK_ID = cboRank.SelectedValue
                                End If
                                .RETEST_SCORE = rntxtRETEST_SCORE.Value
                                If cboRank2.SelectedValue <> "" Then
                                    .RETEST_RANK_ID = cboRank2.SelectedValue
                                End If
                                .RETEST_REMARK = txtRETEST_REMARK.Text
                                .FINAL_SCORE = rntxtFINAL_SCORE.Value
                                .ABSENT_REASON = cbABSENT_REASON.Checked
                                .ABSENT_UNREASON = cbABSENT_UNREASON.Checked
                                .IS_CERTIFICATE = cbIsCer.Checked
                                .CERTIFICATE_DATE = rdCerDate.SelectedDate
                                .CERTIFICATE_NO = txtCerNo.Text
                                .CER_RECEIVE_DATE = rdCER_RECEIVE_DATE.SelectedDate
                                .COMMIT_STARTDATE = rdCOMMIT_STARTDATE.SelectedDate
                                .COMMIT_ENDDATE = rdCOMMIT_ENDDATE.SelectedDate
                                .COMMIT_WORKMONTH = rntxtCOMMIT_WORKMONTH.Value
                                .IS_REFUND_FEE = cbIS_REFUND_FEE.Checked
                                .IS_RESERVES = cbIS_RESERVES.Checked
                                .REMARK = txtREMARK.Text
                                .ATTACH_FILE = txtATTACH_FILE.Text
                            End With
                            lst.Add(obj)
                        End If
                    Next

                    If rep.UpdateProgramResult(lst) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        For Each item As GridDataItem In rgData.MasterTableView.Items
                            item.Edit = False
                        Next
                        rgData.Rebind()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    End If
            End Select
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                'Kiểm tra các điều kiện trước khi xóa
                Dim lstCanID As New List(Of Decimal)
                For Each dr As Telerik.Web.UI.GridDataItem In rgData.SelectedItems
                    lstCanID.Add(dr.GetDataKeyValue("CANDIDATE_ID"))
                Next
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        CreateDataFilterStudent()
    End Sub
    Private Sub CreateDataFilterStudent()
        Try
            Dim rep As New TrainingRepository
            Dim _filter As New ProgramResultDTO
            Dim MaximumRows As Integer
            _filter.TR_PROGRAM_ID = Decimal.Parse(hidProgramID.Value)
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of ProgramResultDTO)
            If Sorts IsNot Nothing Then
                lstData = rep.GetProgramResult(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, Sorts)
            Else
                lstData = rep.GetProgramResult(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows)
            End If
            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = lstData
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub rgData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        'If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
        '    Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
        '    ''datarow("COM_NAME").ToolTip = Utilities.DrawTreeByString(datarow("COM_DESC").Text)
        '    'datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow("ORG_DESC").Text)
        '    If datarow("ATTACH_FILE").Text <> "" Then
        '        Dim linkCell As GridTableCell = datarow("ATTACH_FILE")
        '        Dim link As HyperLink = CType(linkCell.FindControl("AttachFile"), HyperLink)
        '        link.Text = datarow("ATTACH_FILE").Text
        '        link.NavigateUrl = "/ReportTemplates/Training/Upload/Result/" & datarow("ATTACH_FILE").Text
        '    End If
        'End If

        If e.Item.Edit Then
            Dim edit = CType(e.Item, GridEditableItem)
            Dim rntxt As RadNumericTextBox
            Dim txt As TextBox
            Dim cbo As RadComboBox
            Dim cbo2 As RadComboBox
            Dim rd As RadDatePicker

            rntxt = CType(edit("DURATION").Controls(0), RadNumericTextBox)
            rntxt.Width = Unit.Percentage(100)
            rntxt.MinValue = 0
            txt = CType(edit("CERTIFICATE_NO").Controls(0), TextBox)
            txt.Width = Unit.Percentage(100)
            txt = CType(edit("REMARK").Controls(0), TextBox)
            txt.Width = Unit.Percentage(100)
            txt = CType(edit("RETEST_REMARK").Controls(0), TextBox)
            txt.Width = Unit.Percentage(100)
            rntxt = CType(edit("TOIEC_BENCHMARK").Controls(0), RadNumericTextBox)
            rntxt.Width = Unit.Percentage(100)
            rntxt = CType(edit("TOIEC_SCORE_IN").Controls(0), RadNumericTextBox)
            rntxt.Width = Unit.Percentage(100)
            rntxt = CType(edit("TOIEC_SCORE_OUT").Controls(0), RadNumericTextBox)
            rntxt.Width = Unit.Percentage(100)
            rntxt = CType(edit("INCREMENT_SCORE").Controls(0), RadNumericTextBox)
            rntxt.Width = Unit.Percentage(100)
            rntxt = CType(edit("RETEST_SCORE").Controls(0), RadNumericTextBox)
            rntxt.Width = Unit.Percentage(100)
            rntxt = CType(edit("FINAL_SCORE").Controls(0), RadNumericTextBox)
            rntxt.Width = Unit.Percentage(100)
            rntxt = CType(edit("COMMIT_WORKMONTH").Controls(0), RadNumericTextBox)
            rntxt.Width = Unit.Percentage(100)
			rd = CType(edit("CERTIFICATE_DATE").Controls(0), RadDatePicker)
            rd.Width = Unit.Percentage(100)
            cbo = CType(edit.FindControl("cboRank"), RadComboBox)
            cbo2 = CType(edit.FindControl("cboRank2"), RadComboBox)

            If cbo IsNot Nothing Then
                Dim dtData As DataTable
                Using rep As New TrainingRepository
                    dtData = rep.GetOtherList("TR_RANK", True)
                    FillRadCombobox(cbo, dtData, "NAME", "ID")
                    If edit.GetDataKeyValue("TR_RANK_ID") IsNot Nothing Then
                        cbo.SelectedValue = edit.GetDataKeyValue("TR_RANK_ID")
                    End If
                End Using
            End If

            If cbo2 IsNot Nothing Then
                Dim dtData As DataTable
                Using rep As New TrainingRepository
                    dtData = rep.GetOtherList("TR_RANK", True)
                    FillRadCombobox(cbo2, dtData, "NAME", "ID")
                    If edit.GetDataKeyValue("RETEST_RANK_ID") IsNot Nothing Then
                        cbo2.SelectedValue = edit.GetDataKeyValue("RETEST_RANK_ID")
                    End If
                End Using
            End If
        End If
    End Sub
    Private Sub rgData_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgData.ItemCommand
        If e.CommandName = "UploadFile" Then
            Upload4Emp = e.CommandArgument
            ctrlUpload1.Show()
        ElseIf e.CommandName = "DownloadFile" Then
            Dim url As String = "Download.aspx?" & e.CommandArgument
            Dim str As String = "window.open('" & url + "');"
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", Str, True)
        End If
    End Sub
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim fileName As String
        Try
            If ctrlUpload1.UploadedFiles.Count > 1 Then
                ShowMessage("Chỉ được chọn 1 file để upload", NotifyType.Error)
                Exit Sub
            Else
                If ctrlUpload1.UploadedFiles.Count > 0 Then
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(0)
                    fileName = Server.MapPath("~/ReportTemplates/Training/Upload/Result")
                    If Not Directory.Exists(fileName) Then
                        Directory.CreateDirectory(fileName)
                    End If
                    fileName = System.IO.Path.Combine(fileName, file.FileName)
                    file.SaveAs(fileName, True)
                    
                    For Each abc As GridDataItem In rgData.MasterTableView.Items
                        If abc.GetDataKeyValue("EMPLOYEE_ID").ToString = Upload4Emp Then
                            Dim txtATTACH_FILE As RadTextBox = DirectCast(abc("ATTACH_FILE").FindControl("_FileName"), RadTextBox)
                            txtATTACH_FILE.Text = file.FileName
                            Exit For
                        End If
                    Next

                    'Dim txtATTACH_FILE As RadTextBox = DirectCast(Upload4Emp("ATTACH_FILE").FindControl("_FileName"), RadTextBox)
                    'txtATTACH_FILE.Text = file.FileName

                    'For Each item As GridDataItem In rgData.Items
                    '    If item("EMPLOYEE_ID").Text = UploadedEMP Then
                    '        Dim edit = CType(item, GridEditableItem)
                    '        Dim txtATTACH_FILE As RadNumericTextBox = CType(edit("ATTACH_FILE").Controls(1), RadNumericTextBox)
                    '        txtATTACH_FILE.Text = file.FileName
                    '    End If
                    'Next
                Else
                    ShowMessage(Translate("Chưa upload được file"), NotifyType.Error)
                End If

            End If
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi"), NotifyType.Error)
        End Try
    End Sub

#End Region

#Region "Custom"
#End Region

End Class
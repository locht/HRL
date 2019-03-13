Imports Framework.UI
Imports Framework.UI.Utilities
Imports Training.TrainingBusiness
Imports Common
Imports Telerik.Web.UI
Imports System.IO

Public Class ctrlTR_AssessmentResult
    Inherits CommonView

    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"

    Public Property SelectedItemCriGroupFormNotForm As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemCriGroupFormNotForm") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemCriGroupFormNotForm") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemCriGroupFormNotForm")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemCriGroupFormNotForm") = value
        End Set
    End Property

    Public Property SelectedItemCriGroupForm As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemCriGroupForm") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemCriGroupForm") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemCriGroupForm")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemCriGroupForm") = value
        End Set
    End Property

    '0 - normal
    '1 - Employee
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            rgData.SetFilter()
            rgData.PageSize = Common.Common.DefaultPageSize
            rgData.AllowCustomPaging = True
            rgData.ClientSettings.EnablePostBackOnRowClick = True
            UpdateControlState()
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try

            rntxtYear.Value = Date.Now.Year
            GetChooseFormProgramByYear()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Export, ToolbarItem.Import)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try

            If Not IsPostBack Then
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Select Case CurrentState
            Case CommonMessage.STATE_NEW
                EnabledGrid(rgData, False, True)
                rntxtYear.ReadOnly = True
                EnableRadCombo(cboCourse, False)

            Case CommonMessage.STATE_NORMAL
                EnabledGrid(rgData, True, True)
                rntxtYear.ReadOnly = False
                EnableRadCombo(cboCourse, True)
            Case CommonMessage.STATE_EDIT

                EnabledGrid(rgData, False, True)
                rntxtYear.ReadOnly = True
                EnableRadCombo(cboCourse, False)

        End Select
        ChangeToolbarState()
    End Sub

#End Region

#Region "Event"

    Private Sub tbarMain_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles tbarMain.ButtonClick
        Dim rep As New TrainingRepository
        Dim _error As Integer = 0
        Dim sType As Object = Nothing
        Try

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EXPORT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn nhân viên cần đánh giá"), NotifyType.Warning)
                        Exit Sub
                    End If
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('TR_ASSESSMENT_RESULT&EMPLOYEE_ID=" & rgData.SelectedValue.ToString & _
                                                        "&TR_CHOOSE_FORM_ID=" & cboCourse.SelectedValue & "');", True)
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn nhân viên cần đánh giá"), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim isSchedule As Boolean = False
                    For Each item As GridDataItem In rgResult.MasterTableView.Items
                        item.Edit = True
                    Next
                    rgResult.MasterTableView.Rebind()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    For Each item As GridDataItem In rgResult.MasterTableView.Items
                        item.Edit = False
                    Next
                    rgResult.MasterTableView.Rebind()
                Case CommonMessage.TOOLBARITEM_SAVE
                    Dim objResult As New AssessmentResultDTO
                    objResult.EMPLOYEE_ID = rgData.SelectedValue
                    objResult.TR_CHOOSE_FORM_ID = cboCourse.SelectedValue
                    Dim lst As New List(Of AssessmentResultDtlDTO)
                    For Each item As GridDataItem In rgResult.Items
                        If item.Edit = True Then
                            Dim obj As New AssessmentResultDtlDTO
                            obj.ID = item.GetDataKeyValue("ID")
                            Dim edit = CType(item, GridEditableItem)
                            Dim rntxtPointAss As RadNumericTextBox = CType(edit("POINT_ASS").Controls(0), RadNumericTextBox)
                            Dim txtRemark As TextBox = CType(edit("REMARK").Controls(0), TextBox)
                            With obj
                                .EMPLOYEE_ID = rgData.SelectedValue
                                .POINT_ASS = rntxtPointAss.Value
                                .REMARK = txtRemark.Text
                                .TR_CRITERIA_GROUP_ID = item.GetDataKeyValue("TR_CRITERIA_GROUP_ID")
                                .TR_CRITERIA_ID = item.GetDataKeyValue("TR_CRITERIA_ID")
                                .TR_CHOOSE_FORM_ID = cboCourse.SelectedValue
                            End With
                            lst.Add(obj)
                        End If
                    Next
                    objResult.lstResult = lst
                    If rep.UpdateAssessmentResult(objResult) Then
                        CurrentState = CommonMessage.STATE_NORMAL
                        For Each item As GridDataItem In rgResult.MasterTableView.Items
                            item.Edit = False
                        Next
                        rgResult.Rebind()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    End If
            End Select
            UpdateControlState()
        Catch ex As Exception
            ShowMessage(ex.Message, Framework.UI.Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub rgData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        CreateDataFilterEmployee()
    End Sub

    Private Sub rgResult_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgResult.NeedDataSource
        CreateDataFilterResult()
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = "INSERT" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_NEW
                UpdateControlState()
            End If
            If e.ActionName = "DELETE" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub cboCourse_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCourse.SelectedIndexChanged
        Try
            If cboCourse.SelectedValue <> "" Then
                Using rep As New TrainingRepository
                    Dim obj = rep.GetProgramByChooseFormId(cboCourse.SelectedValue)
                    If obj IsNot Nothing Then
                        txtAddress.Text = obj.VENUE
                        txtCenters.Text = obj.Centers_NAME
                        txtLectures.Text = obj.Lectures_NAME
                        rdEndDate.SelectedDate = obj.START_DATE
                        rdStartDate.SelectedDate = obj.END_DATE
                    End If
                End Using
            Else
                txtAddress.Text = ""
                txtCenters.Text = ""
                txtLectures.Text = ""
                rdEndDate.SelectedDate = Nothing
                rdStartDate.SelectedDate = Nothing
            End If
            rgResult.Rebind()
            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Private Sub rntxtYear_TextChanged(sender As Object, e As System.EventArgs) Handles rntxtYear.TextChanged
        Try
            GetChooseFormProgramByYear()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgResult_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgResult.ItemDataBound
        If e.Item.Edit Then
            Dim edit = CType(e.Item, GridEditableItem)
            Dim rntxt As RadNumericTextBox
            Dim txt As TextBox
            rntxt = CType(edit("POINT_ASS").Controls(0), RadNumericTextBox)
            rntxt.Width = Unit.Percentage(100)
            rntxt.MinValue = 0
            txt = CType(edit("REMARK").Controls(0), TextBox)
            txt.Width = Unit.Percentage(100)
            txt.MaxLength = 1023
        End If
    End Sub

    Private Sub rgData_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rgData.SelectedIndexChanged
        rgResult.Rebind()
    End Sub


    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Try

            Dim tempPath As String = "Excel"
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)

                '//Instantiate LoadOptions specified by the LoadFormat.
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                If workbook.Worksheets.GetSheetByCodeName("ImportResult") Is Nothing Then
                    ShowMessage("File mẫu không đúng định dạng", NotifyType.Warning)
                    Exit Sub
                End If
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(4, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))

                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            Dim dtData As DataTable
            For Each dt As DataTable In dsDataPrepare.Tables
                If dtData Is Nothing Then
                    dtData = dt.Clone
                End If
                For Each row In dt.Rows
                    dtData.ImportRow(row)
                Next
            Next

            ImportData(dtData)
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub


    Public Sub ImportData(ByVal dtData As DataTable)
        Try
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Exit Sub
            End If
            Dim dtError As DataTable = dtData.Clone
            dtError.TableName = "DATA"
            dtError.Columns.Add("STT", GetType(String))
            Dim iRow As Integer = 8
            Dim IsError As Boolean = False
            Dim objResult As New AssessmentResultDTO
            Dim lstDtl As New List(Of AssessmentResultDtlDTO)
            For Each row In dtData.Rows
                Dim sError As String = ""
                Dim rowError = dtError.NewRow
                Dim isRow = ImportValidate.TrimRow(row)
                Dim isScpExist As Boolean = False
                If Not isRow Then
                    iRow += 1
                    Continue For
                End If
                Dim obj As New AssessmentResultDtlDTO


                objResult.EMPLOYEE_ID = Decimal.Parse(row("EMPLOYEE_ID"))
                objResult.TR_CHOOSE_FORM_ID = Decimal.Parse(row("TR_CHOOSE_FORM_ID"))
                obj.TR_CHOOSE_FORM_ID = Decimal.Parse(row("TR_CHOOSE_FORM_ID"))
                obj.TR_CRITERIA_GROUP_ID = Decimal.Parse(row("TR_CRITERIA_GROUP_ID"))
                obj.TR_CRITERIA_ID = Decimal.Parse(row("TR_CRITERIA_ID"))
                sError = "Chưa nhập Điểm đánh giá"
                ImportValidate.IsValidNumber("POINT_ASS", row, rowError, IsError, sError)
                If Not IsError Then
                    obj.POINT_ASS = Decimal.Parse(row("POINT_ASS"))
                End If
                obj.REMARK = row("REMARK")
                lstDtl.Add(obj)
                If IsError Then
                    rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE")
                    rowError("EMPLOYEE_NAME") = row("EMPLOYEE_NAME")
                    dtError.Rows.Add(rowError)
                End If
            Next
            If dtError.Rows.Count > 0 Then
                Session("EXPORTREPORT") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('TR_ASSESSMENT_RESULT_ERROR')", True)
                ShowMessage(Translate("Giao dịch không thành công, chi tiết lỗi tệp tin đính kèm"), NotifyType.Warning)
            Else
                Dim rep As New TrainingRepository
                objResult.lstResult = lstDtl
                If rep.UpdateAssessmentResult(objResult) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgResult.Rebind()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub


#End Region

#Region "Custom"

    Private Sub CreateDataFilterResult()

        Try
            Dim rep As New TrainingRepository
            Dim _filter As New AssessmentResultDtlDTO
            If cboCourse.SelectedValue <> "" Then
                _filter.TR_CHOOSE_FORM_ID = Decimal.Parse(cboCourse.SelectedValue)
            End If
            If rgData.SelectedItems.Count <> 0 Then
                _filter.EMPLOYEE_ID = rgData.SelectedValues("EMPLOYEE_ID")
            End If
            Dim Sorts As String = rgResult.MasterTableView.SortExpressions.GetSortString()
            Dim MaximumRows As Decimal = 0
            If Sorts IsNot Nothing Then
                rgResult.DataSource = rep.GetAssessmentResultByID(_filter)
            Else

                rgResult.DataSource = rep.GetAssessmentResultByID(_filter)
            End If
            rgData.VirtualItemCount = MaximumRows
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub CreateDataFilterEmployee()

        Try
            Dim rep As New TrainingRepository
            Dim _filter As New AssessmentResultDTO
            SetValueObjectByRadGrid(rgData, _filter)
            If cboCourse.SelectedValue <> "" Then
                _filter.TR_CHOOSE_FORM_ID = Decimal.Parse(cboCourse.SelectedValue)
            End If
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim MaximumRows As Decimal = 0
            If Sorts IsNot Nothing Then
                rgData.DataSource = rep.GetEmployeeAssessmentResult(_filter, rgData.CurrentPageIndex, _
                                                            rgData.PageSize, _
                                                            MaximumRows, _
                                                            Sorts)
            Else
                rgData.DataSource = rep.GetEmployeeAssessmentResult(_filter, rgData.CurrentPageIndex, _
                                                            rgData.PageSize, _
                                                            MaximumRows)
            End If
            rgData.VirtualItemCount = MaximumRows

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub GetChooseFormProgramByYear()
        Try

            If rntxtYear.Value IsNot Nothing Then
                Using rep As New TrainingRepository
                    Dim dtData = rep.GetTrChooseProgramFormByYear(True, rntxtYear.Value)
                    FillRadCombobox(cboCourse, dtData, "NAME", "ID")

                End Using
            Else
                cboCourse.ClearSelection()
                cboCourse.Items.Clear()
            End If
        Catch ex As Exception
            Throw ex

        End Try

    End Sub
#End Region

End Class
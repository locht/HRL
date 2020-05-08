Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Common
Imports Common.CommonBusiness
Imports Telerik.Web.UI
Imports System.IO
Imports Aspose.Cells

Public Class ctrlRC_ProgramInterviewResult
    Inherits Common.CommonView
    Protected WithEvents StageView As ViewBase
    Private store As New RecruitmentStoreProcedure()
    Private userlog As UserLog

#Region "Property"
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("CODE", GetType(String))
                dt.Columns.Add("ID", GetType(String))
                dt.Columns.Add("FullName", GetType(String))
                dt.Columns.Add("NAME", GetType(String))
                dt.Columns.Add("NAME_ID", GetType(String))
                dt.Columns.Add("SCHEDULE_DATE", GetType(String))
                dt.Columns.Add("COMMENT_INFO", GetType(String))
                dt.Columns.Add("STATUS_NAME", GetType(String))
                dt.Columns.Add("STATUS_ID", GetType(String))
                dt.Columns.Add("fullname_vn", GetType(String))
                dt.Columns.Add("EXAMS_ORDER", GetType(String))
                dt.Columns.Add("ID_PSC", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property
    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property
    Property IsRight As Decimal
        Get
            Return ViewState(Me.ID & "_IsRight")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IsRight") = value
        End Set
    End Property

    Public Property tabSource As DataTable
        Get
            Return PageViewState(Me.ID & "_tabSource")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_tabSource") = value
        End Set
    End Property

    Property lstItemDecimals As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_lstDeleteDecimals")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_lstDeleteDecimals") = value
        End Set
    End Property

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

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()

            gridCadidate.SetFilter()
            gridCadidate.AllowCustomPaging = True
            gridCadidate.PageSize = Common.Common.DefaultPageSize

            rgDataInterview.SetFilter()
            rgDataInterview.AllowCustomPaging = True
            rgDataInterview.PageSize = Common.Common.DefaultPageSize
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            gridCadidate.SetFilter()
            gridCadidate.AllowCustomPaging = True
            gridCadidate.PageSize = Common.Common.DefaultPageSize

            rgDataInterview.SetFilter()
            rgDataInterview.AllowCustomPaging = True
            rgDataInterview.PageSize = Common.Common.DefaultPageSize

            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(MainToolBar.Items(1), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False

            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    Utilities.EnabledGrid(gridCadidate, True)
                    Utilities.EnabledGridNotPostback(rgDataInterview, True)
                    txtAssessment.Enabled = False
                    txtComment.Enabled = False
                    cbbStatus.Enabled = False
                    ResetText()
                Case CommonMessage.STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgDataInterview, False)
                    txtAssessment.Enabled = True
                    txtComment.Enabled = True
                    cbbStatus.Enabled = True
            End Select
            ChangeToolbarState()

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub ResetText()
        Try
            lblExamName_Interview.Text = String.Empty
            cbbStatus.SelectedIndex = 0
            txtAssessment.Text = String.Empty
            txtComment.Text = String.Empty
            lblProctor.Text = String.Empty
        Catch ex As Exception

        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Try
            If IsRight = 0 Then
                hdProgramID.Value = Request.Params("PROGRAM_ID")
                CurrentState = CommonMessage.STATE_NORMAL
            End If
            If Not IsPostBack Then
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDataInterview.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgDataInterview.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Using rep As New RecruitmentRepository
            Dim dtData As New DataTable
            dtData = rep.GetOtherList("RC_SUGGEST_INTERN_FORM", True)
            FillRadCombobox(cboSuggestIntern, dtData, "NAME", "CODE")
        End Using
        Dim store As New RecruitmentStoreProcedure
        Try
            Dim dtData As New DataTable
            If IsNumeric(Request.Params("PROGRAM_ID")) Then
                dtData = store.GET_PROGRAM_SCHCEDULE_LIST(Request.Params("PROGRAM_ID"))
            Else
                dtData = store.GET_PROGRAM_SCHCEDULE_LIST(0)
            End If
            FillDropDownList(cboSchedule, dtData, "EXAMS_NAME", "ID")
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("EXAM_NAME", lblExamName_Interview)
            dic.Add("IS_PASS", cbbStatus)
            dic.Add("COMMENT_INFO", txtComment)
            dic.Add("ASSESSMENT_INFO", txtAssessment)
            dic.Add("PV_PERSON", lblProctor)
            Utilities.OnClientRowSelectedChanged(rgDataInterview, dic)
        Catch ex As Exception
            Throw ex
        End Try
        
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As PROGRAM_SCHEDULE_CAN_DTO
        Dim IsSaveCompleted As Boolean
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgDataInterview.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgDataInterview.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgDataInterview.SelectedItems
                        lblExamName_Interview.Text = item.GetDataKeyValue("EXAM_NAME").ToString
                        lblProctor.Text = item.GetDataKeyValue("PV_PERSON").ToString
                        If item.GetDataKeyValue("IS_PASS").ToString <> "" Then
                            cbbStatus.SelectedValue = item.GetDataKeyValue("IS_PASS")
                        End If
                        txtComment.Text = item.GetDataKeyValue("COMMENT_INFO").ToString
                    Next

                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                    IsRight = 1
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If gridCadidate.SelectedItems.Count = 0 Then
                            ShowMessage(Translate("Chưa chọn nhân viên,Xin kiểm tra lại"), NotifyType.Warning)
                            Exit Sub
                        End If
                        obj = New PROGRAM_SCHEDULE_CAN_DTO
                        obj.COMMENT_INFO = txtComment.Text
                        obj.ASSESSMENT_INFO = txtAssessment.Text
                        obj.IS_PASS = cbbStatus.SelectedValue


                        userlog = New UserLog
                        userlog = LogHelper.GetUserLog
                        Select Case CurrentState
                            Case CommonMessage.STATE_EDIT
                                obj.ID = rgDataInterview.SelectedValue
                                IsSaveCompleted = store.UPDATE_CANDIDATE_RESULT(
                                                            obj.ID,
                                                            0,
                                                            obj.COMMENT_INFO,
                                                            obj.ASSESSMENT_INFO,
                                                            obj.IS_PASS)
                        End Select

                        Dim dataItem = TryCast(gridCadidate.SelectedItems(0), GridDataItem)


                        'If obj.ISPASS = 1 And dataItem("STATUS").Text = "Đủ điều kiện" Then
                        '    store.UPDATE_CANDIDATE_STATUS(Int32.Parse(dataItem("ID").Text), "DAT")
                        'End If

                        'Ràng buộc khi thay đổi status chỗ này ?
                        Dim giatri As New Decimal
                        Dim max As Decimal = 0
                        For Each item As GridDataItem In rgDataInterview.SelectedItems
                            giatri = Decimal.Parse(item.GetDataKeyValue("EXAMS_ORDER"))
                        Next
                        max = store.GET_MAX_EXAMS_ORDER(hdProgramID.Value)
                        If giatri >= max Then
                            max = 1
                        Else
                            max = 0
                        End If
                        If max = 1 Then
                            If obj.IS_PASS = 0 Then
                                store.UPDATE_CANDIDATE_STATUS(Int32.Parse(dataItem("ID").Text), "KDAT")
                            ElseIf obj.IS_PASS = 1 Then
                                store.UPDATE_CANDIDATE_STATUS(Int32.Parse(dataItem("ID").Text), "DAT")
                            ElseIf obj.IS_PASS = -1 Then
                                store.UPDATE_CANDIDATE_STATUS(Int32.Parse(dataItem("ID").Text), "PROCESS")
                            End If
                        End If
                        If IsSaveCompleted Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                            rgDataInterview.Rebind()
                            gridCadidate.Rebind()
                            ResetText()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand

    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles gridCadidate.NeedDataSource
        Try
            GetCadidateList()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub rgData_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDataInterview.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub gridCadidate_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gridCadidate.SelectedIndexChanged
        Dim dataItem = TryCast(gridCadidate.SelectedItems(0), GridDataItem)
        If dataItem IsNot Nothing Then
            IsRight = 1
            CreateDataFilter()
            rgDataInterview.Rebind()
            ResetText()
        End If
    End Sub

    Private Sub cmdSendEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSendEmail.Click
        Dim dataMail As DataTable
        Dim dtValues As DataTable
        Dim body As String = ""
        Dim mail As String = ""
        Dim mailCC As String = ""
        Dim titleMail As String = ""
        Dim bodyNew As String = ""
        If gridCadidate.SelectedItems.Count = 0 Then
            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
            Exit Sub
        End If

        Dim dataItem = TryCast(gridCadidate.SelectedItems(0), GridDataItem)
        If dataItem Is Nothing Then
            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
            Exit Sub
        End If
        For index = 0 To gridCadidate.SelectedItems.Count - 1
            Dim item As GridDataItem = gridCadidate.SelectedItems(index)
            Dim ID As Decimal = item.GetDataKeyValue("ID")
            mail = store.Get_Email_Candidate(ID)
            If mail = "" Then
                ShowMessage(Translate("Ứng viên được chọn không có email,Xin vui lòng kiểm tra lại"), NotifyType.Warning)
                Exit Sub
            End If
        Next
        For index = 0 To gridCadidate.SelectedItems.Count - 1
            Dim item As GridDataItem = gridCadidate.SelectedItems(index)
            Dim ID As Decimal = item.GetDataKeyValue("ID")
            mail = store.Get_Email_Candidate(ID)
            dataMail = store.GET_MAIL_TEMPLATE("TCO", "Recruitment")
            body = dataMail.Rows(0)("CONTENT").ToString
            titleMail = "THƯ CẢM ƠN"
            ' mailCC = If(dataMail.Rows(0)("MAIL_CC").ToString <> "", dataMail.Rows(0)("MAIL_CC").ToString, Nothing)
            'mailCC = If(LogHelper.CurrentUser.EMAIL IsNot Nothing, LogHelper.CurrentUser.EMAIL.ToString, Nothing)
            dtValues = store.GET_INFO_CADIDATE(item.GetDataKeyValue("ID"))
            mailCC = store.GET_EMAIL_COMPANY(LogHelper.CurrentUser.EMPLOYEE_ID)
            Dim values(dtValues.Columns.Count) As String
            If dtValues.Rows.Count > 0 Then
                For i As Integer = 0 To dtValues.Columns.Count - 1
                    values(i) = If(dtValues.Rows(0)(i).ToString() <> "", dtValues.Rows(0)(i), String.Empty)
                Next
            Else
                ShowMessage(Translate("Chưa có thông tin,Bạn vui lòng kiểm tra lại"), NotifyType.Warning)
                Exit Sub
            End If
            bodyNew = String.Format(body, values)
            If Not Common.Common.sendEmailByServerMail(mail,
                                                     If(mailCC <> "", mailCC, dataMail.Rows(0)("MAIL_CC").ToString()),
                                                      titleMail, bodyNew, String.Empty) Then
                ShowMessage(Translate("Gửi mail thất bại"), NotifyType.Warning)
                Exit Sub
            Else
                ShowMessage(Translate("Gửi mail thành công"), NotifyType.Success)
                ' Update Candidate Status
                store.UPDATE_CANDIDATE_STATUS(Int32.Parse(dataItem("ID").Text), RCContant.TUCHOI)
                CurrentState = CommonMessage.STATE_NORMAL
                UpdateControlState()
            End If
        Next
        ' format email
        'Dim receiver As String = dataItem("Email").Text
        'Dim receiver As String = "tanvn@tinhvan.com"
        'Dim subject As String = "Thư cám ơn"
        'Dim body As String = String.Empty
        ''format body by html template
        'Dim reader As StreamReader = New StreamReader(Server.MapPath("~/Modules/Recruitment/Templates/ThuCamOn.htm"))
        'body = reader.ReadToEnd
        'body = body.Replace("{ngày}", DateTime.Now.Day)
        'body = body.Replace("{tháng}", DateTime.Now.Month)
        'body = body.Replace("{năm}", DateTime.Now.Year)
        'body = body.Replace("{họ tên}", dataItem("FullName").Text.ToUpper())

        'If Common.Common.sendEmailByServerMail(receiver, String.Empty, subject, body, String.Empty, "") Then
        '    ShowMessage(Translate(CommonMessage.MESSAGE_SENDMAIL_COMPLETED), NotifyType.Success)

        '    'Update Candidate Status
        '    store.UPDATE_CANDIDATE_STATUS(Int32.Parse(dataItem("ID").Text), RCContant.TUCHOI)

        '    CurrentState = CommonMessage.STATE_NORMAL
        '    UpdateControlState()
        'Else
        '    ShowMessage(Translate(CommonMessage.MESSAGE_SENDMAIL_ERROR), NotifyType.Warning)
        '    Exit Sub
        'End If
    End Sub

#End Region

#Region "Custom"

    Protected Function GetCadidateList()
        Try
            If hdProgramID.Value IsNot Nothing Then
                tabSource = store.CANDIDATE_LIST_GETBYPROGRAM(hdProgramID.Value)
                If tabSource IsNot Nothing Then
                    gridCadidate.VirtualItemCount = tabSource.Rows.Count
                    gridCadidate.DataSource = tabSource
                Else
                    gridCadidate.DataSource = New List(Of COSTALLOCATE_DTO)
                End If
            Else
                gridCadidate.DataSource = New List(Of COSTALLOCATE_DTO)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Function

    Protected Function CreateDataFilter()
        Try
            If gridCadidate.Items.Count > 0 Then
                If hdProgramID.Value IsNot Nothing Then

                    ' set default first row selected
                    If IsRight = 0 Then
                        gridCadidate.MasterTableView.Items(0).Selected = True
                    End If
                    Dim dataItem = TryCast(gridCadidate.SelectedItems(0), GridDataItem)

                    If dataItem IsNot Nothing Then
                        tabSource = store.EXAMS_GETBYCANDIDATE(hdProgramID.Value, Int32.Parse(dataItem("ID").Text), -1)
                        If tabSource IsNot Nothing And tabSource.Rows.Count > 0 Then
                            rgDataInterview.VirtualItemCount = tabSource.Rows.Count
                            rgDataInterview.DataSource = tabSource
                        Else
                            rgDataInterview.DataSource = New List(Of PROGRAM_SCHEDULE_CAN_DTO)
                        End If
                    End If
                Else
                    rgDataInterview.DataSource = New List(Of PROGRAM_SCHEDULE_CAN_DTO)
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

#End Region

    Protected Sub gridCadidate_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gridCadidate.ItemDataBound
        If TypeOf e.Item Is GridHeaderItem Then
            Dim chkbx As CheckBox = DirectCast(TryCast(e.Item, GridHeaderItem)("cbStatus").Controls(0), CheckBox)
            'to disable the chekbox 
            chkbx.Enabled = False
            'or to hide the checkbox 
            chkbx.Visible = False
        End If
    End Sub

    Private Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        Try
            'Template_ExportProgramDeclare()
            If cboSchedule.SelectedValue = "" Then
                ShowMessage(Translate("Bạn phải chọn vòng phỏng vấn"), NotifyType.Warning)
                Exit Sub
            End If
            HttpContext.Current.Session("PROGRAMID") = hdProgramID.Value
            HttpContext.Current.Session("SCHEDULEID") = cboSchedule.SelectedValue
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_ExportProgramDeclare');", True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Template_ExportProgramDeclare()
        Dim rep As New RecruitmentStoreProcedure
        Try
            Dim configPath As String = Server.MapPath("ReportTemplates\Recruitment\Import\Template_import_kqtuyendung.xls")
            Dim dsData As DataSet = Nothing
            If File.Exists(configPath) Then
                ExportTemplate(configPath,
                                      dsData, Nothing, "Template_import_kqtuyendung" & Format(Date.Now, "yyyyMMdd"))
            Else
                ShowMessage(Translate("Template không tồn tại"), Utilities.NotifyType.Error)
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dsData As DataSet,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        'Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            'templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            'filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName

            'cau hinh lai duong dan tren server
            filePath = sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dsData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Private Sub btnImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Try
            ctrlUpload.isMultiple = False
            ctrlUpload.Show()
            'CurrentState = CommonMessage.TOOLBARITEM_IMPORT
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked
        Import_DeclareProgram()
    End Sub

    Private Sub Import_DeclareProgram()
        Try
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Dim startTime As DateTime = DateTime.UtcNow
            Dim fileName As String
            Dim dsDataPrepare As New DataSet
            Dim workbook As Aspose.Cells.Workbook
            Dim worksheet As Aspose.Cells.Worksheet

            Try
                Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
                Dim savepath = Context.Server.MapPath(tempPath)

                For Each file As UploadedFile In ctrlUpload.UploadedFiles
                    fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                    file.SaveAs(fileName, True)
                    workbook = New Aspose.Cells.Workbook(fileName)
                    worksheet = workbook.Worksheets(0)
                    dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                    If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
                Next
                dtData = dtData.Clone()
                TableMapping(dsDataPrepare.Tables(0))
                For Each rows As DataRow In dsDataPrepare.Tables(0).Select("CODE<>'""'").CopyToDataTable.Rows
                    If IsDBNull(rows("CODE")) OrElse rows("CODE") = "" Then Continue For
                    Dim newRow As DataRow = dtData.NewRow
                    newRow("STT") = rows("STT")
                    newRow("CODE") = rows("CODE")
                    newRow("ID") = If(IsNumeric(rows("ID")), rows("ID"), 0)
                    newRow("FullName") = rows("FullName")
                    newRow("NAME") = rows("NAME")
                    newRow("NAME_ID") = If(IsNumeric(rows("NAME_ID")), rows("NAME_ID"), 0)
                    newRow("SCHEDULE_DATE") = rows("SCHEDULE_DATE")
                    newRow("COMMENT_INFO") = rows("COMMENT_INFO")
                    newRow("STATUS_NAME") = rows("STATUS_NAME")
                    newRow("STATUS_ID") = If(IsNumeric(rows("STATUS_ID")), rows("STATUS_ID"), 0)
                    newRow("fullname_vn") = rows("fullname_vn")
                    newRow("EXAMS_ORDER") = If(IsNumeric(rows("EXAMS_ORDER")), rows("EXAMS_ORDER"), 0)
                    newRow("ID_PSC") = If(IsNumeric(rows("ID_PSC")), rows("ID_PSC"), 0)
                    dtData.Rows.Add(newRow)
                Next
                dtData.TableName = "DATA"
                Dim IsSaveCompleted As Boolean
                Dim max As Decimal = 0
                max = store.GET_MAX_EXAMS_ORDER(hdProgramID.Value)
                For Each rows As DataRow In dtData.Rows

                    IsSaveCompleted = store.UPDATE_CANDIDATE_RESULT(
                                                          Decimal.Parse(rows("ID_PSC")),
                                                            0,
                                                           rows("COMMENT_INFO").ToString,
                                                            "",
                                                          Decimal.Parse(rows("STATUS_ID")))

                    Dim giatri As New Decimal
                    Dim check As Decimal = 0
                    giatri = Decimal.Parse(rows("EXAMS_ORDER"))
                    If giatri >= max Then
                        check = 1
                    Else
                        check = 0
                    End If
                    If check = 1 Then
                        If Decimal.Parse(rows("STATUS_ID")) = 0 Then
                            store.UPDATE_CANDIDATE_STATUS(Int32.Parse(rows("ID")), "KDAT")
                        ElseIf Decimal.Parse(rows("STATUS_ID")) = 1 Then
                            store.UPDATE_CANDIDATE_STATUS(Int32.Parse(rows("ID")), "DAT")
                        ElseIf Decimal.Parse(rows("STATUS_ID")) = -1 Then
                            store.UPDATE_CANDIDATE_STATUS(Int32.Parse(rows("ID")), "PROCESS")
                        End If
                    End If
                Next
                'import này k cần kiểm tra dữ liệu trên excel
                If IsSaveCompleted Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgDataInterview.Rebind()
                    gridCadidate.Rebind()
                    ResetText()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If




            Catch ex As Exception
                ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            End Try
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TableMapping(ByVal dtdata As DataTable)
        Dim row As DataRow = dtdata.Rows(2)
        Dim index As Integer = 0
        For Each cols As DataColumn In dtdata.Columns
            Try
                cols.ColumnName = row(index)
                index += 1
                If index > row.ItemArray.Length - 1 Then Exit For
            Catch ex As Exception
                Exit For
            End Try
        Next
        dtdata.Rows(0).Delete()
        dtdata.Rows(0).Delete()
        dtdata.Rows(0).Delete()
        dtdata.AcceptChanges()
    End Sub
#Region "DS Đề nghị thử việc - Button"

    Private Sub btnSuggestIntern_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSuggestIntern.Click
        Try
            If cboSuggestIntern.SelectedValue.ToString = "" Then
                ShowMessage(Translate("Vui lòng chọn biểu mẫu"), NotifyType.Warning)
                cboSuggestIntern.Focus()
                Exit Sub
            End If
            Form_Suggest_Intern_Clicked()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub Form_Suggest_Intern_Clicked()
        Try            
            HttpContext.Current.Session("SuggestIntern_Value") = cboSuggestIntern.SelectedValue.ToString
            HttpContext.Current.Session("PROGRAMID") = hdProgramID.Value
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Form_Suggest_Intern');", True)          
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
   
#End Region
End Class
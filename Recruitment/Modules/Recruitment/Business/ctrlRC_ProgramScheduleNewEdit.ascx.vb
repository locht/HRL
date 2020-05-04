﻿Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Common
Imports Common.CommonBusiness
Imports Telerik.Web.UI
Imports System.IO
Imports HistaffFrameworkPublic
Imports System.Reflection
Imports System.Net.Mail

Public Class ctrlRC_ProgramScheduleNewEdit
    Inherits Common.CommonView
    Private store As New RecruitmentStoreProcedure()
    Private rep As New HistaffFrameworkRepository
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = True
    Private userlog As UserLog

#Region "Property"

    Public Property SelectedItemCanNotSchedule As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemCanNotSchedule") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemCanNotSchedule") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemCanNotSchedule")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemCanNotSchedule") = value
        End Set
    End Property

    Public Property SelectedItemCanSchedule As List(Of Decimal)
        Get
            If ViewState(Me.ID & "_SelectedItemCanSchedule") Is Nothing Then
                ViewState(Me.ID & "_SelectedItemCanSchedule") = New List(Of Decimal)
            End If
            Return ViewState(Me.ID & "_SelectedItemCanSchedule")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItemCanSchedule") = value
        End Set
    End Property

    Public Property lstCanNotSchedule As List(Of ProgramScheduleCanDTO)
        Get
            Return ViewState(Me.ID & "_lstCanNotSchedule")
        End Get
        Set(ByVal value As List(Of ProgramScheduleCanDTO))
            ViewState(Me.ID & "_lstCanNotSchedule") = value
        End Set
    End Property

    Public Property lstCanSchedule As List(Of ProgramScheduleCanDTO)
        Get
            Return ViewState(Me.ID & "_lstCanSchedule")
        End Get
        Set(ByVal value As List(Of ProgramScheduleCanDTO))
            ViewState(Me.ID & "_lstCanSchedule") = value
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

    Property tabCandidateSchedule As DataTable
        Get
            Return ViewState(Me.ID & "_tabCandidateSchedule")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_tabCandidateSchedule") = value
        End Set
    End Property

    Public Property dtCan As DataTable
        Get
            If ViewState(Me.ID & "_dtCan") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("ID", GetType(String))
                dt.Columns.Add("CANDIDATE_CODE", GetType(String))
                dt.Columns.Add("FULLNAME_VN", GetType(String))
                dt.Columns.Add("BIRTH_DATE", GetType(String))
                dt.Columns.Add("BIRTH_PROVINCE_NAME", GetType(String))
                dt.Columns.Add("PER_EMAIL", GetType(String))
                dt.Columns.Add("ID_NO", GetType(String))
                dt.Columns.Add("SCHEDULE_DATE", GetType(String))
                dt.Columns.Add("SCHEDULE_BY", GetType(String))
                dt.Columns.Add("EXAM_NAME", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtCan")
        End Get
        Set(value As DataTable)
            ViewState(Me.ID & "_dtCan") = value
        End Set
    End Property

    Public Property dtCanNot As DataTable
        Get
            If ViewState(Me.ID & "_dtCanNot") Is Nothing Then
                Dim dt As New DataTable
                dt.Columns.Add("ID", GetType(String))
                dt.Columns.Add("CANDIDATE_CODE", GetType(String))
                dt.Columns.Add("FULLNAME_VN", GetType(String))
                dt.Columns.Add("BIRTH_DATE", GetType(String))
                dt.Columns.Add("BIRTH_PROVINCE_NAME", GetType(String))
                dt.Columns.Add("PER_EMAIL", GetType(String))
                dt.Columns.Add("ID_NO", GetType(String))
                ViewState(Me.ID & "_dtCanNot") = dt
            End If
            Return ViewState(Me.ID & "_dtCanNot")
        End Get
        Set(value As DataTable)
            ViewState(Me.ID & "_dtCanNot") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            rgCanNotSchedule.PageSize = 50
            rgCanNotSchedule.AllowCustomPaging = True
            rgCanSchedule.PageSize = 50
            rgCanSchedule.AllowCustomPaging = True
            hidProgramID.Value = Request.Params("PROGRAM_ID")
            hidID.Value = Request.Params("SCHEDULE_ID")
            If hidID.Value = "" Then
                hidID.Value = 0
            End If
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
            Dim dtData
            hidProgramID.Value = Request.Params("PROGRAM_ID")
            Using rep As New RecruitmentRepository
                dtData = rep.GetProgramExamsList(hidProgramID.Value, True)
                'FillRadCombobox(cboExams, dtData, "NAME", "ID")
            End Using
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try

            If Not IsPostBack Then
                LoadExamsList()
                rdScheduleDate.SelectedDate = DateTime.Now
                store.DELETE_PRO_SCHEDULE_CAN_ISNULL()
                Dim FULLNAME As String = ""
                Dim employee_ID As Decimal?

                Using rep As New RecruitmentRepository
                    Dim objPro = rep.GetProgramByID(New ProgramDTO With {.ID = Decimal.Parse(hidProgramID.Value)})
                    lblJobName.Text = objPro.ORG_NAME
                    lblTitleName.Text = objPro.TITLE_NAME
                    lblRequestNo.Text = objPro.REQUEST_NUMBER
                    lbRecruited.Text = objPro.CANDIDATE_RECEIVED

                    If hidID.Value <> "" And Decimal.Parse(hidID.Value) > 0 Then


                        Dim tab As DataTable
                        tab = store.GET_PRO_SCHEDULE_BYID(Decimal.Parse(hidID.Value))
                        If tab.Rows.Count > 0 Then
                            rdScheduleDate.SelectedDate = tab.Rows(0)("SCHEDULE_DATE").ToString()
                            txtExamsPlace.Text = tab.Rows(0)("EXAMS_PLACE").ToString()
                            txtNote.Text = tab.Rows(0)("NOTE").ToString()
                            FULLNAME = tab.Rows(0)("FULLNAME").ToString()
                            employee_ID = If(tab.Rows(0)("EMPLOYEE_ID").ToString() = "", Nothing, Decimal.Parse(tab.Rows(0)("EMPLOYEE_ID").ToString()))
                            If employee_ID <> 0 Then
                                cboUsher.CheckBoxes = True
                                cboUsher.SelectedValue = employee_ID
                                cboUsher.CheckedItemsTexts = RadComboBoxCheckedItemsTexts.DisplayAllInInput
                                Dim item1 As New RadComboBoxItem()
                                item1.Text = FULLNAME
                                item1.Value = employee_ID
                                cboUsher.Items.Add(item1)
                                cboUsher.ClearCheckedItems()
                                For Each chk As RadComboBoxItem In cboUsher.Items
                                    If cboUsher.SelectedValue.Contains(chk.Value) Then
                                        chk.Checked = True
                                    End If
                                Next
                            End If
                        End If
                        'Dim obj = rep.GetProgramScheduleByID(New ProgramScheduleDTO With {.ID = Decimal.Parse(hidID.Value)})
                        'rdScheduleDate.SelectedDate = obj.SCHEDULE_DATE
                        'txtExamsPlace.Text = obj.EXAMS_PLACE
                        'txtNote.Text = obj.NOTE

                        'cboUsher.CheckBoxes = True
                        'cboUsher.CheckedItemsTexts = RadComboBoxCheckedItemsTexts.DisplayAllInInput
                        'For Each itm In obj.lstScheduleUsher
                        '    Dim item As New RadComboBoxItem
                        '    item.Value = itm.EMPLOYEE_ID
                        '    item.Text = itm.EMPLOYEE_CODE & " - " & itm.EMPLOYEE_NAME
                        '    item.Checked = True
                        '    cboUsher.Items.Add(item)
                        'Next

                        'rdScheduleDate.Enabled = True
                        'txtExamsPlace.Enabled = True
                        'txtNote.Enabled = True
                    End If
                End Using
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        If phFindUsher.Controls.Contains(ctrlFindEmployeePopup) Then
            phFindUsher.Controls.Remove(ctrlFindEmployeePopup)
        End If
        Select Case isLoadPopup
            Case 1
                ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                ctrlFindEmployeePopup.MustHaveContract = False
                phFindUsher.Controls.Add(ctrlFindEmployeePopup)
                ctrlFindEmployeePopup.MultiSelect = False
        End Select

        'Using rep As New RecruitmentRepository
        '    'Dim lstCanNotID As New List(Of Decimal)
        '    'For Each dr As Telerik.Web.UI.GridDataItem In rgCanNotSchedule.SelectedItems
        '    '    lstCanNotID.Add(dr.GetDataKeyValue("CANDIDATE_ID"))
        '    'Next
        '    'Dim lstCanID As New List(Of Decimal)
        '    'For Each dr As Telerik.Web.UI.GridDataItem In rgCanNotSchedule.SelectedItems
        '    '    lstCanID.Add(dr.GetDataKeyValue("CANDIDATE_ID"))
        '    'Next
        '    Select Case CurrentState
        '        Case CommonMessage.STATE_NEW
        '            CurrentState = CommonMessage.STATE_NORMAL
        '            For Each studentID In SelectedItemCanNotSchedule
        '                Dim objNot = (From p In lstCanNotSchedule Where p.CANDIDATE_ID = studentID).FirstOrDefault
        '                If objNot IsNot Nothing Then
        '                    objNot.CANDIDATE_ID = studentID
        '                    'Dim startHour As New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day,
        '                    '                          rtStartHour.SelectedDate.Value.Hour,
        '                    '                          rtStartHour.SelectedDate.Value.Minute,
        '                    '                          rtStartHour.SelectedDate.Value.Second)
        '                    'Dim endHour As New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day,
        '                    '                          rtEndHour.SelectedDate.Value.Hour,
        '                    '                          rtEndHour.SelectedDate.Value.Minute,
        '                    '                          rtEndHour.SelectedDate.Value.Second)
        '                    'objNot.START_HOUR = startHour
        '                    'objNot.END_HOUR = endHour
        '                    objNot.STATUS_ID = "DATLICH"
        '                    'If objNot IsNot Nothing Then
        '                    '    lstCanSchedule.Add(objNot)
        '                    '    lstCanNotSchedule.Remove(objNot)
        '                    '    rep.UpdateStatusCandidate(lstCanNotID, objNot.STATUS_ID = RecruitmentCommon.RC_CANDIDATE_STATUS.DATLICH)
        '                    'End If
        '                End If
        '            Next
        '            rgCanNotSchedule.Rebind()
        '            rgCanSchedule.Rebind()

        '            'Case CommonMessage.STATE_DELETE
        '            '    CurrentState = CommonMessage.STATE_NORMAL
        '            '    For Each studentID In SelectedItemCanSchedule
        '            '        Dim obj As ProgramScheduleCanDTO = (From p In lstCanSchedule Where p.CANDIDATE_ID = studentID).FirstOrDefault
        '            '        obj.CANDIDATE_ID = studentID
        '            '        obj.STATUS_ID = "DUDK"
        '            '        If obj IsNot Nothing Then
        '            '            lstCanNotSchedule.Add(obj)
        '            '            lstCanSchedule.Remove(obj)
        '            '            rep.UpdateStatusCandidate(lstCanNotID, obj.STATUS_ID = RecruitmentCommon.RC_CANDIDATE_STATUS.DUDIEUKIEN_ID)
        '            '        End If
        '            '    Next
        '            '    rgCanNotSchedule.Rebind()
        '            '    rgCanSchedule.Rebind()

        '    End Select

        'End Using
    End Sub

    Private Sub LoadExamsList()
        'If Not IsPostBack Then
        rlbExams.DataSource = store.GET_AllExams_ByProgram(Decimal.Parse(hidProgramID.Value), hidID.Value)
        rlbExams.DataTextField = "NAME"
        rlbExams.DataValueField = "ID"
        rlbExams.DataBind()
        rlbExams.Items(0).Selected = True

        If hidID.Value <> 0 Then
            rlbExams.Enabled = False
        Else
            rlbExams.Enabled = True
        End If

        'Dim collection As IList(Of RadListBoxItem) = rlbExams.Items
        'For Each item As RadListBoxItem In collection
        '    item.Checked = True
        'Next
        'rlbExams.Enabled = False

        'End If
    End Sub

#End Region

#Region "Event"

    Private Sub tbarMain_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles tbarMain.ButtonClick
        Dim rep As New RecruitmentRepository
        Dim _error As Integer = 0
        Dim sType As Object = Nothing
        Dim IsSaveCompleted As Boolean
        Try
            Using doc As New WordCommon
                Select Case CType(e.Item, Telerik.Web.UI.RadToolBarButton).CommandName
                    Case CommonMessage.TOOLBARITEM_SAVE
                        If rdScheduleDate.SelectedDate Is Nothing Then
                            ShowMessage(Translate("Bạn phải chọn ngày thi"), NotifyType.Warning)
                            Exit Sub
                        End If

                        Dim obj As New ProgramScheduleDTO
                        'obj.RC_PROGRAM_EXAMS_ID = Decimal.Parse(cboExams.SelectedValue)
                        obj.RC_PROGRAM_ID = Decimal.Parse(hidProgramID.Value)
                        obj.SCHEDULE_DATE = rdScheduleDate.SelectedDate
                        obj.EXAMS_PLACE = txtExamsPlace.Text
                        obj.NOTE = txtNote.Text
                        If cboUsher.SelectedValue <> "" Then
                            obj.EMPLOYEE_ID = cboUsher.SelectedValue
                        Else
                            obj.EMPLOYEE_ID = Nothing
                        End If
                        If hidID.Value <> Nothing And Decimal.Parse(hidID.Value) > 0 Then
                            IsSaveCompleted = store.UPDATE_PRO_SCHEDULE(Decimal.Parse(hidID.Value), obj.EMPLOYEE_ID, obj.SCHEDULE_DATE, obj.EXAMS_PLACE, obj.NOTE, String.Empty,
                                                             String.Empty)

                        Else
                            IsSaveCompleted = store.ADDNEW_PRO_SCHEDULE(obj.RC_PROGRAM_ID, obj.EMPLOYEE_ID, obj.SCHEDULE_DATE, obj.EXAMS_PLACE, obj.NOTE, String.Empty,
                                                            String.Empty)

                            If IsSaveCompleted Then
                                'update Pro_Shedule_Can
                                Dim idProSchedule As Int32
                                idProSchedule = store.GET_TOPID_PRO_SCHEDULE(obj.RC_PROGRAM_ID)
                                hidID.Value = idProSchedule
                                'If idProSchedule > 0 Then
                                '    store.UPDATE_PRO_SCHEDULE_ID(idProSchedule)
                                'End If
                            End If
                        End If

                        store.DELETE_PRO_SCHEDULE_CAN(Decimal.Parse(hidID.Value), 0)
                        For Each item As GridDataItem In rgCanSchedule.Items
                            store.ADDNEW_CAN_PRO_SCHEDULE(item.GetDataKeyValue("ID"), Decimal.Parse(hidID.Value), rlbExams.SelectedValue)
                            store.UPDATE_CANDIDATE_STATUS(item.GetDataKeyValue("ID"), "PROCESS")
                        Next
                        

                        If IsSaveCompleted Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                            rgCanNotSchedule.Rebind()
                            rgCanSchedule.Rebind()
                            ''POPUPTOLINK
                            Dim PROGRAM_ID As String = Request.QueryString("PROGRAM_ID")
                            'Response.Redirect("/Default.aspx?mid=Recruitment&fid=ctrlRC_ProgramSchedule&group=Business&PROGRAM_ID=" & PROGRAM_ID)

                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                        
                    Case CommonMessage.TOOLBARITEM_CANCEL
                        ''POPUPTOLINK_CANCEL
                        
                        'Dim PROGRAM_ID As String = Request.QueryString("PROGRAM_ID")
                        ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "window.close();", True)
                        'Response.Redirect("/Default.aspx?mid=Recruitment&fid=ctrlRC_ProgramSchedule&group=Business&PROGRAM_ID=" & PROGRAM_ID)
                        ' Response.Redirect("/Default.aspx?mid=Recruitment&fid=ctrlRC_ProgramSchedule&group=Business")
                End Select


            End Using
        Catch ex As Exception
            ShowMessage(ex.Message, Framework.UI.Utilities.NotifyType.Error)
        End Try
    End Sub

    Private Sub rgCanNotSchedule_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgCanNotSchedule.PageIndexChanged
        'GetCanNotScheduleSelected()
    End Sub

    Private Sub rgCanNotSchedule_PageSizeChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles rgCanNotSchedule.PageSizeChanged
        'GetCanNotScheduleSelected()
    End Sub

    Private Sub rgCanNotSchedule_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles rgCanNotSchedule.SortCommand
        'GetCanNotScheduleSelected()
    End Sub

    Private Sub rgCanNotSchedule_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCanNotSchedule.NeedDataSource
        getCanNotSchedule()
    End Sub






    Private Sub rgCanSchedule_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgCanSchedule.PageIndexChanged
        'GetCanScheduleSelected()
    End Sub

    Private Sub rgCanSchedule_PageSizeChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles rgCanSchedule.PageSizeChanged
        'GetCanScheduleSelected()
    End Sub

    Private Sub rgCanSchedule_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles rgCanSchedule.SortCommand
        'GetCanScheduleSelected()
    End Sub

    Private Sub rgCanSchedule_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCanSchedule.NeedDataSource
        getCanSchedule()
    End Sub




    Protected Sub btnFindUsher_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindUsher.Click
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New RecruitmentRepository
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            'If lstCommonEmployee.ToList.Count > 1 Then
            '    ShowMessage(Translate("Chọn duy nhất một nhân viên"), NotifyType.Warning)
            '    Exit Sub
            'End If
            cboUsher.Items.Clear()
            cboUsher.CheckBoxes = True
            cboUsher.CheckedItemsTexts = RadComboBoxCheckedItemsTexts.DisplayAllInInput
            For Each itm In lstCommonEmployee
                Dim item As New RadComboBoxItem
                item.Value = itm.EMPLOYEE_ID
                '  item.Text = itm.EMPLOYEE_CODE & " - " & itm.FULLNAME_VN
                item.Text = itm.FULLNAME_VN
                item.Checked = True
                cboUsher.Items.Add(item)
            Next
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFindEmployeePopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub




    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        'GetCanScheduleSelected()
        CurrentState = CommonMessage.STATE_DELETE
        UpdateControlState()

    End Sub

    Private Sub btnInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        If Page.IsValid Then
            'GetCanNotScheduleSelected()
            CurrentState = CommonMessage.STATE_NEW
            UpdateControlState()
        End If
    End Sub

    Private Sub btnSendMail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSendMail.Click
        Dim dataMail As DataTable
        Dim dtValues As DataTable
        Dim body As String = ""
        Dim mail As String = ""
        Dim mailCC As String = ""
        Dim titleMail As String = ""
        Dim bodyNew As String = ""
        If rgCanSchedule.SelectedItems.Count = 0 Then
            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
            Exit Sub
        End If

        Dim dataItem = TryCast(rgCanSchedule.SelectedItems(0), GridDataItem)
        If dataItem Is Nothing Then
            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
            Exit Sub
        End If
        For index = 0 To rgCanSchedule.SelectedItems.Count - 1
            Dim item As GridDataItem = rgCanSchedule.SelectedItems(index)
            Dim ID As Decimal = item.GetDataKeyValue("ID")
            mail = store.Get_Email_Candidate(ID)
            If mail = "" Then
                ShowMessage(Translate("Ứng viên được chọn không có email,Xin vui lòng kiểm tra lại"), NotifyType.Warning)
                Exit Sub
            End If
        Next
        For index = 0 To rgCanSchedule.SelectedItems.Count - 1
            Dim item As GridDataItem = rgCanSchedule.SelectedItems(index)
            Dim ID As Decimal = item.GetDataKeyValue("ID")
            mail = store.Get_Email_Candidate(ID)
            dataMail = store.GET_MAIL_TEMPLATE("TMPV", "Recruitment")
            body = dataMail.Rows(0)("CONTENT").ToString
            titleMail = "THƯ MỜI PHỎNG VẤN"
            'mailCC = If(dataMail.Rows(0)("MAIL_CC").ToString <> "", dataMail.Rows(0)("MAIL_CC").ToString, Nothing)
            'mailCC = If(LogHelper.CurrentUser.EMAIL IsNot Nothing, LogHelper.CurrentUser.EMAIL.ToString, Nothing)
            mail = store.Get_Email_Employee(If(LogHelper.CurrentUser.EMPLOYEE_ID IsNot Nothing, LogHelper.CurrentUser.EMPLOYEE_ID.ToString, Nothing))
            dtValues = store.GET_INFO_CADIDATE(item.GetDataKeyValue("ID"))
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
                CurrentState = CommonMessage.STATE_NORMAL
                UpdateControlState()
            End If
        Next
    End Sub

    Private Sub btnInterviewList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInterviewList.Click
        Dim template_URL As String = String.Format("~/ReportTemplates/Recruitment/Report/Danh sach phong van.xlxs")
        Dim fileName As String = String.Format("Danh sách phỏng vấn - {0}", lblJobName.Text)
        Dim _error As String = ""
        'Dim dt = lstCanSchedule.ToTable
        'Dim dt As New DataTable()
        'dt = DirectCast(rgCanSchedule.DataSource, DataTable)
        'dt.TableName = "Data"
        'Dim ds As New DataSet
        'ds.Tables.Add(dt)

        'tabCandidateSchedule = store.GET_PROGRAM_SCHCEDULE_LIST(Decimal.Parse(hidProgramID.Value), Decimal.Parse(hidID.Value))

        Using xls As New ExcelCommon
            'If dt.Rows.Count > 0 Then
            rgCanSchedule.ExportExcel(Server, Response, tabCandidateSchedule, "data")
            'End If
        End Using
    End Sub

    Private Sub btnRecLetter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRecLetter.Click
        If rgCanSchedule.SelectedItems.Count = 0 Then
            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
            Exit Sub
        End If

        'Dim item As GridDataItem = rgCanSchedule.SelectedItems(0)
        'Dim dtData As DataTable = lstCanSchedule.Where(Function(x) x.CANDIDATE_ID = item("CANDIDATE_ID").Text).ToList.ToTable
        Dim folderName As String = ""
        Dim filePath As String = ""
        Dim extension As String = ""
        Dim iError As Integer = 0
        ExportFileWord(tabCandidateSchedule, folderName, filePath, extension, iError, rgCanSchedule.SelectedItems(0))
    End Sub

    Private Sub btnRecInterviewLetter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRecInterviewLetter.Click
        If rgCanSchedule.SelectedItems.Count = 0 Then
            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
            Exit Sub
        End If

        'Dim dtData As DataTable
        Dim folderName As String = ""
        Dim filePath As String = ""
        Dim extension As String = ""
        Dim iError As Integer = 0
        'Dim item As GridDataItem = rgCanSchedule.SelectedItems(0)
        'dtData = lstCanSchedule.Where(Function(x) x.CANDIDATE_ID = item("CANDIDATE_ID").Text).ToList.ToTable
        ExportFileWord(tabCandidateSchedule, folderName, filePath, extension, iError, rgCanSchedule.SelectedItems(0))
    End Sub

#End Region

#Region "Custom"

    Protected Function getCanNotSchedule(Optional ByVal isFull As Boolean = False) As DataTable
        Try
            Dim lstID As String = ","
            For Each item As GridDataItem In rgCanSchedule.Items
                lstID = lstID & item.GetDataKeyValue("ID") & ","
            Next
            dtCanNot = store.GET_CANDIDATE_NOT_SCHEDULE(Decimal.Parse(hidProgramID.Value), hidID.Value, chkFillter.Checked, rlbExams.SelectedValue, lstID)
            rgCanNotSchedule.DataSource = dtCanNot
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Function

    Protected Function getCanSchedule(Optional ByVal isFull As Boolean = False) As DataTable
        Try

            dtCan = store.GET_PROGRAM_SCHCEDULE_LIST(Decimal.Parse(hidProgramID.Value), Decimal.Parse(hidID.Value))
            rgCanSchedule.DataSource = dtCan

            If dtCan.Rows.Count = 0 Then
                rlbExams.Enabled = True
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Function

    Private Sub ExportFileWord(ByVal dtData As DataTable, ByVal folderName As String, ByVal filePath As String,
                               ByVal extension As String, ByVal iError As Integer, ByVal item As GridDataItem)
        If rgCanSchedule.SelectedItems.Count = 0 Then
            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
            Exit Sub
        End If
        If rgCanSchedule.SelectedItems.Count > 1 Then
            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
            Exit Sub
        End If

        'folderName = ""
        'filePath = ""
        'extension = ""
        'iError = 0
        'item = rgCanSchedule.SelectedItems(0)
        ' Kiểm tra + lấy thông tin trong database
        'Using rep As New ProfileRepository
        '    dtData = rep.GetHU_DataDynamic(item.GetDataKeyValue("ID"),
        '                                   ProfileCommon.HU_TEMPLATE_TYPE.CONTRACT_ID,
        '                                   folderName)
        '    If dtData.Rows.Count = 0 Then
        '        ShowMessage("Dữ liệu không tồn tại", NotifyType.Warning)
        '        Exit Sub
        '    End If
        '    If folderName = "" Then
        '        ShowMessage("Thư mục không tồn tại", NotifyType.Warning)
        '        Exit Sub
        '    End If
        'End Using

        'dtData = lstCanSchedule.Where(Function(x) x.CANDIDATE_ID = item("CANDIDATE_ID").Text).ToList.ToTable
        ' Kiểm tra file theo thông tin trong database
        If Not Utilities.GetTemplateLinkFile(item.GetDataKeyValue("ID"),
                                         folderName,
                                         filePath,
                                         extension,
                                         iError) Then
            Select Case iError
                Case 1
                    ShowMessage("Biểu mẫu không tồn tại", NotifyType.Warning)
                    Exit Sub
            End Select
        End If
        ' Export file mẫu
        Using word As New WordCommon
            word.ExportMailMerge(filePath,
                                 "AAAA",
                                 dtData,
                                 Response)
        End Using
    End Sub

#End Region

    Protected Sub RadAjaxPanel1_AjaxRequest(ByVal sender As Object, ByVal e As AjaxRequestEventArgs)
        userlog = LogHelper.GetUserLog
        Dim check As Integer = 0
        Dim str As String
        Dim arr() As String
        arr = e.Argument.Split("_")
        str = arr(arr.Count - 1)

        'If hidID.Value = 0 Then
        '    store.ADDNEW_PRO_SCHEDULE(hidProgramID.Value, If(cboUsher.SelectedValue = "", Nothing, cboUsher.SelectedValue), rdScheduleDate.SelectedDate, txtExamsPlace.Text, txtNote.Text, userlog.Username, String.Format("{0}-{1}", userlog.ComputerName, userlog.Ip))
        '    'store.ADDNEW_PRO_SCHEDULE(hidProgramID.Value, rdScheduleDate.SelectedDate, txtExamsPlace.Text, txtNote.Text, String.Empty, String.Empty)
        '    hidID.Value = store.GET_TOPID_PRO_SCHEDULE(hidProgramID.Value)
        'End If

        Select Case str
            Case "btnInsert"

                For idx = 0 To rgCanNotSchedule.SelectedItems.Count - 1
                    Dim item As GridDataItem = rgCanNotSchedule.SelectedItems(idx)
                    Dim newRow1 As DataRow = dtCan.NewRow
                    newRow1("ID") = item.GetDataKeyValue("ID")
                    newRow1("CANDIDATE_CODE") = item.GetDataKeyValue("CANDIDATE_CODE")
                    newRow1("FULLNAME_VN") = item.GetDataKeyValue("FULLNAME_VN")
                    newRow1("BIRTH_DATE") = If(Not IsDBNull(item.GetDataKeyValue("BIRTH_DATE")), String.Format(ToDate(item.GetDataKeyValue("BIRTH_DATE")), "DD/MM/YYYY"), Nothing)
                    newRow1("BIRTH_PROVINCE_NAME") = item.GetDataKeyValue("BIRTH_PROVINCE_NAME")
                    newRow1("PER_EMAIL") = item.GetDataKeyValue("PER_EMAIL")
                    newRow1("ID_NO") = item.GetDataKeyValue("ID_NO")
                    newRow1("SCHEDULE_DATE") = Nothing
                    newRow1("SCHEDULE_BY") = Nothing
                    newRow1("EXAM_NAME") = Nothing
                    dtCan.Rows.Add(newRow1)
                    'insert candidate --> program schedule candidate
                    'Dim idCandidate As Int32 = Int32.Parse(item.GetDataKeyValue("ID").ToString())
                    ''For Each itemExams As RadListBoxItem In rlbExams.Items
                    ''    If itemExams.Checked = True Then
                    ''        Dim idPro_Exams As Int32 = Decimal.Parse(itemExams.Value)
                    ''        store.ADDNEW_CAN_PRO_SCHEDULE(idCandidate, Decimal.Parse(hidID.Value), idPro_Exams)
                    ''        check = 2
                    ''    Else
                    ''        Continue For
                    ''    End If
                    ''Next
                    'Dim idPro_Exams As Int32 = Decimal.Parse(rlbExams.SelectedValue)
                    'store.ADDNEW_CAN_PRO_SCHEDULE(idCandidate, Decimal.Parse(hidID.Value), idPro_Exams)
                    'check = 2
                    'If check = 2 Then
                    '    store.UPDATE_CANDIDATE_STATUS(idCandidate, "PROCESS")
                    'End If
                    Dim s = (From q In dtCanNot Where q("ID") = item.GetDataKeyValue("ID")).FirstOrDefault
                    dtCanNot.Rows.Remove(s)
                Next
                
            Case "btnDelete"
                For idx = 0 To rgCanSchedule.SelectedItems.Count - 1
                    Dim item As GridDataItem = rgCanSchedule.SelectedItems(idx)
                    Dim newRow1 As DataRow = dtCanNot.NewRow
                    newRow1("ID") = item.GetDataKeyValue("ID")
                    newRow1("CANDIDATE_CODE") = item.GetDataKeyValue("CANDIDATE_CODE")
                    newRow1("FULLNAME_VN") = item.GetDataKeyValue("FULLNAME_VN")
                    newRow1("BIRTH_DATE") = String.Format(ToDate(item.GetDataKeyValue("BIRTH_DATE")), "DD/MM/YYYY")
                    newRow1("BIRTH_PROVINCE_NAME") = item.GetDataKeyValue("BIRTH_PROVINCE_NAME")
                    newRow1("PER_EMAIL") = item.GetDataKeyValue("PER_EMAIL")
                    newRow1("ID_NO") = item.GetDataKeyValue("ID_NO")
                    dtCanNot.Rows.Add(newRow1)

                    'Dim idCandidate As Int32 = Int32.Parse(item.GetDataKeyValue("ID").ToString())
                    'If store.Check_Exams_IsExist(Decimal.Parse(hidID.Value), idCandidate) > 0 Then
                    '    ShowMessage("Thông tin kết quả điểm thi/pv của ứng viên đã tồn tại. Vui lòng kiểm tra lại!", NotifyType.Warning)
                    '    Exit Sub
                    'End If

                    ''delete candidate --> program schedule candidate
                    'store.DELETE_PRO_SCHEDULE_CAN(Decimal.Parse(hidID.Value), idCandidate)
                    Dim s = (From q In dtCan Where q("ID") = item.GetDataKeyValue("ID")).FirstOrDefault
                    dtCan.Rows.Remove(s)
                Next
        End Select
        rgCanNotSchedule.DataSource = dtCanNot
        rgCanSchedule.DataSource = dtCan
        rgCanSchedule.Rebind()
        rgCanNotSchedule.Rebind()
        If rgCanSchedule.Items.Count = 0 Then
            LoadExamsList()
            rlbExams.Enabled = True
        Else
            rlbExams.Enabled = False
        End If

    End Sub

    Private Sub rlbExams_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rlbExams.SelectedIndexChanged
        getCanNotSchedule()
        rgCanNotSchedule.Rebind()
    End Sub

    Private Sub chkFillter_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkFillter.CheckedChanged
        getCanNotSchedule()
        rgCanNotSchedule.Rebind()
    End Sub
End Class
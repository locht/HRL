﻿Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Common
Imports Telerik.Web.UI
Imports Common.Common
Imports Common.CommonMessage
Imports System.IO

Public Class ctrlRC_CandidateList

    Inherits Common.CommonView

    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
#Region "Properties"
    ' Không dùng nên khóa lại
    'Private Property CandidateList As List(Of CandidateDTO)
    '    Get
    '        Return ViewState(Me.ID & "_CandidateList")
    '    End Get
    '    Set(ByVal value As List(Of CandidateDTO))
    '        ViewState(Me.ID & "_CandidateList") = value
    '    End Set
    'End Property
    Private Property sender As String
        Get
            Return ViewState(Me.ID & "_sender")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_sender") = value
        End Set
    End Property
    Dim str As Integer
    ' Không dùng nên khóa lại
    'Public Property _filter As CandidateDTO
    '    Get
    '        If PageViewState(Me.ID & "_filter") Is Nothing Then
    '            PageViewState(Me.ID & "_filter") = New CandidateDTO
    '        End If
    '        Return PageViewState(Me.ID & "_filter")
    '    End Get
    '    Set(ByVal value As CandidateDTO)
    '        PageViewState(Me.ID & "_filter") = value
    '    End Set
    'End Property

    Private Property lstData As List(Of CandidateDTO)
        Get
            Return ViewState(Me.ID & "_lstData")
        End Get
        Set(ByVal value As List(Of CandidateDTO))
            ViewState(Me.ID & "_lstData") = value
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
            hidProgramID.Value = Request.Params("PROGRAM_ID")
            UpdateControlState()
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgCandidateList.AllowCustomPaging = True
            rgCandidateList.PageSize = Common.Common.DefaultPageSize
            rgCandidateList.ClientSettings.EnablePostBackOnRowClick = False
            rgCandidateList.SetFilter()
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Delete, ToolbarItem.Export, ToolbarItem.Import)
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                Dim rep As New RecruitmentRepository
                Dim objPro = rep.GetProgramByID(New ProgramDTO With {.ID = Decimal.Parse(hidProgramID.Value)})
                lblOrgName.Text = objPro.ORG_NAME
                hidOrg.Value = objPro.ORG_ID
                lblTitleName.Text = objPro.TITLE_NAME
                hidTitle.Value = objPro.TITLE_ID
                lblSendDate.Text = objPro.SEND_DATE
                lblCode.Text = objPro.CODE
                lblJobName.Text = objPro.JOB_NAME
                lblRequestNumber.Text = objPro.NUMBERRECRUITMENT
                lblQuantityHasRecruitment.Text = objPro.CANDIDATE_RECEIVED
                lblStatusRequest.Text = objPro.STATUS_NAME
                lblReasonRecruitment.Text = objPro.RECRUIT_REASON
                lblOtherRequest.Text = objPro.REQUESTOTHER
                lblExperienceRequired.Text = objPro.REQUEST_EXPERIENCE
                CurrentState = CommonMessage.STATE_NORMAL
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Public Overrides Sub UpdateControlState()
        Try

            If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
            End If
            Select Case isLoadPopup
                Case 1
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.MustHaveContract = False
                    ctrlFindEmployeePopup.LoadAllOrganization = True
                    phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = False
            End Select
        Catch ex As Exception
            Throw ex
        End Try
        ChangeToolbarState()
    End Sub


#End Region

#Region "Event"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            Me.sender = "btnSearch"
            rgCandidateList.CurrentPageIndex = 0
            rgCandidateList.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Dim objCandidate As New CandidateDTO
            Dim rep As New RecruitmentRepository

            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case TOOLBARITEM_DELETE
                    'Kiểm tra các điều kiện để xóa.
                    If rgCandidateList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi xóa"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case TOOLBARITEM_EXPORT
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('RC_CANDIDATE_IMPORT');", True)

                Case TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
            End Select
            'UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnBlacklist_Click(sender As Object, e As System.EventArgs) Handles btnBlacklist.Click

        ctrlMessageBox.MessageText = Translate("Bạn có chắc chắn muốn chuyển trạng thái Blacklist cho các ứng viên?")
        ctrlMessageBox.ActionName = "BLACKLIST"
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub

    Private Sub btnDuDieuKien_Click(sender As Object, e As System.EventArgs) Handles btnDuDieuKien.Click

        For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
            Dim status_id As String = dr.GetDataKeyValue("STATUS_ID")
            Select Case status_id
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.GUITHU_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
                    Exit Sub
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.LANHANVIEN_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    Exit Sub
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.THIDAT_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Thi đạt"), NotifyType.Warning)
                    Exit Sub
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.TRUNGTUYEN_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Trúng tuyển"), NotifyType.Warning)
                    Exit Sub
            End Select
        Next
        'Hiển thị Confirm delete.
        ctrlMessageBox.MessageText = Translate("Bạn có chắc chắn muốn chuyển trạng thái Đủ điều kiện cho các ứng viên?")
        ctrlMessageBox.ActionName = "DUDIEUKIEN"
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub

    Private Sub btnKhongDuDieuKien_Click(sender As Object, e As System.EventArgs) Handles btnKhongDuDieuKien.Click
        For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
            Dim status_id As String = dr.GetDataKeyValue("STATUS_ID")
            Select Case status_id
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.DUDIEUKIEN_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đủ điều kiện"), NotifyType.Warning)
                    Exit Sub
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.GUITHU_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
                    Exit Sub
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.LANHANVIEN_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    Exit Sub
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.THIDAT_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Thi đạt"), NotifyType.Warning)
                    Exit Sub
                Case RecruitmentCommon.RC_CANDIDATE_STATUS.TRUNGTUYEN_ID
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Trúng tuyển"), NotifyType.Warning)
                    Exit Sub
            End Select
        Next
        'Hiển thị Confirm delete.
        ctrlMessageBox.MessageText = Translate("Bạn có chắc chắn muốn chuyển trạng thái Không đủ điều kiện cho các ứng viên?")
        ctrlMessageBox.ActionName = "KHONGDUDIEUKIEN"
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then

                'Kiểm tra các điều kiện trước khi xóa
                Dim lstCanID As New List(Of Decimal)
                For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
                    lstCanID.Add(dr.GetDataKeyValue("ID"))
                Next
                Using rep As New RecruitmentRepository
                    'Xóa nhân viên.
                    Select Case e.ActionName
                        Case CommonMessage.TOOLBARITEM_DELETE
                            Dim strError As String = ""
                            rep.DeleteCandidate(lstCanID, strError)
                            If strError = "" Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            Else
                                ShowMessage(Translate("Tồn tại ứng viên đã tham gia thi tuyển") & strError.Substring(1, strError.Length - 1), Utilities.NotifyType.Error)
                            End If
                        Case "DUDIEUKIEN"
                            If rep.UpdateStatusCandidate(lstCanID, RecruitmentCommon.RC_CANDIDATE_STATUS.DUDIEUKIEN_ID) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Success)
                            End If
                        Case "KHONGDUDIEUKIEN"
                            If rep.UpdateStatusCandidate(lstCanID, RecruitmentCommon.RC_CANDIDATE_STATUS.KHONGDUDIEUKIEN_ID) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Success)
                            End If
                        Case "BLACKLIST"
                            If rep.UpdateBlackListCandidate(lstCanID, True) Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Success)
                            End If
                    End Select
                    rgCandidateList.Rebind()
                End Using


            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgCandidateList.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub RadGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgCandidateList.PageIndexChanged
        Try
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Try
            Dim url = e.Argument
            Dim code = url.Split(";")(0)
            Select Case code
                Case "TRANSFER"
                    Dim lstCanID As New List(Of Decimal)
                    Using rep As New RecruitmentRepository
                        For Each item As GridDataItem In rgCandidateList.SelectedItems
                            lstCanID.Add(item.GetDataKeyValue("ID"))
                        Next
                        If rep.UpdateProgramCandidate(lstCanID, url.Split(";")(1)) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            rgCandidateList.Rebind()
                        End If
                    End Using
                Case "PONTENTIAL_TRANSFER"
                    Dim lstCanID As New List(Of Decimal)
                    lstCanID.Add(url.Split(";")(1))
                    Using rep As New RecruitmentRepository
                        If rep.UpdateProgramCandidate(lstCanID, hidProgramID.Value) Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            rgCandidateList.Rebind()
                        End If
                    End Using
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
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
                If workbook.Worksheets.GetSheetByCodeName("ImportCandidate") Is Nothing Then
                    ShowMessage("File mẫu không đúng định dạng", NotifyType.Warning)
                    Exit Sub
                End If
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(3, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))

                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            Dim dtData As DataTable
            For Each dt As DataTable In dsDataPrepare.Tables
                If dtData Is Nothing Then
                    dtData = dt.Clone
                End If
                For Each row In dt.Rows
                    Dim isRow = ImportValidate.TrimRow(row)
                    If Not isRow Then
                        Continue For
                    End If
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
            Dim iRow As Integer = 5
            Dim IsError As Boolean = False
            For Each row In dtData.Rows
                Dim sError As String = ""
                Dim rowError = dtError.NewRow
                Dim isRow = ImportValidate.TrimRow(row)
                Dim isScpExist As Boolean = False
                If Not isRow Then
                    iRow += 1
                    Continue For
                End If
                sError = "Họ tên chưa nhập"
                ImportValidate.EmptyValue("FIRST_NAME_VN", row, rowError, IsError, sError)
                sError = "Tên chưa nhập"
                ImportValidate.EmptyValue("LAST_NAME_VN", row, rowError, IsError, sError)
                sError = "Giới tính nhập sai định dạng"
                ImportValidate.IsValidNumber("GENDER", row, rowError, IsError, sError)

                If row("MARITAL_STATUS_NAME").ToString <> "" Then
                    sError = "Mã tình trạng hôn nhân sai định dạng"
                    ImportValidate.IsValidNumber("MARITAL_STATUS", row, rowError, IsError, sError)
                Else
                    row("MARITAL_STATUS") = DBNull.Value
                End If

                If row("NATIVE_NAME").ToString <> "" Then
                    sError = "Mã dân tộc sai định dạng"
                    ImportValidate.IsValidNumber("NATIVE", row, rowError, IsError, sError)
                Else
                    row("NATIVE") = DBNull.Value
                End If

                sError = ""
                ImportValidate.IsValidDate("BIRTH_DATE", row, rowError, IsError, sError)

                sError = "Mã quốc gia nhập sai định dạng"
                ImportValidate.IsValidNumber("BIRTH_NATION_ID", row, rowError, IsError, sError)

                sError = "Mã nơi sinh nhập sai định dạng"
                ImportValidate.IsValidNumber("BIRTH_PROVINCE", row, rowError, IsError, sError)

                sError = "Mã Tỉnh/Thành phố nhập sai định dạng"
                ImportValidate.IsValidNumber("NAV_PROVINCE", row, rowError, IsError, sError)

                sError = "Mã Quốc tịch nhập sai định dạng"
                ImportValidate.IsValidNumber("NATIONALITY_ID", row, rowError, IsError, sError)

                If row("RELIGION_NAME").ToString <> "" Then
                    sError = "Mã Tôn giáo sai định dạng"
                    ImportValidate.IsValidNumber("RELIGION", row, rowError, IsError, sError)
                Else
                    row("RELIGION") = DBNull.Value
                End If

                sError = "Số CMND nhập sai định dạng"
                ImportValidate.IsValidNumber("ID_NO", row, rowError, IsError, sError)
                If rowError("ID_NO").ToString = "" Then
                    Using rep As New RecruitmentRepository
                        Dim isValid = rep.ValidateInsertCandidate("", row("ID_NO"), "", Date.Now, "NO_ID")
                        If Not isValid Then
                            rowError("ID_NO") = "CMND này đã tồn tại"
                        End If
                        If rowError("ID_NO").ToString = "" Then
                            isValid = rep.ValidateInsertCandidate("", row("ID_NO"), "", Date.Now, "BLACK_LIST")
                            If Not isValid Then
                                rowError("ID_NO") = "Ứng viên thuộc danh sách đen"
								IsError = True
							End If
                        End If
                        If rowError("ID_NO").ToString = "" And
                            rowError("FIRST_NAME_VN").ToString = "" And
                            rowError("LAST_NAME_VN").ToString = "" And
                            rowError("BIRTH_DATE").ToString = "" Then
                            isValid = rep.ValidateInsertCandidate("",
                                                               row("ID_NO"),
                                                               row("FIRST_NAME_VN").ToString & " " & row("LAST_NAME_VN").ToString,
                                                               Date.Parse(row("BIRTH_DATE").ToString),
                                                               "DATE_FULLNAME")
                            If Not isValid Then
                                rowError("ID_NO") = "Ứng viên thuộc danh sách đen"
                                IsError = True
                            End If
                        End If
                    End Using
                End If
                sError = ""
                ImportValidate.IsValidDate("ID_DATE", row, rowError, IsError, sError)

                If row("ID_DATE_EXPIRATION").ToString <> "" Then
                    sError = ""
                    ImportValidate.IsValidDate("ID_DATE_EXPIRATION", row, rowError, IsError, sError)
                End If

                If rowError("ID_DATE").ToString = "" And _
                    rowError("ID_DATE_EXPIRATION").ToString = "" And _
                    row("ID_DATE").ToString <> "" And _
                    row("ID_DATE_EXPIRATION").ToString <> "" Then
                    Dim startdate = Date.Parse(row("ID_DATE"))
                    Dim enddate = Date.Parse(row("ID_DATE_EXPIRATION"))
                    If startdate > enddate Then
                        rowError("ID_DATE_EXPIRATION") = "Ngày hết hạn CMND phải lớn hơn Ngày cấp"
                        IsError = True
                    End If
                End If


                sError = "Mã Nơi cấp nhập sai định dạng"
                ImportValidate.IsValidNumber("ID_PLACE", row, rowError, IsError, sError)

                ImportValidate.CheckNumber("IS_RESIDENT", row, 0)

                sError = "Mã Nguyên quán nhập sai định dạng"
                ImportValidate.IsValidNumber("NAV_NATION_ID", row, rowError, IsError, sError)

                sError = "Địa chỉ thường trú chưa nhập"
                ImportValidate.EmptyValue("PER_ADDRESS", row, rowError, IsError, sError)

                sError = "Mã Quốc gia nhập sai định dạng"
                ImportValidate.IsValidNumber("PER_NATION_ID", row, rowError, IsError, sError)

                sError = "Mã Tỉnh/Thành phố nhập sai định dạng"
                ImportValidate.IsValidNumber("PER_PROVINCE", row, rowError, IsError, sError)

                sError = "Mã Quận/huyện nhập sai định dạng"
                ImportValidate.IsValidNumber("PER_PROVINCE", row, rowError, IsError, sError)

                sError = "Địa chỉ tạm trú chưa nhập"
                ImportValidate.EmptyValue("CONTACT_ADDRESS", row, rowError, IsError, sError)

                sError = "Mã Quốc gia nhập sai định dạng"
                ImportValidate.IsValidNumber("CONTACT_NATION_ID", row, rowError, IsError, sError)

                sError = "Mã Tỉnh/Thành phố nhập sai định dạng"
                ImportValidate.IsValidNumber("CONTACT_PROVINCE", row, rowError, IsError, sError)

                sError = "Mã Quận/huyện nhập sai định dạng"
                ImportValidate.IsValidNumber("CONTACT_DISTRICT_ID", row, rowError, IsError, sError)

                If row("PER_TAX_DATE").ToString <> "" Then
                    sError = ""
                    ImportValidate.IsValidDate("PER_TAX_DATE", row, rowError, IsError, sError)
                End If

                If row("PASSPORT_DATE").ToString <> "" Then
                    sError = ""
                    ImportValidate.IsValidDate("PASSPORT_DATE", row, rowError, IsError, sError)
                End If

                If row("PASSPORT_DATE_EXPIRATION").ToString <> "" Then
                    sError = ""
                    ImportValidate.IsValidDate("PASSPORT_DATE_EXPIRATION", row, rowError, IsError, sError)
                End If

                If rowError("PASSPORT_DATE").ToString = "" And _
                    rowError("PASSPORT_DATE_EXPIRATION").ToString = "" And _
                    row("PASSPORT_DATE").ToString <> "" And _
                    row("PASSPORT_DATE_EXPIRATION").ToString <> "" Then
                    Dim startdate = Date.Parse(row("PASSPORT_DATE"))
                    Dim enddate = Date.Parse(row("PASSPORT_DATE_EXPIRATION"))
                    If startdate > enddate Then
                        rowError("PASSPORT_DATE_EXPIRATION") = "Ngày hết hạn phải lớn hơn Ngày cấp"
                        IsError = True
                    End If
                End If

                If row("VISA_DATE").ToString <> "" Then
                    sError = ""
                    ImportValidate.IsValidDate("VISA_DATE", row, rowError, IsError, sError)
                End If

                If row("VISA_DATE_EXPIRATION").ToString <> "" Then
                    sError = ""
                    ImportValidate.IsValidDate("VISA_DATE_EXPIRATION", row, rowError, IsError, sError)
                End If

                If rowError("VISA_DATE").ToString = "" And _
                    rowError("VISA_DATE_EXPIRATION").ToString = "" And _
                    row("VISA_DATE").ToString <> "" And _
                    row("VISA_DATE_EXPIRATION").ToString <> "" Then
                    Dim startdate = Date.Parse(row("VISA_DATE"))
                    Dim enddate = Date.Parse(row("VISA_DATE_EXPIRATION"))
                    If startdate > enddate Then
                        rowError("VISA_DATE_EXPIRATION") = "Ngày hết hạn phải lớn hơn Ngày cấp"
                        IsError = True
                    End If
                End If

                If row("VNAIRLINES_DATE").ToString <> "" Then
                    sError = ""
                    ImportValidate.IsValidDate("VNAIRLINES_DATE", row, rowError, IsError, sError)
                End If

                If row("VNAIRLINES_DATE_EXPIRATION").ToString <> "" Then
                    sError = ""
                    ImportValidate.IsValidDate("VNAIRLINES_DATE_EXPIRATION", row, rowError, IsError, sError)
                End If

                If rowError("VNAIRLINES_DATE").ToString = "" And _
                    rowError("VNAIRLINES_DATE_EXPIRATION").ToString = "" And _
                    row("VNAIRLINES_DATE").ToString <> "" And _
                    row("VNAIRLINES_DATE_EXPIRATION").ToString <> "" Then
                    Dim startdate = Date.Parse(row("VNAIRLINES_DATE"))
                    Dim enddate = Date.Parse(row("VNAIRLINES_DATE_EXPIRATION"))
                    If startdate > enddate Then
                        rowError("VNAIRLINES_DATE_EXPIRATION") = "Ngày hết hạn phải lớn hơn Ngày cấp"
                        IsError = True
                    End If
                End If

                If row("LABOUR_DATE").ToString <> "" Then
                    sError = ""
                    ImportValidate.IsValidDate("LABOUR_DATE", row, rowError, IsError, sError)
                End If

                If row("LABOUR_DATE_EXPIRATION").ToString <> "" Then
                    sError = ""
                    ImportValidate.IsValidDate("LABOUR_DATE_EXPIRATION", row, rowError, IsError, sError)
                End If

                If rowError("LABOUR_DATE").ToString = "" And _
                    rowError("LABOUR_DATE_EXPIRATION").ToString = "" And _
                    row("LABOUR_DATE").ToString <> "" And _
                    row("LABOUR_DATE_EXPIRATION").ToString <> "" Then
                    Dim startdate = Date.Parse(row("LABOUR_DATE"))
                    Dim enddate = Date.Parse(row("LABOUR_DATE_EXPIRATION"))
                    If startdate > enddate Then
                        rowError("LABOUR_DATE_EXPIRATION") = "Ngày hết hạn phải lớn hơn Ngày cấp"
                        IsError = True
                    End If
                End If

                If row("WORK_PERMIT_START").ToString <> "" Then
                    sError = ""
                    ImportValidate.IsValidDate("WORK_PERMIT_START", row, rowError, IsError, sError)
                End If

                If row("WORK_PERMIT_END").ToString <> "" Then
                    sError = ""
                    ImportValidate.IsValidDate("WORK_PERMIT_END", row, rowError, IsError, sError)
                End If

                If rowError("WORK_PERMIT_START").ToString = "" And _
                    rowError("WORK_PERMIT_END").ToString = "" And _
                    row("WORK_PERMIT_START").ToString <> "" And _
                    row("WORK_PERMIT_END").ToString <> "" Then
                    Dim startdate = Date.Parse(row("WORK_PERMIT_START"))
                    Dim enddate = Date.Parse(row("WORK_PERMIT_END"))
                    If startdate > enddate Then
                        rowError("WORK_PERMIT_END") = "Ngày hết hạn phải lớn hơn Ngày cấp"
                        IsError = True
                    End If
                End If

                If row("TEMP_RESIDENCE_CARD_START").ToString <> "" Then
                    sError = ""
                    ImportValidate.IsValidDate("TEMP_RESIDENCE_CARD_START", row, rowError, IsError, sError)
                End If

                If row("TEMP_RESIDENCE_CARD_END").ToString <> "" Then
                    sError = ""
                    ImportValidate.IsValidDate("TEMP_RESIDENCE_CARD_END", row, rowError, IsError, sError)
                End If

                If rowError("TEMP_RESIDENCE_CARD_START").ToString = "" And _
                    rowError("TEMP_RESIDENCE_CARD_END").ToString = "" And _
                    row("TEMP_RESIDENCE_CARD_START").ToString <> "" And _
                    row("TEMP_RESIDENCE_CARD_END").ToString <> "" Then
                    Dim startdate = Date.Parse(row("TEMP_RESIDENCE_CARD_START"))
                    Dim enddate = Date.Parse(row("TEMP_RESIDENCE_CARD_END"))
                    If startdate > enddate Then
                        rowError("TEMP_RESIDENCE_CARD_END") = "Đến ngày phải lớn hơn Từ ngày"
                        IsError = True
                    End If
                End If

                If row("ACADEMY_NAME").ToString <> "" Then
                    sError = "Mã Trình độ văn hóa sai định dạng"
                    ImportValidate.IsValidNumber("ACADEMY", row, rowError, IsError, sError)
                Else
                    row("ACADEMY") = DBNull.Value
                End If


                If row("LEARNING_LEVEL_NAME").ToString <> "" Then
                    sError = "Mã Trình độ văn hóa sai định dạng"
                    ImportValidate.IsValidNumber("LEARNING_LEVEL", row, rowError, IsError, sError)
                Else
                    row("LEARNING_LEVEL") = DBNull.Value
                End If


                If row("FIELD_NAME").ToString <> "" Then
                    sError = "Mã Trình độ chuyên môn sai định dạng"
                    ImportValidate.IsValidNumber("FIELD", row, rowError, IsError, sError)
                Else
                    row("FIELD") = DBNull.Value
                End If

                If row("SCHOOL_NAME").ToString <> "" Then
                    sError = "Mã Trường học sai định dạng"
                    ImportValidate.IsValidNumber("SCHOOL", row, rowError, IsError, sError)
                Else
                    row("SCHOOL") = DBNull.Value
                End If

                If row("MAJOR_NAME").ToString <> "" Then
                    sError = "Mã Chuyên ngành sai định dạng"
                    ImportValidate.IsValidNumber("MAJOR", row, rowError, IsError, sError)
                Else
                    row("MAJOR") = DBNull.Value
                End If

                If row("DEGREE_NAME").ToString <> "" Then
                    sError = "Mã Bằng cấp sai định dạng"
                    ImportValidate.IsValidNumber("DEGREE", row, rowError, IsError, sError)
                Else
                    row("DEGREE") = DBNull.Value
                End If

                If row("MARK_EDU_NAME").ToString <> "" Then
                    sError = "Mã Xếp loại sai định dạng"
                    ImportValidate.IsValidNumber("MARK_EDU", row, rowError, IsError, sError)
                Else
                    row("MARK_EDU") = DBNull.Value
                End If

                If row("GPA").ToString <> "" Then
                    sError = "Điểm tốt nghiệp sai định dạng"
                    ImportValidate.IsValidNumber("GPA", row, rowError, IsError, sError)
                Else
                    row("GPA") = DBNull.Value
                End If

                If row("IT_LEVEL_NAME").ToString <> "" Then
                    sError = "Mã Trình độ 1 sai định dạng"
                    ImportValidate.IsValidNumber("IT_LEVEL", row, rowError, IsError, sError)
                Else
                    row("IT_LEVEL") = DBNull.Value
                End If

                If row("IT_LEVEL1_NAME").ToString <> "" Then
                    sError = "Mã Trình độ 2 sai định dạng"
                    ImportValidate.IsValidNumber("IT_LEVEL1", row, rowError, IsError, sError)
                Else
                    row("IT_LEVEL1") = DBNull.Value
                End If

                If row("IT_LEVEL2_NAME").ToString <> "" Then
                    sError = "Mã Trình độ 3 sai định dạng"
                    ImportValidate.IsValidNumber("IT_LEVEL2", row, rowError, IsError, sError)
                Else
                    row("IT_LEVEL2") = DBNull.Value
                End If

                If row("ENGLISH_LEVEL_NAME").ToString <> "" Then
                    sError = "Mã Trình độ 1 sai định dạng"
                    ImportValidate.IsValidNumber("ENGLISH_LEVEL", row, rowError, IsError, sError)
                Else
                    row("ENGLISH_LEVEL") = DBNull.Value
                End If

                If row("ENGLISH_MARK").ToString <> "" Then
                    sError = "Điểm 1 sai định dạng"
                    ImportValidate.IsValidNumber("GPA", row, rowError, IsError, sError)
                Else
                    row("ENGLISH_MARK") = DBNull.Value
                End If

                If row("ENGLISH_LEVEL1_NAME").ToString <> "" Then
                    sError = "Mã Trình độ 2 sai định dạng"
                    ImportValidate.IsValidNumber("ENGLISH_LEVEL1", row, rowError, IsError, sError)
                Else
                    row("ENGLISH_LEVEL1") = DBNull.Value
                End If

                If row("ENGLISH_MARK1").ToString <> "" Then
                    sError = "Điểm 2 sai định dạng"
                    ImportValidate.IsValidNumber("GPA", row, rowError, IsError, sError)
                Else
                    row("ENGLISH_MARK1") = DBNull.Value
                End If

                If row("ENGLISH_LEVEL2_NAME").ToString <> "" Then
                    sError = "Mã Trình độ 3 sai định dạng"
                    ImportValidate.IsValidNumber("ENGLISH_LEVEL2", row, rowError, IsError, sError)
                Else
                    row("ENGLISH_LEVEL2") = DBNull.Value
                End If

                If row("ENGLISH_MARK2").ToString <> "" Then
                    sError = "Điểm 3 sai định dạng"
                    ImportValidate.IsValidNumber("GPA", row, rowError, IsError, sError)
                Else
                    row("ENGLISH_MARK2") = DBNull.Value
                End If

                If row("NGAYVAOCONGDOAN").ToString <> "" Then
                    sError = ""
                    ImportValidate.IsValidDate("NGAYVAOCONGDOAN", row, rowError, IsError, sError)
                End If

                ImportValidate.CheckNumber("CONGDOANPHI", row, 0)

                If row("ACCOUNT_NUMBER").ToString <> "" Then
                    sError = "Số TK sai định dạng"
                    ImportValidate.IsValidNumber("ACCOUNT_NUMBER", row, rowError, IsError, sError)
                Else
                    row("ACCOUNT_NUMBER") = DBNull.Value
                End If

                If row("ACCOUNT_EFFECT_DATE").ToString <> "" Then
                    sError = ""
                    ImportValidate.IsValidDate("ACCOUNT_EFFECT_DATE", row, rowError, IsError, sError)
                End If

                If row("BANK_BRANCH_NAME").ToString <> "" Then
                    sError = "Mã Chi nhánh ngân hàng sai định dạng"
                    ImportValidate.IsValidNumber("BANK_BRANCH", row, rowError, IsError, sError)
                Else
                    row("BANK_BRANCH") = DBNull.Value
                End If

                If row("BANK_NAME").ToString <> "" Then
                    sError = "Mã Ngân hàng sai định dạng"
                    ImportValidate.IsValidNumber("BANK", row, rowError, IsError, sError)
                Else
                    row("BANK") = DBNull.Value
                End If

                ImportValidate.CheckNumber("IS_PAYMENT_VIA_BANK", row, 0)

                If IsError Then
                    dtError.Rows.Add(rowError)
                End If
            Next
            If dtError.Rows.Count > 0 Then
                Session("EXPORTREPORT") = dtError
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('RC_CANDIDATE_IMPORT_ERROR')", True)
                ShowMessage(Translate("Giao dịch không thành công, chi tiết lỗi tệp tin đính kèm"), NotifyType.Warning)
            Else
                Dim lst = CreateCanImport(dtData)
                Using rep As New RecruitmentRepository
                    If rep.ImportCandidate(lst) Then
                        ShowMessage(Translate("Import thành công " & lst.Count & " ứng viên"), NotifyType.Success)
                        rgCandidateList.Rebind()
                    End If

                End Using
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function CreateCanImport(dtData As DataTable) As List(Of CandidateImportDTO)
        Dim lst As New List(Of CandidateImportDTO)
        Try
            For Each dr In dtData.Rows
                Dim can_cv As CandidateCVDTO
                Dim can_other As CandidateOtherInfoDTO
                Dim can_edu As New CandidateEduDTO
                Dim can As New CandidateDTO
                Dim canimport As New CandidateImportDTO
                'Candidate
                can.RC_PROGRAM_ID = Decimal.Parse(hidProgramID.Value)
                can.ORG_ID = Decimal.Parse(hidOrg.Value)
                can.TITLE_ID = Decimal.Parse(hidTitle.Value)
                can.FIRST_NAME_VN = dr("FIRST_NAME_VN").ToString
                can.LAST_NAME_VN = dr("LAST_NAME_VN").ToString

                'Candidate CV
                can_cv = New CandidateCVDTO
                can_cv.GENDER = dr("GENDER").ToString
                can_cv.NATIVE = dr("NATIVE").ToString
                can_cv.MARITAL_STATUS = dr("MARITAL_STATUS").ToString
                can_cv.RELIGION = dr("RELIGION").ToString
                can_cv.ID_NO = Decimal.Parse(dr("ID_NO").ToString)

                can_cv.ID_DATE = Date.Parse(dr("ID_DATE").ToString)

                If dr("ID_PLACE").ToString <> "" Then
                    can_cv.ID_PLACE = Decimal.Parse(dr("ID_PLACE").ToString)
                End If

                can_cv.PASSPORT_ID = dr("PASSPORT_ID").ToString
                can_cv.PASSPORT_PLACE_NAME = dr("PASSPORT_PLACE_NAME").ToString
                can_cv.BIRTH_DATE = Date.Parse(dr("BIRTH_DATE").ToString)

                If dr("BIRTH_NATION_ID").ToString <> "" Then
                    can_cv.BIRTH_NATION_ID = Decimal.Parse(dr("BIRTH_NATION_ID"))
                End If
                If dr("BIRTH_PROVINCE").ToString <> "" Then
                    can_cv.BIRTH_PROVINCE = Decimal.Parse(dr("BIRTH_PROVINCE"))
                End If
                If dr("NATIONALITY_ID").ToString <> "" Then
                    can_cv.NATIONALITY_ID = Decimal.Parse(dr("NATIONALITY_ID"))
                End If
                If dr("NAV_NATION_ID").ToString <> "" Then
                    can_cv.NAV_NATION_ID = Decimal.Parse(dr("NAV_NATION_ID"))
                End If
                If dr("NAV_PROVINCE").ToString <> "" Then
                    can_cv.NAV_PROVINCE = Decimal.Parse(dr("NAV_PROVINCE"))
                End If
                can_cv.PER_ADDRESS = dr("PER_ADDRESS").ToString
                If dr("PER_DISTRICT_ID").ToString <> "" Then
                    can_cv.PER_DISTRICT_ID = Decimal.Parse(dr("PER_DISTRICT_ID"))
                End If
                If dr("PER_NATION_ID").ToString <> "" Then
                    can_cv.PER_NATION_ID = Decimal.Parse(dr("PER_NATION_ID"))
                End If
                If dr("PER_PROVINCE").ToString <> "" Then
                    can_cv.PER_PROVINCE = Decimal.Parse(dr("PER_PROVINCE"))
                End If
                can_cv.CONTACT_ADDRESS = dr("CONTACT_ADDRESS")
                If dr("CONTACT_NATION_ID").ToString <> "" Then
                    can_cv.CONTACT_NATION_ID = Decimal.Parse(dr("CONTACT_NATION_ID"))
                End If
                If dr("CONTACT_PROVINCE").ToString <> "" Then
                    can_cv.CONTACT_PROVINCE = Decimal.Parse(dr("CONTACT_PROVINCE"))
                End If
                If dr("CONTACT_DISTRICT_ID").ToString <> "" Then
                    can_cv.CONTACT_DISTRICT_ID = Decimal.Parse(dr("CONTACT_DISTRICT_ID"))
                End If
                If dr("ID_DATE_EXPIRATION").ToString <> "" Then
                    can_cv.ID_DATE_EXPIRATION = Date.Parse(dr("ID_DATE_EXPIRATION"))
                End If

                can_cv.IS_RESIDENT = 0
                If dr("IS_RESIDENT").ToString <> 0 Then
                    can_cv.IS_RESIDENT = Decimal.Parse(dr("IS_RESIDENT"))
                End If


                can_cv.CONTACT_MOBILE = dr("CONTACT_MOBILE").ToString
                can_cv.CONTACT_PHONE = dr("CONTACT_PHONE").ToString
                can_cv.PER_EMAIL = dr("PER_EMAIL").ToString
                can_cv.PERTAXCODE = dr("PERTAXCODE").ToString
                If dr("PER_TAX_DATE").ToString <> "" Then
                    can_cv.PER_TAX_DATE = Date.Parse(dr("PER_TAX_DATE"))
                End If
                can_cv.PER_TAX_PLACE = dr("PER_TAX_PLACE").ToString
                If dr("PASSPORT_DATE").ToString <> "" Then
                    can_cv.PASSPORT_DATE = Date.Parse(dr("PASSPORT_DATE"))
                End If
                If dr("PASSPORT_DATE_EXPIRATION").ToString <> "" Then
                    can_cv.PASSPORT_DATE_EXPIRATION = Date.Parse(dr("PASSPORT_DATE_EXPIRATION"))
                End If
                can_cv.VISA_NUMBER = dr("VISA_NUMBER").ToString
                If dr("VISA_DATE").ToString <> "" Then
                    can_cv.VISA_DATE = Date.Parse(dr("VISA_DATE"))
                End If
                If dr("VISA_DATE_EXPIRATION").ToString <> "" Then
                    can_cv.VISA_DATE_EXPIRATION = Date.Parse(dr("VISA_DATE_EXPIRATION"))
                End If
                can_cv.VISA_PLACE = dr("VISA_PLACE").ToString
                can_cv.VNAIRLINES_NUMBER = dr("VNAIRLINES_NUMBER").ToString
                If dr("VNAIRLINES_DATE").ToString <> "" Then
                    can_cv.VNAIRLINES_DATE = Date.Parse(dr("VNAIRLINES_DATE"))
                End If
                If dr("VNAIRLINES_DATE_EXPIRATION").ToString <> "" Then
                    can_cv.VNAIRLINES_DATE_EXPIRATION = Date.Parse(dr("VNAIRLINES_DATE_EXPIRATION"))
                End If
                can_cv.VNAIRLINES_PLACE = dr("VNAIRLINES_PLACE").ToString
                can_cv.LABOUR_NUMBER = dr("LABOUR_NUMBER").ToString
                If dr("LABOUR_DATE").ToString <> "" Then
                    can_cv.LABOUR_DATE = Date.Parse(dr("LABOUR_DATE"))
                End If
                If dr("LABOUR_DATE_EXPIRATION").ToString <> "" Then
                    can_cv.LABOUR_DATE_EXPIRATION = Date.Parse(dr("LABOUR_DATE_EXPIRATION"))
                End If

                can_cv.LABOUR_PLACE = dr("LABOUR_PLACE").ToString
                can_cv.WORK_PERMIT = dr("WORK_PERMIT").ToString
                If dr("WORK_PERMIT_END").ToString <> "" Then
                    can_cv.WORK_PERMIT_END = Date.Parse(dr("WORK_PERMIT_END"))
                End If
                If dr("WORK_PERMIT_START").ToString <> "" Then
                    can_cv.WORK_PERMIT_START = Date.Parse(dr("WORK_PERMIT_START"))
                End If

                can_cv.TEMP_RESIDENCE_CARD = dr("TEMP_RESIDENCE_CARD")
                If dr("TEMP_RESIDENCE_CARD_START").ToString <> "" Then
                    can_cv.TEMP_RESIDENCE_CARD_START = Date.Parse(dr("TEMP_RESIDENCE_CARD_START"))
                End If
                If dr("TEMP_RESIDENCE_CARD_END").ToString <> "" Then
                    can_cv.TEMP_RESIDENCE_CARD_END = Date.Parse(dr("TEMP_RESIDENCE_CARD_END"))
                End If

                'CanEducation

                If dr("MARK_EDU").ToString <> "" Then
                    can_edu.MARK_EDU = dr("MARK_EDU")
                End If

                If dr("ACADEMY").ToString <> "" Then
                    can_edu.ACADEMY = dr("ACADEMY")
                End If

                If dr("LEARNING_LEVEL").ToString <> "" Then
                    can_edu.LEARNING_LEVEL = dr("LEARNING_LEVEL")
                End If

                If dr("MAJOR").ToString <> "" Then
                    can_edu.MAJOR = dr("MAJOR")
                End If

                If dr("SCHOOL").ToString <> "" Then
                    can_edu.SCHOOL = dr("SCHOOL")
                End If

                If dr("FIELD").ToString <> "" Then
                    can_edu.FIELD = dr("FIELD")
                End If

                If dr("DEGREE").ToString <> "" Then
                    can_edu.DEGREE = dr("DEGREE")
                End If

                If dr("GPA").ToString <> "" Then
                    can_edu.GPA = Decimal.Parse(dr("GPA"))
                End If

                can_edu.IT_CERTIFICATE = dr("IT_CERTIFICATE")
                If dr("IT_LEVEL").ToString <> "" Then
                    can_edu.IT_LEVEL = dr("IT_LEVEL")
                End If
                can_edu.IT_MARK = dr("IT_MARK").ToString

                can_edu.IT_CERTIFICATE1 = dr("IT_CERTIFICATE1").ToString
                If dr("IT_LEVEL1").ToString <> "" Then
                    can_edu.IT_LEVEL1 = dr("IT_LEVEL1")
                End If
                can_edu.IT_MARK1 = dr("IT_MARK1").ToString

                can_edu.IT_CERTIFICATE2 = dr("IT_CERTIFICATE2").ToString
                If dr("IT_LEVEL2").ToString <> "" Then
                    can_edu.IT_LEVEL2 = dr("IT_LEVEL2").ToString
                End If
                can_edu.IT_MARK2 = dr("IT_MARK2").ToString

                can_edu.ENGLISH = dr("ENGLISH").ToString
                If dr("ENGLISH_LEVEL").ToString <> "" Then
                    can_edu.ENGLISH_LEVEL = dr("ENGLISH_LEVEL")
                End If
                If dr("ENGLISH_MARK").ToString <> "" Then
                    can_edu.ENGLISH_MARK = dr("ENGLISH_MARK")
                End If
                can_edu.ENGLISH1 = dr("ENGLISH1").ToString
                If dr("ENGLISH_LEVEL1").ToString <> "" Then
                    can_edu.ENGLISH_LEVEL1 = dr("ENGLISH_LEVEL1")
                End If
                If dr("ENGLISH_MARK1").ToString <> "" Then
                    can_edu.ENGLISH_MARK1 = dr("ENGLISH_MARK1")
                End If
                can_edu.ENGLISH2 = dr("ENGLISH2").ToString
                If dr("ENGLISH_LEVEL2").ToString <> "" Then
                    can_edu.ENGLISH_LEVEL2 = dr("ENGLISH_LEVEL2")
                End If
                If dr("ENGLISH_MARK2").ToString <> "" Then
                    can_edu.ENGLISH_MARK2 = dr("ENGLISH_MARK2")
                End If

                'Candidate Other
                can_other = New CandidateOtherInfoDTO

                can_other.DOAN_PHI = 0
                If dr("CONGDOANPHI").ToString <> 0 Then
                    can_other.DOAN_PHI = -1
                End If
                If dr("NGAYVAOCONGDOAN").ToString <> "" Then
                    can_other.NGAY_VAO_DOAN = Date.Parse(dr("NGAYVAOCONGDOAN"))
                End If
                can_other.NOI_VAO_DOAN = dr("NOIVAOCONGDOAN")
                can_other.ACCOUNT_NAME = dr("ACCOUNT_NAME")
                If dr("ACCOUNT_NUMBER").ToString <> "" Then
                    can_other.ACCOUNT_NUMBER = Decimal.Parse(dr("ACCOUNT_NUMBER"))
                End If

                can_other.BANK = dr("BANK").ToString
                can_other.BANK_BRANCH = dr("BANK_BRANCH").ToString
                can_other.IS_PAYMENT_VIA_BANK = 0
                If dr("IS_PAYMENT_VIA_BANK").ToString <> 0 Then
                    can_other.IS_PAYMENT_VIA_BANK = 1
                End If

                If dr("ACCOUNT_EFFECT_DATE").ToString <> "" Then
                    can_other.ACCOUNT_EFFECT_DATE = Date.Parse(dr("ACCOUNT_EFFECT_DATE"))
                End If
                canimport.can = can
                canimport.can_cv = can_cv
                canimport.can_edu = can_edu
                canimport.can_other = can_other
                lst.Add(canimport)
            Next

            Return lst
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'Protected Sub btnHSNVTransfer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnHSNVTransfer.Click
    '    Try
    '        isLoadPopup = 1
    '        UpdateControlState()
    '        ctrlFindEmployeePopup.Show()
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try

    'End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Try
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count > 0 Then
                Using rep As New RecruitmentRepository
                    If rep.TransferHSNVToCandidate(lstCommonEmployee(0).EMPLOYEE_ID, hidOrg.Value, hidTitle.Value, hidProgramID.Value) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        rgCandidateList.Rebind()
                    End If
                End Using

            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFindEmployeePopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub

#End Region

#Region "Custom"
    Private Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Try
            Dim rep As New RecruitmentRepository
            Dim _filter As New CandidateDTO
            'Mã nhân viên
            ' Không dùng nên tạm khóa
            '_filter.CANDIDATE_CODE = txtCandidateCode.Text.Trim()
            SetValueObjectByRadGrid(rgCandidateList, _filter)
            If hidProgramID.Value <> "" Then
                _filter.RC_PROGRAM_ID = Decimal.Parse(hidProgramID.Value)

            End If

            ' Set params filter
            If chkCandidateUnsatisfactory.Checked Then
                _filter.KHONGDUDIEUKIEN_ID = RecruitmentCommon.RC_CANDIDATE_STATUS.KHONGDUDIEUKIEN_ID
            Else
                _filter.KHONGDUDIEUKIEN_ID = ""
            End If
            If chkCandidateQualified.Checked Then
                _filter.DUDIEUKIEN_ID = RecruitmentCommon.RC_CANDIDATE_STATUS.DUDIEUKIEN_ID
            Else
                _filter.DUDIEUKIEN_ID = ""
            End If
            If chkElectCandidate.Checked Then
                _filter.TRUNGTUYEN_ID = RecruitmentCommon.RC_CANDIDATE_STATUS.TRUNGTUYEN_ID
            Else
                _filter.TRUNGTUYEN_ID = ""
            End If
            If chkCandidatePotential.Checked Then
                _filter.PONTENTIAL = RecruitmentCommon.RC_CANDIDATE_STATUS.PONTENTIAL
            Else
                _filter.PONTENTIAL = ""
            End If
            If chkCandidateCancel.Checked Then
                _filter.TUCHOI_ID = RecruitmentCommon.RC_CANDIDATE_STATUS.TUCHOI_ID
            Else
                _filter.TUCHOI_ID = ""
            End If
            If chkCandidateHavesentmail.Checked Then
                _filter.GUITHU_ID = RecruitmentCommon.RC_CANDIDATE_STATUS.GUITHU_ID
            Else
                _filter.GUITHU_ID = ""
            End If
            If chkCandiateIsEmp.Checked Then
                _filter.LANHANVIEN_ID = RecruitmentCommon.RC_CANDIDATE_STATUS.LANHANVIEN_ID
            Else
                _filter.LANHANVIEN_ID = ""
            End If
            If chkCandidateIsLocaltion.Checked Then
                _filter.NOIBO_ID = RecruitmentCommon.RC_CANDIDATE_STATUS.NOIBO_ID
            Else
                _filter.NOIBO_ID = ""
            End If

            Dim MaximumRows As Integer
            
            Dim Sorts As String = rgCandidateList.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As List(Of CandidateDTO)
            If Not isFull Then
                If Sorts IsNot Nothing Then
                    lstData = rep.GetListCandidatePaging(rgCandidateList.CurrentPageIndex, _
                                                                rgCandidateList.PageSize, _
                                                                MaximumRows, _
                                                                _filter, _
                                                                Sorts)
                Else
                    lstData = rep.GetListCandidatePaging(rgCandidateList.CurrentPageIndex, _
                                                                rgCandidateList.PageSize, _
                                                                MaximumRows, _filter)
                End If

                rgCandidateList.VirtualItemCount = MaximumRows

                If lstData IsNot Nothing Then
                    rgCandidateList.DataSource = lstData
                Else
                    rgCandidateList.DataSource = New List(Of CandidateDTO)
                End If
                'Else
                '    If Sorts IsNot Nothing Then
                '        Return rep.GetListCandidate(_filter, Sorts).ToTable
                '    Else
                '        Return rep.GetListCandidate(_filter).ToTable
                '    End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

#End Region

End Class
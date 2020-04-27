Imports System.Security.Authentication.ExtendedProtection
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Common
Imports Telerik.Web.UI
Imports System.IO
Imports Common.CommonBusiness

Public Class ctrlRC_CandidateTransferList
    Inherits Common.CommonView
    Private Property psp As New RecruitmentRepository
    Protected WithEvents ctrlFindProgramDialog As New ctrlFindProgramPopupDialog

#Region "Properties"
    Private Property CandidateList As List(Of CandidateDTO)
        Get
            Return ViewState(Me.ID & "_CandidateList")
        End Get
        Set(ByVal value As List(Of CandidateDTO))
            ViewState(Me.ID & "_CandidateList") = value
        End Set
    End Property
    Private Property sender As String
        Get
            Return ViewState(Me.ID & "_sender")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_sender") = value
        End Set
    End Property
    Dim str As Integer
    Public Property _filter As CandidateDTO
        Get
            If PageViewState(Me.ID & "_filter") Is Nothing Then
                PageViewState(Me.ID & "_filter") = New CandidateDTO
            End If
            Return PageViewState(Me.ID & "_filter")
        End Get
        Set(ByVal value As CandidateDTO)
            PageViewState(Me.ID & "_filter") = value
        End Set
    End Property

    Private Property lstData As List(Of CandidateDTO)
        Get
            Return ViewState(Me.ID & "_lstData")
        End Get
        Set(ByVal value As List(Of CandidateDTO))
            ViewState(Me.ID & "_lstData") = value
        End Set
    End Property

    Property ListComboData As Recruitment.RecruitmentBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Recruitment.RecruitmentBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    Private Property strStatus As String
        Get
            Return ViewState(Me.ID & "_strStatus")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_strStatus") = value
        End Set
    End Property

    Private Property strIDCandidate As String
        Get
            Return ViewState(Me.ID & "_strIDCandidate")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_strIDCandidate") = value
        End Set
    End Property
#End Region
    Public WithEvents AjaxManager As RadAjaxManager
#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            hidProgramID.Value = Request.Params("PROGRAM_ID")
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgCandidateList.AllowCustomPaging = True
            rgCandidateList.PageSize = Common.Common.DefaultPageSize
            rgCandidateList.SetFilter()

            rgResult.AllowCustomPaging = True
            rgResult.PageSize = Common.Common.DefaultPageSize
            rgResult.SetFilter()
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            'tbarMainToolBar.Visible = False
            'rgCandidateList.ClientSettings.EnablePostBackOnRowClick = True
            'rgAspiration.ClientSettings.EnablePostBackOnRowClick = True
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save)
            CType(MainToolBar.Items(0), RadToolBarButton).Text = "Lưu thông tin thỏa thuận"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            'Load danh sach trang thai ung vien len rad listbox
            LoadDataRadlistBox()
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
                For Each item As RadListBoxItem In rlbStatus.Items
                    If item.Value = RCContant.DAT Then
                        item.Checked = True
                        strStatus = strStatus & item.Value & ","
                        Exit For
                    End If
                Next
                    Dim rep As New RecruitmentRepository
                    Dim objPro = rep.GetProgramByID(New ProgramDTO With {.ID = Decimal.Parse(hidProgramID.Value)})
                    lblOrgName.Text = objPro.ORG_NAME
                    hidOrg.Value = objPro.ORG_ID
                    lblTitleName.Text = objPro.TITLE_NAME
                    hidTitle.Value = objPro.TITLE_ID
                    lblSendDate.Text = objPro.SEND_DATE.Value.ToString("dd/MM/yyyy")
                    lblRequestNumber.Text = objPro.REQUEST_NUMBER
                    lblNumberHaveRecruit.Text = 0
                    lblCode.Text = objPro.CODE
                    lblStatus.Text = objPro.STATUS_NAME
                    lblTypeRecruit.Text = objPro.RECRUIT_TYPE_NAME
                    lblRecruitReason.Text = objPro.RECRUIT_REASON_NAME
                End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            strStatus = ""
            For Each item As RadListBoxItem In rlbStatus.Items
                If item.Checked Then
                    strStatus = strStatus & item.Value & ","
                End If
            Next
            rgCandidateList.Rebind()
            rgAspiration.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub CountNumberHaveRecruit(ByVal id As Integer)
        lblNumberHaveRecruit.Text = psp.COUNT_NUMBER_RC(id)
    End Sub
    Private Sub LoadDataRadlistBox()
        If Not IsPostBack Then
            rlbStatus.DataSource = psp.GET_STATUS_CANDIDATE()
            rlbStatus.DataTextField = "NAME"
            rlbStatus.DataValueField = "CODE"
            rlbStatus.DataBind()

        End If
    End Sub
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Dim objCandidate As New CandidateDTO
            Dim rep As New RecruitmentRepository

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If rgAspiration.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Chọn record muốn lưu"), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each Item As GridDataItem In rgAspiration.SelectedItems
                        Dim ID_CANDIDATE = Item("ID_CANDIDATE").Text
                        Dim PLACE_WORK = DirectCast(Item.FindControl("PLACE_WORK"), RadTextBox).Text
                        Dim RECEIVE_FROM As Date? = DirectCast(Item.FindControl("RECEIVE_FROM"), RadDatePicker).SelectedDate
                        Dim RECEIVE_TO As Date? = DirectCast(Item.FindControl("RECEIVE_TO"), RadDatePicker).SelectedDate
                        Dim PROBATION_FROM As Date? = DirectCast(Item.FindControl("PROBATION_FROM"), RadDatePicker).SelectedDate
                        Dim PROBATION_TO As Date? = DirectCast(Item.FindControl("PROBATION_TO"), RadDatePicker).SelectedDate
                        If psp.UPDATE_ASPIRATION(ID_CANDIDATE, PLACE_WORK, RECEIVE_FROM, RECEIVE_TO, PROBATION_FROM, PROBATION_TO) = 1 Then
                            rgAspiration.Rebind()
                            ShowMessage(Translate("Lưu thành công"), NotifyType.Success)
                        End If
                    Next
            End Select
            'UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    'Private Sub btnThankLetter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnThankLetter.Click
    '    Dim status As String
    '    For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
    '        status = dr.GetDataKeyValue("STATUS_CODE").ToString
    '        Select Case status
    '            Case RCContant.TRUNGTUYEN
    '                ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
    '                Exit Sub
    '            Case RCContant.BLACKLIST
    '                ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái BlackList"), NotifyType.Warning)
    '                Exit Sub
    '            Case RCContant.PONTENTIAL
    '                ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
    '                Exit Sub
    '            Case RCContant.THUMOI
    '                ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
    '                Exit Sub
    '            Case RCContant.NHANVIEN
    '                ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
    '                Exit Sub
    '            Case RCContant.TIEPNHANLD
    '                ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái gửi thông báo tiếp nhận LĐ thử việc"), NotifyType.Warning)
    '                Exit Sub
    '        End Select
    '    Next
    '    Dim dataItem = TryCast(rgCandidateList.SelectedItems(0), GridDataItem)
    '    If dataItem Is Nothing Then
    '        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
    '        Exit Sub
    '    End If
    '    ' format email
    '    'Dim receiver As String = dataItem("Email").Text
    '    Dim receiver As String = "tanvn@tinhvan.com"
    '    Dim cc As String = String.Empty
    '    Dim subject As String = "Thư cám ơn"
    '    Dim body As String = String.Empty
    '    Dim fileAttachments As String = String.Empty
    '    'format body by html template
    '    Dim reader As StreamReader = New StreamReader(Server.MapPath("~/Modules/Recruitment/Templates/TiepNhanThuViec.htm"))
    '    body = reader.ReadToEnd
    '    'body = body.Replace("{ngày}", DateTime.Now.Day)
    '    'body = body.Replace("{tháng}", DateTime.Now.Month)
    '    'body = body.Replace("{năm}", DateTime.Now.Year)
    '    body = body.Replace("{họ tên}", dataItem("FULLNAME_VN").Text.ToUpper())

    '    If Common.Common.sendEmailByServerMail(receiver, cc, subject, body, fileAttachments, "") Then
    '        ShowMessage(Translate(CommonMessage.MESSAGE_SENDMAIL_COMPLETED), NotifyType.Success)
    '        'Update Candidate Status
    '        psp.UPDATE_CANDIDATE_STATUS(dataItem("ID").Text, RCContant.TUCHOI)
    '        CurrentState = CommonMessage.STATE_NORMAL
    '        UpdateControlState()
    '    Else
    '        ShowMessage(Translate(CommonMessage.MESSAGE_SENDMAIL_ERROR), NotifyType.Warning)
    '        Exit Sub
    '    End If
    'End Sub

    'Private Sub cmdYCTDKhac_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdYCTDKhac.Click
    '    Dim strEmp As String = ""
    '    Dim status As String
    '    If rgCandidateList.SelectedItems.Count = 0 Then
    '        ShowMessage("Vui lòng chọn 1 ứng viên", NotifyType.Warning)
    '        Exit Sub
    '    End If

    '    For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
    '        status = dr.GetDataKeyValue("STATUS_CODE").ToString
    '        Select Case status
    '            'Case RCContant.TUCHOI
    '            '    ShowMessage(Translate("Tồn tại ứng viên đang ở từ chối trúng tuyển"), NotifyType.Warning)
    '            '    Exit Sub
    '            'Case RCContant.BLACKLIST
    '            '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái BlackList"), NotifyType.Warning)
    '            '    Exit Sub
    '            'Case RCContant.THUMOI
    '            '    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
    '            '    Exit Sub
    '            Case RCContant.NHANVIEN
    '                ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
    '                Exit Sub
    '            Case RCContant.TIEPNHANLD
    '                ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái gửi thông báo tiếp nhận LĐ thử việc"), NotifyType.Warning)
    '                Exit Sub
    '        End Select
    '        If strEmp = "" Then
    '            strEmp = dr.GetDataKeyValue("FULLNAME_VN")
    '        Else
    '            strEmp = strEmp + "," + dr.GetDataKeyValue("FULLNAME_VN")
    '        End If
    '    Next
    '    ctrlMessageBoxTransferProgram.MessageText = Translate("Bạn có muốn chuyển ứng viên: " + strEmp + " sang vị trí khác không")
    '    ctrlMessageBoxTransferProgram.ActionName = "CHUYENUNGVIEN"
    '    ctrlMessageBoxTransferProgram.DataBind()
    '    ctrlMessageBoxTransferProgram.Show()
    'End Sub


    Private Sub btnBlacklist_Click(sender As Object, e As System.EventArgs) Handles btnBlacklist.Click
        Dim status As String
        If rgCandidateList.SelectedItems.Count = 0 Then
            ShowMessage(Translate("Mời chọn ứng viên trước khi cập nhật"), NotifyType.Warning)
            Exit Sub
        End If
        For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
            status = dr.GetDataKeyValue("STATUS_CODE").ToString
            Select Case status
                Case RCContant.TRUNGTUYEN
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Trúng tuyển"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.BLACKLIST
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái BlackList"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.THUMOI
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.NHANVIEN
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.TIEPNHANLD
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái gửi thông báo tiếp nhận LĐ thử việc"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.XNTT
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Xác nhận Offer"), NotifyType.Warning)
                    Exit Sub
            End Select
        Next
        ctrlMessageBox.MessageText = Translate("Bạn có chắc chắn muốn chuyển trạng thái Blacklist cho các ứng viên?")
        ctrlMessageBox.ActionName = RCContant.BLACKLIST
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub

    Private Sub btnPontential_Click(sender As Object, e As System.EventArgs) Handles btnPontential.Click
        Dim status As String
        If rgCandidateList.SelectedItems.Count < 0 Then
            ShowMessage(Translate("Mời chọn nhân viên trước khi cập nhật"), NotifyType.Warning)
            Exit Sub
        End If
        For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
            status = dr.GetDataKeyValue("STATUS_CODE").ToString
            Select Case status
                Case RCContant.TRUNGTUYEN
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Trúng tuyển"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.PONTENTIAL
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái ứng viên tiềm năng"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.BLACKLIST
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái BlackList"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.THUMOI
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.NHANVIEN
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.TIEPNHANLD
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái gửi thông báo tiếp nhận LĐ thử việc"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.XNTT
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Xác nhận Offer"), NotifyType.Warning)
                    Exit Sub
            End Select
        Next
        ctrlMessageBox.MessageText = Translate("Bạn có chắc chắn muốn lưu danh sách tiềm năng?")
        ctrlMessageBox.ActionName = RCContant.PONTENTIAL
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub

    Private Sub btnTransfer_Click(sender As Object, e As System.EventArgs) Handles btnTransfer.Click

        Dim status As String
        If rgCandidateList.SelectedItems.Count = 0 Then
            ShowMessage(Translate("Mời chọn ứng viên trước khi cập nhật"), NotifyType.Warning)
            Exit Sub
        End If
        For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
            status = dr.GetDataKeyValue("STATUS_CODE").ToString
            Select Case status
                Case RCContant.THUMOI
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.NHANVIEN
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.KHONGDAT
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Không thi đạt"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.TUCHOI
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Từ chối trúng tuyển"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.BLACKLIST
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái BlackList"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.PONTENTIAL
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Ứng viên tìêm năng"), NotifyType.Warning)
                    Exit Sub
            End Select
        Next
        'Hiển thị Confirm delete.

        ctrlMessageBox.MessageText = Translate("Bạn có chắc chắn muốn chuyển sang HSNV?")
        ctrlMessageBox.ActionName = RCContant.NHANVIEN
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub

    Private Sub btnTrungTuyen_Click(sender As Object, e As System.EventArgs) Handles btnTrungTuyen.Click
        Dim status As String
        If rgCandidateList.SelectedItems.Count = 0 Then
            ShowMessage(Translate("Mời chọn ứng viên trước khi cập nhật"), NotifyType.Warning)
            Exit Sub
        End If
        For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
            status = dr.GetDataKeyValue("STATUS_CODE").ToString
            Select Case status
                Case RCContant.TRUNGTUYEN
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Trúng tuyển"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.BLACKLIST
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái BlackList"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.THUMOI
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.NHANVIEN
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.TIEPNHANLD
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái gửi thông báo tiếp nhận LĐ thử việc"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.XNTT
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Xác nhận Offer"), NotifyType.Warning)
                    Exit Sub
            End Select
        Next
        ctrlMessageBox.MessageText = Translate("Bạn có chắc chắn muốn chuyển trạng thái Xác nhận Offer cho các ứng viên?")
        ctrlMessageBox.ActionName = RCContant.XNTT
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub

    Private Sub btnKhongTrungTuyen_Click(sender As Object, e As System.EventArgs) Handles btnKhongTrungTuyen.Click
        Dim status As String
        If rgCandidateList.SelectedItems.Count = 0 Then
            ShowMessage(Translate("Mời chọn ứng viên trước khi cập nhật"), NotifyType.Warning)
            Exit Sub
        End If
        For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
            status = dr.GetDataKeyValue("STATUS_CODE").ToString
            Select Case status
                Case RCContant.TRUNGTUYEN
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Trúng tuyển"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.BLACKLIST
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái BlackList"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.THUMOI
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.NHANVIEN
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.TIEPNHANLD
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái gửi thông báo tiếp nhận LĐ thử việc"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.XNTT
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Xác nhận Offer"), NotifyType.Warning)
                    Exit Sub
            End Select
        Next
        'Hiển thị Confirm delete.
        ctrlMessageBox.MessageText = Translate("Bạn có chắc chắn muốn chuyển trạng thái Không đủ điều kiện cho các ứng viên?")
        ctrlMessageBox.ActionName = RCContant.TUCHOI
        ctrlMessageBox.DataBind()
        ctrlMessageBox.Show()
    End Sub

    Private Sub btnExportContract_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportContract.Click
        Dim strID As String
        Dim tempPath As String = ConfigurationManager.AppSettings("WordFileFolder")
        If rgCandidateList.SelectedItems.Count = 0 Then
            ShowMessage(Translate("Mời chọn ứng viên trước khi xuất tờ trình"), NotifyType.Warning)
            Exit Sub
        End If
        For Each dr As GridDataItem In rgCandidateList.SelectedItems
            strID = strID & dr.GetDataKeyValue("ID_CANDIDATE") & ","
        Next
        Dim dtData = psp.CONTRACT_RECIEVE(strID)
        If dtData.Rows.Count > 0 Then
            ExportWordMailMerge(System.IO.Path.Combine(Server.MapPath(tempPath), "Recruitment/CONTRACT_RECIEVE.doc"),
                 "CONTRACT_RECIEVE_" & dtData.Rows(0)("NAME") & ".doc",
                 dtData,
                 Response)
        End If
    End Sub

    'Private Sub btnReceive_Click(sender As Object, e As System.EventArgs) Handles btnReceive.Click
    '    Dim status As String
    '    Dim strID As String
    '    Dim tempPath As String = ConfigurationManager.AppSettings("WordFileFolder")
    '    Dim filePath As String = ""
    '    For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
    '        status = dr.GetDataKeyValue("STATUS_CODE").ToString
    '        Select Case status
    '            Case RCContant.TUCHOI
    '                ShowMessage(Translate("Tồn tại ứng viên đang ở từ chối trúng tuyển"), NotifyType.Warning)
    '                Exit Sub
    '            Case RCContant.BLACKLIST
    '                ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái BlackList"), NotifyType.Warning)
    '                Exit Sub
    '            Case RCContant.THUMOI
    '                ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
    '                Exit Sub
    '            Case RCContant.NHANVIEN
    '                ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
    '                Exit Sub
    '            Case RCContant.TIEPNHANLD
    '                ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái gửi thông báo tiếp nhận LĐ thử việc"), NotifyType.Warning)
    '                Exit Sub
    '        End Select
    '    Next

    '    For Each dr As GridDataItem In rgCandidateList.SelectedItems
    '        strID = strID & dr.GetDataKeyValue("ID_CANDIDATE") & ","
    '    Next
    '    Dim dtData = psp.LETTER_RECIEVE(strID)
    '    If dtData.Rows.Count > 0 Then
    '        Using word As New WordCommon
    '            Dim sourcePath = Server.MapPath("~/ReportTemplates/Profile/LocationInfo/")
    '            word.ExportMailMerge(System.IO.Path.Combine(Server.MapPath(tempPath),
    '                                "Recruitment/LETTER_RECIEVE.doc"),
    '                                "LETTER_RECIEVE_" & dtData.Rows(0)("NAME") & "HOTEN.doc" & "_" & _
    '                                Format(Date.Now, "yyyyMMddHHmmss"),
    '                                dtData,
    '                               sourcePath,
    '                                Response)
    '            'word.ExportMailMerge(System.IO.Path.Combine(Server.MapPath(tempPath),
    '            '                                      "Recruitment/LETTER_RECIEVE.doc"),
    '            '                                     "LETTER_RECIEVE_" & dtData.Rows(0)("NAME") & "HOTEN.doc",
    '            '                                      dtData,
    '            '                                      Response)
    '        End Using

    '    Else
    '        ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
    '        Exit Sub
    '    End If
    'End Sub

    Private Sub btnLĐ_Click(sender As Object, e As System.EventArgs) Handles btnLĐ.Click
        Dim status As String
        If rgCandidateList.SelectedItems.Count = 0 Then
            ShowMessage(Translate("Mời chọn ứng viên trước khi gửi"), NotifyType.Warning)
            Exit Sub
        End If
        For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
            status = dr.GetDataKeyValue("STATUS_CODE").ToString
            Select Case status
                Case RCContant.TRUNGTUYEN
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Trúng tuyển"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.TUCHOI
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Từ chối trúng tuyển"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.BLACKLIST
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái BlackList"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.PONTENTIAL
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.THUMOI
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã gửi thư mời tuyển dụng"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.TIEPNHANLD
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái gửi thông báo tiếp nhận LĐ thử việc"), NotifyType.Warning)
                    Exit Sub
                Case RCContant.XNTT
                    ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Xác nhận Offer"), NotifyType.Warning)
                    Exit Sub
            End Select
        Next
        'Dim status As String
        For Each dr As GridDataItem In rgCandidateList.SelectedItems
            'status = dr.GetDataKeyValue("STATUS_CODE").ToString
            'Select Case status
            '    Case RCContant.NHANVIEN
            '        ShowMessage(Translate("Tồn tại ứng viên đang ở trạng thái Đã là nhân viên"), NotifyType.Warning)
            '        Exit Sub
            'End Select
            Dim ID = dr.GetDataKeyValue("ID_CANDIDATE")
            Dim dataItem = psp.EMAIL_RECIEVE(ID)
            If dataItem.Rows.Count > 0 Then

                Dim receiver As String = "tanvn@tinhvan.com"
                Dim cc As String = String.Empty
                Dim subject As String = "Gửi email thông báo tiếp nhận LĐ thử việc"
                Dim body As String = String.Empty
                Dim fileAttachments As String = String.Empty
                'format body by html template
                Dim reader As StreamReader = New StreamReader(Server.MapPath("~/Modules/Recruitment/Templates/TiepNhanThuViec.htm"))
                body = reader.ReadToEnd
                body = body.Replace("{DAY}", dataItem(0)("DAY").ToString)
                body = body.Replace("{TITLE}", dataItem(0)("TITLE").ToString)
                body = body.Replace("{NAME}", dataItem(0)("NAME").ToString)
                body = body.Replace("{GENDER}", dataItem(0)("GENDER").ToString)
                body = body.Replace("{BIRTHDAY}", dataItem(0)("BIRTHDAY").ToString)
                body = body.Replace("{PLACE}", dataItem(0)("PLACE").ToString)
                body = body.Replace("{IDNO}", dataItem(0)("IDNO").ToString)
                body = body.Replace("{PHONE}", dataItem(0)("PHONE").ToString)
                body = body.Replace("{ADDRESS_CONTRACT}", dataItem(0)("ADDRESS_CONTRACT").ToString)
                body = body.Replace("{EDUCATION_TIME}", dataItem(0)("EDUCATION_TIME").ToString)
                body = body.Replace("{CERTIFICATE}", dataItem(0)("CERTIFICATE").ToString)
                body = body.Replace("{MAJORS}", dataItem(0)("MAJORS").ToString)
                body = body.Replace("{SCHOOL}", dataItem(0)("SCHOOL").ToString)
                body = body.Replace("{EXPERIENCE_TIME}", dataItem(0)("EXPERIENCE_TIME").ToString)
                body = body.Replace("{EXPERIENCE_TITILE}", dataItem(0)("EXPERIENCE_TITILE").ToString)
                body = body.Replace("{EXPERIENCE_COMPANY}", dataItem(0)("EXPERIENCE_COMPANY").ToString)
                body = body.Replace("{TITILE_PROBATION}", dataItem(0)("TITILE_PROBATION").ToString)
                body = body.Replace("{ORG_PROBATION}", dataItem(0)("ORG_PROBATION").ToString)
                body = body.Replace("{CONTRACT_TIME}", dataItem(0)("CONTRACT_TIME").ToString)
                body = body.Replace("{TASK}", dataItem(0)("TASK").ToString)
                If Common.Common.sendEmailByServerMail(receiver, cc, subject, body, fileAttachments, "") Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_SENDMAIL_COMPLETED), NotifyType.Success)
                    'Update Candidate Status
                    psp.UPDATE_CANDIDATE_STATUS(ID, RCContant.NHANVIEN)
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_SENDMAIL_ERROR), NotifyType.Warning)
                    Exit Sub
                End If
            End If

        Next

    End Sub


    Private Sub ctrlMessageBoxTransferProgram_ButtonCommand(ByVal sender As Object, ByVal e As Common.MessageBoxEventArgs) Handles ctrlMessageBoxTransferProgram.ButtonCommand
        Dim log As New UserLog
        log = LogHelper.GetUserLog
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim strID As String
                For Each dr As GridDataItem In rgCandidateList.SelectedItems
                    strID &= IIf(strID = vbNullString, dr.GetDataKeyValue("ID_CANDIDATE"), "," & dr.GetDataKeyValue("ID_CANDIDATE"))
                Next
                Session("ID_CANDIDATE") = strID

                If Not FindOrgTitle.Controls.Contains(ctrlFindProgramDialog) Then
                    'HttpContext.Current.Session("CallAllOrg") = "LoadAllOrg"
                    ctrlFindProgramDialog = Me.Register("ctrlFindProgramPopupDialog", "Recruitment", "ctrlFindProgramPopupDialog", "Shared")
                    ctrlFindProgramDialog.LoadAllOrganization = True
                    FindOrgTitle.Controls.Add(ctrlFindProgramDialog)
                    ctrlFindProgramDialog.Show()
                End If
            Else
                Return
            End If
            rgCandidateList.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim log As New UserLog
        log = LogHelper.GetUserLog
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                strIDCandidate = ""
                Dim lstCanID As New List(Of Decimal)
                For Each dr As Telerik.Web.UI.GridDataItem In rgCandidateList.SelectedItems
                    strIDCandidate = strIDCandidate & dr.GetDataKeyValue("ID_CANDIDATE") & ","
                Next
                ' Kiem tra neu la trang thai nhan vien thi insert du lieu moi vao cac table nhan vien
                If e.ActionName = RCContant.NHANVIEN Then
                    If psp.INSERT_CADIDATE_EMPLOYEE(strIDCandidate, log.Username, log.Ip + log.ComputerName) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Success)
                    End If
                Else
                    If psp.UPDATE_CANDIDATE_STATUS(strIDCandidate, e.ActionName) = 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Success)
                    End If
                End If
                rgCandidateList.Rebind()
                rgResult.Rebind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgCandidateList.NeedDataSource
        Try
            If hidProgramID.Value <> "" Then
                rgCandidateList.DataSource = psp.GET_LIST_EMPLOYEE_ELECT(Decimal.Parse(hidProgramID.Value), strStatus)
                rgCandidateList.VirtualItemCount = psp.GET_LIST_EMPLOYEE_ELECT(Decimal.Parse(hidProgramID.Value), strStatus).Rows.Count
                CountNumberHaveRecruit(hidProgramID.Value)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub rgAspiration_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgAspiration.NeedDataSource
        Try
            If hidProgramID.Value <> "" Then
                rgAspiration.DataSource = psp.GET_LIST_EMPLOYEE_ASPIRATION(Decimal.Parse(hidProgramID.Value), strStatus)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub rgResult_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgResult.NeedDataSource
        Try
            If rgCandidateList.SelectedValues IsNot Nothing Then
                rgResult.DataSource = Nothing
                Dim idProGram = rgCandidateList.SelectedValues.Item("ID")
                Dim Candidate_CODE = rgCandidateList.SelectedValues.Item("CANDIDATE_CODE")
                rgResult.DataSource = psp.GET_LIST_EMPLOYEE_EXAMS(idProGram, Candidate_CODE)
            Else
                rgResult.DataSource = psp.GET_LIST_EMPLOYEE_EXAMS(0, "")
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub rgCandidateList_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rgCandidateList.SelectedIndexChanged
        Try
            'If rgCandidateList.SelectedValues IsNot Nothing Then
            '    Dim idProGram = rgCandidateList.SelectedValues.Item("ID")
            '    Dim Candidate_CODE = rgCandidateList.SelectedValues.Item("CANDIDATE_CODE")
            '    rgResult.DataSource = psp.GET_LIST_EMPLOYEE_EXAMS(idProGram, Candidate_CODE)
            '    rgResult.DataBind()
            'End If
            rgResult.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ' Cập nhật nguyện vọng
    'Protected Sub rgAspiration_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rgAspiration.SelectedIndexChanged
    '    For Each Item As GridDataItem In rgAspiration.SelectedItems
    '        Dim ID_CANDIDATE = Item("ID_CANDIDATE").Text
    '        Dim PLACE_WORK = DirectCast(Item.FindControl("PLACE_WORK"), RadTextBox).Text
    '        Dim RECEIVE_FROM As Date? = DirectCast(Item.FindControl("RECEIVE_FROM"), RadDatePicker).SelectedDate
    '        Dim RECEIVE_TO As Date? = DirectCast(Item.FindControl("RECEIVE_TO"), RadDatePicker).SelectedDate
    '        Dim PROBATION_FROM As Date? = DirectCast(Item.FindControl("PROBATION_FROM"), RadDatePicker).SelectedDate
    '        Dim PROBATION_TO As Date? = DirectCast(Item.FindControl("PROBATION_TO"), RadDatePicker).SelectedDate
    '        If psp.UPDATE_ASPIRATION(ID_CANDIDATE, PLACE_WORK, RECEIVE_FROM, RECEIVE_TO, PROBATION_FROM, PROBATION_TO) = 1 Then
    '            rgAspiration.Rebind()
    '        End If
    '    Next

    'End Sub
#End Region

#Region "Custom"
    Private Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Try
            Dim rep As New RecruitmentRepository

            'Mã nhân viên
            If hidProgramID.Value <> "" Then
                _filter.RC_PROGRAM_ID = Decimal.Parse(hidProgramID.Value)

            End If
            Dim MaximumRows As Integer

            Dim Sorts As String = rgCandidateList.MasterTableView.SortExpressions.GetSortString()

            If Not isFull Then
                If Sorts IsNot Nothing Then
                    Me.CandidateList = rep.GetListCandidateTransferPaging(rgCandidateList.CurrentPageIndex, _
                                                                rgCandidateList.PageSize, _
                                                                MaximumRows, _
                                                                _filter, _
                                                                Sorts)
                Else
                    Me.CandidateList = rep.GetListCandidateTransferPaging(rgCandidateList.CurrentPageIndex, _
                                                                rgCandidateList.PageSize, _
                                                                MaximumRows, _filter)
                End If

                rgCandidateList.VirtualItemCount = MaximumRows

                If CandidateList IsNot Nothing Then
                    rgCandidateList.DataSource = CandidateList
                Else
                    rgCandidateList.DataSource = New List(Of CandidateDTO)
                End If
            Else
                If Sorts IsNot Nothing Then
                    Return rep.GetListCandidate(_filter, Sorts).ToTable
                Else
                    Return rep.GetListCandidate(_filter).ToTable
                End If
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

#End Region

    'Protected Sub chkPassed_CheckedChanged(sender As Object, e As EventArgs) Handles chkPassed.CheckedChanged

    'End Sub
    'Protected Sub chkElect_CheckedChanged(sender As Object, e As EventArgs) Handles chkElect.CheckedChanged

    'End Sub
    'Protected Sub chkInternal_CheckedChanged(sender As Object, e As EventArgs) Handles chkInternal.CheckedChanged

    'End Sub
    'Protected Sub chkPotential_CheckedChanged(sender As Object, e As EventArgs) Handles chkPotential.CheckedChanged

    'End Sub
    'Protected Sub chkInvitation_CheckedChanged(sender As Object, e As EventArgs) Handles chkInvitation.CheckedChanged

    'End Sub
    'Protected Sub chkEmployee_CheckedChanged(sender As Object, e As EventArgs) Handles chkEmployee.CheckedChanged

    'End Sub

    Private Sub AjaxManager_AjaxRequest(sender As Object, e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Try
            Dim eventArg As String = e.Argument  'Request("__EVENTARGUMENT")
            If Left(eventArg, 13) <> "PopupPostback" Then
                Exit Sub
            End If
            If eventArg <> "" Then
                eventArg = Right(eventArg, eventArg.Length - 14)
                If eventArg = "Cancel" Then
                ElseIf eventArg = "OK" Then
                    'For Each dr As GridDataItem In rgCandidateList.SelectedItems
                    '    If dr.GetDataKeyValue("ID_CANDIDATE") = Session("ID_CANDIDATE") Then
                    '        dr("STATUS_NAME").Text = "Ứng viên đã chuyển sang vị trí khác"
                    '        dr("STATUS_CODE").Text = "CHUYENVITRI"
                    '        Exit For
                    '    End If
                    'Next
                    ShowMessage("Thao tác thành công", NotifyType.Success)
                    rgCandidateList.Rebind()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
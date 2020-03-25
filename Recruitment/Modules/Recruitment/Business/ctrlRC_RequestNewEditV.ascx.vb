Imports Framework.UI
Imports Common
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Profile.ProfileBusiness
Imports Ionic.Crc

Public Class ctrlRC_RequestNewEditV
    Inherits CommonView
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = True
    Private rep As New HistaffFrameworkRepository
    Private store As New RecruitmentStoreProcedure()

#Region "Property"

    '0 - normal
    '1 - Employee
    '2 - Org
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    '0 - Declare
    '1 - Extent
    '2 - Details
    Dim FormType As Integer
    Dim IDSelect As Decimal?
    Property STATUSCODE As Decimal
        Get
            Return ViewState(Me.ID & "_STATUSCODE")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_STATUSCODE") = value
        End Set
    End Property
    Property ListAttachFile As List(Of AttachFilesDTO)
        Get
            Return ViewState(Me.ID & "_ListAttachFile")
        End Get
        Set(ByVal value As List(Of AttachFilesDTO))
            ViewState(Me.ID & "_ListAttachFile") = value
        End Set
    End Property

    Property Down_File As String
        Get
            Return ViewState(Me.ID & "_Down_File")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Down_File") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try

            ScriptManager.GetCurrent(Page).RegisterPostBackControl(tbarMain)
            GetParams()
            Refresh()
            UpdateControlState()
            SetVisibleFileAttach()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Me.MainToolBar = tbarMain
        Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
        CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
        Me.MainToolBar.OnClientButtonClicking = "clientButtonClicking"
        CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
    End Sub

    Public Overrides Sub BindData()
        Try
            GetDataCombo()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Try
            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim obj = rep.GetRequestByID(New RequestDTO With {.ID = IDSelect})
                    hidOrgID.Value = obj.ORG_ID.ToString()
                    txtOrgName.Text = obj.ORG_NAME
                    If obj.ORG_DESC IsNot Nothing AndAlso obj.ORG_DESC <> "" Then
                        txtOrgName.ToolTip = DrawTreeByString(obj.ORG_DESC)
                    End If
                  

                    LoadComboTitle()
                    If obj.IS_IN_PLAN Then
                        cboTitle.SelectedValue = obj.RC_PLAN_ID
                    Else
                        cboTitle.SelectedValue = obj.TITLE_ID
                    End If

                    rdSendDate.SelectedDate = obj.SEND_DATE
                   
                    

                    If obj.RECRUIT_REASON_ID IsNot Nothing Then
                        cboRecruitReason.SelectedValue = obj.RECRUIT_REASON_ID
                    End If
                   

                  
                  
                 


                    rdExpectedJoinDate.SelectedDate = obj.EXPECTED_JOIN_DATE
                  
                    rntxtRecruitNumber.Value = obj.RECRUIT_NUMBER
                  
                  
                  

                    txtUpload.Text = obj.FILE_NAME
                    txtUploadFile.Text = obj.UPLOAD_FILE

                   
                    hidID.Value = obj.ID
                    hidOrgID.Value = obj.ORG_ID
                    
                    'For Each itm In obj.lstEmp
                    '    Dim item As New RadListBoxItem
                    '    item.Value = itm.EMPLOYEE_ID
                    '    item.Text = itm.EMPLOYEE_CODE & " - " & itm.EMPLOYEE_NAME
                    '    lstEmployee.Items.Add(item)
                    'Next

                    If obj.STATUS_ID = RecruitmentCommon.RC_REQUEST_STATUS.APPROVE_ID Then
                        RadPane2.Enabled = False
                    End If

                    GetTotalEmployeeByTitleID()

                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
                    'chkIsInPlan.Checked = True
                    rdSendDate.AutoPostBack = True

                    Me.MainToolBar = tbarMain
            End Select

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick

        Dim gID As Decimal
        Dim rep As New RecruitmentRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim obj As New RequestDTO
                        Dim lstEmp As New List(Of RecruitmentInsteadDTO)
                        obj.ORG_ID = hidOrgID.Value
                        obj.TITLE_ID = cboTitle.SelectedValue
                        obj.SEND_DATE = rdSendDate.SelectedDate
                       
                        obj.RECRUIT_NUMBER = rntxtRecruitNumber.Value
                        obj.FILE_NAME = txtUpload.Text.Trim
                        obj.UPLOAD_FILE = txtUploadFile.Text.Trim

                        obj.EXPECTED_JOIN_DATE = rdExpectedJoinDate.SelectedDate
                        If cboRecruitReason.SelectedValue <> "" Then
                            obj.RECRUIT_REASON_ID = cboRecruitReason.SelectedValue
                        End If
                       
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertRequest(obj, gID) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Recruitment&fid=ctrlRC_Request&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                obj.ID = hidID.Value
                                If rep.ModifyRequest(obj, gID) Then
                                    ''POPUPTOLINK
                                    Response.Redirect("/Default.aspx?mid=Recruitment&fid=ctrlRC_Request&group=Business")

                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim hfr As New HistaffFrameworkRepository
                    Dim tempPath As String = "ReportTemplates/Recruitment/Report/"
                    'Dim obj = hfr.ExecuteToDataSet("PKG_RECRUITMENT.EXPORT_RECRUITMENT_NEEDS", New List(Of Object)({hidID.Value, If(txtPayrollLimit.Text = "", Nothing, txtPayrollLimit.Text), If(txtCurrentNumber.Text = "", Nothing, txtCurrentNumber.Text), rntxtRecruitNumber.Text}))
                    'ExportWordMailMerge(System.IO.Path.Combine(Server.MapPath(tempPath), "BM01_TT_Nhu_cau_TD.doc"),
                    '                    "TT_Nhu_cau_TD_" + DateTime.Now.ToString("HHmmssddMMyyyy") + ".doc",
                    '                    obj.Tables(0), Response)
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Recruitment&fid=ctrlRC_Request&group=Business")
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
        UpdateControlState()
    End Sub

    'Protected Sub btnFindEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindEmployee.Click
    '    Try
    '        isLoadPopup = 1
    '        UpdateControlState()
    '        ctrlFindEmployeePopup.Show()
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try

    'End Sub

    Protected Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindOrg.Click
        Try
            isLoadPopup = 2
            UpdateControlState()
            ctrlFindOrgPopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    'Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
    '    Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
    '    Dim rep As New RecruitmentRepository
    '    Try
    '        lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
    '        lstEmployee.Items.Clear()
    '        For Each itm In lstCommonEmployee
    '            Dim item As New RadListBoxItem
    '            item.Value = itm.EMPLOYEE_ID
    '            item.Text = itm.EMPLOYEE_CODE & " - " & itm.FULLNAME_VN
    '            lstEmployee.Items.Add(item)
    '        Next
    '        isLoadPopup = 0
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try

    'End Sub

    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Try
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue 'gán org đã chọn vào hiddenfield
                txtOrgName.Text = orgItem.NAME_VN
                'txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
                'duy fix ngay 11/07
                If orgItem.DESCRIPTION_PATH IsNot Nothing AndAlso orgItem.DESCRIPTION_PATH <> "" Then
                    txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
                End If
            End If
            LoadComboTitle()
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked, ctrlFindOrgPopup.CancelClicked
        isLoadPopup = 0
    End Sub

    'Private Sub btnUploadFileDescription_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFileDescription.Click
    '    ctrlUpload1.Show()
    'End Sub

    'Private Sub btnDeleteFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteFile.Click
    '    Dim sPath As String = "~/ReportTemplates//" & "Recruitment" & "/" & "Upload/" & hddFile.Value
    '    If System.IO.File.Exists(MapPath(sPath)) Then
    '        System.IO.File.Delete(MapPath(sPath))
    '        hddFile.Value = ""
    '        hypFile.Text = ""
    '        hypFile.NavigateUrl = ""
    '        SetVisibleFileAttach()
    '    Else
    '        ShowMessage(Translate("Không tìm thấy đường dẫn"), NotifyType.Error)
    '    End If
    'End Sub

    'Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
    '    'Dim fileName As String
    '    Try
    '        Dim file As UploadedFile = ctrlUpload1.UploadedFiles(0)
    '        Dim sPath As String = "~/ReportTemplates//" & "Recruitment" & "/" & "Upload/"

    '        If file.GetExtension = ".pdf" Or file.GetExtension = ".doc" Or file.GetExtension = ".docx" Or file.GetExtension = ".jpg" Or file.GetExtension = ".png" Then
    '            Dim fileName As String = hidOrgID.Value & "_" & "_" & cboTitle.SelectedValue & Date.Now.ToString("HHmmssffff") & "_" & file.FileName
    '            If System.IO.Directory.Exists(MapPath(sPath)) Then
    '                file.SaveAs(MapPath(sPath) & fileName, True)
    '                hddFile.Value = fileName
    '                hypFile.Text = file.FileName
    '                hypFile.NavigateUrl = "http://" & Request.Url.Host & ":" & Request.Url.Port & "/ReportTemplates/Recruitment/Upload/" + fileName
    '                SetVisibleFileAttach()
    '            Else
    '                ShowMessage(Translate("Không tìm thấy đường dẫn"), NotifyType.Error)
    '            End If

    '        Else
    '            ShowMessage(Translate("Vui lòng upload file có đuôi mở rộng: .pdf,.doc, .docx, .jpg, .png"), NotifyType.Error)
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '        ShowMessage(Translate("Import bị lỗi"), NotifyType.Error)
    '    End Try
    'End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ctrlUpload2.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
            ctrlUpload2.Show()
            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    Private Sub ctrlUpload2_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload2.OkClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            txtUpload.Text = ""
            Dim listExtension = New List(Of String)
            listExtension.Add(".xls")
            listExtension.Add(".xlsx")
            listExtension.Add(".txt")
            listExtension.Add(".ctr")
            listExtension.Add(".doc")
            listExtension.Add(".docx")
            listExtension.Add(".xml")
            listExtension.Add(".png")
            listExtension.Add(".jpg")
            listExtension.Add(".bitmap")
            listExtension.Add(".jpeg")
            listExtension.Add(".gif")
            listExtension.Add(".pdf")
            listExtension.Add(".rar")
            listExtension.Add(".zip")
            listExtension.Add(".ppt")
            listExtension.Add(".pptx")
            Dim fileName As String

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Recruitment/RequestRCInfo/")
            If ctrlUpload2.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload2.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload2.UploadedFiles(i)
                    Dim str_Filename = Guid.NewGuid.ToString() + "\"
                    If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        strPath = strPath + str_Filename
                        fileName = System.IO.Path.Combine(strPath, file.FileName)
                        file.SaveAs(fileName, True)
                        txtUpload.Text = file.FileName
                        Down_File = str_Filename
                    Else
                        ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file XLS,XLSX,TXT,CTR,DOC,DOCX,XML,PNG,JPG,BITMAP,JPEG,GIF,PDF,RAR,ZIP,PPT,PPTX"), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                loadDatasource(txtUpload.Text)
            End If
            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    Private Sub btnDownload_Click(sender As Object, e As System.EventArgs) Handles btnDownload.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim strPath_Down As String
        Try
            If txtUpload.Text <> "" Then
                strPath_Down = Server.MapPath("~/ReportTemplates/Recruitment/RequestRCInfo/" + txtUploadFile.Text + txtUpload.Text)
                ZipFiles(strPath_Down, 2)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Custom"

    Public Overrides Sub UpdateControlState()
        Try

            If phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                phFindEmployee.Controls.Remove(ctrlFindEmployeePopup)
                'Me.Views.Remove(ctrlFindEmployeePopup.ID.ToUpper)
            End If
            If phFindOrg.Controls.Contains(ctrlFindOrgPopup) Then
                phFindOrg.Controls.Remove(ctrlFindOrgPopup)
                'Me.Views.Remove(ctrlFindOrgPopup.ID.ToUpper)
            End If
            Select Case isLoadPopup
                Case 1
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.MustHaveContract = False
                    phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
                    ctrlFindEmployeePopup.MultiSelect = True
                Case 2
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    ctrlFindOrgPopup.OrganizationType = OrganizationType.OrganizationLocation
                    phFindOrg.Controls.Add(ctrlFindOrgPopup)
                    ctrlFindOrgPopup.Show()
            End Select
        Catch ex As Exception
            Throw ex
        End Try
        'ChangeToolbarState()
    End Sub

    Private Sub GetDataCombo()
        Try
            Dim dtData As DataTable
            Using rep As New RecruitmentRepository
                dtData = rep.GetOtherList("RC_RECRUIT_REASON", True)
                FillRadCombobox(cboRecruitReason, dtData, "NAME", "ID")
                
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetParams()
        Try
            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    IDSelect = Decimal.Parse(Request.Params("ID"))
                End If
                If IDSelect IsNot Nothing Then
                    Refresh("UpdateView")
                Else
                    Refresh("InsertView")
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SetVisibleFileAttach()
        'If hddFile.Value <> "" Then
        '    btnDeleteFile.Visible = True
        '    hypFile.Visible = True
        '    btnUploadFileDescription.Visible = False
        'Else
        '    btnDeleteFile.Visible = False
        '    hypFile.Visible = False
        '    btnUploadFileDescription.Visible = True
        'End If

    End Sub

    Private Sub ZipFiles(ByVal path As String, ByVal order As Decimal?)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim crc As New CRC32()
            'Dim fileNameZip As String

            'If order = 0 Then
            '    fileNameZip = txtUpload_LG.Text.Trim
            'ElseIf order = 1 Then
            '    fileNameZip = txtUpload_HD.Text.Trim
            'Else
            '    fileNameZip = txtUpload_FT.Text.Trim
            'End If

            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
            Response.WriteFile(file.FullName)
            Response.End()
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
        End Try
    End Sub
#End Region

    Private Sub rntxtRecruitNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rntxtRecruitNumber.TextChanged
        Try
            Dim recruitNum As Decimal = If(IsNumeric(rntxtRecruitNumber.Value), Decimal.Parse(rntxtRecruitNumber.Value), 0)
           
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub LoadComboTitle()

        If hidOrgID.Value <> "" Then
            Dim dtData As DataTable
            dtData = store.GET_TITLE_IN_PLAN(hidOrgID.Value, 0)
            FillRadCombobox(cboTitle, dtData, "NAME", "ID")
        Else
            cboTitle.Items.Clear()
            cboTitle.ClearSelection()
            cboTitle.Text = ""
        End If

        

    End Sub

    Protected Sub cboTitle_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTitle.SelectedIndexChanged
        Dim rep As New RecruitmentRepository
        GetTotalEmployeeByTitleID()

       
        If hidID.Value <> "" Then
            Dim obj = rep.GetRequestByID(New RequestDTO With {.ID = Decimal.Parse(hidID.Value)})
            If obj.ID > 0 And cboTitle.SelectedValue = obj.RC_PLAN_ID Then
                
                rdExpectedJoinDate.SelectedDate = obj.EXPECTED_JOIN_DATE

               
            Else
                Dim dt As DataTable = store.PLAN_GET_BY_ID(Int32.Parse(cboTitle.SelectedValue))
                If dt.Rows.Count > 0 Then
                   
                    If dt.Rows(0)("EXPECTED_JOIN_DATE") IsNot Nothing And dt.Rows(0)("EXPECTED_JOIN_DATE").ToString() <> String.Empty Then
                        rdExpectedJoinDate.SelectedDate = DateTime.Parse(dt.Rows(0)("EXPECTED_JOIN_DATE").ToString())
                    End If
                   
                Else
                   
                    rdExpectedJoinDate.SelectedDate = Nothing
                    
                End If
            End If
        Else
            If cboTitle.SelectedValue <> "" Then
                Dim dt As DataTable = store.PLAN_GET_BY_ID(Int32.Parse(cboTitle.SelectedValue))
                If dt.Rows.Count > 0 Then
                    
                   
                    
                   
                   


                    If dt.Rows(0)("EXPECTED_JOIN_DATE") IsNot Nothing And dt.Rows(0)("EXPECTED_JOIN_DATE").ToString() <> String.Empty Then
                        rdExpectedJoinDate.SelectedDate = DateTime.Parse(dt.Rows(0)("EXPECTED_JOIN_DATE").ToString())
                    End If
                Else
                    rdExpectedJoinDate.SelectedDate = Nothing
                End If
            End If
        End If
    End Sub

    Protected Sub GetTotalEmployeeByTitleID()
        Try
            'Dim OUT_NUMBER As String
            'Dim obj = rep.ExecuteStoreScalar("PKG_RECRUITMENT.GET_TOTAL_EMPLOYEE_BY_TITLEID", New List(Of Object)({hidOrgID.Value, cboTitle.SelectedValue, Common.Common.GetUserName(), OUT_NUMBER}))
            'txtCurrentNumber.Text = obj(0).ToString()
            If IsDate(rdSendDate.SelectedDate) Then
                If hidOrgID.Value <> String.Empty Then
                    Dim tab As DataTable = store.GetCurrentManningTitle1(Int32.Parse(hidOrgID.Value), cboTitle.SelectedValue, rdSendDate.SelectedDate)
                    'If tab.Rows.Count > 0 Then
                    '    txtPayrollLimit.Text = tab.Rows(0)("NEW_MANNING").ToString()
                    '    txtCurrentNumber.Text = tab.Rows(0)("CURRENT_MANNING").ToString()
                    '    txtDifferenceNumber.Text = tab.Rows(0)("MOBILIZE_COUNT_MANNING").ToString()
                    'Else
                    '    txtPayrollLimit.Text = "0"
                    '    txtCurrentNumber.Text = "0"
                    '    txtDifferenceNumber.Text = "0"
                    'End If
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Private Sub loadDatasource(ByVal strUpload As String)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If strUpload <> "" Then
                txtUpload.Text = strUpload
                txtUploadFile.Text = Down_File
            Else
                strUpload = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub rdSendDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdSendDate.SelectedDateChanged
        Try
            If IsDate(rdSendDate.SelectedDate) Then
                If hidOrgID.Value <> String.Empty Then
                    Dim tab As DataTable = store.GetCurrentManningTitle1(Int32.Parse(hidOrgID.Value), cboTitle.SelectedValue, rdSendDate.SelectedDate)
                    'If tab.Rows.Count > 0 Then
                    '    txtPayrollLimit.Text = tab.Rows(0)("NEW_MANNING").ToString()
                    '    txtCurrentNumber.Text = tab.Rows(0)("CURRENT_MANNING").ToString()
                    '    txtDifferenceNumber.Text = tab.Rows(0)("MOBILIZE_COUNT_MANNING").ToString()
                    'Else
                    '    txtPayrollLimit.Text = "0"
                    '    txtCurrentNumber.Text = "0"
                    '    txtDifferenceNumber.Text = "0"
                    'End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
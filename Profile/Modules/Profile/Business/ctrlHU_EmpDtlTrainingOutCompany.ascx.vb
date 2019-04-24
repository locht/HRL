Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports Common
Imports System.IO
Imports WebAppLog
Imports Common.Common
Imports Common.CommonMessage
Imports Aspose.Words
Imports Aspose.Words.Reporting
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Public Class ctrlHU_EmpDtlTrainingOutCompany
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\List" + Me.GetType().Name.ToString()


#Region "Property"

    Property EmployeeInfo As EmployeeDTO
        Get
            Return PageViewState(Me.ID & "_EmployeeInfo")
        End Get
        Set(ByVal value As EmployeeDTO)
            PageViewState(Me.ID & "_EmployeeInfo") = value
        End Set
    End Property

    Public Property EmployeeTrain As List(Of HU_PRO_TRAIN_OUT_COMPANYDTO)
        Get
            Return ViewState(Me.ID & "_EmployeeTrain")
        End Get
        Set(ByVal value As List(Of HU_PRO_TRAIN_OUT_COMPANYDTO))
            ViewState(Me.ID & "_EmployeeTrain") = value
        End Set
    End Property

    Property DeleteEmployeeTrain As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_DeleteEmployeeTrain")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_DeleteEmployeeTrain") = value
        End Set
    End Property

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
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

    Property FileOldName As String
        Get
            Return ViewState(Me.ID & "_FileOldName")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_FileOldName") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ctrlEmpBasicInfo.SetProperty("EmployeeInfo", EmployeeInfo)
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgEmployeeTrain.SetFilter()
            rgEmployeeTrain.AllowCustomPaging = True
            rgEmployeeTrain.ClientSettings.EnablePostBackOnRowClick = True
            InitControl()
            If Not IsPostBack Then
                ViewConfig(RadPane1)
                GirdConfig(rgEmployeeTrain)
            End If
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
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete)
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <remarks></remarks>
    Private Sub btnUPLOAD_FILE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFile.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf"
            ctrlUpload1.Show()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                rgEmployeeTrain.Rebind()
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgEmployeeTrain.Rebind()
                        'SelectedItemDataGridByKey(rgEmployeeTrain, IDSelect, , rgEmployeeTrain.CurrentPageIndex)

                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgEmployeeTrain.CurrentPageIndex = 0
                        rgEmployeeTrain.MasterTableView.SortExpressions.Clear()
                        rgEmployeeTrain.Rebind()
                        'SelectedItemDataGridByKey(rgEmployeeTrain, IDSelect, )
                    Case "Cancel"
                        txtRemindLink.Text = ""
                        FileOldName = ""
                        rgEmployeeTrain.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub CreateDataFilter()

        Dim obj As New HU_PRO_TRAIN_OUT_COMPANYDTO
        Try
            SetValueObjectByRadGrid(rgEmployeeTrain, obj)

            Dim rep As New ProfileBusinessRepository
            Dim objEmployeeTrain As New HU_PRO_TRAIN_OUT_COMPANYDTO
            objEmployeeTrain.EMPLOYEE_ID = If(EmployeeInfo Is Nothing, Nothing, EmployeeInfo.ID)

            Me.EmployeeTrain = rep.GetProcessTraining(objEmployeeTrain)
            rep.Dispose()
            rgEmployeeTrain.DataSource = Me.EmployeeTrain
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()

        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW

                    EnabledGridNotPostback(rgEmployeeTrain, False)
                    'rdToiThang.SelectedDate = Nothing
                    'rdTuThang.SelectedDate = Nothing
                    rntGraduateYear.Text = ""
                    txtRemark.Text = ""
                    txtTrainingSchool.Text = ""
                    txtRemark.Text = ""
                    cboTrainingForm.SelectedValue = ""
                    txtBangCap.Text = ""
                    txtChuyenNganh.Text = ""
                    txtKetQua.Text = ""
                    rdFrom.SelectedDate = Nothing
                    rdTo.SelectedDate = Nothing
                    cboTrainingType.SelectedValue = ""
                    rdReceiveDegree.SelectedDate = Nothing
                    EnableControlAll(True, rdToiThang, rdTuThang, rntGraduateYear, txtRemark, cboTrainingForm, txtBangCap, txtChuyenNganh, txtKetQua, txtTrainingSchool, rdFrom, rdTo, cboTrainingType, rdReceiveDegree)


                Case CommonMessage.STATE_NORMAL
                    'rdToiThang.SelectedDate = Nothing
                    'rdTuThang.SelectedDate = Nothing
                    rntGraduateYear.Text = ""
                    txtRemark.Text = ""
                    txtTrainingSchool.Text = ""
                    cboTrainingForm.SelectedValue = ""
                    txtBangCap.Text = ""
                    txtChuyenNganh.Text = ""
                    txtKetQua.Text = ""
                    rdFrom.SelectedDate = Nothing
                    rdTo.SelectedDate = Nothing
                    cboTrainingType.SelectedValue = ""
                    rdReceiveDegree.SelectedDate = Nothing
                    EnabledGridNotPostback(rgEmployeeTrain, True)
                    EnableControlAll(False, rdTuThang, rdToiThang, rntGraduateYear, txtRemark, cboTrainingForm, txtBangCap, txtChuyenNganh, txtKetQua, txtTrainingSchool, rdFrom, rdTo, cboTrainingType, rdReceiveDegree)
                Case CommonMessage.STATE_EDIT

                    EnabledGridNotPostback(rgEmployeeTrain, False)
                    EnableControlAll(True, rdTuThang, rdToiThang, rntGraduateYear, txtRemark, cboTrainingForm, txtBangCap, txtChuyenNganh, txtKetQua, txtTrainingSchool, rdFrom, rdTo, cboTrainingType, rdReceiveDegree)
                Case CommonMessage.STATE_DELETE
                    Dim rep As New ProfileBusinessRepository
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgEmployeeTrain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgEmployeeTrain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteProcessTraining(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        UpdateControlState()
                    End If
                    rep.Dispose()
            End Select
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub BindData()
        Dim dic As New Dictionary(Of String, Control)
        dic.Add("FROM_DATE", rdTuThang)
        dic.Add("TO_DATE", rdToiThang)
        dic.Add("YEAR_GRA", rntGraduateYear)
        dic.Add("NAME_SHOOLS", txtTrainingSchool)
        dic.Add("FORM_TRAIN_ID", cboTrainingForm)
        dic.Add("SPECIALIZED_TRAIN", txtChuyenNganh)
        dic.Add("RESULT_TRAIN", txtKetQua)
        dic.Add("CERTIFICATE", txtBangCap)
        dic.Add("EFFECTIVE_DATE_FROM", rdFrom)
        dic.Add("EFFECTIVE_DATE_TO", rdTo)
        dic.Add("UPLOAD_FILE", txtUploadFile)
        dic.Add("FILE_NAME", txtRemark)
        dic.Add("TYPE_TRAIN_ID", cboTrainingType)
        dic.Add("RECEIVE_DEGREE_DATE", rdReceiveDegree)

        Utilities.OnClientRowSelectedChanged(rgEmployeeTrain, dic)

        Try
            Dim rep As New ProfileRepository
            Dim comboBoxDataDTO As New ComboBoxDataDTO
            If comboBoxDataDTO Is Nothing Then
                comboBoxDataDTO = New ComboBoxDataDTO
            End If
            comboBoxDataDTO.GET_TRAINING_FORM = True
            comboBoxDataDTO.GET_LEARNING_LEVEL = True
            comboBoxDataDTO.GET_MAJOR = True
            comboBoxDataDTO.GET_MARK_EDU = True
            comboBoxDataDTO.GET_TRAINING_TYPE = True
            rep.GetComboList(comboBoxDataDTO)
            If comboBoxDataDTO IsNot Nothing Then
                FillDropDownList(cboTrainingForm, comboBoxDataDTO.LIST_TRAINING_FORM, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboTrainingForm.SelectedValue)
                FillDropDownList(cboTrainingType, comboBoxDataDTO.LIST_TRAINING_TYPE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboTrainingType.SelectedValue)
            End If
            rep.Dispose()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objTrain As New HU_PRO_TRAIN_OUT_COMPANYDTO
        Dim gID As Decimal

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    If EmployeeInfo.WORK_STATUS Is Nothing Or
                        (EmployeeInfo.WORK_STATUS IsNot Nothing AndAlso
                         (EmployeeInfo.WORK_STATUS <> ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Or
                          (EmployeeInfo.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID And
                           EmployeeInfo.TER_EFFECT_DATE > Date.Now.Date))) Then
                        CurrentState = CommonMessage.STATE_NEW
                        rgEmployeeTrain.Rebind()
                    Else
                        ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                        Exit Sub
                    End If
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgEmployeeTrain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rgEmployeeTrain.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    If EmployeeInfo.WORK_STATUS Is Nothing Or
                        (EmployeeInfo.WORK_STATUS IsNot Nothing AndAlso
                         (EmployeeInfo.WORK_STATUS <> ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Or
                          (EmployeeInfo.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID And
                           EmployeeInfo.TER_EFFECT_DATE > Date.Now.Date))) Then
                        CurrentState = CommonMessage.STATE_EDIT
                    Else
                        ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                        Exit Sub
                    End If
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgEmployeeTrain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If EmployeeInfo.WORK_STATUS Is Nothing Or
                        (EmployeeInfo.WORK_STATUS IsNot Nothing AndAlso
                         (EmployeeInfo.WORK_STATUS <> ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Or
                          (EmployeeInfo.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID And
                           EmployeeInfo.TER_EFFECT_DATE > Date.Now.Date))) Then
                        ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                        ctrlMessageBox.Show()
                    Else
                        ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                        Exit Sub
                    End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Utilities.GridExportExcel(rgEmployeeTrain, "EmployeeTrain")
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim rep As New ProfileBusinessRepository
                        objTrain.EMPLOYEE_ID = EmployeeInfo.ID
                        objTrain.FROM_DATE = rdTuThang.SelectedDate
                        objTrain.TO_DATE = rdToiThang.SelectedDate
                        objTrain.NAME_SHOOLS = txtTrainingSchool.Text.Trim
                        If cboTrainingForm.SelectedValue = "" Then
                            objTrain.FORM_TRAIN_ID = Nothing
                        Else
                            objTrain.FORM_TRAIN_ID = cboTrainingForm.SelectedValue
                        End If
                        If cboTrainingType.SelectedValue = "" Then
                            objTrain.TYPE_TRAIN_ID = Nothing
                        Else
                            objTrain.TYPE_TRAIN_ID = cboTrainingType.SelectedValue
                        End If
                        objTrain.RECEIVE_DEGREE_DATE = rdReceiveDegree.SelectedDate
                        objTrain.YEAR_GRA = rntGraduateYear.Value
                        objTrain.SPECIALIZED_TRAIN = txtChuyenNganh.Text.Trim
                        objTrain.RESULT_TRAIN = txtKetQua.Text.Trim
                        objTrain.CERTIFICATE = txtBangCap.Text.Trim
                        objTrain.EFFECTIVE_DATE_FROM = rdFrom.SelectedDate
                        objTrain.EFFECTIVE_DATE_TO = rdTo.SelectedDate
                        objTrain.FILE_NAME = txtRemark.Text.Trim
                        If Down_File = Nothing Then
                            objTrain.UPLOAD_FILE = If(txtRemindLink.Text Is Nothing, "", txtRemindLink.Text)
                        Else
                            objTrain.UPLOAD_FILE = If(Down_File Is Nothing, "", Down_File.ToString)
                        End If
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.InsertProcessTraining(objTrain, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT

                                Dim repCheck As New ProfileRepository
                                Dim lst As New List(Of Decimal)
                                For Each dr As GridDataItem In rgEmployeeTrain.SelectedItems
                                    lst.Add(Decimal.Parse(dr("ID").Text))
                                Next

                                If repCheck.CheckExistID(lst, "HU_PRO_TRAIN_OUT_COMPANY", "ID") Then
                                    ShowMessage(Translate("NO_DATA_AVAILABLE"), NotifyType.Warning)
                                    rgEmployeeTrain.Rebind()
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    Refresh("Cancel")
                                    UpdateControlState()
                                    Return
                                End If

                                objTrain.ID = rgEmployeeTrain.SelectedValue
                                If rep.ModifyProcessTraining(objTrain, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objTrain.ID
                                    Refresh("UpdateView")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                                rep.Dispose()
                        End Select
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
            End Select

            UpdateControlState()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ButtonID = MessageBoxButtonType.ButtonYes Then
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE Then
                CurrentState = CommonMessage.STATE_DELETE
            End If
            UpdateControlState()
        End If
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgEmployeeTrain.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim bCheck As Boolean = False

        Try
            'If txtRemark.CheckedItems.Count >= 1 Then
            'Using zip As New ZipFile
            '    zip.AlternateEncodingUsage = ZipOption.AsNecessary
            '    zip.AddDirectoryByName("Files")

            '    For Each item As RadComboBoxItem In txtRemark.CheckedItems
            '        Dim file As System.IO.FileInfo = New System.IO.FileInfo(System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/Profile/Commend/"), item.Text))
            '        If file.Exists Then
            '            zip.AddFile(file.FullName, "Files")
            '        End If
            '    Next

            '    Response.Clear()
            '    Response.BufferOutput = False
            '    Dim zipName As String = [String].Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"))
            '    Response.ContentType = "application/zip"
            '    Response.AddHeader("content-disposition", "attachment; filename=" + zipName)
            '    zip.Save(Response.OutputStream)
            '    Response.[End]()
            'End Using

            'Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/Title/")
            'If Title IsNot Nothing Then
            '    Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/Title/" + Title.UPLOAD_FILE)
            '    'For Each item As RadComboBoxItem In txtRemark.CheckedItems
            '    'Dim file As System.IO.FileInfo = New System.IO.FileInfo(System.IO.Path.Combine(strPath, item.Text))
            '    'If file.Exists Then
            '    bCheck = True
            '    'End If
            '    'Next

            '    If bCheck Then
            '        ZipFiles(strPath)
            '    End If
            'End If
            If txtRemark.Text <> "" Then
                Dim strPath As String

                If txtRemindLink.Text IsNot Nothing Then
                    If txtRemindLink.Text <> "" Then
                        strPath = Server.MapPath("~/ReportTemplates/Profile/TrainOutCompany/" + txtRemindLink.Text)
                        bCheck = True
                    End If
                End If
                If Down_File <> "" Then
                    strPath = Server.MapPath("~/ReportTemplates/Profile/TrainOutCompany/" + Down_File)
                    bCheck = True
                End If
                If bCheck Then
                    ZipFiles(strPath)
                End If
            End If
            'End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ZipFiles(ByVal path As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim crc As New Crc32()
            'Dim fileNameZip As String = "QuanLiChucDanh.zip"
            Dim fileNameZip As String = txtRemark.Text.Trim

            'If Not Directory.Exists(pathZip) Then
            '    Directory.CreateDirectory(pathZip)
            'Else
            '    For Each deleteFile In Directory.GetFiles(pathZip, "*.*", SearchOption.TopDirectoryOnly)
            '        File.Delete(deleteFile)
            '    Next
            'End If

            'Dim s As New ZipOutputStream(File.Create(pathZip & fileNameZip))
            's.SetLevel(0)
            '' 0 - store only to 9 - means best compression
            'For i As Integer = 0 To Directory.GetFiles(path).Length - 1
            '    ' Must use a relative path here so that files show up in the Windows Zip File Viewer
            '    ' .. hence the use of Path.GetFileName(...)
            '    Dim fileName As String = System.IO.Path.GetFileName(Directory.GetFiles(path)(i))

            '    Dim entry As New ZipEntry(fileName)
            '    entry.DateTime = DateTime.Now

            '    ' Read in the 
            '    Using fs As FileStream = File.Open(Directory.GetFiles(path)(i), FileMode.Open)
            '        Dim buffer As Byte() = New Byte(fs.Length - 1) {}
            '        fs.Read(buffer, 0, buffer.Length)
            '        entry.Size = fs.Length
            '        fs.Close()
            '        crc.Reset()
            '        crc.Update(buffer)
            '        entry.Crc = crc.Value
            '        s.PutNextEntry(entry)
            '        s.Write(buffer, 0, buffer.Length)
            '    End Using
            'Next
            's.Finish()
            's.Close()
            'Using FileStream = File.Open(path & fileNameZip, FileMode.Open)
            '    Dim buffer As Byte() = New Byte(FileStream.Length - 1) {}
            '    FileStream.Read(buffer, 0, buffer.Length)
            '    Dim rEx As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9_\-\.]+")
            '    Response.Clear()
            '    Response.AddHeader("Content-Disposition", "attachment; filename=" + rEx.Replace(fileNameZip, "_"))
            '    Response.AddHeader("Content-Length", FileStream.Length.ToString())
            '    Response.ContentType = "application/octet-stream"
            '    Response.BinaryWrite(buffer)
            '    FileStream.Close()
            'End Using

            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path & fileNameZip)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            'Response.ContentType = "application/octet-stream"
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
            Response.WriteFile(file.FullName)
            Response.End()


            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            txtRemark.Text = ""
            Dim listExtension = New List(Of String)
            listExtension.Add(".xls")
            listExtension.Add(".xlsx")
            listExtension.Add(".doc")
            listExtension.Add(".docx")
            listExtension.Add(".pdf")
            listExtension.Add(".jpg")
            listExtension.Add(".png")
            Dim fileName As String

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/TrainOutCompany/")
            If ctrlUpload1.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload1.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(i)
                    Dim str_Filename = Guid.NewGuid.ToString() + "\"
                    If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                        'If Title IsNot Nothing Then
                        '    If Title.UPLOAD_FILE IsNot Nothing Then
                        '        strPath += Title.UPLOAD_FILE
                        '    Else
                        '        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        '        strPath = strPath + str_Filename
                        '    End If
                        '    fileName = System.IO.Path.Combine(strPath, file.FileName)
                        '    file.SaveAs(fileName, True)
                        '    Title.UPLOAD_FILE = str_Filename
                        '    txtUploadFile.Text = file.FileName
                        'Else
                        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        strPath = strPath + str_Filename
                        fileName = System.IO.Path.Combine(strPath, file.FileName)
                        file.SaveAs(fileName, True)
                        txtRemark.Text = file.FileName
                        'End If
                        Down_File = str_Filename
                    Else
                        ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif"), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                loadDatasource(txtRemark.Text)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub loadDatasource(ByVal strUpload As String)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            'Dim data As New DataTable
            'data.Columns.Add("FileName")
            'Dim row As DataRow
            'Dim str() As String

            If strUpload <> "" Then
                txtRemark.Text = strUpload
                FileOldName = txtRemark.Text
                txtRemark.Text = strUpload
                'str = strUpload.Split(";")

                'For Each s As String In str
                '    If s <> "" Then
                '        row = data.NewRow
                '        row("FileName") = s
                '        data.Rows.Add(row)
                '    End If
                'Next

                'txtRemark.DataSource = data
                'txtRemark.DataTextField = "FileName"
                'txtRemark.DataValueField = "FileName"
                'txtRemark.DataBind()
            Else
                'txtRemark.DataSource = Nothing
                'txtRemark.ClearSelection()
                'txtRemark.ClearCheckedItems()
                'txtRemark.Items.Clear()
                strUpload = String.Empty
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    'Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgEmployeeTrain.ItemDataBound
    '    Try
    '        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
    '            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
    '            If IDSelect <> 0 Then
    '                If IDSelect = datarow.GetDataKeyValue("ID") Then
    '                    e.Item.Selected = True
    '                End If
    '            End If

    '            Dim a = CType(datarow.DataItem, EmployeeTrainDTO)
    '            Dim strFromMonthYear As String = ""
    '            Dim strToMonthYear As String = ""
    '            Dim lblFromMonthYear As Label = CType(datarow.FindControl("lblFromMonthYear"), Label)
    '            Dim lblToMonthYear As Label = CType(datarow.FindControl("lblToMonthYear"), Label)

    '            strFromMonthYear = a.FMONTH.ToString + "/" + a.FYEAR.ToString
    '            strToMonthYear = a.TMONTH.ToString + "/" + a.TYEAR.ToString

    '            If lblFromMonthYear IsNot Nothing Then
    '                lblFromMonthYear.Text = strFromMonthYear
    '            End If
    '            If lblToMonthYear IsNot Nothing Then
    '                lblToMonthYear.Text = strToMonthYear
    '            End If
    '        End If
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try

    'End Sub
#End Region

#Region "Custom"
    Private Sub cvalToiThang_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalToiThang.ServerValidate
        Try
            If rdToiThang.SelectedDate IsNot Nothing Then
                If rdTuThang.SelectedDate > rdToiThang.SelectedDate Then
                    args.IsValid = False
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

    Protected Sub rgData_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgEmployeeTrain.SelectedIndexChanged
        Dim dataItem = TryCast(rgEmployeeTrain.SelectedItems(0), GridDataItem)
        If dataItem IsNot Nothing Then
            rdTuThang.SelectedDate = dataItem.GetDataKeyValue("FROM_DATE")
            rdToiThang.SelectedDate = dataItem.GetDataKeyValue("TO_DATE")
            rntGraduateYear.Value = Double.Parse(dataItem.GetDataKeyValue("YEAR_GRA"))
            txtTrainingSchool.Text = dataItem.GetDataKeyValue("NAME_SHOOLS")
            cboTrainingForm.SelectedValue = dataItem.GetDataKeyValue("FORM_TRAIN_ID")
            txtChuyenNganh.Text = dataItem.GetDataKeyValue("SPECIALIZED_TRAIN")
            cboTrainingType.SelectedValue = dataItem.GetDataKeyValue("TYPE_TRAIN_ID")
            txtKetQua.Text = dataItem.GetDataKeyValue("RESULT_TRAIN")
            txtBangCap.Text = dataItem.GetDataKeyValue("CERTIFICATE")
            rdReceiveDegree.SelectedDate = dataItem.GetDataKeyValue("RECEIVE_DEGREE_DATE")
            rdFrom.SelectedDate = dataItem.GetDataKeyValue("EFFECTIVE_DATE_FROM")
            rdTo.SelectedDate = dataItem.GetDataKeyValue("EFFECTIVE_DATE_TO")
            txtRemindLink.Text = dataItem.GetDataKeyValue("UPLOAD_FILE")
            txtRemark.Text = dataItem.GetDataKeyValue("FILE_NAME")
        End If
    End Sub
End Class
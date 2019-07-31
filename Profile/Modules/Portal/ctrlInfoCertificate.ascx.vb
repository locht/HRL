Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Common.Common
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports Ionic.Zip
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports Ionic.Crc


Public Class ctrlInfoCertificate
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"
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
    Private Property tempPathFile As String
        Get
            Return PageViewState(Me.ID & "_tempPathFile")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_tempPathFile") = value
        End Set
    End Property
    Public Property EmployeeID As Decimal
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            rgCetificate.SetFilter()
            rgCetificateEdit.SetFilter()
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
      
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Seperator,
                         ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Seperator,
                         ToolbarItem.Submit)
            CType(Me.MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Public Overrides Sub BindData()
        Try
            Dim ComboBoxDataDTO As New ComboBoxDataDTO
            Using rep As New ProfileRepository
                
                ComboBoxDataDTO.GET_FIELD_TRAIN = True
                ComboBoxDataDTO.GET_MAJOR_TRAIN = True
                ComboBoxDataDTO.GET_LEVEL_TRAIN = True
                rep.GetComboList(ComboBoxDataDTO)
                If ComboBoxDataDTO IsNot Nothing Then
                    FillDropDownList(cbField, ComboBoxDataDTO.LIST_FIELD_TRAIN, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cbField.SelectedValue)
                    FillDropDownList(cbLevel, ComboBoxDataDTO.LIST_LEVEL_TRAIN, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cbLevel.SelectedValue)
                    FillDropDownList(cbMajor, ComboBoxDataDTO.LIST_MAJOR_TRAIN, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cbMajor.SelectedValue)

                End If
            End Using

            Dim dic As New Dictionary(Of String, Control)
            'dic.Add("ADDRESS", txtAdress)
            'dic.Add("ID_NO", txtIDNO)
            'dic.Add("FULLNAME", txtFullName)
            'dic.Add("TAXTATION", txtTax)
            'dic.Add("REMARK", txtRemark)
            'dic.Add("BIRTH_DATE", rdBirthDate)
            'dic.Add("IS_DEDUCT", chkIsDeduct)
            'dic.Add("DEDUCT_REG", rdDeductReg)
            'dic.Add("DEDUCT_FROM", rdDeductFrom)
            'dic.Add("DEDUCT_TO", rdDeductTo)
            'dic.Add("RELATION_ID", cboRelationship)
            'dic.Add("ID", hidFamilyID)
            'dic.Add("CAREER", txtCareer)
            'dic.Add("TITLE_NAME", txtTitle)
            'dic.Add("PROVINCE_ID", cboNguyenQuan)
            Utilities.OnClientRowSelectedChanged(rgCetificate, dic)

            Dim dic1 As New Dictionary(Of String, Control)
            dic1.Add("CODE_CERTIFICATE", txtCodeCertificate)
            dic1.Add("FIELD", cbField)
            dic1.Add("FROM_DATE", rdFromDate)
            dic1.Add("TO_DATE", rdToDate)
            dic1.Add("REMARK", txtRemark)
            dic1.Add("MAJOR", cbMajor)
            dic1.Add("LEVEL", cbLevel)
            dic1.Add("MARK", txtMark)
            dic1.Add("CONTENT_NAME", txtContentTrain)
            dic1.Add("TYPE_NAME", txtTypeTrain)
            dic1.Add("EFFECT_FROM", rdEffectFrom)
            dic1.Add("EFFECT_TO", rdEffectTo)
            dic1.Add("ID", hidID)
            dic1.Add("FK_PKEY", hidFamilyID)
            dic1.Add("CLASSIFICATION", txtClassification)
            dic1.Add("YEAR", txtYear)
            dic1.Add("FILENAME", txtUploadFile)
            Utilities.OnClientRowSelectedChanged(rgCetificateEdit, dic1)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    'dung
                    EnableControlAll(False, cbField, rdFromDate, rdToDate, txtSchool, cbMajor, cbLevel,
                                     txtMark, txtContentTrain, txtTypeTrain, txtCodeCertificate, rdEffectFrom,
                                     rdEffectTo, txtClassification, txtYear, is_Renew, txtRemark, txtUploadFile, btnUploadFile, btnDownload)

                    'If Not chkIsDeduct.Checked Then
                    '    chkIsDeduct_CheckedChanged(Nothing, Nothing)
                    'End If
                    'chkIsDeduct.AutoPostBack = False
                    EnabledGridNotPostback(rgCetificate, True)
                    EnabledGridNotPostback(rgCetificateEdit, True)
                Case CommonMessage.STATE_NEW
                    'dung
                    EnableControlAll(True, cbField, rdFromDate, rdToDate, txtSchool, cbMajor, cbLevel,
                                     txtMark, txtContentTrain, txtTypeTrain, txtCodeCertificate, rdEffectFrom,
                                     rdEffectTo, txtClassification, txtYear, is_Renew, txtRemark, txtUploadFile, btnUploadFile, btnDownload)

                    'If Not chkIsDeduct.Checked Then
                    '    chkIsDeduct_CheckedChanged(Nothing, Nothing)
                    'End If
                    'chkIsDeduct.AutoPostBack = True
                    EnabledGridNotPostback(rgCetificate, False)
                    EnabledGridNotPostback(rgCetificateEdit, False)
                Case CommonMessage.STATE_EDIT
                    'dung
                    EnableControlAll(True, cbField, rdFromDate, rdToDate, txtSchool, cbMajor, cbLevel,
                                     txtMark, txtContentTrain, txtTypeTrain, txtCodeCertificate, rdEffectFrom,
                                     rdEffectTo, txtClassification, txtYear, is_Renew, txtRemark, txtUploadFile, btnUploadFile, btnDownload)

                    'If Not chkIsDeduct.Checked Then
                    '    chkIsDeduct_CheckedChanged(Nothing, Nothing)
                    'End If
                    'chkIsDeduct.AutoPostBack = True
                    EnabledGridNotPostback(rgCetificate, False)
                    EnabledGridNotPostback(rgCetificateEdit, False)
            End Select

            ChangeToolbarState()

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    'dung
                    ClearControlValue(cbField, rdFromDate, rdToDate, txtSchool, cbMajor, cbLevel,
                                     txtMark, txtContentTrain, txtTypeTrain, txtCodeCertificate, rdEffectFrom,
                                     rdEffectTo, txtClassification, txtYear, is_Renew, txtRemark, txtUploadFile, btnUploadFile, btnDownload, hidFamilyID)
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim isInsert As Boolean = True

                        Dim obj As New CETIFICATE_EDITDTO
                        obj.EMPLOYEE_ID = EmployeeID
                        If cbField.SelectedValue <> "" Then
                            obj.FIELD = cbField.SelectedValue
                        End If
                        obj.FROM_DATE = rdFromDate.SelectedDate
                        obj.TO_DATE = rdToDate.SelectedDate
                        obj.SCHOOL_NAME = txtSchool.Text
                        If cbMajor.SelectedValue <> "" Then
                            obj.MAJOR = cbMajor.SelectedValue
                        End If
                        If cbLevel.SelectedValue <> "" Then
                            obj.LEVEL = cbLevel.SelectedValue
                        End If
                        If txtMark.Text <> "" Then
                            obj.MARK = txtMark.Text
                        End If

                        obj.CONTENT_NAME = txtContentTrain.Text
                        obj.TYPE_NAME = txtTypeTrain.Text
                        obj.CODE_CERTIFICATE = txtCodeCertificate.Text
                        obj.EFFECT_FROM = rdEffectFrom.SelectedDate
                        obj.EFFECT_TO = rdEffectTo.SelectedDate
                        obj.CLASSIFICATION = txtClassification.Text
                        If txtYear.Text <> "" Then
                            obj.YEAR = txtYear.Text
                        End If
                        obj.RENEW = is_Renew.Checked
                        obj.REMARK = txtRemark.Text
                        obj.FILENAME = txtUploadFile.Text
                        If Down_File = Nothing Then
                            obj.UPLOAD = If(txtRemindLink.Text Is Nothing, "", txtRemindLink.Text)
                        Else
                            obj.UPLOAD = If(Down_File Is Nothing, "", Down_File.ToString)
                        End If
                        Using rep As New ProfileBusinessRepository
                            If hidFamilyID.Value <> "" Then
                                obj.FK_PKEY = hidFamilyID.Value
                                Dim bCheck = rep.CheckExistCertificateEdit(hidFamilyID.Value)
                                If bCheck IsNot Nothing Then
                                    Dim status = bCheck.STATUS
                                    Dim pkey = bCheck.ID
                                    ' Trạng thái chờ phê duyệt
                                    If status = 1 Then
                                        ShowMessage("Thông đang đang Chờ phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                                        Exit Sub
                                    End If
                                    isInsert = False
                                    obj.ID = pkey
                                End If
                            End If
                            If hidID.Value <> "" Then
                                isInsert = False
                            End If
                            If isInsert Then
                                rep.InsertCertificateEdit(obj, 0)
                            Else
                                obj.ID = hidID.Value
                                rep.ModifyCertificateEdit(obj, 0)
                            End If
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgCetificate.Rebind()
                            rgCetificateEdit.Rebind()
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                        End Using
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL

                    If rgCetificateEdit.SelectedItems.Count > 0 Then
                        Dim item = CType(rgCetificateEdit.SelectedItems(rgCetificateEdit.SelectedItems.Count - 1), GridDataItem)
                        hidFamilyID.Value = item.GetDataKeyValue("ID")
                        'txtAdress.Text = item.GetDataKeyValue("ADDRESS")
                        'txtIDNO.Text = item.GetDataKeyValue("ID_NO")
                        'txtFullName.Text = item.GetDataKeyValue("FULLNAME")
                        'txtTax.Text = item.GetDataKeyValue("TAXTATION")
                        'txtRemark.Text = item.GetDataKeyValue("REMARK")
                        'rdBirthDate.SelectedDate = item.GetDataKeyValue("BIRTH_DATE")
                        'chkIsDeduct.Checked = item.GetDataKeyValue("IS_DEDUCT")
                        'rdDeductReg.SelectedDate = item.GetDataKeyValue("DEDUCT_REG")
                        'rdDeductFrom.SelectedDate = item.GetDataKeyValue("DEDUCT_FROM")
                        'rdDeductTo.SelectedDate = item.GetDataKeyValue("DEDUCT_TO")
                        'cboRelationship.SelectedValue = item.GetDataKeyValue("RELATION_ID")
                        'txtCareer.Text = item.GetDataKeyValue("CAREER")
                        'txtTitle.Text = item.GetDataKeyValue("TITLE_NAME")
                        'cboNguyenQuan.SelectedValue = item.GetDataKeyValue("PROVINCE_ID")
                        'txtHouseCertificate_Code.Text = item.GetDataKeyValue("CERTIFICATE_CODE")
                        'txtHouseCertificate_Num.Text = item.GetDataKeyValue("CERTIFICATE_NUM")
                        'txtTempAdress.Text = item.GetDataKeyValue("ADDRESS_TT")
                        'cbPROVINCE_ID.SelectedValue = item.GetDataKeyValue("AD_PROVINCE_ID")
                        'cbDISTRICT_ID.SelectedValue = item.GetDataKeyValue("AD_DISTRICT_ID")
                        'cbWARD_ID.SelectedValue = item.GetDataKeyValue("AD_WARD_ID")
                        'txtAD_Village.Text = item.GetDataKeyValue("AD_VILLAGE")
                        'cbTempPROVINCE_ID.SelectedValue = item.GetDataKeyValue("TT_PROVINCE_ID")
                        'cbTempDISTRICT_ID.SelectedValue = item.GetDataKeyValue("TT_DISTRICT_ID")
                        'cbTempWARD_ID.SelectedValue = item.GetDataKeyValue("TT_WARD_ID")
                        'chkIs_Owner.Checked = item.GetDataKeyValue("IS_OWNER")
                        'chkIs_Pass.Checked = item.GetDataKeyValue("IS_PASS")
                        If item.GetDataKeyValue("FK_PKEY") IsNot Nothing Then
                            hidFamilyID.Value = item.GetDataKeyValue("FK_PKEY")
                        End If
                        hidID.Value = item.GetDataKeyValue("ID")
                        ' chkIsDeduct_CheckedChanged(Nothing, Nothing)
                    Else
                        'dung
                        ClearControlValue(cbField, rdFromDate, rdToDate, txtSchool, cbMajor, cbLevel,
                                     txtMark, txtContentTrain, txtTypeTrain, txtCodeCertificate, rdEffectFrom,
                                     rdEffectTo, txtClassification, txtYear, is_Renew, txtRemark, txtUploadFile, btnUploadFile, btnDownload)
                    End If


                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_SUBMIT
                    If rgCetificateEdit.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim status As String = ""
                    For Each item As GridDataItem In rgCetificateEdit.SelectedItems
                        status = item.GetDataKeyValue("STATUS")
                        If status = 1 Then
                            ShowMessage("Thông tin đang Chờ phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                            Exit Sub
                        End If
                        If status = 2 Then
                            ShowMessage("Thông tin đang phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                            Exit Sub
                        End If
                        'If status = 3 Then
                        '   ShowMessage("Thông tin đang không phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                        '   Exit Sub
                        'End If
                    Next

                    ctrlMessageBox.MessageText = Translate("Thông tin đã gửi duyệt sẽ không được chỉnh sửa. Bạn chắc chắn muốn gửi duyệt?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()


            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgCetificate_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCetificate.NeedDataSource
        Try
            SetValueObjectByRadGrid(rgCetificate, New CETIFICATEDTO)

            Using rep As New ProfileBusinessRepository
                rgCetificate.DataSource = rep.GetCertificate(New CETIFICATEDTO With {.EMPLOYEE_ID = EmployeeID})
            End Using
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgCetificateEdit_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCetificateEdit.NeedDataSource
        Try
            SetValueObjectByRadGrid(rgCetificateEdit, New CETIFICATE_EDITDTO)
            Using rep As New ProfileBusinessRepository
                rgCetificateEdit.DataSource = rep.GetCertificateEdit(New CETIFICATE_EDITDTO With {.EMPLOYEE_ID = EmployeeID})
            End Using
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgCetificateEdit_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgCetificateEdit.ItemCommand
        Try
            If e.CommandName = "EditRow" Then
                Dim item = CType(e.Item, GridDataItem)
                Dim status As String = ""
                If item.GetDataKeyValue("STATUS") IsNot Nothing Then
                    status = item.GetDataKeyValue("STATUS")
                End If
                Select Case status
                    Case 1
                        ShowMessage("Bản ghi đang Chờ phê duyệt chỉ được xem thông tin", NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case 2
                        ShowMessage("Bản ghi đã Phê duyệt chỉ được xem thông tin", NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case Else
                        CurrentState = CommonMessage.STATE_EDIT
                End Select
                hidFamilyID.Value = item.GetDataKeyValue("ID")
                cbField.SelectedValue = item.GetDataKeyValue("FIELD")
                cbField.Text = item.GetDataKeyValue("FIELD_NAME")
                rdFromDate.SelectedDate = item.GetDataKeyValue("FROM_DATE")
                rdToDate.SelectedDate = item.GetDataKeyValue("TO_DATE")
                txtSchool.Text = item.GetDataKeyValue("SCHOOL_NAME")
                cbMajor.SelectedValue = item.GetDataKeyValue("MAJOR")
                cbMajor.Text = item.GetDataKeyValue("MAJOR_NAME")
                cbLevel.SelectedValue = item.GetDataKeyValue("LEVEL")
                cbLevel.Text = item.GetDataKeyValue("LEVEL_NAME")
                txtMark.Text = item.GetDataKeyValue("MARK")
                txtContentTrain.Text = item.GetDataKeyValue("CONTENT_NAME")
                txtTypeTrain.Text = item.GetDataKeyValue("TYPE_NAME")
                txtCodeCertificate.Text = item.GetDataKeyValue("CODE_CERTIFICATE")
                rdEffectFrom.SelectedDate = item.GetDataKeyValue("EFFECT_FROM")
                rdEffectTo.SelectedDate = item.GetDataKeyValue("EFFECT_TO")
                txtClassification.Text = item.GetDataKeyValue("CLASSIFICATION")
                txtYear.Text = item.GetDataKeyValue("YEAR")
                is_Renew.Checked = item.GetDataKeyValue("RENEW")
                txtRemark.Text = item.GetDataKeyValue("REMARK")
                txtUploadFile.Text = item.GetDataKeyValue("FILENAME")
                txtRemindLink.Text = item.GetDataKeyValue("UPLOAD")
                If item.GetDataKeyValue("FK_PKEY") IsNot Nothing Then
                    hidFamilyID.Value = item.GetDataKeyValue("FK_PKEY")
                End If
                hidID.Value = item.GetDataKeyValue("ID")
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgCetificate_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgCetificate.ItemCommand
        Try
            If e.CommandName = "EditRow" Then
                CurrentState = CommonMessage.STATE_EDIT
                Dim item = CType(e.Item, GridDataItem)
                hidFamilyID.Value = item.GetDataKeyValue("ID")
                'txtAdress.Text = item.GetDataKeyValue("ADDRESS")
                'txtIDNO.Text = item.GetDataKeyValue("ID_NO")
                'txtFullName.Text = item.GetDataKeyValue("FULLNAME")
                'txtTax.Text = item.GetDataKeyValue("TAXTATION")
                'txtRemark.Text = item.GetDataKeyValue("REMARK")
                'rdBirthDate.SelectedDate = item.GetDataKeyValue("BIRTH_DATE")
                'chkIsDeduct.Checked = item.GetDataKeyValue("IS_DEDUCT")
                'rdDeductReg.SelectedDate = item.GetDataKeyValue("DEDUCT_REG")
                'rdDeductFrom.SelectedDate = item.GetDataKeyValue("DEDUCT_FROM")
                'rdDeductTo.SelectedDate = item.GetDataKeyValue("DEDUCT_TO")
                'cboRelationship.SelectedValue = item.GetDataKeyValue("RELATION_ID")
                'txtCareer.Text = item.GetDataKeyValue("CAREER")
                'txtTitle.Text = item.GetDataKeyValue("TITLE_NAME")
                'cboNguyenQuan.SelectedValue = item.GetDataKeyValue("PROVINCE_ID")
                'txtHouseCertificate_Code.Text = item.GetDataKeyValue("CERTIFICATE_CODE")
                'txtHouseCertificate_Num.Text = item.GetDataKeyValue("CERTIFICATE_NUM")
                'txtTempAdress.Text = item.GetDataKeyValue("ADDRESS_TT")
                'txtAD_Village.Text = item.GetDataKeyValue("AD_VILLAGE")
                'If IsNumeric(item.GetDataKeyValue("AD_PROVINCE_ID")) Then
                '    cbPROVINCE_ID.SelectedValue = item.GetDataKeyValue("AD_PROVINCE_ID")
                'End If
                'If IsNumeric(item.GetDataKeyValue("TT_PROVINCE_ID")) Then
                '    cbTempPROVINCE_ID.SelectedValue = item.GetDataKeyValue("TT_PROVINCE_ID")
                'End If
                'Using rep As New ProfileRepository
                '    If cbPROVINCE_ID.SelectedValue <> "" Then
                '        Dim dt As DataTable = rep.GetDistrictList(cbPROVINCE_ID.SelectedValue, False)
                '        FillRadCombobox(cbDISTRICT_ID, dt, "NAME", "ID")
                '    End If
                '    If IsNumeric(item.GetDataKeyValue("AD_DISTRICT_ID")) Then
                '        cbDISTRICT_ID.SelectedValue = item.GetDataKeyValue("AD_DISTRICT_ID")
                '    End If
                '    If cbDISTRICT_ID.SelectedValue <> "" Then
                '        Dim dt As DataTable = rep.GetWardList(cbDISTRICT_ID.SelectedValue, False)
                '        FillRadCombobox(cbWARD_ID, dt, "NAME", "ID")
                '    End If
                '    If IsNumeric(item.GetDataKeyValue("AD_WARD_ID")) Then
                '        cbWARD_ID.SelectedValue = item.GetDataKeyValue("AD_WARD_ID")
                '    End If
                '    If cbTempPROVINCE_ID.SelectedValue <> "" Then
                '        Dim dt As DataTable = rep.GetDistrictList(cbTempPROVINCE_ID.SelectedValue, False)
                '        FillRadCombobox(cbTempDISTRICT_ID, dt, "NAME", "ID")
                '    End If
                '    If IsNumeric(item.GetDataKeyValue("TT_DISTRICT_ID")) Then
                '        cbTempDISTRICT_ID.SelectedValue = item.GetDataKeyValue("TT_DISTRICT_ID")
                '    End If
                '    If cbTempDISTRICT_ID.SelectedValue <> "" Then
                '        Dim dt As DataTable = rep.GetWardList(cbTempDISTRICT_ID.SelectedValue, False)
                '        FillRadCombobox(cbTempWARD_ID, dt, "NAME", "ID")
                '    End If
                '    If IsNumeric(item.GetDataKeyValue("TT_WARD_ID")) Then
                '        cbTempWARD_ID.SelectedValue = item.GetDataKeyValue("TT_WARD_ID")
                '    End If
                'End Using
                'chkIs_Owner.Checked = item.GetDataKeyValue("IS_OWNER")
                'chkIs_Pass.Checked = item.GetDataKeyValue("IS_PASS")
                hidFamilyID.Value = item.GetDataKeyValue("ID")
                hidID.Value = ""
                ' chkIsDeduct_CheckedChanged(Nothing, Nothing)
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

 

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Using rep As New ProfileBusinessRepository
                    Dim lstID As New List(Of Decimal)
                    For Each item As GridDataItem In rgCetificateEdit.SelectedItems
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next
                    If lstID.Count > 0 Then
                        rep.SendCertificateEdit(lstID)
                    End If
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgCetificate.Rebind()
                    rgCetificateEdit.Rebind()
                End Using
            End If


        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"

#End Region

    
    Private Sub btnUploadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFile.Click

        ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,pdf"
        ctrlUpload1.Show()

    End Sub
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            txtUploadFile.Text = ""
            Dim listExtension = New List(Of String)
            listExtension.Add(".xls")
            listExtension.Add(".xlsx")
            listExtension.Add(".doc")
            listExtension.Add(".docx")
            listExtension.Add(".pdf")
            listExtension.Add(".jpg")
            listExtension.Add(".png")
            Dim fileName As String

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/CETIFICATE/")
            If ctrlUpload1.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload1.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(i)
                    Dim str_Filename = Guid.NewGuid.ToString() + "\"
                    If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                        
                        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        strPath = strPath + str_Filename
                        fileName = System.IO.Path.Combine(strPath, file.FileName)
                        file.SaveAs(fileName, True)
                        txtUploadFile.Text = file.FileName
                        'End If
                        Down_File = str_Filename
                    Else
                        ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif"), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                loadDatasource(txtUploadFile.Text)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim bCheck As Boolean = False

        Try
            If txtUploadFile.Text <> "" Then
                Dim strPath As String

                If txtRemindLink.Text IsNot Nothing Then
                    If txtRemindLink.Text <> "" Then
                        strPath = Server.MapPath("~/ReportTemplates/Profile/CETIFICATE/" + txtRemindLink.Text)
                        bCheck = True
                    End If
                End If
                If Down_File <> "" Then
                    strPath = Server.MapPath("~/ReportTemplates/Profile/CETIFICATE/" + Down_File)
                    bCheck = True
                End If
                If bCheck Then
                    ZipFiles(strPath)
                End If
            End If
            'End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ZipFiles(ByVal path As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim crc As New CRC32()
            'Dim fileNameZip As String = "QuanLiChucDanh.zip"
            Dim fileNameZip As String = txtRemark.Text.Trim
            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path & fileNameZip)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            'Response.ContentType = "application/octet-stream"
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
            Response.WriteFile(file.FullName)
            Response.End()


        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
        End Try
    End Sub
    Private Sub loadDatasource(ByVal strUpload As String)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If strUpload <> "" Then
                txtUploadFile.Text = strUpload
                FileOldName = txtRemark.Text
                txtRemark.Text = strUpload
            Else
                strUpload = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    

    Protected Sub rgFamilyEdit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rgCetificateEdit.SelectedIndexChanged
        Try
            'dung
            ClearControlValue(cbField, rdFromDate, rdToDate, txtSchool, cbMajor, cbLevel,
                                     txtMark, txtContentTrain, txtTypeTrain, txtCodeCertificate, rdEffectFrom,
                                     rdEffectTo, txtClassification, txtYear, is_Renew, txtRemark, txtUploadFile, btnUploadFile, btnDownload)

            Dim item = CType(rgCetificateEdit.SelectedItems(rgCetificateEdit.SelectedItems.Count - 1), GridDataItem)
            CurrentState = CommonMessage.STATE_NORMAL
            hidFamilyID.Value = item.GetDataKeyValue("ID")
            cbField.SelectedValue = item.GetDataKeyValue("FIELD")
            cbField.Text = item.GetDataKeyValue("FIELD_NAME")
            rdFromDate.SelectedDate = item.GetDataKeyValue("FROM_DATE")
            rdToDate.SelectedDate = item.GetDataKeyValue("TO_DATE")
            txtSchool.Text = item.GetDataKeyValue("SCHOOL_NAME")
            cbMajor.SelectedValue = item.GetDataKeyValue("MAJOR")
            cbMajor.Text = item.GetDataKeyValue("MAJOR_NAME")
            cbLevel.SelectedValue = item.GetDataKeyValue("LEVEL")
            cbLevel.Text = item.GetDataKeyValue("LEVEL_NAME")
            txtMark.Text = item.GetDataKeyValue("MARK")
            txtContentTrain.Text = item.GetDataKeyValue("CONTENT_NAME")
            txtTypeTrain.Text = item.GetDataKeyValue("TYPE_NAME")
            txtCodeCertificate.Text = item.GetDataKeyValue("CODE_CERTIFICATE")
            rdEffectFrom.SelectedDate = item.GetDataKeyValue("EFFECT_FROM")
            rdEffectTo.SelectedDate = item.GetDataKeyValue("EFFECT_TO")
            txtClassification.Text = item.GetDataKeyValue("CLASSIFICATION")
            txtYear.Text = item.GetDataKeyValue("YEAR")
            is_Renew.Checked = item.GetDataKeyValue("RENEW")
            txtRemark.Text = item.GetDataKeyValue("REMARK")
            txtUploadFile.Text = item.GetDataKeyValue("FILENAME")
            txtRemindLink.Text = item.GetDataKeyValue("UPLOAD")
            If item.GetDataKeyValue("FK_PKEY") IsNot Nothing Then
                hidFamilyID.Value = item.GetDataKeyValue("FK_PKEY")
            End If
            hidID.Value = item.GetDataKeyValue("ID")
            'chkIsDeduct_CheckedChanged(Nothing, Nothing)
            UpdateControlState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rgCetificate_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rgCetificate.SelectedIndexChanged
        Try
            If rgCetificate.SelectedItems.Count = 0 Then
                'dung
                ClearControlValue(cbField, rdFromDate, rdToDate, txtSchool, cbMajor, cbLevel,
                                     txtMark, txtContentTrain, txtTypeTrain, txtCodeCertificate, rdEffectFrom,
                                     rdEffectTo, txtClassification, txtYear, is_Renew, txtRemark, txtUploadFile, btnUploadFile, btnDownload)
                Exit Sub
            End If
            'dung
            ClearControlValue(cbField, rdFromDate, rdToDate, txtSchool, cbMajor, cbLevel,
                                     txtMark, txtContentTrain, txtTypeTrain, txtCodeCertificate, rdEffectFrom,
                                     rdEffectTo, txtClassification, txtYear, is_Renew, txtRemark, txtUploadFile, btnUploadFile, btnDownload)
            CurrentState = CommonMessage.STATE_NORMAL
            Dim item = CType(rgCetificate.SelectedItems(rgCetificate.SelectedItems.Count - 1), GridDataItem)
            hidFamilyID.Value = item.GetDataKeyValue("ID")
            'txtAdress.Text = item.GetDataKeyValue("ADDRESS")
            'txtIDNO.Text = item.GetDataKeyValue("ID_NO")
            'txtFullName.Text = item.GetDataKeyValue("FULLNAME")
            'txtTax.Text = item.GetDataKeyValue("TAXTATION")
            'txtRemark.Text = item.GetDataKeyValue("REMARK")
            'rdBirthDate.SelectedDate = item.GetDataKeyValue("BIRTH_DATE")
            'chkIsDeduct.Checked = item.GetDataKeyValue("IS_DEDUCT")
            'rdDeductReg.SelectedDate = item.GetDataKeyValue("DEDUCT_REG")
            'rdDeductFrom.SelectedDate = item.GetDataKeyValue("DEDUCT_FROM")
            'rdDeductTo.SelectedDate = item.GetDataKeyValue("DEDUCT_TO")
            'cboRelationship.SelectedValue = item.GetDataKeyValue("RELATION_ID")
            'txtCareer.Text = item.GetDataKeyValue("CAREER")
            'txtTitle.Text = item.GetDataKeyValue("TITLE_NAME")
            'cboNguyenQuan.SelectedValue = item.GetDataKeyValue("PROVINCE_ID")
            'txtHouseCertificate_Code.Text = item.GetDataKeyValue("CERTIFICATE_CODE")
            'txtHouseCertificate_Num.Text = item.GetDataKeyValue("CERTIFICATE_NUM")
            'txtTempAdress.Text = item.GetDataKeyValue("ADDRESS_TT")
            'txtAD_Village.Text = item.GetDataKeyValue("AD_VILLAGE")
            'Using rep As New ProfileRepository
            '    If cbPROVINCE_ID.SelectedValue <> "" Then
            '        Dim dt As DataTable = rep.GetDistrictList(cbPROVINCE_ID.SelectedValue, False)
            '        FillRadCombobox(cbDISTRICT_ID, dt, "NAME", "ID")
            '    End If
            '    If IsNumeric(item.GetDataKeyValue("AD_DISTRICT_ID")) Then
            '        cbDISTRICT_ID.SelectedValue = item.GetDataKeyValue("AD_DISTRICT_ID")
            '    End If
            '    If cbDISTRICT_ID.SelectedValue <> "" Then
            '        Dim dt As DataTable = rep.GetWardList(cbDISTRICT_ID.SelectedValue, False)
            '        FillRadCombobox(cbWARD_ID, dt, "NAME", "ID")
            '    End If
            '    If IsNumeric(item.GetDataKeyValue("AD_WARD_ID")) Then
            '        cbWARD_ID.SelectedValue = item.GetDataKeyValue("AD_WARD_ID")
            '    End If
            '    If cbTempPROVINCE_ID.SelectedValue <> "" Then
            '        Dim dt As DataTable = rep.GetDistrictList(cbTempPROVINCE_ID.SelectedValue, False)
            '        FillRadCombobox(cbTempDISTRICT_ID, dt, "NAME", "ID")
            '    End If
            '    If IsNumeric(item.GetDataKeyValue("TT_DISTRICT_ID")) Then
            '        cbTempDISTRICT_ID.SelectedValue = item.GetDataKeyValue("TT_DISTRICT_ID")
            '    End If
            '    If cbTempDISTRICT_ID.SelectedValue <> "" Then
            '        Dim dt As DataTable = rep.GetWardList(cbTempDISTRICT_ID.SelectedValue, False)
            '        FillRadCombobox(cbTempWARD_ID, dt, "NAME", "ID")
            '    End If
            '    If IsNumeric(item.GetDataKeyValue("TT_WARD_ID")) Then
            '        cbTempWARD_ID.SelectedValue = item.GetDataKeyValue("TT_WARD_ID")
            '    End If
            'End Using
            'chkIs_Owner.Checked = item.GetDataKeyValue("IS_OWNER")
            'chkIs_Pass.Checked = item.GetDataKeyValue("IS_PASS")
            hidFamilyID.Value = item.GetDataKeyValue("ID")
            hidID.Value = ""
            'chkIsDeduct_CheckedChanged(Nothing, Nothing)
            UpdateControlState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class

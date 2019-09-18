Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Common
Imports Telerik.Web.UI
Imports HistaffFrameworkPublic
Imports Ionic.Zip
Imports System.IO
Imports System.Globalization

Public Class ctrlHU_ContractAppendix
    Inherits Common.CommonView

#Region "Property"
    Private psp As New ProfileStoreProcedure
    Property Contract As FileContractDTO
        Get
            Return ViewState(Me.ID & "_Contract")
        End Get
        Set(ByVal value As FileContractDTO)
            ViewState(Me.ID & "_Contract") = value
        End Set
    End Property
    Private Property dtLogs As DataTable
        Get
            Return PageViewState(Me.ID & "_dtLogs")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtLogs") = value
        End Set
    End Property


    Property Contracts As List(Of FileContractDTO)
        Get
            Return ViewState(Me.ID & "_Contracts")
        End Get
        Set(ByVal value As List(Of FileContractDTO))
            ViewState(Me.ID & "_Contracts") = value
        End Set
    End Property

    Property InsertContracts As FileContractDTO
        Get
            Return ViewState(Me.ID & "_InsertContracts")
        End Get
        Set(ByVal value As FileContractDTO)
            ViewState(Me.ID & "_InsertContracts") = value
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

    Property DeleteContract As FileContractDTO
        Get
            Return ViewState(Me.ID & "_DeleteContract")
        End Get
        Set(ByVal value As FileContractDTO)
            ViewState(Me.ID & "_DeleteContract") = value
        End Set
    End Property

    Public Property popupId As String
    Public Property popupId2 As String
    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsUpdate")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsUpdate") = value
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
    Public Property _filter As FileContractDTO
        Get
            If PageViewState(Me.ID & "_filter") Is Nothing Then
                PageViewState(Me.ID & "_filter") = New FileContractDTO
            End If
            Return PageViewState(Me.ID & "_filter")
        End Get
        Set(ByVal value As FileContractDTO)
            PageViewState(Me.ID & "_filter") = value
        End Set
    End Property

    Private Property lstData As List(Of FileContractDTO)
        Get
            Return ViewState(Me.ID & "_lstData")
        End Get
        Set(ByVal value As List(Of FileContractDTO))
            ViewState(Me.ID & "_lstData") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()

            'Check state after add new or modify 
            If Session("Appendix") = "Success" Then
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                Session("Appendix") = Nothing
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            Dim popup As RadWindow
            popup = CType(Me.Page, AjaxPage).PopupWindow
            popup.Title = "Thanh lý hợp đồng"
            popup.Height = 450
            popup.Width = 800
            popupId = popup.ClientID


            Dim popup2 As RadWindow
            popup2 = CType(Me.Page, AjaxPage).PopupWindow
            popup2.Title = "Thông tin nhân viên"
            'popup2.Height = 643
            'popup2.Width = 1350
            popupId2 = popup2.ClientID

            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            SetGridFilter(rgContract)
            rgContract.AllowCustomPaging = True
            rgContract.PageSize = Common.Common.DefaultPageSize
            InitControl()
            If Not IsPostBack Then
                ViewConfig(RadPane1)
                GirdConfig(rgContract)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Try
            GetDataCombo()
            Session("IsNextPage") = Nothing
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarContracts
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_APPROVE,
                                                                      ToolbarIcons.Add,
                                                                      ToolbarAuthorize.Create,
                                                                    Translate("Khai báo PLHĐ")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_PRINT,
                                                                     ToolbarIcons.Print,
                                                                     ToolbarAuthorize.Print,
                                                                     Translate("In")))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_EXPORT,
                                                  ToolbarIcons.Export,
                                                  ToolbarAuthorize.Export,
                                                  Translate("Xuất dữ liệu")))


            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_NEXT,
                                                 ToolbarIcons.Export,
                                                 ToolbarAuthorize.Export,
                                                 Translate("Xuất file mẫu")))

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_IMPORT,
                                                 ToolbarIcons.Import,
                                                 ToolbarAuthorize.Import,
                                                 Translate("Nhập file mẫu")))

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_CREATE_BATCH,
                                                              ToolbarIcons.Add,
                                                              ToolbarAuthorize.None,
                                                              "Phê duyệt hàng loạt"))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_DELETE,
                                                 ToolbarIcons.Delete,
                                                 ToolbarAuthorize.Delete,
                                                 Translate("Xóa")))
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        'Dim rep As New ProfileBusinessRepository
        Try
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation

            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        rgContract.Rebind()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        rgContract.Rebind()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objOrgFunction As New OrganizationDTO
        Dim sError As String = String.Empty
        Dim rep As New ProfileRepository
        Dim sv_sID As String = String.Empty
        Dim sv_sdateliqui As String = String.Empty
        Dim sv_emp As String = ""
        Dim sv_FormID As Decimal = 0
        Dim sourcePath = Server.MapPath("~/ReportTemplates/Profile/LocationInfo/")
        Dim dtData As DataTable
        Dim reportName As String = String.Empty
        Dim reportNameOut As String = "String.Empty"
        Dim folderName As String = "ContractAppendixSupport"
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_PRINT
                    If rgContract.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim tempPath As String = "TemplateDynamic\"
                    If Not File.Exists(System.IO.Path.Combine(Server.MapPath(tempPath), "ContractAppendixSupport\PLHD.doc")) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_EXSIXT_FILE), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim prp As New ProfileBusinessRepository

                    'For Each dr As Telerik.Web.UI.GridDataItem In rgContract.SelectedItems
                    '    sv_sID &= IIf(sv_sID = vbNullString, Decimal.Parse(dr("ID").Text).ToString, "," & Decimal.Parse(dr("ID").Text).ToString)
                    'Next
                    'sv_sID = sv_sID & ","

                    For Each dr As GridDataItem In rgContract.SelectedItems
                        sv_sID &= "|" & IIf(sv_sID = vbNullString, Decimal.Parse(dr("ID").Text).ToString, "," & Decimal.Parse(dr("ID").Text).ToString) & "|"
                    Next

                    dtData = rep.GetHU_DataDynamicContractAppendix(sv_sID, ProfileCommon.HU_TEMPLATE_TYPE.APPENDIX_SUPPORT_ID,
                                                       folderName)

                    ' dtData = rep.PrintFileContract(sv_emp, sv_sID)

                    If dtData.Rows.Count = 1 Then
                        ExportWordMailMerge(System.IO.Path.Combine(Server.MapPath(tempPath), "ContractAppendixSupport\" + (CType(rgContract.SelectedItems(0), GridDataItem)).GetDataKeyValue("CONTRACTTYPE_CODE") + ".doc"),
                                  (CType(rgContract.SelectedItems(0), GridDataItem)).GetDataKeyValue("EMPLOYEE_CODE") + (CType(rgContract.SelectedItems(0), GridDataItem)).GetDataKeyValue("CONTRACTTYPE_CODE") + ".doc",
                                  dtData,
                                  Response,
                                  sourcePath)
                    Else
                        Dim lstFile As List(Of String) = Utilities.SaveMultyFile(dtData, System.IO.Path.Combine(Server.MapPath(tempPath), "ContractAppendixSupport\" + (CType(rgContract.SelectedItems(0), GridDataItem)).GetDataKeyValue("CONTRACTTYPE_CODE") + ".doc"),
                                                                                 (CType(rgContract.SelectedItems(0), GridDataItem)).GetDataKeyValue("CONTRACTTYPE_CODE"))
                        Using zip As New ZipFile
                            zip.AlternateEncodingUsage = ZipOption.AsNecessary
                            zip.AddDirectoryByName("Files")
                            For i As Integer = 0 To lstFile.Count - 1
                                Dim file As System.IO.FileInfo = New System.IO.FileInfo(lstFile(i))
                                If file.Exists Then
                                    zip.AddFile(file.FullName, "Files")
                                End If
                            Next
                            Response.Clear()

                            Dim zipName As String = [String].Format("{0}_{1}.zip", "PLHD", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"))
                            Response.ContentType = "application/zip"
                            Response.AddHeader("content-disposition", "attachment; filename=" + zipName)
                            zip.Save(Response.OutputStream)
                            Response.Flush()
                            Response.SuppressContent = True
                            HttpContext.Current.ApplicationInstance.CompleteRequest()
                        End Using
                        For i As Integer = 0 To lstFile.Count - 1
                            'Delete files
                            Dim file As System.IO.FileInfo = New System.IO.FileInfo(lstFile(i))
                            If file.Exists Then
                                file.Delete()
                            End If
                        Next
                    End If

                    'Dim inforForm = prp.GetContractForm(sv_FormID)

                    'If inforForm Is Nothing Then
                    '    ShowMessage(Translate("Không tìm thấy biểu mẫu phù hợp"), NotifyType.Warning)
                    '    Exit Sub
                    'End If

                    'Dim filePaths() As String = Directory.GetFiles(Server.MapPath("~/Word/Contract/"))

                    'Dim listFile As New List(Of String)

                    ''
                    'If filePaths IsNot Nothing Then
                    '    For Each File As String In filePaths
                    '        listFile.Add(Path.GetFileName(File))
                    '    Next
                    'End If

                    ''kiem tra template bieu mau co hop boi bieu mau duoc chon
                    'If listFile.Any(Function(x) x.Trim().ToUpper() = inforForm.CODE.Trim().ToUpper() + ".DOC" OrElse x.Trim().ToUpper() = inforForm.CODE.Trim().ToUpper() + ".DOCX") Then
                    '    ExportWordMailMerge(System.IO.Path.Combine(Server.MapPath(tempPath), "Contract/" + inforForm.CODE + ".doc"),
                    '          inforForm.NAME_VN + ".doc",
                    '          printData,
                    '          Response)
                    'Else
                    '    ShowMessage(Translate("Không tìm thấy biểu mẫu phù hợp"), NotifyType.Warning)
                    '    Exit Sub
                    'End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Using xls As New ExcelCommon
                        lstData = Contracts.ToList
                        Dim dtData1 = lstData.ToTable
                        If dtData1.Rows.Count > 0 Then
                            rgContract.ExportExcel(Server, Response, dtData1, "Contract")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_CREATE_BATCH
                    BatchApproveListContract()
                Case CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case Common.CommonMessage.TOOLBARITEM_NEXT
                    Dim dataSet As New DataSet
                    Dim dtVariable As New DataTable
                    Dim tempPath = "~/ReportTemplates//Profile//Import//import_phuluchopdong.xls"
                    If Not File.Exists(System.IO.Path.Combine(Server.MapPath(tempPath))) Then
                        ' Mẫu báo cáo không tồn tại
                        ShowMessage(Translate("AT_IMPORTTIMESHEET_NO_EXIST_TEMPLATE"), Framework.UI.Utilities.NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim dsDanhMuc As DataSet
                    Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                         .IS_DISSOLVE = ctrlOrg.IsDissolve}
                    dsDanhMuc = rep.EXPORT_PLHD(_param)

                    Using xls As New AsposeExcelCommon
                        Dim bCheck = xls.ExportExcelTemplate(
                          System.IO.Path.Combine(Server.MapPath(tempPath)), "IMPORT_PLHĐ", dsDanhMuc, Nothing, Response)
                    End Using
                Case Common.CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.isMultiple = False
                    ctrlUpload1.Show()
            End Select
            rep.Dispose()
            'UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_REFRESH And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim str As String = "btnAdvancedFind_Click();"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged

        Try
            rgContract.CurrentPageIndex = 0
            rgContract.MasterTableView.SortExpressions.Clear()
            rgContract.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub RadGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rgContract.SelectedIndexChanged
        Try
            Dim rep As New ProfileRepository

            If rgContract.SelectedItems.Count > 0 Then
                Dim slItem As GridDataItem
                slItem = rgContract.SelectedItems(0)
                hidID.Value = slItem("ID").Text
                Contract = rep.GetFileConTractID(hidID.Value)

                If Contract IsNot Nothing Then
                    'txtEmployee.Text = Contract.EMPLOYEE_NAME
                    'txtOrg.Text = Contract.ORG_NAME
                    'txtTitle.Text = Contract.TITLE_NAME
                    'txtContract.Text = Contract.CONTRACT_NO
                    'If Contract.CONTRACTTYPE_ID IsNot Nothing Then
                    '    cboContractType.SelectedValue = Contract.CONTRACTTYPE_ID
                    'End If
                    'If Contract.APPEND_TYPEID IsNot Nothing Then
                    '    cboAppend_TypeID.SelectedValue = Contract.APPEND_TYPEID
                    'End If
                    'If Contract.STATUS_ID IsNot Nothing Then
                    '    cbStatus.SelectedValue = Contract.STATUS_ID
                    'End If
                    'txtContract_NumAppen.Text = Contract.APPEND_NUMBER
                    'rdStartDate.SelectedDate = Contract.START_DATE.Value
                    'rdExpireDate.SelectedDate = Contract.EXPIRE_DATE.Value
                    txtUploadFile.Text = Contract.FILEUPLOAD
                    loadFileUpdate(Contract)

                End If
            End If
            rep.Dispose()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub loadFileUpdate(ByVal contract As FileContractDTO)
        Dim data As New DataTable
        data.Columns.Add("FileName")
        Dim row As DataRow
        Dim str() As String
        If contract.FILEUPLOAD IsNot Nothing Then
            str = contract.FILEUPLOAD.Split(";")
            For Each s As String In str
                If s <> "" Then
                    row = data.NewRow
                    row("FileName") = s
                    data.Rows.Add(row)
                End If
            Next
            cboUpload.DataSource = data
            cboUpload.DataTextField = "FileName"
            cboUpload.DataValueField = "FileName"
            cboUpload.DataBind()
        Else
            cboUpload.DataSource = Nothing
            cboUpload.ClearSelection()
            cboUpload.ClearCheckedItems()
            cboUpload.Items.Clear()
            contract.FILEUPLOAD = String.Empty
        End If

    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            rgContract.CurrentPageIndex = 0
            rgContract.MasterTableView.SortExpressions.Clear()
            rgContract.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgContract.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub rgContract_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgContract.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
        End If
    End Sub

    'Private Sub ctrlUpload1_OkClicked(sender As Object, e As System.EventArgs) Handles ctrlUpload1.OkClicked
    '    txtUploadFile.Text = ""
    '    Dim fileName As String
    '    If ctrlUpload1.UploadedFiles.Count >= 1 Then
    '        For i = 0 To ctrlUpload1.UploadedFiles.Count - 1
    '            Dim file As UploadedFile = ctrlUpload1.UploadedFiles(i)
    '            If file.GetExtension = ".xls" Or file.GetExtension = ".xlsx" Then
    '                fileName = System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/Profile/Title/"), file.FileName)
    '                file.SaveAs(fileName, True)
    '                If Contract IsNot Nothing Then
    '                    If ctrlUpload1.UploadedFiles.Count >= 2 Then
    '                        If Contract.FILEUPLOAD IsNot Nothing Then
    '                            Contract.FILEUPLOAD = Contract.FILEUPLOAD + ";" + file.FileName + ";"
    '                        Else
    '                            Contract.FILEUPLOAD = file.FileName + ";"
    '                        End If
    '                    Else
    '                        If Contract.FILEUPLOAD IsNot Nothing Then
    '                            Contract.FILEUPLOAD = Contract.FILEUPLOAD + ";" + file.FileName
    '                        Else
    '                            Contract.FILEUPLOAD = file.FileName
    '                        End If

    '                    End If
    '                    txtUploadFile.Text = Contract.FILEUPLOAD
    '                End If
    '            Else
    '                ShowMessage(Translate("Vui lòng chọn file excel !!! Hệ thống chỉ nhận file .xls hoặc .xlsx"), NotifyType.Error)
    '                Exit Sub
    '            End If
    '        Next
    '    End If
    'End Sub

    Private Sub btnDownload_Click(sender As Object, e As System.EventArgs) Handles btnDownload.Click
        Try
            If cboUpload.CheckedItems.Count >= 1 Then
                Using zip As New ZipFile
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary
                    zip.AddDirectoryByName("Files")
                    For Each item As RadComboBoxItem In cboUpload.CheckedItems
                        Dim file As System.IO.FileInfo = New System.IO.FileInfo(System.IO.Path.Combine(Server.MapPath("~/ReportTemplates/Profile/Title/"), item.Text))
                        If file.Exists Then
                            zip.AddFile(file.FullName, "Files")
                        End If
                    Next

                    Response.Clear()
                    Response.BufferOutput = False
                    Dim zipName As String = [String].Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"))
                    Response.ContentType = "application/zip"
                    Response.AddHeader("content-disposition", "attachment; filename=" + zipName)
                    zip.Save(Response.OutputStream)
                    Response.[End]()

                End Using

            End If
        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region "Custom"
    Private Sub ExportOnGrid()
        Utilities.SetValueObjectByRadGrid(rgContract, _filter)

        Dim query = From p In Contracts
        If _filter.EMPLOYEE_CODE <> "" Then
            query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
        End If
        If _filter.EMPLOYEE_NAME <> "" Then
            query = query.Where(Function(f) f.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
        End If
        If _filter.ORG_NAME <> "" Then
            query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
        End If
        If _filter.TITLE_NAME <> "" Then
            query = query.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
        End If
        If _filter.CONTRACT_NO <> "" Then
            query = query.Where(Function(f) f.CONTRACT_NO.ToUpper.Contains(_filter.CONTRACT_NO.ToUpper))
        End If
        If _filter.CONTRACTTYPE_NAME <> "" Then
            query = query.Where(Function(f) f.CONTRACTTYPE_NAME.ToUpper.Contains(_filter.CONTRACTTYPE_NAME.ToUpper))
        End If
        If _filter.START_DATE IsNot Nothing Then
            query = query.Where(Function(p) p.START_DATE IsNot Nothing And p.START_DATE = _filter.START_DATE)
        End If
        If _filter.EXPIRE_DATE IsNot Nothing Then
            query = query.Where(Function(p) p.EXPIRE_DATE IsNot Nothing And p.EXPIRE_DATE = _filter.EXPIRE_DATE)
        End If
        If _filter.SIGNER_NAME <> "" Then
            query = query.Where(Function(f) f.SIGNER_NAME.ToUpper.Contains(_filter.SIGNER_NAME.ToUpper))
        End If
        If _filter.SIGN_DATE IsNot Nothing Then
            query = query.Where(Function(p) p.SIGN_DATE IsNot Nothing And p.SIGN_DATE = _filter.SIGN_DATE)
        End If
        If _filter.STATUS_NAME <> "" Then
            query = query.Where(Function(f) f.STATUS_NAME.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
        End If
        lstData = query.ToList
    End Sub
    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    For Each _item As GridDataItem In rgContract.SelectedItems
                        If _item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Đã được phê duyệt, không thể xóa. Vui lòng kiểm tra lại !"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    If rep.DeleteFileContract(Contract) Then
                        Contract = Nothing
                        Refresh("UpdateView")
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
            End Select
            tbarContracts.Enabled = True
            rgContract.Enabled = True
            ctrlOrg.Enabled = True
            Select Case CurrentState
            End Select
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub GetDataCombo()
        Dim rep As New ProfileRepository
        Try
            Dim appendData As New List(Of ContractTypeDTO)
            appendData = rep.GetListContractType("PLHD")

            appendData.Insert(0, New ContractTypeDTO With {.NAME = "", .ID = 0})

            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_CONTRACTTYPE = True
                ListComboData.GET_DECISION_STATUS = True
                rep.GetComboList(ListComboData)
            End If
            rep.Dispose()
            ' FillDropDownList(cboContractType, ListComboData.LIST_CONTRACTTYPE, "NAME", "ID", Common.Common.SystemLanguage, True, cboContractType.SelectedValue)
            FillDropDownList(cbStatus, ListComboData.LIST_DECISION_STATUS, "NAME_VN", "ID", Common.Common.SystemLanguage, True)
            'FillDropDownList(cboContractType, ListComboData.LIST_CONTRACTTYPE, "NAME", "ID", Common.Common.SystemLanguage, True)
            'FillDropDownList(cboAppend_TypeID, appendData, "NAME", "ID", Common.Common.SystemLanguage, False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CreateDataFilter(Optional ByVal isFull As Boolean = False)
        Dim rep As New ProfileRepository
        Dim _filter As New FileContractDTO
        Try

            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                          .IS_DISSOLVE = ctrlOrg.IsDissolve}

            If txtEmployee.Text <> "" Then
                _filter.EMPLOYEE_CODE = txtEmployee.Text
            End If

            If cbStatus.SelectedValue <> "" Then
                _filter.STATUS_ID = cbStatus.SelectedValue
            End If

            'If txtOrg.Text <> "" Then
            '    _filter.ORG_NAME = txtOrg.Text
            'End If

            'If txtTitle.Text <> "" Then
            '    _filter.TITLE_NAME = txtTitle.Text
            'End If

            'If txtContract.Text <> "" Then
            '    _filter.CONTRACT_NO = txtContract.Text
            'End If

            'If cboContractType.SelectedValue <> "" Then
            '    _filter.CONTRACTTYPE_ID = cboContractType.SelectedValue
            'End If

            'If txtContract_NumAppen.Text <> "" Then
            '    _filter.APPEND_NUMBER = txtContract_NumAppen.Text
            'End If

            'If cboAppend_TypeID.SelectedValue <> "" Then
            '    _filter.APPEND_TYPEID = cboAppend_TypeID.SelectedValue
            'End If
            If rdStartDate.SelectedDate IsNot Nothing Then
                _filter.EFFECT_DATE = rdExpireDate.SelectedDate
            End If
            If rdExpireDate.SelectedDate IsNot Nothing Then
                _filter.EXPIRE_DATE = rdStartDate.SelectedDate
            End If
            _filter.IS_TER = chkTerminate.Checked
            SetValueObjectByRadGrid(rgContract, _filter)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgContract.MasterTableView.SortExpressions.GetSortString()
            Contracts = rep.GetContractAppendixPaging(_filter, rgContract.CurrentPageIndex, rgContract.PageSize, MaximumRows, _param)
            If Contracts IsNot Nothing And Contracts.Count <> 0 Then
                rgContract.DataSource = Contracts
            Else
                rgContract.DataSource = New List(Of FileContractDTO)
            End If
            rep.Dispose()
            rgContract.VirtualItemCount = MaximumRows
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub BatchApproveListContract()
        Dim rep As New ProfileBusinessClient
        '1. Check có rows nào được select hay không
        If rgContract Is Nothing OrElse rgContract.SelectedItems.Count <= 0 Then
            ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
            Exit Sub
        End If

        Dim lstContract As New List(Of FileContractDTO)
        '2. Lấy ID trạng thái phê duyệt
        Dim p_Status_ID As Integer = -1
        p_Status_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID

        '3. Lấy những rows được check chọn và chưa phê duyệt để phê duyệt
        Dim dtb As New DataTable("DATA")
        Dim data As DataRow
        Dim log As New CurrentUserLog
        Dim lstID As New List(Of Decimal)
        log = UserLogHelper.GetCurrentLogUser()

        dtb.Columns.Add("P_ID_CONTRACT", GetType(Integer))
        dtb.Columns.Add("P_MODIFIED_BY", GetType(String))
        dtb.Columns("P_MODIFIED_BY").DefaultValue = log.Username
        dtb.Columns.Add("P_MODIFIED_LOG", GetType(String))
        dtb.Columns("P_MODIFIED_LOG").DefaultValue = log.Ip & "-" & log.ComputerName
        dtb.Columns.Add("P_STATUS", GetType(Integer))
        dtb.Columns("P_STATUS").DefaultValue = p_Status_ID

        For Each dr As Telerik.Web.UI.GridDataItem In rgContract.SelectedItems
            Dim ID As New Decimal
            If dr("STATUS_ID").Text <> ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                data = dtb.NewRow()
                data("P_ID_CONTRACT") = dr("ID").Text
                dtb.Rows.Add(data)
                ID = dr("ID").Text
                lstID.Add(ID)
            End If
        Next
        dtb.AcceptChanges()

        If dtb.Rows.Count > 0 Then
            Dim bCheckHasfile = rep.CheckHasFileFileContract(lstID)
            If bCheckHasfile = 1 Then
                ShowMessage(Translate("Duyệt khi tất cả các record đã có tập tin đính kèm,bạn kiểm tra lại"), NotifyType.Warning)
                Exit Sub
            End If
            If psp.BatchApprovedListContract(dtb) = dtb.Rows.Count Then

                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                rgContract.Rebind()

            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
            End If
        Else
            ShowMessage("Các phụ lục hợp đồng được chọn đã được phê duyệt", NotifyType.Information)
        End If
        'rep.Dispose()
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cho button ctrUpload
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Import_Data()
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    Private Sub Import_Data()
        Try
            Dim rep As New ProfileRepository
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload1.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim ds As New DataSet
            If countFile > 0 Then
                Dim file As UploadedFile = ctrlUpload1.UploadedFiles(countFile - 1)
                fileName = System.IO.Path.Combine(savepath, file.FileName)
                file.SaveAs(fileName, True)
                Using ep As New ExcelPackage
                    ds = ep.ReadExcelToDataSet(fileName, False)
                End Using
            End If
            If ds Is Nothing Then
                Exit Sub
            End If
            TableMapping(ds.Tables(0))

            If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then
                'Dim count As Integer = ds.Tables(0).Columns.Count - 6
                'For i = 0 To count
                '    If ds.Tables(0).Columns(i).ColumnName.Contains("Column") Then
                '        ds.Tables(0).Columns.RemoveAt(i)
                '        i = i - 1
                '    End If
                'Next

                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    If rep.INPORT_PLHD(DocXml) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgContract.Rebind()
                    Else
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Warning)
                    End If
                End If
            Else
                Session("EXPORTREPORT") = dtLogs
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('HU_ANNUALLEAVE_PLANS_ERROR')", True)
                ShowMessage(Translate("Có lỗi trong quá trình import. Lưu file lỗi chi tiết"), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        Dim rep As New ProfileBusinessClient
        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
        dtTemp.Columns(0).ColumnName = "EMPLOYEE_CODE"
        dtTemp.Columns(6).ColumnName = "ID_CONTRACT"
        dtTemp.Columns(7).ColumnName = "STT"
        dtTemp.Columns(8).ColumnName = "CONTRACT_NO"
        dtTemp.Columns(9).ColumnName = "CONTENT"
        dtTemp.Columns(11).ColumnName = "SALARY"
        dtTemp.Columns(13).ColumnName = "START_DATE"
        dtTemp.Columns(14).ColumnName = "EXPIRE_DATE"
        dtTemp.Columns(15).ColumnName = "SIGN_DATE"
        dtTemp.Columns(16).ColumnName = "SIGN_ID"
        dtTemp.Columns(17).ColumnName = "SIGN_ID2"
        dtTemp.Columns(19).ColumnName = "STATUS_ID"
        dtTemp.Columns(20).ColumnName = "REMARK"
        'XOA DONG TIEU DE VA HEADER
        dtTemp.Rows(0).Delete()
        dtTemp.Rows(1).Delete()
        ' add Log
        Dim _error As Boolean = True
        Dim count As Integer
        Dim newRow As DataRow
        If dtLogs Is Nothing Then
            dtLogs = New DataTable("data")
            dtLogs.Columns.Add("ID", GetType(Integer))
            dtLogs.Columns.Add("EMPLOYEE_CODE", GetType(String))
            dtLogs.Columns.Add("DISCIPTION", GetType(String))
        End If
        dtLogs.Clear()

        'XOA NHUNG DONG DU LIEU NULL EMPLOYYE CODE
        Dim rowDel As DataRow
        For i As Integer = 0 To dtTemp.Rows.Count - 1
            If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
            rowDel = dtTemp.Rows(i)
            If rowDel("EMPLOYEE_CODE").ToString.Trim = "" Then
                dtTemp.Rows(i).Delete()
            End If
        Next

        For Each rows As DataRow In dtTemp.Rows
            If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For

            newRow = dtLogs.NewRow
            newRow("ID") = count + 1
            newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")

            'Nhân viên k có trong hệ thống
            If rep.CHECK_EMPLOYEE(rows("EMPLOYEE_CODE")) = 0 Then
                newRow("DISCIPTION") = "Mã nhân viên - Không tồn tại,"
                _error = False
            End If

            'NGUOI KY k có trong hệ thống
            If Not IsDBNull(rows("SIGN_ID")) Then
                If rep.CHECK_SIGN(rows("SIGN_ID")) = 0 Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Người ký 1 - Không tồn tại,"
                    _error = False
                End If
            End If

            'NGUOI KY k có trong hệ thống
            If Not IsDBNull(rows("SIGN_ID2")) Then
                If rep.CHECK_SIGN(rows("SIGN_ID2")) = 0 Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Người ký 2 - Không tồn tại,"
                    _error = False
                End If
            End If

            'Hop dong k có trong hệ thống
            If IsNumeric(rows("ID_CONTRACT")) Then
                If rep.CHECK_CONTRACT(rows("ID_CONTRACT")) = 0 Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Hợp đồng - Không tồn tại,"
                    _error = False
                End If
            Else
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Hợp đồng - Không đúng định dạng,"
                _error = False
            End If

            'Ho so luong k có trong hệ thống
            If IsNumeric(rows("SALARY")) Then
                If rep.CHECK_SALARY(rows("SALARY")) = 0 Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Lương ký PLHĐ - Không tồn tại,"
                    _error = False
                End If
            Else
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Lương ký PLHĐ - Không đúng định dạng,"
                _error = False
            End If

            If IsDBNull(rows("START_DATE")) OrElse rows("START_DATE") = "" OrElse CheckDate(rows("START_DATE")) = False Then
                rows("START_DATE") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày hiệu lực - Không đúng định dạng,"
                _error = False
            End If

            If IsNumeric(rows("ID_CONTRACT")) AndAlso CheckDate(rows("START_DATE")) Then
                Dim result As Date
                DateTime.TryParseExact(rows("START_DATE"), "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, result)
                If rep.CHECK_CONTRACT_EXITS(rows("ID_CONTRACT"), rows("EMPLOYEE_CODE"), result) = 1 Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "PLHĐ - Tồn tại,"
                    _error = False
                End If
            End If

            If IsDBNull(rows("CONTRACT_NO")) Then
                rows("CONTRACT_NO") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Số PLHĐ - Không đúng định dạng,"
                _error = False
            End If

            If Not IsDBNull(rows("EXPIRE_DATE")) OrElse rows("EXPIRE_DATE") <> "" Then
                If CheckDate(rows("EXPIRE_DATE")) = False Then
                    rows("EXPIRE_DATE") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày kết thúc - Không đúng định dạng,"
                    _error = False
                End If
            End If

            If Not IsDBNull(rows("SIGN_DATE")) Then
                If CheckDate(rows("SIGN_DATE")) = False Then
                    rows("SIGN_DATE") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày ký - Không đúng định dạng,"
                    _error = False
                End If
            End If

            If Not (IsNumeric(rows("STATUS_ID"))) Then
                rows("STATUS_ID") = 0
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Trạng thái - Không đúng định dạng,"
                _error = False
            End If

            If _error = False Then
                dtLogs.Rows.Add(newRow)
                count = count + 1
                _error = True
            End If
        Next
        dtTemp.AcceptChanges()
    End Sub

    Private Function CheckDate(ByVal value As String) As Boolean
        Dim dateCheck As Boolean
        Dim result As Date

        If value = "" Or value = "&nbsp;" Then
            value = ""
            Return True
        End If

        Try
            dateCheck = DateTime.TryParseExact(value, "dd/MM/yyyy", New CultureInfo("en-US"), DateTimeStyles.None, result)
            Return dateCheck
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region


End Class
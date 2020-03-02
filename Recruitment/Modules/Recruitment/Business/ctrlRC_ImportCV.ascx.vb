Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Common
Imports Telerik.Web.UI
Imports Aspose.Cells
Imports System.IO

Public Class ctrlRC_ImportCV
    Inherits Common.CommonView
    Protected WithEvents ProgramView As ViewBase
    Public Overrides Property MustAuthorize As Boolean = False
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
#Region "Property"

    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property
    Private Property lst As List(Of CandidateImportDTO)
        Get
            Return ViewState(Me.ID & "_lst")
        End Get
        Set(ByVal value As List(Of CandidateImportDTO))
            ViewState(Me.ID & "_lst") = value
        End Set
    End Property
    Private Property lstct As List(Of CandidateBeforeWTDTO)
        Get
            Return ViewState(Me.ID & "_lstct")
        End Get
        Set(ByVal value As List(Of CandidateBeforeWTDTO))
            ViewState(Me.ID & "_lstct") = value
        End Set
    End Property
    Private Property lstdt As List(Of TrainSingerDTO)
        Get
            Return ViewState(Me.ID & "_lstdt")
        End Get
        Set(ByVal value As List(Of TrainSingerDTO))
            ViewState(Me.ID & "_lstdt") = value
        End Set
    End Property
    Private Property lstfm As List(Of CandidateFamilyDTO)
        Get
            Return ViewState(Me.ID & "_lstfm")
        End Get
        Set(ByVal value As List(Of CandidateFamilyDTO))
            ViewState(Me.ID & "_lstfm") = value
        End Set
    End Property
    Private Property lstrf As List(Of CandidateReferenceDTO)
        Get
            Return ViewState(Me.ID & "_lstrf")
        End Get
        Set(ByVal value As List(Of CandidateReferenceDTO))
            ViewState(Me.ID & "_lstrf") = value
        End Set
    End Property
    Private Property dtError As DataTable
        Get
            Return ViewState(Me.ID & "_dtError")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtError") = value
        End Set
    End Property
    Private Property dtError1 As DataTable
        Get
            Return ViewState(Me.ID & "_dtError1")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtError1") = value
        End Set
    End Property
    Private Property dtError2 As DataTable
        Get
            Return ViewState(Me.ID & "_dtError2")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtError2") = value
        End Set
    End Property
    Private Property dtError3 As DataTable
        Get
            Return ViewState(Me.ID & "_dtError3")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtError3") = value
        End Set
    End Property
    Private Property lstSave As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_lstSave")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_lstSave") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
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

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.ExportTemplate, ToolbarItem.Import, ToolbarItem.Export, ToolbarItem.Save, ToolbarItem.Cancel)
            MainToolBar.Items(0).Text = Translate("Xuất file CV mẫu")
            MainToolBar.Items(1).Text = Translate("Import CV")
            MainToolBar.Items(2).Text = Translate("Xuất file CV lỗi")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New RecruitmentRepository
        Try
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count > 1 Then
                        For Each item As GridDataItem In rgData.SelectedItems
                            If item.GetDataKeyValue("S_ERROR") <> "" And item.GetDataKeyValue("IS_CMND") Is Nothing Then
                                ShowMessage(Translate("Tồn tại dữ liệu lỗi!"), NotifyType.Warning)
                                lstSave.Clear()
                                Exit Sub
                            End If
                            If item.GetDataKeyValue("IS_CMND") = 1 Then
                                ShowMessage(Translate("Tồn tại Ứng viên đã là nhân viên!"), NotifyType.Warning)
                                lstSave.Clear()
                                Exit Sub
                            ElseIf item.GetDataKeyValue("IS_CMND") = 2 Then
                                ShowMessage(Translate("Tồn tại Ứng viên là nhân viên đã nghỉ việc!"), NotifyType.Warning)
                                lstSave.Clear()
                                Exit Sub
                            ElseIf item.GetDataKeyValue("IS_CMND") = 3 Then
                                ShowMessage(Translate("Tồn tại Ứng viên đã từng ứng tuyển!"), NotifyType.Warning)
                                lstSave.Clear()
                                Exit Sub
                            End If
                            lstSave.Add(Decimal.Parse(item.GetDataKeyValue("ID")))
                        Next
                        SAVEIMPORT()
                    End If
                    If rgData.SelectedItems.Count = 1 Then
                        Dim check As Integer = 0
                        For Each item As GridDataItem In rgData.SelectedItems
                            If item.GetDataKeyValue("S_ERROR") <> "" And item.GetDataKeyValue("IS_CMND") Is Nothing Then
                                ShowMessage(Translate("Tồn tại dữ liệu lỗi!"), NotifyType.Warning)
                                lstSave.Clear()
                                Exit Sub
                            End If
                            If item.GetDataKeyValue("IS_CMND") = 1 Then
                                ShowMessage(Translate("Ứng viên đã là nhân viên!"), NotifyType.Warning)
                                lstSave.Clear()
                                Exit Sub
                            ElseIf item.GetDataKeyValue("IS_CMND") = 2 Then
                                check = 2
                            ElseIf item.GetDataKeyValue("IS_CMND") = 3 Then
                                check = 3
                            End If
                            lstSave.Add(Decimal.Parse(item.GetDataKeyValue("ID")))
                        Next
                        If check = 2 Or check = 3 Then
                            ctrlMessageBox.MessageText = "Ứng viên đã tồn tại!"
                            If check = 2 Then
                                ctrlMessageBox.MessageText = Translate("Ứng viên là nhân viên đã nghỉ việc, Bạn có muốn tiếp tục?")
                            End If
                            If check = 3 Then
                                ctrlMessageBox.MessageText = Translate("Ứng viên đã từng ứng tuyển, Bạn có muốn tiếp tục?")
                            End If
                            ctrlMessageBox.ActionName = "TRUNGCMND"
                            ctrlMessageBox.DataBind()
                            ctrlMessageBox.Show()
                        Else
                            SAVEIMPORT()
                        End If
                    End If
                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('RC_CANDIDATE_IMPORT')", True)
                    'GetInformationLists()
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload.isMultiple = AsyncUpload.MultipleFileSelection.Automatic
                    ctrlUpload.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgData.SelectedItems
                        If item.GetDataKeyValue("IS_CMND") = 1 Then
                            ShowMessage(Translate("Ứng viên đã là nhân viên!"), NotifyType.Warning)
                            Exit Sub
                        ElseIf item.GetDataKeyValue("IS_CMND") = 2 Then
                            ShowMessage(Translate("Ứng viên là nhân viên đã nghỉ việc!"), NotifyType.Warning)
                            Exit Sub
                        ElseIf item.GetDataKeyValue("IS_CMND") = 3 Then
                            ShowMessage(Translate("Ứng viên đã từng ứng tuyển!"), NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    Dim dt = dtError.Clone
                    For Each item As GridDataItem In rgData.SelectedItems
                        Dim key As String = item.GetDataKeyValue("FILE_NAME").ToString
                        For Each r As DataRow In dtError.Rows
                            If r("FILE_NAME").ToString <> "" Then
                                If r("FILE_NAME").ToString = key Then
                                    dt.ImportRow(r)
                                End If
                            End If
                        Next
                    Next
                    Session("EXPORTREPORT") = dt
                    Session("DATA_E1") = dtError1
                    Session("DATA_E2") = dtError2
                    Session("DATA_E3") = dtError3
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('RC_CANDIDATE_IMPORT_ERROR')", True)
                    'End If
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub GetInformationLists()
        Dim repStore As New RecruitmentStoreProcedure
        Dim dsDB As New DataSet
        Dim dtTonGiao, dtMoiQH, dtQuocGia, dtTinh, dtHuyen, dtXa, dtChuyenNganh,
            dtChuyenMon, dtHonNhan, dtDanToc, dtLogo, dtGender, dtSchool, dtTDVH As New DataTable
        Try
            dsDB = repStore.GET_ALL_LIST(1)
            If dsDB.Tables.Count > 0 Then

                'Ton Giao
                If dsDB.Tables(0) IsNot Nothing AndAlso dsDB.Tables(0).Rows.Count > 0 Then
                    dtTonGiao = dsDB.Tables(0)
                End If

                'Moi Quan He
                If dsDB.Tables(1) IsNot Nothing AndAlso dsDB.Tables(1).Rows.Count > 0 Then
                    dtMoiQH = dsDB.Tables(1)
                End If

                'Quoc Gia
                If dsDB.Tables(2) IsNot Nothing AndAlso dsDB.Tables(2).Rows.Count > 0 Then
                    dtQuocGia = dsDB.Tables(2)
                End If

                'Tinh(Thanh pho)
                If dsDB.Tables(3) IsNot Nothing AndAlso dsDB.Tables(3).Rows.Count > 0 Then
                    dtTinh = dsDB.Tables(3)
                End If

                'Huyen(Quan)
                If dsDB.Tables(4) IsNot Nothing AndAlso dsDB.Tables(4).Rows.Count > 0 Then
                    dtHuyen = dsDB.Tables(4)
                End If

                'Xa(Phuong)
                If dsDB.Tables(5) IsNot Nothing AndAlso dsDB.Tables(5).Rows.Count > 0 Then
                    dtXa = dsDB.Tables(5)
                End If

                'Chuyen Nganh
                If dsDB.Tables(6) IsNot Nothing AndAlso dsDB.Tables(6).Rows.Count > 0 Then
                    dtChuyenNganh = dsDB.Tables(6)
                End If

                'Chuyen Mon
                If dsDB.Tables(7) IsNot Nothing AndAlso dsDB.Tables(7).Rows.Count > 0 Then
                    dtChuyenMon = dsDB.Tables(7)
                End If

                'Tinh Trang Hon Nhan
                If dsDB.Tables(8) IsNot Nothing AndAlso dsDB.Tables(8).Rows.Count > 0 Then
                    dtHonNhan = dsDB.Tables(8)
                End If

                'Dan Toc
                If dsDB.Tables(9) IsNot Nothing AndAlso dsDB.Tables(9).Rows.Count > 0 Then
                    dtDanToc = dsDB.Tables(9)
                End If
                'file logo
                If dsDB.Tables(10) IsNot Nothing AndAlso dsDB.Tables(10).Rows.Count > 0 Then
                    dtLogo = dsDB.Tables(10)
                End If
                'gioi tinh
                If dsDB.Tables(11) IsNot Nothing AndAlso dsDB.Tables(11).Rows.Count > 0 Then
                    dtGender = dsDB.Tables(11)
                End If
                'truong hoc 
                If dsDB.Tables(12) IsNot Nothing AndAlso dsDB.Tables(12).Rows.Count > 0 Then
                    dtSchool = dsDB.Tables(12)
                End If
                'trinh do van hoa
                If dsDB.Tables(13) IsNot Nothing AndAlso dsDB.Tables(13).Rows.Count > 0 Then
                    dtTDVH = dsDB.Tables(13)
                End If

                ExportTemplate("Recruitment\Import\Import_ungvien_acv.xls", dtTonGiao, dtMoiQH, dtQuocGia, dtTinh, dtHuyen, dtXa, dtChuyenNganh, dtChuyenMon, dtHonNhan, dtDanToc, dtLogo, dtGender, dtSchool, dtTDVH, "Import_UngVien")
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dt1 As DataTable,
                                                    ByVal dt2 As DataTable,
                                                    ByVal dt3 As DataTable,
                                                    ByVal dt4 As DataTable,
                                                    ByVal dt5 As DataTable,
                                                    ByVal dt6 As DataTable,
                                                    ByVal dt7 As DataTable,
                                                    ByVal dt8 As DataTable,
                                                    ByVal dt9 As DataTable,
                                                    ByVal dt10 As DataTable,
                                                    ByVal dt11 As DataTable,
                                                    ByVal dt12 As DataTable,
                                                    ByVal dt13 As DataTable,
                                                    ByVal dt14 As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & "\" & templatefolder & "\" & sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)

            If dt1 IsNot Nothing Then
                dt1.TableName = "TableTonGiao"
                designer.SetDataSource(dt1)
            End If

            If dt2 IsNot Nothing Then
                dt2.TableName = "TableMoiQH"
                designer.SetDataSource(dt2)
            End If

            If dt3 IsNot Nothing Then
                dt3.TableName = "TableQuocGia"
                designer.SetDataSource(dt3)
            End If

            If dt4 IsNot Nothing Then
                dt4.TableName = "TableTinh"
                designer.SetDataSource(dt4)
            End If

            If dt5 IsNot Nothing Then
                dt5.TableName = "TableHuyen"
                designer.SetDataSource(dt5)
            End If

            If dt6 IsNot Nothing Then
                dt6.TableName = "TableXa"
                designer.SetDataSource(dt6)
            End If

            If dt7 IsNot Nothing Then
                dt7.TableName = "TableChuyenNganh"
                designer.SetDataSource(dt7)
            End If

            If dt8 IsNot Nothing Then
                dt8.TableName = "TableChuyenMon"
                designer.SetDataSource(dt8)
            End If

            If dt9 IsNot Nothing Then
                dt9.TableName = "TableHonNhan"
                designer.SetDataSource(dt9)
            End If

            If dt10 IsNot Nothing Then
                dt10.TableName = "TableDanToc"
                designer.SetDataSource(dt10)
            End If
            If dt12 IsNot Nothing Then
                dt12.TableName = "TableGioiTinh"
                designer.SetDataSource(dt12)
            End If
            If dt13 IsNot Nothing Then
                dt13.TableName = "TableSchool"
                designer.SetDataSource(dt13)
            End If
            If dt14 IsNot Nothing Then
                dt14.TableName = "TableTDVH"
                designer.SetDataSource(dt14)
            End If
            'vnm hien tai k dung logo nen rem doan code nay lai
            'If dt11 IsNot Nothing Then
            '    designer.SetDataSource(dt11)
            '    Dim sourcePath = Server.MapPath("~/ReportTemplates/Profile/LocationInfo/")
            '    Dim str As String = sourcePath + dt11.Rows(0)("ATTACH_FILE_LOGO").ToString + dt11.Rows(0)("FILE_LOGO").ToString
            '    Dim worksheet As Aspose.Cells.Worksheet = designer.Workbook.Worksheets(0)
            '    Dim b As Byte() = File.ReadAllBytes(str)
            '    Dim ms As New System.IO.MemoryStream(b)
            '    Dim pictureIndex As Integer = worksheet.Pictures.Add(1, 1, ms)

            '    'Accessing the newly added picture
            '    Dim picture As Aspose.Cells.Drawing.Picture = worksheet.Pictures(pictureIndex)

            '    'Positioning the picture proportional to row height and colum width
            '    picture.Width = 400
            '    picture.Height = 120

            'End If

            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Protected Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Try
            'Dim url = e.Argument
            'Dim code = url.Split(";")(0)
            'Select Case code
            '    Case "TRANSFER"
            '        Dim str As String = "getRadWindow().close('1');"
            '        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ClosePopup", str, True)
            'End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

#End Region

#Region "Custom"
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Using rep As New RecruitmentRepository
                    'Xóa nhân viên.
                    Select Case e.ActionName
                        Case "TRUNGCMND"
                            SAVEIMPORT()
                    End Select
                End Using
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Sub SAVEIMPORT()
        Dim rep As New RecruitmentRepository
        Try
            Dim userlog = LogHelper.GetUserLog
            Dim lst_new As New List(Of CandidateImportDTO)
            Dim lstct_new As New List(Of CandidateBeforeWTDTO)
            Dim lstdt_new As New List(Of TrainSingerDTO)
            Dim lstfm_new As New List(Of CandidateFamilyDTO)
            Dim lstrf_new As New List(Of CandidateReferenceDTO)
            For Each i In lstSave
                For Each item In lst
                    If item.can.ID = i Then
                        Dim obj_new As New CandidateImportDTO
                        obj_new.can = item.can
                        obj_new.can_cv = item.can_cv
                        obj_new.can_edu = item.can_edu
                        'obj_new.can_ex = item.can_ex
                        'obj_new.can_h = item.can_h
                        obj_new.can_other = item.can_other
                        lst_new.Add(obj_new)
                    End If
                Next
                For Each item In lstct
                    If item.CANDIDATE_ID = i Then
                        Dim objct_new As New CandidateBeforeWTDTO
                        objct_new = item
                        lstct_new.Add(objct_new)
                    End If
                Next
                For Each item In lstdt
                    If item.CANDIDATE_ID = i Then
                        Dim objdt_new As New TrainSingerDTO
                        objdt_new = item
                        lstdt_new.Add(objdt_new)
                    End If
                Next
                For Each Item In lstfm
                    If Item.CANDIDATE_ID = i Then
                        Dim objfm_new As New CandidateFamilyDTO
                        objfm_new = Item
                        lstfm_new.Add(objfm_new)
                    End If
                Next
                For Each Item In lstrf
                    If Item.CANDIDATE_ID = i Then
                        Dim objrf_new As New CandidateReferenceDTO
                        objrf_new = Item
                        lstrf_new.Add(objrf_new)
                    End If
                Next
            Next
            'If rep.ImportCandidateCV(lst_new, lstct_new, lstdt_new, lstfm_new, lstrf_new, userlog) Then
            '    Dim msgt As String = "Import thành công " & lst_new.Count & " ứng viên"
            '    'If msg.Length > 0 Then
            '    '    msgt &= ". Danh sách ứng viên không được import " & msg.Substring(msg.Length - 1, 1).ToString & ". Vui lòng kiểm tra lại file import của những ứng viên này."
            '    'End If
            '    ShowMessage(Translate(msgt), NotifyType.Success)
            '    Dim lstDel As New List(Of CandidateImportDTO)
            '    For Each item In lst
            '        lstDel.Add(item)
            '    Next
            '    For Each item In lst_new
            '        For Each it In lstDel
            '            If item.can.ID = it.can.ID Then
            '                lst.Remove(it)
            '            End If
            '        Next
            '    Next
            '    rgData.Rebind()
            'Else
            '    ShowMessage(Translate("Không có ứng viên nào được import vui lòng kiểm tra lại toàn bộ file import của ứng viên."), NotifyType.Warning)
            'End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New CandidateDTO
        Dim rep As New RecruitmentRepository
        Dim bCheck As Boolean = False
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim lstData As New List(Of CandidateDTO)
            If lst IsNot Nothing AndAlso lst.Count > 0 Then
                For Each obj As CandidateImportDTO In lst
                    lstData.Add(obj.can)
                Next
            End If
            rgData.VirtualItemCount = MaximumRows
            rgData.DataSource = lstData
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub ctrlUpload_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload.OkClicked
        Dim fileName As String
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Dim dsDataPrepare As New DataSet
        Dim dsDataPreparect As New DataSet
        Dim dsDataPreparedt As New DataSet
        Dim dsDataPreparefm As New DataSet
        Dim dsDataPreparerf As New DataSet
        Dim dtFile As New DataTable
        dtFile.Columns.Add("ID", GetType(Decimal))
        dtFile.Columns.Add("FILENAME")
        Try
            Dim tempPath As String = "Excel"
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim i As Integer = 0
            For Each file As UploadedFile In ctrlUpload.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                '//Instantiate LoadOptions specified by the LoadFormat.
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(1)
                If workbook.Worksheets.GetSheetByCodeName("Data") Is Nothing Then
                    ShowMessage("File mẫu không đúng định dạng", NotifyType.Warning)
                    Exit Sub
                End If
                i = i + 1
                Dim r As DataRow
                r = dtFile.NewRow
                r("ID") = i
                r("FILENAME") = ctrlUpload.UploadedFiles.Item(i - 1).FileName
                dtFile.Rows.Add(r)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(3, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                worksheet = workbook.Worksheets(3)
                dsDataPreparedt.Tables.Add(worksheet.Cells.ExportDataTableAsString(3, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                worksheet = workbook.Worksheets(4)
                dsDataPreparect.Tables.Add(worksheet.Cells.ExportDataTableAsString(3, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                worksheet = workbook.Worksheets(2)
                dsDataPreparefm.Tables.Add(worksheet.Cells.ExportDataTableAsString(3, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            ImportData(dsDataPrepare, dsDataPreparect, dsDataPreparedt, dsDataPreparefm, dsDataPreparerf, dtFile)
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    Public Sub ImportData(ByVal dsDataPrepare As DataSet, ByVal dsDataPreparect As DataSet, ByVal dsDataPreparedt As DataSet, ByVal dsDataPreparefm As DataSet, ByVal dsDataPreparerf As DataSet, ByVal dtFile As DataTable)
        Try
            Dim dtData As New DataTable
            Dim dtDatact As New DataTable
            Dim dtDatadt As New DataTable
            Dim dtDatafm As New DataTable
            Dim dtDatarf As New DataTable
            lstSave = New List(Of Decimal)
            lst = New List(Of CandidateImportDTO)
            lstct = New List(Of CandidateBeforeWTDTO)
            lstdt = New List(Of TrainSingerDTO)
            lstfm = New List(Of CandidateFamilyDTO)
            lstrf = New List(Of CandidateReferenceDTO)
            dtError = New DataTable
            dtError1 = New DataTable
            dtError2 = New DataTable
            dtError3 = New DataTable
            If dsDataPrepare.Tables.Count = 0 Then
                ShowMessage(Translate("Vui lòng chọn file để import."), NotifyType.Warning)
                Exit Sub
            End If

            Dim msg As String = ""
            For i As Integer = 0 To dsDataPrepare.Tables.Count - 1
                dtData = dsDataPrepare.Tables(i).Clone
                dtData.Columns.Add("ERROR")
                dtData.Columns.Add("FILE_NAME")
                dtData.Columns.Add("IS_CMND")
                For Each row In dsDataPrepare.Tables(i).Rows
                    Dim isRow = ImportValidate.TrimRow(row)
                    If isRow Then
                        dtData.ImportRow(row)
                    End If
                Next
                'Dim dtError As New DataTable
                If i = 0 Then
                    dtError = dtData.Clone
                    dtError.TableName = "DATA"
                    dtError.Columns.Add("STT", GetType(String))
                End If
                Dim iRow As Integer = 5
                Dim IsError As Boolean = False
                Dim Is_Er_IDNO As Boolean = False
                Dim sError As String = ""
                Dim iFile As Integer = i + 1
                Dim sFile_Name As String = ""
                For Each rF As DataRow In dtFile.Rows
                    If rF("ID") = iFile Then
                        sFile_Name = rF("FILENAME").ToString
                        Exit For
                    End If
                Next
                For Each row In dtData.Rows
                    Dim rowError = dtError.NewRow
                    Dim isRow = ImportValidate.TrimRow(row)
                    Dim isScpExist As Boolean = False
                    If Not isRow Then
                        iRow += 1
                        Continue For
                    End If

                    Dim IsCMND As Boolean = False
                    sError = "CMND chưa nhập"
                    ImportValidate.EmptyValue("ID_NO", row, rowError, IsCMND, sError)
                    sError = "Số CMND nhập sai định dạng"
                    ImportValidate.IsValidNumber("ID_NO", row, rowError, IsCMND, sError)

                    'If rowError("ID_NO").ToString <> row("ID_NO").ToString Then
                    '    row("ERROR") = sError
                    '    row("FILE_NAME") = sFile_Name
                    'End If
                    If IsCMND = False Then
                        Dim rep As New RecruitmentRepository
                        If rowError("ID_NO").ToString = "" Then
                            Dim isValid = rep.ValidateInsertCandidate("", row("ID_NO"), "", Date.Now, "BLACK_LIST")
                            If Not isValid Then
                                rowError("ID_NO") = "Ứng viên thuộc danh sách đen"
                            End If
                        End If
                        Dim check As Integer = 0
                        'check = rep.CheckExitID_NO(row("ID_NO"), 0)
                        'If check = 1 Then
                        '    row("ERROR") = "Ứng viên đã là nhân viên"
                        '    sError = "Ứng viên đã là nhân viên"
                        '    row("FILE_NAME") = sFile_Name
                        '    Is_Er_IDNO = True
                        '    'IsError = True
                        '    row("IS_CMND") = 1
                        '    'ShowMessage(Translate("Ứng viên đã là nhân viên"), Utilities.NotifyType.Error)
                        '    'Exit Sub
                        'End If
                        If check = 2 Then
                            'ShowMessage(Translate("Ứng viên là nhân viên đã nghỉ việc"), Utilities.NotifyType.Error)
                            'Exit Sub
                            row("ERROR") = "Ứng viên là nhân viên đã nghỉ việc"
                            sError = "Ứng viên là nhân viên đã nghỉ việc"
                            row("FILE_NAME") = sFile_Name
                            Is_Er_IDNO = True
                            'IsError = True
                            row("IS_CMND") = 2
                        End If
                        If check = 3 Then
                            'ShowMessage(Translate("Ứng viên là nhân viên đã nghỉ việc"), Utilities.NotifyType.Error)
                            'Exit Sub
                            row("ERROR") = "Ứng viên đã từng ứng tuyển"
                            sError = "Ứng viên đã từng ứng tuyển"
                            row("FILE_NAME") = sFile_Name
                            Is_Er_IDNO = True
                            'IsError = True
                            row("IS_CMND") = 3
                        End If
                    End If
                    sError = "Họ tên chưa nhập"
                    ImportValidate.EmptyValue("FIRST_NAME_VN", row, rowError, IsError, sError)
                    'If rowError("FIRST_NAME_VN").ToString <> "" Then
                    '    row("ERROR") = sError
                    '    row("FILE_NAME") = sFile_Name
                    'End If
                    sError = "Tên chưa nhập"
                    ImportValidate.EmptyValue("LAST_NAME_VN", row, rowError, IsError, sError)
                    'If rowError("LAST_NAME_VN").ToString <> "" Then
                    '    If row("LAST_NAME_VN").ToString <> rowError("LAST_NAME_VN").ToString Then
                    '        row("ERROR") = sError
                    '        row("FILE_NAME") = sFile_Name
                    '    End If
                    'End If
                    rowError("FIRST_NAME_VN") = row("FIRST_NAME_VN")
                    rowError("LAST_NAME_VN") = row("LAST_NAME_VN")
                    rowError("ID_NO") = row("ID_NO")
                    sError = "Giới tính chưa nhập"
                    ImportValidate.EmptyValue("GENDER", row, rowError, IsError, sError)
                    'If rowError("GENDER").ToString <> "" Then
                    '    If rowError("GENDER").ToString <> row("GENDER").ToString Then
                    '        row("ERROR") = sError
                    '        row("FILE_NAME") = sFile_Name
                    '    End If
                    'End If
                    sError = "Giới tính nhập sai định dạng"
                    ImportValidate.IsValidNumber("GENDER", row, rowError, IsError, sError)
                    'If rowError("GENDER").ToString <> "" Then
                    '    If rowError("GENDER").ToString <> row("GENDER").ToString Then
                    '        row("ERROR") = sError
                    '        row("FILE_NAME") = sFile_Name
                    '    End If
                    'End If
                    If row("NATIVE_NAME").ToString <> "" Then
                        sError = "Mã dân tộc sai định dạng"
                        ImportValidate.IsValidNumber("NATIVE", row, rowError, IsError, sError)
                        If rowError("NATIVE").ToString <> "" Then
                            row("ERROR") = sError
                            row("FILE_NAME") = sFile_Name
                        End If
                    Else
                        row("NATIVE") = DBNull.Value
                    End If

                    'sError = ""
                    'ImportValidate.IsValidDate("BIRTH_DATE", row, rowError, IsError, sError)

                    sError = "Nơi sinh chưa nhập"
                    ImportValidate.EmptyValue("BIRTH_PROVINCE_NAME", row, rowError, IsError, sError)
                    If row("BIRTH_PROVINCE_NAME").ToString <> "" Then
                        sError = "Mã nơi sinh nhập sai định dạng"
                        ImportValidate.IsValidNumber("BIRTH_PROVINCE", row, rowError, IsError, sError)
                        'If rowError("BIRTH_PROVINCE").ToString <> row("BIRTH_PROVINCE").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("BIRTH_PROVINCE") = DBNull.Value
                    End If
                    sError = "Quốc tịch chưa nhập"
                    ImportValidate.EmptyValue("NATIONALITY_NAME", row, rowError, IsError, sError)
                    If row("NATIONALITY_NAME").ToString <> "" Then
                        sError = "Mã Quốc tịch nhập sai định dạng"
                        ImportValidate.IsValidNumber("NATIONALITY_ID", row, rowError, IsError, sError)
                        'If rowError("NATIONALITY_ID").ToString <> row("NATIONALITY_ID").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("NATIONALITY_ID") = DBNull.Value
                    End If
                    If row("RELIGION_NAME").ToString <> "" Then
                        sError = "Mã Tôn giáo sai định dạng"
                        ImportValidate.IsValidNumber("RELIGION", row, rowError, IsError, sError)
                        'If rowError("RELIGION_NAME").ToString <> row("RELIGION_NAME").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("RELIGION") = DBNull.Value
                    End If
                    sError = "Ngày cấp CMND chưa nhập"
                    ImportValidate.EmptyValue("ID_DATE", row, rowError, IsError, sError)
                    'If rowError("ID_DATE").ToString <> row("ID_DATE").ToString Then
                    '    row("ERROR") = sError
                    '    row("FILE_NAME") = sFile_Name
                    'End If
                    sError = "Ngày cấp CNMD sai định dạng"
                    ImportValidate.IsValidDate("ID_DATE", row, rowError, IsError, sError)
                    'If rowError("ID_DATE").ToString <> row("ID_DATE").ToString Then
                    '    row("ERROR") = sError
                    '    row("FILE_NAME") = sFile_Name
                    'End If
                    sError = "Nơi cấp CMND chưa nhập"
                    ImportValidate.EmptyValue("ID_PLACE_NAME", row, rowError, IsError, sError)
                    If row("ID_PLACE_NAME").ToString <> "" Then
                        sError = "Mã Nơi cấp nhập sai định dạng"
                        ImportValidate.IsValidNumber("ID_PLACE", row, rowError, IsError, sError)
                        'If rowError("ID_PLACE").ToString <> row("ID_PLACE").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("ID_PLACE") = DBNull.Value
                    End If

                    sError = "Hộ khẩu thường trú chưa nhập"
                    ImportValidate.EmptyValue("NAV_ADDRESS", row, rowError, IsError, sError)
                    'If rowError("NAV_ADDRESS").ToString <> row("NAV_ADDRESS").ToString Then
                    '    row("ERROR") = sError
                    '    row("FILE_NAME") = sFile_Name
                    'End If

                    'TUNGNT
                    'Fixed: Per Address not require
                    '-----===T===-----
                    'sError = "Địa chỉ/ số nhà chưa nhập"
                    'ImportValidate.EmptyValue("PER_ADDRESS", row, rowError, IsError, sError)
                    '-----===T===-----

                    'If rowError("PER_ADDRESS").ToString <> row("PER_ADDRESS").ToString Then
                    '    row("ERROR") = sError
                    '    row("FILE_NAME") = sFile_Name
                    'End If
                    If row("PER_NATION_NAME").ToString <> "" Then
                        sError = "Mã Quốc gia nhập sai định dạng"
                        ImportValidate.IsValidNumber("PER_NATION_ID", row, rowError, IsError, sError)
                        'If rowError("PER_NATION_ID").ToString <> row("PER_NATION_ID").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("PER_NATION_ID") = DBNull.Value
                    End If
                    If row("PER_PROVINCE_NAME").ToString <> "" Then
                        sError = "Mã Tỉnh/Thành phố nhập sai định dạng"
                        ImportValidate.IsValidNumber("PER_PROVINCE", row, rowError, IsError, sError)
                        'If rowError("PER_PROVINCE").ToString <> row("PER_PROVINCE").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("PER_PROVINCE") = DBNull.Value
                    End If
                    If row("PER_DISTRICT_NAME").ToString <> "" Then
                        sError = "Mã Quận/huyện nhập sai định dạng"
                        ImportValidate.IsValidNumber("PER_DISTRICT_ID", row, rowError, IsError, sError)
                        'If rowError("PER_DISTRICT_ID").ToString <> row("PER_DISTRICT_ID").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("PER_DISTRICT_ID") = DBNull.Value
                    End If

                    If row("PER_WARD_NAME").ToString <> "" Then
                        sError = "Mã Quận/huyện nhập sai định dạng"
                        ImportValidate.IsValidNumber("PER_WARD_ID", row, rowError, IsError, sError)
                        'If rowError("PER_WARD_ID").ToString <> row("PER_WARD_ID").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("PER_WARD_ID") = DBNull.Value
                    End If

                    'sError = "Nguyên quán chưa nhập"
                    'ImportValidate.EmptyValue("NGUYENQUAN", row, rowError, IsError, sError)

                    sError = "Địa chỉ liên lạc chưa nhập"
                    ImportValidate.EmptyValue("CONTACT_ADDRESS", row, rowError, IsError, sError)
                    'If rowError("CONTACT_ADDRESS").ToString <> row("CONTACT_ADDRESS").ToString Then
                    '    row("ERROR") = sError
                    '    row("FILE_NAME") = sFile_Name
                    'End If

                    'sError = "Địa chỉ/số nhà chưa nhập"
                    'ImportValidate.EmptyValue("CONTACT_APARTMENT", row, rowError, IsError, sError)

                    If row("CONTACT_NATION_NAME").ToString <> "" Then
                        sError = "Mã Quốc gia nhập sai định dạng"
                        ImportValidate.IsValidNumber("CONTACT_NATION_ID", row, rowError, IsError, sError)
                        'If rowError("CONTACT_NATION_ID").ToString <> row("CONTACT_NATION_ID").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("CONTACT_NATION_ID") = DBNull.Value
                    End If
                    If row("CONTACT_PROVINCE_NAME").ToString <> "" Then
                        sError = "Mã Tỉnh/Thành phố nhập sai định dạng"
                        ImportValidate.IsValidNumber("CONTACT_PROVINCE", row, rowError, IsError, sError)
                        'If rowError("CONTACT_PROVINCE").ToString <> row("CONTACT_PROVINCE").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("CONTACT_PROVINCE") = DBNull.Value
                    End If
                    If row("CONTACT_DISTRICT_NAME").ToString <> "" Then
                        sError = "Mã Quận/huyện nhập sai định dạng"
                        ImportValidate.IsValidNumber("CONTACT_DISTRICT_ID", row, rowError, IsError, sError)
                        'If rowError("CONTACT_DISTRICT_ID").ToString <> row("CONTACT_DISTRICT_ID").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("CONTACT_DISTRICT_ID") = DBNull.Value
                    End If

                    If row("CON_WARD_NAME").ToString <> "" Then
                        sError = "Mã xã/phường nhập sai định dạng"
                        ImportValidate.IsValidNumber("CON_WARD_ID", row, rowError, IsError, sError)
                        'If rowError("CON_WARD_ID").ToString <> row("CON_WARD_ID").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("CON_WARD_ID") = DBNull.Value
                    End If

                    If row("ACADEMY_NAME").ToString <> "" Then
                        sError = "Mã Trình độ văn hóa sai định dạng"
                        ImportValidate.IsValidNumber("ACADEMY", row, rowError, IsError, sError)
                        'If rowError("ACADEMY").ToString <> row("ACADEMY").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("ACADEMY") = DBNull.Value
                    End If

                    If row("CHIEU_CAO").ToString <> "" Then
                        sError = "Chiều cao chưa nhập"
                        ImportValidate.EmptyValue("CHIEU_CAO", row, rowError, IsError, sError)
                        'If rowError("CHIEU_CAO").ToString <> row("CHIEU_CAO").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("CHIEU_CAO") = DBNull.Value
                    End If

                    If row("CAN_NANG").ToString <> "" Then
                        sError = "Cân nặng chưa nhập"
                        ImportValidate.EmptyValue("CAN_NANG", row, rowError, IsError, sError)
                        'If rowError("CAN_NANG").ToString <> row("CAN_NANG").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("CAN_NANG") = DBNull.Value
                    End If
                    sError = "Điện thoại di động chưa nhập"
                    ImportValidate.EmptyValue("CONTACT_MOBILE", row, rowError, IsError, sError)
                    'If rowError("CONTACT_MOBILE").ToString <> row("CONTACT_MOBILE").ToString Then
                    '    row("ERROR") = sError
                    '    row("FILE_NAME") = sFile_Name
                    'End If
                    If row("PER_EMAIL").ToString <> "" Then
                        sError = "Địa chỉ email chưa nhập"
                        ImportValidate.EmptyValue("PER_EMAIL", row, rowError, IsError, sError)
                        'If rowError("PER_EMAIL").ToString <> row("PER_EMAIL").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("PER_EMAIL") = DBNull.Value
                    End If

                    If row("EXPECT_1").ToString <> "" Then
                        sError = "Nguyện vọng 1 chưa nhập"
                        ImportValidate.EmptyValue("EXPECT_1", row, rowError, IsError, sError)
                        'If rowError("EXPECT_1").ToString <> row("EXPECT_1").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("EXPECT_1") = DBNull.Value
                    End If

                    If row("EXPECT_2").ToString <> "" Then
                        sError = "Nguyện vọng 2 chưa nhập"
                        ImportValidate.EmptyValue("EXPECT_2", row, rowError, IsError, sError)
                        'If rowError("EXPECT_2").ToString <> row("EXPECT_2").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("EXPECT_2") = DBNull.Value
                    End If

                    If row("NOILAMVIEC").ToString <> "" Then
                        sError = "Nơi làm việc mong muốn chưa nhập"
                        ImportValidate.EmptyValue("NOILAMVIEC", row, rowError, IsError, sError)
                        'If rowError("NOILAMVIEC").ToString <> row("NOILAMVIEC").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("NOILAMVIEC") = DBNull.Value
                    End If

                    If row("PROBATIONARY_SALARY").ToString <> "" Then
                        sError = "Mức lương mong muốn chưa nhập"
                        ImportValidate.EmptyValue("PROBATIONARY_SALARY", row, rowError, IsError, sError)
                        'If rowError("PROBATIONARY_SALARY").ToString <> row("PROBATIONARY_SALARY").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("PROBATIONARY_SALARY") = DBNull.Value
                    End If

                    If row("DATE_START").ToString <> "" Then
                        sError = "Ngày bắt đầu mong muốn chưa nhập"
                        ImportValidate.EmptyValue("DATE_START", row, rowError, IsError, sError)
                        'If rowError("DATE_START").ToString <> row("DATE_START").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("DATE_START") = DBNull.Value
                    End If

                    If row("LEARNING_LEVEL_NAME").ToString <> "" Then
                        sError = "Mã Trình độ văn hóa sai định dạng"
                        ImportValidate.IsValidNumber("LEARNING_LEVEL", row, rowError, IsError, sError)
                        'If rowError("LEARNING_LEVEL").ToString <> row("LEARNING_LEVEL").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("LEARNING_LEVEL") = DBNull.Value
                    End If
                    'If row("FIELD_ID").ToString <> "" Then
                    '    sError = "Mã Trình độ chuyên môn sai định dạng"
                    '    ImportValidate.IsValidNumber("FIELD", row, rowError, IsError, sError)
                    'Else
                    '    row("FIELD") = DBNull.Value
                    'End If
                    If row("SCHOOL_NAME").ToString <> "" Then
                        sError = "Mã Trường học sai định dạng"
                        ImportValidate.IsValidNumber("SCHOOL", row, rowError, IsError, sError)
                        'If rowError("SCHOOL").ToString <> row("SCHOOL").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                        ImportValidate.EmptyValue("SCHOOL_NAME", row, rowError, IsError, sError)
                    Else
                        row("SCHOOL") = DBNull.Value
                        row("SCHOOL_NAME") = DBNull.Value
                    End If
                    If row("MAJOR_NAME").ToString <> "" Then
                        sError = "Mã Chuyên ngành sai định dạng"
                        ImportValidate.IsValidNumber("MAJOR", row, rowError, IsError, sError)
                        'If rowError("MAJOR").ToString <> row("MAJOR").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("MAJOR") = DBNull.Value
                    End If
                    'If row("DEGREE_NAME").ToString <> "" Then
                    '    sError = "Mã Bằng cấp sai định dạng"
                    '    ImportValidate.IsValidNumber("DEGREE", row, rowError, IsError, sError)
                    'Else
                    '    row("DEGREE") = DBNull.Value
                    'End If
                    'sError = "Chứng chỉ tin học chưa nhập"
                    'ImportValidate.EmptyValue("IT_CERTIFICATE", row, rowError, IsError, sError)
                    'sError = "Điểm tin học chưa nhập"
                    'ImportValidate.EmptyValue("IT_MARK", row, rowError, IsError, sError)

                    If row("IT_LEVEL_NAME").ToString <> "" Then
                        sError = "Mã Trình độ tin học sai định dạng"
                        ImportValidate.IsValidNumber("IT_LEVEL_ID", row, rowError, IsError, sError)
                        'If rowError("IT_LEVEL_NAME").ToString <> row("IT_LEVEL_NAME").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("IT_LEVEL_ID") = DBNull.Value
                    End If

                    If row("ENGLISH_LEVEL_NAME").ToString <> "" Then
                        sError = "Mã Trình độ ngoại ngữ sai định dạng"
                        ImportValidate.IsValidNumber("ENGLISH_LEVEL", row, rowError, IsError, sError)
                        'If rowError("ENGLISH_LEVEL_NAME").ToString <> row("ENGLISH_LEVEL_NAME").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("ENGLISH_LEVEL") = DBNull.Value
                    End If
                    If row("ENGLISH_NAME").ToString <> "" Then
                        sError = "Mã ngoại ngữ sai định dạng"
                        ImportValidate.IsValidNumber("ENGLISH_ID", row, rowError, IsError, sError)
                        'If rowError("ENGLISH_NAME").ToString <> row("ENGLISH_NAME").ToString Then
                        '    row("ERROR") = sError
                        '    row("FILE_NAME") = sFile_Name
                        'End If
                    Else
                        row("ENGLISH_ID") = DBNull.Value
                    End If
                    'sError = "Điểm ngoại ngữ chưa nhập"
                    'ImportValidate.EmptyValue("ENGLISH_MARK", row, rowError, IsError, sError)

                    If IsError Then
                        'row("ERROR") = sError
                        row("ERROR") = "Lỗi dữ liệu nhập"
                        row("FILE_NAME") = sFile_Name
                        rowError("FILE_NAME") = sFile_Name
                        dtError.Rows.Add(rowError)
                    Else
                        If Is_Er_IDNO = True Then
                            row("FILE_NAME") = sFile_Name
                            rowError("FILE_NAME") = sFile_Name
                            dtError.Rows.Add(rowError)
                        Else
                            row("ERROR") = ""
                            row("FILE_NAME") = sFile_Name
                            rowError("FILE_NAME") = sFile_Name
                        End If
                    End If
                Next
                'Dim dtError1 As New DataTable("ERROR1")
                'Dim dtError2 As New DataTable("ERROR2")
                'Dim dtError3 As New DataTable("ERROR3")
                'Dim dtError4 As New DataTable("ERROR4")
                If dtData.Rows.Count > 0 Then
                    dtDatact = New DataTable
                    dtDatact = dsDataPreparect.Tables(i).Clone
                    For Each row In dsDataPreparect.Tables(i).Rows
                        Dim isRow = ImportValidate.TrimRow(row)
                        If isRow Then
                            dtDatact.ImportRow(row)
                        End If
                    Next
                    If i = 0 Then
                        dtError1 = dtDatact.Clone
                    End If
                    iRow = 59
                    For Each row As DataRow In dtDatact.Rows
                        Dim isRow = ImportValidate.TrimRow(row)
                        If Not isRow Then
                            iRow += 1
                            Continue For
                        End If
                        IsError = False
                        Dim money As Decimal = 0
                        ImportValidate.TrimRow(row)
                        Dim rowError = dtError1.NewRow
                        sError = "Tên công ty"
                        ImportValidate.EmptyValue("ORG_NAME", row, rowError, IsError, sError)
                        sError = "Địa chỉ công ty"
                        ImportValidate.EmptyValue("ORG_ADDRESS", row, rowError, IsError, sError)
                        sError = "Chức vụ"
                        ImportValidate.EmptyValue("TITLE_NAME", row, rowError, IsError, sError)
                        sError = "Từ ngày"
                        ImportValidate.EmptyValue("FROMDATE", row, rowError, IsError, sError)
                        If row("FROMDATE").ToString <> "" Then
                            sError = "Từ ngày"
                            ImportValidate.IsValidDate("FROMDATE", row, rowError, IsError, sError)
                        End If
                        If row("TODATE").ToString <> "" Then
                            sError = "Đến ngày"
                            ImportValidate.IsValidDate("TODATE", row, rowError, IsError, sError)
                        End If
                        If row("WORK").ToString <> "" Then
                            sError = "Nhiệm vụ"
                            ImportValidate.EmptyValue("WORK", row, rowError, IsError, sError)
                        Else
                            row("WORK") = DBNull.Value
                        End If
                        If row("DIRECT_MANAGER").ToString <> "" Then
                            sError = "Quản lý trực tiếp"
                            ImportValidate.EmptyValue("DIRECT_MANAGER", row, rowError, IsError, sError)
                        Else
                            row("DIRECT_MANAGER") = DBNull.Value
                        End If

                        If row("REASON_LEAVE").ToString <> "" Then
                            sError = "Lý do nghỉ việc"
                            ImportValidate.EmptyValue("REASON_LEAVE", row, rowError, IsError, sError)
                        Else
                            row("REASON_LEAVE") = DBNull.Value
                        End If
                        If IsError Then
                            row("ERROR") = sError
                            row("FILE_NAME") = sFile_Name
                            dtError1.Rows.Add(rowError)
                        End If
                        iRow += 1
                    Next
                    dtDatadt = New DataTable
                    dtDatadt = dsDataPreparedt.Tables(i).Clone
                    For Each row In dsDataPreparedt.Tables(i).Rows
                        Dim isRow = ImportValidate.TrimRow(row)
                        If isRow Then
                            dtDatadt.ImportRow(row)
                        End If
                    Next
                    If i = 0 Then
                        dtError2 = dtDatadt.Clone
                    End If
                    iRow = 29
                    For Each row As DataRow In dtDatadt.Rows
                        Dim isRow = ImportValidate.TrimRow(row)
                        If Not isRow Then
                            iRow += 1
                            Continue For
                        End If
                        IsError = False
                        Dim money As Decimal = 0
                        ImportValidate.TrimRow(row)
                        Dim rowError = dtError2.NewRow
                        sError = "Từ ngày"
                        ImportValidate.EmptyValue("FROMDATE", row, rowError, IsError, sError)
                        If row("FROMDATE").ToString <> "" Then
                            sError = "Từ ngày"
                            ImportValidate.IsValidDate("FROMDATE", row, rowError, IsError, sError)
                        End If
                        If row("TODATE").ToString <> "" Then
                            sError = "Đến ngày"
                            ImportValidate.IsValidDate("TODATE", row, rowError, IsError, sError)
                        End If

                        If row("SCHOOL_NAME").ToString <> "" Then
                            sError = "Mã trường học sai định dạng"
                            ImportValidate.EmptyValue("SCHOOL_NAME", row, rowError, IsError, sError)
                            ImportValidate.IsValidNumber("SCHOOL_ID", row, rowError, IsError, sError)
                        Else
                            row("SCHOOL_ID") = DBNull.Value
                            row("SCHOOL_NAME") = DBNull.Value
                        End If
                        sError = "Chuyên ngành"
                        If row("MAJOR_NAME").ToString <> "" Then
                            sError = "Mã chuyên ngành sai định dạng"
                            ImportValidate.IsValidNumber("MAJOR_ID", row, rowError, IsError, sError)
                            ImportValidate.EmptyValue("MAJOR_NAME", row, rowError, IsError, sError)
                        Else
                            row("MAJOR_ID") = DBNull.Value
                            row("MAJOR_NAME") = DBNull.Value
                        End If

                        sError = "Xếp loại"
                        If row("MARK_EDU_NAME").ToString <> "" Then
                            sError = "Mã xếp loại sai định dạng"
                            ImportValidate.IsValidNumber("MARK_EDU_ID", row, rowError, IsError, sError)
                        Else
                            row("MARK_EDU_ID") = DBNull.Value
                        End If

                        If IsError Then
                            row("ERROR") = sError
                            row("FILE_NAME") = sFile_Name
                            dtError2.Rows.Add(rowError)
                        End If
                        iRow += 1
                    Next

                    ' Nhan than
                    dtDatafm = New DataTable
                    dtDatafm = dsDataPreparefm.Tables(i).Clone
                    For Each row In dsDataPreparefm.Tables(i).Rows
                        Dim isRow = ImportValidate.TrimRow(row)
                        If isRow Then
                            dtDatafm.ImportRow(row)
                        End If
                    Next
                    If i = 0 Then
                        dtError3 = dtDatafm.Clone
                    End If
                    iRow = 78
                    For Each row As DataRow In dtDatafm.Rows
                        Dim isRow = ImportValidate.TrimRow(row)
                        If Not isRow Then
                            iRow += 1
                            Continue For
                        End If
                        IsError = False
                        Dim money As Decimal = 0
                        ImportValidate.TrimRow(row)
                        Dim rowError = dtError3.NewRow
                        sError = "Họ và tên"
                        ImportValidate.EmptyValue("FULLNAME", row, rowError, IsError, sError)

                        If row("BIRTH_YEAR").ToString <> "" Then
                            sError = "Năm sinh"
                            ImportValidate.IsValidNumber("BIRTH_YEAR", row, rowError, IsError, sError)
                        End If
                        If row("RELATION_NAME").ToString <> "" Then
                            sError = "Quan hệ chọn bị sai"
                            ImportValidate.IsValidNumber("RELATION_ID", row, rowError, IsError, sError)
                            If IsError Then
                                rowError("RELATION_NAME") = sError
                            End If
                        Else
                            row("RELATION_ID") = DBNull.Value
                        End If
                        sError = "Nghề nghiệp"
                        ImportValidate.EmptyValue("JOB", row, rowError, IsError, sError)
                        sError = "Chỗ ở hiện nay"
                        ImportValidate.EmptyValue("ADDRESS", row, rowError, IsError, sError)

                        If IsError Then
                            row("ERROR") = sError
                            row("FILE_NAME") = sFile_Name
                            dtError3.Rows.Add(rowError)
                        End If
                        iRow += 1
                    Next
                    dtDatarf = New DataTable
                End If
                'If dtError.Rows.Count > 0 Or dtError1.Rows.Count > 0 Or dtError2.Rows.Count > 0 Or dtError3.Rows.Count > 0 Or dtError4.Rows.Count > 0 Then
                '    'For Each item As DataRow In dtError.Rows
                '    '    msg &= item("FIRST_NAME_VN").ToString & " " & item("LAST_NAME_VN").ToString & ","
                '    'Next
                '    Session("EXPORTREPORT") = dtError
                '    Session("DATA_E1") = dtError1
                '    Session("DATA_E2") = dtError2
                '    Session("DATA_E3") = dtError3
                '    Session("DATA_E4") = dtError4
                '    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('RC_CANDIDATE_IMPORT_ERROR')", True)
                '    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                'Else
                CreateCanImportCV(dtData, dtDatact, dtDatadt, dtDatafm, dtDatarf)
                'End If
            Next
            Dim userlog = LogHelper.GetUserLog

            If lst IsNot Nothing AndAlso lst.Count > 0 Then
                rgData.Rebind()
                Using rep As New RecruitmentRepository
                    'Dim checkIns As Integer = 0
                    'For Each objCan As CandidateImportDTO In lst
                    '    For Each obj As CandidateDTO In objCan.can
                    '        Dim check As Integer = 0
                    '        check = rep.CheckExitID_NO(objCan.can_cv.ID_NO, 0)
                    '        If check = 1 Then
                    '            checkIns = 1
                    '            objCan.can.S_ERROR = "Ứng viên đã là nhân viên"
                    '            'ShowMessage(Translate("Ứng viên đã là nhân viên"), Utilities.NotifyType.Error)
                    '            'Exit Sub
                    '        End If
                    '        If check = 2 Then
                    '            'ShowMessage(Translate("Ứng viên là nhân viên đã nghỉ việc"), Utilities.NotifyType.Error)
                    '            'Exit Sub
                    '            objCan.can.S_ERROR = "Ứng viên là nhân viên đã nghỉ việc"
                    '            checkIns = 2
                    '        End If
                    '        If check = 3 Then
                    '            'ShowMessage(Translate("Ứng viên là nhân viên đã nghỉ việc"), Utilities.NotifyType.Error)
                    '            'Exit Sub
                    '            objCan.can.S_ERROR = "Ứng viên là nhân viên đã nghỉ việc"
                    '            checkIns = 3
                    '        End If
                    '    Next

                    'Next
                    'If checkIns = 0 Then
                    '    If rep.ImportCandidateCV(lst, lstct, lstdt, lstfm, lstrf, userlog) Then
                    '        Dim msgt As String = "Import thành công " & lst.Count & " ứng viên"
                    '        If msg.Length > 0 Then
                    '            msgt &= ". Danh sách ứng viên không được import " & msg.Substring(msg.Length - 1, 1).ToString & ". Vui lòng kiểm tra lại file import của những ứng viên này."
                    '        End If
                    '        ShowMessage(Translate(msgt), NotifyType.Success)
                    '        rgData.Rebind()
                    '    Else
                    '        ShowMessage(Translate("Không có ứng viên nào được import vui lòng kiểm tra lại toàn bộ file import của ứng viên."), NotifyType.Warning)
                    '    End If
                    'Else
                    '    ctrlMessageBox.MessageText = "Ứng viên đã tồn tại!"
                    '    If checkIns = 2 Then
                    '        ctrlMessageBox.MessageText = Translate("Ứng viên là nhân viên đã nghỉ việc, Bạn có muốn tiếp tục?")
                    '    End If
                    '    If checkIns = 3 Then
                    '        ctrlMessageBox.MessageText = Translate("Ứng viên đã từng ứng tuyển, Bạn có muốn tiếp tục?")
                    '    End If
                    '    ctrlMessageBox.ActionName = "TRUNGCMND"
                    '    ctrlMessageBox.DataBind()
                    '    ctrlMessageBox.Show()
                    'End If
                End Using
            Else
                ShowMessage(Translate("Không có ứng viên nào được import vui lòng kiểm tra lại toàn bộ file import của ứng viên."), NotifyType.Warning)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CreateCanImportCV(ByVal dtData As DataTable, ByVal dtDatact As DataTable, ByVal dtDatadt As DataTable, ByVal dtDatafm As DataTable, ByVal dtDatarf As DataTable)
        Dim candidateid As Decimal = lst.Count + 1
        Try
            For Each dr In dtData.Rows
                Dim can_cv As CandidateCVDTO
                Dim can_other As CandidateOtherInfoDTO
                Dim can_edu As New CandidateEduDTO
                Dim can As New CandidateDTO
                Dim can_ex As New CandidateExpectDTO
                Dim can_health As New CandidateHealthDTO
                Dim canimport As New CandidateImportDTO
                'Candidate
                If hidProgramID.Value <> "" Then
                    can.RC_PROGRAM_ID = Decimal.Parse(hidProgramID.Value)
                End If
                can.FIRST_NAME_VN = dr("FIRST_NAME_VN").ToString
                can.LAST_NAME_VN = dr("LAST_NAME_VN").ToString
                can.FILE_NAME = dr("FILE_NAME").ToString
                'can.S_ERROR = dr("ERROR").ToString
                can.ID_NO = dr("ID_NO").ToString
                can.BIRTH_DATE = ToDate(dr("BIRTH_DATE").ToString)
                'If dr("IS_CMND") <> "" Then
                '    can.IS_CMND = Decimal.Parse(dr("IS_CMND"))
                'End If
                can.ID = candidateid
                'Candidate CV
                can_cv = New CandidateCVDTO
                can_cv.CANDIDATE_ID = candidateid
                can_cv.GENDER = dr("GENDER").ToString
                can_cv.NATIVE = dr("NATIVE").ToString
                can_cv.RELIGION = dr("RELIGION").ToString
                can_cv.ID_NO = dr("ID_NO").ToString
                can_cv.ID_DATE = ToDate(dr("ID_DATE").ToString)
                If dr("ID_PLACE").ToString <> "" Then
                    can_cv.ID_PLACE = Decimal.Parse(dr("ID_PLACE").ToString)
                End If
                can_cv.BIRTH_DATE = ToDate(dr("BIRTH_DATE").ToString)
                If dr("BIRTH_PROVINCE").ToString <> "" Then
                    can_cv.BIRTH_PROVINCE = Decimal.Parse(dr("BIRTH_PROVINCE")) ' NƠI SINH
                End If

                If dr("NATIONALITY_ID").ToString <> "" Then
                    can_cv.NATIONALITY_ID = Decimal.Parse(dr("NATIONALITY_ID")) ' QUỐC TỊCH
                End If
                can_cv.PER_EMAIL = dr("PER_EMAIL").ToString ' EMAIL
                can_cv.CONTACT_MOBILE = dr("CONTACT_MOBILE").ToString ' ĐIỆN THOẠI
                can_cv.NAV_ADDRESS = dr("NAV_ADDRESS").ToString ' HỘ KHẨU THƯỜNG TRÚ
                can_cv.PER_ADDRESS = dr("PER_ADDRESS").ToString ' ĐỊA CHỈ THƯỜNG TRÚ
                If dr("PER_DISTRICT_ID").ToString <> "" Then
                    can_cv.PER_DISTRICT_ID = Decimal.Parse(dr("PER_DISTRICT_ID"))  ' QUẬN HUYỆN THƯỜNG TRÚ 
                End If
                If dr("PER_NATION_ID").ToString <> "" Then
                    can_cv.PER_NATION_ID = Decimal.Parse(dr("PER_NATION_ID"))    ' QUỐC GIA THƯỜNG TRÚ
                End If
                If dr("PER_PROVINCE").ToString <> "" Then
                    can_cv.PER_PROVINCE = Decimal.Parse(dr("PER_PROVINCE"))    ' TỈNH THÀNH PHỐ THƯỜNG TRÚ
                End If
                'If dr("PER_WARD_ID").ToString <> "" Then
                '    can_cv.PER_WARD_ID = Decimal.Parse(dr("PER_WARD_ID"))   ' XÃ PHƯỜNG THƯỜNG TRÚ
                'End If


                'can_cv.NGUYENQUAN = dr("NGUYENQUAN").ToString   ' NGUYÊN QUÁN
                can_cv.CONTACT_ADDRESS = dr("CONTACT_ADDRESS").ToString  ' ĐỊA CHỈ LIÊN LẠC
                'can_cv.CONTACT_APARTMENT = dr("CONTACT_APARTMENT").ToString   ' ĐỊA CHỈ SỐ NHÀ

                If dr("CONTACT_NATION_ID").ToString <> "" Then
                    can_cv.CONTACT_NATION_ID = Decimal.Parse(dr("CONTACT_NATION_ID"))   ' QUỐC GIA LIÊN LẠC
                End If


                If dr("CONTACT_PROVINCE").ToString <> "" Then
                    can_cv.CONTACT_PROVINCE = Decimal.Parse(dr("CONTACT_PROVINCE"))   'TỈNH THÀNH PHỐ LIÊN LẠC
                End If

                If dr("CONTACT_DISTRICT_ID").ToString <> "" Then
                    can_cv.CONTACT_DISTRICT_ID = Decimal.Parse(dr("CONTACT_DISTRICT_ID"))   'QUẬN /HUYỆN LIÊN LẠC
                End If


                'If dr("CON_WARD_ID").ToString <> "" Then
                '    can_cv.CON_WARD_ID = Decimal.Parse(dr("CON_WARD_ID"))   'XÃ PHƯỜNG LIÊN LẠC
                'End If

                If Val(dr("FAMILY_STATUS").ToString) <> Nothing Or Val(dr("FAMILY_STATUS").ToString) <> 0 Then
                    can_cv.MARITAL_STATUS = Decimal.Parse(dr("FAMILY_STATUS").ToString)
                End If

                'CanEducation
                can_edu.CANDIDATE_ID = candidateid
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
                If dr("FIELD").ToString <> "" Then
                    can_edu.FIELD = dr("FIELD")
                End If
                can_edu.IT_CERTIFICATE = dr("IT_CERTIFICATE")
                If dr("IT_LEVEL_ID").ToString <> "" Then
                    can_edu.IT_LEVEL = dr("IT_LEVEL_ID")
                End If
                can_edu.IT_MARK = dr("IT_MARK").ToString

                If dr("SCHOOL_NAME").ToString <> "" Then
                    can_edu.SCHOOL_NAME = dr("SCHOOL_NAME").ToString
                    can_edu.SCHOOL = dr("SCHOOL").ToString
                End If
                If dr("ENGLISH_ID").ToString <> "" Then
                    can_edu.ENGLISH = dr("ENGLISH_ID").ToString
                End If
                If dr("ENGLISH_LEVEL").ToString <> "" Then
                    can_edu.ENGLISH_LEVEL = dr("ENGLISH_LEVEL")
                End If
                If dr("ENGLISH_MARK").ToString <> "" Then
                    can_edu.ENGLISH_MARK = dr("ENGLISH_MARK")
                End If
                'If dr("IS_INTERNATIONAL").ToString <> "" Then
                '    can_edu.IS_INTERNATIONAL = Decimal.Parse(dr("IS_INTERNATIONAL").ToString)
                'End If
                'Candidate Other
                can_other = New CandidateOtherInfoDTO
                can_other.CANDIDATE_ID = candidateid

                'If Val(dr("FAMILY_STATUS").ToString) <> Nothing Or Val(dr("FAMILY_STATUS").ToString) <> 0 Then
                '    can_other.FAMILY_STATUS = Decimal.Parse(dr("FAMILY_STATUS").ToString)
                'End If
                'If dr("STRENGTHS").ToString <> "" Then
                '    can_other.STRENGTHS = dr("STRENGTHS").ToString
                'End If
                'If dr("LIMIT").ToString <> "" Then
                '    can_other.LIMIT = dr("LIMIT").ToString
                'End If

                'If dr("INDEPENDENCE_WORK").ToString <> "" Then
                '    can_other.INDEPENDENCE_WORK = dr("INDEPENDENCE_WORK").ToString
                'End If
                'If dr("CHARACTER").ToString <> "" Then
                '    can_other.CHARACTER = dr("CHARACTER").ToString
                'End If
                'If dr("ACHIEVEMENTS").ToString <> "" Then
                '    can_other.ACHIEVEMENTS = dr("ACHIEVEMENTS").ToString
                'End If
                'If dr("DIFFICULT").ToString <> "" Then
                '    can_other.DIFFICULT = dr("DIFFICULT").ToString
                'End If
                'If dr("INFOR_SOURCES").ToString <> "" Then
                '    can_other.INFOR_SOURCES = dr("INFOR_SOURCES").ToString
                'End If
                'If dr("OTHER_SOUCES").ToString <> "" Then
                '    can_other.OTHER_SOUCES = dr("OTHER_SOUCES").ToString
                'End If

                If dr("DATE_START").ToString <> "" Then
                    can_ex.DATE_START = ToDate(dr("DATE_START").ToString)
                End If
                If dr("PROBATIONARY_SALARY").ToString <> "" Then
                    can_ex.PROBATIONARY_SALARY = Decimal.Parse(dr("PROBATIONARY_SALARY").ToString)
                End If
                'If dr("NOILAMVIEC").ToString <> "" Then
                '    can_ex.NOILAMVIEC = dr("NOILAMVIEC").ToString
                'End If
                'If dr("EXPECT_1").ToString <> "" Then
                '    can_ex.EXPECT_1 = dr("EXPECT_1").ToString
                'End If
                'If dr("EXPECT_2").ToString <> "" Then
                '    can_ex.EXPECT_2 = dr("EXPECT_2").ToString
                'End If
                can_health = New CandidateHealthDTO
                can_health.CANDIDATE_ID = candidateid
                If dr("CHIEU_CAO").ToString <> "" Then
                    can_health.CHIEU_CAO = Decimal.Parse(dr("CHIEU_CAO").ToString)
                End If

                If dr("CAN_NANG").ToString <> "" Then
                    can_health.CAN_NANG = Decimal.Parse(dr("CAN_NANG").ToString)
                End If

                If dr("LOAI_SUC_KHOE").ToString <> "" And dr("LOAI_SUC_KHOE").ToString <> "#N/A" Then
                    can_health.LOAI_SUC_KHOE = Decimal.Parse(dr("LOAI_SUC_KHOE").ToString)
                End If
                canimport.can = can
                canimport.can_cv = can_cv
                canimport.can_edu = can_edu
                canimport.can_other = can_other
                'canimport.can_ex = can_ex
                'canimport.can_h = can_health

                lst.Add(canimport)
            Next

            For Each dr In dtDatact.Rows
                Dim can_ct As New CandidateBeforeWTDTO
                can_ct.CANDIDATE_ID = candidateid
                If dr("FROMDATE").ToString <> "" Then
                    can_ct.FROMDATE = ToDate(dr("FROMDATE"))
                End If
                If dr("TODATE").ToString <> "" Then
                    can_ct.TODATE = ToDate(dr("TODATE"))
                End If
                If dr("ORG_NAME").ToString <> "" Then
                    can_ct.ORG_NAME = dr("ORG_NAME").ToString
                End If
                If dr("ORG_ADDRESS").ToString <> "" Then
                    can_ct.ORG_ADDRESS = dr("ORG_ADDRESS").ToString
                End If
                If dr("TITLE_NAME").ToString <> "" Then
                    can_ct.TITLE_NAME = dr("TITLE_NAME").ToString
                End If
                If dr("WORK").ToString <> "" Then
                    can_ct.WORK = (dr("WORK")).ToString
                End If
                If dr("REASON_LEAVE").ToString <> "" Then
                    can_ct.REASON_LEAVE = dr("REASON_LEAVE").ToString
                End If
                If dr("DIRECT_MANAGER").ToString <> "" Then
                    can_ct.DIRECT_MANAGER = dr("DIRECT_MANAGER").ToString
                End If
                lstct.Add(can_ct)
            Next

            For Each dr In dtDatadt.Rows
                Dim can_dt As New TrainSingerDTO
                can_dt.CANDIDATE_ID = candidateid
                If dr("FROMDATE").ToString <> "" Then
                    can_dt.FROMDATE = ToDate(dr("FROMDATE"))
                End If
                If dr("TODATE").ToString <> "" Then
                    can_dt.TODATE = ToDate(dr("TODATE"))
                End If

                can_dt.SCHOOL_NAME = dr("SCHOOL_NAME").ToString
                'If dr("SCHOOL_ID").ToString <> "" And dr("SCHOOL_ID").ToString <> "#N/A" Then
                '    can_dt.SCHOOL_ID = Decimal.Parse(dr("SCHOOL_ID").ToString)
                'End If
                'If dr("MAJOR_ID").ToString <> "" And dr("MAJOR_ID").ToString <> "#N/A" Then
                '    can_dt.MAJOR_ID = Decimal.Parse(dr("MAJOR_ID").ToString)
                '    can_dt.MAJOR_NAME = dr("MAJOR_NAME").ToString
                'End If
                'If dr("MARK_EDU_ID").ToString <> "" And dr("MARK_EDU_ID").ToString <> "#N/A" Then
                '    can_dt.MARK_EDU_ID = Decimal.Parse(dr("MARK_EDU_ID").ToString)
                'End If
                lstdt.Add(can_dt)
            Next

            For Each dr In dtDatafm.Rows
                Dim can_fm As New CandidateFamilyDTO
                can_fm.CANDIDATE_ID = candidateid
                If dr("FULLNAME").ToString <> "" Then
                    can_fm.FULLNAME = dr("FULLNAME").ToString
                End If
                If dr("RELATION_ID").ToString <> "" And dr("RELATION_ID").ToString <> "#N/A" Then
                    can_fm.RELATION_ID = Decimal.Parse(dr("RELATION_ID").ToString)
                End If
                If dr("JOB").ToString <> "" Then
                    can_fm.JOB = dr("JOB").ToString
                End If
                If dr("ADDRESS").ToString <> "" Then
                    can_fm.ADDRESS = dr("ADDRESS").ToString
                End If
                If dr("BIRTH_YEAR").ToString <> "" Then
                    can_fm.BIRTH_YEAR = Decimal.Parse(dr("BIRTH_YEAR").ToString)
                End If
                lstfm.Add(can_fm)
            Next


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

End Class
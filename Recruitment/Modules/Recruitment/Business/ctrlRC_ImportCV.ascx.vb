Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Common
Imports Telerik.Web.UI
Imports Aspose.Cells
Imports System.IO
Imports System.Globalization

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
    Private Property org_id As Decimal
        Get
            Return ViewState(Me.ID & "_org_id")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_org_id") = value
        End Set
    End Property
    Private Property title_id As Decimal
        Get
            Return ViewState(Me.ID & "_title_id")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_title_id") = value
        End Set
    End Property
    Private Property program_id As Decimal
        Get
            Return ViewState(Me.ID & "_program_id")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_program_id") = value
        End Set
    End Property
    Private Property org_name As String
        Get
            Return ViewState(Me.ID & "_org_name")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_org_name") = value
        End Set
    End Property
    Private Property title_name As String
        Get
            Return ViewState(Me.ID & "_title_name")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_title_name") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            If Not IsPostBack Then
                program_id = Request.Params("PROGRAMID")
                org_id = Request.Params("ORGID")
                title_id = Request.Params("TITLEID")
                Using rep As New RecruitmentBusinessClient
                    Dim obj As CandidateDTO = rep.OrgAndTitle(org_id, title_id)
                    org_name = obj.ORG_NAME
                    title_name = obj.TITLE_NAME
                End Using

            End If
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
            For Each i In lstSave
                For Each item In lst
                    If item.can.ID = i Then
                        Dim obj_new As New CandidateImportDTO
                        obj_new.can = item.can
                        obj_new.can_cv = item.can_cv
                        obj_new.can_edu = item.can_edu
                        lst_new.Add(obj_new)
                    End If
                Next
             
            Next
            If rep.ImportCandidateCV(lst_new) Then
                Dim msgt As String = "Import thành công " & lst_new.Count & " ứng viên"
                'If msg.Length > 0 Then
                '    msgt &= ". Danh sách ứng viên không được import " & msg.Substring(msg.Length - 1, 1).ToString & ". Vui lòng kiểm tra lại file import của những ứng viên này."
                'End If
                ShowMessage(Translate(msgt), NotifyType.Success)
                Dim lstDel As New List(Of CandidateImportDTO)
                For Each item In lst
                    lstDel.Add(item)
                Next
                For Each item In lst_new
                    For Each it In lstDel
                        If item.can.ID = it.can.ID Then
                            lst.Remove(it)
                        End If
                    Next
                Next
                rgData.Rebind()
            Else
                ShowMessage(Translate("Không có ứng viên nào được import vui lòng kiểm tra lại toàn bộ file import của ứng viên."), NotifyType.Warning)
            End If
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
                worksheet = workbook.Worksheets(0)
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
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            ImportData(dsDataPrepare, dtFile)
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    Public Sub ImportData(ByVal dsDataPrepare As DataSet, ByVal dtFile As DataTable)
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

            dtData = dsDataPrepare.Tables(0).Clone
            dtData.Columns.Add("ERROR")
            dtData.Columns.Add("FILE_NAME")
            dtData.Columns.Add("IS_CMND")
            For Each row In dsDataPrepare.Tables(0).Rows
                Dim isRow = ImportValidate.TrimRow(row)
                If isRow Then
                    dtData.ImportRow(row)
                End If
            Next
            dtError = dtData.Clone
            dtError.TableName = "DATA"
            dtError.Columns.Add("STT", GetType(String))
            Dim iRow As Integer = 5
            Dim IsError As Boolean = False
            Dim Is_Er_IDNO As Boolean = False
            Dim sError As String = ""
            
           
            TableMapping(dtData, dtFile)
            CreateCanImportCV(dtData)
            'End If
            Dim userlog = LogHelper.GetUserLog

            If lst IsNot Nothing AndAlso lst.Count > 0 Then
                rgData.Rebind()
                Using rep As New RecruitmentRepository

                End Using
            Else
                ShowMessage(Translate("Không có ứng viên nào được import vui lòng kiểm tra lại toàn bộ file import của ứng viên."), NotifyType.Warning)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable, ByVal dtFile As System.Data.DataTable)
        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
        dtTemp.Columns(0).ColumnName = "STT"
        dtTemp.Columns(1).ColumnName = "CODE"
        dtTemp.Columns(2).ColumnName = "FULLNAME_VN"
        dtTemp.Columns(3).ColumnName = "GENDER"
        dtTemp.Columns(4).ColumnName = "GENDER_ID"
        dtTemp.Columns(5).ColumnName = "PHONE"
        dtTemp.Columns(6).ColumnName = "EMAIL"
        dtTemp.Columns(7).ColumnName = "MAJOR"
        dtTemp.Columns(8).ColumnName = "MAJOR_ID"
        dtTemp.Columns(9).ColumnName = "SCHOOL"
        dtTemp.Columns(10).ColumnName = "SCHOOL_ID"
        dtTemp.Columns(11).ColumnName = "TDVH"
        dtTemp.Columns(12).ColumnName = "TDVH_ID"
        dtTemp.Columns(13).ColumnName = "ID_NO"
        dtTemp.Columns(14).ColumnName = "DATE_NC"
        dtTemp.Columns(15).ColumnName = "PROVINCE"
        dtTemp.Columns(16).ColumnName = "PROVINCE_ID"
        dtTemp.Columns(17).ColumnName = "RELIGION"
        dtTemp.Columns(18).ColumnName = "RELIGION_ID"
        dtTemp.Columns(19).ColumnName = "NATION"
        dtTemp.Columns(20).ColumnName = "NATION_ID"
        dtTemp.Columns(21).ColumnName = "DT"
        dtTemp.Columns(22).ColumnName = "DT_ID"
        'XOA DONG TIEU DE VA HEADER
        dtTemp.Rows(0).Delete()
        dtTemp.Rows(0).Delete()
        ' add Log
        Dim _error As Boolean = True
        Dim count As Integer
        Dim newRow As DataRow
        Dim dsEMP As DataTable
        'XOA NHUNG DONG DU LIEU NULL STT
        Dim rowDel As DataRow
        For i As Integer = 0 To dtTemp.Rows.Count - 1
            If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
            rowDel = dtTemp.Rows(i)
            If rowDel("STT").ToString.Trim = "" Then
                dtTemp.Rows(i).Delete()
            End If
        Next
        Dim sError As String = ""
        Dim iFile As Integer = 1
        Dim sFile_Name As String = ""
        For Each rF As DataRow In dtFile.Rows
            If rF("ID") = iFile Then
                sFile_Name = rF("FILENAME").ToString
                Exit For
            End If
        Next
        Dim Is_Er_IDNO As Boolean = False
        For Each rows As DataRow In dtTemp.Rows
            If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For
            newRow = dtError.NewRow
            newRow("STT") = count + 1
            Dim IsCMND As Boolean = False
            If IsCMND = False Then
                Dim rep As New RecruitmentRepository
                If rows("ID_NO").ToString = "" Then
                    Dim isValid = rep.ValidateInsertCandidate("", rows("ID_NO"), "", Date.Now, "BLACK_LIST")
                    If Not isValid Then
                        newRow("ID_NO") = "Ứng viên thuộc danh sách đen"
                    End If
                End If
                Dim check As Integer = 0
                check = rep.CheckExitID_NO(rows("ID_NO"), 0)
                If check = 1 Then
                    rows("ERROR") = "Ứng viên đã là nhân viên,"
                    sError = "Ứng viên đã là nhân viên,"
                    rows("FILE_NAME") = sFile_Name
                    Is_Er_IDNO = True
                    rows("IS_CMND") = 1
                End If
                If check = 2 Then
                    rows("ERROR") = "Ứng viên là nhân viên đã nghỉ việc,"
                    sError = "Ứng viên là nhân viên đã nghỉ việc,"
                    rows("FILE_NAME") = sFile_Name
                    Is_Er_IDNO = True
                    rows("IS_CMND") = 2
                End If
                If check = 3 Then
                    rows("ERROR") = "Ứng viên đã từng ứng tuyển,"
                    sError = "Ứng viên đã từng ứng tuyển,"
                    rows("FILE_NAME") = sFile_Name
                    Is_Er_IDNO = True
                    rows("IS_CMND") = 3
                End If
            End If
            If IsDBNull(rows("FULLNAME_VN")) Or rows("FULLNAME_VN").ToString = "" Then
                rows("ERROR") = rows("ERROR") + "Chưa nhập tên ứng viên,"
                rows("FILE_NAME") = sFile_Name
            End If
            If IsDBNull(rows("DATE_NC")) OrElse CheckDate(rows("DATE_NC")) = False Then
                rows("ERROR") = rows("ERROR") + "Ngày nhập vào không đúng định dạng"
                rows("FILE_NAME") = sFile_Name
            End If
            count += 1
        Next
        dtTemp.AcceptChanges()
    End Sub
    Private Sub CreateCanImportCV(ByVal dtData As DataTable)

        Try
            For Each dr In dtData.Rows
                Dim candidateid As Decimal = lst.Count + 1
                Dim can As New CandidateDTO
                Dim can_cv As CandidateCVDTO
                Dim can_edu As New CandidateEduDTO
                Dim canimport As New CandidateImportDTO
                
                can.ORG_ID = org_id
                can.TITLE_ID = title_id
                can.ORG_NAME = org_name
                can.RC_PROGRAM_ID = program_id
                can.TITLE_NAME = title_name
                can.FULLNAME_VN = dr("FULLNAME_VN").ToString
                can.ID_NO = dr("ID_NO").ToString
                can.FILE_NAME = dr("FILE_NAME").ToString
                can.S_ERROR = dr("ERROR").ToString
                If dr("IS_CMND").ToString <> "" Then
                    can.IS_CMND = Decimal.Parse(dr("IS_CMND"))
                End If
                can.ID = candidateid

                'Candidate CV
                can_cv = New CandidateCVDTO
                can_cv.CANDIDATE_ID = candidateid
                can_cv.GENDER = dr("GENDER_ID").ToString
                can_cv.CONTACT_MOBILE = dr("PHONE").ToString ' ĐIỆN THOẠI3.
                can_cv.PER_EMAIL = dr("EMAIL").ToString ' EMAIL


                can_cv.ID_NO = dr("ID_NO").ToString
                can_cv.ID_DATE = ToDate(dr("DATE_NC").ToString)
                If dr("NATION_ID").ToString <> "" Then
                    can_cv.NATIONALITY_ID = Decimal.Parse(dr("NATION_ID")) ' QUỐC TỊCH
                End If
                If dr("RELIGION_ID").ToString <> "" Then
                    can_cv.RELIGION = dr("RELIGION_ID").ToString
                End If
                If dr("DT_ID").ToString <> "" Then
                    can_cv.NATIVE = dr("DT_ID").ToString
                End If

                'CanEducation
                can_edu = New CandidateEduDTO
                If dr("TDVH_ID").ToString <> "" Then
                    can_edu.ACADEMY = dr("TDVH_ID")
                End If
                If dr("MAJOR_ID").ToString <> "" Then
                    can_edu.MAJOR = dr("MAJOR_ID")
                End If
                If dr("SCHOOL_ID").ToString <> "" Then
                    can_edu.SCHOOL = dr("SCHOOL_ID").ToString
                End If
                canimport.can = can
                canimport.can_cv = can_cv
                canimport.can_edu = can_edu
                lst.Add(canimport)
            Next
        Catch ex As Exception
            Throw ex
        End Try
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
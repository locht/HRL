Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Common
Imports Telerik.Web.UI
Imports Aspose.Cells
Imports System.IO
Imports System.Globalization
Imports HistaffFrameworkPublic

Public Class ctrlRC_ImportCV
    Inherits Common.CommonView
    Protected WithEvents ProgramView As ViewBase
    Public Overrides Property MustAuthorize As Boolean = False
    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    'Public Property lst As New List(Of CandidateImportDTO)
    Public Property dtip As New CandidateImportDTO
#Region "Property"
    Property checkOut As Decimal
        Get
            Return ViewState(Me.ID & "_checkOut")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_checkOut") = value
        End Set
    End Property
    Property count As Decimal
        Get
            Return ViewState(Me.ID & "_count")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_count") = value
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
    Private Property lst As List(Of CandidateImportDTO)
        Get
            Return ViewState(Me.ID & "_lst")
        End Get
        Set(ByVal value As List(Of CandidateImportDTO))
            ViewState(Me.ID & "_lst") = value
        End Set
    End Property
    'Private Property dtip As CandidateImportDTO
    '    Get
    '        Return ViewState(Me.ID & "_dtip")
    '    End Get
    '    Set(ByVal value As CandidateImportDTO)
    '        ViewState(Me.ID & "_dtip") = value
    '    End Set
    'End Property
    'Private Property lst As List(Of CandidateImportDTO)
    '    Get
    '        Return ViewState(Me.ID & "_lst")
    '    End Get
    '    Set(ByVal value As List(Of CandidateImportDTO))
    '        ViewState(Me.ID & "_lst") = value
    '    End Set
    'End Property
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
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.ExportTemplate, ToolbarItem.Import, ToolbarItem.Export, ToolbarItem.Next, ToolbarItem.Save, ToolbarItem.Cancel)
            MainToolBar.Items(0).Text = Translate("Xuất file template DS ứng viên")
            MainToolBar.Items(1).Text = Translate("Import DS ứng viên")
            'MainToolBar.Items(2).Text = Translate("Xuất file CV lỗi")
            MainToolBar.Items(2).Text = Translate("Xuất file template CV")
            CType(Me.MainToolBar.Items(3), RadToolBarButton).ImageUrl = CType(Me.MainToolBar.Items(1), RadToolBarButton).ImageUrl
            MainToolBar.Items(3).Text = Translate("Import CV")
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
                    If rgData.SelectedItems.Count >= 1 Then
                        For Each item As GridDataItem In rgData.SelectedItems
                            If item.GetDataKeyValue("S_ERROR") <> "" And item.GetDataKeyValue("FILE_NAME") <> "" Then
                                ShowMessage(Translate("Tồn tại dữ liệu lỗi!"), NotifyType.Warning)
                                lstSave.Clear()
                                Exit Sub
                            End If
                            'If item.GetDataKeyValue("IS_CMND") = 1 Then
                            '    ShowMessage(Translate("Tồn tại Ứng viên đã là nhân viên!"), NotifyType.Warning)
                            '    lstSave.Clear()
                            '    Exit Sub
                            'ElseIf item.GetDataKeyValue("IS_CMND") = 2 Then
                            '    ShowMessage(Translate("Tồn tại Ứng viên là nhân viên đã nghỉ việc!"), NotifyType.Warning)
                            '    lstSave.Clear()
                            '    Exit Sub
                            'ElseIf item.GetDataKeyValue("IS_CMND") = 3 Then
                            '    ShowMessage(Translate("Tồn tại Ứng viên đã từng ứng tuyển!"), NotifyType.Warning)
                            '    lstSave.Clear()
                            '    Exit Sub
                            'End If
                            lstSave.Add(Decimal.Parse(item.GetDataKeyValue("ID")))
                        Next
                        SAVEIMPORT()
                    End If
                    'If rgData.SelectedItems.Count = 1 Then
                    '    Dim check As Integer = 0
                    '    For Each item As GridDataItem In rgData.SelectedItems
                    '        If item.GetDataKeyValue("S_ERROR") <> "" And item.GetDataKeyValue("IS_CMND") Is Nothing Then
                    '            ShowMessage(Translate("Tồn tại dữ liệu lỗi!"), NotifyType.Warning)
                    '            lstSave.Clear()
                    '            Exit Sub
                    '        End If
                    '        If item.GetDataKeyValue("IS_CMND") = 1 Then
                    '            ShowMessage(Translate("Ứng viên đã là nhân viên!"), NotifyType.Warning)
                    '            lstSave.Clear()
                    '            Exit Sub
                    '        ElseIf item.GetDataKeyValue("IS_CMND") = 2 Then
                    '            check = 2
                    '        ElseIf item.GetDataKeyValue("IS_CMND") = 3 Then
                    '            check = 3
                    '        End If
                    '        lstSave.Add(Decimal.Parse(item.GetDataKeyValue("ID")))
                    '    Next
                    '    If check = 2 Or check = 3 Then
                    '        ctrlMessageBox.MessageText = "Ứng viên đã tồn tại!"
                    '        If check = 2 Then
                    '            ctrlMessageBox.MessageText = Translate("Ứng viên là nhân viên đã nghỉ việc, Bạn có muốn tiếp tục?")
                    '        End If
                    '        If check = 3 Then
                    '            ctrlMessageBox.MessageText = Translate("Ứng viên đã từng ứng tuyển, Bạn có muốn tiếp tục?")
                    '        End If
                    '        ctrlMessageBox.ActionName = "TRUNGCMND"
                    '        ctrlMessageBox.DataBind()
                    '        ctrlMessageBox.Show()
                    '    Else
                    '        SAVEIMPORT()
                    '    End If
                    'End If
                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('RC_CANDIDATE_IMPORT')", True)
                    'GetInformationLists()
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload.isMultiple = AsyncUpload.MultipleFileSelection.Automatic
                    ctrlUpload.Show()
                Case CommonMessage.TOOLBARITEM_NEXT
                    ctrlUpload1.isMultiple = AsyncUpload.MultipleFileSelection.Automatic
                    ctrlUpload1.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    GetInformationLists()
                    'If rgData.SelectedItems.Count = 0 Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    'If rgData.SelectedItems.Count > 1 Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    'For Each item As GridDataItem In rgData.SelectedItems
                    '    If item.GetDataKeyValue("IS_CMND") = 1 Then
                    '        ShowMessage(Translate("Ứng viên đã là nhân viên!"), NotifyType.Warning)
                    '        Exit Sub
                    '    ElseIf item.GetDataKeyValue("IS_CMND") = 2 Then
                    '        ShowMessage(Translate("Ứng viên là nhân viên đã nghỉ việc!"), NotifyType.Warning)
                    '        Exit Sub
                    '    ElseIf item.GetDataKeyValue("IS_CMND") = 3 Then
                    '        ShowMessage(Translate("Ứng viên đã từng ứng tuyển!"), NotifyType.Warning)
                    '        Exit Sub
                    '    End If
                    'Next
                    'Dim dt = dtError.Clone
                    'For Each item As GridDataItem In rgData.SelectedItems
                    '    Dim key As String = item.GetDataKeyValue("FILE_NAME").ToString
                    '    For Each r As DataRow In dtError.Rows
                    '        If r("FILE_NAME").ToString <> "" Then
                    '            If r("FILE_NAME").ToString = key Then
                    '                dt.ImportRow(r)
                    '            End If
                    '        End If
                    '    Next
                    'Next
                    'Session("EXPORTREPORT") = dt
                    'Session("DATA_E1") = dtError1
                    'Session("DATA_E2") = dtError2
                    'Session("DATA_E3") = dtError3
                    'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('RC_CANDIDATE_IMPORT_ERROR')", True)
                    ''End If
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub GetInformationLists()
        Dim repStore As New RecruitmentStoreProcedure
        Dim dsDB As New DataSet
        Dim dtTonGiao, dtMoiQH, dtQuocGia, dtTinh, dtHuyen, dtXa, dtChuyenNganh,
            dtChuyenMon, dtHonNhan, dtDanToc, dtLogo, dtGender, dtSchool, dtTDVH,
            dtNgoaiNgu, dtBangCapNgoaiNgu, dtChungChiTinHoc, dtTrinhDoTinHoc, dtXepLoaiHocVan, dtLoaiSucKhoe As New DataTable
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
                'ngoai ngu
                If dsDB.Tables(14) IsNot Nothing AndAlso dsDB.Tables(14).Rows.Count > 0 Then
                    dtNgoaiNgu = dsDB.Tables(14)
                End If
                'bang cap ngoai ngu
                If dsDB.Tables(15) IsNot Nothing AndAlso dsDB.Tables(15).Rows.Count > 0 Then
                    dtBangCapNgoaiNgu = dsDB.Tables(15)
                End If
                'chung chi tin hoc
                If dsDB.Tables(16) IsNot Nothing AndAlso dsDB.Tables(16).Rows.Count > 0 Then
                    dtChungChiTinHoc = dsDB.Tables(16)
                End If
                'trinh do tin hoc
                If dsDB.Tables(17) IsNot Nothing AndAlso dsDB.Tables(17).Rows.Count > 0 Then
                    dtTrinhDoTinHoc = dsDB.Tables(17)
                End If
                'xep loai hoc van
                If dsDB.Tables(18) IsNot Nothing AndAlso dsDB.Tables(18).Rows.Count > 0 Then
                    dtXepLoaiHocVan = dsDB.Tables(18)
                End If
                'loai suc khoe
                If dsDB.Tables(19) IsNot Nothing AndAlso dsDB.Tables(19).Rows.Count > 0 Then
                    dtLoaiSucKhoe = dsDB.Tables(19)
                End If

                ExportTemplate("Recruitment\Import\Import_CV_mau.xls",
                               dtTonGiao, dtMoiQH, dtQuocGia, dtTinh, dtHuyen, dtXa,
                               dtChuyenNganh, dtChuyenMon, dtHonNhan, dtDanToc, dtLogo, dtGender, dtSchool, dtTDVH,
                               dtNgoaiNgu, dtBangCapNgoaiNgu, dtChungChiTinHoc, dtTrinhDoTinHoc, dtXepLoaiHocVan, dtLoaiSucKhoe,
                               "Import_CV_mau")
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
                                                    ByVal dt15 As DataTable,
                                                    ByVal dt16 As DataTable,
                                                    ByVal dt17 As DataTable,
                                                    ByVal dt18 As DataTable,
                                                    ByVal dt19 As DataTable,
                                                    ByVal dt20 As DataTable,
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

            If dt15 IsNot Nothing Then
                dt15.TableName = "TableNgoaiNgu"
                designer.SetDataSource(dt15)
            End If
            If dt16 IsNot Nothing Then
                dt16.TableName = "TableBangCapNgoaiNgu"
                designer.SetDataSource(dt16)
            End If
            If dt17 IsNot Nothing Then
                dt17.TableName = "TableCCTH"
                designer.SetDataSource(dt17)
            End If
            If dt18 IsNot Nothing Then
                dt18.TableName = "TableTDTH"
                designer.SetDataSource(dt18)
            End If
            If dt19 IsNot Nothing Then
                dt19.TableName = "TableXepLoaiHocVan"
                designer.SetDataSource(dt19)
            End If
            If dt20 IsNot Nothing Then
                dt20.TableName = "TableLoaiSucKhoe"
                designer.SetDataSource(dt20)
            End If

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
            If lst IsNot Nothing Then
                If rep.ImportCandidateCV1(lst) Then
                    Dim msgt As String = "Import thành công " & count & " ứng viên"
                    ShowMessage(Translate(msgt), NotifyType.Success)
                Else
                    ShowMessage(Translate("Không có ứng viên nào được import vui lòng kiểm tra lại toàn bộ file import của ứng viên."), NotifyType.Warning)
                End If
            End If
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
                'worksheet.get
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
            If checkOut = 1 Then
                Exit Sub
            End If
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
        Try

            ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
            dtTemp.Columns(0).ColumnName = "STT"
            dtTemp.Columns(1).ColumnName = "FIRST_NAME"
            dtTemp.Columns(2).ColumnName = "LAST_NAME"
            dtTemp.Columns(3).ColumnName = "FULLNAME_VN"
            dtTemp.Columns(4).ColumnName = "BIRTH_DAY"
            dtTemp.Columns(5).ColumnName = "GENDER"
            dtTemp.Columns(6).ColumnName = "GENDER_ID"
            dtTemp.Columns(7).ColumnName = "PHONE"
            dtTemp.Columns(8).ColumnName = "EMAIL"
            dtTemp.Columns(9).ColumnName = "MAJOR"
            dtTemp.Columns(10).ColumnName = "MAJOR_ID"
            dtTemp.Columns(11).ColumnName = "SCHOOL"
            dtTemp.Columns(12).ColumnName = "SCHOOL_ID"
            dtTemp.Columns(13).ColumnName = "TDVH"
            dtTemp.Columns(14).ColumnName = "TDVH_ID"
            dtTemp.Columns(15).ColumnName = "ID_NO"
            dtTemp.Columns(16).ColumnName = "DATE_NC"
            dtTemp.Columns(17).ColumnName = "PROVINCE"
            dtTemp.Columns(18).ColumnName = "PROVINCE_ID"
            dtTemp.Columns(19).ColumnName = "RELIGION"
            dtTemp.Columns(20).ColumnName = "RELIGION_ID"
            dtTemp.Columns(21).ColumnName = "NATION"
            dtTemp.Columns(22).ColumnName = "NATION_ID"
            dtTemp.Columns(23).ColumnName = "DT"
            dtTemp.Columns(24).ColumnName = "DT_ID"
            'XOA DONG TIEU DE VA HEADER
            dtTemp.Rows(0).Delete()
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
                Else
                    checkOut = 0
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
                'Dim IsCMND As Boolean = False
                'If IsCMND = False Then
                '    Dim rep As New RecruitmentRepository
                '    If rows("ID_NO").ToString = "" Then
                '        Dim isValid = rep.ValidateInsertCandidate("", rows("ID_NO"), "", Date.Now, "BLACK_LIST")
                '        If Not isValid Then
                '            rows("ERROR") = rows("ERROR") + "Ứng viên thuộc danh sách đen,"
                '            rows("FILE_NAME") = sFile_Name
                '        End If
                '    End If
                '    Dim check As Integer = 0
                '    check = rep.CheckExitID_NO(rows("ID_NO"), 0)
                '    If check = 1 Then
                '        rows("ERROR") = "Ứng viên đã là nhân viên,"
                '        sError = "Ứng viên đã là nhân viên,"
                '        rows("FILE_NAME") = sFile_Name
                '        Is_Er_IDNO = True
                '        rows("IS_CMND") = 1
                '    End If
                '    If check = 2 Then
                '        rows("ERROR") = "Ứng viên là nhân viên đã nghỉ việc,"
                '        sError = "Ứng viên là nhân viên đã nghỉ việc,"
                '        rows("FILE_NAME") = sFile_Name
                '        Is_Er_IDNO = True
                '        rows("IS_CMND") = 2
                '    End If
                '    If check = 3 Then
                '        rows("ERROR") = "Ứng viên đã từng ứng tuyển,"
                '        sError = "Ứng viên đã từng ứng tuyển,"
                '        rows("FILE_NAME") = sFile_Name
                '        Is_Er_IDNO = True
                '        rows("IS_CMND") = 3
                '    End If
                'End If
                'nếu ngày sinh k đúng định dạng thì k check ứng viên đã làm việc hay thuộc blacklist vì sai dl đầu vào
                Dim rep As New RecruitmentRepository
                'If IsDBNull(rows("STT")) Then
                '    checkOut = 1
                '    ShowMessage(Translate("Phải nhập số thứ tự,Xin kiểm tra lại"), NotifyType.Warning)
                '    Exit Sub
                'End If
                If rows("BIRTH_DAY").ToString = "" OrElse CheckDate(rows("BIRTH_DAY")) = False Then
                    rows("ERROR") = rows("ERROR") + "Ngày sinh không đúng định dạng,"
                    rows("FILE_NAME") = sFile_Name
                Else
                    If Not rep.ValidateInsertCandidate("", rows("ID_NO"), rows("FULLNAME_VN"), rows("BIRTH_DAY"), "NO_ID") Then
                        rows("ERROR") = rows("ERROR") + "Ứng viên đang tồn tại trong một chương trình tuyển dụng khác,"
                    End If
                    If Not rep.ValidateInsertCandidate("", rows("ID_NO"), rows("FULLNAME_VN"), rows("BIRTH_DAY"), "BLACK_LIST") Then
                        rows("ERROR") = rows("ERROR") + "Ứng viên đang thuộc Blacklist,"
                    End If
                    If Not rep.ValidateInsertCandidate("", rows("ID_NO"), rows("FULLNAME_VN"), rows("BIRTH_DAY"), "WORKING") Then
                        rows("ERROR") = rows("ERROR") + "Ứng viên đang làm việc tại ACV,"
                    End If
                    If Not rep.ValidateInsertCandidate("", rows("ID_NO"), rows("FULLNAME_VN"), rows("BIRTH_DAY"), "TERMINATE") Then
                        rows("ERROR") = rows("ERROR") + "Ứng viên đã nghỉ việc ACV,"
                    End If
                End If
                If IsDBNull(rows("FIRST_NAME")) Or rows("FIRST_NAME").ToString = "" Then
                    rows("ERROR") = rows("ERROR") + "Chưa nhập Họ và tên lót,"
                    rows("FILE_NAME") = sFile_Name
                End If
                If IsDBNull(rows("LAST_NAME")) Or rows("LAST_NAME").ToString = "" Then
                    rows("ERROR") = rows("ERROR") + "Chưa nhập tên,"
                    rows("FILE_NAME") = sFile_Name
                End If
                If IsDBNull(rows("FULLNAME_VN")) Or rows("FULLNAME_VN").ToString = "" Then
                    rows("ERROR") = rows("ERROR") + "Chưa nhập tên ứng viên,"
                    rows("FILE_NAME") = sFile_Name
                End If
                If IsDBNull(rows("DATE_NC")) OrElse CheckDate(rows("DATE_NC")) = False Then
                    rows("ERROR") = rows("ERROR") + "Ngày cấp CMND không đúng định dạng,"
                    rows("FILE_NAME") = sFile_Name
                End If
                If IsDBNull(rows("GENDER")) Or rows("GENDER").ToString = "" Then
                    rows("ERROR") = rows("ERROR") + "Chưa nhập giới tính,"
                    rows("FILE_NAME") = sFile_Name
                End If
                If IsDBNull(rows("MAJOR")) Or rows("MAJOR").ToString = "" Then
                    rows("ERROR") = rows("ERROR") + "Chưa nhập chuyên môn,"
                    rows("FILE_NAME") = sFile_Name
                End If
                If IsDBNull(rows("SCHOOL")) Or rows("SCHOOL").ToString = "" Then
                    rows("ERROR") = rows("ERROR") + "Chưa nhập trường,"
                    rows("FILE_NAME") = sFile_Name
                End If
                If IsDBNull(rows("TDVH")) Or rows("TDVH").ToString = "" Then
                    rows("ERROR") = rows("ERROR") + "Chưa nhập trình độ văn hóa,"
                    rows("FILE_NAME") = sFile_Name
                End If
                If IsDBNull(rows("PROVINCE")) Or rows("PROVINCE").ToString = "" Then
                    rows("ERROR") = rows("ERROR") + "Chưa nhập nơi cấp cmnd,"
                    rows("FILE_NAME") = sFile_Name
                End If
                If IsDBNull(rows("RELIGION")) Or rows("RELIGION").ToString = "" Then
                    rows("ERROR") = rows("ERROR") + "Chưa nhập tôn giáo,"
                    rows("FILE_NAME") = sFile_Name
                End If
                If IsDBNull(rows("NATION")) Or rows("NATION").ToString = "" Then
                    rows("ERROR") = rows("ERROR") + "Chưa nhập quốc tịch,"
                    rows("FILE_NAME") = sFile_Name
                End If
                If IsDBNull(rows("DT")) Or rows("DT").ToString = "" Then
                    rows("ERROR") = rows("ERROR") + "Chưa nhập dân tộc,"
                    rows("FILE_NAME") = sFile_Name
                End If
                count += 1
            Next
            dtTemp.AcceptChanges()

        Catch ex As Exception
            checkOut = 1
            ShowMessage(Translate("Phải nhập số thứ tự,Xin kiểm tra lại"), NotifyType.Warning)
            Exit Sub
        End Try
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
                can.FIRST_NAME_VN = dr("FIRST_NAME").ToString
                can.LAST_NAME_VN = dr("LAST_NAME").ToString
                'Dim xx = dr("IS_CMND").ToString
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
                can_cv.ID_PLACE = dr("PROVINCE_ID").ToString
                can_cv.BIRTH_DATE = ToDate(dr("BIRTH_DAY").ToString)
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

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim fileName As String
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet0 As Aspose.Cells.Worksheet
        Dim worksheet As Aspose.Cells.Worksheet
        Dim worksheet1 As Aspose.Cells.Worksheet
        Dim worksheet2 As Aspose.Cells.Worksheet
        Dim worksheet3 As Aspose.Cells.Worksheet
        Dim worksheet4 As Aspose.Cells.Drawing.PictureCollection
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
            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                '//Instantiate LoadOptions specified by the LoadFormat.
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet0 = workbook.Worksheets("ACV_CV")
                'Dim a = worksheet0.Hyperlinks
                worksheet4 = worksheet0.Pictures

                worksheet = workbook.Worksheets("DATA")
                worksheet1 = workbook.Worksheets("DAOTAO")
                worksheet2 = workbook.Worksheets("KINHNGHIEMLAMVIEC")
                worksheet3 = workbook.Worksheets("NHANTHAN")
                If worksheet Is Nothing Then
                    ShowMessage("File mẫu không đúng định dạng", NotifyType.Warning)
                    Exit Sub
                End If

                'Using ep As New ExcelPackage
                '    dsDataPrepare = ep.ReadExcelToDataSet(fileName, False)
                'End Using
                i = i + 1
                Dim r As DataRow
                r = dtFile.NewRow
                r("ID") = i
                r("FILENAME") = ctrlUpload1.UploadedFiles.Item(i - 1).FileName
                dtFile.Rows.Add(r)
                'dsDataPrepare.Tables.Add(worksheet0.Cells.ExportDataTableAsString(0, 0, worksheet0.Cells.MaxRow + 1, worksheet0.Cells.MaxColumn + 1, True))
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                dsDataPrepare.Tables.Add(worksheet1.Cells.ExportDataTableAsString(0, 0, worksheet1.Cells.MaxRow + 1, worksheet1.Cells.MaxColumn + 1, True))
                dsDataPrepare.Tables.Add(worksheet2.Cells.ExportDataTableAsString(0, 0, worksheet2.Cells.MaxRow + 1, worksheet2.Cells.MaxColumn + 1, True))
                dsDataPrepare.Tables.Add(worksheet3.Cells.ExportDataTableAsString(0, 0, worksheet3.Cells.MaxRow + 1, worksheet3.Cells.MaxColumn + 1, True))
                'dsDataPrepare.Tables.Add(worksheet4.Cells.ExportDataTableAsString(0, 0, worksheet4.Cells.MaxRow + 1, worksheet4.Cells.MaxColumn + 1, True))
                'dsDataPrepare.Tables.Add(worksheet4.Item)
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
                ImportData1(dsDataPrepare, dtFile)
                count = count + 1
            Next
            'ImportData(dsDataPrepare, dtFile)
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    Public Sub ImportData1(ByVal dsDataPrepare As DataSet, ByVal dtFile As DataTable)
        Try
            Dim dtData As New DataTable
            Dim dtDataTraning As New DataTable
            Dim dtDataExp As New DataTable
            Dim dtDataFamily As New DataTable
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

            dtDataTraning = dsDataPrepare.Tables(1).Clone
            dtDataTraning.Columns.Add("ERROR")
            dtDataTraning.Columns.Add("FILE_NAME")

            dtDataExp = dsDataPrepare.Tables(2).Clone
            dtDataExp.Columns.Add("ERROR")
            dtDataExp.Columns.Add("FILE_NAME")

            dtDataFamily = dsDataPrepare.Tables(3).Clone
            dtDataFamily.Columns.Add("ERROR")
            dtDataFamily.Columns.Add("FILE_NAME")

            For Each row In dsDataPrepare.Tables(0).Rows
                Dim isRow = ImportValidate.TrimRow(row)
                If isRow Then
                    dtData.ImportRow(row)
                End If
            Next
            For Each row In dsDataPrepare.Tables(1).Rows
                Dim isRow = ImportValidate.TrimRow(row)
                If isRow Then
                    dtDataTraning.ImportRow(row)
                End If
            Next
            For Each row In dsDataPrepare.Tables(2).Rows
                Dim isRow = ImportValidate.TrimRow(row)
                If isRow Then
                    dtDataExp.ImportRow(row)
                End If
            Next
            For Each row In dsDataPrepare.Tables(3).Rows
                Dim isRow = ImportValidate.TrimRow(row)
                If isRow Then
                    dtDataFamily.ImportRow(row)
                End If
            Next
            dtError = dtData.Clone
            dtError.TableName = "DATA"
            dtError.Columns.Add("STT", GetType(String))

            dtError1 = dtDataTraning.Clone
            dtError1.TableName = "DATA"
            dtError1.Columns.Add("STT", GetType(String))

            dtError2 = dtDataExp.Clone
            dtError2.TableName = "DATA"
            dtError2.Columns.Add("STT", GetType(String))

            dtError3 = dtDataFamily.Clone
            dtError3.TableName = "DATA"
            dtError3.Columns.Add("STT", GetType(String))


            Dim iRow As Integer = 5
            Dim IsError As Boolean = False
            Dim Is_Er_IDNO As Boolean = False
            Dim sError As String = ""


            TableMapping1(dtData, dtFile)
            TableMapping2(dtDataTraning, dtFile)
            TableMapping3(dtDataExp, dtFile)
            TableMapping4(dtDataFamily, dtFile)
            If checkOut = 1 Then
                Exit Sub
            End If
            CreateCanImportCV1(dtData, dtDataTraning, dtDataExp, dtDataFamily)
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
    Private Sub TableMapping1(ByVal dtTemp As System.Data.DataTable, ByVal dtFile As System.Data.DataTable)
        Try

            ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
            dtTemp.Columns(0).ColumnName = "FIRST_NAME_VN"
            dtTemp.Columns(1).ColumnName = "LAST_NAME_VN"
            dtTemp.Columns(2).ColumnName = "BIRTH_DATE"
            dtTemp.Columns(3).ColumnName = "BIRTH_PROVINCE_NAME"
            dtTemp.Columns(4).ColumnName = "BIRTH_PROVINCE"
            dtTemp.Columns(5).ColumnName = "GENDER_NAME"
            dtTemp.Columns(6).ColumnName = "GENDER"
            dtTemp.Columns(7).ColumnName = "NATIVE_NAME"
            dtTemp.Columns(8).ColumnName = "NATIVE"
            dtTemp.Columns(9).ColumnName = "RELIGION_NAME"
            dtTemp.Columns(10).ColumnName = "RELIGION"

            dtTemp.Columns(11).ColumnName = "NATIONALITY_NAME"
            dtTemp.Columns(12).ColumnName = "NATIONALITY_ID"
            dtTemp.Columns(13).ColumnName = "ID_NO"
            dtTemp.Columns(14).ColumnName = "ID_DATE"
            dtTemp.Columns(15).ColumnName = "ID_PLACE_NAME"
            dtTemp.Columns(16).ColumnName = "ID_PLACE"
            dtTemp.Columns(17).ColumnName = "PER_ADDRESS"
            dtTemp.Columns(18).ColumnName = "PER_PROVINCE"
            dtTemp.Columns(19).ColumnName = "PER_PROVINCE_ID"
            dtTemp.Columns(20).ColumnName = "PER_DISTRICT"
            dtTemp.Columns(21).ColumnName = "PER_DISTRICT_ID"
            dtTemp.Columns(22).ColumnName = "CONTACT_ADDRESS_TEMP"
            dtTemp.Columns(23).ColumnName = "CONTACT_PROVINCE_TEMP"
            dtTemp.Columns(24).ColumnName = "CONTACT_PROVINCE_TEMP_ID"

            dtTemp.Columns(25).ColumnName = "CONTACT_DISTRICT"
            dtTemp.Columns(26).ColumnName = "CONTACT_DISTRICT_ID"
            dtTemp.Columns(27).ColumnName = "CONTACT_MOBILE"
            dtTemp.Columns(28).ColumnName = "CONTACT_PHONE"
            dtTemp.Columns(29).ColumnName = "PER_EMAIL"
            dtTemp.Columns(30).ColumnName = "MARITAL_STATUS"
            dtTemp.Columns(31).ColumnName = "MARITAL_STATUS_ID"
            dtTemp.Columns(32).ColumnName = "CONTACT_PERSON"
            dtTemp.Columns(33).ColumnName = "RELATIONS_CONTACT"
            dtTemp.Columns(34).ColumnName = "RELATIONS_CONTACT_ID"
            dtTemp.Columns(35).ColumnName = "CONTACT_PERSON_ADDRESS"
            dtTemp.Columns(36).ColumnName = "CONTACT_PERSON_PHONE"

            'dtTemp.Columns(37).ColumnName = "ACADEMY_NAME"
            'dtTemp.Columns(38).ColumnName = "ACADEMY"

            dtTemp.Columns(37).ColumnName = "ENGLISH"
            dtTemp.Columns(38).ColumnName = "ENGLISH_ID"
            dtTemp.Columns(39).ColumnName = "ENGLISH_LEVEL"
            dtTemp.Columns(40).ColumnName = "ENGLISH_LEVEL_ID"
            dtTemp.Columns(41).ColumnName = "ENGLISH_MARK"
            dtTemp.Columns(42).ColumnName = "ENGLISH1"
            dtTemp.Columns(43).ColumnName = "ENGLISH1_ID"
            dtTemp.Columns(44).ColumnName = "ENGLISH_LEVEL1"
            dtTemp.Columns(45).ColumnName = "ENGLISH_LEVEL1_ID"
            dtTemp.Columns(46).ColumnName = "ENGLISH_MARK1"
            dtTemp.Columns(47).ColumnName = "ENGLISH2"
            dtTemp.Columns(48).ColumnName = "ENGLISH2_ID"
            dtTemp.Columns(49).ColumnName = "ENGLISH_LEVEL2"
            dtTemp.Columns(50).ColumnName = "ENGLISH_LEVEL2_ID"
            dtTemp.Columns(51).ColumnName = "ENGLISH_MARK2"
            dtTemp.Columns(52).ColumnName = "IT_CERTIFICATE"
            dtTemp.Columns(53).ColumnName = "IT_CERTIFICATE_ID"
            dtTemp.Columns(54).ColumnName = "IT_LEVEL"
            dtTemp.Columns(55).ColumnName = "IT_LEVEL_ID"
            dtTemp.Columns(56).ColumnName = "IT_MARK"
            dtTemp.Columns(57).ColumnName = "IT_CERTIFICATE1"
            dtTemp.Columns(58).ColumnName = "IT_CERTIFICATE1_ID"
            dtTemp.Columns(59).ColumnName = "IT_LEVEL1"
            dtTemp.Columns(60).ColumnName = "IT_LEVEL1_ID"
            dtTemp.Columns(61).ColumnName = "IT_MARK1"
            dtTemp.Columns(62).ColumnName = "IT_CERTIFICATE2"
            dtTemp.Columns(63).ColumnName = "IT_CERTIFICATE2_ID"
            dtTemp.Columns(64).ColumnName = "IT_LEVEL2"
            dtTemp.Columns(65).ColumnName = "IT_LEVEL2_ID"
            dtTemp.Columns(66).ColumnName = "IT_MARK2"
            dtTemp.Columns(67).ColumnName = "WORK_LOCATION"
            dtTemp.Columns(68).ColumnName = "PROBATIONARY_SALARY"
            dtTemp.Columns(69).ColumnName = "DATE_START"
            dtTemp.Columns(70).ColumnName = "LOAI_SUC_KHOE"
            dtTemp.Columns(71).ColumnName = "LOAI_SUC_KHOE_ID"
            dtTemp.Columns(72).ColumnName = "CHIEU_CAO"
            dtTemp.Columns(73).ColumnName = "CAN_NANG"
            dtTemp.Columns(74).ColumnName = "FULL_NAME_VN"

            'XOA DONG TIEU DE VA HEADER
            dtTemp.Rows(0).Delete()
            dtTemp.Rows(0).Delete()
            dtTemp.Rows(0).Delete()
            ' add Log
            Dim _error As Boolean = True
            Dim count As Integer
            Dim newRow As DataRow
            Dim dsEMP As DataTable
            'XOA NHUNG DONG DU LIEU NULL STT
            'Dim rowDel As DataRow
            'For i As Integer = 0 To dtTemp.Rows.Count - 1
            '    If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
            '    rowDel = dtTemp.Rows(i)
            '    If rowDel("STT").ToString.Trim = "" Then
            '        dtTemp.Rows(i).Delete()
            '    Else
            '        checkOut = 0
            '    End If
            'Next
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

                'nếu ngày sinh k đúng định dạng thì k check ứng viên đã làm việc hay thuộc blacklist vì sai dl đầu vào
                Dim rep As New RecruitmentRepository
                'If IsDBNull(rows("STT")) Then
                '    checkOut = 1
                '    ShowMessage(Translate("Phải nhập số thứ tự,Xin kiểm tra lại"), NotifyType.Warning)
                '    Exit Sub
                'End If
                If rows("BIRTH_DATE").ToString = "" OrElse CheckDate(rows("BIRTH_DATE")) = False Then
                    rows("ERROR") = rows("ERROR") + "Ngày sinh không đúng định dạng,"
                    rows("FILE_NAME") = sFile_Name
                Else
                    If Not rep.ValidateInsertCandidate("", rows("ID_NO"), rows("FULL_NAME_VN"), rows("BIRTH_DATE"), "NO_ID") Then
                        rows("ERROR") = rows("ERROR") + "Ứng viên đang tồn tại trong một chương trình tuyển dụng khác,"
                    End If
                    If Not rep.ValidateInsertCandidate("", rows("ID_NO"), rows("FULL_NAME_VN"), rows("BIRTH_DATE"), "BLACK_LIST") Then
                        rows("ERROR") = rows("ERROR") + "Ứng viên đang thuộc Blacklist,"
                    End If
                    If Not rep.ValidateInsertCandidate("", rows("ID_NO"), rows("FULL_NAME_VN"), rows("BIRTH_DATE"), "WORKING") Then
                        rows("ERROR") = rows("ERROR") + "Ứng viên đang làm việc tại ACV,"
                    End If
                    If Not rep.ValidateInsertCandidate("", rows("ID_NO"), rows("FULL_NAME_VN"), rows("BIRTH_DATE"), "TERMINATE") Then
                        rows("ERROR") = rows("ERROR") + "Ứng viên đã nghỉ việc ACV,"
                    End If
                End If
                If rows("ID_DATE").ToString = "" OrElse CheckDate(rows("ID_DATE")) = False Then
                    rows("ERROR") = rows("ERROR") + "Ngày cấp CMND không đúng định dạng,"
                    rows("FILE_NAME") = sFile_Name
                End If

                If IsDBNull(rows("FIRST_NAME_VN")) Or rows("FIRST_NAME_VN").ToString = "" Then
                    rows("ERROR") = rows("ERROR") + "Chưa nhập Họ và tên lót,"
                    rows("FILE_NAME") = sFile_Name
                End If
                If IsDBNull(rows("LAST_NAME_VN")) Or rows("LAST_NAME_VN").ToString = "" Then
                    rows("ERROR") = rows("ERROR") + "Chưa nhập tên,"
                    rows("FILE_NAME") = sFile_Name
                End If
                'If IsDBNull(rows("FULL_NAME_VN")) Or rows("FULL_NAME_VN").ToString = "" Then
                '    rows("ERROR") = rows("ERROR") + "Chưa nhập tên ứng viên,"
                '    rows("FILE_NAME") = sFile_Name
                'End If
                If IsDBNull(rows("GENDER")) Or rows("GENDER").ToString = "" Then
                    rows("ERROR") = rows("ERROR") + "Chưa nhập giới tính,"
                    rows("FILE_NAME") = sFile_Name
                End If
                'If IsDBNull(rows("POSITION_APPLY")) Or rows("POSITION_APPLY").ToString = "" Then
                '    rows("ERROR") = rows("ERROR") + "Chưa nhập vị trí ứng tuyển,"
                '    rows("FILE_NAME") = sFile_Name
                'End If

                If IsDBNull(rows("ID_PLACE_NAME")) Or rows("ID_PLACE_NAME").ToString = "" Then
                    rows("ERROR") = rows("ERROR") + "Chưa nhập nơi cấp cmnd,"
                    rows("FILE_NAME") = sFile_Name
                End If
                count += 1
            Next
            dtTemp.AcceptChanges()

        Catch ex As Exception
            checkOut = 1
            ShowMessage(Translate("Phải nhập số thứ tự,Xin kiểm tra lại"), NotifyType.Warning)
            Exit Sub
        End Try
    End Sub
    Private Sub CreateCanImportCV1(ByVal dtData As DataTable,
                                   ByVal dtDataTraning As DataTable,
                                   ByVal dtDataExp As DataTable,
                                   ByVal dtDatafamily As DataTable)

        Try
            Dim candidateid As Decimal = lst.Count + 1
            'Dim dtip As New CandidateImportDTO
            'Dim lst As New List(Of CandidateImportDTO)
            Dim can As New CandidateDTO
            Dim can_cv As CandidateCVDTO
            Dim can_edu As New CandidateEduDTO
            Dim can_experince As New CandidateBeforeWTDTO
            Dim can_family As New CandidateFamilyDTO
            Dim can_health As New CandidateHealthDTO
            Dim can_expect As New CandidateExpectDTO
            Dim can_training As New CandidateTrainingDTO
            Dim canimport As New CandidateImportDTO


            Dim dr = dtData.Rows(0)

            'Candidate
            can.ID = candidateid
            can.ORG_ID = org_id
            can.TITLE_ID = title_id
            can.ORG_NAME = org_name
            can.RC_PROGRAM_ID = program_id
            can.TITLE_NAME = title_name
            can.FIRST_NAME_VN = dr("FIRST_NAME_VN").ToString
            can.LAST_NAME_VN = dr("LAST_NAME_VN").ToString
            can.FULLNAME_VN = dr("FIRST_NAME_VN").ToString & dr("LAST_NAME_VN").ToString



            'candidateCv
            can_cv = New CandidateCVDTO
            can_cv.CANDIDATE_ID = candidateid
            can_cv.BIRTH_DATE = ToDate(dr("BIRTH_DATE").ToString)
            can_cv.BIRTH_PROVINCE = CDec(Val(dr("BIRTH_PROVINCE")))
            can_cv.GENDER = dr("GENDER").ToString
            If dr("NATIVE").ToString <> "" Then
                can_cv.NATIVE = CDec(Val(dr("NATIVE"))) ' dan toc
            End If
            If dr("RELIGION").ToString <> "" Then
                can_cv.RELIGION = CDec(Val(dr("RELIGION"))) ' ton giao
            End If
            If dr("NATIONALITY_ID").ToString <> "" Then
                can_cv.NATIONALITY_ID = CDec(Val(dr("NATIONALITY_ID")))
            End If

            can_cv.ID_NO = dr("ID_NO").ToString
            can_cv.ID_DATE = ToDate(dr("ID_DATE").ToString)
            can_cv.ID_PLACE = dr("ID_PLACE").ToString
            can_cv.PER_ADDRESS = dr("PER_ADDRESS")
            can_cv.PER_PROVINCE = CDec(Val(dr("PER_PROVINCE_ID")))
            can_cv.PER_DISTRICT_ID = CDec(Val(dr("PER_DISTRICT_ID")))
            can_cv.CONTACT_ADDRESS_TEMP = dr("CONTACT_ADDRESS_TEMP").ToString
            can_cv.CONTACT_PROVINCE_TEMP = CDec(Val(dr("CONTACT_PROVINCE_TEMP_ID")))
            can_cv.CONTACT_DISTRICT_TEMP = CDec(Val(dr("CONTACT_DISTRICT_ID")))
            can_cv.MOBILE_PHONE = dr("CONTACT_MOBILE").ToString
            can_cv.CONTACT_PHONE = dr("CONTACT_PHONE").ToString    'edit'
            can_cv.PER_EMAIL = dr("PER_EMAIL")
            can_cv.MARITAL_STATUS = dr("MARITAL_STATUS_ID").ToString
            can_cv.URGENT_PER_NAME = dr("CONTACT_PERSON").ToString
            can_cv.URGENT_PER_RELATION = dr("RELATIONS_CONTACT_ID")
            can_cv.URGENT_ADDRESS = dr("CONTACT_PERSON_ADDRESS")
            can_cv.URGENT_PER_SDT = dr("CONTACT_PERSON_PHONE")

            'candidate_edu
            can_edu.CANDIDATE_ID = candidateid
            can_edu.ENGLISH = dr("ENGLISH_ID").ToString
            can_edu.ENGLISH_LEVEL = dr("ENGLISH_LEVEL_ID").ToString
            can_edu.ENGLISH_MARK = dr("ENGLISH_MARK").ToString

            can_edu.ENGLISH1 = dr("ENGLISH1_ID").ToString
            can_edu.ENGLISH_LEVEL2 = dr("ENGLISH_LEVEL1_ID").ToString
            can_edu.ENGLISH_MARK1 = dr("ENGLISH_MARK1").ToString

            can_edu.ENGLISH2 = dr("ENGLISH2_ID").ToString
            can_edu.ENGLISH_LEVEL2 = dr("ENGLISH_LEVEL2_ID").ToString
            can_edu.ENGLISH_MARK2 = dr("ENGLISH_MARK2").ToString

            can_edu.IT_CERTIFICATE = dr("IT_CERTIFICATE_ID").ToString
            can_edu.IT_LEVEL = dr("IT_LEVEL_ID").ToString
            can_edu.IT_MARK = dr("IT_MARK").ToString

            can_edu.IT_CERTIFICATE1 = dr("IT_CERTIFICATE1_ID").ToString
            can_edu.IT_LEVEL1 = dr("IT_LEVEL1_ID").ToString
            can_edu.IT_MARK1 = dr("IT_MARK1").ToString

            can_edu.IT_CERTIFICATE2 = dr("IT_CERTIFICATE2_ID").ToString
            can_edu.IT_LEVEL2 = dr("IT_LEVEL2_ID").ToString
            can_edu.IT_MARK2 = dr("IT_MARK2").ToString

            'candidate expect
            can_expect.CANDIDATE_ID = candidateid
            can_expect.WORK_LOCATION = dr("WORK_LOCATION").ToString
            can_expect.PROBATIONARY_SALARY = CDec(Val(dr("PROBATIONARY_SALARY")))
            can_expect.DATE_START = ToDate(dr("DATE_START"))
            'can_expect.


            'candidate health
            can_health.CANDIDATE_ID = candidateid
            can_health.LOAI_SUC_KHOE = dr("LOAI_SUC_KHOE_ID").ToString
            can_health.CHIEU_CAO = dr("CHIEU_CAO").ToString
            can_health.CAN_NANG = dr("CAN_NANG").ToString


            'If dr("DT_ID").ToString <> "" Then
            'can_cv.NATIVE = dr("DT_ID").ToString
            'End If
            dtip.can = can
            dtip.can_cv = can_cv
            dtip.can_edu = can_edu
            dtip.can_health = can_health
            dtip.can_expect = can_expect
            'canimport.can = can
            'canimport.can_cv = can_cv
            'canimport.can_edu = can_edu
            'canimport.can_health = can_health
            'canimport.can_expect = can_expect
            'lst.Add(canimport)

            'dao tao
            Dim can_training_lst As New List(Of CandidateTrainingDTO)
            For Each dr1 In dtDataTraning.Rows
                can_training.CANDIDATE_ID = candidateid
                can_training.SCHOOL_ID = CDec(Val(dr1("SCHOOL_ID")))
                can_training.MAJOR_ID = CDec(Val(dr1("MAJOR_ID")))
                can_training.FROM_DATE = ToDate(dr1("FROMDATE"))
                can_training.TO_DATE = ToDate(dr1("TODATE"))
                can_training.MARK_EDU_ID = CDec(Val(dr1("MARK_EDU_ID")))
                can_training_lst.Add(can_training)
                'canimport.can_training = can_training_lst
                'lst.Add(canimport)
            Next
            dtip.can_training = can_training_lst


            'kinh nghiem lam viec
            Dim can_experince_lst As New List(Of CandidateBeforeWTDTO)
            For Each dr2 In dtDataExp.Rows
                can_experince.CANDIDATE_ID = candidateid
                can_experince.FROMDATE = ToDate(dr2("FROMDATE"))
                can_experince.TODATE = ToDate(dr2("TODATE"))
                can_experince.ORG_NAME = dr2("ORG_NAME").ToString
                can_experince.ORG_ADDRESS = dr2("ORG_ADDRESS").ToString
                can_experince.TITLE_NAME = dr2("TITLE_NAME")
                can_experince.SALARY = CDec(Val(dr2("LAST_SALARY")))
                can_experince.WORK = dr2("WORK").ToString
                can_experince.DIRECT_MANAGER = dr2("DIRECT_MANAGER").ToString
                can_experince.DIRECT_PHONE = dr2("DIRECT_PHONE").ToString 'db chua co
                can_experince.REASON_LEAVE = dr2("REASON_LEAVE").ToString

                can_experince_lst.Add(can_experince)
                'canimport.can_exp = can_experince_lst
                'lst.Add(canimport)
            Next
            dtip.can_exp = can_experince_lst

            'nhan than
            Dim can_family_lst As New List(Of CandidateFamilyDTO)
            For Each dr3 In dtDatafamily.Rows
                can_family.CANDIDATE_ID = candidateid
                can_family.RELATION_ID = CDec(Val(dr3("RELATION_ID")))
                can_family.FULLNAME = dr3("FULLNAME").ToString
                can_family.BIRTH_YEAR = CDec(Val(dr3("BIRTH_YEAR")))
                can_family.JOB = dr3("JOB").ToString
                can_family.ADDRESS = dr3("ADDRESS").ToString
                can_family_lst.Add(can_family)
                'canimport.can_family = can_family_lst
                'lst.Add(canimport)
            Next
            dtip.can_family = can_family_lst

            lst.Add(dtip)
            'Dim candidateid As Decimal = lst.Count + 1

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TableMapping2(ByVal dtTemp As System.Data.DataTable, ByVal dtFile As System.Data.DataTable)
        Try

            ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
            dtTemp.Columns(0).ColumnName = "SCHOOL_NAME"
            dtTemp.Columns(1).ColumnName = "SCHOOL_ID"
            dtTemp.Columns(2).ColumnName = "MAJOR_NAME"
            dtTemp.Columns(3).ColumnName = "MAJOR_ID"
            dtTemp.Columns(4).ColumnName = "FROMDATE"
            dtTemp.Columns(5).ColumnName = "TODATE"
            dtTemp.Columns(6).ColumnName = "MARK_EDU_NAME"
            dtTemp.Columns(7).ColumnName = "MARK_EDU_ID"
            'XOA DONG TIEU DE VA HEADER
            dtTemp.Rows(0).Delete()
            dtTemp.Rows(0).Delete()
            dtTemp.Rows(0).Delete()
            ' add Log
            Dim _error As Boolean = True
            Dim count As Integer
            Dim newRow As DataRow
            Dim dsEMP As DataTable
            'XOA NHUNG DONG DU LIEU NULL STT
            'Dim rowDel As DataRow
            'For i As Integer = 0 To dtTemp.Rows.Count - 1
            '    If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
            '    rowDel = dtTemp.Rows(i)
            '    If rowDel("STT").ToString.Trim = "" Then
            '        dtTemp.Rows(i).Delete()
            '    Else
            '        checkOut = 0
            '    End If
            'Next
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
                Dim rep As New RecruitmentRepository

                count += 1
                If rows("FROMDATE").ToString = "" OrElse CheckDate(rows("FROMDATE")) = False Then
                    rows("ERROR") = rows("ERROR") + "Thời gian bắt đầu không đúng định dạng,"
                    rows("FILE_NAME") = sFile_Name
                End If
                If rows("TODATE").ToString = "" OrElse CheckDate(rows("TODATE")) = False Then
                    rows("ERROR") = rows("ERROR") + "Thời gian kết thúc không đúng định dạng,"
                    rows("FILE_NAME") = sFile_Name
                End If
            Next
            dtTemp.AcceptChanges()

        Catch ex As Exception
            checkOut = 1
            'ShowMessage(Translate("Phải nhập số thứ tự,Xin kiểm tra lại"), NotifyType.Warning)
            Exit Sub
        End Try
    End Sub

    Private Sub TableMapping3(ByVal dtTemp As System.Data.DataTable, ByVal dtFile As System.Data.DataTable)
        Try

            ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
            dtTemp.Columns(0).ColumnName = "FROMDATE"
            dtTemp.Columns(1).ColumnName = "TODATE"
            dtTemp.Columns(2).ColumnName = "ORG_NAME"
            dtTemp.Columns(3).ColumnName = "ORG_ADDRESS"

            dtTemp.Columns(4).ColumnName = "TITLE_NAME"
            dtTemp.Columns(5).ColumnName = "LAST_SALARY"
            dtTemp.Columns(6).ColumnName = "WORK"
            dtTemp.Columns(7).ColumnName = "DIRECT_MANAGER"
            dtTemp.Columns(8).ColumnName = "DIRECT_PHONE"
            dtTemp.Columns(9).ColumnName = "REASON_LEAVE"
            'XOA DONG TIEU DE VA HEADER
            dtTemp.Rows(0).Delete()
            dtTemp.Rows(0).Delete()
            dtTemp.Rows(0).Delete()
            ' add Log
            Dim _error As Boolean = True
            Dim count As Integer
            Dim newRow As DataRow
            Dim dsEMP As DataTable
            'XOA NHUNG DONG DU LIEU NULL STT
            'Dim rowDel As DataRow
            'For i As Integer = 0 To dtTemp.Rows.Count - 1
            '    If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
            '    rowDel = dtTemp.Rows(i)
            '    If rowDel("STT").ToString.Trim = "" Then
            '        dtTemp.Rows(i).Delete()
            '    Else
            '        checkOut = 0
            '    End If
            'Next
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
                Dim rep As New RecruitmentRepository

                count += 1
            Next
            dtTemp.AcceptChanges()

        Catch ex As Exception
            checkOut = 1
            'ShowMessage(Translate("Phải nhập số thứ tự,Xin kiểm tra lại"), NotifyType.Warning)
            Exit Sub
        End Try
    End Sub

    Private Sub TableMapping4(ByVal dtTemp As System.Data.DataTable, ByVal dtFile As System.Data.DataTable)
        Try

            ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
            dtTemp.Columns(0).ColumnName = "RELATION_NAME"
            dtTemp.Columns(1).ColumnName = "RELATION_ID"
            dtTemp.Columns(2).ColumnName = "FULLNAME"
            dtTemp.Columns(3).ColumnName = "BIRTH_YEAR"
            dtTemp.Columns(4).ColumnName = "JOB"
            dtTemp.Columns(5).ColumnName = "ADDRESS"
            'XOA DONG TIEU DE VA HEADER
            dtTemp.Rows(0).Delete()
            dtTemp.Rows(0).Delete()
            dtTemp.Rows(0).Delete()
            ' add Log
            Dim _error As Boolean = True
            Dim count As Integer
            Dim newRow As DataRow
            Dim dsEMP As DataTable
            'XOA NHUNG DONG DU LIEU NULL STT
            'Dim rowDel As DataRow
            'For i As Integer = 0 To dtTemp.Rows.Count - 1
            '    If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
            '    rowDel = dtTemp.Rows(i)
            '    If rowDel("STT").ToString.Trim = "" Then
            '        dtTemp.Rows(i).Delete()
            '    Else
            '        checkOut = 0
            '    End If
            'Next
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
                Dim rep As New RecruitmentRepository

                count += 1
            Next
            dtTemp.AcceptChanges()

        Catch ex As Exception
            checkOut = 1
            'ShowMessage(Translate("Phải nhập số thứ tự,Xin kiểm tra lại"), NotifyType.Warning)
            Exit Sub
        End Try
    End Sub

End Class
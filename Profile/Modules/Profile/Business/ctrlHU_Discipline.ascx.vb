Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Common
Imports Telerik.Web.UI
Imports System.IO
Imports WebAppLog
Imports Aspose.Cells
Imports System.Drawing
Imports HistaffFrameworkPublic
Imports Common.CommonBusiness
Imports System.Globalization

Public Class ctrlHU_Discipline
    Inherits Common.CommonView
    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
    Dim log As New UserLog

#Region "Property"
    ''' <summary>
    ''' Object Disciplines - ky luat
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Disciplines As List(Of DisciplineDTO)
        Get
            Return ViewState(Me.ID & "_Disciplines")
        End Get
        Set(ByVal value As List(Of DisciplineDTO))
            ViewState(Me.ID & "_Disciplines") = value
        End Set
    End Property
    ''' <summary>
    ''' Xoa danh sach ky luat
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DeleteDisciplines As List(Of DisciplineDTO)
        Get
            Return ViewState(Me.ID & "_DeleteDiscipline")
        End Get
        Set(ByVal value As List(Of DisciplineDTO))
            ViewState(Me.ID & "_DeleteDiscipline") = value
        End Set
    End Property
    ''' <summary>
    ''' Danh sach combobox data
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListComboData As Profile.ProfileBusiness.ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As Profile.ProfileBusiness.ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
    ''' <summary>
    ''' idSelect
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property
    ''' <summary>
    ''' Isload
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property
    ''' <summary>
    ''' Duyet ky luat
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ApproveDiscipline As DisciplineDTO
        Get
            Return ViewState(Me.ID & "_ApproveDiscipline")
        End Get
        Set(ByVal value As DisciplineDTO)
            ViewState(Me.ID & "_ApproveDiscipline") = value
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

#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi de phuong thuc ViewLoad hien thi trang
    ''' Lam moi trang thai cac control tren trang
    ''' Cap nhat lai trang thai cac control tren trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi de phuong thuc khoi tao cac control tren trang
    ''' Khoi tao cac thiet lap cho rad grid rgDiscipline
    ''' Khoi tao trang thai cho cac control tren page thong qua viec goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgDiscipline.AllowCustomPaging = True
            rgDiscipline.SetFilter()
            'rgDiscipline.ClientSettings.EnablePostBackOnRowClick = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            btnSearch.CausesValidation = False
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi de phuong thuc bind du lieu vao cac control tren page
    ''' Bind data cho cac combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Phuong thuc khoi tao, thiet lap cac trang thai cua cac control tren page
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarDisciplines
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Export,
                                       ToolbarItem.Delete, ToolbarItem.Print, ToolbarItem.Next, ToolbarItem.Import)
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_CREATE_BATCH,
                                                                  ToolbarIcons.Add,
                                                                  ToolbarAuthorize.Special1,
                                                                  "Phê duyệt hàng loạt"))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_APPROVE_OPEN,
                                                                  ToolbarIcons.Add,
                                                                  ToolbarAuthorize.Special1,
                                                                  "Mở phê duyệt"))
            CType(MainToolBar.Items(4), RadToolBarButton).Text = "In quyết định"
            CType(Me.MainToolBar.Items(5), RadToolBarButton).Text = Translate("Xuất file mẫu")
            CType(Me.MainToolBar.Items(5), RadToolBarButton).ImageUrl = CType(Me.MainToolBar.Items(2), RadToolBarButton).ImageUrl
            CType(Me.MainToolBar.Items(6), RadToolBarButton).Text = Translate("Nhập file mẫu")
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc Cap nhat trang thai cua cua cac control tren page 
    ''' Cac trang thai bao gom: xoa, duyet, huy kich hoat
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            UpdateCotrolEnabled(False)
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE

                    Dim objD As New List(Of DisciplineDTO)
                    Dim lst As New List(Of Decimal)
                    For Each item As GridDataItem In rgDiscipline.SelectedItems
                        Dim obj As New DisciplineDTO
                        obj.ID = Utilities.ObjToDecima(item.GetDataKeyValue("ID"))
                        objD.Add(obj)
                    Next
                    If rep.DeleteDiscipline(objD) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        Refresh("UpdateView")
                        UpdateControlState()
                    End If

                    'For Each DeleteDiscipline In DeleteDisciplines
                    '    Dim lst As New List(Of Decimal)
                    '    rep.DeleteDiscipline(DeleteDiscipline)
                    'Next
                    'DeleteDisciplines = Nothing
                    'Refresh("UpdateView")
                    'UpdateControlState()
                Case CommonMessage.STATE_APPROVE
                    If rep.ApproveDiscipline(ApproveDiscipline) Then
                        ApproveDiscipline = Nothing
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                Case CommonMessage.STATE_DEACTIVE

                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgDiscipline.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDiscipline.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.StopDisciplineSalary(lstDeletes) Then
                        ApproveDiscipline = Nothing
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi de phuong thuc lam moi cac thiet lap cho cac control tren page the trang thai page: trang thai view thong thuong,
    ''' trang thai updateView, InsertView, Cancel 
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDiscipline.Rebind()

                        SelectedItemDataGridByKey(rgDiscipline, IDSelect, , rgDiscipline.CurrentPageIndex)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgDiscipline.CurrentPageIndex = 0
                        rgDiscipline.MasterTableView.SortExpressions.Clear()
                        rgDiscipline.Rebind()
                        SelectedItemDataGridByKey(rgDiscipline, IDSelect, )
                    Case "Cancel"
                        rgDiscipline.MasterTableView.ClearSelectedItems()
                End Select
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command tren toolbar OnMainToolbarClick
    ''' Cac trang thai command bao gom: them moi, sua, xoa, huy, in an, xuat file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            log = LogHelper.GetUserLog
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    DeleteDisciplines = New List(Of DisciplineDTO)
                    Dim lstID As New List(Of Decimal)
                    For idx = 0 To rgDiscipline.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgDiscipline.SelectedItems(idx)
                        Dim objDiscipline As DisciplineDTO
                        objDiscipline = (From p In Disciplines Where p.ID = item.GetDataKeyValue("ID")).FirstOrDefault

                        If objDiscipline.STATUS_ID = ProfileCommon.DISCIPLINE_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Kỷ luật đã phê duyệt không được phép xóa. Vui lòng kiểm tra lại."), NotifyType.Warning)
                            Exit Sub
                        End If

                        DeleteDisciplines.Add(New DisciplineDTO With {.ID = objDiscipline.ID,
                                                                   .DECISION_ID = objDiscipline.DECISION_ID})
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    ctrlMessageBox.MessageText = "Bạn có chắc chắn muốn Dừng giảm trừ?"
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_PRINT
                    If rgDiscipline.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgDiscipline.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim dtData As DataTable
                    Dim folderName As String = ""
                    Dim filePath As String = ""
                    Dim extension As String = ""
                    Dim iError As Integer = 0
                    Dim item As GridDataItem = rgDiscipline.SelectedItems(0)
                    ' Kiểm tra + lấy thông tin trong database
                    Using rep As New ProfileRepository
                        dtData = rep.GetHU_DataDynamic(item.GetDataKeyValue("ID"),
                                                       ProfileCommon.HU_TEMPLATE_TYPE.DISCIPLINE_ID,
                                                       folderName)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage("Dữ liệu không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                        If folderName = "" Then
                            ShowMessage("Thư mục không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

                    ' Kiểm tra file theo thông tin trong database
                    If Not Utilities.GetTemplateLinkFile(1,
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
                                             item.GetDataKeyValue("EMPLOYEE_CODE") & "_KL_" & _
                                             Format(Date.Now, "yyyyMMddHHmmss"),
                                             dtData,
                                             Response)
                    End Using

                Case "PRINT_VPCT"
                    If rgDiscipline.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgDiscipline.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim dtData As DataTable
                    Dim folderName As String = ""
                    Dim filePath As String = ""
                    Dim extension As String = ""
                    Dim iError As Integer = 0
                    Dim item As GridDataItem = rgDiscipline.SelectedItems(0)
                    ' Kiểm tra + lấy thông tin trong database
                    Using rep As New ProfileRepository
                        dtData = rep.GetHU_DataDynamic(item.GetDataKeyValue("ID"),
                                                       ProfileCommon.HU_TEMPLATE_TYPE.DISCIPLINE_ID,
                                                       folderName)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage("Dữ liệu không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                        If folderName = "" Then
                            ShowMessage("Thư mục không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

                    ' Kiểm tra file theo thông tin trong database
                    If Not Utilities.GetTemplateLinkFile(6,
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
                                             item.GetDataKeyValue("EMPLOYEE_CODE") & "_KL_" & _
                                             Format(Date.Now, "yyyyMMddHHmmss"),
                                             dtData,
                                             Response)
                    End Using
                Case "PRINT_BKS"
                    If rgDiscipline.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgDiscipline.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim dtData As DataTable
                    Dim folderName As String = ""
                    Dim filePath As String = ""
                    Dim extension As String = ""
                    Dim iError As Integer = 0
                    Dim item As GridDataItem = rgDiscipline.SelectedItems(0)
                    ' Kiểm tra + lấy thông tin trong database
                    Using rep As New ProfileRepository
                        dtData = rep.GetHU_DataDynamic(item.GetDataKeyValue("ID"),
                                                       ProfileCommon.HU_TEMPLATE_TYPE.DISCIPLINE_ID,
                                                       folderName)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage("Dữ liệu không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                        If folderName = "" Then
                            ShowMessage("Thư mục không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

                    ' Kiểm tra file theo thông tin trong database
                    If Not Utilities.GetTemplateLinkFile(7,
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
                                             item.GetDataKeyValue("EMPLOYEE_CODE") & "_KL_" & _
                                             Format(Date.Now, "yyyyMMddHHmmss"),
                                             dtData,
                                             Response)
                    End Using
                Case CommonMessage.TOOLBARITEM_APPROVE
                    Dim objDiscipline As DisciplineDTO
                    'Dim rep As New ProfileBusinessRepository
                    objDiscipline = (From p In Disciplines Where p.ID = rgDiscipline.SelectedValue).FirstOrDefault

                    If objDiscipline.STATUS_ID = ProfileCommon.DISCIPLINE_STATUS.APPROVE_ID Then
                        ShowMessage(Translate("Kỷ luật đã phê duyệt. Vui lòng kiểm tra lại."), NotifyType.Warning)
                        Exit Sub
                    End If

                    If objDiscipline IsNot Nothing Then
                        ApproveDiscipline = New DisciplineDTO With {.ID = objDiscipline.ID,
                                                                    .EMPLOYEE_ID = objDiscipline.EMPLOYEE_ID,
                                                                    .EFFECT_DATE = objDiscipline.EFFECT_DATE}
                    End If
                    If ApproveDiscipline IsNot Nothing Then

                        ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_APPROVE)
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    End If

                Case CommonMessage.TOOLBARITEM_EXPORT

                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgDiscipline.ExportExcel(Server, Response, dtData, "Discipline")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_CREATE_BATCH
                    BatchApproveDiscipline()
                Case CommonMessage.TOOLBARITEM_APPROVE_OPEN
                    Open_ApproveDiscipline()
                Case CommonMessage.TOOLBARITEM_NEXT
                    Dim rep As New ProfileBusinessRepository
                    Dim dtDATA As DataSet = rep.EXPORT_DISCIPLINE()

                    ExportTemplate("Profile\Import\Template_Import_Discipline.xls",
                                   dtDATA, Nothing, "Template_Import_Discipline" & Format(Date.Now, "yyyyMMdd"))
                    rep.Dispose()
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/08/2017</lastupdate>
    ''' <summary>
    ''' Event xu ly upload file khi click button [OK] o popup ctrlUpload
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim fileName As String
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim rep As New ProfileBusinessRepository
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim countFile As Integer = ctrlUpload1.UploadedFiles.Count
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

                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    If rep.INPORT_DISCIPLINE(DocXml, log.Username) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        rgDiscipline.Rebind()
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
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
        End Try
    End Sub

    Private Function SplipKey(ByVal value As String) As String
        Dim array() As String = value.Split("-")
        Dim key = array(array.Length - 1)
        If key IsNot Nothing Then
            key = key.Replace(" ", "")
            Return key
        Else
            key = ""
            Return key
        End If
    End Function

    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
        dtTemp.Columns(0).ColumnName = "EMPLOYEE_CODE"
        dtTemp.Columns(4).ColumnName = "EFFECT_DATE"
        dtTemp.Columns(5).ColumnName = "SIGN_DATE"
        dtTemp.Columns(6).ColumnName = "SIGN_CODE"
        dtTemp.Columns(7).ColumnName = "DISCIPLINE_LEVEL_NAME"
        dtTemp.Columns(8).ColumnName = "DISCIPLINE_TYPE_NAME"
        dtTemp.Columns(9).ColumnName = "INDEMNIFY_MONEY"
        dtTemp.Columns(10).ColumnName = "PAIDMONEY"
        dtTemp.Columns(11).ColumnName = "DISCIPLINE_REASON"
        dtTemp.Columns(12).ColumnName = "EXPLAIN"
        dtTemp.Columns(13).ColumnName = "VIOLATION_DATE"
        dtTemp.Columns(14).ColumnName = "DISCIPLINE_REASON_DETAIL"
        dtTemp.Columns(15).ColumnName = "DEDUCT_FROM_SALARY"
        dtTemp.Columns(16).ColumnName = "YEAR"
        dtTemp.Columns(17).ColumnName = "PERIOD_NAME"
        dtTemp.Columns(18).ColumnName = "EXPIRE_DATE"
        dtTemp.Columns(19).ColumnName = "NO"
        dtTemp.Columns(20).ColumnName = "ISSUE_DATE"
        dtTemp.Columns(21).ColumnName = "MONEY_MATERIAL"
        dtTemp.Columns(22).ColumnName = "REMARK"

        'XOA DONG TIEU DE VA HEADER
        dtTemp.Rows(0).Delete()
        dtTemp.Rows(1).Delete()

        ' add Log
        Dim _error As Boolean = True
        Dim count As Integer
        Dim newRow As DataRow
        Dim dsEMP As DataTable
        Dim rep As New ProfileBusinessRepository
        Dim empId As Integer
        Dim startDate As Date
        If dtLogs Is Nothing Then
            dtLogs = New DataTable("data")
            dtLogs.Columns.Add("STT", GetType(Integer))
            dtLogs.Columns.Add("EMPLOYEE_CODE", GetType(String))
            dtLogs.Columns.Add("DISCIPTION", GetType(String))
        End If
        dtLogs.Clear()
        'XOA NHUNG DONG DU LIEU NULL STT
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
            newRow("STT") = count + 1
            newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")

            empId = rep.CheckEmployee_Exits(rows("EMPLOYEE_CODE"))

            If empId = 0 Then
                newRow("DISCIPTION") = "Mã nhân viên - Không tồn tại,"
                _error = False
            Else
                rows("EMPLOYEE_CODE") = empId
            End If

            If Not IsDBNull(rows("DEDUCT_FROM_SALARY")) AndAlso rows("DEDUCT_FROM_SALARY") = "Có" Then
                rows("DEDUCT_FROM_SALARY") = -1
            Else
                rows("DEDUCT_FROM_SALARY") = 0
            End If

            If IsDBNull(rows("EFFECT_DATE")) OrElse rows("EFFECT_DATE") = "" OrElse CheckDate(rows("EFFECT_DATE"), startDate) = False Then
                rows("EFFECT_DATE") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày hiệu lực - Không đúng định dạng,"
                _error = False
            End If

            If IsDBNull(rows("SIGN_DATE")) OrElse rows("SIGN_DATE") = "" OrElse CheckDate(rows("SIGN_DATE"), startDate) = False Then
                rows("SIGN_DATE") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày ký - Không đúng định dạng,"
                _error = False
            End If

            If IsDBNull(rows("DISCIPLINE_LEVEL_NAME")) OrElse rows("DISCIPLINE_LEVEL_NAME") = "" Then
                rows("DISCIPLINE_LEVEL_NAME") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Cấp kỷ luật - bắt buộc nhập,"
                _error = False
            End If

            If IsDBNull(rows("DISCIPLINE_TYPE_NAME")) OrElse rows("DISCIPLINE_TYPE_NAME") = "" Then
                rows("DISCIPLINE_TYPE_NAME") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Hình thức kỷ luật - bắt buộc nhập,"
                _error = False
            End If

            If IsDBNull(rows("VIOLATION_DATE")) OrElse rows("VIOLATION_DATE") = "" Then
            Else
                If CheckDate(rows("VIOLATION_DATE"), startDate) = False Then
                    rows("VIOLATION_DATE") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày vi phạm - Không đúng định dạng,"
                    _error = False
                End If
            End If

            If IsDBNull(rows("INDEMNIFY_MONEY")) Then
            Else
                If IsNumeric(rows("INDEMNIFY_MONEY")) = False Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Số tiền - Không đúng định dạng,"
                    _error = False
                End If
            End If

            If IsDBNull(rows("PAIDMONEY")) Then
            Else
                If IsNumeric(rows("PAIDMONEY")) = False Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Số tiền bồi thường - Không đúng định dạng,"
                    _error = False
                End If
            End If

            If IsDBNull(rows("SIGN_CODE")) OrElse rows("SIGN_CODE") = "" Then
            Else
                rows("SIGN_CODE") = SplipKey(rows("SIGN_CODE"))
            End If

            If IsDBNull(rows("EXPIRE_DATE")) OrElse rows("EXPIRE_DATE") = "" Then
            Else
                If CheckDate(rows("EXPIRE_DATE"), startDate) = False Then
                    rows("EXPIRE_DATE") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày hết hiệu lực - Không đúng định dạng,"
                    _error = False
                End If
            End If

            If IsDBNull(rows("ISSUE_DATE")) OrElse rows("ISSUE_DATE") = "" Then
            Else
                If CheckDate(rows("ISSUE_DATE"), startDate) = False Then
                    rows("ISSUE_DATE") = "NULL"
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày ban hành - Không đúng định dạng,"
                    _error = False
                End If
            End If

            If IsDBNull(rows("MONEY_MATERIAL")) Then
            Else
                If IsNumeric(rows("MONEY_MATERIAL")) = False Then
                    newRow("DISCIPTION") = newRow("DISCIPTION") + "Tiền bồi thường vật chất - Không đúng định dạng,"
                    _error = False
                End If
            End If

            If _error = False Then
                dtLogs.Rows.Add(newRow)
                _error = True
            End If
            count += 1
        Next
        dtTemp.AcceptChanges()
    End Sub

    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click vao button command ctrlMessageBox
    ''' voi cac trang thai xoa, duyet, huy kich hoat
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_APPROVE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
                UpdateControlState()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click vao button btnSearch
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgDiscipline.CurrentPageIndex = 0
            rgDiscipline.MasterTableView.SortExpressions.Clear()
            rgDiscipline.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Xu ly su kien SelectedNodeChanged cho control ctrlOrg
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgDiscipline.CurrentPageIndex = 0
            rgDiscipline.MasterTableView.SortExpressions.Clear()
            rgDiscipline.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Need data source cho rad grid rgDiscipline
    ''' Tao du lieu cho filter
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDiscipline.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien ItemDataBound cho rad grin rgDiscipline
    ''' Hien thi tooltip tren rgDiscipline
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgDiscipline.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow("ORG_DESC").Text)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Click khi click vao button btnPrintSupport
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrintSupport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintSupport.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If cboPrintSupport.SelectedValue = "" Then
                ShowMessage(Translate("Bạn chưa chọn biểu mẫu"), NotifyType.Warning)
                Exit Sub
            End If
            Dim dtData As DataTable
            Dim folderName As String = ""
            Dim filePath As String = ""
            Dim extension As String = ""
            Dim iError As Integer = 0
            Dim item As GridDataItem = rgDiscipline.SelectedItems(0)
            ' Kiểm tra + lấy thông tin trong database
            Using rep As New ProfileRepository
                dtData = rep.GetHU_DataDynamic(item.GetDataKeyValue("ID"),
                                               ProfileCommon.HU_TEMPLATE_TYPE.DISCIPLINE_SUPPORT_ID,
                                               folderName)
                If dtData.Rows.Count = 0 Then
                    ShowMessage("Dữ liệu không tồn tại", NotifyType.Warning)
                    Exit Sub
                End If
                If folderName = "" Then
                    ShowMessage("Thư mục không tồn tại", NotifyType.Warning)
                    Exit Sub
                End If
            End Using

            ' Kiểm tra file theo thông tin trong database
            If Not Utilities.GetTemplateLinkFile(cboPrintSupport.SelectedValue,
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
                                     item.GetDataKeyValue("EMPLOYEE_CODE") & "_KL_" & _
                                     Format(Date.Now, "yyyyMMddHHmmss"),
                                     dtData,
                                     Response)
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dsData As DataSet,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dsData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function


#End Region

#Region "Custom"
    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý phê duyệt ky luat</summary>
    ''' <remarks></remarks>
    Private Sub BatchApproveDiscipline()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim lstID As New List(Of Decimal)

        Try
            '1. Check có rows nào được select hay không
            If rgDiscipline Is Nothing OrElse rgDiscipline.SelectedItems.Count <= 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            End If

            Dim listCon As New List(Of ContractDTO)

            For Each dr As Telerik.Web.UI.GridDataItem In rgDiscipline.SelectedItems
                Dim ID As New Decimal
                If Not dr.GetDataKeyValue("STATUS_ID").Equals("447") Then
                    ID = dr.GetDataKeyValue("ID")
                    lstID.Add(ID)
                End If
            Next

            If lstID.Count > 0 Then
                'Dim bCheckHasfile = rep.CheckHasFileDiscipline(lstID)
                For Each item As GridDataItem In rgDiscipline.SelectedItems
                    If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                        ShowMessage(Translate("Bản ghi đã phê duyệt."), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                'If bCheckHasfile = 1 Then
                '    ShowMessage(Translate("Duyệt khi tất cả các record đã có tập tin đính kèm,bạn kiểm tra lại"), NotifyType.Warning)
                '    Exit Sub
                'End If
                If rep.ApproveListDiscipline(lstID) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgDiscipline.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            Else
                ShowMessage("Các kỷ luật được chọn đã ở trạng thái chờ phê duyệt", NotifyType.Information)
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub Open_ApproveDiscipline()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim lstID As New List(Of Decimal)

        Try
            '1. Check có rows nào được select hay không
            If rgDiscipline Is Nothing OrElse rgDiscipline.SelectedItems.Count <= 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            End If

            For idx = 0 To rgDiscipline.SelectedItems.Count - 1
                Dim item As GridDataItem = rgDiscipline.SelectedItems(idx)
                Dim objDiscipline As DisciplineDTO
                Dim ID As New Decimal
                objDiscipline = (From p In Disciplines Where p.ID = item.GetDataKeyValue("ID")).FirstOrDefault
                If objDiscipline.STATUS_ID = ProfileCommon.DISCIPLINE_STATUS.APPROVE_ID Then
                    ID = objDiscipline.ID
                    lstID.Add(ID)
                End If
            Next

            If lstID.Count > 0 Then
                If rep.Open_ApproveDiscipline(lstID) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgDiscipline.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            Else
                ShowMessage("Các kỷ luật được chọn đã được phê duyệt", NotifyType.Information)
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Cap nhat trang thai cho phep cac control tren page
    ''' </summary>
    ''' <param name="bCheck"></param>
    ''' <remarks></remarks>
    Private Sub UpdateCotrolEnabled(ByVal bCheck As Boolean)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ctrlOrg.Enabled = Not bCheck
            Utilities.EnabledGrid(rgDiscipline, Not bCheck, False)
            rdFromDate.Enabled = Not bCheck
            rdToDate.Enabled = Not bCheck
            btnSearch.Enabled = Not bCheck
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Lay du lieu cho combobox cboPrintSupport
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim dtData As DataTable
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            Using rep As New ProfileRepository
                dtData = rep.GetOtherList("DISCIPLINE_SUPPORT")
                FillRadCombobox(cboPrintSupport, dtData, "NAME", "ID")
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 11/07/2017 13:40
    ''' </lastupdate>
    ''' <summary>
    ''' Tao du lieu cho filter
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New DisciplineDTO
        _filter.param = New Profile.ProfileBusiness.ParamDTO
        Dim rep As New ProfileBusinessRepository
        Dim bCheck As Boolean = False
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If ctrlOrg.CurrentValue IsNot Nothing Then
                _filter.param.ORG_ID = Utilities.ObjToDecima(ctrlOrg.CurrentValue)
            Else
                _filter.ORG_ID = 46
            End If
            _filter.IS_TERMINATE = chkChecknghiViec.Checked
            _filter.param.IS_DISSOLVE = ctrlOrg.IsDissolve
            SetValueObjectByRadGrid(rgDiscipline, _filter)
            _filter.EFFECT_FROM = rdFromDate.SelectedDate
            _filter.EFFECT_TO = rdToDate.SelectedDate
            Dim MaximumRows As Integer
            Dim Sorts As String = rgDiscipline.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetDiscipline(_filter, Sorts).ToTable()
                Else
                    Return rep.GetDiscipline(_filter).ToTable()
                End If
            Else

                If Sorts IsNot Nothing Then
                    Me.Disciplines = rep.GetDiscipline(_filter, rgDiscipline.CurrentPageIndex, rgDiscipline.PageSize, MaximumRows, Sorts)
                Else
                    Me.Disciplines = rep.GetDiscipline(_filter, rgDiscipline.CurrentPageIndex, rgDiscipline.PageSize, MaximumRows)
                End If
                rgDiscipline.VirtualItemCount = MaximumRows
                rgDiscipline.DataSource = Me.Disciplines
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Function

    Private Function CheckDate(ByVal value As String, ByRef result As Date) As Boolean
        Dim dateCheck As Boolean
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
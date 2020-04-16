Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Common
Imports Telerik.Web.UI
Imports WebAppLog
Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Aspose.Cells
Imports HistaffFrameworkPublic
Imports Microsoft.VisualBasic.Logging


Public Class ctrlHU_Health_Mng
    Inherits Common.CommonView
    Protected WithEvents WelfareMng As ViewBase
    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

    'Dim log As New UserLog
#Region "Property"

    ''' <summary>
    ''' List obj HealthMng
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HealthMng As DataTable
        Get
            Return ViewState(Me.ID & "_WelfareMng")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_WelfareMng") = value
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

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("EMPLOYEE_NAME", GetType(String))
                dt.Columns.Add("ORG_NAME", GetType(String))
                dt.Columns.Add("YEAR_KHAMBENH", GetType(String))
                dt.Columns.Add("DATE_KHAMBENH", GetType(String))
                dt.Columns.Add("CAN_NANG", GetType(String))
                dt.Columns.Add("HUYET_AP", GetType(String))
                dt.Columns.Add("NOI_KHOA", GetType(String))
                dt.Columns.Add("NGOAI_KHOA", GetType(String))
                dt.Columns.Add("PHU_KHOA", GetType(String))
                dt.Columns.Add("MAT", GetType(String))
                dt.Columns.Add("TMH", GetType(String))
                dt.Columns.Add("RHM", GetType(String))
                dt.Columns.Add("DA_LIEU", GetType(String))
                dt.Columns.Add("CONG_THUC_MAU", GetType(String))
                dt.Columns.Add("DUONG_MAU", GetType(String))
                dt.Columns.Add("XN_NUOC_TIEU", GetType(String))
                dt.Columns.Add("X_QUANG", GetType(String))
                dt.Columns.Add("DIEN_TIM", GetType(String))
                dt.Columns.Add("SIEU_AM", GetType(String))
                dt.Columns.Add("XN_PHAN", GetType(String))
                dt.Columns.Add("SIEU_VI_A", GetType(String))
                dt.Columns.Add("SIEU_VI_E", GetType(String))
                dt.Columns.Add("SIEU_VI_B", GetType(String))
                dt.Columns.Add("KT_VIEN_GAN_B", GetType(String))
                dt.Columns.Add("CHUC_NANG_GAN", GetType(String))
                dt.Columns.Add("CHUC_NANG_THAN", GetType(String))
                dt.Columns.Add("MO_MAU", GetType(String))
                dt.Columns.Add("KQ1", GetType(String))
                dt.Columns.Add("KQ2", GetType(String))
                dt.Columns.Add("KQ3", GetType(String))
                dt.Columns.Add("KQ4", GetType(String))
                dt.Columns.Add("KQ5", GetType(String))
                dt.Columns.Add("KQ6", GetType(String))
                dt.Columns.Add("KQ7", GetType(String))
                dt.Columns.Add("KQ8", GetType(String))
                dt.Columns.Add("KQ9", GetType(String))
                dt.Columns.Add("KQ10", GetType(String))
                dt.Columns.Add("HEALTH_TYPE", GetType(String))
                dt.Columns.Add("NHOM_BENH", GetType(String))
                dt.Columns.Add("TEN_BENH", GetType(String))
                dt.Columns.Add("KET_LUAB", GetType(String))
                dt.Columns.Add("DO_THINH_LUC_SB ", GetType(String))
                dt.Columns.Add("DO_THINH_LUC_HC ", GetType(String))
                dt.Columns.Add("DO_CN_HO_HAP ", GetType(String))
                dt.Columns.Add("XN_HAMLUONG_TOLUEN ", GetType(String))
                dt.Columns.Add("BENH_NN1 ", GetType(String))
                dt.Columns.Add("BENH_NN2 ", GetType(String))
                dt.Columns.Add("BENH_TN_NN ", GetType(String))
                dt.Columns.Add("NGAY_DIEU_TRI ", GetType(String))
                dt.Columns.Add("PP_DIEU_TRI ", GetType(String))
                dt.Columns.Add("KQ_DIEU_TRI ", GetType(String))


                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property


#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 10/07/2017 09:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            rgWelfareMng.SetFilter()
            rgWelfareMng.AllowCustomPaging = True
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 09:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Xét các trạng thái của grid rgWelfareMng
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            InitControl()
            ctrlUpload1.isMultiple = False
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 09:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức BindData
    ''' Gọi phương thức load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository
        Try
            GetDataCombo()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub GetDataCombo()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileRepository
        Try
            'ListComboData = New ComboBoxDataDTO
            'ListComboData.GET_WELFARE = True
            'rep.GetComboList(ListComboData)
            'FillRadCombobox(cbTyleWelfare, ListComboData.LIST_WELFARE, "NAME", "ID")
            Dim dtData = rep.GetOtherList("WELFARE", False)
            FillRadCombobox(cbTyleWelfare, dtData, "NAME", "ID", True)
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <lastupdate>
    ''' 10/07/2017 09:40
    ''' </lastupdate>
    ''' <summary>
    ''' Ham khoi tao toolbar voi cac button them moi, sua, xuat file, xoa
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarHealthMng
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Export,
                                     ToolbarItem.ExportTemplate, ToolbarItem.Import)
            MainToolBar.Items(0).Text = Translate("Xuất Excel")
            MainToolBar.Items(1).Text = Translate("Xuất file mẫu")
            CType(Me.MainToolBar.Items(2), RadToolBarButton).ImageUrl = CType(Me.MainToolBar.Items(0), RadToolBarButton).ImageUrl

            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 09:40
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Xet cac tuy chon gia tri cua Message la UpdateView, InsertView
    ''' Bind lai du lieu cho grid rgWelfareMng
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim rep As New ProfileBusinessRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        rgWelfareMng.Rebind()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        rgWelfareMng.Rebind()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
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
    ''' 10/07/2017 09:30
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command khi click item tren toolbar
    ''' Command them, sua phuc loi ca nhan, xoa phuc loi ca nhan, xuat file excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objOrgFunction As New OrganizationDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim sError As String = ""
        Dim rep As New ProfileBusinessRepository
        Dim id As String = ""
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_WelfareMngNewEdit&group=Business")
                Case CommonMessage.TOOLBARITEM_EDIT
                    CurrentState = CommonMessage.STATE_EDIT
                    If rgWelfareMng.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    ElseIf rgWelfareMng.SelectedItems.Count > 1 Then
                        ShowMessage(Translate("Bạn không thể sửa nhiều bản ghi! Không thể thực hiện thao tác này"), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each item As GridDataItem In rgWelfareMng.SelectedItems
                        id = item.GetDataKeyValue("ID")
                    Next
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_WelfareMngNewEdit&group=Business&gUID=" + id)
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgWelfareMng.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    'For Each item As GridDataItem In rgWelfareMng.SelectedItems
                    '    If item.GetDataKeyValue("WORK_STATUS") = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                    '        ShowMessage(Translate("Nhân viên nghỉ việc. Không được xóa thông tin."), Utilities.NotifyType.Warning)
                    '        Exit Sub
                    '    End If
                    'Next
                    For Each item As GridDataItem In rgWelfareMng.SelectedItems
                        If item.GetDataKeyValue("EFFECT_DATE") <= Date.Now Then
                            ShowMessage(Translate("Không được xóa những phúc lợi đã tới ngày hiệu lực"), Utilities.NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgWelfareMng.ExportExcel(Server, Response, dtData, "HealthMng")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    Template_ImportHealth_Mng()
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#Region "import Quản lý sức khỏe"
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Try
            Import_Data()
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi"), NotifyType.Error)
        End Try

    End Sub


    Private Sub Import_Data()
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim countFile As Integer = ctrlUpload1.UploadedFiles.Count
            Dim fileName As String
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim rep As New ProfileBusinessRepository
            '_mylog = LogHelper.GetUserLog
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
            TableMapping1(ds.Tables(0))
            If dtLogs Is Nothing Or dtLogs.Rows.Count <= 0 Then
                Dim DocXml As String = String.Empty
                Dim sw As New StringWriter()
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    ds.Tables(0).WriteXml(sw, False)
                    DocXml = sw.ToString
                    If rep.Import_Health_Mng(DocXml) Then
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                    Else
                        ShowMessage(Translate(Common.CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Warning)
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TableMapping1(ByVal dtTemp As System.Data.DataTable)
        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
        dtTemp.Columns(0).ColumnName = "STT"
        dtTemp.Columns(1).ColumnName = "EMPLOYEE_CODE"
        dtTemp.Columns(2).ColumnName = "YEAR_KHAMBENH"
        dtTemp.Columns(3).ColumnName = "DATE_KHAMBENH"
        dtTemp.Columns(4).ColumnName = "CAN_NANG"
        dtTemp.Columns(5).ColumnName = "HUYET_AP"
        dtTemp.Columns(6).ColumnName = "NOI_KHOA"
        dtTemp.Columns(7).ColumnName = "NGOAI_KHOA"
        dtTemp.Columns(8).ColumnName = "PHU_KHOA"
        dtTemp.Columns(9).ColumnName = "MAT"
        dtTemp.Columns(10).ColumnName = "TMH"
        dtTemp.Columns(11).ColumnName = "RHM"
        dtTemp.Columns(12).ColumnName = "DA_LIEU"
        dtTemp.Columns(13).ColumnName = "CONG_THUC_MAU"
        dtTemp.Columns(14).ColumnName = "DUONG_MAU"
        dtTemp.Columns(15).ColumnName = "XN_NUOC_TIEU"
        dtTemp.Columns(16).ColumnName = "X_QUANG"
        dtTemp.Columns(17).ColumnName = "DIEN_TIM"
        dtTemp.Columns(18).ColumnName = "SIEU_AM"
        dtTemp.Columns(19).ColumnName = "SIEU_VI_A"
        dtTemp.Columns(20).ColumnName = "SIEU_VI_E"
        dtTemp.Columns(21).ColumnName = "SIEU_VI_B"
        dtTemp.Columns(22).ColumnName = "KT_VIEN_GAN_B"
        dtTemp.Columns(23).ColumnName = "CHUC_NANG_GAN"
        dtTemp.Columns(24).ColumnName = "CHUC_NANG_THAN"
        dtTemp.Columns(25).ColumnName = "MO_MAU"
        dtTemp.Columns(26).ColumnName = "KQ1"
        dtTemp.Columns(27).ColumnName = "KQ2"
        dtTemp.Columns(28).ColumnName = "KQ3"
        dtTemp.Columns(29).ColumnName = "KQ4"
        dtTemp.Columns(30).ColumnName = "KQ5"
        dtTemp.Columns(31).ColumnName = "KQ6"
        dtTemp.Columns(32).ColumnName = "KQ7"
        dtTemp.Columns(33).ColumnName = "KQ8"
        dtTemp.Columns(34).ColumnName = "KQ9"
        dtTemp.Columns(35).ColumnName = "KQ10"
        dtTemp.Columns(36).ColumnName = "HEALTH_TYPE"
        dtTemp.Columns(37).ColumnName = "NHOM_BENH"
        dtTemp.Columns(38).ColumnName = "TEN_BENH"
        dtTemp.Columns(39).ColumnName = "KET_LUAB"
        dtTemp.Columns(40).ColumnName = "GHI_CHU"
        dtTemp.Columns(41).ColumnName = "DO_THINH_LUC_SB"
        dtTemp.Columns(42).ColumnName = "DO_THINH_LUC_HC"
        dtTemp.Columns(43).ColumnName = "DO_CN_HO_HAP"
        dtTemp.Columns(44).ColumnName = "XN_HAMLUONG_TOLUEN"
        dtTemp.Columns(45).ColumnName = "BENH_NN1"
        dtTemp.Columns(46).ColumnName = "BENH_NN2"
        dtTemp.Columns(47).ColumnName = "BENH_TN_NN"
        dtTemp.Columns(48).ColumnName = "NGAY_DIEU_TRI"
        dtTemp.Columns(49).ColumnName = "PP_DIEU_TRI"
        dtTemp.Columns(50).ColumnName = "KQ_DIEU_TRI"

        dtTemp.Rows(0).Delete()
        dtTemp.Rows(1).Delete()
        dtTemp.Rows(2).Delete()


        ' add Log
        Dim _error As Boolean = True
        Dim count As Integer
        Dim newRow As DataRow
        Dim empId As Integer
        Dim rep As New ProfileBusinessRepository
        If dtLogs Is Nothing Then
            dtLogs = New DataTable("data")
            'dtLogs.Columns.Add("ID", GetType(Integer))
            'dtLogs.Columns.Add("EMPLOYEE_CODE", GetType(String))
            'dtLogs.Columns.Add("DISCIPTION", GetType(String))
        End If
        dtLogs.Clear()

        'XOA NHUNG DONG DU LIEU NULL EMPLOYYE CODE
        Dim rowDel As DataRow



        For i As Integer = 3 To dtTemp.Rows.Count - 1
            If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
            rowDel = dtTemp.Rows(i)
            If rowDel("EMPLOYEE_CODE").ToString.Trim = "" Then
                dtTemp.Rows(i).Delete()
            End If
        Next
        dtTemp.AcceptChanges()
    End Sub


    'Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim fileName As String
    '    Dim dsDataPrepare As New DataSet
    '    Dim workbook As Aspose.Cells.Workbook
    '    Dim worksheet As Aspose.Cells.Worksheet

    '    Try
    '        Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
    '        Dim savepath = Context.Server.MapPath(tempPath)

    '        For Each file As UploadedFile In ctrlUpload1.UploadedFiles
    '            fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
    '            file.SaveAs(fileName, True)
    '            workbook = New Aspose.Cells.Workbook(fileName)
    '            worksheet = workbook.Worksheets(0)
    '            dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
    '            If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
    '        Next
    '        dtData = dtData.Clone()
    '        TableMapping(dsDataPrepare.Tables(0))
    '        For Each rows As DataRow In dsDataPrepare.Tables(0).Select("Column2<>'""'").CopyToDataTable.Rows
    '            If IsDBNull(rows("Column2")) OrElse rows("Column2") = "" Then Continue For

    '            Dim newRow As DataRow = dtData.
    '            NewRow("STT") = rows("Column1")
    '            newRow("EMPLOYEE_CODE") = rows("Column2")
    '            newRow("EMPLOYEE_NAME") = rows("Column3")
    '            newRow("ORG_NAME") = rows("Phòng ban")
    '            newRow("YEAR_KHAMBENH") = rows("Năm")
    '            newRow("DATE_KHAMBENH") = rows("Đợt khám")
    '            newRow("CAN_NANG") = rows("Cân nặng")
    '            newRow("HUYET_AP") = rows("Huyết áp")
    '            newRow("NOI_KHOA") = rows("Nội Khoa")
    '            newRow("NGOAI_KHOA") = rows("Ngoại Khoa")
    '            newRow("PHU_KHOA") = rows("Phụ Khoa")
    '            newRow("MAT") = rows("Mắt")
    '            newRow("TMH") = rows("TMH")
    '            newRow("RHM") = rows("RHM")
    '            newRow("DA_LIEU") = rows("Da liễu")
    '            newRow("CONG_THUC_MAU") = rows("Công thức máu")
    '            newRow("DUONG_MAU") = rows("Đường máu")
    '            newRow("XN_NUOC_TIEU") = rows("XN Nước Tiểu")
    '            newRow("X_QUANG") = rows("X Quang")
    '            newRow("DIEN_TIM") = rows("ĐiệnTim")
    '            newRow("SIEU_AM") = rows("Siêu Âm")
    '            newRow("XN_PHAN") = rows("XN Phân")
    '            newRow("SIEU_VI_A") = rows("Siêu Vi A")
    '            newRow("SIEU_VI_E") = rows("Siêu Vi E")
    '            newRow("SIEU_VI_B") = rows("Siêu Vi B")
    '            newRow("KT_VIEN_GAN_B") = rows("Kháng thể viêm gan B")
    '            newRow("CHUC_NANG_GAN") = rows("Chức năng gan")
    '            newRow("CHUC_NANG_THAN") = rows("Chức năng thận")
    '            newRow("MO_MAU") = rows("Mỡ máu")
    '            newRow("KQ1") = rows("KQ1")
    '            newRow("KQ2") = rows("KQ2")
    '            newRow("KQ3") = rows("KQ3")
    '            newRow("KQ4") = rows("KQ4")
    '            newRow("KQ5") = rows("KQ5")
    '            newRow("KQ6") = rows("KQ6")
    '            newRow("KQ7") = rows("KQ7")
    '            newRow("KQ8") = rows("KQ8")
    '            newRow("KQ9") = rows("KQ9")
    '            newRow("KQ10") = rows("KQ10")
    '            newRow("HEALTH_TYPE") = rows("Loại sức khỏe(Mã)")
    '            newRow("NHOM_BENH") = rows("Nhóm bệnh(Mã)")
    '            newRow("TEN_BENH") = rows("Tên bệnh")
    '            newRow("KET_LUAB") = rows("Kết Luận")
    '            newRow("GHI_CHU") = rows("Ghi chú")
    '            newRow("DO_THINH_LUC_SB") = rows("Đo thính lực sơ bộ")
    '            newRow("DO_THINH_LUC_HC") = rows("Đo thính lục hoàn chỉnh")
    '            newRow("DO_CN_HO_HAP") = rows("Đo chức năng hô hấp")
    '            newRow("XN_HAMLUONG_TOLUEN") = rows("XN hàm lượng TOLUEN")
    '            newRow("BENH_NN1") = rows("NN1")
    '            newRow("BENH_NN2") = rows("NN2")
    '            newRow("BENH_TN_NN") = rows("Bệnh TN, NN cần theo dõi")
    '            newRow("NGAY_DIEU_TRI") = rows("Ngày điều trị")
    '            newRow("PP_DIEU_TRI") = rows("Phương pháp điều trị")
    '            newRow("KQ_DIEU_TRI") = rows("Kết quả điều trị")
    '            dtData.Rows.Add(newRow)
    '        Next
    '        dtData.TableName = "DATA"
    '        If loadToGrid() Then
    '            Dim sw As New StringWriter()
    '            Dim DocXml As String = String.Empty
    '            dtData.WriteXml(sw, False)
    '            DocXml = sw.ToString
    '            Dim sp As New ProfileStoreProcedure()
    '            If sp.Import_HoSoLuong(LogHelper.GetUserLog().Username.ToUpper, DocXml) Then
    '                ShowMessage(Translate("Import thành công"), NotifyType.Success)
    '            Else
    '                ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
    '            End If
    '            'End edit;
    '            rgWelfareMng.Rebind()
    '        End If

    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    Private Sub TableMapping(ByVal dtdata As DataTable)
        Dim row As DataRow = dtdata.Rows(0)
        Dim index As Integer = 0
        For Each cols As DataColumn In dtdata.Columns
            Try
                cols.ColumnName = row(index)
                index += 1
                If index > row.ItemArray.Length - 1 Then Exit For
            Catch ex As Exception
                Exit For
            End Try
        Next
        dtdata.Rows(0).Delete()
        dtdata.Rows(0).Delete()
        dtdata.AcceptChanges()
    End Sub

    Function loadToGrid() As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtError As New DataTable("ERROR")
        Try
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            HealthMng = dtData.Clone
            dtError = dtData.Clone
            Dim iRow = 1
            Dim _filter As New WorkingDTO
            Dim rep As New ProfileBusinessRepository
            Dim IBusiness As IProfileBusiness = New ProfileBusinessClient()
            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                isError = False
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)
                If isError Then
                    rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    dtError.Rows.Add(rowError)
                Else
                    HealthMng.ImportRow(row)
                End If
                iRow = iRow + 1
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                ' gộp các lỗi vào 1 cột ghi chú 
                Dim dtErrorGroup As New DataTable
                Dim RowErrorGroup As DataRow
                dtErrorGroup.Columns.Add("STT")
                dtErrorGroup.Columns.Add("NOTE")
                For j As Integer = 0 To dtError.Rows.Count - 1
                    Dim strNote As String = String.Empty
                    RowErrorGroup = dtErrorGroup.NewRow
                    For k As Integer = 1 To dtError.Columns.Count - 1
                        If Not dtError.Rows(j)(k) Is DBNull.Value Then
                            strNote &= dtError.Rows(j)(k) & "\"
                        End If
                    Next
                    RowErrorGroup("STT") = dtError.Rows(j)("EMPLOYEE_CODE")
                    RowErrorGroup("NOTE") = strNote
                    dtErrorGroup.Rows.Add(RowErrorGroup)
                Next
                dtErrorGroup.TableName = "DATA"
                Session("EXPORTREPORT") = dtErrorGroup
                rep.Dispose()
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_importIO_error');", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
            If isError OrElse (dtError IsNot Nothing AndAlso dtError.Rows.Count > 0) Then
                Return False
            Else
                Return True
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    Private Sub Template_ImportHealth_Mng()
        Dim rep As New Profile.ProfileBusinessRepository
        Try
            Dim configPath As String = ConfigurationManager.AppSettings("PathImportFolder")
            Dim dsData As DataSet = rep.GetHoSoLuongImport()

            rep.Dispose()
            If File.Exists(configPath + "Profile\RPT_HU_HEALTH_MNG.xls") Then
                ExportTemplate(configPath + "Profile\RPT_HU_HEALTH_MNG.xls",
                                      dsData, Nothing, "Template_QUANLYSUCKHOE_" & Format(Date.Now, "yyyyMMdd"))
            Else
                ShowMessage(Translate("Template không tồn tại"), Utilities.NotifyType.Error)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
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

            'cau hinh lai duong dan tren server
            filePath = sReportFileName

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


    ''' <lastupdate>
    ''' 10/07/2017 09:30
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien SelectedNodeChanged cua control ctrlOrg
    ''' Xet lai cac thiet lap trang thai cho grid rgWelfareMng
    ''' Bind lai du lieu cho rgWelfareMng
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgWelfareMng.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 09:30
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command button cua control ctrkMessageBox
    ''' Cap nhat trang thai cua cac control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim rep As New ProfileBusinessRepository
                Dim objD As New List(Of WelfareMngDTO)
                Dim lst As New List(Of Decimal)
                For Each item As GridDataItem In rgWelfareMng.SelectedItems
                    Dim obj As New WelfareMngDTO
                    obj.ID = Utilities.ObjToDecima(item.GetDataKeyValue("ID"))
                    objD.Add(obj)
                Next
                If rep.DeleteWelfareMng(objD) Then
                    objD = Nothing
                    Refresh("UpdateView")
                End If
                rep.Dispose()
                UpdateControlState()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 10/07/2017 09:30
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cho rad grid 
    ''' Tao du lieu filter
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgWelfareMng.NeedDataSource

        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
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
    ''' Xu ly su kien ItemDataBound cho rad grin rgWelfareMng
    ''' Hien thi tooltip tren rgWelfareMng
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgWelfareMng.ItemDataBound
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
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cua button btnSearch
    ''' Thiets lap trang thai cho rgWelfareMng
    ''' Bind lai du lieu cho rgWelfareMng
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgWelfareMng.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 10/07/2017 08:45
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc tao du lieu filter
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New HealthMngDTO
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            If ctrlOrg.CurrentValue <> "" Then
                _filter.ORG_ID = ctrlOrg.CurrentValue
            Else
                _filter.ORG_ID = 0
            End If
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                                .IS_DISSOLVE = ctrlOrg.IsDissolve}
            Dim MaximumRows As Integer
            Dim Sorts As String = rgWelfareMng.MasterTableView.SortExpressions.GetSortString()
            SetValueObjectByRadGrid(rgWelfareMng, _filter)
            '_filter.IS_TER = chkTerminate.Checked

            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GET_HEALTH_MNG_LIST(_filter, _param, Sorts)
                Else
                    Return rep.GET_HEALTH_MNG_LIST(_filter, _param, ctrlOrg.IsDissolve)
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.HealthMng = rep.GET_HEALTH_MNG_LIST(_filter, rgWelfareMng.CurrentPageIndex, rgWelfareMng.PageSize, MaximumRows, _param, Sorts)
                Else
                    Me.HealthMng = rep.GET_HEALTH_MNG_LIST(_filter, rgWelfareMng.CurrentPageIndex, rgWelfareMng.PageSize, MaximumRows, _param)
                End If

                rgWelfareMng.VirtualItemCount = MaximumRows
                Dim dt = Me.HealthMng
                rgWelfareMng.DataSource = dt

            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Function

#End Region

End Class
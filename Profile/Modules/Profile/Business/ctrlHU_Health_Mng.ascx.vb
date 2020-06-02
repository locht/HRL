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
Imports System.Globalization

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
                                     ToolbarItem.ExportTemplate, ToolbarItem.Import, ToolbarItem.Delete)
            MainToolBar.Items(0).Text = Translate("Xuất Excel")
            MainToolBar.Items(1).Text = Translate("Xuất file mẫu")
            CType(Me.MainToolBar.Items(2), RadToolBarButton).ImageUrl = CType(Me.MainToolBar.Items(0), RadToolBarButton).ImageUrl
            MainToolBar.Items(2).Text = Translate("Nhập file mẫu")
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

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgWelfareMng.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
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
                        rgWelfareMng.Rebind()
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

    Private Sub TableMapping1(ByVal dtTemp As System.Data.DataTable)
        ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
        dtTemp.Columns(0).ColumnName = "STT"
        dtTemp.Columns(1).ColumnName = "EMPLOYEE_CODE"
        dtTemp.Columns(2).ColumnName = "EMPLOYEE_NAME"
        dtTemp.Columns(3).ColumnName = "ORG_NAME"
        dtTemp.Columns(4).ColumnName = "YEAR_KHAMBENH"
        dtTemp.Columns(5).ColumnName = "DATE_KHAMBENH"
        dtTemp.Columns(6).ColumnName = "CAN_NANG"
        dtTemp.Columns(7).ColumnName = "HUYET_AP"
        dtTemp.Columns(8).ColumnName = "NOI_KHOA"
        dtTemp.Columns(9).ColumnName = "NGOAI_KHOA"
        dtTemp.Columns(10).ColumnName = "PHU_KHOA"
        dtTemp.Columns(11).ColumnName = "MAT"
        dtTemp.Columns(12).ColumnName = "TMH"
        dtTemp.Columns(13).ColumnName = "RHM"
        dtTemp.Columns(14).ColumnName = "DA_LIEU"
        dtTemp.Columns(15).ColumnName = "CONG_THUC_MAU"
        dtTemp.Columns(16).ColumnName = "DUONG_MAU"
        dtTemp.Columns(17).ColumnName = "XN_NUOC_TIEU"
        dtTemp.Columns(18).ColumnName = "X_QUANG"
        dtTemp.Columns(19).ColumnName = "DIEN_TIM"
        dtTemp.Columns(20).ColumnName = "SIEU_AM"
        dtTemp.Columns(21).ColumnName = "XN_PHAN"
        dtTemp.Columns(22).ColumnName = "SIEU_VI_A"
        dtTemp.Columns(23).ColumnName = "SIEU_VI_E"
        dtTemp.Columns(24).ColumnName = "SIEU_VI_B"
        dtTemp.Columns(25).ColumnName = "KT_VIEN_GAN_B"
        dtTemp.Columns(26).ColumnName = "CHUC_NANG_GAN"
        dtTemp.Columns(27).ColumnName = "CHUC_NANG_THAN"
        dtTemp.Columns(28).ColumnName = "MO_MAU"
        dtTemp.Columns(29).ColumnName = "KQ1"
        dtTemp.Columns(30).ColumnName = "KQ2"
        dtTemp.Columns(31).ColumnName = "KQ3"
        dtTemp.Columns(32).ColumnName = "KQ4"
        dtTemp.Columns(33).ColumnName = "KQ5"
        dtTemp.Columns(34).ColumnName = "KQ6"
        dtTemp.Columns(35).ColumnName = "KQ7"
        dtTemp.Columns(36).ColumnName = "KQ8"
        dtTemp.Columns(37).ColumnName = "KQ9"
        dtTemp.Columns(38).ColumnName = "KQ10"
        dtTemp.Columns(39).ColumnName = "HEALTH_TYPE"
        dtTemp.Columns(41).ColumnName = "NHOM_BENH"
        dtTemp.Columns(43).ColumnName = "TEN_BENH"
        dtTemp.Columns(44).ColumnName = "KET_LUAB"
        dtTemp.Columns(45).ColumnName = "GHI_CHU"
        dtTemp.Columns(46).ColumnName = "DO_THINH_LUC_SB"
        dtTemp.Columns(47).ColumnName = "DO_THINH_LUC_HC"
        dtTemp.Columns(48).ColumnName = "DO_CN_HO_HAP"
        dtTemp.Columns(49).ColumnName = "XN_HAMLUONG_TOLUEN"
        dtTemp.Columns(50).ColumnName = "BENH_NN1"
        dtTemp.Columns(51).ColumnName = "BENH_NN2"
        dtTemp.Columns(52).ColumnName = "BENH_TN_NN"
        dtTemp.Columns(53).ColumnName = "NGAY_DIEU_TRI"
        dtTemp.Columns(54).ColumnName = "PP_DIEU_TRI"
        dtTemp.Columns(55).ColumnName = "KQ_DIEU_TRI"

        dtTemp.Rows(0).Delete()
        dtTemp.Rows(1).Delete()
        dtTemp.Rows(2).Delete()

        ' add Log
        Dim _error As Boolean = True
        Dim count As Integer
        Dim newRow As DataRow
        Dim empId As Integer
        Dim startDate As Date
        Dim rep As New ProfileBusinessRepository
        Dim result As Integer
        Dim Health_Type As String
        Dim Sick_Group As String
        If dtLogs Is Nothing Then
            dtLogs = New DataTable("data")
            dtLogs.Columns.Add("ID", GetType(Integer))
            dtLogs.Columns.Add("EMPLOYEE_CODE", GetType(String))
            dtLogs.Columns.Add("DISCIPTION", GetType(String))
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

        For Each rows As DataRow In dtTemp.Rows
            If rows.RowState = DataRowState.Deleted OrElse rows.RowState = DataRowState.Detached Then Continue For
            newRow = dtLogs.NewRow
            'newRow("STT") = count + 1
            newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")

            empId = rep.CheckEmployee_Exits(rows("EMPLOYEE_CODE"))

            If empId = 0 Then
                newRow("DISCIPTION") = "Mã nhân viên - Không tồn tại,"
                _error = False
            Else
                rows("EMPLOYEE_CODE") = empId
            End If

            If IsDBNull(rows("DATE_KHAMBENH")) OrElse rows("DATE_KHAMBENH") = "" OrElse CheckDate(rows("DATE_KHAMBENH"), startDate) = False Then
                rows("DATE_KHAMBENH") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Đợt khám - Không đúng định dạng,"
                _error = False
            End If

            If IsDBNull(rows("NGAY_DIEU_TRI")) OrElse rows("NGAY_DIEU_TRI") = "" OrElse CheckDate(rows("NGAY_DIEU_TRI"), startDate) = False Then
                rows("NGAY_DIEU_TRI") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Ngày điều trị - Không đúng định dạng,"
                _error = False
            End If
            Health_Type = rows("HEALTH_TYPE")
            Sick_Group = rows("NHOM_BENH")
            result = rep.CheckChooseComboxFomat_HealthMng(Health_Type, Sick_Group, 0)
            If result = 0 Then
                rows("HEALTH_TYPE") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Không được nhập Loại sức khỏe, "
                _error = False
                result = 1
            End If
            result = rep.CheckChooseComboxFomat_HealthMng(Health_Type, Sick_Group, 1)
            If result = 0 Then
                rows("HEALTH_TYPE") = "NULL"
                newRow("DISCIPTION") = newRow("DISCIPTION") + "Không được nhập Nhóm bệnh "
                _error = False
                result = 1
            End If

            If _error = False Then
                dtLogs.Rows.Add(newRow)
                _error = True
            End If
            count += 1
        Next

        dtTemp.AcceptChanges()
    End Sub

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

    Private Sub Template_ImportHealth_Mng()
        Dim rep As New Profile.ProfileBusinessRepository

        Try
            Dim configPath As String = "~/ReportTemplates//Profile//RPT_HU_HEALTH_MNG.xls"
            'Dim configPath As String = "D:\acv_19\HistaffWebApp\ReportTemplates\Profile\RPT_HU_HEALTH_MNG.xls"
            Dim dsData As DataSet = rep.EXPORT_HEALTH_MNG()

            rep.Dispose()
            If File.Exists(System.IO.Path.Combine(Server.MapPath(configPath))) Then
                'If File.Exists(configPath) Then
                ' ExportTemplate(configPath, dsData, Nothing, "Template_QUANLYSUCKHOE_" & Format(Date.Now, "yyyyMMdd"))
                Using xls As New AsposeExcelCommon
                    Dim bCheck = xls.ExportExcelTemplate(
                      System.IO.Path.Combine(Server.MapPath(configPath)), "Template_QUANLYSUCKHOE_" & Format(Date.Now, "yyyyMMdd"), dsData, Nothing, Response)

                End Using
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

        'Dim filePath As String
        'Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            'templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            'filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName

            'If Not File.Exists(filePath) Then
            '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
            '    Return False
            'End If

            designer = New WorkbookDesigner
            designer.Open(sReportFileName)

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
                Dim objD As New List(Of HealthMngDTO)
                Dim lst As New List(Of Decimal)
                For Each item As GridDataItem In rgWelfareMng.SelectedItems
                    Dim obj As New HealthMngDTO
                    obj.ID = Utilities.ObjToDecima(item.GetDataKeyValue("ID"))
                    objD.Add(obj)
                Next

                If rep.Delete_Health_Mng(objD) Then
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
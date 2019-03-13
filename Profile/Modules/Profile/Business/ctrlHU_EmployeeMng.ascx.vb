Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Common
Imports Telerik.Web.UI
Imports Common.Common
Imports Common.CommonMessage
Imports WebAppLog
Imports System.IO
Imports System.Globalization
Imports Ionic.Zip

Public Class ctrlHU_EmployeeMng
    Inherits Common.CommonView

    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

    Private TYPEREPORT As ArrayList

#Region "Properties"

    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String

#End Region

#Region "Page"
    ''' <summary>
    ''' Hiển thị thông tin trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        'Edit by: ChienNV 
        'Trước khi Load thì kiểm tra PostBack
        If Not IsPostBack Then
            Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
            Try

                Dim startTime As DateTime = DateTime.UtcNow
                ctrlOrganization.AutoPostBack = True
                ctrlOrganization.LoadDataAfterLoaded = True
                ctrlOrganization.OrganizationType = OrganizationType.OrganizationLocation
                ctrlOrganization.CheckBoxes = TreeNodeTypes.None
                Refresh()
                _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                'DisplayException(Me.ViewName, Me.ID, ex)
                _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            End Try
        Else
            Exit Sub
        End If

    End Sub
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo các thiết lập, giá trị các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID
            rgEmployeeList.SetFilter()
            rgEmployeeList.AllowCustomPaging = True
            rgEmployeeList.ClientSettings.EnablePostBackOnRowClick = False
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' khởi tạo các thành phần trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMainToolBar
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Export)
            'Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem("PRINT_CV", ToolbarIcons.Print,
            '                                                        ToolbarAuthorize.Export, Translate("In lý lịch trích ngang")))

            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")

        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    ''' <summary>
    ''' Edit by: ChienNV;
    ''' Fill data in control cboPrintSupport
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Try
            GetDataCombo()
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Message = CommonMessage.ACTION_UPDATED Then
                rgEmployeeList.Rebind()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Event"
    ''' <summary>
    ''' Xử lý sự kiện click khi click btnSearch
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If rdToDate.SelectedDate IsNot Nothing AndAlso rdFromDate.SelectedDate IsNot Nothing Then
                If rdToDate.SelectedDate < rdFromDate.SelectedDate Then
                    ShowMessage("Đến ngày phải lớn hơn Từ ngày", NotifyType.Warning)
                    Exit Sub
                End If
            End If
            rgEmployeeList.CurrentPageIndex = 0
            rgEmployeeList.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Xử lý sự kiện SelectedNodeChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrganization_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrganization.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgEmployeeList.CurrentPageIndex = 0
            rgEmployeeList.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Xử lý sự kiện Command khi click item trên toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim objEmployee As New EmployeeDTO
            Dim rep As New ProfileBusinessRepository
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_DELETE
                    'Kiểm tra các điều kiện để xóa.
                    If rgEmployeeList.SelectedItems.Count = 0 Then
                        ShowMessage(Translate("Bạn phải chọn nhân viên trước khi xóa"), Utilities.NotifyType.Error)
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgEmployeeList.ExportExcel(Server, Response, dtData, "EmployeeList")
                        Else
                            ShowMessage(Translate(MESSAGE_WARNING_EXPORT_EMPTY), Utilities.NotifyType.Warning)
                        End If
                    End Using

                Case "PRINT_CV"
                    Print_CV()

            End Select
            rep.Dispose()
            ' UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Xử lý sự kiện item databound
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgEmployeeList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgEmployeeList.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow.GetDataKeyValue("ORG_DESC"))
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Xử lý sự kiện ButtonCommand Yes/No của ctrlMessageBox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim strError As String = ""

                DeleteEmployee(strError)
                If strError = "" Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                Else
                    ShowMessage(Translate("Nhân viên đã có hợp đồng " & strError.Substring(1, strError.Length - 1) & " không thực hiện được thao tác này."), Utilities.NotifyType.Error)
                End If
                Refresh(ACTION_UPDATED)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <summary>
    ''' Xử lý sự kiện NeedDataSource của rad grid rgEmployeeList
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgEmployeeList.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            CreateDataFilter()

        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' RadGrid_PageIndexChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgEmployeeList.PageIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            CType(sender, RadGrid).CurrentPageIndex = e.NewPageIndex
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' AjaxManager_AjaxRequest
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim url = e.Argument
            If (url.Contains("reload=1")) Then
                rgEmployeeList.CurrentPageIndex = 0
                rgEmployeeList.Rebind()
                If rgEmployeeList.Items IsNot Nothing AndAlso rgEmployeeList.Items.Count > 0 Then
                    rgEmployeeList.Items(0).Selected = True
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Create date:20/10/2017
    ''' Create by: CHIENNV
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrintSupport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintSupport.Click

        Dim validate As New OtherListDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If cboPrintSupport.SelectedValue = "" Then
                ShowMessage(Translate("Bạn chưa chọn biểu mẫu"), NotifyType.Warning)
                Exit Sub
            End If
            Using rep As New ProfileRepository
                validate.ID = cboPrintSupport.SelectedValue
                validate.ACTFLG = "A"
                validate.CODE = "PROFILE_SUPPORT"
                If Not rep.ValidateOtherList(validate) Then
                    ShowMessage(Translate("Biểu mẫu không tồn tại hoặc đã ngừng áp dụng."), NotifyType.Warning)
                    ClearControlValue(cboPrintSupport)
                    GetDataCombo()
                    Exit Sub
                End If
            End Using
            Dim dtData As DataTable
            Dim folderName As String = ""
            Dim filePath As String = ""
            Dim extension As String = ""
            Dim iError As Integer = 0
            Dim strId As String = ""
            For Each item As GridDataItem In rgEmployeeList.SelectedItems
                strId = strId & item.GetDataKeyValue("ID") & ","
            Next
            strId = strId.Substring(0, strId.Length - 1) 'Loai bỏ kí tự , cuối cùng
            ' Kiểm tra + lấy thông tin trong database
            Using rep As New ProfileRepository
                dtData = rep.GetHU_MultyDataDynamic(strId,
                                               ProfileCommon.HU_TEMPLATE_TYPE.PROFILE_SUPPORT_ID,
                                               folderName)
                If dtData.Rows.Count = 0 Then
                    ShowMessage(Translate("Dữ liệu không tồn tại"), NotifyType.Warning)
                    Exit Sub
                End If
                If folderName = "" Then
                    ShowMessage(Translate("Thư mục không tồn tại"), NotifyType.Warning)
                    Exit Sub
                End If
            End Using

            If dtData.Columns.Contains("EMPLOYEE_NAME_EN") Then
                For i As Int32 = 0 To dtData.Rows.Count - 1
                    dtData.Rows(i)("EMPLOYEE_NAME_EN") = Utilities.RemoveUnicode(dtData.Rows(i)("EMPLOYEE_NAME_EN").ToString)
                Next
            End If

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
            If cboPrintSupport.SelectedValue <> 6549 Then
                If rgEmployeeList.SelectedItems.Count = 1 Then
                    Dim item As GridDataItem = rgEmployeeList.SelectedItems(0)
                    Using word As New WordCommon
                        word.ExportMailMerge(filePath,
                                             item.GetDataKeyValue("EMPLOYEE_CODE") & "_" & cboPrintSupport.Text & "_" & Format(Date.Now, "yyyyMMddHHmmss"),
                                             dtData,
                                             Response)
                    End Using
                Else
                    Dim lstFile As List(Of String) = Utilities.SaveMultyFile(dtData, filePath, cboPrintSupport.Text)
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

                        Dim zipName As String = [String].Format("{0}_{1}.zip", cboPrintSupport.Text, DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"))
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
            Else
                Dim dsData As New DataSet

                Dim dtTable As New DataTable

                Dim column1 As DataColumn = New DataColumn("DATE_NOW")
                column1.DataType = System.Type.GetType("System.String")

                dtTable.Columns.Add(column1)
                Dim row As DataRow
                row = dtTable.NewRow()
                row("DATE_NOW") = Date.Now.ToString("dd MMM yyyy", CultureInfo.CreateSpecificCulture("en-US"))
                dtTable.Rows.Add(row)

                dtTable.TableName = "TABLE"
                dsData.Tables.Add(dtTable)
                dtData.TableName = "TABLE1"
                dsData.Tables.Add(dtData)
                Using word As New WordCommon
                    word.ExportMailMerge(filePath,
                                         cboPrintSupport.Text & "_" & Format(Date.Now, "yyyyMMddHHmmss"),
                                         dsData,
                                         Response)
                End Using
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"
    ''' <summary>
    ''' Hàm xử lý tạo dữ liệu filter
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim EmployeeList As List(Of EmployeeDTO)
        Dim MaximumRows As Integer
        Dim Sorts As String
        Dim _filter As New EmployeeDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Using rep As New ProfileBusinessRepository

                If ctrlOrganization.CurrentValue IsNot Nothing Then
                    _filter.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue)
                Else
                    rgEmployeeList.DataSource = New List(Of EmployeeDTO)
                    Exit Function
                End If
                Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrganization.CurrentValue),
                                                .IS_DISSOLVE = ctrlOrganization.IsDissolve}


                SetValueObjectByRadGrid(rgEmployeeList, _filter)

                If rdFromDate.SelectedDate IsNot Nothing Then
                    _filter.FROM_DATE = rdFromDate.SelectedDate
                End If

                If rdToDate.SelectedDate IsNot Nothing Then
                    _filter.TO_DATE = rdToDate.SelectedDate
                End If

                _filter.IS_TER = chkTerminate.Checked
                _filter.GHI_CHU_SUC_KHOE = txtGhiChu.Text
                Sorts = rgEmployeeList.MasterTableView.SortExpressions.GetSortString()
                If isFull Then
                    If Sorts IsNot Nothing Then
                        Return rep.GetListEmployeePaging(_filter, _param, Sorts).ToTable()
                    Else
                        Return rep.GetListEmployeePaging(_filter, _param).ToTable()
                    End If
                Else
                    If Sorts IsNot Nothing Then
                        EmployeeList = rep.GetListEmployeePaging(_filter, rgEmployeeList.CurrentPageIndex, rgEmployeeList.PageSize, MaximumRows, _param, Sorts)
                    Else
                        EmployeeList = rep.GetListEmployeePaging(_filter, rgEmployeeList.CurrentPageIndex, rgEmployeeList.PageSize, MaximumRows, _param)
                    End If

                    rgEmployeeList.VirtualItemCount = MaximumRows
                    rgEmployeeList.DataSource = EmployeeList
                End If


            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
    ''' <summary>
    ''' Xử lý sự kiện xóa nhân viên
    ''' </summary>
    ''' <param name="strError"></param>
    ''' <remarks></remarks>
    Private Sub DeleteEmployee(ByRef strError As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New ProfileBusinessRepository

            'Kiểm tra các điều kiện trước khi xóa
            Dim lstEmpID As New List(Of Decimal)
            For Each dr As Telerik.Web.UI.GridDataItem In rgEmployeeList.SelectedItems
                lstEmpID.Add(dr.GetDataKeyValue("ID"))
            Next
            'Xóa nhân viên.
            rep.DeleteEmployee(lstEmpID, strError)
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Xử lý action in cv
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Print_CV()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim dsData As DataSet
            Dim rp As New ProfileRepository
            Dim sPath As String = ""

            If rgEmployeeList.SelectedItems.Count = 0 Then
                ShowMessage(Translate("Bạn phải chọn nhân viên trước khi in ."), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            If rgEmployeeList.SelectedItems.Count > 1 Then
                ShowMessage(Translate("Chỉ được chọn 1 bản ghi để in ."), Utilities.NotifyType.Warning)
                Exit Sub
            End If

            'Kiểm tra các điều kiện trước khi in
            Dim lstEmpID As String = ""
            Dim srtImage As String = ""

            For Each dr As Telerik.Web.UI.GridDataItem In rgEmployeeList.SelectedItems
                lstEmpID = dr.GetDataKeyValue("ID")
                srtImage = dr.GetDataKeyValue("IMAGE")
            Next

            Dim rep As New ProfileBusinessRepository

            dsData = rp.GetEmployeeCVByID("PKG_PROFILE_REPORT.HU0150", lstEmpID)
            dsData.Tables(0).TableName = "DATA"
            dsData.Tables(1).TableName = "DATA1" ' quá trình đào tạo ngoài tập đoàn ( hiện tại chưa có)
            dsData.Tables(2).TableName = "DATA2" ' qua trinh cong tac tai tap doan
            dsData.Tables(3).TableName = "DATA3" ' quá trình công tác trước khi vào tập đoàn
            dsData.Tables(4).TableName = "DATA4" ' quá trình đóng bh trước khi vào tập đoàn
            dsData.Tables(5).TableName = "DATA5" ' quá trình đóng bh tại tập đoàn

            dsData.Tables(6).TableName = "DATA6" ' quá trình khen thưởng
            dsData.Tables(7).TableName = "DATA7" ' quan hệ nhân thân

            dsData.Tables(8).TableName = "DATA8" ' thông tin nhân viên

            dsData.Tables(9).TableName = "DATA9" ' quá trình kỷ luật
            dsData.Tables(10).TableName = "DATA10" ' có thể BHXH hay không
            dsData.Tables(11).TableName = "DATA12" ' tình trạng hôn nhân
            dsData.Tables(12).TableName = "DATA13" ' QUÁ TRÌNH THAY ĐỔI LUONG VÀ PHỤ CẤP
            sPath = Server.MapPath("~/ReportTemplates/Profile/Report/InCV.xls")
            'sPath = Server.MapPath("~/ReportTemplates/Profile/Report/InCV.docx")


            Dim arrPicture() As Byte
            Dim sError As String = ""

            If srtImage <> "" Then
                arrPicture = rep.GetEmployeeImage(lstEmpID, sError) 'Lấy ảnh của nhân viên.
            Else
                arrPicture = rep.GetEmployeeImage(0, sError) 'Lấy ảnh mặc định (NoImage.jpg)
            End If


            Using xls As New ExcelCommon
                xls.ExportExcelTemplate(sPath,
                    "InCV", arrPicture, dsData, Nothing, Response, "", ExcelCommon.ExportType.Excel)
            End Using
            rep.Dispose()
            '' Export file mẫu
            'Using word As New WordCommon
            '    word.ExportMailMerge(sPath, "InCV", dsData, arrPicture, Response)
            'End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub GetDataCombo()
        Dim dtData As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Using rep As New ProfileRepository
                dtData = rep.GetOtherList("PROFILE_SUPPORT")
                FillRadCombobox(cboPrintSupport, dtData, "NAME", "ID")
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region


End Class
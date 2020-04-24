Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Common
Imports Telerik.Web.UI
Imports ICSharpCode
Imports ICSharpCode.SharpZipLib.Zip
Imports System
Imports System.IO
Imports Aspose.Words
Imports Aspose.Words.Reporting
'Imports Ionic.Crc
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.IO.Compression
Imports WebAppLog
Imports HistaffWebAppResources.My.Resources
Imports Aspose.Cells
Imports System.Globalization

Public Class ctrlHU_Contract
    Inherits Common.CommonView

    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()


#Region "Property"
    ''' <summary>
    ''' Obj ContractDTO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Contract As ContractDTO
        Get
            Return ViewState(Me.ID & "_Contract")
        End Get
        Set(ByVal value As ContractDTO)
            ViewState(Me.ID & "_Contract") = value
        End Set
    End Property
    ''' <summary>
    ''' List obj Contracts
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Contracts As List(Of ContractDTO)
        Get
            Return ViewState(Me.ID & "_Contracts")
        End Get
        Set(ByVal value As List(Of ContractDTO))
            ViewState(Me.ID & "_Contracts") = value
        End Set
    End Property
    ''' <summary>
    ''' Insert ContractDTO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InsertContracts As ContractDTO
        Get
            Return ViewState(Me.ID & "_InsertContracts")
        End Get
        Set(ByVal value As ContractDTO)
            ViewState(Me.ID & "_InsertContracts") = value
        End Set
    End Property
    ''' <summary>
    ''' Delete ContractDTO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DeleteContract As ContractDTO
        Get
            Return ViewState(Me.ID & "_DeleteContract")
        End Get
        Set(ByVal value As ContractDTO)
            ViewState(Me.ID & "_DeleteContract") = value
        End Set
    End Property

    ''' <summary>
    '''  Kiem tra trang thai update
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsUpdate")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsUpdate") = value
        End Set
    End Property
    ''' <summary>
    ''' Gia tri _IDSelect
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

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("FULLNAME_VN", GetType(String))
                dt.Columns.Add("WORK_UNIT", GetType(String))
                dt.Columns.Add("TITLE", GetType(String))
                dt.Columns.Add("CONTRACT_TYPE", GetType(String))
                dt.Columns.Add("CONTRACT_TYPE_ID", GetType(Decimal))
                dt.Columns.Add("WORK_FORM", GetType(String))
                dt.Columns.Add("WORK_FORM_ID", GetType(Decimal))
                dt.Columns.Add("SIGN_CONTRACT", GetType(String))
                dt.Columns.Add("SIGN_CONTRACT_ID", GetType(Decimal))
                dt.Columns.Add("START_DATE", GetType(String))
                dt.Columns.Add("END_DATE", GetType(String))
                dt.Columns.Add("EFFECT_SALARY_DATE", GetType(String))
                dt.Columns.Add("SALARY_RECORD_ID", GetType(Decimal))
                dt.Columns.Add("START_TIME_MORNING", GetType(String))
                dt.Columns.Add("END_TIME_MORNING", GetType(String))
                dt.Columns.Add("START_TIME_AFTERNOON", GetType(String))
                dt.Columns.Add("END_TIME_AFTERNOON", GetType(String))
                dt.Columns.Add("RISK_COMPENSATION", GetType(String))
                dt.Columns.Add("AUTHORITY", GetType(Decimal))
                dt.Columns.Add("AUTHORITY_NO", GetType(String))
                dt.Columns.Add("WORK_TO_DO", GetType(String))
                dt.Columns.Add("SIGN_DATE", GetType(String))
                'dt.Columns.Add("SIGN_EMP_CODE", GetType(String))
                dt.Columns.Add("SIGN_EMP_NAME", GetType(String))
                dt.Columns.Add("SIGN_EMP_ID", GetType(String))
                dt.Columns.Add("NOTE", GetType(String))
                'dt.Columns.Add("MAP", GetType(String))
                
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    Property dtDataImportWorking As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataImportWorking")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataImportWorking") = value
        End Set
    End Property

#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 06/07/2017 18:11
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
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
    ''' 06/07/2017 18:11
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Xét các trạng thái của grid rgContract
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            rgContract.AllowCustomPaging = True
            rgContract.SetFilter()
            InitControl()
            If Not IsPostBack Then
                ViewConfig(RadPane1)
                GirdConfig(rgContract)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 18:11
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức BindData
    ''' Gọi phương thức load dữ liệu cho các combobox
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
    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Ham khoi taso toolbar voi cac button them moi, sua, xuat file, xoa, in hop dong
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarContracts

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit,
                                       ToolbarItem.Export,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Print,
                                       ToolbarItem.Refresh,
                                       ToolbarItem.Next,
                                       ToolbarItem.Import)


            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_CREATE_BATCH,
                                                                  ToolbarIcons.Add,
                                                                  ToolbarAuthorize.Special1,
                                                                  "Phê duyệt hàng loạt"))

            'CType(MainToolBar.Items(4), RadToolBarButton).Text = "In hợp đồng"
            CType(MainToolBar.Items(5), RadToolBarButton).Text = Translate("Thanh lý hợp đồng")
            CType(MainToolBar.Items(5), RadToolBarButton).ImageUrl = CType(MainToolBar.Items(1), RadToolBarButton).ImageUrl

            CType(Me.MainToolBar.Items(6), RadToolBarButton).Text = Translate("Xuất file mẫu")
            CType(Me.MainToolBar.Items(6), RadToolBarButton).ImageUrl = CType(Me.MainToolBar.Items(2), RadToolBarButton).ImageUrl
            CType(Me.MainToolBar.Items(7), RadToolBarButton).Text = Translate("Nhập file mẫu")
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Xet cac tuy chon gia tri cua Message la UpdateView, InsertView
    ''' Bind lai du lieu cho grid rgContract
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim rep As New ProfileBusinessRepository
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation

            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                If Session("Result") = "1" Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Session("Result") = Nothing
                End If
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
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command khi click item tren toolbar
    ''' Command in hop dong, xoa hop dong, xuat file excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objOrgFunction As New OrganizationDTO
        Dim sError As String = ""
        Dim status As Integer
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_NEXT
                    Template_ImportContract()
                    'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_ImportHoSoLuong');", True)
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
                Case CommonMessage.TOOLBARITEM_PRINT
                    Dim dtData As DataTable
                    Dim dtDataCon As DataTable
                    Dim sourcePath = Server.MapPath("~/ReportTemplates/Profile/LocationInfo/")
                    Dim folderName As String = "ContractSupport"
                    Dim filePath As String = ""
                    Dim extension As String = ""
                    Dim iError As Integer = 0
                    Dim path As String = ""
                    If Not Directory.Exists(AppDomain.CurrentDomain.BaseDirectory & "Zip\") Then
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory & "Zip\")
                    End If
                    Dim dir As String() = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory & "Zip\")
                    If dir.Length > 0 Then
                        For Each f As String In dir
                            Try
                                File.Delete(f)
                            Catch ex As Exception
                            End Try
                        Next
                    End If
                    If Not Directory.Exists(AppDomain.CurrentDomain.BaseDirectory & "Files\") Then
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory & "Files\")
                    End If
                    Dim dir2 As String() = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory & "Files\")
                    If dir2.Length > 0 Then
                        For Each f2 As String In dir2
                            Try
                                File.Delete(f2)
                            Catch ex As Exception
                            End Try
                        Next
                    End If

                    Dim item = rgContract.SelectedItems

                    Dim lstIDs As String = ""
                    'For idx = 0 To rgContract.SelectedItems.Count - 1
                    '    If idx <> rgContract.SelectedItems.Count - 1 Then
                    '        Dim value As GridDataItem = rgContract.SelectedItems(idx)
                    '        lstIDs = lstIDs & value.GetDataKeyValue("ID") & ","
                    '    Else
                    '        Dim value As GridDataItem = rgContract.SelectedItems(idx)
                    '        lstIDs = lstIDs & value.GetDataKeyValue("ID")
                    '    End If
                    'Next
                    If item.Count > 1 Then
                        ShowMessage(Translate("Vui lòng chọn chỉ một bản ghi"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If item.Count > 0 Then
                        Dim value As GridDataItem = item(0)
                        lstIDs = lstIDs & value.GetDataKeyValue("ID")
                    End If
                    'check loại hợp đồng cùng loại
                    Using rep As New ProfileRepository
                        dtDataCon = rep.GetCheckContractTypeID(lstIDs)
                        If dtDataCon.Rows(0)(0) = 2 Then
                            ShowMessage("Các bản ghi không cùng loại hợp đồng !", NotifyType.Warning)
                            Exit Sub
                        End If
                        If folderName = "" Then
                            ShowMessage("Thư mục không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

                    Dim icheck As GridDataItem = item.Item(0)

                    ' Kiểm tra file theo thông tin trong database
                    If Not Utilities.GetTemplateLinkFile(icheck.GetDataKeyValue("CONTRACTTYPE_CODE"),
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

                    Dim lstID As String = ""
                    For Each i As GridDataItem In item
                        lstID &= "|" & i.GetDataKeyValue("ID").ToString() & "|"
                    Next

                    Using rep As New ProfileRepository
                        dtData = rep.GetHU_DataDynamicContract(lstID, ProfileCommon.HU_TEMPLATE_TYPE.CONTRACT_ID, folderName)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage("Dữ liệu không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                        If folderName = "" Then
                            ShowMessage("Thư mục không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                        'If dtData.Rows(0)("ORG_CODE2") = "TNE&C SG" Then
                        '    ShowMessage("Không thể in hợp đồng có đơn vị TNE&C SG", NotifyType.Warning)
                        '    Exit Sub
                        'End If
                    End Using

                    If item.Count = 1 Then
                        'Export file mẫu
                        Using word As New WordCommon
                            word.ExportMailMerge(filePath,
                                                 icheck.GetDataKeyValue("EMPLOYEE_CODE") & icheck.GetDataKeyValue("CONTRACTTYPE_CODE") & _
                                                 Format(Date.Now, "yyyyMMddHHmmss") & extension,
                                                 dtData,
                                                 sourcePath,
                                                 Response)
                        End Using
                    Else
                        Dim item1 As GridDataItem = rgContract.SelectedItems(0)
                        Dim fileName As String = item1.GetDataKeyValue("EMPLOYEE_CODE") & icheck.GetDataKeyValue("CONTRACTTYPE_CODE") & _
                                                 Format(Date.Now, "yyyyMMddHHmmss") & 0 & extension
                        Dim doc As New Document(filePath)
                        doc.MailMerge.Execute(dtData.Rows(0))
                        path = AppDomain.CurrentDomain.BaseDirectory & "Files\"
                        'path = "Files\"
                        If Not Directory.Exists(path) Then
                            Directory.CreateDirectory(path)
                        End If
                        doc.Save(path & fileName)
                        ZipFiles(path)
                    End If

                Case CommonMessage.TOOLBARITEM_NEXT
                    Dim dtData As DataTable
                    Dim dtDataCon As DataTable
                    Dim folderName As String = "ContractSupport"
                    Dim filePath As String = ""
                    Dim extension As String = ""
                    Dim iError As Integer = 0
                    Dim path As String = ""
                    If Not Directory.Exists(AppDomain.CurrentDomain.BaseDirectory & "Zip\") Then
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory & "Zip\")
                    End If
                    Dim dir As String() = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory & "Zip\")
                    If dir.Length > 0 Then
                        For Each f As String In dir
                            Try
                                File.Delete(f)
                            Catch ex As Exception
                            End Try
                        Next
                    End If
                    If Not Directory.Exists(AppDomain.CurrentDomain.BaseDirectory & "Files\") Then
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory & "Files\")
                    End If
                    Dim dir2 As String() = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory & "Files\")
                    If dir2.Length > 0 Then
                        For Each f2 As String In dir2
                            Try
                                File.Delete(f2)
                            Catch ex As Exception
                            End Try
                        Next
                    End If

                    Dim item = rgContract.SelectedItems

                    Dim lstIDs As String = ""
                    For idx = 0 To rgContract.SelectedItems.Count - 1
                        If idx <> rgContract.SelectedItems.Count - 1 Then
                            Dim value As GridDataItem = rgContract.SelectedItems(idx)
                            lstIDs = lstIDs & value.GetDataKeyValue("ID") & ","
                        Else
                            Dim value As GridDataItem = rgContract.SelectedItems(idx)
                            lstIDs = lstIDs & value.GetDataKeyValue("ID")
                        End If
                    Next
                    'check loại hợp đồng cùng loại
                    Using rep As New ProfileRepository
                        dtDataCon = rep.GetCheckContractTypeID(lstIDs)
                        If dtDataCon.Rows(0)(0) = 2 Then
                            ShowMessage("Các bản ghi không cùng loại hợp đồng !", NotifyType.Warning)
                            Exit Sub
                        End If
                        If folderName = "" Then
                            ShowMessage("Thư mục không tồn tại", NotifyType.Warning)
                            Exit Sub
                        End If
                    End Using

                    Dim icheck As GridDataItem = item.Item(0)
                    ' Kiểm tra file theo thông tin trong database
                    If Not Utilities.GetTemplateLinkFile("TMF_Offer_Letter_Revised",
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

                    Dim lstID As String = ""
                    For Each i As GridDataItem In item
                        lstID &= "|" & i.GetDataKeyValue("ID").ToString() & "|"
                    Next

                    Using rep As New ProfileRepository
                        dtData = rep.GetHU_DataDynamicContract(lstID,
                                                       ProfileCommon.HU_TEMPLATE_TYPE.CONTRACT_ID,
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

                    If item.Count = 1 Then
                        'Export file mẫu
                        Using word As New WordCommon
                            word.ExportMailMerge(filePath,
                                                 icheck.GetDataKeyValue("EMPLOYEE_CODE") & "_HDLD_" & _
                                                 Format(Date.Now, "yyyyMMddHHmmss") & extension,
                                                 dtData,
                                                 Response)
                        End Using
                    Else
                        For lst = 0 To rgContract.SelectedItems.Count - 1
                            Dim item1 As GridDataItem = rgContract.SelectedItems(lst)
                            Dim fileName As String = item1.GetDataKeyValue("EMPLOYEE_CODE") & "_HDLD_" & _
                                                     Format(Date.Now, "yyyyMMddHHmmss") & lst & extension
                            Dim doc As New Document(filePath)
                            doc.MailMerge.Execute(dtData.Rows(lst))
                            path = AppDomain.CurrentDomain.BaseDirectory & "Files\"
                            'path = "Files\"
                            If Not Directory.Exists(path) Then
                                Directory.CreateDirectory(path)
                            End If
                            doc.Save(path & fileName)
                        Next
                        ZipFiles(path)
                    End If
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgContract.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    ElseIf rgContract.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim item As GridDataItem = rgContract.SelectedItems(0)
                    If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                        ShowMessage(Translate("Bản ghi đã phê duyệt. Thao tác thực hiện không thành công"), NotifyType.Warning)
                        Exit Sub
                    End If
                    If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.NOT_APPROVE_ID Then
                        ShowMessage(Translate("Bản ghi không phê duyệt. Thao tác thực hiện không thành công"), NotifyType.Warning)
                        Exit Sub
                    End If

                    DeleteContract = New ContractDTO With {.ID = Decimal.Parse(item("ID").Text),
                                                           .EMPLOYEE_ID = Decimal.Parse(item("EMPLOYEE_ID").Text)}
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
                            rgContract.ExportExcel(Server, Response, dtData, "Title")
                            Exit Sub
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_CREATE_BATCH
                    BatchApproveContract()
                Case CommonMessage.TOOLBARITEM_REFRESH
                    If rgContract.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgContract.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim item As GridDataItem = rgContract.SelectedItems(0)
                    status = If(item.GetDataKeyValue("STATUS_ID").ToString() = "", 0, Decimal.Parse(item.GetDataKeyValue("STATUS_ID").ToString()))
                    If status <> 447 Then
                        ShowMessage("Hợp đồng chưa phê duyệt, vui lòng kiểm tra lại", NotifyType.Warning)
                        Exit Sub
                    End If
                    'ctrlMessageBox.MessageText = "Bạn thực sự muốn thanh lý hợp đồng này ?"
                    'ctrlMessageBox.MessageTitle = item.GetDataKeyValue("EMPLOYEE_CODE").ToString
                    'ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_REFRESH
                    'ctrlMessageBox.DataBind()
                    'ctrlMessageBox.Show()
            End Select

            'UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command button cua control ctrkMessageBox
    ''' Cap nhat trang thai cua cac control
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
            ' Mở form thanh lý hợp đồng
            If e.ActionName = CommonMessage.TOOLBARITEM_REFRESH And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim str As String = "Liquidation_Click();"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                'Dim item As GridDataItem = rgContract.SelectedItems(0)
                'Dim employeeid As String = item.GetDataKeyValue("EMPLOYEE_ID").ToString
                'Dim id As String = item.GetDataKeyValue("ID").ToString
                'Response.Redirect("Dialog.aspx?mid=Profile&fid=ctrlContract_Liquidate&group=Business&noscroll=1&empid=" + employeeid + "&idCT=" + id)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien SelectedNodeChanged cua control ctrlOrg
    ''' Xet lai cac thiet lap trang thai cho grid rgContract
    ''' Bind lai du lieu cho rgContract
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgContract.CurrentPageIndex = 0
            rgContract.MasterTableView.SortExpressions.Clear()
            rgContract.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cua button btnSearch
    ''' Thiets lap trang thai cho rgContract
    ''' Bind lai du lieu cho rgContract
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgContract.CurrentPageIndex = 0
            rgContract.MasterTableView.SortExpressions.Clear()
            rgContract.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien NeedDataSource cho rad grid 
    ''' Tao du lieu filter
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgContract.NeedDataSource
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
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien Click cua btnPrintSupport
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub btnPrintSupport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintSupport.Click
    '    Dim validate As New OtherListDTO
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        If cboPrintSupport.SelectedValue = "" Then
    '            ShowMessage(Translate("Bạn chưa chọn biểu mẫu"), NotifyType.Warning)
    '            Exit Sub
    '        End If
    '        Using rep As New ProfileRepository
    '            validate.ID = cboPrintSupport.SelectedValue
    '            validate.ACTFLG = "A"
    '            validate.CODE = ProfileCommon.CONTRACT_SUPPORT.Code
    '            If Not rep.ValidateOtherList(validate) Then
    '                ShowMessage(Translate("Biểu mẫu không tồn tại hoặc đã ngừng áp dụng."), NotifyType.Warning)
    '                ClearControlValue(cboPrintSupport)
    '                GetDataCombo()
    '                Exit Sub
    '            End If
    '        End Using
    '        Dim dtData As DataTable
    '        Dim folderName As String = ""
    '        Dim filePath As String = ""
    '        Dim extension As String = ""
    '        Dim iError As Integer = 0
    '        Dim strId As String = ""
    '        For Each item As GridDataItem In rgContract.SelectedItems
    '            strId = strId & item.GetDataKeyValue("ID") & ","
    '        Next
    '        strId = strId.Substring(0, strId.Length - 1) 'Loai bỏ kí tự , cuối cùng
    '        ' Kiểm tra + lấy thông tin trong database
    '        Using rep As New ProfileRepository
    '            dtData = rep.GetHU_MultyDataDynamic(strId,
    '                                           ProfileCommon.HU_TEMPLATE_TYPE.CONTRACT_SUPPORT_ID,
    '                                           folderName)
    '            If dtData.Rows.Count = 0 Then
    '                ShowMessage(Translate("Dữ liệu không tồn tại"), NotifyType.Warning)
    '                Exit Sub
    '            End If
    '            If folderName = "" Then
    '                ShowMessage(Translate("Thư mục không tồn tại"), NotifyType.Warning)
    '                Exit Sub
    '            End If
    '        End Using

    '        If dtData.Columns.Contains("EMPLOYEE_NAME_EN") Then
    '            For i As Int32 = 0 To dtData.Rows.Count - 1
    '                dtData.Rows(i)("EMPLOYEE_NAME_EN") = Utilities.RemoveUnicode(dtData.Rows(i)("EMPLOYEE_NAME_EN").ToString)
    '            Next
    '        End If

    '        ' Kiểm tra file theo thông tin trong database
    '        If Not Utilities.GetTemplateLinkFile(cboPrintSupport.SelectedValue,
    '                                             folderName,
    '                                             filePath,
    '                                             extension,
    '                                             iError) Then
    '            Select Case iError
    '                Case 1
    '                    ShowMessage("Biểu mẫu không tồn tại", NotifyType.Warning)
    '                    Exit Sub
    '            End Select
    '        End If
    '        ' Export file mẫu
    '        If rgContract.SelectedItems.Count = 1 Then
    '            Dim item As GridDataItem = rgContract.SelectedItems(0)
    '            Using word As New WordCommon
    '                word.ExportMailMerge(filePath,
    '                                     item.GetDataKeyValue("EMPLOYEE_CODE") & "_" & cboPrintSupport.Text & "_" & Format(Date.Now, "yyyyMMddHHmmss"),
    '                                     dtData,
    '                                     Response)
    '            End Using
    '        Else
    '            Dim lstFile As List(Of String) = Utilities.SaveMultyFile(dtData, filePath, cboPrintSupport.Text)
    '            Using zip As New ZipFile
    '                zip.AlternateEncodingUsage = ZipOption.AsNecessary
    '                zip.AddDirectoryByName("Files")
    '                For i As Integer = 0 To lstFile.Count - 1
    '                    Dim file As System.IO.FileInfo = New System.IO.FileInfo(lstFile(i))
    '                    If file.Exists Then
    '                        zip.AddFile(file.FullName, "Files")
    '                    End If
    '                Next
    '                Response.Clear()

    '                Dim zipName As String = [String].Format("{0}_{1}.zip", cboPrintSupport.Text, DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"))
    '                Response.ContentType = "application/zip"
    '                Response.AddHeader("content-disposition", "attachment; filename=" + zipName)
    '                zip.Save(Response.OutputStream)
    '                Response.Flush()
    '                Response.SuppressContent = True
    '                HttpContext.Current.ApplicationInstance.CompleteRequest()
    '            End Using
    '            For i As Integer = 0 To lstFile.Count - 1
    '                'Delete files
    '                Dim file As System.IO.FileInfo = New System.IO.FileInfo(lstFile(i))
    '                If file.Exists Then
    '                    file.Delete()
    '                End If
    '            Next
    '        End If

    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    ' ''' <lastupdate>
    ' ''' 07/07/2017 08:24
    ' ''' </lastupdate>
    ' ''' <summary>
    ' ''' Phuong thuc xu ly viec zip file vao folder Zip
    ' ''' </summary>
    ' ''' <param name="path"></param>
    ' ''' <remarks></remarks>
    Private Sub ZipFiles(ByVal path As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim crc As New Crc32()

            Dim s As New ZipOutputStream(File.Create(AppDomain.CurrentDomain.BaseDirectory & "Zip\DocumentFolder.zip"))
            s.SetLevel(0)
            ' 0 - store only to 9 - means best compression
            For i As Integer = 0 To Directory.GetFiles(path).Length - 1
                ' Must use a relative path here so that files show up in the Windows Zip File Viewer
                ' .. hence the use of Path.GetFileName(...)
                Dim entry As New ZipEntry(Directory.GetFiles(path)(i))
                entry.DateTime = DateTime.Now

                ' Read in the 
                Using fs As FileStream = File.Open(Directory.GetFiles(path)(i), FileMode.Open)
                    Dim buffer As Byte() = New Byte(fs.Length - 1) {}
                    fs.Read(buffer, 0, buffer.Length)
                    entry.Size = fs.Length
                    fs.Close()
                    crc.Reset()
                    crc.Update(buffer)
                    entry.Crc = crc.Value
                    s.PutNextEntry(entry)
                    s.Write(buffer, 0, buffer.Length)
                End Using
            Next
            s.Finish()
            s.Close()

            Using FileStream = File.Open(AppDomain.CurrentDomain.BaseDirectory & "Zip\DocumentFolder.zip", FileMode.Open)
                Dim buffer As Byte() = New Byte(FileStream.Length - 1) {}
                FileStream.Read(buffer, 0, buffer.Length)
                Dim rEx As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9_\-\.]+")
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" + rEx.Replace("DocumentFolder.zip", "_"))
                Response.AddHeader("Content-Length", FileStream.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.BinaryWrite(buffer)
                FileStream.Close()
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet
        Dim dtDataALL As DataTable
        Using rep As New ProfileRepository
            dtDataALL = rep.GetHU_AllowanceList()
        End Using
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload1.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            TableMapping(dsDataPrepare.Tables(0))
            For Each rows As DataRow In dsDataPrepare.Tables(0).Select("EMPLOYEE_CODE<>'""'").CopyToDataTable.Rows
                If IsDBNull(rows("EMPLOYEE_CODE")) OrElse rows("EMPLOYEE_CODE") = "" Then Continue For
                'Dim repFactor As String = rows("FACTORSALARY").ToString.Trim.Replace(",", ".")
                Dim newRow As DataRow = dtData.NewRow
                newRow("EMPLOYEE_CODE") = rows("EMPLOYEE_CODE")
                newRow("FULLNAME_VN") = rows("FULLNAME_VN")
                newRow("WORK_UNIT") = rows("WORK_UNIT")
                newRow("TITLE") = rows("TITLE")
                newRow("CONTRACT_TYPE") = rows("CONTRACT_TYPE")
                newRow("CONTRACT_TYPE_ID") = If(IsNumeric(rows("CONTRACT_TYPE_ID")), Decimal.Parse(rows("CONTRACT_TYPE_ID")), 0)
                newRow("WORK_FORM") = rows("WORK_FORM")
                newRow("WORK_FORM_ID") = If(IsNumeric(rows("WORK_FORM_ID")), Decimal.Parse(rows("WORK_FORM_ID")), 0)
                newRow("SIGN_CONTRACT") = rows("SIGN_CONTRACT")
                newRow("SIGN_CONTRACT_ID") = If(IsNumeric(rows("SIGN_CONTRACT_ID")), Decimal.Parse(rows("SIGN_CONTRACT_ID")), 0)
                newRow("START_DATE") = rows("START_DATE")
                newRow("END_DATE") = rows("END_DATE")
                newRow("EFFECT_SALARY_DATE") = rows("EFFECT_SALARY_DATE")
                newRow("SALARY_RECORD_ID") = If(IsNumeric(rows("SALARY_RECORD_ID")), Decimal.Parse(rows("SALARY_RECORD_ID")), 0)
                
                newRow("START_TIME_MORNING") = rows("START_TIME_MORNING")
                newRow("END_TIME_MORNING") = rows("END_TIME_MORNING")
                newRow("START_TIME_AFTERNOON") = rows("START_TIME_AFTERNOON")
                newRow("END_TIME_AFTERNOON") = rows("END_TIME_AFTERNOON")

                newRow("RISK_COMPENSATION") = rows("RISK_COMPENSATION")
                newRow("AUTHORITY") = If(IsNumeric(rows("AUTHORITY")), Decimal.Parse(rows("AUTHORITY")), 0)
                newRow("AUTHORITY_NO") = rows("AUTHORITY_NO")
                newRow("WORK_TO_DO") = rows("WORK_TO_DO")
                newRow("SIGN_DATE") = rows("SIGN_DATE")
                newRow("SIGN_EMP_NAME") = rows("SIGN_EMP_NAME")
                newRow("SIGN_EMP_ID") = rows("SIGN_EMP_ID")
                newRow("NOTE") = rows("NOTE")
                'newRow("MAP") = rows("MAP")

                
                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim sp As New ProfileStoreProcedure()
                If sp.Import_Contract(LogHelper.GetUserLog().Username.ToUpper, DocXml) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
                'End edit;
                rgContract.Rebind()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


#End Region

#Region "Custom"
    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc cap nhat trang thai cac control trong page
    ''' Cap nhat trang thai tbarContracts, rgContract, ctrlOrg
    ''' Cap nhat trang thai hien tai, neu trang thai hien tai cua trang la trang thai xoa
    ''' thi xoa hop dong vaf lam moi lai UpdateView
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            tbarContracts.Enabled = True
            rgContract.Enabled = True
            ctrlOrg.Enabled = True
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
                    If rep.DeleteContract(DeleteContract) Then
                        DeleteContract = Nothing
                        IDSelect = Nothing
                        Refresh("UpdateView")
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
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc fill du lieu cho cac combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        'Dim dtData As DataTable
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            'Using rep As New ProfileRepository
            '    dtData = rep.GetOtherList("CONTRACT_SUPPORT")
            '    FillRadCombobox(cboPrintSupport, dtData, "NAME", "ID")
            'End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/07/2017 08:24
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc tao du lieu filter
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileBusinessRepository
        Dim _filter As New ContractDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If ctrlOrg.CurrentValue Is Nothing Then
                rgContract.DataSource = New List(Of ContractDTO)
                Exit Function
            End If
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}

            If rdFromDate.SelectedDate IsNot Nothing Then
                _filter.FROM_DATE = rdFromDate.SelectedDate
            End If
            If rdToDate.SelectedDate IsNot Nothing Then
                _filter.TO_DATE = rdToDate.SelectedDate
            End If
            _filter.IS_TER = chkTerminate.Checked
            SetValueObjectByRadGrid(rgContract, _filter)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgContract.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetContract(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetContract(_filter, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.Contracts = rep.GetContract(_filter, rgContract.CurrentPageIndex, rgContract.PageSize, MaximumRows, _param, Sorts)
                Else
                    Me.Contracts = rep.GetContract(_filter, rgContract.CurrentPageIndex, rgContract.PageSize, MaximumRows, _param)
                End If

                rgContract.VirtualItemCount = MaximumRows
                rgContract.DataSource = Me.Contracts
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <lastupdate>11/07/2017</lastupdate>
    ''' <summary>Xử lý phê duyệt hop dong</summary>
    ''' <remarks></remarks>
    Private Sub BatchApproveContract()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim lstID As New List(Of Decimal)

        Try
            '1. Check có rows nào được select hay không
            If rgContract Is Nothing OrElse rgContract.SelectedItems.Count <= 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                Exit Sub
            End If

            Dim listCon As New List(Of ContractDTO)

            For Each dr As Telerik.Web.UI.GridDataItem In rgContract.SelectedItems
                Dim ID As New Decimal
                If Not dr.GetDataKeyValue("STATUS_ID").Equals("447") Then
                    ID = dr.GetDataKeyValue("ID")
                    lstID.Add(ID)
                End If
            Next
            If lstID.Count > 0 Then
                'Dim bCheckHasfile = rep.CheckHasFileContract(lstID)
                For Each item As GridDataItem In rgContract.SelectedItems
                    If item.GetDataKeyValue("STATUS_ID") = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                        ShowMessage(Translate("Bản ghi đã phê duyệt."), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                'If bCheckHasfile = 1 Then
                '    ShowMessage(Translate("Duyệt khi tất cả các record đã có tập tin đính kèm,bạn kiểm tra lại"), NotifyType.Warning)
                '    Exit Sub
                'End If
                If rep.ApproveListContract(lstID) Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgContract.Rebind()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            Else
                ShowMessage("Các hợp đồng được chọn đã được phê duyệt", NotifyType.Information)
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub Template_ImportContract()
        Dim rep As New Profile.ProfileBusinessRepository
        Try
            'D:\ACV\acv_19\HistaffWebApp\ReportTemplates\Profile\Import
            Dim configPath As String = Server.MapPath("ReportTemplates\Profile\Import\import_hopdong.xls")
            'ApproveListContract

            Dim dsData As DataSet = rep.GetContractImport()
            dsData.Tables(0).TableName = "Table"
            dsData.Tables(1).TableName = "Table1"
            dsData.Tables(2).TableName = "Table2"
            dsData.Tables(3).TableName = "Table3"
            'dsData.Tables(4).TableName = "Table4"
            'dsData.Tables(5).TableName = "Table5"
            rep.Dispose()

            If File.Exists(configPath) Then
                ExportTemplate(configPath, dsData, Nothing, "Template_Contract_" & Format(Date.Now, "yyyyMMdd"))
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
        'Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            'templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            'filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName

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
            'designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", Aspose.Cells.ContentDisposition.Attachment, New XlsSaveOptions())
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

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
                ShowMessage(Translate("File không có dữ liệu <br/> Vui lòng kiểm tra lại"), NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            dtDataImportWorking = dtData.Clone
            dtError = dtData.Clone
            Dim iRow = 1
            Dim _filter As New WorkingDTO
            Dim rep As New ProfileBusinessRepository
            Dim IBusiness As IProfileBusiness = New ProfileBusinessClient()
            Dim empId, empid1, empid2, empid4 As Integer
            Dim empid3, emp6 As Boolean
            Dim empid5 As EmployeeDTO
            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                isError = False
                'sError = "Chưa nhập mã nhân viên"
                'ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)
                empId = rep.CheckEmployee_Exits(row("EMPLOYEE_CODE"))
                empid1 = rep.CheckEmployee_Terminate(row("EMPLOYEE_CODE"))
                empid2 = rep.CheckEmployee_Contract_Count(row("EMPLOYEE_CODE"))
                'If row("EFFECT_SALARY_DATE") Is DBNull.Value OrElse row("EFFECT_SALARY_DATE") = "" Then
                'empid3 = rep.EffectDate_Check_Same(row("EMPLOYEE_CODE"), ToDate(row("EFFECT_SALARY_DATE")))
                'emp6 = rep.ValidContract1(row("EMPLOYEE_CODE"), ToDate(row("EFFECT_SALARY_DATE")))
                'Else
                'empid3 = True
                'emp6 = True
                'End If
                'empid4 = rep.CheckEmployee_EffectDate_exits
                empid5 = rep.GetEmpIdByCode(row("EMPLOYEE_CODE"))

                If empId = 0 Then
                    sError = "Mã nhân viên - Không tồn tại"
                    ImportValidate.IsValidTime("EMPLOYEE_CODE", row, rowError, isError, sError)
                End If
                If empid1 <> 0 Then
                    sError = "Nhân viên đã nghỉ việc"
                    ImportValidate.IsValidTime("EMPLOYEE_CODE", row, rowError, isError, sError)
                End If
                If empid2 = 0 Then
                    sError = "Nhân viên đã có 2 hợp đồng chính thức"
                    ImportValidate.IsValidTime("EMPLOYEE_CODE", row, rowError, isError, sError)
                End If

                If row("EFFECT_SALARY_DATE") Is DBNull.Value OrElse row("EFFECT_SALARY_DATE") = "" Then
                    sError = "Chưa nhập ngày hiệu lực lương"
                    ImportValidate.IsValidTime("EFFECT_SALARY_DATE", row, rowError, isError, sError)
                ElseIf CheckDate(row("EFFECT_SALARY_DATE")) = False Then
                    sError = "Ngày hiệu lực - không đúng định dạng"
                    ImportValidate.IsValidTime("EFFECT_SALARY_DATE", row, rowError, isError, sError)
                Else
                    Try
                        empid3 = rep.EffectDate_Check_Same(row("EMPLOYEE_CODE"), ToDate(row("EFFECT_SALARY_DATE")))
                        emp6 = rep.ValidContract1(row("EMPLOYEE_CODE"), ToDate(row("EFFECT_SALARY_DATE")))
                        If empid3 = False Then
                            sError = "Trùng ngày hiệu lực với hợp đồng cũ"
                            ImportValidate.IsValidTime("EFFECT_SALARY_DATE", row, rowError, isError, sError)
                        ElseIf emp6 = False Then
                            sError = "Ngày hiệu lực phải lớn hơn ngày hết hiệu lực của hợp đồng cũ"
                            ImportValidate.IsValidTime("EFFECT_SALARY_DATE", row, rowError, isError, sError)
                        ElseIf Not rep.ValidContract(empid5.EMPLOYEE_ID, ToDate(row("EFFECT_SALARY_DATE"))) Then
                            sError = "Hợp đồng cũ còn hiệu lực. Không được phép tạo hợp đồng mới."
                            ImportValidate.IsValidTime("EFFECT_SALARY_DATE", row, rowError, isError, sError)
                        End If
                    Catch ex As Exception
                        GoTo VALIDATE
                    End Try
                End If
VALIDATE:

                If Not IsNumeric(row("CONTRACT_TYPE_ID")) Then
                    sError = "Chưa nhập loại hợp đồng"
                    ImportValidate.IsValidTime("CONTRACT_TYPE_ID", row, rowError, isError, sError)
                End If
                If Not IsNumeric(row("SIGN_CONTRACT_ID")) Then
                    sError = "Chưa nhập đơn vị kí hợp đồng"
                    ImportValidate.IsValidTime("SIGN_CONTRACT_ID", row, rowError, isError, sError)
                End If
                If row("START_DATE") Is DBNull.Value OrElse row("START_DATE") = "" Then
                    sError = "Chưa nhập ngày bắt đầu"
                    ImportValidate.IsValidTime("START_DATE", row, rowError, isError, sError)
                ElseIf CheckDate(row("START_DATE")) = False Then
                    sError = "Ngày bắt đầu - không đúng định dạng"
                    ImportValidate.IsValidTime("START_DATE", row, rowError, isError, sError)
                End If
                If row("CONTRACT_TYPE_ID") <> 5 Then
                    If row("END_DATE") Is DBNull.Value OrElse row("END_DATE") = "" Then
                        sError = "Chưa nhập ngày kết thúc"
                        ImportValidate.IsValidTime("END_DATE", row, rowError, isError, sError)
                    ElseIf CheckDate(row("END_DATE")) = False Then
                        sError = "Ngày kết thúc - không đúng định dạng"
                        ImportValidate.IsValidTime("END_DATE", row, rowError, isError, sError)
                    End If
                End If
                If row("START_TIME_MORNING") Is DBNull.Value OrElse row("START_TIME_MORNING") = "" Then
                    sError = "Chưa nhập thời gian bắt đầu làm việc buổi sáng - hh:mm"
                    ImportValidate.IsValidTime("START_TIME_MORNING", row, rowError, isError, sError)
                ElseIf CheckTimeIn(row("START_TIME_MORNING")) = False Then
                    sError = "Thời gian bắt đầu làm việc buổi sáng không đúng định dạng - hh:mm"
                    ImportValidate.IsValidTime("START_TIME_MORNING", row, rowError, isError, sError)
                End If
                If row("END_TIME_MORNING") Is DBNull.Value OrElse row("END_TIME_MORNING") = "" Then
                    sError = "Chưa nhập thời gian kết thúc làm việc buổi sáng - hh:mm"
                    ImportValidate.IsValidTime("END_TIME_MORNING", row, rowError, isError, sError)
                ElseIf CheckTimeIn(row("END_TIME_MORNING")) = False Then
                    sError = "Thời gian kết thúc làm việc buổi sáng không đúng định dạng - hh:mm"
                    ImportValidate.IsValidTime("END_TIME_MORNING", row, rowError, isError, sError)
                End If
                If row("START_TIME_AFTERNOON") Is DBNull.Value OrElse row("START_TIME_AFTERNOON") = "" Then
                    sError = "Chưa nhập thời gian bắt đầu làm việc buổi chiều - hh:mm"
                    ImportValidate.IsValidTime("START_TIME_AFTERNOON", row, rowError, isError, sError)
                ElseIf CheckTimeIn(row("START_TIME_AFTERNOON")) = False Then
                    sError = "Thời gian bắt đầu làm việc buổi chiều không đúng định dạng - hh:mm"
                    ImportValidate.IsValidTime("START_TIME_AFTERNOON", row, rowError, isError, sError)
                End If
                If row("END_TIME_AFTERNOON") Is DBNull.Value OrElse row("END_TIME_AFTERNOON") = "" Then
                    sError = "Chưa nhập thời gian kết thúc làm việc buổi chiều - hh:mm"
                    ImportValidate.IsValidTime("END_TIME_AFTERNOON", row, rowError, isError, sError)
                ElseIf CheckTimeIn(row("END_TIME_AFTERNOON")) = False Then
                    sError = "Thời gian kết thúc làm việc buổi chiều không đúng định dạng - hh:mm"
                    ImportValidate.IsValidTime("END_TIME_AFTERNOON", row, rowError, isError, sError)
                End If

                ''''
                If row("SIGN_DATE") Is DBNull.Value OrElse row("SIGN_DATE") = "" Then
                    sError = "Chưa nhập ngày ký"
                    ImportValidate.IsValidTime("SIGN_DATE", row, rowError, isError, sError)
                ElseIf CheckDate(row("SIGN_DATE")) = False Then
                    sError = "Ngày ký - không đúng định dạng"
                    ImportValidate.IsValidTime("SIGN_DATE", row, rowError, isError, sError)
                End If
                If row("SIGN_EMP_ID") Is DBNull.Value OrElse row("SIGN_EMP_ID") = "" Then
                    sError = "Chưa nhập người ký"
                    ImportValidate.IsValidTime("SIGN_EMP_NAME", row, rowError, isError, sError)
                End If
                'empId = rep.CheckEmployee_Exits(row("SIGN_EMP_CODE"))
                'If empId = 0 Then
                '    sError = "Mã nhân viên ký - Không tồn tại hoặc đã nghỉ việc"
                '    ImportValidate.IsValidTime("SIGN_EMP_CODE", row, rowError, isError, sError)
                'End If
                'If Not IsNumeric(row("PERCENTSALARY")) Then
                '    sError = "Chưa nhập % hưởng lương - Chỉ được nhập số"
                '    ImportValidate.IsValidTime("PERCENTSALARY", row, rowError, isError, sError)
                'End If


                If isError Then
                    'rowError("STT") = row("EMPLOYEE_CODE").ToString
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    dtError.Rows.Add(rowError)
                Else
                    dtDataImportWorking.ImportRow(row)
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
                    RowErrorGroup("STT") = dtError.Rows(j)("STT")
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

#End Region
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
    Private Function CheckTimeIn(ByVal value As String) As Boolean
        Dim timeCheck As Boolean
        Dim result As Date

        If value = "" Or value = "&nbsp;" Then
            value = ""
            Return True
        End If

        Try
            timeCheck = IsDate(value)
            Dim a = Format(Now, "h:mm tt")
            If a Is Nothing Then
                timeCheck = False
            Else
                timeCheck = True
            End If
            'dateCheck = DateTime.TryParseExact(value, "HH24:MI", New CultureInfo("en-US"), DateTimeStyles.None, result)
            Return timeCheck
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
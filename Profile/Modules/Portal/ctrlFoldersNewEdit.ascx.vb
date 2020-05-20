Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog
Imports System.IO
Imports Common
Imports Profile.ProfileBusiness

Public Class ctrlFoldersNewEdit
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False

    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _flag As Boolean = True
    Dim _classPath As String = "Profile\Portal" + Me.GetType().Name.ToString()
#Region "Property"
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    Property OldFolderName As String
        Get
            Return ViewState(Me.ID & "_OldFolderName")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_OldFolderName") = value
        End Set
    End Property
#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 06/07/2017 14:36
    ''' </lastupdate>
    ''' <summary>
    ''' Hiển thị thông tin các control trên page
    ''' Cập nhật các trạng thái của các control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(tbarFolderEdit)
            Dim startTime As DateTime = DateTime.UtcNow
            GetParams()
            Refresh()
            UpdateControlState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    '''<lastupdate>
    ''' 06/07/2017 14:39
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo giá trị cho các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdated>
    ''' 06/07/2017 14:40
    ''' </lastupdated>
    ''' <summary>
    ''' Phương thức bind dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 14:56
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc khoi tao cac gia tri cho cac control tren page
    ''' Fixed doi voi user la HR.Admin hoac Admin thi them chuc nang "Mo cho phe duyet"
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Me.MainToolBar = tbarFolderEdit

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 15:17
    ''' </lastupdate>
    ''' <summary>
    ''' Lam moi trang thai cua cac control tren page
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New ProfileBusinessRepository
        Try
            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim objFolder = rep.GetFolderByID(IDSelect)
                    txtFolderName.Text = objFolder.NAME
                    OldFolderName = objFolder.NAME
                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
                    Dim dt As New DataTable
                    'rgAllow.DataSource = New List(Of WorkingAllowanceDTO)
                    'rgAllow.DataBind()
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 06/07/2017 15:41
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện command tren toolbar khi click vao cac item cua no
    ''' Cac command la luu, mo khoa, huy 
    ''' Cập nhật lại trạng thái các control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim gID As Decimal
        Dim rep As New ProfileBusinessRepository
        Dim store As New ProfileStoreProcedure
        'Dim stt As OtherListDTOsave
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    Dim _folder As New FoldersDTO
                    If CurrentState = CommonMessage.STATE_EDIT Then
                        _folder.ID = IDSelect
                    End If

                    _folder.NAME = txtFolderName.Text

                    If CurrentState = CommonMessage.STATE_NEW Then
                        _folder.PARENT_ID = IDSelect
                    End If

                    If rep.AddFolder(_folder) = 1 Then
                        ShowMessage(Translate("Trùng tên thư mục"), NotifyType.Warning)
                        Exit Sub
                    Else
                        Dim path = store.Get_Folder_link(IDSelect)

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                Dim link = Server.MapPath("TemplateDynamic\UserFiles\" & path & "\" & _folder.NAME)
                                If Not Directory.Exists(link) Then
                                    Directory.CreateDirectory(link)
                                End If
                            Case CommonMessage.STATE_EDIT
                                Dim lastindx = path.LastIndexOf("\")
                                path = path.Substring(0, lastindx)
                                Dim link = Server.MapPath("TemplateDynamic\UserFiles\" & path & "\" & _folder.NAME)
                                Dim _oldLink = Server.MapPath("TemplateDynamic\UserFiles\" & path & "\" & OldFolderName)
                                If Not Directory.Exists(_oldLink) Then
                                    Directory.CreateDirectory(link)
                                Else
                                    FileIO.FileSystem.RenameDirectory(_oldLink, _folder.NAME)
                                End If
                        End Select
                        

                        Gotolink("/Default.aspx?mid=Profile&fid=ctrlPortalEmpFileMng")
                    End If
                    Refresh()
                    CurrentState = CommonMessage.STATE_NORMAL
            End Select
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    'Private Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim rep As New ProfileBusinessRepository
    '    Dim store As New ProfileStoreProcedure
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try

    '        ScriptManager.RegisterStartupScript(Page, Page.GetType, "Close", "CloseWindow();", True)
    '        'Dim PrID As Decimal = 0
    '        'If IsNumeric(Request.Params("PrID")) Then
    '        '    PrID = Request.Params("PrID")
    '        'End If
    '        'Dim _folder As New FoldersDTO
    '        '_folder.NAME = txtFolderName.Text
    '        '_folder.PARENT_ID = PrID
    '        'If rep.AddFolder(_folder) = 1 Then
    '        '    ShowMessage(Translate("Trùng tên thư mục"), NotifyType.Warning)
    '        '    Exit Sub
    '        'Else
    '        '    Dim link = Server.MapPath("TemplateDynamic\UserFiles\" & store.Get_Folder_link(_folder.PARENT_ID) & "\" & _folder.NAME)
    '        '    If Not Directory.Exists(link) Then
    '        '        Directory.CreateDirectory(link)
    '        '    End If
    '        '    ScriptManager.RegisterStartupScript(Page, Page.GetType, "Close", "CloseWindow();", True)
    '        'End If
    '        'UpdateControlState()
    '        CurrentState = CommonMessage.STATE_NORMAL
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try

    'End Sub
#End Region

#Region "Custom"



    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Hàm cập nhật trạng thái của các control trên page
    ''' Xử lý đăng ký popup ứng với giá trị isLoadPopup
    ''' </summary>
    ''' <remarks></remarks>

    Public Overrides Sub UpdateControlState()
        'Dim rep As New ProfileBusinessRepository

        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(ViewName, ID, ex)
        End Try

    End Sub
    ''' <lastupdate>
    ''' 06/07/2017 17:53
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc load dữ liệu cho các combobox
    ''' cboContractType, cboStatus
    ''' </summary>
    ''' <remarks></remarks>

    Private Sub GetParams()
        Try
            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    IDSelect = If(IsNumeric(Request.Params("ID")), Decimal.Parse(Request.Params("ID")), 0)
                    Refresh("UpdateView")
                ElseIf Request.Params("PrID") IsNot Nothing Then
                    IDSelect = If(IsNumeric(Request.Params("PrID")), Decimal.Parse(Request.Params("PrID")), 0)
                    Refresh("InsertView")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

    Sub Gotolink(ByVal link As String)
        Dim str As String
        str = "var link=document.createElement('a');"
        str &= String.Format("link.href='{0}';", link)
        str &= String.Format("link.target='{0}';", "_parent")
        str &= "link.click();"

        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
    End Sub

End Class
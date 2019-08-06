Imports Framework.UI
Imports Common.CommonBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Imports System.IO
Imports System.Threading
Imports System.Drawing

Public Class CommonView
    Inherits ViewBase
    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Common\Modules\Common\" + Me.GetType().Name.ToString()

    Protected WithEvents _toolbar As RadToolBar
    ''' <lastupdate>
    ''' 13/07/2017 11:14
    ''' </lastupdate>
    ''' <summary>
    ''' Get maintoolbar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainToolBar As RadToolBar
        Get
            Return _toolbar
        End Get
        Set(ByVal value As RadToolBar)
            _toolbar = value
        End Set
    End Property
    Public _SE_CASE_CONFIG As DataTable
    Public Property SE_CASE_CONFIG As DataTable
        Get
            Return _SE_CASE_CONFIG
        End Get
        Set(ByVal value As DataTable)
            _SE_CASE_CONFIG = value
        End Set
    End Property

    Public Function getSE_CASE_CONFIG(ByVal codecase As String) As Integer
        Try
            Dim rep As New CommonRepository
            Dim count As Integer ' get thong config case theo Me.ID
            count = rep.GetCaseConfigByID(Me.ID, codecase)
            Return count
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <lastupdate>
    ''' 13/07/2017 11:14
    ''' </lastupdate>
    ''' <summary>
    ''' Check authen
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function IsAuthenticated() As Boolean
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            'If Utilities.IsAuthenticated AndAlso LogHelper.CurrentUser IsNot Nothing Then
            If LogHelper.CurrentUser IsNot Nothing Then
                Return True
            End If
            Return False
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ''DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Return False
        End Try

    End Function
    ''' <lastupdate>
    ''' 13/07/2017 11:14
    ''' </lastupdate>
    ''' <summary>
    ''' Cập nhật trạng thái toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ChangeToolbarState()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If _toolbar Is Nothing Then Exit Sub
            Dim item As RadToolBarButton
            For i = 0 To _toolbar.Items.Count - 1
                item = CType(_toolbar.Items(i), RadToolBarButton)
                Select Case CurrentState
                    Case CommonMessage.STATE_EDIT, CommonMessage.STATE_NEW
                        If item.CommandName = CommonMessage.TOOLBARITEM_SAVE Or item.CommandName = CommonMessage.TOOLBARITEM_CANCEL Then
                            item.Enabled = True
                        Else
                            item.Enabled = False
                        End If
                    Case Else
                        If item.CommandName = CommonMessage.TOOLBARITEM_SAVE Or item.CommandName = CommonMessage.TOOLBARITEM_CANCEL Then
                            item.Enabled = False
                        Else
                            item.Enabled = True
                        End If
                End Select
            Next
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ''DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 13/07/2017 11:14
    ''' </lastupdate>
    ''' <summary>
    ''' Get quyen hien thi toolbar của user
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ToolbarAuthorization()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If MainToolBar IsNot Nothing Then
                For Each item As RadToolBarItem In MainToolBar.Items
                    Dim bAllow As Boolean = False

                    If item.Attributes("Authorize") IsNot Nothing AndAlso item.Attributes("Authorize").Trim <> "" Then
                        Select Case item.Attributes("Authorize")
                            Case CommonMessage.AUTHORIZE_CREATE
                                bAllow = Me.AllowCreate
                            Case CommonMessage.AUTHORIZE_MODIFY
                                bAllow = Me.AllowModify
                            Case CommonMessage.AUTHORIZE_DELETE
                                bAllow = Me.AllowDelete
                            Case CommonMessage.AUTHORIZE_PRINT
                                bAllow = Me.AllowPrint
                            Case CommonMessage.AUTHORIZE_IMPORT
                                bAllow = Me.AllowImport
                            Case CommonMessage.AUTHORIZE_EXPORT
                                bAllow = Me.AllowExport
                            Case CommonMessage.AUTHORIZE_SPECIAL1
                                bAllow = Me.AllowSpecial1
                            Case CommonMessage.AUTHORIZE_SPECIAL2
                                bAllow = Me.AllowSpecial2
                            Case CommonMessage.AUTHORIZE_SPECIAL3
                                bAllow = Me.AllowSpecial3
                            Case CommonMessage.AUTHORIZE_SPECIAL4
                                bAllow = Me.AllowSpecial4
                            Case CommonMessage.AUTHORIZE_SPECIAL5
                                bAllow = Me.AllowSpecial5
                            Case CommonMessage.AUTHORIZE_RESET
                                bAllow = Me.AllowReset
                        End Select
                    Else
                        bAllow = True
                    End If
                    item.Visible = bAllow
                Next
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ''DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <lastupdate>
    ''' 13/07/2017 11:14
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click on toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnMainToolbar_Click(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles _toolbar.ButtonClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            LogHelper.ViewName = Me.ViewName
            LogHelper.ViewDescription = Me.ViewDescription
            LogHelper.ViewGroup = Me.ViewGroup
            LogHelper.ActionName = CType(e.Item, RadToolBarButton).CommandName
            RaiseEvent OnMainToolbarClick(sender, e)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Delegate Sub ToolBarClickDelegate(ByVal sender As Object, ByVal e As RadToolBarEventArgs)
    Public Event OnMainToolbarClick As ToolBarClickDelegate
    ''' <lastupdate>
    ''' 13/07/2017 11:14
    ''' </lastupdate>
    ''' <summary>
    ''' Check quyền của user
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub CheckAuthorization()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim rep As New CommonRepository
            Dim user = LogHelper.CurrentUser
            Dim strApp As String = LogHelper.GetSessionCurrentApp(Session.SessionID)
            If strApp = "Main" Then
                Dim strStatus As String = LogHelper.GetSessionStatus(Session.SessionID)
                If strStatus = "KILLED" Then
                    If LogHelper.OnlineUsers.ContainsKey(Session.SessionID) Then
                        LogHelper.SaveAccessLog(Session.SessionID, "Killed")
                        LogHelper.OnlineUsers.Remove(Session.SessionID)
                        Session.Abandon()
                        FormsAuthentication.SignOut()
                        Response.Redirect("/SessionKilled.aspx")
                    End If
                End If
            End If

            If Me.MustAuthorize Then
                'If Utilities.IsAuthenticated Then
                If LogHelper.CurrentUser IsNot Nothing Then
                    Dim GroupAdmin As Boolean = rep.CheckGroupAdmin(Common.GetUsername)
                    If GroupAdmin = False Then
                        Dim permissions As List(Of PermissionDTO) = rep.GetUserPermissions(Common.GetUsername)
                        If permissions IsNot Nothing Then
                            Dim ViewPermissions As List(Of PermissionDTO)
                            ViewPermissions = (From p In permissions Where p.FID = Me.ViewName And p.IS_REPORT = False).ToList
                            If ViewPermissions IsNot Nothing Then
                                For Each item In ViewPermissions
                                    Me.Allow = True
                                    Me.AllowCreate = Me.AllowCreate Or item.AllowCreate
                                    Me.AllowModify = Me.AllowModify Or item.AllowModify
                                    Me.AllowDelete = Me.AllowDelete Or item.AllowDelete
                                    Me.AllowPrint = Me.AllowPrint Or item.AllowPrint
                                    Me.AllowImport = Me.AllowImport Or item.AllowImport
                                    Me.AllowExport = Me.AllowExport Or item.AllowExport
                                    Me.AllowSpecial1 = Me.AllowSpecial1 Or item.AllowSpecial1
                                    Me.AllowSpecial2 = Me.AllowSpecial2 Or item.AllowSpecial2
                                    Me.AllowSpecial3 = Me.AllowSpecial3 Or item.AllowSpecial3
                                    Me.AllowSpecial4 = Me.AllowSpecial4 Or item.AllowSpecial4
                                    Me.AllowSpecial5 = Me.AllowSpecial5 Or item.AllowSpecial5
                                    Me.AllowReset = Me.AllowReset
                                Next
                            End If
                        End If
                    Else
                        If user.MODULE_ADMIN = "*" OrElse (user.MODULE_ADMIN IsNot Nothing AndAlso user.MODULE_ADMIN.Contains(Me.ModuleName)) Then
                            Me.Allow = True
                            Me.AllowCreate = True
                            Me.AllowModify = True
                            Me.AllowDelete = True
                            Me.AllowPrint = True
                            Me.AllowImport = True
                            Me.AllowExport = True
                            Me.AllowSpecial1 = True
                            Me.AllowSpecial2 = True
                            Me.AllowSpecial3 = True
                            Me.AllowSpecial4 = True
                            Me.AllowSpecial5 = True
                            Me.AllowReset = True
                        End If
                    End If
                End If
            Else
                Me.Allow = True
                Me.AllowCreate = True
                Me.AllowModify = True
                Me.AllowDelete = True
                Me.AllowPrint = True
                Me.AllowImport = True
                Me.AllowExport = True
                Me.AllowSpecial1 = True
                Me.AllowSpecial2 = True
                Me.AllowSpecial3 = True
                Me.AllowSpecial4 = True
                Me.AllowSpecial5 = True
                Me.AllowReset = True
            End If
            ToolbarAuthorization()
            If Me.Allow Then
                Dim func = rep.GetFunction(Me.ViewName)
                If func IsNot Nothing Then
                    Me.ViewDescription = Translate(func.NAME)
                    Me.ViewGroup = func.FUNCTION_GROUP_NAME
                End If

                If Me.EnableLogAccess Then
                    LogHelper.UpdateAccessLog(Me)
                End If

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ''DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    ''' <lastupdate>
    ''' 13/07/2017 11:14
    ''' </lastupdate>
    ''' <summary>
    ''' Hiển thị exception
    ''' </summary>
    ''' <param name="ViewName"></param>
    ''' <param name="ID"></param>
    ''' <param name="ex"></param>
    ''' <param name="ExtraInfo"></param>
    ''' <remarks></remarks>
    Public Overrides Sub DisplayException(ByVal ViewName As String, ByVal ID As String, ByVal ex As System.Exception, Optional ByVal ExtraInfo As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Common.DisplayException(Me, ex, "ViewName: " & ViewName & " ViewID:" & ID)

        Catch e As Exception
            ''DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, e, "")
        End Try

    End Sub
    Protected Overrides Sub FrameworkInitialize()
        Thread.CurrentThread.CurrentCulture = Common.SystemLanguage
        Thread.CurrentThread.CurrentUICulture = Common.SystemLanguage
        MyBase.FrameworkInitialize()
    End Sub

#Region "View config"
    Dim vcf As DataSet
    Public Sub ViewConfig(ByVal rp As RadPane)
        'If LogHelper.CurrentUser.USERNAME.ToUpper = "ADMIN" Then Return
        vcf = New DataSet
        Using rep = New CommonRepository
            vcf.ReadXml(New IO.StringReader(rep.GetConfigView(Me.ID).Rows(0)("config_data").ToString()))
        End Using
        Dim star As String = "*"
        Try
            If vcf IsNot Nothing AndAlso vcf.Tables("control") IsNot Nothing Then
                Dim dtCtrl As DataTable = vcf.Tables("control")
                For Each ctrs As Control In rp.Controls
                    Dim row As DataRow
                    Try
                        row = dtCtrl.Select("Ctl_ID ='" + ctrs.ID + "'")(0)
                    Catch ex As Exception
                        Continue For
                    End Try
                    If row IsNot Nothing Then
                        ctrs.Visible = If(IsDBNull(row("Is_Visible")), False, CBool(row("Is_Visible")))
                        Try
                            Dim validator As BaseValidator = rp.FindControl(row.Field(Of String)("Validator_ID"))
                            Dim labelCtr As Label = rp.FindControl(row.Field(Of String)("Label_ID").Trim())
                            If labelCtr IsNot Nothing Then
                                labelCtr.Visible = ctrs.Visible
                                labelCtr.Text = If(IsDBNull(row("Label_text")), labelCtr.Text, Translate(row("Label_text")))
                            End If
                            If validator IsNot Nothing Then
                                validator.Enabled = If(IsDBNull(row("Is_Validator")), True, CBool(row("Is_Validator")))
                                validator.ErrorMessage = If(IsDBNull(row("ErrorMessage")), validator.ErrorMessage, row("ErrorMessage"))
                                validator.ToolTip = If(IsDBNull(row("ErrorToolTip")), validator.ToolTip, row("ErrorToolTip"))
                                If validator.Enabled Then
                                    labelCtr.Text = If(IsDBNull(row("Label_text")), labelCtr.Text, Translate(row("Label_text"))) + String.Format("<span style='color:red'> {0}</span>", star)
                                Else
                                    labelCtr.Text = If(IsDBNull(row("Label_text")), labelCtr.Text, Translate(row("Label_text")))
                                End If
                            End If
                        Catch ex As Exception
                            Continue For
                        End Try
                    End If
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Sub GirdConfig(ByVal rg As RadGrid)
        vcf = New DataSet
        Using rep = New CommonRepository
            vcf.ReadXml(New IO.StringReader(rep.GetConfigView(Me.ID).Rows(0)("config_data").ToString()))
        End Using
        'If LogHelper.CurrentUser.USERNAME.ToUpper = "ADMIN" Then Return
        Dim dtGrid As DataTable = vcf.Tables("girdColumm")
        If dtGrid.Rows.Count = 0 AndAlso dtGrid Is Nothing Then Exit Sub
        Dim view As DataView = New DataView(dtGrid)
        view.Sort = "Orderby"
        dtGrid = view.ToTable()

        Dim rCol As GridBoundColumn
        Dim rColClientSelect As GridClientSelectColumn
        Dim rColCheck As GridCheckBoxColumn
        Dim rColdImage As GridBinaryImageColumn
        rg.MasterTableView.Columns.Clear()
        For Each row As DataRow In dtGrid.Rows
            Try
                If row.Field(Of String)("ID").Trim() = "cbStatus" Then
                    rColClientSelect = New GridClientSelectColumn()
                    rg.MasterTableView.Columns.Add(rColClientSelect)
                    rColClientSelect.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColClientSelect.HeaderStyle.Width = 30
                    rColClientSelect.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColClientSelect.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "Boolean".ToUpper Then
                    rColCheck = New GridCheckBoxColumn()
                    rg.MasterTableView.Columns.Add(rColCheck)
                    rColCheck.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rColCheck.DataField = row.Field(Of String)("ID").Trim()
                    If IsNumeric(row("Width")) Then
                        rColCheck.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                    End If
                    rColCheck.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    rColCheck.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rColCheck.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                    rColCheck.Visible = Boolean.Parse(row.Item("Is_Visible"))
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "IMAGE".ToUpper Then
                    rColdImage = New GridBinaryImageColumn
                    rg.MasterTableView.Columns.Add(rColdImage)
                    rColdImage.DataField = row.Field(Of String)("ID").Trim()
                    rColdImage.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    rColdImage.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rColdImage.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                    rColdImage.DataAlternateTextField = row.Field(Of String)("ID").Trim()
                    rColdImage.ImageHeight = 80
                    rColdImage.ImageWidth = 80
                    rColdImage.ResizeMode = 3
                    rColdImage.DataAlternateTextFormatString = "Image of {0}"
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper.Contains("Short-DateTime".ToUpper) = True Then
                    rCol = New GridBoundColumn()
                    rg.MasterTableView.Columns.Add(rCol)
                    rCol.DataField = row.Field(Of String)("ID").Trim()
                    rCol.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rCol.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rCol.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rCol.AllowFiltering = True
                    rCol.AllowSorting = True
                    rCol.AutoPostBackOnFilter = True
                    rCol.CurrentFilterFunction = GridKnownFunction.Contains
                    rCol.ShowFilterIcon = False
                    rCol.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rCol.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rCol.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    Dim StrFormat As String = String.Empty
                    If row.Field(Of String)("DataType").Trim().Split("#").Count > 1 AndAlso row.Field(Of String)("DataType").Trim().Split("#")(1) IsNot Nothing Then
                        StrFormat = row.Field(Of String)("DataType").Trim().Split("#")(1).ToString
                        rCol.DataFormatString = StrFormat
                    End If
                ElseIf row.Field(Of String)("ID").Trim().Contains("FROM_MONTH") Or row.Field(Of String)("ID").Trim().Contains("TO_MONTH") Then
                    rCol = New GridBoundColumn()
                    rg.MasterTableView.Columns.Add(rCol)
                    rCol.DataField = row.Field(Of String)("ID").Trim()
                    rCol.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rCol.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rCol.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rCol.AllowFiltering = True
                    rCol.AllowSorting = True
                    rCol.AutoPostBackOnFilter = True
                    rCol.CurrentFilterFunction = GridKnownFunction.Contains
                    rCol.ShowFilterIcon = False
                    rCol.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rCol.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rCol.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    If row.Field(Of String)("DataType").Trim().ToUpper = "DateTime".ToUpper Then
                        rCol.DataFormatString = ConfigurationManager.AppSettings("FDATE_MONTH_YEAR_GRID")
                    End If
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "MonthYear".ToUpper Then
                    rCol = New GridBoundColumn()
                    rg.MasterTableView.Columns.Add(rCol)
                    rCol.DataField = row.Field(Of String)("ID").Trim()
                    rCol.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rCol.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rCol.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rCol.AllowFiltering = True
                    rCol.AllowSorting = True
                    rCol.AutoPostBackOnFilter = True
                    rCol.CurrentFilterFunction = GridKnownFunction.Contains
                    rCol.ShowFilterIcon = False
                    rCol.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rCol.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rCol.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    rCol.DataFormatString = ConfigurationManager.AppSettings("FDATE_MONTH_YEAR_GRID")
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "Time".ToUpper Then
                    rCol = New GridBoundColumn()
                    rg.MasterTableView.Columns.Add(rCol)
                    rCol.DataField = row.Field(Of String)("ID").Trim()
                    rCol.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rCol.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rCol.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rCol.AllowFiltering = True
                    rCol.AllowSorting = True
                    rCol.AutoPostBackOnFilter = True
                    rCol.CurrentFilterFunction = GridKnownFunction.Contains
                    rCol.ShowFilterIcon = False
                    rCol.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rCol.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rCol.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    If row.Field(Of String)("DataType").Trim().ToUpper = "Time".ToUpper Then
                        rCol.DataFormatString = ConfigurationManager.AppSettings("TIME")
                    End If
                ElseIf row.Field(Of String)("DataType").Trim().ToUpper = "Numeric".ToUpper Then
                    rCol = New GridNumericColumn()
                    rg.MasterTableView.Columns.Add(rCol)
                    rCol.DataField = row.Field(Of String)("ID").Trim()
                    rCol.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rCol.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rCol.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rCol.AllowFiltering = True
                    rCol.AllowSorting = True
                    rCol.AutoPostBackOnFilter = True
                    rCol.CurrentFilterFunction = GridKnownFunction.Contains
                    rCol.ShowFilterIcon = False
                    rCol.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rCol.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rCol.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    If row.Field(Of String)("DataType").Trim().ToUpper = "DateTime".ToUpper Then
                        rCol.DataFormatString = ConfigurationManager.AppSettings("FDATEGRID")
                    ElseIf row.Field(Of String)("DataType").Trim() = "Numeric" Then
                        rCol.DataFormatString = "{0:#,##0.##}"
                    End If
                Else
                    rCol = New GridBoundColumn()
                    rg.MasterTableView.Columns.Add(rCol)
                    rCol.DataField = row.Field(Of String)("ID").Trim()
                    rCol.HeaderText = Translate(row.Field(Of String)("Name").Trim())
                    If IsNumeric(row("Width").ToString()) Then
                        rCol.HeaderStyle.Width = Integer.Parse(row("Width").ToString())
                        rCol.FilterControlWidth = Integer.Parse(row("Width").ToString())
                    End If
                    rCol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    rCol.AllowFiltering = True
                    rCol.AllowSorting = True
                    rCol.AutoPostBackOnFilter = True
                    rCol.CurrentFilterFunction = GridKnownFunction.Contains
                    rCol.ShowFilterIcon = False
                    rCol.HeaderTooltip = (row.Field(Of String)("Name").Trim())
                    rCol.FilterControlToolTip = (row.Field(Of String)("Name").Trim())
                    rCol.Visible = Boolean.Parse(row.Item("Is_Visible"))
                    If row.Field(Of String)("DataType").Trim().ToUpper = "DateTime".ToUpper Then
                        rCol.DataFormatString = ConfigurationManager.AppSettings("FDATEGRID")
                    ElseIf row.Field(Of String)("DataType").Trim() = "Number" Then
                        rCol.DataFormatString = "{0:#,##0.##}"
                    End If
                End If

            Catch ex As Exception
                Continue For
            End Try
        Next
    End Sub
#End Region
End Class

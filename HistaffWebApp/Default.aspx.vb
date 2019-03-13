Imports Framework.UI
Imports Common
Imports Telerik.Web.UI
Imports System.Xml
Imports Telerik.Web.Zip
Imports WebAppLog
Imports System.IO


Public Class _Default
    Inherits AjaxPage
    Protected WithEvents CurrentView As ViewBase
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = Me.GetType().Name.ToString()

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            MyBase.OnInit(e)
            Me.AjaxManager = RadAjaxManager1
            Me.AjaxLoading = LoadingPanel
            Me.ToolTipManager = Me.RadToolTipManager1
            Me.PopupWindow = rwMainPopup
            If Request.Params("EnableAjax") IsNot Nothing AndAlso Request.Params("EnableAjax") = False Then
                Me.AjaxManager.EnableAJAX = False
            End If
            Dim mid As String = Request.Params("mid")
            Dim fid As String = Request.Params("fid")
            Dim group As String = IIf(Request.Params("group") Is Nothing, "", Request.Params("group"))

            Try
                If mid IsNot Nothing AndAlso mid.Trim <> "" AndAlso fid IsNot Nothing AndAlso fid.Trim <> "" Then
                    CurrentView = Me.Register(fid, mid, fid, group, , True)
                    Me.Title = CurrentView.ViewDescription
                End If

            Catch ex As Exception
                _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
                'DisplayException("", "", ex)
            End Try
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try


    End Sub

    Public Overrides Sub PageLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim mid As String = Request.Params("mid")
        Dim fid As String = Request.Params("fid")
        Dim group As String = IIf(Request.Params("group") Is Nothing, "", Request.Params("group"))
        Dim tlbQuickLaunchToolbar As RadToolBar = Nothing
        Dim mnuMain As RadMenu = Nothing
        Try
            If Not IsPostBack Then
                Try
                    If Common.Common.GetUsername <> "" Then
                        tlbQuickLaunchToolbar = Me.Master.FindControl("QuickLaunchToolbar")
                        If tlbQuickLaunchToolbar IsNot Nothing AndAlso mid IsNot Nothing AndAlso mid.Trim <> "" Then
                            tlbQuickLaunchToolbar.LoadContentFile(String.Format(Utilities.ModulePath & "/{0}/QuickLaunch-" & Common.Common.SystemLanguage.Name & ".xml", mid))
                        End If
                        Using rep As New CommonRepository


                            Dim GroupAdmin As Boolean = rep.CheckGroupAdmin(Common.Common.GetUsername)
                            If Not GroupAdmin Then
                                Dim lstPermission = rep.GetUserPermissions(Common.Common.GetUsername)
                                For Each dr In tlbQuickLaunchToolbar.GetAllItems
                                    If Not DirectCast(dr, RadToolBarButton).IsSeparator Then
                                        Dim query = (From p In lstPermission Where p.FID = dr.Value).Count
                                        If query = 0 Then
                                            dr.Visible = False
                                        End If
                                    End If
                                Next

                                If mid Is Nothing OrElse mid.Trim = "" OrElse fid Is Nothing OrElse fid.Trim = "" Then
                                    Utilities.Redirect("Profile", "ctrlInformation")
                                End If
                            Else
                                Dim user = LogHelper.CurrentUser

                                If mid Is Nothing OrElse mid.Trim = "" OrElse fid Is Nothing OrElse fid.Trim = "" Then
                                    Utilities.Redirect("Profile", "ctrlInformation")
                                End If

                                Dim bShowQuickLaunch As Boolean = False
                                If user.MODULE_ADMIN = "*" OrElse (user.MODULE_ADMIN IsNot Nothing AndAlso user.MODULE_ADMIN.Contains(mid)) Then
                                    bShowQuickLaunch = True
                                End If
                                If Not bShowQuickLaunch Then
                                    For Each dr In tlbQuickLaunchToolbar.GetAllItems
                                        dr.Visible = False
                                    Next
                                End If

                            End If
                        End Using
                    End If
                    _mylog.WriteLog(_mylog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
                Catch ex As Exception
                    _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
                End Try
                Me.DataBind()

            End If

            'Kiểm tra new user đã thay đổi mật khẩu lần đầu
            'If LogHelper.CurrentUser IsNot Nothing Then
            '    If LogHelper.CurrentUser.IS_CHANGE_PASS >= 0 AndAlso Not LogHelper.CurrentUser.IS_AD Then
            '        HttpContext.Current.Response.Redirect("/Account/ChangePassword.aspx", False)
            '        Exit Sub
            '    End If
            'End If

            If CurrentView IsNot Nothing AndAlso Not PagePlaceHolder.Controls.Contains(CurrentView) Then
                Try
                    PagePlaceHolder.Controls.Add(CurrentView)
                Catch ex As Exception
                    _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
                End Try

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ' DisplayException("", "", ex)
        End Try
    End Sub

End Class
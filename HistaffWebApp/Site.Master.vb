﻿Imports Framework.UI
Imports Common
Imports System.Xml
Imports Attendance
Imports Common.CommonBusiness
Imports Telerik.Web.UI
Imports Profile.ProfileBusiness
Imports Profile
Imports System.IO
Imports WebAppLog

Public Class Site
    Inherits System.Web.UI.MasterPage
    Protected Property DatabaseName As String
    Protected Property Username As String
    Dim bAdmin As Boolean
    Dim lstFuncId As New List(Of String)

    Dim birthdayRemind As Integer
    Dim contractRemind As Integer
    Dim probationRemind As Integer
    Dim retireRemind As Integer

    Dim workingRemind As Integer
    Dim terminateRemind As Integer
    Dim terminateDebtRemind As Integer
    Dim noPaperRemind As Integer
    Dim visaRemind As Integer
    Dim worPermitRemind As Integer
    Dim certificateRemind As Integer
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = Me.GetType().Name.ToString()

    Public Property RemindList As List(Of ReminderLogDTO)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim loginStatus As LoginStatus = HeadLoginStatus
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            'PhongDV
            If loginStatus IsNot Nothing Then
                AddHandler loginStatus.LoggingOut, AddressOf LoggingOut
            End If
            'End PhongDV

            Using rep As New Common.CommonRepository
                Username = Common.Common.GetUsername.Trim
                If Username = "" Then Exit Sub
                'PhongDV
                If LogHelper.CurrentUser IsNot Nothing AndAlso LogHelper.CurrentUser.EMPLOYEE_ID IsNot Nothing Then
                    lblUserName.Text = LogHelper.CurrentUser.FULLNAME
                    Dim lastname As String
                    Dim result As String() = LogHelper.CurrentUser.FULLNAME.Split(" ")
                    For Each s As String In result
                        lastname = s
                    Next
                    lblUserName2.Text = lastname
                    lblEmail.Text = LogHelper.CurrentUser.EMAIL
                    Try
                        Dim repP As New ProfileBusinessRepository
                        Dim sError As String = ""

                        If LogHelper.CurrentUser.EMPLOYEE_CODE <> "" Then
                            If LogHelper.ImageUser Is Nothing Then
                                LogHelper.ImageUser = repP.GetEmployeeImage(LogHelper.CurrentUser.EMPLOYEE_ID, sError)
                            End If
                            rbiEmployeeImage.DataValue = LogHelper.ImageUser
                            rbiEmployeeImage1.DataValue = LogHelper.ImageUser
                        End If
                    Catch ex As Exception
                        Throw ex
                    End Try
                End If
                'End PhongDV
                'Kiểm tra phân quyền Menu
                Dim lstFunc = rep.GetUserPermissions(Common.Common.GetUsername)
                bAdmin = rep.CheckGroupAdmin(Common.Common.GetUsername)
                lstFuncId = (From p In lstFunc Select p.FID).ToList
            End Using
            LoadConfig()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Function Translate(ByVal str As String, ByVal ParamArray args() As String) As String
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Using langMgr As New LanguageManager
            Try
                Return langMgr.Translate(str, args)
                _mylog.WriteLog(_mylog._info, _classPath, method,
                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            End Try
        End Using

        Return str
    End Function


    Public Sub LoggingOut(ByVal sender As Object, ByVal e As LoginCancelEventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If LogHelper.OnlineUsers.ContainsKey(Session.SessionID) Then
                LogHelper.SaveAccessLog(Session.SessionID, "Logout")
                LogHelper.OnlineUsers.Remove(Session.SessionID)
                Session.Abandon()
                FormsAuthentication.SignOut()
                FormsAuthentication.RedirectToLoginPage()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Utilities.ShowMessage(Me.Page, ex.Message, Utilities.NotifyType.Error)
        End Try

    End Sub
    ' CreatBy: nhungdt
    ' CreateDate: 22/06/2017
    ' LastUpdate: 23/06/2017
    ' Name: BuildSubMenu
    ' Description: Xây dựng danh sách cây thư mục con của menu
    ' Input:
    ' Output: String
    Protected Function BuildSubMenu() As String
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Dim mid As String = Request.Params("mid")
            Dim xmlFile As String = String.Format(Utilities.ModulePath & "/{0}/Menu-" & Common.Common.SystemLanguage.Name & ".xml", mid)
            Return BuildMenu(xmlFile)
            _mylog.WriteLog(_mylog._info, _classPath, method,
                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Return ""
        End Try
    End Function

    Protected Function BuildMainMenu() As String
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Common.Common.GetUsername = "" Then Return "<li><a class=""fNiv"" href='#' onclick=""mnuHelpClick()""><span>Trợ giúp</span> </a></li>"
            Dim mid As String = Request.Params("mid")
            Dim xmlFile As String = String.Format(Utilities.ModulePath & "/Menu-" & Common.Common.SystemLanguage.Name & ".xml")
            ' Nếu tồn tại file có tên xmlFile thì hàm trả về giá trị rỗng
            If Not IO.File.Exists(Server.MapPath(xmlFile)) Then
                Return ""
            End If
            Dim rep As New CommonRepository
            Dim stringAccessModule As String = rep.GetUserAccessMenuModule(Common.Common.GetUsername)
            Dim stringReturn As String = ""
            Dim stringTemp As String = ""
            Dim stringModule As String = ""
            Dim xmlDoc As New XmlDocument
            xmlDoc.Load(Server.MapPath(xmlFile))
            Dim xElement As XmlElement = xmlDoc.DocumentElement.FirstChild

            Dim xChildElement As XmlElement = xElement.ChildNodes(0)
            stringModule = xChildElement.GetAttribute("Text")
            For j As Integer = 0 To xChildElement.FirstChild.ChildNodes.Count - 1
                Dim xChild As XmlElement = xChildElement.FirstChild.ChildNodes(j)
                If stringAccessModule = "*" OrElse xChild.GetAttribute("Value").ToUpper = "HELP" OrElse _
                     xChild.GetAttribute("Value").ToUpper = "HOME" OrElse _
                    stringAccessModule.ToUpper.Contains(xChild.GetAttribute("Value").ToUpper) Then
                    stringReturn &= String.Format("<li class='menuItem'><a class='menuLink' {2} {1} {3}>{0}</a>",
                                                    xChild.Attributes("Text").Value, _
                                                    If(xChild.HasAttribute("NavigateUrl"), _
                                                        String.Format("href=""{0}""", xChild.Attributes("NavigateUrl").Value), _
                                                        "href=""#"""),
                                                    If(xChild.GetAttribute("Value").ToString() = mid, "class='selected'", ""), _
                         If(xChild.HasAttribute("OnClick"), String.Format("OnClick='{0}'", xChild.Attributes("OnClick").Value), "")
                                                    )
                    Dim xmlFileSub As String = String.Format(Utilities.ModulePath & "/{0}/Menu-" & Common.Common.SystemLanguage.Name & ".xml", xChild.GetAttribute("Value"))
                    stringReturn &= "<ul>" & BuildMenu(xmlFileSub) & "</ul>"
                    stringReturn &= "</li>"
                End If
            Next
            _mylog.WriteLog(_mylog._info, _classPath, method,
                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Return stringReturn
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Return ""
        End Try

    End Function

    Protected Function BuildMenu(ByVal xmlFile As String) As String
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IO.File.Exists(Server.MapPath(xmlFile)) Then
                Return ""
            End If
            If Common.Common.GetUsername = "" Then Return ""
            Dim bCheckPermission As Boolean
            Dim stringReturn As String = ""

            Dim xmlDoc As New XmlDocument
            xmlDoc.Load(Server.MapPath(xmlFile))
            Dim xElement As XmlElement = xmlDoc.DocumentElement.FirstChild

            For i As Integer = 0 To xElement.ChildNodes.Count - 1
                bCheckPermission = True
                Dim xChildElement As XmlElement = xElement.ChildNodes(i)
                If Not bAdmin Then
                    If xChildElement.HasAttribute("CheckPermission") AndAlso xChildElement.HasAttribute("Value") Then
                        If xChildElement.GetAttribute("CheckPermission").ToUpper = "TRUE" Then
                            If lstFuncId.Contains(xChildElement.GetAttribute("Value")) Then
                                bCheckPermission = True
                            Else
                                bCheckPermission = False
                            End If
                        End If
                    End If
                End If
                Dim childString As String
                If bCheckPermission Then
                    If xChildElement.HasChildNodes Then
                        childString = getChildElement(xChildElement)
                        If childString <> "" Then
                            stringReturn &= String.Format("<li><a {2} href=""{1}"" >{0}</a>",
                                                          xChildElement.GetAttribute("Text"),
                                                          If(xChildElement.HasAttribute("NavigateUrl"),
                                                             String.Format("href=""{0}""", xChildElement.GetAttribute("NavigateUrl")), ""),
                                                          If(xChildElement.HasChildNodes, "class='hsub'", ""))
                            If xChildElement.HasChildNodes Then
                                stringReturn &= childString
                            End If
                            stringReturn &= "</li>"
                        End If
                    Else
                        stringReturn &= String.Format("<li><a href=""{1}"" {2}>{0}</a>",
                                                          xChildElement.GetAttribute("Text"),
                                                          If(xChildElement.HasAttribute("NavigateUrl"),
                                                              xChildElement.GetAttribute("NavigateUrl"), ""),
                                                          If(xChildElement.HasAttribute("OnClick"), String.Format("OnClick='{0}'", xChildElement.Attributes("OnClick").Value), ""))
                        stringReturn &= "</li>"
                    End If
                End If

            Next
            _mylog.WriteLog(_mylog._info, _classPath, method,
                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Return stringReturn
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Return ""
        End Try

    End Function

    Function getChildElement(ByVal xElement As XmlElement) As String
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            xElement = xElement.FirstChild
            Dim stringReturn As String = "<ul>"
            Dim bCheckPermission As Boolean
            Dim childString As String

            For i As Integer = 0 To xElement.ChildNodes.Count - 1
                bCheckPermission = True
                Dim xChildElement As XmlElement = xElement.ChildNodes(i)

                If xChildElement.HasAttribute("IsSeparator") Then
                    stringReturn &= "<li class=""separator""></li>"

                    Continue For
                End If
                If Not bAdmin Then
                    If xChildElement.HasAttribute("CheckPermission") AndAlso xChildElement.HasAttribute("Value") Then
                        If xChildElement.GetAttribute("CheckPermission").ToUpper = "TRUE" Then
                            If lstFuncId.Contains(xChildElement.GetAttribute("Value")) Then
                                bCheckPermission = True
                            Else
                                bCheckPermission = False
                            End If
                        End If
                    End If
                End If
                If bCheckPermission Then

                    If xChildElement.HasChildNodes Then
                        childString = getChildElement(xChildElement)
                        If childString <> "" Then
                            stringReturn &= String.Format("<li><a class='hsub' {1}>{2}{0}</a>",
                                                          xChildElement.Attributes("Text").Value, _
                                                          If(xChildElement.HasAttribute("NavigateUrl"), _
                                                             String.Format("href=""{0}""", xChildElement.Attributes("NavigateUrl").Value), _
                                                             "href=""#"""), _
                                                          If(xChildElement.HasAttribute("ImageUrl"), _
                                                             String.Format("<img src=""{0}"" />", xChildElement.Attributes("ImageUrl").Value), _
                                                             ""))
                            stringReturn &= childString & "</li>"
                        End If
                    Else
                        stringReturn &= String.Format("<li><a {1}>{2}{0}</a></li>",
                                                      xChildElement.Attributes("Text").Value, _
                                                      If(xChildElement.HasAttribute("NavigateUrl"), _
                                                         String.Format("href=""{0}""", xChildElement.Attributes("NavigateUrl").Value), _
                                                         "href=""#"""), _
                                                      If(xChildElement.HasAttribute("ImageUrl"), _
                                                         String.Format("<img src=""{0}"" />", xChildElement.Attributes("ImageUrl").Value), _
                                                         "") _
                                                     )
                    End If
                End If
            Next

            stringReturn &= vbNewLine & "</ul>"
            If stringReturn = "<ul>" & vbNewLine & "</ul>" Then
                stringReturn = ""
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Return stringReturn
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Return ""
        End Try

    End Function

#Region "Notify"
    'Public Sub Load_Noty()
    '    Dim db As New AttendanceRepository
    '    Dim filter As New AttendanceBusiness.ATRegSearchDTO With {
    '        .EmployeeIdName = String.Empty,
    '        .FromDate = Date.Now.FirstDateOfMonth(),
    '        .ToDate = Date.Now.LastDateOfMonth(),
    '        .Status = 0
    '    }
    '    GetDataNotify()
    '    'Dim listApprove = db.GetListWaitingForApprove(LogHelper.CurrentUser.EMPLOYEE_ID, ATConstant.GSIGNCODE_LEAVE, filter)
    '    'listApprove = listApprove.Where(Function(p) p.STATUS = 1).ToList()
    '    'ltrTime_LEAVE.DataSource = listApprove
    '    'ltrTime_LEAVE.DataBind()

    '    'Dim listApproveDMVS = db.GetListWaitingForApprove(LogHelper.CurrentUser.EMPLOYEE_ID, ATConstant.GSIGNCODE_WLEO, filter)
    '    'listApproveDMVS = listApproveDMVS.Where(Function(p) p.STATUS = 1).ToList()
    '    'ltrWLEO.DataSource = listApproveDMVS
    '    'ltrWLEO.DataBind()

    '    'ltrNumberRocord.Text = listApprove.Count + listApproveDMVS.Count
    'End Sub

    'Private Sub btnNotiTimer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNotiTimer.Click
    '    'Load_Noty()
    'End Sub

    Private Sub LoadConfig()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            contractRemind = CommonConfig.ReminderContractDays
            birthdayRemind = CommonConfig.ReminderBirthdayDays
            probationRemind = CommonConfig.ReminderProbation

            workingRemind = CommonConfig.ReminderWorking
            terminateRemind = CommonConfig.ReminderTerminate
            terminateDebtRemind = CommonConfig.ReminderTerminateDebt
            noPaperRemind = CommonConfig.ReminderNoPaper
            visaRemind = CommonConfig.ReminderVisa

            worPermitRemind = CommonConfig.ReminderLabor
            certificateRemind = CommonConfig.ReminderCertificate

            If probationRemind <> 0 Then
                dProbationRemind.Visible = True
            Else
                dProbationRemind.Visible = False
            End If

            If contractRemind <> 0 Then
                dcontractRemind.Visible = True
            Else
                dcontractRemind.Visible = False
            End If

            If birthdayRemind <> 0 Then
                dbirthdayRemind.Visible = True
            Else
                dbirthdayRemind.Visible = False
            End If

            If terminateRemind <> 0 Then
                dterminateRemind.Visible = True
            Else
                dterminateRemind.Visible = False
            End If

            If noPaperRemind <> 0 Then
                dnoPaperRemind.Visible = True
            Else
                dnoPaperRemind.Visible = False
            End If

            'If visaRemind <> 0 Then
            '    dvisaRemind.Visible = True
            'Else
            '    dvisaRemind.Visible = False
            'End If
            dvisaRemind.Visible = False
            dvisaRemind1.Visible = False

            'If worPermitRemind <> 0 Then
            '    dworPermitRemind.Visible = True
            'Else
            '    dworPermitRemind.Visible = False
            'End If

            dworPermitRemind.Visible = False

            'If certificateRemind <> 0 Then
            '    dcertificateRemind.Visible = True
            'Else
            '    dcertificateRemind.Visible = False
            'End If
            dcertificateRemind.Visible = False
            dworkingRemind1.Visible = False
            GetDataNotify()
            _mylog.WriteLog(_mylog._info, _classPath, method,
                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    Private Sub GetDataNotify()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Using rep As New ProfileDashboardRepository
            Try
                If RemindList Is Nothing OrElse (RemindList IsNot Nothing AndAlso RemindList.Count = 0) Then
                    'RemindList = rep.GetRemind(contractRemind.ToString & "," & _
                    '                           birthdayRemind.ToString & "," & _
                    '                           visaRemind.ToString & "," & _
                    '                           workingRemind.ToString & "," & _
                    '                           terminateRemind.ToString & "," & _
                    '                           terminateDebtRemind.ToString & "," & _
                    '                           noPaperRemind.ToString & "," & _
                    '                           "0," & _
                    '                           "0," & _
                    '                           worPermitRemind.ToString & "," & _
                    '                           certificateRemind.ToString
                    '                           )

                    RemindList = rep.GetRemind(probationRemind.ToString & "," & _
                                               contractRemind.ToString & "," & _
                                               birthdayRemind.ToString & "," & _
                                               terminateRemind.ToString & "," & _
                                               noPaperRemind.ToString
                                               )
                    'For Each item In RemindList
                    '    item.REMIND_NAME = Translate(item.REMIND_NAME)
                    'Next
                End If
                'Nhân viên sắp hết hạn hợp đồng
                Dim listApproveDMVS = From p In RemindList Where p.REMIND_TYPE = 1
                ltrWLEO.DataSource = listApproveDMVS
                ltrWLEO.DataBind()
                lblWLEO.Text = Utilities.ObjToInt(listApproveDMVS.Count)
                'Nhân viên sắp tới sinh nhật
                Dim listApprove = From p In RemindList Where p.REMIND_TYPE = 2
                ltrTime_LEAVE.DataSource = listApprove
                ltrTime_LEAVE.DataBind()
                lblTime_LEAVE.Text = Utilities.ObjToInt(listApprove.Count)
                'Chưa nộp đủ giấy tờ khi tiếp nhận
                Dim listGiayTo = From p In RemindList Where p.REMIND_TYPE = 16
                ltrTime_GiayTo.DataSource = listGiayTo
                ltrTime_GiayTo.DataBind()
                lblTime_GiayTo.Text = Utilities.ObjToInt(listGiayTo.Count)
                'Hết hạn Visa
                'Dim listVisa = From p In RemindList Where p.REMIND_TYPE = 5
                'ltrTime_Visa.DataSource = listVisa
                'ltrTime_Visa.DataBind()
                'lblTime_Visa.Text = Utilities.ObjToInt(listVisa.Count)
                'Het han to trinh - Thay đổi thông tin nhân sự
                'Dim listToTrinh = From p In RemindList Where p.REMIND_TYPE = 13 And p.REMIND_NAME = "Thay đổi thông tin nhân sự"
                'ltrTime_ToTrinh.DataSource = listToTrinh
                'ltrTime_ToTrinh.DataBind()
                'lblTime_ToTrinh.Text = Utilities.ObjToInt(listToTrinh.Count)
                'Tờ trình chưa phê duyệt
                'Dim listToTrinhPheDuyet = From p In RemindList Where p.REMIND_TYPE = 13 And p.REMIND_NAME = "Tờ trình chưa phê duyệt"
                'ltrTime_ToTrinhPheDuyet.DataSource = listToTrinhPheDuyet
                'ltrTime_ToTrinhPheDuyet.DataBind()
                'lblTime_ToTrinhPheDuyet.Text = Utilities.ObjToInt(listToTrinhPheDuyet.Count)
                'Giấy phép lao động
                'Dim listGPLD = From p In RemindList Where p.REMIND_TYPE = 19
                'ltrTime_GiayPhepLaoDong.DataSource = listGPLD
                'ltrTime_GiayPhepLaoDong.DataBind()
                'lblTime_GiayPhepLaoDong.Text = Utilities.ObjToInt(listGPLD.Count)
                'Chung chi lao dong
                'Dim listChungChi = From p In RemindList Where p.REMIND_TYPE = 20
                'ltrTime_ChungChi.DataSource = listChungChi
                'ltrTime_ChungChi.DataBind()
                'lblTime_ChungChi.Text = Utilities.ObjToInt(listChungChi.Count)
                'nghi viec trong thang
                Dim listNghiViec = From p In RemindList Where p.REMIND_TYPE = 14
                ltrTime_NghiViec.DataSource = listNghiViec
                ltrTime_NghiViec.DataBind()
                lblTime_NghiViec.Text = Utilities.ObjToInt(listNghiViec.Count)
                'het han thu viec
                Dim listProbation = From p In RemindList Where p.REMIND_TYPE = 20
                ltrProbation.DataSource = listProbation
                ltrProbation.DataBind()
                lblProbation.Text = Utilities.ObjToInt(listProbation.Count)
                ltrNumberRocord.Text = RemindList.Count
                _mylog.WriteLog(_mylog._info, _classPath, method,
                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Catch ex As Exception
                _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            End Try
        End Using

    End Sub


#End Region

End Class
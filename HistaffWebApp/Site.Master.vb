Imports Framework.UI
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

    Dim approveRemind As Integer
    Dim approveHDLDRemind As Integer
    Dim maternitiRemind As Integer
    Dim retirementRemind As Integer
    Dim noneSalaryRemind As Integer

    Dim noneEmpDtlFamily As Integer

    Dim workingRemind As Integer
    Dim terminateRemind As Integer
    Dim terminateDebtRemind As Integer
    Dim noPaperRemind As Integer
    Dim certificateRemind As Integer

    Dim visaRemind As Integer
    Dim identifyRemind As Integer
    Dim passportRemind As Integer
    Dim worPermitRemind As Integer
    Dim licenseRemind As Integer
    Dim approveTHHDRemind As Integer

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
            certificateRemind = CommonConfig.ReminderCertificate
            approveRemind = CommonConfig.ReminderApproveDays
            approveHDLDRemind = CommonConfig.ReminderApproveHDLDDays
            maternitiRemind = CommonConfig.ReminderMaternitiDays
            retirementRemind = CommonConfig.ReminderRetirementDays
            noneSalaryRemind = CommonConfig.ReminderNoneSalaryDays
            'noneExpiredCertificateRemind = CommonConfig.ReminderExpiredCertificate
            'noneBIRTHDAY_LD = CommonConfig.ReminderBIRTHDAY_LD
            'noneConcurrently = CommonConfig.ReminderConcurrently
            noneEmpDtlFamily = CommonConfig.ReminderEmpDtlFamily

            visaRemind = CommonConfig.ReminderVisa
            identifyRemind = CommonConfig.ReminderIdentify
            passportRemind = CommonConfig.ReminderPassPort
            worPermitRemind = CommonConfig.ReminderLabor 'giấy phép lao động
            licenseRemind = CommonConfig.ReminderWorkPer 'giấy phép hành nghề
            approveTHHDRemind = CommonConfig.ReminderApproveTHHDDays 'tạm hoãn hợp đồng lao động

            If approveRemind <> 0 Then
                dApproveRemind.Visible = True
            Else
                dApproveRemind.Visible = False
            End If
            If approveHDLDRemind <> 0 Then
                dApproveHDLDRemind.Visible = True
            Else
                dApproveHDLDRemind.Visible = False
            End If
            If approveTHHDRemind <> 0 Then
                dApproveTHHDRemind.Visible = True
            Else
                dApproveTHHDRemind.Visible = False
            End If
            If maternitiRemind <> 0 Then
                dMaternitiRemind.Visible = True
            Else
                dMaternitiRemind.Visible = False
            End If
            If retirementRemind <> 0 Then
                dRetirementRemind.Visible = True
            Else
                dRetirementRemind.Visible = False
            End If
            If noneSalaryRemind <> 0 Then
                dNoneSalaryRemind.Visible = True
            Else
                dNoneSalaryRemind.Visible = False
            End If
            'If noneExpiredCertificateRemind <> 0 Then
            '    dExpiredCertificateRemind.Visible = True
            'Else
            '    dExpiredCertificateRemind.Visible = False
            'End If
            'If noneBIRTHDAY_LD <> 0 Then
            '    dBIRTHDAY_LD.Visible = True
            'Else
            '    dBIRTHDAY_LD.Visible = False
            'End If
            'If noneConcurrently <> 0 Then
            '    dConcurrently.Visible = True
            'Else
            '    dConcurrently.Visible = False
            'End If
            If noneEmpDtlFamily <> 0 Then
                dEmpDtlFamily.Visible = True
            Else
                dEmpDtlFamily.Visible = False
            End If
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

            If visaRemind <> 0 Then
                dvisaRemind.Visible = True
            Else
                dvisaRemind.Visible = False
            End If

            If identifyRemind <> 0 Then
                dRemindIdentify.Visible = True
            Else
                dRemindIdentify.Visible = False
            End If
            If passportRemind <> 0 Then
                dRemindPassport.Visible = True
            Else
                dRemindPassport.Visible = False
            End If

            If licenseRemind <> 0 Then
                dRemindlicense.Visible = True
            Else
                dRemindlicense.Visible = False
            End If

            If worPermitRemind <> 0 Then
                dworPermitRemind.Visible = True
            Else
                dworPermitRemind.Visible = False
            End If

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

                    RemindList = rep.GetRemind(probationRemind.ToString & "," &
                                               contractRemind.ToString & "," &
                                               birthdayRemind.ToString & "," &
                                               terminateRemind.ToString & "," &
                                               noPaperRemind.ToString & "," &
                                               approveRemind.ToString & "," &
                                               approveHDLDRemind.ToString & "," &
                                               approveTHHDRemind.ToString & "," &
                                               maternitiRemind.ToString & "," &
                                               retirementRemind.ToString & "," &
                                               noneSalaryRemind.ToString & "," &
                                               noneEmpDtlFamily.ToString & "," &
                                               visaRemind.ToString & "," &
                                               identifyRemind.ToString & "," &
                                               passportRemind.ToString & "," &
                                               worPermitRemind.ToString & "," &
                                               licenseRemind.ToString)
                    'For Each item In RemindList
                    '    item.REMIND_NAME = Translate(item.REMIND_NAME)
                    'Next
                End If
                'Nhân viên đến hạn bổ nhiệm lại chức vụ
                Dim listApproveNVBN = From p In RemindList Where p.REMIND_TYPE = 21
                ltrApprove.DataSource = listApproveNVBN
                ltrApprove.DataBind()
                lblApprove.Text = Utilities.ObjToInt(listApproveNVBN.Count)

                'Nhân viên đến hạn ký lại HDLD
                Dim listApproveHDLD = From p In RemindList Where p.REMIND_TYPE = 22
                ltrApproveHDLD.DataSource = listApproveHDLD
                ltrApproveHDLD.DataBind()
                lblApproveHDLD.Text = Utilities.ObjToInt(listApproveHDLD.Count)



                'Nhân viên nghỉ thai sản đi làm lại
                Dim listMaterniti = From p In RemindList Where p.REMIND_TYPE = 24
                ltrMaterniti.DataSource = listMaterniti
                ltrMaterniti.DataBind()
                lblMaterniti.Text = Utilities.ObjToInt(listMaterniti.Count)

                'Nhân viên đến tuổi nghỉ hưu
                Dim listRetirement = From p In RemindList Where p.REMIND_TYPE = 25
                ltrRetirement.DataSource = listRetirement
                ltrRetirement.DataBind()
                lblRetirement.Text = Utilities.ObjToInt(listRetirement.Count)

                'Nhân viên nghỉ không lương đi làm lại
                Dim listNoneSalary = From p In RemindList Where p.REMIND_TYPE = 26
                ltrNoneSalary.DataSource = listNoneSalary
                ltrNoneSalary.DataBind()
                lblNoneSalary.Text = Utilities.ObjToInt(listNoneSalary.Count)

                'Nhân viên sắp hết hạn chứng chỉ
                'Dim listExpiredCertificate = From p In RemindList Where p.REMIND_TYPE = 27
                'ltrExpiredCertificate.DataSource = listExpiredCertificate
                'ltrExpiredCertificate.DataBind()
                'lbExpiredCertificate.Text = Utilities.ObjToInt(listExpiredCertificate.Count)

                ''Sinh nhật lãnh đạo
                'Dim listBIRTHDAY_LD = From p In RemindList Where p.REMIND_TYPE = 28
                'ltrBIRTHDAY_LD.DataSource = listBIRTHDAY_LD
                'ltrBIRTHDAY_LD.DataBind()
                'lbBIRTHDAY_LD.Text = Utilities.ObjToInt(listBIRTHDAY_LD.Count)

                'Hết hiệu lực kiêm nhiệm
                'Dim listConcurrently = From p In RemindList Where p.REMIND_TYPE = 29
                'ltrConcurrently.DataSource = listConcurrently
                'ltrConcurrently.DataBind()
                'lbConcurrently.Text = Utilities.ObjToInt(listConcurrently.Count)

                'Hết hạn giảm trừ gia cảnh
                Dim listEmpDtlFamily = From p In RemindList Where p.REMIND_TYPE = 30
                ltrEmpDtlFamily.DataSource = listEmpDtlFamily
                ltrEmpDtlFamily.DataBind()
                lbEmpDtlFamily.Text = Utilities.ObjToInt(listEmpDtlFamily.Count)

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
                Dim listVisa = From p In RemindList Where p.REMIND_TYPE = 5
                ltrTime_Visa.DataSource = listVisa
                ltrTime_Visa.DataBind()
                lblTime_Visa.Text = Utilities.ObjToInt(listVisa.Count)

                'Hết hạn CMND
                Dim listIdentify = From p In RemindList Where p.REMIND_TYPE = 31
                ltrRemindIdentify.DataSource = listIdentify
                ltrRemindIdentify.DataBind()
                lbRemindIdentify.Text = Utilities.ObjToInt(listIdentify.Count)

                'Hết hạn hộ chiếu
                Dim listPassport = From p In RemindList Where p.REMIND_TYPE = 32
                ltrRemindPassport.DataSource = listPassport
                ltrRemindPassport.DataBind()
                lbRemindPassport.Text = Utilities.ObjToInt(listPassport.Count)

                'Giấy phép lao động
                Dim listGPLD = From p In RemindList Where p.REMIND_TYPE = 19
                ltrTime_GiayPhepLaoDong.DataSource = listGPLD
                ltrTime_GiayPhepLaoDong.DataBind()
                lblTime_GiayPhepLaoDong.Text = Utilities.ObjToInt(listGPLD.Count)

                'Hết hạn giấy phép hành nghề
                Dim listlicense = From p In RemindList Where p.REMIND_TYPE = 33
                ltrRemindlicense.DataSource = listlicense
                ltrRemindlicense.DataBind()
                lbRemindlicense.Text = Utilities.ObjToInt(listlicense.Count)

                'Nhân viên hết hạn tạm hoãn HD
                Dim listApproveTHHD = From p In RemindList Where p.REMIND_TYPE = 23
                ltrApproveTHHD.DataSource = listApproveTHHD
                ltrApproveTHHD.DataBind()
                lblApproveTHHD.Text = Utilities.ObjToInt(listApproveTHHD.Count)

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
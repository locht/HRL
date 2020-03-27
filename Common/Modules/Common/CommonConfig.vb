Imports System.Web.Configuration
Imports Common.CommonBusiness

Public Class CommonConfig
    Private Shared Property AppConfig As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")

    Public Shared ReadOnly Property CurrentUser As String
        Get
            Return LogHelper.CurrentUser.USERNAME
        End Get
    End Property

    Public Shared Property ModuleID As SystemConfigModuleID = SystemConfigModuleID.All

#Region "iSecure"
    Public Shared ReadOnly Property dicConfig As Dictionary(Of String, String)
        Get
            If HttpContext.Current.Session("ConfigDictionaryCache" & ModuleID) Is Nothing Then
                Using rep As New CommonRepository
                    HttpContext.Current.Session("ConfigDictionaryCache" & ModuleID) = rep.GetConfig(ModuleID)
                End Using
            End If
            Return HttpContext.Current.Session("ConfigDictionaryCache" & ModuleID)
        End Get
    End Property

    Public Shared ReadOnly Property ListLDAP As List(Of LdapDTO)
        Get
            If HttpContext.Current.Session("ListLDAPCache") Is Nothing Then
                Using rep As New CommonRepository
                    HttpContext.Current.Session("ListLDAPCache") = rep.GetLdap(New LdapDTO)
                End Using
            End If
            Return HttpContext.Current.Session("ListLDAPCache")
        End Get
    End Property

    Public Shared Property SessionTimeout() As Integer
        Get
            Dim web As SessionStateSection
            web = CommonConfig.AppConfig.GetSection("system.web/sessionState")
            Return web.Timeout.TotalMinutes
        End Get
        Set(ByVal value As Integer)
            Dim web As SessionStateSection
            web = CommonConfig.AppConfig.GetSection("system.web/sessionState")
            web.Timeout = New System.TimeSpan(value / 60, value Mod 60, 0)

            'Lưu Timeout cho Form Authentication.
            Dim auth As AuthenticationSection
            auth = CommonConfig.AppConfig.GetSection("system.web/authentication")
            auth.Forms.Timeout = New System.TimeSpan((value * 2) / 60, (value * 2) Mod 60, 0)
        End Set
    End Property

    Public Shared Property SessionWarning() As Integer
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("SessionWarning") Then
                Return 5
            End If
            Return AppConfig.AppSettings.Settings("SessionWarning").Value
        End Get
        Set(ByVal value As Integer)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("SessionWarning") Then
                AppConfig.AppSettings.Settings("SessionWarning").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("SessionWarning", value)
            End If
        End Set
    End Property

    Public Shared Property ActiveTimeout() As Integer
        Get
            If Not dicConfig.ContainsKey("ActiveTimeout") Then
                Return 1
            End If
            Return dicConfig("ActiveTimeout")
        End Get
        Set(ByVal value As Integer)
            If dicConfig.ContainsKey("ActiveTimeout") Then
                dicConfig("ActiveTimeout") = value
            Else
                dicConfig.Add("ActiveTimeout", value)
            End If
        End Set
    End Property

    Public Shared Property MailIsSSL() As Integer
        Get
            If Not dicConfig.ContainsKey("MailIsSSL") Then
                Return 0
            End If
            If dicConfig("MailIsSSL") = "" Then
                Return 0
            End If
            Return dicConfig("MailIsSSL")
        End Get
        Set(ByVal value As Integer)
            If dicConfig.ContainsKey("MailIsSSL") Then
                dicConfig("MailIsSSL") = value
            Else
                dicConfig.Add("MailIsSSL", value)
            End If
        End Set
    End Property

    Public Shared Property MailIsAuthen() As Integer
        Get
            If Not dicConfig.ContainsKey("MailIsAuthen") Then
                Return 0
            End If
            If dicConfig("MailIsAuthen") = "" Then
                Return 0
            End If
            Return dicConfig("MailIsAuthen")
        End Get
        Set(ByVal value As Integer)
            If dicConfig.ContainsKey("MailIsAuthen") Then
                dicConfig("MailIsAuthen") = value
            Else
                dicConfig.Add("MailIsAuthen", value)
            End If
        End Set
    End Property

    Public Shared Property MailFrom() As String
        Get
            If Not dicConfig.ContainsKey("MailFrom") Then
                Return ""
            End If
            Return dicConfig("MailFrom")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("MailFrom") Then
                dicConfig("MailFrom") = value
            Else
                dicConfig.Add("MailFrom", value)
            End If
        End Set
    End Property

    Public Shared Property MailServer() As String
        Get
            If Not dicConfig.ContainsKey("MailServer") Then
                Return ""
            End If
            Return dicConfig("MailServer")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("MailServer") Then
                dicConfig("MailServer") = value
            Else
                dicConfig.Add("MailServer", value)
            End If
        End Set
    End Property

    Public Shared Property MailPort() As String
        Get
            If Not dicConfig.ContainsKey("MailPort") Then
                Return ""
            End If
            Return dicConfig("MailPort")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("MailPort") Then
                dicConfig("MailPort") = value
            Else
                dicConfig.Add("MailPort", value)
            End If
        End Set
    End Property

    Public Shared Property MailAccount() As String
        Get
            If Not dicConfig.ContainsKey("MailAccount") Then
                Return ""
            End If
            Return dicConfig("MailAccount")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("MailAccount") Then
                dicConfig("MailAccount") = value
            Else
                dicConfig.Add("MailAccount", value)
            End If
        End Set
    End Property

    Public Shared Property MailAccountPassword() As String
        Get
            If Not dicConfig.ContainsKey("MailAccountPassword") Then
                Return ""
            End If
            Return dicConfig("MailAccountPassword")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("MailAccountPassword") Then
                dicConfig("MailAccountPassword") = value
            Else
                dicConfig.Add("MailAccountPassword", value)
            End If
        End Set
    End Property

    Public Shared Property PortalPort() As String
        Get
            If Not dicConfig.ContainsKey("PortalPort") Then
                Return ""
            End If
            Return dicConfig("PortalPort")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("PortalPort") Then
                dicConfig("PortalPort") = value
            Else
                dicConfig.Add("PortalPort", value)
            End If
        End Set
    End Property

    Public Shared Property AppPort() As String
        Get
            If Not dicConfig.ContainsKey("AppPort") Then
                Return ""
            End If
            Return dicConfig("AppPort")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("AppPort") Then
                dicConfig("AppPort") = value
            Else
                dicConfig.Add("AppPort", value)
            End If
        End Set
    End Property

    Public Shared Property SendMailPasswordSubject() As String
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("SendMailPasswordSubject") Then
                Return 3
            End If
            Return AppConfig.AppSettings.Settings("SendMailPasswordSubject").Value
        End Get
        Set(ByVal value As String)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("SendMailPasswordSubject") Then
                AppConfig.AppSettings.Settings("SendMailPasswordSubject").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("SendMailPasswordSubject", value)
            End If
        End Set
    End Property

    Public Shared Property SendMailPasswordContent() As String
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("SendMailPasswordContent") Then
                Return 3
            End If
            Return AppConfig.AppSettings.Settings("SendMailPasswordContent").Value
        End Get
        Set(ByVal value As String)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("SendMailPasswordContent") Then
                AppConfig.AppSettings.Settings("SendMailPasswordContent").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("SendMailPasswordContent", value)
            End If
        End Set
    End Property

    Public Shared Property MaxNumberLoginFail() As Integer
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("MaxNumberLoginFail") Then
                Return 3
            End If
            Return AppConfig.AppSettings.Settings("MaxNumberLoginFail").Value
        End Get
        Set(ByVal value As Integer)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("MaxNumberLoginFail") Then
                AppConfig.AppSettings.Settings("MaxNumberLoginFail").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("MaxNumberLoginFail", value)
            End If
        End Set
    End Property

    Public Shared Property PasswordLength() As Integer
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordLength") Then
                Return 4
            End If
            Return AppConfig.AppSettings.Settings("PasswordLength").Value
        End Get
        Set(ByVal value As Integer)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordLength") Then
                AppConfig.AppSettings.Settings("PasswordLength").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("PasswordLength", value)
            End If
        End Set
    End Property

    Public Shared Property PasswordUpperChar() As Integer
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordUpperChar") Then
                Return 0
            End If
            Return AppConfig.AppSettings.Settings("PasswordUpperChar").Value
        End Get
        Set(ByVal value As Integer)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordUpperChar") Then
                AppConfig.AppSettings.Settings("PasswordUpperChar").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("PasswordUpperChar", value)
            End If
        End Set
    End Property

    Public Shared Property PasswordLowerChar() As Integer
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordLowerChar") Then
                Return 0
            End If
            Return AppConfig.AppSettings.Settings("PasswordLowerChar").Value
        End Get
        Set(ByVal value As Integer)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordLowerChar") Then
                AppConfig.AppSettings.Settings("PasswordLowerChar").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("PasswordLowerChar", value)
            End If
        End Set
    End Property

    Public Shared Property PasswordNumberChar() As Integer
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordNumberChar") Then
                Return 0
            End If
            Return AppConfig.AppSettings.Settings("PasswordNumberChar").Value
        End Get
        Set(ByVal value As Integer)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordNumberChar") Then
                AppConfig.AppSettings.Settings("PasswordNumberChar").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("PasswordNumberChar", value)
            End If
        End Set
    End Property

    Public Shared Property PasswordSpecialChar() As Integer
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordSpecialChar") Then
                Return 0
            End If
            Return AppConfig.AppSettings.Settings("PasswordSpecialChar").Value
        End Get
        Set(ByVal value As Integer)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordSpecialChar") Then
                AppConfig.AppSettings.Settings("PasswordSpecialChar").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("PasswordSpecialChar", value)
            End If
        End Set
    End Property

    Public Shared Property PasswordNotifyDays() As Integer
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordNotifyDays") Then
                Return 10
            End If
            Return AppConfig.AppSettings.Settings("PasswordNotifyDays").Value
        End Get
        Set(ByVal value As Integer)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordNotifyDays") Then
                AppConfig.AppSettings.Settings("PasswordNotifyDays").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("PasswordNotifyDays", value)
            End If
        End Set
    End Property

    Public Shared Property PasswordNotifyCount() As Integer
        Get
            If Not AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordNotifyCount") Then
                Return 1
            End If
            Return AppConfig.AppSettings.Settings("PasswordNotifyCount").Value
        End Get
        Set(ByVal value As Integer)
            If AppConfig.AppSettings.Settings.AllKeys.Contains("PasswordNotifyCount") Then
                AppConfig.AppSettings.Settings("PasswordNotifyCount").Value = value
            Else
                AppConfig.AppSettings.Settings.Add("PasswordNotifyCount", value)
            End If
        End Set
    End Property

#End Region

#Region "iProfile"
    Public Shared Property ReminderEmail() As String
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.Email, Integer)) Then
                Return 0
            End If
            Return dicReminderConfig(CType(RemindConfigType.Email, Integer))
        End Get
        Set(ByVal value As String)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.Email, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.Email, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.Email, Integer), value)
            End If
        End Set
    End Property

    Public Shared ReadOnly Property dicReminderConfig As Dictionary(Of Integer, String)
        Get
            If HttpContext.Current.Session("ConfigReminderDictionaryCache") Is Nothing Then
                Using rep As New CommonRepository
                    HttpContext.Current.Session("ConfigReminderDictionaryCache") = rep.GetReminderConfig(CurrentUser)
                End Using
            End If
            Return HttpContext.Current.Session("ConfigReminderDictionaryCache")
        End Get
    End Property
    Public Shared Property ReminderApproveHDLDDays() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ApproveHDLD, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ApproveHDLD, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ApproveHDLD, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ApproveHDLD, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ApproveHDLD, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderApproveTHHDDays() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ApprovetTHHD, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ApprovetTHHD, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ApprovetTHHD, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ApprovetTHHD, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ApprovetTHHD, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderMaternitiDays() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.Materniti, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.Materniti, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.Materniti, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.Materniti, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.Materniti, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderRetirementDays() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.Retirement, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.Retirement, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.Retirement, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.Retirement, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.Retirement, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderNoneSalaryDays() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.NoneSalary, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.NoneSalary, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.NoneSalary, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.NoneSalary, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.NoneSalary, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderExpiredCertificate() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpiredCertificate, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ExpiredCertificate, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpiredCertificate, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ExpiredCertificate, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ExpiredCertificate, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderBIRTHDAY_LD() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.BIRTHDAY_LD, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.BIRTHDAY_LD, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.BIRTHDAY_LD, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.BIRTHDAY_LD, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.BIRTHDAY_LD, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderConcurrently() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.Concurrently, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.Concurrently, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.Concurrently, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.Concurrently, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.Concurrently, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderEmpDtlFamily() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.EmpDtlFamily, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.EmpDtlFamily, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.EmpDtlFamily, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.EmpDtlFamily, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.EmpDtlFamily, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderApproveDays() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.Approve, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.Approve, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.Approve, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.Approve, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.Approve, Integer), value)
            End If

        End Set
    End Property
    Public Shared Property ReminderContractDays() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.Birthday, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.Contract, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.Contract, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.Contract, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.Contract, Integer), value)
            End If

        End Set
    End Property

    Public Shared Property ReminderBirthdayDays() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.Birthday, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.Birthday, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.Birthday, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.Birthday, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.Birthday, Integer), value)
            End If

        End Set
    End Property

    Public Shared Property ReminderVisa() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireVisa, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ExpireVisa, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireVisa, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ExpireVisa, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ExpireVisa, Integer), value)
            End If
        End Set
    End Property

    Public Shared Property ReminderPassPort() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpirePassPort, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ExpirePassPort, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpirePassPort, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ExpirePassPort, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ExpirePassPort, Integer), value)
            End If
        End Set
    End Property

    Public Shared Property ReminderIdentify() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireIdentify, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ExpireIdentify, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireIdentify, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ExpireIdentify, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ExpireIdentify, Integer), value)
            End If
        End Set
    End Property

    Public Shared Property ReminderWorkPer() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireWorkPer, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ExpireWorkPer, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireWorkPer, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ExpireWorkPer, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ExpireWorkPer, Integer), value)
            End If
        End Set
    End Property

    Public Shared Property ReminderWorking() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireWorking, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ExpireWorking, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireWorking, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ExpireWorking, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ExpireWorking, Integer), value)
            End If
        End Set
    End Property

    Public Shared Property ReminderTerminate() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireTerminate, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ExpireTerminate, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireTerminate, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ExpireTerminate, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ExpireTerminate, Integer), value)
            End If
        End Set
    End Property

    Public Shared Property ReminderTerminateDebt() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireTerminateDebt, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ExpireTerminateDebt, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireTerminateDebt, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ExpireTerminateDebt, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ExpireTerminateDebt, Integer), value)
            End If
        End Set
    End Property

    Public Shared Property ReminderNoPaper() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireNoPaper, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ExpireNoPaper, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireNoPaper, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ExpireNoPaper, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ExpireNoPaper, Integer), value)
            End If
        End Set
    End Property

    Public Shared Property ReminderLabor() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireLabor, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ExpireLabor, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireLabor, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ExpireLabor, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ExpireLabor, Integer), value)
            End If
        End Set
    End Property

    Public Shared Property ReminderCertificate() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireCertificate, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.ExpireCertificate, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.ExpireCertificate, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.ExpireCertificate, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.ExpireCertificate, Integer), value)
            End If
        End Set
    End Property

    Public Shared Property ReminderProbation() As Integer
        Get
            If Not dicReminderConfig.ContainsKey(CType(RemindConfigType.Probation, Integer)) Then
                Return 0
            End If
            Return Integer.Parse("0" & dicReminderConfig(CType(RemindConfigType.Probation, Integer)).ToString)
        End Get
        Set(ByVal value As Integer)
            If dicReminderConfig.ContainsKey(CType(RemindConfigType.Probation, Integer)) Then
                dicReminderConfig(CType(RemindConfigType.Probation, Integer)) = value
            Else
                dicReminderConfig.Add(CType(RemindConfigType.Probation, Integer), value)
            End If
        End Set
    End Property

    Public Shared Sub GetReminderConfigFromDatabase()
        Using rep As New CommonRepository
            HttpContext.Current.Session("ConfigReminderDictionaryCache") = rep.GetReminderConfig(CurrentUser)
        End Using
    End Sub
#End Region

#Region "iPayroll"

    Public Shared Property PA_BASIC_SAL() As Boolean
        Get
            If Not dicConfig.ContainsKey("PA_BASIC_SAL") Then
                Return True
            End If
            Return dicConfig("PA_BASIC_SAL")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("PA_BASIC_SAL") Then
                dicConfig("PA_BASIC_SAL") = value
            Else
                dicConfig.Add("PA_BASIC_SAL", value)
            End If
        End Set
    End Property

    Public Shared Property PA_SOFT_SAL() As Boolean
        Get
            If Not dicConfig.ContainsKey("PA_SOFT_SAL") Then
                Return False
            End If
            Return dicConfig("PA_SOFT_SAL")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("PA_SOFT_SAL") Then
                dicConfig("PA_SOFT_SAL") = value
            Else
                dicConfig.Add("PA_SOFT_SAL", value)
            End If
        End Set
    End Property

    Public Shared Property PA_COEFFICIENT() As Boolean
        Get
            If Not dicConfig.ContainsKey("PA_COEFFICIENT") Then
                Return False
            End If
            Return dicConfig("PA_COEFFICIENT")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("PA_COEFFICIENT") Then
                dicConfig("PA_COEFFICIENT") = value
            Else
                dicConfig.Add("PA_COEFFICIENT", value)
            End If
        End Set
    End Property

    Public Shared Property PA_OTHER_SAL As Boolean
        Get
            If Not dicConfig.ContainsKey("PA_OTHER_SAL") Then
                Return False
            End If
            Return dicConfig("PA_OTHER_SAL")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("PA_OTHER_SAL") Then
                dicConfig("PA_OTHER_SAL") = value
            Else
                dicConfig.Add("PA_OTHER_SAL", value)
            End If
        End Set
    End Property

    Public Shared Property PA_BASIC_UP_STEP() As Boolean
        Get
            If Not dicConfig.ContainsKey("PA_BASIC_UP_STEP") Then
                Return False
            End If
            Return dicConfig("PA_BASIC_UP_STEP")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("PA_BASIC_UP_STEP") Then
                dicConfig("PA_BASIC_UP_STEP") = value
            Else
                dicConfig.Add("PA_BASIC_UP_STEP", value)
            End If
        End Set
    End Property

    Public Shared Property PA_SOFT_UP_STEP() As Boolean
        Get
            If Not dicConfig.ContainsKey("PA_SOFT_UP_STEP") Then
                Return False
            End If
            Return dicConfig("PA_SOFT_UP_STEP")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("PA_SOFT_UP_STEP") Then
                dicConfig("PA_SOFT_UP_STEP") = value
            Else
                dicConfig.Add("PA_SOFT_UP_STEP", value)
            End If
        End Set
    End Property

    Public Shared Property PA_MIN_SAL() As Boolean
        Get
            If Not dicConfig.ContainsKey("PA_MIN_SAL") Then
                Return False
            End If
            Return dicConfig("PA_MIN_SAL")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("PA_MIN_SAL") Then
                dicConfig("PA_MIN_SAL") = value
            Else
                dicConfig.Add("PA_MIN_SAL", value)
            End If
        End Set
    End Property

    Public Shared Property PA_MAX_SAL() As Boolean
        Get
            If Not dicConfig.ContainsKey("PA_MAX_SAL") Then
                Return False
            End If
            Return dicConfig("PA_MAX_SAL")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("PA_MAX_SAL") Then
                dicConfig("PA_MAX_SAL") = value
            Else
                dicConfig.Add("PA_MAX_SAL", value)
            End If
        End Set
    End Property

    Public Shared Property PA_CURRENCY() As String
        Get
            If Not dicConfig.ContainsKey("PA_CURRENCY") Then
                Return ""
            End If
            Return dicConfig("PA_CURRENCY")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("PA_CURRENCY") Then
                dicConfig("PA_CURRENCY") = value
            Else
                dicConfig.Add("PA_CURRENCY", value)
            End If
        End Set
    End Property

#End Region

#Region "iTime"

    ' Ngày làm mới phép năm
    Public Shared Property AT_AL_DATE_RESET() As String
        Get
            If Not dicConfig.ContainsKey("AT_AL_DATE_RESET") Then
                Return "31/03/2013"
            End If
            Return dicConfig("AT_AL_DATE_RESET")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("AT_AL_DATE_RESET") Then
                dicConfig("AT_AL_DATE_RESET") = value
            Else
                dicConfig.Add("AT_AL_DATE_RESET", value)
            End If
        End Set
    End Property

    'Số phép năm được hưởng 1 năm
    Public Shared Property AT_AL_IN_YEAR As String
        Get
            If Not dicConfig.ContainsKey("AT_AL_IN_YEAR") Then
                Return 12
            End If
            Return dicConfig("AT_AL_IN_YEAR")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("AT_AL_IN_YEAR") Then
                dicConfig("AT_AL_IN_YEAR") = value
            Else
                dicConfig.Add("AT_AL_IN_YEAR", value)
            End If
        End Set
    End Property

    'Số phép được phép ứng trước
    Public Shared Property AT_AL_ADVANCE_TAKEN As String
        Get
            If Not dicConfig.ContainsKey("AT_AL_ADVANCE_TAKEN") Then
                Return 0
            End If
            Return dicConfig("AT_AL_ADVANCE_TAKEN")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("AT_AL_ADVANCE_TAKEN") Then
                dicConfig("AT_AL_ADVANCE_TAKEN") = value
            Else
                dicConfig.Add("AT_AL_ADVANCE_TAKEN", value)
            End If
        End Set
    End Property

    'Số năm thâm niên được tăng phép
    Public Shared Property AT_AL_SENIORITY As String
        Get
            If Not dicConfig.ContainsKey("AT_AL_SENIORITY") Then
                Return 1
            End If
            Return dicConfig("AT_AL_SENIORITY")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("AT_AL_SENIORITY") Then
                dicConfig("AT_AL_SENIORITY") = value
            Else
                dicConfig.Add("AT_AL_SENIORITY", value)
            End If
        End Set
    End Property

    'Số phép tăng theo thâm niên
    Public Shared Property AT_AL_SENIORITY_UP As String
        Get
            If Not dicConfig.ContainsKey("AT_AL_SENIORITY_UP") Then
                Return 1
            End If
            Return dicConfig("AT_AL_SENIORITY_UP")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("AT_AL_SENIORITY_UP") Then
                dicConfig("AT_AL_SENIORITY_UP") = value
            Else
                dicConfig.Add("AT_AL_SENIORITY_UP", value)
            End If
        End Set
    End Property

    'Đăng ký phép trong thời gian thử việc
    Public Shared Property AT_AL_REG_PROBA As Boolean
        Get
            If Not dicConfig.ContainsKey("AT_AL_REG_PROBA") Then
                Return False
            End If
            Return dicConfig("AT_AL_REG_PROBA")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("AT_AL_REG_PROBA") Then
                dicConfig("AT_AL_REG_PROBA") = value
            Else
                dicConfig.Add("AT_AL_REG_PROBA", value)
            End If
        End Set
    End Property

    ' Tính theo tháng
    Public Shared Property AT_AL_CAL_MONTH As Boolean
        Get
            If Not dicConfig.ContainsKey("AT_AL_CAL_MONTH") Then
                Return False
            End If
            Return dicConfig("AT_AL_CAL_MONTH")
        End Get
        Set(ByVal value As Boolean)
            If dicConfig.ContainsKey("AT_AL_CAL_MONTH") Then
                dicConfig("AT_AL_CAL_MONTH") = value
            Else
                dicConfig.Add("AT_AL_CAL_MONTH", value)
            End If
        End Set
    End Property

    ' Tính theo tháng
    Public Shared Property AT_AL_CTRACT_TYPE As String
        Get
            If Not dicConfig.ContainsKey("AT_AL_CTRACT_TYPE") Then
                Return ""
            End If
            Return dicConfig("AT_AL_CTRACT_TYPE")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("AT_AL_CTRACT_TYPE") Then
                dicConfig("AT_AL_CTRACT_TYPE") = value
            Else
                dicConfig.Add("AT_AL_CTRACT_TYPE", value)
            End If
        End Set
    End Property

    ' Số giờ OT 1 năm
    Public Shared Property AT_AL_OT_YEAR_NUMBER As String
        Get
            If Not dicConfig.ContainsKey("AT_AL_OT_YEAR_NUMBER") Then
                Return 0
            End If
            Return dicConfig("AT_AL_OT_YEAR_NUMBER")
        End Get
        Set(ByVal value As String)
            If dicConfig.ContainsKey("AT_AL_OT_YEAR_NUMBER") Then
                dicConfig("AT_AL_OT_YEAR_NUMBER") = value
            Else
                dicConfig.Add("AT_AL_OT_YEAR_NUMBER", value)
            End If
        End Set
    End Property
#End Region

    Public Shared Function SaveChanges(Optional ByVal isSaveConfig As Boolean = True)
        Try
            Using rep As New CommonRepository
                rep.UpdateConfig(dicConfig, ModuleID)
                If isSaveConfig Then
                    AppConfig.Save()
                End If
            End Using
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function SaveConfig()
        Try
            AppConfig.Save()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function SaveConfigHost()
        Try
            Using rep As New CommonRepository
                rep.UpdateConfig(dicConfig, ModuleID)
            End Using
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function SaveReminderPerUser() As Boolean
        Try
            Using rep As New CommonRepository
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.Contract, Integer), ReminderContractDays)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.Birthday, Integer), ReminderBirthdayDays)
                '--------
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpireVisa, Integer), ReminderVisa)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpirePassPort, Integer), ReminderPassPort)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpireIdentify, Integer), ReminderIdentify)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpireWorkPer, Integer), ReminderWorkPer)
                '----------
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpireTerminate, Integer), ReminderTerminate)
                'rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpireTerminateDebt, Integer), ReminderTerminateDebt)
                'rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpireWorking, Integer), ReminderWorking)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpireNoPaper, Integer), ReminderNoPaper)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.Probation, Integer), ReminderProbation)
                'rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpireCertificate, Integer), ReminderCertificate)
                'rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpireLabor, Integer), ReminderLabor)

                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.Approve, Integer), ReminderApproveDays)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ApproveHDLD, Integer), ReminderApproveHDLDDays)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ApprovetTHHD, Integer), ReminderApproveTHHDDays)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.Materniti, Integer), ReminderMaternitiDays)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.Retirement, Integer), ReminderRetirementDays)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.NoneSalary, Integer), ReminderNoneSalaryDays)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.ExpiredCertificate, Integer), ReminderExpiredCertificate)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.BIRTHDAY_LD, Integer), ReminderBIRTHDAY_LD)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.Concurrently, Integer), ReminderConcurrently)
                rep.SetReminderConfig(CurrentUser, CType(RemindConfigType.EmpDtlFamily, Integer), ReminderEmpDtlFamily)
                rep.SetReminderConfig(CurrentUser, RemindConfigType.Email, ReminderEmail)
            End Using

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Sub GetConfigFromDatabase()
        Using rep As New CommonRepository
            HttpContext.Current.Session("ConfigDictionaryCache" & ModuleID) = rep.GetConfig(ModuleID)
        End Using
    End Sub

End Class

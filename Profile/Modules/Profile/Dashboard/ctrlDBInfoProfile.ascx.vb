Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlDBInfoProfile
    Inherits Common.CommonView

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>MustAuthorize</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Property MustAuthorize As Boolean = False

    ''' <summary>
    ''' birthdayRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim birthdayRemind As Integer

    ''' <summary>
    ''' contractRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim contractRemind As Integer

    ''' <summary>
    ''' workingRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim workingRemind As Integer

    ''' <summary>
    ''' terminateRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim terminateRemind As Integer

    ''' <summary>
    ''' terminateDebtRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim terminateDebtRemind As Integer

    ''' <summary>
    ''' noPaperRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim noPaperRemind As Integer

    ''' <summary>
    ''' visaRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim visaRemind As Integer

    ''' <summary>
    ''' worPermitRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim worPermitRemind As Integer

    ''' <summary>
    ''' certificateRemind
    ''' </summary>
    ''' <remarks></remarks>
    Dim certificateRemind As Integer

    ''' <summary>
    ''' width
    ''' </summary>
    ''' <remarks></remarks>
    Private width As Integer

    ''' <summary>
    ''' height
    ''' </summary>
    ''' <remarks></remarks>
    Private height As Integer

    ''' <summary>
    ''' title
    ''' </summary>
    ''' <remarks></remarks>
    Public title As String = ""

    ''' <summary>
    ''' name
    ''' </summary>
    ''' <remarks>Default : "Năm"</remarks>
    Public name As String = "Năm"

    ''' <summary>
    ''' data
    ''' </summary>
    ''' <remarks></remarks>
    Public data As String ''{name: 'Microsoft Internet Explorer', y: 56.33},\n

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Dashboard" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' InfoList
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InfoList As List(Of StatisticDTO)
        Get
            Return ViewState(Me.ID & "_InfoList")
        End Get

        Set(ByVal value As List(Of StatisticDTO))
            ViewState(Me.ID & "_InfoList") = value
        End Set
    End Property

    ''' <summary>
    ''' RemindList
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RemindList As List(Of ReminderLogDTO)
        Get
            Return PageViewState(Me.ID & "_RemindContractList")
        End Get

        Set(ByVal value As List(Of ReminderLogDTO))
            PageViewState(Me.ID & "_RemindContractList") = value
        End Set
    End Property

    ''' <summary>
    ''' StatisticData
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StatisticData As List(Of StatisticDTO)
        Get
            Return Session(Me.ID & "_StatisticData")
        End Get

        Set(ByVal value As List(Of StatisticDTO))
            Session(Me.ID & "_StatisticData") = value
        End Set
    End Property

    ''' <summary>
    ''' _filter
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _filter As ReminderLogDTO
        Get
            If PageViewState(Me.ID & "_filter") Is Nothing Then
                PageViewState(Me.ID & "_filter") = New ReminderLogDTO
            End If
            Return PageViewState(Me.ID & "_filter")
        End Get

        Set(ByVal value As ReminderLogDTO)
            PageViewState(Me.ID & "_filter") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim resize As Integer = 0
            If Request("resize") IsNot Nothing Then
                resize = CInt(Request("resize"))
            End If

            LoadConfig()

            If Not IsPostBack Or resize = 0 Then
                Refresh(CommonMessage.ACTION_UPDATED)
            Else
                Refresh()
            End If

            GetInforRemind()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            If Not IsPostBack Then
                ViewConfig(RadPane1)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileDashboardRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If InfoList Is Nothing Or Message = CommonMessage.ACTION_UPDATED Then
                InfoList = rep.GetCompanyNewInfo()
            End If
            rep.Dispose()
            lbtnEmpCount.Text = (From p In InfoList Where p.NAME = "EMP_COUNT" Select p.VALUE).FirstOrDefault
            lbtnEmpNew.Text = (From p In InfoList Where p.NAME = "EMP_NEW" Select p.VALUE).FirstOrDefault
            lbtnEmpTer.Text = (From p In InfoList Where p.NAME = "EMP_TER" Select p.VALUE).FirstOrDefault
            lbtnContractNew.Text = (From p In InfoList Where p.NAME = "CONTRACT_NEW" Select p.VALUE).FirstOrDefault
            lbtnTransferNew.Text = (From p In InfoList Where p.NAME = "TRANSFER_NEW" Select p.VALUE).FirstOrDefault
            lbtnTransferMove.Text = (From p In InfoList Where p.NAME = "TRANSFER_MOVE" Select p.VALUE).FirstOrDefault
            lbtnAgeAvg.Text = (From p In InfoList Where p.NAME = "AGE_AVG" Select p.VALUE).FirstOrDefault
            lbtnSeniority.Text = (From p In InfoList Where p.NAME = "SENIORITY_AVG" Select p.VALUE).FirstOrDefault

            'data = String.Empty
            'If StatisticData Is Nothing Or Message = CommonMessage.ACTION_UPDATED Then
            '    StatisticData = rep.GetStatisticSeniority()
            'End If
            'If (From p In StatisticData Where p.VALUE <> 0).Any Then
            '    For index As Integer = 0 To StatisticData.Count - 1
            '        If index = StatisticData.Count - 1 Then
            '            data &= "{name: '" & StatisticData(index).NAME & "', y: " & StatisticData(index).VALUE & "}"
            '        Else
            '            data &= "{name: '" & StatisticData(index).NAME & "', y: " & StatisticData(index).VALUE & "}," & vbNewLine
            '        End If
            '    Next
            'End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

#End Region

#Region "Custom"

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>Load data từ file config</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Private Sub LoadConfig()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            contractRemind = CommonConfig.ReminderContractDays
            birthdayRemind = CommonConfig.ReminderBirthdayDays

            workingRemind = CommonConfig.ReminderWorking
            terminateRemind = CommonConfig.ReminderTerminate
            terminateDebtRemind = CommonConfig.ReminderTerminateDebt
            noPaperRemind = CommonConfig.ReminderNoPaper
            visaRemind = CommonConfig.ReminderVisa

            worPermitRemind = CommonConfig.ReminderLabor
            certificateRemind = CommonConfig.ReminderCertificate

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>Xuất ra file excel</summary>
    ''' <param name="grid"></param>
    ''' <remarks></remarks>
    Public Sub ExportToExcel(ByVal grid As RadGrid)
        Dim lstData As List(Of ReminderLogDTO)
        Dim _error As Integer = 0
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Using xls As New ExcelCommon
                Dim query = From p In RemindList
                If _filter.EMPLOYEE_CODE <> "" Then
                    query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
                End If

                If _filter.FULLNAME <> "" Then
                    query = query.Where(Function(f) f.FULLNAME.ToUpper.Contains(_filter.FULLNAME.ToUpper))
                End If

                If _filter.REMIND_NAME <> "" Then
                    query = query.Where(Function(f) f.REMIND_NAME.ToUpper.Contains(_filter.REMIND_NAME.ToUpper))
                End If

                If _filter.TITLE_NAME <> "" Then
                    query = query.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
                End If

                If _filter.ORG_NAME <> "" Then
                    query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
                End If

                If _filter.JOIN_DATE IsNot Nothing Then
                    query = query.Where(Function(f) f.JOIN_DATE = _filter.JOIN_DATE)
                End If

                If _filter.REMIND_DATE IsNot Nothing Then
                    query = query.Where(Function(f) f.REMIND_DATE = _filter.REMIND_DATE)
                End If

                lstData = query.ToList
                Dim dtData = lstData.ToTable

                Dim bCheck = xls.ExportExcelTemplate(
                    Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/" & Request.Params("fid") & ".xls"),
                    "Reminder", dtData, Response, _error)

                If Not bCheck Then
                    Select Case _error
                        Case 1
                            ShowMessage(Translate("Mẫu báo cáo không tồn tại"), NotifyType.Warning)
                        Case 2
                            ShowMessage(Translate("Dữ liệu không tồn tại"), NotifyType.Warning)
                    End Select
                    Exit Sub
                End If
            End Using

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>17/07/2017</lastupdate>
    ''' <summary>Lấy ra thông tin remind</summary>
    ''' <remarks></remarks>
    Protected Sub GetInforRemind()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Using rep As New ProfileDashboardRepository
                Try
                    If RemindList Is Nothing OrElse (RemindList IsNot Nothing AndAlso RemindList.Count = 0) Then
                        RemindList = rep.GetRemind(contractRemind.ToString & "," & _
                                                   birthdayRemind.ToString & "," & _
                                                   visaRemind.ToString & "," & _
                                                   workingRemind.ToString & "," & _
                                                   terminateRemind.ToString & "," & _
                                                   terminateDebtRemind.ToString & "," & _
                                                   noPaperRemind.ToString & "," & _
                                                   "0," & _
                                                   "0," & _
                                                   worPermitRemind.ToString & "," & _
                                                   certificateRemind.ToString
                                                   )

                        For Each item In RemindList
                            item.REMIND_NAME = Translate(item.REMIND_NAME)
                        Next
                    End If

                    lbReminder1.Text = (From p In RemindList Where p.REMIND_TYPE = 1).Count
                    lbReminder2.Text = (From p In RemindList Where p.REMIND_TYPE = 2).Count
                    lbReminder13.Text = (From p In RemindList Where p.REMIND_TYPE = 13).Count
                    lbReminder14.Text = (From p In RemindList Where p.REMIND_TYPE = 14).Count
                    lbReminder16.Text = (From p In RemindList Where p.REMIND_TYPE = 16).Count
                    lbReminder19.Text = (From p In RemindList Where p.REMIND_TYPE = 19).Count
                    lbReminder5.Text = (From p In RemindList Where p.REMIND_TYPE = 5).Count
                    lbReminder20.Text = (From p In RemindList Where p.REMIND_TYPE = 20).Count
                Catch ex As Exception
                    rep.Dispose()
                    'DisplayException(Me.ViewName, Me.ID, ex)
                    _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
                End Try                
            End Using

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception            
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

End Class
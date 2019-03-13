Imports Telerik.Web.UI
Imports Common.CommonBusiness
Imports System.Web.Configuration
Imports Common
Imports WebAppLog

Public Class ctrlRemindConfig
    Inherits CommonView

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Modules/Profile/Setting/" + Me.GetType().Name.ToString()
#Region "Property"
#End Region

#Region "Page"
    ''' <summary>
    ''' Create menu toolbar
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                       ToolbarItem.Seperator, _
                                       ToolbarItem.Cancel)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Khoi tao, load page, update state page, control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh()
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <summary>
    ''' update state luon la edit cho page, enable control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CurrentState = CommonMessage.STATE_EDIT
            Me.ChangeToolbarState()
            rntxtCONTRACT.Enabled = chkCONTRACT.Checked
            rntxtBIRTHDAY.Enabled = chkBIRTHDAY.Checked
            rntxtVISA.Enabled = chkVisa.Checked

            rntxtWORKING.Enabled = chkWorking.Checked
            rntxtTERMINATE.Enabled = chkTerminate.Checked
            rntxtTERMINATEDEBT.Enabled = chkTerminateDebt.Checked
            'rntxtNOPAPER.Enabled = chkNoPaper.Checked
            rntxtCERTIFICATE.Enabled = chkCertificate.Checked
            rntxtLABOR.Enabled = chkLabor.Checked

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Reset page, check value du lieu neu = 0 thi uncheck group tuong ung va disable textbox do
    ''' </summary>
    ''' <param name="Message">Ko su dung</param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CommonConfig.GetReminderConfigFromDatabase()
            End If
            If CommonConfig.ReminderContractDays = 0 Then
                chkCONTRACT.Checked = False
                rntxtCONTRACT.Enabled = False
                rntxtCONTRACT.Value = Nothing
            Else
                chkCONTRACT.Checked = True
                rntxtCONTRACT.Enabled = True
                rntxtCONTRACT.Value = CommonConfig.ReminderContractDays
            End If
            If CommonConfig.ReminderBirthdayDays = 0 Then
                chkBIRTHDAY.Checked = False
                rntxtBIRTHDAY.Enabled = False
                rntxtBIRTHDAY.Value = Nothing
            Else
                chkBIRTHDAY.Checked = True
                rntxtBIRTHDAY.Enabled = True
                rntxtBIRTHDAY.Value = CommonConfig.ReminderBirthdayDays
            End If

            If CommonConfig.ReminderVisa = 0 Then
                chkVisa.Checked = False
                rntxtVISA.Enabled = False
                rntxtVISA.Value = Nothing
            Else
                chkVisa.Checked = True
                rntxtVISA.Enabled = True
                rntxtVISA.Value = CommonConfig.ReminderVisa
            End If


            If CommonConfig.ReminderWorking = 0 Then
                chkWorking.Checked = False
                rntxtWORKING.Enabled = False
                rntxtWORKING.Value = Nothing
            Else
                chkWorking.Checked = True
                rntxtWORKING.Enabled = True
                rntxtWORKING.Value = CommonConfig.ReminderWorking
            End If

            If CommonConfig.ReminderTerminate = 0 Then
                chkTerminate.Checked = False
                rntxtTERMINATE.Enabled = False
                rntxtTERMINATE.Value = Nothing
            Else
                chkTerminate.Checked = True
                rntxtTERMINATE.Enabled = True
                rntxtTERMINATE.Value = CommonConfig.ReminderTerminate
            End If

            If CommonConfig.ReminderTerminateDebt = 0 Then
                chkTerminateDebt.Checked = False
                rntxtTERMINATEDEBT.Enabled = False
                rntxtTERMINATEDEBT.Value = Nothing
            Else
                chkTerminateDebt.Checked = True
                rntxtTERMINATEDEBT.Enabled = True
                rntxtTERMINATEDEBT.Value = CommonConfig.ReminderTerminateDebt
            End If


            If CommonConfig.ReminderNoPaper = 0 Then
                'chkNoPaper.Checked = False
                'rntxtNOPAPER.Enabled = False
                'rntxtNOPAPER.Value = Nothing
            Else
                'chkNoPaper.Checked = True
                'rntxtNOPAPER.Enabled = True
                'rntxtNOPAPER.Value = CommonConfig.ReminderNoPaper
            End If

            If CommonConfig.ReminderCertificate = 0 Then
                chkCertificate.Checked = False
                rntxtCERTIFICATE.Enabled = False
                rntxtCERTIFICATE.Value = Nothing
            Else
                chkCertificate.Checked = True
                rntxtCERTIFICATE.Enabled = True
                rntxtCERTIFICATE.Value = CommonConfig.ReminderCertificate
            End If

            If CommonConfig.ReminderLabor = 0 Then
                chkLabor.Checked = False
                rntxtLABOR.Enabled = False
                rntxtLABOR.Value = Nothing
            Else
                chkLabor.Checked = True
                rntxtLABOR.Enabled = True
                rntxtLABOR.Value = CommonConfig.ReminderLabor
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                      CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"
    ''' <summary>
    ''' event click nut luu, huy
    ''' update trang thai page, control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If chkCONTRACT.Checked Then
                            CommonConfig.ReminderContractDays = rntxtCONTRACT.Value
                            rntxtCONTRACT.Enabled = True
                        Else
                            CommonConfig.ReminderContractDays = 0
                            rntxtCONTRACT.Enabled = False
                        End If
                        If chkBIRTHDAY.Checked Then
                            CommonConfig.ReminderBirthdayDays = rntxtBIRTHDAY.Value
                            rntxtBIRTHDAY.Enabled = True
                        Else
                            CommonConfig.ReminderBirthdayDays = 0
                            rntxtBIRTHDAY.Enabled = True
                        End If


                        If chkVisa.Checked Then
                            If rntxtVISA.Value IsNot Nothing Then
                                CommonConfig.ReminderVisa = rntxtVISA.Value
                            End If

                            rntxtVISA.Enabled = True
                        Else
                            CommonConfig.ReminderVisa = 0
                            rntxtVISA.Enabled = True
                        End If
                        If chkWorking.Checked Then
                            If rntxtWORKING.Value IsNot Nothing Then
                                CommonConfig.ReminderWorking = rntxtWORKING.Value
                            End If
                            rntxtWORKING.Enabled = True
                        Else
                            CommonConfig.ReminderWorking = 0
                            rntxtWORKING.Enabled = True
                        End If
                        If chkTerminate.Checked Then
                            If rntxtTERMINATE.Value IsNot Nothing Then
                                CommonConfig.ReminderTerminate = rntxtTERMINATE.Value
                            End If

                            rntxtTERMINATE.Enabled = True
                        Else
                            CommonConfig.ReminderTerminate = 0
                            rntxtTERMINATE.Enabled = True
                        End If
                        If chkTerminateDebt.Checked Then
                            If rntxtTERMINATEDEBT.Value IsNot Nothing Then
                                CommonConfig.ReminderTerminateDebt = rntxtTERMINATEDEBT.Value
                            End If

                            rntxtTERMINATEDEBT.Enabled = True
                        Else
                            CommonConfig.ReminderTerminateDebt = 0
                            rntxtTERMINATEDEBT.Enabled = True
                        End If
                        'If chkNoPaper.Checked Then
                        '    If rntxtNOPAPER.Value IsNot Nothing Then
                        '        CommonConfig.ReminderNoPaper = rntxtNOPAPER.Value
                        '    End If

                        '    rntxtNOPAPER.Enabled = True
                        'Else
                        '    CommonConfig.ReminderNoPaper = 0
                        '    rntxtNOPAPER.Enabled = True
                        'End If

                        '---------------------------------------
                        CommonConfig.ReminderNoPaper = 0
                        '--------------------------------------



                        If chkCertificate.Checked Then
                            If rntxtCERTIFICATE.Value IsNot Nothing Then
                                CommonConfig.ReminderCertificate = rntxtCERTIFICATE.Value
                            End If

                            rntxtCERTIFICATE.Enabled = True
                        Else
                            CommonConfig.ReminderCertificate = 0
                            rntxtCERTIFICATE.Enabled = True
                        End If

                        If chkLabor.Checked Then
                            If rntxtLABOR.Value IsNot Nothing Then
                                CommonConfig.ReminderLabor = rntxtLABOR.Value
                            End If

                            rntxtLABOR.Enabled = True
                        Else
                            CommonConfig.ReminderLabor = 0
                            rntxtLABOR.Enabled = True

                        End If
                        CommonConfig.SaveReminderPerUser()
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Framework.UI.Utilities.NotifyType.Success)
                        Common.Common.Reminder = Nothing
                    End If
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Refresh()
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Framework.UI.Utilities.NotifyType.Error)
        End Try
    End Sub
    ''' <summary>
    ''' validate cua exception cvalBirthday
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalBIRTHDAY_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalBIRTHDAY.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkBIRTHDAY.Checked And (rntxtBIRTHDAY.Value Is Nothing OrElse rntxtBIRTHDAY.Value = 0) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <summary>
    ''' validate cua exception cvalCONTRACT
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCONTRACT_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCONTRACT.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkCONTRACT.Checked And (rntxtCONTRACT.Value Is Nothing OrElse rntxtCONTRACT.Value = 0) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <summary>
    ''' Validate hết hạn visa
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub valnmVISA_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles valnmVISA.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If chkVisa.Checked And (rntxtVISA.Value Is Nothing OrElse rntxtVISA.Value = 0) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

#End Region

#Region "Custom"

#End Region

End Class
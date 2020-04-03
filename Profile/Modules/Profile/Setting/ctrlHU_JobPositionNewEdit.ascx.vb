Imports Framework.UI
Imports Common
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports Framework.UI.Utilities
Imports Ionic.Zip
Imports WebAppLog

Public Class ctrlHU_JobPositionNewEdit
    Inherits CommonView

    Protected WithEvents _ctrlFindTitlePopup As ctrlFindTitlePopup
    Protected WithEvents _ctrlFindTitlePopupParent As ctrlFindTitlePopup
    Protected WithEvents _ctrlFindOrgPopup As ctrlFindOrgPopup
    Dim psp As New ProfileStoreProcedure
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/Setting/" + Me.GetType().Name.ToString()
#Region "Property"
    Private _decIDSelectPopup As Decimal
    Public Property IDSelectPopup() As Decimal
        Get
            Return _decIDSelectPopup
        End Get
        Set(ByVal value As Decimal)
            _decIDSelectPopup = value
        End Set
    End Property
    Property Commend As CommendDTO
        Get
            Return ViewState(Me.ID & "_Commend")
        End Get
        Set(ByVal value As CommendDTO)
            ViewState(Me.ID & "_Commend") = value
        End Set
    End Property
    ''0 - normal
    ''1 - Employee
    ''2 - Org
    ''3 - Sign
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    Property Employee_Commend As List(Of TitleDTO)
        Get
            Return ViewState(Me.ID & "_Employee_Commend")
        End Get
        Set(ByVal value As List(Of TitleDTO))
            ViewState(Me.ID & "_Employee_Commend") = value
        End Set
    End Property
    ''0 - NEW
    ''1 - EDIT
    Dim _FormType As Integer
    Dim _IDSelect As Decimal?
    Dim _OrgID As Decimal?
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            GetParams()
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dsdata As DataSet
        Dim dtData As DataTable
        Dim dtData1 As DataTable
        Try

            Select Case Message
                Case "UpdateView"
                    Using rep As New ProfileRepository
                        dsdata = rep.GET_JOB_POSITION_DETAIL(_IDSelect)
                        dtData = rep.GetTitleByOrgID(dsdata.Tables(0).Rows(0)("ORG_ID"), True)
                        dtData1 = rep.GET_JOB_POSITION_LIST(dsdata.Tables(0).Rows(0)("ORG_ID"), _IDSelect)
                    End Using

                    CurrentState = CommonMessage.STATE_EDIT
                    hidID.Value = dsdata.Tables(0).Rows(0)("ID")
                    txtCode.Text = dsdata.Tables(0).Rows(0)("CODE")
                    hidCode.Value = dsdata.Tables(0).Rows(0)("CODE")
                    txtJobName.Text = dsdata.Tables(0).Rows(0)("JOB_NAME")
                    txtOrgName.Text = dsdata.Tables(0).Rows(0)("ORG_NAME")
                    hidOrgID.Value = dsdata.Tables(0).Rows(0)("ORG_ID")

                    If Not IsDBNull(dsdata.Tables(0).Rows(0)("JOB_NOTE")) Then
                        txtJobNote.Text = dsdata.Tables(0).Rows(0)("JOB_NOTE")
                    End If

                    If Not IsDBNull(dsdata.Tables(0).Rows(0)("COST_CODE")) Then
                        txtCostCode.Text = dsdata.Tables(0).Rows(0)("COST_CODE")
                    End If

                    If dsdata.Tables(0).Rows(0)("IS_LEADER") <> "0" Then
                        cbIS_LEADER.Checked = True
                    Else
                        cbIS_LEADER.Checked = False
                    End If

                    rdEffectDate.SelectedDate = dsdata.Tables(0).Rows(0)("EFFECT_DATE")

                    cboTitle.ClearValue()
                    cboTitle.Items.Clear()
                    For Each item As DataRow In dtData.Rows
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(item("NAME").ToString(), item("ID").ToString())
                        radItem.Attributes("GROUP_NAME") = item("GROUP_NAME").ToString()
                        cboTitle.Items.Add(radItem)
                    Next

                    cboTitle.SelectedValue = dsdata.Tables(0).Rows(0)("TITLE_ID")
                    hidTitleID.Value = dsdata.Tables(0).Rows(0)("TITLE_ID")

                    For Each item As DataRow In dtData1.Rows
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(item("JOB_NAME").ToString(), item("ID").ToString())
                        For Each item1 As DataRow In dsdata.Tables(1).Rows
                            If item("ID").ToString() = item1("ID").ToString() Then
                                radItem.Checked = True
                                Continue For
                            End If
                        Next
                        cboDM.Items.Add(radItem)
                    Next

                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            Dim dic As New Dictionary(Of String, Control)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.MainToolBar = tbarOrg
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Seperator,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Event"


    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim objOrgTitle As New OrgTitleDTO
        Dim lstOrgTitle As New List(Of OrgTitleDTO)
        Dim gID As Decimal
        Dim rep As New ProfileRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW

                                gID = rep.INSERT_JOB_POSITION(txtCode.Text, txtJobName.Text, hidOrgID.Value, cboTitle.SelectedValue,
                                                              txtJobNote.Text, txtCostCode.Text, cbIS_LEADER.Checked, rdEffectDate.SelectedDate)

                                If gID > 0 Then
                                    Dim strDM As String = ""
                                        For i As Integer = 0 To cboDM.CheckedItems.Count - 1 Step 1
                                            strDM = strDM + "," + cboDM.CheckedItems(i).Value
                                        Next
                                    If strDM <> "" Then
                                        Dim result = rep.INSERT_DIRECT_MANAGER(gID, strDM)
                                    End If

                                    Response.Redirect("/Default.aspx?mid=Organize&fid=ctrlHU_JobPosition&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                gID = rep.UPDATE_JOB_POSITION(hidID.Value, txtCode.Text, txtJobName.Text, hidOrgID.Value, cboTitle.SelectedValue,
                                                              txtJobNote.Text, txtCostCode.Text, cbIS_LEADER.Checked, rdEffectDate.SelectedDate)

                                If gID > 0 Then
                                    Dim strDM As String = ""
                                    For i As Integer = 0 To cboDM.CheckedItems.Count - 1 Step 1
                                        strDM = strDM + "," + cboDM.CheckedItems(i).Value
                                    Next
                                    If strDM <> "" Then
                                        Dim result = rep.INSERT_DIRECT_MANAGER(gID, strDM)
                                    End If

                                    Response.Redirect("/Default.aspx?mid=Organize&fid=ctrlHU_JobPosition&group=Business")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Response.Redirect("/Default.aspx?mid=Organize&fid=ctrlHU_JobPosition&group=Business")
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    Protected Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindOrg.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            isLoadPopup = 3
            UpdateControlState()
            _ctrlFindOrgPopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
   
    Public Overrides Sub UpdateControlState()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case isLoadPopup
                Case 1
                    If Not Me.Controls.Contains(_ctrlFindTitlePopup) Then
                        _ctrlFindTitlePopup = Me.Register("ctrlFindTitlePopup", "Common", "ctrlFindTitlePopup")
                        Dim strIDOtherSelect As String = ""

                        For Each TitleDTO In Me.Employee_Commend
                            ' Xu ly lay list
                            strIDOtherSelect += TitleDTO.ID.ToString() + ","
                        Next
                        If strIDOtherSelect.Length > 1 Then
                            strIDOtherSelect = strIDOtherSelect.Substring(0, strIDOtherSelect.Length - 1)
                        End If
                        _ctrlFindTitlePopup.TitleOtherSelect = strIDOtherSelect
                        Me.Controls.Add(_ctrlFindTitlePopup)
                    End If
                Case 6
                    If Not Me.Controls.Contains(_ctrlFindTitlePopupParent) Then
                        _ctrlFindTitlePopupParent = Me.Register("ctrlFindTitlePopup", "Common", "ctrlFindTitlePopup")

                        Me.Controls.Add(_ctrlFindTitlePopupParent)
                    End If
                Case 3
                    If Not Me.Controls.Contains(_ctrlFindOrgPopup) Then
                        _ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                        Me.Controls.Add(_ctrlFindOrgPopup)
                    End If

            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    Private Sub ctrlFindOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles _ctrlFindOrgPopup.OrganizationSelected
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim dtData As DataTable = Nothing
        Try
            Dim orgItem = _ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue 'gán org đã chọn vào hiddenfield
                txtOrgName.Text = orgItem.NAME_VN
                If orgItem.DESCRIPTION_PATH IsNot Nothing AndAlso orgItem.DESCRIPTION_PATH <> "" Then
                    txtOrgName.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
                End If
                Using rep As New ProfileRepository
                    If IsNumeric(e.CurrentValue) Then
                        dtData = rep.GetTitleByOrgID(Decimal.Parse(e.CurrentValue), True)
                        cboTitle.ClearValue()
                        cboTitle.Items.Clear()
                        For Each item As DataRow In dtData.Rows
                            Dim radItem As RadComboBoxItem = New RadComboBoxItem(item("NAME").ToString(), item("ID").ToString())
                            radItem.Attributes("GROUP_NAME") = item("GROUP_NAME").ToString()
                            cboTitle.Items.Add(radItem)
                        Next

                        dtData = rep.GET_JOB_POSITION_LIST(Decimal.Parse(e.CurrentValue), 0)
                        For Each item As DataRow In dtData.Rows
                            Dim radItem As RadComboBoxItem = New RadComboBoxItem(item("JOB_NAME").ToString(), item("ID").ToString())
                            cboDM.Items.Add(radItem)
                        Next

                    End If
                End Using
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlFindEmpImportPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ctrlFindTitlePopup.CancelClicked, _ctrlFindTitlePopupParent.CancelClicked
        isLoadPopup = 0
    End Sub

    ' ''' <lastupdate>06/07/2017</lastupdate>
    ' ''' <summary> Event selected Item combobox </summary>
    ' ''' <param name="sender"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    'Protected Sub cboCommon_ItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs) _
    '                                                Handles cboTitle.ItemsRequested

    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim startTime As DateTime = DateTime.UtcNow

    '    Try
    '        Using rep As New ProfileRepository
    '            Dim dtData As DataTable = Nothing
    '            Dim sText As String = e.Text
    '            Dim dValue As Decimal
    '            Select Case sender.ID
    '                Case cboTitle.ID
    '                    dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
    '                    dtData = rep.GetTitleByOrgID(dValue, True)
    '            End Select

    '            If sText <> "" Then
    '                Dim dtExist = (From p In dtData
    '                              Where p("NAME") IsNot DBNull.Value AndAlso _
    '                              p("NAME").ToString.ToUpper = sText.ToUpper)

    '                If dtExist.Count = 0 Then
    '                    Dim dtFilter = (From p In dtData
    '                              Where p("NAME") IsNot DBNull.Value AndAlso _
    '                              p("NAME").ToString.ToUpper.Contains(sText.ToUpper))

    '                    If dtFilter.Count > 0 Then
    '                        dtData = dtFilter.CopyToDataTable
    '                    Else
    '                        dtData = dtData.Clone
    '                    End If

    '                    Dim itemOffset As Integer = e.NumberOfItems
    '                    Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
    '                    e.EndOfItems = endOffset = dtData.Rows.Count

    '                    For i As Integer = itemOffset To endOffset - 1
    '                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
    '                        sender.Items.Add(radItem)
    '                    Next
    '                Else

    '                    Dim itemOffset As Integer = e.NumberOfItems
    '                    Dim endOffset As Integer = dtData.Rows.Count
    '                    e.EndOfItems = True

    '                    For i As Integer = itemOffset To endOffset - 1
    '                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
    '                        sender.Items.Add(radItem)
    '                    Next
    '                End If
    '            Else
    '                Dim itemOffset As Integer = e.NumberOfItems
    '                Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
    '                e.EndOfItems = endOffset = dtData.Rows.Count

    '                For i As Integer = itemOffset To endOffset - 1
    '                    Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
    '                    sender.Items.Add(radItem)
    '                Next
    '            End If
    '        End Using

    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        'DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    'Private Sub ctrlFindEmpImportPopup_TitleSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ctrlFindTitlePopup.TitleSelected
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim lstCommonEmployee As New List(Of Common.CommonBusiness.TitleDTO)
    '    'Dim rep As New ProfileBusinessRepository
    '    'Dim repNew As New ProfileRepository
    '    Try

    '        lstCommonEmployee = CType(_ctrlFindTitlePopup.SelectedTitle1, List(Of Common.CommonBusiness.TitleDTO))

    '        If lstCommonEmployee.Count > 0 Then
    '            If Employee_Commend Is Nothing Then
    '                Employee_Commend = New List(Of TitleDTO)
    '            End If
    '            For Each emp As CommonBusiness.TitleDTO In lstCommonEmployee
    '                Dim employee As New TitleDTO
    '                employee.ID = emp.ID
    '                employee.CODE = emp.CODE
    '                employee.NAME_VN = emp.NAME_VN
    '                Employee_Commend.Add(employee)
    '            Next
    '        End If
    '        isLoadPopup = 0
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    'Private Sub ctrlFindTitlePopupParent_TitleSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ctrlFindTitlePopupParent.TitleSelected
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        _decIDSelectPopup = _ctrlFindTitlePopupParent.SelectedTitle(0)

    '        FillDataParent(_decIDSelectPopup)
    '        isLoadPopup = 0
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    'Private Sub FillDataParent(ByVal empid As Decimal)
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim rep As New ProfileRepository
    '        Dim Titles As List(Of TitleDTO)

    '        Titles = rep.GetTitleByID(empid)
    '        Dim obj As New TitleDTO
    '        obj = Titles.FirstOrDefault()
    '        rep.Dispose()
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try

    'End Sub
    Private Sub GetParams()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If CurrentState Is Nothing Then
                If Request.Params("FormType") IsNot Nothing Then
                    _FormType = Request.Params("FormType")
                End If
                If Request.Params("IDSelect") IsNot Nothing Then
                    _IDSelect = Decimal.Parse(Request.Params("IDSelect"))
                End If
                If Request.Params("orgID") IsNot Nothing Then
                    Dim orgID = Request.Params("orgID")
                    Dim objOrg As New OrganizationDTO
                    Dim lstOrgTitle As New List(Of OrgTitleDTO)
                    Dim rep As New ProfileRepository
                    objOrg = rep.GetOrganizationByID(orgID)
                    rep.Dispose()
                    hidOrgID.Value = objOrg.ID
                    txtOrgName.Text = objOrg.NAME_VN
                End If
                If _IDSelect IsNot Nothing Then
                    Refresh("UpdateView")
                Else
                    Refresh("InsertView")
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub cboTitle_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTitle.SelectedIndexChanged
        Try
            If hidTitleID.Value IsNot Nothing And hidTitleID.Value <> "" Then
                If hidTitleID.Value <> cboTitle.SelectedValue Then
                    Using rep As New ProfileRepository
                        txtCode.Text = rep.GET_JOB_CODE_AUTO(cboTitle.SelectedValue)
                    End Using
                Else
                    txtCode.Text = hidCode.Value
                End If
            Else
                Using rep As New ProfileRepository
                    txtCode.Text = rep.GET_JOB_CODE_AUTO(cboTitle.SelectedValue)
                End Using
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region
End Class
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlApproveSetupOrg
    Inherits CommonView

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/ApproveProcess/" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' ApproveSetups
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ApproveSetups() As List(Of ApproveSetupDTO)
        Get
            Return ViewState(Me.ID & "_ApproveSetups")
        End Get

        Set(ByVal value As List(Of ApproveSetupDTO))
            ViewState(Me.ID & "_ApproveSetups") = value
        End Set
    End Property

    ''' <summary>
    ''' ListToDelete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListToDelete As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_Delete_List")
        End Get

        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_Delete_List") = value
        End Set
    End Property

    ''' <summary>
    ''' OrgID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OrgID As Decimal
        Get
            Return ViewState(Me.ID & "_OrgID")
        End Get

        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_OrgID") = value
        End Set
    End Property

    ''' <summary>
    ''' IDDetailSelecting
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IDDetailSelecting As Decimal?
        Get
            Return ViewState(Me.ID & "_ID_Detail_Selecting")
        End Get

        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_ID_Detail_Selecting") = value
        End Set
    End Property

#End Region

#Region "Event"

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If Not IsPostBack Then
                Refresh()
            End If

            UpdateControlState()            

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.MainToolBar = tbarDetail
            Common.BuildToolbar(tbarDetail,
                                ToolbarItem.Create,
                                ToolbarItem.Edit, ToolbarItem.Seperator,
                                ToolbarItem.Save,
                                ToolbarItem.Cancel, ToolbarItem.Seperator,
                                ToolbarItem.Delete)

            CType(tbarDetail.Items(3), RadToolBarButton).CausesValidation = True
            rgDetail.ClientSettings.EnablePostBackOnRowClick = True

            SetStatusToolbar(tbarDetail, CommonMessage.STATE_NORMAL, False)
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary> Load data cho combobox </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            LoadComboData()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgDetail_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgDetail.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CreateDataFilter()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Load data vào textbox khi chọn checkbox ở grid</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgDetail_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgDetail.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            IDDetailSelecting = Decimal.Parse(CType(rgDetail.SelectedItems(0), GridDataItem).GetDataKeyValue("ID").ToString)

            LoadDetailView()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Sự kiện khi lựa chọn phần tử của popup ctrlOrg</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            OrgID = Decimal.Parse(ctrlOrg.CurrentValue)
            CurrentState = CommonMessage.STATE_NORMAL

            CreateDataFilter()
            UpdateControlState()
            Refresh()            

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)            
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Sự kiện khi click button Detail vủa 1 row trên grid</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tbarDetail_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    Me.CurrentState = CommonMessage.STATE_NEW
                    IDDetailSelecting = Nothing
                    UpdateControlState()
                    ClearControlForInsert()

                Case CommonMessage.TOOLBARITEM_EDIT
                    Me.CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_SAVE

                    If Not Page.IsValid Then
                        ExcuteScript("Resize", "setDefaultSize()")
                        Exit Sub
                    End If

                    Dim db As New CommonRepository
                    If Not IDDetailSelecting.HasValue Then
                        'Thêm mới
                        Dim itemAdd As New ApproveSetupDTO
                        With itemAdd
                            .ORG_ID = OrgID
                            .PROCESS_ID = Decimal.Parse(cboApproveProcess.SelectedValue)
                            .TEMPLATE_ID = Decimal.Parse(cboApproveTemplate.SelectedValue)
                            .FROM_DATE = rdFromDate.SelectedDate
                            .TO_DATE = rdToDate.SelectedDate
                        End With

                        If CheckExistValueCombo(itemAdd.PROCESS_ID, itemAdd.TEMPLATE_ID) Then
                            LoadComboData()
                            cboApproveProcess.SelectedIndex = 0
                            cboApproveTemplate.SelectedIndex = 0
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                            Exit Sub
                        End If

                        If db.InsertApproveSetup(itemAdd) Then
                            ClearControlValue(cboApproveProcess, cboApproveTemplate, rdFromDate, rdToDate)
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            Exit Sub
                        End If
                    Else
                        ' Sửa đổi
                        Dim idEdit As Decimal = IDDetailSelecting.Value

                        Dim itemEdit As ApproveSetupDTO = db.GetApproveSetup(idEdit)
                        With itemEdit
                            .PROCESS_ID = Decimal.Parse(cboApproveProcess.SelectedValue)
                            .TEMPLATE_ID = Decimal.Parse(cboApproveTemplate.SelectedValue)
                            .FROM_DATE = rdFromDate.SelectedDate
                            .TO_DATE = rdToDate.SelectedDate
                        End With

                        If CheckExistValueCombo(itemEdit.PROCESS_ID, itemEdit.TEMPLATE_ID) Then
                            LoadComboData()
                            cboApproveProcess.SelectedIndex = 0
                            cboApproveTemplate.SelectedIndex = 0
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                            Exit Sub
                        End If

                        If db.UpdateApproveSetup(itemEdit) Then
                            ClearControlValue(cboApproveProcess, cboApproveTemplate, rdFromDate, rdToDate)
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                            Exit Sub
                        End If
                    End If

                    Refresh()
                    Me.CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_CANCEL
                    Me.CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_DELETE
                    Dim idDelete = Decimal.Parse(CType(rgDetail.SelectedItems(0), GridDataItem).GetDataKeyValue("ID").ToString)
                    Dim db As New CommonRepository
                    Dim listDelete As New List(Of Decimal)

                    listDelete.Add(idDelete)
                    ListToDelete = listDelete

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Sự kiện khi click button của popup warning</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim db As New CommonRepository

                If db.DeleteApproveSetup(ListToDelete) Then
                    ClearControlValue(cboApproveProcess, cboApproveTemplate, rdFromDate, rdToDate)
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Refresh()
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                End If
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Sự kiện khi click button của popup warning</summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCheckDateExist_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCheckDateExist.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim db As New CommonRepository
            Dim idExclude As Decimal? = IDDetailSelecting
            Dim itemCheck As New ApproveSetupDTO _
            With {
                .ORG_ID = OrgID,
                .PROCESS_ID = Decimal.Parse(cboApproveProcess.SelectedValue),
                .TEMPLATE_ID = Decimal.Parse(cboApproveTemplate.SelectedValue),
                .FROM_DATE = rdFromDate.SelectedDate,
                .TO_DATE = rdToDate.SelectedDate
            }

            args.IsValid = db.IsExistApproveSetupByDate(itemCheck, idExclude)

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary> Cập nhật trạng thái control </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            SetStatusToolbar(tbarDetail, Me.CurrentState)
            Select Case Me.CurrentState
                Case CommonMessage.STATE_NORMAL
                    EnabledGrid(rgDetail, True)
                    pnlDetail.Enabled = False
                    ctrlOrg.Enabled = True

                Case CommonMessage.STATE_EDIT, CommonMessage.STATE_NEW
                    EnabledGrid(rgDetail, True)
                    pnlDetail.Enabled = True
                    ctrlOrg.Enabled = False
            End Select

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <remarks></remarks>
    Protected Sub CreateDataFilter()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New CommonRepository
        Dim obj As New ApproveSetupDTO

        Try
            If ctrlOrg.CurrentValue = "" Then
                rgDetail.DataSource = New List(Of String)
            Else
                Dim MaximumRows As Integer

                SetValueObjectByRadGrid(rgDetail, obj)

                Dim Sorts As String = rgDetail.MasterTableView.SortExpressions.GetSortString()
                If Sorts IsNot Nothing Then
                    Me.ApproveSetups = rep.GetApproveSetupByOrg(OrgID, Sorts)
                Else
                    Me.ApproveSetups = rep.GetApproveSetupByOrg(OrgID)
                End If

                rgDetail.VirtualItemCount = MaximumRows
                rgDetail.DataSource = Me.ApproveSetups
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Khoi tao, refresh grid</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try            
            rgDetail.Rebind()
            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Clear ctrl trước khi thêm mới</summary>
    ''' <remarks></remarks>
    Private Sub ClearControlForInsert()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            cboApproveProcess.SelectedIndex = 0
            cboApproveTemplate.SelectedIndex = 0
            rdFromDate.SelectedDate = Nothing
            rdToDate.SelectedDate = Nothing

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Load dữ liệu cho combobox</summary>
    ''' <remarks></remarks>
    Private Sub LoadComboData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim db As New CommonRepository

        Try
            Dim dbProcess = db.GetApproveProcessList().Where(Function(p) p.ACTFLG = "A").ToList()
            cboApproveProcess.DataSource = dbProcess
            cboApproveProcess.DataTextField = "NAME"
            cboApproveProcess.DataValueField = "ID"
            cboApproveProcess.DataBind()

            Dim dbTemplate = db.GetApproveTemplateList().Where(Function(p) p.TEMPLATE_TYPE = 0).ToList()
            cboApproveTemplate.DataSource = dbTemplate
            cboApproveTemplate.DataTextField = "TEMPLATE_NAME"
            cboApproveTemplate.DataValueField = "ID"
            cboApproveTemplate.DataBind()

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Load thông tin chi tiết của mỗi thiết lập</summary>
    ''' <remarks></remarks>
    Private Sub LoadDetailView()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim db As New CommonRepository

        Try
            Dim itemSelected = db.GetApproveSetup(IDDetailSelecting)

            If itemSelected IsNot Nothing Then
                cboApproveProcess.SelectedValue = itemSelected.PROCESS_ID.ToString
                cboApproveTemplate.SelectedValue = itemSelected.TEMPLATE_ID.ToString
                rdFromDate.SelectedDate = itemSelected.FROM_DATE
                rdToDate.SelectedDate = itemSelected.TO_DATE
            Else
                ShowMessage(Translate("Đã có lỗi khi truy xuất thông tin đang chọn."), NotifyType.Error)
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Thiết lập trạng thái của Toolbar</summary>
    ''' <remarks></remarks>
    Private Sub SetStatusToolbar(ByVal _tbar As RadToolBar, ByVal formState As String, Optional ByVal stateToAll As Boolean? = Nothing)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Select Case formState
                Case CommonMessage.STATE_NORMAL
                    For Each item As RadToolBarItem In _tbar.Items
                        Select Case CType(item, RadToolBarButton).CommandName
                            Case CommonMessage.TOOLBARITEM_CANCEL, CommonMessage.TOOLBARITEM_SAVE
                                item.Enabled = False
                            Case Else
                                item.Enabled = True
                        End Select
                    Next

                Case CommonMessage.STATE_EDIT, CommonMessage.STATE_NEW
                    For Each item As RadToolBarItem In _tbar.Items
                        Select Case CType(item, RadToolBarButton).CommandName
                            Case CommonMessage.TOOLBARITEM_CANCEL, CommonMessage.TOOLBARITEM_SAVE
                                item.Enabled = True
                            Case Else
                                item.Enabled = False
                        End Select
                    Next
            End Select

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>25/07/2017</lastupdate>
    ''' <summary>Kiểm tra value của combobox Quy trình, Template áp dụng có tồn tại hay không</summary>
    ''' <param name="decAppProcess"></param>
    ''' <param name="decAppTemplate"></param>
    ''' <remarks></remarks>
    Private Function CheckExistValueCombo(ByVal decAppProcess As Decimal, ByVal decAppTemplate As Decimal) As Boolean
        CheckExistValueCombo = False
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim db As New CommonRepository

        Try
            Dim lst As New List(Of Decimal)

            lst.Add(decAppProcess)
            If db.CheckExistIDTable(lst, "SE_APP_PROCESS", "ID") Then
                Return True
            End If

            lst.Clear()
            lst.Add(decAppTemplate)
            If db.CheckExistIDTable(lst, "SE_APP_TEMPLATE", "ID") Then
                Return True
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

#End Region

End Class
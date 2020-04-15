Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Common
Imports Telerik.Web.UI
Imports WebAppLog
Imports System.IO
Imports Ionic.Crc
Imports Aspose.Cells

Public Class ctrlHU_Title
    Inherits Common.CommonView

    ''' <creator>HongDX</creator>
    ''' <lastupdate>21/06/2017</lastupdate>
    ''' <summary>Write Log</summary>
    ''' <remarks>_mylog, _pathLog, _classPath</remarks>
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\List" + Me.GetType().Name.ToString()

#Region "Property"
    Property dtDataImportWorking As DataTable
        Get
            Return ViewState(Me.ID & "_dtDataImportWorking")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtDataImportWorking") = value
        End Set
    End Property
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("GR_NGACH_LUONG", GetType(String))
                dt.Columns.Add("GR_NGACH_LUONG_ID", GetType(String))
                dt.Columns.Add("GR_TITLE", GetType(String))
                dt.Columns.Add("GR_TITLE_ID", GetType(String))
                dt.Columns.Add("CODE_TITLE", GetType(String))
                dt.Columns.Add("NAME_TITLE", GetType(String))
                dt.Columns.Add("GROUP_WORK", GetType(String))
                dt.Columns.Add("GROUP_WORK_ID", GetType(String))
                dt.Columns.Add("FUNCTION_WORK", GetType(String))
                dt.Columns.Add("REQUEST_WORK", GetType(String))
                dt.Columns.Add("PURPOSE_WORK", GetType(String))
                dt.Columns.Add("IS_SIGN", GetType(String))
                dt.Columns.Add("IS_SIGN_ID", GetType(String))
                dt.Columns.Add("HURT_FULL", GetType(String))
                dt.Columns.Add("HURT_FULL_ID", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property
    ''' <summary>
    ''' IDSelect
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get

        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    ''' <summary>
    ''' Down_File
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Down_File As String
        Get
            Return ViewState(Me.ID & "_Down_File")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Down_File") = value
        End Set
    End Property

    ''' <summary>
    ''' FileOldName
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FileOldName As String
        Get
            Return ViewState(Me.ID & "_FileOldName")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_FileOldName") = value
        End Set
    End Property
    Property dataTable As DataTable
        Get
            Return ViewState(Me.ID & "_dataTable")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dataTable") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Refresh()
            UpdateControlState()
            rgMain.SetFilter()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            rgMain.AllowCustomPaging = True
            InitControl()
            ctrlUpload2.isMultiple = False
            If Not IsPostBack Then
                ViewConfig(RadPane1)
                GirdConfig(rgMain)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Active, ToolbarItem.Deactive, ToolbarItem.Export, ToolbarItem.Next, ToolbarItem.Import)
            CType(Me.MainToolBar.Items(8), RadToolBarButton).Text = Translate("Xuất file mẫu")
            CType(Me.MainToolBar.Items(8), RadToolBarButton).ImageUrl = CType(Me.MainToolBar.Items(7), RadToolBarButton).ImageUrl
            CType(Me.MainToolBar.Items(9), RadToolBarButton).Text = Translate("Nhập file mẫu")

            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New ProfileRepository

        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.Rebind()
                        ClearControlValue(txtCode, txtNameVN, txtRemark, cboTitleGroup, chkSign, cboOrgType, cboHurtType, ckOVT, txtRemindLink, txtUpload, txtUploadFile, cboNgachLuong, cboGroupWork, txtFuncWork, txtRequestWork, txtPurpose)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                        ClearControlValue(txtCode, txtNameVN, txtRemark, cboTitleGroup, chkSign, cboOrgType, cboHurtType, ckOVT, txtRemindLink, txtUpload, txtUploadFile, cboNgachLuong, cboGroupWork, txtFuncWork, txtRequestWork, txtPurpose)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgMain.MasterTableView.ClearSelectedItems()
                        ClearControlValue(txtNameVN, txtRemark, cboOrgType, cboHurtType, ckOVT, txtRemindLink, txtUpload, txtUploadFile, chkSign, cboNgachLuong, cboGroupWork, txtFuncWork, txtRequestWork, txtPurpose)
                    Case ""
                        cboTitleGroup.AutoPostBack = False
                        cboOrgType.AutoPostBack = False
                        cboHurtType.AutoPostBack = False
                End Select
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>  Load data len grid </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        CreateDataFilter = Nothing
        Dim rep As New ProfileRepository
        Dim _filter As New TitleDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim MaximumRows As Integer

            SetValueObjectByRadGrid(rgMain, _filter)

            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()

            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetTitle(_filter, Sorts).ToTable()
                Else
                    Return rep.GetTitle(_filter).ToTable()
                End If
            Else
                Dim Titles As List(Of TitleDTO)
                If Sorts IsNot Nothing Then
                    Titles = rep.GetTitle(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
                Else
                    Titles = rep.GetTitle(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
                End If

                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = Titles
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>
    ''' Load control, Khoi tao popup tim Ma Nhan Vien, Tim Don Vi To Chuc
    ''' Set Trang thai control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnabledGridNotPostback(rgMain, False)
                    EnableControlAll(True, cboTitleGroup, txtNameVN, txtRemark, chkSign, cboOrgType, cboHurtType, ckOVT, btnDownload, btnUploadFile, cboNgachLuong, cboGroupWork, txtFuncWork, txtRequestWork, txtPurpose, txtCode)
                    If cboHurtType.SelectedItem IsNot Nothing Or txtRemark.Text IsNot Nothing Then
                        GoTo dontrefresh
                    End If
                    Refresh("Cancel")
                    btnDownload.Enabled = False
dontrefresh:
                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgMain, True)
                    EnableControlAll(False, cboTitleGroup, txtNameVN, txtRemark, chkSign, cboOrgType, cboHurtType, ckOVT, btnUploadFile, cboNgachLuong, cboGroupWork, txtFuncWork, txtRequestWork, txtPurpose, txtCode)
                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgMain, False)
                    Utilities.EnableRadCombo(cboTitleGroup, True)
                    EnableControlAll(True, cboTitleGroup, txtNameVN, txtRemark, chkSign, cboOrgType, cboHurtType, ckOVT, btnDownload, btnUploadFile, cboNgachLuong, cboGroupWork, txtFuncWork, txtRequestWork, txtPurpose, txtCode)
                    If txtUpload.Text <> "" Then
                        btnDownload.Enabled = True
                    Else
                        btnDownload.Enabled = False
                    End If

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)

                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.DeleteTitle(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_EFFECT_NOT_DELETE), NotifyType.Error)
                        UpdateControlState()
                    End If

                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)

                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.ActiveTitle(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)

                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.ActiveTitle(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
            End Select
            rep.Dispose()
            txtNameVN.Focus()

            UpdateToolbarState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Fill du lieu len combobox</summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim repS As New ProfileStoreProcedure
        Try
            Dim dtData As New DataTable
            Using rep As New ProfileRepository
                dataTable = rep.GetOtherList("HU_TITLE_GROUP", True)
                FillRadCombobox(cboTitleGroup, dataTable, "NAME", "ID")
                dtData = rep.GetOtherList("HURT_TYPE", True)
                FillRadCombobox(cboHurtType, dtData, "NAME", "ID")
                dataTable = rep.GetOtherList("GROUP_WORK", True)
                FillRadCombobox(cboGroupWork, dataTable, "NAME", "ID")
            End Using
            dataTable = repS.GET_NGACH_LUONG_ACV()
            FillRadCombobox(cboNgachLuong, dataTable, "NAME_VN", "ID")

            'Dim dtOrgType As DataTable
            ' dtOrgType = repS.GET_ORG_TYPE()
            'FillRadCombobox(cboOrgType, dtOrgType, "TYPE_NAME_VN", "ORG_ID_TYPE", True)

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("CODE", txtCode)
            dic.Add("NAME_VN", txtNameVN)
            dic.Add("REMARK", txtRemark)
            dic.Add("TITLE_GROUP_ID", cboTitleGroup)
            dic.Add("TITLE_GROUP_ID1", hidTITLE_GROUP_ID)
            dic.Add("IS_SIGN", chkSign)
            dic.Add("GR_GLONE_ID", cboNgachLuong)
            dic.Add("GR_WORK_ID", cboGroupWork)
            dic.Add("FUNCTION_WORK", txtFuncWork)
            dic.Add("REQUEST_WORK", txtRequestWork)
            dic.Add("PURPOSE_WORK", txtPurpose)
            'dic.Add("ORG_TYPE", cboOrgType)
            dic.Add("HURT_TYPE_ID", cboHurtType)
            dic.Add("OVT_CHECK", ckOVT)
            dic.Add("FILENAME", txtUpload)
            dic.Add("UPLOAD_FILE", txtRemindLink)
            Utilities.OnClientRowSelectedChanged(rgMain, dic)

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"
    'Private Sub cboTitleGroup_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboTitleGroup.SelectedIndexChanged
    '    Dim repS As New ProfileStoreProcedure
    '    Dim dtOrgLevel As New DataTable()
    '    Try
    '        Dim ORG_CODE As String = String.Empty
    '        Dim TITLE_GROUP As String = String.Empty
    '        If hidTITLE_GROUP_ID.Value <> cboTitleGroup.SelectedValue Then
    '            GenerateTitleCode()
    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        dtOrgLevel.Dispose()
    '    End Try
    'End Sub

    'Private Sub GenerateTitleCode()
    '    Dim rep As New ProfileRepository
    '    Dim dtData As New DataTable()
    '    Try
    '        Dim TITLE_GROUP As String = String.Empty
    '        If IsNumeric(cboTitleGroup.SelectedValue) Then
    '            dtData = rep.GetOtherList("HU_TITLE_GROUP", True)
    '            TITLE_GROUP = dtData.Select("ID='" + cboTitleGroup.SelectedValue + "'")(0)("CODE").ToString
    '            txtCode.Text = rep.AutoGenCode(TITLE_GROUP, "HU_TITLE", "CODE")
    '        End If
    '        If rgMain.SelectedValue IsNot Nothing Then
    '            txtCode.Text = rep.AutoGenCode(TITLE_GROUP, "HU_TITLE", "CODE")
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        dtData.Dispose()
    '        rep.Dispose()
    '    End Try
    'End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> 
    ''' Event click item tren menu toolbar
    ''' Set trang thai control sau khi process xong 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objTitle As New TitleDTO
        Dim rep As New ProfileRepository
        Dim gID As Decimal
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim commonRes As New CommonRepository

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(txtCode, txtNameVN, cboOrgType, txtRemark, cboTitleGroup, chkSign, ckOVT, cboHurtType, txtUpload, cboNgachLuong, cboGroupWork, txtFuncWork, txtRequestWork, txtPurpose)
                    ' txtCode.Text = rep.AutoGenCode("CD", "HU_TITLE", "CODE")

                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    If rgMain.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    CurrentState = CommonMessage.STATE_EDIT

                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(Decimal.Parse(item("ID").Text))
                    Next

                    If Not rep.CheckExistInDatabase(lstDeletes, ProfileCommonTABLE_NAME.HU_TITLE) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                        Return
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                    ClearControlValue(cboTitleGroup, txtCode, txtNameVN, txtRemark, cboNgachLuong, cboGroupWork, txtFuncWork, txtRequestWork, txtPurpose)

                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtData, "Title")
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_NEXT
                    Template_ImportHU_TITLE()
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload2.Show()
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        ' Dim code = (From a In dataTable.AsEnumerable Where a("ID") = cboTitleGroup.SelectedValue Select a("CODE"))
                        'txtCode.Text = rep.AutoGenCode("CD", "HU_TITLE", "CODE")
                        'txtCode.Text = rep.AutoGenCode(TITLE_GROUP, "HU_TITLE", "CODE")
                        'GenerateTitleCode()
                        objTitle.CODE = txtCode.Text.Trim
                        objTitle.NAME_VN = txtNameVN.Text.Trim
                        objTitle.NAME_EN = txtNameVN.Text.Trim
                        objTitle.REMARK = txtRemark.Text.Trim

                        If cboTitleGroup.SelectedValue <> "" Then
                            objTitle.TITLE_GROUP_ID = cboTitleGroup.SelectedValue
                        End If
                        If cboNgachLuong.SelectedValue <> "" Then
                            objTitle.GR_GLONE_ID = cboNgachLuong.SelectedValue
                        End If
                        If cboGroupWork.SelectedValue <> "" Then
                            objTitle.GR_WORK_ID = cboGroupWork.SelectedValue
                        End If
                        objTitle.FUNCTION_WORK = txtFuncWork.Text
                        objTitle.REQUEST_WORK = txtRequestWork.Text
                        objTitle.PURPOSE_WORK = txtPurpose.Text

                        objTitle.IS_SIGN = chkSign.Checked

                        If cboOrgType.SelectedValue <> "" Then
                            objTitle.ORG_TYPE = cboOrgType.SelectedValue
                        End If

                        If cboHurtType.SelectedValue <> "" Then
                            objTitle.HURT_TYPE_ID = cboHurtType.SelectedValue
                        End If
                        objTitle.OVT = If(ckOVT.Checked = True, -1, 0)

                        'Attach File
                        objTitle.FILENAME = txtUpload.Text.Trim
                        objTitle.UPLOAD_FILE = If(Down_File Is Nothing, "", Down_File)
                        If objTitle.UPLOAD_FILE = "" Then
                            objTitle.UPLOAD_FILE = If(txtRemindLink.Text Is Nothing, "", txtRemindLink.Text)
                        Else
                            objTitle.UPLOAD_FILE = If(objTitle.UPLOAD_FILE Is Nothing, "", objTitle.UPLOAD_FILE)
                        End If

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objTitle.ACTFLG = "A"
                                If rep.InsertTitle(objTitle, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If

                            Case CommonMessage.STATE_EDIT
                                objTitle.ID = rgMain.SelectedValue

                                Dim lst As New List(Of Decimal)
                                lst.Add(objTitle.ID)
                                If commonRes.CheckExistIDTable(lst, "HU_TITLE", "ID") Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                                    Exit Sub
                                End If

                                If rep.ModifyTitle(objTitle, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objTitle.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select

                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgMain')")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ClearControlValue(txtCode, txtNameVN, txtRemark, cboTitleGroup, cboNgachLuong, cboGroupWork, txtFuncWork, txtRequestWork, txtPurpose)
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> Event xử lý sự kiện khi ấn button trên MsgBox </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_ACTIVE
                UpdateControlState()
                ClearControlValue(txtCode, txtNameVN, txtRemark, cboTitleGroup, cboNgachLuong, cboGroupWork, txtFuncWork, txtRequestWork, txtPurpose)
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
                UpdateControlState()
                ClearControlValue(txtCode, txtNameVN, txtRemark, cboTitleGroup, cboNgachLuong, cboGroupWork, txtFuncWork, txtRequestWork, txtPurpose)
            End If

            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
                ClearControlValue(txtCode, txtNameVN, txtRemark, cboTitleGroup, cboNgachLuong, cboGroupWork, txtFuncWork, txtRequestWork, txtPurpose)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CreateDataFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Check sự kiện validate cho combobox tồn tại hoặc ngừng áp dụng</summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New ProfileRepository
        Dim _validate As New TitleDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.ID = rgMain.SelectedValue
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateTitle(_validate)
            Else
                _validate.CODE = txtCode.Text.Trim
                args.IsValid = rep.ValidateTitle(_validate)
            End If

            If Not args.IsValid Then
                ' txtCode.Text = rep.AutoGenCode("CD", "HU_TITLE", "CODE")
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>
    ''' Validate combobox Nhóm chức danh
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalTitleGroup_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalTitleGroup.ServerValidate
        Dim rep As New ProfileRepository
        Dim validate As New OtherListDTO
        Dim dtData As DataTable = Nothing
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            If CurrentState = CommonMessage.STATE_EDIT Or CurrentState = CommonMessage.STATE_NEW Then
                validate.ID = cboTitleGroup.SelectedValue
                validate.ACTFLG = "A"
                validate.CODE = "HU_TITLE_GROUP"
                args.IsValid = rep.ValidateOtherList(validate)
            End If
            If Not args.IsValid Then
                dtData = rep.GetOtherList("HU_TITLE_GROUP", True)
                FillRadCombobox(cboTitleGroup, dtData, "NAME", "ID")

                cboTitleGroup.Items.Insert(0, New RadComboBoxItem("", ""))
                cboTitleGroup.ClearSelection()
                cboTitleGroup.SelectedIndex = 0
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <summary>Xử lý sự kiện khi click btn UploadFile</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUploadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFile.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
            ctrlUpload1.Show()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>Xử lý sự kiện khi click [OK] xác nhận sẽ Upload file</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            txtUploadFile.Text = ""
            Dim listExtension = New List(Of String)
            listExtension.Add(".xls")
            listExtension.Add(".xlsx")
            listExtension.Add(".txt")
            listExtension.Add(".ctr")
            listExtension.Add(".doc")
            listExtension.Add(".docx")
            listExtension.Add(".xml")
            listExtension.Add(".png")
            listExtension.Add(".jpg")
            listExtension.Add(".bitmap")
            listExtension.Add(".jpeg")
            listExtension.Add(".gif")
            listExtension.Add(".pdf")
            listExtension.Add(".rar")
            listExtension.Add(".zip")
            listExtension.Add(".ppt")
            listExtension.Add(".pptx")
            Dim fileName As String

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/TitleInfo/")
            If ctrlUpload1.UploadedFiles.Count >= 1 Then
                For i = 0 To ctrlUpload1.UploadedFiles.Count - 1
                    Dim file As UploadedFile = ctrlUpload1.UploadedFiles(i)
                    Dim str_Filename = Guid.NewGuid.ToString() + "\"
                    If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                        System.IO.Directory.CreateDirectory(strPath + str_Filename)
                        strPath = strPath + str_Filename
                        fileName = System.IO.Path.Combine(strPath, file.FileName)
                        file.SaveAs(fileName, True)
                        txtUploadFile.Text = file.FileName
                        Down_File = str_Filename
                    Else
                        ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file XLS, XLSX, TXT, CTR, DOC, DOCX, XML, PNG, JPG, BITMAP, JPEG, PDF"), NotifyType.Warning)
                        Exit Sub
                    End If
                Next
                loadDatasource(txtUploadFile.Text)
                btnDownload.Enabled = True
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>
    ''' Xử lý sự kiện khi click btnDownload
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim bCheck As Boolean = False
        Try
            If txtUpload.Text <> "" Then
                Dim strPath_Down As String
                If FileOldName = txtUpload.Text.Trim Or FileOldName Is Nothing Then
                    If txtRemindLink.Text IsNot Nothing Then
                        If txtRemindLink.Text <> "" Then
                            strPath_Down = Server.MapPath("~/ReportTemplates/Profile/TitleInfo/" + txtRemindLink.Text)
                            ZipFiles(strPath_Down)
                        End If
                    End If
                Else
                    If Down_File <> "" Then
                        strPath_Down = Server.MapPath("~/ReportTemplates/Profile/TitleInfo/" + Down_File)
                        ZipFiles(strPath_Down)
                    End If
                End If
                'Else
                '    ShowMessage(Translate("Không có gì để  tải xuôgns"), NotifyType.Warning)
                '    Exit Sub
            Else
                Dim dic As New Dictionary(Of String, Control)
                dic.Add("CODE", txtCode)
                dic.Add("NAME_VN", txtNameVN)
                dic.Add("REMARK", txtRemark)
                dic.Add("TITLE_GROUP_ID", cboTitleGroup)
                dic.Add("IS_SIGN", chkSign)
                dic.Add("GR_GLONE_ID", cboNgachLuong)
                dic.Add("GR_WORK_ID", cboGroupWork)
                dic.Add("FUNCTION_WORK", txtFuncWork)
                dic.Add("REQUEST_WORK", txtRequestWork)
                dic.Add("PURPOSE_WORK", txtPurpose)
                dic.Add("ORG_TYPE", cboOrgType)
                dic.Add("HURT_TYPE_ID", cboHurtType)
                dic.Add("OVT_CHECK", ckOVT)
                dic.Add("FILENAME", txtUpload)
                dic.Add("UPLOAD_FILE", txtRemindLink)
                Utilities.OnClientRowSelectedChanged(rgMain, dic)
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Cập nhật trạng thái cho Toolbar</summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            ChangeToolbarState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ZipFiles(ByVal path As String)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Dim crc As New CRC32()
            'Dim pathZip As String = AppDomain.CurrentDomain.BaseDirectory & "Zip\"
            'Dim fileNameZip As String = "ThongTinKhenThuong.zip"
            Dim fileNameZip As String = txtUpload.Text.Trim

            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path & fileNameZip)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            'Response.ContentType = "application/octet-stream"
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
            Response.WriteFile(file.FullName)
            Response.End()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub loadDatasource(ByVal strUpload As String)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If strUpload <> "" Then
                txtUploadFile.Text = strUpload
                FileOldName = txtUpload.Text
                txtUpload.Text = strUpload
            Else
                strUpload = String.Empty
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

    Private Sub Template_ImportHU_TITLE()
        Dim rep As New ProfileRepository
        Try
            Dim configPath As String = Server.MapPath("ReportTemplates\Profile\TMP_HU_TITLE.xls")
            Dim dsData As DataSet = rep.GET_HU_TITLE_IMPORT()
            dsData.Tables(0).TableName = "Table"
            dsData.Tables(1).TableName = "Table1"
            dsData.Tables(2).TableName = "Table2"
            dsData.Tables(3).TableName = "Table3"
            rep.Dispose()
            If File.Exists(configPath) Then
                ExportTemplate(configPath,
                                      dsData, Nothing, "Template_HU_TITLE_" & Format(Date.Now, "yyyyMMdd"))
            Else
                ShowMessage(Translate("Template không tồn tại"), Utilities.NotifyType.Error)
                Exit Sub
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dsData As DataSet,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        'Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            'templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            'filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName

            'cau hinh lai duong dan tren server
            filePath = sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dsData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
#Region "import"
    Private Sub ctrlUpload2_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload2.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Dim dsDataPrepare As New DataSet
        Dim workbook As Aspose.Cells.Workbook
        Dim worksheet As Aspose.Cells.Worksheet

        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)

            For Each file As UploadedFile In ctrlUpload2.UploadedFiles
                fileName = System.IO.Path.Combine(savepath, Guid.NewGuid().ToString() & ".xls")
                file.SaveAs(fileName, True)
                workbook = New Aspose.Cells.Workbook(fileName)
                worksheet = workbook.Worksheets(0)
                dsDataPrepare.Tables.Add(worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, True))
                If System.IO.File.Exists(fileName) Then System.IO.File.Delete(fileName)
            Next
            dtData = dtData.Clone()
            TableMapping(dsDataPrepare.Tables(0))
            For Each rows As DataRow In dsDataPrepare.Tables(0).Select("GR_NGACH_LUONG<>'""'").CopyToDataTable.Rows
                If IsDBNull(rows("GR_NGACH_LUONG")) OrElse rows("GR_NGACH_LUONG") = "" Then Continue For
                Dim newRow As DataRow = dtData.NewRow
                newRow("GR_NGACH_LUONG") = rows("GR_NGACH_LUONG")
                newRow("GR_NGACH_LUONG_ID") = If(IsNumeric(rows("GR_NGACH_LUONG_ID")), rows("GR_NGACH_LUONG_ID"), 0)
                newRow("GR_TITLE") = rows("GR_TITLE")
                newRow("GR_TITLE_ID") = If(IsNumeric(rows("GR_TITLE_ID")), rows("GR_TITLE_ID"), 0)
                newRow("CODE_TITLE") = rows("CODE_TITLE")
                newRow("NAME_TITLE") = rows("NAME_TITLE")
                newRow("GROUP_WORK") = rows("GROUP_WORK")
                newRow("GROUP_WORK_ID") = If(IsNumeric(rows("GROUP_WORK_ID")), rows("GROUP_WORK_ID"), 0)
                newRow("FUNCTION_WORK") = rows("FUNCTION_WORK")
                newRow("REQUEST_WORK") = rows("REQUEST_WORK")
                newRow("PURPOSE_WORK") = rows("PURPOSE_WORK")
                newRow("IS_SIGN") = rows("IS_SIGN")
                newRow("IS_SIGN_ID") = If(IsNumeric(rows("IS_SIGN_ID")), rows("IS_SIGN_ID"), 0)
                newRow("HURT_FULL") = rows("HURT_FULL")
                newRow("HURT_FULL_ID") = If(IsNumeric(rows("HURT_FULL_ID")), rows("HURT_FULL_ID"), 0)
                dtData.Rows.Add(newRow)
            Next
            dtData.TableName = "DATA"
            If loadToGrid() Then
                Dim sw As New StringWriter()
                Dim DocXml As String = String.Empty
                dtData.WriteXml(sw, False)
                DocXml = sw.ToString
                Dim sp As New ProfileStoreProcedure()
                If sp.Import_HU_TITLE(LogHelper.GetUserLog().Username.ToUpper, DocXml) Then
                    ShowMessage(Translate("Import thành công"), NotifyType.Success)
                Else
                    ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                End If
                'End edit;
                rgMain.Rebind()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub TableMapping(ByVal dtdata As DataTable)
        Dim row As DataRow = dtdata.Rows(1)
        Dim index As Integer = 0
        For Each cols As DataColumn In dtdata.Columns
            Try
                cols.ColumnName = row(index)
                index += 1
                If index > row.ItemArray.Length - 1 Then Exit For
            Catch ex As Exception
                Exit For
            End Try
        Next
        dtdata.Rows(0).Delete()
        dtdata.Rows(0).Delete()
        dtdata.AcceptChanges()
    End Sub
    Function loadToGrid() As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtError As New DataTable("ERROR")
        Try
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate("Chưa chọn nhóm ngạch lương"), NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            dtDataImportWorking = dtData.Clone
            dtError = dtData.Clone
            Dim iRow = 1
            Dim _filter As New WorkingDTO
            Dim rep As New ProfileBusinessRepository
            Dim IBusiness As IProfileBusiness = New ProfileBusinessClient()
            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                isError = False
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("GR_NGACH_LUONG", row, rowError, isError, sError)


                If row("GR_TITLE_ID") Is DBNull.Value OrElse row("GR_TITLE") = "" Then
                    sError = "Chưa chọn phân loại nhân viên"
                    ImportValidate.EmptyValue("GR_TITLE_ID", row, rowError, isError, sError)
                End If
                If row("CODE_TITLE") = "" Then
                    sError = "Chưa chọn Mã chức danh"
                    ImportValidate.EmptyValue("CODE_TITLE", row, rowError, isError, sError)
                Else
                    Dim rep1 As New ProfileStoreProcedure()
                    If rep1.CHECK_EXIT_HU_TITLE(row("CODE_TITLE")) Then
                        sError = "Mã chức danh đã tồn tại,xin kiểm tra lại"
                        row("CODE_TITLE") = ""
                        ImportValidate.EmptyValue("CODE_TITLE", row, rowError, True, sError)
                        isError = True
                    End If
                End If
                If row("NAME_TITLE") = "" Then
                    sError = "Chưa chọn Tên chức danh"
                    ImportValidate.EmptyValue("NAME_TITLE", row, rowError, isError, sError)
                End If
                If isError Then
                    rowError("GR_NGACH_LUONG") = row("GR_NGACH_LUONG").ToString
                    If rowError("GR_NGACH_LUONG").ToString = "" Then
                        rowError("GR_NGACH_LUONG") = row("GR_NGACH_LUONG").ToString
                    End If
                    dtError.Rows.Add(rowError)
                Else
                    dtDataImportWorking.ImportRow(row)
                End If
                iRow = iRow + 1
            Next
            If dtError.Rows.Count > 0 Then
                dtError.TableName = "DATA"
                ' gộp các lỗi vào 1 cột ghi chú 
                Dim dtErrorGroup As New DataTable
                Dim RowErrorGroup As DataRow
                dtErrorGroup.Columns.Add("STT")
                dtErrorGroup.Columns.Add("NOTE")
                For j As Integer = 0 To dtError.Rows.Count - 1
                    Dim strNote As String = String.Empty
                    RowErrorGroup = dtErrorGroup.NewRow
                    For k As Integer = 1 To dtError.Columns.Count - 1
                        If Not dtError.Rows(j)(k) Is DBNull.Value Then
                            strNote &= dtError.Rows(j)(k) & "\"
                        End If
                    Next
                    RowErrorGroup("STT") = dtError.Rows(j)("GR_NGACH_LUONG")
                    RowErrorGroup("NOTE") = strNote
                    dtErrorGroup.Rows.Add(RowErrorGroup)
                Next
                dtErrorGroup.TableName = "DATA"
                Session("EXPORTREPORT") = dtErrorGroup
                rep.Dispose()
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_importIO_error');", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
            If isError OrElse (dtError IsNot Nothing AndAlso dtError.Rows.Count > 0) Then
                Return False
            Else
                Return True
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
#End Region
End Class
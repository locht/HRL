Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports Common
Imports Common.Common
Imports Common.CommonMessage
Public Class ctrlHU_EmpDtlBackGround
    Inherits CommonView
    Dim employeeCode As String
    'Public Overrides Property MustAuthorize As Boolean = False
#Region "Properties"
    Property SelectedItem As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_SelectedItem")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_SelectedItem") = value
        End Set
    End Property
    Property EmployeeInfo As EmployeeDTO
        Get
            Return PageViewState(Me.ID & "_EmployeeInfo")
        End Get
        Set(ByVal value As EmployeeDTO)
            PageViewState(Me.ID & "_EmployeeInfo") = value
        End Set
    End Property

    Property isLoad As Boolean
        Get
            Return ViewState(Me.ID & "_isLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isLoad") = value
        End Set
    End Property

    Property lstWorkingBefore As List(Of WorkingBeforeDTO)
        Get
            Return ViewState(Me.ID & "_lstWorkingBefore")
        End Get
        Set(ByVal value As List(Of WorkingBeforeDTO))
            ViewState(Me.ID & "_lstWorkingBefore") = value
        End Set
    End Property

    Property lstBackGround As List(Of EmployeeBackgroundDTO)
        Get
            Return ViewState(Me.ID & "_lstBackGround")
        End Get
        Set(ByVal value As List(Of EmployeeBackgroundDTO))
            ViewState(Me.ID & "_lstBackGround") = value
        End Set
    End Property

    'Lưu lại đang ở View nào để load khi dùng nút Next và Previous để chuyển sang xem thông tin nhân viên khác.
    Public Property CurrentPlaceHolder As String
        Get
            Return PageViewState(Me.ID & "_CurrentPlaceHolder")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_CurrentPlaceHolder") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ctrlEmpBasicInfo.SetProperty("EmployeeInfo", EmployeeInfo)
            UpdateControlState()
            Refresh()
            Me.CurrentPlaceHolder = Me.ViewName
            ctrlEmpBasicInfo.SetProperty("CurrentPlaceHolder", Me.CurrentPlaceHolder)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            rgGrid.SetFilter()
            rgGrid.AllowCustomPaging = True
            InitControl()
            If Not IsPostBack Then
                ViewConfig(RadPane1)
                ViewConfig(RadPane2)
                ViewConfig(RadPane3)
                GirdConfig(rgGrid)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim dtData
            Dim dtPlace
            Dim dtLanguageleve As DataTable
            Dim dtLanguage As DataTable
            Using rep As New ProfileRepository
                dtData = rep.GetOtherList("HU_PAPER")
                dtPlace = rep.GetProvinceList(True)
                dtLanguageleve = rep.GetOtherList("LANGUAGE_LEVEL", True)
                dtLanguage = rep.GetOtherList("RC_LANGUAGE_LEVEL", True)
                FillRadCombobox(cboLicensePlace, dtPlace, "NAME", "ID")
                
                
            End Using

            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Seperator, ToolbarItem.Edit,
                         ToolbarItem.Seperator, ToolbarItem.Export, ToolbarItem.Save, ToolbarItem.Seperator,
                         ToolbarItem.Cancel, ToolbarItem.Seperator, ToolbarItem.Delete)

            CType(Me.MainToolBar.Items(5), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_CREATE
                    If EmployeeInfo.WORK_STATUS Is Nothing Or _
                        (EmployeeInfo.WORK_STATUS IsNot Nothing AndAlso _
                         (EmployeeInfo.WORK_STATUS <> ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Or _
                          (EmployeeInfo.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID And _
                           EmployeeInfo.TER_EFFECT_DATE > Date.Now.Date))) Then
                        CurrentState = CommonMessage.STATE_NEW
                        ResetControlValue()
                    Else
                        ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                        Exit Sub
                    End If
                Case TOOLBARITEM_EDIT
                    ' gọi lại hàm này để lấy những row được select sau khi sửa xong và click sửa thêm 1 lần nữa 
                    rgGrid_SelectedIndexChanged(Nothing, Nothing)
                    If SelectedItem Is Nothing Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If SelectedItem.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If SelectedItem.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If EmployeeInfo.WORK_STATUS Is Nothing Or _
                        (EmployeeInfo.WORK_STATUS IsNot Nothing AndAlso _
                         (EmployeeInfo.WORK_STATUS <> ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Or _
                          (EmployeeInfo.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID And _
                           EmployeeInfo.TER_EFFECT_DATE > Date.Now.Date))) Then
                        CurrentState = CommonMessage.STATE_EDIT
                    Else
                        ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                        Exit Sub
                    End If
                Case TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Select Case CurrentState
                            Case STATE_NEW
                                If Execute() Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    UpdateControlState()
                                    rgGrid.Rebind()
                                    ClearControlValue(rdEffectiveDate, txtIdNo, rdLicenseDate, cboLicensePlace, txtCurrentAddress,
                                                      cboNation_Cur, cboProvince_Cur, cboDistrict_Cur, cboWard_Cur,
                                                      cboNation_Per, cboProvince_Per, cboDistrict_Per, cboWard_Per)
                                    ExcuteScript("Clear", "clRadDatePicker()")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case STATE_EDIT
                                If Not String.IsNullOrEmpty(hidBackGroundID.Value) Then
                                    Dim cmRep As New CommonRepository
                                    Dim lstID As New List(Of Decimal)

                                    lstID.Add(Convert.ToDecimal(hidBackGroundID.Value))

                                    If cmRep.CheckExistIDTable(lstID, "HU_EMPLOYEE_BACKGROUND", "ID") Then
                                        ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Warning)
                                        Exit Sub
                                    End If
                                Else
                                    Exit Sub
                                End If

                                If Execute() Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    UpdateControlState()
                                    rgGrid.Rebind()
                                    ClearControlValue(rdEffectiveDate, txtIdNo, rdLicenseDate, cboLicensePlace, txtCurrentAddress,
                                                      cboNation_Cur, cboProvince_Cur, cboDistrict_Cur, cboWard_Cur,
                                                      cboNation_Per, cboProvince_Per, cboDistrict_Per, cboWard_Per)
                                    ExcuteScript("Clear", "clRadDatePicker()")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                        SelectedItem = Nothing
                        isLoad = False
                    End If
                Case TOOLBARITEM_CANCEL
                    SelectedItem = Nothing
                    If CurrentState = STATE_NEW Then
                        ResetControlValue()
                    End If
                    CurrentState = CommonMessage.STATE_NORMAL
                Case TOOLBARITEM_DELETE
                    If SelectedItem Is Nothing Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If SelectedItem.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If EmployeeInfo.WORK_STATUS Is Nothing Or _
                        (EmployeeInfo.WORK_STATUS IsNot Nothing AndAlso _
                         (EmployeeInfo.WORK_STATUS <> ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Or _
                          (EmployeeInfo.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID And _
                           EmployeeInfo.TER_EFFECT_DATE > Date.Now.Date))) Then
                        ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                        ctrlMessageBox.Show()
                    Else
                        ShowMessage(Translate("Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin."), Utilities.NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                        Exit Sub
                    End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = lstBackGround.ToTable()
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgGrid.ExportExcel(Server, Response, dtData, "EmpDtlBackGround")
                            Exit Sub
                        End If
                    End Using
            End Select
            UpdateControlState()
            rgGrid_NeedDataSource(Nothing, Nothing)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgGrid.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            If SelectedItem IsNot Nothing Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                If SelectedItem.Contains(Decimal.Parse(datarow("ID").Text)) Then
                    e.Item.Selected = True
                End If
            End If
        End If
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            If DeleteBackGround() Then  'Xóa
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                rgGrid_NeedDataSource(Nothing, Nothing)
                'Hủy selectedItem của grid.
                SelectedItem = Nothing
                CurrentState = CommonMessage.STATE_NORMAL
                UpdateControlState()
                rgGrid.Rebind()
                ClearControlValue(rdEffectiveDate, txtIdNo, rdLicenseDate, cboLicensePlace, txtCurrentAddress,
                                                      cboNation_Cur, cboProvince_Cur, cboDistrict_Cur, cboWard_Cur,
                                                      cboNation_Per, cboProvince_Per, cboDistrict_Per, cboWard_Per)
                ExcuteScript("Clear", "clRadDatePicker()")
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If

        End If
    End Sub

    Private Sub rgGrid_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGrid.NeedDataSource
        If EmployeeInfo IsNot Nothing Then
            Dim rep As New ProfileBusinessRepository
            Dim MaximumRows As Integer
            Dim objBackGround As New EmployeeBackgroundDTO
            objBackGround.EMPLOYEE_ID = EmployeeInfo.ID
            hidEmployeeID.Value = EmployeeInfo.ID.ToString()
            lstBackGround = rep.GetEmpBackGround(objBackGround)
            'lstWorkingBefore = rep.GetEmpWorkingBefore(objBackGround)
            rgGrid.VirtualItemCount = MaximumRows
            rgGrid.DataSource = lstBackGround
            rep.Dispose()
        Else
            rgGrid.DataSource = New List(Of WorkingBeforeDTO)
        End If
    End Sub

    Private Sub rgGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgGrid.SelectedIndexChanged
        Try
            If rgGrid.SelectedItems.Count = 0 Then Exit Sub
            'Lưu vào viewStates để giữ những item được select phục vụ cho phương thức delete.
            SelectedItem = New List(Of Decimal)
            For Each dr As Telerik.Web.UI.GridDataItem In rgGrid.SelectedItems
                SelectedItem.Add(Decimal.Parse(dr("ID").Text))
            Next
            Dim item As GridDataItem = CType(rgGrid.SelectedItems(0), GridDataItem)
            hidBackGroundID.Value = item.GetDataKeyValue("ID")
            rdEffectiveDate.SelectedDate = item.GetDataKeyValue("EFFECTIVE_DATE")

            txtIdNo.Text = item.GetDataKeyValue("ID_NO")
            txtMobilePhone.Text = item.GetDataKeyValue("MOBILE_PHONE")
            txtFixedPhone.Text = item.GetDataKeyValue("FIXED_PHONE")
            rdLicenseDate.SelectedDate = item.GetDataKeyValue("LICENSE_DATE")
            cboLicensePlace.SelectedValue = item.GetDataKeyValue("LICENSE_PLACE_ID")
            cboLicensePlace.Text = item.GetDataKeyValue("LICENSE_PLACE")

            txtCurrentAddress.Text = item.GetDataKeyValue("CURRENT_ADDRESS_F")
            cboNation_Cur.SelectedValue = item.GetDataKeyValue("CURRENT_NATION_ID")
            cboProvince_Cur.SelectedValue = item.GetDataKeyValue("CURRENT_PROVINCE_ID")
            cboDistrict_Cur.SelectedValue = item.GetDataKeyValue("CURRENT_DISTRICT_ID")
            cboWard_Cur.SelectedValue = item.GetDataKeyValue("CURRENT_WARD_ID")

            cboNation_Cur.Text = item.GetDataKeyValue("CURRENT_NATION_NAME")
            cboProvince_Cur.Text = item.GetDataKeyValue("CURRENT_PROVINCE_NAME")
            cboDistrict_Cur.Text = item.GetDataKeyValue("CURRENT_DISTRICT_NAME")
            cboWard_Cur.Text = item.GetDataKeyValue("CURRENT_WARD_NAME")

            txtPermanentAddress.Text = item.GetDataKeyValue("PERMANENT_ADDRESS_F")
            cboNation_Per.SelectedValue = item.GetDataKeyValue("PERMANENT_NATION_ID")
            cboProvince_Per.SelectedValue = item.GetDataKeyValue("PERMANENT_PROVINCE_ID")
            cboDistrict_Per.SelectedValue = item.GetDataKeyValue("PERMANENT_DISTRICT_ID")
            cboWard_Per.SelectedValue = item.GetDataKeyValue("PERMANENT_WARD_ID")

            cboNation_Per.Text = item.GetDataKeyValue("PERMANENT_NATION_NAME")
            cboProvince_Per.Text = item.GetDataKeyValue("PERMANENT_PROVINCE_NAME")
            cboDistrict_Per.Text = item.GetDataKeyValue("PERMANENT_DISTRICT_NAME")
            cboWard_Per.Text = item.GetDataKeyValue("PERMANENT_WARD_NAME")
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Protected Sub cboCommon_ItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs) _
    Handles cboNation_Cur.ItemsRequested, cboProvince_Cur.ItemsRequested, cboDistrict_Cur.ItemsRequested, cboWard_Cur.ItemsRequested,
        cboNation_Per.ItemsRequested, cboProvince_Per.ItemsRequested, cboDistrict_Per.ItemsRequested, cboWard_Per.ItemsRequested
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Using rep As New ProfileRepository
                Dim dtData As DataTable
                Dim sText As String = e.Text
                Dim dValue As Decimal
                Dim sSelectValue As String = IIf(e.Context("value") IsNot Nothing, e.Context("value"), "")
                Select Case sender.ID

                    Case cboNation_Cur.ID
                        dtData = rep.GetNationList(True)
                    Case cboNation_Per.ID
                        dtData = rep.GetNationList(True)

                    Case cboProvince_Cur.ID, cboProvince_Per.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        dtData = rep.GetProvinceList1(dValue, True)
                    Case cboDistrict_Cur.ID, cboDistrict_Per.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        dtData = rep.GetDistrictList(dValue, True)
                    Case cboWard_Cur.ID, cboWard_Per.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        dtData = rep.GetWardList(dValue, True)
                End Select

                If sText <> "" Then
                    Dim dtExist = (From p In dtData
                                   Where p("NAME") IsNot DBNull.Value AndAlso
                                  p("NAME").ToString.ToUpper = sText.ToUpper)

                    'If dtExist.Count = 0 Then
                    Dim dtFilter = (From p In dtData
                                    Where p("NAME") IsNot DBNull.Value AndAlso
                              p("NAME").ToString.ToUpper.Contains(sText.ToUpper))
                    'Dim dtFilter = (From p In dtData
                    '                Where p("NAME") IsNot DBNull.Value)

                    If dtFilter.Count > 0 Then
                        dtData = dtFilter.CopyToDataTable
                    Else
                        dtData = dtData.Clone
                    End If

                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                    e.EndOfItems = endOffset = dtData.Rows.Count
                    sender.Items.Clear()
                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                        Select Case sender.ID
                            'Case cboTitle.ID
                            ' radItem.Attributes("GROUP_NAME") = dtData.Rows(i)("GROUP_NAME").ToString()
                        End Select
                        sender.Items.Add(radItem)
                    Next
                Else
                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                    e.EndOfItems = endOffset = dtData.Rows.Count
                    sender.Items.Clear()
                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                        Select Case sender.ID
                            'Case cboTitle.ID
                            'radItem.Attributes("GROUP_NAME") = dtData.Rows(i)("GROUP_NAME").ToString()
                        End Select
                        sender.Items.Add(radItem)
                    Next
                End If
            End Using
            '_mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            ' DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"
    Private Function Execute() As Boolean
        Try
            Dim objBackGround As New EmployeeBackgroundDTO
            Dim rep As New ProfileBusinessRepository

            'objBackGround.ID = CDec(Val(hidBackGroundID.Value))
            'Decimal.Parse(hidBackGroundID.Value)
            objBackGround.EMPLOYEE_ID = Decimal.Parse(hidEmployeeID.Value)

            objBackGround.EFFECTIVE_DATE = rdEffectiveDate.SelectedDate()

            objBackGround.ID_NO = txtIdNo.Text.Trim()
            objBackGround.LICENSE_DATE = rdLicenseDate.SelectedDate()

            If cboLicensePlace.SelectedValue <> "" Then
                objBackGround.LICENSE_PLACE_ID = CDec(Val(cboLicensePlace.SelectedValue))
            End If
            'objBackGround.LICENSE_PLACE_ID = CDec(Val(cboLicensePlace.SelectedValue))

            objBackGround.MOBILE_PHONE = txtMobilePhone.Text.Trim()
            objBackGround.FIXED_PHONE = txtFixedPhone.Text.Trim()

            objBackGround.CURRENT_ADDRESS_F = txtCurrentAddress.Text.ToString.Trim()

            If cboNation_Cur.SelectedValue <> "" Then
                objBackGround.CURRENT_NATION_ID = CDec(Val(cboNation_Cur.SelectedValue))
            End If
            If cboProvince_Cur.SelectedValue <> "" Then
                objBackGround.CURRENT_PROVINCE_ID = CDec(Val(cboProvince_Cur.SelectedValue))
            End If
            If cboDistrict_Cur.SelectedValue <> "" Then
                objBackGround.CURRENT_DISTRICT_ID = CDec(Val(cboDistrict_Cur.SelectedValue))
            End If
            If cboWard_Cur.SelectedValue <> "" Then
                objBackGround.CURRENT_WARD_ID = CDec(Val(cboWard_Cur.SelectedValue))
            End If

            'objBackGround.CURRENT_NATION_ID = CDec(Val(cboNation_Cur.SelectedValue))
            'objBackGround.CURRENT_PROVINCE_ID = CDec(Val(cboProvince_Cur.SelectedValue))
            'objBackGround.CURRENT_DISTRICT_ID = CDec(Val(cboDistrict_Cur.SelectedValue))
            'objBackGround.CURRENT_WARD_ID = CDec(Val(cboWard_Cur.SelectedValue))

            objBackGround.PERMANENT_ADDRESS_F = txtPermanentAddress.Text.Trim()

            If cboNation_Per.SelectedValue <> "" Then
                objBackGround.PERMANENT_NATION_ID = CDec(Val(cboNation_Per.SelectedValue))
            End If
            If cboProvince_Per.SelectedValue <> "" Then
                objBackGround.PERMANENT_PROVINCE_ID = CDec(Val(cboProvince_Per.SelectedValue))
            End If
            If cboDistrict_Per.SelectedValue <> "" Then
                objBackGround.PERMANENT_DISTRICT_ID = CDec(Val(cboDistrict_Per.SelectedValue))
            End If
            If cboWard_Per.SelectedValue <> "" Then
                objBackGround.PERMANENT_WARD_ID = CDec(Val(cboWard_Per.SelectedValue))
            End If
            'objBackGround.PERMANENT_NATION_ID = CDec(Val(cboNation_Per.SelectedValue))
            'objBackGround.PERMANENT_PROVINCE_ID = CDec(Val(cboProvince_Per.SelectedValue))
            'objBackGround.PERMANENT_DISTRICT_ID = CDec(Val(cboDistrict_Per.SelectedValue))
            'objBackGround.PERMANENT_WARD_ID = CDec(Val(cboWard_Per.SelectedValue))


            Dim gID As Decimal
            If Not IsNumeric(hidBackGroundID.Value) Then
                rep.InsertBackGround(objBackGround, gID)
            Else
                objBackGround.ID = Decimal.Parse(hidBackGroundID.Value)
                rep.ModifyBackGround(objBackGround, gID)
            End If
            rgGrid.Rebind()
            rep.Dispose()
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Overrides Sub UpdateControlState()
        Try
            If CurrentState Is Nothing Then
                CurrentState = STATE_NORMAL
            End If
            Select Case CurrentState
                Case STATE_NEW
                    SetStatusControl(True)
                    EnabledGrid(rgGrid, False)
                    hidBackGroundID.Value = ""
                Case STATE_EDIT
                    EnabledGrid(rgGrid, False)
                    SetStatusControl(True)
                Case STATE_NORMAL
                    EnabledGrid(rgGrid, True)
                    SetStatusControl(False)
                    rgGrid.Rebind()
                    ResetControlValue()
            End Select
            Me.Send(CurrentState)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub SetStatusToolBar()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SetStatusControl(ByVal sTrangThai As Boolean)
        Try
            txtIdNo.ReadOnly = Not sTrangThai
            'cboLicensePlace.Enabled = Not sTrangThai
            txtCurrentAddress.ReadOnly = Not sTrangThai
            txtPermanentAddress.ReadOnly = Not sTrangThai
            txtIdNo.ReadOnly = Not sTrangThai

            'txtsalary.readonly = Not sTrangThai
            'txttelephone.readonly = Not sTrangThai
            'txtterreason.readonly = Not sTrangThai

            rdEffectiveDate.Enabled = sTrangThai
            rdLicenseDate.Enabled = sTrangThai
            SetStatusToolBar()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub ResetControlValue()
        hidBackGroundID.Value = ""
        txtIdNo.Text = ""
        txtCurrentAddress.Text = ""
        txtPermanentAddress.Text = ""
        txtMobilePhone.Text = ""
        txtFixedPhone.Text = ""
        cboNation_Cur.SelectedValue = ""
        cboProvince_Cur.SelectedValue = ""
        cboDistrict_Cur.SelectedValue = ""
        cboWard_Cur.SelectedValue = ""
        cboNation_Per.SelectedValue = ""
        cboProvince_Per.SelectedValue = ""
        cboDistrict_Per.SelectedValue = ""
        cboWard_Per.SelectedValue = ""
        rdEffectiveDate.Clear()
        rdLicenseDate.Clear()
    End Sub

    Private Function DeleteBackGround() As Boolean
        Try
            Dim rep As New ProfileBusinessRepository
            Return rep.DeleteBackGround(SelectedItem)
            rep.Dispose()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class
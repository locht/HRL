Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Common.Common
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlPortalTrainingOutCompany_Edit
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"

    Public Property EmployeeID As Decimal

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            rgTrainingOutCompany.SetFilter()
            rgTrainingOutCompanyEdit.SetFilter()

            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Seperator,
                         ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Seperator,
                         ToolbarItem.Submit)
            CType(Me.MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileBusinessRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            Dim rep As New ProfileRepository
            Dim comboBoxDataDTO As New ComboBoxDataDTO
            If comboBoxDataDTO Is Nothing Then
                comboBoxDataDTO = New ComboBoxDataDTO
            End If
            comboBoxDataDTO.GET_TRAINING_FORM = True
            comboBoxDataDTO.GET_LEVEL_TRAIN = True
            comboBoxDataDTO.GET_MAJOR = True
            comboBoxDataDTO.GET_MARK_EDU = True
            comboBoxDataDTO.GET_CERTIFICATE_TYPE = True
            comboBoxDataDTO.GET_GRADUATE_SCHOOL = True
            rep.GetComboList(comboBoxDataDTO)
            If comboBoxDataDTO IsNot Nothing Then
                FillDropDownList(cboTrainingForm, comboBoxDataDTO.LIST_TRAINING_FORM, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboTrainingForm.SelectedValue)
                FillDropDownList(cboRemark, comboBoxDataDTO.LIST_CERTIFICATE_TYPE, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboRemark.SelectedValue)
                FillDropDownList(cboLevelId, comboBoxDataDTO.LIST_LEVEL_TRAIN, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboLevelId.SelectedValue)
                FillDropDownList(cboKetQua, comboBoxDataDTO.LIST_MARK_EDU, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboKetQua.SelectedValue)
                FillDropDownList(cboChuyenNganh, comboBoxDataDTO.LIST_MAJOR, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboChuyenNganh.SelectedValue)
                FillDropDownList(cboTrainingSchool, comboBoxDataDTO.LIST_GRADUATE_SCHOOL, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboTrainingSchool.SelectedValue)
            End If
            ' FK_PKEY
            Dim dic As New Dictionary(Of String, Control)
            dic.Add("FROM_DATE", rdTuNgay)
            dic.Add("TO_DATE", rdToiNgay)
            dic.Add("YEAR_GRA", rntGraduateYear)
            dic.Add("SCHOOLS_ID", cboTrainingSchool)
            dic.Add("FORM_TRAIN_ID", cboTrainingForm)
            dic.Add("SPECIALIZED_TRAIN_ID", cboChuyenNganh)
            dic.Add("RESULT_TRAIN_ID", cboKetQua)
            dic.Add("CERTIFICATE_ID", cboRemark)
            dic.Add("LEVEL_ID", cboLevelId)
            dic.Add("CONTENT_LEVEL", rtxtContentLevel)
            'dic.Add("EFFECTIVE_DATE_FROM", rdFrom)
            'dic.Add("EFFECTIVE_DATE_TO", rdTo)
            dic.Add("ID", hidProcessTrainID)
            Utilities.OnClientRowSelectedChanged(rgTrainingOutCompany, dic)

            Dim dic1 As New Dictionary(Of String, Control)
            dic1.Add("FROM_DATE", rdTuNgay)
            dic1.Add("TO_DATE", rdToiNgay)
            dic1.Add("YEAR_GRA", rntGraduateYear)
            dic1.Add("SCHOOLS_ID", cboTrainingSchool)
            dic1.Add("FORM_TRAIN_ID", cboTrainingForm)
            dic1.Add("SPECIALIZED_TRAIN_ID", cboChuyenNganh)
            dic1.Add("RESULT_TRAIN_ID", cboKetQua)
            dic1.Add("CERTIFICATE_ID", cboRemark)
            dic1.Add("LEVEL_ID", cboLevelId)
            dic1.Add("CONTENT_TRAIN", rtxtContentLevel)
            'dic1.Add("EFFECTIVE_DATE_FROM", rdFrom)
            'dic1.Add("EFFECTIVE_DATE_TO", rdTo)
            dic1.Add("ID", hidID)
            dic1.Add("FK_PKEY", hidProcessTrainID)
            Utilities.OnClientRowSelectedChanged(rgTrainingOutCompanyEdit, dic1)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    EnableControlAll(False, rdToiNgay, rdTuNgay, rntGraduateYear, cboTrainingForm, cboRemark, cboChuyenNganh, cboKetQua, cboTrainingSchool, rtxtContentLevel, cboLevelId)

                    EnabledGridNotPostback(rgTrainingOutCompany, True)
                    EnabledGridNotPostback(rgTrainingOutCompanyEdit, True)

                Case CommonMessage.STATE_NEW
                    EnableControlAll(True, rdToiNgay, rdTuNgay, rntGraduateYear, cboTrainingForm, cboRemark, cboChuyenNganh, cboKetQua, cboTrainingSchool, rtxtContentLevel, cboLevelId)

                    EnabledGridNotPostback(rgTrainingOutCompany, False)
                    EnabledGridNotPostback(rgTrainingOutCompanyEdit, False)
                Case CommonMessage.STATE_EDIT
                    EnableControlAll(True, rdToiNgay, rdTuNgay, rntGraduateYear, cboTrainingForm, cboRemark, cboChuyenNganh, cboKetQua, cboTrainingSchool, rtxtContentLevel, cboLevelId)

                    EnabledGridNotPostback(rgTrainingOutCompany, False)
                    EnabledGridNotPostback(rgTrainingOutCompanyEdit, False)
            End Select

            ChangeToolbarState()

        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(rdTuNgay, rdToiNgay, rntGraduateYear, cboTrainingForm, cboRemark, cboChuyenNganh, cboKetQua, cboTrainingSchool, hidProcessTrainID, hidID, rtxtContentLevel, cboLevelId)
                    rdTuNgay.SelectedDate = Nothing
                    rdToiNgay.SelectedDate = Nothing
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim isInsert As Boolean = True

                        Dim objTrain As New HU_PRO_TRAIN_OUT_COMPANYDTOEDIT
                        objTrain.EMPLOYEE_ID = EmployeeID
                        objTrain.FROM_DATE = rdTuNgay.SelectedDate
                        objTrain.TO_DATE = rdToiNgay.SelectedDate
                        If cboTrainingSchool.SelectedValue = "" Then
                            objTrain.SCHOOLS_ID = Nothing
                        Else
                            objTrain.SCHOOLS_ID = cboTrainingSchool.SelectedValue
                        End If
                        If cboTrainingForm.SelectedValue = "" Then
                            objTrain.FORM_TRAIN_ID = Nothing
                        Else
                            objTrain.FORM_TRAIN_ID = cboTrainingForm.SelectedValue
                        End If
                        objTrain.YEAR_GRA = rntGraduateYear.Value
                        If cboChuyenNganh.SelectedValue = "" Then
                            objTrain.SPECIALIZED_TRAIN_ID = Nothing
                        Else
                            objTrain.SPECIALIZED_TRAIN_ID = cboChuyenNganh.SelectedValue
                        End If
                        If cboKetQua.SelectedValue = "" Then
                            objTrain.RESULT_TRAIN_ID = Nothing
                        Else
                            objTrain.RESULT_TRAIN_ID = cboKetQua.SelectedValue
                        End If
                        If cboRemark.SelectedValue = "" Then
                            objTrain.CERTIFICATE_ID = Nothing
                        Else
                            objTrain.CERTIFICATE_ID = cboRemark.SelectedValue
                        End If
                        If cboLevelId.SelectedValue = "" Then
                            objTrain.LEVEL_ID = Nothing
                        Else
                            objTrain.LEVEL_ID = cboLevelId.SelectedValue
                        End If
                        objTrain.CONTENT_TRAIN = rtxtContentLevel.Text.Trim.ToLower
                        Using rep As New ProfileBusinessRepository
                            If hidProcessTrainID.Value <> "" Then
                                objTrain.FK_PKEY = hidProcessTrainID.Value
                                Dim bCheck = rep.CheckExistProcessTrainingEdit(hidProcessTrainID.Value)
                                If bCheck IsNot Nothing Then
                                    Dim status = bCheck.STATUS
                                    Dim pkey = bCheck.ID
                                    ' Trạng thái chờ phê duyệt
                                    If status = 1 Then
                                        ShowMessage("Thông tin đang Chờ phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                                        Exit Sub
                                    End If
                                    isInsert = False
                                    objTrain.ID = pkey
                                End If
                            End If


                            If hidID.Value <> "" Then
                                isInsert = False
                            End If

                            If isInsert Then
                                rep.InsertProcessTrainingEdit(objTrain, 0)
                            Else
                                objTrain.ID = hidID.Value
                                rep.ModifyProcessTrainingEdit(objTrain, 0)
                            End If

                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                            rgTrainingOutCompany.Rebind()
                            rgTrainingOutCompanyEdit.Rebind()
                            CurrentState = CommonMessage.STATE_NORMAL
                            UpdateControlState()
                        End Using
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    ClearControlValue(rdTuNgay, rdToiNgay, rntGraduateYear, cboTrainingForm, cboRemark, cboLevelId, cboChuyenNganh, cboKetQua, cboTrainingSchool, hidProcessTrainID, hidID, rtxtContentLevel)
                    rdTuNgay.SelectedDate = Nothing
                    rdToiNgay.SelectedDate = Nothing
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SUBMIT
                    If rgTrainingOutCompanyEdit.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim status As String = ""
                    For Each item As GridDataItem In rgTrainingOutCompanyEdit.SelectedItems
                        status = item.GetDataKeyValue("STATUS")
                        If status = 1 Then
                            ShowMessage("Thông tin đang Chờ phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                            Exit Sub
                        End If
                        If status = 2 Then
                            ShowMessage("Thông tin đang phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                            Exit Sub
                        End If
                        If status = 3 Then
                            ShowMessage("Thông tin đang không phê duyệt, thao tác thực hiện không thành công", NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate("Thông tin đã gửi duyệt sẽ không được chỉnh sửa. Bạn chắc chắn muốn gửi duyệt?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_APPROVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()


            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgTrainingOutCompany_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgTrainingOutCompany.NeedDataSource
        Try
            SetValueObjectByRadGrid(rgTrainingOutCompany, New HU_PRO_TRAIN_OUT_COMPANYDTO)

            Using rep As New ProfileBusinessRepository
                rgTrainingOutCompany.DataSource = rep.GetProcessTraining(New HU_PRO_TRAIN_OUT_COMPANYDTO With {.EMPLOYEE_ID = EmployeeID})
            End Using
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgTrainingOutCompanyEdit_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgTrainingOutCompanyEdit.NeedDataSource
        Try
            SetValueObjectByRadGrid(rgTrainingOutCompanyEdit, New HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)

            Using rep As New ProfileBusinessRepository
                rgTrainingOutCompanyEdit.DataSource = rep.GetProcessTrainingEdit(New HU_PRO_TRAIN_OUT_COMPANYDTOEDIT With {.EMPLOYEE_ID = EmployeeID})
            End Using
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgTrainingOutCompanyEdit_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgTrainingOutCompanyEdit.ItemCommand
        Try
            If e.CommandName = "EditRow" Then
                Dim item = CType(e.Item, GridDataItem)
                Dim status As String = ""
                If item.GetDataKeyValue("STATUS") IsNot Nothing Then
                    status = item.GetDataKeyValue("STATUS")
                End If
                Select Case status
                    Case 1
                        ShowMessage("Bản ghi đang Chờ phê duyệt chỉ được xem thông tin", NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case 2
                        ShowMessage("Bản ghi đã Phê duyệt chỉ được xem thông tin", NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case 3
                        ShowMessage("Bản ghi không được Phê duyệt chỉ được xem thông tin", NotifyType.Warning)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case Else
                        CurrentState = CommonMessage.STATE_EDIT
                End Select
                hidProcessTrainID.Value = item.GetDataKeyValue("ID")

                rdTuNgay.SelectedDate = item.GetDataKeyValue("FROM_DATE")
                rdToiNgay.SelectedDate = item.GetDataKeyValue("FROM_DATE")
                rntGraduateYear.Text = item.GetDataKeyValue("YEAR_GRA")
                cboTrainingSchool.SelectedValue = item.GetDataKeyValue("SCHOOLS_ID")
                cboTrainingForm.SelectedValue = item.GetDataKeyValue("FORM_TRAIN_ID")
                cboChuyenNganh.SelectedValue = item.GetDataKeyValue("SPECIALIZED_TRAIN_ID")
                cboKetQua.SelectedValue = item.GetDataKeyValue("RESULT_TRAIN_ID")
                cboRemark.SelectedValue = item.GetDataKeyValue("CERTIFICATE_ID")
                rtxtContentLevel.Text = item.GetDataKeyValue("CONTENT_TRAIN")
                cboLevelId.SelectedValue = item.GetDataKeyValue("LEVEL_ID")
                If item.GetDataKeyValue("FK_PKEY") IsNot Nothing Then
                    hidProcessTrainID.Value = item.GetDataKeyValue("FK_PKEY")
                End If
                hidID.Value = item.GetDataKeyValue("ID")

                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgTrainingOutCompany_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgTrainingOutCompany.ItemCommand
        Try
            If e.CommandName = "EditRow" Then
                CurrentState = CommonMessage.STATE_EDIT
                Dim item = CType(e.Item, GridDataItem)
                hidProcessTrainID.Value = item.GetDataKeyValue("ID")
                rdTuNgay.SelectedDate = item.GetDataKeyValue("FROM_DATE")
                rdToiNgay.SelectedDate = item.GetDataKeyValue("FROM_DATE")
                rntGraduateYear.Text = item.GetDataKeyValue("YEAR_GRA")
                cboTrainingSchool.SelectedValue = item.GetDataKeyValue("SCHOOLS_ID")
                cboTrainingForm.SelectedValue = item.GetDataKeyValue("FORM_TRAIN_ID")
                cboChuyenNganh.Text = item.GetDataKeyValue("SPECIALIZED_TRAIN_ID")
                cboKetQua.SelectedValue = item.GetDataKeyValue("RESULT_TRAIN_ID")
                cboRemark.SelectedValue = item.GetDataKeyValue("CERTIFICATE_ID")
                rtxtContentLevel.Text = item.GetDataKeyValue("CONTENT_LEVEL")
                cboLevelId.SelectedValue = item.GetDataKeyValue("LEVEL_ID")
                hidProcessTrainID.Value = item.GetDataKeyValue("ID")
                hidID.Value = ""

                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_APPROVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Using rep As New ProfileBusinessRepository
                    Dim lstID As New List(Of Decimal)
                    For Each item As GridDataItem In rgTrainingOutCompanyEdit.SelectedItems
                        lstID.Add(item.GetDataKeyValue("ID"))
                    Next

                    If lstID.Count > 0 Then
                        rep.SendProcessTrainingEdit(lstID)
                    End If
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    rgTrainingOutCompany.Rebind()
                    rgTrainingOutCompanyEdit.Rebind()
                End Using
            End If


        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"
    Private Sub cvalToiNgay_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalToiNgay.ServerValidate
        Try
            If rdToiNgay.SelectedDate IsNot Nothing Then
                If rdTuNgay.SelectedDate > rdToiNgay.SelectedDate Then
                    args.IsValid = False
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

End Class
﻿Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Common
Imports Telerik.Web.UI
Imports HistaffFrameworkPublic
Imports HistaffFrameworkPublic.FrameworkUtilities
Imports Ionic.Zip
Imports System.IO
Imports WebAppLog
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Public Class ctrlHU_ContractTemplete
    Inherits CommonView
    Private hfr As New HistaffFrameworkRepository
    Dim _mylog As New MyLog()
    Dim _flag As Boolean = True
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
#Region "Property"

    Dim rep As New ProfileBusinessRepository
    Protected asp As New HistaffFrameworkRepository
    Private psp As New ProfileStoreProcedure()
    Dim dtFile As New DataTable
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner2 As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSalaryPopup As ctrlFindSalaryPopup
    Private Property EmpCode As EmployeeDTO
    ' Protected WithEvents ctrlLiquidate As ctrlContract_Liquidate
    Property Contract As FileContractDTO
        Get
            Return ViewState(Me.ID & "_Contract")
        End Get
        Set(ByVal value As FileContractDTO)
            ViewState(Me.ID & "_Contract") = value
        End Set
    End Property

    Property FContract As FileContractDTO
        Get
            Return ViewState(Me.ID & "_FContract")
        End Get
        Set(ByVal value As FileContractDTO)
            ViewState(Me.ID & "_FContract") = value
        End Set
    End Property
    Property FileOldName As String
        Get
            Return ViewState(Me.ID & "_FileOldName")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_FileOldName") = value
        End Set
    End Property
    Property Contracts As List(Of FileContractDTO)
        Get
            Return ViewState(Me.ID & "_Contracts")
        End Get
        Set(ByVal value As List(Of FileContractDTO))
            ViewState(Me.ID & "_Contracts") = value
        End Set
    End Property
    Property Down_File As String
        Get
            Return ViewState(Me.ID & "_Down_File")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Down_File") = value
        End Set
    End Property

    Property dtContracts As DataTable
        Get
            Return ViewState(Me.ID & "_dtContracts")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtContracts") = value
        End Set
    End Property

    Property objContracts As DataRow
        Get
            Return ViewState(Me.ID & "_objContracts")
        End Get
        Set(ByVal value As DataRow)
            ViewState(Me.ID & "_objContracts") = value
        End Set
    End Property
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
    Property listContract As List(Of ContractDTO)
        Get
            Return ViewState(Me.ID & "_listContract")
        End Get
        Set(ByVal value As List(Of ContractDTO))
            ViewState(Me.ID & "_listContract") = value
        End Set
    End Property
    Property STT As Decimal
        Get
            Return ViewState(Me.ID & "_STT")
        End Get
        Set(value As Decimal)
            ViewState(Me.ID & "_STT") = value
        End Set
    End Property

    '0 - normal
    '1 - Employee
    '2 - Signer
    '3 - Salary
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    Property IDSelect As String
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    Private Property checkStatus As String
        Get
            Return PageViewState(Me.ID & "_checkStatus")
        End Get
        Set(ByVal value As String)
            PageViewState(Me.ID & "_checkStatus") = value
        End Set
    End Property

    Private Property EndDateCT As Date?
        Get
            Return PageViewState(Me.ID & "_EndDateCT")
        End Get
        Set(ByVal value As Date?)
            PageViewState(Me.ID & "_EndDateCT") = value
        End Set

    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As EventArgs)
        Try

            GetParams()
            Refresh()
            UpdateControlState()
            'If Request.Params("IDSelect") IsNot Nothing Then
            '    MainToolBar.Items(3).Enabled = True
            'End If
            txtContract_NumAppen.Enabled = False
            If (_flag = False) Then
                EnableControlAll_Cus(False, NORMAL)
                'btnDownload.Enabled = True
                'btnUploadFile.Enabled = True
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As EventArgs)
        Try

            InitControl()
            If Not IsPostBack Then
                ViewConfig(NORMAL)
            End If
            'Me.MainToolBar.OnClientButtonClicking = "clientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            ' CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"




        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Public Overrides Sub BindData()
        Try
            GetDataCombo()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            End If
            Me.MainToolBar = tbarContract
            '       ToolbarItem.Delete)
            Common.Common.BuildToolbar(Me.MainToolBar,
                             ToolbarItem.Save,
                             ToolbarItem.Cancel)
            'ToolbarItem.Seperator, ToolbarItem.Print, ToolbarItem.Delete)
            'CType(MainToolBar.Items(2), RadToolBarButton).Enabled = False
            'CType(MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            'tbarContract.Items(3).Text = Translate("Thanh lý hợp đồng")
            'tbarContract.Items(8).Text = Translate("Thông tin nhân viên")

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        'Dim rep As New ProfileBusinessRepository
        Try

            If Contract Is Nothing Then
                Contract = New FileContractDTO
            End If
            Select Case Message
                Case "UpdateView"
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    'CreateFilterData()
                    CurrentState = CommonMessage.STATE_NORMAL
                Case "NormalView"
                    ' CreateFilterData()
            End Select
            If dtContracts IsNot Nothing Then
                If IDSelect <> String.Empty Then

                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objContract As New FileContractDTO
        Dim rep As New ProfileRepository
        Dim gID As Decimal
        'Dim stt As OtherListDTO
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_SUBMIT
                    If hidEmployeeID.Value <> String.Empty Then
                        hidEmployeeCode.Value = txtEmployeeCode.Text.Trim()
                        Dim str As String = "OpenEditTransfer();"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                    End If
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                    ClearControl()
                    'Util.ClearDataControl(NORMAL)
                Case CommonMessage.TOOLBARITEM_EDIT
                    'If rgContract.SelectedItems.Count = 0 Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    '    Exit Sub
                    'End If

                    If Contract.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID And Contract.START_DATE.Value.Date < Date.Now Then
                        ShowMessage(Translate("Hợp đồng đã phê duyệt, không thể thực hiện thao tác này"), NotifyType.Warning)
                        Exit Sub
                    End If

                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If hidEmployeeID.Value IsNot Nothing Then
                            objContract.EMPLOYEE_ID = hidEmployeeID.Value
                        End If
                        If cboContract.Text <> "" AndAlso cboContract.Text.Contains("KXD") Then
                            ShowMessage(Translate("Loại phụ lục này không áp dụng cho loại hợp đồng có mã chứa chuỗi KXD"), NotifyType.Error)
                            Exit Sub
                        End If

                        If IsDate(EndDateCT) AndAlso rdExpireDate.SelectedDate IsNot Nothing Then
                            If rdExpireDate.SelectedDate < EndDateCT Then
                                ShowMessage(Translate("Ngày hết hiệu lực lớn hơn ngày hết hạn của hợp đồng"), NotifyType.Error)
                                Exit Sub
                            End If
                        End If

                        'If cboStatus_ID.SelectedValue = 447 Then
                        '    'If txtUpload.Text = "" Then
                        '    '    ShowMessage(Translate("Bạn phải đính kèm tập tin khi phê duyệt"), NotifyType.Warning)
                        '    '    Exit Sub
                        '    'End If
                        'End If

                        objContract.START_DATE = rdStartDate.SelectedDate
                        If rdExpireDate.SelectedDate IsNot Nothing Then
                            objContract.EXPIRE_DATE = rdExpireDate.SelectedDate
                        End If
                        objContract.CONTENT_APPEND = txtAppend_Content.Text
                        objContract.EMPLOYEE_CODE = txtEmployeeCode.Text
                        'objContract.REMARK = txtRemark.Text
                        If cboContract.SelectedValue <> "" Then
                            objContract.CONTRACT_NO = cboContract.Text
                            objContract.ID_CONTRACT = cboContract.SelectedValue
                        End If
                        'objContract.FILENAME = txtUpload.Text.Trim
                        ' objContract.UPLOADFILE = If(Down_File Is Nothing, "", Down_File)
                        'If objContract.UPLOADFILE = "" Then
                        '    objContract.UPLOADFILE = If(txtRemindLink.Text Is Nothing, "", txtRemindLink.Text)
                        'Else
                        '    objContract.UPLOADFILE = If(objContract.UPLOADFILE Is Nothing, "", objContract.UPLOADFILE)
                        'End If
                        If hidContractType_ID.Value <> "" Then
                            objContract.CONTRACTTYPE_ID = hidContractType_ID.Value
                        End If

                        If hidOrgID.Value <> "" Then
                            objContract.SIGN_ORG_ID = hidOrgID.Value
                        End If

                        If cboAppend_TypeID.SelectedValue <> "" Then
                            objContract.APPEND_TYPEID = cboAppend_TypeID.SelectedValue
                        End If

                        If cboStatus_ID.SelectedValue <> "" Then
                            objContract.STATUS_ID = cboStatus_ID.SelectedValue
                        End If
                        If hidSign.Value <> "" Then
                            objContract.SIGN_ID = hidSign.Value
                        End If
                        If hidSign2.Value <> "" Then
                            objContract.SIGN_ID2 = hidSign2.Value
                        End If
                        If hidWorkingID.Value <> "" Then
                            objContract.WORKING_ID = hidWorkingID.Value
                        End If
                        objContract.APPEND_NUMBER = txtContract_NumAppen.Text
                        objContract.SIGN_DATE = rdSignDate.SelectedDate

                        objContract.AUTHORITY = chkAuthor.Checked
                        objContract.AUTHORITY_NUMBER = txtAuthorNumber.Text

                        objContract.SIGNER_NAME = txtSign.Text
                        'objContract.SIGNER_NAME2 = txtSign2.Text
                        objContract.SIGNER_TITLE = txtSign_Title.Text
                        'objContract.SIGNER_TITLE2 = txtSign_Title2.Text
                        objContract.STT = STT
                        'If rdExpireDate.SelectedDate IsNot Nothing Then
                        '    If Not rep.CheckExpireFileContract(rdStartDate.SelectedDate, rdExpireDate.SelectedDate, objContract.ID_CONTRACT) Then
                        '        ShowMessage(Translate("Thời hạn của phụ lục hợp đồng không được lớn hơn thời hạn của hợp đồng"), NotifyType.Warning)
                        '        Exit Sub
                        '    End If
                        'End If


                        'If listContract IsNot Nothing AndAlso listContract.Count > 0 Then
                        '    Dim lstC = listContract.Find(Function(x) x.ID = cboContract.SelectedValue)
                        '    If lstC IsNot Nothing AndAlso rdExpireDate.SelectedDate IsNot Nothing AndAlso lstC.EXPIRE_DATE IsNot Nothing AndAlso rdExpireDate.SelectedDate.Value.Date > lstC.EXPIRE_DATE.Value.Date Then
                        '        ShowMessage(Translate("Ngày kết thúc của Phụ lục hợp đồng không được lớn hơn ngày kết thúc của hợp đồng"), NotifyType.Warning)
                        '        Exit Sub
                        '    End If
                        'End If

                        ''kiem tra xem nhan vien nay da tao phu luc gia han hop dong chua
                        Dim count = psp.COUNT_PLGHHD(hidEmployeeID.Value, cboAppend_TypeID.SelectedValue) 'THanhNT added 27/04/2017 - truyền vào cái loại phụ lục để check thêm

                        If count > 0 Then
                            ShowMessage(Translate("Mẫu phụ lục Gia hạn hợp đồng không được khai báo quá 1 lần "), NotifyType.Warning)
                            Exit Sub
                        End If


                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If Not rep.CheckExistFileContract(hidEmployeeID.Value, rdStartDate.SelectedDate, cboAppend_TypeID.SelectedValue) Then
                                    ShowMessage("Phụ lục này đã tạo vào ngày bắt đầu :" + rdStartDate.SelectedDate, NotifyType.Error)
                                    Exit Sub
                                End If
                                If rep.InsertFileContract(objContract, gID, If(txtContract_NumAppen.Text Is Nothing, "", txtContract_NumAppen.Text)) Then
                                    IDSelect = gID.ToString
                                    Dim str As String = "getRadWindow().close('1');"
                                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    Session("Appendix") = "Success"
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_ContractAppendix&group=Business")

                                    'cboContract_SelectedIndexChanged(Nothing, Nothing)
                                    'NORMAL.Enabled = False
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If

                            Case CommonMessage.STATE_EDIT
                                objContract.ID = Decimal.Parse(IDSelect)
                                'objContract.NUMBER_CONTRACT = txtContractNo.Text
                                'objContract.NUMBER_CONTRACT_INS = txtContractNo1.Text
                                If rep.UpdateFileContract(objContract, gID) Then
                                    IDSelect = objContract.ID.ToString
                                    Dim str As String = "getRadWindow().close('1');"
                                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    Session("Appendix") = "Success"
                                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_ContractAppendix&group=Business")
                                    'Refresh("UpdateView")
                                    'CurrentState = CommonMessage.STATE_EDIT
                                    'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    'cboContract_SelectedIndexChanged(Nothing, Nothing)
                                    'NORMAL.Enabled = False
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.TOOLBARITEM_PRINT
                                If rgContract.SelectedItems.Count = 0 Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                                    Exit Sub
                                End If


                        End Select
                        CurrentState = CommonMessage.STATE_NORMAL
                        Refresh()

                        UpdateControlState()
                        rep.Dispose()
                        'ClearControl()
                        'End If

                    End If
                    ' If Page.IsValid Then
                    '  objContract.ID_CONTRACT = cbContract.SelectedValue

                Case CommonMessage.TOOLBARITEM_DELETE


                    'If Decimal.Parse(objContracts("STATUS_ID").ToString) = 471 Then
                    '    ShowMessage(Translate("Hợp đồng đã phê duyệt, không thể thực hiện thao tác này"), NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    If Contract IsNot Nothing Then
                        If Contract.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                            ShowMessage(Translate("Hợp đồng đã phê duyệt, không thể thực hiện thao tác này"), NotifyType.Warning)
                            Exit Sub
                        End If
                        ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    End If

                    'Dim item As GridDataItem = rgContract.SelectedItems(0)

                Case CommonMessage.TOOLBARITEM_CANCEL
                    'IDSelect = ""
                    'Refresh()
                    CurrentState = CommonMessage.STATE_NORMAL
                    ClearControl()
                    UpdateControlState()
                    txtRemindLink.Text = ""
                    FileOldName = ""
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_ContractAppendix&group=Business")
                    'ScriptManager.RegisterStartupScript(Page, Page.GetType, "Close", "CloseWindow();", True)
                Case CommonMessage.TOOLBARITEM_PRINT

                    'If rgContract.SelectedItems.Count = 0 Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    Dim prp As New ProfileBusinessRepository
                    Dim dtData As DataTable

                    Dim tempPath As String = ConfigurationManager.AppSettings("WordFileFolder")

                    dtData = rep.PrintFileContract(txtEmployeeCode.Text, IDSelect)

                    'ExportWordMailMerge(System.IO.Path.Combine(Server.MapPath(tempPath), "Contract/PLHD.docx"),
                    '           "PhuLucHDLD.doc",
                    '           dtData,
                    '           Response)

                    'lay danh sach bieu mau
                    Dim filePaths() As String = Directory.GetFiles(Server.MapPath("~/Word/Contract/"))

                    Dim listFile As New List(Of String)

                    '
                    If filePaths IsNot Nothing Then
                        For Each File As String In filePaths
                            listFile.Add(Path.GetFileName(File))
                        Next
                    End If

                    ''kiem tra template bieu mau co hop boi bieu mau duoc chon
                    'If listFile.Any(Function(x) x.Trim().ToUpper() = inforForm.CODE.Trim().ToUpper() + ".DOC" OrElse x.Trim().ToUpper() = inforForm.CODE.Trim().ToUpper() + ".DOCX") Then
                    '    ExportWordMailMerge(System.IO.Path.Combine(Server.MapPath(tempPath), "Contract/" + inforForm.CODE + ".doc"),
                    '          inforForm.NAME_VN + ".doc",
                    '          dtData,
                    '          Response)
                    'Else
                    '    ShowMessage(Translate("Không tìm thấy biểu mẫu phù hợp"), NotifyType.Warning)
                    '    Exit Sub
                    'End If
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(sender As Object, e As Common.MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        'Dim rep As New ProfileBusinessRepository
        If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
            CurrentState = CommonMessage.STATE_DELETE
            UpdateControlState()
            ClearControl()
            CurrentState = CommonMessage.STATE_EDIT
        End If
    End Sub
    Private Sub btnEmployee_Click(sender As Object, e As System.EventArgs) Handles btnEmployee.Click
        Try
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Private Sub btnSign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSign.Click
        Try
            isLoadPopup = 2
            UpdateControlState()
            ctrlFindSigner.MustHaveContract = True
            ctrlFindSigner.LoadAllOrganization = False
            ctrlFindSigner.IsOnlyWorkingWithoutTer = True
            ctrlFindSigner.Show()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    'Private Sub btnSign2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSign2.Click
    '    Try
    '        isLoadPopup = 4
    '        UpdateControlState()
    '        ctrlFindSigner2.MustHaveContract = True
    '        ctrlFindSigner2.LoadAllOrganization = False
    '        ctrlFindSigner2.IsOnlyWorkingWithoutTer = True
    '        ctrlFindSigner2.Show()
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    Private Sub ctrlFindSignPopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindSigner.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        'Dim rep As New ProfileRepository
        'Dim repOrg As New ProfileRepository
        'Dim title As TitleDTO
        Try
            lstCommonEmployee = CType(ctrlFindSigner.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)
                hidSign.Value = item.ID
                txtSign.Text = item.FULLNAME_VN
                txtSign_Title.Text = item.TITLE_NAME
            End If
        Catch ex As Exception

        End Try
    End Sub
    'Private Sub ctrlFindSignPopup2_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindSigner2.EmployeeSelected
    '    Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
    '    'Dim rep As New ProfileRepository
    '    'Dim repOrg As New ProfileRepository
    '    'Dim title As TitleDTO
    '    Try
    '        lstCommonEmployee = CType(ctrlFindSigner2.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
    '        If lstCommonEmployee.Count <> 0 Then
    '            Dim item = lstCommonEmployee(0)
    '            hidSign2.Value = item.ID
    '            txtSign2.Text = item.FULLNAME_VN
    '            'txtSign_Title2.Text = item.TITLE_NAME
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub btnSalary_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalary.Click
        Try
            If hidEmployeeID.Value = "" Then
                ShowMessage(Translate("Bạn phải chọn nhân viên."), NotifyType.Warning)
                Exit Sub
            End If
            isLoadPopup = 3
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlFind_CancelClick(ByVal sender As Object, ByVal e As System.EventArgs) _
      Handles ctrlFindSigner.CancelClicked, ctrlFindSigner2.CancelClicked,
      ctrlFindEmployeePopup.CancelClicked,
      ctrlFindSalaryPopup.CancelClicked
        isLoadPopup = 0
    End Sub

    Private Sub ctrlFindSalaryPopup_SalarySelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindSalaryPopup.SalarySelected
        Dim lstCommon As New List(Of WorkingDTO)
        Dim rep As New ProfileBusinessRepository
        Try
            lstCommon = CType(ctrlFindSalaryPopup.SelectedSalary, List(Of WorkingDTO))
            If lstCommon.Count <> 0 Then
                Dim item = lstCommon(0)
                hidWorkingID.Value = item.ID
                Dim working = rep.GetWorkingByID(New WorkingDTO() With {.ID = item.ID})
                'Working_ID.Text = working.ID
                Working_ID.Text = If(working.DECISION_NO <> "", working.DECISION_NO, working.EFFECT_DATE.Value.Date)
                rntxtBasicSal.Value = working.SAL_BASIC
                Salary_Total.Value = working.SAL_TOTAL
                SalaryInsurance.Value = working.SAL_INS
                PercentSalary.Value = working.PERCENT_SALARY
                'rnOtherSalary1.Value = working.OTHERSALARY1
                'rnOtherSalary2.Value = working.OTHERSALARY2
                'rnOtherSalary3.Value = working.OTHERSALARY3
                Allowance_Total.Value = working.ALLOWANCE_TOTAL
                SetValueComboBox(cboSalTYPE, working.SAL_TYPE_ID, working.SAL_TYPE_NAME)

                SetValueComboBox(cbSalaryGroup, working.SAL_GROUP_ID, working.SAL_GROUP_NAME)
                SetValueComboBox(cbSalaryLevel, working.SAL_LEVEL_ID, working.SAL_LEVEL_NAME)
                SetValueComboBox(cbSalaryRank, working.SAL_RANK_ID, working.SAL_RANK_NAME)
                'SetValueComboBox(cboTaxTable, working.TAX_TABLE_ID, working.TAX_TABLE_Name)
                'rgAllow.DataSource = working.lstAllowance
                'rgAllow.Rebind()
            End If
            rep.Dispose()
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub ctrlFindEmployeePopup_EmployeeSelected(sender As Object, e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        Dim rep As New ProfileRepository
        Dim title As TitleDTO
        Try
            IDSelect = Nothing
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Dim item = lstCommonEmployee(0)

                ''Load danh sách hợp đồng của nhân viên

                listContract = rep.GetContractList(item.ID)

                If listContract Is Nothing Or listContract.Count = 0 Then
                    ShowMessage("Bạn chưa tạo hợp đồng lao động cho nhân viên " & item.FULLNAME_VN, NotifyType.Warning)
                    Exit Sub
                End If

                hidOrgID.Value = item.ORG_ID.ToString
                hidEmployeeID.Value = item.ID.ToString
                txtEmployeeCode.Text = item.EMPLOYEE_CODE
                txtEmployeeName.Text = item.FULLNAME_VN

                hidOrgCode.Value = rep.GetOrgList(hidOrgID.Value)

                'get thong tin chuc danh 
                title = rep.GetTitileBaseOnEmp(item.TITLE_ID)
                If title IsNot Nothing Then
                    txtTitle.Text = title.NAME_VN
                End If

                If Not rdStartDate.SelectedDate Is Nothing Then
                    FillData(Convert.ToDecimal(hidEmployeeID.Value), True)
                End If

                FillDropDownList(cboContract, listContract, "CONTRACT_NO", "ID", Common.Common.SystemLanguage, True)
                cboContract.SelectedIndex = 0

                txtOrg.Text = item.ORG_NAME
                hidOrgID.Value = item.ORG_ID

                GetWorkingMax()
            End If
            isLoadPopup = 0
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub cval_EffectDate_ExpireDate_ServerValidate(ByVal source As Object, ByVal args As ServerValidateEventArgs) Handles cval_EffectDate_ExpireDate.ServerValidate
        Try
            If rdExpireDate.SelectedDate IsNot Nothing Then
                If rdExpireDate.SelectedDate < rdStartDate.SelectedDate Then
                    args.IsValid = False
                    Exit Sub
                Else
                    args.IsValid = True
                End If
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub rgvDataImport_NeedDataSource(ByVal sender As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgContract.NeedDataSource
        Dim lstContract As New List(Of ContractDTO)
        rgContract.DataSource = lstContract
    End Sub
    Private Sub rdStartDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdStartDate.SelectedDateChanged
        Dim store As New ProfileStoreProcedure
        Dim PERIOD As String = ""
        If hidEmployeeID.Value <> "" Then
            FillData(Convert.ToDecimal(hidEmployeeID.Value), True)
            Dim rep As New ProfileRepository
            If rdStartDate.SelectedDate IsNot Nothing And cboAppend_TypeID.SelectedValue <> 0 Then
                Dim objContractType = rep.GetContractTypeID(cboAppend_TypeID.SelectedValue)
                If objContractType IsNot Nothing Then
                    If objContractType.PERIOD IsNot Nothing And objContractType.PERIOD <> 0 Then
                        rdExpireDate.SelectedDate = rdStartDate.SelectedDate.Value.AddMonths(objContractType.PERIOD)
                    End If
                End If
            End If

            If cboContract.SelectedValue <> "" Then
                Dim inforContract = listContract.Find(Function(x) x.ID = cboContract.SelectedValue)
                If inforContract IsNot Nothing Then
                    radDate.SelectedDate = inforContract.START_DATE
                    PERIOD = rep.GetContractTypeCT(inforContract.CONTRACTTYPE_ID)
                    EndDateCT = inforContract.EXPIRE_DATE
                End If
            End If

            If PERIOD <> "" Then
                rdExpireDate.SelectedDate = rdStartDate.SelectedDate.Value.AddMonths(PERIOD)
            End If

            Dim stt = store.GetLastAppenNumber(hidEmployeeID.Value)
            If rdStartDate.SelectedDate IsNot Nothing Then
                txtContract_NumAppen.Text = String.Format("{0}/{1}/AV/{2}/PL{3}", txtEmployeeCode.Text, rdStartDate.SelectedDate.Value.Year, hidOrgCode.Value, stt + 1)
            Else
                txtContract_NumAppen.Text = Nothing
            End If

            rep.Dispose()
        End If
        'rdSignDate.SelectedDate = rdStartDate.SelectedDate
    End Sub
    Private Sub cboContract_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboContract.SelectedIndexChanged
        Dim contrItem As New FileContractDTO
        Dim rep As New ProfileRepository
        Dim store As New ProfileStoreProcedure
        Dim emp_id As Decimal
        Dim idContractType As Decimal
        Dim PERIOD As String = ""
        If hidEmployeeID.Value IsNot Nothing Then
            emp_id = hidEmployeeID.Value
            contrItem.EMPLOYEE_ID = hidEmployeeID.Value
        End If
        If cboContract.SelectedValue <> "" Then
            'lấy thong tin hop dong sau khi chọn hợp đồng
            Dim inforContract = listContract.Find(Function(x) x.ID = cboContract.SelectedValue)

            'Hiển thị số phụ lục hợp đồng sau khi chọn hợp đồng

            'If CurrentState = CommonMessage.STATE_NEW Then
            '    If inforContract IsNot Nothing Then
            '        Dim outNum As Integer = rep.GET_NEXT_APPENDIX_ORDER(0, cboContract.SelectedValue, contrItem.EMPLOYEE_ID) 'lay so thu tu tiep theo
            '        Dim order = String.Format("{0}", Format(outNum, "00"))
            '        txtContract_NumAppen.Text = inforContract.CONTRACT_NO + "-" + order
            '        'lay ngay het han hop dong gán vao ngày het hạn phụ lục
            '        'If inforContract.EXPIRE_DATE IsNot Nothing Then
            '        '    rdExpireDate.SelectedDate = inforContract.EXPIRE_DATE
            '        'Else
            '        '    rdExpireDate.SelectedDate = Nothing
            '        'End If
            '        STT = outNum
            '        radDate.SelectedDate = inforContract.START_DATE
            '    End If
            'End If
            'If CurrentState = CommonMessage.STATE_EDIT Then
            '    If inforContract IsNot Nothing Then
            '        Dim outNum As Integer = rep.GET_NEXT_APPENDIX_ORDER(IDSelect, cboContract.SelectedValue, contrItem.EMPLOYEE_ID) 'lay so thu tu tiep theo
            '        Dim order = String.Format("{0}", Format(outNum, "00"))
            '        txtContract_NumAppen.Text = inforContract.CONTRACT_NO + "-" + order
            '        STT = outNum
            '        radDate.SelectedDate = inforContract.START_DATE
            '    End If
            'End If
            'hiển thị thông tin PLHĐ đối với trường hợp sửa thông tin
            contrItem.ID_CONTRACT = cboContract.SelectedValue

            contrItem.CONTRACT_NO = cboContract.Text

            hidContract_ID.Value = cboContract.SelectedValue
            radDate.SelectedDate = inforContract.START_DATE

            'If inforContract IsNot Nothing AndAlso inforContract.UNIT_SIGN IsNot Nothing Then
            '    cboLocation.SelectedValue = inforContract.UNIT_SIGN
            'End If

            idContractType = listContract.Find(Function(x) x.ID = cboContract.SelectedValue).CONTRACTTYPE_ID

            Dim inforContractType = rep.GetContractTypeID(idContractType)

            If inforContractType IsNot Nothing Then
                'lay thong tin loại hợp đồng 
                hidContractType_ID.Value = inforContractType.ID
                PERIOD = rep.GetContractTypeCT(inforContract.CONTRACTTYPE_ID)
                EndDateCT = inforContract.EXPIRE_DATE
                'txtContractType.Text = inforContractType.NAME
            End If

            If PERIOD <> "" Then
                rdExpireDate.SelectedDate = rdStartDate.SelectedDate.Value.AddMonths(PERIOD)
            End If

            Dim stt = store.GetLastAppenNumber(hidEmployeeID.Value)
            If rdStartDate.SelectedDate IsNot Nothing Then
                txtContract_NumAppen.Text = String.Format("{0}/{1}/AV/{2}/PL{3}", txtEmployeeCode.Text, rdStartDate.SelectedDate.Value.Year, hidOrgCode.Value, stt + 1)
            Else
                txtContract_NumAppen.Text = Nothing
            End If

        End If

        Dim lstContract As New List(Of FileContractDTO)
        lstContract = rep.GetContractAppendix(contrItem)
        rgContract.DataSource = lstContract
        rgContract.Rebind()
        rep.Dispose()
        'Dim ds2 As DataSet = asp.ExecuteToDataSet("PKG_HU_CONTRACT.GET_LIST_HU_FILECONTRACT", New List(Of Object)(New Object() {cboContract.SelectedValue, emp_id}))
        'If ds2 IsNot Nothing Then
        '    dtContracts = ds2.Tables(0)
        '    rgContract.DataSource = dtContracts
        '    rgContract.Rebind()
        'End If
    End Sub
    Private Sub rgContract_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rgContract.SelectedIndexChanged
        Dim rep As New ProfileRepository

        If rgContract.SelectedItems.Count > 0 Then
            'ClearControl()
            Dim slItem As GridDataItem
            slItem = rgContract.SelectedItems(0)
            hidID.Value = slItem("ID").Text
            IDSelect = hidID.Value
            If hidID.Value IsNot Nothing Or hidID.Value <> "" Then

                Contract = rep.GetFileConTractID(hidID.Value)
                'If cboContractType.DataSource = Nothing Then
                '    GetDataCombo()
                'End If
                If Contract IsNot Nothing Then
                    If Contract.APPEND_TYPEID IsNot Nothing Then
                        cboAppend_TypeID.SelectedValue = Contract.APPEND_TYPEID
                    End If
                    If Contract.STATUS_ID IsNot Nothing Then
                        cboStatus_ID.SelectedValue = Contract.STATUS_ID
                    End If
                    txtContract_NumAppen.Text = Contract.APPEND_NUMBER
                    txtAppend_Content.Text = Contract.CONTENT_APPEND
                    rdStartDate.SelectedDate = Contract.START_DATE
                    If Contract.EXPIRE_DATE IsNot Nothing Then
                        rdExpireDate.SelectedDate = Contract.EXPIRE_DATE
                    End If

                    'txtRemark.Text = Contract.REMARK

                    If Contract.SIGN_ID IsNot Nothing Then
                        hidSign.Value = Contract.SIGN_ID
                    End If
                    If Contract.SIGN_ID2 IsNot Nothing Then
                        hidSign2.Value = Contract.SIGN_ID2
                    End If

                    txtSign.Text = Contract.SIGNER_NAME
                    'txtSign2.Text = Contract.SIGNER_NAME2
                    txtSign_Title.Text = Contract.SIGNER_TITLE
                    'txtSign_Title2.Text = Contract.SIGNER_TITLE2

                    rdSignDate.SelectedDate = Contract.SIGN_DATE

                    If Contract.WORKING_ID IsNot Nothing Then
                        hidWorkingID.Value = Contract.WORKING_ID
                    End If

                    If Contract.AUTHORITY IsNot Nothing Then
                        chkAuthor.Checked = Contract.AUTHORITY
                    End If

                    txtAuthorNumber.Text = Contract.AUTHORITY_NUMBER

                    'Working_ID.Text = Contract.ID
                    'Working_ID.Text = If(Contract.DECISION_NO <> "", Contract.DECISION_NO, Contract.EFFECT_DATE1.Value.Date)
                    rep.Dispose()
                    'lay thong tin luong
                    GetSalary(Contract)
                End If
            End If
        End If

    End Sub

    Private Sub cboAppend_TypeID_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboAppend_TypeID.SelectedIndexChanged
        Dim rep As New ProfileRepository
        If rdStartDate.SelectedDate IsNot Nothing And cboAppend_TypeID.SelectedValue <> 0 Then
            Dim objContractType = rep.GetContractTypeID(cboAppend_TypeID.SelectedValue)
            If objContractType IsNot Nothing Then
                'khóa sự kiên này lại vì dl period trong hu_contract_type k có
                'rdExpireDate.SelectedDate = rdStartDate.SelectedDate.Value.AddMonths(objContractType.PERIOD)
            End If
        End If
        rep.Dispose()
    End Sub
    'Private Sub btnUploadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadFile.Click
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

    '    Try
    '        ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
    '        ctrlUpload1.Show()

    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    'Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

    '    Try
    '        txtUploadFile.Text = ""
    '        Dim listExtension = New List(Of String)
    '        listExtension.Add(".xls")
    '        listExtension.Add(".xlsx")
    '        listExtension.Add(".txt")
    '        listExtension.Add(".ctr")
    '        listExtension.Add(".doc")
    '        listExtension.Add(".docx")
    '        listExtension.Add(".xml")
    '        listExtension.Add(".png")
    '        listExtension.Add(".jpg")
    '        listExtension.Add(".bitmap")
    '        listExtension.Add(".jpeg")
    '        listExtension.Add(".gif")
    '        listExtension.Add(".pdf")
    '        listExtension.Add(".rar")
    '        listExtension.Add(".zip")
    '        listExtension.Add(".ppt")
    '        listExtension.Add(".pptx")
    '        Dim fileName As String

    '        Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/FileContractInfo/")
    '        If ctrlUpload1.UploadedFiles.Count >= 1 Then
    '            For i = 0 To ctrlUpload1.UploadedFiles.Count - 1
    '                Dim file As UploadedFile = ctrlUpload1.UploadedFiles(i)
    '                Dim str_Filename = Guid.NewGuid.ToString() + "\"
    '                If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
    '                    System.IO.Directory.CreateDirectory(strPath + str_Filename)
    '                    strPath = strPath + str_Filename
    '                    fileName = System.IO.Path.Combine(strPath, file.FileName)
    '                    file.SaveAs(fileName, True)
    '                    txtUploadFile.Text = file.FileName
    '                    Down_File = str_Filename
    '                Else
    '                    ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"), NotifyType.Warning)
    '                    Exit Sub
    '                End If
    '            Next
    '            loadDatasource(txtUploadFile.Text)
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    'Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim bCheck As Boolean = False
    '    Try

    '        If txtUpload.Text <> "" Then
    '            Dim strPath_Down As String
    '            If FileOldName = txtUpload.Text.Trim Or FileOldName Is Nothing Then
    '                If txtRemindLink.Text IsNot Nothing Then
    '                    If txtRemindLink.Text <> "" Then
    '                        strPath_Down = Server.MapPath("~/ReportTemplates/Profile/FileContractInfo/" + txtRemindLink.Text)
    '                        'bCheck = True
    '                        ZipFiles(strPath_Down)
    '                    End If
    '                End If
    '            Else
    '                If Down_File <> "" Then
    '                    strPath_Down = Server.MapPath("~/ReportTemplates/Profile/FileContractInfo/" + Down_File)
    '                    'bCheck = True
    '                    ZipFiles(strPath_Down)
    '                End If
    '            End If
    '            'If bCheck Then
    '            '    ZipFiles(strPath_Down)
    '            'End If
    '        End If

    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    'Private Sub ZipFiles(ByVal path As String)
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Dim startTime As DateTime = DateTime.UtcNow

    '    Try
    '        Dim crc As New Crc32()

    '        Dim fileNameZip As String = txtUploadFile.Text.Trim


    '        Dim file As System.IO.FileInfo = New System.IO.FileInfo(path & fileNameZip)
    '        Response.Clear()
    '        Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
    '        Response.AddHeader("Content-Length", file.Length.ToString())
    '        'Response.ContentType = "application/octet-stream"
    '        Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
    '        Response.WriteFile(file.FullName)
    '        Response.End()

    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        HttpContext.Current.Trace.Warn(ex.ToString())
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
    'Private Sub loadDatasource(ByVal strUpload As String)
    '    Dim startTime As DateTime = DateTime.UtcNow
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        'Dim data As New DataTable
    '        'data.Columns.Add("FileName")
    '        'Dim row As DataRow
    '        'Dim str() As String
    '        If strUpload <> "" Then
    '            txtUploadFile.Text = strUpload
    '            FileOldName = txtUpload.Text
    '            txtUpload.Text = strUpload

    '            'Str = strUpload.Split(";")

    '            'For Each s As String In str
    '            '    If s <> "" Then
    '            '        row = data.NewRow
    '            '        row("FileName") = s
    '            '        data.Rows.Add(row)
    '            '    End If
    '            'Next

    '            'txtUpload.DataSource = data
    '            'txtUpload.DataTextField = "FileName"
    '            'txtUpload.DataValueField = "FileName"
    '            'txtUpload.DataBind()
    '        Else
    '            'txtUpload.DataSource = Nothing
    '            'txtUpload.ClearSelection()
    '            'txtUpload.ClearCheckedItems()
    '            'txtUpload.Items.Clear()
    '            strUpload = String.Empty
    '        End If

    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub
#End Region

#Region "Custom"
    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NORMAL
                    UpdateControl(False)
                    ' EnabledGrid(rgContract, True)
                    cboContract.Enabled = True
                    ReadOnlyRadComBo(cboContract, False)

                Case CommonMessage.STATE_NEW
                    UpdateControl(True)
                    ' EnabledGrid(rgContract, False)
                    btnEmployee.Enabled = True
                    ' cbContract.Enabled = True

                Case CommonMessage.STATE_EDIT
                    UpdateControl(True)
                    'EnabledGrid(rgContract, False)
                    btnEmployee.Enabled = False
                    ' cbContract.Enabled = False
                Case CommonMessage.STATE_DELETE
                    If rep.DeleteFileContract(Contract) Then
                        Contract = Nothing
                        IDSelect = Nothing
                        Refresh("UpdateView")
                        rgContract.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
            End Select
            Select Case isLoadPopup
                Case 1
                    If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        'If Contract.ORG_ID <> 0 Then
                        '    ctrlFindEmployeePopup.CurrentValue = Contract.ORG_ID.ToString
                        'End If
                        ctrlFindEmployeePopup.MustHaveContract = True
                        FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                    End If
                Case 2
                    If Not FindSigner.Controls.Contains(ctrlFindSigner) Then
                        ctrlFindSigner = Me.Register("ctrlFindSigner", "Common", "ctrlFindEmployeePopup")
                        FindSigner.Controls.Add(ctrlFindSigner)
                        ctrlFindSigner.MultiSelect = False
                        ctrlFindSigner.MustHaveContract = False
                        ctrlFindSigner.LoadAllOrganization = True
                    End If
                Case 4
                    If Not FindSigner2.Controls.Contains(ctrlFindSigner2) Then
                        ctrlFindSigner2 = Me.Register("ctrlFindSigner2", "Common", "ctrlFindEmployeePopup")
                        FindSigner2.Controls.Add(ctrlFindSigner2)
                        ctrlFindSigner2.MultiSelect = False
                        ctrlFindSigner2.MustHaveContract = False
                        ctrlFindSigner2.LoadAllOrganization = True
                    End If
                Case 3
                    If Not FindSalary.Controls.Contains(ctrlFindSalaryPopup) Then
                        ctrlFindSalaryPopup = Me.Register("ctrlFindSalaryPopup", "Profile", "ctrlFindSalaryPopup", "Shared")
                        ctrlFindSalaryPopup.MultiSelect = False
                        If hidEmployeeID.Value <> "" Then
                            ctrlFindSalaryPopup.EmployeeID = Decimal.Parse(hidEmployeeID.Value)
                            Session("EmployeeID") = Decimal.Parse(hidEmployeeID.Value)
                        End If

                        FindSalary.Controls.Add(ctrlFindSalaryPopup)
                        ctrlFindSalaryPopup.Show()
                    End If
            End Select
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
        'ChangeToolbarState()
    End Sub

    Protected Sub UpdateControl(ByVal bCheck As Boolean)
        Select Case CurrentState
            Case CommonMessage.STATE_NORMAL, CommonMessage.STATE_NEW
                btnEmployee.Enabled = True
            Case CommonMessage.STATE_EDIT
                btnEmployee.Enabled = True
        End Select
        txtEmployeeCode.Enabled = False
        txtEmployeeName.Enabled = False
        txtContract_NumAppen.Enabled = False
        txtOrg.Enabled = False
        txtTitle.Enabled = False
        'txtContract_NumAppen.Enabled = bCheck
        'txtContract_NumAppen.Enabled = True
        txtAppend_Content.Enabled = bCheck
        '  ReadOnlyRadComBo(cboContractType, Not bCheck)
        EnableRadDatePicker(rdStartDate, bCheck)
        EnableRadDatePicker(rdExpireDate, bCheck)
        EnableRadDatePicker(rdSignDate, bCheck)
        ReadOnlyRadComBo(cboContract, Not bCheck)
        ReadOnlyRadComBo(cboAppend_TypeID, Not bCheck)
        ReadOnlyRadComBo(cboStatus_ID, Not bCheck)
        btnSign.ReadOnly = Not bCheck
        'btnSign2.ReadOnly = Not bCheck
        txtSign_Title.Enabled = bCheck
        'txtSign_Title2.Enabled = bCheck
        'txtRemark.Enabled = bCheck
        rntxtBasicSal.Enabled = bCheck
    End Sub

    Protected Sub ClearControl()
        txtEmployeeCode.Text = String.Empty
        txtEmployeeName.Text = String.Empty
        txtOrg.Text = String.Empty
        txtTitle.Text = String.Empty
        txtContract_NumAppen.Text = String.Empty
        ' cboContractType.SelectedIndex = 0
        cboAppend_TypeID.SelectedIndex = 0
        cboContract.SelectedIndex = 0
        'txtRemark.Text = String.Empty
        txtAppend_Content.Text = String.Empty
        rdExpireDate.SelectedDate = Nothing
        rdStartDate.SelectedDate = Nothing
        rdSignDate.SelectedDate = Nothing
        radDate.SelectedDate = Nothing
        cboContract.SelectedIndex = 0
        cboStatus_ID.SelectedIndex = 0
        txtSign_Title.Text = String.Empty
        'txtSign_Title2.Text = String.Empty
        'txtRemark.Text = String.Empty
        rgContract.MasterTableView.ClearSelectedItems()
        rgContract.DataSource = New List(Of FileContractDTO)
        rgContract.Rebind()
        txtSign.Text = String.Empty
        'txtSign2.Text = String.Empty
        rntxtBasicSal.ClearValue()
        'rgAllow.MasterTableView.ClearSelectedItems()
        'rgAllow.DataSource = New List(Of WorkingAllowanceDTO)
        'rgAllow.DataBind()
    End Sub
    ''' <summary>
    ''' lay thông tin lương
    ''' </summary>
    ''' <param name="wkm"></param>
    ''' <remarks></remarks>
    Private Sub GetSalary(ByVal fileContract As FileContractDTO)
        If fileContract.WORKING_ID IsNot Nothing Then
            hidWorkingID.Value = fileContract.WORKING_ID
            Dim wkm As WorkingDTO = rep.GetWorkingByID(New WorkingDTO With {.ID = fileContract.WORKING_ID})
            If wkm IsNot Nothing Then
                ' Working_ID.Text = wkm.ID
                Working_ID.Text = If(wkm.DECISION_NO <> "", wkm.DECISION_NO, wkm.EFFECT_DATE.Value.Date)
                rntxtBasicSal.Value = wkm.SAL_BASIC
                'If wkm.lstAllowance IsNot Nothing Then
                '    rgAllow.DataSource = wkm.lstAllowance
                '    rgAllow.DataBind()
                'End If
                PercentSalary.Value = wkm.PERCENT_SALARY
                'rnOtherSalary1.Value = wkm.OTHERSALARY1
                'rnOtherSalary2.Value = wkm.OTHERSALARY2
                'rnOtherSalary3.Value = wkm.OTHERSALARY3
                Salary_Total.Value = wkm.SAL_TOTAL
                SetValueComboBox(cboSalTYPE, wkm.SAL_TYPE_ID, wkm.SAL_TYPE_NAME)

                SetValueComboBox(cbSalaryGroup, wkm.SAL_GROUP_ID, wkm.SAL_GROUP_NAME)
                SetValueComboBox(cbSalaryLevel, wkm.SAL_LEVEL_ID, wkm.SAL_LEVEL_NAME)
                SetValueComboBox(cbSalaryRank, wkm.SAL_RANK_ID, wkm.SAL_RANK_NAME)

                'SetValueComboBox(cboTaxTable, wkm.TAX_TABLE_ID, wkm.TAX_TABLE_Name)
                SalaryInsurance.Value = wkm.SAL_INS
                Allowance_Total.Value = wkm.ALLOWANCE_TOTAL
            End If
        End If

    End Sub
    Private Sub GetDataCombo()
        Dim rep As New ProfileRepository
        Try
            Dim appendData As New List(Of ContractTypeDTO)
            appendData = rep.GetListContractType("PLHD")

            appendData.Insert(0, New ContractTypeDTO With {.NAME = "", .ID = 0})

            If ListComboData Is Nothing Then
                ListComboData = New ComboBoxDataDTO
                ListComboData.GET_CONTRACTTYPE = True
                ListComboData.GET_DECISION_STATUS = True

                'ListComboData.GET_TITLE = True 'ThanhNT delete: vì không load theo côm bô bốc nữa.
                rep.GetComboList(ListComboData)
            End If
            '  FillDropDownList(cboContractType, ListComboData.LIST_CONTRACTTYPE, "NAME", "ID", Common.Common.SystemLanguage, True)
            FillDropDownList(cboStatus_ID, ListComboData.LIST_DECISION_STATUS, "NAME_VN", "ID", Common.Common.SystemLanguage, False)
            cboStatus_ID.SelectedIndex = 0
            FillDropDownList(cboAppend_TypeID, appendData, "NAME", "ID", Common.Common.SystemLanguage, False)
            If appendData.Count > 1 Then
                cboAppend_TypeID.SelectedValue = appendData(1).ID
            End If
            'Dim dtLocation As New List(Of LocationDTO)

            'dtLocation = rep.GetLocation("A")

            'If dtLocation IsNot Nothing Then
            '    FillDropDownList(cboLocation, dtLocation, "LOCATION_VN_NAME", "ID", Common.Common.SystemLanguage, False)
            '    If cboLocation.Items.Count > 0 Then
            '        cboLocation.SelectedIndex = 0
            '    End If
            'End If
            rep.Dispose()
            'get combobox bieu mau
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetWorkingMax()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim working As WorkingDTO
            Using rep As New ProfileBusinessRepository
                working = rep.GetLastWorkingSalary(New WorkingDTO With {.EMPLOYEE_ID = hidEmployeeID.Value})
            End Using
            If working IsNot Nothing Then
                hidWorkingID.Value = working.ID
                SetValueComboBox(cboSalTYPE, working.SAL_TYPE_ID, working.SAL_TYPE_NAME)

                SetValueComboBox(cbSalaryGroup, working.SAL_GROUP_ID, working.SAL_GROUP_NAME)
                SetValueComboBox(cbSalaryLevel, working.SAL_LEVEL_ID, working.SAL_LEVEL_NAME)
                SetValueComboBox(cbSalaryRank, working.SAL_RANK_ID, working.SAL_RANK_NAME)
                'SetValueComboBox(cboTaxTable, working.TAX_TABLE_ID, working.TAX_TABLE_Name)
                rntxtBasicSal.Value = working.SAL_BASIC
                SalaryInsurance.Value = working.SAL_INS
                Allowance_Total.Value = working.ALLOWANCE_TOTAL
                Salary_Total.Value = working.SAL_TOTAL
                ' Working_ID.Text = working.ID
                PercentSalary.Value = working.PERCENT_SALARY
                'rnOtherSalary1.Value = working.OTHERSALARY1
                'rnOtherSalary2.Value = working.OTHERSALARY2
                'rnOtherSalary3.Value = working.OTHERSALARY3
                Working_ID.Text = If(working.DECISION_NO <> "", working.DECISION_NO, working.EFFECT_DATE.Value.Date)
                'If rdStartDate.SelectedDate Is Nothing Then
                '    rdStartDate.SelectedDate = working.EFFECT_DATE
                'End If
                'rgAllow.DataSource = working.lstAllowance
                'rgAllow.Rebind()
            Else
                ClearControlValue(cboSalTYPE, rntxtBasicSal, SalaryInsurance, Allowance_Total, Salary_Total, rdSignDate, cbSalaryGroup, cbSalaryLevel, cbSalaryRank)
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "ThanhNT added"

    Private Sub GetParams()
        Try

            If Not Page.IsPostBack Then
                'ThanhNT edited 30062016 đáp ứng cho nhu cầu process của U

                If Request.Params("Check") IsNot Nothing Then
                    checkStatus = Request.Params("Check")
                End If

                'If CurrentState Is Nothing Then
                If Request.Params("add") IsNot Nothing AndAlso Request.Params("add").ToString = "1" Then
                    If Request.Params("EmpID") IsNot Nothing Then
                        CurrentState = CommonMessage.STATE_NEW
                        'btnEmployee.ReadOnly = True
                        FillData(Request.Params("EmpID"))
                    Else
                        CurrentState = CommonMessage.STATE_NEW
                    End If
                    '
                    Refresh("NormalView")
                ElseIf Request.Params("IDSelect") IsNot Nothing Then
                    CurrentState = CommonMessage.STATE_EDIT
                    ' btnEmployee.ReadOnly = True
                    hidID.Value = Request.Params("IDSelect")
                    FillDataDoubleClick(Request.Params("EmpID"))
                Else 'ThanhNT added 29062016 - Trường hợp theo quy trình thì sẽ load dữ liệu lên để user thao tác
                    If Request.Params("empID") IsNot Nothing Then
                        btnEmployee.ReadOnly = True
                        FillData(Request.Params("empID"))
                        CurrentState = CommonMessage.STATE_NEW
                    End If

                    Refresh("NormalView")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' load khi double click từ man hinh quan lý phụ lục
    ''' </summary>
    ''' <param name="empID"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub FillDataDoubleClick(ByVal empID As Decimal)
        Try
            Dim rep As New ProfileRepository
            'Dim repOrg As New ProfileRepository
            'Dim title As TitleDTO
            'Dim repEmp As New ProfileBusinessRepository
            'Dim obj = repEmp.GetEmployeCurrentByID(New WorkingDTO With {.EMPLOYEE_ID = empID})
            'hidEmployeeID.Value = obj.EMPLOYEE_ID
            'txtEmployeeCode.Text = obj.EMPLOYEE_CODE
            'txtEmployeeName.Text = obj.EMPLOYEE_NAME
            'txtOrg.Text = obj.ORG_NAME
            'hidOrgID.Value = obj.ORG_ID
            'txtRemark.Text = obj.REMARK

            'get thong tin chuc danh 
            'title = rep.GetTitileBaseOnEmp(obj.TITLE_ID)
            'If title IsNot Nothing Then
            '    txtTitle.Text = title.NAME_VN
            'End If

            listContract = rep.GetContractList(empID)
            'listContract = rep.GetListContractBaseOnEmp(empID)
            If listContract IsNot Nothing And listContract.Count <> 0 Then
                FillDropDownList(cboContract, listContract, "CONTRACT_NO", "ID", Common.Common.SystemLanguage, True)
                cboContract.SelectedIndex = 0
            End If

            ''Tao moi ma hop dong
            'If hidOrgID.Value <> String.Empty Then
            '    Dim dt = DateTime.Now
            '    If rdStartDate.SelectedDate IsNot Nothing Then
            '        dt = rdStartDate.SelectedDate
            '    End If
            'End If

            Dim lstContractAppen As New List(Of FileContractDTO)
            Dim contractAppen = rep.GetFileConTractID(hidID.Value)
            If contractAppen IsNot Nothing Then
                'txtUploadFile.Text = contractAppen.FILENAME
                txtRemindLink.Text = If(contractAppen.UPLOADFILE Is Nothing, "", contractAppen.UPLOADFILE)
                'loadDatasource(txtUploadFile.Text)
                'FileOldName = If(FileOldName = "", txtUpload.Text, FileOldName)
                hidEmployeeID.Value = contractAppen.EMPLOYEE_ID
                txtEmployeeCode.Text = contractAppen.EMPLOYEE_CODE
                txtEmployeeName.Text = contractAppen.EMPLOYEE_NAME
                txtOrg.Text = contractAppen.ORG_NAME
                'hidOrgID.Value = contractAppen.ORG_ID
                ' txtRemark.Text = contractAppen.REMARK
                txtTitle.Text = contractAppen.TITLE_NAME

                Contract = contractAppen
                If contractAppen.ID_CONTRACT IsNot Nothing Then
                    cboContract.SelectedValue = contractAppen.ID_CONTRACT

                    If listContract IsNot Nothing Then
                        If listContract.Any(Function(x) x.ID = Contract.ID_CONTRACT) Then
                            'txtContractType.Text = listContract.Find(Function(x) x.ID = cboContract.SelectedValue).CONTRACTTYPE_NAME
                            hidContractType_ID.Value = listContract.Find(Function(x) x.ID = cboContract.SelectedValue).CONTRACTTYPE_ID
                        End If
                    End If

                End If

                lstContractAppen.Add(contractAppen)
                rgContract.DataSource = lstContractAppen
                rgContract.Rebind()

                rgContract.MasterTableView.Items(0).Selected = True
                rgContract_SelectedIndexChanged(Nothing, Nothing)

                If contractAppen.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID AndAlso checkStatus Is Nothing Then
                    _flag = False
                    EnableControlAll_Cus(False, NORMAL)
                    'btnDownload.Enabled = True
                    'btnUploadFile.Enabled = True
                    '  MainToolBar.Items(0).Enabled = False
                    MainToolBar.Items(1).Enabled = True
                    'MainToolBar.Items(3).Enabled = True
                End If
            End If
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FillData(ByVal empID As Decimal, Optional ByVal isChangedStartDate As Boolean = False)
        Try
            Dim rep As New ProfileRepository
            'Dim repOrg As New ProfileRepository
            Dim repEmp As New ProfileBusinessRepository
            Dim obj = repEmp.GetEmployeCurrentByID(New WorkingDTO With {.EMPLOYEE_ID = empID, .IS_WAGE = False, .EFFECT_DATE = If(rdStartDate.SelectedDate Is Nothing, Date.Now.Date, rdStartDate.SelectedDate)})


            hidEmployeeID.Value = empID
            txtEmployeeCode.Text = obj.EMPLOYEE_CODE
            txtEmployeeName.Text = obj.EMPLOYEE_NAME
            txtOrg.Text = obj.ORG_NAME
            hidOrgID.Value = obj.ORG_ID
            hidOrgCode.Value = rep.GetOrgList(hidOrgID.Value)
            'get thong tin chuc danh 
            'Dim title As TitleDTO
            'title = rep.GetTitileBaseOnEmp(obj.TITLE_ID)
            'If title IsNot Nothing Then
            '    txtTitle.Text = title.NAME_VN
            'End If
            If obj.TITLE_ID IsNot Nothing Then
                txtTitle.Text = obj.TITLE_NAME
            End If
            If isChangedStartDate Then
                Exit Sub
            End If

            'Load danh sách hợp đồng của nhân viên
            listContract = rep.GetContractList(empID)
            If listContract Is Nothing Or listContract.Count = 0 Then
                ShowMessage("Bạn chưa tạo hợp đồng lao động cho nhân viên " & obj.EMPLOYEE_NAME, NotifyType.Warning)
                Exit Sub
            End If

            FillDropDownList(cboContract, listContract, "CONTRACT_NO", "ID", Common.Common.SystemLanguage, True)
            cboContract.SelectedIndex = 0
            'listContract = rep.GetListContractBaseOnEmp(empID)
            'If listContract IsNot Nothing And listContract.Count <> 0 Then
            '    FillDropDownList(cboContract, listContract, "CONTRACT_NO", "ID", Common.Common.SystemLanguage, True)
            '    cboContract.SelectedIndex = 0
            'End If
            GetWorkingMax()
            rep.Dispose()
            repEmp.Dispose()
            'Tao moi ma hop dong
            'If hidOrgID.Value <> String.Empty Then
            '    Dim dt = DateTime.Now
            '    If rdStartDate.SelectedDate IsNot Nothing Then
            '        dt = rdStartDate.SelectedDate
            '    End If
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

    Private Sub CustomValidator1_ServerValidate(source As Object, args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
        Try
            If rdStartDate.SelectedDate IsNot Nothing Then
                If rdStartDate.SelectedDate < radDate.SelectedDate Then
                    args.IsValid = False
                    Exit Sub
                Else
                    args.IsValid = True
                End If
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
End Class
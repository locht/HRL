Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports Common
Imports Common.Common
Imports Common.CommonMessage
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Xml.Serialization
Imports WebAppLog

Public Class ctrlHU_EmpDtlProfile
    Inherits CommonView
    Dim employeeCode As String
    Public Property EmployeeID As Decimal
    Protected WithEvents ctrlFindOrgPopup As ctrlFindOrgPopup
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
    Private procedure As ProfileStoreProcedure
    'Content: Write log time and error
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

#Region "Properties"

    Property EmployeeInfo As EmployeeDTO
        Get
            Return PageViewState(Me.ID & "_EmployeeInfo")
        End Get
        Set(ByVal value As EmployeeDTO)
            PageViewState(Me.ID & "_EmployeeInfo") = value
        End Set
    End Property

    '0 - normal
    '1 - Chọn Org
    '2 - Chọn Quản lý trực tiếp
    Property isLoadPopup As Decimal
        Get
            Return PageViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property

    Property isLoad As Boolean
        Get
            Return PageViewState(Me.ID & "_isLoad")
        End Get
        Set(ByVal value As Boolean)
            PageViewState(Me.ID & "_isLoad") = value
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

    Property ImageFile As Telerik.Web.UI.UploadedFile
        Get
            Return PageViewState(Me.ID & "_ImageFile")
        End Get
        Set(ByVal value As Telerik.Web.UI.UploadedFile)
            PageViewState(Me.ID & "_ImageFile") = value
        End Set
    End Property

#End Region

#Region "Page"
    Public _DIRECT_MANAGER As String
    Public _LEVEL_MANAGER As String
    Public _ORG_ID As String
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            UpdateControlState()
            Refresh()
            CurrentPlaceHolder = Me.ViewName
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If EmployeeInfo IsNot Nothing Then
                EmployeeID = EmployeeInfo.ID
                ctrlHU_TitleConcurrent.SetProperty("EmployeeID", EmployeeID)
            End If
            If Not isLoad Then
                Using rep As New ProfileBusinessRepository
                    If EmployeeInfo IsNot Nothing AndAlso EmployeeInfo.ID <> 0 Then
                        lstbPaper.ClearChecked()
                        If EmployeeInfo.lstPaper IsNot Nothing Then
                            For Each item As RadListBoxItem In lstbPaper.Items
                                If EmployeeInfo.lstPaper.Contains(item.Value) Then
                                    item.Checked = True
                                End If
                            Next
                        End If
                        lstbPaperFiled.ClearChecked()
                        rtEmpCode_OLD.Text = EmployeeInfo.EMPLOYEE_CODE_OLD
                        rtBookNo.Text = EmployeeInfo.BOOKNO
                        rtOtherName.Text = EmployeeInfo.EMPLOYEE_NAME_OTHER
                        If EmployeeInfo.lstPaperFiled IsNot Nothing Then
                            For Each item As RadListBoxItem In lstbPaperFiled.Items
                                If EmployeeInfo.lstPaperFiled.Contains(item.Value) Then
                                    item.Checked = True
                                End If
                            Next
                        End If
                        hidID.Value = EmployeeInfo.ID
                        If EmployeeInfo.LAST_WORKING_ID IsNot Nothing Then
                            hidWorkingID.Value = EmployeeInfo.LAST_WORKING_ID
                        End If
                        If EmployeeInfo.CONTRACT_ID IsNot Nothing Then
                            hidContractID.Value = EmployeeInfo.CONTRACT_ID
                        End If
                        If EmployeeInfo.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                            hidIsTer.Value = -1
                        End If
                        ''Info
                        lblChucDanh.Text = EmployeeInfo.TITLE_NAME_VN
                        lblPhongBan.Text = EmployeeInfo.ORG_NAME
                        lblQLTT.Text = EmployeeInfo.DIRECT_MANAGER_NAME
                        lblQLTMC.Text = EmployeeInfo.LEVEL_MANAGER_NAME
                        _DIRECT_MANAGER = Utilities.ObjToString(EmployeeInfo.DIRECT_MANAGER)
                        _LEVEL_MANAGER = Utilities.ObjToString(EmployeeInfo.LEVEL_MANAGER)
                        _ORG_ID = EmployeeInfo.ORG_ID
                        ''
                        txtEmpCODE.Text = EmployeeInfo.EMPLOYEE_CODE
                        txtFirstNameVN.Text = EmployeeInfo.FIRST_NAME_VN
                        txtLastNameVN.Text = EmployeeInfo.LAST_NAME_VN
                        hidOrgID.Value = EmployeeInfo.ORG_ID
                        txtOrgName.Text = EmployeeInfo.ORG_NAME
                        txtOrgName.ToolTip = Utilities.DrawTreeByString(EmployeeInfo.ORG_DESC)

                        FillDataInControls(EmployeeInfo.ORG_ID)

                        'If EmployeeInfo.ORG_DESC.Split(";").Count > 1 Then
                        '    Dim orgName2 = EmployeeInfo.ORG_DESC.ToString.Split(";")(1)
                        '    txtOrgName2.Text = orgName2.Substring(orgName2.IndexOf(" - ") + 3)
                        'End If
                        cboTitle.SelectedValue = EmployeeInfo.TITLE_ID
                        cboTitle.Text = EmployeeInfo.TITLE_NAME_VN
                        txtTitleGroup.Text = EmployeeInfo.TITLE_GROUP_NAME
                        rdJoinDate.SelectedDate = EmployeeInfo.JOIN_DATE
                        'rdJoinDateState.SelectedDate = EmployeeInfo.JOIN_DATE_STATE
                        rdter_effect_date.SelectedDate = EmployeeInfo.TER_EFFECT_DATE
                        '--------------------------------------------------------------
                        txtContractNo.Text = EmployeeInfo.CONTRACT_NO
                        txtContractType.Text = EmployeeInfo.CONTRACT_TYPE_NAME
                        rdContractEffectDate.SelectedDate = EmployeeInfo.CONTRACT_EFFECT_DATE
                        rdContractExpireDate.SelectedDate = EmployeeInfo.CONTRACT_EXPIRE_DATE
                        If EmployeeInfo.STAFF_RANK_ID IsNot Nothing Then
                            cboStaffRank.SelectedValue = EmployeeInfo.STAFF_RANK_ID
                            cboStaffRank.Text = EmployeeInfo.STAFF_RANK_NAME
                        End If
                        If EmployeeInfo.DIRECT_MANAGER IsNot Nothing Then
                            txtDirectManager.Text = EmployeeInfo.DIRECT_MANAGER_NAME
                            hidDirectManager.Value = EmployeeInfo.DIRECT_MANAGER
                        End If
                        'If EmployeeInfo.LEVEL_MANAGER IsNot Nothing Then
                        '    txtLevelManager.Text = EmployeeInfo.LEVEL_MANAGER_NAME
                        '    hidLevelManager.Value = EmployeeInfo.LEVEL_MANAGER
                        'End If
                        txtTimeID.Text = EmployeeInfo.ITIME_ID

                        If EmployeeInfo.WORK_STATUS IsNot Nothing Then
                            cboWorkStatus.Text = EmployeeInfo.WORK_STATUS_NAME
                            cboWorkStatus.SelectedValue = EmployeeInfo.WORK_STATUS
                        End If

                        If EmployeeInfo.EMP_STATUS IsNot Nothing Then
                            cboEmpStatus.Text = EmployeeInfo.EMP_STATUS_NAME
                            cboEmpStatus.SelectedValue = EmployeeInfo.EMP_STATUS
                        End If

                        If EmployeeInfo.LAST_WORKING_ID IsNot Nothing Then
                            btnFindOrg.Enabled = False
                            EnableRadCombo(cboTitle, False)
                        End If
                        Dim empCV As EmployeeCVDTO
                        Dim empEdu As EmployeeEduDTO
                        Dim empHealth As EmployeeHealthDTO
                        rep.GetEmployeeAllByID(EmployeeInfo.ID, empCV, empEdu, empHealth)
                        If empCV IsNot Nothing Then
                            If empCV.IS_PAY_BANK IsNot Nothing Then
                                chkIs_pay_bank.Checked = empCV.IS_PAY_BANK
                            End If
                            '=======================================================
                            If IsNumeric(empCV.PROVINCENQ_ID) Then
                                cbPROVINCENQ_ID.SelectedValue = empCV.PROVINCENQ_ID
                            End If
                            rtOpption1.Text = empCV.OPPTION1
                            rtOpption2.Text = empCV.OPPTION2
                            rtOpption3.Text = empCV.OPPTION3
                            rtOpption4.Text = empCV.OPPTION4
                            rtOpption5.Text = empCV.OPPTION5
                            If IsDate(empCV.OPPTION6) Then
                                rdOpption6.SelectedDate = empCV.OPPTION6
                            End If
                            If IsDate(empCV.OPPTION6) Then
                                rdOpption6.SelectedDate = empCV.OPPTION6
                            End If
                            If IsDate(empCV.OPPTION7) Then
                                rdOpption7.SelectedDate = empCV.OPPTION7
                            End If
                            If IsDate(empCV.OPPTION9) Then
                                rdOpption9.SelectedDate = empCV.OPPTION9
                            End If
                            If IsDate(empCV.OPPTION10) Then
                                rdOpption10.SelectedDate = empCV.OPPTION10
                            End If
                          
                            If IsNumeric(empCV.PROVINCEEMP_ID) Then
                                cbPROVINCEEMP_ID.SelectedValue = empCV.PROVINCEEMP_ID
                                cbPROVINCEEMP_ID.Text = empCV.PROVINCEEMP_NAME
                            End If
                            If IsNumeric(empCV.DISTRICTEMP_ID) Then
                                cbDISTRICTEMP_ID.SelectedValue = empCV.DISTRICTEMP_ID
                                cbDISTRICTEMP_ID.Text = empCV.DISTRICTEMP_NAME
                            End If
                            If IsNumeric(empCV.WARDEMP_ID) Then
                                cbWARDEMP_ID.SelectedValue = empCV.WARDEMP_ID
                                cbWARDEMP_ID.Text = empCV.WARDEMP_NAME
                            End If
                            '===============================================

                            If empCV.WORKPLACE_ID IsNot Nothing Then
                                cboWorkplace.SelectedValue = empCV.WORKPLACE_ID
                                cboWorkplace.Text = empCV.WORKPLACE_NAME
                            End If
                            If empCV.INS_REGION_ID IsNot Nothing Then
                                cboInsRegion.SelectedValue = empCV.INS_REGION_ID
                                cboInsRegion.Text = empCV.INS_REGION_NAME
                            End If
                            If empCV.GENDER IsNot Nothing Then
                                cboGender.SelectedValue = empCV.GENDER
                                cboGender.Text = empCV.GENDER_NAME
                            End If
                            rdBirthDate.SelectedDate = empCV.BIRTH_DATE

                            If empCV.MARITAL_STATUS IsNot Nothing Then
                                cboFamilyStatus.SelectedValue = empCV.MARITAL_STATUS
                                cboFamilyStatus.Text = empCV.MARITAL_STATUS_NAME
                            End If
                            If empCV.RELIGION IsNot Nothing Then
                                cboReligion.SelectedValue = empCV.RELIGION
                                cboReligion.Text = empCV.RELIGION_NAME
                            End If
                            If empCV.NATIVE IsNot Nothing Then
                                cboNative.SelectedValue = empCV.NATIVE
                                cboNative.Text = empCV.NATIVE_NAME
                            End If
                            If empCV.NATIONALITY IsNot Nothing Then
                                cboNationlity.SelectedValue = empCV.NATIONALITY
                                cboNationlity.Text = empCV.NATIONALITY_NAME
                            End If
                            txtNavAddress.Text = empCV.NAV_ADDRESS
                            If empCV.NAV_PROVINCE IsNot Nothing Then
                                cboNav_Province.SelectedValue = empCV.NAV_PROVINCE
                                cboNav_Province.Text = empCV.NAV_PROVINCE_NAME
                            End If
                            If empCV.NAV_DISTRICT IsNot Nothing Then
                                cboNav_District.SelectedValue = empCV.NAV_DISTRICT
                                cboNav_District.Text = empCV.NAV_DISTRICT_NAME
                            End If
                            If empCV.NAV_WARD IsNot Nothing Then
                                cboNav_Ward.SelectedValue = empCV.NAV_WARD
                                cboNav_Ward.Text = empCV.NAV_WARD_NAME
                            End If
                            txtPerAddress.Text = empCV.PER_ADDRESS
                            If empCV.PER_PROVINCE IsNot Nothing Then
                                cboPer_Province.SelectedValue = empCV.PER_PROVINCE
                                cboPer_Province.Text = empCV.PER_PROVINCE_NAME
                            End If
                            If empCV.PER_DISTRICT IsNot Nothing Then
                                cboPer_District.SelectedValue = empCV.PER_DISTRICT
                                cboPer_District.Text = empCV.PER_DISTRICT_NAME
                            End If
                            If empCV.PER_WARD IsNot Nothing Then
                                cboPer_Ward.SelectedValue = empCV.PER_WARD
                                cboPer_Ward.Text = empCV.PER_WARD_NAME
                            End If
                            ' SĐT
                            txtHomePhone.Text = empCV.HOME_PHONE
                            txtMobilePhone.Text = empCV.MOBILE_PHONE
                            ' CMND
                            txtID_NO.Text = empCV.ID_NO
                            rdIDDate.SelectedDate = empCV.ID_DATE
                            SetValueComboBox(cboIDPlace, empCV.ID_PLACE, empCV.PLACE_NAME)
                            ' cboIDPlace.SelectedValue = empCV.ID_PLACE
                            'Hộ chiếu
                            txtPassNo.Text = empCV.PASS_NO
                            rdPassDate.SelectedDate = empCV.PASS_DATE
                            rdPassExpireDate.SelectedDate = empCV.PASS_EXPIRE
                            txtPassPlace.Text = empCV.PASS_PLACE
                            'Visa
                            txtVisa.Text = empCV.VISA
                            rdVisaDate.SelectedDate = empCV.VISA_DATE
                            rdVisaExpireDate.SelectedDate = empCV.VISA_EXPIRE
                            txtVisaPlace.Text = empCV.VISA_PLACE
                            'Giấy phép lao động
                            txtWorkPermit.Text = empCV.WORK_PERMIT
                            rdWorkPermitDate.SelectedDate = empCV.WORK_PERMIT_DATE
                            rdWorPermitExpireDate.SelectedDate = empCV.WORK_PERMIT_EXPIRE
                            txtWorkPermitPlace.Text = empCV.WORK_PERMIT_PLACE
                            txtPitCode.Text = empCV.PIT_CODE
                            txtPerEmail.Text = empCV.PER_EMAIL
                            txtCareer.Text = empCV.CAREER
                            txtWorkEmail.Text = empCV.WORK_EMAIL
                            'Người liên hệ khi cần
                            txtContactPerson.Text = empCV.CONTACT_PER
                            'Điện thoại người liên hệ
                            txtContactPersonPhone.Text = empCV.CONTACT_PER_PHONE
                            'chkDangPhi.Checked = False
                            'If empCV.DANG_PHI IsNot Nothing Then
                            '    chkDangPhi.Checked = empCV.DANG_PHI
                            'End If
                            chkDoanPhi.Checked = False
                            If empCV.DOAN_PHI IsNot Nothing Then
                                chkDoanPhi.Checked = empCV.DOAN_PHI
                            End If
                            If empCV.BANK_ID IsNot Nothing Then
                                cboBank.SelectedValue = empCV.BANK_ID
                                cboBank.Text = empCV.BANK_NAME
                            End If
                            If empCV.BANK_BRANCH_ID IsNot Nothing Then
                                cboBankBranch.SelectedValue = empCV.BANK_BRANCH_ID
                                cboBankBranch.Text = empCV.BANK_BRANCH_NAME
                            End If

                            'rdNgayVaoDang.SelectedDate = empCV.NGAY_VAO_DANG
                            rdNgayVaoDoan.SelectedDate = empCV.NGAY_VAO_DOAN
                            'txtChucVuDang.Text = empCV.CHUC_VU_DANG
                            txtChucVuDoan.Text = empCV.CHUC_VU_DOAN
                            'txtNoiVaoDang.Text = empCV.NOI_VAO_DANG
                            txtNoiVaoDoan.Text = empCV.NOI_VAO_DOAN
                            txtBankNo.Text = empCV.BANK_NO

                        End If
                        If empEdu IsNot Nothing Then
                            If empEdu.GRADUATION_YEAR IsNot Nothing Then
                                txtNamTN.Value = empEdu.GRADUATION_YEAR
                            End If
                            If empEdu.ACADEMY IsNot Nothing Then
                                cboAcademy.SelectedValue = empEdu.ACADEMY
                                cboAcademy.Text = empEdu.ACADEMY_NAME
                            End If
                            If empEdu.LEARNING_LEVEL IsNot Nothing Then
                                cboLearningLevel.SelectedValue = empEdu.LEARNING_LEVEL
                                cboLearningLevel.Text = empEdu.LEARNING_LEVEL_NAME
                            End If
                            If empEdu.LANGUAGE_LEVEL IsNot Nothing Then
                                cboLangLevel.SelectedValue = empEdu.LANGUAGE_LEVEL
                                cboLangLevel.Text = empEdu.LANGUAGE_LEVEL_NAME
                            End If
                            txtLangMark.Text = empEdu.LANGUAGE_MARK
                            If empEdu.LANGUAGE IsNot Nothing Then
                                cboLanguage.SelectedValue = empEdu.LANGUAGE
                            End If
                            If empEdu.MAJOR IsNot Nothing Then
                                cboMajor.SelectedValue = empEdu.MAJOR
                                cboMajor.Text = empEdu.MAJOR_NAME
                            End If
                        End If

                        If empHealth IsNot Nothing Then
                            txtCanNang.Text = empHealth.CAN_NANG
                            txtChieuCao.Text = empHealth.CHIEU_CAO
                            txtDaHoaLieu.Text = empHealth.DA_HOA_LIEU
                            txtGhiChuSK.Text = empHealth.GHI_CHU_SUC_KHOE
                            txtHuyetAp.Text = empHealth.HUYET_AP
                            txtMatPhai.Text = empHealth.MAT_PHAI
                            txtMatTrai.Text = empHealth.MAT_TRAI
                            txtNhomMau.Text = empHealth.NHOM_MAU
                            txtPhoiNguc.Text = empHealth.PHOI_NGUC
                            txtRangHamMat.Text = empHealth.RANG_HAM_MAT
                            txtTaiMuiHong.Text = empHealth.TAI_MUI_HONG
                            txtTim.Text = empHealth.TIM
                            txtVienGanB.Text = empHealth.VIEM_GAN_B
                            txtLoaiSucKhoe.Text = empHealth.LOAI_SUC_KHOE
                        End If

                    End If
                End Using
                isLoad = True
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
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
            'Dim dtDistrict As DataTable
            'Dim dtWard As DataTable
            Using rep As New ProfileRepository
                dtData = rep.GetOtherList("HU_PAPER")
                dtPlace = rep.GetProvinceList(True)
                dtLanguageleve = rep.GetOtherList("LANGUAGE_LEVEL", True)
                dtLanguage = rep.GetOtherList("RC_LANGUAGE_LEVEL", True)
                FillRadCombobox(cboIDPlace, dtPlace, "NAME", "ID")
                FillRadCombobox(cbPROVINCENQ_ID, dtPlace, "NAME", "ID")
                FillRadCombobox(cboLangLevel, dtLanguageleve, "NAME", "ID")
                FillRadCombobox(cboLanguage, dtLanguage, "NAME", "ID")
                'FillRadCombobox(cboBIRTH_PLACE, dtPlace, "NAME", "ID")
            End Using
            FillCheckBoxList(lstbPaper, dtData, "NAME", "ID")
            FillCheckBoxList(lstbPaperFiled, dtData, "NAME", "ID")

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'Khoi tao ToolBar
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar,
                         ToolbarItem.Edit,
                         ToolbarItem.Save,
                         ToolbarItem.Cancel)
            CType(Me.MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = False

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Using rep As New ProfileRepository
                Dim regions = rep.GetOtherList("LOCATION", True)
                cboInsRegion.DataSource = regions
                cboInsRegion.DataTextField = "Name"
                cboInsRegion.DataValueField = "ID"
            End Using
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If ctrlFindOrgPopup IsNot Nothing AndAlso phPopupOrg.Controls.Contains(ctrlFindOrgPopup) Then
                phPopupOrg.Controls.Remove(ctrlFindOrgPopup)
            End If
            If ctrlFindEmployeePopup IsNot Nothing AndAlso phPopupDirect.Controls.Contains(ctrlFindEmployeePopup) Then
                phPopupDirect.Controls.Remove(ctrlFindEmployeePopup)
            End If
            If ctrlFindEmployeePopup IsNot Nothing AndAlso phPopupLevel.Controls.Contains(ctrlFindEmployeePopup) Then
                phPopupLevel.Controls.Remove(ctrlFindEmployeePopup)
            End If
            Select Case isLoadPopup
                Case 1
                    ctrlFindOrgPopup = Me.Register("ctrlFindOrgPopup", "Common", "ctrlFindOrgPopup")
                    phPopupOrg.Controls.Add(ctrlFindOrgPopup)
                Case 2
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeeDirectPopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.MultiSelect = False
                    ctrlFindEmployeePopup.LoadAllOrganization = True
                    phPopupDirect.Controls.Add(ctrlFindEmployeePopup)
                Case 3
                    ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeeLevelPopup", "Common", "ctrlFindEmployeePopup")
                    ctrlFindEmployeePopup.MultiSelect = False
                    ctrlFindEmployeePopup.LoadAllOrganization = True
                    phPopupLevel.Controls.Add(ctrlFindEmployeePopup)
            End Select

            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    rtIdTitleConcurrent.Visible = False
                    RadPane4.Visible = False
                    chkIs_pay_bank.Checked = True
                    chkSaveHistory.Visible = False
                    EnableControlAll(False, cboWorkStatus, txtEmpCODE, cboEmpStatus)
                    EnableControlAll(True, lstbPaper, lstbPaperFiled,
                                        txtBankNo,
                                       txtDaHoaLieu, txtTimeID,
                                       txtFirstNameVN, txtGhiChuSK,
                                       txtHomePhone, txtHuyetAp, txtID_NO,
                                       cboIDPlace, txtLangMark,
                                       txtLastNameVN, txtMatPhai, txtMatTrai,
                                       txtMobilePhone, txtNavAddress, txtNhomMau,
                                        txtPassNo, txtPassPlace,
                                       txtPerAddress, txtPerEmail, txtPhoiNguc, txtCareer,
                                       txtPitCode, txtRangHamMat, txtTaiMuiHong, txtTim,
                                       txtVienGanB, txtVisa, txtVisaPlace,
                                       txtWorkEmail, txtWorkPermit, txtWorkPermitPlace,
                                       txtContactPerson, txtContactPersonPhone, txtChucVuDoan,
                                       rdBirthDate, rdContractExpireDate, rdContractEffectDate, rdIDDate,
                                       rdNgayVaoDoan, rdPassDate, rdPassExpireDate,
                                       rdVisaDate, rdVisaExpireDate, rdWorkPermitDate, rdWorPermitExpireDate,
                                       txtCanNang, txtChieuCao,
                                       cboAcademy, cboBank, cboBankBranch, cboFamilyStatus, cboWorkplace, cboInsRegion,
                                       cboGender, cboLangLevel, cboLanguage, cboLearningLevel, txtLoaiSucKhoe,
                                       cboMajor, cboNationlity, cboNative, cboNav_Province, cboPer_Province,
                                       cboReligion, cboStaffRank, cboTitle,
                                       cboPer_District, cboPer_Ward, cboNav_District, cboNav_Ward, cbPROVINCEEMP_ID, cbDISTRICTEMP_ID, cbWARDEMP_ID, cbPROVINCENQ_ID,
                                       hidID, hidOrgID, hidDirectManager, hidLevelManager,
                                       chkDoanPhi, btnFindDirect, btnFindOrg,
                                       rtOpption1, rtOpption2, rtOpption3, rtOpption4, rtOpption5,
                                       rdOpption6, rdOpption7, rdOpption8, rdOpption9, rdOpption10,
                                       rtEmpCode_OLD, rtBookNo, rtOtherName)
                    If Not Me.AllowCreate Then
                        txtFirstNameVN.ReadOnly = True
                        txtLastNameVN.ReadOnly = True
                        txtTimeID.ReadOnly = True
                    End If

                Case CommonMessage.STATE_EDIT
                    rtIdTitleConcurrent.Visible = True
                    RadPane4.Visible = True
                    chkSaveHistory.Visible = True
                    chkSaveHistory.Checked = True
                    EnableControlAll(False, cboWorkStatus, txtEmpCODE, cboEmpStatus)
                    EnableControlAll(True, lstbPaper, lstbPaperFiled,
                                        txtBankNo,
                                       txtDaHoaLieu,
                                       txtFirstNameVN, txtGhiChuSK,
                                       txtHomePhone, txtHuyetAp, txtID_NO,
                                       cboIDPlace, txtLangMark, txtTimeID,
                                       txtLastNameVN, txtMatPhai, txtMatTrai,
                                       txtMobilePhone, txtNavAddress, txtNhomMau,
                                        txtPassNo, txtPassPlace,
                                       txtPerAddress, txtPerEmail, txtPhoiNguc, txtCareer,
                                       txtPitCode, txtRangHamMat, txtTaiMuiHong, txtTim,
                                       txtVienGanB, txtVisa, txtVisaPlace,
                                       txtWorkEmail, txtWorkPermit, txtWorkPermitPlace,
                          txtContactPerson, txtContactPersonPhone, txtChucVuDoan,
                          rdBirthDate, rdContractExpireDate, rdContractEffectDate, rdIDDate,
                           rdNgayVaoDoan, rdPassDate, rdPassExpireDate,
                          rdVisaDate, rdVisaExpireDate, rdWorkPermitDate, rdWorPermitExpireDate,
                                       txtCanNang, txtChieuCao,
                                       cboAcademy, cboBank, cboBankBranch, cboFamilyStatus,
                                       cboGender, cboLangLevel, cboWorkplace, cboInsRegion,
                                       cboLanguage, cboLearningLevel, txtLoaiSucKhoe,
                                       cboMajor, cboNationlity, cboNative, cboNav_Province, cboPer_Province,
                                       cboReligion, cboStaffRank, cboTitle,
                                       cboPer_District, cboPer_Ward, cboNav_District, cboNav_Ward,
                                       hidID, hidOrgID, hidDirectManager, hidLevelManager,
                                        chkDoanPhi, cbPROVINCEEMP_ID, cbDISTRICTEMP_ID, cbWARDEMP_ID, cbPROVINCENQ_ID,
                                       btnFindDirect, btnFindOrg,
                                       rtOpption1, rtOpption2, rtOpption3, rtOpption4, rtOpption5,
                                       rdOpption6, rdOpption7, rdOpption8, rdOpption9, rdOpption10,
                                       rtEmpCode_OLD, rtBookNo, rtOtherName)
                    If Not Me.AllowModify Then
                        txtFirstNameVN.ReadOnly = True
                        txtLastNameVN.ReadOnly = True
                        txtTimeID.ReadOnly = True
                    End If
                    If EmployeeInfo.LAST_WORKING_ID IsNot Nothing Then
                        EnableControlAll(False, cboTitle, cboStaffRank, btnFindOrg)
                    End If
                Case Else
                    EnableControlAll(False, lstbPaper, lstbPaperFiled, cboWorkStatus, cboEmpStatus, txtEmpCODE,
                                       txtBankNo,
                                       txtDaHoaLieu,
                                       txtFirstNameVN, txtGhiChuSK,
                                       txtHomePhone, txtHuyetAp, txtID_NO,
                                       cboIDPlace, txtLangMark, txtTimeID,
                                       txtLastNameVN, txtMatPhai, txtMatTrai,
                                       txtMobilePhone, txtNavAddress, txtNhomMau,
                                        txtPassNo, txtPassPlace,
                                       txtPerAddress, txtPerEmail, txtPhoiNguc, txtCareer,
                                       txtPitCode, txtRangHamMat, txtTaiMuiHong, txtTim,
                                       txtVienGanB, txtVisa, txtVisaPlace,
                                       txtWorkEmail, txtWorkPermit, txtWorkPermitPlace,
                                       txtContactPerson, txtContactPersonPhone, txtChucVuDoan,
                                       rdBirthDate, rdContractExpireDate, rdContractEffectDate, rdIDDate,
                                       rdNgayVaoDoan, rdPassDate, rdPassExpireDate,
                                       rdVisaDate, rdVisaExpireDate, rdWorkPermitDate, rdWorPermitExpireDate,
                                       txtCanNang, txtChieuCao,
                                       cboAcademy, cboBank, cboBankBranch, cboFamilyStatus,
                                       cboGender, cboLangLevel, cboWorkplace, cboInsRegion,
                                       cboLanguage, cboLearningLevel, txtLoaiSucKhoe,
                                       cboMajor, cboNationlity, cboNative, cboNav_Province, cboPer_Province,
                                       cboReligion, cboStaffRank, cboTitle,
                                       cboPer_District, cboPer_Ward, cboNav_District, cboNav_Ward,
                                       hidID, hidOrgID, hidDirectManager, hidLevelManager,
                                       chkDoanPhi, cbPROVINCEEMP_ID, cbDISTRICTEMP_ID, cbWARDEMP_ID, cbPROVINCENQ_ID,
                                       btnFindDirect, btnFindOrg,
                                       rtOpption1, rtOpption2, rtOpption3, rtOpption4, rtOpption5,
                                       rdOpption6, rdOpption7, rdOpption8, rdOpption9, rdOpption10,
                                       rtEmpCode_OLD, rtBookNo, rtOtherName)

            End Select
            rep.Dispose()
            ChangeToolbarState()
            Me.Send(CurrentState)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim objEmployee As New EmployeeDTO
            Dim rep As New ProfileBusinessRepository
            Dim _err As String = ""
            Dim strEmpID As Decimal
            If EmployeeInfo IsNot Nothing Then
                strEmpID = EmployeeInfo.ID
            End If
            Dim checkID_NO As Boolean = False
            Dim checkBank_No As Boolean = False
            Dim dtData As DataTable
            Dim lstEmpID As String = ""

            Dim _param = New ParamDTO With {.ORG_ID = 46,
                       .IS_DISSOLVE = False}
            Dim _filter As New EmployeeDTO

            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case TOOLBARITEM_CREATE
                    ResetControlValue()
                    EmployeeInfo = Nothing
                    CurrentState = CommonMessage.STATE_NEW

                Case TOOLBARITEM_EDIT
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
                    Page.Validate("EmpProfile")
                    If Page.IsValid Then
                        Select Case CurrentState
                            Case STATE_NEW

                                checkID_NO = rep.ValidateEmployee("EXIST_ID_NO", "", txtID_NO.Text)


                                checkBank_No = rep.ValidateEmployee("EXIST_BANK_NO", "", txtBankNo.Text)

                                If Not checkID_NO Then
                                    _filter.ID_NO = txtID_NO.Text

                                    dtData = rep.GetListEmployeePaging(_filter, _param).ToTable()
                                    If dtData IsNot Nothing Then
                                        For index = 0 To dtData.Rows.Count - 1
                                            lstEmpID = lstEmpID + "," + dtData.Rows(index)("EMPLOYEE_CODE").ToString
                                        Next
                                    End If

                                    If lstEmpID <> "" Then
                                        lstEmpID = lstEmpID.Substring(1, lstEmpID.Length - 1)
                                    End If


                                    ctrlMessageBox.MessageText = Translate("Số CMND đã tồn tại cho nhân viên :" + lstEmpID + " Bạn muốn lưu không?")
                                    ctrlMessageBox.ActionName = "ACTION_BANK_NO"
                                    ctrlMessageBox.DataBind()
                                    ctrlMessageBox.Show()

                                ElseIf Not checkBank_No Then
                                    ctrlMessageBox.MessageText = Translate("Số tài khoản: " + txtBankNo.Text + " đã tồn tại Bạn muốn lưu không?")
                                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ID_NO
                                    ctrlMessageBox.DataBind()
                                    ctrlMessageBox.Show()
                                Else

                                    If Save(strEmpID, _err) Then
                                        Page.Response.Redirect("Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=" & strEmpID & "&state=Normal&message=success", False)
                                        Exit Sub
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                                        Exit Sub
                                    End If
                                End If

                            Case STATE_EDIT

                                checkID_NO = rep.ValidateEmployee("EXIST_ID_NO", EmployeeInfo.EMPLOYEE_CODE, txtID_NO.Text)
                                If Not checkID_NO Then
                                    _filter.ID_NO = txtID_NO.Text

                                    dtData = rep.GetListEmployeePaging(_filter, _param).ToTable()
                                    If dtData IsNot Nothing Then
                                        For index = 0 To dtData.Rows.Count - 1
                                            If dtData.Rows(index)("EMPLOYEE_CODE").ToString <> txtEmpCODE.Text Then
                                                lstEmpID = lstEmpID + "," + dtData.Rows(index)("EMPLOYEE_CODE").ToString
                                            End If
                                        Next
                                    End If

                                    If lstEmpID <> "" Then
                                        lstEmpID = lstEmpID.Substring(1, lstEmpID.Length - 1)
                                    End If

                                    ctrlMessageBox.MessageText = Translate("Số CMND đã tồn tại cho nhân viên :" + lstEmpID + ". Bạn muốn lưu không?")
                                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ID_NO
                                    ctrlMessageBox.DataBind()
                                    ctrlMessageBox.Show()
                                Else
                                    If Save(strEmpID, _err) Then
                                        CurrentState = CommonMessage.STATE_NORMAL
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                    Else
                                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                                    End If
                                End If

                        End Select
                    End If
                Case TOOLBARITEM_CANCEL
                    If CurrentState = CommonMessage.STATE_NEW Then 'Nếu là trạng thái new thì xóa ảnh hiện tại
                        ScriptManager.RegisterStartupScript(Page, Page.GetType, "Close", "CloseWindow();", True)
                    End If
                    CurrentState = CommonMessage.STATE_NORMAL

                Case TOOLBARITEM_DELETE
                    'Kiểm tra các điều kiện để xóa.
                    If EmployeeInfo.ID = 0 Then 'Nếu đang thêm mới nhân viên thì exit sub.
                        Exit Sub
                    End If
                    'Hiển thị Confirm delete.
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

            End Select
            UpdateControlState()
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            ' DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim strError As String = ""
                Dim lstEmpID = New List(Of Decimal)
                Dim rep As New ProfileBusinessRepository
                lstEmpID.Add(EmployeeInfo.ID)
                rep.DeleteEmployee(lstEmpID, strError)
                If strError = "" Then
                    ResetControlValue()
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                    CurrentState = CommonMessage.STATE_NEW
                Else
                    ShowMessage(Translate("Nhân viên này đã có hợp đồng không thể xóa"), Utilities.NotifyType.Warning)
                    CurrentState = CommonMessage.STATE_NORMAL
                End If
                UpdateControlState()
                rep.Dispose()
            End If

            ' kiểm tra xem trùng CMND có muốn lưu hay không
            If e.ActionName = CommonMessage.ACTION_ID_NO And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim _err As String = ""
                Dim strEmpID As Decimal
                If EmployeeInfo IsNot Nothing Then
                    strEmpID = EmployeeInfo.ID
                End If

                Select Case CurrentState
                    Case CommonMessage.STATE_NEW

                        If Save(strEmpID, _err) Then
                            Page.Response.Redirect("Dialog.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=" & strEmpID & "&state=Normal&noscroll=1&message=success&reload=1")
                            Exit Sub
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                            Exit Sub
                        End If

                    Case CommonMessage.STATE_EDIT
                        If Save(strEmpID, _err) Then
                            CurrentState = CommonMessage.STATE_NORMAL
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                        End If
                End Select

            End If

            If e.ActionName = "ACTION_BANK_NO" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim _err As String = ""
                Dim strEmpID As Decimal
                If EmployeeInfo IsNot Nothing Then
                    strEmpID = EmployeeInfo.ID
                End If
                Select Case CurrentState
                    Case CommonMessage.STATE_NEW
                        If Save(strEmpID, _err) Then
                            Page.Response.Redirect("Dialog.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=" & strEmpID & "&state=Normal&noscroll=1&message=success&reload=1")
                            Exit Sub
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                            Exit Sub
                        End If
                End Select
            Else
                txtBankNo.Text = String.Empty
                txtBankNo.Focus()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub cboCommon_ItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs) _
    Handles cboAcademy.ItemsRequested, cboBank.ItemsRequested, cboBankBranch.ItemsRequested, cboWorkplace.ItemsRequested,
        cboFamilyStatus.ItemsRequested, cboGender.ItemsRequested, cboLangLevel.ItemsRequested, cboLearningLevel.ItemsRequested, cboMajor.ItemsRequested,
         cboNationlity.ItemsRequested, cboNative.ItemsRequested, cboNav_Province.ItemsRequested,
        cboPer_Province.ItemsRequested, cboReligion.ItemsRequested, cboStaffRank.ItemsRequested, cboTitle.ItemsRequested,
        cboWorkStatus.ItemsRequested, cboEmpStatus.ItemsRequested, cbWARDEMP_ID.ItemsRequested, cbDISTRICTEMP_ID.ItemsRequested, cbPROVINCEEMP_ID.ItemsRequested,
        cboPer_District.ItemsRequested, cboPer_Ward.ItemsRequested, cboNav_District.ItemsRequested, cboNav_Ward.ItemsRequested, cbPROVINCENQ_ID.ItemsRequested
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Using rep As New ProfileRepository
                Dim dtData As DataTable
                Dim sText As String = e.Text
                Dim dValue As Decimal
                Dim sSelectValue As String = IIf(e.Context("value") IsNot Nothing, e.Context("value"), "")
                Select Case sender.ID
                    'Case cboGraduateSchool.ID
                    '    dtData = rep.GetOtherList("HU_GRADUATE_SCHOOL", True)
                    Case cboAcademy.ID
                        dtData = rep.GetOtherList("ACADEMY", True)
                    Case cboBank.ID
                        dtData = rep.GetBankList(True)
                    Case cboBankBranch.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        dtData = rep.GetBankBranchList(dValue, True)
                    Case cboFamilyStatus.ID
                        dtData = rep.GetOtherList("FAMILY_STATUS", True)
                    Case cboGender.ID
                        dtData = rep.GetOtherList("GENDER", True)
                    Case cboLearningLevel.ID
                        dtData = rep.GetOtherList("LEARNING_LEVEL", True)
                    Case cboMajor.ID
                        dtData = rep.GetOtherList("MAJOR", True)
                    Case cboNationlity.ID
                        dtData = rep.GetNationList(True)
                    Case cboNative.ID
                        dtData = rep.GetOtherList("NATIVE", True)
                    Case cboNav_Province.ID, cboPer_Province.ID, cboWorkplace.ID, cboIDPlace.ID, cbPROVINCEEMP_ID.ID, cbPROVINCENQ_ID.ID
                        dtData = rep.GetProvinceList(True)
                    Case cboNav_District.ID, cboPer_District.ID, cbDISTRICTEMP_ID.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        dtData = rep.GetDistrictList(dValue, True)
                    Case cboNav_Ward.ID, cboPer_Ward.ID, cbWARDEMP_ID.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        dtData = rep.GetWardList(dValue, True)
                    Case cboReligion.ID
                        dtData = rep.GetOtherList("RELIGION", True)
                    Case cboStaffRank.ID
                        dtData = rep.GetStaffRankList(True)
                    Case cboTitle.ID
                        dValue = IIf(e.Context("valueCustom") IsNot Nothing, e.Context("valueCustom"), 0)
                        dtData = rep.GetTitleByOrgID(dValue, True)
                        'Case cboTrainingForm.ID
                        '    dtData = rep.GetOtherList("TRAINING_FORM", True)
                    Case cboWorkStatus.ID
                        dtData = rep.GetOtherList("WORK_STATUS", True)
                    Case cboWorkStatus.ID
                        dtData = rep.GetOtherList("EMP_STATUS", True)
                    Case cboLangLevel.ID
                        dtData = rep.GetOtherList("LEARNING_LEVEL", True)
                    Case cboInsRegion.ID
                        dtData = rep.GetInsRegionList(True)
                End Select

                If sText <> "" Then
                    Dim dtExist = (From p In dtData
                                   Where p("NAME") IsNot DBNull.Value AndAlso
                                  p("NAME").ToString.ToUpper = sText.ToUpper)

                    If dtExist.Count = 0 Then
                        Dim dtFilter = (From p In dtData
                                        Where p("NAME") IsNot DBNull.Value AndAlso
                                  p("NAME").ToString.ToUpper.Contains(sText.ToUpper))

                        If dtFilter.Count > 0 Then
                            dtData = dtFilter.CopyToDataTable
                        Else
                            dtData = dtData.Clone
                        End If

                        Dim itemOffset As Integer = e.NumberOfItems
                        Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                        e.EndOfItems = endOffset = dtData.Rows.Count

                        For i As Integer = itemOffset To endOffset - 1
                            Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                            Select Case sender.ID
                                Case cboTitle.ID
                                    radItem.Attributes("GROUP_NAME") = dtData.Rows(i)("GROUP_NAME").ToString()
                            End Select
                            sender.Items.Add(radItem)
                        Next
                    Else

                        Dim itemOffset As Integer = e.NumberOfItems
                        Dim endOffset As Integer = dtData.Rows.Count
                        e.EndOfItems = True

                        For i As Integer = itemOffset To endOffset - 1
                            Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                            Select Case sender.ID
                                Case cboTitle.ID
                                    radItem.Attributes("GROUP_NAME") = dtData.Rows(i)("GROUP_NAME").ToString()
                            End Select
                            sender.Items.Add(radItem)
                        Next
                    End If
                Else
                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                    e.EndOfItems = endOffset = dtData.Rows.Count

                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                        Select Case sender.ID
                            Case cboTitle.ID
                                radItem.Attributes("GROUP_NAME") = dtData.Rows(i)("GROUP_NAME").ToString()
                        End Select
                        sender.Items.Add(radItem)
                    Next
                End If
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            ' DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnFindDirect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindDirect.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 2
            UpdateControlState()
            ctrlFindEmployeePopup.MustHaveContract = False
            ctrlFindEmployeePopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim lstCommonEmployee As New List(Of CommonBusiness.EmployeePopupFindDTO)
        'Dim list As List(Of EmployeeDTO)
        'Dim rep As New ProfileBusinessRepository
        Dim _param = New ParamDTO With {.ORG_ID = 46,
                   .IS_DISSOLVE = False}
        Dim _filter As New EmployeeDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            lstCommonEmployee = CType(ctrlFindEmployeePopup.SelectedEmployee, List(Of CommonBusiness.EmployeePopupFindDTO))
            If lstCommonEmployee.Count <> 0 Then
                Select Case isLoadPopup
                    Case 2
                        txtDirectManager.Text = lstCommonEmployee(0).FULLNAME_VN
                        hidDirectManager.Value = lstCommonEmployee(0).ID.ToString()
                        'If lstCommonEmployee(0).DIRECT_MANAGER IsNot Nothing Then
                        '    _filter.DIRECT_MANAGER = lstCommonEmployee(0).DIRECT_MANAGER
                        '    list = rep.GetListEmployeePaging(_filter, _param)
                        '    If list.Count > 0 Then
                        '        txtLevelManager.Text = list(0).FULLNAME_VN
                        '        hidLevelManager.Value = list(0).ID.ToString()
                        '    End If
                        'End If
                    Case 3
                        'txtLevelManager.Text = lstCommonEmployee(0).FULLNAME_VN
                        'hidLevelManager.Value = lstCommonEmployee(0).ID.ToString()
                End Select
            End If
            isLoadPopup = 0
            ' rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnFindOrg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindOrg.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindOrgPopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlPopupCommon_CancelClicked(ByVal sender As Object, ByVal e As EventArgs) Handles ctrlFindOrgPopup.CancelClicked, ctrlFindEmployeePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    Private Sub ctrlOrgPopup_OrganizationSelected(ByVal sender As Object, ByVal e As Common.OrganizationSelectedEventArgs) Handles ctrlFindOrgPopup.OrganizationSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim orgItem = ctrlFindOrgPopup.CurrentItemDataObject
            If orgItem IsNot Nothing Then
                hidOrgID.Value = e.CurrentValue
                'txtOrgName.Text = orgItem.NAME_VN
                'Fill data in controls
                'Edit by: ChienNV
                FillDataInControls(e.CurrentValue)
                'End edit;
                'If orgItem.DESCRIPTION_PATH.ToString.Split(";").Count > 1 Then
                '    Dim orgName2 = orgItem.DESCRIPTION_PATH.ToString.Split(";")(1)
                '    txtOrgName2.Text = orgName2.Substring(orgName2.IndexOf(" - ") + 3)
                'End If
                'txtOrgName2.ToolTip = Utilities.DrawTreeByString(orgItem.DESCRIPTION_PATH)
            End If
            cboTitle.ClearValue()
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    '' check CMND có nằm trong danh sách đen hay không
    Private Sub cusNO_ID_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusNO_ID.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If txtID_NO.Text = "" Then
                args.IsValid = True
                Exit Sub
            End If
            Select Case CurrentState
                Case STATE_NEW
                    Using rep As New ProfileBusinessRepository
                        args.IsValid = rep.ValidateEmployee("EXIST_ID_NO_TERMINATE", "", txtID_NO.Text)
                    End Using
                Case STATE_EDIT
                    Using rep As New ProfileBusinessRepository
                        args.IsValid = rep.ValidateEmployee("EXIST_ID_NO_TERMINATE", EmployeeInfo.EMPLOYEE_CODE, txtID_NO.Text)
                    End Using
                Case Else
                    args.IsValid = True
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub cusTitle_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusTitle.ServerValidate
        If cboTitle.Text = "" Then
            args.IsValid = False
            Exit Sub
        End If
    End Sub

    Private Sub cusTimeID_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusTimeID.ServerValidate
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If cboTitle.Text = "" Then
                args.IsValid = False
                Exit Sub
            End If
            If txtTimeID.Text Is Nothing Then
                args.IsValid = True
                Exit Sub
            End If
            Select Case CurrentState
                Case STATE_NEW
                    Using rep As New ProfileBusinessRepository
                        args.IsValid = rep.ValidateEmployee("EXIST_TIME_ID", "", txtTimeID.Text)
                    End Using
                Case STATE_EDIT
                    Using rep As New ProfileBusinessRepository
                        args.IsValid = rep.ValidateEmployee("EXIST_TIME_ID", EmployeeInfo.EMPLOYEE_CODE, txtTimeID.Text)
                    End Using
                Case Else
                    args.IsValid = True
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

    Public Sub ResetControlValue()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue(txtEmpCODE, txtBankNo,
                          txtDaHoaLieu, txtDirectManager, txtManager,
                          txtFirstNameVN, txtGhiChuSK,
                          txtHomePhone, txtHuyetAp, txtID_NO,
                          cboIDPlace, txtLangMark, txtTimeID,
                          txtLastNameVN, txtMatPhai, txtMatTrai,
                          txtMobilePhone, txtNavAddress, txtNhomMau, txtOrgName,
                          txtOrgName2, txtBan, txtTo, txtPassNo, txtPassPlace, txtTimeID,
                          txtPerAddress, txtPerEmail, txtPhoiNguc, txtCareer,
                          txtPitCode, txtRangHamMat, txtTaiMuiHong, txtTim,
                          txtVienGanB, txtVisa, txtVisaPlace,
                          txtWorkEmail, txtWorkPermit, txtWorkPermitPlace,
                          txtContactPerson, txtContactPersonPhone, txtChucVuDoan,
                          rdBirthDate, rdContractExpireDate, rdContractEffectDate, rdIDDate,
                          rdJoinDate, rdNgayVaoDoan, rdPassDate, rdPassExpireDate,
                          rdVisaDate, rdVisaExpireDate, rdWorkPermitDate, rdWorPermitExpireDate,
                          txtCanNang, txtChieuCao,
                          cboAcademy, cboBank, cboBankBranch, cboFamilyStatus,
                          cboGender, cboLangLevel, cboWorkplace, cboInsRegion,
                          cboLanguage, cboLearningLevel, txtLoaiSucKhoe, cboMajor, cboNationlity, cboNative, cboNav_Province, cboPer_Province,
                          cboReligion, cboStaffRank, cboTitle, cboWorkStatus, cboEmpStatus,
                          cboPer_District, cboPer_Ward, cboNav_District, cboNav_Ward,
                          hidID, hidOrgID, hidDirectManager, hidLevelManager, chkDoanPhi)
            chkDoanPhi.Checked = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    Private Function Save(ByRef strEmpID As Decimal, Optional ByRef _err As String = "") As Boolean
        Dim result As Boolean
        Dim gID As Decimal
        Dim rep As New ProfileBusinessRepository
        Dim EmpCV As New EmployeeCVDTO
        Dim EmpEdu As New EmployeeEduDTO
        Dim EmpHealth As EmployeeHealthDTO
        Dim _binaryImage As Byte()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Select Case CurrentState
                Case STATE_NEW
                    If Not rep.ValidateEmployee("EXIST_WORK_EMAIL", "", txtWorkEmail.Text) Then
                        ShowMessage("Địa chỉ email đã tồn tại", NotifyType.Warning)
                        Exit Function
                    End If
                Case STATE_EDIT
                    If Not rep.ValidateEmployee("EXIST_WORK_EMAIL", EmployeeInfo.EMPLOYEE_CODE, txtWorkEmail.Text) Then
                        ShowMessage("Địa chỉ email đã tồn tại", NotifyType.Warning)
                        Exit Function
                    End If
            End Select

            'Lấy ảnh của nhân viên

            If EmployeeInfo Is Nothing Then
                EmployeeInfo = New EmployeeDTO
            End If
            If hidID.Value.Trim = "" Then
                EmployeeInfo.ID = 0
            Else
                EmployeeInfo.ID = Decimal.Parse(hidID.Value)
            End If

            If hidDirectManager.Value <> "" Then
                EmployeeInfo.DIRECT_MANAGER = hidDirectManager.Value
            End If
            If hidLevelManager.Value <> "" Then
                EmployeeInfo.LEVEL_MANAGER = hidLevelManager.Value
            End If
            EmployeeInfo.EMPLOYEE_NAME_OTHER = rtOtherName.Text
            EmployeeInfo.EMPLOYEE_CODE_OLD = rtEmpCode_OLD.Text
            EmployeeInfo.BOOKNO = rtBookNo.Text
            EmployeeInfo.EMPLOYEE_CODE = txtEmpCODE.Text.Trim
            EmployeeInfo.FIRST_NAME_VN = txtFirstNameVN.Text.Trim
            EmployeeInfo.FULLNAME_VN = txtFirstNameVN.Text.Trim & " " & txtLastNameVN.Text.Trim
            EmployeeInfo.LAST_NAME_VN = txtLastNameVN.Text.Trim
            EmployeeInfo.ORG_ID = hidOrgID.Value
            If txtTo.Text <> "" Then
                EmployeeInfo.ORG_ID = txtTo.ToolTip
            ElseIf txtBan.Text <> "" Then
                EmployeeInfo.ORG_ID = txtBan.ToolTip
            ElseIf txtOrgName.Text <> "" Then
                EmployeeInfo.ORG_ID = txtOrgName.ToolTip
            End If
            'EmployeeInfo.ORG_ID = txtOrgName.ToolTip
            EmployeeInfo.TITLE_ID = If(cboTitle.Text.Equals(""), Nothing, cboTitle.SelectedValue)
            EmployeeInfo.ITIME_ID = txtTimeID.Text
            EmployeeInfo.JOIN_DATE = rdJoinDate.SelectedDate

            'EmployeeInfo.JOIN_DATE_STATE = rdJoinDateState.SelectedDate

            If cboStaffRank.SelectedValue <> "" Then
                EmployeeInfo.STAFF_RANK_ID = cboStaffRank.SelectedValue
            End If
            If cboWorkStatus.SelectedValue <> "" Then
                EmployeeInfo.WORK_STATUS = cboWorkStatus.SelectedValue
            End If
            If cboEmpStatus.SelectedValue <> "" Then
                EmployeeInfo.EMP_STATUS = cboEmpStatus.SelectedValue
            End If
            EmpCV = New EmployeeCVDTO
            If cboWorkplace.SelectedValue <> "" Then
                EmpCV.WORKPLACE_ID = Decimal.Parse(cboWorkplace.SelectedValue)
            End If
            If cboInsRegion.SelectedValue <> "" Then
                EmpCV.INS_REGION_ID = Decimal.Parse(cboInsRegion.SelectedValue)
            End If
            If cboGender.SelectedValue <> "" Then
                EmpCV.GENDER = Decimal.Parse(cboGender.SelectedValue)
            End If
            If rdBirthDate.SelectedDate IsNot Nothing Then
                EmpCV.BIRTH_DATE = rdBirthDate.SelectedDate
            End If
            If cboFamilyStatus.SelectedValue <> "" Then
                EmpCV.MARITAL_STATUS = Decimal.Parse(cboFamilyStatus.SelectedValue)
            End If
            If cboReligion.SelectedValue <> "" Then
                EmpCV.RELIGION = Decimal.Parse(cboReligion.SelectedValue)
            End If

            If IsNumeric(cbPROVINCEEMP_ID.SelectedValue) Then
                EmpCV.PROVINCEEMP_ID = cbPROVINCEEMP_ID.SelectedValue
            End If
            If IsNumeric(cbDISTRICTEMP_ID.SelectedValue) Then
                EmpCV.DISTRICTEMP_ID = cbDISTRICTEMP_ID.SelectedValue
            End If
            If IsNumeric(cbWARDEMP_ID.SelectedValue) Then
                EmpCV.WARDEMP_ID = cbWARDEMP_ID.SelectedValue
            End If
            'If cboBIRTH_PLACE.SelectedValue <> "" Then
            '    EmpCV.BIRTH_PLACE = Decimal.Parse(cboBIRTH_PLACE.SelectedValue)
            'End If
            If cboNative.SelectedValue <> "" Then
                EmpCV.NATIVE = Decimal.Parse(cboNative.SelectedValue)
            End If
            If cboNationlity.SelectedValue <> "" Then
                EmpCV.NATIONALITY = Decimal.Parse(cboNationlity.SelectedValue)
            End If
            EmpCV.NAV_ADDRESS = txtNavAddress.Text.Trim()
            If cboNav_Province.SelectedValue <> "" Then
                EmpCV.NAV_PROVINCE = Decimal.Parse(cboNav_Province.SelectedValue)
            End If
            If cboNav_District.SelectedValue <> "" Then
                EmpCV.NAV_DISTRICT = Decimal.Parse(cboNav_District.SelectedValue)
            End If
            If cboNav_Ward.SelectedValue <> "" Then
                EmpCV.NAV_WARD = Decimal.Parse(cboNav_Ward.SelectedValue)
            End If
            EmpCV.PER_ADDRESS = txtPerAddress.Text.Trim()
            If cboPer_Province.SelectedValue <> "" Then
                EmpCV.PER_PROVINCE = Decimal.Parse(cboPer_Province.SelectedValue)
            End If
            If cboPer_District.SelectedValue <> "" Then
                EmpCV.PER_DISTRICT = Decimal.Parse(cboPer_District.SelectedValue)
            End If
            If cboPer_Ward.SelectedValue <> "" Then
                EmpCV.PER_WARD = Decimal.Parse(cboPer_Ward.SelectedValue)
            End If
            ' SĐT
            EmpCV.HOME_PHONE = txtHomePhone.Text.Trim()
            EmpCV.MOBILE_PHONE = txtMobilePhone.Text.Trim()
            ' CMND
            EmpCV.ID_NO = txtID_NO.Text.Trim()
            EmpCV.ID_DATE = rdIDDate.SelectedDate
            EmpCV.ID_PLACE = GetValueFromComboBox(cboIDPlace)
            'Hộ chiếu
            EmpCV.PASS_NO = txtPassNo.Text.Trim()
            EmpCV.PASS_DATE = rdPassDate.SelectedDate
            EmpCV.PASS_EXPIRE = rdPassExpireDate.SelectedDate
            EmpCV.PASS_PLACE = txtPassPlace.Text.Trim()
            'Visa
            EmpCV.VISA = txtVisa.Text.Trim()
            EmpCV.VISA_DATE = rdVisaDate.SelectedDate
            EmpCV.VISA_EXPIRE = rdVisaExpireDate.SelectedDate
            EmpCV.VISA_PLACE = txtVisaPlace.Text.Trim()
            'Giấy phép lao động
            EmpCV.WORK_PERMIT = txtWorkPermit.Text.Trim()
            EmpCV.WORK_PERMIT_DATE = rdWorkPermitDate.SelectedDate
            EmpCV.WORK_PERMIT_EXPIRE = rdWorPermitExpireDate.SelectedDate
            EmpCV.WORK_PERMIT_PLACE = txtWorkPermitPlace.Text.Trim()
            EmpCV.PIT_CODE = txtPitCode.Text.Trim()
            EmpCV.PER_EMAIL = txtPerEmail.Text.Trim()
            EmpCV.WORK_EMAIL = txtWorkEmail.Text.Trim()
            EmpCV.CAREER = txtCareer.Text.Trim()
            'Người liên hệ khi cần
            EmpCV.CONTACT_PER = txtContactPerson.Text.Trim()
            'Điện thoại người liên hệ
            EmpCV.CONTACT_PER_PHONE = txtContactPersonPhone.Text.Trim()

            ' EmpCV.DANG_PHI = chkDangPhi.Checked
            EmpCV.DOAN_PHI = chkDoanPhi.Checked
            EmpCV.IS_PAY_BANK = chkIs_pay_bank.Checked
            If cboBank.SelectedValue <> "" Then
                EmpCV.BANK_ID = Decimal.Parse(cboBank.SelectedValue)
            End If
            If cboBankBranch.SelectedValue <> "" Then
                EmpCV.BANK_BRANCH_ID = Decimal.Parse(cboBankBranch.SelectedValue)
            End If
            ' EmpCV.NGAY_VAO_DANG = rdNgayVaoDang.SelectedDate
            EmpCV.NGAY_VAO_DOAN = rdNgayVaoDoan.SelectedDate
            'EmpCV.CHUC_VU_DANG = txtChucVuDang.Text.Trim()
            EmpCV.CHUC_VU_DOAN = txtChucVuDoan.Text.Trim()
            ' EmpCV.NOI_VAO_DANG = txtNoiVaoDang.Text.Trim()
            EmpCV.NOI_VAO_DOAN = txtNoiVaoDoan.Text.Trim()
            EmpCV.BANK_NO = txtBankNo.Text.Trim()
            If IsNumeric(cbPROVINCENQ_ID.SelectedValue) Then
                EmpCV.PROVINCENQ_ID = cbPROVINCENQ_ID.SelectedValue
            End If
            EmpCV.OPPTION1 = rtOpption1.Text
            EmpCV.OPPTION2 = rtOpption2.Text
            EmpCV.OPPTION3 = rtOpption3.Text
            EmpCV.OPPTION4 = rtOpption4.Text
            EmpCV.OPPTION5 = rtOpption5.Text
            If IsDate(rdOpption6.SelectedDate) Then
                EmpCV.OPPTION6 = rdOpption6.SelectedDate
            End If
            If IsDate(rdOpption7.SelectedDate) Then
                EmpCV.OPPTION7 = rdOpption7.SelectedDate
            End If
            If IsDate(rdOpption8.SelectedDate) Then
                EmpCV.OPPTION8 = rdOpption8.SelectedDate
            End If
            If IsDate(rdOpption9.SelectedDate) Then
                EmpCV.OPPTION9 = rdOpption9.SelectedDate
            End If
            If IsDate(rdOpption10.SelectedDate) Then
                EmpCV.OPPTION10 = rdOpption10.SelectedDate
            End If

            EmpHealth = New EmployeeHealthDTO
            EmpHealth.CAN_NANG = txtCanNang.Text
            EmpHealth.CHIEU_CAO = txtChieuCao.Text
            EmpHealth.DA_HOA_LIEU = txtDaHoaLieu.Text.Trim()
            EmpHealth.GHI_CHU_SUC_KHOE = txtGhiChuSK.Text.Trim()
            EmpHealth.HUYET_AP = txtHuyetAp.Text.Trim()
            EmpHealth.MAT_PHAI = txtMatPhai.Text.Trim()
            EmpHealth.MAT_TRAI = txtMatTrai.Text.Trim()
            EmpHealth.NHOM_MAU = txtNhomMau.Text.Trim()
            EmpHealth.PHOI_NGUC = txtPhoiNguc.Text.Trim()
            EmpHealth.RANG_HAM_MAT = txtRangHamMat.Text.Trim()
            EmpHealth.TAI_MUI_HONG = txtTaiMuiHong.Text.Trim()
            EmpHealth.TIM = txtTim.Text.Trim()
            EmpHealth.VIEM_GAN_B = txtVienGanB.Text.Trim()
            EmpHealth.LOAI_SUC_KHOE = txtLoaiSucKhoe.Text

            EmpEdu = New EmployeeEduDTO
            If txtNamTN.Text <> "" Then
                EmpEdu.GRADUATION_YEAR = txtNamTN.Value
            End If

            If cboAcademy.SelectedValue <> "" Then
                EmpEdu.ACADEMY = cboAcademy.SelectedValue
            End If
            If cboLearningLevel.SelectedValue <> "" Then
                EmpEdu.LEARNING_LEVEL = cboLearningLevel.SelectedValue
            End If
            If cboLangLevel.SelectedValue <> "" Then
                EmpEdu.LANGUAGE_LEVEL = cboLangLevel.SelectedValue

            End If
            EmpEdu.LANGUAGE_MARK = txtLangMark.Text
            If cboLanguage.SelectedValue <> "" Then
                EmpEdu.LANGUAGE = cboLanguage.SelectedValue
            End If

            If cboMajor.SelectedValue <> "" Then
                EmpEdu.MAJOR = cboMajor.SelectedValue

            End If
            If ImageFile IsNot Nothing Then
                Dim bytes(ImageFile.ContentLength - 1) As Byte
                ImageFile.InputStream.Read(bytes, 0, ImageFile.ContentLength)
                _binaryImage = bytes
                EmpCV.IMAGE = ImageFile.GetExtension 'Lưu lại đuôi ảnh để hiển thị trong view ImageUpload
            Else
                EmpCV.IMAGE = ""
            End If
            EmployeeInfo.lstPaper = lstbPaper.CheckedItems.Select(Function(f) Decimal.Parse(f.Value)).ToList
            EmployeeInfo.lstPaperFiled = lstbPaperFiled.CheckedItems.Select(Function(f) Decimal.Parse(f.Value)).ToList
            If hidID.Value <> "" Then

                EmployeeInfo.ID = Decimal.Parse(hidID.Value)
                EmployeeInfo.IS_HISTORY = chkSaveHistory.Checked
                result = rep.ModifyEmployee(EmployeeInfo, gID, _binaryImage,
                                            EmpCV, _
                                            EmpEdu, _
                                            EmpHealth)

            Else
                result = rep.InsertEmployee(EmployeeInfo, gID, "", _binaryImage,
                                            EmpCV, _
                                            EmpEdu, _
                                            EmpHealth)

            End If
            strEmpID = gID
            rep.Dispose()
            Return result

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' create by: ChienNV
    ''' create date:16/10/2017
    ''' FILL DATA IN CONTROLS
    ''' txtOrgName2,txtOrgName,txtBan,txtTo
    ''' </summary>
    ''' <param name="orgid"></param>
    ''' <remarks></remarks>
    Private Sub FillDataInControls(ByVal orgid As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        If procedure Is Nothing Then
            procedure = New ProfileStoreProcedure()
        End If
        Using rep As New ProfileRepository
            Dim org = rep.GetOrganizationByID(orgid)
            If org IsNot Nothing Then
                SetValueComboBox(cboInsRegion, org.REGION_ID, Nothing)
            End If
        End Using
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Dim dtBranch As DataTable = procedure.GET_ALL_BRANCH_ORGLEVEL(orgid)
            If dtBranch IsNot Nothing AndAlso dtBranch.Rows.Count > 0 Then
                'Bo phan
                txtOrgName2.ToolTip = If(dtBranch.Select("CODE='" & "BP" & "'").Length > 0, dtBranch.Select("CODE='" & "BP" & "'")(0)("ID").ToString, "")
                txtOrgName2.Text = If(dtBranch.Select("CODE='" & "BP" & "'").Length > 0, dtBranch.Select("CODE='" & "BP" & "'")(0)("NAME").ToString, "")
                'PHONG
                txtOrgName.ToolTip = If(dtBranch.Select("CODE='" & "PHONG" & "'").Length > 0, dtBranch.Select("CODE='" & "PHONG" & "'")(0)("ID").ToString, "")
                txtOrgName.Text = If(dtBranch.Select("CODE='" & "PHONG" & "'").Length > 0, dtBranch.Select("CODE='" & "PHONG" & "'")(0)("NAME").ToString, "")
                'BAN
                txtBan.Text = If(dtBranch.Select("CODE='" & "BAN" & "'").Length > 0, dtBranch.Select("CODE='" & "BAN" & "'")(0)("NAME").ToString, "")
                txtBan.ToolTip = If(dtBranch.Select("CODE='" & "BAN" & "'").Length > 0, dtBranch.Select("CODE='" & "BAN" & "'")(0)("ID").ToString, "")
                'TO
                txtTo.Text = If(dtBranch.Select("CODE='" & "TO" & "'").Length > 0, dtBranch.Select("CODE='" & "TO" & "'")(0)("NAME").ToString, "")
                txtTo.ToolTip = If(dtBranch.Select("CODE='" & "TO" & "'").Length > 0, dtBranch.Select("CODE='" & "TO" & "'")(0)("ID").ToString, "")
                'Manager
                txtManager.ToolTip = If(dtBranch.Select("CODE='" & "BP" & "'").Length > 0, dtBranch.Select("CODE='" & "BP" & "'")(0)("REPRESENTATIVE_ID").ToString, "")
                txtManager.Text = If(dtBranch.Select("CODE='" & "BP" & "'").Length > 0, dtBranch.Select("CODE='" & "BP" & "'")(0)("REPRESENTATIVE_NAME").ToString, "")
            End If


            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        Finally
            procedure = Nothing
        End Try
    End Sub
#End Region
End Class
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

    Property vcf As DataSet
        Get
            Return PageViewState(Me.ID & "_vcf")
        End Get
        Set(ByVal value As DataSet)
            PageViewState(Me.ID & "_vcf") = value
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
        Dim repNS As New ProfileRepository
        Dim lstP, lstD, lstW As DataTable
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
                        If EmployeeInfo.OBJECTTIMEKEEPING IsNot Nothing Then
                            cboObject.SelectedValue = EmployeeInfo.OBJECTTIMEKEEPING
                            cboObject.Text = EmployeeInfo.OBJECTTIMEKEEPING_NAME
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
                        cboTitle.SelectedValue = EmployeeInfo.TITLE_ID
                        cboTitle.Text = EmployeeInfo.TITLE_NAME_VN
                        txtTitleGroup.Text = EmployeeInfo.TITLE_GROUP_NAME
                        rdJoinDate.SelectedDate = EmployeeInfo.JOIN_DATE
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
                            If IsDate(empCV.OPPTION8) Then
                                rdOpption8.SelectedDate = empCV.OPPTION8
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

                            'Get Province lst
                            lstP = repNS.GetProvinceList(False)
                            FillRadCombobox(cbPROVINCEEMP_ID, lstP, "NAME", "ID")

                            If IsNumeric(empCV.PROVINCEEMP_ID) Then
                                cbPROVINCEEMP_ID.SelectedValue = empCV.PROVINCEEMP_ID
                                'cbPROVINCEEMP_ID.Text = empCV.PROVINCEEMP_NAME
                                lstD = repNS.GetDistrictList(cbPROVINCEEMP_ID.SelectedValue, False)
                                FillRadCombobox(cbDISTRICTEMP_ID, lstD, "NAME", "ID")
                            End If
                            If IsNumeric(empCV.DISTRICTEMP_ID) Then
                                cbDISTRICTEMP_ID.SelectedValue = empCV.DISTRICTEMP_ID
                                'cbDISTRICTEMP_ID.Text = empCV.DISTRICTEMP_NAME
                                lstW = repNS.GetWardList(cbDISTRICTEMP_ID.SelectedValue, False)
                                FillRadCombobox(cbWARDEMP_ID, lstW, "NAME", "ID")
                            End If
                            If IsNumeric(empCV.WARDEMP_ID) Then
                                cbWARDEMP_ID.SelectedValue = empCV.WARDEMP_ID
                                'cbWARDEMP_ID.Text = empCV.WARDEMP_NAME
                            End If
                            '=========================================================
                            rtSkill.Text = empCV.SKILL
                            If IsNumeric(empCV.DANG) Then
                                ckDANG.Checked = CType(empCV.DANG, Boolean)
                            End If
                            If IsNumeric(empCV.CA) Then
                                ckCA.Checked = CType(empCV.CA, Boolean)
                            End If
                            If IsNumeric(empCV.BANTT) Then
                                ckBanTT_ND.Checked = CType(empCV.BANTT, Boolean)
                            End If
                            If IsNumeric(empCV.CONG_DOAN) Then
                                ckCONG_DOAN.Checked = CType(empCV.CONG_DOAN, Boolean)
                            End If
                            rtCV_BANTT.Text = empCV.CV_BANTT
                            If IsDate(empCV.NGAY_TG_BANTT) Then
                                rdNgay_TG_BanTT.SelectedDate = empCV.NGAY_TG_BANTT
                            End If
                            If IsNumeric(empCV.NU_CONG) Then
                                ckNU_CONG.Checked = CType(empCV.NU_CONG, Boolean)
                            End If
                            rtCV_Ban_Nu_Cong.Text = empCV.CV_BAN_NU_CONG
                            If IsDate(empCV.NGAY_TG_BAN_NU_CONG) Then
                                rdNgay_TG_Ban_Nu_Cong.SelectedDate = empCV.NGAY_TG_BAN_NU_CONG
                            End If
                            If IsDate(empCV.NGAY_NHAP_NGU_CA) Then
                                rdNgay_Nhap_Ngu_CA.SelectedDate = empCV.NGAY_NHAP_NGU_CA
                            End If
                            If IsDate(empCV.NGAY_XUAT_NGU_CA) Then
                                rdNgay_Xuat_Ngu_CA.SelectedDate = empCV.NGAY_XUAT_NGU_CA
                            End If
                            rtDV_Xuat_Ngu_CA.Text = empCV.DV_XUAT_NGU_CA
                            If IsNumeric(empCV.QD) Then
                                ckQD.Checked = CType(empCV.QD, Boolean)
                            End If
                            If IsDate(empCV.NGAY_NHAP_NGU_QD) Then
                                rdNgay_Nhap_Ngu_QD.SelectedDate = empCV.NGAY_NHAP_NGU_QD
                            End If
                            If IsDate(empCV.NGAY_XUAT_NGU_QD) Then
                                rdNgay_Xuat_Ngu_QD.SelectedDate = empCV.NGAY_XUAT_NGU_QD
                            End If
                            rtDV_Xuat_Ngu_QD.Text = empCV.DV_XUAT_NGU_QD
                            If IsNumeric(empCV.THUONG_BINH) Then
                                ckThuong_Binh.Checked = CType(empCV.THUONG_BINH, Boolean)
                            End If
                            If IsNumeric(empCV.HANG_THUONG_BINH) Then
                                cbHang_Thuong_Binh.SelectedValue = empCV.HANG_THUONG_BINH
                            End If
                            If IsNumeric(empCV.GD_CHINH_SACH) Then
                                cbGD_Chinh_Sach.SelectedValue = empCV.GD_CHINH_SACH
                            End If

                            If IsDate(empCV.NGAY_VAO_DANG_DB) Then
                                rdNGAY_VAO_DANG_DB.SelectedDate = empCV.NGAY_VAO_DANG_DB
                            End If
                            rtCHUC_VU_DANG.Text = empCV.CHUC_VU_DANG
                            If IsDate(empCV.NGAY_VAO_DANG) Then
                                rdNGAY_VAO_DANG.SelectedDate = empCV.NGAY_VAO_DANG
                            End If
                            rtCHUC_VU_DOAN.Text = empCV.CHUC_VU_DOAN
                            '===============================================
                            rtWorkplace.Text = empCV.WORKPLACE_NAME
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
                            ckDOAN_PHI.Checked = empCV.DOAN_PHI
                            If IsDate(empCV.NGAY_VAO_DOAN) Then
                                rdNGAY_VAO_DOAN.SelectedDate = empCV.NGAY_VAO_DOAN
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
                            '============================================================
                            If empEdu.LANGUAGE_LEVEL2 IsNot Nothing Then
                                cboLangLevel2.SelectedValue = empEdu.LANGUAGE_LEVEL2
                                cboLangLevel2.Text = empEdu.LANGUAGE_LEVEL_NAME2
                            End If
                            txtLangMark2.Text = empEdu.LANGUAGE_MARK2
                            If empEdu.LANGUAGE2 IsNot Nothing Then
                                cboLanguage2.SelectedValue = empEdu.LANGUAGE2
                            End If
                            '============================================================

                            If empEdu.MAJOR IsNot Nothing Then
                                cboMajor.SelectedValue = empEdu.MAJOR
                                cboMajor.Text = empEdu.MAJOR_NAME
                            End If
                            If IsNumeric(empEdu.QLNN) Then
                                cbQLNN.SelectedValue = empEdu.QLNN
                            End If
                            If IsNumeric(empEdu.LLCT) Then
                                cbLLCT.SelectedValue = empEdu.LLCT
                            End If
                            If IsNumeric(empEdu.TDTH) Then
                                cbTDTH.SelectedValue = empEdu.TDTH
                            End If
                            rtDiem_XL_TH.Text = empEdu.DIEM_XLTH
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
                            rtTTSucKhoe.Text = empHealth.TTSUCKHOE
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
            If Not IsPostBack Then
                'ViewConfig(DetailPane)
                vcf = New DataSet
                Using rep = New CommonRepository
                    vcf.ReadXml(New IO.StringReader(rep.GetConfigView(Me.ID).Rows(0)("config_data").ToString()))
                End Using
                If vcf IsNot Nothing AndAlso vcf.Tables("control") IsNot Nothing Then
                    Dim dtCtrl As DataTable = vcf.Tables("control")
                    For Each ctrs As Control In rtabProfileInfo.Controls
                        Dim row As DataRow
                        Try
                            row = dtCtrl.Select("Ctl_ID ='" + ctrs.ID + "'")(0)
                        Catch ex As Exception
                            Continue For
                        End Try
                        If row IsNot Nothing Then
                            ctrs.Visible = If(IsDBNull(row("Is_Visible")), False, CBool(row("Is_Visible")))
                            Try
                                Dim validator As BaseValidator = rtabProfileInfo.FindControl(row.Field(Of String)("Validator_ID"))
                                Dim labelCtr As Label = rtabProfileInfo.FindControl(row.Field(Of String)("Label_ID").Trim())
                                If labelCtr IsNot Nothing Then
                                    labelCtr.Visible = ctrs.Visible
                                    labelCtr.Text = If(IsDBNull(row("Label_text")), labelCtr.Text, Translate(row("Label_text")))
                                End If
                                If validator IsNot Nothing Then
                                    validator.Enabled = If(IsDBNull(row("Is_Validator")), True, CBool(row("Is_Validator")))
                                    validator.ErrorMessage = If(IsDBNull(row("ErrorMessage")), validator.ErrorMessage, row("ErrorMessage"))
                                    validator.ToolTip = If(IsDBNull(row("ErrorToolTip")), validator.ToolTip, row("ErrorToolTip"))
                                End If
                            Catch ex As Exception
                                Continue For
                            End Try
                        End If
                    Next

                    '========================================================================================================
                    For Each ctrs As Control In rpvEmpInfo.Controls
                        Dim row As DataRow
                        Try
                            row = dtCtrl.Select("Ctl_ID ='" + ctrs.ID + "'")(0)
                        Catch ex As Exception
                            Continue For
                        End Try
                        If row IsNot Nothing Then
                            ctrs.Visible = If(IsDBNull(row("Is_Visible")), False, CBool(row("Is_Visible")))
                            Try
                                Dim validator As BaseValidator = rpvEmpInfo.FindControl(row.Field(Of String)("Validator_ID"))
                                Dim labelCtr As Label = rpvEmpInfo.FindControl(row.Field(Of String)("Label_ID").Trim())
                                If labelCtr IsNot Nothing Then
                                    labelCtr.Visible = ctrs.Visible
                                    labelCtr.Text = If(IsDBNull(row("Label_text")), labelCtr.Text, Translate(row("Label_text")))
                                End If
                                If validator IsNot Nothing Then
                                    validator.Enabled = If(IsDBNull(row("Is_Validator")), True, CBool(row("Is_Validator")))
                                    validator.ErrorMessage = If(IsDBNull(row("ErrorMessage")), validator.ErrorMessage, row("ErrorMessage"))
                                    validator.ToolTip = If(IsDBNull(row("ErrorToolTip")), validator.ToolTip, row("ErrorToolTip"))
                                End If
                            Catch ex As Exception
                                Continue For
                            End Try
                        End If
                    Next
                    '========================================================================================================
                    For Each ctrs As Control In rpvEmpPaper.Controls
                        Dim row As DataRow
                        Try
                            row = dtCtrl.Select("Ctl_ID ='" + ctrs.ID + "'")(0)
                        Catch ex As Exception
                            Continue For
                        End Try
                        If row IsNot Nothing Then
                            ctrs.Visible = If(IsDBNull(row("Is_Visible")), False, CBool(row("Is_Visible")))
                            Try
                                Dim validator As BaseValidator = rpvEmpPaper.FindControl(row.Field(Of String)("Validator_ID"))
                                Dim labelCtr As Label = rpvEmpPaper.FindControl(row.Field(Of String)("Label_ID").Trim())
                                If labelCtr IsNot Nothing Then
                                    labelCtr.Visible = ctrs.Visible
                                    labelCtr.Text = If(IsDBNull(row("Label_text")), labelCtr.Text, Translate(row("Label_text")))
                                End If
                                If validator IsNot Nothing Then
                                    validator.Enabled = If(IsDBNull(row("Is_Validator")), True, CBool(row("Is_Validator")))
                                    validator.ErrorMessage = If(IsDBNull(row("ErrorMessage")), validator.ErrorMessage, row("ErrorMessage"))
                                    validator.ToolTip = If(IsDBNull(row("ErrorToolTip")), validator.ToolTip, row("ErrorToolTip"))
                                End If
                            Catch ex As Exception
                                Continue For
                            End Try
                        End If
                    Next
                    '========================================================================================================
                    For i As Integer = 0 To RadPanelBar1.Items.Count - 1
                        For Each ctrs In RadPanelBar1.Items(i).Controls
                            Dim row As DataRow
                            Try
                                row = dtCtrl.Select("Ctl_ID ='" + ctrs.ID + "'")(0)
                            Catch ex As Exception
                                Continue For
                            End Try
                            If row IsNot Nothing Then
                                ctrs.Visible = If(IsDBNull(row("Is_Visible")), False, CBool(row("Is_Visible")))
                                Try
                                    Dim validator As BaseValidator = RadPanelBar1.Items(i).FindControl(row.Field(Of String)("Validator_ID"))
                                    Dim labelCtr As Label = RadPanelBar1.Items(i).FindControl(row.Field(Of String)("Label_ID").Trim())
                                    If labelCtr IsNot Nothing Then
                                        labelCtr.Visible = ctrs.Visible
                                        labelCtr.Text = If(IsDBNull(row("Label_text")), labelCtr.Text, Translate(row("Label_text")))
                                    End If
                                    If validator IsNot Nothing Then
                                        validator.Enabled = If(IsDBNull(row("Is_Validator")), True, CBool(row("Is_Validator")))
                                        validator.ErrorMessage = If(IsDBNull(row("ErrorMessage")), validator.ErrorMessage, row("ErrorMessage"))
                                        validator.ToolTip = If(IsDBNull(row("ErrorToolTip")), validator.ToolTip, row("ErrorToolTip"))
                                    End If
                                Catch ex As Exception
                                    Continue For
                                End Try
                            End If
                        Next
                    Next
                End If
            End If
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
            Using rep As New ProfileRepository
                dtData = rep.GetOtherList("HU_PAPER")
                dtPlace = rep.GetProvinceList(True)
                dtLanguageleve = rep.GetOtherList("LANGUAGE_LEVEL", True)
                dtLanguage = rep.GetOtherList("RC_LANGUAGE_LEVEL", True)
                FillRadCombobox(cboIDPlace, dtPlace, "NAME", "ID")
                FillRadCombobox(cbPROVINCENQ_ID, dtPlace, "NAME", "ID")
                FillRadCombobox(cboLangLevel, dtLanguageleve, "NAME", "ID")
                FillRadCombobox(cboLangLevel2, dtLanguageleve, "NAME", "ID")
                FillRadCombobox(cboLanguage, dtLanguage, "NAME", "ID")
                FillRadCombobox(cboLanguage2, dtLanguage, "NAME", "ID")
                FillCheckBoxList(lstbPaper, dtData, "NAME", "ID")
                FillCheckBoxList(lstbPaperFiled, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("HANG_TB")
                FillRadCombobox(cbHang_Thuong_Binh, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("HCGD")
                FillRadCombobox(cbGD_Chinh_Sach, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("RC_COMPUTER_LEVEL")
                FillRadCombobox(cbTDTH, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("QLNN")
                FillRadCombobox(cbQLNN, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("LLCT")
                FillRadCombobox(cbLLCT, dtData, "NAME", "ID")
            End Using
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
                    EnableControlAll(False, cboWorkStatus, txtEmpCODE, cboEmpStatus)
                    EnableControlAll(True, rtCHUC_VU_DANG, rdNGAY_VAO_DANG_DB, rdNGAY_VAO_DANG)
                    EnableControlAll(True, ckDOAN_PHI, rtCHUC_VU_DOAN, rdNGAY_VAO_DOAN)
                    EnableControlAll(True, rtCV_BANTT, rdNgay_TG_BanTT)
                    EnableControlAll(True, rtCV_Ban_Nu_Cong, rdNgay_TG_Ban_Nu_Cong)
                    EnableControlAll(True, rdNgay_Nhap_Ngu_QD, rdNgay_Xuat_Ngu_QD, rtDV_Xuat_Ngu_QD)
                    EnableControlAll(True, cbHang_Thuong_Binh, cbGD_Chinh_Sach)
                    EnableControlAll(True, lstbPaper, lstbPaperFiled,
                                        txtBankNo, chkSaveHistory, ckBanTT_ND,
                                       txtDaHoaLieu, txtTimeID, rtDiem_XL_TH,
                                       txtFirstNameVN, txtGhiChuSK, txtNamTN,
                                       txtHomePhone, txtHuyetAp, txtID_NO, chkIs_pay_bank,
                                       cboIDPlace, txtLangMark, txtLangMark2,
                                       txtLastNameVN, txtMatPhai, txtMatTrai,
                                       txtMobilePhone, txtNavAddress, txtNhomMau,
                                        txtPassNo, txtPassPlace, cboObject,
                                       txtPerAddress, txtPerEmail, txtPhoiNguc, txtCareer,
                                       txtPitCode, txtRangHamMat, txtTaiMuiHong, txtTim,
                                       txtVienGanB, txtVisa, txtVisaPlace,
                                       txtWorkEmail, txtWorkPermit, txtWorkPermitPlace,
                                       txtContactPerson, txtContactPersonPhone, txtChucVuDoan,
                                       rdBirthDate, rdContractExpireDate, rdContractEffectDate, rdIDDate,
                                       rdNgayVaoDoan, rdPassDate, rdPassExpireDate,
                                       rdVisaDate, rdVisaExpireDate, rdWorkPermitDate, rdWorPermitExpireDate,
                                       txtCanNang, txtChieuCao,
                                       cboAcademy, cboBank, cboBankBranch, cboFamilyStatus, rtWorkplace, cboInsRegion,
                                       cboGender, cboLangLevel, cboLangLevel2, cboLanguage, cboLanguage2, cboLearningLevel, txtLoaiSucKhoe,
                                       cboMajor, cboNationlity, cboNative, cboNav_Province, cboPer_Province,
                                       cboReligion, cboStaffRank, cboTitle,
                                       cboPer_District, cboPer_Ward, cboNav_District, cboNav_Ward, cbPROVINCEEMP_ID, cbDISTRICTEMP_ID, cbWARDEMP_ID, cbPROVINCENQ_ID,
                                       hidID, hidOrgID, hidDirectManager, hidLevelManager,
                                       chkDoanPhi, btnFindDirect, btnFindOrg,
                                       rtOpption1, rtOpption2, rtOpption3, rtOpption4, rtOpption5,
                                       rdOpption6, rdOpption7, rdOpption8, rdOpption9, rdOpption10,
                                       rtEmpCode_OLD, rtBookNo, rtOtherName, cbGD_Chinh_Sach, cbHang_Thuong_Binh, ckThuong_Binh,
                                        ckQD, rtDV_Xuat_Ngu_CA, rdNgay_Xuat_Ngu_CA, rdNgay_Nhap_Ngu_CA,
                                       ckNU_CONG, ckCONG_DOAN, ckCA, ckDANG, rtSkill,
                                       cbQLNN, cbLLCT, cbTDTH, rtTTSucKhoe)
                    If Not Me.AllowCreate Then
                        txtFirstNameVN.ReadOnly = True
                        txtLastNameVN.ReadOnly = True
                        txtTimeID.ReadOnly = True
                    End If

                Case CommonMessage.STATE_EDIT
                    rtIdTitleConcurrent.Visible = True
                    RadPane4.Visible = True
                    EnableControlAll(False, cboWorkStatus, txtEmpCODE, cboEmpStatus)
                    EnableControlAll(True, rtCHUC_VU_DANG, rdNGAY_VAO_DANG_DB, rdNGAY_VAO_DANG)
                    EnableControlAll(True, ckDOAN_PHI, rtCHUC_VU_DOAN, rdNGAY_VAO_DOAN)
                    EnableControlAll(True, rtCV_BANTT, rdNgay_TG_BanTT)
                    EnableControlAll(True, rtCV_Ban_Nu_Cong, rdNgay_TG_Ban_Nu_Cong)
                    EnableControlAll(True, rdNgay_Nhap_Ngu_QD, rdNgay_Xuat_Ngu_QD, rtDV_Xuat_Ngu_QD)
                    EnableControlAll(True, cbHang_Thuong_Binh, cbGD_Chinh_Sach)
                    EnableControlAll(True, lstbPaper, lstbPaperFiled, chkSaveHistory,
                                        txtBankNo, ckBanTT_ND, rtDiem_XL_TH,
                                       txtDaHoaLieu, txtNamTN,
                                       txtFirstNameVN, txtGhiChuSK, chkIs_pay_bank,
                                       txtHomePhone, txtHuyetAp, txtID_NO,
                                       cboIDPlace, txtLangMark, txtLangMark2, txtTimeID,
                                       txtLastNameVN, txtMatPhai, txtMatTrai,
                                       txtMobilePhone, txtNavAddress, txtNhomMau,
                                        txtPassNo, txtPassPlace, cboObject,
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
                                       cboGender, cboLangLevel, cboLangLevel2, rtWorkplace, cboInsRegion,
                                       cboLanguage, cboLanguage2, cboLearningLevel, txtLoaiSucKhoe,
                                       cboMajor, cboNationlity, cboNative, cboNav_Province, cboPer_Province,
                                       cboReligion, cboStaffRank, cboTitle,
                                       cboPer_District, cboPer_Ward, cboNav_District, cboNav_Ward,
                                       hidID, hidOrgID, hidDirectManager, hidLevelManager,
                                        chkDoanPhi, cbPROVINCEEMP_ID, cbDISTRICTEMP_ID, cbWARDEMP_ID, cbPROVINCENQ_ID,
                                       btnFindDirect, btnFindOrg,
                                       rtOpption1, rtOpption2, rtOpption3, rtOpption4, rtOpption5,
                                       rdOpption6, rdOpption7, rdOpption8, rdOpption9, rdOpption10,
                                       rtEmpCode_OLD, rtBookNo, rtOtherName,
                                       ckThuong_Binh,
                                        ckQD, rtDV_Xuat_Ngu_CA, rdNgay_Xuat_Ngu_CA, rdNgay_Nhap_Ngu_CA,
                                       ckNU_CONG, ckCONG_DOAN, ckCA, ckDANG, rtSkill,
                                       cbQLNN, cbLLCT, cbTDTH, rtTTSucKhoe)
                    If Not Me.AllowModify Then
                        txtFirstNameVN.ReadOnly = True
                        txtLastNameVN.ReadOnly = True
                        txtTimeID.ReadOnly = True
                    End If
                    If EmployeeInfo.LAST_WORKING_ID IsNot Nothing Then
                        EnableControlAll(False, cboTitle, cboStaffRank, cboObject, btnFindOrg)
                    End If
                Case Else
                    EnableControlAll(False, lstbPaper, lstbPaperFiled, cboWorkStatus, cboEmpStatus, txtEmpCODE,
                                       txtBankNo, chkSaveHistory, ckDOAN_PHI, rtCHUC_VU_DANG, rdNGAY_VAO_DOAN,
                                       txtDaHoaLieu, rdNGAY_VAO_DANG_DB, rdNGAY_VAO_DANG, ckBanTT_ND, txtNamTN,
                                       txtFirstNameVN, txtGhiChuSK, rtCHUC_VU_DOAN, rtDiem_XL_TH,
                                       txtHomePhone, txtHuyetAp, txtID_NO, chkIs_pay_bank,
                                       cboIDPlace, txtLangMark, txtLangMark2, txtTimeID,
                                       txtLastNameVN, txtMatPhai, txtMatTrai,
                                       txtMobilePhone, txtNavAddress, txtNhomMau,
                                        txtPassNo, txtPassPlace, cboObject,
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
                                       cboGender, cboLangLevel, cboLangLevel2, rtWorkplace, cboInsRegion,
                                       cboLanguage, cboLanguage2, cboLearningLevel, txtLoaiSucKhoe,
                                       cboMajor, cboNationlity, cboNative, cboNav_Province, cboPer_Province,
                                       cboReligion, cboStaffRank, cboTitle,
                                       cboPer_District, cboPer_Ward, cboNav_District, cboNav_Ward,
                                       hidID, hidOrgID, hidDirectManager, hidLevelManager,
                                       chkDoanPhi, cbPROVINCEEMP_ID, cbDISTRICTEMP_ID, cbWARDEMP_ID, cbPROVINCENQ_ID,
                                       btnFindDirect, btnFindOrg,
                                       rtOpption1, rtOpption2, rtOpption3, rtOpption4, rtOpption5,
                                       rdOpption6, rdOpption7, rdOpption8, rdOpption9, rdOpption10,
                                       rtEmpCode_OLD, rtBookNo, rtOtherName,
                                       cbGD_Chinh_Sach, cbHang_Thuong_Binh, ckThuong_Binh,
                                       rtDV_Xuat_Ngu_QD, rdNgay_Xuat_Ngu_QD, rdNgay_Nhap_Ngu_QD, ckQD, rtDV_Xuat_Ngu_CA, rdNgay_Xuat_Ngu_CA, rdNgay_Nhap_Ngu_CA,
                                       rdNgay_TG_Ban_Nu_Cong, rtCV_Ban_Nu_Cong, ckNU_CONG, rdNgay_TG_BanTT, rtCV_BANTT, ckCONG_DOAN, ckCA, ckDANG, rtSkill,
                                       cbQLNN, cbLLCT, cbTDTH, rtTTSucKhoe)

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
                                Dim message As String = String.Empty
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
                                    message = "Số CMND đã tồn tại."
                                End If
                                If Not checkBank_No Then
                                    message = If(message <> "", message + " Số tài khoản: " + txtBankNo.Text + " đã tồn tại.", "Số tài khoản: " + txtBankNo.Text + " đã tồn tại.")
                                End If
                                If hidDirectManager.Value = "" Then
                                    message = If(message <> "", message + " Nhân viên này không có Quản lý trực tiếp.", "Nhân viên này không có Quản lý trực tiếp.")
                                End If
                                If message <> "" Then
                                    message += " Bạn có muốn lưu không?"
                                    ctrlMessageBox.MessageText = Translate(message)
                                    ctrlMessageBox.ActionName = "WARNING"
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
                                'If Not checkID_NO Then
                                '    _filter.ID_NO = txtID_NO.Text

                                '    dtData = rep.GetListEmployeePaging(_filter, _param).ToTable()
                                '    If dtData IsNot Nothing Then
                                '        For index = 0 To dtData.Rows.Count - 1
                                '            lstEmpID = lstEmpID + "," + dtData.Rows(index)("EMPLOYEE_CODE").ToString
                                '        Next
                                '    End If

                                '    If lstEmpID <> "" Then
                                '        lstEmpID = lstEmpID.Substring(1, lstEmpID.Length - 1)
                                '    End If


                                '    ctrlMessageBox.MessageText = Translate("Số CMND đã tồn tại cho nhân viên :" + lstEmpID + " Bạn muốn lưu không?")
                                '    ctrlMessageBox.ActionName = "ACTION_BANK_NO"
                                '    ctrlMessageBox.DataBind()
                                '    ctrlMessageBox.Show()

                                'ElseIf Not checkBank_No Then
                                '    ctrlMessageBox.MessageText = Translate("Số tài khoản: " + txtBankNo.Text + " đã tồn tại Bạn muốn lưu không?")
                                '    ctrlMessageBox.ActionName = CommonMessage.ACTION_ID_NO
                                '    ctrlMessageBox.DataBind()
                                '    ctrlMessageBox.Show()
                                'Else
                                '    If hidDirectManager.Value <> "" Then
                                '        If Save(strEmpID, _err) Then
                                '            Page.Response.Redirect("Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=" & strEmpID & "&state=Normal&message=success", False)
                                '            Exit Sub
                                '        Else
                                '            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                                '            Exit Sub
                                '        End If
                                '    Else
                                '        ctrlMessageBox.MessageText = Translate("Bạn chắc chắn nhân viên này không có Quản lý trực tiếp ?")
                                '        ctrlMessageBox.MessageTitle = Translate("Thông báo")
                                '        ctrlMessageBox.ActionName = "CHECK_DIRECTMANAGER"
                                '        ctrlMessageBox.DataBind()
                                '        ctrlMessageBox.Show()
                                '    End If
                                'End If
                            Case STATE_EDIT
                                Dim message As String = String.Empty
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
                                    message = "Số CMND đã tồn tại."
                                End If
                                If hidDirectManager.Value = "" Then
                                    message = If(message <> "", message + " Nhân viên này không có Quản lý trực tiếp.", "Nhân viên này không có Quản lý trực tiếp.")
                                End If
                                If message <> "" Then
                                    message += " Bạn có muốn lưu không?"
                                    ctrlMessageBox.MessageText = Translate(message)
                                    ctrlMessageBox.ActionName = "WARNING"
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
                                'If Not checkID_NO Then
                                '    _filter.ID_NO = txtID_NO.Text

                                '    dtData = rep.GetListEmployeePaging(_filter, _param).ToTable()
                                '    If dtData IsNot Nothing Then
                                '        For index = 0 To dtData.Rows.Count - 1
                                '            If dtData.Rows(index)("EMPLOYEE_CODE").ToString <> txtEmpCODE.Text Then
                                '                lstEmpID = lstEmpID + "," + dtData.Rows(index)("EMPLOYEE_CODE").ToString
                                '            End If
                                '        Next
                                '    End If

                                '    If lstEmpID <> "" Then
                                '        lstEmpID = lstEmpID.Substring(1, lstEmpID.Length - 1)
                                '    End If

                                '    ctrlMessageBox.MessageText = Translate("Số CMND đã tồn tại cho nhân viên :" + lstEmpID + ". Bạn muốn lưu không?")
                                '    ctrlMessageBox.ActionName = CommonMessage.ACTION_ID_NO
                                '    ctrlMessageBox.DataBind()
                                '    ctrlMessageBox.Show()
                                'Else
                                '    If hidDirectManager.Value <> "" Then
                                '        If Save(strEmpID, _err) Then
                                '            CurrentState = CommonMessage.STATE_NORMAL
                                '            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                '        Else
                                '            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                                '        End If
                                '    Else
                                '        ctrlMessageBox.MessageText = Translate("Bạn chắc chắn nhân viên này không có Quản lý trực tiếp ?")
                                '        ctrlMessageBox.MessageTitle = Translate("Thông báo")
                                '        ctrlMessageBox.ActionName = "CHECK_DIRECTMANAGER"
                                '        ctrlMessageBox.DataBind()
                                '        ctrlMessageBox.Show()
                                '    End If

                                'End If
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

            If e.ActionName = "WARNING" And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim _err As String = ""
                If Save(EmployeeID, _err) Then
                    CurrentState = CommonMessage.STATE_NORMAL
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                Else
                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL) & vbNewLine & Translate(_err), Utilities.NotifyType.Error)
                End If
                UpdateControlState()
            Else
                Exit Sub
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub cboCommon_ItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs) _
    Handles cboAcademy.ItemsRequested, cboBank.ItemsRequested, cboBankBranch.ItemsRequested,
        cboFamilyStatus.ItemsRequested, cboGender.ItemsRequested, cboLangLevel.ItemsRequested, cboLangLevel2.ItemsRequested, cboLearningLevel.ItemsRequested, cboMajor.ItemsRequested,
         cboNationlity.ItemsRequested, cboNative.ItemsRequested, cboNav_Province.ItemsRequested, cboObject.ItemsRequested,
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
                    Case cboObject.ID
                        dtData = rep.GetOtherList("OBJECT_ATTENDANCE", True)
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
                    Case cboNav_Province.ID, cboPer_Province.ID, cboIDPlace.ID, cbPROVINCEEMP_ID.ID, cbPROVINCENQ_ID.ID
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
                        dtData = rep.GetOtherList("WORK_STATUS", True)
                    Case cboLangLevel.ID
                        dtData = rep.GetOtherList("LEARNING_LEVEL", True)
                    Case cboLangLevel2.ID
                        dtData = rep.GetOtherList("LEARNING_LEVEL", True)
                    Case cboInsRegion.ID
                        dtData = rep.GetInsRegionList(True)
                End Select

                If sText <> "" Then
                    Dim dtExist = (From p In dtData
                                   Where p("NAME") IsNot DBNull.Value AndAlso
                                  p("NAME").ToString.ToUpper = sText.ToUpper)

                    'If dtExist.Count = 0 Then
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
                    sender.Items.Clear()
                    For i As Integer = itemOffset To endOffset - 1
                        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                        Select Case sender.ID
                            Case cboTitle.ID
                                radItem.Attributes("GROUP_NAME") = dtData.Rows(i)("GROUP_NAME").ToString()
                        End Select
                        sender.Items.Add(radItem)
                    Next
                    'Else

                    '    Dim itemOffset As Integer = e.NumberOfItems
                    '    Dim endOffset As Integer = dtData.Rows.Count
                    '    e.EndOfItems = True
                    '    sender.Items.Clear()
                    '    For i As Integer = itemOffset To endOffset - 1
                    '        Dim radItem As RadComboBoxItem = New RadComboBoxItem(dtData.Rows(i)("NAME").ToString(), dtData.Rows(i)("ID").ToString())
                    '        Select Case sender.ID
                    '            Case cboTitle.ID
                    '                radItem.Attributes("GROUP_NAME") = dtData.Rows(i)("GROUP_NAME").ToString()
                    '        End Select
                    '        sender.Items.Add(radItem)
                    '    Next
                    'End If
                Else
                    Dim itemOffset As Integer = e.NumberOfItems
                    Dim endOffset As Integer = Math.Min(itemOffset + sender.ItemsPerRequest, dtData.Rows.Count)
                    e.EndOfItems = endOffset = dtData.Rows.Count
                    sender.Items.Clear()
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
                FillDataInControls(e.CurrentValue)
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

    'Private Sub ckDANG_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckDANG.CheckedChanged

    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow

    '        If (ckDANG.Checked) Then
    '            EnableControlAll(True, rtCHUC_VU_DANG, rdNGAY_VAO_DANG_DB, rdNGAY_VAO_DANG)
    '        Else
    '            EnableControlAll(False, rtCHUC_VU_DANG, rdNGAY_VAO_DANG_DB, rdNGAY_VAO_DANG)
    '            ClearControlValue(rtCHUC_VU_DANG, rdNGAY_VAO_DANG_DB, rdNGAY_VAO_DANG)
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '        'DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    'Private Sub ckCONG_DOAN_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckCONG_DOAN.CheckedChanged

    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow

    '        If (ckCONG_DOAN.Checked) Then
    '            EnableControlAll(True, ckDOAN_PHI, rtCHUC_VU_DOAN, rdNGAY_VAO_DOAN)
    '        Else
    '            EnableControlAll(False, ckDOAN_PHI, rtCHUC_VU_DOAN, rdNGAY_VAO_DOAN)
    '            ClearControlValue(ckDOAN_PHI, rtCHUC_VU_DOAN, rdNGAY_VAO_DOAN)
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '        'DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    'Private Sub ckBanTT_ND_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckBanTT_ND.CheckedChanged

    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow

    '        If (ckBanTT_ND.Checked) Then
    '            EnableControlAll(True, rtCV_BANTT, rdNgay_TG_BanTT)
    '        Else
    '            EnableControlAll(False, rtCV_BANTT, rdNgay_TG_BanTT)
    '            ClearControlValue(rtCV_BANTT, rdNgay_TG_BanTT)
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '        'DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    'Private Sub ckNU_CONG_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckNU_CONG.CheckedChanged

    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow

    '        If (ckNU_CONG.Checked) Then
    '            EnableControlAll(True, rtCV_Ban_Nu_Cong, rdNgay_TG_Ban_Nu_Cong)
    '        Else
    '            EnableControlAll(False, rtCV_Ban_Nu_Cong, rdNgay_TG_Ban_Nu_Cong)
    '            ClearControlValue(rtCV_Ban_Nu_Cong, rdNgay_TG_Ban_Nu_Cong)
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '        'DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    'Private Sub ckQD_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckQD.CheckedChanged

    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow

    '        If (ckQD.Checked) Then
    '            EnableControlAll(True, rdNgay_Nhap_Ngu_QD, rdNgay_Xuat_Ngu_QD, rtDV_Xuat_Ngu_QD)
    '        Else
    '            EnableControlAll(False, rdNgay_Nhap_Ngu_QD, rdNgay_Xuat_Ngu_QD, rtDV_Xuat_Ngu_QD)
    '            ClearControlValue(rdNgay_Nhap_Ngu_QD, rdNgay_Xuat_Ngu_QD, rtDV_Xuat_Ngu_QD)
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '        'DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    'Private Sub ckThuong_Binh_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckThuong_Binh.CheckedChanged

    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow

    '        If (ckThuong_Binh.Checked) Then
    '            EnableControlAll(True, cbHang_Thuong_Binh, cbGD_Chinh_Sach)
    '        Else
    '            EnableControlAll(False, cbHang_Thuong_Binh, cbGD_Chinh_Sach)
    '            ClearControlValue(cbHang_Thuong_Binh, cbGD_Chinh_Sach)
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '        'DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

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
                          cboIDPlace, txtLangMark, txtLangMark2, txtTimeID,
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
                          cboGender, cboLangLevel, cboLangLevel2, cboInsRegion, rtWorkplace,
                          cboLanguage, cboLanguage2, cboLearningLevel, txtLoaiSucKhoe, cboMajor, cboNationlity, cboNative, cboNav_Province, cboPer_Province,
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

            EmployeeInfo.IMAGE_URL = Server.MapPath(ConfigurationManager.AppSettings("PathFileEmployeeImage")) + "\EmployeeImage"

            If hidID.Value.Trim = "" Then
                EmployeeInfo.ID = 0
            Else
                EmployeeInfo.ID = Decimal.Parse(hidID.Value)
            End If
            If cboObject.SelectedValue <> "" Then
                EmployeeInfo.OBJECTTIMEKEEPING = cboObject.SelectedValue
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

            EmpCV.WORKPLACE_NAME = rtWorkplace.Text
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
            'EmpCV.NGAY_VAO_DOAN = rdNgayVaoDoan.SelectedDate
            EmpCV.NGAY_VAO_DOAN = rdNGAY_VAO_DOAN.SelectedDate
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
            '=============================================
            EmpCV.SKILL = rtSkill.Text
            If IsNumeric(ckDANG.Checked) Then
                EmpCV.DANG = CType(ckDANG.Checked, Decimal)
            End If
            If IsNumeric(ckCA.Checked) Then
                EmpCV.CA = CType(ckCA.Checked, Decimal)
            End If
            EmpCV.BANTT = ckBanTT_ND.Checked
            If IsNumeric(ckCONG_DOAN.Checked) Then
                EmpCV.CONG_DOAN = CType(ckCONG_DOAN.Checked, Decimal)
            End If
            EmpCV.CV_BANTT = rtCV_BANTT.Text
            If IsDate(rdNgay_TG_BanTT.SelectedDate) Then
                EmpCV.NGAY_TG_BANTT = rdNgay_TG_BanTT.SelectedDate
            End If
            If IsNumeric(ckNU_CONG.Checked) Then
                EmpCV.NU_CONG = CType(ckNU_CONG.Checked, Decimal)
            End If
            EmpCV.CV_BAN_NU_CONG = rtCV_Ban_Nu_Cong.Text
            If IsDate(rdNgay_TG_Ban_Nu_Cong.SelectedDate) Then
                EmpCV.NGAY_TG_BAN_NU_CONG = rdNgay_TG_Ban_Nu_Cong.SelectedDate
            End If
            If IsDate(rdNgay_Nhap_Ngu_CA.SelectedDate) Then
                EmpCV.NGAY_NHAP_NGU_CA = rdNgay_Nhap_Ngu_CA.SelectedDate
            End If
            If IsDate(rdNgay_Xuat_Ngu_CA.SelectedDate) Then
                EmpCV.NGAY_XUAT_NGU_CA = rdNgay_Xuat_Ngu_CA.SelectedDate
            End If
            EmpCV.DV_XUAT_NGU_CA = rtDV_Xuat_Ngu_CA.Text
            If IsNumeric(ckQD.Checked) Then
                EmpCV.QD = CType(ckQD.Checked, Decimal)
            End If
            If IsDate(rdNgay_Nhap_Ngu_QD.SelectedDate) Then
                EmpCV.NGAY_NHAP_NGU_QD = rdNgay_Nhap_Ngu_QD.SelectedDate
            End If
            If IsDate(rdNgay_Xuat_Ngu_QD.SelectedDate) Then
                EmpCV.NGAY_XUAT_NGU_QD = rdNgay_Xuat_Ngu_QD.SelectedDate
            End If
            EmpCV.DV_XUAT_NGU_QD = rtDV_Xuat_Ngu_QD.Text
            If IsNumeric(ckThuong_Binh.Checked) Then
                EmpCV.THUONG_BINH = CType(ckThuong_Binh.Checked, Decimal)
            End If
            If IsNumeric(cbHang_Thuong_Binh.SelectedValue) Then
                EmpCV.HANG_THUONG_BINH = cbHang_Thuong_Binh.SelectedValue
            End If
            If IsNumeric(cbGD_Chinh_Sach.SelectedValue) Then
                EmpCV.GD_CHINH_SACH = cbGD_Chinh_Sach.SelectedValue
            End If
            If IsDate(rdNGAY_VAO_DANG.SelectedDate) Then
                EmpCV.NGAY_VAO_DANG = rdNGAY_VAO_DANG.SelectedDate
            End If
            If IsDate(rdNGAY_VAO_DANG_DB.SelectedDate) Then
                EmpCV.NGAY_VAO_DANG_DB = rdNGAY_VAO_DANG.SelectedDate
            End If
            EmpCV.CHUC_VU_DANG = rtCHUC_VU_DANG.Text

            EmpCV.CHUC_VU_DOAN = rtCHUC_VU_DOAN.Text
            EmpCV.DOAN_PHI = ckDOAN_PHI.Checked

            '=============================================
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
            EmpHealth.TTSUCKHOE = rtTTSucKhoe.Text.Trim
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
            If cboLangLevel2.SelectedValue <> "" Then
                EmpEdu.LANGUAGE_LEVEL2 = cboLangLevel2.SelectedValue
            End If
            EmpEdu.LANGUAGE_MARK = txtLangMark.Text
            EmpEdu.LANGUAGE_MARK2 = txtLangMark2.Text
            If cboLanguage.SelectedValue <> "" Then
                EmpEdu.LANGUAGE = cboLanguage.SelectedValue
            End If

            If cboLanguage2.SelectedValue <> "" Then
                EmpEdu.LANGUAGE2 = cboLanguage2.SelectedValue
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

            '=========================================
            If IsNumeric(cbQLNN.SelectedValue) Then
                EmpEdu.QLNN = cbQLNN.SelectedValue
            End If
            If IsNumeric(cbTDTH.SelectedValue) Then
                EmpEdu.TDTH = cbTDTH.SelectedValue
            End If
            If IsNumeric(cbLLCT.SelectedValue) Then
                EmpEdu.LLCT = cbLLCT.SelectedValue
            End If
            EmpEdu.DIEM_XLTH = rtDiem_XL_TH.Text
            '=========================================
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
        Dim startTime As DateTime = DateTime.UtcNow
        Dim orgTree As OrganizationTreeDTO
        Using rep As New ProfileRepository
            Dim org = rep.GetOrganizationByID(orgid)
            If org IsNot Nothing Then
                SetValueComboBox(cboInsRegion, org.REGION_ID, Nothing)
            End If
            orgTree = rep.GetTreeOrgByID(orgid)
        End Using
        Try
            If orgTree IsNot Nothing Then
                If IsNumeric(orgTree.ORG_ID2) Then
                    txtOrgName2.Text = orgTree.ORG_NAME2
                    txtOrgName2.ToolTip = orgTree.ORG_ID2
                End If
                If IsNumeric(orgTree.ORG_ID3) Then
                    txtOrgName.Text = orgTree.ORG_NAME3
                    txtOrgName.ToolTip = orgTree.ORG_ID3
                End If
                If IsNumeric(orgTree.ORG_ID4) Then
                    txtBan.Text = orgTree.ORG_NAME4
                    txtBan.ToolTip = orgTree.ORG_ID4
                End If
                If IsNumeric(orgTree.ORG_ID5) Then
                    txtTo.Text = orgTree.ORG_NAME5
                    txtTo.ToolTip = orgTree.ORG_ID5
                End If
                If IsNumeric(orgTree.REPRESENTATIVE_ID) Then
                    txtManager.Text = orgTree.REPRESENTATIVE_NAME
                    txtManager.ToolTip = orgTree.REPRESENTATIVE_ID
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        Finally
        End Try
    End Sub

    ''' <summary>
    ''' Create by: TUNGLD - 14/05/2019
    ''' Upper First Character
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpperCaseFirst(ByVal str As String) As String
        Try
            If String.IsNullOrEmpty(str) = True Then
                Return ""
            Else
                Return Char.ToUpper(str(0)) + str.Substring(1).ToLower
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

    
    
End Class
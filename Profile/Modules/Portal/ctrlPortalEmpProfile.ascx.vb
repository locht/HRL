Imports Framework.UI
Imports Framework.UI.Utilities
Imports Common
Imports Common.Common
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlPortalEmpProfile
    Inherits CommonView
    Protected WithEvents ViewItem As ViewBase
    Public Overrides Property MustAuthorize As Boolean = False

#Region "Property"

    Public Property EmployeeID As Decimal
    Public Property EmployeeCode As String
    Public Property unit As String

    Property TitleConcurrents As List(Of TitleConcurrentDTO)
        Get
            Return ViewState(Me.ID & "_TitleConcurrents")
        End Get
        Set(ByVal value As List(Of TitleConcurrentDTO))
            ViewState(Me.ID & "_TitleConcurrents") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        EnableControlAll(False, txtWorkStatus, txtEmpCODE,
                                      txtBankNo,
                                      txtFirstNameVN, txtGhiChuSK,
                                      txtHomePhone,
                                      cboIDPlace,
                                      txtLastNameVN, txtID_NO,
                                      txtMobilePhone, txtNavAddress, txtNhomMau,
                                       txtPassNo, txtPassPlace,
                                      txtPerAddress, txtPerEmail, txtCareer,
                                      txtPitCode, txtTaiMuiHong,
                                      txtWorkEmail, txtWorkPermit, txtWorkPermitPlace,
                                      txtContactPerson, txtContactPersonPhone,
                                      rdBirthDate, rdIDDate,
                                      rdPassDate, rdPassExpireDate,
                                      rdWorkPermitDate, rdWorPermitExpireDate,
                                      txtCanNang, txtChieuCao,
                                      txtAcademy, txtBank, txtBankBranch, txtFamilyStatus,
                                      txtGender, txtLoaiSucKhoe, txtMajor, txtNationlity, txtNative, txtNav_Province, txtPer_Province,
                                      txtReligion, txtTitle, txtEmpStatus,
                                      txtPer_District, txtPer_Ward, txtNav_District, txtNav_Ward,
                                      txtContractType, rdContractEffectDate, rdContractExpireDate,
                                      txtEmpOldCODE, txtNoBHXH, txtDifferentName, txtGraduateYear,
                                      txtProvinceBorn, txtDistrictBorn, txtWardBorn, txtMngLevel, txtWorkPlace, txtMngLevel,
                                      txtWorkPlace, txtHoiDongBanToNhom, txtDepartment, txtTeamLead, txtInsArea, rdOfficialDate,
                                      rdProbationDate, rdQuitDate, txtDomicile, txtCareerWhenRecruit, txtForteBussiness, txtSpecialized,
                                      cboQLNN, cboLLCT, cboLanguage, txtEngLevel1, txtMark1, cboLanguage2, cboTDTH, txtITMark,
                                      txtEngLevel2, txtMark2, chkPayViaBank, txtTinhTrangSucKhoe, ckDANG, ckCA, ckBanTT_ND,
                                      ckCONG_DOAN, rtCV_BANTT, rdNgay_TG_BanTT, ckNU_CONG, rtCV_Ban_Nu_Cong,
                                      rdNgay_TG_Ban_Nu_Cong, rdNgay_Nhap_Ngu_CA, rdNgay_Xuat_Ngu_CA, rtDV_Xuat_Ngu_CA,
                                      ckQD, rdNgay_Nhap_Ngu_QD, rdNgay_Xuat_Ngu_QD, rtDV_Xuat_Ngu_QD, ckThuong_Binh,
                                      txtHang_Thuong_Binh, txtGD_Chinh_Sach, rdNGAY_VAO_DANG_DB, rtCHUC_VU_DANG, rdNGAY_VAO_DANG,
                                      rtCHUC_VU_DOAN, rdNGAY_VAO_DOAN, ckDOAN_PHI)
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim EmployeeInfo As EmployeeDTO
        Try
            If Not IsPostBack Then
                Dim orgTree As OrganizationTreeDTO
                Using rep As New ProfileBusinessRepository
                    EmployeeInfo = rep.GetEmployeeByEmployeeID(EmployeeID)
                    Using repRepo As New ProfileRepository
                        orgTree = repRepo.GetTreeOrgByID(EmployeeInfo.ORG_ID)
                    End Using
                    If EmployeeInfo IsNot Nothing AndAlso EmployeeInfo.ID <> 0 Then

                        txtEmpCODE.Text = EmployeeInfo.EMPLOYEE_CODE
                        txtFirstNameVN.Text = EmployeeInfo.FIRST_NAME_VN
                        txtLastNameVN.Text = EmployeeInfo.LAST_NAME_VN
                        txtOrgName.Text = EmployeeInfo.ORG_NAME
                        txtOrgName.ToolTip = Utilities.DrawTreeByString(EmployeeInfo.ORG_DESC)
                        txtTitle.Text = EmployeeInfo.TITLE_ID
                        txtTitle.Text = EmployeeInfo.TITLE_NAME_VN
                        txtTitleGroup.Text = EmployeeInfo.TITLE_GROUP_NAME
                        rdJoinDate.SelectedDate = EmployeeInfo.JOIN_DATE
                        rdQuitDate.SelectedDate = EmployeeInfo.TER_EFFECT_DATE
                        'rdJoinDateState.SelectedDate = EmployeeInfo.JOIN_DATE_STATE
                        'txtStaffRank.Text = EmployeeInfo.STAFF_RANK_NAME
                        txtDirectManager.Text = EmployeeInfo.DIRECT_MANAGER_NAME
                        'txtLevelManager.Text = EmployeeInfo.LEVEL_MANAGER_NAME
                        txtContractType.Text = EmployeeInfo.CONTRACT_TYPE_NAME
                        rdContractEffectDate.SelectedDate = EmployeeInfo.CONTRACT_EFFECT_DATE
                        rdContractExpireDate.SelectedDate = EmployeeInfo.CONTRACT_EXPIRE_DATE
                        txtWorkStatus.Text = EmployeeInfo.WORK_STATUS_NAME
                        txtEmpOldCODE.Text = EmployeeInfo.EMPLOYEE_CODE_OLD
                        txtNoBHXH.Text = EmployeeInfo.BOOKNO
                        txtDifferentName.Text = EmployeeInfo.EMPLOYEE_NAME_OTHER
                        txtEmpStatus.Text = EmployeeInfo.EMP_STATUS_NAME
                        txtMngLevel.Text = orgTree.ORG_NAME2
                        txtHoiDongBanToNhom.Text = orgTree.ORG_NAME3
                        txtDepartment.Text = orgTree.ORG_NAME5
                        txtTeamLead.Text = orgTree.REPRESENTATIVE_NAME

                        Dim empCV As EmployeeCVDTO
                        Dim empEdu As EmployeeEduDTO
                        Dim empHealth As EmployeeHealthDTO
                        rep.GetEmployeeAllByID(EmployeeInfo.ID, empCV, empEdu, empHealth)
                        If empCV IsNot Nothing Then

                            Dim dtPlace
                            Dim dtLanguageleve As DataTable
                            Using rep1 As New ProfileRepository
                                dtPlace = rep1.GetProvinceList(True)
                                dtLanguageleve = rep1.GetOtherList("RC_LANGUAGE_LEVEL", True)
                                FillRadCombobox(cboIDPlace, dtPlace, "NAME", "ID")
                                'FillRadCombobox(cboLangLevel, dtLanguageleve, "NAME", "ID")
                                'FillRadCombobox(cboBIRTH_PLACE, dtPlace, "NAME", "ID")
                            End Using

                            txtGender.Text = empCV.GENDER_NAME
                            rdBirthDate.SelectedDate = empCV.BIRTH_DATE
                            'cboBIRTH_PLACE.SelectedValue = empCV.BIRTH_PLACE
                            txtFamilyStatus.Text = empCV.MARITAL_STATUS_NAME
                            txtReligion.Text = empCV.RELIGION_NAME
                            txtNative.Text = empCV.NATIVE_NAME
                            txtNationlity.Text = empCV.NATIONALITY_NAME
                            txtNavAddress.Text = empCV.NAV_ADDRESS
                            txtNav_Province.Text = empCV.NAV_PROVINCE_NAME
                            txtNav_District.Text = empCV.NAV_DISTRICT_NAME
                            txtNav_Ward.Text = empCV.NAV_WARD_NAME
                            txtPerAddress.Text = empCV.PER_ADDRESS
                            txtPer_Province.Text = empCV.PER_PROVINCE_NAME
                            txtPer_District.Text = empCV.PER_DISTRICT_NAME
                            txtPer_Ward.Text = empCV.PER_WARD_NAME
                            txtProvinceBorn.Text = empCV.PROVINCEEMP_NAME
                            txtDistrictBorn.Text = empCV.DISTRICTEMP_NAME
                            txtWardBorn.Text = empCV.WARDEMP_NAME
                            txtWorkPlace.Text = empCV.WORKPLACE_NAME
                            txtInsArea.Text = empCV.INS_REGION_NAME
                            rdOfficialDate.SelectedDate = empCV.OPPTION7
                            rdProbationDate.SelectedDate = empCV.OPPTION6
                            txtDomicile.Text = empCV.PROVINCENQ_NAME
                            txtCareerWhenRecruit.Text = empCV.OPPTION1
                            txtForteBussiness.Text = empCV.SKILL

                            ' SĐT
                            txtHomePhone.Text = empCV.HOME_PHONE
                            txtMobilePhone.Text = empCV.MOBILE_PHONE
                            ' CMND
                            txtID_NO.Text = empCV.ID_NO
                            rdIDDate.SelectedDate = empCV.ID_DATE
                            cboIDPlace.SelectedValue = empCV.ID_PLACE
                            'Hộ chiếu
                            txtPassNo.Text = empCV.PASS_NO
                            rdPassDate.SelectedDate = empCV.PASS_DATE
                            rdPassExpireDate.SelectedDate = empCV.PASS_EXPIRE
                            txtPassPlace.Text = empCV.PASS_PLACE
                            'Visa
                            'txtVisa.Text = empCV.VISA
                            'rdVisaDate.SelectedDate = empCV.VISA_DATE
                            'rdVisaExpireDate.SelectedDate = empCV.VISA_EXPIRE
                            'txtVisaPlace.Text = empCV.VISA_PLACE
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
                            'chkDoanPhi.Checked = False
                            'If empCV.DOAN_PHI IsNot Nothing Then
                            '    chkDoanPhi.Checked = empCV.DOAN_PHI
                            'End If
                            txtBank.Text = empCV.BANK_NAME
                            txtBankBranch.Text = empCV.BANK_BRANCH_NAME
                            'rdNgayVaoDang.SelectedDate = empCV.NGAY_VAO_DANG
                            'rdNgayVaoDoan.SelectedDate = empCV.NGAY_VAO_DOAN
                            'txtChucVuDang.Text = empCV.CHUC_VU_DANG
                            'txtChucVuDoan.Text = empCV.CHUC_VU_DOAN
                            'txtNoiVaoDang.Text = empCV.NOI_VAO_DANG
                            'txtNoiVaoDoan.Text = empCV.NOI_VAO_DOAN
                            txtBankNo.Text = empCV.BANK_NO
                            txtHang_Thuong_Binh.Text = empCV.HANG_THUONG_BINH_NAME
                            txtGD_Chinh_Sach.Text = empCV.GD_CHINH_SACH_NAME
                            chkPayViaBank.Checked = empCV.IS_PAY_BANK
                            '=========================================================
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
                            If IsDate(empCV.NGAY_VAO_DANG_DB) Then
                                rdNGAY_VAO_DANG_DB.SelectedDate = empCV.NGAY_VAO_DANG_DB
                            End If
                            rtCHUC_VU_DANG.Text = empCV.CHUC_VU_DANG
                            If IsDate(empCV.NGAY_VAO_DANG) Then
                                rdNGAY_VAO_DANG.SelectedDate = empCV.NGAY_VAO_DANG
                            End If
                            rtCHUC_VU_DOAN.Text = empCV.CHUC_VU_DOAN
                            ckDOAN_PHI.Checked = empCV.DOAN_PHI
                            If IsDate(empCV.NGAY_VAO_DOAN) Then
                                rdNGAY_VAO_DOAN.SelectedDate = empCV.NGAY_VAO_DOAN
                            End If
                            '===============================================
                        End If
                        If empEdu IsNot Nothing Then
                            Dim dtLanguage As DataTable
                            Dim dtData As DataTable
                            Using repEdu As New ProfileRepository
                                dtLanguage = repEdu.GetOtherList("RC_LANGUAGE_LEVEL", True)
                                FillRadCombobox(cboLanguage, dtLanguage, "NAME", "ID")
                                FillRadCombobox(cboLanguage2, dtLanguage, "NAME", "ID")
                                dtData = repEdu.GetOtherList("QLNN")
                                FillRadCombobox(cboQLNN, dtData, "NAME", "ID")
                                dtData = repEdu.GetOtherList("LLCT")
                                FillRadCombobox(cboLLCT, dtData, "NAME", "ID")
                                dtData = repEdu.GetOtherList("RC_COMPUTER_LEVEL")
                                FillRadCombobox(cboTDTH, dtData, "NAME", "ID")
                            End Using
                            txtAcademy.Text = empEdu.ACADEMY_NAME
                            'txtTrainingForm.Text = empEdu.TRAINING_FORM_NAME
                            'txtLearningLevel.Text = empEdu.LEARNING_LEVEL_NAME
                            'txtLangLevel.Text = empEdu.LANGUAGE_LEVEL_NAME
                            'txtLangMark.Text = empEdu.LANGUAGE_MARK
                            'cboLangLevel.SelectedValue = empEdu.LANGUAGE
                            txtMajor.Text = empEdu.MAJOR_NAME
                            'txtMajorRemark.Text = empEdu.MAJOR_REMARK
                            'txtGraduateSchool.Text = empEdu.GRADUATE_SCHOOL_NAME
                            txtSpecialized.Text = empEdu.MAJOR
                            If IsNumeric(empEdu.QLNN) Then
                                cboQLNN.SelectedValue = empEdu.QLNN
                            End If
                            If IsNumeric(empEdu.LLCT) Then
                                cboLLCT.SelectedValue = empEdu.LLCT
                            End If
                            If empEdu.LANGUAGE IsNot Nothing Then
                                cboLanguage.SelectedValue = empEdu.LANGUAGE
                            End If
                            txtEngLevel1.Text = empEdu.LANGUAGE_LEVEL_NAME
                            txtMark1.Text = empEdu.LANGUAGE_MARK
                            If empEdu.LANGUAGE2 IsNot Nothing Then
                                cboLanguage2.SelectedValue = empEdu.LANGUAGE2
                            End If
                            txtEngLevel2.Text = empEdu.LANGUAGE_LEVEL_NAME2
                            txtMark2.Text = empEdu.LANGUAGE_MARK2
                            txtGraduateYear.Text = empEdu.GRADUATION_YEAR
                            If IsNumeric(empEdu.TDTH) Then
                                cboTDTH.SelectedValue = empEdu.TDTH
                            End If
                            txtITMark.Text = empEdu.DIEM_XLTH
                        End If

                        If empHealth IsNot Nothing Then
                            txtCanNang.Text = empHealth.CAN_NANG
                            txtChieuCao.Text = empHealth.CHIEU_CAO
                            'txtDaHoaLieu.Text = empHealth.DA_HOA_LIEU
                            txtGhiChuSK.Text = empHealth.GHI_CHU_SUC_KHOE
                            'txtHuyetAp.Text = empHealth.HUYET_AP
                            'txtMatPhai.Text = empHealth.MAT_PHAI
                            'txtMatTrai.Text = empHealth.MAT_TRAI
                            txtNhomMau.Text = empHealth.NHOM_MAU
                            'txtPhoiNguc.Text = empHealth.PHOI_NGUC
                            'txtRangHamMat.Text = empHealth.RANG_HAM_MAT
                            txtTaiMuiHong.Text = empHealth.TAI_MUI_HONG
                            'txtTim.Text = empHealth.TIM
                            'txtVienGanB.Text = empHealth.VIEM_GAN_B
                            txtLoaiSucKhoe.Text = empHealth.LOAI_SUC_KHOE
                            txtTinhTrangSucKhoe.Text = empHealth.TTSUCKHOE
                        End If

                    End If

                End Using

            End If


        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        'rgMain.SetFilter()
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMainToolBar
            BuildToolbar(Me.MainToolBar, ToolbarItem.Edit)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    'Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
    '    Try
    '        CreateDataFilter()

    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try

    'End Sub
#End Region

#Region "Custom"
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileRepository
        Dim _filter As New TitleConcurrentDTO
        Try
            _filter.EMPLOYEE_ID = EmployeeID
            'SetValueObjectByRadGrid(rgMain, _filter)

            'Dim MaximumRows As Integer
            'Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()


            'If Sorts IsNot Nothing Then
            '    Me.TitleConcurrents = rep.GetTitleConcurrent(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
            'Else
            '    Me.TitleConcurrents = rep.GetTitleConcurrent(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
            'End If

            'rgMain.VirtualItemCount = MaximumRows
            'rgMain.DataSource = Me.TitleConcurrents
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
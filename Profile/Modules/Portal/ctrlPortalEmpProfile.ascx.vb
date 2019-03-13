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
                                      txtBankNo, cboBIRTH_PLACE,
                                      txtDaHoaLieu,
                                      txtFirstNameVN, txtGhiChuSK, txtGraduateSchool,
                                      txtHomePhone, txtHuyetAp, txtID_NO,
                                      cboIDPlace, txtLangMark,
                                      txtLastNameVN, txtMatPhai, txtMatTrai,
                                      txtMobilePhone, txtNavAddress, txtNhomMau,
                                       txtPassNo, txtPassPlace,
                                      txtPerAddress, txtPerEmail, txtPhoiNguc, txtCareer,
                                      txtPitCode, txtRangHamMat, txtTaiMuiHong, txtTim,
                                      txtVienGanB, txtVisa, txtVisaPlace,
                                      txtWorkEmail, txtWorkPermit, txtWorkPermitPlace,
                                      txtContactPerson, txtContactPersonPhone, txtChucVuDang, txtChucVuDoan,
                                      rdBirthDate, rdIDDate,
                                      rdNgayVaoDang, rdNgayVaoDoan, rdPassDate, rdPassExpireDate,
                                      rdVisaDate, rdVisaExpireDate, rdWorkPermitDate, rdWorPermitExpireDate,
                                      txtCanNang, txtChieuCao,
                                      txtAcademy, txtBank, txtBankBranch, txtFamilyStatus,
                                      txtGender, txtLangLevel,
                                      cboLangLevel, txtLearningLevel, txtLoaiSucKhoe,
                                      txtMajorRemark, txtMajor, txtNationlity, txtNative, txtNav_Province, txtPer_Province,
                                      txtReligion, txtStaffRank, txtTitle, txtTrainingForm,
                                      txtPer_District, txtPer_Ward, txtNav_District, txtNav_Ward,
                                      txtContractType, rdContractEffectDate, rdContractExpireDate,
                                      chkDangPhi, chkDoanPhi)
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim EmployeeInfo As EmployeeDTO
        Try
            If Not IsPostBack Then
                Using rep As New ProfileBusinessRepository
                    EmployeeInfo = rep.GetEmployeeByEmployeeID(EmployeeID)
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
                        rdJoinDateState.SelectedDate = EmployeeInfo.JOIN_DATE_STATE
                        txtStaffRank.Text = EmployeeInfo.STAFF_RANK_NAME
                        txtDirectManager.Text = EmployeeInfo.DIRECT_MANAGER_NAME
                        txtLevelManager.Text = EmployeeInfo.LEVEL_MANAGER_NAME
                        txtContractType.Text = EmployeeInfo.CONTRACT_TYPE_NAME
                        rdContractEffectDate.SelectedDate = EmployeeInfo.CONTRACT_EFFECT_DATE
                        rdContractExpireDate.SelectedDate = EmployeeInfo.CONTRACT_EXPIRE_DATE
                        txtWorkStatus.Text = EmployeeInfo.WORK_STATUS_NAME

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
                                FillRadCombobox(cboLangLevel, dtLanguageleve, "NAME", "ID")
                                FillRadCombobox(cboBIRTH_PLACE, dtPlace, "NAME", "ID")
                            End Using

                            txtGender.Text = empCV.GENDER_NAME
                            rdBirthDate.SelectedDate = empCV.BIRTH_DATE
                            cboBIRTH_PLACE.SelectedValue = empCV.BIRTH_PLACE
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
                            chkDangPhi.Checked = False
                            If empCV.DANG_PHI IsNot Nothing Then
                                chkDangPhi.Checked = empCV.DANG_PHI
                            End If
                            chkDoanPhi.Checked = False
                            If empCV.DOAN_PHI IsNot Nothing Then
                                chkDoanPhi.Checked = empCV.DOAN_PHI
                            End If
                            txtBank.Text = empCV.BANK_NAME
                            txtBankBranch.Text = empCV.BANK_BRANCH_NAME
                            rdNgayVaoDang.SelectedDate = empCV.NGAY_VAO_DANG
                            rdNgayVaoDoan.SelectedDate = empCV.NGAY_VAO_DOAN
                            txtChucVuDang.Text = empCV.CHUC_VU_DANG
                            txtChucVuDoan.Text = empCV.CHUC_VU_DOAN
                            txtNoiVaoDang.Text = empCV.NOI_VAO_DANG
                            txtNoiVaoDoan.Text = empCV.NOI_VAO_DOAN
                            txtBankNo.Text = empCV.BANK_NO
                        End If
                        If empEdu IsNot Nothing Then
                            txtAcademy.Text = empEdu.ACADEMY_NAME
                            txtTrainingForm.Text = empEdu.TRAINING_FORM_NAME
                            txtLearningLevel.Text = empEdu.LEARNING_LEVEL_NAME
                            txtLangLevel.Text = empEdu.LANGUAGE_LEVEL_NAME
                            txtLangMark.Text = empEdu.LANGUAGE_MARK
                            cboLangLevel.SelectedValue = empEdu.LANGUAGE
                            txtMajor.Text = empEdu.MAJOR_NAME
                            txtMajorRemark.Text = empEdu.MAJOR_REMARK
                            txtGraduateSchool.Text = empEdu.GRADUATE_SCHOOL_NAME

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
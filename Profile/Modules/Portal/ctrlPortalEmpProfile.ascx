<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalEmpProfile.ascx.vb"
    Inherits="Profile.ctrlPortalEmpProfile" %>
    <style type="text/css">
    div .rlbItem
    {
        float: left;
        width: 250px;
    }
    
    .lb3
    {
        text-align: right;
        padding-right: 5px;
        padding-left: 5px;
        vertical-align: middle;
        width: 14%;
    }
    .control3
    {
        width: 20%;
    }
    .RadListBox_Metro .rlbGroup, .RadListBox_Metro .rlbTemplateContainer
    {
        border: none !important;
    }
</style>
<tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
<table class="table-form">
    <tr>
        <td colspan="6">
            <b>
                <%# Translate("Sơ yếu lý lịch")%></b> 
            <hr />
        </td>
    </tr>
    <tr>
        <td class="lb" style="width: 130px">
            <%# Translate("Mã nhân viên")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtEmpCODE" runat="server" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
        <td class="lb" style="width: 130px">
            <%# Translate("Họ và tên lót")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtFirstNameVN" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb" style="width: 130px">
            <%# Translate("Tên")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtLastNameVN" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb" style="width: 130px">
            <%# Translate("Mã cũ")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtEmpOldCODE" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb" style="width: 130px">
            <%# Translate("Số sổ BHXH")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtNoBHXH" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb" style="width: 130px">
            <%# Translate("Tên gọi khác")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtDifferentName" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Ngày sinh")%>
        </td>
        <td>
            <tlk:RadDatePicker runat="server" ID="rdBirthDate">
            </tlk:RadDatePicker>
        </td>
        <td class="lb">
            <%# Translate("Giới tính")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtGender">
            </tlk:RadTextBox>
        </td>
        <td class="lb" style="width: 130px">
            <%# Translate("Tình trạng nhân viên")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtEmpStatus" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb" style="width: 130px">
            <%# Translate("Tỉnh/Thành phố nơi sinh")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtProvinceBorn" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb" style="width: 130px">
            <%# Translate("Huyện nơi sinh")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtDistrictBorn" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb" style="width: 130px">
            <%# Translate("Xã nơi sinh")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtWardBorn" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>        
        <td class="lb" style="width: 130px">
            <%# Translate("Chức danh")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtTitle">
            </tlk:RadTextBox>
        </td>
        <td class="lb" style="width: 130px">
            <%# Translate("Nhóm chức danh")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtTitleGroup" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb" style="width: 130px">
            <%# Translate("Cấp quản lý")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtMngLevel" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
        <td class="lb" style="width: 130px">
            <%# Translate("Đơn vị")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtOrgName" ReadOnly="true" />
        </td>
        <td class="lb" style="width: 130px">
            <%# Translate("Nơi làm việc")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtWorkPlace" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb" style="width: 130px">
            <%# Translate("Hội đồng/Ban/Tổ/Nhóm")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtHoiDongBanToNhom" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
        <td class="lb" style="width: 130px">
            <%# Translate("Bộ phận")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtDepartment" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Trạng thái nhân viên")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtWorkStatus">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Quản lý trực tiếp")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtDirectManager" ReadOnly="true" />
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Trưởng nhóm")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtTeamLead" ReadOnly="true" />
            </tlk:RadTextBox>
        </td>
        <%--<td class="lb">
            <%# Translate("Quản lý trên một cấp")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtLevelManager" ReadOnly="true" />
            </tlk:RadTextBox>
        </td>--%>
        <%--<td class="lb" style="width: 120px">
            <%# Translate("Cấp nhân sự")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtStaffRank">
            </tlk:RadTextBox>
        </td>--%>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Số CMND")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtID_NO">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Ngày cấp")%>
        </td>
        <td>
            <tlk:RadDatePicker runat="server" ID="rdIDDate">
            </tlk:RadDatePicker>
        </td>
        <td class="lb">
            <%# Translate("Nơi cấp")%>
        </td>
        <td>
            <tlk:RadComboBox runat="server" ID="cboIDPlace">
                                            </tlk:RadComboBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Quốc tịch")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtNationlity">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Dân tộc")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtNative">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Tôn giáo")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtReligion">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Vùng bảo hiểm")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtInsArea">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Ngày vào công ty")%>
        </td>
        <td>
            <tlk:RadDatePicker runat="server" ID="rdJoinDate" Enabled="false" DateInput-Enabled="false">
            </tlk:RadDatePicker>
        </td>
        <td class="lb">
            <%# Translate("Ngày vào chính thức")%>
        </td>
        <td>
            <tlk:RadDatePicker runat="server" ID="rdOfficialDate" Enabled="false" DateInput-Enabled="false">
            </tlk:RadDatePicker>
        </td>
        <%--<td class="lb">
            <%# Translate("Nơi sinh")%>
        </td>
        <td colspan="5">
            <tlk:RadComboBox runat="server" ID="cboBIRTH_PLACE" Width="100%">
                                            </tlk:RadComboBox>
        </td>--%>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Ngày thử việc")%>
        </td>
        <td>
            <tlk:RadDatePicker runat="server" ID="rdProbationDate" Enabled="false" DateInput-Enabled="false">
            </tlk:RadDatePicker>
        </td>
        <td class="lb">
            <%# Translate("Ngày thôi việc")%>
        </td>
        <td>
            <tlk:RadDatePicker runat="server" ID="rdQuitDate" Enabled="false" DateInput-Enabled="false">
            </tlk:RadDatePicker>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Nguyên quán")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtDomicile" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <%--<tr>
        <td class="lb">
            <%# Translate("Ngày vào tập đoàn")%>
        </td>
        <td>
            <tlk:RadDatePicker runat="server" ID="rdJoinDateState" Enabled="false" DateInput-Enabled="false">
            </tlk:RadDatePicker>
        </td>        
    </tr>--%>
    <tr>
        <td class="lb">
            <%# Translate("Địa chỉ thường trú")%>
        </td>
        <td colspan="5">
            <tlk:RadTextBox ID="txtPerAddress" runat="server" Width="100%">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Thành phố")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtPer_Province">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Quận huyện")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtPer_District">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Xã phường")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtPer_Ward">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Địa chỉ tạm trú")%>
        </td>
        <td colspan="5">
            <tlk:RadTextBox ID="txtNavAddress" runat="server" Width="100%">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Thành phố")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtNav_Province">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Quận huyện")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtNav_District">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Xã phường")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtNav_Ward">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Nghề nghiệp khi tuyển dụng")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtCareerWhenRecruit">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <b>
                <%# Translate("Thông tin hợp đồng lao động mới nhất")%></b>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Loại hợp đồng")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtContractType" runat="server" Width="100%" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Từ ngày")%>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdContractEffectDate" runat="server" Enabled="false" DateInput-Enabled="false">
            </tlk:RadDatePicker>
        </td>
        <td class="lb">
            <%# Translate("Đến ngày")%>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdContractExpireDate" runat="server" Enabled="false" DateInput-Enabled="false">
            </tlk:RadDatePicker>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <b>
                <%# Translate("Thông tin Đảng đoàn thể")%></b>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="control3">
            <asp:CheckBox ID="ckDANG" Text ="Đảng" runat ="server" />
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID ="lbCHUC_VU_DANG" Text ="Chức vụ"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox runat ="server" ID="rtCHUC_VU_DANG"></tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID ="lbNGAY_DB_DANG" Text ="Ngày dự bị"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadDatePicker  runat ="server" ID="rdNGAY_VAO_DANG_DB"></tlk:RadDatePicker>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID ="lbNGAY_VAO_DANG" Text ="Ngày chính thức"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadDatePicker  runat ="server" ID="rdNGAY_VAO_DANG"></tlk:RadDatePicker>
        </td>
    </tr>
    <%--=================================================================================--%>
    <tr>
        <td class="control3">
            <asp:CheckBox ID="ckCONG_DOAN" Text ="Công đoàn" runat ="server" />
        </td>
        <td class="lb3">
                                        
                                    </td>
        <td class="control3">
            <asp:CheckBox ID="ckDOAN_PHI" Text ="Công đoàn phí" runat ="server" />
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID ="lbCHUC_VU_DOAN" Text ="Chức vụ"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox runat ="server" ID="rtCHUC_VU_DOAN"></tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID ="lbNGAY_VAO_DOAN" Text ="Ngày tham gia"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadDatePicker  runat ="server" ID="rdNGAY_VAO_DOAN"></tlk:RadDatePicker>
        </td>
    </tr>
    <tr>
        <td class="control3">
            <asp:CheckBox ID="ckBanTT_ND" Text ="Ban thanh tra nhân dân" runat ="server" />
        </td>        
        <td class="lb3">
            <asp:Label runat="server" ID ="lbCV_BANTT" Text ="Chức vụ"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox runat ="server" ID="rtCV_BANTT"></tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID ="lbNgay_TG_BanTT" Text ="Ngày tham gia"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadDatePicker  runat ="server" ID="rdNgay_TG_BanTT"></tlk:RadDatePicker>
        </td>
    </tr>
    <tr>
        <td class="control3">
            <asp:CheckBox ID="ckNU_CONG" Text ="Ban nữ công" runat ="server" />
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID ="lbCV_Ban_Nu_Cong" Text ="Chức vụ"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox runat ="server" ID="rtCV_Ban_Nu_Cong"></tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID ="lbNgay_TG_Ban_Nu_Cong" Text ="Ngày tham gia"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadDatePicker  runat ="server" ID="rdNgay_TG_Ban_Nu_Cong"></tlk:RadDatePicker>
        </td>
    </tr>
    <tr>
        <td class="control3">
            <asp:CheckBox ID="ckCA" Text ="Công an" runat ="server" />
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID ="lbNgay_Nhap_Ngu_CA" Text ="Ngày nhập ngũ"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadDatePicker  runat ="server" ID="rdNgay_Nhap_Ngu_CA"></tlk:RadDatePicker>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID ="lbNgay_Xuat_Ngu_CA" Text ="Ngày xuất ngũ"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadDatePicker  runat ="server" ID="rdNgay_Xuat_Ngu_CA"></tlk:RadDatePicker>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID ="lbDV_Xuat_Ngu_CA" Text ="Đơn vị xuất ngũ"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox runat ="server" ID="rtDV_Xuat_Ngu_CA"></tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="control3">
            <asp:CheckBox ID="ckQD" Text ="Quân đội" runat ="server" />
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID ="lbNgay_Nhap_Ngu_QD" Text ="Ngày nhập ngũ"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadDatePicker  runat ="server" ID="rdNgay_Nhap_Ngu_QD"></tlk:RadDatePicker>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID ="lbNgay_Xuat_Ngu_QD" Text ="Ngày xuất ngũ"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadDatePicker  runat ="server" ID="rdNgay_Xuat_Ngu_QD"></tlk:RadDatePicker>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID ="lbDV_Xuat_Ngu_QD" Text ="Đơn vị xuất ngũ"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox runat ="server" ID="rtDV_Xuat_Ngu_QD"></tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="control3">
            <asp:CheckBox ID="ckThuong_Binh" Text ="Thương binh" runat ="server" />
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID ="lbHang_Thuong_Binh" Text ="Hạng"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox runat ="server" ID="txtHang_Thuong_Binh"></tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID ="lbGD_Chinh_Sach" Text ="Gia đình chính sách"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox runat ="server" ID="txtGD_Chinh_Sach"></tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <b>
                <%# Translate("Thông tin phụ")%></b>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Email công ty")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtWorkEmail">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Email cá nhân")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtPerEmail">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Tình trạng hôn nhân")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtFamilyStatus">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Điện thoại di động")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtMobilePhone">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Điện thoại cố định")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtHomePhone">
            </tlk:RadTextBox>
        </td>
    </tr>
    <%--<tr>
        <td>
        </td>
        <td colspan="3">
            <asp:CheckBox ID="chkDoanPhi" runat="server" Text="<%$ Translate: Công đoàn phí %>" />
        </td>
    </tr>--%>
    <tr>
        <td class="lb">
            <%# Translate("Số hộ chiếu")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtPassNo" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Ngày cấp")%>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdPassDate" runat="server">
            </tlk:RadDatePicker>
        </td>
        <td class="lb">
            <%# Translate("Ngày hết hạn")%>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdPassExpireDate" runat="server">
            </tlk:RadDatePicker>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Nơi cấp")%>
        </td>
        <td colspan="5">
            <tlk:RadTextBox ID="txtPassPlace" runat="server" Width="100%">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Giấy phép lao động")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtWorkPermit" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Từ ngày")%>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdWorkPermitDate" runat="server">
            </tlk:RadDatePicker>
        </td>
        <td class="lb">
            <%# Translate("Đến ngày")%>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdWorPermitExpireDate" runat="server">
            </tlk:RadDatePicker>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Nơi cấp")%>
        </td>
        <td colspan="5">
            <tlk:RadTextBox ID="txtWorkPermitPlace" runat="server" Width="100%">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Ngành nghề")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtCareer">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Sở trường công tác")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtForteBussiness">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Người liên hệ")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtContactPerson" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Số điện thoại")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtContactPersonPhone" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <%--<tr>
        <td class="lb">
            <%# Translate("Ngày vào")%>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdNgayVaoDoan" runat="server">
            </tlk:RadDatePicker>
        </td>
        <td class="lb">
            <%# Translate("Chức vụ")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtChucVuDoan" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Nơi vào")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtNoiVaoDoan" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td colspan="3">
            <asp:CheckBox ID="chkDangPhi" runat="server" Text="<%$ Translate: Đảng phí %>" />
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Ngày vào")%>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdNgayVaoDang" runat="server" Width="160px">
            </tlk:RadDatePicker>
        </td>
        <td class="lb">
            <%# Translate("Chức vụ")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtChucVuDang" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Nơi vào")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtNoiVaoDang" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr> --%>   
    
    <%--<tr>
        <td class="lb">
            <%# Translate("Visa")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtVisa" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Ngày cấp")%>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdVisaDate" runat="server">
            </tlk:RadDatePicker>
        </td>
        <td class="lb">
            <%# Translate("Ngày hết hạn")%>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdVisaExpireDate" runat="server">
            </tlk:RadDatePicker>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Nơi cấp")%>
        </td>
        <td colspan="5">
            <tlk:RadTextBox ID="txtVisaPlace" runat="server" Width="100%">
            </tlk:RadTextBox>
        </td>
    </tr>--%>    
    <tr>
        <td colspan="6">
            <b>
                <%# Translate("Trình độ văn hóa")%></b>
            <hr />
        </td>
    </tr>
    <tr>
        <%--<td class="lb">
            <%# Translate("Tên trường")%>
        </td>
        <td colspan="5">
            <tlk:RadTextBox ID="txtGraduateSchool" runat="server" Width="100%">
            </tlk:RadTextBox>
        </td>--%>
        <td class="lb">
            <%# Translate("Chuyên ngành")%>
        </td>
        <td>
           <tlk:RadTextBox ID="txtSpecialized" runat="server">
                                            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Năm tốt nghiệp")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtGraduateYear" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Trình độ văn hóa")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtAcademy" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Trình độ chuyên môn cao nhất")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtMajor" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Quản lý nhà nước")%>
        </td>
        <td>
            <tlk:RadComboBox ID="cboQLNN" runat="server">
            </tlk:RadComboBox>
        </td>
        <td class="lb">
            <%# Translate("Lý luận chính trị")%>
        </td>
        <td>
            <tlk:RadComboBox ID="cboLLCT" runat="server">
            </tlk:RadComboBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Ngoại ngữ 1")%>
        </td>
        <td>
            <tlk:RadComboBox ID="cboLanguage" runat="server">
            </tlk:RadComboBox>
        </td>
        <td class="lb">
            <%# Translate("Trình độ ngoại ngữ")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtEngLevel1" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Điểm số/Xếp loại")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtMark1" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Ngoại ngữ 2")%>
        </td>
        <td>
            <tlk:RadComboBox ID="cboLanguage2" runat="server">
            </tlk:RadComboBox>
        </td>
        <td class="lb">
            <%# Translate("Trình độ ngoại ngữ")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtEngLevel2" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Điểm số/Xếp loại")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtMark2" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Trình độ tin học")%>
        </td>
        <td>
            <tlk:RadCombobox ID="cboTDTH" runat="server">
            </tlk:RadCombobox>
        </td>
        <td class="lb">
            <%# Translate("Điểm số/Xếp loại")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtITMark" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <%--<tr>        
        <td class="lb">
            <%# Translate("Diễn giải")%>
        </td>
        <td colspan="3">
            <tlk:RadTextBox ID="txtMajorRemark" runat="server" Width="100%">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>        
        <td class="lb">
            <%# Translate("Hình thức đào tạo")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtTrainingForm" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Trình độ học vấn")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtLearningLevel" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Trình độ ngoại ngữ")%>
        </td>
        <td>
           <tlk:RadComboBox ID="cboLangLevel" runat="server">
                                            </tlk:RadComboBox>
        </td>
        <td class="lb">
            <%# Translate("Trình độ")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtLangLevel" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Điểm số/Xếp loại")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtLangMark" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>--%>
    <tr>
        <td colspan="6">
            <b>
                <%# Translate("Tài khoản ngân hàng")%></b>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Số tài khoản")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtBankNo" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Ngân hàng")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtBank" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Chi nhánh")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtBankBranch" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Mã số Thuế")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtPitCode" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="control3">
            <asp:CheckBox ID="chkPayViaBank" Text ="Thanh toán qua ngân hàng" runat ="server" />
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <b>
                <%# Translate("Thông tin sức khỏe")%></b>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Chiều cao(cm)")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtChieuCao" runat="server" SkinID="Textbox50">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Tai mũi họng")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtTaiMuiHong" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Cân nặng(kg)")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtCanNang" runat="server" SkinID="Textbox50">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Nhóm máu")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtNhomMau" runat="server" SkinID="Textbox50">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Loại sức khỏe")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtLoaiSucKhoe" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Tình trạng sức khỏe")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtTinhTrangSucKhoe" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Ghi chú")%>
        </td>
        <td colspan="5">
            <tlk:RadTextBox runat="server" ID="txtGhiChuSK" Width="100%" SkinID="Textbox1023" />
        </td>
    </tr>
    <%--<tr>
        <td class="lb">
            <%# Translate("Răng hàm mặt")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtRangHamMat" runat="server">
            </tlk:RadTextBox>
        </td>
        
        <td class="lb">
            <%# Translate("Tim")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtTim" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Huyết áp")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtHuyetAp" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Phổi và ngực")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtPhoiNguc" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Thị lực mắt trái")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtMatTrai" runat="server" SkinID="Textbox50">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Viêm gan B")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtVienGanB" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Thị lực mắt phải")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtMatPhai" runat="server" SkinID="Textbox50">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Da và Hoa liễu")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtDaHoaLieu" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>--%>
    
    <%--<tr>
        <td colspan="6">
            <b>
                <%# Translate("Chức danh kiêm nhiệm")%></b>
            <hr />
        </td>
    </tr>--%>
</table>
<%--<tlk:RadGrid PageSize=50 ID="rgMain" runat="server" AutoGenerateColumns="False" AllowPaging="True"
    Height="250px" AllowSorting="True" AllowMultiRowSelection="true" Width="73%">
    <ClientSettings EnableRowHoverStyle="true">
        <Selecting AllowRowSelect="true" />
    </ClientSettings>
    <MasterTableView DataKeyNames="ID,NAME,EFFECT_DATE,EXPIRE_DATE" ClientDataKeyNames="ID,NAME,EFFECT_DATE,EXPIRE_DATE">
        <Columns>
            <tlk:GridBoundColumn DataField="ID" Visible="false" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="NAME" UniqueName="NAME"
                SortExpression="NAME" ItemStyle-HorizontalAlign="Center" />
            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}"
                ItemStyle-HorizontalAlign="Center" />
            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE"
                UniqueName="EXPIRE_DATE" SortExpression="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}"
                ItemStyle-HorizontalAlign="Center" />
        </Columns>
    </MasterTableView>
</tlk:RadGrid>--%>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "EDIT") {
                OpenInNewTab('Default.aspx?mid=Profile&fid=ctrlPortalEmpProfile_Edit');
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
        }

        function OpenInNewTab(url) {
            window.location.href = url;
        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:RadCodeBlock>

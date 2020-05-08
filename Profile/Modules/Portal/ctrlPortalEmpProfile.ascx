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
                <%# Translate("Thông tin chung")%></b>
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
            <%# Translate("Mã quẹt thẻ")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtITimeID" runat="server" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
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
        <td class="lb">
            <asp:Label runat="server" ID="lbEMPLOYEE_OBJECT" Text="Đối tượng nhân viên"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtEMPLOYEE_OBJECT" Enabled="false" />
        </td>
        <td class="lb">
            <asp:Label runat="server" ID="lbObject" Text="Đối tượng chấm công"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtObject" />
        </td>
        <td>
            <asp:CheckBox ID="chkIs_Hazardous" Enabled="false" runat="server" Text="<%$ Translate: Môi trường độc hại %>"
                AutoPostBack="false" />
        </td>
        <td>
            <asp:CheckBox ID="chkIS_HDLD" Enabled="false" runat="server" Text="<%$ Translate: Tạm hoãn HDLD %>"
                AutoPostBack="false" />
        </td>
    </tr>
    <tr>
        <td class="lb" style="width: 130px">
            <asp:Label runat="server" ID="Label1" Text="Loại nhân viên"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtEmpStatus" runat="server" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
        <td class="lb" style="width: 130px">
            <%# Translate("Tình trạng nhân viên")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtWorkStatus" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <b>
                <%# Translate("Thông tin công tác")%></b>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="lb" style="width: 130px">
            <%# Translate("Đơn vị")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtOrgName" ReadOnly="true" />
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbOrg_C2" Text="Chi nhánh/ khối/ Trung tâm"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtOrg_C2" ReadOnly="true" />
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="lbOrg_C3" Text="Nhà máy/phòng"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtOrg_C3" ReadOnly="true" />
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbOrg_C3_1" Text="Ban"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtOrg_C3_1" ReadOnly="true" />
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="lbOrg_C4" Text="Ngành/VPDD"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtOrg_C4" ReadOnly="true" />
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbOrg_C4_1" Text="Bộ phận"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtOrg_C4_1" ReadOnly="true" />
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="lbOrg_C5" Text="Ca"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtOrg_C5" ReadOnly="true" />
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbOrg_C5_1" Text="Tổ"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtOrg_C5_1" ReadOnly="true" />
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
        <td class="lb">
            <asp:Label runat="server" ID="lbJobPos" Text="Vị trí công việc"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtJobPosition" Enabled="false" />
        </td>
    </tr>
    <tr>
        <td class="lb">
            <asp:Label runat="server" ID="lbJobDescription" Text="Mô tả chức danh công việc"></asp:Label>
        </td>
        <td>
            <tlk:RadComboBox runat="server" ID="cboJobDescription" />
        </td>
        <td class="lb">
            <asp:Label runat="server" ID="lbJobPersional" Text="Mô tả công việc cá nhân"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server">
            </tlk:RadTextBox>
            <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
            </tlk:RadTextBox>
            <tlk:RadButton runat="server" ID="btnUpload" Visible="false" SkinID="ButtonView"
                CausesValidation="false" TabIndex="3" />
            <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải xuống%>"
                CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
            </tlk:RadButton>
        </td>
        <td class="lb">
            <asp:Label runat="server" ID="lbProductionProcess" Text="Công đoạn sản xuất"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtProductionProcess" />
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Quản lý trực tiếp")%>
        </td>
        <td style="display: none">
            <tlk:RadTextBox runat="server" ID="txtDirectManager" ReadOnly="true" />
        </td>
        <td>
            <tlk:RadComboBox ID="cboDirectManager" runat="server" AutoPostBack="true" CausesValidation="false">
            </tlk:RadComboBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbManager" Text="Chức danh quản lý trực tiếp"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtManager" ReadOnly="true" Width="85%" />
        </td>
        <td class="lb" style="width: 130px">
            <%# Translate("Loại hình lao động")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtObjectLabor" runat="server" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Số hợp đồng")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtContractNo" runat="server" Width="100%" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Loại hợp đồng")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtContractType" runat="server" Width="100%" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
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
        <td class="lb">
            <%# Translate("Ngày thử việc")%>
        </td>
        <td>
            <tlk:RadDatePicker runat="server" ID="rdProbationDate" Enabled="false" DateInput-Enabled="false">
            </tlk:RadDatePicker>
        </td>
        <td class="lb">
            <%# Translate("Ngày vào chính thức")%>
        </td>
        <td>
            <tlk:RadDatePicker runat="server" ID="rdOfficialDate" Enabled="false" DateInput-Enabled="false">
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
        <td colspan="6">
            <b>
                <%# Translate("Sơ yếu lý lịch")%></b>
            <hr />
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
        <td class="lb" style="width: 130px">
            <%# Translate("Nơi sinh")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtProvinceBorn" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr style="display: none">
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
    <tr style="display: none">
        <td class="lb" style="width: 130px">
            <asp:Label runat="server" ID="lbPROVINCEEMP_BRITH" Text="Tỉnh/Thành khai sinh"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtPROVINCEEMP_BRITH">
            </tlk:RadTextBox>
        </td>
        <td class="lb" style="width: 130px">
            <asp:Label runat="server" ID="lbDISTRICTEMP_BRITH" Text="Quận/Huyện khai sinh"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtDISTRICTEMP_BRITH">
            </tlk:RadTextBox>
        </td>
        <td class="lb" style="width: 130px">
            <asp:Label runat="server" ID="lbWARDEMP_BRITH" Text="Xã/Phường khai sinh"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtWARDEMP_BRITH">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Giới tính")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtGender">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Quốc tịch")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtNationlity">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
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
        <td class="lb" style="display: none">
            <%# Translate("Vùng bảo hiểm")%>
        </td>
        <td style="display: none">
            <tlk:RadTextBox runat="server" ID="txtInsArea">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb" style="width: 130px; display: none">
            <%# Translate("Số sổ BHXH")%>
        </td>
        <td style="display: none">
            <tlk:RadTextBox ID="txtNoBHXH" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb" style="width: 130px; display: none">
            <%# Translate("Đối tượng đóng bảo hiểm")%>
        </td>
        <td style="display: none">
            <tlk:RadTextBox ID="txtObjectIns" runat="server" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
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
            <asp:Label runat="server" ID="lbExpireIDNO" Text="Ngày hết hạn"></asp:Label>
        </td>
        <td>
            <tlk:RadDatePicker runat="server" ID="rdExpireIDNO">
            </tlk:RadDatePicker>
        </td>
    </tr>
    <tr>
        <td class="lb" style="display: none">
            <%# Translate("Ghi chú thay đổi CMND")%>
        </td>
        <td colspan="5" style="display: none">
            <tlk:RadTextBox runat="server" ID="txtID_REMARK" Width="100%">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="Label9" Text="Người nước ngoài"></asp:Label>
        </td>
        <td>
            <asp:CheckBox ID="chkForeigner" runat="server" />
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbDateOfEntry" Text="Ngày nhập cảnh"></asp:Label>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdDateOfEntry" runat="server">
            </tlk:RadDatePicker>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbPassNo" Text="Số hộ chiếu"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtPassNo" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
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
            <%# Translate("Địa chỉ thường trú")%>
        </td>
        <td colspan="5">
            <tlk:RadTextBox ID="txtPerAddress" runat="server" Width="100%">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="Label10" Text="Quốc gia"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtNationa_TT">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Thành phố")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtPer_Province">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
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
        <td class="lb" style="display: none">
            <%# Translate("Thôn/Ấp/Khu phố")%>
        </td>
        <td colspan="5" style="display: none">
            <tlk:RadTextBox ID="txtVillage" runat="server" Width="100%">
            </tlk:RadTextBox>
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
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="lbNationlity_NQ" Text="Quốc gia"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtNationlity_NQ">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbPROVINCEEMP_ID" Text="Tỉnh/Thành phố"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtPROVINCEEMP_ID">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="lbDISTRICTEMP_ID" Text="Quận/Huyện"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtDISTRICTEMP_ID">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbWARDEMP_ID" Text="Xã/Phường"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtWARDEMP_ID">
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
        <td class="lb3">
            <asp:Label runat="server" ID="Label11" Text="Quốc gia"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtNationlity_TTRU">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Thành phố")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtNav_Province">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
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
        <td colspan="6">
            <b>
                <%# Translate("Thông tin phụ")%></b>
            <hr />
        </td>
    </tr>
    <tr style="display: none">
        <td class="control3" align="right">
            <asp:CheckBox ID="ckCHUHO" Text="Là chủ hộ" runat="server" Checked="false" />
        </td>
        <td>
        </td>
        <td class="lb">
            <asp:Label runat="server" ID="lbNoHouseHolds" Text="Số hộ khẩu"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox runat="server" ID="txtNoHouseHolds">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <asp:Label runat="server" ID="lbCodeHouseHolds" Text="Mã hộ gia đình"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox runat="server" ID="txtCodeHouseHolds">
            </tlk:RadTextBox>
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
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Người liên lạc khẩn cấp")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtContactPerson" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb" style="display: none">
            <asp:Label runat="server" ID="Label3" Text="Mối quan hệ"></asp:Label>
        </td>
        <td style="display: none">
            <tlk:RadTextBox runat="server" ID="txtRelationNLH">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="Label7" Text="Số điện thoại liên hệ khẩn cấp"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtContactMobilePhone" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Tình trạng hôn nhân")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtFamilyStatus">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="Label12" Text="Ngày kết hôn"></asp:Label>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdWeddingDay" runat="server">
            </tlk:RadDatePicker>
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
    <tr style="display: none">
        <td class="lb3">
            <asp:Label runat="server" ID="Label5" Text="Địa chỉ liên lạc"></asp:Label>
        </td>
        <td colspan="5">
            <tlk:RadTextBox ID="txtAddressPerContract" runat="server" Width="100%">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <asp:Label runat="server" ID="lbContactPersonPhone" Text="Điện thoại cố định"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtPerHomePhone" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <asp:Label runat="server" ID="Label2" Text="Điện thoại di động"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtPerMobilePhone" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td></td>
        <td class="control3">
            <asp:CheckBox ID="ckCONG_DOAN" Text="Cán bộ đoàn" runat="server" />
        </td>
        <td class="lb3">
        </td>
        <td class="control3" style="display: none">
            <asp:CheckBox ID="ckDOAN_PHI" Text="Công đoàn phí" runat="server" />
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbNGAY_VAO_DOAN" Text="Ngày vào đoàn"></asp:Label>
        </td>
        <td class="control">
            <tlk:RadDatePicker runat="server" ID="rdNGAY_VAO_DOAN">
            </tlk:RadDatePicker>
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="lbNoiVaoDoan" Text="Nơi vào"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtNoiVaoDoan" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbCHUC_VU_DOAN" Text="Chức vụ"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox runat="server" ID="txtCHUC_VU_DOAN">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td></td>
        <td class="control3">
            <asp:CheckBox ID="ckDANG" Text="Cán bộ Đảng" runat="server" />
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbNGAY_VAO_DANG" Text="Ngày vào Đảng"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadDatePicker runat="server" ID="rdNGAY_VAO_DANG">
            </tlk:RadDatePicker>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbNOI_VAO_DANG" Text="Nơi vào Đảng"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox runat="server" ID="txtNOI_VAO_DANG">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="lbCHUC_VU_DANG" Text="Chức vụ"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox runat="server" ID="rtCHUC_VU_DANG">
            </tlk:RadTextBox>
        </td>
        <td>
            <asp:CheckBox ID="chkDangPhi" Text="Đảng phí" runat="server" />
        </td>
        <td class="lb3" style="display: none">
            <asp:Label runat="server" ID="lbNGAY_DB_DANG" Text="Ngày dự bị"></asp:Label>
        </td>
        <td class="control3" style="display: none">
            <tlk:RadDatePicker runat="server" ID="rdNGAY_VAO_DANG_DB">
            </tlk:RadDatePicker>
        </td>
    </tr>
    <tr class="lb3">
        <td>
            <asp:CheckBox ID="chkATVS" Text="ATVS" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="Label13" Text="Giấy phép hành nghề"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtGPHN" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="Label14" Text="Từ ngày"></asp:Label>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdFrom_GPHN" runat="server">
            </tlk:RadDatePicker>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="Label15" Text="Đến ngày"></asp:Label>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdTo_GPHN" runat="server">
            </tlk:RadDatePicker>
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="Label16" Text="Nơi cấp"></asp:Label>
        </td>
        <td colspan="5">
            <tlk:RadTextBox ID="txtNoiCap_GPHN" runat="server" Width="100%">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="lbWorkPermit" Text="Giấy phép lao động"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtWorkPermit" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbWorkPermitDate" Text="Từ ngày"></asp:Label>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdWorkPermitDate" runat="server">
            </tlk:RadDatePicker>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbWorPermitExpireDate" Text="Đến ngày"></asp:Label>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdWorPermitExpireDate" runat="server">
            </tlk:RadDatePicker>
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="lbWorkPermitPlace" Text="Nơi cấp"></asp:Label>
        </td>
        <td colspan="5">
            <tlk:RadTextBox ID="txtWorkPermitPlace" runat="server" Width="100%">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <b>
                <%# Translate("Trình độ văn hóa")%></b>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="lbLearningLevel" Text="Trình độ học vấn"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtLearningLevel" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbMajor" Text="Trình độ chuyên môn(Chuyên ngành)"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox ID="txtMajor" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbSchool" Text="Trường học"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox ID="txtGraduateSchool" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="lbAcademy" Text="Trình độ văn hóa"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtAcademy" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb3" style="display: none">
            <asp:Label runat="server" ID="lbNamTN" Text="Năm tốt nghiệp"></asp:Label>
        </td>
        <td class="control3" style="display: none">
            <tlk:RadTextBox ID="txtNamTN" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="lbBasic" Text="Vi tính"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtAppDung" runat="server">
            </tlk:RadTextBox>
        </td>
        <td>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbAppDung" Text="Trình độ tin học ứng dụng"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtBasic" runat="server">
            </tlk:RadTextBox>
        </td>
        <td style="display: none">
            <tlk:RadTextBox ID="txtCertificate" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="lbLanguage" Text="Ngoại ngữ 1"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtLanguage" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbLangLevel" Text="Trình độ "></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtLangLevel" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb3" style="display: none">
            <asp:Label runat="server" ID="lbLangMark" Text="Điểm số"></asp:Label>
        </td>
        <td style="display: none">
            <tlk:RadTextBox ID="txtLangMark" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="lbLanguage2" Text="Ngoại ngữ 2"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtLanguage2" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbLangLevel2" Text="Trình độ ngoại ngữ 2 "></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtLangLevel2" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="Label17" Text="Bằng lái xe"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtDriverType" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="Label18" Text="Bằng lái xe mô tô"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtMotoDrivingLicense" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="Label19" Text="Thông tin bổ sung"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtNote" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <b>
                <%# Translate("Thông tin ngân hàng")%></b>
            <hr />
        </td>
    </tr>
    <tr style="display: none">
        <td class="lb3">
            <asp:Label runat="server" ID="lbDayPitcode" Text="Ngày cấp"></asp:Label>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdDayPitcode" runat="server">
            </tlk:RadDatePicker>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbPlacePitcode" Text="Nơi cấp"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtPlacePitcode">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb3" style="display: none">
            <asp:Label runat="server" ID="Label8" Text="Tên người hưởng thụ"></asp:Label>
        </td>
        <td style="display: none">
            <tlk:RadTextBox runat="server" ID="txtPerson_Inheritance">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Mã số Thuế")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtPitCode" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Số tài khoản")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtBankNo" runat="server">
            </tlk:RadTextBox>
        </td>
        <td>
            <asp:CheckBox ID="chkIS_TRANSFER" runat="server" Text="<%$ Translate: Chuyển khoản qua ngân hàng %>"
                AutoPostBack="false" />
        </td>
    </tr>
    <tr>
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
        <td class="lb3" style="display: none">
            <asp:Label runat="server" ID="lbEffect_Bank" Text="Ngày hiệu lực"></asp:Label>
        </td>
        <td style="display: none">
            <tlk:RadDatePicker ID="rdEffect_Bank" runat="server">
            </tlk:RadDatePicker>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <b>
                <%# Translate("Thông tin tham chiếu")%></b>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="Label21" Text="Người phỏng vấn 1"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtInterviewer_1">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="Label22" Text="Người phỏng vấn 2"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtInterviewer_2">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="Label23" Text="Người giới thiệu"></asp:Label>
            <span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtPresenter" ReadOnly="true" Width="130px" />
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="Label24" Text="Địa chỉ"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtAddress">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="Label25" Text="Điện thoại"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtNumberPhone">
            </tlk:RadTextBox>
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
        <td class="lb3">
            <asp:Label runat="server" ID="lbChieuCao" Text="Chiều cao(cm)"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox ID="txtChieuCao" runat="server" SkinID="Textbox50">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbNhomMau" Text="Nhóm máu"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtNhomMau" runat="server" SkinID="Textbox50">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbCanNang" Text="Cân nặng(kg)"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox ID="txtCanNang" runat="server" SkinID="Textbox50">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="Label29" Text="Tiểu sử bản thân"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtTieuSuBanThan">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="Label30" Text="Tiểu sử gia đình"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtTieuSuGiaDinh">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbGhiChuSK" Text="Ghi chú"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtGhiChuSK" />
        </td>
    </tr>
    <tr>
        <td colspan="6" style="display: none">
            <b>
                <%# Translate("Thông tin tổ chức chính trị, xã hội")%></b>
            <hr />
        </td>
    </tr>
    <%--=================================================================================--%>
    <tr style="display: none">
        <td class="control3">
            <asp:CheckBox ID="ckBanTT_ND" Text="Ban thanh tra nhân dân" runat="server" />
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbCV_BANTT" Text="Chức vụ"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox runat="server" ID="rtCV_BANTT">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbNgay_TG_BanTT" Text="Ngày tham gia"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadDatePicker runat="server" ID="rdNgay_TG_BanTT">
            </tlk:RadDatePicker>
        </td>
    </tr>
    <tr style="display: none">
        <td class="control3">
            <asp:CheckBox ID="ckNU_CONG" Text="Ban nữ công" runat="server" />
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbCV_Ban_Nu_Cong" Text="Chức vụ"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox runat="server" ID="rtCV_Ban_Nu_Cong">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbNgay_TG_Ban_Nu_Cong" Text="Ngày tham gia"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadDatePicker runat="server" ID="rdNgay_TG_Ban_Nu_Cong">
            </tlk:RadDatePicker>
        </td>
    </tr>
    <tr style="display: none">
        <td class="control3">
            <asp:CheckBox ID="ckCA" Text="Công an" runat="server" />
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbNgay_Nhap_Ngu_CA" Text="Ngày nhập ngũ"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadDatePicker runat="server" ID="rdNgay_Nhap_Ngu_CA">
            </tlk:RadDatePicker>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbNgay_Xuat_Ngu_CA" Text="Ngày xuất ngũ"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadDatePicker runat="server" ID="rdNgay_Xuat_Ngu_CA">
            </tlk:RadDatePicker>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbDV_Xuat_Ngu_CA" Text="Đơn vị xuất ngũ"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox runat="server" ID="rtDV_Xuat_Ngu_CA">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr style="display: none">
        <td class="control3">
            <asp:CheckBox ID="ckQD" Text="Quân đội" runat="server" />
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbNgay_Nhap_Ngu_QD" Text="Ngày nhập ngũ"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadDatePicker runat="server" ID="rdNgay_Nhap_Ngu_QD">
            </tlk:RadDatePicker>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbNgay_Xuat_Ngu_QD" Text="Ngày xuất ngũ"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadDatePicker runat="server" ID="rdNgay_Xuat_Ngu_QD">
            </tlk:RadDatePicker>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbDV_Xuat_Ngu_QD" Text="Đơn vị xuất ngũ"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox runat="server" ID="rtDV_Xuat_Ngu_QD">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr style="display: none">
        <td class="control3">
            <asp:CheckBox ID="ckThuong_Binh" Text="Thương binh" runat="server" />
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbHang_Thuong_Binh" Text="Hạng"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox runat="server" ID="txtHang_Thuong_Binh">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbGD_Chinh_Sach" Text="Gia đình chính sách"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox runat="server" ID="txtGD_Chinh_Sach">
            </tlk:RadTextBox>
        </td>
    </tr>
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

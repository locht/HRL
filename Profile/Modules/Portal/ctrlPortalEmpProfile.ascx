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
        <td class="lb" style="width: 130px">
            <%# Translate("Mã nhân viên")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtEmpCODE" runat="server" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
        <td class="lb" style="width: 130px">
            <%# Translate("Mã chấm công")%>
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
        <td class="lb" style="width: 130px">
            <%# Translate("Phòng ban")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtOrgName" ReadOnly="true" />
        </td>
        <td class="lb" style="width: 130px">
            <%# Translate("Chức danh")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtTitle">
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
         <td class="lb3">
            <asp:Label runat="server" ID="lbManager" Text="Chức danh quản lý trực tiếp"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtManager" ReadOnly="true" Width="85%" />
        </td>
         <td class="lb" style="width: 130px">
            <%# Translate("Đối tượng nhân viên")%>
        </td>
        <td>           
             <tlk:RadTextBox ID="txtObjectLabor" runat="server" ReadOnly="true">
            </tlk:RadTextBox>
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
    <tr style = "display:none">       
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
            <asp:Label runat="server" ID="lbPROVINCEEMP_BRITH" Text="Tỉnh/Thành khai sinh"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtPROVINCEEMP_BRITH" >
            </tlk:RadTextBox>
        </td>   
        <td class="lb" style="width: 130px">
            <asp:Label runat="server" ID="lbDISTRICTEMP_BRITH" Text="Quận/Huyện khai sinh"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtDISTRICTEMP_BRITH" >
            </tlk:RadTextBox>
        </td>
        <td class="lb" style="width: 130px">
            <asp:Label runat="server" ID="lbWARDEMP_BRITH" Text="Xã/Phường khai sinh"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtWARDEMP_BRITH" >
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
         <td class="lb">
            <%# Translate("Vùng bảo hiểm")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtInsArea">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb" style="width: 130px">
            <%# Translate("Số sổ BHXH")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtNoBHXH" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb" style="width: 130px">
            <%# Translate("Đối tượng đóng bảo hiểm")%>
        </td>
        <td>           
             <tlk:RadTextBox ID="txtObjectIns" runat="server" ReadOnly="true">
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
         <td class="lb">
            <%# Translate("Ghi chú thay đổi CMND")%>
        </td>
        <td colspan="5">
            <tlk:RadTextBox runat="server" ID="txtID_REMARK" Width="100%">
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
            <tlk:RadTextBox ID="txtLearningLevel" runat="server" >
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbMajor" Text="Trình độ chuyên môn"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox ID="txtMajor" runat="server" >
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
        <td class="lb3">
            <asp:Label runat="server" ID="lbNamTN" Text="Năm tốt nghiệp"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox ID="txtNamTN" runat="server" >
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="lbBasic" Text="Trình độ tin học cơ bản"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtBasic" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbCertifiace" Text="Loại chứng chỉ"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtCertificate" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbAppDung" Text="Trình độ tin học ứng dụng"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtAppDung" runat="server" >
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="lbLanguage" Text="Ngoại ngữ"></asp:Label>
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
        <td class="lb3">
            <asp:Label runat="server" ID="lbLangMark" Text="Điểm số"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtLangMark" runat="server">
            </tlk:RadTextBox>
        </td>
    </tr>

    <tr>
        <td colspan="6">
            <b>
                <%# Translate("Thông tin liên hệ")%></b>
            <hr />
        </td>
    </tr>
    <tr>
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
            <%# Translate("Thôn/Ấp/Khu phố")%>
        </td>
        <td colspan="5">
            <tlk:RadTextBox ID="txtVillage" runat="server" Width="100%">
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
        <td colspan="6">
            <b>
                <%# Translate("Thông tin liên hệ khẩn cấp")%></b>
            <hr />
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
            <asp:Label runat="server" ID="Label3" Text="Mối quan hệ"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtRelationNLH">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
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
        <td colspan="6">
            <b>
                <%# Translate("Giấy tờ tùy thân")%></b>
            <hr />
        </td>
    </tr>
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
        <td colspan="6">
            <b>
                <%# Translate("Thông tin thuế và tài khoản ngân hàng")%></b>
            <hr />
        </td>
    </tr>    
    <tr>
        <td class="lb3">
            <asp:Label runat="server" ID="Label8" Text="Tên người hưởng thụ"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtPerson_Inheritance">
            </tlk:RadTextBox>
        </td>
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
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Chi nhánh")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtBankBranch" runat="server">
            </tlk:RadTextBox>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID="lbEffect_Bank" Text="Ngày hiệu lực"></asp:Label>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdEffect_Bank" runat="server">
            </tlk:RadDatePicker>
        </td>
    </tr>      
        
    <tr>
        <td colspan="6">
            <b>
                <%# Translate("Thông tin tổ chức chính trị, xã hội")%></b>
            <hr />
        </td>
    </tr>   
    <tr>
        <td class="control3">
            <asp:CheckBox ID="ckCONG_DOAN" Text ="Đoàn viên" runat ="server" />
        </td>
        <td class="lb3">                                        
        </td>
        <td class="control3" >
            <asp:CheckBox ID="ckDOAN_PHI" Text ="Công đoàn phí" runat ="server" />
        </td>
<%--        <td class="lb3">
            <asp:Label runat="server" ID ="lbCHUC_VU_DOAN" Text ="Chức vụ"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox runat ="server" ID="rtCHUC_VU_DOAN"></tlk:RadTextBox>
        </td>--%>
        <td class="lb3">
            <asp:Label runat="server" ID ="lbNGAY_VAO_DOAN" Text ="Ngày tham gia"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadDatePicker  runat ="server" ID="rdNGAY_VAO_DOAN"></tlk:RadDatePicker>
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
        <td class="lb3" style="display: none">
            <asp:Label runat="server" ID ="lbNGAY_DB_DANG" Text ="Ngày dự bị"></asp:Label>
        </td>
        <td class="control3" style="display: none">
            <tlk:RadDatePicker  runat ="server" ID="rdNGAY_VAO_DANG_DB"></tlk:RadDatePicker>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID ="lbNGAY_VAO_DANG" Text ="Ngày chính thức"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadDatePicker  runat ="server" ID="rdNGAY_VAO_DANG"></tlk:RadDatePicker>
        </td>
        <td class="lb3">
            <asp:Label runat="server" ID ="lbNOI_VAO_DANG" Text ="Nơi vào Đảng"></asp:Label>
        </td>
        <td class="control3">
            <tlk:RadTextBox runat ="server" ID="txtNOI_VAO_DANG"></tlk:RadTextBox>
        </td>
    </tr>

    <%--=================================================================================--%>    
    <tr style="display: none">
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
    <tr style="display: none">
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
    <tr style="display: none">
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
    <tr style="display: none">
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
    <tr style="display: none">
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

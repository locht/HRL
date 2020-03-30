﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_CanDtlProfile.ascx.vb"
    Inherits="Recruitment.ctrlRC_CanDtlProfile" %>
<%@ Import Namespace="Common" %>
<style type="text/css">
    .form-table td
    {
        padding: 5px;
    }
    fieldset
    {
        padding: 0.35em 0.625em 0.75em !important;
        margin: 0 8px !important;
        border: 1px solid #c0c0c0 !important;
    }
    
    fieldset legend
    {
        border-bottom: none !important;
        width: inherit !important;
        margin-bottom: 0px !important;
    }
    .RadTabStripTop_Office2007 .rtsLevel1
    {
        position: fixed;
        z-index: 100;
    }
    .lb
    {
        width: 115px;
    }
    .msg-error
    {
        margin-left: 420px;
        font-family: "Open Sans" , "Helvetica Neue" , Helvetica, Arial, sans-serif;
        padding: 0px 33px 0px 0px;
        border-radius: 5px;
        text-align: center;
        position: fixed;
        left: 50%;
        top: 50%; /* margin-right: -184px; */
        margin-top: -267px;
        overflow: hidden;
        z-index: 99999;
    }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="ToolbarPane" runat="server" Height="32px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
    </tlk:RadPane>
    <%--BEGIN: TÌM KIẾM--%>
    <tlk:RadPane ID="RadPane1" runat="server" Height="110px" Scrolling="None">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="msg-error validationsummary" />
        <table class="table-form">
            <tr>
                <td>
                    <asp:Panel runat="server" ID="Panel1" DefaultButton="btnSearchEmp">
                        <table class="table-form">
                            <tr>
                                <td class="lb">
                                    <%# Translate("Mã ứng viên")%>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <%--Mã nhân viên--%>
                                        <asp:HiddenField ID="hidID" runat="server" />
                                        <%--Nơi làm việc--%>
                                        <asp:HiddenField ID="hidOrg" runat="server" />
                                        <asp:HiddenField ID="hidTitle" runat="server" />
                                        <asp:HiddenField ID="hidProgramID" runat="server" />
                                        <tlk:RadTextBox ID="txtEmpCODE" runat="server" Enabled="false">
                                        </tlk:RadTextBox>
                                        <tlk:RadButton runat="server" ID="btnSearchEmp" CausesValidation="false" Visible="false"
                                            SkinID="ButtonView" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb  lb-first">
                                    <%# Translate("Họ tên")%><span class="lbReq">*</span>
                                </td>
                                <td>
                                    <tlk:RadTextBox ID="txtFirstNameVN" runat="server" Placeholder="Họ tên lót">
                                    </tlk:RadTextBox>
                                    <asp:RequiredFieldValidator ID="rfvFirstNameVN" ControlToValidate="txtFirstNameVN"
                                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập họ và tên lót %>" ToolTip="<%$ Translate: Bạn phải nhập họ và tên lót %>">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td class="lb">
                                    <%# Translate("Tên")%><span class="lbReq">*</span>
                                </td>
                                <td>
                                    <tlk:RadTextBox ID="txtLastNameVN" runat="server" Placeholder="Tên">
                                    </tlk:RadTextBox>
                                    <asp:RequiredFieldValidator ID="rfvLastNameVN" ControlToValidate="txtLastNameVN"
                                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên %>" ToolTip="<%$ Translate: Bạn phải nhập tên %>">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Nơi làm việc")%>
                                </td>
                                <td>
                                    <tlk:RadTextBox runat="server" ID="txtOrgName" ReadOnly="true" />
                                </td>
                                <td class="lb">
                                    <%# Translate("Chức danh")%>
                                </td>
                                <td>
                                    <tlk:RadTextBox ID="txtTitleName" runat="server" ReadOnly="true">
                                    </tlk:RadTextBox>
                                </td>
                                <td class="lb" style="display: none">
                                    <%# Translate("Chức danh quan tâm")%>
                                </td>
                                <td style="display: none">
                                    <tlk:RadTextBox ID="txtCare_TitleName" runat="server">
                                    </tlk:RadTextBox>
                                </td>
                                <td class="lb" style="display: none">
                                    <%# Translate("Trang tuyển dụng")%>
                                </td>
                                <td style="display: none">
                                    <tlk:RadTextBox ID="txtCare_Website" runat="server">
                                    </tlk:RadTextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <%--BEGIN: HỒ SƠ--%>
    <tlk:RadPane ID="DetailPane" runat="server" Scrolling="Y">
        <tlk:RadTabStrip ID="rtabRecruitmentInfo" runat="server" MultiPageID="RadMultiPage1"
            AutoPostBack="false" CausesValidation="false">
            <Tabs>
                <tlk:RadTab runat="server" ID="rtIdEmpCV" PageViewID="rpvEmpCV" Text="<%$ Translate: Sơ yếu lý lịch %>"
                    Selected="true">
                </tlk:RadTab>
                <tlk:RadTab runat="server" ID="rtIdEmpDegree" PageViewID="rpvEmpDegree" Text="<%$ Translate: Thông tin trình độ %>">
                </tlk:RadTab>
                <tlk:RadTab runat="server" ID="rtIdExpect" PageViewID="rpvIdExpect" Text="<%$ Translate: Nguyện vọng %>">
                </tlk:RadTab>
                <tlk:RadTab runat="server" ID="rtIdOtherInfo" PageViewID="rpvOtherInfo" Text="<%$ Translate: Thông tin khác %>">
                </tlk:RadTab>
            </Tabs>
        </tlk:RadTabStrip>
        <tlk:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" runat="server" Style="margin-top: 28px"
            Width="100%" ScrollBars="None">
            <tlk:RadPageView ID="rpvEmpCV" runat="server" Selected="true">
                <fieldset>
                    <legend>
                        <%# Translate("Thông tin cá nhân")%>
                    </legend>
                    <table class="table-form">
                        <tr>
                            <td class="lb">
                                <%# Translate("Giới tính")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboGender">
                                </tlk:RadComboBox>
                                <asp:RequiredFieldValidator ID="cv_rfvGender" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Giới tính %>"
                                    ToolTip="<%$ Translate: Bạn phải chọn Giới tính %>" ControlToValidate="cboGender">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td class="lb">
                                <%# Translate("Tình trạng hôn nhân")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboTinhTrangHN">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Dân tộc")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboNative">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Tôn giáo")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboReligion">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Ngày sinh")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadDatePicker runat="server" ID="rdBirthDate">
                                </tlk:RadDatePicker>
                                <asp:RequiredFieldValidator ID="cv_rfvBirthDate" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Ngày sinh %>"
                                    ToolTip="<%$ Translate: Bạn phải chọn Ngày sinh %>" ControlToValidate="rdBirthDate">
                                </asp:RequiredFieldValidator>
                                <%--<asp:CustomValidator ID="val_Same_Date_FullName" runat="server" ErrorMessage="<%$ Translate: Ngày sinh và họ tên đã tồn tại %>"
                                    ToolTip="<%$ Translate: Ngày sinh và họ tên đã tồn tại %>">
                                </asp:CustomValidator>--%>
                            </td>
                            <td class="lb" style="display: none">
                                <%# Translate("Nơi sinh")%>
                            </td>
                            <td style="display: none">
                                <tlk:RadComboBox runat="server" ID="cboProvince">
                                </tlk:RadComboBox>
                            </td>
                            <td>
                                <%--<asp:RequiredFieldValidator ID="cv_rfvcboProvince" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn nơi sinh %>"
                                    ToolTip="<%$ Translate: Bạn phải chọn nơi sinh %>" ControlToValidate="cboProvince">
                                </asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Nơi sinh")%>
                            </td>
                            <td colspan="7">
                                <tlk:RadTextBox runat="server" ID="txtBirthAddress" runat="server" Width="100%" SkinID="TextBox1023">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Quốc gia")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboNation">
                                </tlk:RadComboBox>
                                <%--<asp:RequiredFieldValidator ID="cv_rfvcboNation" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn quốc gia %>"
                                    ToolTip="<%$ Translate: Bạn phải chọn quốc gia %>" ControlToValidate="cboNation">
                                </asp:RequiredFieldValidator>--%>
                            </td>
                            <td class="lb">
                                <%# Translate("Tỉnh/Thành phố")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboNav_Province">
                                </tlk:RadComboBox>
                                <%--<asp:RequiredFieldValidator ID="cv_rfvNav_Province" ControlToValidate="cboNav_Province"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn tỉnh/thành phố %>"
                                    ToolTip="<%$ Translate: Bạn phải chọn tỉnh/thành phố %>"> 
                                </asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Quốc tịch")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboNationality">
                                </tlk:RadComboBox>
                                <%--<asp:RequiredFieldValidator ID="cv_rfvcboNationality" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn quốc tịch %>"
                                    ToolTip="<%$ Translate: Bạn phải chọn quốc tịch %>" ControlToValidate="cboNationality">
                                </asp:RequiredFieldValidator>--%>
                            </td>
                            <td class="lb">
                                <%# Translate("Số CMND/thẻ căn cước")%>
                                <span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="rntxtCMND" runat="server">
                                    <ClientEvents OnKeyPress="keyPress" />
                                </tlk:RadTextBox>
                                <asp:RequiredFieldValidator ID="reqID_NO" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Số CMND %>"
                                    ToolTip="<%$ Translate: Bạn phải chọn Số CMND %>" ControlToValidate="rntxtCMND">
                                </asp:RequiredFieldValidator>
                                <%--<asp:CustomValidator ID="cusNO_ID" runat="server" ErrorMessage="<%$ Translate: CMND này đã tồn tại %>"
                                    ToolTip="<%$ Translate: CMND này đã tồn tại %>">
                                </asp:CustomValidator>
                                <asp:CustomValidator ID="cusBlackList" runat="server" ErrorMessage="<%$ Translate: Ứng viên thuộc danh sách đen %>"
                                    ToolTip="<%$ Translate: Ứng viên thuộc danh sách đen %>">
                                </asp:CustomValidator>
                                <asp:CustomValidator ID="cusWorking" runat="server" ErrorMessage="<%$ Translate: Ứng viên đang làm việc tại ACV %>"
                                    ToolTip="<%$ Translate: Ứng viên đang làm việc tại ACV %>">
                                </asp:CustomValidator>
                                <asp:CustomValidator ID="cusTerminate" runat="server" ErrorMessage="<%$ Translate: Ứng viên đã từng làm việc tại ACV %>"
                                    ToolTip="<%$ Translate: Ứng viên đã từng làm việc tại ACV %>">
                                </asp:CustomValidator>--%>
                            </td>
                            <td class="lb">
                                <%# Translate("Ngày cấp")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker runat="server" ID="rdCMNDDate" AutoPostBack="true">
                                </tlk:RadDatePicker>
                                <asp:RequiredFieldValidator ID="cv_rfvrdpCMNDDate" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Ngày cấp %>"
                                    ToolTip="<%$ Translate: Bạn phải chọn Ngày cấp %>" ControlToValidate="rdCMNDDate">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Ngày hết hạn CMND")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker runat="server" ID="rdCMNDEnd">
                                </tlk:RadDatePicker>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdCMNDEnd"
                                    Type="Date" ControlToCompare="rdCMNDDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>"
                                    ToolTip="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>"></asp:CompareValidator>
                            </td>
                            <td class="lb">
                                <%# Translate("Nơi cấp")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboCMNDPlace">
                                </tlk:RadComboBox>
                            </td>
                            <td>
                                <%--<asp:RequiredFieldValidator ID="rfvcboCMNDPlace" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn nơi cấp %>"
                                    ToolTip="<%$ Translate: Bạn phải chọn nơi cấp %>" ControlToValidate="cboCMNDPlace">
                                </asp:RequiredFieldValidator>--%>
                            </td>
                            <td>
                            </td>
                            <td>
                                <tlk:RadButton ID="cv_cbxKhongCuTru" ToggleType="CheckBox" ButtonType="ToggleButton"
                                    Text="<%$ Translate: Đối tượng không cư trú %>" runat="server" CausesValidation="false"
                                    AutoPostBack="false" Visible="true">
                                </tlk:RadButton>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="lb">
                                <%# Translate("Điểm mạnh")%>
                            </td>
                            <td colspan="7">
                                <tlk:RadTextBox ID="txtStranger" runat="server" Width="100%" SkinID="TextBox1023">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="lb">
                                <%# Translate("Điểm yếu")%>
                            </td>
                            <td colspan="7">
                                <tlk:RadTextBox ID="txtWeakness" runat="server" Width="100%" SkinID="TextBox1023">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset style="width: auto; height: auto">
                    <legend><b>
                        <%# Translate("Thông tin liên hệ")%>
                    </b></legend>
                    <table class="table-form">
                        <tr style="display: none">
                            <td class="lb">
                                <%# Translate("Nguyên quán")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboNavNation">
                                </tlk:RadComboBox>
                                <%--<asp:RequiredFieldValidator ID="rfvcboNavNation" ControlToValidate="cboNavNation"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn nguyên quán %>" ToolTip="<%$ Translate: Bạn phải chọn nguyên quán %>"> 
                                </asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Nguyên quán")%>
                            </td>
                            <td colspan="7">
                                <tlk:RadTextBox ID="txtNavDomicile" runat="server" Width="100%">
                                </tlk:RadTextBox>
                                <%--<asp:RequiredFieldValidator ID="rfvcboNavNation" ControlToValidate="cboNavNation"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn nguyên quán %>" ToolTip="<%$ Translate: Bạn phải chọn nguyên quán %>"> 
                                </asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Địa chỉ thường trú")%>
                            </td>
                            <td colspan="7">
                                <tlk:RadTextBox ID="txtPerAddress" runat="server" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                            <td>
                                <%-- <asp:RequiredFieldValidator ID="rfvtxtPerAddress" ControlToValidate="txtPerAddress"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập địa chỉ thường trú %>"
                                    ToolTip="<%$ Translate: Bạn phải nhập địa chỉ thường trú %>"> 
                                </asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Quốc gia")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboPerNation" AutoPostBack="true" CausesValidation="false">
                                </tlk:RadComboBox>
                                <%--<asp:RequiredFieldValidator ID="rfv_cboPerNation" ControlToValidate="cboPerNation"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn quốc gia %>" ToolTip="<%$ Translate: Bạn phải chọn quốc gia %>"> 
                                </asp:RequiredFieldValidator>--%>
                            </td>
                            <td class="lb">
                                <%# Translate("Tỉnh/thành phố")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" AutoPostBack="true" CausesValidation="false" ID="cboPerProvince">
                                </tlk:RadComboBox>
                                <%--<asp:RequiredFieldValidator ID="cv_rfvcboPerProvince" ControlToValidate="cboPerProvince"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn tỉnh/thành phố %>"
                                    ToolTip="<%$ Translate: Bạn phải chọn tỉnh/thành phố %>"> 
                                </asp:RequiredFieldValidator>--%>
                            </td>
                            <td class="lb">
                                <%# Translate("Quận/huyện")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" AutoPostBack="true" CausesValidation="false" ID="cboPerDictrict">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb" style="display: none">
                                <%# Translate("Phường/Xã")%>
                            </td>
                            <td style="display: none">
                                <tlk:RadComboBox runat="server" CausesValidation="false" ID="cbPerward">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="lb">
                                <%# Translate("Thôn ấp khu phố")%>
                            </td>
                            <td colspan="7">
                                <tlk:RadTextBox ID="txtContactAdd" runat="server" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                            <td>
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtContactAdd"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập địa chỉ liên hệ %>"
                                    ToolTip="<%$ Translate: Bạn phải nhập địa chỉ liên hệ %>"> 
                                </asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Địa chỉ tạm trú")%>
                            </td>
                            <td colspan="7">
                                <tlk:RadTextBox ID="txtContactAddress" runat="server" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                            <td>
                                <%--<asp:RequiredFieldValidator ID="cv_rfvContactAddress" ControlToValidate="txtContactAddress"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập địa chỉ tạm trú %>"
                                    ToolTip="<%$ Translate: Bạn phải nhập địa chỉ tạm trú %>"> 
                                </asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Quốc gia")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" AutoPostBack="true" CausesValidation="false" ID="cboContactNation">
                                </tlk:RadComboBox>
                                <%--<asp:RequiredFieldValidator ID="rfv_cboContactNation" ControlToValidate="cboContactNation"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn quốc gia %>" ToolTip="<%$ Translate: Bạn phải chọn quốc gia %>"> 
                                </asp:RequiredFieldValidator>--%>
                            </td>
                            <td class="lb ">
                                <%# Translate("Tỉnh/thành phố")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" AutoPostBack="true" CausesValidation="false" ID="cboContactProvince">
                                </tlk:RadComboBox>
                                <%--<asp:RequiredFieldValidator ID="cv_rfvContactProvince" ControlToValidate="cboContactProvince"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn tỉnh/thành phố %>"
                                    ToolTip="<%$ Translate: Bạn phải chọn tỉnh/thành phố %>"> 
                                </asp:RequiredFieldValidator>--%>
                            </td>
                            <td class="lb">
                                <%# Translate("Quận/huyện")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboContractDictrict" AutoPostBack="true" CausesValidation="false">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb" style="display: none">
                                <%# Translate("Phường/Xã")%>
                            </td>
                            <td style="display: none">
                                <tlk:RadComboBox runat="server" CausesValidation="false" ID="cboContractWard">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Địa chỉ liên hệ")%>
                            </td>
                            <td colspan="7">
                                <tlk:RadTextBox ID="txtContactAddNow" runat="server" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                            <td>
                                <%--<asp:RequiredFieldValidator ID="cv_rfvContactAddress" ControlToValidate="txtContactAddress"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập địa chỉ tạm trú %>"
                                    ToolTip="<%$ Translate: Bạn phải nhập địa chỉ tạm trú %>"> 
                                </asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Quốc gia")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" AutoPostBack="true" CausesValidation="false" ID="cboContactAddNation">
                                </tlk:RadComboBox>
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboContactAddNation"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn quốc gia %>" ToolTip="<%$ Translate: Bạn phải chọn quốc gia %>"> 
                                </asp:RequiredFieldValidator>--%>
                            </td>
                            <td class="lb">
                                <%# Translate("Tỉnh/thành phố")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" AutoPostBack="true" CausesValidation="false" ID="cboContactAddProvince">
                                </tlk:RadComboBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cboContactAddProvince"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn tỉnh/thành phố %>"
                                    ToolTip="<%$ Translate: Bạn phải chọn tỉnh/thành phố %>"> 
                                </asp:RequiredFieldValidator>--%>
                            </td>
                            <td class="lb">
                                <%# Translate("Quận/huyện")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboContractAddDictrict" CausesValidation="false">
                                </tlk:RadComboBox>
                            </td>
                            <td>
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="cboContractAddDictrict"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn quận/huyện %>" ToolTip="<%$ Translate: Bạn phải chọn quận/huyện %>"> 
                                </asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Email công ty")%>
                                <span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadTextBox runat="server" ID="cv_txtEmailCaNhanCongTy">
                                </tlk:RadTextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="cv_txtEmailCaNhanCongTy"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Email công ty %>" ToolTip="<%$ Translate: Bạn phải nhập Email công ty %>">
                                </asp:RequiredFieldValidator>--%>
                            </td>
                            <td class="lb">
                                <%# Translate("Email cá nhân")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtEmailCaNhan" runat="server">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Điện thoại di dộng")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtSoDienThoaiCaNhan" runat="server">
                                    <ClientEvents OnKeyPress="keyPress" />
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Điện thoại cố định")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtSoDienThoaiCoDinh" runat="server">
                                    <ClientEvents OnKeyPress="keyPress" />
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Mã số thuế cá nhân")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtMST" runat="server">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Ngày cấp")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker runat="server" ID="rdNgayCapMST">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Nơi cấp")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtNoiCapMST" runat="server">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Người liên hệ(Gấp)")%>
                            </td>
                            <td>
                                <tlk:RadTextBox runat="server" ID="txtContactInstancy">
                                </tlk:RadTextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtContactInstancy"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Người liên hệ khi cần %>"
                                    ToolTip="<%$ Translate:  Bạn phải nhập Người liên hệ khi cần %>">
                                </asp:RequiredFieldValidator>--%>
                            </td>
                            <td class="lb" style="width: 130px;">
                                <%# Translate("Địa chỉ")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadTextBox runat="server" ID="txtRela_Or_Address">
                                </tlk:RadTextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtRela_Or_Address"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Địa chỉ người liên hệ %>"
                                    ToolTip="<%$ Translate:  Bạn phải nhập Địa chỉ người liên hệ %>">
                                </asp:RequiredFieldValidator>--%>
                            </td>
                            <td class="lb" style="width: 150px;">
                                <%# Translate("SĐT(Người liên hệ)")%>
                            </td>
                            <td>
                                <tlk:RadTextBox runat="server" ID="txtContactPersonPhone">
                                </tlk:RadTextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtContactPersonPhone"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Số điện thoại người liên hệ %>"
                                    ToolTip="<%$ Translate:  Bạn phải nhập Số điện thoại người liên hệ %>">
                                </asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset style="width: auto; height: auto">
                    <legend><b>
                        <%# Translate("Nhóm giấy tờ tùy thân")%>
                    </b></legend>
                    <table class="table-form">
                        <tr>
                            <td class="lb">
                                <%# Translate("Số hộ chiếu")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtPassport" runat="server">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Ngày cấp")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker runat="server" ID="rdPassport">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Ngày hết hạn")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker runat="server" ID="rdPassportEnd">
                                </tlk:RadDatePicker>
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="rdPassportEnd"
                                    Type="Date" ControlToCompare="rdPassport" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>"
                                    ToolTip="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Nơi cấp")%>
                            </td>
                            <td colspan="5">
                                <tlk:RadTextBox ID="txtPassportNoiCap" runat="server" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Visa")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtSoViSa" runat="server">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Ngày cấp")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdNgayCapViSa" runat="server">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Ngày hết hạn")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdNgayHetHanVisa" runat="server">
                                </tlk:RadDatePicker>
                                <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="rdNgayHetHanVisa"
                                    Type="Date" ControlToCompare="rdNgayCapViSa" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>"
                                    ToolTip="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>"></asp:CompareValidator>

                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Nơi cấp")%>
                            </td>
                            <td colspan="5">
                                <tlk:RadTextBox ID="txtNoiCapVisa" runat="server" Width="100%">
                                </tlk:RadTextBox>
                                <asp:RegularExpressionValidator ID="cv_revtxtNoiCapVisa" runat="server" ErrorMessage="Chuỗi ký tự quá dài"
                                    ControlToValidate="txtNoiCapVisa" ValidationExpression="^.{1,1000}">
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="lb">
                                <%# Translate("Số thẻ VN Airlines")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtVNAirlines" runat="server">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Ngày cấp thẻ")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdVNANgayCap" runat="server">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Ngày hết hạn")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdVNAHetHan" runat="server">
                                </tlk:RadDatePicker>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="lb">
                                <%# Translate("Nơi cấp")%>
                            </td>
                            <td colspan="5">
                                <tlk:RadTextBox ID="txtVNANoiCap" runat="server" Width="100%">
                                </tlk:RadTextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Chuỗi ký tự quá dài"
                                    ControlToValidate="txtVNANoiCap" ValidationExpression="^.{1,1000}">
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Số sổ lao động")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtSoLaoDong" runat="server">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Ngày cấp")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdLaoDongNgayCap" runat="server">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Ngày hết hạn")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdLaoDongHetHan" runat="server">
                                </tlk:RadDatePicker>
                                <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="rdLaoDongHetHan"
                                    Type="Date" ControlToCompare="rdLaoDongNgayCap" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>"
                                    ToolTip="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Nơi cấp")%>
                            </td>
                            <td colspan="5">
                                <tlk:RadTextBox ID="txtLaoDongNoiCap" runat="server" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Giấy phép lao động")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtGiayPhepLaoDong" runat="server">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Từ ngày")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdGiayPhepLaoDongTyNgay" runat="server">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Đến ngày")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdGiayPhepLaoDongDenNgay" runat="server">
                                </tlk:RadDatePicker>
                                <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="rdGiayPhepLaoDongDenNgay"
                                    Type="Date" ControlToCompare="rdGiayPhepLaoDongTyNgay" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>"
                                    ToolTip="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Thẻ tạm trú")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtTheTamTru" runat="server">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Từ ngày")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdTheTamTruTuNgay" runat="server">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Đến ngày")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdTheTamTruDenNgay" runat="server">
                                </tlk:RadDatePicker>
                                <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="rdTheTamTruDenNgay"
                                    Type="Date" ControlToCompare="rdTheTamTruTuNgay" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>"
                                    ToolTip="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>"></asp:CompareValidator>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </tlk:RadPageView>
            <tlk:RadPageView ID="rpvEmpDegree" runat="server" Width="100%">
                <fieldset>
                    <legend>
                        <%# Translate("Thông tin cá nhân")%>
                    </legend>
                    <table class="table-form">
                        
                        <tr>
                            <td class="lb">
                                <%# Translate("Trình độ văn hóa")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboTrinhDoVanHoa">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Trình độ học vấn")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboTrinhDoHocVan">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Trình độ chuyên môn")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboTrinhDoChuyenMon">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Trường học")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboTruongHoc">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Chuyên ngành")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboChuyenNganh">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Từ ngày")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker runat="server" ID="txtEduDateStart">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Đến ngày")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker runat="server" ID="txtEduDateEnd">
                                </tlk:RadDatePicker>
                                <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtEduDateEnd"
                                    Type="Date" ControlToCompare="txtEduDateStart" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>"
                                    ToolTip="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Năm tốt nghiệp")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox runat="server" ID="txtYearGra">
                                    <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1"
                                        DecimalSeparator="." />
                                    <ClientEvents OnBlur="displayDecimalFormat" OnLoad="displayDecimalFormat" OnValueChanged="displayDecimalFormat" />
                                </tlk:RadNumericTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Bằng cấp")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboBangCap">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Xếp loại")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboXepLoai">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Điểm tốt ngiệp")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="txtDiemTotNghiep" runat="server">
                                    <NumberFormat AllowRounding="false" GroupSeparator="" />
                                </tlk:RadNumericTextBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>
                        <%# Translate("Trình độ tin học")%>
                    </legend>
                    <table class="table-form">
                        <tr >
                            <td class="lb">
                                <%# Translate("Tin học 1")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboDegreeTrinhDo1">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb" >
                                <%# Translate("Trình độ tin học 1")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboChungchi">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb" style="display:none">
                                <%# Translate("Loại chứng chỉ")%>
                            </td>
                            <td  style="display:none">
                                <tlk:RadTextBox ID="txtDegreeChungChi1" runat="server">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb" >
                                <%# Translate("Điểm số/Xếp loại 1")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="rnDegreeDiemSoXepLoai1" runat="server" MinValue="0" AutoPostBack="false"
                                    SkinID="Decimal">
                                </tlk:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr >
                            <td class="lb">
                                <%# Translate("Tin học 2")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboDegreeTrinhDo2">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Trình độ tin học 2")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboChungchi2">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Điểm số/Xếp loại 2")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="rnDegreeDiemSoXepLoai2" runat="server" MinValue="0" AutoPostBack="false"
                                    SkinID="Decimal">
                                </tlk:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr >
                            <td class="lb">
                                <%# Translate("Tin học 3")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboDegreeTrinhDo3">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Trình độ tin học 3")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboChungchi3">
                                </tlk:RadComboBox>
                            </td>
                            <%-- sua lai trương này cho dúng yêu cầu--%>
                            <td class="lb">
                                <%# Translate("Điểm số/Xếp loại 3")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="rnDegreeDiemSoXepLoai3" runat="server" MinValue="0" AutoPostBack="false"
                                    SkinID="Decimal">
                                </tlk:RadNumericTextBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>
                        <%# Translate("Trình độ ngoại ngữ")%>
                    </legend>
                    <table class="table-form">
                        <tr>
                            <td class="lb">
                                <%# Translate("Ngoại ngữ 1")%>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cboNgoaNgu1" runat="server">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb" style="display:none">
                                <%# Translate("Trình độ ngoại ngữ 1")%>
                            </td>
                            <td style="display:none">
                                <tlk:RadTextBox ID="txtTDNNNgoaiNgu1" runat="server">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Trình độ ngoại ngữ 1")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboTDNNTrinhDo1">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Điểm số/Xếp loại 1")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="rnTDNNDiem1" runat="server" MinValue="0" AutoPostBack="false"
                                    SkinID="Decimal">
                                </tlk:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Ngoại ngữ 2")%>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cboNgoaiNgu2" runat="server">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Trình độ ngoại ngữ 2")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboTDNNTrinhDo2">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Điểm số/Xếp loại 2")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="rnTDNNDiem2" runat="server" MinValue="0" AutoPostBack="false"
                                    SkinID="Decimal">
                                </tlk:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Ngoại ngữ 3")%>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cboNgoaiNgu3" runat="server">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Trình độ ngoại ngữ 3")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboTDNNTrinhDo3">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Điểm số/Xếp loại 3")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="rnTDNNDiem3" runat="server" MinValue="0" AutoPostBack="false"
                                    SkinID="Decimal">
                                </tlk:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr style="display:none">
                            <td class="lb" >
                                <%# Translate("Kỹ năng")%>
                            </td>
                            <td colspan="5">
                                <tlk:RadTextBox ID="txtEduSkill" runat="server" TextMode="MultiLine" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </tlk:RadPageView>
            <tlk:RadPageView ID="rpvIdExpect" runat="server" Width="100%">
                <fieldset>
                    <legend>
                        <%# Translate("Thông tin nguyện vọng")%>
                    </legend>
                    <table class="table-form">
                        <tr>
                            <td class="lb">
                                <%# Translate("Mức lương thử việc")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="txtExpectMucLuongThuViec" runat="server" MinValue="0"
                                    AutoPostBack="false" SkinID="Money">
                                </tlk:RadNumericTextBox>
                            </td>
                            <td class="lb" style="width: 150px">
                                <%# Translate("Mức lương chính thức")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="txtExpectMucLuongChinhThuc" runat="server" MinValue="0"
                                    AutoPostBack="false" SkinID="Money">
                                </tlk:RadNumericTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Địa điểm làm việc")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtWORK_LOCATION" runat="server">
                                </tlk:RadTextBox>
                            </td>
                             <td class="lb"  >
                                <%# Translate("Thời gian làm việc")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboExpectThoiGianLamViec" SkinID="LoadDemand"
                                    Visible="true">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Ngày bắt đầu")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker runat="server" ID="txtExpectNgayBatDau">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                            </td>
                            <td>
                            </td>
                            <td class="lb">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Đề nghị khác")%>
                            </td>
                            <td colspan="5">
                                <tlk:RadTextBox ID="txtExpectDeNghiKhac" runat="server" TextMode="MultiLine" Width="100%"
                                    SkinID="TextBox1023">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </tlk:RadPageView>
            <tlk:RadPageView ID="rpvOtherInfo" runat="server" Width="100%">
                <fieldset>
                    <legend>
                        <%# Translate("Thông tin tổ chức chính trị, xã hội")%>
                    </legend>
                    <table class="table-form">
                        <tr>
                            <td colspan="6">
                                <asp:CheckBox ID="chkDoanVien" runat="server" Text="<%$ Translate: Đoàn viên %>" />
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Ngày vào Đoàn")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdNgayVaoDoan" runat="server">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Chức vụ Đoàn")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboChucVuDoan" SkinID="LoadDemand">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Ngày nhận chức vụ Đoàn")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdNgayNhanChucVuDoan" runat="server" Width="160px">
                                </tlk:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Nơi vào")%>
                            </td>
                            <td colspan="4">
                                <tlk:RadTextBox ID="txtNoiVaoDoan" runat="server" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:CheckBox ID="chkDoanPhi" runat="server" Text="<%$ Translate: Đoàn phí %>" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:CheckBox ID="chkDangVien" runat="server" Text="<%$ Translate: Đảng viên %>" />
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Ngày vào Đảng")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdNgayVaoDang" runat="server" Width="160px">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Chức vụ Đảng")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboChucVuDang" SkinID="LoadDemand">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Ngày nhận chức Đảng")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdNgayNhanChucVuDang" runat="server" Width="160px">
                                </tlk:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Chức vụ kiêm nhiệm")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboDangKiemNhiem" SkinID="LoadDemand">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Nơi vào")%>
                            </td>
                            <td colspan="2">
                                <tlk:RadTextBox ID="txtNoiVaoDang" runat="server" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkDangPhi" runat="server" Text="<%$ Translate: Đảng phí %>" />
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Cấp ủy hiện tại")%>
                            </td>
                            <td colspan="5">
                                <tlk:RadTextBox ID="txtCapUyHienTai" runat="server" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Cấp ủy kiêm nhiệm")%>
                            </td>
                            <td colspan="5">
                                <tlk:RadTextBox ID="txtCapUyKiemNhiem" runat="server" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:CheckBox ID="chkCongDoanPhi" runat="server" Text="<%$ Translate: Công đoàn phí %>" />
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Ngày vào Công đoàn")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdNgayVaoCongDoan" runat="server">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Nơi vào công đoàn")%>
                            </td>
                            <td colspan="3">
                                <tlk:RadTextBox ID="txtNoiVaoCongDoan" runat="server" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:CheckBox ID="chkCuuChienBinh" runat="server" Text="<%$ Translate: Tham gia cựu chiến binh %>" />
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Ngày vào hội cứu chiến binh")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdNgayVaoHoiCuuChienBinh" runat="server">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Chức vụ")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboChucVuCuuChienBinh" SkinID="LoadDemand">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Nơi vào hội cựu chiến binh")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtNoiVaoHoiCuuChienBinh" runat="server">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Ngày nhập ngũ")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdNgayNhapNgu" runat="server">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Ngày xuất ngũ")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdNgayXuatNgu" runat="server">
                                </tlk:RadDatePicker>
                                <asp:CompareValidator ValidationGroup="EmpProfile" ID="CompareValidator1" runat="server"
                                    ErrorMessage="<%$ Translate: Ngày xuất ngũ phải lớn hơn ngày nhập ngũ %>" ToolTip="<%$ Translate: Ngày xuất ngũ phải lớn hơn ngày nhập ngũ %>"
                                    ControlToValidate="rdNgayXuatNgu" ControlToCompare="rdNgayNhapNgu" Operator="GreaterThanEqual"
                                    Type="Date">
                                </asp:CompareValidator>
                            </td>
                            <td class="lb">
                                <%# Translate("Quân hàm, chức vụ cao nhất")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtQuanHamChucVuCaoNhat" runat="server">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Thành phần gia đình")%>
                            </td>
                            <td colspan="5">
                                <tlk:RadTextBox ID="txtTPGiaDinh" runat="server" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Danh hiệu được phong")%>
                            </td>
                            <td colspan="5">
                                <tlk:RadTextBox ID="txtDanhHieuDuocPhong" runat="server" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Nghề nghiệp bản thân")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtCareer" runat="server">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Sở trường công tác")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtSoTruongCongTac" runat="server">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Công tác lâu nhất")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtCongTacLauNhat" runat="server">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Lý luận chính trị")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboLyLuanChinhTri" SkinID="LoadDemand">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Quản lý nhà nước")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboQuanLyNhaNuoc" SkinID="LoadDemand">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Thương binh")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboThuongBinh" SkinID="LoadDemand">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Gia đình chính sách")%>
                            </td>
                            <td colspan="5">
                                <tlk:RadComboBox runat="server" ID="cboGDChinhSach" SkinID="LoadDemand">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset style="display:none">
                    <legend>
                        <%# Translate("Thông tin người thân")%>
                    </legend>
                    <table class="table-form">
                        <tr>
                            <td class="lb">
                                <%# Translate("Họ tên")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtNT_FullName" runat="server">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Mối quan hệ")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="rcbNT_Relation">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Số điện thoại")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtNT_SDT" runat="server">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Địa chỉ")%>
                            </td>
                            <td colspan="5">
                                <tlk:RadTextBox ID="txtNT_DiaChi" runat="server" SkinID="Textbox1023" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset style="display:none">
                    <legend>
                        <%# Translate("Thông tin người giới thiệu")%>
                    </legend>
                    <table class="table-form">
                        <tr>
                            <td class="lb">
                                <%# Translate("Họ tên")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtNGT_Fullname" runat="server">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Số điện thoại")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtNGT_SDT" runat="server">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Địa chỉ")%>
                            </td>
                            <td colspan="5">
                                <tlk:RadTextBox ID="txtNGT_DiaChi" runat="server" SkinID="Textbox1023" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>
                        <%# Translate("Thông tin tài khoản")%>
                    </legend>
                    <table class="table-form">
                        <tr>
                            <td class="lb">
                                <%# Translate("Tên người thụ hưởng")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtTKNguoiThuHuong" runat="server">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Số TK chuyển khoản")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="txtTKTKChuyenKhoan" runat="server" MinValue="0">
                                    <NumberFormat AllowRounding="false" GroupSeparator="" />
                                </tlk:RadNumericTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Ngày hiệu lực")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdpTKNgayHieuLuc" runat="server">
                                </tlk:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Ngân hàng")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboTKNganHang" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                    OnClientItemsRequesting="OnClientItemsRequesting">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Chi nhánh ngân hàng")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboTKChiNhanhNganHang" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                    OnClientItemsRequesting="OnClientItemsRequesting">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                            </td>
                            <td>
                                <asp:CheckBox ID="other_cbxThanhToanQuaNH" runat="server" AutoPostBack="false" CausesValidation="False"
                                    ForeColor="White" /><%# Translate("Thanh toán qua ngân hàng")%>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>
                        <%# Translate("Thông tin sức khỏe")%>
                    </legend>
                    <table class="table-form">
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
                                <%# Translate("Răng hàm mặt")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtRangHamMat" runat="server">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Nhóm máu")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtNhomMau" runat="server" SkinID="Textbox50">
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
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Loại sức khỏe")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboLoaiSucKhoe">
                                </tlk:RadComboBox>
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
                    </table>
                </fieldset>
            </tlk:RadPageView>
        </tlk:RadMultiPage>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrgSL" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrgIns" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadScriptBlock runat="server" ID="ScriptBlock">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=valSum.ClientID %>").addClass("msg-error");
        });
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function cusID_Place(oSrc, args) {
            var cbo = $find("<%# cboCMNDPlace.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "SAVE") {
            }
            else if (item.get_commandName() == "PRINT" || item.get_commandName() == "UNLOCK") {
                enableAjax = false;
            }
        }
        function OpenEditTransfer(sender, args) {
            enableAjax = false;
        }
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function CloseWindow() {
            var oWindow = GetRadWindow();
            if (oWindow) oWindow.close(1);
        }

        function LoadEmp(strEmpCode, strMessage, strCurrentPlaceHolder) {
            var oArg = new Object();
            var oWnd = GetRadWindow();
            if (strMessage != '') {
                var notify = noty({ text: strMessage, dismissQueue: true, type: 'success' });
                setTimeout(function () {
                    oWnd.setUrl('Dialog.aspx?mid=Recruitment&fid=ctrlRC_CanDtl&group=Business&Can=' + strEmpCode + '&state=Normal&Place=' + strCurrentPlaceHolder + '&noscroll=1&reload=1');
                }, 2000);
            } else {
                oWnd.setUrl('Dialog.aspx?mid=Recruitment&fid=ctrlRC_CanDtl&group=Business&Can=' + strEmpCode + '&state=Normal&Place=' + strCurrentPlaceHolder + '&noscroll=1&reload=1');
            }
        }

        //Function này để hủy postback khi nhập lương cơ bản rồi enter 
        //Lỗi này chỉ gặp ở trình duyệt chrome và ie.
        function OnKeyBSPress(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                eventArgs.set_cancel(true);
            }
        }
        function displayDecimalFormat(sender, args) {
            sender.set_textBoxValue(sender.get_value().toString());
        }
        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {
                case '<%= cboTKNganHang.ClientID %>':
                    cbo = $find('<%= cboTKChiNhanhNganHang.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                default:
                    break;
            }
        }
        function clearSelectRadcombo(cbo) {
            if (cbo) {
                cbo.clearItems();
                cbo.clearSelection();
                cbo.set_text('');
            }
        }
        function clearSelectRadtextbox(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }
        function OnClientItemsRequesting(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;
            switch (id) {
                case '<%= cboTKChiNhanhNganHang.ClientID %>':
                    cbo = $find('<%= cboTKNganHang.ClientID %>');
                    value = cbo.get_value();
                    break;
                default:
                    break;
            }

            if (!value) {
                value = 0;
            }
            var context = eventArgs.get_context();
            context["valueCustom"] = value;
            context["value"] = sender.get_value();

        }

        function keyPress(sender, args) {
            var text = sender.get_value() + args.get_keyCharacter();
            if (!text.match('^[0-9]+$'))
                args.set_cancel(true);
        }
    </script>
</tlk:RadScriptBlock>

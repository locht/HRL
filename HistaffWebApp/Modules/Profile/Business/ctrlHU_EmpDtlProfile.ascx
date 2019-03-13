<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlProfile.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlProfile" %>
<%@ Import Namespace="Common" %>
<%@ Register Src="../List/ctrlHU_TitleConcurrent.ascx" TagName="ctrlHU_TitleConcurrent"
    TagPrefix="Profile" %>
<asp:PlaceHolder ID="phPopupOrg" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phPopupDirect" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phPopupLevel" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
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
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal"
    SkinID="Demo">
    <tlk:RadPane ID="ToolbarPane" runat="server" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="clientButtonClicking"
            ValidationGroup="EmpProfile" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Height="13%" Scrolling="X">
        <asp:Panel runat="server" ID="Panel1">
            <table class="table-form" style="width: 99%" onkeydown="return (event.keyCode!=13)">
                <tr>
                    <td class="lbCom">
                        <%= Translate("Chức danh:")%>
                    </td>
                    <td class="infoCom">
                        <asp:Label ID="lblChucDanh" runat="server"></asp:Label>
                    </td>
                    <td class="lbCom">
                        <%= Translate("Quản lý trực tiếp:")%>
                    </td>
                    <td class="infoCom">
                        <a href='/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=<%=_DIRECT_MANAGER%>&state=Normal'
                            target="_blank" title="Xem thông tin quản lý trực tiếp">
                            <asp:Label ID="lblQLTT" runat="server"></asp:Label>
                        </a>
                    </td>
                </tr>
                <tr>
                    <td class="lbCom">
                        <%= Translate("Phòng ban:")%>
                    </td>
                    <td class="infoCom">
                        <%-- <a href='/Default.aspx?mid=Organize&fid=ctrlHU_Organization&group=Business&id=<%=_ORG_ID%>'
                            target="_blank" title="Xem thông tin phòng ban">--%>
                        <asp:Label ID="lblPhongBan" runat="server"></asp:Label>
                        <%--  </a>--%>
                    </td>
                    <td class="lbCom">
                        <%= Translate("Quản lý trên một cấp:")%>
                    </td>
                    <td class="infoCom">
                        <a href='/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=<%=_LEVEL_MANAGER%>&state=Normal'
                            target="_blank" title="Xem thông tin quản lý trên một cấp">
                            <asp:Label ID="lblQLTMC" runat="server"></asp:Label>
                        </a>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </tlk:RadPane>
    <tlk:RadPane ID="DetailPane" runat="server" Scrolling="None" Height="73%">
        <tlk:RadTabStrip ID="rtabProfileInfo" runat="server" CausesValidation="false" MultiPageID="RadMultiPage1"
            AutoPostBack="false">
            <Tabs>
                <tlk:RadTab runat="server" ID="rtIdEmpInfo" PageViewID="rpvEmpInfo" Text="<%$ Translate: Thông tin hồ sơ %>"
                    Selected="True">
                </tlk:RadTab>
                <tlk:RadTab runat="server" ID="rtIdEmpPaper" PageViewID="rpvEmpPaper" Text="<%$ Translate: Giấy tờ cần nộp %>">
                </tlk:RadTab>
                <tlk:RadTab runat="server" ID="rtIdTitleConcurrent" PageViewID="rpvEmpTitleConcurrent"
                    Text="<%$ Translate: Chức danh kiêm nhiệm %>">
                </tlk:RadTab>
            </Tabs>
        </tlk:RadTabStrip>
        <tlk:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" runat="server" Width="100%"
            ScrollBars="Auto" Height="90%">
            <tlk:RadPageView ID="rpvEmpInfo" runat="server" Width="100%">
                <table style="width: 99%" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td colspan="6">
                            <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary"
                                ValidationGroup="EmpProfile" />
                        </td>
                    </tr>
                </table>
                <table class="table-form" style="width: 99%" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lbInfo">
                            <%# Translate("Mã nhân viên")%>
                        </td>
                        <td>
                            <asp:HiddenField ID="hidID" runat="server" />
                            <asp:HiddenField ID="hidIsTer" runat="server" />
                            <asp:HiddenField ID="hidContractID" runat="server" />
                            <asp:HiddenField ID="hidWorkingID" runat="server" />
                            <asp:HiddenField ID="hidPrevious" runat="server" />
                            <asp:HiddenField ID="hidNext" runat="server" />
                            <asp:HiddenField ID="hidOrgID" runat="server" />
                            <asp:HiddenField ID="hidDirectManager" runat="server" />
                            <asp:HiddenField ID="hidLevelManager" runat="server" />
                            <tlk:RadTextBox ID="txtEmpCODE" runat="server" ReadOnly="true" Width="100px">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lbInfo">
                            <%# Translate("Họ và tên lót")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <%--<tlk:RadTextBox ID="txtDangHong" runat="server" Width="120px">
                            </tlk:RadTextBox>--%>
                            <tlk:RadTextBox ID="txtFirstNameVN" runat="server" Width="120px" ClientEvents-OnValueChanged="txtFirstNameVNOnValueChanged">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="reqFirstNameVN" ControlToValidate="txtFirstNameVN"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập họ và tên lót %>" ToolTip="<%$ Translate: Bạn phải nhập họ và tên lót %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lbInfo">
                            <%# Translate("Tên")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtLastNameVN" runat="server" Width="100px" ClientEvents-OnValueChanged="txtLastNameVNOnValueChanged">
                            </tlk:RadTextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="reqLastNameVN" ControlToValidate="txtLastNameVN"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên %>" ToolTip="<%$ Translate: Bạn phải nhập tên %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lbInfo">
                            <%# Translate("Mã quẹt thẻ")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtTimeID" runat="server" Width="100px">
                            </tlk:RadTextBox>
                            <asp:CustomValidator ValidationGroup="EmpProfile" ID="cusTimeID" runat="server" ErrorMessage="<%$ Translate: Mã quẹt thẻ đã tồn tại %>"
                                ToolTip="<%$ Translate: Mã quẹt thẻ đã tồn tại %>">
                            </asp:CustomValidator>
                        </td>
                        <td class="lbInfo">
                            <%# Translate("Tình trạng nhân viên")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboEmpStatus" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                OnClientItemsRequesting="OnClientItemsRequesting">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkSaveHistory" runat="server" Text="<%$ Translate: Có lưu lại lịch sử không? %>"
                                AutoPostBack="false" Visible="false" />
                        </td>
                    </tr>
                </table>
                <tlk:RadPanelBar runat="server" ID="RadPanelBar1" Width="100%" ExpandMode="MultipleExpandedItems"
                    OnClientItemClicking="HeightGridClick">
                    <Items>
                        <tlk:RadPanelItem Expanded="false" Text="<%$ Translate: Sơ yếu lý lịch %>">
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%" onkeydown="return (event.keyCode!=13)">
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Bộ phận")%><span class="lbReq">*</span>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="txtOrgName2" ReadOnly="true" Width="130px" />
                                            <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator1"
                                                ControlToValidate="txtOrgName2" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đơn vị %>"
                                                ToolTip="<%$ Translate: Bạn phải chọn bộ phận %>">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Phòng")%>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="txtOrgName" ReadOnly="true" />
                                        </td>

                                        <td class="lb3">
                                            <%# Translate("Ban")%>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="txtBan" ReadOnly="true" />
                                        </td>

                                         <td class="lb3">
                                            <%# Translate("Tổ")%>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="txtTo" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Chức danh")%><span class="lbReq">*</span>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="cboTitle" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            <asp:CustomValidator ValidationGroup="EmpProfile" ID="cusTitle" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Chức danh %>"
                                                ToolTip="<%$ Translate: Bạn phải chọn Chức danh  %>" ClientValidationFunction="cusTitle">
                                            </asp:CustomValidator>
                                        </td>

                                        <td class="lb3">
                                            <%# Translate("Nhóm chức danh")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="txtTitleGroup" ReadOnly="true">
                                            </tlk:RadTextBox>
                                        </td>

                                        <td class="lb3">
                                            <%# Translate("Bậc nhân sự")%><span class="lbReq"></span>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboStaffRank" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            <%--<asp:CustomValidator ValidationGroup="EmpProfile" ID="cusStaffRank" runat="server"
                                                ErrorMessage="<%$ Translate: Bạn phải chọn Cấp nhân sự %>" ToolTip="<%$ Translate: Bạn phải chọn Bậc nhân sự  %>"
                                                ClientValidationFunction="cusStaffRank">
                                            </asp:CustomValidator>--%>
                                        </td>
                                        
                                    </tr>
                                    <tr> 
                                        <td class="lb3">
                                            <%# Translate("Quản lý trực tiếp")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="txtDirectManager" ReadOnly="true" Width="130px" />
                                            <tlk:RadButton runat="server" ID="btnFindDirect" SkinID="ButtonView" CausesValidation="false">
                                            </tlk:RadButton>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Trưởng bộ phận")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="txtManager" ReadOnly="true" Width="85%" />
                                        </td>
                                        <%--<td class="lb3">
                                            <%# Translate("Quản lý trên một cấp")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="txtLevelManager" ReadOnly="true" Width="130px" />
                                            <tlk:RadButton runat="server" ID="btnFindLevel" SkinID="ButtonView" CausesValidation="false">
                                            </tlk:RadButton>
                                        </td>--%>
                                         <td class="lb3">
                                            <%# Translate("Ngày sinh")%>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker runat="server" ID="rdBirthDate">
                                            </tlk:RadDatePicker>
                                            <%--<asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="reqBirthDate" ControlToValidate="rdBirthDate"
                                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày sinh%>" ToolTip="<%$ Translate:  Bạn phải nhập ngày sinh %>">
                                            </asp:RequiredFieldValidator>--%>
                                        </td>

                                         <td class="lb3">
                                            <%# Translate("Giới tính")%><span class="lbReq">*</span>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboGender" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            <asp:CustomValidator ValidationGroup="EmpProfile" ID="cusGender" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Giới tính %>"
                                                ToolTip="<%$ Translate: Bạn phải chọn Giới tính  %>" ClientValidationFunction="cusGender">
                                            </asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        
                                       
                                       
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Nơi làm việc")%>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboWorkplace" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                           <%-- <asp:CustomValidator ValidationGroup="EmpProfile" ID="cusWorkplace" runat="server"
                                                ErrorMessage="<%$ Translate: Bạn phải chọn Nơi làm việc %>" ToolTip="<%$ Translate: Bạn phải chọn Nơi làm việc  %>"
                                                ClientValidationFunction="cusWorkplace">
                                            </asp:CustomValidator>--%>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Số CMND")%><span class="lbReq">*</span>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="txtID_NO">
                                            </tlk:RadTextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator3"
                                                ControlToValidate="txtID_NO" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập số CMND %>"
                                                ToolTip="<%$ Translate:  Bạn phải nhập số CMND %>">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ValidationGroup="EmpProfile" ID="cusNO_ID" runat="server" ErrorMessage="<%$ Translate: Mã CMND đã tồn tại trong danh sách đen, không được phép thêm mới %>"
                                                ToolTip="<%$ Translate: Mã CMND đã tồn tại trong danh sách đen, không được phép thêm mới %>">
                                            </asp:CustomValidator>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Ngày cấp")%>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker runat="server" ID="rdIDDate">
                                            </tlk:RadDatePicker>
                                            <%--<asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator2"
                                                ControlToValidate="rdIDDate" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày cấp %>"
                                                ToolTip="<%$ Translate:  Bạn phải nhập Ngày cấp %>">
                                            </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Nơi cấp")%>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboIDPlace">
                                            </tlk:RadComboBox>
                                            <%--<asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator4"
                                                ControlToValidate="cboIDPlace" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Nơi cấp %>"
                                                ToolTip="<%$ Translate:  Bạn phải nhập Nơi cấp %>">
                                            </asp:RequiredFieldValidator>--%>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Quốc tịch")%>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboNationlity" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Dân tộc")%>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboNative" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                         <td class="lb3">
                                            <%# Translate("Nơi sinh")%>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboBIRTH_PLACE">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Tôn giáo")%>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboReligion" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Vùng bảo hiểm")%><span class="lbReq">*</span>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboInsRegion" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                >
                                            </tlk:RadComboBox>
                                            <asp:CustomValidator ValidationGroup="EmpProfile" ID="cusInsRegion" runat="server"
                                                ErrorMessage="<%$ Translate: Bạn phải chọn Vùng bảo hiểm %>" ToolTip="<%$ Translate: Bạn phải chọn Vùng bảo hiểm  %>"
                                                ClientValidationFunction="cusInsRegion">
                                            </asp:CustomValidator>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        
                                        <td class="lb3">
                                            <%# Translate("Ngày vào công ty")%>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker runat="server" ID="rdJoinDate" Enabled="false" DateInput-Enabled="false">
                                            </tlk:RadDatePicker>
                                        </td>

                                        <td class="lb3">
                                            <%# Translate("Ngày thôi việc")%>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker runat="server" ID="rdter_effect_date" Enabled="false" DateInput-Enabled="false">
                                            </tlk:RadDatePicker>
                                        </td>

                                        <td class="lb3">
                                            <%# Translate("Trạng thái nhân viên")%>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboWorkStatus" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Địa chỉ thường trú")%><span class="lbReq">*</span>
                                        </td>
                                        <td colspan="5">
                                            <tlk:RadTextBox ID="txtPerAddress" runat="server" Width="100%">
                                            </tlk:RadTextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator5"
                                                ControlToValidate="txtPerAddress" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Địa chỉ thường trú %>"
                                                ToolTip="<%$ Translate:  Bạn phải nhập Địa chỉ thường trú %>">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Thành phố")%><span class="lbReq">*</span>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboPer_Province" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            <asp:CustomValidator ValidationGroup="EmpProfile" ID="cusPer_Province" runat="server"
                                                ErrorMessage="<%$ Translate: Bạn phải chọn Thành phố %>" ToolTip="<%$ Translate: Bạn phải chọn Thành phố  %>"
                                                ClientValidationFunction="cusPer_Province">
                                            </asp:CustomValidator>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Quận huyện")%><span class="lbReq">*</span>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboPer_District" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            <asp:CustomValidator ValidationGroup="EmpProfile" ID="cusPer_District" runat="server"
                                                ErrorMessage="<%$ Translate: Bạn phải chọn Quận huyện %>" ToolTip="<%$ Translate: Bạn phải chọn Quận huyện  %>"
                                                ClientValidationFunction="cusPer_District">
                                            </asp:CustomValidator>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Xã phường")%>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboPer_Ward" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Địa chỉ tạm trú")%><span class="lbReq">*</span>
                                        </td>
                                        <td colspan="5">
                                            <tlk:RadTextBox ID="txtNavAddress" runat="server" Width="100%">
                                            </tlk:RadTextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator2"
                                                ControlToValidate="txtNavAddress" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập đại chỉ tạm trú  %>"
                                                ToolTip="<%$ Translate:  Bạn phải nhập đại chỉ tạm trú %>">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Thành phố")%>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboNav_Province" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Quận huyện")%>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboNav_District" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Xã phường")%>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboNav_Ward" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>
                        <tlk:RadPanelItem Expanded="false" Text="<%$ Translate: Thông tin hợp đồng lao động mới nhất %>">
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%" onkeydown="return (event.keyCode!=13)">
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Số hợp đồng")%>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtContractNo" runat="server" ReadOnly="true">
                                            </tlk:RadTextBox>
                                        </td>

                                        <td class="lb3">
                                            <%# Translate("Loại hợp đồng")%>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtContractType" runat="server" ReadOnly="true">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Từ ngày")%>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadDatePicker ID="rdContractEffectDate" runat="server" Enabled="false" DateInput-Enabled="false">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Đến ngày")%>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadDatePicker ID="rdContractExpireDate" runat="server" Enabled="false" DateInput-Enabled="false">
                                            </tlk:RadDatePicker>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>
                        <tlk:RadPanelItem Expanded="false" Text="<%$ Translate: Thông tin phụ %>">
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%" onkeydown="return (event.keyCode!=13)">
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Email công ty")%>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="txtWorkEmail">
                                            </tlk:RadTextBox>
                                            <asp:RegularExpressionValidator ValidationGroup="EmpProfile" ID="regEMAIL" ControlToValidate="txtWorkEmail"
                                                ValidationExpression="^([a-zA-Z0-9_\.-]+)@([a-zA-Z0-9_\.-]+)\.([a-zA-Z\.]{2,6})$"
                                                runat="server" ErrorMessage="<%$ Translate: Định dạng Email công ty không chính xác %>"
                                                ToolTip="<%$ Translate: Định dạng Email công ty không chính xác %>"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Email cá nhân")%>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="txtPerEmail">
                                            </tlk:RadTextBox>
                                            <asp:RegularExpressionValidator ValidationGroup="EmpProfile" ID="RegularExpressionValidator1"
                                                ControlToValidate="txtPerEmail" ValidationExpression="^([a-zA-Z0-9_\.-]+)@([a-zA-Z0-9_\.-]+)\.([a-zA-Z\.]{2,6})$"
                                                runat="server" ErrorMessage="<%$ Translate: Định dạng Email cá nhân không chính xác %>"
                                                ToolTip="<%$ Translate: Định dạng Email cá nhân không chính xác %>"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Tình trạng hôn nhân")%>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="cboFamilyStatus" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Điện thoại di động")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="txtMobilePhone">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Điện thoại cố định")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="txtHomePhone">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td colspan="3">
                                            <asp:CheckBox ID="chkDoanPhi" runat="server" Text="<%$ Translate: Công đoàn phí %>" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Ngày vào đoàn")%>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdNgayVaoDoan" runat="server">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Chức vụ")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtChucVuDoan" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Nơi vào")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtNoiVaoDoan" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    
                                  <%--  <tr>
                                        <td>
                                        </td>
                                        <td colspan="3">
                                            <asp:CheckBox ID="chkDangPhi" runat="server" Text="<%$ Translate: Đảng phí %>" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Ngày vào đảng")%>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdNgayVaoDang" runat="server">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Chức vụ")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtChucVuDang" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Nơi vào")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtNoiVaoDang" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Số hộ chiếu")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtPassNo" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Ngày cấp")%>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdPassDate" runat="server">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Ngày hết hạn")%>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdPassExpireDate" runat="server">
                                            </tlk:RadDatePicker>
                                            <asp:CompareValidator ValidationGroup="EmpProfile" ID="cvalPassDate" runat="server"
                                                ErrorMessage="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>" ToolTip="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>"
                                                ControlToValidate="rdPassExpireDate" ControlToCompare="rdPassDate" Operator="GreaterThanEqual"
                                                Type="Date">
                                            </asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Nơi cấp")%>
                                        </td>
                                        <td colspan="5">
                                            <tlk:RadTextBox ID="txtPassPlace" runat="server" Width="100%">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Visa")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtVisa" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Ngày cấp")%>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdVisaDate" runat="server">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Ngày hết hạn")%>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdVisaExpireDate" runat="server">
                                            </tlk:RadDatePicker>
                                            <asp:CompareValidator ValidationGroup="EmpProfile" ID="compare_VisaDate_ExpireDate"
                                                runat="server" ToolTip="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>"
                                                ErrorMessage="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>" Type="Date"
                                                Operator="GreaterThan" ControlToCompare="rdVisaDate" ControlToValidate="rdVisaExpireDate"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Nơi cấp")%>
                                        </td>
                                        <td colspan="5">
                                            <tlk:RadTextBox ID="txtVisaPlace" runat="server" Width="100%">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Giấy phép lao động")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtWorkPermit" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Từ ngày")%>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdWorkPermitDate" runat="server">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Đến ngày")%>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdWorPermitExpireDate" runat="server">
                                            </tlk:RadDatePicker>
                                            <asp:CompareValidator ValidationGroup="EmpProfile" ID="compare_WorkPermitDate_PermitExpireDate"
                                                runat="server" ToolTip="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>"
                                                ErrorMessage="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>" Type="Date"
                                                Operator="GreaterThan" ControlToCompare="rdWorkPermitDate" ControlToValidate="rdWorPermitExpireDate"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Nơi cấp")%>
                                        </td>
                                        <td colspan="5">
                                            <tlk:RadTextBox ID="txtWorkPermitPlace" runat="server" Width="100%">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Ngành nghề")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="txtCareer">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Người liên hệ")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtContactPerson" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Số điện thoại")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtContactPersonPhone" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>
                        <tlk:RadPanelItem Expanded="false" Text="<%$ Translate: Trình độ văn hóa %>">
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%" onkeydown="return (event.keyCode!=13)">
                                    <tr>
                                        <%--<td class="lb3">
                                            <%# Translate("Tên trường")%>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox ID="cboGraduateSchool" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>--%>
                                        <td class="lb3">
                                            <%# Translate("Trình độ chuyên môn")%>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox ID="cboMajor" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Năm tốt nghiệp")%>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadNumericTextBox ID="txtNamTN" runat="server" NumberFormat-GroupSeparator="">
                                            </tlk:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Trình độ văn hóa")%>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox ID="cboAcademy" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                       <%-- <td class="lb3">
                                            <%# Translate("Hình thức đào tạo")%>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox ID="cboTrainingForm" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>--%>
                                        <td class="lb3">
                                            <%# Translate("Trình độ học vấn")%>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox ID="cboLearningLevel" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Ngoại ngữ")%>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox ID="cboLanguage" runat="server">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Trình độ ngoại ngữ")%>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox ID="cboLangLevel" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Điểm số/Xếp loại")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtLangMark" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>
                        <tlk:RadPanelItem Expanded="false" Text="<%$ Translate: Thông tin tài khoản %>">
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%" onkeydown="return (event.keyCode!=13)">
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Số tài khoản")%>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtBankNo" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Ngân hàng")%>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox ID="cboBank" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Chi nhánh")%>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox ID="cboBankBranch" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Mã số Thuế")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtPitCode" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td colspan="3">
                                            <asp:CheckBox ID="chkIs_pay_bank" runat="server" Text="<%$ Translate: Thanh toán qua ngân hàng %>" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>
                        <tlk:RadPanelItem Expanded="false" Text="<%$ Translate: Thông tin sức khỏe %>">
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%" onkeydown="return (event.keyCode!=13)">
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Chiều cao(cm)")%>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtChieuCao" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Tai mũi họng")%>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtTaiMuiHong" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Cân nặng(kg)")%>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtCanNang" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Răng hàm mặt")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtRangHamMat" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Nhóm máu")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtNhomMau" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Tim")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtTim" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Huyết áp")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtHuyetAp" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Phổi và ngực")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtPhoiNguc" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Thị lực mắt trái")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtMatTrai" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Viêm gan B")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtVienGanB" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Thị lực mắt phải")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtMatPhai" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <%# Translate("Da và Hoa liễu")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtDaHoaLieu" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Loại sức khỏe")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtLoaiSucKhoe" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <%# Translate("Ghi chú")%>
                                        </td>
                                        <td colspan="5">
                                            <tlk:RadTextBox runat="server" ID="txtGhiChuSK" Width="100%" SkinID="Textbox1023" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>
                    </Items>
                </tlk:RadPanelBar>
            </tlk:RadPageView>
            <tlk:RadPageView ID="rpvEmpPaper" runat="server" Width="100%">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="item-head" colspan="2">
                            <%# Translate("Giấy tờ cần nộp")%>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb3" style="vertical-align: top; width: 130px">
                        </td>
                        <td style="width: 530px">
                            <tlk:RadListBox ID="lstbPaper" CheckBoxes="true" runat="server" Height="180px" Width="100%"
                                OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging">
                            </tlk:RadListBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="item-head" colspan="2">
                            <%# Translate("Giấy tờ đã nộp")%>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb3" style="vertical-align: top; width: 130px">
                        </td>
                        <td style="width: 530px">
                            <tlk:RadListBox ID="lstbPaperFiled" CheckBoxes="true" runat="server" Height="180px"
                                Width="100%" OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging">
                            </tlk:RadListBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPageView>
            <tlk:RadPageView ID="rpvEmpTitleConcurrent" runat="server" Width="100%">
                <Profile:ctrlHU_TitleConcurrent runat="server" ID="ctrlHU_TitleConcurrent" />
            </tlk:RadPageView>
        </tlk:RadMultiPage>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
        
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="950px"
            Height="300px" EnableShadow="true" Behaviors="Close, Maximize, Move" Modal="true"
            ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadScriptBlock runat="server" ID="ScriptBlock">
    <script type="text/javascript">
        function cusPer_District(oSrc, args) {
            var cbo = $find("<%# cboPer_District.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusPer_Province(oSrc, args) {
            var cbo = $find("<%# cboPer_Province.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusWorkplace(oSrc, args) {
            var cbo = $find("<%# cboWorkplace.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusInsRegion(oSrc, args) {
            var cbo = $find("<%# cboInsRegion.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusGender(oSrc, args) {
            var cbo = $find("<%# cboGender.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusTitle(oSrc, args) {
            var cbo = $find("<%# cboTitle.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusStaffRank(oSrc, args) {
            var cbo = $find("<%# cboStaffRank.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function CloseWindow() {
            var oWindow = GetRadWindow();
            if (oWindow) oWindow.close();
        }

        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {
                case '<%= cboBank.ClientID %>':
                    cbo = $find('<%= cboBankBranch.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cboPer_Province.ClientID %>':
                    cbo = $find('<%= cboPer_District.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboPer_Ward.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cboNav_Province.ClientID %>':
                    cbo = $find('<%= cboNav_District.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboNav_Ward.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cboTitle.ClientID %>':
                    cbo = $find('<%= txtTitleGroup.ClientID %>');
                    clearSelectRadtextbox(cbo);
                    var item = eventArgs.get_item();
                    if (item) {
                        cbo.set_value(item.get_attributes().getAttribute("GROUP_NAME"));
                    }
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
                case '<%= cboBankBranch.ClientID %>':
                    cbo = $find('<%= cboBank.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboTitle.ClientID %>':
                    value = $get("<%= hidOrgID.ClientID %>").value;
                    break;
                case '<%= cboPer_District.ClientID %>':
                    cbo = $find('<%= cboPer_Province.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboPer_Ward.ClientID %>':
                    cbo = $find('<%= cboPer_District.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboNav_District.ClientID %>':
                    cbo = $find('<%= cboNav_Province.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboNav_Ward.ClientID %>':
                    cbo = $find('<%= cboNav_District.ClientID %>');
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

        function OnClientClose(oWnd, args) {
            var arg = args.get_argument();
            if (arg == '1') {
                location.reload();
            }
        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
        }

        function btnWageClick(sender, args) {
            var empID = $get("<%= hidID.ClientID %>").value;
            var isTer = $get("<%= hidIsTer.ClientID %>").value;
            if (!empID) {
                m = 'Bạn phải thêm nhân viên trước khi làm Hồ sơ lương';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            if (isTer) {
                m = 'Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            oWindow = radopen('/Dialog.aspx?mid=Profile&fid=ctrlHU_WageNewEdit&group=Business&empID=' + empID, "_self"); /*
            oWindow.setSize(1020, $(window).height());
            oWindow.center(); */
            oWindow.add_close(OnClientClose);
        }

        function btnContractClick(sender, args) {
            var empID = $get("<%= hidID.ClientID %>").value;
            var isTer = $get("<%= hidIsTer.ClientID %>").value;
            var oWindow;
            if (!empID) {
                m = 'Bạn phải thêm nhân viên trước khi làm hợp đồng';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            if (isTer) {
                m = 'Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }

            oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlHU_ContractNewEdit&group=Business&empid=' + empID, "_self"); /*
            oWindow.setSize(1020, $(window).height());
            oWindow.center(); */
            oWindow.add_close(OnClientClose);
        }

        function btnChangeInfoClick(sender, args) {
            var empID = $get("<%= hidID.ClientID %>").value;
            var workingID = $get("<%= hidWorkingID.ClientID %>").value;
            var contractID = $get("<%= hidContractID.ClientID %>").value;
            var isTer = $get("<%= hidIsTer.ClientID %>").value;
            if (!empID) {
                m = 'Bạn phải thêm nhân viên trước khi làm Thay đổi thông tin nhân sự';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            if (!contractID) {
                m = 'Bạn phải thêm Hợp đồng trước khi làm Thay đổi thông tin nhân sự';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            if (isTer) {
                m = 'Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            oWindow = radopen('/Dialog.aspx?mid=Profile&fid=ctrlHU_ChangeInfoNewEdit&group=Business&empID=' + empID, "_self"); /*
            var iWidtd = 1300;
            if ($(window).width() < 1300) {
                iWidtd = $(window).width();
            }
            oWindow.setSize(iWidtd, $(window).height());
            oWindow.center(); */
            oWindow.add_close(OnClientClose);
        }

        function btnTransferTripartiteClick(sender, args) {
            var empID = $get("<%= hidID.ClientID %>").value;
            var workingID = $get("<%= hidWorkingID.ClientID %>").value;
            var contractID = $get("<%= hidContractID.ClientID %>").value;
            var isTer = $get("<%= hidIsTer.ClientID %>").value;
            if (!empID) {
                m = 'Bạn phải thêm nhân viên trước khi làm Điều chuyển 3 bên';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            if (!contractID) {
                m = 'Bạn phải thêm Hợp đồng trước khi làm Điều chuyển 3 bên';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            if (isTer) {
                m = 'Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            oWindow = radopen('/Dialog.aspx?mid=Profile&fid=ctrlHU_TransferTripartiteNewEdit&group=Business&empID=' + empID + '&parentID=Emp', "_self"); /*
            oWindow.setSize(1020, $(window).height());
            oWindow.center(); */
            oWindow.add_close(OnClientClose);
        }


        function clientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "PRINT") {
                enableAjax = false;
            }
        }
        function HeightGrid() {
            var itemHeight_01 = $('#table_01').height();
            var itemHeight_02 = $('#table_02').height();
            var availHeight = $(window).height();
            var difference = availHeight - itemHeight_01 - itemHeight_02 - 35 - 20;
            $('#RadPaneData').css('height', difference + 'px');
            //alert(difference);
        }

        function HeightGridClick(sender, args) {
            HeightGrid();
            //  enumerateChildItems(args.get_item());
            //  $('#RadPaneData').css('height', 230 + 'px');
        }
        function txtFirstNameVNOnValueChanged(args) {
            var cbo = $find("<%# txtFirstNameVN.ClientID %>");
            cbo.set_value(toTitleCase(cbo.get_value()));
        }
        function txtLastNameVNOnValueChanged(args) {
            var cbo = $find("<%# txtLastNameVN.ClientID %>");
            cbo.set_value(toTitleCase(cbo.get_value()));
        }
        function toTitleCase(str) {
            return str.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase(); });
        }
    </script>
</tlk:RadScriptBlock>

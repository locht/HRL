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
    <tlk:RadPane ID="RadPane1" runat="server" Height="1%" Scrolling="X">
        <asp:Panel runat="server" ID="Panel1">
            <table class="table-form" style="width: 99%" onkeydown="return (event.keyCode!=13)">
                <tr style="display: none">
                    <td class="lbCom">
                        <asp:Label runat="server" ID="lbChucdanh" Text="Chức danh:"></asp:Label>
                    </td>
                    <td class="infoCom">
                        <asp:Label ID="lblChucDanh" runat="server"></asp:Label>
                    </td>
                    <td class="lbCom">
                        <asp:Label ID="Label2" Text="Quản lý trực tiếp" runat="server"></asp:Label>
                    </td>
                    <td class="infoCom">
                        <a href='/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=<%=_DIRECT_MANAGER%>&state=Normal'
                            target="_blank" title="Xem thông tin quản lý trực tiếp">
                            <asp:Label ID="lblQLTT" runat="server"></asp:Label>
                        </a>
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="lbCom">
                        <% Translate("Phòng ban:")%>
                    </td>
                    <td class="infoCom">
                        <%-- <a href='/Default.aspx?mid=Organize&fid=ctrlHU_Organization&group=Business&id=<%=_ORG_ID%>'
                            target="_blank" title="Xem thông tin phòng ban">--%>
                        <asp:Label ID="lblPhongBan" runat="server"></asp:Label>
                        <%--  </a>--%>
                    </td>
                    <%--<td class="lbCom">
                        <%= Translate("Quản lý trên một cấp:")%>
                    </td>
                    <td class="infoCom">
                        <a href='/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=<%=_LEVEL_MANAGER%>&state=Normal'
                            target="_blank" title="Xem thông tin quản lý trên một cấp">
                            <asp:Label ID="lblQLTMC" runat="server"></asp:Label>
                        </a>
                    </td>--%>
                </tr>
            </table>
        </asp:Panel>
    </tlk:RadPane>
    <tlk:RadPane ID="DetailPane" runat="server" Scrolling="None" Height="73%">
        <tlk:RadTabStrip ID="rtabProfileInfo" runat="server" CausesValidation="false" MultiPageID="RadMultiPage1"
            AutoPostBack="false">
            <Tabs>
                <tlk:RadTab runat="server" ID="rtIdEmpInfo" PageViewID="rpvEmpInfo" Text="Thông tin chung"
                    Selected="True">
                </tlk:RadTab>
                <tlk:RadTab runat="server" ID="rtIdEmpPaper" PageViewID="rpvEmpPaper" Text="Giấy tờ cần nộp "
                    Visible="false">
                </tlk:RadTab>
                <tlk:RadTab runat="server" ID="rtIdTitleConcurrent" PageViewID="rpvEmpTitleConcurrent"
                    Text="Chức danh kiêm nhiệm " Visible="false">
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
                            <asp:Label runat="server" ID="lbEmoCode" Text="Mã nhân viên"></asp:Label>
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
                        <%--  <td class="lbInfo">
                            <asp:Label runat="server" ID="lbEmoCode_OLD" Text="Mã cũ"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rtEmpCode_OLD" Width="100px">
                            </tlk:RadTextBox>
                        </td>--%>
                        <%-- <td class="lbInfo">
                            <asp:Label runat="server" ID="lbBookNo" Text="Số sổ BHXH"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rtBookNo" Width="100px">
                            </tlk:RadTextBox>
                        </td>--%>
                        <%-- <td class="lb3">
                            <asp:Label runat="server" ID="lbiTime" Text="Mã chấm công"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtITime">
                            </tlk:RadTextBox>
                        </td>--%>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbTimeID" Text="Mã chấm công"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtTimeID" runat="server" ReadOnly="true" Width="100px">
                            </tlk:RadTextBox>
                            <asp:CustomValidator ValidationGroup="EmpProfile" ID="cusTimeID" runat="server" ErrorMessage="Mã quẹt thẻ đã tồn tại"
                                ToolTip="Mã quẹt thẻ đã tồn tại">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbFirstNameVN" Text="Họ và tên lót"></asp:Label>
                            <span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtFirstNameVN" runat="server" Width="120px">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="reqFirstNameVN" ControlToValidate="txtFirstNameVN"
                                runat="server" ErrorMessage="Bạn phải nhập họ và tên lót" ToolTip="Bạn phải nhập họ và tên lót ">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbLastNameVN" Text="Tên"></asp:Label>
                            <span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtLastNameVN" runat="server" Width="100px">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="reqLastNameVN" ControlToValidate="txtLastNameVN"
                                runat="server" ErrorMessage="Bạn phải nhập tên" ToolTip="Bạn phải nhập tên">
                            </asp:RequiredFieldValidator>
                        </td>
                        <%-- <td class="lbInfo">
                            <asp:Label runat="server" ID="lbOtherName" Text="Tên gọi khác"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="rtOtherName" runat="server" Width="200px">
                            </tlk:RadTextBox>
                        </td>--%>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label1" Text="Loại nhân viên"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboEmpStatus" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                OnClientItemsRequesting="OnClientItemsRequesting">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbEMPLOYEE_OBJECT" Text="Đối tượng nhân viên"></asp:Label>
                            <span class="lbReq"></span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboEMPLOYEE_OBJECT" Enabled="false" SkinID="LoadDemand">
                            </tlk:RadComboBox>
                        </td>
                          <td class="lb">
                            <asp:Label runat="server" ID="Label46" Text="Đối tượng nghỉ bù"></asp:Label>
                            <span class="lbReq"></span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboCOMPENSATORY_OBJECT" Enabled="false" SkinID="LoadDemand">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbObject" Text="Đối tượng chấm công"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboObject" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIs_Hazardous" Enabled="false" runat="server" Text="<%$ Translate: Môi trường độc hại %>"
                                AutoPostBack="false" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIS_HDLD" Enabled="false" runat="server" Text="<%$ Translate: Tạm hoãn HDLD %>"
                                AutoPostBack="false" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkSaveHistory" runat="server" Text="<%$ Translate: Có lưu lại lịch sử không? %>"
                                AutoPostBack="false" Visible="false" Checked="true" />
                        </td>
                    </tr>
                </table>
                <tlk:RadPanelBar runat="server" ID="RadPanelBar1" Width="100%" ExpandMode="MultipleExpandedItems"
                    OnClientItemClicking="HeightGridClick">
                    <Items>
                        <tlk:RadPanelItem Expanded="false" Text="<%$ Translate: Thông tin công tác %>">
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%" onkeydown="return (event.keyCode!=13)">
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbOrgName2" Text="Đơn vị"></asp:Label>
                                            <span class="lbReq">*</span>
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
                                            <asp:Label runat="server" ID="lbOrg_C2" Text="Chi nhánh/ khối/ Trung tâm"></asp:Label><span
                                                class="lbReq">*</span>
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
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbTitle" Text="Chức danh"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="cboTitle" SkinID="LoadDemand" AutoPostBack="true"
                                                OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            <asp:CustomValidator ValidationGroup="EmpProfile" ID="cusTitle" runat="server" ErrorMessage="Bạn phải chọn Chức danh"
                                                ToolTip="Bạn phải chọn Chức danh" ClientValidationFunction="cusTitle">
                                            </asp:CustomValidator>
                                        </td>
                                        <td class="lb3" style="display:none">
                                            <asp:Label runat="server" ID="lbTitleGroup" Text="Nhóm chức danh"></asp:Label>
                                        </td>
                                        <td style="display:none">
                                            <tlk:RadTextBox runat="server" ID="txtTitleGroup" ReadOnly="true">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3" style="display:none">
                                            <asp:Label runat="server" ID="lbStaffRank" Text="Bậc nhân sự"></asp:Label>
                                            <span class="lbReq"></span>
                                        </td>
                                        <td style="display:none">
                                            <tlk:RadComboBox runat="server" ID="cboStaffRank" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <%--<td class="lb3">
                                            <asp:Label runat="server" ID="lbOrgName" Text="Phòng"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="txtOrgName" ReadOnly="true" />
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbBan" Text="Ban"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="txtBan" ReadOnly="true" />
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbTo" Text="Tổ"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="txtTo" ReadOnly="true" />
                                        </td>--%>
                                        <td class="lb">
                                            <asp:Label runat="server" ID="lbJobPos" Text="Vị trí công việc"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboJobPosition" Enabled="false" />
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
                                            <tlk:RadButton runat="server" ID="btnUpload" SkinID="ButtonView" CausesValidation="false"
                                                TabIndex="3" />
                                            <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải xuống%>"
                                                CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                                            </tlk:RadButton>
                                        </td>
                                        <td class="lb">
                                            <asp:Label runat="server" ID="lbProductionProcess" Text="Công đoạn sản xuất"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboProductionProcess" />
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator6"
                                                ControlToValidate="cboProductionProcess" runat="server" ErrorMessage="Bạn phải chọn công đoạn sản xuất"
                                                ToolTip="Bạn phải chọn công đoạn sản xuất">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <%--   <tr>
                                       
                                    </tr>--%>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbDirectManager" Text="Quản lý trực tiếp"></asp:Label>
                                            <%--<span class="lbReq">*</span>--%>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox ID="cboDirectManager" runat="server" AutoPostBack="true" CausesValidation="false">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbmanager" Text="Chức danh quản lý trực tiếp"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="txtmanager" ReadOnly="true" Width="85%" />
                                        </td>
                                        <td style="display: none" class="lbInfo">
                                            <asp:Label runat="server" ID="lbObjectBook" Text="Đối tượng đóng bảo hiểm"></asp:Label>
                                        </td>
                                        <td style="display: none">
                                            <tlk:RadComboBox runat="server" ID="cbObjectBook">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lbInfo">
                                            <asp:Label runat="server" ID="lbObjectLabor" Text="Đối tượng lao động"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboObjectLabor" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="reqObjectLabor" ControlToValidate="cboObjectLabor"
                                                runat="server" ErrorMessage="Bạn phải nhập đối tượng lao động" ToolTip="Bạn phải nhập đối tượng lao động">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        
                                    </tr>
                                    
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label8" Text="Ngày tính thâm niên"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker runat="server" ID="rdSeniorityDate">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbJoinDate" Text="Ngày vào làm/thử việc"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker runat="server" ID="rdJoinDate" Enabled="false" DateInput-Enabled="false">
                                            </tlk:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbContractNo" Text="Số hợp đồng"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtContractNo" runat="server" ReadOnly="true">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbContractType" Text="Loại hợp đồng"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtContractType" runat="server" ReadOnly="true">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbContractEffectDate" Text="Từ ngày"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdContractEffectDate" runat="server" Enabled="false" DateInput-Enabled="false">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbContractExpireDate" Text="Đến ngày"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdContractExpireDate" runat="server" Enabled="false" DateInput-Enabled="false">
                                            </tlk:RadDatePicker>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>
                        <tlk:RadPanelItem Expanded="false" Text="<%$ Translate: Sơ yếu lý lịch %>">
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%" onkeydown="return (event.keyCode!=13)">
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <%# Translate("Nơi đăng ký khai sinh:")%>
                                        </td>
                                        <td style="display: none">
                                            <tlk:RadTextBox runat="server" ID="txtPlaceKS" Width="100%">
                                            </tlk:RadTextBox>
                                        </td>
                                        <%--------------------------%>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbBirthDate" Text="Ngày sinh"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker runat="server" ID="rdBirthDate">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbBirth_PlaceId" Text="Nơi sinh"></asp:Label>
                                        </td>
                                        <td class="lb3">
                                            <tlk:RadComboBox runat="server" ID="cbBirth_PlaceId" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting" EnabledLoadOnDemand="True">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbGender" Text="Giới tính"></asp:Label>
                                            <span class="lbReq">*</span>
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
                                    <%--------------------------%>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNative" Text="Dân tộc"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboNative" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbReligion" Text="Tôn giáo"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboReligion" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNationlity" Text="Quốc tịch"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboNationlity" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbID_NO" Text="Số CMND"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="txtID_NO">
                                            </tlk:RadTextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator3"
                                                ControlToValidate="txtID_NO" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập số CMND %>"
                                                ToolTip="<%$ Translate:  Bạn phải nhập số CMND %>">
                                            </asp:RequiredFieldValidator>
                                            <%--<asp:CustomValidator ValidationGroup="EmpProfile" ID="cusNO_ID" runat="server" ErrorMessage="<%$ Translate: Mã CMND đã tồn tại trong danh sách đen, không được phép thêm mới %>"
                                                ToolTip="<%$ Translate: Mã CMND đã tồn tại trong danh sách đen, không được phép thêm mới %>">
                                            </asp:CustomValidator>--%>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbIDDate" Text="Ngày cấp"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker runat="server" ID="rdIDDate">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbExpireIDNO" Text="Ngày hết hạn"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker runat="server" ID="rdExpireIDNO">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbIDPlace" Text="Nơi cấp"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboIDPlace">
                                            </tlk:RadComboBox>
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
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPassDate" Text="Ngày cấp"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdPassDate" runat="server">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPassExpireDate" Text="Ngày hết hạn"></asp:Label>
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
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPassPlace" Text="Nơi cấp"></asp:Label>
                                        </td>
                                        <td colspan="5">
                                            <tlk:RadTextBox ID="txtPassPlace" runat="server" Width="100%">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPerAddress" Text="Địa chỉ thường trú"></asp:Label>
                                            <span class="lbReq">*</span>
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
                                            <asp:Label runat="server" ID="Label10" Text="Quốc gia"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboNationa_TT" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPer_Province" Text="Thành phố"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboPer_Province" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            <%-- <asp:CustomValidator ValidationGroup="EmpProfile" ID="cusPer_Province" runat="server"
                                                ErrorMessage="<%$ Translate: Bạn phải chọn Thành phố %>" ToolTip="<%$ Translate: Bạn phải chọn Thành phố  %>"
                                                ClientValidationFunction="cusPer_Province">
                                            </asp:CustomValidator>--%>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator2"
                                                ControlToValidate="cboPer_Province" runat="server" ErrorMessage="Bạn phải chọn Thành phố địa chỉ thường trú"
                                                ToolTip="Bạn phải chọn Thành phố địa chỉ thường trú">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPer_District" Text="Quận huyện"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboPer_District" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            <%--<asp:CustomValidator ValidationGroup="EmpProfile" ID="cusPer_District" runat="server"
                                                ErrorMessage="<%$ Translate: Bạn phải chọn Quận huyện %>" ToolTip="<%$ Translate: Bạn phải chọn Quận huyện  %>"
                                                ClientValidationFunction="cusPer_District">
                                            </asp:CustomValidator>--%>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator4"
                                                ControlToValidate="cboPer_District" runat="server" ErrorMessage="Bạn phải chọn Quận huyện địa chỉ thường trú"
                                                ToolTip="Bạn phải chọn Quận huyện địa chỉ thường trú">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPer_Ward" Text="Xã phường"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboPer_Ward" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPROVINCENQ_ID" Text="Nguyên quán"></asp:Label>
                                        </td>
                                        <td colspan="5">
                                            <%--<tlk:RadComboBox runat="server" ID="cbPROVINCENQ_ID" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting" EnabledLoadOnDemand="True">
                                            </tlk:RadComboBox>--%>
                                            <tlk:RadTextBox ID="txtResidence" runat="server" Width="100%">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNationlity_NQ" Text="Quốc gia"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboNationlity_NQ" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPROVINCEEMP_ID" Text="Tỉnh/Thành phố"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cbPROVINCEEMP_ID" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting" EnabledLoadOnDemand="True">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbDISTRICTEMP_ID" Text="Quận/Huyện"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cbDISTRICTEMP_ID" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting" EnabledLoadOnDemand="True">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbWARDEMP_ID" Text="Xã/Phường"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cbWARDEMP_ID" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting" EnabledLoadOnDemand="True">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNavAddress" Text="Địa chỉ tạm trú"></asp:Label>
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
                                            <tlk:RadComboBox runat="server" ID="cboNationlity_TTRU" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNav_Province" Text="Thành phố"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboNav_Province" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            <asp:CustomValidator ID="cusNav_Province" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Thành phố địa chỉ tạm trú %>"
                                                ToolTip="<%$ Translate: Bạn phải chọn Thành phố địa chỉ tạm trú %>" ClientValidationFunction="cusNav_Province">
                                            </asp:CustomValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNav_District" Text="Quận huyện"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboNav_District" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            <asp:CustomValidator ID="cusNav_District" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Quận huyện địa chỉ tạm trú %>"
                                                ToolTip="<%$ Translate: Bạn phải chọn Quận huyện địa chỉ tạm trú %>" ClientValidationFunction="cusNav_District">
                                            </asp:CustomValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNav_Ward" Text="Xã phường"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboNav_Ward" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label4" Text="Ngày vào công ty"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker runat="server" ID="rdJoinDateState" Enabled="false" DateInput-Enabled="false">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbWorkStatus" Text="Trạng thái nhân viên"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboWorkStatus" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbter_effect_date" Text="Ngày nghỉ việc"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker runat="server" ID="rdter_effect_date" Enabled="false" DateInput-Enabled="false">
                                            </tlk:RadDatePicker>
                                        </td>
                                    </tr>
                                    <%--------------------------%>
                                    <%--<tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbIDRemark" Text="Ghi chú thay đổi CMND" ></asp:Label>
                                        </td>
                                        <td colspan="5">
                                            <tlk:RadTextBox ID="txtIDRemark" runat="server" Width="100%"  > 
                                            </tlk:RadTextBox>
                                        </td>
                             
                                    </tr>--%>
                                    <tr style="display: none">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPROVINCEEMP_BRITH" Text="Tỉnh/Thành khai sinh"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboPROVINCEEMP_BRITH" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting" EnabledLoadOnDemand="True">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbDISTRICTEMP_BRITH" Text="Quận/Huyện khai sinh"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboDISTRICTEMP_BRITH" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting" EnabledLoadOnDemand="True">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbWARDEMP_BRITH" Text="Xã/Phường khai sinh"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboWARDEMP_BRITH" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting" EnabledLoadOnDemand="True">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3" style="display:none">
                                            <asp:Label runat="server" ID="lbInsRegion" Text="Vùng bảo hiểm"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td style="display:none">
                                            <tlk:RadComboBox runat="server" ID="cboInsRegion" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
                                            </tlk:RadComboBox>
                                            <%--<asp:CustomValidator ValidationGroup="EmpProfile" ID="cusInsRegion" runat="server"
                                                ErrorMessage="<%$ Translate: Bạn phải chọn Vùng bảo hiểm %>" ToolTip="<%$ Translate: Bạn phải chọn Vùng bảo hiểm  %>"
                                                ClientValidationFunction="cusInsRegion">
                                            </asp:CustomValidator>--%>
                                        </td>
                                        <td class="lb3" style="display:none">
                                            <asp:Label runat="server" ID="lbObjectIns" Text="Đối tượng bảo hiểm"></asp:Label>
                                        </td>
                                        <td style="display:none">
                                            <tlk:RadComboBox runat="server" ID="cboObjectIns" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lbInfo">
                                            <asp:Label runat="server" ID="lbBookNo" Text="Số sổ BHXH"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="rtBookNo">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPitCode" Text="Mã số Thuế"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtPitCode" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td style="display: none" colspan="3">
                                            <asp:CheckBox ID="chkIs_pay_bank" runat="server" Text="Thanh toán qua ngân hàng" />
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
                                    <tr style="display: none">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbWorkplace" Text="Nơi làm việc"></asp:Label>
                                        </td>
                                        <td colspan="5">
                                            <tlk:RadTextBox runat="server" ID="rtWorkplace" Width="100%">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbOpption1" Text="Thông tin 1"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <tlk:RadTextBox ID="rtOpption1" runat="server" Width="100%">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbOpption6" Text="Ngày tháng 1"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdOpption6" runat="server">
                                            </tlk:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbOpption2" Text="Thông tin 2"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <tlk:RadTextBox ID="rtOpption2" runat="server" Width="100%">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbOpption7" Text="Ngày tháng 2"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdOpption7" runat="server">
                                            </tlk:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbOpption3" Text="Thông tin 3"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <tlk:RadTextBox ID="rtOpption3" runat="server" Width="100%">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbOpption8" Text="Ngày tháng 3"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdOpption8" runat="server">
                                            </tlk:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbOpption4" Text="Thông tin 4"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <tlk:RadTextBox ID="rtOpption4" runat="server" Width="100%">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbOpption9" Text="Ngày tháng 4"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdOpption9" runat="server">
                                            </tlk:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbOpption5" Text="Thông tin 5"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <tlk:RadTextBox ID="rtOpption5" runat="server" Width="100%">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbOpption10" Text="Ngày tháng 5"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdOpption10" runat="server">
                                            </tlk:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>
                        <tlk:RadPanelItem Expanded="false" Text="<%$ Translate: Trình độ văn hóa %>">
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%" onkeydown="return (event.keyCode!=13)">
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbSchool" Text="Trường học"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox ID="cboGraduateSchool" runat="server">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbAcademy" Text="Trình độ văn hóa"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox ID="cboAcademy" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbLearningLevel" Text="Trình độ học vấn"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox ID="cboLearningLevel" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbMajor" Text="Trình độ chuyên môn"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox ID="cboMajor" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbBasic" Text="Vi tính"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox ID="cboBasic" runat="server">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbAppDung" Text="Trình độ tin học"></asp:Label>
                                        </td>
                                        <td>
                                            <%--<tlk:RadTextBox ID="txtAppDung" runat="server" SkinID="LoadDemand">
                                            </tlk:RadTextBox>--%>
                                            <tlk:RadComboBox ID="cboComputerRank" runat="server">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbLanguage" Text="Ngoại ngữ 1"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox ID="cboLanguage" runat="server">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbLangLevel" Text="Trình độ ngoại ngữ 1"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox ID="cboLangLevel" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbLanguage2" Text="Ngoại ngữ 2"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox ID="cboLanguage2" runat="server">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbLangLevel2" Text="Trình độ ngoại ngữ 2 "></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox ID="cboLangLevel2" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label17" Text="Bằng lái xe"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox ID="cboDriverType" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label18" Text="Bằng lái xe mô tô"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtDriverType" runat="server" SkinID="LoadDemand" Visible="false">
                                            </tlk:RadTextBox>
                                            <tlk:RadComboBox ID="cboMotoDrivingLicense" runat="server" >
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label19" Text="Thông tin bổ sung"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtNote" runat="server" SkinID="LoadDemand">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <%---------------------------------------------------%>
                                    <tr style="display: none">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNamTN" Text="Năm tốt nghiệp"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadNumericTextBox ID="txtNamTN" runat="server" NumberFormat-GroupSeparator="">
                                            </tlk:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbCertifiace" Text="Loại chứng chỉ"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox ID="cboCertificate" runat="server">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbLangMark" Text="Điểm số"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtLangMark" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbQLNN" Text="Quản lý nhà nước"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="cbQLNN">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbLLCT" Text="Lý luận chính trị"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cbLLCT">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbLangMark2" Text="Điểm số/Xếp loại"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtLangMark2" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbTDTH" Text="Trình độ tin học"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox ID="cbTDTH" runat="server">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbDiem_XL_TH" Text="Điểm số/Xếp loại"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="rtDiem_XL_TH" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNoteTDTH1" Text="Mô tả trình độ tin học 1"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtNoteTDTH1" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbTDTH2" Text="Trình độ tin học 2"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox ID="cboTDTH2" runat="server">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbDiem_XL_TH2" Text="Điểm số/Xếp loại"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtDiem_XL_TH2" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNoteTDTH2" Text="Mô tả trình độ tin học 2"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtNoteTDTH2" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>
                        <tlk:RadPanelItem Expanded="false" Text="<%$ Translate: Thông tin liên hệ %>" Style="display: none">
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%" onkeydown="return (event.keyCode!=13)">
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbCodeHouseHolds" Text="Mã hộ gia đình"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="txtCodeHouseHolds">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label6" Text="Thôn/Ấp/khu phố"></asp:Label>
                                        </td>
                                        <td colspan="5">
                                            <tlk:RadTextBox ID="txtVillage" runat="server" Width="100%">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>
                        <tlk:RadPanelItem Expanded="false" Text="<%$ Translate: Thông tin phụ %>">
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%" onkeydown="return (event.keyCode!=13)">
                                    <tr>
                                        <td class="control3" align="right">
                                            <asp:CheckBox ID="ckCHUHO" Text="Là chủ hộ" runat="server" Checked="false" onclick="enableTextbox(this.id)" />
                                        </td>
                                        <td>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNoHouseHolds" Text="Số hộ khẩu"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="txtNoHouseHolds">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbWorkEmail" Text="Email công ty"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="txtWorkEmail">
                                            </tlk:RadTextBox>
                                            <asp:RegularExpressionValidator ValidationGroup="EmpProfile" ID="regEMAIL" ControlToValidate="txtWorkEmail"
                                                ValidationExpression="^([a-zA-Z0-9_\.-]+)@([a-zA-Z0-9_\.-]+)\.([a-zA-Z\.]{2,6})$"
                                                runat="server" ErrorMessage="<%$ Translate: Định dạng Email công ty không chính xác %>"
                                                ToolTip="<%$ Translate: Định dạng Email công ty không chính xác %>"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPerEmail" Text="Email cá nhân"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="txtPerEmail">
                                            </tlk:RadTextBox>
                                            <asp:RegularExpressionValidator ValidationGroup="EmpProfile" ID="RegularExpressionValidator1"
                                                ControlToValidate="txtPerEmail" ValidationExpression="^([a-zA-Z0-9_\.-]+)@([a-zA-Z0-9_\.-]+)\.([a-zA-Z\.]{2,6})$"
                                                runat="server" ErrorMessage="<%$ Translate: Định dạng Email cá nhân không chính xác %>"
                                                ToolTip="<%$ Translate: Định dạng Email cá nhân không chính xác %>"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbContactPerson" Text="Người liên hệ"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtContactPerson" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label7" Text="Điện thoại di động"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtContactMobilePhone" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbFamilyStatus" Text="Tình trạng hôn nhân"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="cboFamilyStatus" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label12" Text="Ngày kết hôn"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdWeddingDay" runat="server">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbHomePhone" Text="Điện thoại cố định"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="txtHomePhone">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbMobilePhone" Text="Điện thoại di động"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="txtMobilePhone">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="display: none" class="lb3">
                                            <asp:CheckBox ID="chkDoanPhi" runat="server" Text="<%$ Translate: Công đoàn phí %>" />
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNgayVaoDoan" Text="Ngày vào Đoàn"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdNgayVaoDoan" runat="server">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNoiVaoDoan" Text="Nơi vào"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtNoiVaoDoan" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbChucVuDoan" Text="Chức vụ đoàn"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtChucVuDoan" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:CheckBox ID="ckCONG_DOAN" Text="Đoàn viên" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNGAY_DB_DANG" Text="Ngày vào Đảng"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker runat="server" ID="rdNGAY_VAO_DANG_DB">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPlaceDang" Text="Nơi vào Đảng"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtNoiVaoDang" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbCHUC_VU_DANG" Text="Chức vụ Đảng"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="rtCHUC_VU_DANG">
                                            </tlk:RadTextBox>
                                        </td>
                                        
                                        <td>
                                            <asp:CheckBox ID="ckDANG" Text="Cán bộ đảng" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkDangPhi" Text="Đảng phí" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                    </tr>
                                    <%----------------------------------------------%>
                                    <tr style="display: none">
                                        <td class="lb3" style="display: none">
                                            <asp:Label runat="server" ID="Label3" Text="Mối quan hệ NLH"></asp:Label>
                                        </td>
                                        <td style="display: none">
                                            <tlk:RadComboBox runat="server" ID="cboRelationNLH" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3" style="display: none">
                                            <asp:Label runat="server" ID="lbContactPersonPhone" Text="Số điện thoại NLH"></asp:Label>
                                        </td>
                                        <td style="display: none">
                                            <tlk:RadTextBox ID="txtContactPersonPhone" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label5" Text="Địa chỉ NLH"></asp:Label>
                                        </td>
                                        <td colspan="5">
                                            <tlk:RadTextBox ID="txtAddressPerContract" runat="server" Width="100%">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <%----------------------------------------------%>
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
                                            <asp:CompareValidator ValidationGroup="EmpProfile" ID="CompareValidator1" runat="server"
                                                ToolTip="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>" ErrorMessage="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>"
                                                Type="Date" Operator="GreaterThan" ControlToCompare="rdFrom_GPHN" ControlToValidate="rdTo_GPHN"></asp:CompareValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label16" Text="Nơi cấp"></asp:Label>
                                        </td>
                                        <td>
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
                                            <asp:CompareValidator ValidationGroup="EmpProfile" ID="compare_WorkPermitDate_PermitExpireDate"
                                                runat="server" ToolTip="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>"
                                                ErrorMessage="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>" Type="Date"
                                                Operator="GreaterThan" ControlToCompare="rdWorkPermitDate" ControlToValidate="rdWorPermitExpireDate"></asp:CompareValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbWorkPermitPlace" Text="Nơi cấp"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtWorkPermitPlace" runat="server" Width="100%">
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
                                        <td class="lb3" style="display:none">
                                            <asp:Label runat="server" ID="Label20" Text="Mã số thuế TNCN"></asp:Label>
                                        </td>
                                        <td style="display:none">
                                            <tlk:RadTextBox runat="server" ID="txtTNCN_NO">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbBankNo" Text="Số tài khoản ngân hàng"></asp:Label>
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
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPerson_Inheritance" Text="Tên người hưởng thụ"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="txtPerson_Inheritance">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbBank" Text="Ngân hàng"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox ID="cboBank" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbBankBranch" Text="Chi nhánh"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox ID="cboBankBranch" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbEffect_Bank" Text="Ngày hiệu lực"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdEffect_Bank" runat="server">
                                            </tlk:RadDatePicker>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>
                        <tlk:RadPanelItem Expanded="false" Text="<%$ Translate: Thông tin chính trị xã hội %>"
                            Style="display: none">
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%" onkeydown="return (event.keyCode!=13)">
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNGAY_VAO_DANG" Text="Ngày vào Đảng chính thức"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadDatePicker runat="server" ID="rdNGAY_VAO_DANG">
                                            </tlk:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="lb3">
                                        </td>
                                        <td class="control3">
                                            <asp:CheckBox ID="ckDOAN_PHI" Text="Công đoàn phí" runat="server" />
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbCHUC_VU_DOAN" Text="Chức vụ"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="rtCHUC_VU_DOAN">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNGAY_VAO_DOAN" Text="Ngày tham gia"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadDatePicker runat="server" ID="rdNGAY_VAO_DOAN">
                                            </tlk:RadDatePicker>
                                        </td>
                                    </tr>
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
                                            <tlk:RadComboBox runat="server" ID="cbHang_Thuong_Binh">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbGD_Chinh_Sach" Text="Gia đình chính sách"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="cbGD_Chinh_Sach">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbVisa" Text="Visa"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtVisa" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbVisaDate" Text="Ngày cấp"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdVisaDate" runat="server">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbVisaExpireDate" Text="Ngày hết hạn"></asp:Label>
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
                                    <tr style="display: none">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbVisaPlace" Text="Nơi cấp"></asp:Label>
                                        </td>
                                        <td colspan="5">
                                            <tlk:RadTextBox ID="txtVisaPlace" runat="server" Width="100%">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbCareer" Text="Ngành nghề"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="txtCareer">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbSkill" Text="Sở trường công tác"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="rtSkill">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>
                        <tlk:RadPanelItem Expanded="false" Text="<%$ Translate: Thông tin tham chiếu %>">
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%" onkeydown="return (event.keyCode!=13)">
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
                                            <tlk:RadButton runat="server" ID="btnPresenter" SkinID="ButtonView" CausesValidation="false">
                                            </tlk:RadButton>
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
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>
                        <tlk:RadPanelItem Expanded="false" Text="<%$ Translate: Thông tin sức khỏe %>">
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%" onkeydown="return (event.keyCode!=13)">
                                    <tr >
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbChieuCao" Text="Chiều cao(cm)"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtChieuCao" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3" style="display: none">
                                            <asp:Label runat="server" ID="lbTaiMuiHong" Text="Tai mũi họng"></asp:Label>
                                        </td>
                                        <td class="control3" style="display: none">
                                            <tlk:RadTextBox ID="txtTaiMuiHong" runat="server">
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
                                    <tr style="display: none">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbRangHamMat" Text="Răng hàm mặt"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtRangHamMat" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbTim" Text="Tim"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtTim" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbHuyetAp" Text="Huyết áp"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtHuyetAp" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPhoiNguc" Text="Phổi và ngực"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtPhoiNguc" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbMatTrai" Text="Thị lực mắt trái"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtMatTrai" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbVienGanB" Text="Viêm gan B"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtVienGanB" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbMatPhai" Text="Thị lực mắt phải"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtMatPhai" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="DaHoaLieu" Text="Da và Hoa liễu"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtDaHoaLieu" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbLoaiSucKhoe" Text="Loại sức khỏe"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtLoaiSucKhoe" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbTTSucKhoe" Text="Tình trạng sức khỏe"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="rtTTSucKhoe" runat="server">
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
                                            <tlk:RadTextBox runat="server" ID="txtGhiChuSK"  />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>
                        <tlk:RadPanelItem Expanded="false" Text="<%$ Translate: Thông tin size bảo hộ lao động %>">
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%" onkeydown="return (event.keyCode!=13)">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lbDPVP" Text="Đồng phục văn phòng"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label32" Text="Áo"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtAo" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label33" Text="Váy"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtVay" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label34" Text="Áo vest"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtAoVest" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label35" Text="Quần tây"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtQuanTay" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lbBHLD" Text="Bảo hộ lao động"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label36" Text="Áo"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtAo_bh" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label37" Text="Quần"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtQuan_bh" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label38" Text="Quần vải"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtQuanVai" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label39" Text="Giày dầu"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtGiayDau" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label40" Text="Giày nhựa"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtGiayNhua" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label41" Text="Áo thun"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtAoThun" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label42" Text="Quần thun"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtQuanThun" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label43" Text="Dép"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtDep" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label44" Text="Nón"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtNon" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label45" Text="Khác"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtKhac" runat="server" SkinID="Textbox50" Width="100%">
                                            </tlk:RadTextBox>
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
    <%--<tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
        </table>
    </tlk:RadPane>--%>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="950px"
            Height="300px" EnableShadow="true" Behaviors="Close, Maximize, Move" Modal="true"
            ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
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
        function cusNav_Province(oSrc, args) {
            var cbo = $find("<%# cboNav_Province.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusNav_District(oSrc, args) {
            var cbo = $find("<%# cboNav_District.ClientID %>");
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
                case '<%= cboPer_District.ClientID %>':
                    cbo = $find('<%= cboPer_Ward.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cboNav_Province.ClientID %>':
                    cbo = $find('<%= cboNav_District.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboNav_Ward.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cboNav_District.ClientID %>':
                    cbo = $find('<%= cboNav_Ward.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cbPROVINCEEMP_ID.ClientID %>':
                    cbo = $find('<%= cbDISTRICTEMP_ID.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cbWARDEMP_ID.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cboPROVINCEEMP_BRITH.ClientID %>':
                    cbo = $find('<%= cboDISTRICTEMP_BRITH.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboWARDEMP_BRITH.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cbDISTRICTEMP_ID.ClientID %>':
                    cbo = $find('<%= cbWARDEMP_ID.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cboDISTRICTEMP_BRITH.ClientID %>':
                    cbo = $find('<%= cboWARDEMP_BRITH.ClientID %>');
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
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
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
                case '<%= cboPer_Province.ClientID %>':
                    cbo = $find('<%= cboNationa_TT.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboPer_District.ClientID %>':
                    cbo = $find('<%= cboPer_Province.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cbPROVINCEEMP_ID.ClientID %>':
                    cbo = $find('<%= cboNationlity_NQ.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboDISTRICTEMP_BRITH.ClientID %>':
                    cbo = $find('<%= cboPROVINCEEMP_BRITH.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cbDISTRICTEMP_ID.ClientID %>':
                    cbo = $find('<%= cbPROVINCEEMP_ID.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboWARDEMP_BRITH.ClientID %>':
                    cbo = $find('<%= cboDISTRICTEMP_BRITH.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cbWARDEMP_ID.ClientID %>':
                    cbo = $find('<%= cbDISTRICTEMP_ID.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboPer_Ward.ClientID %>':
                    cbo = $find('<%= cboPer_District.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboNav_Province.ClientID %>':
                    cbo = $find('<%= cboNationlity_TTRU.ClientID %>');
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
                case '<%= cboNative.ClientID %>':
                    cbo = $find('<%= cboNative.ClientID %>');
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
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_ChangeInfoNewEdit&group=Business&empID=' + empID, "_self"); /*
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
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_TransferTripartiteNewEdit&group=Business&empID=' + empID + '&parentID=Emp', "_self"); /*
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
        function enableTextbox(checkbox) {
            document.getElementById('<%= txtNoHouseHolds.ClientID %>').disabled = !document.getElementById(checkbox).checked;
            document.getElementById('<%= txtCodeHouseHolds.ClientID %>').disabled = !document.getElementById(checkbox).checked;
        }
    </script>
</tlk:RadScriptBlock>

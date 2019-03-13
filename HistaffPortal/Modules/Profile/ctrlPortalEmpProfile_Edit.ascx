﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalEmpProfile_Edit.ascx.vb"
    Inherits="Profile.ctrlPortalEmpProfile_Edit" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidStatus" runat="server" />
<asp:HiddenField ID="hidFamilyID" runat="server" />
<tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
<asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
<b>
    <asp:Label runat="server" ID="lbStatus" ForeColor="Red"></asp:Label></b>
<table class="table-form">
    <tr>
        <td class="lb" style="width: 130px">
            <%# Translate("Số CMND")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtID_NO">
            </tlk:RadTextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtID_NO"
                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập số CMND %>" ToolTip="<%$ Translate:  Bạn phải nhập số CMND %>">
            </asp:RequiredFieldValidator>
            <asp:CustomValidator ID="cusNO_ID" runat="server" ErrorMessage="<%$ Translate: Mã CMND đã tồn tại trong danh sách đen, không được phép thêm mới %>"
                ToolTip="<%$ Translate: Mã CMND đã tồn tại trong danh sách đen, không được phép thêm mới %>">
            </asp:CustomValidator>
        </td>
        <td class="lb" style="width: 130px">
            <%# Translate("Ngày cấp")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadDatePicker runat="server" ID="rdIDDate">
            </tlk:RadDatePicker>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdIDDate"
                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày cấp %>" ToolTip="<%$ Translate:  Bạn phải nhập Ngày cấp %>">
            </asp:RequiredFieldValidator>
        </td>
        <td class="lb" style="width: 130px">
            <%# Translate("Nơi cấp")%><span class="lbReq">*</span>
        </td>
         <td>
            <tlk:RadComboBox runat="server" ID="cboIDPlace">
                                            </tlk:RadComboBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="cboIDPlace"
                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Nơi cấp %>" ToolTip="<%$ Translate:  Bạn phải nhập Nơi cấp %>">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Địa chỉ thường trú")%><span class="lbReq">*</span>
        </td>
        <td colspan="5">
            <tlk:RadTextBox ID="txtPerAddress" runat="server" Width="100%">
            </tlk:RadTextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtPerAddress"
                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Địa chỉ thường trú %>"
                ToolTip="<%$ Translate:  Bạn phải nhập Địa chỉ thường trú %>">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Thành phố")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadComboBox runat="server" ID="cboPer_Province" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                OnClientItemsRequesting="OnClientItemsRequesting">
            </tlk:RadComboBox>
            <asp:CustomValidator ID="cusPer_Province" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Thành phố %>"
                ToolTip="<%$ Translate: Bạn phải chọn Thành phố  %>" ClientValidationFunction="cusPer_Province">
            </asp:CustomValidator>
        </td>
        <td class="lb">
            <%# Translate("Quận huyện")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadComboBox runat="server" ID="cboPer_District" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                OnClientItemsRequesting="OnClientItemsRequesting">
            </tlk:RadComboBox>
            <asp:CustomValidator ID="cusPer_District" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Quận huyện %>"
                ToolTip="<%$ Translate: Bạn phải chọn Quận huyện  %>" ClientValidationFunction="cusPer_District">
            </asp:CustomValidator>
        </td>
        <td class="lb">
            <%# Translate("Xã phường")%>
        </td>
        <td>
            <tlk:RadComboBox runat="server" ID="cboPer_Ward" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                OnClientItemsRequesting="OnClientItemsRequesting">
            </tlk:RadComboBox>
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
            <tlk:RadComboBox runat="server" ID="cboNav_Province" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                OnClientItemsRequesting="OnClientItemsRequesting">
            </tlk:RadComboBox>
        </td>
        <td class="lb">
            <%# Translate("Quận huyện")%>
        </td>
        <td>
            <tlk:RadComboBox runat="server" ID="cboNav_District" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                OnClientItemsRequesting="OnClientItemsRequesting">
            </tlk:RadComboBox>
        </td>
        <td class="lb">
            <%# Translate("Xã phường")%>
        </td>
        <td>
            <tlk:RadComboBox runat="server" ID="cboNav_Ward" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                OnClientItemsRequesting="OnClientItemsRequesting">
            </tlk:RadComboBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Tình trạng hôn nhân")%>
        </td>
        <td>
            <tlk:RadComboBox runat="server" ID="cboFamilyStatus" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                OnClientItemsRequesting="OnClientItemsRequesting">
            </tlk:RadComboBox>
        </td>
    </tr>
     <tr>
        <td class="lb">
            <%# Translate("Lý do không phê duyệt")%>
        </td>
        <td colspan="5">
            <tlk:RadTextBox ID="txtReason" runat="server" Width="100%" Enabled="false">
            </tlk:RadTextBox>
        </td>
    </tr>
</table>

<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function cusPer_District(oSrc, args) {
            var cbo = $find("<%# cboPer_District.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusPer_Province(oSrc, args) {
            var cbo = $find("<%# cboPer_Province.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function clientButtonClicking(sender, args) {
           
            if (args.get_item().get_commandName() == "CANCEL") {
                OpenInNewTab('Default.aspx?mid=Profile&fid=ctrlPortalEmpProfile');
                args.set_cancel(true);
            }
        }


        function OpenInNewTab(url) {
            window.location.href = url;
        }

       

        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {
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

    </script>
</tlk:RadCodeBlock>

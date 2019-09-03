<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsManagerSunCareNewEdit.ascx.vb"
    Inherits="Insurance.ctrlInsManagerSunCareNewEdit" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="600px" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidDecisionID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidOrgAbbr" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="item-head" colspan="4">
                        <%# Translate("Thông tin chế độ bảo hiểm")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 150px">
                    <%# Translate("Mã nhân viên")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEMPLOYEE_CODE" Width="130px" SkinID="ReadOnly" runat="server"
                        Enabled="false">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqrcboCHANGE_TYPE" ControlToValidate="txtEMPLOYEE_CODE"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chưa chọn nhân viên. %>"></asp:RequiredFieldValidator>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                </td>
                <td class="lb" style="width: 120px">
                    <%# Translate("Họ và tên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEMPLOYEE_NAME" SkinID="ReadOnly" runat="server" Enabled="false">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Phòng ban")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtORG_NAME" SkinID="ReadOnly" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTITLE_NAME" SkinID="ReadOnly" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Cấp nhân sự")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCapNS" SkinID="ReadOnly" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày cấp")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdThoiDiem" runat="server" DateInput-DateFormat="dd/MM/yyyy">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="dpSTART_DATE" runat="server" DateInput-DateFormat="dd/MM/yyyy">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hết hiệu lực")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="dpEND_DATE" runat="server" DateInput-DateFormat="dd/MM/yyyy">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ToolTip="<%$ Translate: Ngày hiệu lực phải nhỏ hơn ngày hết hiệu lực %>"
                        ErrorMessage="<%$ Translate: Ngày hiệu lực phải nhỏ hơn ngày hết hiệu lực %>"
                        Type="Date" Operator="GreaterThan" ControlToCompare="dpSTART_DATE" ControlToValidate="dpEND_DATE"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Đơn vị cung cấp BH")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboLEVEL" AutoPostBack="true" runat="server" CausesValidation="false">
                    </tlk:RadComboBox>
                   <%-- <asp:CustomValidator ID="cvalLEVEL" ControlToValidate="cboLEVEL" runat="server" ErrorMessage="<%$ Translate: Nhóm hưởng không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Nhóm hưởng không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>--%>
                </td>
                <td class="lb">
                    <%# Translate("Chi phí mua")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmCOST" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                 <td class="lb">
                    <%# Translate("Mức bồi thường")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmCOSTSAL" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr style="display:none">
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtNote" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

        //mandatory for the RadWindow dialogs functionality
        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        
    </script>
</tlk:RadCodeBlock>

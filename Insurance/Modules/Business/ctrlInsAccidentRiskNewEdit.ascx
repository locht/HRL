<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsAccidentRiskNewEdit.ascx.vb"
    Inherits="Insurance.ctrlInsAccidentRiskNewEdit" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="600px" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidInsOrgID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidOrgAbbr" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="item-head" colspan="4">
                        <%# Translate("Thông tin tai nạn rủi ro")%>
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
                    <%# Translate("Vị trí công việc")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtJOB_NAME" SkinID="ReadOnly" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày sinh")%>
                </td>
                <td>
                     <tlk:RadDatePicker ID="rdBIRTH_DATE" runat="server" DateInput-DateFormat="dd/MM/yyyy" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Giới tính")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtGENDER_NAME" SkinID="ReadOnly" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Địa chỉ thường trú")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtADDRESS" SkinID="ReadOnly" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
                 <td class="lb">
                    <%# Translate("Số hợp đồng")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCONTRACT_NO" SkinID="ReadOnly" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số thứ tự HD")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtROWNUM_NO" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày ký hợp đồng")%>
                </td>
                <td>
                     <tlk:RadDatePicker ID="rdCONTRACT_SIGN_DATE" runat="server" DateInput-DateFormat="dd/MM/yyyy" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực hợp đồng")%>
                </td>
                <td>
                     <tlk:RadDatePicker ID="rdCONTRACT_START_DATE" runat="server" DateInput-DateFormat="dd/MM/yyyy" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Ngày kết thúc hợp đồng")%>
                </td>
                <td>
                     <tlk:RadDatePicker ID="rdCONTRACT_EXPIRE_DATE" runat="server" DateInput-DateFormat="dd/MM/yyyy" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Phụ lục hợp đồng")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtPLHD_CONTRACT_NO" SkinID="ReadOnly" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày ký phụ lục hợp đồng")%>
                </td>
                <td>
                     <tlk:RadDatePicker ID="rdPLHD_SIGN_DATE" runat="server" DateInput-DateFormat="dd/MM/yyyy" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực của phụ lục")%>
                </td>
                <td>
                     <tlk:RadDatePicker ID="rdPLHD_START_DATE" runat="server" DateInput-DateFormat="dd/MM/yyyy" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Ngày kết thúc của phụ lục")%>
                </td>
                <td>
                     <tlk:RadDatePicker ID="rdPLHD_EXPIRE_DATE" runat="server" DateInput-DateFormat="dd/MM/yyyy" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Đơn vị bảo hiểm")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtINS_ORG_NAME" SkinID="ReadOnly" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td></td>
                <td></td>
            </tr>
             <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtREMARK" SkinID="Textbox1023" runat="server" Width="100%">
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

<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_RecruitmentCostNewEdit.ascx.vb"
    Inherits="Recruitment.ctrlRC_RecruitmentCostNewEdit" %>
<style type="text/css">
    .ages
    {
        width: 39% !important;
        float: left;
    }
    .LevelLanguage
    {
        width: 44% !important;
        float: left;
    }
    .ages span
    {
        width: 100% !important;
    }
    .LevelLanguage div
    {
        width: 100% !important;
    }
</style>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Chi nhánh")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindOrg" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Chi nhánh %>" ToolTip="<%$ Translate: Bạn phải nhập Chi nhánh %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Năm")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtYear" runat="server" MinValue="1900" MaxValue="9999"
                        DataFormatString="{0:N0}" SkinID="Number">
                        <NumberFormat DecimalDigits="1" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rntxtYear"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn năm %>" ToolTip="<%$ Translate: Bạn phải chọn năm %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tháng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtMonth" runat="server" MinValue="1" MaxValue="12"
                        DataFormatString="{0:N0}" SkinID="Number">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rntxtMonth"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn tháng %>" ToolTip="<%$ Translate: Bạn phải chọn tháng %>"> 
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chi phí HeadHunt")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtHeadhunt" runat="server" SkinID="Money" DataFormatString="{0:N2}" onchange="javascript:Cal_Total_Money();" >
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1"
                            />
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Chi phí đăng tuyển")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtRecruitCost" runat="server" SkinID="Money" DataFormatString="{0:N2}" onchange="javascript:Cal_Total_Money();" >
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1"
                            />
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Thương hiệu tuyển dụng")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtTrademark" runat="server" SkinID="Money" DataFormatString="{0:N2}" onchange="javascript:Cal_Total_Money();" >
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1"
                            />
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chi phí công cụ tổ chức")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtToolCost" runat="server" SkinID="Money" DataFormatString="{0:N2}" onchange="javascript:Cal_Total_Money();" >
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1"
                            />
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Chi phí tham gia")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtJoinCost" runat="server" SkinID="Money" DataFormatString="{0:N2}" onchange="javascript:Cal_Total_Money();" >
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1"/>
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Chi phí in ấn")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtPrintCost" runat="server" SkinID="Money" DataFormatString="{0:N2}" onchange="javascript:Cal_Total_Money();" >
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1"
                            />
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chi phí khác")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtOtherCost" runat="server" SkinID="Money" DataFormatString="{0:N2}" onchange="javascript:Cal_Total_Money();" >
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1"
                            />
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Tổng chi phí")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtTotalCost" ReadOnly="true" runat="server" SkinID="Money"
                        DataFormatString="{0:N2}">
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="6">
                    <tlk:RadTextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
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
        function Cal_Total_Money() {
            try {
                var Total_money = $find('<%#rntxtTotalCost.ClientID %>');
                var headHunt = $find('<%#rntxtHeadhunt.ClientID %>').get_value();
                var recruitCost = $find('<%#rntxtRecruitCost.ClientID %>').get_value();
                var Trademark = $find('<%#rntxtTrademark.ClientID %>').get_value();
                var ToolCost = $find('<%#rntxtToolCost.ClientID %>').get_value();
                var JoinCost = $find('<%#rntxtJoinCost.ClientID %>').get_value();
                var PrintCost = $find('<%#rntxtPrintCost.ClientID %>').get_value();
                var OtherCost = $find('<%#rntxtOtherCost.ClientID %>').get_value();
                Total_money.set_value(Number(headHunt) + Number(recruitCost) + Number(Trademark) + Number(ToolCost) + Number(JoinCost) + Number(PrintCost) + Number(OtherCost));

            }
            catch (err) {
            }
        }
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }; 
    </script>
</tlk:RadCodeBlock>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />

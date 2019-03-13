<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_WelfareMngNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_WelfareMngNewEdit" %>
<tlk:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
<asp:HiddenField ID="hidIDEmp" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="TopPane" runat="server" Scrolling="None" Height="35px">
        <tlk:RadToolBar ID="tbarMenu" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary"
            ValidationGroup="" />
        <table class="table-form">
            <tr>
                <td class="lb" style="width: 130px">
                    <%# Translate("Tên loại phúc lợi")%><span class="lbReq">*</span></b>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboWELFARE_ID" runat="server" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusWELFARE_ID" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Tên loại phúc lợi %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Tên loại phúc lợi  %>" ClientValidationFunction="cusWELFARE_ID">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cusValWELFARE_ID" ControlToValidate="cboWELFARE_ID" runat="server" ErrorMessage="<%$ Translate: Loại phúc lợi không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Loại phúc lợi không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 130px">
                    <%# Translate("Mã nhân viên")%>
                    <span class="lbReq">*</span></b>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" ReadOnly="True" Width="130px" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="5">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Nhân viên %>" ToolTip="<%$ Translate: Bạn phải chọn Nhân viên %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Tên nhân viên")%></b>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEMPLOYEE" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 130px">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTITLE" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Đơn vị")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtORG" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
              <%--  <td class="lb" style="width: 130px">
                    <%# Translate("Cấp bậc nhân sự")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSTAFF_RANK" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>--%>
                <td class="lb">
                    <%# Translate("Ngày thanh toán")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="dpEFFECT_DATE" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số tiền phúc lợi")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtAmount" runat="server" SkinID="Money" ValidationGroup="Allowance">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                </td>
                <td>
                    <tlk:RadComboBox ID="cboIS_TAXION" runat="server" AutoPostBack="true" SkinID="dDropdownList">
                        <Items>
                            <tlk:RadComboBoxItem Text="Không tính vào lương" Value="0" />
                            <tlk:RadComboBoxItem Text="Tính vào lương (chịu thuế)" Value="1" />
                            <tlk:RadComboBoxItem Text="Tính vào lương (không chịu thuế)" Value="2" />
                        </Items>
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi Chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtSDESC" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Năm")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmYear" runat="server" AutoPostBack="true" Enabled="false"
                        SkinID="Number" NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="4"
                        MinValue="1900" NumberFormat-DecimalDigits="0">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Kỳ lương")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboPeriod" runat="server" Enabled="false" SkinID="dDropdownList">
                    </tlk:RadComboBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            registerOnfocusOut('RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_WelfareMngNewEdit_LeftPane');
        });

        function cusWELFARE_ID(oSrc, args) {
            var cbo = $find("<%# cboWELFARE_ID.ClientID%>");
            args.IsValid = (cbo.get_value().length != 0);
        }

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
//            if (args.get_item().get_commandName() == 'CANCEL') {
//                getRadWindow().close(null);
//                args.set_cancel(true);
//            }
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
        }

        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {
                case '<%= cboWELFARE_ID.ClientID %>':

                    var item = eventArgs.get_item();
                    cbo = $find('<%= rntxtAmount.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    if (item) {
                        cbo.set_value(item.get_attributes().getAttribute("MONEY"));
                    }
                    break;
                default:
                    break;
            }
        }

        function clearSelectRadnumeric(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }

        function clearSelectRadtextbox(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }

    </script>
</tlk:RadCodeBlock>

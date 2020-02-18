<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_RequestNewEditV.ascx.vb"
    Inherits="Recruitment.ctrlRC_RequestNewEditV" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
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
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Phòng ban")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="True" Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindOrg" runat="server" SkinID="ButtonView" CausesValidation="false" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập phòng ban %>" ToolTip="<%$ Translate: Bạn phải nhập phòng ban %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 150px">
                    <%# Translate("Vị trí tuyển dụng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTitle" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusTitle" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn chức danh %>"
                        ToolTip="<%$ Translate: Bạn phải chọn chức danh %>" ClientValidationFunction="cusTitle">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số lượng cần tuyển")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtRecruitNumber" runat="server" NumberFormat-DecimalDigits="1"
                        NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="3" MinValue="0"
                        MaxValue="100">
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rntxtRecruitNumber"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Số lượng cần tuyển %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Số lượng cần tuyển %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày đi làm dự kiến")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpectedJoinDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdExpectedJoinDate"
                        runat="server" ErrorMessage="<%$ Translate: Ngày đi làm dự kiến %>" ToolTip="<%$ Translate: Ngày đi làm dự kiến %>"> 
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Lý do tuyển dụng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboRecruitReason" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusRecruitReason" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Lý do tuyển dụng%>"
                        ToolTip="<%$ Translate: Bạn phải chọn Lý do tuyển dụng %>" ClientValidationFunction="cusRecruitReason">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày gửi yêu cầu")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSendDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Đính kèm tập tin")%>
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
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlUpload ID="ctrlUpload2" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function cusTitle(oSrc, args) {
            var cbo = $find("<%# cboTitle.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusRecruitReason(oSrc, args) {
            var cbo = $find("<%# cboRecruitReason.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
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
        }

        function OnValueChanged(sender, args) {

        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
    </script>
</tlk:RadCodeBlock>

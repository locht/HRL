<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFoldersNewEdit.ascx.vb"
    Inherits="Profile.ctrlFoldersNewEdit" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<style>
    .tvc_table_1
    {
        height: 140px;
        position: relative;
    }
    .tvc_table_1 tr
    {
        position: absolute;
        top: 30%;
        left: 15%;
    }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Width="100%" Scrolling="None">
        <tlk:RadToolBar ID="tbarFolderEdit" runat="server"  OnClientButtonClicking="clientButtonClicking"/>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form tvc_table_1" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="label">
                    <asp:Label ID="lbOrg_Name" runat="server" Text="<%$ Translate: Tên thư mục %>"></asp:Label><span
                        class="lbReq">*</span>
                </td>
                <td colspan="8">
                    <tlk:RadTextBox ID="txtFolderName" runat="server" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqFName" ControlToValidate="txtFolderName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Tên thư mục %>" ToolTip="<%$ Translate: Bạn phải nhập Tên thư mục %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript" language="javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
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
                getRadWindow().close(0);
            }
        }
    </script>
</tlk:RadCodeBlock>

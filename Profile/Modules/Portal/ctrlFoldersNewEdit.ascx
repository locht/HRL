<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFoldersNewEdit.ascx.vb"
    Inherits="Profile.ctrlFoldersNewEdit" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<style>
    .tvc_table_1
    {
        height:140px;
        position:relative;
    }
    .tvc_table_1 tr
    {
        position: absolute;
        top:40%;
        left: 9%;
    }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Orientation="Horizontal">
    <tlk:RadPane ID="LeftPane" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form tvc_table_1" onkeydown="return (event.keyCode!=13)" >
            <tr>
                <td class="label">
                    <asp:Label ID="lbOrg_Name" runat="server" Text="<%$ Translate: Tên thư mục %>"></asp:Label><span
                        class="lbReq">*</span>
                </td>
                <td colspan="6">
                    <tlk:RadTextBox ID="txtFolderName" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqFName" ControlToValidate="txtFolderName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Tên thư mục %>" ToolTip="<%$ Translate: Bạn phải nhập Tên thư mục %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td></td>
                <td>
                    <asp:Button runat="server" ID="btnSave" Text="Thêm" />
                </td>
                <td>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCancel" Text="Hủy" />
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript" language="javascript">
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function CloseWindow() {
            var oWindow = GetRadWindow();
            if (oWindow) oWindow.close(1);
        }
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }
    </script>
</tlk:RadCodeBlock>

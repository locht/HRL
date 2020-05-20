<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlUserFileNewEdit.ascx.vb"
    Inherits="Profile.ctrlUserFileNewEdit" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<style>
    .tvc_table_1
    {
        margin-top: 2%;
    }
</style>
<tlk:RadToolBar ID="tbarFolderEdit" runat="server" OnClientButtonClicking="clientButtonClicking" />
<asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
<b>
    <asp:Label runat="server" ID="lbStatus" ForeColor="Red"></asp:Label></b>
<table class="table-form tvc_table_1" onkeydown="return (event.keyCode!=13)">
    <tr>
        <td class="label">
            <asp:Label ID="lbOrg_Name" runat="server" Text="<%$ Translate: Tên File %>"></asp:Label><span
                class="lbReq">*</span>
        </td>
        <td colspan="8">
            <tlk:RadTextBox ID="txtFileName" runat="server" Width="300px">
            </tlk:RadTextBox>
            <asp:RequiredFieldValidator ID="reqFName" ControlToValidate="txtFileName" runat="server"
                ErrorMessage="<%$ Translate: Bạn phải nhập Tên File %>" ToolTip="<%$ Translate: Bạn phải nhập Tên File %>">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="Label1" runat="server" Text="<%$ Translate: Mô tả %>"></asp:Label><span
                class="lbReq">*</span>
        </td>
        <td colspan="8">
            <tlk:RadTextBox ID="txtDescription" runat="server" Width="300px">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="Label2" runat="server" Text="<%$ Translate: Nhập File %>"></asp:Label><span
                class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server">
            </tlk:RadTextBox>
            <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
            </tlk:RadTextBox>
            <tlk:RadButton runat="server" ID="btnUpload" SkinID="ButtonView" CausesValidation="false"
                TabIndex="3" />
            <tlk:RadButton ID="btnDownload" runat="server" Visible="false" Text="<%$ Translate: Tải xuống%>"
                CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
            </tlk:RadButton>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtUploadFile"
                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập File %>" ToolTip="<%$ Translate: Bạn phải nhập File %>">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
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

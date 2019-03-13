<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlEmpBasicInfo.ascx.vb"
    Inherits="Profile.ctrlEmpBasicInfo" %>
<table class="table-form">
    <tr>
        <td class="lb" style="width: 130px">
            <%# Translate("Mã nhân viên")%>
        </td>
        <td>
            <asp:HiddenField ID="hidID" runat="server" />
            <tlk:RadTextBox ID="txtEmployeeCODE1" runat="server" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
        <td class="lb" style="width: 150px">
            <%# Translate("Họ và tên lót")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtFullName" runat="server" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
    </tr>
</table>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadScriptBlock runat="server" ID="ScriptBlock">
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }
    </script>
</tlk:RadScriptBlock>

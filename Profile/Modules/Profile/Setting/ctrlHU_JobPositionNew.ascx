<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_JobPositionNew.ascx.vb"
    Inherits="Profile.ctrlHU_JobPositionNew" %>
<%@ Register Src="~/Modules/Common/ctrlMessageBox.ascx" TagName="ctrlMessageBox"
    TagPrefix="Common" %>
<tlk:RadToolBar ID="rtbMain" runat="server" />
<div style="margin-left: 30px; margin-right: 30px;">
    <fieldset style="width: auto; height: auto">
        <legend>
            <%# Translate("Thông tin nhân bản")%>
        </legend>
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã vị trí công việc")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Tên vị trí công việc")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtJobName" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Số phân bản")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtNumber" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
        </table>
    </fieldset>
</div>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
        }

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

        function resize(width, heigth) {
            var oWindow = getRadWindow();
            oWindow.set_width(width);
            oWindow.set_height(heigth);
            oWindow.center();
        }

    </script>
</tlk:RadCodeBlock>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
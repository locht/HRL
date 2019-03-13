<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDBInfoLeave.ascx.vb"
    Inherits="Profile.ctrlDBInfoLeave" %>
<style type="text/css">
    .lblInfo
    {
        font-weight: bold;
        color: #2196f3;
    }
</style>
<link href="../../../../Styles/font-awesome.css" rel="stylesheet" type="text/css" />
<span class="title-dbportal">Ngày phép của tôi</span>
<div class="boxdbPortal">
    <table class="table-form">
        <tr>
            <td>
                <span class="lbllegen">Tổng số phép năm:</span>
            </td>
            <td>
                <span class="lblNumber">
                    <asp:Label ID="lblNgayDuocNghi" runat="server"></asp:Label>
                    (ngày)</span>
            </td>
        </tr>
        <tr>
            <td>
                <span class="lbllegen">Số phép năm còn lại:</span>
            </td>
            <td>
                <span class="lblNumber">
                    <asp:Label ID="lblConLai" runat="server"></asp:Label>
                    (ngày)</span>
            </td>
        </tr>
        <tr>
            <td>
                <span class="lbllegen">Tổng số ngày nghỉ bù:</span>
            </td>
            <td>
                <span class="lblNumber">
                    <asp:Label ID="lblNgayDuocNghiB" runat="server"></asp:Label>
                    (ngày)</span>
            </td>
        </tr>
        <tr>
            <td>
                <span class="lbllegen">Số ngày nghỉ bù còn lại:</span>
            </td>
            <td>
                <span class="lblNumber">
                    <asp:Label ID="lblConLaiB" runat="server"></asp:Label>
                    (ngày)</span>
            </td>
        </tr>
    </table>
</div>
<tlk:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

        function RaiseCommand(sender, eventArgs) {
            var item = eventArgs.get_commandName();
            if (item == "ExportToExcel") {
                enableAjax = false;
            }
        }

        function POPUP(value) {
            var RadWinManager = GetRadWindow().get_windowManager();
            RadWinManager.set_visibleTitlebar(true);
            var oWindow = RadWinManager.open(value);
            var pos = $("html").offset();
            oWindow.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close); //set the desired behavioursvar pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize(parent.document.body.clientWidth, parent.document.body.clientHeight);
            setTimeout(function () { oWindow.setActive(true); }, 0);
            return false;
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function GetRadWindow() {
            var oWindow = null;

            if (window.radWindow)
                oWindow = window.radWindow;
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow;

            return oWindow;
        }

        $(document).ready(InIEvent);
    </script>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
    </script>
</tlk:RadScriptBlock>

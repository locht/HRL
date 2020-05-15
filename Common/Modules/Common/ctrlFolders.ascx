<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFolders.ascx.vb"
    Inherits="Common.ctrlFolders" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarContracts" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <<tlk:RadPane ID="RadPane2" runat="server" Height="93%" Scrolling="None">
        <tlk:RadTreeView ID="trvOrg" runat="server" CausesValidation="false" Height="93%"
            Width="100%">
        </tlk:RadTreeView>
        <tlk:RadTreeView ID="trvOrgPostback" runat="server" CausesValidation="false" Height="93%"
            Width="100%">
        </tlk:RadTreeView>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock runat="server" ID="RadCodeBlock1">
    <script type="text/javascript">
        if (Telerik.Web.UI.RadTreeView != undefined) {
            Telerik.Web.UI.RadTreeView.prototype.saveClientState = function () {
                return "{\"expandedNodes\":" + this._expandedNodesJson +
                ",\"collapsedNodes\":" + this._collapsedNodesJson +
                ",\"logEntries\":" + this._logEntriesJson +
                ",\"selectedNodes\":" + this._selectedNodesJson +
                ",\"checkedNodes\":" + this._checkedNodesJson +
                ",\"scrollPosition\":" + Math.round(this._scrollPosition) + "}";
            }
        }
    </script>
</tlk:RadCodeBlock>

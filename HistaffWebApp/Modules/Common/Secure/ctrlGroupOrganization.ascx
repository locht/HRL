<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlGroupOrganization.ascx.vb"
    Inherits="Common.ctrlGroupOrganization" %>
<%@ Register Src="../ctrlOrganization.ascx" TagName="ctrlOrganization" TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" Width="100%" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <Common:ctrlOrganization ID="orgLoca" runat="server"/>
    </tlk:RadPane>
</tlk:RadSplitter>

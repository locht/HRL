<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlUserOrganization.ascx.vb"
    Inherits="Common.ctrlUserOrganization" %>
<%@ Register Src="../ctrlOrganization.ascx" TagName="ctrlOrganization" TagPrefix="Common" %>
<tlk:radsplitter id="RadSplitter1" runat="server" width="100%" height="100%" orientation="Horizontal">
    <tlk:radpane id="RadPane1" runat="server" Height="35px" scrolling="None">
        <tlk:radtoolbar id="tbarMain" runat="server" width="100%" />
    </tlk:radpane>
    <tlk:radpane id="RadPane2" runat="server">
        <Common:ctrlOrganization ID="orgLoca" runat="server" />
    </tlk:radpane>
</tlk:radsplitter>

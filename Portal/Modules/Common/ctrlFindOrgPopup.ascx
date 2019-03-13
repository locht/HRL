<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindOrgPopup.ascx.vb"
    Inherits="Common.ctrlFindOrgPopup" %>
<%@ Register Src="ctrlOrganization.ascx" TagName="ctrlOrganization" TagPrefix="Common" %>
<tlk:RadWindow runat="server" ID="rwMessage" Behaviors="None" VisibleStatusbar="false"
    Height="430" Width="400" Modal="true" Title="<%$ Translate: VIEW_TITLE_ORGANIZATION_POPUP %>">
    <ContentTemplate>
        <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <p>
                <Common:ctrlOrganization ID="ctrlOrg1" runat="server" Height="300px" />
            </p>
            <div style="margin: 0px 10px 10px 10px; text-align: center;">
                <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: SELECT %>"
                    Font-Bold="true" CausesValidation="false">
                </tlk:RadButton>
                <tlk:RadButton ID="btnNO" runat="server" Width="60px" Text="<%$ Translate: CANCEL %>"
                    Font-Bold="true" CausesValidation="false">
                </tlk:RadButton>
            </div>
        </tlk:RadAjaxPanel>
    </ContentTemplate>
</tlk:RadWindow>

﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindOrgPopup.ascx.vb"
    Inherits="Common.ctrlFindOrgPopup" %>
<%@ Register Src="ctrlOrganization.ascx" TagName="ctrlOrganization" TagPrefix="Common" %>
<tlk:RadWindow runat="server" ID="rwMessage" Behaviors="None" VisibleStatusbar="false"
    Height="420" Width="400" Modal="true" Title="<%$ Translate: Tìm kiếm đơn vị %>">
    <ContentTemplate>
        <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <div style="height: 330px;">
                <common:ctrlorganization id="ctrlOrg1" runat="server" />
            </div>
            <div style="margin: 0px 10px 10px 10px; text-align: center;">
                <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: Chọn %>"
                    Font-Bold="true" CausesValidation="false">
                </tlk:RadButton>
                <tlk:RadButton ID="btnNO" runat="server" Width="60px" Text="<%$ Translate: Hủy %>"
                    Font-Bold="true" CausesValidation="false">
                </tlk:RadButton>
            </div>
        </tlk:RadAjaxPanel>
    </ContentTemplate>
</tlk:RadWindow>

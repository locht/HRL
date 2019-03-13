﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCO_Dashboard.ascx.vb"
    Inherits="Profile.ctrlCO_Dashboard" %>
<tlk:RadWindowManager ID="RadWindowManager1" Behaviors="None" runat="server" Width="200px"
    VisibleTitlebar="false" VisibleStatusbar="false" EnableShadow="false" ShowContentDuringLoad="false"
    VisibleOnPageLoad="true" Style="z-index: 2000 !important;">
    <Windows>
        <tlk:RadWindow runat="server" ID="rw1" CssClass="RadWindow_Custom">
        </tlk:RadWindow>
        <tlk:RadWindow runat="server" ID="rw2" CssClass="RadWindow_Custom">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var winW, winH, pos;
        var oWnd1, oWnd2;

        var i1 = 0;
        var i2 = 0;
        function SizeToFit(resize) {

            window.setTimeout(function () {
                winW = ($("#WindowMainRegion").width() - 16) / 2;
                winH = ($("#WindowMainRegion").height() - 13);
                pos = $("#WindowMainRegion").offset();
                var left = pos.left + 8;
                var top = pos.top + 32;

                oWnd1 = $find("<%=rw1.ClientID %>");
                oWnd2 = $find("<%=rw2.ClientID %>");

                oWnd1.setSize(winW, winH / 2);
                oWnd1.moveTo(left, top);
                if (i1 == 0) {
                    radopen('Dialog.aspx?mid=Profile&fid=ctrlCO_DBTop5Comp&group=Dashboard&width=' + (oWnd1.get_width()) + '&height=' + (oWnd1.get_height() - 30) + "&noscroll=1&resize=" + resize, "rw1");
                    i1 = 1;
                }

                oWnd2.setSize(winW, winH / 2);
                oWnd2.moveTo(left, top + winH / 2);
                if (i2 == 0) {
                    radopen('Dialog.aspx?mid=Profile&fid=ctrlCO_DBTop5CompAvg&group=Dashboard&width=' + (oWnd1.get_width()) + '&height=' + (oWnd1.get_height() - 30) + "&noscroll=1&resize=" + resize, "rw2");
                    i2 = 1;
                }

            }, 100);
        }
        $(window).resize(function () {
            SizeToFit(1);
        });
    </script>
</tlk:RadCodeBlock>

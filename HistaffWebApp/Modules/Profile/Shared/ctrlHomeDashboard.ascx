<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHomeDashboard.ascx.vb" 
Inherits="Profile.ctrlHomeDashboard" %>
<tlk:RadWindowManager ID="RadWindowManager1" Behaviors="None" runat="server" Width="1200px"
    VisibleTitlebar="false" VisibleStatusbar="false" EnableShadow="false" ShowContentDuringLoad="false"
    VisibleOnPageLoad="true" Style="z-index: 2000 !important;">
    <Windows>
        <tlk:RadWindow runat="server" ID="rw4" CssClass="RadWindow_Custom">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var winW, winH, pos;
        var oWnd4;

        var i4 = 0;

        function SizeToFit(resize) {

            window.setTimeout(function () {
                winW = ($("#WindowMainRegion").width() + 85) / 2;
                winH = ($("#WindowMainRegion").height() + 500);
                pos = $("#WindowMainRegion").offset();
                var left = pos.left + 8;
                var top = pos.top + 32;

                oWnd4 = $find("<%=rw4.ClientID %>");

                oWnd4.setSize(winW * 2 - 100, winH / 2);
                oWnd4.moveTo(left, top);
                if (i4 == 0) {
                    radopen('Dialog.aspx?mid=Profile&fid=ctrlDashboardHome&group=Shared&width=' + (oWnd4.get_width()) + '&height=' + (oWnd4.get_height()) + "&noscroll=1&resize=" + resize, "rw4");
                    i4 = 1;
                }

            }, 100);
        }
        $(window).resize(function () {
            SizeToFit(1);
        });
    </script>
</tlk:RadCodeBlock>

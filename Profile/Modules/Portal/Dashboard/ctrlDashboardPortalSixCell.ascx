<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDashboardPortalSixCell.ascx.vb"
    Inherits="Profile.ctrlDashboardPortalSixCell" %>
<tlk:RadWindowManager ID="RadWindowManager1" Behaviors="None" runat="server" Width="200px"
    VisibleTitlebar="false" VisibleStatusbar="false" EnableShadow="false" ShowContentDuringLoad="false"
    VisibleOnPageLoad="true" Style="z-index: 2000 !important;">
    <Windows>
        <tlk:RadWindow runat="server" ID="rw1" CssClass="RadWindow_Custom">
        </tlk:RadWindow>
        <tlk:RadWindow runat="server" ID="rw2" CssClass="RadWindow_Custom">
        </tlk:RadWindow>
        <tlk:RadWindow runat="server" ID="rw4" CssClass="RadWindow_Custom">
        </tlk:RadWindow>
        <tlk:RadWindow runat="server" ID="rw5" CssClass="RadWindow_Custom">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">

    <img src="http://localhost:7777/Static/Images/IMAGE_PANEL.png" style="width:100%;height:100%"/>
  <%--  <script type="text/javascript">
        var winW, winH, pos;
        var oWnd1, oWnd2, oWnd4, oWnd5;

        var i1 = 0;
        var i2 = 0;
        var i4 = 0;
        var i5 = 0;
        function SizeToFit(resize) {

            window.setTimeout(function () {
                winW = ($("#WindowMainRegion").width() - 16) / 2;
                winH = ($("#WindowMainRegion").height() - 13) / 2;
                pos = $("#WindowMainRegion").offset();
                var left = pos.left + 30;
                var top = pos.top + 50;

                oWnd1 = $find("<%=rw1.ClientID %>");
                oWnd2 = $find("<%=rw2.ClientID %>");
                oWnd4 = $find("<%=rw4.ClientID %>");
                oWnd5 = $find("<%=rw5.ClientID %>");

                oWnd1.setSize(winW, winH);
                oWnd1.moveTo(left, top);
                if (i1 == 0) {
                    radopen('Dialog.aspx?mid=Dashboard&fid=ctrlDBEmployeePaper&width=' + (oWnd1.get_width()) + '&height=' + (oWnd1.get_height() - 30) + "&noscroll=1&resize=" + resize, "rw1");
                    i1 = 1;
                }

                oWnd2.setSize(winW, winH);
                oWnd2.moveTo(left + winW, top);
                if (i2 == 0) {
                    radopen('Dialog.aspx?mid=Dashboard&fid=ctrlDBEmployeeReg&width=' + (oWnd1.get_width()) + '&height=' + (oWnd1.get_height() - 30) + "&noscroll=1&resize=" + resize, "rw2");
                    i2 = 1;
                }

                oWnd4.setSize(winW, winH);
                oWnd4.moveTo(left, top + winH);
                if (i4 == 0) {
                    radopen('Dialog.aspx?mid=Dashboard&fid=ctrlDBInfoLeave&width=' + (oWnd1.get_width() - 20) + '&height=' + (oWnd1.get_height() - 80) + "&noscroll=1&resize=" + resize, "rw4");
                    i4 = 1;
                }

                oWnd5.setSize(winW, winH);
                oWnd5.moveTo(left + winW, top + winH);
                if (i5 == 0) {
                    radopen('Dialog.aspx?mid=Dashboard&fid=ctrlDBRegApp&width=' + (oWnd1.get_width() - 20) + '&height=' + (oWnd1.get_height() - 80) + "&noscroll=1&resize=" + resize, "rw5");
                    i5 = 1;
                }

            }, 100);
        }
        $(window).resize(function () {
            SizeToFit(1);
        });
    </script>--%>
</tlk:RadCodeBlock>

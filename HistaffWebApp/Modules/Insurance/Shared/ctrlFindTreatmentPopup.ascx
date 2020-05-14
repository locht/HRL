<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindTreatmentPopup.ascx.vb"
    Inherits="Insurance.ctrlFindTreatmentPopup" %>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        function <%=ClientID%>_OnClientClose(oWnd, args) {
            debugger;
            oWnd = $find('<%=popupId %>');
            oWnd.remove_close(<%=ClientID%>_OnClientClose);
            var arg = args.get_argument();
            if(arg == null)
            {
                arg = new Object();
                arg.ID = 'Cancel';
            }
            if (arg) {
                var ajaxManager = $find("<%= AjaxManagerId %>");
                ajaxManager.ajaxRequest("<%= ClientID %>_PopupPostback:" + arg.ID);
            }
        }

    </script>
</tlk:RadScriptBlock>

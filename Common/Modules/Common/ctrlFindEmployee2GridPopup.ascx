<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindEmployee2GridPopup.ascx.vb"
     Inherits="Common.ctrlFindEmployee2GridPopup" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<%@ Register Src="ctrlOrganization.ascx" TagName="ctrlOrganization" TagPrefix="Common" %>
    Inherits="Common.ctrlFindEmployee2GridPopup" %>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        function <%=ClientID%>_OnClientClose(oWnd, args) {
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

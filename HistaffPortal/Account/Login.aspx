<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="HistaffPortal.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="image/x-icon" rel="shortcut icon" href="/Static/images/fav-icon.ico" />
    <script type="text/javascript" src="/Scripts/jquery-1.7.1.min.js"></script>
</head>
<body style="overflow: visible; background-color: #e6e6e6;">
    <div class="bgrtrans">
    </div>
    <tlk:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        <StyleSheets>
            <tlk:StyleSheetReference Path="~/Styles/Login.css" />
        </StyleSheets>
    </tlk:RadStyleSheetManager>
    <form id="form1" runat="server">
    <tlk:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="360000">
        <Scripts>
            <tlk:RadScriptReference Path="~/Scripts/noty/jquery.noty.js" />
            <tlk:RadScriptReference Path="~/Scripts/noty/layouts/center.js" />
            <tlk:RadScriptReference Path="~/Scripts/noty/layouts/topCenter.js" />
            <tlk:RadScriptReference Path="~/Scripts/noty/themes/default.js" />
        </Scripts>
    </tlk:RadScriptManager>
    <div>
        <asp:Panel ID="Panel1" runat="server" DefaultButton="LoginUser$LoginButton" Height="100%">
            <asp:Login ID="LoginUser" runat="server" EnableViewState="true" Width="100%" RememberMeSet="true">
                <LayoutTemplate>
                    <div class="box">
                        <div style=" height:70px; center; padding-bottom: 20px;margin-left:30px">
                            <%--<img height="76" id="logo" src="../Static/Images/logo_largest.png">--%>
                        </div>
                        <div class="sperator">
                        </div>
                        <div class="input">
                            <asp:TextBox ID="UserName" runat="server" CssClass="text"></asp:TextBox>
                        </div>
                        <div class="input" style="padding-bottom: 43px;">
                            <asp:TextBox ID="Password" runat="server" CssClass="text next" TextMode="Password"></asp:TextBox>
                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="<%$ Translate: Đăng nhập%>"
                                CssClass="button" />
                            <div class="remember">
                                <asp:CheckBox ID="RememberMe" runat="server" Text="<%$ Translate: Ghi nhớ tài khoản%>"
                                    TextAlign="Left" />
                              <%--  <asp:DropDownList ID="rcbLanguage" runat="server" AutoPostBack="true" CausesValidation="false"
                                    Visible="True">
                                </asp:DropDownList>--%>
                            </div>
                        </div>
                    </div>
                    <div class="copyright">
                        <img alt="Copyright" width="78" src="../Static/Images/tvc_logo.png">
                        <div>
                            Copyright &copy; 2016 Tinhvan Consulting</div>
                    </div>
                </LayoutTemplate>
            </asp:Login>
        </asp:Panel>
        <tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
        $(document).ready(function(){
             var path = getCookie("useravatarPortal");
            if (path != "")
                $("#avatar").attr("src", path);
            else
                $("#avatar").attr("src", "../Static/Images/user_login.png");
        });
        function getCookie(cname)
        {
            var name = cname + "=";
            var ca = document.cookie.split(';');
            for(var i=0; i<ca.length; i++)
            {
                var c = ca[i].trim();
                if (c.indexOf(name)==0) return c.substring(name.length,c.length);
            }
            return "";
        } 

        function OnClientClicking() {

            var user = $("#<%= LoginUser.FindControl("UserName").ClientID %>").val();
            user = $.trim(user);
            $("#<%= LoginUser.FindControl("UserName").ClientID %>").val(user);
        }

        function OnClientShow(sender, eventArgs) {
            $get("<%= LoginUser.FindControl("UserName").ClientID %>").focus();
        }

         //Thiết lập notify
            $.noty.defaults = {
                layout: 'topCenter',
                theme: 'defaultTheme',
                type: 'alert',
                text: '',
                dismissQueue: true, // If you want to use queue feature set this true
                template: '<div class="noty_message"><span class="noty_text"></span><div class="noty_close"></div></div>',
                animation: {
                    open: { height: 'toggle' },
                    close: { height: 'toggle' },
                    easing: 'swing',
                    speed: 500 // opening & closing animation speed
                },
                timeout: false, // delay for closing event. Set false for sticky notifications
                force: true, // adds notification to the beginning of queue when set to true
                modal: false,
                closeWith: ['click'], // ['click', 'button', 'hover']
                callback: {
                    onShow: function () { },
                    afterShow: function () { },
                    onClose: function () { },
                    afterClose: function () { }
                },
                buttons: false // an array of buttons
            };
            </script>
        </tlk:RadCodeBlock>
    </div>
    </form>
</body>
</html>

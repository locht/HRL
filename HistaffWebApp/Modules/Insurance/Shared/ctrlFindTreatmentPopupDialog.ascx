<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindTreatmentPopupDialog.ascx.vb"
    Inherits="Insurance.ctrlFindTreatmentPopupDialog" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgEmployeeInfo" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="ID,CODE,NAME_VN,ADDRESS,PROVINCE_NAME,DISTRICT_NAME">
                        <Columns>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã %>" DataField="CODE" SortExpression="CODE"
                                UniqueName="CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi khám chữa bệnh %>" DataField="NAME_VN"
                                SortExpression="NAME_VN" UniqueName="NAME_VN" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ %>" DataField="ADDRESS"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" SortExpression="ADDRESS"
                                UniqueName="ADDRESS" />
                            
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tỉnh/ Thành phố %>" DataField="PROVINCE_NAME"
                                SortExpression="PROVINCE_NAME" UniqueName="PROVINCE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Quận/ Huyện %>" DataField="DISTRICT_NAME"
                                SortExpression="DISTRICT_NAME" UniqueName="DISTRICT_NAME" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <%--<ClientEvents OnGridCreated="GridCreated" />--%>
                        <%--<ClientEvents OnCommand="ValidateFilter" />--%>
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" MinHeight="50" Height="50px" Scrolling="None">
                <div style="margin: 20px 10px 10px 10px; text-align: right; vertical-align: middle">
                    <asp:HiddenField ID="hidSelected" runat="server" />
                    <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: Chọn %>"
                        Font-Bold="true" CausesValidation="false">
                    </tlk:RadButton>
                    <tlk:RadButton ID="btnNO" runat="server" Width="60px" Text="<%$ Translate: Hủy %>"
                        Font-Bold="true" CausesValidation="false" OnClientClicked="btnCancelClick">
                    </tlk:RadButton>
                </div>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">

        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }

        //        function GridCreated(sender, eventArgs) {
        //            registerOnfocusOut('RAD_SPLITTER_PANE_CONTENT_RadPaneMain');
        //        }

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function btnYesClick() {
            //create the argument that will be returned to the parent page
            var oArg = new Object();

            //get a reference to the current RadWindow
            var oWnd = GetRadWindow();

            oArg.ID = $("#<%=hidSelected.ClientID %>").val();
            //Close the RadWindow and send the argument to the parent page
            oWnd.close(oArg);
        }

        function btnCancelClick() {
            //create the argument that will be returned to the parent page
            var oArg = new Object();

            //get a reference to the current RadWindow
            var oWnd = GetRadWindow();

            oArg.ID = 'Cancel';
            //Close the RadWindow and send the argument to the parent page
            oWnd.close(oArg);
        }

        function OnClientClose(oWnd, args) {
            debugger;
            oWnd = $find('<%=popupId %>');
            oWnd.remove_close(OnClientClose);
            var arg = args.get_argument();
            if (arg) {
                postBack(arg);
            }
        }

        function postBack(arg) {
            var ajaxManager = $find("<%= AjaxManagerId %>");
            ajaxManager.ajaxRequest(arg); //Making ajax request with the argument
        }
    </script>
</tlk:RadScriptBlock>

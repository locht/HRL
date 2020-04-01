<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_SafeLaborMng.ascx.vb"
    Inherits="Profile.ctrlHU_SafeLaborMng" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarWelfareMngs" runat="server" OnClientButtonClicking="clientButtonClicking" />
                <%--<tlk:RadToolBar ID="tbarWelfareMngs" runat="server" />--%>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="100%">
                <tlk:RadGrid PageSize="50" ID="rgWelfareMng" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="ID" EditMode="InPlace" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã vụ tai nạn %>" DataField="CODE"
                                ReadOnly="true" UniqueName="CODE" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên vụ tai bạn %>" DataField="NAME"
                                ReadOnly="true" UniqueName="NAME" HeaderStyle-Width="60px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                ReadOnly="true" UniqueName="ORG_NAME" HeaderStyle-Width="200px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate:Ngày xảy ra tại nạn  %>" DataField="DATE_OCCUR"
                                ReadOnly="true" UniqueName="DATE_OCCUR" HeaderStyle-Width="150px" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời gian xảy ra %>" DataField="HOUR_OCCUR"
                                ReadOnly="true" UniqueName="HOUR_OCCUR" HeaderStyle-Width="200px" DataFormatString="{0:HH:MM}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại tai nạn %>" DataField="TYPE_ACCIDENT_NAME"
                                ReadOnly="true" UniqueName="TYPE_ACCIDENT_NAME" HeaderStyle-Width="200px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nguyên nhân tai nạn %>" DataField="REASON_ACCIDENT_NAME"
                                ReadOnly="true" UniqueName="REASON_ACCIDENT_NAME" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mức độ thương tật %>" DataField="LEVEL_INJURED"
                                ReadOnly="true" UniqueName="LEVEL_INJURED" HeaderStyle-Width="55px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi xảy ra tai nạn %>" DataField="PLACE_ACCIDENT"
                                ReadOnly="true" UniqueName="PLACE_ACCIDENT" HeaderStyle-Width="200px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chi phí tai nạn %>" DataField="COST_ACCIDENT"
                                ReadOnly="true" UniqueName="COST_ACCIDENT" HeaderStyle-Width="65px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" ReadOnly="true"
                                UniqueName="REMARK" HeaderStyle-Width="100px" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="4" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move" Modal="true"
            ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin quản lý phúc l%>"
            OnClientClose="popupclose">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function OpenNew() {
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_SafeLaborMngNewEdit&group=Business&noscroll=1', "_self");
            //            var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlHU_SafeLaborMngNewEdit&group=Business&noscroll=1', "rwPopup");
            //            var pos = $("html").offset();
            //            oWindow.moveTo(pos.left, pos.top);
            //            oWindow.setSize($(window).width(), $(window).height());
        }
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

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_SafeLaborMng_RadSplitter3');
        }

        function CheckValidate() {
            var bCheck = $find('<%= rgWelfareMng.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }
            return 0;
        }

        function OpenEdit() {

            if ($find('<%= rgWelfareMng.ClientID%>').get_masterTableView().get_selectedItems().length == 1) {
                var id = $find('<%= rgWelfareMng.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                window.open('/Default.aspx?mid=Profile&fid=ctrlHU_SafeLaborMngNewEdit&group=Business&gUID=' + id + '', "_self");
            }
        }

        function clientButtonClicking(sender, args) {
            var m;
            var bCheck;
            var n;
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "EDIT") {
                bCheck = OpenEdit();
                if (bCheck == 0) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                }
                if (bCheck == 1) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                }

                args.set_cancel(true);
            }

            if (args.get_item().get_commandName() == 'DELETE' || args.get_item().get_commandName() == 'DEACTIVE') {
                bCheck = $find('<%= rgWelfareMng.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                }
            }
        }

        var enableAjax = true;

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function gridRowDblClick(sender, eventArgs) {
            OpenEdit();
        }

        function popupclose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                $find("<%= rgWelfareMng.ClientID %>").get_masterTableView().rebind();
            }
        }
    </script>
</tlk:RadCodeBlock>

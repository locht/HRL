<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Determine.ascx.vb"
    Inherits="Profile.ctrlHU_Determine" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgContract" runat="server" Height="100%" AllowPaging="True"
                    AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" HeaderText="<%$ Translate: ID %>" UniqueName="ID"
                                Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chi nhánh %>" DataField="BRANCH"
                                SortExpression="BRANCH" UniqueName="BRANCH" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhà máy/ Phòng - Ban %>" DataField="ORG_NAME3"
                                SortExpression="ORG_NAME3" UniqueName="ORG_NAME3" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngành/ VPDD - Bộ phận %>" DataField="ORG_NAME4"
                                SortExpression="ORG_NAME4" UniqueName="ORG_NAME4" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca - Tổ %>" DataField="ORG_NAME5" SortExpression="ORG_NAME5"
                                UniqueName="ORG_NAME5" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc %>" DataField="TITLE_NAME" SortExpression="TITLE_NAME"
                                UniqueName="TITLE_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số lượng ghế (định biên) %>" DataField="DINHBIEN"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" SortExpression="DINHBIEN"
                                UniqueName="DINHBIEN" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số lượng nhân viên (HeadCount) %>" DataField="HEADCOUNT"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" SortExpression="HEADCOUNT"
                                UniqueName="HEADCOUNT" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số trường hợp mỗi người 1 ghế %>" DataField="ONE_IN_ONE"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" SortExpression="ONE_IN_ONE"
                                UniqueName="ONE_IN_ONE" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số trường hợp trên 1 người ngồi cùng ghế %>" DataField="MANY_IN_ONE"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" SortExpression="MANY_IN_ONE"
                                UniqueName="MANY_IN_ONE" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số lượng ghế trống %>" DataField="BLANK"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" SortExpression="BLANK"
                                UniqueName="BLANK" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle Width="120px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }

        }


        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'REFRESH') {
                OPENTHANHLY();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "PRINT") {
                var bCheck = $find('<%= rgContract.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            }
            //if (args.get_item().get_commandName() == "NEXT") {
            //var bCheck = $find('<%= rgContract.ClientID %>').get_masterTableView().get_selectedItems().length;
            //if (bCheck == 0) {
            //var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
            //var n = noty({ text: m, dismissQueue: true, type: 'warning' });
            //setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            //args.set_cancel(true);
            //return;
            //}
            //enableAjax = false;
            //}
            if (args.get_item().get_commandName() == 'NEXT') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "EDIT") {
                if (OpenEditContract() == 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                } else if (OpenEditContract() == 2) {
                    var m = '<%= Translate("Bạn không thể sửa nhiều bản ghi! Không thể thực hiện thao tác này") %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                args.set_cancel(true);
            }
        } function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
        }
    </script>
</tlk:RadCodeBlock>

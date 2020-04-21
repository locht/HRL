<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_RecruitmentCost.ascx.vb"
    Inherits="Recruitment.ctrlRC_RecruitmentCost" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="40px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtYear" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Tháng")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtMonth" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td colspan="2" class="lb">
                            <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                                Text="<%$ Translate: Tìm kiếm %>">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chi nhánh %>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" HeaderStyle-Width="200px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm %>" DataField="YEAR" SortExpression="YEAR"
                                UniqueName="YEAR" AllowFiltering="false"  HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tháng %>" DataField="MONTH" SortExpression="MONTH"
                                UniqueName="MONTH"  AllowFiltering="false"  HeaderStyle-Width="80px"/>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng chi phí %>" DataField="TOTAL_COST"
                                SortExpression="TOTAL_COST" UniqueName="TOTAL_COST" 
                                HeaderStyle-Width="90px" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí HeadHunt %>" DataField="HEADHUNT_COST"
                                SortExpression="HEADHUNT_COST" UniqueName="HEADHUNT_COST" 
                                HeaderStyle-Width="90px" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí đăng tuyển %>" DataField="RECRUIT_COST"
                                SortExpression="RECRUIT_COST" UniqueName="RECRUIT_COST" 
                                HeaderStyle-Width="90px" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Thương hiệu tuyển dụng %>" DataField="RC_TRADEMARK"
                                SortExpression="RC_TRADEMARK" UniqueName="RC_TRADEMARK" 
                                HeaderStyle-Width="90px" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí dụng cụ tuyển dụng %>" DataField="RC_TOOL_COST"
                                SortExpression="RC_TOOL_COST" UniqueName="RC_TOOL_COST" 
                                HeaderStyle-Width="90px" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí tham gia %>" DataField="JOIN_COST"
                                SortExpression="JOIN_COST" UniqueName="JOIN_COST" 
                                HeaderStyle-Width="90px" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí in ấn %>" DataField="PRINT_COST"
                                SortExpression="PRINT_COST" UniqueName="PRINT_COST" 
                                HeaderStyle-Width="90px" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí khác %>" DataField="OTHER_COST"
                                SortExpression="OTHER_COST" UniqueName="OTHER_COST" 
                                HeaderStyle-Width="90px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK"
                                SortExpression="REMARK" UniqueName="REMARK" 
                                HeaderStyle-Width="200px" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="1000px" VisibleStatusbar="false"
            OnClientClose="OnClientClose" Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OpenNew() {
            var oWindow = radopen('Dialog.aspx?mid=Recruitment&fid=ctrlRC_RecruitmentCostNewEdit&group=Business&noscroll=1', "rwPopup");
            oWindow.moveTo(pos.center, pos.middle);
            //oWindow.setSize($(window).width(), $(window).height());
            oWindow.setSize(1000, 500);
        }
        function gridRowDblClick(sender, eventArgs) {
            OpenEdit();
        }

        function OpenEdit() {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;

            if (bCheck == 0)
                return 0;
            if (bCheck > 1)
                return 1;

            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var oWindow = radopen('Dialog.aspx?mid=Recruitment&fid=ctrlRC_RecruitmentCostNewEdit&group=Business&noscroll=1&ID=' + id, "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.center, pos.middle);
            //oWindow.setSize($(window).width(), $(window).height());
            oWindow.setSize(1000, 500);
            return 2;
        }

        function clientButtonClicking(sender, args) {
            var m;
            var bCheck;
            var n;
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "EDIT") {
                bCheck = OpenEdit();
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (bCheck == 1) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }

                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "PRINT" || args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'DELETE') {
                bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                } else {
                    //OpenRemark_Reject();
                    //args.set_cancel(true);
                }
            }
        }

        function getRadWindow() {
            var oWindow = null;
            if (window.radWindow)
                oWindow = window.radWindow;
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow;
            return oWindow;
        }
        function OnClientClose(oWnd, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                //postBack(arg);
                //getRadWindow().Close(null);
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
                args.set_cancel(true);
            }
        }
    </script>
</tlk:RadCodeBlock>

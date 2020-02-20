<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_PlanYear.ascx.vb"
    Inherits="Recruitment.ctrlRC_PlanYear" %>
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
            <tlk:RadPane ID="RadPane3" runat="server" Height="80px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td>
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
                            <tlk:GridBoundColumn FooterStyle-Width="100px" HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME"  />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME"  />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số lượng hiện tại %>" DataField="QUANTITY_CURRENT"
                                SortExpression="QUANTITY_CURRENT" UniqueName="QUANTITY_CURRENT" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng SL dự báo %>" DataField="QUANTITY_NEEDED"
                                SortExpression="QUANTITY_NEEDED" UniqueName="QUANTITY_NEEDED" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số lượng đã tuyển %>" DataField="QUANTITY_RECEIVED"
                                SortExpression="QUANTITY_RECEIVED" UniqueName="QUANTITY_RECEIVED" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 1 %>" DataField="MONTH_1"
                                SortExpression="MONTH_1" UniqueName="MONTH_1" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 2 %>" DataField="MONTH_2"
                                SortExpression="MONTH_2" UniqueName="MONTH_2" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 3 %>" DataField="MONTH_3"
                                SortExpression="MONTH_3" UniqueName="MONTH_3" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 4 %>" DataField="MONTH_4"
                                SortExpression="MONTH_4" UniqueName="MONTH_4" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 5 %>" DataField="MONTH_5"
                                SortExpression="MONTH_5" UniqueName="MONTH_5" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 6 %>" DataField="MONTH_6"
                                SortExpression="MONTH_6" UniqueName="MONTH_6" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 7 %>" DataField="MONTH_7"
                                SortExpression="MONTH_7" UniqueName="MONTH_7" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 8 %>" DataField="MONTH_8"
                                SortExpression="MONTH_8" UniqueName="MONTH_8" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 9 %>" DataField="MONTH_9"
                                SortExpression="MONTH_9" UniqueName="MONTH_9" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 10 %>" DataField="MONTH_10"
                                SortExpression="MONTH_10" UniqueName="MONTH_10" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 11 %>" DataField="MONTH_11"
                                SortExpression="MONTH_11" UniqueName="MONTH_11" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 12 %>" DataField="MONTH_12"
                                SortExpression="MONTH_12" UniqueName="MONTH_12" />
                            <tlk:GridBoundColumn HeaderText="ORG_DESC" DataField="ORG_DESC" UniqueName="ORG_DESC"
                                SortExpression="ORG_DESC" Visible="false" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:HiddenField ID="hddLinkPopup" runat="server" Value="Dialog.aspx?mid=Recruitment&fid=ctrlRC_PlanYearReject&group=Business&noscroll=1" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false"
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




        function clientButtonClicking(sender, args) {
            var m;
            var bCheck;
            var n;

            if (args.get_item().get_commandName() == "PRINT" ) {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "PRINT" || args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
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

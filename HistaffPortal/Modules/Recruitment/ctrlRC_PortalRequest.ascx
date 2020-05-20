﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_PortalRequest.ascx.vb" Inherits="Recruitment.ctrlRC_PortalRequest" %>
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
                            <asp:Label ID="lbFromDate" runat="server" Text="Ngày bắt đầu từ"></asp:Label>
                        </td>
                        <td>
                            <tlk:raddatepicker id="rdFromDate" runat="server">
                            </tlk:raddatepicker>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbToDate" runat="server" Text="Đến"></asp:Label>
                        </td>
                        <td>
                            <tlk:raddatepicker id="rdToDate" runat="server">
                            </tlk:raddatepicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Trạng thái")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboStatus" runat="server">
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
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" AllowMultiRowSelection="True">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,STATUS_ID,RC_RECRUIT_PROPERTY,RC_RECRUIT_PROPERTY_NAME,IS_OVER_LIMIT,IS_SUPPORT,FOREIGN_ABILITY,COMPUTER_APP_LEVEL,GENDER_PRIORITY,GENDER_PRIORITY_NAME,RECRUIT_NUMBER,UPLOAD_FILE">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày gửi yêu cầu %>" DataField="SEND_DATE"
                                SortExpression="SEND_DATE" UniqueName="SEND_DATE" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày dự kiến đi làm %>" DataField="EXPECTED_JOIN_DATE"
                                SortExpression="EXPECTED_JOIN_DATE" UniqueName="EXPECTED_JOIN_DATE" HeaderStyle-Width="120px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Bộ phận %>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí tuyển dụng %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                              <tlk:GridBoundColumn HeaderText="<%$ Translate: Số lượng cần tuyển %>" DataField="RECRUIT_NUMBER"
                                SortExpression="RECRUIT_NUMBER" UniqueName="RECRUIT_NUMBER" AllowFiltering="false"
                                HeaderStyle-Width="90px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do tuyển dụng %>" DataField="RECRUIT_REASON_NAME"
                                SortExpression="RECRUIT_REASON_NAME" UniqueName="RECRUIT_REASON_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do không phê duyệt %>" DataField="REMARK_REJECT"
                                SortExpression="REMARK_REJECT" UniqueName="REMARK_REJECT" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                                SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" />
                            <tlk:GridBoundColumn HeaderText="ORG_DESC" DataField="ORG_DESC" UniqueName="ORG_DESC"
                                SortExpression="ORG_DESC" Visible="false" />
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
<asp:HiddenField ID="hddLinkPopup" runat="server" Value="Dialog.aspx?mid=Recruitment&fid=ctrlRC_RequestReject&group=Business&noscroll=1" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
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
        var idCtrl = 'ctrlPortalRequest';
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OpenNew() {

            window.open('/Default.aspx?mid=Recruitment&fid=ctrlRC_RequestPortalNewEdit&noscroll=1', "_self");
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
            window.open('/Default.aspx?mid=Recruitment&fid=ctrlRC_RequestPortalNewEdit&noscroll=1&ID=' + id, "_self");
            //            var oWindow = radopen('Dialog.aspx?mid=Recruitment&fid=ctrlRC_RequestNewEdit&group=Business&noscroll=1&ID=' + id, "rwPopup");
            //            var pos = $("html").offset();
            //            oWindow.moveTo(pos.left, pos.top);
            //            oWindow.setSize($(window).width(), $(window).height());
            //            oWindow.moveTo(pos.left, pos.middle);
            //            oWindow.setSize(1200, 500);
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
            if (args.get_item().get_commandName() == "NEXT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'DELETE' ||
            args.get_item().get_commandName() == 'APROVE' ||
            args.get_item().get_commandName() == 'REJECT') {
                bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
            }
        }

        function OnClientClose(oWnd, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
            }
        }

        function btnPrintSupportClick(sender, args) {
            var bCheck = $find('<%= rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
                return;
            }

            enableAjax = false;
        }

    </script>
</tlk:RadCodeBlock>
<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Commend.ascx.vb"
    Inherits="Profile.ctrlHU_Commend" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarCommends" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="100px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("MSNV/Tên NV")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEmployee" runat="server" CausesValidation="false">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToDate" runat="server">
                            </tlk:RadDatePicker>
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
                        <td class="lb">
                            <%# Translate("Đối tượng khen thưởng")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboCommendObj" AutoPostBack="false" runat="server" CausesValidation="False">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Liệt kê cả nhân viên nghỉ việc %>" />
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm %>" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgCommend" runat="server" AllowMultiRowSelection="True" Height="100%"
                    AllowPaging="true" PageSize="50">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID, STATUS_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="STATUS_ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại khen thưởng %>" DataField="Commend_OBJ_NAME"
                                SortExpression="Commend_OBJ_NAME" UniqueName="Commend_OBJ_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="OBJ_ORG_NAME"
                                SortExpression="OBJ_ORG_NAME" UniqueName="OBJ_ORG_NAME" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên nhân viên %>" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" />
                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME">
                                <HeaderStyle Width="200px" />
                                <ItemTemplate>
                                 <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ORG_NAME") %>'>
                                </asp:Label>
                                <tlk:RadToolTip RenderMode="Lightweight" ID="RadToolTip1" runat="server" TargetControlID="Label1"
                                                    RelativeTo="Element" Position="BottomCenter">
                                <%# DrawTreeByString(DataBinder.Eval(Container, "DataItem.ORG_DESC"))%>
                                </tlk:RadToolTip>
                            </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị nhân viên %>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" />--%>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh nhân viên %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm %>" DataField="YEAR"
                                SortExpression="YEAR" UniqueName="YEAR" />
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp khen thưởng %>" DataField="Commend_LEVEL_NAME"
                                SortExpression="Commend_LEVEL_NAME" UniqueName="Commend_LEVEL_NAME" />--%>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Danh hiệu khen thưởng %>" DataField="COMMEND_TITLE_NAME"
                                SortExpression="COMMEND_TITLE_NAME" UniqueName="COMMEND_TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do %>" DataField="REMARK" SortExpression="REMARK"
                                UniqueName="REMARK" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức khen thưởng %>" DataField="Commend_TYPE_NAME"
                                SortExpression="Commend_TYPE_NAME" UniqueName="Commend_TYPE_NAME" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Mức thưởng %>" DataField="MONEY"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" SortExpression="MONEY"
                                UniqueName="MONEY" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số quyết định %>" DataField="DECISION_NO"
                                SortExpression="DECISION_NO" UniqueName="DECISION_NO" />

                            <tlk:GridBoundColumn HeaderText="ORG_DESC" DataField="ORG_DESC" UniqueName="ORG_DESC"
                                SortExpression="ORG_DESC" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức trả thưởng %>" DataField="COMMEND_PAY_NAME"
                                SortExpression="COMMEND_PAY_NAME" UniqueName="COMMEND_PAY_NAME" />
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Nguồn chi %>" DataField="POWER_PAY_NAME"
                                SortExpression="POWER_PAY_NAME" UniqueName="POWER_PAY_NAME" />--%>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                                ItemStyle-HorizontalAlign="Center" SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <%--<ClientEvents OnGridCreated="GridCreated" />--%>
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <HeaderStyle Width="120px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false"
            OnClientClose="popupclose" Height="550px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
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
//            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_Commend_RadSplitter3');
//        }

        function OpenNew() {
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_CommendNewEdit&group=Business&FormType=0', "_self"); /*
            var pos = $("html").offset();
            oWindow.setSize(1020, $(window).height());
            oWindow.center(); */
        }

        function OpenEdit() {
            var bCheck = $find('<%= rgCommend.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
                return 0;
            if (bCheck > 1)
                return 1;
            var id = $find('<%= rgCommend.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var id_status = $find('<%= rgCommend.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('STATUS_ID');

            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_CommendNewEdit&group=Business&FormType=1&ID=' + id + '&Status=' + id_status, "_self"); /*
            var pos = $("html").offset();
            oWindow.setSize(1020, $(window).height());
            oWindow.center(); */
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

            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "EDIT") {
                bCheck = OpenEdit();
                if (bCheck == 0) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (bCheck == 1) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }

                args.set_cancel(true);
            }

            if (args.get_item().get_commandName() == 'DELETE') {
                bCheck = $find('<%= rgCommend.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
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
            var bCheck = OpenEdit();
            if (bCheck == 0) {
                m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            }
            if (bCheck == 1) {
                m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            }

        }

        function popupclose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $get("<%= btnSearch.ClientId %>").click();
            }
        }
    </script>
</tlk:RadCodeBlock>

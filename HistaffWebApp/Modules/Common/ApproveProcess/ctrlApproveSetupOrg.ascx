<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlApproveSetupOrg.ascx.vb"
    Inherits="Common.ctrlApproveSetupOrg" %>    
<%@ Register Src="../ctrlOrganization.ascx" TagName="ctrlOrganization" TagPrefix="Common" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<style type="text/css">
     #ctl00_MainContent_ctrlApproveSetupOrg_rgDetail_ctl00{
        width:100% !important;
    }        
</style>   
<tlk:RadSplitter runat="server" ID="splitFull" Width="100%" Height="100%">
    <tlk:RadPane runat="server" ID="paneLeftFull" Width="250" MaxWidth="300">
        <Common:ctrlOrganization runat="server" ID="ctrlOrg" />
    </tlk:RadPane>
    <tlk:RadPane runat="server" ID="paneRightFull" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="140px" Scrolling="None">
                <tlk:RadToolBar runat="server" ID="tbarDetail" Width="100%" OnClientButtonClicking="OnClientButtonClicking" />
                <asp:ValidationSummary runat="server" ID="valSummaryVal" />
                <asp:Panel runat="server" ID="pnlDetail" Enabled="false">
                    <table class="table-form">
                        <tr>
                            <td class="lb">
                                <%# Translate("Quy trình")%>
                            </td>
                            <td colspan="3">
                                <tlk:RadComboBox runat="server" ID="cboApproveProcess" Width="250px">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Template áp dụng")%>
                            </td>
                            <td colspan="3">
                                <tlk:RadComboBox runat="server" ID="cboApproveTemplate" Width="250px">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Áp dụng từ ngày")%>
                                <span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadDatePicker runat="server" ID="rdFromDate">
                                </tlk:RadDatePicker>
                                <asp:RequiredFieldValidator runat="server" ID="reqFromDate" ControlToValidate="rdFromDate"
                                    ErrorMessage='<%$ Translate: Chưa nhập Từ ngày %>'></asp:RequiredFieldValidator>
                                <asp:CustomValidator runat="server" ID="cvalCheckDateExist" ErrorMessage='<%$ Translate: Đã có thiết lập áp dụng vào khoảng thời gian bạn chọn. %>'></asp:CustomValidator>
                            </td>
                            <td class="lb">
                                <%# Translate("Đến ngày")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker runat="server" ID="rdToDate">
                                </tlk:RadDatePicker>
                                <%--<asp:CustomValidator runat="server" ID="valCustomFromDateToDate" ></asp:CustomValidator>--%>
                                <asp:CompareValidator runat="server" ID="compareFromDateToDate" ErrorMessage="<%$ Translate: Từ ngày lớn hơn đến ngày. %>"
                                    ToolTip="<%$ Translate: Từ ngày lớn hơn đến ngày. %>"                                    
                                    ControlToCompare="rdFromDate" ControlToValidate="rdToDate" Operator="GreaterThan" Type="Date">
                                </asp:CompareValidator>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 runat="server" ID="rgDetail" Height="100%" SkinID="GridSingleSelect">
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                        <Selecting AllowRowSelect="true" UseClientSelectColumnOnly="false" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID" GroupsDefaultExpanded="true">
                        <GroupByExpressions>
                            <tlk:GridGroupByExpression>
                                <GroupByFields>
                                    <tlk:GridGroupByField FieldName="PROCESS_NAME" />
                                </GroupByFields>
                                <SelectFields>
                                    <tlk:GridGroupByField FieldName="PROCESS_NAME" HeaderText="Quy trình" />
                                </SelectFields>
                            </tlk:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Template %>' DataField="TEMPLATE_NAME"
                                UniqueName="TEMPLATE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Email thông báo %>' DataField="REQUEST_EMAIL"
                                UniqueName="REQUEST_EMAIL">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Áp dụng từ ngày %>' DataField="FROM_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" UniqueName="FROM_DATE">
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Áp dụng đến ngày %>' DataField="TO_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" UniqueName="TO_DATE">
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadScriptBlock runat="server" ID="radScriptBlock">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlApproveSetupOrg_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlApproveSetupOrg_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlApproveSetupOrg_RadPane2';
        var validateID = 'MainContent_ctrlApproveSetupOrg_valSummaryVal';
        var oldSize = $('#' + pane1ID).height();

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
            registerOnfocusOut(splitterID);
        }

        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDetail');
        }

        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDetail');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
                    
            switch (args.get_item().get_commandName()) {
                case 'CREATE':
                    if ($find('<%= ctrlOrg.TreeClientID %>').get_selectedNodes().length == 0) {
                        var n = noty({ text: 'Bạn chưa chọn phòng ban cần thiết lập', dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                        args.set_cancel(true);
                    }
                    break;
                case 'EDIT':
                    if ($find('<%= rgDetail.ClientID %>').get_masterTableView().get_selectedItems().length == 0) {
                        var n = noty({ text: 'Bạn chưa chọn bản ghi nào! Không thể thực hiện thao tác này', dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                        args.set_cancel(true);
                    }
                    break;
                case 'DELETE':
                    if ($find('<%= rgDetail.ClientID %>').get_masterTableView().get_selectedItems().length == 0) {
                        var n = noty({ text: 'Bạn chưa chọn dòng cần xóa', dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                        args.set_cancel(true);
                    }

                    break;
            }
        }
    </script>
</tlk:RadScriptBlock>

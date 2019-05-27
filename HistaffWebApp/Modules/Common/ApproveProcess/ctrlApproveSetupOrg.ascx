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
            <tlk:RadPane ID="RadPane1" runat="server" Height="190px" Scrolling="None">
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
                            <td class="lb">
                                <%# Translate("Chức vụ")%>
                            </td>
                            <td colspan="2">
                                <tlk:RadComboBox runat="server" ID="cboPosition" Width="150px">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Kiểu công")%>
                            </td>
                            <td colspan="2">
                                <tlk:RadComboBox runat="server" ID="cboKieuCong" Width="150px">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("CC mail khi được duyệt")%>
                            </td>
                            <td colspan="2">
                                <tlk:RadTextBox runat="server" ID="txtCCMailAccepted" Width="150px">
                                </tlk:RadTextBox>
                            </td>
                            <asp:RegularExpressionValidator ID="emailValidator" runat="server" Display="Dynamic"
                                    ErrorMessage="Vui lòng nhập email hợp lệ tại trường CC mail khi được duyệt." ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$"
                                    ControlToValidate="txtCCMailAccepted">
                                </asp:RegularExpressionValidator>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Template áp dụng")%>
                            </td>
                            <td colspan="3">
                                <tlk:RadComboBox runat="server" ID="cboApproveTemplate" Width="250px">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Số giờ từ")%>
                            </td>
                            <td colspan="2">
                                <tlk:RadNumericTextBox runat="server" ID="rntxtFromHour" MinValue="0" Width="150px" DataType="System.Decimal">
                                    <NumberFormat DecimalDigits="2" AllowRounding="false" DecimalSeparator="." KeepNotRoundedValue="true"/>
                                </tlk:RadNumericTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("đến")%>                                
                            </td>
                            <td colspan="2">
                                <tlk:RadNumericTextBox runat="server" ID="rntxtToHour" MinValue="0" Width="150px">
                                    <NumberFormat DecimalDigits="2" AllowRounding="false" DecimalSeparator="." KeepNotRoundedValue="true"/>
                                </tlk:RadNumericTextBox>
                            </td>                                                     
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Số ngày từ")%>
                            </td>
                            <td colspan="2">
                                <tlk:RadNumericTextBox runat="server" ID="rntxtFromDay" MinValue="0" Width="150px">
                                    <NumberFormat DecimalDigits="2" AllowRounding="false" DecimalSeparator="." KeepNotRoundedValue="true"/>
                                </tlk:RadNumericTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("đến")%>                                
                            </td>
                            <td colspan="2">
                                <tlk:RadNumericTextBox runat="server" ID="rntxtToDay" MinValue="0" Width="150px">
                                    <NumberFormat DecimalDigits="2" AllowRounding="false" DecimalSeparator="." KeepNotRoundedValue="true"/>
                                </tlk:RadNumericTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("CC mail trong khi duyệt")%>
                            </td>
                            <td colspan="2">
                                <tlk:RadTextBox runat="server" ID="txtCCMailAccepting">
                                </tlk:RadTextBox>
                            </td>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                                    ErrorMessage="Vui lòng nhập email hợp lệ tại trường CC mail trong khi duyệt." ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$"
                                    ControlToValidate="txtCCMailAccepting">
                                </asp:RegularExpressionValidator>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Áp dụng từ ngày")%><span class="lbReq">*</span>                                
                            </td>
                            <td colspan="2">
                                <tlk:RadDatePicker runat="server" ID="rdFromDate">
                                </tlk:RadDatePicker>
                                <asp:RequiredFieldValidator runat="server" ID="reqFromDate" ControlToValidate="rdFromDate"
                                    ErrorMessage='<%$ Translate: Chưa nhập Áp dụng từ ngày %>'></asp:RequiredFieldValidator>
                                <%--<asp:CustomValidator runat="server" ID="cvalCheckDateExist" ErrorMessage='<%$ Translate: Đã có thiết lập áp dụng vào khoảng thời gian bạn chọn. %>'></asp:CustomValidator>--%>
                            </td>
                            <td class="lb">
                                <%# Translate("Đến ngày")%>
                            </td>
                            <td colspan="2">
                                <tlk:RadDatePicker runat="server" ID="rdToDate">
                                </tlk:RadDatePicker>                                
                                <asp:CompareValidator runat="server" ID="compareFromDateToDate" ErrorMessage="<%$ Translate: Từ ngày lớn hơn đến ngày. %>"
                                    ToolTip="<%$ Translate: Từ ngày lớn hơn đến ngày. %>"                                    
                                    ControlToCompare="rdFromDate" ControlToValidate="rdToDate" Operator="GreaterThan" Type="Date">
                                </asp:CompareValidator>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="Y">
                <tlk:RadGrid PageSize="50" runat="server" ID="rgDetail" Height="100%" SkinID="GridSingleSelect">
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                        <Selecting AllowRowSelect="true" UseClientSelectColumnOnly="false" />
                        <%--<ClientEvents OnGridCreated="GridCreated" />--%>
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
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadScriptBlock runat="server" ID="radScriptBlock">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlApproveSetupOrg_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlApproveSetupOrg_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlApproveSetupOrg_RadPane2';
        var validateID = 'MainContent_ctrlApproveSetupOrg_valSummaryVal';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
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

//        function GridCreated(sender, eventArgs) {
//            registerOnfocusOut(splitterID);
//        }

        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDetail');
        }

        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "EXPORT" || args.get_item().get_commandName() == "NEXT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
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

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;

            } else {
                var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
    </script>
</tlk:RadScriptBlock>

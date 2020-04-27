<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_TaxFinalization.ascx.vb"
    Inherits="Payroll.ctrlPA_TaxFinalization" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="185px" Scrolling="Y">
                <tlk:RadToolBar ID="tbarMenu" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rdnYear" runat="server" MinValue="1900" MaxLength="4"
                                SkinID="Number">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdnYear"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập năm %>" ToolTip="<%$ Translate: Bạn phải nhập năm %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Tên kỳ quyết toán")%><span class="lbReq">*</span>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtTaxName" runat="server" Width="100%">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtTaxName"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên quyết toán %>"
                                ToolTip="<%$ Translate: Bạn phải nhập tên kỳ quyết toán %>">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Tháng bắt đầu")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="rdFromMonth"
                                TabIndex="11" Culture="en-US">
                            </tlk:RadMonthYearPicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdFromMonth"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tháng bắt đầu %>" ToolTip="<%$ Translate: Bạn phải nhập tháng bắt đầu %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Tháng kết thúc")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="rdEndMonth"
                                TabIndex="11" Culture="en-US">
                            </tlk:RadMonthYearPicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rdEndMonth"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tháng kết thúc %>"
                                ToolTip="<%$ Translate: Bạn phải nhập tháng kết thúc %>">
                            </asp:RequiredFieldValidator>
                           <%-- <asp:CompareValidator ID="CompareValidator1" runat="server" ToolTip="<%$ Translate: Tháng bắt đầu phải nhỏ hơn hoặc bằng tháng kết thúc %>"
                                ErrorMessage="<%$ Translate: Tháng bắt đầu phải nhỏ hơn hoặc bằng tháng kết thúc %>"
                                Type="Date" Operator="GreaterThan" ControlToCompare="rdFromMonth" ControlToValidate="rdEndMonth"></asp:CompareValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm chi trả")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rdnYearperiod" runat="server" MinValue="1900" MaxLength="4"
                                SkinID="Number" AutoPostBack="true">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdnYearperiod"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập năm chi trả %>" ToolTip="<%$ Translate: Bạn phải nhập năm chi trả %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Tháng chi trả")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriod" runat="server">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="cboPeriod"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tháng chi trả %>" ToolTip="<%$ Translate: Bạn phải nhập tháng chi trả %>">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtRemark" runat="server" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" SkinID="GridSingleSelect" Height="100%" OnPreRender="rgData_PreRender">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,YEAR,TAX_FINALIZATION_NAME,START_DATE,END_DATE,REMARK,ACTFLG">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm %>" DataField="YEAR" SortExpression="YEAR"
                                UniqueName="YEAR" HeaderStyle-Width="50px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên kỳ quyết toán %>" DataField="TAX_FINALIZATION_NAME"
                                SortExpression="TAX_FINALIZATION_NAME" UniqueName="TAX_FINALIZATION_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Từ tháng bắt đầu %>" DataField="START_DATE"
                                SortExpression="START_DATE" UniqueName="START_DATE" DataFormatString="{0:MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Đến tháng kết thúc%>" DataField="END_DATE"
                                SortExpression="END_DATE" UniqueName="END_DATE" DataFormatString="{0:MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                                SortExpression="ACTFLG" UniqueName="ACTFLG" HeaderStyle-Width="130px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" SortExpression="REMARK"
                                UniqueName="REMARK" HeaderStyle-Width="100px" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlPA_Period_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_Period_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_Period_RadPane2';
        var validateID = 'MainContent_ctrlPA_Period_valSum';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;

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
        function displayDecimalFormat(sender, args) {
            sender.set_textBoxValue(sender.get_value().toString());
        }
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:RadCodeBlock>

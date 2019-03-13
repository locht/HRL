<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_WorkStandard.ascx.vb"
    Inherits="Payroll.ctrlPA_WorkStandard" %>
<%@ Import Namespace="Framework.UI" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="165px" Scrolling="None">
        <tlk:RadToolBar ID="tbarSalaryTypes" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Đối tượng lao động")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboLabor" runat="server" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqLabor" ControlToValidate="cboLabor" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn đối tượng lao động. %>" ToolTip="<%$ Translate: Bạn phải chọn đối tượng lao động. %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Năm")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtYEAR" Width="60px" Value="1" ShowSpinButtons="True"
                        ReadOnly="true" CausesValidation="false" MinValue="2015" AutoPostBack="True">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rntxtYEAR"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập năm. %>" ToolTip="<%$ Translate: Bạn phải nhập năm. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Kỳ lương")%></label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboPeriod" runat="server" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb" runat="server">
                    <label id="lbOrders">
                        <%# Translate("Ngày công chế độ")%></label>
                </td>
                <td runat="server">
                    <tlk:RadTextBox ID="txtWordStandard" runat="server" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtWordStandard"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày công chế độ. %>"
                        ToolTip="<%$ Translate: Ngày công chế độ. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="YEAR,PERIOD_ID,OBJECT_ID,OBJECT_NAME,PERIOD_STANDARD,PERIOD_NAME,ACTFLG,REMARK">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng lao động %>" DataField="OBJECT_NAME"
                        SortExpression="OBJECT_NAME" UniqueName="OBJECT_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm %>" DataField="YEAR" SortExpression="YEAR"
                        UniqueName="YEAR" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kỳ lương %>" DataField="PERIOD_NAME"
                        SortExpression="PERIOD_NAME" UniqueName="PERIOD_NAME" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Ngày công chế độ %>" DataField="PERIOD_STANDARD"
                        SortExpression="PERIOD_STANDARD" UniqueName="PERIOD_STANDARD">
                    </tlk:GridNumericColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        SortExpression="ACTFLG" UniqueName="ACTFLG" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" SortExpression="REMARK"
                        UniqueName="REMARK" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
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
</tlk:RadCodeBlock>

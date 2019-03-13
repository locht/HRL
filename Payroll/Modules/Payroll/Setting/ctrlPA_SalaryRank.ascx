<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_SalaryRank.ascx.vb"
    Inherits="Payroll.ctrlPA_SalaryRank" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="165px" Scrolling="None">
        <tlk:RadToolBar ID="tbarSalaryRanks" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Thang lương")%></span><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSalaryGroup" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Nhóm lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSalaryLevel" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqSalaryLevel" ControlToValidate="cboSalaryLevel"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Nhóm lương %>" ToolTip="<%$ Translate: Bạn phải chọn Nhóm lương %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Bậc lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtRank" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqRank" ControlToValidate="txtRank" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập bậc lương %>" ToolTip="<%$ Translate: Bạn phải nhập bậc lương %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Lương cơ bản")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtSalaryBasic" runat="server" ShowSpinButtons="false"
                        MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="3">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rntxtSalaryBasic"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập lương cơ bản %>" ToolTip="<%$ Translate: Bạn phải nhập lương cơ bản %>">
                    </asp:RequiredFieldValidator>
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
        <tlk:RadGrid PageSize=20 ID="rgData" runat="server" Height="100%" AllowFilteringByColumn="true">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="SAL_GROUP_NAME,SAL_LEVEL_ID,SAL_LEVEL_NAME,SAL_LEVEL_ID, RANK, SALARY_BASIC, REMARK">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thang lương %>" DataField="SAL_GROUP_NAME"
                        SortExpression="SAL_GROUP_NAME" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                        AndCurrentFilterFunction="Contains" UniqueName="SAL_GROUP_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm lương %>" DataField="SAL_LEVEL_NAME"
                        SortExpression="SAL_LEVEL_NAME" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                        AndCurrentFilterFunction="Contains" UniqueName="SAL_LEVEL_NAME" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Bậc lương %>" DataField="RANK"
                        SortExpression="RANK" AutoPostBackOnFilter="true" ShowFilterIcon="false" AndCurrentFilterFunction="EqualTo"
                        UniqueName="RANK" NumericType="Number">
                    </tlk:GridNumericColumn>
                    <tlk:GridNumericColumn DataField="SALARY_BASIC" HeaderText="<%$ Translate: Lương cơ bản %>"
                        SortExpression="SALARY_BASIC" UniqueName="SALARY_BASIC" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" Visible="true" DataFormatString="{0:n0}">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="right" />
                    </tlk:GridNumericColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" AutoPostBackOnFilter="true"
                        ShowFilterIcon="false" AndCurrentFilterFunction="Contains" SortExpression="REMARK"
                        UniqueName="REMARK" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        SortExpression="ACTFLG" UniqueName="ACTFLG" />
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

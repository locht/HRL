<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_SalaryGroup.ascx.vb"
    Inherits="Payroll.ctrlPA_SalaryGroup" %>
<%@ Import Namespace="Framework.UI" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="165px" Scrolling="None">
        <tlk:RadToolBar ID="tbarSalaryGroups" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã bảng lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" SkinID="Textbox50" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập mã. %>" ToolTip="<%$ Translate: Bạn phải nhập mã. %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên bảng lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tên thang bảng lương. %>" ToolTip="<%$ Translate: Bạn phải nhập tên thang bảng lương. %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" ControlToValidate="rdEffectDate" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbIS_COEFFICIENT"  Text ="Theo hệ số"></asp:Label>
                </td>
                <td>
                    <div style="margin: -4px; margin-top: 1px">
                        <tlk:RadButton ID="cbIS_COEFFICIENT" ToggleType="CheckBox" ButtonType="ToggleButton"
                            Text="" runat="server" AutoPostBack="false" CausesValidation="false">
                        </tlk:RadButton>
                    </div>
                </td>
                <td class="lb">
                    <asp:Label runat ="server"  ID="lbISHOSE" Text ="Thang lương quản lý (Hose)" ></asp:Label>
                </td>
                <td>
                    <div style="margin: -4px; margin-top: 1px">
                        <tlk:RadButton ID="cbISHOSE" ToggleType="CheckBox" ButtonType="ToggleButton"
                            Text="" runat="server" AutoPostBack="false" CausesValidation="false">
                        </tlk:RadButton>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbRemark"  Text ="Ghi chú"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" id="tdSGlbOrders">
                    <label id="lbOrders">
                        <%# Translate("Thứ tự")%><span class="lbReq">*</span></label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtOrders" MinValue="1" Width="60px"
                        Value="1" ShowSpinButtons="True" ReadOnly="true" CausesValidation="false">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rntxtOrders"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập số thứ tự. %>" ToolTip="<%$ Translate: Bạn phải nhập số thứ tự. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb" id="tdSGlbIs_Incentive"  style='<%# if(Utilities.Account = "U", "display: None" , "Block") %>'>
                    <label id="Label3">
                        <%# Translate("Bảng thưởng HQCV")%></label>
                </td>
                <td id="tdSGcbIs_Incentive" runat="server"  style='<%# if(Utilities.Account = "U", "display: None" , "Block") %>'>
                    <div style="margin: -4px; margin-top: 1px">
                        <tlk:RadButton ID="cbIS_INCENTIVE" ToggleType="CheckBox" ButtonType="ToggleButton"
                            Text="" runat="server" AutoPostBack="false" CausesValidation="false">
                        </tlk:RadButton>
                    </div>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME,EFFECT_DATE,REMARK,IS_COEFFICIENT,IS_INCENTIVE,ORDERS,ISHOSE">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã bảng lương %>" DataField="CODE" SortExpression="CODE"
                        UniqueName="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên bảng lương %>" DataField="NAME" SortExpression="NAME"
                        UniqueName="NAME" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                        UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridTemplateColumn HeaderText="Theo hệ số" UniqueName="IS_COEFFICIENT" DataField="IS_COEFFICIENT">
                        <ItemTemplate>
                            <tlk:RadButton ID="chkIS_COEFFICIENT" ToggleType="CheckBox" ButtonType="ToggleButton"
                                Checked='<%# if(Eval("IS_COEFFICIENT").ToString() = "0", false , true) %>' Text=""
                                runat="server" AutoPostBack="false" CausesValidation="false" ReadOnly="True">
                            </tlk:RadButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                    </tlk:GridTemplateColumn>

                    <tlk:GridTemplateColumn HeaderText="Thang lương quản lý (Hose)" UniqueName="ISHOSE" DataField="ISHOSE">
                        <ItemTemplate>
                            <tlk:RadButton ID="chkISHOSE" ToggleType="CheckBox" ButtonType="ToggleButton"
                                Checked='<%# if(Eval("ISHOSE").ToString() = "0", false , true) %>' Text=""
                                runat="server" AutoPostBack="false" CausesValidation="false" ReadOnly="True">
                            </tlk:RadButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn HeaderText="<%$ Translate: Bảng thưởng HQCV %>" UniqueName="IS_INCENTIVE" DataField="IS_INCENTIVE" >
                        <ItemTemplate>
                            <tlk:RadButton ID="chkIS_INCENTIVE" ToggleType="CheckBox" ButtonType="ToggleButton"
                                Checked='<%# ParseBoolean(Eval("IS_INCENTIVE").ToString()) %>' Text=""
                                runat="server" AutoPostBack="false" CausesValidation="false" ReadOnly="True">
                            </tlk:RadButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                    </tlk:GridTemplateColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Thứ tự %>" DataField="ORDERS" SortExpression="ORDERS"
                        UniqueName="ORDERS">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridNumericColumn>
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

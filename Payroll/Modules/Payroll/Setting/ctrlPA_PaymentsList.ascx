<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_PaymentsList.ascx.vb"
    Inherits="Payroll.ctrlPA_PaymentsList" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="200px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMenu" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <table class="table-form">
            <tr style="display: none">
                <td class="lb">
                    <%# Translate("Đối tượng lương")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboObjectPayment" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">                    
                </td>
                <td>                  
                </td>
            </tr>
            <tr>
            <td class="lb">
                    <%# Translate("Mã tham số")%><span class="lbReq">*</span>
                </td>
                <td>
                   <tlk:RadTextBox ID="txtCode" runat="server"  Width="100%">
                    </tlk:RadTextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập mã khoản tiền. %>" ToolTip="<%$ Translate: Bạn phải nhập mã khoản tiền. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                    
                </td>
                <td>
                <tlk:RadDatePicker ID="dpEffectiveDate" runat="server">
                    </tlk:RadDatePicker>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="dpEffectiveDate" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                   
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên tham số")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" runat="server" Width="100%">
                    </tlk:RadTextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên khoản tiền. %>" ToolTip="<%$ Translate: Bạn phải nhập tên khoản tiền. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Giá trị")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmValue" runat="server">
                    </tlk:RadNumericTextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="nmValue" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập định mức qui định. %>" ToolTip="<%$ Translate: Bạn phải nhập định mức quy định. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDesc" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" id="tdSGlbOrders">
                    <label id="lbOrders">
                        <%# Translate("Thứ tự")%></label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtOrders" MinValue="1" Width="60px"
                        Value="1" ShowSpinButtons="True" CausesValidation="false">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rntxtOrders"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập số thứ tự. %>" ToolTip="<%$ Translate: Bạn phải nhập số thứ tự. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,OBJ_PAYMENT_ID,CODE, NAME,EFFECTIVE_DATE,VALUE,SDESC,ACTFLG,ORDERS">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng lương %>" DataField="OBJ_PAYMENT_NAME_VN"
                        SortExpression="OBJ_PAYMENT_NAME_VN" UniqueName="OBJ_PAYMENT_NAME_VN" Display="False" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã tham số %>" DataField="CODE" SortExpression="CODE" UniqueName="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tham số %>" DataField="NAME" SortExpression="NAME" UniqueName="NAME" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECTIVE_DATE"
                        UniqueName="EFFECTIVE_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Giá trị %>" DataField="VALUE"
                        DataFormatString="{0:N0}" DataType="System.Int32" SortExpression="VALUE" UniqueName="VALUE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="SDESC" SortExpression="SDESC"
                        UniqueName="SDESC" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG" SortExpression="ACTFLG" UniqueName="ACTFLG" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Thứ tự %>" DataField="ORDERS" SortExpression="ORDERS"
                        UniqueName="ORDERS">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridNumericColumn>
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

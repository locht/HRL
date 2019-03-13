<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_TaxationList.ascx.vb"
    Inherits="Payroll.ctrlPA_TaxationList" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="220px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMenu" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <table class="table-form">
            <tr style="display: none">
                <td class="lb">
                    <%# Translate("Đối tượng cư trú")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="rcboResident" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnmValueFrom" runat="server" Width="130px">
                    </tlk:RadNumericTextBox>
                    <%# Translate("VNĐ")%>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rnmValueFrom" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập từ VNĐ. %>" ToolTip="<%$ Translate: Bạn phải nhập từ VNĐ. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đến")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnmValueTo" runat="server" Width="130px">
                    </tlk:RadNumericTextBox>
                    <%# Translate("VNĐ")%>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rnmValueTo" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập đến VNĐ. %>" ToolTip="<%$ Translate: Bạn phải nhập đến VNĐ. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tỉ lệ (%)")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnmRate" runat="server">
                    </tlk:RadNumericTextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rnmRate" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tỉ lệ (%). %>" ToolTip="<%$ Translate: Bạn phải nhập tỉ lệ (%). %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Trừ nhanh")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnmExceptFast" runat="server">
                    </tlk:RadNumericTextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="rnmExceptFast" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập trừ nhanh. %>" ToolTip="<%$ Translate: Bạn phải nhập trừ nhanh. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdpEffectDate" runat="server">
                    </tlk:RadDatePicker>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdpEffectDate" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="display: none">
                    <%# Translate("Ngày hết hiệu lực")%>
                </td>
                <td style="display: none">
                    <tlk:RadDatePicker ID="rdpExpireDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="rtxtDesc" runat="server" SkinID="Textbox1023" Width="100%">
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
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID, RESIDENT_ID, VALUE_FROM, VALUE_TO, RATE, EXCEPT_FAST, FROM_DATE, TO_DATE, ACTFLG, SDESC,ORDERS">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng cơ trú %>" DataField="RESIDENT_NAMEVN"
                        SortExpression="RESIDENT_NAMEVN" UniqueName="RESIDENT_NAMEVN" Display="False" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Từ %>" DataField="VALUE_FROM" SortExpression="VALUE_FROM"
                        UniqueName="VALUE_FROM" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến %>" DataField="VALUE_TO" SortExpression="VALUE_TO"
                        UniqueName="VALUE_TO" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tỉ lệ (%) %>" DataField="RATE" SortExpression="RATE"
                        UniqueName="RATE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trừ nhanh %>" DataField="EXCEPT_FAST" SortExpression="EXCEPT_FAST"
                        UniqueName="EXCEPT_FAST" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="FROM_DATE"
                        UniqueName="FROM_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="FROM_DATE">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="TO_DATE"
                        UniqueName="TO_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="TO_DATE" Display="False">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="SDESC" SortExpression="SDESC"
                        UniqueName="SDESC" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Thứ tự %>" DataField="ORDERS" SortExpression="ORDERS"
                        UniqueName="ORDERS">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridNumericColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" HeaderStyle-Width="60px">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
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

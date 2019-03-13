﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPE_Classification.ascx.vb"
    Inherits="Performance.ctrlPE_Classification" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="140px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Loại xếp loại")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server" SkinID="Textbox50">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Loại %>" ToolTip="<%$ Translate: Bạn phải nhập Loại xếp loại %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate:Loại xếp loại đã tồn tại. %>"
                        ToolTip="<%$ Translate: Loại xếp loại đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Loại không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
                <td class="lb">
                    <%# Translate("Mô tả xếp loại")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqNamVN" ControlToValidate="txtName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Mô tả xếp loại %>" ToolTip="<%$ Translate: Bạn phải nhập Mô tả xếp loại %>">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="<%$ Translate: Thông tin nhập liệu có chứa mã html %>"
                        ControlToValidate="txtName" ValidationExpression="^(?!.*<[^>]+>).*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Khoảng điểm từ")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtValue_From" runat="server" NumberFormat-DecimalDigits="1"
                        NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="3" MinValue="0" MaxValue="100">
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                    </tlk:RadNumericTextBox>
                     <asp:RequiredFieldValidator ID="reqValue_From" ControlToValidate="txtValue_From" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Khoảng điểm từ %>" ToolTip="<%$ Translate: Bạn phải nhập Khoảng điểm từ %>">
                    </asp:RequiredFieldValidator>
                    %
                </td>
                <td class="lb">
                    <%# Translate("Đến")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtValue_To" runat="server" NumberFormat-DecimalDigits="1"
                        NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="3" MinValue="0" MaxValue="100">
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                    </tlk:RadNumericTextBox>
                     <asp:RequiredFieldValidator ID="reqValue_To" ControlToValidate="txtValue_To" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Khoảng điểm đếns %>" ToolTip="<%$ Translate: Bạn phải nhập Khoảng điểm đến %>">
                    </asp:RequiredFieldValidator>
                    %
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgMain" runat="server" AutoGenerateColumns="False"
            AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME,VALUE_FROM,VALUE_TO">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại xếp loại %>" DataField="CODE"
                        UniqueName="CODE" SortExpression="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả xếp loại %>" DataField="NAME"
                        UniqueName="NAME" SortExpression="NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Khoảng điểm từ (%) %>" DataField="VALUE_FROM" UniqueName="VALUE_FROM"
                        SortExpression="VALUE_FROM" />
                     <tlk:GridBoundColumn HeaderText="<%$ Translate: Khoảng điểm đến (%) %>" DataField="VALUE_TO" UniqueName="VALUE_TO"
                        SortExpression="VALUE_TO" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" />
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

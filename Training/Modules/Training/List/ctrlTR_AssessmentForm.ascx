﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_AssessmentForm.ascx.vb"
    Inherits="Training.ctrlTR_AssessmentForm" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="120px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <asp:Label ID="lbFrCuType" runat="server" Text="Loại đánh giá"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                     <tlk:RadComboBox ID="cboRateType" runat="server" >
                    </tlk:RadComboBox>    
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="cboRateType" runat="server"
                        ErrorMessage="Bạn phải chọn Loại đánh giá" ToolTip="Bạn phải chọn Loại đánh giá">
                    </asp:RequiredFieldValidator>  
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Loại đánh gi không được chứa ký tự đặc biệt và khoảng trắng"
                        ControlToValidate="cboRateType" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên mẫu biểu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" MaxLength="255" runat="server" Width="350px">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqNameVN" ControlToValidate="txtName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Tên mẫu biểu %>" ToolTip="<%$ Translate: Bạn phải nhập Tên mẫu biểu %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtRemark" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgMain" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="NAME,REMARK,RATE_TYPE">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                      <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại đánh giá %>" DataField="RATE_TYPE"
                        UniqueName="RATE_TYPE" SortExpression="RATE_TYPE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên mẫu biểu %>" DataField="NAME"
                        UniqueName="NAME" SortExpression="NAME" HeaderStyle-Width="40%" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" UniqueName="REMARK"
                        SortExpression="REMARK" HeaderStyle-Width="60%" />
                </Columns>
                <ItemStyle Width="100px" />
                <HeaderStyle Width="100px" />
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

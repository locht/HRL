﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlOtherList.ascx.vb"
    Inherits="Common.ctrlOtherList" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="300" Width="300px">
        <tlk:RadTreeView ID="treeOtherListType" runat="server" CausesValidation="false">
        </tlk:RadTreeView>
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="80px" Scrolling="None">
                <tlk:RadToolBar ID="tbarOtherLists" runat="server" />
                <asp:HiddenField ID="hidID" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Mã tham số")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtCODE" SkinID="Textbox15" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Tên tham số")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtOtherListNameVN" MaxLength="255" runat="server">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="reqOtherNameVN" ControlToValidate="txtOtherListNameVN"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tên tham số %>"
                                ToolTip="<%$ Translate:  Bạn phải nhập tên tham số %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgOtherList" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME_VN">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã tham số %>" DataField="CODE" UniqueName="CODE"
                                SortExpression="CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tham số %>" DataField="NAME_VN"
                                UniqueName="NAME_VN" SortExpression="NAME_VN" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Status %>" DataField="ACTFLG" UniqueName="ACTFLG"
                                SortExpression="ACTFLG" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var oldSize = 0;
        var enableAjax = true;

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
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
            }
        }
    </script>
</tlk:RadCodeBlock>

<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlListGroup.ascx.vb"
    Inherits="Common.ctrlListGroup" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="100px" Scrolling="None">
        <tlk:RadToolBar ID="rtbMain" runat="server" />
        <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" OnClientResized="PaneClientResized">
        <tlk:RadGrid PageSize=50 ID="rgGrid" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" />
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhóm %>" DataField="CODE" UniqueName="CODE"
                        SortExpression="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhóm %>" DataField="NAME" UniqueName="NAME"
                        SortExpression="NAME" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                        UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE"
                        UniqueName="EXPIRE_DATE" SortExpression="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
        <Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        var oldSize = 0;

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter1.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter1.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
            }
        }

        function PaneClientResized(sender, eventArgs) {
            var grid = $find("<%=rgGrid.ClientID %>");
            grid.repaint();
        }
    </script>
</tlk:RadScriptBlock>

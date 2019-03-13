<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlOtherList.ascx.vb"
    Inherits="Common.ctrlOtherList" %>
<%@ Register Src="../Common/ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<%@ Import Namespace="Common" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="300" Width="300px">
        <tlk:RadTreeView ID="treeOtherListType" runat="server" CausesValidation="false">
        </tlk:RadTreeView>
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="120px" Scrolling="None">
                <tlk:RadToolBar ID="tbarOtherLists" runat="server" />
                <asp:HiddenField ID="hidID" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Mã tham số")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtCode" SkinID="Textbox50" runat="server">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải nhập Mã tham số %>" ToolTip="<%$ Translate: Bạn phải nhập Mã tham số %>">
                            </asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã tham số đã tồn tại %>"
                                ToolTip="<%$ Translate: Mã tham số đã tồn tại %>">
                            </asp:CustomValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                                ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Tên tham số")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtOtherListNameVN" runat="server">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="reqOtherNameVN" ControlToValidate="txtOtherListNameVN"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tên tham số %>"
                                ToolTip="<%$ Translate:  Bạn phải nhập tên tham số %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mô tả")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtRemark" SkinID="Textbox1023" Width="100%" Height = "37px" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgOtherList" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME_VN,REMARK">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã tham số %>" DataField="CODE" UniqueName="CODE"
                                SortExpression="CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tham số %>" DataField="NAME_VN"
                                UniqueName="NAME_VN" SortExpression="NAME_VN" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả %>" DataField="REMARK" UniqueName="REMARK"
                                SortExpression="REMARK" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Status %>" DataField="ACTFLG" UniqueName="ACTFLG"
                                SortExpression="ACTFLG" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect ="True" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlOtherList_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlOtherList_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlOtherList_RadPane2';
        var validateID = 'MainContent_ctrlOtherList_valSum';
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
            registerOnfocusOut('ctl00_MainContent_ctrlOtherList_RadSplitter3');
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                var grid = $find("<%=rgOtherList.ClientID %>");
                var masterTable = grid.get_masterTableView();
                var rows = masterTable.get_dataItems();
                if (rows.length == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgOtherList');
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

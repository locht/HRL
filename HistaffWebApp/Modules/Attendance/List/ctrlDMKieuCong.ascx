<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDMKieuCong.ascx.vb"
    Inherits="Attendance.ctrlDMKieuCong" %>
<%@ Import Namespace="Common" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="165px" Scrolling="None">
        <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã ký hiệu công")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" SkinID="Textbox50" runat="server" Width="250px" />
                </td>
                <td>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCode"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập mã ký hiệu công. %>"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã kiểu công đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã kiểu công đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
                <td class="lb">
                    <%# Translate("Công nghỉ")%>
                </td>
                <td>
                    <asp:CheckBox ID="chkIsLeave" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên ký hiệu công")%><span class="lbReq">*</span>
                </td>
                <td colspan="4">
                    <tlk:RadTextBox ID="txtNameVN" runat="server"  SkinID="Textbox1023" Width="100%" Height="40px" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNameVN"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tên kiểu công. %>" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mô tả")%>
                </td>
                <td colspan="4">
                    <tlk:RadTextBox ID="txtNote" runat="server" SkinID="Textbox1023" Width="100%" Height="40px" />
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgDanhMuc" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME_VN,NAME_EN,EFFECT_DATE,IS_LEAVE,ACTFLG,NOTE">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã ký hiệu công %>" DataField="CODE"
                        UniqueName="CODE" SortExpression="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên ký hiệu công %>" DataField="NAME_VN"
                        UniqueName="NAME_VN" SortExpression="NAME_VN" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Công nghỉ %>" AllowFiltering="false"
                        DataField="IS_LEAVE" SortExpression="IS_LEAVE" UniqueName="IS_LEAVE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả %>" DataField="NOTE" UniqueName="NOTE"
                        SortExpression="NOTE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG">
                        <HeaderStyle Width="100px" />
                    </tlk:GridBoundColumn>
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlDMKieuCong_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlDMKieuCong_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlDMKieuCong_RadPane2';
        var validateID = 'MainContent_ctrlDMKieuCong_valSum';
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
            registerOnfocusOut(splitterID);
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "EXPORT") {
                var rows = $find('<%= rgDanhMuc.ClientID %>').get_masterTableView().get_dataItems().length;
                if (rows == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else if (args.get_item().get_commandName() == "EDIT") {
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                var bCheck = $find('<%= rgDanhMuc.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                 }
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

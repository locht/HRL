<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_AllowanceList.ascx.vb"
    Inherits="Profile.ctrlHU_AllowanceList" %>
    <%@ Import Namespace ="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="190px" Scrolling="None">
        <tlk:RadToolBar ID="tbarAllowanceLists" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã phụ cấp")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" SkinID="Textbox50" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập mã phụ cấp. %>" ToolTip="<%$ Translate: Bạn phải nhập mã phụ cấp. %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã phụ cấp đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã phụ cấp đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên phụ cấp")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tên phụ cấp. %>" ToolTip="<%$ Translate: Bạn phải nhập tên phụ cấp. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                 <td class="lb">
                    <%# Translate("Loại phụ cấp")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cbDMAlow"></tlk:RadComboBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cbDMAlow" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tên phụ cấp. %>" ToolTip="<%$ Translate: Bạn phải nhập tên phụ cấp. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Nhóm phụ cấp")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboNAlow"></tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboNAlow" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tên phụ cấp. %>" ToolTip="<%$ Translate: Bạn phải nhập tên phụ cấp. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr> 
                <td class="lb">
                    <%# Translate("Thứ tự")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtOrder" runat="server">
                    </tlk:RadNumericTextBox>
                </td>           
                <td>
                    <tlk:RadButton runat="server" ID="chkIsContract" ToggleType="CheckBox" ButtonType="ToggleButton"
                        Enabled="false" CausesValidation="false" Text=" <%$ Translate: Hiển thị trên HĐLĐ %>"
                        AutoPostBack="false">
                    </tlk:RadButton>
                </td>
                <td>
                     <tlk:RadButton runat="server" ID="chkIsInsurrance" ToggleType="CheckBox" ButtonType="ToggleButton"
                        Enabled="false" CausesValidation="false" Text=" <%$ Translate: Đóng bảo hiểm %>"
                        AutoPostBack="false">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgMain" runat="server" AutoGenerateColumns="False" AllowPaging="false"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME,IS_INSURANCE,ALLOWANCE_TYPE,ALLOWANCE_TYPE_NAME,ALLOWANCE_GROUP,ALLOWANCE_GROUP_NAME,ORDERS,IS_CONTRACT">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã phụ cấp %>" DataField="CODE" SortExpression="CODE"
                        UniqueName="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên phụ cấp %>" DataField="NAME"
                        SortExpression="NAME" UniqueName="NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại phụ cấp %>" DataField="ALLOWANCE_TYPE_NAME"
                        SortExpression="ALLOWANCE_TYPE_NAME" UniqueName="ALLOWANCE_TYPE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm phụ cấp %>" DataField="ALLOWANCE_GROUP_NAME"
                        SortExpression="ALLOWANCE_GROUP_NAME" UniqueName="ALLOWANCE_GROUP_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thứ tự %>" DataField="ORDERS"
                        SortExpression="ORDERS" UniqueName="ORDERS" />
                     <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Hiển thị trên HĐLĐ %>" DataField="IS_CONTRACT" 
                        AllowFiltering ="false" FooterStyle-HorizontalAlign="Center"
                                    SortExpression="IS_CONTRACT" UniqueName="IS_CONTRACT">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                     </tlk:GridCheckBoxColumn>
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đóng bảo hiểm %>" DataField="IS_INSURANCE" 
                        AllowFiltering ="false" FooterStyle-HorizontalAlign="Center"
                                    SortExpression="IS_INSURANCE" UniqueName="IS_INSURANCE">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </tlk:GridCheckBoxColumn>
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

        var splitterID = 'ctl00_MainContent_ctrlHU_AllowanceList_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_AllowanceList_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_AllowanceList_RadPane2';
        var validateID = 'MainContent_ctrlHU_AllowanceList_valSum';
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
            if (args.get_item().get_commandName() == 'EXPORT') {
                var rows = $find('<%= rgMain.ClientID %>').get_masterTableView().get_dataItems().length;
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
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgMain');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else if (args.get_item().get_commandName() == "EDIT") {
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                Page_IsValid = false;

                var bCheck = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
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

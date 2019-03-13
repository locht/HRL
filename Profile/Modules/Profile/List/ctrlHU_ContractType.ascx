<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ContractType.ascx.vb"
    Inherits="Profile.ctrlHU_ContractType" %>
<%@ Import Namespace ="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="150px" Scrolling="None">
        <tlk:RadToolBar ID="tbarContractTypes" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã loại hợp đồng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập mã loại hợp đồng. %>" ToolTip="<%$ Translate: Bạn phải nhập mã loại hợp đồng. %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã loại hợp đồng đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã loại hợp đồng đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
                <td class="lb">
                    <%# Translate("Thời hạn loại hợp đồng")%> <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtPeriod" runat="server" MinValue="0">
                        <NumberFormat ZeroPattern="n" DecimalDigits="0"></NumberFormat>
                    </tlk:RadNumericTextBox>
                    <%# Translate("(tháng)")%>
                    <asp:RequiredFieldValidator ID="reqPeriod" ControlToValidate="rntxtPeriod" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập thời hạn loại hợp đồng >= 0. %>" ToolTip="<%$ Translate: Bạn phải nhập thời hạn loại hợp đồng >=0. %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalPeriod" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập thời hạn loại hợp đồng >= 0. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập thời hạn loại hợp đồng >= 0. %>">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên loại hợp đồng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tên loại hợp đồng. %>" ToolTip="<%$ Translate: Bạn phải nhập tên loại hợp đồng. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Loại hợp đồng")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboContract_Type" runat="server" CausesValidation="false" >
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="472px" Height="43px" />
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgContractType" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME,PERIOD,REMARK,TYPE_ID">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã loại HĐLĐ %>" DataField="CODE" SortExpression="CODE"
                        UniqueName="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên loại hợp đồng %>" DataField="NAME"
                        SortExpression="NAME" UniqueName="NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại hợp đồng %>" DataField="TYPE_NAME" SortExpression="TYPE_NAME"
                        UniqueName="TYPE_NAME" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Thời hạn loại hợp đồng (tháng) %>" DataField="PERIOD"
                        SortExpression="PERIOD" UniqueName="PERIOD" ItemStyle-HorizontalAlign="Right" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" SortExpression="REMARK"
                        UniqueName="REMARK" />
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

    var splitterID = 'ctl00_MainContent_ctrlHU_ContractType_RadSplitter3';
    var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_ContractType_RadPane1';
    var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_ContractType_RadPane2';
    var validateID = 'MainContent_ctrlHU_ContractType_valSum';
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
            var rows = $find('<%= rgContractType.ClientID %>').get_masterTableView().get_dataItems().length;
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
                ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgContractType');
            else
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
        } else if (args.get_item().get_commandName() == "EDIT") {
            ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            var bCheck = $find('<%= rgContractType.ClientID %>').get_masterTableView().get_selectedItems().length;
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

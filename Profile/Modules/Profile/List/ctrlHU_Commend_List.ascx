﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Commend_List.ascx.vb"
    Inherits="Profile.ctrlHU_Commend_List" %>
<%@ Import Namespace="Framework.UI" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="245px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <asp:HiddenField ID="hfID" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã khen thưởng")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server" SkinID="Textbox50">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Mã khen thưởng %>" ToolTip="<%$ Translate: Bạn phải nhập Mã khen thưởng %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã khen thưởng đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã khen thưởng đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên khen thưởng")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNameVN" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqNamVN" ControlToValidate="txtNameVN" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Tên khen thưởng %>" ToolTip="<%$ Translate: Bạn phải nhập Tên khen thưởng %>">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="<%$ Translate: Thông tin nhập liệu có chứa mã html %>"
                        ControlToValidate="txtNameVN" ValidationExpression="^(?!.*<[^>]+>).*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Kiểu dữ liệu")%> <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbDatatype" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqDatatype" ControlToValidate="cbDatatype" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn Kiểu dữ liệu %>" ToolTip="<%$ Translate: Bạn phải chọn Kiểu dữ liệu %>">
                    </asp:RequiredFieldValidator>
                     <asp:CustomValidator ID="cusDatatype" ControlToValidate="cbDatatype" runat="server" ErrorMessage="<%$ Translate: Kiểu dữ liệu không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Kiểu dữ liệu không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Loại danh mục")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbTYPE" runat="server">
                    </tlk:RadComboBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cbTYPE"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn loại danh mục %>" ToolTip="<%$ Translate: Bạn phải chọn loại danh mục %>"> 
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cusTYPE" ControlToValidate="cbTYPE" runat="server" ErrorMessage="<%$ Translate: Loại danh mục không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Loại danh mục không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Thứ tự")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmNumberOrder" MinValue="0" MaxLength="38" runat="server">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="cvalNumberOrder" ControlToValidate="nmNumberOrder"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập thứ tự. %>" ToolTip="<%$ Translate: Bạn phải nhập thứ tự. %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Cấp khen thưởng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbLevel" runat="server">
                    </tlk:RadComboBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cbLevel"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn cấp khen thưởng %>" ToolTip="<%$ Translate: Bạn phải chọn cấp khen thưởng %>"> 
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cusLevel" ControlToValidate="cbLevel" runat="server" ErrorMessage="<%$ Translate: Cấp khen thưởng không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Cấp khen thưởng không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Đối tượng khen thưởng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbObject" runat="server">
                    </tlk:RadComboBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cbLevel"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đối tượng khen thưởng %>" ToolTip="<%$ Translate: Bạn phải chọn đối tượng khen thưởng %>"> 
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cusObject" ControlToValidate="cbObject" runat="server" ErrorMessage="<%$ Translate: Đối tượng khen thưởng không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Đối tượng khen thưởng không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%" Height = "38px">
                    </tlk:RadTextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="<%$ Translate: Thông tin nhập liệu có chứa mã html %>"
                        ControlToValidate="txtRemark" ValidationExpression="^(?!.*<[^>]+>).*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                </td>
                <td>
                    <tlk:RadButton ID="chkExcel" AutoPostBack="false" ToggleType="CheckBox" ButtonType="ToggleButton" ValidationGroup="Val"
                        runat="server" Text="<%$ Translate: Import dữ liệu từ excel %>">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgMain" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true" >
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME,DATATYPE_ID,DATATYPE_NAME,TYPE_ID,TYPE_NAME,NUMBER_ORDER,LEVEL_ID,LEVEL_NAME,OBJECT_ID,OBJECT_NAME,REMARK,EXCEL,EXCEL_BOOL">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã khen thưởng %>" DataField="CODE"
                        UniqueName="CODE" SortExpression="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên khen thưởng%>" DataField="NAME"
                        UniqueName="NAME" SortExpression="NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kiểu dữ liệu %>" DataField="DATATYPE_NAME"
                        UniqueName="DATATYPE_NAME" SortExpression="DATATYPE_NAME" Display="False" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại danh mục %>" DataField="TYPE_NAME"
                        UniqueName="TYPE_NAME" SortExpression="TYPE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thứ tự %>" DataField="NUMBER_ORDER"
                        UniqueName="NUMBER_ORDER" SortExpression="NUMBER_ORDER" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp khen thưởng %>" DataField="LEVEL_NAME"
                        UniqueName="LEVEL_NAME" SortExpression="LEVEL_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng khen thưởng %>" DataField="OBJECT_NAME"
                         UniqueName="OBJECT_NAME" SortExpression="OBJECT_NAME" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Import từ excel %>" DataField="EXCEL_BOOL" AllowFiltering ="false"
                         SortExpression="EXCEL" UniqueName="EXCEL" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" UniqueName="REMARK"
                        SortExpression="REMARK" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
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
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_Commend_List_RadSplitter3');
        }

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
                var masterTable = $find("<%= rgMain.ClientID %>").get_masterTableView();
                masterTable.rebind();
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
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }

        window.addEventListener('keydown', function (e) {
            if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
                if (e.target.nodeName == 'INPUT' && e.target.type == 'text') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true);
    </script>
</tlk:RadCodeBlock>

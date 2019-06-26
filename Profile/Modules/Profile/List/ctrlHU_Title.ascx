<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Title.ascx.vb"
    Inherits="Profile.ctrlHU_Title" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="180px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Nhóm chức danh")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTitleGroup" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusTitleGroup" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Nhóm chức danh %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Nhóm chức danh %>" ClientValidationFunction="cusTitleGroup">
                    </asp:CustomValidator>
                     <asp:CustomValidator ID="cvalTitleGroup" ControlToValidate="cboTitleGroup" runat="server"
                        ErrorMessage="<%$ Translate: Nhóm chức danh không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Nhóm chức danh không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã chức danh")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Mã chức danh %>" ToolTip="<%$ Translate: Bạn phải nhập Mã chức danh %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã chức danh đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã chức danh đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên chức danh")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNameVN" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqNamVN" ControlToValidate="txtNameVN" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Tên chức danh %>" ToolTip="<%$ Translate: Bạn phải nhập Tên chức danh %>">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="<%$ Translate: Thông tin nhập liệu có chứa mã html %>"
                        ControlToValidate="txtNameVN" ValidationExpression="^(?!.*<[^>]+>).*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mô tả công việc")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Height="70" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="<%$ Translate: Thông tin nhập liệu có chứa mã html %>"
                        ControlToValidate="txtRemark" ValidationExpression="^(?!.*<[^>]+>).*"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgMain" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME_VN,TITLE_GROUP_NAME,TITLE_GROUP_ID,REMARK">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã chức danh %>" DataField="CODE" HeaderStyle-Width="100px"
                        UniqueName="CODE" SortExpression="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên chức danh %>" DataField="NAME_VN"
                        UniqueName="NAME_VN" SortExpression="NAME_VN" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm chức danh %>" DataField="TITLE_GROUP_NAME"
                        UniqueName="TITLE_GROUP_NAME" SortExpression="TITLE_GROUP_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả công việc %>" DataField="REMARK" UniqueName="REMARK"
                        SortExpression="REMARK" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" />
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
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
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_Title_RadSplitter3');
        }

        function cusTitleGroup(oSrc, args) {
            var cbo = $find("<%# cboTitleGroup.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        var enableAjax = true;
        var oldSize = 180;

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        var space = 0;
        var defaultPage = 0;

        function pageLoad(sender, args) {
            $(document).ready(function () {
                ResizeSplitterDefault();
            });
        }

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
                var validateHeight = $("#<%= valSum.ClientID%>").height();
                var ePager = $("#ctl00_MainContent_ctrlHU_Title_rgMain_GridData");

                var height = pane.getContentElement().scrollHeight;
                var splHeight = splitter.get_height();
                var pane1Height = pane.get_height();
                var pagerHeight = 0;


                if (height > 182) {
                    space = height - validateHeight - 200;
                }

                pane.set_height(height - space);

                if (validateHeight > 0) {
                    var re = defaultPage - (validateHeight + 26);
                    ePager.css({ 'height': re + 'px', 'overflow': 'auto' });
                }
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');

            defaultPage = $("#ctl00_MainContent_ctrlHU_Title_rgMain_GridData").height();

            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');

                var splHeight = splitter.get_height();
                var paneHeight = pane.get_height();
                var newHeight = splHeight + paneHeight - oldSize;

                splitter.set_height(newHeight);
                pane.set_height(oldSize);
                pane2.set_height(splHeight - oldSize - 1);
            }
        }       
    </script>
</tlk:RadCodeBlock>

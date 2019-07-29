<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Title.ascx.vb"
    Inherits="Profile.ctrlHU_Title" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="230px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbTitleGroup" Text ="Nhóm chức danh" ></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTitleGroup" runat="server" AutoPostBack ="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusTitleGroup" runat="server" ErrorMessage="Bạn phải chọn Nhóm chức danh"
                        ToolTip="Bạn phải chọn Nhóm chức danh" ClientValidationFunction="cusTitleGroup">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvalTitleGroup" ControlToValidate="cboTitleGroup" runat="server"
                        ErrorMessage="Nhóm chức danh không tồn tại hoặc đã ngừng áp dụng."
                        ToolTip="Nhóm chức danh không tồn tại hoặc đã ngừng áp dụng.">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbOrgLevel" Text ="Tên công ty" ></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboOrgLevel" runat="server" AutoPostBack ="true" >
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusOrgLevel" runat="server" ErrorMessage="Bạn phải chọn Tên công ty"
                        ToolTip="Bạn phải chọn Tên công ty" ClientValidationFunction="cusOrgLevel">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbOrgType" Text ="Loại tổ chức" ></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboOrgType" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbCode" Text ="Mã chức danh" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server" SkinID="Readonly" ReadOnly="true" >
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="Bạn phải nhập Mã chức danh" ToolTip="Bạn phải nhập Mã chức danh">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="Mã chức danh đã tồn tại."
                        ToolTip="Mã chức danh đã tồn tại.">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Mã không được chứa ký tự đặc biệt và khoảng trắng"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbNameVN" Text ="Tên chức danh" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNameVN" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqNamVN" ControlToValidate="txtNameVN" runat="server"
                        ErrorMessage="Bạn phải nhập Tên chức danh" ToolTip="Bạn phải nhập Tên chức danh">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Thông tin nhập liệu có chứa mã html"
                        ControlToValidate="txtNameVN" ValidationExpression="^(?!.*<[^>]+>).*"></asp:RegularExpressionValidator>
                </td>
                <td class="lb">
                    <%--<asp:CheckBox ID="ckDH" runat="server" Text="Độc hại" />--%>
                    <asp:Label runat="server" ID="lbHurtType" Text = "Đối tượng độc hại"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboHurtType"></tlk:RadComboBox>
                </td>
                <td>
                    <%--<asp:CheckBox ID="ckSpecDH" runat="server" Text="Đặc biệt độc hại" />--%>
                    <asp:CheckBox ID="ckOVT" runat="server" Text="Tính OVT" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbUploadFile" Text="Tập tin đính kèm"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnUploadFile" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" />
                    <tlk:RadButton ID="btnDownload" runat="server" Text="Tải xuống" CausesValidation="false"
                        OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbRemark" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Height="70" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Thông tin nhập liệu có chứa mã html"
                        ControlToValidate="txtRemark" ValidationExpression="^(?!.*<[^>]+>).*"></asp:RegularExpressionValidator>
                </td>
                <td style="visibility: hidden">
                    <tlk:RadTextBox ID="txtRemindLink" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgMain" runat="server" AutoGenerateColumns="false"
            AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME_VN,TITLE_GROUP_NAME,TITLE_GROUP_ID,REMARK,ORG_ID,ORG_TYPE,FILENAME,HURTFUL,HURTFUL_CHECK,OVT,OVT_CHECK,SPEC_HURFUL,SPEC_HURFUL_CHECK,UPLOAD_FILE,HURT_TYPE_ID,HURT_TYPE_NAME">
                <Columns>
                    <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="Mã chức danh" DataField="CODE"
                        HeaderStyle-Width="100px" UniqueName="CODE" SortExpression="CODE" />
                    <tlk:GridBoundColumn HeaderText="Tên công ty" DataField="ORG_ID_NAME"
                        HeaderStyle-Width="100px" UniqueName="ORG_ID_NAME" SortExpression="ORG_ID_NAME" />
                    <tlk:GridBoundColumn HeaderText="Loại tổ chức" DataField="ORG_TYPE_NAME"
                        HeaderStyle-Width="100px" UniqueName="ORG_TYPE_NAME" SortExpression="ORG_TYPE_NAME" />
                    <tlk:GridBoundColumn HeaderText="Tên chức danh" DataField="NAME_VN"
                        UniqueName="NAME_VN" SortExpression="NAME_VN" />
                    <tlk:GridBoundColumn HeaderText="Nhóm chức danh" DataField="TITLE_GROUP_NAME"
                        UniqueName="TITLE_GROUP_NAME" SortExpression="TITLE_GROUP_NAME" />
                    <tlk:GridBoundColumn HeaderText="Mô tả công việc" DataField="REMARK"
                        UniqueName="REMARK" SortExpression="REMARK" />
                    <tlk:GridCheckBoxColumn HeaderText="Độc hại" DataField="HURTFUL_CHECK"
                        UniqueName="HURTFUL_CHECK" SortExpression="HURTFUL_CHECK" AllowFiltering="false" />
                    <tlk:GridCheckBoxColumn HeaderText="Đặc biệt độc hại" DataField="SPEC_HURFUL_CHECK"
                        UniqueName="SPEC_HURFUL_CHECK" SortExpression="SPEC_HURFUL_CHECK" AllowFiltering="false" />
                    <tlk:GridBoundColumn HeaderText="Đối tượng độc hại" DataField="HURT_TYPE_NAME"
                        UniqueName="HURT_TYPE_NAME" SortExpression="HURT_TYPE_NAME" />
                    <tlk:GridCheckBoxColumn HeaderText="Tính OVT" DataField="OVT_CHECK" UniqueName="OVT_CHECK"
                        SortExpression="OVT_CHECK" AllowFiltering="false" />
                    <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" />
                    <tlk:GridBoundColumn HeaderText="Tài liệu đính kèm" DataField="FILENAME"
                        HeaderStyle-Width="100px" UniqueName="FILENAME" SortExpression="FILENAME" />--%>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />                
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlHU_Title_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_Title_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_Title_RadPane2';
        var validateID = 'MainContent_ctrlHU_Title_valSum';
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
                var bCheck = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck > 1) {
                    var l = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var k = noty({ text: l, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(k.options.id); }, 5000);
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

        function cusTitleGroup(oSrc, args) {
            var cbo = $find("<%# cboTitleGroup.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusOrgLevel(oSrc, args) {
            var cbo = $find("<%# cboOrgLevel.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
    </script>
</tlk:RadCodeBlock>

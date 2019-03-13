<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_SalaryGroup.ascx.vb"
    Inherits="Payroll.ctrlPA_SalaryGroup" %>
<%@ Import Namespace="Framework.UI" %>
<%@ Import Namespace="Common" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="155px" Scrolling="None">
        <tlk:RadToolBar ID="tbarSalaryGroups" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã bảng lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" SkinID="Textbox50" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập mã bảng lương. %>" ToolTip="<%$ Translate: Bạn phải nhập mã bảng lương. %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã bảng lương đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã bảng lương đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã bảng lương không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
                <td class="lb" style="width: 100px">
                    <%# Translate("Tên bảng lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tên bảng lương. %>" ToolTip="<%$ Translate: Bạn phải nhập tên bảng lương. %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 100px">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" ControlToValidate="rdEffectDate" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb" id="tdSGlbOrders">
                    <label id="lbOrders">
                        <%# Translate("Thứ tự")%><span class="lbReq">*</span></label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtOrders" MinValue="1" Width="60px"
                        Value="1" ShowSpinButtons="True" ReadOnly="true" CausesValidation="false">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rntxtOrders"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập số thứ tự. %>" ToolTip="<%$ Translate: Bạn phải nhập số thứ tự. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb" id="tdSGlbIs_Incentive" style='<%# if(Utilities.Account = "U", "display: None" , "Block") %>'>
                    <label id="Label3">
                        <%# Translate("Bảng thưởng HQCV")%></label>
                </td>
                <td id="tdSGcbIs_Incentive" runat="server" style='<%# if(Utilities.Account = "U", "display: None" , "Block") %>'>
                    <div style="margin: -4px; margin-top: 1px">
                        <tlk:RadButton ID="cbIS_INCENTIVE" ToggleType="CheckBox" ButtonType="ToggleButton"
                            Text="" runat="server" AutoPostBack="false" CausesValidation="false">
                        </tlk:RadButton>
                    </div>
                </td>
                <td class="lb">
                    <label id="Label1">
                        <%# Translate("IS_COEFFICIENT")%></label>
                </td>
                <td>
                    <div style="margin: -4px; margin-top: 1px">
                        <tlk:RadButton ID="cbIS_COEFFICIENT" ToggleType="CheckBox" ButtonType="ToggleButton"
                            Text="" runat="server" AutoPostBack="false" CausesValidation="false">
                        </tlk:RadButton>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="98.9%" Height="40px">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME,EFFECT_DATE,REMARK,IS_COEFFICIENT,IS_INCENTIVE,ORDERS">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã bảng lương %>" DataField="CODE"
                        SortExpression="CODE" UniqueName="CODE" HeaderStyle-Width="130px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên bảng lương %>" DataField="NAME"
                        SortExpression="NAME" UniqueName="NAME" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                        UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE"
                        HeaderStyle-Width="130px">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridTemplateColumn HeaderText="<%$ Translate: IS_COEFFICIENT %>" UniqueName="IS_COEFFICIENT"
                        DataField="IS_COEFFICIENT">
                        <ItemTemplate>
                            <%--<input id="chk_Display" type="checkbox" checked='<%# Eval("IS_INCENTIVE") %>' />--%>
                            <%--<asp:CheckBox ID="chkIS_INCENTIVE" runat="server" Enabled="False" Checked='<%# if(Eval("IS_INCENTIVE").ToString() = "0", false , true) %>' />--%>
                            <%--<input id="chkIS" type="checkbox" Checked='<%# if(DataBinder.Eval(Container.DataItem, "IS_INCENTIVE").ToString() = "0", false , true) %>' />--%>
                            <tlk:RadButton ID="chkIS_COEFFICIENT" ToggleType="CheckBox" ButtonType="ToggleButton"
                                Checked='<%# if(Eval("IS_COEFFICIENT").ToString() = "0", false , true) %>' Text=""
                                runat="server" AutoPostBack="false" CausesValidation="false" ReadOnly="True">
                            </tlk:RadButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn HeaderText="<%$ Translate: Bảng thưởng HQCV %>" UniqueName="IS_INCENTIVE"
                        DataField="IS_INCENTIVE">
                        <ItemTemplate>
                            <tlk:RadButton ID="chkIS_INCENTIVE" ToggleType="CheckBox" ButtonType="ToggleButton"
                                Checked='<%# ParseBoolean(Eval("IS_INCENTIVE").ToString()) %>' Text="" runat="server"
                                AutoPostBack="false" CausesValidation="false" ReadOnly="True">
                            </tlk:RadButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                    </tlk:GridTemplateColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thứ tự %>" DataField="ORDERS" SortExpression="ORDERS"
                        UniqueName="ORDERS" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        SortExpression="ACTFLG" UniqueName="ACTFLG" HeaderStyle-Width="130px" />
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
                <KeyboardNavigationSettings AllowSubmitOnEnter="true" EnableKeyboardShortcuts="true" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctl00_MainContent_PagePlaceHolderPanel';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_SalaryGroup_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_SalaryGroup_RadPane2';
        var validateID = 'MainContent_ctrlPA_SalaryGroup_ValidationSummary1';
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
            if (item.get_commandName() == "EXPORT") {
                var rows = $find('<%= rgData.ClientID %>').get_masterTableView().get_dataItems().length;
                if (rows == 0) {
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
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData');
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

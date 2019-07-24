<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsListParamInsurance.ascx.vb"
    Inherits="Insurance.ctrlInsListParamInsurance" %>
    <%@ Import Namespace="Common" %>
    <link type  = "text/css" href = "/Styles/StyleCustom.css" rel = "Stylesheet"/>
<%@ Register Src="~/Modules/Common/ctrlMessageBox.ascx" TagName="ctrlMessageBox"
    TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter4" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPaneField" runat="server" Width="100%" Height="285px">
        <tlk:RadToolBar ID="tbarOtherLists" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectiveDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqrcboCHANGE_TYPE" ControlToValidate="rdEffectiveDate"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày chốt sổ BHXH")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtSI_DATE"  MinValue="1" MaxValue="31" SkinID="Textbox15"
                        runat="server">
                        <NumberFormat DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                </td>
                 <td class="lb">
                    <%# Translate("Ngày chốt sổ BHYT")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtHI_DATE"  MinValue="1" MaxValue="31" SkinID="Textbox15"
                        runat="server">
                        <NumberFormat DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="2">
                        <%# Translate("Mức trần")%>
                    <hr />
                </td>
                <td class="item-head" colspan="2" style = "Width: 150px">
                        <%# Translate("Tỉ lệ công ty")%>
                    <hr />
                </td>
                <td class="item-head" colspan="2">
                        <%# Translate("Tỉ lệ nhân viên")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("BHXH")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmSI" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                    (VND)
                </td>
                <td class="lb">
                    <%# Translate("BHXH")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmSI_COM" MinValue="0" MaxValue="100" runat="server" 
                        SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    (%)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="radnmSI_COM" 
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tỷ lệ công ty đóng BHXH. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("BHXH")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmSI_EMP" MinValue="0" MaxValue="100" runat="server"
                        SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    (%)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="radnmSI_EMP"  
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tỷ lệ nhân viên đóng BHXH. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("BHYT")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmHI" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                    (VND)
                </td>
                <td class="lb">
                    <%# Translate("BHYT")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmHI_COM" runat="server" MinValue="0" MaxValue="100"
                        SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    (%)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="radnmHI_COM"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tỷ lệ công ty đóng BHYT. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("BHYT")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmHI_EMP" runat="server" MinValue="0" MaxValue="100"
                        SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    (%)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="radnmHI_EMP"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tỷ lệ nhân viên đóng BHYT. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%--<%# Translate("BHTN")%>--%>
                    &nbsp;
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmUI" runat="server" SkinID="Money" Visible ="false">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("BHTN")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmUI_COM" MinValue="0" MaxValue="100" runat="server"
                        SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    (%)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="radnmUI_COM"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tỷ lệ công ty đóng BHTN. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("BHTN")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmUI_EMP" MinValue="0" MaxValue="100" runat="server"
                        SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    (%)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="radnmUI_EMP"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tỷ lệ nhân viên đóng BHTN. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%--<%# Translate("BH TNLD, BNN")%>--%>
                    &nbsp;
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="RadNumericTextBox1" runat="server" SkinID="Money" Visible ="false">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("BH TNLD, BNN")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmTNLD_BNN_COM" MinValue="0" MaxValue="100" runat="server"
                        SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    (%)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="radnmUI_COM"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tỷ lệ công ty đóng BH TNLD, BNN. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("BH TNLD, BNN")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmTNLD_BNN_EMP" MinValue="0" MaxValue="100" runat="server"
                        SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    (%)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="radnmTNLD_BNN_EMP"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tỷ lệ nhân viên đóng BH TNLD, BNN. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("BHXH-NN")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmSI_NN" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                    (VND)
                </td>
                <td class="lb">
                    <%# Translate("BHXH-NN")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmSI_COM_NN" MinValue="0" MaxValue="100" runat="server" 
                        SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    (%)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="radnmSI_COM_NN" 
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tỷ lệ công ty đóng BHXH. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("BHXH-NN")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmSI_EMP_NN" MinValue="0" MaxValue="100" runat="server"
                        SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    (%)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="radnmSI_EMP_NN"  
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tỷ lệ nhân viên đóng BHXH. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="2">
                        <%# Translate("Hệ số hưởng ốm đau thai sản")%>
                    <hr />
                </td>
                <td class="item-head" colspan="2">
                        <%# Translate("Hệ số hưởng chể độ nghỉ DS, PHSK")%>
                    <hr />
                </td>
                <td class="item-head" colspan="2">
                        <%# Translate("Tuổi về hưu")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chế độ ốm đau")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmSICK" MinValue="0" MaxValue="100" runat="server">
                    </tlk:RadNumericTextBox>
                    (%)
                </td>
                <td class="lb">
                    <%# Translate("Nghỉ tại nhà")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmOFF_IN_HOUSE" MinValue="0" MaxValue="100" runat="server">
                    </tlk:RadNumericTextBox>
                    (%)
                </td>
                <td class="lb">
                    <%# Translate("Nam")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmRETIRE_MALE" MinValue="0" MaxValue="1000" runat="server">
                    </tlk:RadNumericTextBox>
                    (Tháng)
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="radnmRETIRE_MALE"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tuổi về hưu nam. %>"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chế độ thai sản")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmMATERNITY" MinValue="0" MaxValue="100" runat="server">
                    </tlk:RadNumericTextBox>
                    (%)
                </td>
                <td class="lb">
                    <%# Translate("Nghỉ tập trung")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmOFF_TOGETHER" MinValue="0" MaxValue="100" runat="server">
                    </tlk:RadNumericTextBox>
                    (%)
                </td>
                <td class="lb">
                    <%# Translate("Nữ")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="radnmRETIRE_FEMALE" MinValue="0" MaxValue="1000" runat="server">
                    </tlk:RadNumericTextBox>
                    (Tháng)
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="radnmRETIRE_MALE"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tuổi về hưu nữ. %>"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPaneGrid" runat="server" Scrolling="None" Width="100%" Height="100%">
        <tlk:RadGrid PageSize=50 ID="rgGridDataRate" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true" AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <%--<ClientEvents OnGridCreated="GridCreated" />--%>
                <ClientEvents OnCommand="ValidateFilter" />
                <KeyboardNavigationSettings AllowSubmitOnEnter="true" EnableKeyboardShortcuts="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,EFFECTIVE_DATE,SI,SI_EMP,SI_COM,HI,HI_EMP,HI_COM,UI,UI_EMP,UI_COM,BHTNLD_BNN_EMP,BHTNLD_BNN_COM,SICK,MATERNITY,OFF_IN_HOUSE,OFF_TOGETHER,RETIRE_MALE,RETIRE_FEMALE,SI_DATE,HI_DATE,SI_NN,SI_EMP_NN,SI_COM_NN">
                <Columns>
                    <tlk:GridBoundColumn DataField="ID" UniqueName="ID" Visible="false" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataFormatString="{0:dd/MM/yyyy}"
                        DataField="EFFECTIVE_DATE" UniqueName="EFFECTIVE_DATE" SortExpression="EFFECTIVE_DATE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày chốt sổ BHXH %>" DataField="SI_DATE"
                        UniqueName="SI_DATE" SortExpression="SI_DATE" DataFormatString="{0:N0}" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày chốt sổ BHYT %>" DataField="HI_DATE"
                        UniqueName="HI_DATE" SortExpression="HI_DATE" DataFormatString="{0:N0}" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Mức trần BHXH %>" DataFormatString="{0:N0}"
                        DataField="SI" UniqueName="SI" SortExpression="SI" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: % Nhân viên BHXH %>" DataFormatString="{0:N2}"
                        DataField="SI_EMP" UniqueName="SI_EMP" SortExpression="SI_EMP" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: % Công ty BHXH %>" DataFormatString="{0:N2}"
                        DataField="SI_COM" UniqueName="SI_COM" SortExpression="SI_COM" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Mức trần BHYT %>" DataFormatString="{0:N0}"
                        DataField="HI" UniqueName="HI" SortExpression="HI" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: % Nhân viên BHYT %>" DataField="HI_EMP"
                        UniqueName="HI_EMP" SortExpression="HI_EMP" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: % Công ty BHYT %>" DataField="HI_COM"
                        UniqueName="HI_COM" SortExpression="HI_COM" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Mức trần BHTN %>" DataFormatString="{0:N0}"
                        DataField="UI" UniqueName="UI" SortExpression="UI" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: % Nhân viên BHTN %>" DataField="UI_EMP"
                        UniqueName="UI_EMP" SortExpression="UI_EMP" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: % Công ty BHTN %>" DataField="UI_COM"
                        UniqueName="UI_COM" SortExpression="UI_COM" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: % Nhân viên BH TNLD, BNN %>" DataField="BHTNLD_BNN_EMP"
                        UniqueName="BHTNLD_BNN_EMP" SortExpression="BHTNLD_BNN_EMP" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: % Công ty BH TNLD, BNN %>" DataField="BHTNLD_BNN_COM"
                        UniqueName="BHTNLD_BNN_COM" SortExpression="BHTNLD_BNN_COM" />

                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Mức trần BHXH-NN %>" DataFormatString="{0:N0}"
                        DataField="SI_NN" UniqueName="SI_NN" SortExpression="SI_NN" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: % Nhân viên BHXH-NN  %>" DataFormatString="{0:N2}"
                        DataField="SI_EMP_NN" UniqueName="SI_EMP_NN" SortExpression="SI_EMP_NN" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: % Công ty BHXH-NN  %>" DataFormatString="{0:N2}"
                        DataField="SI_COM_NN" UniqueName="SI_COM_NN" SortExpression="SI_COM_NN" />


                    <tlk:GridNumericColumn HeaderText="<%$ Translate: % hường chế độ ốm đau %>" DataFormatString="{0:N0}"
                        DataField="SICK" UniqueName="SICK" SortExpression="SICK" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: % hưởng chế độ thai sản %>" DataFormatString="{0:N0}"
                        DataField="MATERNITY" UniqueName="MATERNITY" SortExpression="MATERNITY" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: % Hưởng chế độ nghỉ tại nhà %> "
                        DataFormatString="{0:N0}" DataField="OFF_IN_HOUSE" UniqueName="OFF_IN_HOUSE"
                        SortExpression="OFF_IN_HOUSE" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: % Hưởng chế độ nghỉ tập trung %>"
                        DataFormatString="{0:N0}" DataField="OFF_TOGETHER" UniqueName="OFF_TOGETHER"
                        SortExpression="OFF_TOGETHER" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Tuổi về hưu nữ (Tháng) %>" DataFormatString="{0:N0}"
                        DataField="RETIRE_MALE" UniqueName="RETIRE_MALE" SortExpression="RETIRE_MALE" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Tuổi về hưu nữ (Tháng) %>" DataFormatString="{0:N0}"
                        DataField="RETIRE_FEMALE" UniqueName="RETIRE_FEMALE" SortExpression="RETIRE_FEMALE" />
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlInsListParamInsurance_RadSplitter4';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlInsListParamInsurance_RadPaneField';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlInsListParamInsurance_RadPaneGrid';
        var validateID = 'MainContent_ctrlInsListParamInsurance_ValidationSummary1';
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

        //        function GridCreated(sender, eventArgs) {
        //            registerOnfocusOut(splitterID);
        //        }
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%=rgGridDataRate.ClientID %>");
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
            } else if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                { } //ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgGridDataRate');
                else
                    ResizeSplitterDefault();
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
                var splitter = $find("<%= RadSplitter4.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPaneField.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter4.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPaneField.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPaneGrid.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
    </script>
</tlk:RadCodeBlock>

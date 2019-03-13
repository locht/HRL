<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRegisterCONewEdit.ascx.vb"
    Inherits="Attendance.ctrlRegisterCONewEdit" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="600px" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="185px">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidDecisionID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidOrgAbbr" runat="server" />
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="lb">
                    <%# Translate("Từ ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdLeaveFrom" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdLeaveFrom"
                        runat="server" ErrorMessage="<%$ Translate: Nghỉ từ ngày chưa chọn. %>" ToolTip="<%$ Translate: Nghỉ từ ngày chưa chọn. %>"> </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" ControlToValidate="rdLeaveFrom" ControlToCompare="rdLeaveTo"
                        Operator="LessThanEqual" Type="Date" runat="server" ErrorMessage="<%$ Translate: Từ ngày phải nhỏ hơn đến ngày. %>"
                        ToolTip="<%$ Translate: Từ ngày phải nhỏ hơn đến ngày. %>"> </asp:CompareValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdLeaveTo" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdLeaveTo"
                        runat="server" ErrorMessage="<%$ Translate: Nghỉ đến ngày chưa chọn. %>" ToolTip="<%$ Translate: Nghỉ đến ngày chưa chọn. %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Kiểu công")%><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadComboBox ID="cboKieuCong" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="cboKieuCong"
                        runat="server" ErrorMessage="<%$ Translate: Chưa chọn kiểu công. %>" ToolTip="<%$ Translate: Chưa chọn kiểu công. %>" />
                    <asp:CustomValidator ID="cvalKieuCong" ControlToValidate="cboKieuCong" runat="server" ErrorMessage="<%$ Translate: Kiểu công không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Kiểu công không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Kiểu công nửa ngày")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbosang" runat="server" Enabled="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Kiểu công nửa ngày")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboChieu" runat="server" Enabled="false">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi Chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtnote" runat="server" Width="100%" SkinID="Textbox1023" Height="40px">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgWorkschedule" SkinID="GridSingleSelect"
            runat="server" Height="100%">
            <MasterTableView AllowPaging="true" AllowCustomPaging="true" DataKeyNames="ID,EMPLOYEE_CODE"
                ClientDataKeyNames="ID,EMPLOYEE_CODE" CommandItemDisplay="Top">
                <CommandItemStyle Height="28px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="100px" ID="btnEmployee" runat="server" Text="<%$ Translate: Chọn nhân viên %>"
                                CausesValidation="false" CommandName="FindEmployee">
                            </tlk:RadButton>
                        </div>
                    </div>
                </CommandItemTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE">
                        <ItemStyle Width="100px" />
                        <HeaderStyle Width="100px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="VN_FULLNAME"
                        UniqueName="VN_FULLNAME" SortExpression="VN_FULLNAME">
                        <ItemStyle Width="190px" />
                        <HeaderStyle Width="190px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME">
                        <ItemStyle Width="300px" />
                        <HeaderStyle Width="300px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        SortExpression="ORG_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Phép còn lại %>" DataField="BALANCE_NOW"
                        UniqueName="BALANCE_NOW" SortExpression="BALANCE_NOW">
                        <ItemStyle Width="120px" />
                        <HeaderStyle Width="120px" />
                    </tlk:GridNumericColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Nghỉ bù còn lại %>" DataField="NBCL"
                        UniqueName="NBCL" SortExpression="NBCL">
                        <ItemStyle Width="120px" />
                        <HeaderStyle Width="120px" />
                    </tlk:GridNumericColumn>
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlRegisterCONewEdit_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlRegisterCONewEdit_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlRegisterCONewEdit_RadPane2';
        var validateID = 'MainContent_ctrlRegisterCONewEdit_valSum';
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

        function clientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        //mandatory for the RadWindow dialogs functionality
        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }
    </script>
</tlk:RadCodeBlock>

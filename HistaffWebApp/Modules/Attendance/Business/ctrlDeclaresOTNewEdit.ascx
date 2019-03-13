<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDeclaresOTNewEdit.ascx.vb"
    Inherits="Attendance.ctrlDeclaresOTNewEdit" %>
    <link type  = "text/css" href = "/Styles/StyleCustom.css" rel = "Stylesheet"/>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="600px" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="150px">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidDecisionID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidOrgAbbr" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="lb">
                    <%# Translate("Ngày đăng ký")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdregisterDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdregisterDate"
                        runat="server" ErrorMessage="<%$ Translate: Chưa chọn ngày đăng ký làm thêm. %>"
                        ToolTip="<%$ Translate: Chưa chọn ngày đăng ký làm thêm. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Hệ số làm thêm")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbohs_ot" Width="150px" MaxLength="80" runat="server" ToolTip="">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="cbohs_ot"
                        runat="server" ErrorMessage="<%$ Translate: Chưa chọn chọn hệ số làm thêm. %>"
                        ToolTip="<%$ Translate: Chưa chọn chọn hệ số làm thêm. %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ giờ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker ID="txtTuGio" runat="server">
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTuGio"
                        runat="server" ErrorMessage="<%$ Translate: Chưa chọn làm thêm từ giờ. %>" ToolTip="<%$ Translate: Chưa chọn làm thêm từ giờ. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đến giờ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker ID="txtDenGio" runat="server">
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtDenGio"
                        runat="server" ErrorMessage="<%$ Translate: Chưa chọn làm thêm đến giờ. %>" ToolTip="<%$ Translate: Chưa chọn làm thêm đến giờ. %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số giờ nghỉ trưa")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtBreak_Hour" runat="server" SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Nghỉ bù")%>
                </td>
                <td>
                    <asp:CheckBox ID="cboIs_nb" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Lý do làm thêm")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtGhiChu" SkinID="Textbox1023" runat="server" Width="100%" Height = "40px">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgWorkschedule" AllowPaging="true" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID,EMPLOYEE_CODE,EMPLOYEE_ID"
                ClientDataKeyNames="ID,EMPLOYEE_CODE,EMPLOYEE_ID" CommandItemDisplay="Top">
                <CommandItemStyle Height="28px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="100px" ID="btnEmployee" runat="server" Text="<%$ Translate: Chọn nhân viên %>"
                                CausesValidation="false" CommandName="FindEmployee">
                            </tlk:RadButton>
                        </div>
                        <div style="float: right;">
                            <tlk:RadButton Width="100px" ID="btnDeleteEmp" runat="server" Text="<%$ Translate: Xóa %>"
                                CausesValidation="false" CommandName="DeleteEmployee">
                            </tlk:RadButton>
                        </div>
                    </div>
                </CommandItemTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="VN_FULLNAME"
                        UniqueName="VN_FULLNAME" SortExpression="VN_FULLNAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        SortExpression="ORG_NAME" />
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
        var splitterID = 'ctl00_MainContent_ctrlDeclaresOTNewEdit_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlDeclaresOTNewEdit_RadPane2';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlDeclaresOTNewEdit_RadPane3';
        var validateID = 'MainContent_ctrlDeclaresOTNewEdit_valSum';
        var oldSize = $('#' + pane1ID).height();
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
        var enableAjax = true;
        //var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {
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
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        /*function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
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
           */
    </script>
</tlk:RadCodeBlock>

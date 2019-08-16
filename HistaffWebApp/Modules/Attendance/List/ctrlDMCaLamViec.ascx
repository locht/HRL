<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDMCaLamViec.ascx.vb"
    Inherits="Attendance.ctrlDMCaLamViec" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    @media screen and (-webkit-min-device-pixel-ratio:0) {
        #ctl00_MainContent_ctrlDMCaLamViec_txtNote {
            height: 58px;
        }
    }

    #ctl00_MainContent_ctrlDMCaLamViec_cboMaCong_DropDown .rcbWidth,
    #ctl00_MainContent_ctrlDMCaLamViec_cboSunDay_DropDown .rcbWidth,
    #ctl00_MainContent_ctrlDMCaLamViec_cboSaturday_DropDown .rcbWidth {
        width: 178px !important;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="235px" Scrolling="None">
        <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã ca")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" SkinID="Textbox50" runat="server">
                    </tlk:RadTextBox>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã ca làm việc đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã ca làm việc đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCode"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập mã ca làm việc. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên ca làm việc")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNameVN" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNameVN"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tên ca làm việc. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb" style = "display:none">
                    <%# Translate("Mã công")%><span class="lbReq">*</span>
                </td>
                <td style = "display:none">
                    <tlk:RadComboBox ID="cboMaCong" runat="server" Width="180px">
                    </tlk:RadComboBox>
                    <%--<asp:CustomValidator ID="cusMaCong" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn mã công. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn mã công. %>" ClientValidationFunction="cusMaCong">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvalMaCong" ControlToValidate="cboMaCong" runat="server"
                        ErrorMessage="<%$ Translate: Mã công không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Mã công không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>--%>
                </td>
                 <td class="lb">
                    <%# Translate("Công ty")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCongTy" runat="server" Width="180px">
                    </tlk:RadComboBox>
                   
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Giờ bắt đầu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdHours_Start">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt"></DateInput>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rdHours_Start"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập giờ bắt đầu. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Giờ kết thúc")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdHours_Stop">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt"></DateInput>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="rdHours_Stop"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập giờ kết thúc. %>"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="rdHours_Start"
                        ControlToValidate="rdHours_Stop" Operator="GreaterThan"
                        ErrorMessage="<%$ Translate: Thiết lập giờ cho ca làm việc không hợp lệ %>"
                        ToolTip="<%$ Translate: Thiết lập giờ cho ca làm việc không hợp lệ %>">
                    </asp:CompareValidator>
                </td>
                <td class="lb">
                    <asp:CheckBox ID="chkIS_HOURS_STOP" runat="server" />
                </td>
                <td>
                    <%# Translate("Qua ngày hôm sau")%>
                </td>
                <td class="lb">
                    <%# Translate("Ngày công ca")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboNgayCongCa" runat="server" Width="180px">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Bắt đầu nghỉ giữa ca")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdSTART_MID_HOURS">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt"></DateInput>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdSTART_MID_HOURS"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập giờ bắt đầu nghỉ giữa ca. %>"></asp:RequiredFieldValidator>

                </td>
                <td class="lb">
                    <%# Translate("Kết thúc nghỉ giữa ca")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdEND_MID_HOURS">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt"></DateInput>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdEND_MID_HOURS"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập giờ kết thúc nghỉ giữa ca. %>"></asp:RequiredFieldValidator>

                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="rdSTART_MID_HOURS"
                        ControlToValidate="rdEND_MID_HOURS" Operator="GreaterThan"
                        ErrorMessage="<%$ Translate: Thiết lập giờ nghỉ giữa ca không hợp lệ %>"
                        ToolTip="<%$ Translate: Thiết lập giờ nghỉ giữa ca không hợp lệ %>">
                    </asp:CompareValidator>
                </td>
                <td class="lb">
                    <asp:CheckBox ID="chkIS_MID_END" runat="server" />
                </td>
                <td>
                    <%# Translate("Qua ngày hôm sau")%>
                </td>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Bắt đầu nhận quẹt thẻ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdHOURS_STAR_CHECKIN">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt"></DateInput>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="rdHOURS_STAR_CHECKIN"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập giờ bắt đầu nhận quẹt thẻ. %>"></asp:RequiredFieldValidator>

                </td>
                <td class="lb">
                    <%# Translate("Kết thúc nhận quẹt thẻ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdHOURS_STAR_CHECKOUT">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt"></DateInput>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="rdHOURS_STAR_CHECKOUT"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập giờ kết thúc nhận quẹt thẻ. %>">
                    </asp:RequiredFieldValidator>

                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToCompare="rdHOURS_STAR_CHECKIN"
                        ControlToValidate="rdHOURS_STAR_CHECKOUT" Operator="GreaterThan"
                        ErrorMessage="<%$ Translate: Thiết lập giờ quẹt thẻ không hợp lệ %>"
                        ToolTip="<%$ Translate: Thiết lập giờ quẹt thẻ không hợp lệ %>">
                    </asp:CompareValidator>
                </td>
                <td class="lb">
                    <asp:CheckBox ID="chkIS_HOURS_CHECKOUT" runat="server" />
                </td>
                <td>
                    <%# Translate("Qua ngày hôm sau")%>
                </td>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mô tả")%>
                </td>
                <td colspan="7">
                    <tlk:RadTextBox ID="txtNote" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgDanhMuc" runat="server" AutoGenerateColumns="False"
            AllowPaging="True" Height="100%" AllowSorting="True"
            AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME_VN,NAME_EN,MANUAL_NAME,MANUAL_ID,HOURS_START,HOURS_STOP,NOTE,SUNDAY,IS_NOON,SATURDAY,MINHOUSER,ORG_ID,SHIFT_DAY,START_MID_HOURS,END_MID_HOURS,HOURS_STAR_CHECKIN,HOURS_STAR_CHECKOUT,IS_HOURS_STOP,IS_HOURS_CHECKOUT,IS_MID_END,ORG_NAME">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã ca %>" DataField="CODE" UniqueName="CODE"
                        SortExpression="CODE" HeaderStyle-Width="120px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên ca  %>" DataField="NAME_VN" UniqueName="NAME_VN"
                        SortExpression="NAME_VN">
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="130px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã công %>" DataField="MANUAL_CODE"
                        UniqueName="MANUAL_CODE" SortExpression="MANUAL_CODE" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Công ty %>" DataField="ORG_NAME"
                        UniqueName="ORG_ID" SortExpression="ORG_ID" HeaderStyle-Width="200px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày công ca %>" DataField="SHIFT_DAY"
                        UniqueName="SHIFT_DAY" SortExpression="SHIFT_DAY" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả mã công %>" DataField="MANUAL_NAME"
                        UniqueName="MANUAL_NAME" SortExpression="MANUAL_NAME" HeaderStyle-Width="200px" />

                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Giờ bắt đầu %>" DataField="HOURS_START"
                        UniqueName="HOURS_START" DataFormatString="{0:HH:mm}" SortExpression="HOURS_START" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Giờ kết thúc %>" DataField="HOURS_STOP"
                        UniqueName="HOURS_STOP" DataFormatString="{0:HH:mm}" SortExpression="HOURS_STOP" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Qua ngày hôm sau %>" DataField="IS_HOURS_STOP_NAME"
                        UniqueName="IS_HOURS_STOP_NAME" SortExpression="IS_HOURS_STOP_NAME" HeaderStyle-Width="100px" />

                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Bắt đầu nghỉ giữa ca %>" DataField="START_MID_HOURS"
                        UniqueName="START_MID_HOURS" DataFormatString="{0:HH:mm}" SortExpression="START_MID_HOURS" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Kết thúc nghỉ giữa ca %>" DataField="END_MID_HOURS"
                        UniqueName="END_MID_HOURS" DataFormatString="{0:HH:mm}" SortExpression="END_MID_HOURS" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Qua ngày hôm sau %>" DataField="IS_MID_END_NAME"
                        UniqueName="IS_MID_END_NAME" SortExpression="IS_MID_END_NAME" HeaderStyle-Width="100px" />

                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Bắt đầu nhận quyẹt thẻ %>" DataField="HOURS_STAR_CHECKIN"
                        UniqueName="HOURS_STAR_CHECKIN" DataFormatString="{0:HH:mm}" SortExpression="HOURS_STAR_CHECKIN" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Kết thúc nhận quyẹt thẻ %>" DataField="HOURS_STAR_CHECKOUT"
                        UniqueName="HOURS_STAR_CHECKOUT" DataFormatString="{0:HH:mm}" SortExpression="HOURS_STAR_CHECKOUT" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Qua ngày hôm sau %>" DataField="IS_HOURS_CHECKOUT_NAME"
                        UniqueName="IS_HOURS_CHECKOUT_NAME" SortExpression="IS_HOURS_CHECKOUT_NAME" HeaderStyle-Width="100px" />

                    
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG">
                        <HeaderStyle Width="100px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả %>" DataField="NOTE" UniqueName="NOTE"
                        SortExpression="NOTE" />
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlDMCaLamViec_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlDMCaLamViec_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlDMCaLamViec_RadPane2';
        var validateID = 'MainContent_ctrlDMCaLamViec_valSum';
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
                var grid = $find("<%=rgDanhMuc.ClientID %>");
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
            }

            if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc', 0, 0, 7);
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function cusMaCong(oSrc, args) {
            var cbo = $find("<%# cboMaCong.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc', 0, 0, 7);
        }
    </script>
</tlk:RadCodeBlock>

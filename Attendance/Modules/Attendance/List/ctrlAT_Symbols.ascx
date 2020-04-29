<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAT_Symbols.ascx.vb"
    Inherits="Attendance.ctrlAT_Symbols" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />

<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="235px" Scrolling="None">
        <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã ký hiệu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="rtWCODE" SkinID="Textbox50" runat="server">
                    </tlk:RadTextBox>
                    <asp:CustomValidator ID="cvaWCODE" ControlToValidate="rtWCODE" runat="server" ErrorMessage="<%$ Translate: Mã ký hiệu đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã ký hiệu đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RequiredFieldValidator ID="reqWCODE" ControlToValidate="rtWCODE"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập mã ký hiệu. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên ký hiệu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="rtWNAME" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqWNAME" ControlToValidate="rtWNAME"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tên ký hiệu. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Nhóm ký hiệu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox  ID="rcWGROUPID" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqWGROUPID" ControlToValidate="rcWGROUPID"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập nhóm ký hiệu. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                 <td class="lb">
                    <%# Translate("Kiểu dữ liệu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox  ID="rcWDATATYEID" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqWDATATYEID" ControlToValidate="rcWDATATYEID"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập kiểu dữ liệu. %>"></asp:RequiredFieldValidator>
                </td>
                 <td class="lb">
                    <%# Translate("Loại dữ liệu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox  ID="rcWDATAMODEID" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqrcWDATAMODEID" ControlToValidate="rcWDATAMODEID"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập loại dữ liệu. %>"></asp:RequiredFieldValidator>
                </td>
               
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%>
                </td>
                <td>
                    <tlk:RadDatePicker  ID="rdEFFECT_DATE" runat="server" Width="180px">
                    </tlk:RadDatePicker>
                </td>
                 <td class="lb">
                    <%# Translate("Ngày hết hiệu lực")%>
                </td>
                <td>
                    <tlk:RadDatePicker  ID="rdEXPIRE_DATE" runat="server" Width="180px">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Thứ tự hiện thị")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnWINDEX" runat ="server" Width="30"></tlk:RadNumericTextBox>
                </td>
                 <td class="lb">
                    <%# Translate("Hiển thị trên màn hình nghiệp vụ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox  ID="rcSYMBOL_FUN_ID" runat="server" CheckBoxes ="true" >
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:CheckBox ID="ckIS_DISPLAY" runat="server" />
                </td>
                <td>
                    <%# Translate("Hiển thị")%>
                </td>
                 <td class="lb">
                    <asp:CheckBox ID="ckIS_DATAFROMEXCEL" runat="server" />
                </td>
                <td>
                    <%# Translate("Import từ Excel")%>
                </td>
                <td class="lb">
                    <asp:CheckBox ID="ckIS_DISPLAY_PORTAL" runat="server" />
                </td>
                <td>
                    <%# Translate("Hiển thị portal")%>
                </td>
                 <td class="lb">
                    <asp:CheckBox ID="ckIS_LEAVE" runat="server" />
                </td>
                <td>
                    <%# Translate("Công nghỉ")%>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:CheckBox ID="ckIS_LEAVE_WEEKLY" runat="server" />
                </td>
                <td>
                    <%# Translate("Nghỉ hàng tuần")%>
                </td>
                <td class="lb">
                    <asp:CheckBox ID="ckIS_LAVE_HOLIDAY" runat="server" />
                </td>
                <td>
                    <%# Translate("Nghỉ lễ")%>
                </td>
                <td class="lb">
                    <asp:CheckBox ID="ckIS_DAY_HALF" runat="server" />
                </td>
                <td>
                    <%# Translate("Nghỉ nửa ngày")%>
                </td>
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
            AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
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
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="false">
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlAT_Symbols_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlAT_Symbols_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlAT_Symbols_RadPane2';
        var validateID = 'MainContent_ctrlAT_Symbols_valSum';
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

        
        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc', 0, 0, 7);
        }
    </script>
</tlk:RadCodeBlock>

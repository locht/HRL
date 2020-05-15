<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAT_Symbols.ascx.vb"
    Inherits="Attendance.ctrlAT_Symbols" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    #ctl00_MainContent_ctrlAT_Symbols_rgDanhMuc_ctl00_ctl02_ctl02_FilterCheckBox_IS_DISPLAY
    {
        display: none;
    }
    #ctl00_MainContent_ctrlAT_Symbols_rgDanhMuc_ctl00_ctl02_ctl02_FilterCheckBox_IS_DATAFROMEXCEL
    {
        display: none;
    }
     #ctl00_MainContent_ctrlAT_Symbols_rgDanhMuc_ctl00_ctl02_ctl02_FilterCheckBox_IS_DISPLAY_PORTAL
    {
        display: none;
    }
     #ctl00_MainContent_ctrlAT_Symbols_rgDanhMuc_ctl00_ctl02_ctl02_FilterCheckBox_IS_LEAVE
    {
        display: none;
    }
     #ctl00_MainContent_ctrlAT_Symbols_rgDanhMuc_ctl00_ctl02_ctl02_FilterCheckBox_IS_LEAVE_WEEKLY
    {
        display: none;
    }
    #ctl00_MainContent_ctrlAT_Symbols_rgDanhMuc_ctl00_ctl02_ctl02_FilterCheckBox_IS_LEAVE_HOLIDAY
    {
        display: none;
    }
     #ctl00_MainContent_ctrlAT_Symbols_rgDanhMuc_ctl00_ctl02_ctl02_FilterCheckBox_IS_DAY_HALF
    {
        display: none;
    }
    }
</style>
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
                <td class="lb">
                    <%# Translate("Thứ tự hiện thị")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnWINDEX" runat ="server" Width="30"></tlk:RadNumericTextBox>
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
                
               <%--  <td class="lb">
                    <%# Translate("Hiển thị trên màn hình nghiệp vụ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox  ID="rcSYMBOL_FUN_ID" runat="server" CheckBoxes ="true" >
                    </tlk:RadComboBox>
                </td>--%>
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
            </tr>
            <tr>
                 <td class="lb">
                    <asp:CheckBox ID="ckIS_LEAVE" runat="server" />
                </td>
                <td>
                    <%# Translate("Công nghỉ")%>
                </td>
                <td class="lb">
                    <asp:CheckBox ID="ckIS_LEAVE_WEEKLY" runat="server" />
                </td>
                <td>
                    <%# Translate("Được đăng ký cho ngày Nghỉ hàng tuần")%>
                </td>
                <td class="lb">
                    <asp:CheckBox ID="ckIS_LEAVE_HOLIDAY" runat="server" />
                </td>
                <td>
                    <%# Translate("Được đăng ký cho ngày lễ")%>
                </td>
                <td class="lb">
                    <asp:CheckBox ID="ckIS_DAY_HALF" runat="server" />
                </td>
                <td>
                    <%# Translate("Được đăng ký Nghỉ nửa ngày")%>
                </td>
                <td colspan="2">
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
            <MasterTableView DataKeyNames="ID,WCODE,WNAME,WGROUPID, WGROUP_NAME, WDATATYEID, WDATATYE_NAME, WDATAMODEID, WDATAMODE_NAME, EFFECT_DATE, EXPIRE_DATE, WINDEX, NOTE, STATUS, IS_DISPLAY, IS_DATAFROMEXCEL, IS_DISPLAY_PORTAL, IS_LEAVE, IS_LEAVE_WEEKLY, IS_LEAVE_HOLIDAY, IS_DAY_HALF" ClientDataKeyNames="ID,WCODE,WNAME,WGROUPID, WGROUP_NAME, WDATATYEID, WDATATYE_NAME, WDATAMODEID, WDATAMODE_NAME, EFFECT_DATE, EXPIRE_DATE, WINDEX, NOTE, STATUS, IS_DISPLAY, IS_DATAFROMEXCEL, IS_DISPLAY_PORTAL, IS_LEAVE, IS_LEAVE_WEEKLY, IS_LEAVE_HOLIDAY, IS_DAY_HALF">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã ký hiệu %>" DataField="WCODE" UniqueName="WCODE"
                        SortExpression="WCODE" HeaderStyle-Width="120px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên ký hiệu  %>" DataField="WNAME" UniqueName="WNAME"
                        SortExpression="WNAME">
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="130px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm ký hiệu %>" DataField="WGROUP_NAME"
                        UniqueName="WGROUP_NAME" SortExpression="WGROUP_NAME" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kiểu dữ liệu %>" DataField="WDATATYE_NAME"
                        UniqueName="WDATATYE_NAME" SortExpression="WDATATYE_NAME" HeaderStyle-Width="200px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại dữ liệu %>" DataField="WDATAMODE_NAME"
                        UniqueName="WDATAMODE_NAME" SortExpression="WDATAMODE_NAME" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                        UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE"
                        UniqueName="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EXPIRE_DATE" />
                     <tlk:GridNumericColumn  HeaderText="<%$ Translate: Thứ tự hiển thị %>" DataField="WINDEX"
                        UniqueName="WINDEX" SortExpression="WINDEX" HeaderStyle-Width="50px" />

                     <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Hiển thị %>" DataField="IS_DISPLAY" 
                                ItemStyle-VerticalAlign="Middle" UniqueName="IS_DISPLAY" SortExpression="IS_DISPLAY" HeaderStyle-Width="40px"
                                FilterControlWidth="99%" ShowFilterIcon="false"></tlk:GridCheckBoxColumn>
                  <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Dữ liệu import %>" DataField="IS_DATAFROMEXCEL"
                                ItemStyle-VerticalAlign="Middle" UniqueName="IS_DATAFROMEXCEL" SortExpression="IS_DATAFROMEXCEL" HeaderStyle-Width="40px"
                                FilterControlWidth="99%" ShowFilterIcon="false"></tlk:GridCheckBoxColumn>
                  <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Hiển thị portal %>" DataField="IS_DISPLAY_PORTAL"
                                ItemStyle-VerticalAlign="Middle" UniqueName="IS_DISPLAY_PORTAL" SortExpression="IS_DISPLAY_PORTAL" HeaderStyle-Width="40px"
                                FilterControlWidth="99%" ShowFilterIcon="false"></tlk:GridCheckBoxColumn>
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Dữ liệu đăng ký nghỉ %>" DataField="IS_LEAVE"
                                ItemStyle-VerticalAlign="Middle" UniqueName="IS_LEAVE" SortExpression="IS_LEAVE" HeaderStyle-Width="40px"
                                FilterControlWidth="99%" ShowFilterIcon="false"></tlk:GridCheckBoxColumn>
                   <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Được đăng ký cho ngày Nghỉ hàng tuần %>" DataField="IS_LEAVE_WEEKLY"
                                ItemStyle-VerticalAlign="Middle" UniqueName="IS_LEAVE_WEEKLY" SortExpression="IS_LEAVE_WEEKLY" HeaderStyle-Width="40px"
                                FilterControlWidth="99%" ShowFilterIcon="false"></tlk:GridCheckBoxColumn>
                   <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Được đăng ký cho Ngày lễ %>" DataField="IS_LEAVE_HOLIDAY"
                                ItemStyle-VerticalAlign="Middle" UniqueName="IS_LEAVE_HOLIDAY" SortExpression="IS_LEAVE_HOLIDAY" HeaderStyle-Width="40px"
                                FilterControlWidth="99%" ShowFilterIcon="false"></tlk:GridCheckBoxColumn>
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Được đăng ký Nghỉ nửa ngày %>" DataField="IS_DAY_HALF"
                                ItemStyle-VerticalAlign="Middle" UniqueName="IS_DAY_HALF" SortExpression="IS_DAY_HALF" HeaderStyle-Width="40px"
                                FilterControlWidth="99%" ShowFilterIcon="false"></tlk:GridCheckBoxColumn>

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

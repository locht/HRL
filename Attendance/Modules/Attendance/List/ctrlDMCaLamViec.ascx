<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDMCaLamViec.ascx.vb"
    Inherits="Attendance.ctrlDMCaLamViec" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    @media screen and (-webkit-min-device-pixel-ratio:0)
    {
        #ctl00_MainContent_ctrlDMCaLamViec_txtNote
        {
            height: 58px;
        }
    }
    
    #ctl00_MainContent_ctrlDMCaLamViec_cboMaCong_DropDown .rcbWidth, #ctl00_MainContent_ctrlDMCaLamViec_cboSunDay_DropDown .rcbWidth, #ctl00_MainContent_ctrlDMCaLamViec_cboSaturday_DropDown .rcbWidth
    {
        width: 178px !important;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="230px" Scrolling="None">
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
               <%--  <td class="lb">
                    <%# Translate("Tên tiếng anh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEnglishName" runat="server">
                    </tlk:RadTextBox>                    
                </td>--%>

                <td class="lb" >
                    <%# Translate("Kiểu công")%><span class="lbReq">*</span>
                </td>
                <td >
                    <tlk:RadComboBox ID="cboManualType" runat="server" Width="180px">
                    </tlk:RadComboBox>                 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="cboManualType"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn kiểu công. %>"></asp:RequiredFieldValidator>
                </td>             
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Giờ bắt đầu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdHours_Start">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt">
                        </DateInput>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rdHours_Start"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập giờ bắt đầu. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Giờ kết thúc")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdHours_Stop">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt">
                        </DateInput>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="rdHours_Stop"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập giờ kết thúc. %>"></asp:RequiredFieldValidator>                   
                </td> 
                <td class="lb">
                    <%# Translate("Bắt đầu nghỉ giữa ca")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdSTART_MID_HOURS">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt">
                        </DateInput>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdSTART_MID_HOURS"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập giờ bắt đầu nghỉ giữa ca. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Kết thúc nghỉ giữa ca")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdEND_MID_HOURS">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt">
                        </DateInput>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdEND_MID_HOURS"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập giờ kết thúc nghỉ giữa ca. %>"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="rdSTART_MID_HOURS"
                        ControlToValidate="rdEND_MID_HOURS" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Thiết lập giờ nghỉ giữa ca không hợp lệ %>"
                        ToolTip="<%$ Translate: Thiết lập giờ nghỉ giữa ca không hợp lệ %>">
                    </asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb" colspan="3">
                    <%# Translate("Giờ bắt đầu tính về sớm (Áp dụng cho trường hợp nghỉ phép 0.5 ngày)")%>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdStart_Cal_Soon">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt">
                        </DateInput>
                    </tlk:RadTimePicker>                   
                </td>
                 <td class="lb" colspan="3">
                    <%# Translate("Giờ bắt đầu tính đi trễ (Áp dụng cho trường hợp nghỉ phép 0.5 ngày)")%>
                </td>
                <td>
                    <tlk:RadTimePicker runat="server" ID="rdStart_Cal_late">
                        <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt">
                        </DateInput>
                    </tlk:RadTimePicker>                   
                </td>
            </tr>
            <tr>               
                <td class="lb">
                    <%# Translate("Được phép đi trễ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rtxtLATE_MINUTES">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="rtxtLATE_MINUTES"
                    runat="server"  ErrorMessage="<%$ Translate: Bạn phải nhập Số phút. %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Được phép về sớm")%><span class="lbReq">*</span>
                </td>
                <td>
                     <tlk:RadNumericTextBox runat="server" ID="rtxtSOON_MINUTES">
                     </tlk:RadNumericTextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="rtxtSOON_MINUTES"
                     runat="server"  ErrorMessage="<%$ Translate: Bạn phải nhập Số phút. %>">
                     </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Giá trị làm tròn đi trễ")%><span class="lbReq">*</span>
                </td>
                <td>
                     <tlk:RadNumericTextBox runat="server" ID="rtxtValue_Late">
                     </tlk:RadNumericTextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="rtxtValue_Late"
                      runat="server"  ErrorMessage="<%$ Translate: Bạn phải nhập Số phút. %>">
                     </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Giá trị làm tròn về sớm")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rtxtValue_Soon">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="rtxtValue_Soon"
                    runat="server"  ErrorMessage="<%$ Translate: Bạn phải nhập Số phút. %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                 <td class="lb">
                    <asp:CheckBox ID="chkIS_Shift_Night" runat="server" />
                </td>
                <td>
                    <%# Translate("Ca đêm")%>
                </td>
                 <td class="lb">
                    <asp:CheckBox ID="chkIS_Show_Iportal" runat="server" />
                </td>
                <td>
                    <%# Translate("Hiển thị trên Iportal")%>
                </td> 
                <td class="lb">
                    <%# Translate("Mô tả")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtNote" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>

                 <td class="lb">
                    <%# Translate("STT hiển thị ca:")%>
                </td>
                <td >                   
                    <tlk:RadNumericTextBox runat="server" ID="rtxtSTT">
                    </tlk:RadNumericTextBox>
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
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME_VN,MANUAL_TYPE,HOURS_START,HOURS_STOP,START_MID_HOURS,END_MID_HOURS,HOURS_STAR_CHECKIN,HOURS_STAR_CHECKOUT,START_CAL_SOON,START_CAL_LATE,LATE_MINUTES,SOON_MINUTES,VALUE_LATE,VALUE_SOON,IS_SHIFT_NIGHT,IS_SHOW_IPORTAL,ACTFLG,STT,NOTE">
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
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kiểu công %>" DataField="MANUAL_TYPE"
                        UniqueName="MANUAL_TYPE" SortExpression="MANUAL_TYPE" HeaderStyle-Width="100px" />                   
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Giờ bắt đầu %>" DataField="HOURS_START"
                        UniqueName="HOURS_START" DataFormatString="{0:HH:mm}" SortExpression="HOURS_START" HeaderStyle-Width="100px" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Giờ kết thúc %>" DataField="HOURS_STOP"
                        UniqueName="HOURS_STOP" DataFormatString="{0:HH:mm}" SortExpression="HOURS_STOP" HeaderStyle-Width="100px"/>                
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Bắt đầu nghỉ giữa ca %>" DataField="START_MID_HOURS"
                        UniqueName="START_MID_HOURS" DataFormatString="{0:HH:mm}" SortExpression="START_MID_HOURS" HeaderStyle-Width="100px"/>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Kết thúc nghỉ giữa ca %>" DataField="END_MID_HOURS"
                        UniqueName="END_MID_HOURS" DataFormatString="{0:HH:mm}" SortExpression="END_MID_HOURS" HeaderStyle-Width="100px"/>                  
                  <%--  <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Bắt đầu nhận quyẹt thẻ %>" DataField="HOURS_STAR_CHECKIN"
                        UniqueName="HOURS_STAR_CHECKIN" DataFormatString="{0:HH:mm}" SortExpression="HOURS_STAR_CHECKIN" HeaderStyle-Width="100px"/>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Kết thúc nhận quyẹt thẻ %>" DataField="HOURS_STAR_CHECKOUT"
                        UniqueName="HOURS_STAR_CHECKOUT" DataFormatString="{0:HH:mm}" SortExpression="HOURS_STAR_CHECKOUT" HeaderStyle-Width="100px"/>--%>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Giờ bắt đầu tính về sớm (Áp dụng cho trường hợp nghỉ phép 0.5 ngày) %>" DataField="START_CAL_SOON"
                        UniqueName="START_CAL_SOON" DataFormatString="{0:HH:mm}" SortExpression="START_CAL_SOON" HeaderStyle-Width="200px"/>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Giờ bắt đầu tính đi trễ (Áp dụng cho trường hợp nghỉ phép 0.5 ngày) %>" DataField="START_CAL_LATE"
                        UniqueName="START_CAL_LATE" DataFormatString="{0:HH:mm}" SortExpression="START_CAL_LATE" HeaderStyle-Width="200px"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Được phép đi trễ %>" DataField="LATE_MINUTES"
                        UniqueName="LATE_MINUTES" SortExpression="LATE_MINUTES" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Được phép về sớm %>" DataField="SOON_MINUTES"
                        UniqueName="SOON_MINUTES" SortExpression="SOON_MINUTES" HeaderStyle-Width="100px" />
                     <tlk:GridBoundColumn HeaderText="<%$ Translate: Giá trị làm tròn đi trễ %>" DataField="VALUE_LATE"
                        UniqueName="VALUE_LATE" SortExpression="VALUE_LATE" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Giá trị làm tròn về sớm %>" DataField="VALUE_SOON"
                        UniqueName="VALUE_SOON" SortExpression="VALUE_SOON" HeaderStyle-Width="100px" />
                     <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Ca đêm %>" DataField="IS_SHIFT_NIGHT" 
                        AllowFiltering ="false" FooterStyle-HorizontalAlign="Center"
                                    SortExpression="IS_SHIFT_NIGHT" UniqueName="IS_SHIFT_NIGHT">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                     </tlk:GridCheckBoxColumn>
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Hiển thị trên Iportal %>" DataField="IS_SHOW_IPORTAL" 
                        AllowFiltering ="false" FooterStyle-HorizontalAlign="Center"
                                    SortExpression="IS_SHOW_IPORTAL" UniqueName="IS_SHOW_IPORTAL">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                     </tlk:GridCheckBoxColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG">
                        <HeaderStyle Width="100px" />
                    </tlk:GridBoundColumn>
                     <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả %>" DataField="NOTE" UniqueName="NOTE"
                        SortExpression="NOTE" HeaderStyle-Width="200px"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: STT hiển thị ca %>" DataField="STT" UniqueName="STT"
                        SortExpression="STT" HeaderStyle-Width="50px"/>
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
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
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
              
        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc', 0, 0, 7);
        }
    </script>
</tlk:RadCodeBlock>

<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRegisterCO.ascx.vb"
    Inherits="Attendance.ctrlRegisterCO" %>
<%@ Import Namespace="Common" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None" Height="35px">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="80px">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" AutoPostBack="true"
                                TabIndex="12" Width="80px">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỳ công")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriod" SkinID="dDropdownList" AutoPostBack="true" Width="150px"
                                MaxLength="80" runat="server" ToolTip="">
                            </tlk:RadComboBox>
                        </td>
                        <td colspan="2">
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Nhân viên nghỉ việc %>" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdtungay" MaxLength="12" SkinID="Readonly" runat="server"
                                ToolTip="">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdDenngay" MaxLength="12" SkinID="Readonly" runat="server"
                                ToolTip="" Width="150px">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Trạng thái")%>
                        </td>
                        <td>
                             <tlk:RadComboBox  ID="cbStatus" Width="160px"
                               runat="server" >
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip="" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgRegisterLeave" runat="server" Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <%--<ClientEvents OnGridCreated="GridCreated" />--%>
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ID,EMPLOYEE_CODE">
                        <Columns>
                            <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="VN_FULLNAME"
                                SortExpression="VN_FULLNAME" UniqueName="VN_FULLNAME" HeaderStyle-Width="120px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" HeaderStyle-Width="120px"
                                DataField="TITLE_NAME" SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" HeaderStyle-Width="200px"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự %>" DataField="STAFF_RANK_NAME"
                                SortExpression="STAFF_RANK_NAME" UniqueName="STAFF_RANK_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số phép còn lại %>" DataField="BALANCE_NOW"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="BALANCE_NOW"
                                UniqueName="BALANCE_NOW">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số nghỉ bù còn lại %>" DataField="NGHIBUCONLAI"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="NGHIBUCONLAI"
                                UniqueName="NGHIBUCONLAI">
                            </tlk:GridNumericColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Nghỉ từ ngày %>" DataField="LEAVE_FROM"
                                UniqueName="LEAVE_FROM" DataFormatString="{0:dd/MM/yyyy}" SortExpression="LEAVE_FROM">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Nghỉ đến ngày %>" DataField="LEAVE_TO"
                                UniqueName="LEAVE_TO" DataFormatString="{0:dd/MM/yyyy}" SortExpression="LEAVE_TO">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày %>" DataField="DAY_NUM"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="DAY_NUM"
                                UniqueName="DAY_NUM">
                            </tlk:GridNumericColumn>
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate:Ngày làm việc %>" DataField="IS_WORKING_DAY" DataType="System.Boolean" FilterControlWidth="20px"   
                                SortExpression="IS_WORKING_DAY" UniqueName="IS_WORKING_DAY" >
                                <HeaderStyle HorizontalAlign="Center" Width="50px"/>
                            </tlk:GridCheckBoxColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày trong kế hoạch %>" DataField="IN_PLAN_DAYS"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="IN_PLAN_DAYS"
                                UniqueName="IN_PLAN_DAYS">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày ngoài kế hoạch %>" DataField="NOT_IN_PLAN_DAYS"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="NOT_IN_PLAN_DAYS"
                                UniqueName="NOT_IN_PLAN_DAYS">
                            </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kiểu công %>" DataField="MANUAL_NAME"
                                SortExpression="MANUAL_NAME" UniqueName="MANUAL_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kiểu công nửa ngày %>" DataField="MORNING_NAME"
                                SortExpression="MORNING_NAME" UniqueName="MORNING_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kiểu công nửa ngày %>" DataField="AFTERNOON_NAME"
                                SortExpression="AFTERNOON_NAME" UniqueName="AFTERNOON_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người phê duyệt %>" HeaderStyle-Width="200px"
                                DataField="NOTE" SortExpression="NOTE" UniqueName="NOTE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi Chú %>" HeaderStyle-Width="200px"
                                DataField="NOTE_APP" SortExpression="NOTE_APP" UniqueName="NOTE_APP" />--%>
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="500" Height="500px"
            OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin chi tiết nhân viên%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">

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
        //            registerOnfocusOut('ctl00_MainContent_ctrlRegisterCO_RadSplitter3');
        //        }

        function gridRowDblClick(sender, eventArgs) {
            OpenEditWindow("Normal");
        }

        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenInsertWindow();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%= rgRegisterLeave.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else if (bCheck > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                } else {
                    OpenEditWindow();
                    args.set_cancel(true);
                }
            }
            else if (args.get_item().get_commandName() == 'EXPORT') {
                var rows = $find('<%= rgRegisterLeave.ClientID %>').get_masterTableView().get_dataItems().length;
                if (rows == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            }
        }

        function OpenInsertWindow() {
            var m;
            var cbo = $find("<%# cboPeriod.ClientID %>");
            var periodID = cbo.get_value();
            if (periodID.length = 0) {
                m = '<%# Translate("Bạn phải chọn kỳ công.") %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            window.open('/Default.aspx?mid=Attendance&fid=ctrlRegisterCONewEdit&group=Business&FormType=0&periodid=' + periodID, "_self"); /*
            oWindow.setSize(800, 480);
            oWindow.center(); */
        }

        function OpenEditWindow() {
            var grid = $find('<%# rgRegisterLeave.ClientID %>');
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            var id = 0
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            if (gridSelected != "") {
                id = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            }
            if (id > 0) {
                var m;
                var cbo = $find("<%# cboPeriod.ClientID %>");
                var periodID = cbo.get_value();
                if (periodID.length = 0) {
                    m = '<%# Translate("Bạn phải chọn kỳ công.") %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    return;
                }
                window.open('/Default.aspx?mid=Attendance&fid=ctrlRegisterCONewEdit&group=Business&VIEW=TRUE&FormType=0&ID=' + id + '&periodid=' + periodID, "_self"); /*
                oWindow.setSize(800, 480);
                oWindow.center(); */
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnClientClose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgRegisterLeave.ClientID %>").get_masterTableView().rebind();
            }
        }
    </script>
</tlk:RadScriptBlock>

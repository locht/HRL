<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTimeTimesheet_machine.ascx.vb"
    Inherits="Attendance.ctrlTimeTimesheet_machine" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link type  = "text/css" href = "/Styles/StyleCustom.css" rel = "Stylesheet"/>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane2" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="80px" Scrolling="None">
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
                            <tlk:RadComboBox ID="cboPeriod" SkinID="dDropdownList" Width="160px" AutoPostBack="true"
                                MaxLength="80" runat="server" ToolTip="">
                            </tlk:RadComboBox>
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
                                ToolTip="">
                            </tlk:RadDatePicker>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip=""
                                SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgTimeTimesheet_machine" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_CODE,EMPLOYEE_ID,ORG_DESC" ClientDataKeyNames="ID,EMPLOYEE_CODE,EMPLOYEE_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px" SortExpression="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="VN_FULLNAME"
                                UniqueName="VN_FULLNAME" HeaderStyle-Width="120px" SortExpression="VN_FULLNAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" HeaderStyle-Width="120px" SortExpression="TITLE_NAME" />
                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME">
                                <HeaderStyle Width="200px" />
                                <ItemTemplate>
                                 <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ORG_NAME") %>'>
                                </asp:Label>
                                <tlk:RadToolTip RenderMode="Lightweight" ID="RadToolTip1" runat="server" TargetControlID="Label1"
                                                    RelativeTo="Element" Position="BottomCenter">
                                <%# DrawTreeByString(DataBinder.Eval(Container, "DataItem.ORG_DESC"))%>
                                </tlk:RadToolTip>
                            </ItemTemplate>
                         </tlk:GridTemplateColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày làm việc %>" DataField="WORKINGDAY"
                                UniqueName="WORKINGDAY" DataFormatString="{0:dd/MM/yyyy}" SortExpression="WORKINGDAY">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca làm việc %>" DataField="SHIFT_CODE"
                                UniqueName="SHIFT_CODE" SortExpression="SHIFT_CODE" HeaderStyle-Width="100px" />
                           <%-- <tlk:GridNumericColumn HeaderText="<%$ Translate:Tổng giờ công %>" DataField="WORKINGHOUR"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="WORKINGHOUR"
                                UniqueName="WORKINGHOUR" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã công%>" DataField="MANUAL_CODE"
                                UniqueName="MANUAL_CODE" SortExpression="MANUAL_CODE" HeaderStyle-Width="100px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>--%>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Giờ vào %>" DataField="SHIFTIN"
                                UniqueName="SHIFTIN" PickerType="TimePicker" AllowFiltering="false" DataFormatString="{0:HH:mm}"
                                SortExpression="SHIFTIN">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Giờ ra %>" DataField="SHIFTBACKOUT"
                                UniqueName="SHIFTBACKOUT" PickerType="TimePicker" AllowFiltering="false" DataFormatString="{0:HH:mm}"
                                SortExpression="SHIFTBACKOUT">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng chấm công %>" DataField="OBJECT_ATTENDANCE_NAME"
                                UniqueName="OBJECT_ATTENDANCE_NAME" HeaderStyle-Width="120px" SortExpression="OBJECT_ATTENDANCE_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Đi muộn(Phút) %>" DataField="LATE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="LATE"
                                UniqueName="LATE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số phút ở cơ quan %>" DataField="LATE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="LATE"
                                UniqueName="LATE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số phút ngoài cơ quan %>" DataField="LATE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="LATE"
                                UniqueName="LATE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số phút giảm trừ do đi việc công %>" DataField="LATE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="LATE"
                                UniqueName="LATE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Đi muộn(Phút) %>" DataField="LATE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="LATE"
                                UniqueName="LATE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Đi muộn(Phút) %>" DataField="LATE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="LATE"
                                UniqueName="LATE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Đi muộn(Phút) %>" DataField="LATE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="LATE"
                                UniqueName="LATE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Đi muộn(Phút) %>" DataField="LATE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="LATE"
                                UniqueName="LATE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Đi muộn(Phút) %>" DataField="LATE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="LATE"
                                UniqueName="LATE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <%--<tlk:GridDateTimeColumn HeaderText="<%$ Translate: Vào giữa %>" DataField="SHIFTBACKIN"
                                UniqueName="SHIFTBACKIN" PickerType="TimePicker" AllowFiltering="false" DataFormatString="{0:HH:mm}"
                                SortExpression="SHIFTBACKIN">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ra cuối %>" DataField="SHIFTOUT"
                                UniqueName="SHIFTOUT" PickerType="TimePicker" AllowFiltering="false" DataFormatString="{0:HH:mm}"
                                SortExpression="SHIFTOUT">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>--%>
                            <%--<tlk:GridNumericColumn HeaderText="<%$ Translate: Đi muộn(Phút) %>" DataField="LATE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="LATE"
                                UniqueName="LATE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Về sớm(phút) %>" DataField="COMEBACKOUT"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n1}" SortExpression="COMEBACKOUT"
                                UniqueName="COMEBACKOUT" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>--%>
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
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin chi tiết nhân viên%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var splitterID = 'ctl00_MainContent_ctrlTimeTimesheet_machine_RadSplitter3';
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
        function gridRowDblClick(sender, eventArgs) {
            OpenEditWindow("Normal");
        }

        function OpenEditWindow(states) {


        }

        function OpenInsertWindow() {


        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%=rgTimeTimesheet_machine.ClientID %>");
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
        }

        function OnClientClose(oWnd, args) {
            postBack(oWnd.get_navigateUrl());
        }

        function postBack(url) {
            var ajaxManager = $find("<%= AjaxManagerId %>");
            ajaxManager.ajaxRequest(url); //Making ajax request with the argument
        }

    </script>
</tlk:RadScriptBlock>
